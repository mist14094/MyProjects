using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using NLog;
using System.Threading;

namespace KTone.RFIDGlobal
{
    public class KTMsgWriter
    {
        #region [ Attributes ]
        public const bool FILE_APPEND = true;
        public const int MIN_FILESIZE = 1000 ;
        private string componentName = string.Empty;
        private string componentId = string.Empty;
        private long maxFileSizeInKB = 1000*2;
        private string fileName = string.Empty;
        private NLog.Logger log = KTone.RFIDGlobal.KTLogger.KTLogManager.GetLogger();
        private StreamWriter textWriter = null;
        private int numOfBkpFiles = 10 ;
        private string msg = string.Empty;
        private object lockWriteMsg = new object();
        private int bkpFileCount = 0;
        #endregion [ Attributes ]
        
        #region [ Properties ]
        public long MaxFileSizeInKB
        {
            get { return maxFileSizeInKB; }
            set { maxFileSizeInKB = value; }
        }

        public string FileName
        {
            get { return this.fileName; }            
        }

        public string ComponentName
        {
            get { return this.componentName; }
        }

        public string ComponentId
        {
            get { return this.componentId; }
        }
        #endregion [ Properties ]

        #region [ Constructor ]
        public KTMsgWriter(string fileName, string componentId, string componentName, long fileSizeInKB, int numOfBkpFiles, Logger log)
        {
            #region [ Param check ]
            if (fileName == null || fileName.Length <= 0)
                throw new ArgumentNullException(@"FileName is invalid. Null/Empty value not allowed.");

            if (componentId == null || componentId.Length <= 0)
                throw new ArgumentNullException(@"ComponentId is invalid. Null/Empty value not allowed.");

            if (componentName == null || componentName.Length <= 0)
                throw new ArgumentNullException(@"ComponentName is invalid. Null/Empty value not allowed.");

            #endregion [ Param check ]

            this.componentId = componentId;
            this.componentName = componentName;


            if (log != null)
                this.log = log;

            if (fileSizeInKB <= MIN_FILESIZE)
            {
                msg = @"Parameter 'FileSizeInKB' should be greater then " + MIN_FILESIZE.ToString() +
                    "Initializing with defualt value = " + maxFileSizeInKB.ToString();
                log.Trace(msg);
                log.Debug(msg);
                log.Warn(msg);
                msg = string.Empty;

                maxFileSizeInKB = MIN_FILESIZE;
            }
            else
                this.maxFileSizeInKB = fileSizeInKB;

            if (numOfBkpFiles <= 0)
            {
                msg = @"Parameter 'NumOfBkpFiles ' should NOT be less then zero
                    Initializing with default value = 0 (zero)";
                log.Trace(msg);
                log.Debug(msg);
                log.Warn(msg);
                msg = string.Empty;

                numOfBkpFiles = 0;
            }
            else
                this.numOfBkpFiles = numOfBkpFiles;

            this.fileName = fileName;
            textWriter = new StreamWriter(this.fileName, FILE_APPEND);
        }
        #endregion [ Constructor ]

        #region [ Public Methods ]
        public void CloseStreams()
        {

            try
            {
                if (this.textWriter != null)
                {
                    this.textWriter.Close();
                    log.Trace("textWriter : Close the stream successfully.");
                }
                else
                {
                    log.Trace("textWriter : Object is already null.");
                }
            }catch(Exception ex)
            {
                log.ErrorException("Error :" ,ex);
            }
        }

        public bool WriteMsg(string msg)
        {
            log.Trace(@" Entering");
            try
            {
                if (msg != null)
                {
                    lock (lockWriteMsg)
                    {
                        #region [ Create BKP file if size is overflow ]
                        FileInfo fInfo = new FileInfo(this.fileName);
                        long l = fInfo.Length;

                        long sizeInKB = l / 1000;

                        if (sizeInKB >= this.maxFileSizeInKB)
                        {
                            if (bkpFileCount >= numOfBkpFiles)
                                bkpFileCount = 0;

                            string bkpFile = this.fileName + "_BKP" + bkpFileCount.ToString() + ".TXT";
                            log.Debug(" Creating new bkp file with name = " + bkpFile);

                            try
                            {
                                fInfo.CopyTo(bkpFile, true);

                                log.Debug(@" Closing existing file stream....");
                                textWriter.Close();

                                Thread.Sleep(100);

                                log.Debug(@" Deleting existing file.");
                                fInfo.Delete();
                                Thread.Sleep(100);

                                textWriter.Dispose();
                                textWriter = null;
                            }
                            catch (Exception ex)
                            {
                                log.ErrorException(@" Error : " ,ex);
                            }
                            finally
                            {
                                try
                                {
                                    log.Debug(@" ReCreating the file stream from start....");
                                    textWriter = new StreamWriter(this.fileName, FILE_APPEND);
                                }
                                catch (Exception ex)
                                {
                                    log.ErrorException(@" Could not recreate the file : " + this.fileName + " Error = " + ex.Message, ex);
                                }
                            }

                            //fInfo.Delete(); //Delete current file.
                            bkpFileCount++;

                            log.Warn(@" File size is overflow. Creating bkp file as " + bkpFile);
                        }
                        #endregion [ Create BKP file if size is overflow ]
                        textWriter.WriteLine(msg);
                        textWriter.Flush();
                        return true;
                    }//lock (lockWriteMsg)
                }
                //else NO ACTION

                return false;
            }
            catch (Exception ex)
            {
                log.Trace(" Error " + this.componentId + " " + ex.Message);
                return false;
            }
            finally
            {
                log.Trace(@" Leaving.");
            }
        }
        #endregion [ Public Methods ]
    }
}
