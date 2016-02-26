using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Collections;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Reflection;
using KTone.RFIDGlobal.EPCTagEncoding;
using System.Net;

//using NUnit.Framework;
using NLog;

namespace KTone.RFIDGlobal
{
    public class RFUtils
    {
        private static string appIdFolderName = @"KeyToneTech\AppIds";

        private static readonly Logger log = KTone.RFIDGlobal.KTLogger.KTLogManager.GetLogger();

        #region  Run Single Instance of Application
        //Usage 
        /*To make a single instance application, add file SingleApplication.cs in your project. It adds a new class SingleApplication defined in namespace SingleInstance and adds the following code for a form based application to your startup code:
         * static void Main() 
        {
           KTone.Core.Utils.RunSingleInstance(new FrmMain());
        }
        */
        /*
         * FrmMain is the main form class name. The Run method returns false if any other instance of the application is already running. For a console based application, call Run( ) without any parameter and check the return value,
         *  if it is true you can execute your application.Using with console application:

        static void Main() 
        {
        if(KTone.Core.Utils.RunSingleInstance() == false)
        {
        return;
        }
           //Write your program logic here
        }
        */

        static Mutex mutex;
        const int SW_RESTORE = 9;
        static string sTitle;
        static IntPtr windowHandle;
        delegate bool EnumWinCallBack(int hwnd, int lParam);

        [DllImport("user32.Dll")]
        private static extern int EnumWindows(EnumWinCallBack callBackFunc, int lParam);

        [DllImport("User32.Dll")]
        private static extern void GetWindowText(int hWnd, StringBuilder str, int nMaxCount);

        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern Boolean ShowWindow(IntPtr hWnd, Int32 nCmdShow);

        /// <summary>
        /// EnumWindowCallBack
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        private static bool EnumWindowCallBack(int hwnd, int lParam)
        {
            windowHandle = (IntPtr)hwnd;

            StringBuilder sbuilder = new StringBuilder(256);
            GetWindowText((int)windowHandle, sbuilder, sbuilder.Capacity);
            string strTitle = sbuilder.ToString();

            if (strTitle == sTitle)
            {
                ShowWindow(windowHandle, SW_RESTORE);
                SetForegroundWindow(windowHandle);
                return false;
            }
            return true;
        }//EnumWindowCallBack



        /// <summary>
        /// Execute a form base application if another instance already running on
        /// the system activate previous one
        /// </summary>
        /// <param name="frmMain">main form</param>
        /// <returns>true if no previous instance is running</returns>
        public static bool RunSingleInstance(System.Windows.Forms.Form frmMain)
        { 
            return RunSingleInstance(frmMain, string.Empty);
        }
        
        public static bool RunSingleInstance(System.Windows.Forms.Form frmMain, string productName)
        {
            if (IsAlreadyRunning(productName))
            {
                sTitle = frmMain.Text;
                //set focus on previously running app

                EnumWindows(new EnumWinCallBack(EnumWindowCallBack), 0);
                return false;
            }

            System.Windows.Forms.Application.Run(frmMain);
            return true;
        }

        /// <summary>
        /// Execute a form base application if another instance already running on
        /// the system activate previous one
        /// </summary>
        /// <param name="frmMain">main form</param>
        /// <returns>true if no previous instance is running</returns>
        public static bool RunSingleInstance(System.Windows.Forms.Form frmMain, string processName, string processId)
        {
            if (IsAlreadyRunning(string.Empty))
            {
                sTitle = frmMain.Text;
                //set focus on previously running app

                EnumWindows(new EnumWinCallBack(EnumWindowCallBack), 0);
                return false;
            }


            #region [ Creating the app-id txt file ]

            #region [ writer ]

            string appFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

            if (appFolder.EndsWith(@"\") == false) { appFolder += @"\"; }

            string appIdFile = appFolder + appIdFolderName + @"\" + processName + @"-pid.txt";

            if (Directory.Exists(appFolder + appIdFolderName) == false)
            {
                Directory.CreateDirectory(appFolder + appIdFolderName);
            }

            System.IO.StreamWriter objStreamWriter;

            try
            {
                objStreamWriter = new StreamWriter(appIdFile, false);
                objStreamWriter.WriteLine(processId);
                objStreamWriter.Close();
            }
            catch
            { }

            #endregion [ writer ]


            #endregion [ Creating the app-id txt file ]

            System.Windows.Forms.Application.Run(frmMain);
            return true;
        }


        /// <summary>
        /// for console base application
        /// </summary>
        /// <returns></returns>
        public static bool RunSingleInstance()
        {
            if (IsAlreadyRunning(string.Empty))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// check if given exe alread running or not
        /// </summary>
        /// <returns>returns true if already running</returns>
        public static bool IsAlreadyRunning(string productName)
        {
            // Use the EntryAssembly to allow multiple applications 
            // to take advantage of the same routine. 
            // It will create one Mutex per EntryAssembly 
            string strLoc = Assembly.GetEntryAssembly().FullName;

            //string strLoc = Assembly.GetExecutingAssembly().Location;

            FileSystemInfo fileInfo = new FileInfo(strLoc);
            string sExeName = fileInfo.Name;
            if (productName != string.Empty)
                sExeName += productName;
            mutex = new Mutex(true, sExeName);

            if (mutex.WaitOne(0, false))
            {
                return false;
            }
            return true;
        }

      

        #endregion  Run Single Instance of Application 
        
        #region Validate the XML file again XML Schema
        /// <summary>
        ///  Validate THE XML file against Schema 
        /// </summary>
        /// <param name="xsdFileName"></param>
        /// <param name="sr"></param>
        public static void ValidateXML(string xsdFileName, StreamReader sr)
        {
            try
            {
                if (File.Exists(xsdFileName) != true)
                {
                    throw new FileNotFoundException("File NOT Found: " + xsdFileName);
                }

                bool debug = false;
                if (debug)
                {
                    if (sr.BaseStream.CanSeek)
                        sr.BaseStream.Seek(0, SeekOrigin.Begin);
                    string xml = sr.ReadToEnd();
                    log.Trace("String for validation :" + xml);
                }

                XmlSchemaSet schemaSet = new XmlSchemaSet();
                XmlSchema x = new XmlSchema();

                schemaSet.ValidationEventHandler +=
                    new ValidationEventHandler(ValidationCallBack);

                schemaSet.Add(null, xsdFileName);

                if (schemaSet.Count > 0)
                {
                    if (sr.BaseStream.CanSeek)
                        sr.BaseStream.Seek(0, SeekOrigin.Begin);

                    XmlTextReader reader = new XmlTextReader(sr);
                    XmlReaderSettings settings = new XmlReaderSettings();
                    settings.ValidationType = ValidationType.Schema;
                    settings.Schemas = schemaSet;
                    settings.ValidationEventHandler +=
                        new ValidationEventHandler(ValidationCallBack);

                    XmlReader validReader = XmlReader.Create(reader, settings);
                    // read	all	nodes and print	out

                    while (validReader.Read())
                    {
                        // Generates too much tracing needs to be rewritten properly 
                        // TODO - milind 
                        
                        if ( debug ) 
                        {
                            String str = "" ; 
                            if(validReader.NodeType	== XmlNodeType.Element)	
                            {
                                str = String.Format ("<{0}", validReader.Name);
								
                                while(validReader.MoveToNextAttribute())
                                {
                                    str += String.Format("	{0}='{1}'",validReader.Name,
                                        validReader.Value);
									
                                }
                                str += ">";
                                log.Trace( str) ; 
                            }
                            else if(validReader.NodeType ==	XmlNodeType.Text)
                            {
                                log.Trace(validReader.Value);
                            }
                            else if(validReader.NodeType ==	XmlNodeType.EndElement)
                            {
                                str = String.Format("</{0}>",validReader.Name);
                                log.Trace( str) ; 
                            }
                        } 
                        // END - milind 
                        
                    } // end of while 

                }
                else
                    throw new ApplicationException("MalFormed XSDfile:" + xsdFileName);
            }
            catch (ApplicationException ex)
            {
                throw (ex);
            }
            catch (FileNotFoundException fileEx)
            {
                throw (fileEx);
            }
            catch (Exception e)
            {
                throw new ApplicationException("Validation failed: " + e.Message, e);
            }
        }

        private static void ValidationCallBack(object sender, ValidationEventArgs e)
        {
            throw new ApplicationException("Validation Failed:" + e.Message);
        }

        #endregion

        #region Get SocketException Description
        public static string GetSocketExceptionDescription(System.Net.Sockets.SocketException ex)
        {
            //log.Trace("Socket Exception : " + ex.NativeErrorCode);
            switch (ex.NativeErrorCode)
            {

                case 10013:
                    return "Permission denied. ";
                case 10048:
                    return "This port is already in use."
                        + "Another application is running on the host computer.\r\n"
                        + "Please make sure no other application instance is running.";
                case 10060:
                    return "The IP address or host name has failed to respond.\r\n"
                        + "Please check the IP address or host name.";
                case 10061:
                    return "Please make sure the port is reachable.\r\n"
                        + "Please make sure the RFIDReaderWinService is running on the server.";

                case 11001:
                    return "Please make sure the host name or IP Address is reachable.";
                default:
                    return "Unknown networking error has occurred on the server.";
            }
        }
        #endregion

        #region Get Class Names in an assembly

        private static string[] GetClassNames(Assembly assembly, string[] filters,
            bool getAbstractClasses)
        {
            //SortedList is used to store unique class names.
            //No values ar emapped against the key.
            //Evenif the same class name appears multiple times in the referenced assemblies,
            //there will be only one entry of that name .New entry will overwrite the old one.
            SortedList list = new SortedList();

            Type[] types = assembly.GetTypes();
            foreach (Type t in types)
            {

                if (!t.IsClass)
                    continue;
                if (!getAbstractClasses && t.IsAbstract)
                    continue;
                string typeName = t.ToString();

                if (filters != null)
                {
                    for (int i = 0; i < filters.Length; i++)
                    {
                        string filter = filters[i].Trim();
                        if (filter != "")
                        {
                            if (typeName.StartsWith(filter))
                                list[t.ToString()] = null;//Add class name to list
                        }
                    }
                }
                else
                    list[t.ToString()] = null;
            }

            string[] classNameArray = new string[list.Count];
            ICollection listColl = list.Keys;
            listColl.CopyTo(classNameArray, 0);
            return classNameArray;
        }

        /// <summary>
        /// This method returns names of classses present in the assembly 
        /// of the specified type.It also gives names of classes present in
        /// the referenced assemblies.
        /// </summary>
        /// <param name="mainType"></param>
        /// <param name="filters">This is a string array used to filter out
        /// unwanted class names.</param>
        /// <returns>String array containing class names</returns>
        public static string[] GetClassNames(Type mainType, string[] filters,
            bool getAbstractClasses)
        {
            //SortedList is used to store unique class names.
            //No values ar emapped against the key.
            //Evenif the same class name appears multiple times in the referenced assemblies,
            //there will be only one entry of that name .New entry will overwrite the old one.

            #region Get class names in the main assembly.
            Assembly assembly = Assembly.GetAssembly(mainType);
            SortedList list = new SortedList();
            string[] classNamesArray = GetClassNames(assembly, filters,
                getAbstractClasses);

            foreach (string className in classNamesArray)
                list[className] = null;//Add class name to list
            #endregion

            #region Get class names in the referenced Assemblies.
            System.Reflection.AssemblyName[] names
                = mainType.Assembly.GetReferencedAssemblies();

            foreach (System.Reflection.AssemblyName name in names)
            {
                try
                {
                  //  log.Trace("Utils::GetClassNames:Loading the assembly : " + name);
                    Assembly refAssembly = Assembly.Load(name);
                    classNamesArray = GetClassNames(refAssembly, filters,
                        getAbstractClasses);
                    foreach (string className in classNamesArray)
                        list[className] = null;//Add class name to list
                }
                catch
                {
                    //log.Debug("Utils::GetClassNames:Failed to load the assembly : " + name);
                }
            }
            #endregion

            string[] classNameArray = new string[list.Count];
            ICollection listColl = list.Keys;
            listColl.CopyTo(classNameArray, 0);
            return classNameArray;
        }

        #endregion

        #region EPC Tag Id Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="EHU"></param>
        /// <param name="recInfo"></param>
        public static bool ValidateUCCNo(ref string UCCNo, out string errMsg)
        {
            
            errMsg = "";

            if (UCCNo.Length == 16)
                UCCNo = "00" + UCCNo;

            if (UCCNo.Length != 18)
            {
                errMsg = "UCC number length = " + UCCNo.Length.ToString() + " is not valid.";
                return false;
            }
            string ssccNo = UCCNo.Substring(0, 17);
            string checkDigit = UCCNo.Substring(17, 1);

            try
            {
                string chkDigit = GetCheckDigitSSCC(ssccNo);
                if (chkDigit != checkDigit)
                {
                    errMsg = "Invalid CheckDigit in External HU Number.";
                    return false;
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Gets the Check Digit based on SSCC no
        /// </summary>
        /// <param name="sscc"></param>
        /// <returns></returns>
        private static string GetCheckDigitSSCC(string sscc)
        {
            if ((sscc == null) || (sscc.Length != 17))
                throw new ApplicationException(" Unable to calculate the check digit.");
            try
            {
                int[] digits = new int[sscc.Length];
                for (int i = 0; i < digits.Length; i++)
                {
                    digits[i] = int.Parse(sscc.Substring(i, 1));
                }
                int checkValue = 10 - ((((digits[0] + digits[2] + digits[4] + digits[6] + digits[8] + digits[10] + digits[12] + digits[14] + digits[16]) * 3) +
                    (digits[1] + digits[3] + digits[5] + digits[7] + digits[9] + digits[11] + digits[13] + digits[15])) % 10);
                if (checkValue == 10)
                    checkValue = 0;
                return checkValue.ToString();
            }
            catch
            {
                throw new ApplicationException(" Unable to calculate the check digit.");
            }
        }

        /// <summary>
        /// Creates a 12 byte array from given manufacturerId,productId and serial no.
        /// Tag header is a byte with value equal to 1.
        /// </summary>
        /// <param name="manufacturerId">28 bit manufacturerId</param>
        /// <param name="productId">24 bit productId</param>
        /// <param name="serialNo">36 bit serial no.</param>
        /// <returns></returns>
        public static byte[] CreateEPC96SerialNumber(System.UInt32 manufacturerId,
            System.UInt32 productId, System.UInt64 serialNo)
        {
            // EPC tags 96 bits = 12 bytes 
            byte[] serialArr = new byte[12];
            serialArr[0] = 0x35;  // EPC tag header 

            manufacturerId &= 0xfffffff; // only 28 bit long 
            productId &= 0xffffff; // only 24 bit long 
            serialNo &= 0xfffffffff;// only 36 bit long

            System.UInt32 shftManIndt = manufacturerId << 4;
            shftManIndt |= productId >> 20;

            serialArr[1] = (byte)((shftManIndt >> 24) & (System.UInt32)0xff);
            serialArr[2] = (byte)(shftManIndt >> 16 & (System.UInt32)0xff);
            serialArr[3] = (byte)(shftManIndt >> 8 & (System.UInt32)0xff);
            serialArr[4] = (byte)(shftManIndt & (System.UInt32)0xff);


            System.UInt32 nextInt = (productId & 0xfffff) << 4;
            uint firstByteOfSerialNo = (uint)(serialNo >> 32);
            nextInt |= firstByteOfSerialNo;

            serialArr[5] = (byte)((nextInt >> 16) & (System.UInt32)0xff);
            serialArr[6] = (byte)(nextInt >> 8 & (System.UInt32)0xff);
            serialArr[7] = (byte)(nextInt & (System.UInt32)0xff);

            System.UInt64 snInt = (serialNo & 0xffffffff);

            serialArr[8] = (byte)((snInt >> 24) & (System.UInt64)0xff);
            serialArr[9] = (byte)((snInt >> 16) & (System.UInt64)0xff);
            serialArr[10] = (byte)(snInt >> 8 & (System.UInt64)0xff);
            serialArr[11] = (byte)(snInt & (System.UInt64)0xff);

            return serialArr;
        }


        /// <summary>
        /// Creates a 12 byte array from given manufacturerId,productId and serial no.
        /// Tag header is a byte with value equal to 1.
        /// </summary>
        /// <param name="manufacturerId">28 bit manufacturerId</param>
        /// <param name="productId">24 bit productId</param>
        /// <param name="serialNo">byte[] of serial no.</param>
        /// <returns></returns>
        public static byte[] CreateEPC96SerialNumber(System.UInt32 manufacturerId,
            System.UInt32 productId, byte[] serialNo)
        {
            // EPC tags 96 bits = 12 bytes 
            byte[] serialArr = new byte[12];
            serialArr[0] = 0x35;  // EPC tag header 

            manufacturerId &= 0xfffffff; // only 28 bit long 
            productId &= 0xffffff; // only 24 bit long 

            System.UInt32 shftManIndt = manufacturerId << 4;
            shftManIndt |= productId >> 20;

            serialArr[1] = (byte)((shftManIndt >> 24) & (System.UInt32)0xff);
            serialArr[2] = (byte)(shftManIndt >> 16 & (System.UInt32)0xff);
            serialArr[3] = (byte)(shftManIndt >> 8 & (System.UInt32)0xff);
            serialArr[4] = (byte)(shftManIndt & (System.UInt32)0xff);

            System.UInt32 nextInt = (productId & 0xfffff) << 4;

            serialArr[5] = (byte)((nextInt >> 16) & (System.UInt32)0xff);
            serialArr[6] = (byte)(nextInt >> 8 & (System.UInt32)0xff);
            serialArr[7] = (byte)(nextInt & (System.UInt32)0xff);

            // Get nibble from serial number 
            serialArr[7] = (byte)((nextInt & (System.UInt32)0xf0)
                            | (byte)(serialNo[0] & 0xf));

            serialArr[8] = serialNo[1];
            serialArr[9] = serialNo[2];
            serialArr[10] = serialNo[3];
            serialArr[11] = serialNo[4];

            return serialArr;
        }

        /// <summary>
        /// Parses the given 12 byte array and returns manufacturerId,
        /// productId and serial no.
        /// </summary>
        /// <param name="serialNoBytes"></param>
        /// <param name="manufacturerId"></param>
        /// <param name="productId"></param>
        /// <param name="serialNo"></param>
        public static void ParseEPC96SerialNumber(byte[] serialNoBytes,
            out System.UInt32 manufacturerId, out System.UInt32 productId,
            out System.UInt64 serialNo)
        {
            //To do
            manufacturerId = 0;
            productId = 0;
            serialNo = 0;

            uint manufacturerId1stByte = serialNoBytes[1];
            manufacturerId |= manufacturerId1stByte;
            manufacturerId = manufacturerId << 20;

            uint manufacturerId2ndByte = serialNoBytes[2];
            manufacturerId2ndByte = manufacturerId2ndByte << 12;
            manufacturerId |= manufacturerId2ndByte;

            uint manufacturerId3rdByte = serialNoBytes[3];
            manufacturerId3rdByte = manufacturerId3rdByte << 4;
            manufacturerId |= manufacturerId3rdByte;

            uint manufacturerId4thByte = serialNoBytes[4];
            manufacturerId4thByte = manufacturerId4thByte >> 4;
            manufacturerId |= manufacturerId4thByte;

            uint productId1stByte = serialNoBytes[4];
            productId1stByte = (productId1stByte & 0xf);
            productId |= productId1stByte;
            productId = productId << 20;

            uint productId2ndByte = serialNoBytes[5];
            productId2ndByte = productId2ndByte << 12;
            productId |= productId2ndByte;

            uint productId3rdByte = serialNoBytes[6];
            productId3rdByte = productId3rdByte << 4;
            productId |= productId3rdByte;

            uint productId4thByte = serialNoBytes[7];
            productId4thByte = productId4thByte >> 4;
            productId |= productId4thByte;

            UInt64 serialNo1stByte = serialNoBytes[7];
            serialNo1stByte = (serialNo1stByte & 0xf);
            serialNo |= serialNo1stByte;
            serialNo = serialNo << 32;

            UInt64 serialNo2ndByte = serialNoBytes[8];
            serialNo2ndByte = serialNo2ndByte << 24;
            serialNo |= serialNo2ndByte;

            UInt64 serialNo3rdByte = serialNoBytes[9];
            serialNo3rdByte = serialNo3rdByte << 16;
            serialNo |= serialNo3rdByte;

            UInt64 serialNo4thByte = serialNoBytes[10];
            serialNo4thByte = serialNo4thByte << 8;
            serialNo |= serialNo4thByte;

            UInt64 serialNo5thByte = serialNoBytes[11];
            //serialNo5thByte = serialNo5thByte; 
            serialNo |= serialNo5thByte;
        }



        /// <summary>
        /// Creates a 8 byte array from given manufacturerId,productId and serial no.
        /// Tag header is of 2 bits with value equal to 1.
        /// </summary>
        /// <param name="manufacturerId">21 bit manufacturerId</param>
        /// <param name="productId">17 bit productId</param>
        /// <param name="serialNo">24 bit serial no.</param>
        /// <returns></returns>
        public static byte[] CreateEPC64SerialNumber(System.UInt32 manufacturerId,
            System.UInt32 productId, System.UInt64 serialNo)
        {
            // EPC tags 64 bits = 8 bytes 
            byte[] serialArr = new byte[8];

            manufacturerId &= 0x1FFFFF; // only 21 bit long 
            productId &= 0x1FFFF; // only 17 bit long 
            serialNo &= 0xFFFFFF;// only 24 bit long

            uint header = 0x01;  // EPC tag header first 2 bits used as header
            header <<= 21;
            header |= manufacturerId;
            header <<= 9;
            uint shftProductId = productId >> 8;//get first 9 bits from product id
            header |= shftProductId;

            serialArr[0] = (byte)((header >> 24) & (System.UInt32)0xff); //get first byte
            serialArr[1] = (byte)((header >> 16) & (System.UInt32)0xff); //get second byte
            serialArr[2] = (byte)((header >> 8) & (System.UInt32)0xff); //get third byte
            serialArr[3] = (byte)(header & (System.UInt32)0xff); //get fourth byte
            serialArr[4] = (byte)(productId & (System.UInt32)0xff); // get remaining 8 bits from product id

            serialArr[5] = (byte)((serialNo >> 16) & (System.UInt32)0xff); //get first byte
            serialArr[6] = (byte)((serialNo >> 8) & (System.UInt32)0xff); //get second byte
            serialArr[7] = (byte)(serialNo & (System.UInt32)0xff); //get third byte

            return serialArr;
        }



        /// <summary>
        /// Parses the given 8 byte array and returns manufacturerId,
        /// productId and serial no.
        /// </summary>
        /// <param name="serialNoBytes"></param>
        /// <param name="manufacturerId"></param>
        /// <param name="productId"></param>
        /// <param name="serialNo"></param>
        public static void ParseEPC64SerialNumber(byte[] serialNoBytes,
            out System.UInt32 manufacturerId, out System.UInt32 productId,
            out System.UInt32 serialNo)
        {
            //Header : 2 bits
            //Company Id : 21 bits
            //Product Id : 17 bits
            //Serial No : 24 bits

            manufacturerId = 0;
            productId = 0;
            serialNo = 0;

            //Get 6 bits from 0th byte
            uint manufacturerId1stByte = (UInt32)(serialNoBytes[0] & 0x3F);
            manufacturerId |= manufacturerId1stByte;
            manufacturerId = manufacturerId << 15;

            uint manufacturerId2ndByte = serialNoBytes[1];
            manufacturerId2ndByte = manufacturerId2ndByte << 7;
            manufacturerId |= manufacturerId2ndByte;

            //Get 7 bits from 2nd byte
            uint manufacturerId3rdByte = serialNoBytes[2];
            manufacturerId3rdByte = manufacturerId3rdByte >> 1;
            manufacturerId |= manufacturerId3rdByte;

            uint productId1stByte = serialNoBytes[2];
            productId1stByte = (productId1stByte & 0x1);
            productId |= productId1stByte;
            productId = productId << 16;

            uint productId2ndByte = serialNoBytes[3];
            productId2ndByte = productId2ndByte << 8;
            productId |= productId2ndByte;

            uint productId3rdByte = serialNoBytes[4];
            productId |= productId3rdByte;

            uint serialNo1stByte = serialNoBytes[5];
            serialNo1stByte = serialNo1stByte << 16;
            serialNo |= serialNo1stByte;

            uint serialNo2ndByte = serialNoBytes[6];
            serialNo2ndByte = serialNo2ndByte << 8;
            serialNo |= serialNo2ndByte;

            uint serialNo3rdByte = serialNoBytes[7];
            serialNo |= serialNo3rdByte;
        }


        #endregion



        //contains several methods for manipulating values between different number systems.
        #region EPCTagEncodingMethods

        /// <summary>
            /// Converts a Hexadecimal value to its corresponding Decimal value
            /// </summary>
            /// <param name="strHex">The Hexadecimal value as string</param>
            /// <returns>The resulting decimal value as string</returns>
            public static string ConvertHexToDecimal(string strHex)
            {
                long mintTempHex = 0L;
                for (int i = 0, j = (strHex.Length - 1); i < strHex.Length; i++, j--)
                {
                    string strPart = "";
                    switch (strHex.Substring(j, 1).ToUpper())
                    {
                        case "A":
                            strPart = "10";
                            break;
                        case "B":
                            strPart = "11";
                            break;
                        case "C":
                            strPart = "12";
                            break;
                        case "D":
                            strPart = "13";
                            break;
                        case "E":
                            strPart = "14";
                            break;
                        case "F":
                            strPart = "15";
                            break;
                        default:
                            strPart = strHex.Substring(j, 1);
                            break;
                    }
                    mintTempHex += Convert.ToSByte(strPart) * Convert.ToInt64(Math.Pow(16, i));
                }
                return mintTempHex.ToString();
            }

            /// <summary>
            /// Connverts a Decimal value to its corresponding binary value
            /// </summary>
            /// <param name="mintNum">Supplied number to convert</param>
            /// <returns>The resultant Binary value as string</returns>
            public static string ConvertDecimalToBinary(System.UInt64 mintNum)
            {
                StringBuilder strTempBin = new StringBuilder();
                StringBuilder strBin = new StringBuilder();


                try
                {
                    while (mintNum >= 1)
                    {
                        UInt64 mintTemp = (mintNum * 10) / 4;
                        while (mintTemp > 1)
                        {
                            mintTemp = mintTemp - 5;
                        }
                        if (mintTemp < 0)
                        {
                            mintNum = (mintNum - 1) / 2;
                            strTempBin.Append("1");
                        }
                        else
                        {
                            mintNum = mintNum / 2;
                            strTempBin.Append("0");
                        }
                    }
                    for (int i = (strTempBin.Length - 1); i >= 0; i--)
                    {
                        strBin.Append(strTempBin.ToString().Substring(i, 1));
                    }
                }
                catch
                {
                    Console.WriteLine("ConvertDecimalToBinary() :Exception Caught :");
                }
                return strBin.ToString();
            }
            /// <summary>
            /// Method to convert binary number to decimal number 
            /// </summary>
            /// <param name="strBin">Supplied binary number</param>
            /// <returns>Calculated decimal number</returns>
            public static string ConvertBinaryToDecimal(string strBin)
            {
                System.Int64 mintTemp = 0;
                for (int i = 0, j = (strBin.Length - 1); i < strBin.Length; i++, j--)
                {
                    mintTemp += Convert.ToInt64(strBin.Substring(j, 1)) * Convert.ToInt64(Math.Pow(2, i));
                }

                return mintTemp.ToString();
            }
            /// <summary>
            /// Method to insert reqd number of zeros to the left of a number to make it of a specific length.(Applicable to both decimal and binary number systems)
            /// </summary>
            /// <param name="mintNo">Supplied number</param>
            /// <param name="mintNoOfTotalBits">Inteneded length of the number after filling zeros to the left</param>
            /// <returns>Calculated zero filled number</returns>
            public static string AddReqdZeros(string mintNo, int mintNoOfTotalBits)
            {
                string strBin = mintNo;
                int mintStrLen = strBin.Length;
                StringBuilder strZeroPrefix = new StringBuilder();

                if (mintStrLen < mintNoOfTotalBits)
                {
                    int mintZerosToAdd = mintNoOfTotalBits - mintStrLen;

                    for (int i = 0; i < mintZerosToAdd; i++)
                    {
                        strZeroPrefix.Append("0");
                    }
                }

                return strZeroPrefix.ToString() + strBin;
            }


            public static string CalculateCheckDigit(string companyPrefixItemRef, int stringLength)
            {
                string chkDigitString = string.Empty;

                int prefixItemRefLength = companyPrefixItemRef.Length;

                if (prefixItemRefLength < stringLength)
                {
                    companyPrefixItemRef = companyPrefixItemRef.PadLeft(stringLength, '0');
                }

                chkDigitString = companyPrefixItemRef;
                //Calculate the checkdigit

                System.Int32 oddDigitTotal = 0;
                //first extract the odd digits from chkDigitString
                for (int oddIndex = 0; oddIndex <= stringLength; oddIndex += 2)
                {
                    oddDigitTotal += Convert.ToInt32(chkDigitString.Substring(oddIndex, 1));
                }

                //Xtract the even digits from chkDigitString
                System.Int32 evenDigitTotal = 0;
                for (int evenIndex = 1; evenIndex < stringLength; evenIndex += 2)
                {
                    evenDigitTotal += Convert.ToInt32(chkDigitString.Substring(evenIndex, 1));
                }

                string calculatedCheckDigit = Convert.ToString((1000 - (3 * (oddDigitTotal) + evenDigitTotal)) % 10).Trim();

                return calculatedCheckDigit;
            }


            public static byte[] StringToByteArray(string inputBinary)
            {
                int byteArrLength = inputBinary.Length / 8;

                byte[] formatSpecificByteArr = new byte[byteArrLength];

                string str8BitsStore = string.Empty;

                //Insert the string values to byte array
                for (int arrIndex = 0, startValue = 0; arrIndex < byteArrLength; arrIndex++, startValue += 8)
                {
                    str8BitsStore = inputBinary.Substring(startValue, 8);
                    formatSpecificByteArr[arrIndex] = Convert.ToByte(str8BitsStore, 2);
                }
                return formatSpecificByteArr;
            }


            public static UInt16 FetchCompanyPrefixIndex(string companyPrefix)
            {
                UInt16 companyPrefixIndex = 0;
                ICompanyPrefixLookup iLookup = CompanyPrefixLookupImpl.GetInstanceOf();

                try
                {
                    bool isValid = iLookup.Lookup(companyPrefix, out companyPrefixIndex);

                    if (!isValid)
                        throw new CompanyPrefixLookupImplException("RFUtils.FetchCompanyPrefix() : Company Prefix not listed in Translation Table : Value of Comp Prefix :" + companyPrefix);
                }
                catch (CompanyPrefixLookupImplException e)
                {
                    throw e;
                }

                //Validate copany prefix
                if (Convert.ToInt64(companyPrefixIndex) > 16383)
                {
                    throw new InvalidSGTIN64EncodingException("RFUtils.FetchCompanyPrefix() This is not a valid company prefix value for GTIN");
                }

                return companyPrefixIndex;
            }


            public static string FetchCompanyPrefix(UInt16 compPrefixIndex)
            {
                string companyPrefix = string.Empty;

                //Validate copany prefix
                if (Convert.ToInt64(compPrefixIndex) > 16383)
                {
                    throw new InvalidSGTIN64EncodingException("RFUtils.FetchCompanyPrefix() This is not a valid company prefix value for GTIN");
                }

                ICompanyPrefixLookup iLookup = CompanyPrefixLookupImpl.GetInstanceOf();

                try
                {
                    companyPrefix = iLookup.Lookup(compPrefixIndex);
                }
                catch (CompanyPrefixLookupImplException e)
                {
                    throw e;
                }
                return companyPrefix;
            }

        /*
            public static string ByteArrayToString(byte[] byteArray)
            {
                if (byteArray == null)
                {
                    throw new EPCTagExceptionBase("ByteArrayToString() : Input byte array is null.");
                }

                StringBuilder strbEncodedString = new StringBuilder();

                for (int byteArrayIndex = 0; byteArrayIndex < byteArray.Length; byteArrayIndex++)
                {
                    strbEncodedString.Append(AddReqdZeros(Convert.ToString(byteArray[byteArrayIndex], 2).Trim(), 8));
                }
                return strbEncodedString.ToString();
            }*/

            #region Helper Functions ::The display and compare routines
            public static void Display(Hashtable dispHash)
            {
                //log.Trace("Entering SGTINTests::Display()");

                if (dispHash == null)
                {
                    throw new ApplicationException("[SGTINTests:Display()]:: dispHash is null.");
                }
                else if (dispHash.Count.Equals(0))
                {
                    Console.WriteLine(" No Input parameters.");
                }
                else
                {
                    String str = String.Empty;
                    IDictionaryEnumerator expectedEnum = dispHash.GetEnumerator();
                    while (expectedEnum.MoveNext())
                    {
                        str = expectedEnum.Key + " : ";
                        Type valType = expectedEnum.Value.GetType();
                        //To display array of Bytes
                        if (valType.IsArray)
                        {
                            if (valType.GetElementType() == typeof(System.Byte))
                            {
                                byte[] byteArray = (byte[])expectedEnum.Value;
                                for (int byteArrayIndex = 0; byteArrayIndex < byteArray.Length; byteArrayIndex++)
                                {
                                    str += "0x" + byteArray[byteArrayIndex].ToString("X2") + " ";
                                }
                            }
                        }
                        //To display single Byte
                        else if (valType == Type.GetType("System.Byte"))
                        {
                            str += "0x" + ((byte)(expectedEnum.Value)).ToString("X2");
                        }
                        else//To display any other Datatype value.i.e string ,char,int,etc
                        {
                            str += expectedEnum.Value;
                        }
                        Console.WriteLine(str);
                    }
                }
                //log.Trace("Leaving SGTINTests::Display()");
            }
            public static void Display(byte[] tempByteArray)
            {
                //log.Trace("Entering SGTINTests::Display()");
                if (tempByteArray == null)
                {
                    throw new ApplicationException("[SGTINTests:Display()]:: tempByteArray is null.");
                }
                int i = 0;
                foreach (byte b in tempByteArray)
                {
                    Console.WriteLine("0x" + b.ToString("X2") + " ");
                    i++;
                }
                Console.WriteLine();
                //log.Trace("Leaving SGTINTests::Display()");
            }
            #endregion

            /// <summary>
            /// Returns SSCC Barcode strings in 2 formats
            /// -spaced 
            /// -Number format without any spaces
            /// For usage of this function refer to PrintTrackTag Application's frmPrintLabel.cs file
            /// </summary>
            /// <param name="extensiondigit"></param>
            /// <param name="companyPrefix"></param>
            /// <param name="serialRef"></param>
            /// <param name="ssccNo"></param>
            /// <param name="sscc"></param>
            public static void GetSSCCBarcode(string extensiondigit, string companyPrefix, string serialRef, out string ssccNo, out string sscc)
            {
                ssccNo = string.Empty;
                sscc = string.Empty;
                try
                {
                    sscc = extensiondigit + " " + companyPrefix + " " + serialRef;//4 0013000 000010000
                    ssccNo = sscc.Replace(" ", "");//40013000000010000
                    sscc = @"(00) " + sscc + " " + CalculateCheckDigit(ssccNo, Constants.SSCC18_LEN - 1);
                    ssccNo = sscc.TrimStart(new char[] { '(' }).Replace(")", "").Replace(@" ", "");
                }
                catch (Exception e)
                {
                    throw new ApplicationException(e.Message, e.InnerException);
                }
            }



            /// <summary>
            /// Returns SGTIN Barcode strings in 2 formats
            /// -spaced 
            /// -Number format without any spaces
            /// For usage of this function refer to PrintTrackTag Application's frmPrintLabel.cs file
            /// </summary>
            /// <param name="companyPrefix"></param>
            /// <param name="productPrefix"></param>
            /// <param name="sgtin"></param>
            /// <param name="sgtinNo"></param>
            public static void GetSGTINBarcode(string companyPrefix, string productPrefix, out string sgtin, out string sgtinNo)
            {
                sgtin = string.Empty;
                sgtinNo = string.Empty;

                try
                {
                    sgtin = @"(01) " + companyPrefix + " " + productPrefix + " "
                        + CalculateCheckDigit(companyPrefix + productPrefix, Constants.SGLN13_LEN);//GetCheckDigitSGTIN13(companyPrefix + productPrefix);
                    sgtinNo = sgtin.TrimStart(new char[] { '(' }).Replace(")", "").Replace(@" ", "");

                }
                catch (Exception e)
                {
                    throw new ApplicationException(e.Message, e.InnerException);
                }
            }

            public static void GetLCTNBarcode(string companyPrefix, out string lctn)
            {
                try
                {
                    lctn = EncodeLCTN.CreateLCTN13(companyPrefix, LCTNHelper.LOCATION_REF);
                }
                catch (Exception e)
                {
                    throw new ApplicationException(e.Message, e.InnerException);
                }
            }

            public static void GetASETBarcode(string companyPrefix, out string aset)
            {
                try
                {
                    aset = EncodeASET.CreateASET13(companyPrefix, LCTNHelper.LOCATION_REF);
                }
                catch (Exception e)
                {
                    throw new ApplicationException(e.Message, e.InnerException);
                }
            }


            #endregion EPCTagEncodingMethods

        public static string ByteArrayToHexString(byte[] dataBytes)
        { 
            return ByteArrayToHexString(dataBytes, true, ' ');
        }

        public static string ByteArrayToHexString(byte[] dataBytes, bool useSeparator)
        {
            return ByteArrayToHexString(dataBytes, useSeparator, ' ');
        }
        public static string HextoAscii(string hex)
        {
            return HextoAscii(hex, 24);
        }
        public static string HextoAscii(byte[] hexValues)
        {
            return HextoAscii(hexValues, 12);
        }


        /// <summary>
        /// splits the connection string and fills the out parameters.
        /// </summary>
        /// <param name="connectionString">connection string to be splitted</param>
        /// <param name="database">database name</param>
        /// <param name="server">server name</param>
        /// <param name="userId">user id</param>
        /// <param name="password">password</param>
        public static void SplitDBConnectionStr(string connectionString,out string database,out string server,out string userId,out string password)
        {
            string[] connStrArray = connectionString.Split(';');
            database = string.Empty;
            server = string.Empty;
            userId = string.Empty;
            password = string.Empty;
            int count = connStrArray.Length;
            Dictionary<string, string> connParamsDic = new Dictionary<string, string>();
            if (count > 0)
            {
                foreach (string connStr in connStrArray)
                {
                    string[] splitConnStrArray = connStr.Split('=');
                    if (splitConnStrArray.Length > 1)
                        connParamsDic[splitConnStrArray[0].Trim()] = splitConnStrArray[1].Trim();
                }
            }

            foreach (KeyValuePair<string, string> keyValue in connParamsDic)
            {
                switch (keyValue.Key.ToLower())
                {
                    case "database":
                        database = connParamsDic[keyValue.Key];
                        break;
                    case "server":
                        server = connParamsDic[keyValue.Key];
                        break;
                    case "user id":
                        userId = connParamsDic[keyValue.Key];
                        break;
                    case "password":
                        password = connParamsDic[keyValue.Key];
                        break;
                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// splits the encoded connection string and fills the out parameters.
        /// </summary>
        /// <param name="encodedConnectionString">encoded connection string to be splitted</param>
        /// <param name="database">database name</param>
        /// <param name="server">server name</param>
        /// <param name="userId">user Id</param>
        /// <param name="password">password</param>
        public static void SplitEncodedDBConnectionStr(string encodedConnectionString, out string database, out string server, out string userId, out string password)
        {
            string connectionString = Decode(encodedConnectionString);
            SplitDBConnectionStr(connectionString,out database,out server,out userId,out password);
        }
        /// <summary>
        /// combines the parameters into connection string.
        /// </summary>
        /// <param name="database">database name</param>
        /// <param name="server">server name</param>
        /// <param name="userId">user Id</param>
        /// <param name="password">password</param>
        /// <returns>connection string</returns>
        public static string CreateDBConnectionStr(string database,string server,string userId,string password)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("database=");
            sb.Append(database);
            sb.Append(";server=");
            sb.Append(server);
            sb.Append(";user id=" );
            sb.Append(userId);
            sb.Append(";password=");
            sb.Append(password);
            sb.Append(";");
            return  sb.ToString();
        }
        /// <summary>
        /// Combines the parameters into encoded connection string.
        /// </summary>
        /// <param name="database">database name</param>
        /// <param name="server">server name</param>
        /// <param name="userId">user Id</param>
        /// <param name="password">password</param>
        /// <returns>encoded connection string</returns>
        public static string CreateEncodedDBConnectionStr(string database, string server, string userId, string password)
        {
            return Encode(CreateDBConnectionStr(database, server, userId, password));
        }
        /// <summary>
        /// Converts byte array to Hex, returns value in string form 
        /// </summary>
        /// <param name="dataBytes"></param>
        /// <returns>hex string</returns>


        public static string HextoAscii(string hex ,int hexlength)
        {
            StringBuilder strAsciiValue = new StringBuilder();
            for (int j = 0; j < hexlength; j = j + 2)
            {
                string hexVal = (hex[j].ToString() + hex[j + 1].ToString());
                byte bytehexVal = Convert.ToByte(hexVal);
                if (bytehexVal > 29 && bytehexVal < 40)
                    strAsciiValue.Append(hexVal);
                else
                    break;
            }
            return strAsciiValue.ToString();
        }

        public static string HextoAscii(byte[] hexValues ,int hexlength)
        {
            StringBuilder strAsciiValue = new StringBuilder();
            for (int j = 0; j < hexlength; j = j + 1)
            {
                byte bytehexVal = Convert.ToByte(hexValues[j]);
                if (bytehexVal > 47 && bytehexVal < 58)
                    strAsciiValue.Append(bytehexVal.ToString("x"));
                else
                    break;
            }
            return strAsciiValue.ToString();
        }

        public static string ByteArrayToHexString(byte[] dataBytes, bool useSeparator, char separatorChar)
        {
            if (dataBytes == null)
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < dataBytes.Length; i++)
            {
                sb.Append(dataBytes[i].ToString("X2"));
                if (useSeparator)
                    sb.Append(separatorChar);
            }
            return sb.ToString();
        }

        public static string ByteArrayToString(byte[] dataBytes)
        {
            if (dataBytes == null)
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < dataBytes.Length; i++)
                sb.Append(dataBytes[i].ToString() + " ");
            return sb.ToString();
        }

        public static byte[] HexStringToByteArray(string input)
        {
            if (input == null)
                return null;

            if (input.Length % 2 != 0)
                input = "0" + input;

            byte[] result = new byte[input.Length / 2];
            for (int i = 0; i < input.Length; )
            {
                string chunk = input.Substring(i, 2);
                result[i / 2] = byte.Parse(chunk, System.Globalization.NumberStyles.HexNumber);
                i += 2;
            }
            return result;
        }

        /// <summary>
        /// Merge two byte arrays, second byte array appended to the first.
        /// </summary>
        /// <param name="firstResponseByteArray"></param>
        /// <param name="secondResponseByteArray"></param>
        /// <returns>merged byte array</returns>
        public static byte[] MergeArrays(byte[] firstResponseByteArray, byte[] secondResponseByteArray)
        {
            byte[] mergedByteArray = null;
            if (firstResponseByteArray == null)
            {
                mergedByteArray = secondResponseByteArray;
                return mergedByteArray;
            }
            else if (secondResponseByteArray == null)
            {
                mergedByteArray = firstResponseByteArray;
                return mergedByteArray;
            }

            int mergedArrayLength = firstResponseByteArray.Length
                + secondResponseByteArray.Length;

            mergedByteArray = new byte[mergedArrayLength];

            int mIndex = 0;
            for (int iIndex = 0;
                iIndex < firstResponseByteArray.Length;
                iIndex++, mIndex++)
                mergedByteArray[mIndex] = firstResponseByteArray[iIndex];

            for (int rIndex = 0;
                rIndex < secondResponseByteArray.Length;
                rIndex++, mIndex++)
                mergedByteArray[mIndex] = secondResponseByteArray[rIndex];

            return mergedByteArray;

        }

        private static XmlNode CheckXmlNode(XmlNode parentNode, string nodeName)
        {
            if (parentNode.Name == nodeName)
                return parentNode;

            if (!parentNode.HasChildNodes)
                return null;

            XmlNodeList nodeList = parentNode.ChildNodes;

            foreach (XmlNode childNode in nodeList)
            {
                XmlNode node = CheckXmlNode(childNode, nodeName);
                if (node != null)
                    return node;
            }
            return null;
        }

        /// <summary>
        /// fetch xmlnode from given xmldoc corresponding to given nodename
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="nodeName"></param>
        /// <returns>xmlnode</returns>
        public static XmlNode GetXmlNode(XmlDocument doc, string nodeName)
        {
            XmlNodeList nodeList = doc.ChildNodes;
            XmlNode nodeFound = null;

            foreach (XmlNode node in nodeList)
            {
                nodeFound = CheckXmlNode(node, nodeName);
                if (nodeFound != null)
                    break;
            }
            return nodeFound;
        }

        /// <summary>
        /// Returns IPAddress of the local host.
        /// </summary>
        /// <returns></returns>
        public static IPAddress GetLocalHostIPAddress()
        {
            return GetIPAddress(string.Empty);
        }

        /// <summary>
        /// if local ip is 127.0.0.1 or localhost or machine name or Actual IP it will return local host IP
        /// </summary>
        /// <param name="ipStr"></param>
        /// <returns></returns>
        public static IPAddress GetIPAddress(string ipStr)
        {
            //GetHostEntry does a DNS reverse lookup for the given IPAddress.
            //If the reverse entry is not there, it may throw exception.
            //Hence, we are using GetHostAddresses instead of GetHostEntry.

            string errorMsg = "Failed to get local host IP address for " + ipStr;

            IPAddress ip = null;
            try
            {

                if (ipStr == null || ipStr == string.Empty || 
                     ipStr.ToLower() == "localhost")
                {

                    string hostName = Dns.GetHostName();
                    IPAddress[] ipAddresses = Dns.GetHostAddresses(hostName);
                    if (ipAddresses == null)
                        log.Error("No IP addresses found for " + ipStr);
                    else
                    {
                        //ip = ipAddresses[0];
                        foreach (IPAddress IPA in ipAddresses)
                        {
                            if (IPA.AddressFamily.ToString() == "InterNetwork")
                            {
                                ip = IPA;
                                log.Trace("InterNetwork IP Address found : " + ip.ToString());
                                break;
                            }
                        }
                        if(ip == null)
                            log.Error("No InterNetwork IP Address found");
                    }
                }
                else
                {
                    IPAddress[] ipAddresses = Dns.GetHostAddresses(ipStr);
                    if (ipAddresses == null)
                        log.Error("No IP addresses found for " + ipStr);
                    else
                    {
                        ipStr = ipStr.Trim();
                        foreach (IPAddress ipAddress in ipAddresses)
                        {
                            if (ipAddress.ToString().Equals(ipStr))
                            {
                                ip = ipAddress;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.ErrorException(errorMsg, ex);
                throw new ApplicationException(errorMsg, ex);
            }
            if (ip == null)
                log.Error(errorMsg);
            else
                log.Trace("IP address found for " + ipStr + " : " + ip.ToString());

            return ip;
        }

        public static IPAddress GetHostBindToIpAddress()
        {
            IPAddress defaultBindToAddress = IPAddress.Parse("127.0.0.1");
            string hostName = "localhost";
            try
            {
                string bindToAddress = string.Empty;
                KTone.RFIDGlobal.ConfigParams.GlobalConfigParams.Lookup("BindTo", out bindToAddress);

                #region Check if bindToAddress is empty
                if (bindToAddress == string.Empty)
                {
                    log.Debug("BindTo entry not found in the global config. Returning default address 127.0.0.1");
                    return defaultBindToAddress;
                }
                #endregion Check if bindToAddress is empty

                #region Check if list of ip addrssses is null
                hostName = Dns.GetHostName();
                IPAddress[] ipAddresses = Dns.GetHostAddresses(hostName);
                if (ipAddresses == null)
                {
                    log.Debug("Host IpAddress not found for " + hostName + " Returning default address 127.0.0.1");
                    return defaultBindToAddress;
                }
                #endregion Check if list of ip addrssses is null

                #region Check if valid Ip address
                foreach (IPAddress ip in ipAddresses)
                {
                    if (ip.ToString().Equals(bindToAddress))
                        return IPAddress.Parse(bindToAddress);
                }

                log.Debug("Inavalifd BindTo ip address " + bindToAddress + " Returning default address 127.0.0.1");
                return defaultBindToAddress;
                #endregion Check if valid Ip address
            }
            catch (Exception ex)
            {
                log.ErrorException("Failed to get IPAddress for " + hostName + " Returning default address 127.0.0.1", ex);
                return defaultBindToAddress;
            }
        }

        /// <summary>
        /// Function to produce the build date for the currently executing assembly.
        /// This ONLY works if the assembly was built using VS.NET and the assembly version attribute is 
        /// set to something like the below. The asterisk (*) is the important part, as if present, 
        /// VS.NET generates both the build and revision numbers automatically.<Assembly: AssemblyVersion("1.0.*")> 
        /// </summary>
        /// <returns></returns>
        public static DateTime GetBuildDate()
        {
            //Build dates start from 01/01/2000

            DateTime buildTime = new DateTime(2000, 1, 1);
            try
            {

                //Retrieve the version information from the assembly from which this code is being executed

                //System.Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                Assembly assembly = System.Reflection.Assembly.GetEntryAssembly();
                System.Version version = assembly.GetName().Version;

                //Add the number of days (build)

                buildTime = buildTime.AddDays(version.Build);

                //Add the number of seconds since midnight (revision) multiplied by 2

                buildTime = buildTime.AddSeconds(version.Revision * 2);

                //If we're currently in daylight saving time add an extra hour

                if (TimeZone.IsDaylightSavingTime(DateTime.Now, TimeZone.CurrentTimeZone.GetDaylightChanges(DateTime.Now.Year)))
                    buildTime = buildTime.AddHours(1);

                log.Info(string.Format("Assembly : {0}, Build Version : {1}, Build Date : {2}",
                    assembly.GetName().Name,version.ToString(), buildTime.ToString()));
            }
            catch (Exception ex)
            {
                log.ErrorException("Failed to get build time information", ex);
            }
            return buildTime;
        }
        public static string Encode(string str)
        {
            try
            {
                if (str != null && str != string.Empty)
                {
                    return Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(str));

                }
                else
                {
                    throw new ApplicationException("Input string is null or empty");
                }
            }
            catch (Exception)
            {
                throw new ApplicationException("Input string is null or empty");
            }

        }
        public static string Decode(string str)
        {
            try
            {
                if (str != null && str != string.Empty)
                {
                    return ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(str));

                }
                else
                {
                    throw new ApplicationException("Input string is null or empty");
                }

            }
            catch (Exception)
            {
                throw new ApplicationException("Input string is null or empty");
            }

        }
    }
}
