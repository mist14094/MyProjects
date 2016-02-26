/********************************************************************************************************
Copyright (c) 2005 KeyTone Technologies.All Right Reserved

KeyTone's source code and documentation is copyrighted and contains proprietary information.
Licensee shall not modify, create any derivative works, make modifications, improvements, 
distribute or reveal the source code ("Improvements") to anyone other than the software 
developers of licensee's organization unless the licensee has entered into a written agreement
("Agreement") to do so with KeyTone Technologies Inc. Licensee hereby assigns to KeyTone all right,
title and interest in and to such Improvements unless otherwise stated in the Agreement. Licensee 
may not resell, rent, lease, or distribute the source code alone, it shall only be distributed in 
compiled component of an application within the licensee'organization. 
The licensee shall not resell, rent, lease, or distribute the products created from the source code
in any way that would compete with KeyTone Technologies Inc. KeyTone' copyright notice may not be 
removed from the source code.
   
Licensee may be held legally responsible for any infringement of intellectual property rights that
is caused or encouraged by licensee's failure to abide by the terms of this Agreement. Licensee may 
make copies of the source code provided the copyright and trademark notices are reproduced in their 
entirety on the copy. KeyTone reserves all rights not specifically granted to Licensee. 
 
Use of this source code constitutes an agreement not to criticize, in any way, the code-writing style
of the author, including any statements regarding the extent of documentation and comments present.

THE SOFTWARE IS PROVIDED "AS IS" BASIS, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING 
BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER  LIABILITY, 
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE
OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. KEYTONE TECHNOLOGIES SHALL NOT BE LIABLE FOR ANY DAMAGES 
SUFFERED BY LICENSEE AS A RESULT OF USING, MODIFYING OR DISTRIBUTING THIS SOFTWARE OR ITS DERIVATIVES.
********************************************************************************************************/

using System;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Threading;
using System.Runtime.Remoting;

using KTone.RFIDGlobal;
using KTone.RFIDGlobal.ImportData;
using KTone.RFIDGlobal.CSVReader;
using System.Data;
using System.Text;
using System.Web.Mail;
using System.Text.RegularExpressions;

using NLog;
using System.Globalization;


namespace KTone.RFIDGlobal.ImportData
{
    /// <summary>
    /// This enum defines all the fieldTypes used in the config file under the FieldNodes.
    /// </summary>
    public enum FieldTypes
    {
        /// <summary>
        /// Index
        /// </summary>
        Index,
        /// <summary>
        /// Custom
        /// </summary>
        Custom,
        /// <summary>
        /// Default
        /// </summary>
        Default,
        /// <summary>
        /// IndexCustom
        /// </summary>
        IndexCustom,
        /// <summary>
        /// DefaultIndexCustom
        /// </summary>
        DefaultIndexCustom,
    }

    public enum TableTypes
    {
        /// <summary>
        /// Primary
        /// </summary>
        Primary,

        /// <summary>
        /// Secondary
        /// </summary>
        Secondary
    }

    /// <summary>
    /// Stores information of a particular field in a flat file.
    /// </summary>
    [Serializable]

    public struct FieldInfo
    {
        /// <summary>
        /// Name of a field as defined in config file.
        /// </summary>
        public string fieldName;
        /// <summary>
        ///  Value of a field as in a flat file
        /// </summary>
        //public string fieldValue;
        /// <summary>
        /// Indicates whether the current field is a key column or not.
        /// </summary>
        public bool isKeyCol;
        /// <summary>
        /// This is one of the Attribute of FieldNode in the ConfigFile.
        /// </summary>
        public bool isOptional;
        /// <summary>
        /// This is one of the Attribute of FieldNode in the ConfigFile.
        /// </summary>
        public FieldTypes fieldType;
        /// <summary>
        /// This is value of the Index Attribute under the FieldNode in the ConfigFile.
        /// </summary>
        public int indexValue;
        /// <summary>
        /// This is value of the Custom Attribute under the FieldNode in the ConfigFile.
        /// </summary>
        public string customValue;
        /// <summary>
        /// This is value of the FileFieldName Attribute under the FieldNode in the ConfigFile.
        /// </summary>
        public string FileFieldName;
        /// <summary>
        /// This is value of the Default Attribute under the FieldNode in the ConfigFile.
        /// </summary>
        public string defaultValue;
        /// <summary>
        /// This is one of the Attribute of FieldNode in the ConfigFile.
        /// </summary>
        public bool DBInsertReq;

        /// <summary>
        /// If update required is false then data won't be updated during import for this field
        /// </summary>
        public bool DBUpdateReq;

    }

    /// <summary>
    /// Stores information of a particular table and its fields.
    /// </summary>
    [Serializable]
    public struct TableInfo
    {
        public string tableName;
        public FieldInfo[] fieldDetails;
        public TableTypes tableType;
        public bool updateData;
    }


    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    [Serializable]
    public class ImportData : MarshalByRefObject, IImportData
    {
        /// <summary>
        /// By default it should be true to delete the processed files from the 
        /// source directory. Only Import Order/Container components will make it 
        /// false based on file type whether it is SAP/AS4 file.
        /// -----------------
        /// If both SAP/AS4 files are having the same folder as a source then 
        /// components are deleting the files from the source and the files 
        /// are not available to other component.
        /// -----------------
        /// If SAP compo is trying import AS4 file it will mark it false
        /// If AS4 compo is trying import SAP file it will mark it false
        /// otherwise it will be true.
        /// </summary>
        protected bool mMarkFileAsProcessed = true;

        #region Attributes

        DBTransact m_objDBTrans = null;
        // Used by derived class to find out if it has SHIPPED in it to mark order as it as shipped 
        protected string m_CurrentFileName = String.Empty;
        private static readonly Logger m_Log
          = KTone.RFIDGlobal.KTLogger.KTLogManager.GetLogger();
        protected Logger customImportLog = m_Log;
        protected Importer m_ImporterType;
        protected bool m_IsEnabled = false;
        protected string m_ConfigFile = string.Empty;
        protected string m_XSDFile = string.Empty;

        private Hashtable m_ClientIdLogFileListHash = new Hashtable();
        private int m_ClientId = 0;
        protected object m_ProcessFileLock = new Object();
        protected bool m_IsProcessFileWorking = false;
        protected FileUploader m_FileUploader = null;
        protected bool m_AcceptDuplicateHeaders = false;

        public event ImportDelHandler m_ImportEvent;
        private object OnImportEventLockObj = new object();

        protected static ImportData m_Singleton = null;

        protected string m_PendingFileDir = string.Empty;
        protected string m_FlatFileDir = string.Empty;
        protected string m_ProcessedFileDir = string.Empty;
        protected string m_ErrorFileDir = string.Empty;
        protected string m_ArchiveFileDir = string.Empty;
        protected string m_ShadowCopies = string.Empty;
        protected bool m_ArchiveEnabled = true;

        protected TableInfo[] m_TableList = null;
        protected ArrayList m_FileFieldList = null;
        protected string m_ConnString = string.Empty;
        protected double m_ImportInterval = 0;
        protected int m_FileCleanupInterval = 0;
        protected ArrayList m_ProcessedRecArr = new ArrayList();
        protected ArrayList m_ErrorRecArr = new ArrayList();
        protected ArrayList m_ErrorRecArrAndLog = new ArrayList();
        protected ArrayList m_WarningRecArr = new ArrayList();
        protected int m_MaxErrorRecordCnt = 50;
        protected string[] m_AcceptFilesCondns = null;
        protected string[] m_IgnoreFilesCondns = null;
        public string PENDINGEXTENSION = ".pending"; // extension used for the pending files
        public string PENDINGDATEFORMAT = "dd-MMM-yyyy";
        protected ArrayList m_PendingRecArr = new ArrayList();
        protected double m_ImpStartTime = 0;
        protected double m_ImpEndTime = 0;

        protected DateTime m_ImportStartTime = DateTime.Now;
        protected DateTime m_ImportEndTime = DateTime.Now;

        protected Int16 m_Filter = 0;
        protected Int16 m_ExtensionDigit = 0;
        protected string m_URNStd = string.Empty;
        protected string m_SGTINFilter = string.Empty;
        protected bool m_SortOnFileName = false;


        protected int m_ErrRecCount = 0;
        protected int m_ImportedRecCount = 0;
        protected int m_RecExistCount = 0;
        protected int m_RecInsertedCount = 0;
        protected int m_PendingFileRetentionDays = 4;
        protected int m_MaxRecProcessCnt = 50;
        protected int m_FieldCnt = 0;
        //public static string FieldSequence;
        protected bool m_FieldHeaderPresent = false;
        protected char m_FieldSeperator;
        protected char m_FieldQuote;//quotation character wrapping every field
        private Thread m_ImportThread = null;

        //Log File EMail Notification
        protected string m_SmtpServerName = string.Empty;
        //private string UserName = string.Empty;
        //private string Password = string.Empty;
        protected string m_From = string.Empty;
        protected string m_To = string.Empty;
        protected string m_Bcc = string.Empty;
        protected string m_CC = string.Empty;
        protected double m_NotifyTime = 0;
        protected DateTime m_NotificationTime = DateTime.Now;

        // Process only one loop at a time 
        bool m_processonce_lock = false;

        private string mDBServerName = string.Empty;
        private string mDatabaseName = string.Empty;
        private string mUserID = string.Empty;
        private string mPassword = string.Empty;


        private DateTime m_LastNotifiedOn = DateTime.Parse("1 Jan 1900 12:00:00 AM");

        private string configStr = "";

        private string m_BasePath = "";
        string importerName = string.Empty;
        #endregion Attributes

        #region Constructor
        protected ImportData()
        {
        }


        public static ImportData GetInstance()
        {
            if (m_Singleton == null)
                m_Singleton = new ImportData();
            return m_Singleton;
        }

        protected void InitCtor(string configString)
        {
            //string basePath = AppDomain.CurrentDomain.BaseDirectory;
            //if(!basePath.EndsWith(@"\"))
            //    basePath += @"\";

            //m_ConfigFile  = basePath + m_ConfigFile;  

            //if(m_XSDFile != string.Empty)//Temp implemented only for Orderdetails.
            //    m_XSDFile = basePath + @"XSD\ImportData\" + m_XSDFile;

            ReadConfigParams(configString);
            try
            {
                m_FileUploader = FileUploader.GetInstance(m_FlatFileDir);
            }
            catch (Exception ex)
            {
                customImportLog.Error("ImportData:InitCtor:Error in initializing fileuploader", ex);
            }
        }


        #endregion Constructor

        #region Public Methods

        public event ImportDelHandler OnImportEvent
        {
            add
            {
                lock (OnImportEventLockObj)
                {
                    m_ImportEvent += value;
                }
            }
            remove
            {
                lock (OnImportEventLockObj)
                {
                    m_ImportEvent -= value;
                }
            }
        }


        public string IsWorking()
        {
            return "Yes I am available.";
        }


        public override object InitializeLifetimeService()
        {
            return null;
        }


        #endregion Public Methods

        #region Private Methods
        private void Start()
        {
            try
            {
                Stop();
                ReadConfigParams(configStr);
                m_ImportThread = new Thread(new ThreadStart(ProcessDirectory));
                m_ImportThread.Name = "ImportDataThread";
                m_ImportThread.Start();
                m_IsEnabled = true;
                customImportLog.Trace("Start():Thread is started.");
            }
            catch (Exception ex)
            {
                customImportLog.Error("Start():Error in starting the Thread.", ex);
                m_IsEnabled = false;
                throw new RemotingException("Error in starting");
            }
        }

        private void Stop()
        {
            try
            {
                m_IsEnabled = false;
                if (m_ImportThread != null)
                {
                    m_ImportThread.Abort();
                    m_ImportThread.Join(10000);
                    m_ImportThread = null;
                    customImportLog.Trace("Stop():Thread is stopped.");
                }
            }
            catch (Exception ex)
            {
                customImportLog.Error("Stop():Errror stopping the Thread.", ex);
                m_IsEnabled = false;

            }

        }



        /// <summary>
        /// Loops through all the file in a flatFile dir (as mentioned in config file)
        /// and processes each file to import the data into DB 
        /// </summary>
        private void ProcessDirectory()
        {
            while (true)
            {
                try
                {
                    double totalMinStart = DateTime.Now.TimeOfDay.TotalMinutes;
                    if (totalMinStart >= m_ImpStartTime && totalMinStart < m_ImpEndTime) //If the curr time is between start-end time
                    {
                        customImportLog.Trace("ImportData::ProcessDirectory=> Start Processing FlatFile Dir at " + DateTime.Now.ToString());

                        if (!Directory.Exists(m_FlatFileDir))
                            Directory.CreateDirectory(m_FlatFileDir);
                        if (m_PendingFileDir != string.Empty)
                        {
                            if (!Directory.Exists(m_PendingFileDir))
                                Directory.CreateDirectory(m_PendingFileDir);
                        }
                        if (!Directory.Exists(m_ProcessedFileDir))
                            Directory.CreateDirectory(m_ProcessedFileDir);
                        if (!Directory.Exists(m_ErrorFileDir))
                            Directory.CreateDirectory(m_ErrorFileDir);
                        if (!Directory.Exists(m_ArchiveFileDir))
                            Directory.CreateDirectory(m_ArchiveFileDir);

                        CleanUpProcessedDir();
                        DirectoryInfo objFileDir = new DirectoryInfo(m_FlatFileDir);
                        //						bool anyFileExist = false;
                        //						if(objFileDir.GetFiles().Length > 0)
                        //							anyFileExist = true;
                        //						else
                        {
                            //Send Log Files of Emported Data=====================
                            if ((m_LastNotifiedOn.DayOfYear < System.DateTime.Today.DayOfYear)
                                && (m_NotifyTime <= System.DateTime.Now.TimeOfDay.TotalMinutes))
                            {
                                try
                                {
                                    SendLogEmail();
                                    m_LastNotifiedOn = System.DateTime.Today;
                                }
                                catch (Exception exp)
                                {
                                    customImportLog.Error("ImportData:SendLogEmail()", exp);
                                }
                            }
                        }
                        // Process the pending files first and then process regular files

                        if (m_PendingFileDir != string.Empty)
                        {
                            SortAndProcessPendingFiles(new DirectoryInfo(m_PendingFileDir));
                        }
                        //Process file based on name 
                        SortAndProcessFiles(objFileDir);
                        //===========================

                        customImportLog.Trace("ImportData::ProcessDirectory=> All files processed at " + DateTime.Now.ToString() + " and thread is sleeping for: " + m_ImportInterval.ToString() + " Min.");
                    }
                    else
                        customImportLog.Trace("ImportData::ProcessDirectory=> ImportData is not initiated as the current time do not fall in between the start and end time.");

                    //If the processing takes more time than the interval then there wont be sleep.
                    double totalMinEnd = DateTime.Now.TimeOfDay.TotalMinutes;
                    if ((totalMinEnd - totalMinStart) < m_ImportInterval)
                    {
                        //DBTransact objDBTrans = DBTransact.GetInstance("");
                        DBTransact objDBTrans = new DBTransact("");
                        objDBTrans.RemoveInstance();
                        System.Threading.Thread.Sleep(TimeSpan.FromMinutes(m_ImportInterval));
                    }
                    else
                        System.Threading.Thread.Sleep(5000); //
                }
                catch (ThreadAbortException tex)
                {
                    customImportLog.Error("Thread Aborted: " + tex.Message);
                }
                catch (Exception ex)
                {
                    customImportLog.Error("ImportData:ProcessDirectory=>" + ex.Message, ex);
                    System.Threading.Thread.Sleep(5000);
                }
            }
        }

        // There will be a files ending with extension PENDINGEXTENSION
        // in the pending folder and we have. Also they will have DD-MON-YYYY 
        // attached to their end 
        // This routine will extract the date from file name and if past the 
        // pending retension date , it will move them to error folder. 
        private void SortAndProcessPendingFiles(DirectoryInfo pendFileDir)
        {
            // The previosu process is still going on 
            // ignore the current request 
            if (m_processonce_lock)
                return;
            try
            {
                m_processonce_lock = true;
                Hashtable fileHash = new Hashtable();
                // Get .pending file from the pending folder 
                foreach (FileInfo file in pendFileDir.GetFiles("*" + PENDINGEXTENSION))
                {
                    int extLen = PENDINGEXTENSION.Length;
                    int dateFormatLength = PENDINGDATEFORMAT.Length; // DD-MMM-YYYY

                    int startingOfdateString = file.Name.Length - extLen - dateFormatLength;
                    if (startingOfdateString <= 0)
                    {
                        // log and continue as somthing has gone wrong and somone has put wrong file in the folder
                        continue;
                    }
                    string dateOfCreation = file.Name.Substring(startingOfdateString, dateFormatLength);
                    if (string.IsNullOrEmpty(dateOfCreation) || dateOfCreation.Length != dateFormatLength)
                    {
                        // log and continue as somthing has gone wrong and somone has put wrong file in the folder
                        continue;
                    }
                    string orgFileName = file.Name.Substring(0, startingOfdateString);
                    DateTime fileDate = DateTime.ParseExact(dateOfCreation, PENDINGDATEFORMAT, null);
                    DateTime todayDate = DateTime.Today;

                    // If the pending file date is still less that # of retention days 
                    // process it again and again
                    if (DateTime.Compare(todayDate, fileDate.AddDays(m_PendingFileRetentionDays)) < 0)
                    {
                        customImportLog.Info(" ImportData::SortAndProcessPendingFiles : Retrying the  file from pending folder " + file.Name);
                        // On certain system the  system you will find multiple files with same tick
                        try
                        {
                            long tTicks = file.LastWriteTime.Ticks;
                            while (fileHash.ContainsKey(tTicks))
                                tTicks += 1;
                            fileHash[tTicks] = file;

                        }
                        catch { }

                    }
                    // Retention days are over so move the pending file to error file 
                    // folder directly. 
                    else
                    {
                        // log 
                        try
                        {
                            FileInfo fileInfo = new FileInfo(file.FullName);
                            DateTime lastWriteTime = fileInfo.LastWriteTime;
                            string month = lastWriteTime.ToString("MMMM");
                            int year = lastWriteTime.Year;
                            string folderName = month + "_" + year;
                            string errFilePath = m_ErrorFileDir + @"\" + folderName;
                            if (!Directory.Exists(errFilePath))
                                Directory.CreateDirectory(errFilePath);
                            errFilePath = errFilePath + @"\" + file.Name;

                            StreamReader strReader = new StreamReader(file.FullName);

                            while (!strReader.EndOfStream)
                            {
                                string errRec = strReader.ReadLine();
                                m_ErrorRecArr.Add(errRec);
                                m_ErrRecCount++;
                            }
                            string fileImpStart = DateTime.Now.ToString();

                            int startingdateString = file.Name.Length - PENDINGEXTENSION.Length - PENDINGDATEFORMAT.Length;
                            string fileName = file.Name.Substring(0, startingdateString);
                            LogImportDetail(fileName, fileImpStart);

                            try
                            {
                                if (strReader != null)
                                {
                                    strReader.Close();
                                    strReader = null;
                                }
                            }
                            catch (Exception) { }
                            File.Move(file.FullName, m_ErrorFileDir + @"\" + file.Name);
                            customImportLog.Info(" ImportData::SortAndProcessPendingFiles : moving file to error folder after retry " + m_ErrorFileDir + file.Name);
                        }
                        catch (Exception ex)
                        {
                            // log 
                            customImportLog.Warn(" ImportData::SortAndProcessPendingFiles : Faield to move file " + ex.Message);
                        }
                    }

                }  // processed all the files from pending folder 

                long[] fileTimeArr = new long[fileHash.Keys.Count];
                fileHash.Keys.CopyTo(fileTimeArr, 0);
                Array.Sort(fileTimeArr, 0, fileTimeArr.Length);
                customImportLog.Trace("ImportData::Processing Pending files => File to be processed = " + fileHash.Keys.Count.ToString());

                for (int i = 0; i < fileTimeArr.Length; i++)
                {

                    FileInfo currFileObj = (FileInfo)fileHash[fileTimeArr[i]];
                    int startingOfdateString = currFileObj.Name.Length - PENDINGEXTENSION.Length - PENDINGDATEFORMAT.Length;
                    string orgFileName = currFileObj.Name.Substring(0, startingOfdateString);


                    Stream objStream = null;
                    try
                    {
                        //objStream = (Stream) currFileObj.Open(FileMode.Open, FileAccess.Read, FileShare.None);
                        int importedRecCnt = 0, errorRecCnt = 0, warningRecCnt = 0;
                        ProcessFile(currFileObj.Name, out importedRecCnt, out errorRecCnt, out warningRecCnt, true);
                        customImportLog.Trace("Processed File : " + currFileObj.Name +
                            ":Imported records:" + importedRecCnt + ",Error records:" + errorRecCnt
                            + ",Warning records:" + warningRecCnt);


                    }
                    catch (Exception ex)
                    {
                        //fileProcessed = false;
                        customImportLog.Error("ImportData::ProcessDirectory=>" + ex.Message, ex);
                    }
                    finally
                    {
                        if (objStream != null)
                            objStream.Close();
                    }

                }
            }
            catch (Exception) { }
            finally
            {
                m_processonce_lock = false;
            }
        }  // end of SortAndProcessPendingFiles

        private void SortAndProcessFiles(DirectoryInfo objFileDir)
        {

            // The previosu process is still going on 
            // ignore the current request 
            if (m_processonce_lock)
                return;
            try
            {
                m_processonce_lock = true;
                Hashtable hash = new Hashtable();
                foreach (FileInfo file in objFileDir.GetFiles())
                {
                    #region Sort the files as specified in config file
                    //Process only those files filenames of which contain the string specified in config.
                    //If nothing is specified process all the files.
                    if (m_AcceptFilesCondns != null && m_AcceptFilesCondns.Length > 0)
                    {
                        bool acceptFile = false;
                        foreach (string str in m_AcceptFilesCondns)
                        {
                            if (Regex.IsMatch(file.Name, str) == true)
                            {
                                acceptFile = true;
                                break;
                            }
                        }
                        if (!acceptFile)
                            continue;
                    }
                    //Don't Process those files filenames of which contain the string specified in config as Ignore.
                    //If nothing is specified in IgnoreFiles process all the files.
                    if (m_IgnoreFilesCondns != null && m_IgnoreFilesCondns.Length > 0)
                    {
                        bool ignoreFile = false;
                        foreach (string str in m_IgnoreFilesCondns)
                        {
                            if (Regex.IsMatch(file.Name, str) == true)
                            {
                                ignoreFile = true;
                                break;
                            }

                        }
                        if (ignoreFile)
                            continue;
                    }
                    #endregion Sort the files as specified in config file

                    // On certain system the  system you will find multiple files with same tick
                    try
                    {
                        long tTicks = file.LastWriteTime.Ticks;
                        while (hash.ContainsKey(tTicks))
                            tTicks += 1;
                        hash[tTicks] = file;

                    }
                    catch { }

                } // end of all files 

                long[] fileTimeArr = new long[hash.Keys.Count];
                hash.Keys.CopyTo(fileTimeArr, 0);
                Array.Sort(fileTimeArr, 0, fileTimeArr.Length);
                customImportLog.Trace("ImportData::ProcessDirectory=> File to be processed = " + hash.Keys.Count.ToString());

                for (int i = 0; i < fileTimeArr.Length; i++)
                {

                    //bool fileProcessed  = true;
                    /// For each file we have to set it true.
                    mMarkFileAsProcessed = true;
                    FileInfo currFileObj = (FileInfo)hash[fileTimeArr[i]];

                    Stream objStream = null;
                    try
                    {
                        //objStream = (Stream) currFileObj.Open(FileMode.Open, FileAccess.Read, FileShare.None);
                        int importedRecCnt = 0, errorRecCnt = 0, warningRecCnt = 0;
                        ProcessFile(currFileObj.Name, out importedRecCnt, out errorRecCnt, out warningRecCnt);
                        customImportLog.Trace("Processed File : " + currFileObj.Name +
                            ":Imported records:" + importedRecCnt + ",Error records:" + errorRecCnt
                            + ",Warning records:" + warningRecCnt);


                    }
                    catch (Exception ex)
                    {
                        //fileProcessed = false;
                        customImportLog.Error("ImportData::ProcessDirectory=>" + ex.Message, ex);
                    }
                    finally
                    {
                        if (objStream != null)
                            objStream.Close();
                    }


                    /// NOTE :- if file processed is mark as false by derived component it 
                    /// is not deleting the orignal file.
                    /// Using it in Import Order/Container components
                    if (mMarkFileAsProcessed)
                    {
                        customImportLog.Warn("ImportData::ProcessDirectory=> mMarkFileAsProcessed is true deleting the file : " + currFileObj.Name);
                        currFileObj.Delete();
                    }
                    else
                    {
                        customImportLog.Warn("ImportData::ProcessDirectory=> mMarkFileAsProcessed is false NOT deleting the file : " + currFileObj.Name);
                    }


                }
            }
            catch (Exception) { }
            finally
            {
                m_processonce_lock = false;
            }

        }

        /// <summary>
        /// Sends Email with ImportData Status Log Files as attachment
        /// </summary>
        private void SendLogEmail()
        {
            StreamReader objLogStream = null;
            Stream objEmailAttStream = null;
            StreamWriter objEmailAttWriter = null;
            try
            {
                SendMail logEmail = null;

                string attachmentFileName = string.Empty;
                //				logEmail.SMTPUser = UserName;
                //				logEmail.SMTPPassword= Password;             
                StringBuilder fileSuccessStringBdr = new StringBuilder();
                StringBuilder fileErrorStringBdr = new StringBuilder();
                string fileString = string.Empty;
                DirectoryInfo objFileDir = new DirectoryInfo(m_ProcessedFileDir + @"\ImportLog");
                bool anySuccessLogFile = false;
                bool anyErrorLogFile = false;
                foreach (FileInfo file in objFileDir.GetFiles())
                {
                    if (file.LastWriteTime.Date == System.DateTime.Now.Date)
                    {
                        if (objLogStream != null)
                            objLogStream.Close();

                        objLogStream = file.OpenText();
                        if (objLogStream != null)
                        {
                            fileString = objLogStream.ReadToEnd();
                        }
                        else
                            continue;

                        if (fileString.Contains("List contains no data"))
                        {
                            continue;
                        }
                        else if (fileString.Contains("Error Records: 0"))
                        {
                            fileSuccessStringBdr.Append(fileString);
                            anySuccessLogFile = true;
                        }
                        else
                        {
                            fileErrorStringBdr.Append(fileString);
                            anyErrorLogFile = true;
                        }
                    }
                }
                if (objLogStream != null)
                    objLogStream.Close();
                string dateStr = System.DateTime.Now.ToShortDateString();
                string mailSubj = string.Empty;
                string mailBody = string.Empty;

                if (anySuccessLogFile)
                {

                    try
                    {
                        logEmail = new SendMail();
                        logEmail.SMTPServer = m_SmtpServerName;

                        mailSubj = m_ImporterType.ToString() + " Data Successfully Imported Files Log For " + dateStr;
                        mailBody = "Please find the attached log files for Successfully Imported Files of " + m_ImporterType.ToString() + " data imported.";

                        dateStr = dateStr.Replace(@"/", "");
                        attachmentFileName =
                            objFileDir.FullName + @"\" + dateStr + m_ImporterType.ToString() + "SuccessLogs.txt";
                        objEmailAttStream =
                            (Stream)File.Open(attachmentFileName, FileMode.Create);//FileMode.OpenOrCreate);
                        objEmailAttStream.Seek(0, SeekOrigin.End);
                        objEmailAttWriter = new StreamWriter(objEmailAttStream, System.Text.Encoding.UTF8);
                        objEmailAttWriter.Write(fileSuccessStringBdr.ToString());
                        if (objEmailAttStream != null)
                        {
                            objEmailAttWriter.Flush();
                            objEmailAttWriter.Close();
                        }
                        logEmail.AddAttachment(attachmentFileName);
                        logEmail.Send(m_From, m_To, m_Bcc, m_CC, mailSubj, mailBody);
                        if (System.IO.File.Exists(attachmentFileName))
                        {
                            FileInfo fiErr = new FileInfo(attachmentFileName);
                            fiErr.Delete();
                        }
                    }
                    catch (Exception exp)
                    {
                        customImportLog.Error("ImportData::SendLogEmail==>", exp);
                    }
                    finally
                    {


                        if (objEmailAttStream != null)
                        {
                            objEmailAttWriter.Close();
                        }
                        if (System.IO.File.Exists(attachmentFileName))
                        {
                            FileInfo fiErr = new FileInfo(attachmentFileName);
                            fiErr.Delete();
                        }
                    }
                }
                if (anyErrorLogFile)
                {

                    try
                    {
                        mailSubj = m_ImporterType.ToString() + " Data  Imported with Error  Log For " + dateStr;
                        mailBody = "Please find the attached log files for files Imported with Error of " + m_ImporterType.ToString() + " data imported.";

                        dateStr = dateStr.Replace(@"/", "");
                        attachmentFileName =
                            objFileDir.FullName + @"\" + dateStr + m_ImporterType.ToString() + "ErrorLogs.txt";
                        objEmailAttStream =
                            (Stream)File.Open(attachmentFileName, FileMode.Create);//FileMode.OpenOrCreate);
                        objEmailAttStream.Seek(0, SeekOrigin.End);
                        objEmailAttWriter = new StreamWriter(objEmailAttStream, System.Text.Encoding.UTF8);
                        objEmailAttWriter.Write(fileErrorStringBdr.ToString());
                        if (objEmailAttStream != null)
                        {
                            objEmailAttWriter.Flush();
                            objEmailAttWriter.Close();
                        }
                        logEmail = new SendMail();
                        logEmail.SMTPServer = m_SmtpServerName;
                        logEmail.AddAttachment(attachmentFileName);
                        logEmail.Send(m_From, m_To, m_Bcc, m_CC, mailSubj, mailBody);
                        if (System.IO.File.Exists(attachmentFileName))
                        {
                            FileInfo fiErr = new FileInfo(attachmentFileName);
                            fiErr.Delete();
                        }
                    }
                    catch (Exception exp)
                    {
                        customImportLog.Error("ImportData::SendLogEmail==>", exp);
                    }
                    finally
                    {


                        if (objEmailAttStream != null)
                        {
                            objEmailAttWriter.Close();
                        }
                        if (System.IO.File.Exists(attachmentFileName))
                        {
                            FileInfo fiErr = new FileInfo(attachmentFileName);
                            fiErr.Delete();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                customImportLog.Error("ImportData::SendLogEmail==>" + ex.Message);
            }
            finally
            {
                if (objEmailAttStream != null)
                {
                    if (objEmailAttWriter != null)
                    {
                        objEmailAttWriter.Close();
                    }
                }

                if (objLogStream != null)
                    objLogStream.Close();
            }
        }


        //Based on configuration processed file will be deleted and archived
        private void CleanUpProcessedDir()
        {
            if (m_FileCleanupInterval <= 0)
                return;
            DirectoryInfo objFileDir = new DirectoryInfo(m_ProcessedFileDir);
            foreach (FileInfo file in objFileDir.GetFiles())
            {
                if ((DateTime.Now.DayOfYear - file.LastWriteTime.DayOfYear) > m_FileCleanupInterval)
                {
                    if (m_ArchiveEnabled)
                    {
                        customImportLog.Trace("ImportData::CleanUpProcessedDir=> Archiving file." + file.FullName);
                        if (File.Exists(m_ArchiveFileDir + @"\" + file.Name))
                            File.Delete(m_ArchiveFileDir + @"\" + file.Name);
                        file.MoveTo(m_ArchiveFileDir + @"\" + file.Name);
                    }
                    else
                    {
                        customImportLog.Trace("ImportData::CleanUpProcessedDir=> Deleting file." + file.FullName);
                        file.Delete();
                    }

                }
            }
        }


        protected void LogImportDetail(string fileName, string fileImpStart)
        {
            if (!(fileName.Trim().ToLower().EndsWith(".txt")))
            {
                fileName = fileName.Remove(fileName.Length - 4, 4);
                fileName += ".txt";
            }


            string logDir = m_ProcessedFileDir + @"\ImportLog";
            string logFile = logDir + @"\" + fileName;
            if (!Directory.Exists(logDir))
                Directory.CreateDirectory(logDir);
            Stream objLogStream = null;
            StreamWriter objLogWriter = null;
            try
            {
                objLogStream = (Stream)File.Open(logFile, FileMode.Create);//FileMode.OpenOrCreate);
                objLogStream.Seek(0, SeekOrigin.End);
                objLogWriter = new StreamWriter(objLogStream, System.Text.Encoding.UTF8);

                objLogWriter.WriteLine("Import Details");
                objLogWriter.WriteLine("FileName : " + fileName);
                objLogWriter.WriteLine("Import Start-Time : " + fileImpStart);
                //objLogWriter.WriteLine("Imported Records: " + m_ImportedRecCount.ToString());
                //objLogWriter.WriteLine("Error Records: " + m_ErrRecCount.ToString() + " : ");
                objLogWriter.WriteLine("Imported Records : " + m_ImportedRecCount.ToString());//m_ProcessedRecArr.Count.ToString());
                objLogWriter.WriteLine("Pending Records : " + m_PendingRecArr.Count.ToString());
                objLogWriter.WriteLine("Warning Records : " + m_WarningRecArr.Count.ToString());

                foreach (string warningRec in m_WarningRecArr)
                {
                    objLogWriter.WriteLine(warningRec);
                }
                if (m_WarningRecArr.Count > 0)
                {
                    objLogWriter.WriteLine("");
                    objLogWriter.WriteLine("");
                }
                objLogWriter.WriteLine("Error Records : " + m_ErrRecCount);
                objLogWriter.WriteLine("Records Inserted : " + m_RecInsertedCount);
                objLogWriter.WriteLine("Records Exists and Updated : " + m_RecExistCount);
                objLogWriter.WriteLine("Import End-Time : " + DateTime.Now.ToString());
                if (m_ErrorRecArr.Count > 0)
                {
                    objLogWriter.WriteLine("");
                    objLogWriter.WriteLine("");
                }

                try
                {
                    foreach (string errorRecStr in m_ErrorRecArrAndLog)
                    {
                        objLogWriter.WriteLine(errorRecStr);

                    }
                }
                catch { }
                objLogWriter.WriteLine("");
                objLogWriter.WriteLine("***********************************************************");

                objLogWriter.WriteLine("");
            }
            catch (Exception ex)
            {
                customImportLog.Error("ImportData:LogImportDetail:: " + ex.Message, ex);
            }
            finally
            {
                if (objLogStream != null)
                {
                    objLogWriter.Flush();
                    objLogWriter.Close();
                }
            }
            m_ImportedRecCount = 0;
            m_RecExistCount = 0;
            m_RecInsertedCount = 0;
            m_ErrRecCount = 0;
            m_ProcessedRecArr.Clear();
            m_ErrorRecArr.Clear();
            m_ErrorRecArrAndLog.Clear();
            m_WarningRecArr.Clear();
            m_PendingRecArr.Clear();
        }


        private void FireImpEvent(FlatFileRecInfo[] recInfoArr, string fileName)
        {
            Delegate[] invocationList = null;
            lock (OnImportEventLockObj)
            {
                invocationList = m_ImportEvent.GetInvocationList();
            }
            foreach (ImportDelHandler del in invocationList)
            {
                try
                {
                    del(recInfoArr, fileName);
                }
                catch (Exception ex)
                {
                    m_ImportEvent -= del;
                    customImportLog.Error("ImportData:Fire Import Event:: " + ex.Message);
                }
            }
        }



        #endregion Private Methods

        #region Protected Methods
        private void ProcessFile(string fileName,
                            out int importedRecCnt, out int errorRecCnt, out int warningRecCnt)
        {
            ProcessFile(fileName, out importedRecCnt, out errorRecCnt, out warningRecCnt, false);
        }
        //while(fs.Read(
        //Read records, segregate into field-value pair, store in hash, generate struct
        //Add to arraylist if arraylist count reaches threshold raise event/method to process record

        /// <summary>
        /// m_From a particular data file it reads the line one by one when the line count reaches the threshold
        /// (as specified in config file) it stores those many records in database, also into file in processed folde.
        /// If any error in record then those will be stored as error records in Error folder.
        /// After above processing It will fire an event so that client can register and view what all data is processed 
        /// </summary>
        /// <param name="fileName">file name(without path)</param>
        private void ProcessFile(string fileName,
                            out int importedRecCnt, out int errorRecCnt, out int warningRecCnt, bool isPending)
        {
            importedRecCnt = 0;
            errorRecCnt = 0;
            warningRecCnt = 0;
            int pendingRecCnt = 0;

            if (m_IsProcessFileWorking == false)
            {
                lock (m_ProcessFileLock)
                {
                    m_IsProcessFileWorking = true;
                    StreamReader strReader = null;
                    StreamWriter strWriterProc = null;
                    StreamWriter strWriterError = null;
                    FileInfo fileInfo = null;
                    try
                    {
                        if (!m_FlatFileDir.EndsWith(@"\")) m_FlatFileDir += @"\";
                        string orgFileName = fileName; // Copy of file that has pending and date to it
                        string fullPhysicalName = m_FlatFileDir + fileName;


                        if (isPending)
                        {
                            fullPhysicalName = m_PendingFileDir + @"\" + fileName;
                            int startingOfdateString = fileName.Length - PENDINGEXTENSION.Length - PENDINGDATEFORMAT.Length;
                            fileName = fileName.Substring(0, startingOfdateString);

                        }

                        //  Get the last write time of the file and generate folder name on month and year
                        fileInfo = new FileInfo(fullPhysicalName);
                        DateTime lastWriteTime = fileInfo.LastWriteTime;
                        string month = lastWriteTime.ToString("MMMM");
                        int year = lastWriteTime.Year;
                        string folderName = month + "_" + year;

                        // end of get the folder name


                        strReader = new StreamReader(fullPhysicalName, System.Text.Encoding.UTF8);
                        customImportLog.Trace("ImportData::ProcessFile=> Processing File = " + fullPhysicalName);
                        m_CurrentFileName = fileName;
                        ResetImport();

                        string fileImpStart = DateTime.Now.ToString();
                        string strRecordLine = String.Empty;

                        #region streamWriters for errorfiles and processed files

                        // string procFilePath = m_ProcessedFileDir + @"\" + fileName;
                        //For Process rec
                        string procFilePath = m_ProcessedFileDir + @"\" + folderName;
                        if (!Directory.Exists(procFilePath))
                            Directory.CreateDirectory(procFilePath);
                        procFilePath = procFilePath + @"\" + fileName;




                        //string errFilePath = m_ErrorFileDir + @"\" + fileName;
                        //For error rec
                        string errFilePath = m_ErrorFileDir + @"\" + folderName;
                        if (!Directory.Exists(errFilePath))
                            Directory.CreateDirectory(errFilePath);
                        errFilePath = errFilePath + @"\" + fileName;


                        //Will copy the file from original destination to the ShadowCopy folder before processing.
                        string shadowFilePath = m_ShadowCopies + @"\" + folderName;
                        if (!Directory.Exists(shadowFilePath))
                            Directory.CreateDirectory(shadowFilePath);
                        shadowFilePath = shadowFilePath + @"\" + fileName;
                        FileInfo fileInfoCopy = new FileInfo(m_FlatFileDir + fileName);
                        if (fileInfoCopy.Exists)
                        {
                            fileInfoCopy.CopyTo(shadowFilePath, true);
                        }



                        FileStream fsProc = null;
                        FileStream fsError = null;

                        try
                        {
                            fsProc = File.Open(procFilePath, FileMode.Append, FileAccess.Write);
                            strWriterProc = new StreamWriter((Stream)fsProc, System.Text.Encoding.UTF8);

                            fsError = File.Open(errFilePath, FileMode.Append, FileAccess.Write);
                            strWriterError = new StreamWriter((Stream)fsError, System.Text.Encoding.UTF8);
                        }
                        catch (Exception ex)
                        {
                            customImportLog.Error("ImportData:ProcessRecords:Error while opening file =>" + ex.Message, ex);
                            //If error in file opening still DB update should go on
                        }
                        #endregion streamWriters for errorfiles and processed files


                        ParseFile(strReader, strWriterProc, strWriterError, orgFileName);
                        customImportLog.Trace("ImportData::Record updated count =" + m_RecExistCount);

                        //returned here as after logging in LogImportDetail the arrays are cleared
                        importedRecCnt = m_ImportedRecCount;// m_ProcessedRecArr.Count;
                        errorRecCnt = m_ErrorRecArr.Count;
                        warningRecCnt = m_WarningRecArr.Count;
                        pendingRecCnt = m_PendingRecArr.Count;

                        LogImportDetail(fileName, fileImpStart);
                        customImportLog.Trace("ImportData::ProcessFile : Processing of current file Done.");

                        if (strWriterProc != null)
                            strWriterProc.Close();
                        if (strWriterError != null)
                            strWriterError.Close();

                        //If no error record exists delete the file
                        // AS we appending the processed lines, check the size and delete if nothing written to file 
                        FileInfo fiErr = new FileInfo(errFilePath);//m_ErrorFileDir + @"\" + fileName);
                        //if (errorRecCnt == 0)
                        if (fiErr.Length <= 3)
                            fiErr.Delete();
                        FileInfo fiProc = new FileInfo(procFilePath);//m_ProcessedFileDir + @"\" + fileName);
                        //if (importedRecCnt == 0)
                        if (fiProc.Length <= 3)
                            fiProc.Delete();


                    }
                    catch (Exception ex)
                    {
                        importedRecCnt = m_ProcessedRecArr.Count;
                        errorRecCnt = m_ErrorRecArr.Count;
                        warningRecCnt = m_WarningRecArr.Count;
                        customImportLog.Error("ImportData::ProcessFile : Error in processing the file", ex);
                        throw new Exception("Error in ProcessFile" + ex.Message);

                    }
                    finally
                    {
                        m_IsProcessFileWorking = false;
                        try
                        {
                            if (strReader != null)
                                strReader.Close();
                        }
                        catch (Exception) { }
                        try
                        {
                            if (strWriterProc != null)
                                strWriterProc.Close();
                        }
                        catch (Exception) { }
                        try
                        {
                            if (strWriterError != null)
                                strWriterError.Close();
                        }
                        catch (Exception) { }


                    }
                }
            }
        }

        protected virtual void ParseFile(StreamReader strReader, StreamWriter strWriterProc, StreamWriter strWriterError, string fileName)
        {
            ParseFile(strReader, strWriterProc, strWriterError, fileName, false);
        }

        protected virtual void ParseFile(StreamReader strReader, StreamWriter strWriterProc, StreamWriter strWriterError, string fileName, bool processingPendingFiles)
        {
            string logHeader = "ImportData::ParseFile()";
            Hashtable fieldValueHash = null;
            int recNo = 0;


            //get file enumerator from CsvReader
            IEnumerator en = null;
            Hashtable fieldIndexHash = null;
            try
            {
                CsvReader csv = new CsvReader(strReader, m_FieldHeaderPresent, m_AcceptDuplicateHeaders, m_FieldSeperator, m_FieldQuote);
                en = csv.GetEnumerator();
                fieldIndexHash = csv.FieldHeaders;

            }
            catch (Exception ex)
            {
                customImportLog.Error("ImportData::ParseFile:Possible field header absent when specified true in config file =>" + ex.Message, ex);
                throw new Exception("Possible field header absent when specified true in config file");
            }
            string fieldValuesStr = string.Empty;
            bool isProcessed = false;
            try
            {
                for (int i = 0; i < fieldIndexHash.Count; i++)
                {
                    foreach (DictionaryEntry entry in fieldIndexHash)
                    {
                        if ((int)entry.Value == i)
                        {
                            if (fieldValuesStr != string.Empty)
                                fieldValuesStr += ",";
                            fieldValuesStr += entry.Key;
                            isProcessed = true;
                        }

                    }
                    if (isProcessed)
                    {
                        strWriterProc.WriteLine(fieldValuesStr);
                        strWriterError.WriteLine(fieldValuesStr);
                    }
                }
                strWriterProc.WriteLine(fieldValuesStr);
                strWriterError.WriteLine(fieldValuesStr);

            }
            catch { }


            try
            {
                ArrayList processedRecArr = new ArrayList();

                while (en.MoveNext())
                {
                    try
                    {
                        //Check Max error count
                        try
                        {
                            if (m_ErrRecCount == m_MaxErrorRecordCnt)
                            {
                                string str = "Number of Error records (" + m_ErrRecCount + ") reached maximum allowed.Hence processing of the file is aborted.";
                                m_ErrorRecArr.Add(str);
                                m_ErrorRecArrAndLog.Add(str);
                                strWriterError.WriteLine(str);
                                customImportLog.Info(str);
                                break;
                            }
                            if (m_ProcessedRecArr.Count == m_MaxRecProcessCnt)
                            {
                                string str = "Number of records (" + m_ImportedRecCount + ") reached maximum allowed.Hence processing of the file is aborted.";
                                m_ProcessedRecArr.Add(str);
                                strWriterProc.WriteLine(str);
                                customImportLog.Info(str);
                                break;
                            }
                        }
                        catch { }

                        #region Process Record Values
                        FlatFileRecInfo recordInfo = new FlatFileRecInfo();
                        string[] fieldValues = (string[])en.Current;
                        fieldValuesStr = string.Empty;
                        string[] processFieldValues =
                            new string[fieldValues.Length];
                        int cnt = 0;
                        foreach (string field in fieldValues)
                        {
                            if (fieldValuesStr != string.Empty)
                                fieldValuesStr += m_FieldSeperator;
                            fieldValuesStr += field;
                            processFieldValues[cnt] = field.Trim().Replace("'", "''");
                            cnt++;
                        }
                        processedRecArr.Add(fieldValuesStr);
                        bool updateDB = false;
                        try
                        {
                            updateDB = ProcessRecordValues(fieldIndexHash, processFieldValues, out fieldValueHash);
                            customImportLog.Info(logHeader + "Update DB=" + updateDB);

                        }
                        catch (Exception ex)
                        {
                            customImportLog.Error("ProcessFile:Error in processing the record", ex);
                            try
                            {
                                m_ErrRecCount += 1;
                                m_ErrorRecArr.AddRange(processedRecArr);
                                m_ErrorRecArrAndLog.AddRange(processedRecArr);
                                m_ErrorRecArrAndLog.Add(ex.Message);
                                processedRecArr.Clear();
                                strWriterError.WriteLine(fieldValuesStr);
                                customImportLog.Info(logHeader + "Error:" + ex.Message);
                                customImportLog.Info(logHeader + "Error records:" + fieldValuesStr);
                                customImportLog.Trace("ImportData:ProcessRecords=>Invalid Rec = " + fieldValuesStr);

                            }
                            catch { }

                        }
                        #endregion Process Record Values

                        #region Update DB
                        if (updateDB)
                        {
                            customImportLog.Info(logHeader + "Update:" + updateDB);
                            string records = string.Empty;
                            foreach (string record in processedRecArr)
                                records += record + "\r\n";


                            recordInfo.recordLine = records;
                            recordInfo.fieldValueHash = fieldValueHash;
                            recordInfo.recNo = recNo++;
                            try
                            {

                                ProcessRecord(recordInfo, fileName, processedRecArr, strWriterProc, strWriterError);
                            }
                            finally
                            {
                                processedRecArr.Clear();
                            }
                        }
                        #endregion Update DB

                    }
                    catch (Exception ex)
                    {
                        customImportLog.Error("ImportData::ProcessFile:Error while processing record=>" + ex.Message + " Type:" + ex.GetType().Name, ex);

                        if (ex.InnerException != null)
                        {
                            customImportLog.Error("ImportData::Inner Exception:" + ex.InnerException + " Type:" + ex.GetType().Name, ex.InnerException);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                customImportLog.Error("ImportData::ProcessFile:Error while processing record=>" + ex.Message, ex);
                string strErr = "Improper File Format : Please check the file or contact Admininstrator.";
                m_ErrorRecArr.Add(strErr);
                m_ErrorRecArrAndLog.Add(strErr);
                strWriterError.WriteLine(strErr);
                customImportLog.Info(strErr);
            }

            //if(m_ImporterType == Importer.RetailLink)
            //{
            //    //update last record
            //    string[] tempfieldValues = new string[6];
            //    ProcessRecordValues(fieldIndexHash,tempfieldValues,out fieldValueHash);
            //    FlatFileRecInfo recordInfo = new FlatFileRecInfo();
            //    recordInfo.recordLine = fieldValuesStr;
            //    recordInfo.fieldValueHash =	fieldValueHash;
            //    recordInfo.recNo = recNo++;
            //    ProcessRecord(recordInfo, fileName,processedRecArr,strWriterProc,strWriterError );
            //    processedRecArr.Clear();
            //}

        }


        #endregion Protected Methods

        #region overridables

        /// <summary>
        /// Reads the configuration parameters from ImportDataConfig.config and stores in 
        /// local variables for future use.
        /// </summary>
        protected virtual void ReadConfigParams(string configString)
        {
            customImportLog.Trace("ImportData:: ReadConfigParams Start.");

            //if(File.Exists(m_ConfigFile))
            //{
            this.configStr = configString;
            XmlDocument xDoc = new XmlDocument();
            MemoryStream streamReader = null;

            try
            {
                //if(m_XSDFile != string.Empty)
                //{
                //    streamReader = new StreamReader(m_ConfigFile);
                //    RFUtils.ValidateXML(m_XSDFile,streamReader);
                //    streamReader.Close();
                //}
                streamReader = new MemoryStream(new ASCIIEncoding().GetBytes(configString));
                xDoc.Load(streamReader);


                string GenericStr = "ImportDataManagerConfig/GenricElements/";

                #region Load Generic Information

                if (xDoc.SelectSingleNode(GenericStr + "BasePath") != null)
                    m_BasePath = xDoc.SelectSingleNode(GenericStr + "BasePath").InnerText.Trim();
                if (m_BasePath.Trim() == string.Empty || !Directory.Exists(m_BasePath))
                {
                    m_BasePath = AppDomain.CurrentDomain.BaseDirectory;
                }
                //Number of days for the pending records 
                try
                {
                    m_PendingFileRetentionDays = Convert.ToInt32(xDoc.SelectSingleNode(GenericStr + "PendingFileRetentionDays").InnerText.Trim());
                    if (m_PendingFileRetentionDays < 1)
                    {
                        m_PendingFileRetentionDays = 1;
                        customImportLog.Info("ImportData:ReadConfigParams:If the config value is 0 or less than 1,then default value will be set to 1. ");
                    }
                    else if (m_PendingFileRetentionDays > 30)
                    {
                        m_PendingFileRetentionDays = 30;
                        customImportLog.Info("ImportData:ReadConfigParams:If the config value is greater than 30,then default value will be set to 30. ");
                    }
                    else if (m_PendingFileRetentionDays.Equals(string.Empty))
                    {
                        customImportLog.Info("ImportData:ReadConfigParams:If the config value is empty,then default value will be set to 2. ");
                        m_PendingFileRetentionDays = 2;
                    }

                }
                catch (Exception)
                {
                    customImportLog.Error("ImportData:ReadConfigParams:Error while reading.Default value will be set to 2. ");
                    m_PendingFileRetentionDays = 2;
                }
                //MaxLength of Records
                try
                {
                    m_MaxRecProcessCnt = Convert.ToInt32(xDoc.SelectSingleNode(GenericStr + "MaxProcessRecordCount").InnerText.Trim());
                }
                catch (Exception)
                {
                    m_MaxRecProcessCnt = 50;
                }
                try
                {
                    m_MaxErrorRecordCnt = Convert.ToInt32(xDoc.SelectSingleNode(GenericStr + "MaxErrorRecordCount").InnerText.Trim());
                }
                catch (Exception)
                {
                    m_MaxErrorRecordCnt = 1000;
                }

                //Timer Interval
                m_ImportInterval = Convert.ToDouble(xDoc.SelectSingleNode(GenericStr + "TimerInfo/ImportInterval").InnerText.Trim());
                m_FileCleanupInterval = Convert.ToInt32(xDoc.SelectSingleNode(GenericStr + "TimerInfo/FileCleanupInterval").InnerText.Trim());

                //Import Start & End Time
                m_ImportStartTime = Convert.ToDateTime(xDoc.SelectSingleNode(GenericStr + "TimerInfo/ImportStartTime").InnerText.Trim());
                m_ImpStartTime = m_ImportStartTime.TimeOfDay.TotalMinutes;
                m_ImportEndTime = Convert.ToDateTime(xDoc.SelectSingleNode(GenericStr + "TimerInfo/ImportEndTime").InnerText.Trim());
                m_ImpEndTime = m_ImportEndTime.TimeOfDay.TotalMinutes;

                // There is comma separated list of the regular expression enclsoed in "
                // If quote is present strip it and take one or more regular expression patterns
                string acceptFiles = xDoc.SelectSingleNode(GenericStr + "FileSort/AcceptFiles").InnerText.Trim();
                if (acceptFiles != null && acceptFiles != string.Empty)
                {
                    m_AcceptFilesCondns = acceptFiles.Split(new char[] { ',' });
                    for (int cnt = 0; cnt < m_AcceptFilesCondns.Length; cnt++)
                    {
                        string s = m_AcceptFilesCondns[cnt];
                        m_AcceptFilesCondns[cnt] = s.Trim(new char[] { '"' });
                    }
                }
                string ignoreFiles = xDoc.SelectSingleNode(GenericStr + "FileSort/IgnoreFiles").InnerText.Trim();
                if (ignoreFiles != null && ignoreFiles != string.Empty)
                {
                    m_IgnoreFilesCondns = ignoreFiles.Split(new char[] { ',' });
                    for (int cnt = 0; cnt < m_AcceptFilesCondns.Length; cnt++)
                    {
                        string s = m_AcceptFilesCondns[cnt];
                        m_AcceptFilesCondns[cnt] = s.Trim(new char[] { '"' });
                    }
                }

                m_SortOnFileName = Convert.ToBoolean(xDoc.SelectSingleNode(GenericStr + "FileSort/FileNameTime").InnerText.Trim());


                m_URNStd = xDoc.SelectSingleNode(GenericStr + "URNInfo/Standard").InnerText.Trim();
                m_Filter = Convert.ToInt16(xDoc.SelectSingleNode(GenericStr + "URNInfo/FilterId").InnerText.Trim());
                //m_ExtensionDigit = Convert.ToInt16( xDoc.SelectSingleNode("ImportDataConfig/URNInfo/ExtDigit").InnerText.Trim() );

                string emailSettingPath = GenericStr + @"LogFileNotification/EMailSettings";
                m_SmtpServerName = xDoc.SelectSingleNode(emailSettingPath + "/SmtpServerConfig").Attributes["SmtpServerName"].Value.Trim();
                //UserName = xDoc.SelectSingleNode(emailSettingPath + "/SmtpServerConfig").Attributes["UserName"].Value.Trim() ;
                //Password = xDoc.SelectSingleNode(emailSettingPath + "/SmtpServerConfig").Attributes["Password"].Value.Trim() ;
                m_From = xDoc.SelectSingleNode(emailSettingPath + "/From").InnerText.Trim();
                m_To = xDoc.SelectSingleNode(emailSettingPath + "/To").InnerText.Trim();
                m_Bcc = xDoc.SelectSingleNode(emailSettingPath + "/Bcc").InnerText.Trim();
                m_CC = xDoc.SelectSingleNode(emailSettingPath + "/CC").InnerText.Trim();
                m_NotificationTime = Convert.ToDateTime(xDoc.SelectSingleNode(GenericStr + @"LogFileNotification/NotifyTime").InnerText.Trim());
                m_NotifyTime = m_NotificationTime.TimeOfDay.TotalMinutes;

                string connectionString = string.Empty;
                KTone.RFIDGlobal.ConfigParams.GlobalConfigParams.Lookup("KTProductConn", out connectionString);
                RFUtils.SplitEncodedDBConnectionStr(connectionString, out this.mDatabaseName, out this.mDBServerName,
                out this.mUserID, out this.mPassword);

                m_ConnString = RFUtils.CreateDBConnectionStr(this.mDatabaseName, this.mDBServerName, this.mUserID, this.mPassword);

                #endregion Load Generic Information

                #region Import Settings

                string ImporterStr = "ImportDataManagerConfig/Importer/ImportDataConfig";

                XmlNode ImporterNode = null;

                foreach (XmlNode node in xDoc.SelectNodes(ImporterStr))
                {
                    if (node.Attributes["ImporterType"].Value == this.ImporterType.ToString())
                        ImporterNode = node;

                    importerName = node.Attributes["ImporterType"].Value.ToString();
                }


                if (ImporterNode != null)
                {
                    string fileInfoStr = "FlatFileInfo/";

                    m_FlatFileDir = ImporterNode.SelectSingleNode(fileInfoStr + "OrigFileDir").InnerText.Trim();
                    string basePath = m_BasePath;
                    if (!basePath.EndsWith(@"\"))
                        basePath += @"\";
                    //if (m_FlatFileDir.StartsWith("ImportData"))
                    //{
                    m_FlatFileDir = basePath + m_FlatFileDir;
                    //}

                    try { m_ProcessedFileDir = ImporterNode.SelectSingleNode(fileInfoStr + "ProcessedFileDir").InnerText.Trim(); }
                    catch (Exception) { };
                    if (string.IsNullOrEmpty(m_ProcessedFileDir))
                        m_ProcessedFileDir = m_FlatFileDir + @"\ProcessedFiles";
                    //else if (m_ProcessedFileDir.StartsWith("ImportData"))
                    m_ProcessedFileDir = basePath + m_ProcessedFileDir;

                    try { m_ErrorFileDir = ImporterNode.SelectSingleNode(fileInfoStr + "ErrorFileDir").InnerText.Trim(); }
                    catch (Exception) { };
                    if (string.IsNullOrEmpty(m_ErrorFileDir))
                        m_ErrorFileDir = m_FlatFileDir + @"\ErrorRecFiles";
                    //else if (m_ErrorFileDir.StartsWith("ImportData"))
                    m_ErrorFileDir = basePath + m_ErrorFileDir;

                    try { m_ArchiveFileDir = ImporterNode.SelectSingleNode(fileInfoStr + "ArchiveFileDir").InnerText.Trim(); }
                    catch (Exception) { };
                    if (string.IsNullOrEmpty(m_ArchiveFileDir))
                        m_ArchiveFileDir = m_FlatFileDir + @"\ArchivedFiles";
                    //else if (m_ArchiveFileDir.StartsWith("ImportData"))
                    m_ArchiveFileDir = basePath + m_ArchiveFileDir;

                    try { m_PendingFileDir = ImporterNode.SelectSingleNode(fileInfoStr + "PendingFileDir").InnerText.Trim(); }
                    catch (Exception) { };
                    if (importerName == "ContainerDetails")
                    {
                        if (string.IsNullOrEmpty(m_PendingFileDir))
                            m_PendingFileDir = m_FlatFileDir + @"\PendingFiles";
                        //else if (m_PendingFileDir.StartsWith("ImportData"))
                        m_PendingFileDir = basePath + m_PendingFileDir;
                    }

                    try { m_ShadowCopies = m_ProcessedFileDir.Replace("ProcessedFiles", "ShadowCopies"); }
                    catch (Exception) { };
                    if (string.IsNullOrEmpty(m_ShadowCopies))
                        m_ShadowCopies = m_FlatFileDir + @"\ShadowCopies";



                    if (!Directory.Exists(m_FlatFileDir))
                        Directory.CreateDirectory(m_FlatFileDir);
                    if (!Directory.Exists(m_ProcessedFileDir))
                        Directory.CreateDirectory(m_ProcessedFileDir);
                    if (!Directory.Exists(m_ErrorFileDir))
                        Directory.CreateDirectory(m_ErrorFileDir);
                    if (!Directory.Exists(m_ArchiveFileDir))
                        Directory.CreateDirectory(m_ArchiveFileDir);
                    if (!Directory.Exists(m_ShadowCopies))
                        Directory.CreateDirectory(m_ShadowCopies);




                    m_ArchiveEnabled = Convert.ToBoolean(ImporterNode.SelectSingleNode(fileInfoStr + "ArchiveFileDir").Attributes["ArchiveEnabled"].Value.Trim());

                    //Read the column mapping information
                    try
                    {
                        m_FieldHeaderPresent = Convert.ToBoolean(ImporterNode.SelectSingleNode(fileInfoStr + "FieldInfo/FieldHeader").InnerText.Trim());
                    }
                    catch (Exception) { m_FieldHeaderPresent = false; }


                    string seperator = ImporterNode.SelectSingleNode(fileInfoStr + "FieldInfo/FieldSeperator").InnerText;
                    if (seperator == string.Empty)
                        m_FieldSeperator = new char();
                    else
                        m_FieldSeperator = Convert.ToChar(seperator.Trim());

                    string quote = ImporterNode.SelectSingleNode(fileInfoStr + "FieldInfo/FieldQuote").InnerText;
                    if (quote == string.Empty)
                        m_FieldQuote = new char();
                    else
                        m_FieldQuote = Convert.ToChar(quote.Trim());

                    //File field Info 
                    ReadFieldInfo(ImporterNode);

                    //TableInfo
                    ReadDBInfo(ImporterNode);
                }
                #endregion Import Settings




            }
            catch (Exception ex)
            {
                customImportLog.Error("ImportData::ReadConfigParams==>" + ex.Message);
                if (streamReader != null)
                    streamReader.Close();
                throw ex;
            }
            //}
            //else
            //{
            //    customImportLog.Error("ImportData::ReadConfigParams==>Configuration Parameter file is missing.");
            //    throw new ApplicationException("Configuration Parameter file is missing.");
            //}
            customImportLog.Trace("ImportData:: ReadConfigParams End.");

        }

        //File field Info 
        protected virtual void ReadFieldInfo(XmlNode ImporterNode)
        {
            string fileInfoStr = "FlatFileInfo/";
            try
            {
                m_FieldCnt = Convert.ToInt32(ImporterNode.SelectSingleNode(fileInfoStr + "FieldInfo/FieldCount").InnerText.Trim());
            }
            catch { }
            //FieldSequence = xDoc.SelectSingleNode(fileInfoStr + "FieldInfo/FieldSequence").InnerText ;

            //Flat File fields
            XmlNodeList nodeList = ImporterNode.SelectNodes(fileInfoStr + "FieldInfo/Fields/Field");
            m_FileFieldList = new ArrayList();
            for (int i = 0; i < nodeList.Count; i++)
                m_FileFieldList.Add(nodeList[i].InnerText.Trim());
        }



        protected virtual void ReadDBInfo(XmlNode ImporterNode)
        {

            //Table Info
            XmlNodeList nodeList = ImporterNode.SelectNodes("DBInfo/Tables/Table");
            m_TableList = new TableInfo[nodeList.Count];
            for (int tblCnt = 0; tblCnt < nodeList.Count; tblCnt++)
            {
                m_TableList[tblCnt].tableName = nodeList[tblCnt].Attributes["Name"].Value;
                m_TableList[tblCnt].tableType = nodeList[tblCnt].Attributes["Type"].Value == "Primary" ? TableTypes.Primary : TableTypes.Secondary;

                XmlNodeList fieldNodeList = nodeList[tblCnt].SelectNodes("Field");
                FieldInfo[] fieldList = new FieldInfo[fieldNodeList.Count];
                for (int fldCnt = 0; fldCnt < fieldNodeList.Count; fldCnt++)
                {
                    fieldList[fldCnt].fieldName = fieldNodeList[fldCnt].Attributes["Name"].Value.Trim();
                    fieldList[fldCnt].isKeyCol = Convert.ToBoolean(fieldNodeList[fldCnt].Attributes["KeyCol"].Value);
                    if (fieldNodeList[fldCnt].Attributes["KeyCol"].Value == "")
                        fieldList[fldCnt].DBInsertReq = true;
                    else
                        fieldList[fldCnt].DBInsertReq = Convert.ToBoolean(fieldNodeList[fldCnt].Attributes["KeyCol"].Value);

                    if (fieldNodeList[fldCnt].SelectNodes("Custom").Count > 0)
                    {
                        fieldList[fldCnt].fieldType = FieldTypes.Custom;
                        fieldList[fldCnt].customValue = fieldNodeList[fldCnt].SelectNodes("Custom")[0].InnerText;
                    }
                    if (fieldNodeList[fldCnt].SelectNodes("Default").Count > 0)
                    {
                        fieldList[fldCnt].fieldType = FieldTypes.Default;
                        fieldList[fldCnt].defaultValue = fieldNodeList[fldCnt].SelectNodes("Default")[0].InnerText;
                    }
                    if (fieldNodeList[fldCnt].SelectNodes("Index").Count > 0)
                    {
                        fieldList[fldCnt].fieldType = FieldTypes.Index;
                        fieldList[fldCnt].indexValue = Convert.ToInt32(fieldNodeList[fldCnt].SelectNodes("Index")[0].InnerText);
                    }
                    if (fieldNodeList[fldCnt].SelectNodes("IndexCustom").Count > 0)
                    {
                        fieldList[fldCnt].fieldType = FieldTypes.IndexCustom;
                        fieldList[fldCnt].indexValue = Convert.ToInt32(fieldNodeList[fldCnt].SelectNodes("IndexCustom/Index")[0].InnerText);
                        fieldList[fldCnt].customValue = fieldNodeList[fldCnt].SelectNodes("IndexCustom/Custom")[0].InnerText;
                    }
                    if (fieldNodeList[fldCnt].SelectNodes("DefaultIndexCustom").Count > 0)
                    {
                        fieldList[fldCnt].fieldType = FieldTypes.DefaultIndexCustom;
                        fieldList[fldCnt].indexValue = Convert.ToInt32(fieldNodeList[fldCnt].SelectNodes("DefaultIndexCustom/Index")[0].InnerText);
                        fieldList[fldCnt].customValue = fieldNodeList[fldCnt].SelectNodes("DefaultIndexCustom/Custom")[0].InnerText;
                        fieldList[fldCnt].defaultValue = fieldNodeList[fldCnt].SelectNodes("DefaultIndexCustom/Default")[0].InnerText;

                    }

                    //
                    // fieldList[fldCnt]. = fieldNodeList[fldCnt].SelectNodes("Custom")[0].InnerText;
                    //                   fieldList[fldCnt].isOptional = Convert.ToBoolean(fieldNodeList[fldCnt].Attributes["IsOptional"].Value);
                }
                m_TableList[tblCnt].fieldDetails = fieldList;
            }
        }

        //By default fields and columns are mapped one to one.
        //The no of Fields and there order listed in the config file should exactly match the actual record values. 
        protected virtual bool ProcessRecordValues(Hashtable fieldIndexHash, string[] fieldValues, out Hashtable fieldValueHash)
        {
            try
            {
                if (fieldValues.Length != m_FileFieldList.Count)
                    throw new ApplicationException("Record Values from file are not matching with Fields read from config file.");
                fieldValueHash = new Hashtable();
                for (int i = 0; i < fieldValues.Length; i++)
                {
                    fieldValueHash[m_FileFieldList[i]] = fieldValues[i];
                }
            }
            catch (Exception ex)
            {
                customImportLog.Error("ProcessRecordValues::Error in processing fieldvalues", ex);
                throw ex;

            }
            return true;
        }

        private DBTransact GetDBTransact()
        {
            if (m_objDBTrans == null)
                m_objDBTrans = new DBTransact(m_ConnString);
            return m_objDBTrans;
        }

        protected virtual bool UpdateDBRecord(TableInfo[] tableList, Hashtable fieldValueHash, string fileName, out bool recExist, out bool recInserted, out bool recUpdated)
        {
            recExist = false;
            try
            {
                DBTransact objDBTrans = GetDBTransact();
                //Try to update the record into database and set the recProcessed flag
                bool recProcessed = objDBTrans.UpdateImportData(m_TableList, fieldValueHash, true, out recExist, out recInserted, out recUpdated);
                return recProcessed;

            }
            catch (Exception ex)
            {
                string msg = "UpdateDBRecord:Error in updating record in DB";
                customImportLog.Error(msg, ex);
                throw ex;
            }
        }


        protected virtual void ResetImport()
        {
            try
            {
                m_ImportedRecCount = 0;
                m_RecExistCount = 0;
                m_ErrRecCount = 0;
                m_ProcessedRecArr.Clear();
                m_ErrorRecArr.Clear();
                m_ErrorRecArrAndLog.Clear();
                m_WarningRecArr.Clear();
                m_PendingRecArr.Clear();
            }
            catch (Exception ex)
            {
                customImportLog.Error("ImportData:ResetImport error : ", ex);
            }

        }



        protected virtual void ProcessRecord(FlatFileRecInfo recInfo, string fileName
            , ArrayList processedRecArr, StreamWriter strWriterProc, StreamWriter strWriterError)
        {
            bool recProcessed = true;
            string records = string.Empty;
            string logHeader = "ImportData::ProcessFile:";
            bool recExist; bool recInserted; bool recUpdated;
            customImportLog.Trace(logHeader + "Entering...");

            try
            {
                foreach (string processedRec in processedRecArr)
                {
                    records += processedRec + "\r\n";
                }
            }
            catch { }

            try
            {
                recProcessed = UpdateDBRecord(m_TableList, recInfo.fieldValueHash, fileName, out recExist, out recInserted, out recUpdated);
                recInfo.recStatus = recProcessed;

                //Update the processed/error file
                string filePath = string.Empty;
                if (recProcessed)
                {
                    m_ImportedRecCount += 1;
                    m_ProcessedRecArr.AddRange(processedRecArr);
                    strWriterProc.WriteLine(records);
                    //strWriterProc.WriteLine(recInfo.recordLine);
                    customImportLog.Trace("ImportData::ProcessRecords=>Valid Rec = " + recInfo.recordLine);
                    customImportLog.Info("File name: " + fileName);
                    customImportLog.Info("Processed records=>Valid Rec = " + records);

                    if (recExist)
                    {
                        if (recUpdated)
                        {
                            m_RecExistCount += 1;
                            customImportLog.Trace("ImportData::RecordExist=>Overriden record is =" + recInfo.recordLine);
                        }
                    }
                    if (recInserted)
                    {
                        m_RecInsertedCount += 1;
                        customImportLog.Trace("ImportData::RecordExist=>Overriden record is =" + recInfo.recordLine);
                    }
                }



                else
                {
                    if (importerName == "ContainerDetails")
                    {
                        m_ErrRecCount += 1;
                        string containerRec = records;
                        containerRec += "Item Qty may have exceeded than the Expected Qty/OverrideQtyInContainer flag set to false /Qty in container blank/Same UCCNo present.";
                        processedRecArr.Clear();
                        processedRecArr.Add(containerRec);
                        m_ErrorRecArr.AddRange(processedRecArr);
                        m_ErrorRecArrAndLog.AddRange(processedRecArr);
                        strWriterError.WriteLine(records);
                        customImportLog.Trace("ImportData::ProcessRecords=>Invalid Rec = " + recInfo.recordLine);
                        customImportLog.Info("File name: " + fileName);
                        customImportLog.Info("Processed records=>Invalid Rec = " + records);
                    }
                    else
                    {
                        m_ErrRecCount += 1;
                        m_ErrorRecArr.AddRange(processedRecArr);
                        m_ErrorRecArrAndLog.AddRange(processedRecArr);
                        strWriterError.WriteLine(records);
                        customImportLog.Trace("ImportData::ProcessRecords=>Invalid Rec = " + recInfo.recordLine);
                        customImportLog.Info("File name: " + fileName);
                        customImportLog.Info("Processed records=>Invalid Rec = " + records);
                    }
                }
            }
            catch (Exception ex)
            {
                //If error in DBConnection it should log the rec in err file
                if (recInfo.fieldValueHash != null)
                {
                    customImportLog.Error(logHeader + "Error Record Details :");
                    foreach (DictionaryEntry dv in recInfo.fieldValueHash)
                    {

                        customImportLog.Error(Environment.NewLine + dv.Key + " = " + dv.Value);
                    }


                }
                customImportLog.Error("ImportData::ProcessFile:Error while processing record=>" + ex.Message, ex);
                customImportLog.Trace(logHeader + "Error while processing record.");
                m_ErrRecCount += 1;
                try
                {
                    string warningMsg = string.Empty;
                    if (IsWarningMessage(ex.Message, out warningMsg))
                    {
                        m_WarningRecArr.AddRange(processedRecArr);
                        foreach (string processedRec in processedRecArr)
                        {
                            strWriterError.WriteLine("Warning : (" + warningMsg + "):" + processedRec);
                        }
                    }
                    else
                    {
                        m_ErrorRecArr.AddRange(processedRecArr);
                        m_ErrorRecArrAndLog.AddRange(processedRecArr);
                        m_ErrorRecArrAndLog.Add(ex.Message);
                        if (recInfo.fieldValueHash != null)
                        {
                            m_ErrorRecArrAndLog.Add(logHeader + "Error Record Details :");
                            foreach (DictionaryEntry dv in recInfo.fieldValueHash)
                            {
                                m_ErrorRecArrAndLog.Add(Environment.NewLine + dv.Key + " = " + dv.Value);
                            }
                        }
                        strWriterError.WriteLine(records);
                        customImportLog.Info("File name: " + fileName);
                        customImportLog.Info("Error records :" + records + " \n " + ex.Message);
                    }

                }
                catch (Exception exp)
                {
                    customImportLog.Error("ProcessRecord::", exp);
                }
            }
        }

        protected virtual bool IsWarningMessage(string excepMsg, out string warningMsg)
        {
            warningMsg = string.Empty;
            return false;

        }


        /// <summary>
        /// Returns the list of strings which if present in the filename the file will be processed.
        /// If Array is null or empty all the files will get processed
        /// </summary>
        /// <returns></returns>
        protected virtual string[] AcceptFileList()
        {
            return null;
        }


        protected virtual void ManualRecordCreate(DataSet dsRecordInfo)
        {

        }
        #endregion overridables

        #region Properties

        public bool IsEnabled
        {
            get
            {
                return m_IsEnabled;
            }
        }

        public Importer ImporterType
        {
            get
            {
                return m_ImporterType;
            }
        }


        #endregion Properties

        #region IImportData Members

        public void ManualCreate(DataSet dsRecordInfo)
        {
            ManualRecordCreate(dsRecordInfo);
        }

        /// <summary>
        /// Enables the  Importer
        /// </summary>
        public void Enable()
        {
            Start();
        }

        /// <summary>
        /// Disables the importer
        /// </summary>
        public void Disable()
        {
            Stop();
        }

        public ImportSettings GetSettings()
        {
            ImportSettings impSettings = new ImportSettings
                (m_ImportStartTime, m_ImportEndTime, m_ImportInterval, m_FileCleanupInterval,
                m_FlatFileDir, m_ConnString, m_NotificationTime, m_SmtpServerName, m_From, m_To, m_Bcc);
            return impSettings;

        }


        public void UpdateSettings(ImportSettings importSettings)
        {
            throw new Exception("Not implemented yet.");
        }

        private string GetFormattedTime(DateTime sDateTime)
        {
            string formattedTime = string.Empty;
            try
            {
                int startPos = -1;

                string tempStr = sDateTime.ToLongTimeString();
                startPos = tempStr.IndexOf(":");
                if (startPos == -1)
                {
                    throw new Exception("DateTime : " +
                        sDateTime.ToLongTimeString() + " not proper format");
                }
                else if (startPos < 2)
                {
                    if (startPos == 1)
                    {
                        tempStr = "0" + tempStr;
                    }
                    else if (startPos == 0)
                    {
                        tempStr = "00" + tempStr;
                    }
                }
                startPos = tempStr.IndexOf(":", 3);
                if (startPos == -1)
                {
                    throw new Exception("DateTime : " +
                        sDateTime.ToLongTimeString() + " not proper format");
                }
                else if (startPos < 5)
                {
                    string firstStr = tempStr.Substring(0, startPos);
                    string lastStr = tempStr.Substring(startPos);
                    //lastStr = lastStr.Trim();
                    if (firstStr.Length == 4)
                    {
                        lastStr = "0" + lastStr;
                    }
                    else if (firstStr.Length == 3)
                    {
                        lastStr = "00" + lastStr;
                    }
                    tempStr = firstStr + lastStr;
                }
                if (tempStr.Length != 11)
                {
                    startPos = tempStr.LastIndexOf(":");
                    string firstStr = tempStr.Substring(0, startPos + 1);
                    string lastStr = tempStr.Substring(startPos + 1);
                    //lastStr = lastStr.Trim();
                    if (lastStr.Length == 4)
                    {
                        lastStr = "0" + lastStr;
                    }
                    else if (lastStr.Length == 3)
                    {
                        lastStr = "00" + lastStr;
                    }
                    tempStr = firstStr + lastStr;
                }
                formattedTime = tempStr;
            }
            catch (Exception ex)
            {
                customImportLog.Error("Error in GetFormattedTime", ex);
                throw ex;
            }
            return formattedTime;
        }

        public string RegisterLogClient()
        {
            try
            {
                m_ClientId++;
                if (!m_ProcessedFileDir.EndsWith(@"\"))
                    m_ProcessedFileDir += @"\";
                LogFileList logFileList = new LogFileList(m_ProcessedFileDir + "ImportLog");
                string clientIdString = String.Format("LOGCLIENT" + "{0:d4}", m_ClientId);
                m_ClientIdLogFileListHash[clientIdString] = logFileList;
                return clientIdString;
            }
            catch (Exception ex)
            {
                customImportLog.Error("RegisterLogClient():Error in registering logClient", ex);
                throw new RemotingException("Error in registering logClient.");
            }

        }

        public void UnRegisterLogClient(string clientId)
        {
            try
            {
                if (m_ClientIdLogFileListHash.ContainsKey(clientId))
                    m_ClientIdLogFileListHash.Remove(clientId);

            }
            catch (Exception ex)
            {
                customImportLog.Error("UnRegisterLogClient():Error in unregistering logClient", ex);
                throw new RemotingException("Error in unregistering logClient.");
            }
        }


        public string GetNextLogMessage(string clientId)
        {
            try
            {
                if (m_ClientIdLogFileListHash.ContainsKey(clientId) == false)
                    throw new Exception("Client not registered.");
                LogFileList logFileList = (LogFileList)m_ClientIdLogFileListHash[clientId];
                return logFileList.MoveNext();
            }
            catch (Exception ex)
            {
                customImportLog.Error("GetNextLog():Error in getting next log msg", ex);
                throw new RemotingException("Error in getting next log msg" + ex.Message);
            }
        }

        public string GetPreviousLogMessage(string clientId)
        {
            try
            {
                if (m_ClientIdLogFileListHash.ContainsKey(clientId) == false)
                    throw new Exception("Client not registered.");
                LogFileList logFileList = (LogFileList)m_ClientIdLogFileListHash[clientId];
                return logFileList.MovePrevious();
            }
            catch (Exception ex)
            {
                customImportLog.Error("GetNextLog():Error in getting previous log msg", ex);
                throw new RemotingException("Error in getting previous log msg" + ex.Message);
            }

        }

        public string GetFirstLogMessage(string clientId)
        {
            try
            {
                if (m_ClientIdLogFileListHash.ContainsKey(clientId) == false)
                    throw new Exception("Client not registered.");
                LogFileList logFileList = (LogFileList)m_ClientIdLogFileListHash[clientId];
                return logFileList.MoveFirst();
            }
            catch (Exception ex)
            {
                customImportLog.Error("GetNextLog():Error in getting first log msg", ex);
                throw new RemotingException("Error in getting first log msg" + ex.Message);
            }


        }

        public string GetLastLogMessage(string clientId)
        {
            try
            {
                if (m_ClientIdLogFileListHash.ContainsKey(clientId) == false)
                    throw new Exception("Client not registered.");
                LogFileList logFileList = (LogFileList)m_ClientIdLogFileListHash[clientId];
                return logFileList.MoveLast();
            }
            catch (Exception ex)
            {
                customImportLog.Error("GetNextLog():Error in getting last log msg", ex);
                throw new RemotingException("Error in getting last log msg" + ex.Message);
            }

        }

        public string StartUploading(string fileName)
        {
            try
            {
                m_FileUploader.RelativePath = m_FlatFileDir;
                return m_FileUploader.StartUpLoad(fileName);
            }
            catch (Exception ex)
            {
                customImportLog.Error("StartUploading:Error starting upload of file", ex);
                throw new RemotingException("Error starting upload of file" + ex.Message);

            }
        }

        public void UpLoadBlock(string sessionID, byte[] fileBlock)
        {
            try
            {
                m_FileUploader.UpLoadBlock(sessionID, fileBlock);
            }
            catch (Exception ex)
            {
                customImportLog.Error("UpLoadBlock:Error starting upload of file", ex);
                throw new RemotingException("Error starting upload of file" + ex.Message);

            }
        }

        public void EndUploading(string sessionId)
        {
            try
            {
                m_FileUploader.EndUpLoad(sessionId);
            }
            catch (Exception ex)
            {
                customImportLog.Error("StartUploading:Error starting upload of file", ex);
                throw new RemotingException("Error starting upload of file" + ex.Message);

            }
        }

        /// <summary>
        /// Processes already uplaoaded file from the configured directory.
        /// </summary>
        /// <param name="fileName">file name without path</param>
        public void ProcessImmediately(string fileName, out int importedRecCnt,
                            out int errorRecCnt, out int warningRecCnt)
        {
            //FileInfo currFileObj = null;
            try
            {
                if (!Directory.Exists(m_FlatFileDir))
                    Directory.CreateDirectory(m_FlatFileDir);
                if (!Directory.Exists(m_ProcessedFileDir))
                    Directory.CreateDirectory(m_ProcessedFileDir);
                if (!Directory.Exists(m_ErrorFileDir))
                    Directory.CreateDirectory(m_ErrorFileDir);
                if (!Directory.Exists(m_ArchiveFileDir))
                    Directory.CreateDirectory(m_ArchiveFileDir);

                if (!m_FlatFileDir.EndsWith(@"\"))
                    m_FlatFileDir += @"\";

                //	currFileObj = new FileInfo(m_FlatFileDir + fileName);
                ProcessFile(fileName, out importedRecCnt, out errorRecCnt, out warningRecCnt);

            }
            catch (Exception ex)
            {
                //fileProcessed = false;
                string msg = ex.Message;
                if (ex.InnerException != null)
                    msg += Environment.NewLine + ex.InnerException.Message;
                customImportLog.Error("ImportData::ProcessImmediately:" + msg, ex);
                customImportLog.Info("ImportData::ProcessImmediately:" + msg, ex);
                throw new RemotingException("Error in Processing the file." + Environment.NewLine + msg);
            }
            finally
            {
                //				if(currFileObj!=null)
                //					currFileObj.Delete();
                File.Delete(m_FlatFileDir + fileName);

            }

        }

        /// <summary>
        /// Creates file from memory stream and processes file .
        /// </summary>
        /// <param name="fileName">file name without path</param>
        /// <param name="fileText"></param>
        /// <param name="importedRecCnt"></param>
        /// <param name="errorRecCnt"></param>
        /// <param name="warningRecCnt"></param>
        public void ProcessImmediately(string fileName, string fileText, out int importedRecCnt,
                            out int errorRecCnt, out int warningRecCnt)
        {
            //FileInfo currFileObj = null;

            try
            {
                if (!Directory.Exists(m_FlatFileDir))
                    Directory.CreateDirectory(m_FlatFileDir);
                if (!Directory.Exists(m_ProcessedFileDir))
                    Directory.CreateDirectory(m_ProcessedFileDir);
                if (!Directory.Exists(m_ErrorFileDir))
                    Directory.CreateDirectory(m_ErrorFileDir);
                if (!Directory.Exists(m_ArchiveFileDir))
                    Directory.CreateDirectory(m_ArchiveFileDir);

                if (!m_FlatFileDir.EndsWith(@"\"))
                    m_FlatFileDir += @"\";
                LoadFileFromStream(ref fileName, fileText);

                //	currFileObj = new FileInfo(m_FlatFileDir + fileName);
                ProcessFile(fileName, out importedRecCnt, out errorRecCnt, out warningRecCnt);

            }
            catch (Exception ex)
            {
                //fileProcessed = false;
                customImportLog.Error("ImportData::ProcessImmediately:Error ", ex);
                throw new RemotingException("Error in Processing the file.");
            }
            finally
            {
                //				if(currFileObj!=null)
                //					currFileObj.Delete();
                File.Delete(m_FlatFileDir + fileName);

            }

        }

        private void LoadFileFromStream(ref string fileName, string fileText)
        {
            if (fileText.Length == 0)
                throw new NoStartUploadException("Invalid File Data");
            if (fileName == string.Empty)
                throw new ApplicationException("Provide valid File Name");
            int index = fileName.LastIndexOf("\\");
            fileName = fileName.Substring(index + 1);
            String filePath = m_FlatFileDir + fileName;

            FileStream fileStream = null;
            try
            {
                fileStream = File.Create(filePath);
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(fileText);
                fileStream.Write(bytes, 0, bytes.Length);
            }
            catch (Exception)
            {
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Flush();
                    fileStream.Close();
                }
            }
        }
        #endregion IImportData Members
    }
}
