
/********************************************************************************************************
Copyright (c) 2010 KeyTone Technologies.All Right Reserved

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
using System.Text;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net;
using System.Collections;
//using KTone.Core.KTComponent;
//using KTone.RFID.Simulator;
//using KTone.Common.KTUtils.KTLogger;
//using KTone.RFID;
//using KTone.Core.KTIRFID;
using NLog;


namespace KTone.RFIDGlobal
{
	/// <summary>Delegate to generate details of TagTypeCount user control</summary>
    //public delegate void ConfigTagEventHandler(object sender, ConfigTagEventArgs e);
	/// <summary>Contain Methods, Fields and Properties need by multiple classes in SimApp.</summary>
	public class Utilities
	{
		public static bool tagsChanged = false;
		public const string APPLICATION_TITLE = "Simulator";

        //public static EthernetPortInfo m_PortInfo = null;
        //private static IPAddress m_LocalIP = null;

		#region Tag Description Constants
		public const string EPC0_96BIT_DESC = "EPC Class 0 Tag - 96 bit";
		public const string EPC1_96BIT_DESC = "EPC Class 1 Tag - 96 bit";
		public const string EPC0_64BIT_DESC = "EPC Class 0 Tag - 64 bit";
		public const string EPC1_64BIT_DESC = "EPC Class 1 Tag - 64 bit";
		public const string EMS_LRP_DESC	= "EMS LRP Read/Write Tag";
		public const string EMS_HMS_DESC	= "EMS HMS Read/Write Tag";
		#endregion

        //private static readonly NLog.Logger m_log
        //    = KTone.Common.KTUtils.KTLogger.KTLogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().Name);
        private static readonly Logger m_log = KTone.RFIDGlobal.KTLogger.KTLogManager.GetLogger();

		static Utilities()
		{
			//m_LocalIP initailized in FrmMain.Load 
			/*
			if (m_PortInfo == null)
			{
				ArrayList usedPorts = new ArrayList();
				usedPorts.Add("17000");
				usedPorts.Add("17001");
				usedPorts.Add("17002");
				usedPorts.Add("17003");
				usedPorts.Add("17004");
				usedPorts.Add("17005");
				usedPorts.Add("17006");
				usedPorts.Add("17007");
				usedPorts.Add("17008");
				usedPorts.Add("17009");
				usedPorts.Add("17010");
				usedPorts.Add("17500");
				usedPorts.Add("17100");
				usedPorts.Add("17101");
				usedPorts.Add("17102");
				usedPorts.Add("17103");
				usedPorts.Add("17104");
				usedPorts.Add("17105");
				usedPorts.Add("17106");
				usedPorts.Add("17107");
				usedPorts.Add("17108");
				usedPorts.Add("17109");
				usedPorts.Add("17110");
				m_LocalIP	= Dns.GetHostEntry("localhost").AddressList[0];
				m_PortInfo	= EthernetPortInfo.getInstance(m_LocalIP, usedPorts);
			}
			*/			
		}

		#region Public Events
        //public delegate void TagListChangedEventHandler(EventArgs e);
        //public static event TagListChangedEventHandler TagListChanged;
		
        //public delegate void ReaderReconfiguredHandler(EventArgs e);
        //public static event ReaderReconfiguredHandler ReaderReconfigured ;

        //public delegate void ReaderStatusChangedHandler(EventArgs e);
        //public static event ReaderStatusChangedHandler ReaderStatusChanged;
		#endregion
		/// <summary>Encoding Types.</summary>
		public enum EncodeType
		{
			EPC64Bit = 1,
			EPC96Bit,
			USDOD64Bit,
			USDOD96Bit,
			Others
		}

		public enum DigitalIOType
		{
			Input = 1,
			Output,
			Generic
		}
		
        //public static IPAddress LocalIP
        //{
        //    get
        //    {
        //        return m_LocalIP;
        //    }
        //    set
        //    {
        //        m_LocalIP	= value;
				
        //        ArrayList usedPorts = new ArrayList();
        //        usedPorts.Add("17000");
        //        usedPorts.Add("17001");
        //        usedPorts.Add("17002");
        //        usedPorts.Add("17003");
        //        usedPorts.Add("17004");
        //        usedPorts.Add("17005");
        //        usedPorts.Add("17006");
        //        usedPorts.Add("17007");
        //        usedPorts.Add("17008");
        //        usedPorts.Add("17009");
        //        usedPorts.Add("17010");
        //        usedPorts.Add("17500");
        //        usedPorts.Add("17100");
        //        usedPorts.Add("17101");
        //        usedPorts.Add("17102");
        //        usedPorts.Add("17103");
        //        usedPorts.Add("17104");
        //        usedPorts.Add("17105");
        //        usedPorts.Add("17106");
        //        usedPorts.Add("17107");
        //        usedPorts.Add("17108");
        //        usedPorts.Add("17109");
        //        usedPorts.Add("17110");

        //        m_PortInfo = EthernetPortInfo.getInstance();//(m_LocalIP, usedPorts);
        //    }
        //}

		#region Random Data Generation
		/// <summary>
		/// Generates Random String Array.
		/// </summary>
		/// <param name="count">Number of Strings</param>
		/// <param name="dataLength">Length of each string</param>
		/// <param name="minValue">Lower Bound</param>
		/// <param name="maxValue">Upper Bound</param>
		/// <param name="zeroPadding">True if zero padding will be done, else false</param>
		/// <returns>Array of Random Strings</returns>
        public static string[] GenerateRandom(int count, int dataLength, UInt64 minValue, UInt64 maxValue, bool zeroPadding)
        {
            if (m_log.IsDebugEnabled)
                m_log.Debug("Utilities::GenerateRandom() : count = " + count + ", dataLength = " + dataLength + ", minValue = " + minValue + ", maxValue = " + maxValue + ", zeroPadding = " + zeroPadding);

            if (minValue > maxValue)
            {
                if (m_log.IsErrorEnabled)
                    m_log.Error("Utilities::GenerateRandom() : minValue parameter should be less than or equal to maxValue parameter.");

                throw new ApplicationException("minValue parameter should be less than or equal to maxValue parameter.");
            }

            if (dataLength == 0)
            {
                if (m_log.IsErrorEnabled)
                    m_log.Error("Utilities::GenerateRandom() : DataLength parameter cannot be equal to zero.");

                throw new ApplicationException("DataLength parameter cannot be equal to zero.");
            }

            if (dataLength > maxValue.ToString().Length && !zeroPadding)
            {
                if (m_log.IsErrorEnabled)
                    m_log.Error("Utilities::GenerateRandom() : When zeroPadding parameter is false, dataLength parameter cannot be less than the length of maxValue parameter.");

                throw new ApplicationException("When zeroPadding parameter is false, dataLength parameter cannot be less than the length of maxValue parameter.");
            }
            if (count < 1)
            {
                if (m_log.IsErrorEnabled)
                    m_log.Error("Utilities::GenerateRandom() : count parameter should be greater than zero.");

                throw new ApplicationException("count parameter should be greater than zero.");
            }

            System.Random random;
            string[] randomNumbers = new string[count];
            List<string> randomNumbersList = new List<string>();
            StringBuilder format = new StringBuilder();
            format.Append('0', dataLength);

            random = new Random();

            int i = 0;
            while (i < count)
            {
                if (random != null)
                {
                    UInt64 rand = minValue + Convert.ToUInt64((maxValue - minValue + 1) * random.NextDouble());
                    string randStr = rand.ToString();
                    if (!zeroPadding)
                    {
                        if (randStr.Length < dataLength)
                            rand *= Convert.ToUInt64(Math.Pow(10, dataLength - randStr.Length));
                    }
                    randStr = rand.ToString();
                    if (randStr.Length > dataLength)
                    {
                        rand = Convert.ToUInt64(randStr.Substring(0, randStr.Length - (randStr.Length - dataLength)));
                        if (rand < minValue)
                            continue;
                    }
                    if (rand < minValue || rand > maxValue)
                        continue;
                    //randomNumbers[i++] =rand.ToString(format.ToString());
                    if (randomNumbersList.Contains(rand.ToString(format.ToString())))
                        continue;
                    randomNumbersList.Add(rand.ToString(format.ToString()));
                    i++;
                }
            }
            randomNumbersList.CopyTo(randomNumbers);
            i = 0;
            if (m_log.IsTraceEnabled)
                foreach (string rand in randomNumbers)
                    m_log.Trace("Utilities::GenerateRandom() : Random Number[" + (i++).ToString() + "] = " + rand);

            return randomNumbers;
        }

		/// <summary>
		/// Generates Random String Array.
		/// </summary>
		/// <param name="count">Number of Strings</param>
		/// <param name="dataLength">Length of each string</param>
		/// <param name="minValue">Lower Bound</param>
		/// <param name="maxValue">Upper Bound</param>
		/// <returns>Array of Random Strings with zeroPadding if required.</returns>
        public static string[] GenerateRandom(int count, int dataLength, UInt64 minValue, UInt64 maxValue)
        {
            return GenerateRandom(count, dataLength, minValue, maxValue, true);
        }
		/// <summary>
		/// Generates Random String Array.
		/// </summary>
		/// <param name="count">Number of Strings</param>
		/// <param name="minValue">Lower Bound</param>
		/// <param name="maxValue">Upper Bound</param>
		/// <returns>Array of Random Strings with zeroPadding if required.</returns>
        public static string[] GenerateRandom(int count, UInt64 minValue, UInt64 maxValue)
        {
            return GenerateRandom(count, minValue, maxValue, true);
        }
		/// <summary>
		/// Generates Random String Array.
		/// </summary>
		/// <param name="count">Number of Strings</param>
		/// <param name="minValue">Lower Bound</param>
		/// <param name="maxValue">Upper Bound</param>
		/// <param name="zeroPadding">True if zero padding will be done, else false</param>
		/// <returns>Array of Random Strings</returns>
        public static string[] GenerateRandom(int count, UInt64 minValue, UInt64 maxValue, bool zeroPadding)
        {
            return GenerateRandom(count, maxValue.ToString().Length, minValue, maxValue, zeroPadding);
        }
		/// <summary>
		/// Generates Random String Array.
		/// </summary>
		/// <param name="count">Number of Strings</param>
		/// <param name="dataLength">Length of each string</param>
		/// <param name="zeroPadding">True if zero padding will be done, else false</param>
		/// <returns>Array of Random Strings</returns>
        public static string[] GenerateRandom(int count, int dataLength, bool zeroPadding)
        {
            ulong maxVal = Convert.ToUInt64(Math.Pow(10, Convert.ToDouble(dataLength))) - 1;
            return GenerateRandom(count, dataLength, 0, maxVal, zeroPadding);
        }
		/// <summary>
		/// Generates Random String Array.
		/// </summary>
		/// <param name="count">Number of Strings</param>
		/// <param name="dataLength">Length of each string</param>
		/// <returns>Array of Random Strings with zeroPadding if required.</returns>
        public static string[] GenerateRandom(int count, int dataLength)
        {
            return GenerateRandom(count, dataLength, true);
        }
		/// <summary>Generates Random Byte Array.</summary>
		/// <param name="size">Size of the Array</param>
		/// <param name="seed">Initial Seed</param>
		/// <param name="useSeed">True if seed will be used, else false</param>
		/// <returns></returns>
        //public static byte[]GenerateRandomByteArray(int size, int seed)
        //{
        //    if ( size < 1 ) 
        //    {
        //        if(m_log.IsErrorEnabled)
        //            m_log.Error("Utilities::GenerateRandom() : count parameter should be greater than zero.");

        //        throw new InvalidParameterException("size parameter should be greater than zero") ;
        //    }
			
        //    System.Random rnd;
        //    rnd  = new System.Random(seed); 
        //    FrmMain.m_seed++;
        //    byte[] arr = new byte[size] ; 
        //    rnd.NextBytes(arr) ;

        //    return arr;
        //}
		
		/// <summary>
		/// Generates unique String Array.
		/// </summary>
		/// <param name="count">Number of Strings</param>
		/// <param name="dataLength">Length of each string</param>
		/// <param name="minValue">Lower Bound</param>
		/// <param name="maxValue">Upper Bound</param>
		/// <param name="zeroPadding">True if zero padding will be done, else false</param>
		/// <param name="checkDataLength">True if data length is to be checked, else false</param>
        /// <param name="checkCount">True if difference between min - max is to be checked </param>
		/// <returns>Array of Unique Strings</returns>
        public static string[] GenerateSerialNos(int count, int dataLength, UInt64 minValue, UInt64 maxValue, bool zeroPadding,
            bool checkDataLength, bool checkCount)
        {
            if (m_log.IsDebugEnabled)
                m_log.Debug("Utilities::GenerateSerialNos() : count = " + count + ", dataLength = " + dataLength + ", minValue = " + minValue
                    + ", maxValue = " + maxValue + ", zeroPadding = " + zeroPadding + ",checkDataLength = " + checkDataLength);

            if (minValue > maxValue)
            {
                if (m_log.IsErrorEnabled)
                    m_log.Error("Utilities::GenerateSerialNos() : minValue parameter should be less than or equal to maxValue parameter.");

                throw new ApplicationException("minValue parameter should be less than or equal to maxValue parameter.");
            }

            if (checkCount && ((maxValue - minValue + 1) < (ulong)count))
            {
                if (m_log.IsErrorEnabled)
                    m_log.Error("Utilities::GenerateSerialNos() : count parameter should be less than or equal to "
                        + "difference between maxValue and minValue parameter.");

                throw new ApplicationException("Count should be less than or equal to "
                        + "difference between maximum and minimum value.");
            }
            if (checkDataLength)
            {
                if (dataLength > maxValue.ToString().Length && !zeroPadding)
                {
                    if (m_log.IsErrorEnabled)
                        m_log.Error("Utilities::GenerateSerialNos() : When zeroPadding parameter is false,"
                            + " dataLength parameter cannot be less than the length of maxValue parameter.");

                    throw new ApplicationException("When zeroPadding parameter is false, dataLength parameter cannot be less than the length of maxValue parameter.");
                }
            }
            if (count < 1)
            {
                if (m_log.IsErrorEnabled)
                    m_log.Error("Utilities::GenerateSerialNos() : count parameter should be greater than zero.");

                throw new ApplicationException("count parameter should be greater than zero.");
            }

            string[] randomNumbers = new string[count];

            StringBuilder format = new StringBuilder();
            format.Append('0', dataLength);

            UInt64 val = minValue;
            int i = 0;
            while (i < count)
            {
                UInt64 rand = val;
                if (checkCount)
                    rand = val++;
                string randStr = rand.ToString();
                if (checkDataLength)
                {
                    if (!zeroPadding)
                    {
                        if (randStr.Length < dataLength)
                            rand *= Convert.ToUInt64(Math.Pow(10, dataLength - randStr.Length));
                    }
                }

                randStr = rand.ToString();
                if (checkDataLength)
                {
                    if (randStr.Length > dataLength)
                    {
                        rand = Convert.ToUInt64(randStr.Substring(0, randStr.Length - (randStr.Length - dataLength)));
                        if (rand < minValue)
                            continue;
                    }
                }
                if (rand < minValue || rand > maxValue)
                    continue;
                if (checkDataLength)
                    randomNumbers[i++] = rand.ToString(format.ToString());
                else
                    randomNumbers[i++] = rand.ToString();
            }

            i = 0;
            if (m_log.IsTraceEnabled)
                foreach (string rand in randomNumbers)
                    m_log.Trace("Utilities::GenerateSerialNos() : Random Number[" + (i++).ToString() + "] = " + rand);

            return randomNumbers;
        }

		/// <summary>
		/// Generates unique String Array.
		/// </summary>
		/// <param name="count">Number of Strings</param>
		/// <param name="dataLength">Length of each string</param>
		/// <param name="minValue">Lower Bound</param>
		/// <param name="maxValue">Upper Bound</param>
		/// <param name="zeroPadding">True if zero padding will be done, else false</param>
		/// <returns>Array of Unique Strings</returns>
        public static string[] GenerateSerialNos(int count, int dataLength, UInt64 minValue, UInt64 maxValue,
            bool zeroPadding)
        {
            return GenerateSerialNos(count, dataLength, minValue, maxValue, zeroPadding, true, true);
        }

        /// <summary>
        /// Generates unique String Array.
        /// </summary>
        /// <param name="count">Number of Strings</param>
        /// <param name="dataLength">Length of each string</param>
        /// <param name="minValue">Lower Bound</param>
        /// <param name="maxValue">Upper Bound</param>
        /// <param name="zeroPadding">True if zero padding will be done, else false</param>
        /// <returns>Array of Unique Strings</returns>
        public static string[] GenerateSerialNosWithoutCountCheck(int count, int dataLength, UInt64 minValue, UInt64 maxValue,
            bool zeroPadding)
        {
            return GenerateSerialNos(count, dataLength, minValue, maxValue, zeroPadding, true, false);
        }

		/// <summary>
		/// Generates unique String Array.
		/// </summary>
		/// <param name="count">Number of Strings</param>
		/// <param name="minValue">Lower Bound</param>
		/// <param name="maxValue">Upper Bound</param>
		/// <param name="zeroPadding">True if zero padding will be done, else false</param>
		/// <param name="checkDataLength">True if data length is to be checked, else false</param>
		/// <returns>Array of Unique Strings</returns>
        public static string[] GenerateSerialNos(int count, UInt64 minValue, UInt64 maxValue, bool zeroPadding,
            bool checkDataLength)
        {
            return GenerateSerialNos(count, maxValue.ToString().Length, minValue, maxValue, zeroPadding, checkDataLength, true);
        }

		#endregion
		
		#region Conversion Methods
		/// <summary>Converts Byte Array to a continuous Hex String.</summary>
		/// <param name="input">Input Byte Array.</param>
		/// <returns>Hex String.</returns>
        public static string ConvertByteArrayToHexString(byte[] input)
        {
            return ConvertByteArrayToHexString(input, false);
        }
		/// <summary>
		/// Converts Byte Array To Hex String.
		/// If the separator is true, then there is a space between each hex value.
		/// Else it is continuous.
		/// </summary>
		/// <param name="input">Input Byte Array.</param>
		/// <param name="separator">space as separator if true, else no separator.</param>
		/// <returns>Hex String.</returns>
        public static string ConvertByteArrayToHexString(byte[] input, bool separator)
        {
            //if separator is false, the hex string is like 0xFFFF
            //if separator is true, the hex string is like 0xFF 0xFF
            if (input != null)
            {
                StringBuilder strBldSlNo = new StringBuilder();
                if (!separator)
                    strBldSlNo.Append("0x");
                foreach (byte bt in input)
                {
                    string btStr = bt.ToString("X");
                    if (separator)
                    {
                        strBldSlNo.Append(" ");
                        strBldSlNo.Append("0x");
                    }
                    else
                    {
                        if (btStr.Length == 1)
                            strBldSlNo.Append("0");
                    }
                    strBldSlNo.Append(btStr);
                }
                return strBldSlNo.ToString().Trim();
            }
            else
            {
                if (m_log.IsErrorEnabled)
                    m_log.Error("Utilities::ConvertByteArrayToHexString() : Input byte[] is null");
                //throw new InvalidValueException("Input byte[] is null");
                throw new ApplicationException("Input byte[] is null");
            }
        }
		/// <summary>Converts a String array to a long array.</summary>
		/// <param name="strArr">String Array.</param>
		/// <returns>Long Array.</returns>
        public static long[] ConvertStringArrayToLongArray(string[] strArr)
        {
            try
            {
                long[] lngArr = new long[strArr.Length];
                int i = 0;
                foreach (string str in strArr)
                {
                    lngArr[i++] = Convert.ToInt64(str);
                }
                return lngArr;
            }
            catch (FormatException ex)
            {
                if (m_log.IsErrorEnabled)
                    m_log.Error("Utilities::ConvertStringArrayToLongArray() : Input string[] contains char(s) which cannot be converted to long", ex);

                throw new ApplicationException("Input string[] contains char which can't be converted to long");
            }
            catch (Exception ex)
            {
                if (m_log.IsErrorEnabled)
                    m_log.Error("Utilities::ConvertStringArrayToLongArray() : Error converting string array to long array", ex);
                throw;
            }
        }
      

		#endregion 

		#region Validate Methods
        //public static bool IsNumericCtlValid(NumericUpDown ctrl, out string strMsg)
        //{
        //    strMsg = string.Empty ;
        //    try
        //    {
        //        if ( ctrl == null )
        //        {  
        //            strMsg = @"Instance of NumericUpDown control is invalid." ;
        //            return false;
        //        }

        //        int val = Convert.ToInt32(ctrl.Text);
        //        if ( ctrl.Minimum <= val && val <= ctrl.Maximum )
        //            return true;
        //        else
        //        {
        //            strMsg = @"Given value is out of range. Please provide value within range. " 
        //                        + Environment.NewLine + @"Minimum value = " + ctrl.Minimum.ToString() 
        //                        + Environment.NewLine + @"Maximum value = " + ctrl.Maximum.ToString() ;
        //            return false;
        //        }
        //    }
        //    catch(Exception ex)
        //    {                      
        //        strMsg = @"Invalid Numeric value found. " + Environment.NewLine + ex.Message ;
        //        return false ;
        //    }
        //}

        //public static bool IsIPAddress(string IPAddress)
        //{
        //    string[] ipc = IPAddress.Split('.');

        //    //Valid ip address has four integer part delimited by '.'
        //    if (ipc.Length != 4)
        //        return false;

        //    //Check the individula part delimited by '.'
        //    for(int i = 0; i < ipc.Length; i++)
        //    {
        //        try
        //        {
        //            int val = Convert.ToInt16(ipc[i].ToString());
        //            //Every integer part must be betwwen 0 to 255
        //            if(val < 0 || val > 255)
        //                return false;
        //        }
        //        catch
        //        {
        //            //If a part is not an integer
        //            return false;
        //        }
        //    }

        //    return true;
        //}

        //public static bool IsPortValid(string readerId, int port, out string strMsg)
        //{			
        //    strMsg = string.Empty;
			
        //    if (readerId == null)
        //    {								
        //        bool isValid = m_PortInfo.ValidatePortInfo("", m_LocalIP, port, out strMsg);
        //        if(!isValid)
        //        {	
        //            strMsg = string.Format("The port {0} is already in use. Please try some other port.", port);
        //            return false;
        //        }			
        //    }	
        //    else
        //    {	
        //        ArrayList aList = m_PortInfo.GetPortList(m_LocalIP, readerId);				
        //        if (aList.BinarySearch(port.ToString()) < 0)
        //        {
        //            bool isValid = m_PortInfo.ValidatePortInfo("", m_LocalIP, port, out strMsg);
        //            if(!isValid)
        //            {						
        //                return false;
        //            }
        //        }
        //    }
        //    return true;
        //}
		/// <summary>
		/// check whether the company prefix is in the index table.
		/// </summary>
		/// <param name="compPref">Company Prefix to be checked.</param>
		/// <returns>
		/// True if Company Prefix exist or if not then insertion successful,
		/// else return false.
		/// </returns>
        public static bool CheckValidCompanyPrefix(string compPref)
        {
            try
            {
                bool chk = true;
                //                if(!Core.EPCTagEncoding.EPCEncoding.ValidateCompPrefix(compPref))
                //                {
                //                    if(m_log.IsWarnEnabled)
                //                        m_log.Warn("Utilities::CheckValidCompanyPrefix() : Company Prefix: "+compPref+" is not in the EPC Company Prefix List");
                //                    //Portion to add company prefix in List commented
                //                    //-------------------------------------------------------
                ///*					if(MessageBox.Show("The Company Prefix "+ compPref + " is not in EPC Standard Company Prefix List.\nDo you still want to created Tag(s) with this Company Prefix","Simulator",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                //                    {
                //                        Core.EPCTagEncoding.EPCEncoding.Insert(compPref);
                //                        if(m_log.IsDebugEnabled)
                //                            m_log.Debug("Utilities::CheckValidCompanyPrefix() : Company Prefix: "+compPref+" is successfully inserted in the EPC Company Prefix List");
                //                        chk = true;
                //                    }*/
                //                    //--------------------------------------------------------
                //                    MessageBox.Show("The Company Prefix " + compPref + " is not in the EPC Standard Company Prefix List.","Simulator",MessageBoxButtons.OK,MessageBoxIcon.Error);
                //                    chk = false;
                //                }
                //                else
                //                {
                //                    if(m_log.IsDebugEnabled)
                //                        m_log.Debug("Utilities::CheckValidCompanyPrefix() : Company Prefix: "+compPref+" is in the EPC Company Prefix List");
                //                    chk = true;
                //                }
                return chk;
            }
            catch (Exception ex)
            {
                if (m_log.IsErrorEnabled)
                    m_log.Error("Utilities::CheckValidCompanyPrefix() : Unable to Check Validity of company Prefix or Company Prefix insertion failed", ex);
                throw ex;
            }
        }

		#endregion

		#region Protected Methods
        //public static void OnTagListChanged(EventArgs e)
        //{
        //    if(TagListChanged != null)
        //        TagListChanged(e);
        //}
        //public static void OnReaderReconfigured(EventArgs e)
        //{
        //    if(ReaderReconfigured != null)
        //        ReaderReconfigured(e);
        //}

        //public static void OnReaderStatusChanged(EventArgs e)
        //{
        //    if(ReaderStatusChanged != null)
        //        ReaderStatusChanged(e);
        //}
		#endregion
	}



	/// <summary>Provides data for the All Configure EPC event.</summary>
    //public class ConfigTagEventArgs:System.EventArgs
    //{
    //    private Hashtable configHash;

    //    public ConfigTagEventArgs(Hashtable hash):base()
    //    {
    //        this.configHash = hash;
    //    }

    //    /// <summary>Gets the Configuration details.</summary>
    //    public Hashtable ConfigureHash
    //    {
    //        get{return configHash;}
    //    }
    //}
}
