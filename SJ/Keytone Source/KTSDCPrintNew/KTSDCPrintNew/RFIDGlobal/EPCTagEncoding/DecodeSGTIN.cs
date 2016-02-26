
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
using NLog;

namespace KTone.RFIDGlobal.EPCTagEncoding
{
	/// <summary>
	/// Summary description for DecodeSGTIN.
	/// </summary>
    public class DecodeSGTIN
	{
        private static Logger log = LogManager.GetCurrentClassLogger();

		#region Private Methods
        public static void BreakGTIN(string strGTIN, int partitionValue, out string companyPrefix, out string itemRef)
		{
			companyPrefix = string.Empty ;
			itemRef = string.Empty ;
			
			try
			{
				PartitionTable.FillPartitionTables(EPCFORMAT.SGTIN) ;
				int companyPrefixDigitLen = PartitionTable.companyPrefixDigitLength[partitionValue] ;
				int itemRefDigitLen  = PartitionTable.itemRefDigitLength[partitionValue] ;

				companyPrefix = strGTIN.Substring(1,companyPrefixDigitLen) ;
				string tempIndicatorDigit = strGTIN.Substring(0,1) ;
				string tempItemRef = strGTIN.Substring(companyPrefixDigitLen+1,itemRefDigitLen-1) ;
				itemRef = tempIndicatorDigit+tempItemRef ;
			}
			catch(Exception e)
			{
				throw e ;
			}
		}
		#endregion 

		#region Public Methods

		private DecodeSGTIN()
		{}


		#region Method to Decode SGTIN64 encoded string
		/// <summary>
		/// Method to Decode 64 bit SGTIN
		/// </summary>
		/// <param name="encodedSGTIN64">Encoded SGTIN64 8 byte byte array</param>
		/// <param name="GTIN14">Output GTIN14 string</param>
		/// <param name="serialNumber">Output serial number</param>
		/// <param name="filterValue">Output filter value</param>
        public static bool DecodeSGTIN64(byte[] encodedSGTIN64, out string GTIN14, out string companyPrefix, out string itemRef, out string serialNumber, out byte filterValue,
            bool throwException, out string errorMessage)
		{
            errorMessage = string.Empty;
            GTIN14 = String.Empty;
			serialNumber = String.Empty;
			companyPrefix = string.Empty ;
			itemRef	 = string.Empty ;

			string indicatorDigit = string.Empty;

			filterValue = 0;
			if(encodedSGTIN64.Length != 8)
			{
                errorMessage = "This is not a valid SGTIN64 encoded string.";
                if(throwException)
				    throw new InvalidSGTIN64EncodingException(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
			}

			StringBuilder strbSGTIN = new StringBuilder();
			
			//Extract the Byte array into a string
			for(int byteArrayIndex = 0; byteArrayIndex < encodedSGTIN64.Length; byteArrayIndex ++)
			{
				strbSGTIN.Append(RFUtils.AddReqdZeros(Convert.ToString(encodedSGTIN64[byteArrayIndex],2).Trim(),8));
			}

			//Check for header value validity header value is 10 in decimal
			if(!strbSGTIN.ToString().Substring(0,2).Equals("10"))
			{
                errorMessage = "Invalid header value for SGTIN 64";
                if(throwException)
				    throw new InvalidHeaderValue(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
			}

			//The first 2 digits of the byte array is the Header value, extract values from 3rd index for filter value
			filterValue = Convert.ToByte(RFUtils.ConvertBinaryToDecimal(strbSGTIN.ToString().Substring(2,3)));

			//Assign the company prefix 
			System.UInt16 companyPrefixIndex  = Convert.ToUInt16(RFUtils.ConvertBinaryToDecimal(strbSGTIN.ToString().Substring(5,14)));

			companyPrefix = RFUtils.FetchCompanyPrefix(companyPrefixIndex) ;

			//Validate the company prefix against the Company Prefix Translation table
			//Get the Company Prefix length

			//Currently it is assigned a value manually, but in actual case it will come from Company Prefix Translation table
			int companyPrefixLength = companyPrefix.Length;

			//Xtract  the GTIN value
			itemRef = RFUtils.ConvertBinaryToDecimal(strbSGTIN.ToString().Substring(19,20));

			//Check the validity of tmpGTIN
			if(Convert.ToUInt64(itemRef) > Math.Pow(10, (13 - companyPrefixLength)))
			{
				//This is not a valid GTIN64
                errorMessage = "This is not a valid SGTIN64 encoding.";
                if(throwException)
				    throw new InvalidSGTIN64EncodingException(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
			}
			//Valid GTIN64
			if((itemRef.Length+companyPrefixLength) > 13 )
			{
                errorMessage = "DecodeSGTIN64() : Length of GTIN number is greater than 13.";
                if(throwException)
				    throw new EPCTagExceptionBase (errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
			}
			//checking if Item Reference Length is less than the required length
			//like in the case if Actual Item Ref = 0543123,
			//then the function ConvertBinaryToDecimal() is going to return 543123.
			//so we require to add the 0's that are missing.
			if(itemRef.Length < (13 - companyPrefixLength))
			{

				itemRef = RFUtils.AddReqdZeros(itemRef, (13 - (companyPrefixLength))); //deducing from 12 coz no chk digit,no indicator digit.
			}

			indicatorDigit =  itemRef.Substring(0,1) ;

			string tempItemRef = itemRef.Substring(1) ;


			//Decoding for the check digit starts
			StringBuilder tmpGTINString = new StringBuilder();
			tmpGTINString.Append(indicatorDigit);//Get the 1st digit for chk digit string
			tmpGTINString.Append(companyPrefix);//Company prefix
			tmpGTINString.Append(tempItemRef) ;//, (companyPrefixLength)));

			//Calculate the checkdigit

			string calculatedCheckDigit = RFUtils.CalculateCheckDigit(tmpGTINString.ToString(),Constants.SGTIN14_LEN-1) ;

			GTIN14 = String.Concat(tmpGTINString.ToString(), calculatedCheckDigit);

			//Get the Serial number
			serialNumber = Convert.ToString(Convert.ToInt32(strbSGTIN.ToString().Substring(39),2));
            return true;
		}


        public static bool DecodeSGTIN64(byte[] encodedSGTIN64, out string companyPrefix, out string itemRef, out string serialNumber, out byte filterValue,
            bool throwException, out string errorMessage)
		{
            errorMessage = string.Empty;
            //indicatorDigit = string.Empty ;
			companyPrefix	= string.Empty ;
			itemRef			= string.Empty ;
			serialNumber	= string.Empty ;
			filterValue		= 0 ; 
			string strGTIN	= string.Empty ;
            if (!DecodeSGTIN64(encodedSGTIN64, out strGTIN, out companyPrefix, out itemRef, out serialNumber, out filterValue, throwException, out errorMessage))
                return false;
            return true;
		}


		/// <summary>
		///  Method to Decode encoded SGTIN64 string and send the hex string
		/// </summary>
		/// <param name="encodedSGTIN96">Encoded SGTIN64 8 byte byte array</param>
		/// <returns>Decoded hex string</returns>
        public static string DecodeSGTIN64(byte[] encodedSGTIN64)
		{
			
			StringBuilder strbSGTIN = new StringBuilder();
			//Extract the Byte array into a string
			for(int byteArrayIndex = 0; byteArrayIndex < encodedSGTIN64.Length; byteArrayIndex ++)
			{
				strbSGTIN.Append(RFUtils.AddReqdZeros(Convert.ToString(encodedSGTIN64[byteArrayIndex],2),8));
			}
			
			StringBuilder strbConverted = new StringBuilder();
	
			for(int startValue = 0; startValue < strbSGTIN.ToString().Length;  startValue+=4)
			{
				strbConverted.Append(Convert.ToString(Convert.ToByte(RFUtils.ConvertBinaryToDecimal(strbSGTIN.ToString().Substring(startValue,4))),16));
			}
			return strbConverted.ToString().ToUpper();
		}
		#endregion

		#region Method to Decode SGTIN96 encoded string
		/// <summary>
		/// Method to Decode encoded SGTIN96 string
		/// </summary>
		/// <param name="encodedSGTIN96">Encoded SGTIN96 12 byte byte array</param>
		/// <param name="GTIN14">Output GTIN14 string</param>
		/// <param name="serialNumber">Output serial number</param>
		/// <param name="filterValue">Output filter value</param>
        public static bool DecodeSGTIN96(byte[] encodedSGTIN96, out string GTIN14, out string serialNumber, out byte filterValue, out int partVal,
            bool throwException, out string errorMessage)
		{
            errorMessage = string.Empty;
            GTIN14 = string.Empty;
            serialNumber = string.Empty;
            filterValue = 0;
            partVal = 0;

			if(encodedSGTIN96.Length != 12)
			{
                errorMessage = "This is not a valid SGTIN96 encoded string.";
                if (throwException)
				    throw new InvalidSGTIN96EncodingException(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
			}
			//Check for header value validity header value is 00110000=48 in decimal
			if(encodedSGTIN96[0] != 48)
			{
                errorMessage = "Invalid header value for SGTIN96";
                if (throwException)
				    throw new InvalidHeaderValue(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
			}

			StringBuilder strbSGTIN = new StringBuilder();
			
			//Extract the Byte array into a string
			for(int byteArrayIndex = 0; byteArrayIndex < encodedSGTIN96.Length; byteArrayIndex ++)
			{
				strbSGTIN.Append(RFUtils.AddReqdZeros(Convert.ToString(encodedSGTIN96[byteArrayIndex],2).Trim(),8));
			}
			
			//Xtract the filter value
			//For SGTIN96 the first 8 bits are header values, next 3 bits are filter values
			filterValue = Convert.ToByte(RFUtils.ConvertBinaryToDecimal(strbSGTIN.ToString().Substring(8,3)));

			//Xtract the prtition value
			//The next 3 bits are for partiotion value
			byte partitionValue = Convert.ToByte(RFUtils.ConvertBinaryToDecimal(strbSGTIN.ToString().Substring(11,3)));
			partVal = Convert.ToInt32(partitionValue) ;
			//Check for partition value validity
			if(partitionValue== 7)
			{
                errorMessage = "This is not a valid SGTIN96 encoding.";
                if (throwException)
				    throw new InvalidSGTIN96EncodingException(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
			}

			//Xtract the number of digits and number of bits for the company prefix
			int companyPrefixBitLength = 0;
			int companyPrefixDigitLength = 0;
			int itemReferenceBitLength = 0;
			//int itemReferenceDigitLength = 0;variable not being used anywhere

			switch(partitionValue)
			{
				case 0:
					companyPrefixBitLength = 40;
					companyPrefixDigitLength = 12;
					itemReferenceBitLength = 4;
					//itemReferenceDigitLength = 1;
					break;
				case 1:
					companyPrefixBitLength = 37;
					companyPrefixDigitLength = 11;
					itemReferenceBitLength = 7;
					//itemReferenceDigitLength = 2;
					break;
				case 2:
					companyPrefixBitLength = 34;
					companyPrefixDigitLength = 10;
					itemReferenceBitLength = 10;
					//itemReferenceDigitLength = 3;
					break;
				case 3:
					companyPrefixBitLength = 30;
					companyPrefixDigitLength = 9;
					itemReferenceBitLength = 14;
					//itemReferenceDigitLength = 4;
					break;
				case 4:
					companyPrefixBitLength = 27;
					companyPrefixDigitLength = 8;
					itemReferenceBitLength = 17;
					//itemReferenceDigitLength = 5;
					break;
				case 5:
					companyPrefixBitLength = 24;
					companyPrefixDigitLength = 7;
					itemReferenceBitLength = 20;
					//itemReferenceDigitLength = 6;
					break;
				case 6:
					companyPrefixBitLength = 20;
					companyPrefixDigitLength = 6;
					itemReferenceBitLength = 24;
					//itemReferenceDigitLength = 7;
					break;
				default:
                    errorMessage = "Invalid SGTIN96 partition value.";
                    if (throwException)
					    throw new InvalidSGTIN96EncodingException(errorMessage);
                    log.Trace("Decode Error:", errorMessage);
                    return false;
			}

			//Generate the company prefix
			string tmpCompanyPrefix = strbSGTIN.ToString().Substring(14,  companyPrefixBitLength);

			//Check for company prefix validity
			if(Convert.ToInt64(RFUtils.ConvertBinaryToDecimal(tmpCompanyPrefix)) >= Math.Pow(10, companyPrefixDigitLength))
			{
                errorMessage = "This is not a valid SGTIN96.";
                if (throwException)
				    throw new InvalidSGTIN96EncodingException(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
			}
			else
			{
				//Add zeros if reqd
				tmpCompanyPrefix = RFUtils.AddReqdZeros(RFUtils.ConvertBinaryToDecimal(tmpCompanyPrefix), companyPrefixDigitLength);
			}

			//Extract the Item refernce number
			string itemReference = strbSGTIN.ToString().Substring((14+companyPrefixBitLength), itemReferenceBitLength);

			//Check the validity of this value
			if(Convert.ToInt64(RFUtils.ConvertBinaryToDecimal(itemReference)) >= Math.Pow(10, (13 - companyPrefixDigitLength)))
			{
                errorMessage = "This is not a valid SGTIN96.";
                if (throwException)
				    throw new InvalidSGTIN96EncodingException(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
			}
			else
			{
				//Construct the valid Item reference
				itemReference = RFUtils.AddReqdZeros(RFUtils.ConvertBinaryToDecimal(itemReference), 13 -companyPrefixDigitLength);
			}

			//Decoding for the check digit starts
			StringBuilder chkDigitString = new StringBuilder();
			chkDigitString.Append(itemReference.Substring(0,1));//Get the 1st digit for chk digit string
			chkDigitString.Append(tmpCompanyPrefix.Substring(0, companyPrefixDigitLength));//Company prefix
			string tempItemRef = itemReference.Substring(1) ;
			chkDigitString.Append(tempItemRef);//deducing 1 from itemref for indicator digit

			//Calculate the checkdigit

			System.Int32 oddDigitTotal = 0;
			//first extract the odd digits from chkDigitString
			for(int oddIndex= 0; oddIndex <= 13; oddIndex+=2)
			{
				oddDigitTotal += Convert.ToInt32(chkDigitString.ToString().Substring(oddIndex,1));
			}

			//Xtract the even digits from chkDigitString
			System.Int32 evenDigitTotal = 0;
			for(int evenIndex=1; evenIndex < 13; evenIndex+=2)
			{
				evenDigitTotal += Convert.ToInt32(chkDigitString.ToString().Substring(evenIndex,1));
			}

			string calculatedCheckDigit = Convert.ToString((1000 - (3 * (oddDigitTotal) + evenDigitTotal)) % 10).Trim();
			GTIN14 = String.Concat( chkDigitString.ToString(), calculatedCheckDigit);

			//Get the Serial number
			serialNumber = RFUtils.ConvertBinaryToDecimal(strbSGTIN.ToString().Substring(58));
            return true;
        }

		
		/// <summary>
		///  Method to Decode encoded SGTIN96 string and send the hex string
		/// </summary>
		/// <param name="encodedSGTIN96">Encoded SGTIN96 12 byte byte array</param>
		/// <returns>Decoded hex string</returns>
        public static string DecodeSGTIN96(byte[] encodedSGTIN96)
		{
			
			StringBuilder strbSGTIN = new StringBuilder();
			//Extract the Byte array into a string
			for(int byteArrayIndex = 0; byteArrayIndex < encodedSGTIN96.Length; byteArrayIndex ++)
			{
				strbSGTIN.Append(RFUtils.AddReqdZeros(Convert.ToString(encodedSGTIN96[byteArrayIndex],2),8));
			}
			
			StringBuilder strbConverted = new StringBuilder();
	
			for(int startValue = 0; startValue < strbSGTIN.ToString().Length;  startValue+=4)
			{
				strbConverted.Append(Convert.ToString(Convert.ToByte(RFUtils.ConvertBinaryToDecimal(strbSGTIN.ToString().Substring(startValue,4))),16));
			}
			return strbConverted.ToString().ToUpper();
		}

        public static bool DecodeSGTIN96(byte[] encodedSGTIN96, out string companyPrefix, out string itemRef, out string serialNumber, out byte filterValue,
            bool throwException, out string errorMessage)
		{
			try
			{
                errorMessage = string.Empty;
                filterValue = 0;
				serialNumber = string.Empty ;
				itemRef = string.Empty ;
				companyPrefix = string.Empty ;
				string GTIN14 = string.Empty ;
				int partValue = 0 ;

                if (!DecodeSGTIN96(encodedSGTIN96, out GTIN14, out serialNumber, out filterValue, out partValue, throwException, out errorMessage))
                    return false;

				BreakGTIN(GTIN14,partValue,out companyPrefix,out itemRef) ;

			}
			catch(Exception)
			{
				throw;
			}
            return true;
		}
		#endregion

		#endregion Public Methods ENDS
	}
}
