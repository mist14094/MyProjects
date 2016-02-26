
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
	/// Summary description for DecodeSGLN.
	/// </summary>
    public class DecodeSGLN
	{
        private static Logger log = LogManager.GetCurrentClassLogger();
		
		#region Private Methods
        public static void BreakGLN(string strGLN, int partitionValue, out string companyPrefix, out string locationRef)
		{
			companyPrefix = string.Empty ;
			locationRef = string.Empty ;
			try
			{
				
				PartitionTable.FillPartitionTables(EPCFORMAT.SGLN) ;
				int companyPrefixDigitLen = PartitionTable.companyPrefixDigitLength[partitionValue] ;
				if(partitionValue == 0 )
				{
					companyPrefix = strGLN.Substring(0,companyPrefixDigitLen) ;
					return ;
				}
				int locationRefDigitLen  = PartitionTable.locationRefDigitLength[partitionValue] ;

				companyPrefix = strGLN.Substring(0,companyPrefixDigitLen) ;
				locationRef = strGLN.Substring(companyPrefixDigitLen,locationRefDigitLen) ;
			}
			catch(Exception e)
			{
				throw e ;
			}
		}
		#endregion Private Methods ENDS
		
		private DecodeSGLN()
		{}

		#region Method to Decode SGLN64
		/// <summary>
		/// Method to Decode SGLN64
		/// </summary>
		/// <param name="encodedGLN64">Encoded SGLN64 8 byte byte array</param>
		/// <param name="GLN">Output GLN string</param>
		/// <param name="serialNo">Output serial no</param>
		/// <param name="filterValue">Output filter value</param>
        public static bool DecodeSGLN64(byte[] encodedGLN64, out string GLN, out string companyPrefix, out string locationRef, out string serialNo, out byte filterValue,
            bool throwException, out string errorMessage)
		{
            errorMessage = string.Empty;
            GLN = string.Empty;
			companyPrefix = string.Empty ;
			locationRef = string.Empty ;
            serialNo = string.Empty;
            filterValue = 0;
			//Check for byte array validity
			if(encodedGLN64.Length != 8)
			{
                errorMessage = "Not a valid SGLN64 string.";
                if(throwException)
				    throw new InvalidSGLN64EncodingException(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
			}
			//Check for header value validity header value is 00001001=9 in decimal
			if(encodedGLN64[0] != 9)
			{
                errorMessage = "Invalid header value for SGLN64";
                if(throwException)
				    throw new InvalidHeaderValue(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
			}

			//Xtract the contents of the byte array to a string
			StringBuilder strbEncodeString = new StringBuilder();

			for(int byteArrayIndex = 0; byteArrayIndex < encodedGLN64.Length; byteArrayIndex++)
			{
				strbEncodeString.Append(RFUtils.AddReqdZeros(Convert.ToString(encodedGLN64[byteArrayIndex],2).Trim(),8));
			}

			//xtract the filter value
			filterValue = Convert.ToByte(RFUtils.ConvertBinaryToDecimal(strbEncodeString.ToString().Substring(8,3)));

			//Xtract the company prefix index
			string companyPrefixIndex = RFUtils.ConvertBinaryToDecimal(strbEncodeString.ToString().Substring(11,14));

			companyPrefix = RFUtils.FetchCompanyPrefix(Convert.ToUInt16(companyPrefixIndex)) ;
			//Validate this company reference with Copnay Prefix translation table
			int companyPrefixLength = companyPrefix.Length ;//Assign the company prefix length here

			//Xtract the location reference
			locationRef = RFUtils.ConvertBinaryToDecimal(strbEncodeString.ToString().Substring(25,20));

			//Check for Location reference validity
			if(Convert.ToInt32(locationRef) >= Math.Pow(10, 12 - companyPrefixLength))
			{
                errorMessage = "DecodeSGLN64() locationReference value exceeds limits : locationRef Value :" + locationRef;
                if(throwException)
				    throw new InvalidSGLN64EncodingException(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
			}

			if(locationRef.Length < (12-companyPrefixLength))
			{
				locationRef = RFUtils.AddReqdZeros(locationRef,(12-companyPrefixLength));
			}

			//Construct a 12-digit check digit string
			StringBuilder strbChkDigitString = new StringBuilder();

			strbChkDigitString.Append(companyPrefixIndex);
			strbChkDigitString.Append(locationRef);

			//Calculate the checkdigit

			string calculatedCheckDigit = RFUtils.CalculateCheckDigit(strbChkDigitString.ToString(),Constants.SGLN13_LEN) ;

			//Xtract the next string
			GLN = String.Concat(strbChkDigitString.ToString(), calculatedCheckDigit);

			//Xtract the 19 digit serial no
			string tempSerialNo = strbEncodeString.ToString().Substring(45) ;
			serialNo = Convert.ToString(Convert.ToInt32(tempSerialNo,2)) ;
            return true;
		}

		/// <summary>
		///  Method to Decode encoded SGLN64 string and send the hex string
		/// </summary>
		/// <param name="encodedSGLN64">Encoded SGLN64 8 byte byte array</param>
		/// <returns>Decoded hex string</returns>
        public static string DecodeSGLN64(byte[] encodedSGLN64)
		{
			
			StringBuilder strbSGLN = new StringBuilder();
			//Extract the Byte array into a string
			for(int byteArrayIndex = 0; byteArrayIndex < encodedSGLN64.Length; byteArrayIndex ++)
			{
				strbSGLN.Append(RFUtils.AddReqdZeros(Convert.ToString(encodedSGLN64[byteArrayIndex],2),8));
			}
			
			StringBuilder strbConverted = new StringBuilder();
	
			for(int startValue = 0; startValue < strbSGLN.ToString().Length;  startValue+=4)
			{
				strbConverted.Append(Convert.ToString(Convert.ToByte(RFUtils.ConvertBinaryToDecimal(strbSGLN.ToString().Substring(startValue,4))),16));
			}
			return strbConverted.ToString().ToUpper();
		}


        public static bool DecodeSGLN64(byte[] encodedGLN64, out string companyPrefix, out string locationRef, out string serialNo, out byte filterValue,
            bool throwException, out string errorMessage)
        {
            errorMessage = string.Empty;
            companyPrefix = string.Empty;
            locationRef = string.Empty;
            serialNo = string.Empty;
            filterValue = 0;
            string strGLN = string.Empty;
            if (!DecodeSGLN64(encodedGLN64, out strGLN, out companyPrefix, out locationRef, out serialNo, out filterValue, throwException, out errorMessage))
                return false;
            return true;
        }

		#endregion

		#region Method to Decode SGLN96
		/// <summary>
		/// Method to decode SGLN96
		/// </summary>
		/// <param name="encodedGLN96">Encoded SGLN96 12 byte byte array</param>
		/// <param name="GLN">Output GLN string</param>
		/// <param name="serialNo">Output serial no</param>
		/// <param name="filterValue">Output filter value</param>
        public static bool DecodeSGLN96(byte[] encodedGLN96, out string GLN, out string serialNo, out byte filterValue, out int partVal,
            bool throwException, out string errorMessage)
		{
            errorMessage = string.Empty;
            GLN = string.Empty;
            serialNo = string.Empty;
            filterValue = 0;
            partVal = 0;

			//Check for the encoded SGLN array length
			if(encodedGLN96.Length != 12)
			{
                errorMessage = "This is not a valid SGLN96 string.";
                if(throwException)
                    throw new InvalidSGLN96EncodingException(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
			}
			//Check for header value validity header value is 00110010=50 in decimal
			if(encodedGLN96[0] != 50)
			{
                errorMessage = "Invalid header value for SGLN 96";
                if(throwException)
				    throw new InvalidHeaderValue(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
			}

			//Xtract the encoded string from the array
			StringBuilder strbEncodedString = new StringBuilder();

			for(int byteArrayIndex = 0; byteArrayIndex < encodedGLN96.Length; byteArrayIndex++)
			{
				strbEncodedString.Append(RFUtils.AddReqdZeros(Convert.ToString(encodedGLN96[byteArrayIndex],2).Trim(),8));
			}



			//Xtract the filter values
			filterValue = Convert.ToByte(RFUtils.AddReqdZeros(RFUtils.ConvertBinaryToDecimal(strbEncodedString.ToString().Substring(8,3)), 3));

			//Xtract the partition value
			byte partitionValue = Convert.ToByte(RFUtils.AddReqdZeros(RFUtils.ConvertBinaryToDecimal(strbEncodedString.ToString().Substring(11,3)), 3));
			partVal = Convert.ToInt32(partitionValue) ;
			//Xtract the number of digits and number of bits for the company prefix
			int companyPrefixBitLength = 0;
			int companyPrefixDigitLength = 0;
			int locationReferenceBitLength = 0;
			int locationReferenceDigitLength = 0;

			switch(partitionValue)
			{
				case 0:
					companyPrefixBitLength = 40;
					companyPrefixDigitLength = 12;
					locationReferenceBitLength = 1;
					locationReferenceDigitLength = 0;
					break;
				case 1:
					companyPrefixBitLength = 37;
					companyPrefixDigitLength = 11;
					locationReferenceBitLength = 4;
					locationReferenceDigitLength = 1;
					break;
				case 2:
					companyPrefixBitLength = 34;
					companyPrefixDigitLength = 10;
					locationReferenceBitLength = 7;
					locationReferenceDigitLength = 2;
					break;
				case 3:
					companyPrefixBitLength = 30;
					companyPrefixDigitLength = 9;
					locationReferenceBitLength = 11;
					locationReferenceDigitLength = 3;
					break;
				case 4:
					companyPrefixBitLength = 27;
					companyPrefixDigitLength = 8;
					locationReferenceBitLength = 14;
					locationReferenceDigitLength = 4;
					break;
				case 5:
					companyPrefixBitLength = 24;
					companyPrefixDigitLength = 7;
					locationReferenceBitLength = 17;
					locationReferenceDigitLength = 5;
					break;
				case 6:
					companyPrefixBitLength = 20;
					companyPrefixDigitLength = 6;
					locationReferenceBitLength = 21;
					locationReferenceDigitLength = 6;
					break;
                default:
                    errorMessage = "Invalid SGLN96 partition value.";
                    if(throwException)
					    throw new InvalidSGLN96EncodingException(errorMessage);
                    log.Trace("Decode Error:", errorMessage);
                    return false;
			}

			//xtract the company prefix
			string companyPrefix = RFUtils.AddReqdZeros(RFUtils.ConvertBinaryToDecimal(strbEncodedString.ToString().Substring(14,companyPrefixBitLength)), companyPrefixDigitLength);

			//Check for company prefix validity
			if(Convert.ToUInt64(companyPrefix) > Math.Pow(10, companyPrefixDigitLength))
			{
                errorMessage = "Invalid company reference.";
                if(throwException)
				    throw new InvalidSGLN96EncodingException(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
			}

			//Xtract the location reference
			string locationReference = RFUtils.AddReqdZeros(RFUtils.ConvertBinaryToDecimal(strbEncodedString.ToString().Substring((14 + companyPrefixBitLength), locationReferenceBitLength)), locationReferenceDigitLength);

			//Check location reference validity
			if(Convert.ToInt64(locationReference) >= Math.Pow(10, 12 - companyPrefixDigitLength))
			{
                errorMessage = "the input bit string is not a legal SGLN-96 encoding.";
                if(throwException)
				    throw new InvalidSGLN96EncodingException(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
			}

			//Construct a check digit string
			StringBuilder strbChkDigitString = new StringBuilder();
			strbChkDigitString.Append(companyPrefix);
			strbChkDigitString.Append(locationReference);

//			//Calculate the checkdigit
//
//			System.Int32 oddDigitTotal = 0;
//			//first extract the odd digits from chkDigitString
//			for(int oddIndex= 0; oddIndex <= 11; oddIndex+=2)
//			{
//				oddDigitTotal += Convert.ToInt32(strbChkDigitString.ToString().Substring(oddIndex,1));
//			}
//
//			//Xtract the even digits from chkDigitString
//			System.Int32 evenDigitTotal = 0;
//			for(int evenIndex=1; evenIndex < 12; evenIndex+=2)
//			{
//				evenDigitTotal += Convert.ToInt32(strbChkDigitString.ToString().Substring(evenIndex,1));
//			}
//			int calculatedCheckDigit = (1000 - ((3*oddDigitTotal) + evenDigitTotal)) % 10;
			string calculatedCheckDigit	=	RFUtils.CalculateCheckDigit(strbChkDigitString.ToString(),Constants.SGLN13_LEN) ;
			GLN = String.Concat(strbChkDigitString.ToString(), calculatedCheckDigit);

			//Extract the serial no
			serialNo = RFUtils.ConvertBinaryToDecimal(strbEncodedString.ToString().Substring(55));
            return true;
		}

		/// <summary>
		///  Method to Decode encoded SGLN96 string and send the hex string
		/// </summary>
		/// <param name="encodedSGLN96">Encoded SGLN96 12 byte byte array</param>
		/// <returns>Decoded hex string</returns>
        public static string DecodeSGLN96(byte[] encodedSGLN96)
		{
			
			StringBuilder strbSGLN = new StringBuilder();
			//Extract the Byte array into a string
			for(int byteArrayIndex = 0; byteArrayIndex < encodedSGLN96.Length; byteArrayIndex ++)
			{
				strbSGLN.Append(RFUtils.AddReqdZeros(Convert.ToString(encodedSGLN96[byteArrayIndex],2),8));
			}
			
			StringBuilder strbConverted = new StringBuilder();
	
			for(int startValue = 0; startValue < strbSGLN.ToString().Length;  startValue+=4)
			{
				strbConverted.Append(Convert.ToString(Convert.ToByte(RFUtils.ConvertBinaryToDecimal(strbSGLN.ToString().Substring(startValue,4))),16));
			}
			return strbConverted.ToString().ToUpper();
		}


        public static bool DecodeSGLN96(byte[] encodedGLN96, out string companyPrefix, out string locationRef, out string serialNo, out byte filterValue,
            bool throwException, out string errorMessage)
		{
            errorMessage = string.Empty;
            companyPrefix = string.Empty;
			locationRef	 = string.Empty ;
			serialNo	 = string.Empty;
			filterValue	 = 0 ;
			string GLN	 = string.Empty ;
			int partVal	 = 0 ;

			try
			{
                if (!DecodeSGLN96(encodedGLN96, out GLN, out serialNo, out filterValue, out partVal, throwException, out errorMessage))
                    return false;
				BreakGLN(GLN,partVal,out companyPrefix,out locationRef) ;
			}
			catch(Exception)
			{
				throw;
			}
            return true;
		}
		#endregion
	}
}
