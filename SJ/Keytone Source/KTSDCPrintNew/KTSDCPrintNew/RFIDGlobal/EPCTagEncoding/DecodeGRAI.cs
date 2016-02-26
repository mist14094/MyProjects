
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
	/// Summary description for DecodeGRAI.
	/// </summary>
    public class DecodeGRAI
	{
        private static Logger log = LogManager.GetCurrentClassLogger();

		#region Private Methods
        public static void BreakGRAI(string strGRAI, int partitionValue, out string companyPrefix, out string assetType)
		{
			companyPrefix = string.Empty ;
			assetType	 = string.Empty ;
			try
			{
				PartitionTable.FillPartitionTables(EPCFORMAT.GRAI) ;
				int companyPrefixDigitLen =  PartitionTable.companyPrefixDigitLength[partitionValue] ;
				int assetTypeDigitLength = PartitionTable.assetTypeDigitLength[partitionValue] ;

				companyPrefix = strGRAI.Substring(1,companyPrefixDigitLen) ;
				assetType	 = strGRAI.Substring(companyPrefixDigitLen+1,assetTypeDigitLength) ;
			}
			catch(Exception e)
			{
				throw e ;
			}
		}
		#endregion Private Methods ENDS

	
		#region Public Methods

		private DecodeGRAI()
		{}

		#region Method to De code GRAI 64 to GRAI
		/// <summary>
		/// Method to De Encode GRAI 64
		/// </summary>
		/// <param name="encodedGRAI64">Encoded GRAI 64 8 byte byte array</param>
		/// <param name="GRAI">Output GRAI string</param>
		/// <param name="filterValue">Output filter value</param>
        public static bool DecodeGRAI64(byte[] encodedGRAI64, out string GRAI, out string companyPrefix, out string assetType, out string serialNo, out byte filterValue,
            bool throwException, out string errorMessage)
		{
			companyPrefix = String.Empty;
			assetType	 = string.Empty ;
			filterValue	 = 0 ;
			serialNo	 = string.Empty ;
            GRAI = string.Empty;
            errorMessage = string.Empty; 

			//Check for GRAI encoded array validation
			if(encodedGRAI64.Length != 8)
			{
                errorMessage = "Invalid GRAI64 string.";
                if (throwException)
                    throw new InvalidGRAI64EncodingException(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
			}
			//Check for header value validity header value is 00001010=10 in decimal
			if(encodedGRAI64[0] != 10)
			{
                errorMessage = "Invalid header value for GRAI 64";
                if (throwException)
                    throw new InvalidHeaderValue(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
			}

			//Xtract the contents of the byte array to a string
			StringBuilder strbEncodedString = new StringBuilder();

			for(int byteArrayIndex = 0; byteArrayIndex < encodedGRAI64.Length; byteArrayIndex++)
			{
				strbEncodedString.Append(RFUtils.AddReqdZeros(Convert.ToString(encodedGRAI64[byteArrayIndex],2).Trim(),8));
			}

			//Assign the filter value
			filterValue =Convert.ToByte(RFUtils.ConvertBinaryToDecimal(strbEncodedString.ToString().Substring(8,3)));

			//Xtract the 14 bit company prefix index
			string companyPrefixIndex = RFUtils.ConvertBinaryToDecimal(strbEncodedString.ToString().Substring(11,14));

			companyPrefix = RFUtils.FetchCompanyPrefix(Convert.ToUInt16(companyPrefixIndex)) ;
			//Validate this company reference with Copnay Prefix translation table
			int companyPrefixLength = companyPrefix.Length ;//Assign the company prefix length here


			//Xtract the string for asset type
			//string assetType = RFUtils.AddReqdZeros(RFUtils.ConvertBinaryToDecimal(strbEncodedString.ToString().Substring(14 + companyPrefixLength, 20)), 12 - companyPrefixLength);
			assetType = RFUtils.ConvertBinaryToDecimal(strbEncodedString.ToString().Substring(25,20));

			if(Convert.ToInt32(assetType) >= Math.Pow(10, 12 - companyPrefixLength))
			{
                errorMessage = "DecodeGRAI64() assetType value exceeds limits : assetType Value :" + assetType;
                if (throwException)
                    throw new InvalidGRAI64EncodingException(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
			}

			if(assetType.Length < (12-companyPrefixLength))
			{
				assetType = RFUtils.AddReqdZeros(assetType,(12-companyPrefixLength));
			}

			//Construct the chk digit
			StringBuilder strbChkDigitString = new StringBuilder();
			strbChkDigitString.Append("0");
			strbChkDigitString.Append(companyPrefix);
			strbChkDigitString.Append(assetType);

			//Calculate the checkdigit
			string calculatedCheckDigit = RFUtils.CalculateCheckDigit(strbChkDigitString.ToString(),Constants.GRAI_LEN-1) ;

			//Extract the serial no
			//Xtract the 19 digit serial no
			string tempSerialNo = strbEncodedString.ToString().Substring(45) ;
			serialNo = Convert.ToString(Convert.ToInt32(tempSerialNo,2)) ;

			GRAI = companyPrefix + assetType + calculatedCheckDigit.ToString()+serialNo;
            return true;

		}
		
		/// <summary>
		///  Method to Decode encoded GRAI64 string and send the hex string
		/// </summary>
		/// <param name="encodedGRAI64">Encoded GRAI64 8 byte byte array</param>
		/// <returns>Decoded hex string</returns>
        public static string DecodeGRAI64(byte[] encodedGRAI64)
		{
			
			StringBuilder strbGRAI = new StringBuilder();
			//Extract the Byte array into a string
			for(int byteArrayIndex = 0; byteArrayIndex < encodedGRAI64.Length; byteArrayIndex ++)
			{
				strbGRAI.Append(RFUtils.AddReqdZeros(Convert.ToString(encodedGRAI64[byteArrayIndex],2),8));
			}
			
			StringBuilder strbConverted = new StringBuilder();
	
			for(int startValue = 0; startValue < strbGRAI.ToString().Length;  startValue+=4)
			{
				strbConverted.Append(Convert.ToString(Convert.ToByte(RFUtils.ConvertBinaryToDecimal(strbGRAI.ToString().Substring(startValue,4))),16));
			}
			return strbConverted.ToString().ToUpper();
		}

        public static bool DecodeGRAI64(byte[] encodedGRAI64, out string companyPrefix, out string assetType, out string serialNo, out byte filterValue,
            bool throwException, out string errorMessage)
		{
            errorMessage = string.Empty;
            string GRAI = string.Empty;
            return DecodeGRAI64(encodedGRAI64, out GRAI, out companyPrefix, out assetType, out serialNo, out filterValue, throwException, out errorMessage);
		}

		#endregion

		#region Method to De code GRAI 96 to GRAI
		/// <summary>
		/// Method to De Encode GRAI96
		/// </summary>
		/// <param name="encodedGRAI96">Encoded GRAI96 12 byte byte array</param>
		/// <param name="GRAI">Output GRAI string</param>
		/// <param name="filterValue">Output filter value</param>
        public static bool DecodeGRAI96(byte[] encodedGRAI96, out string GRAI, out byte filterValue, out int partvalue, out string serialNo,
            bool throwException, out string errorMessage)
		{
            errorMessage = string.Empty;
            GRAI = string.Empty;
            serialNo = string.Empty;
            filterValue = 0;
            partvalue = 0;
			//Check for GRAI encoded array validation
			if(encodedGRAI96.Length != 12)
			{
                errorMessage = "Invalid GRAI96 string.";
                if(throwException)
				    throw new InvalidGRAI96EncodingException(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
			}
			//Check for header value validity header value is 00110011=51 in decimal
			if(encodedGRAI96[0] != 51)
			{
                errorMessage = "Invalid header value for GRAI 96";
                if(throwException)
                    throw new InvalidHeaderValue(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
			}

			//Xtract the contents of the byte array to a string
			StringBuilder strbEncodedString = new StringBuilder();

			for(int byteArrayIndex = 0; byteArrayIndex < encodedGRAI96.Length; byteArrayIndex++)
			{
				strbEncodedString.Append(RFUtils.AddReqdZeros(Convert.ToString(encodedGRAI96[byteArrayIndex],2).Trim(),8));
			}
			
			//Assign the filter value
			filterValue =Convert.ToByte(RFUtils.ConvertBinaryToDecimal(strbEncodedString.ToString().Substring(8,3)));

			//Get the partition value
			int partitionvalue = Convert.ToInt32(RFUtils.ConvertBinaryToDecimal(strbEncodedString.ToString().Substring(11,3)));
			partvalue = partitionvalue ;

			int  companyPrefixBitsLength=0,companyPrefixDigitsLength = 0, assetTypeDigitsLength =0,assetTypeBitsLength = 0;
			//Lookup the partition value depending on the company prefix length from the partition table
			switch(partitionvalue)
			{
				case 0:
					companyPrefixBitsLength = 40;
					companyPrefixDigitsLength = 12;
					assetTypeDigitsLength = 0;
					assetTypeBitsLength = 4;
					break;
				case 1:
					companyPrefixBitsLength = 37;
					companyPrefixDigitsLength = 11;
					assetTypeDigitsLength = 1;
					assetTypeBitsLength = 7;
					break;
				case 2:
					companyPrefixBitsLength = 34;
					companyPrefixDigitsLength = 10;
					assetTypeDigitsLength = 2;
					assetTypeBitsLength = 10;
					break;
				case 3:
					companyPrefixBitsLength = 30;
					companyPrefixDigitsLength = 9;
					assetTypeDigitsLength = 3;
					assetTypeBitsLength = 14;
					break;
				case 4:
					companyPrefixBitsLength = 27;
					companyPrefixDigitsLength = 8;
					assetTypeDigitsLength = 4;
					assetTypeBitsLength = 17;
					break;
				case 5:
					companyPrefixBitsLength = 24;
					companyPrefixDigitsLength = 7;
					assetTypeDigitsLength = 5;
					assetTypeBitsLength = 20;
					break;
				case 6:
					companyPrefixBitsLength = 20;
					companyPrefixDigitsLength = 6;
					assetTypeDigitsLength = 6;
					assetTypeBitsLength = 24;
					break;
				default:
                    errorMessage = "Invalid company prefix length.";
                    if(throwException)
					    throw new InvalidCompanyPrefixLengthException(errorMessage);
                    log.Trace("Decode Error:", errorMessage);
                    return false;
			}

			//Xtract the company prefix
			string tempCompanyPrefix = RFUtils.ConvertBinaryToDecimal(strbEncodedString.ToString(14,companyPrefixBitsLength));
			string companyPrefix = RFUtils.AddReqdZeros(tempCompanyPrefix, companyPrefixDigitsLength);

			//Xtract asset type
			string tempAssetType = RFUtils.ConvertBinaryToDecimal(strbEncodedString.ToString().Substring(14 + companyPrefixBitsLength, assetTypeBitsLength)) ;
			string assetType = RFUtils.AddReqdZeros(tempAssetType, assetTypeDigitsLength);

			//Check the validity of asset type reference
			if(Convert.ToInt32(assetType) >= Math.Pow(10, 12 - companyPrefixDigitsLength))
			{
                errorMessage = "the input bit string is not a legal GRAI-96 encoding.";
                if(throwException)
				    throw new InvalidGRAI96EncodingException(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
			}

			//Construct the check digit string
			StringBuilder strbCheckDigitString = new StringBuilder();

			strbCheckDigitString.Append("0");
			strbCheckDigitString.Append(companyPrefix);
			strbCheckDigitString.Append(assetType.Substring(0,(12 - companyPrefixDigitsLength)));

			//Calculate the checkdigit

			System.Int32 oddDigitTotal = 0;
			//first extract the odd digits from chkDigitString
			for(int oddIndex= 0; oddIndex <= 12; oddIndex+=2)
			{
				oddDigitTotal += Convert.ToInt32(strbCheckDigitString.ToString().Substring(oddIndex,1));
			}

			//Xtract the even digits from chkDigitString
			System.Int32 evenDigitTotal = 0;
			for(int evenIndex=1; evenIndex < 12; evenIndex+=2)
			{
				evenDigitTotal += Convert.ToInt32(strbCheckDigitString.ToString().Substring(evenIndex,1));
			}
			int calculatedCheckDigit = (1000 - ((3*oddDigitTotal) + evenDigitTotal)) % 10;

			//Xtract the serial no
			serialNo = RFUtils.ConvertBinaryToDecimal (strbEncodedString.ToString().Substring(14 + companyPrefixBitsLength + assetTypeBitsLength));

			GRAI = String.Concat(strbCheckDigitString.ToString(), calculatedCheckDigit.ToString());
			GRAI = String.Concat(GRAI, serialNo);
            return true;
		}
		
		/// <summary>
		///  Method to Decode encoded GRAI96 string and send the hex string
		/// </summary>
		/// <param name="encodedGRAI96">Encoded GRAI96 12 byte byte array</param>
		/// <returns>Decoded hex string</returns>
        public static string DecodeGRAI96(byte[] encodedGRAI96)
		{
			
			StringBuilder strbGRAI = new StringBuilder();
			//Extract the Byte array into a string
			for(int byteArrayIndex = 0; byteArrayIndex < encodedGRAI96.Length; byteArrayIndex ++)
			{
				strbGRAI.Append(RFUtils.AddReqdZeros(Convert.ToString(encodedGRAI96[byteArrayIndex],2),8));
			}
			
			StringBuilder strbConverted = new StringBuilder();
	
			for(int startValue = 0; startValue < strbGRAI.ToString().Length;  startValue+=4)
			{
				strbConverted.Append(Convert.ToString(Convert.ToByte(RFUtils.ConvertBinaryToDecimal(strbGRAI.ToString().Substring(startValue,4))),16));
			}
			return strbConverted.ToString().ToUpper();
		}


        public static bool DecodeGRAI96(byte[] encodedGRAI96, out string companyPrefix, out string assetType, out byte filterValue, out string serialNumRet,
            bool throwException, out string errorMessage)
		{
            errorMessage = string.Empty;
            int partVal = 0;
			companyPrefix = string.Empty ;
			assetType	 = string.Empty ;
			string strGRAI = string.Empty ;

            if (!DecodeGRAI96(encodedGRAI96, out strGRAI, out filterValue, out partVal, out serialNumRet, throwException, out errorMessage)) 
                return false;

			BreakGRAI(strGRAI,partVal,out companyPrefix,out assetType) ;
            return true;
		}

		#endregion Public Methods ENDS

		#endregion
	}
}
