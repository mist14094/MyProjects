
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
	/// Summary description for DecodeSSCC.
	/// </summary>
    public class DecodeSSCC
	{
        private static Logger log = LogManager.GetCurrentClassLogger();

		#region Private Methods

        public static void BreakSCC(string SCC, int partitionValue, out string companyPrefix, out string serialRef)
		{
			companyPrefix = string.Empty ;
			serialRef = string.Empty ;
			
			try
			{
				PartitionTable.FillPartitionTables(EPCFORMAT.SSCC) ;
				int companyPrefixDigitLen = PartitionTable.companyPrefixDigitLength[partitionValue] ;
				int serialRefDigitLen  = PartitionTable.serialRefDigitLength[partitionValue] ;

				companyPrefix = SCC.Substring(1,companyPrefixDigitLen) ;
				string tempExtDigit = SCC.Substring(0,1) ;
				string tempSerialRef = SCC.Substring(companyPrefixDigitLen+1,serialRefDigitLen-1) ;
				serialRef = tempExtDigit+tempSerialRef ;
			}
			catch(Exception e)
			{
				throw e ;
			}
		}

		#endregion Private Methods ENDS
		
		
		private DecodeSSCC()
		{}

		#region Method to Decode SCC to SSCC64
		/// <summary>
		/// Method to serialize SCC to SSCC64
		/// </summary>
		/// <param name="encodedSSCC64">Encoded SSCC64 8 byte byte array</param>
		/// <param name="SCC">Output SCC string</param>
		/// <param name="filterValue">Output filter value</param>
        public static bool DecodeSSCC64(byte[] encodedSSCC64, out string SCC, out string extensionDigit, out string companyPrefix, out string serialReference, out byte filterValue,
            bool throwException, out string errorMessage)	
		{
            errorMessage = string.Empty;
            SCC = String.Empty;
			companyPrefix = string.Empty ;
			serialReference = string.Empty ;
            extensionDigit = string.Empty;
			filterValue = 0;
			//Check for the encoded SSCC array length
			if(encodedSSCC64.Length != 8)
			{
                errorMessage = "This is not a valid SSCC64 string.";
                if(throwException)
				    throw new InvalidSSCC64EncodingException(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
			}

			//Check for header value validity header value is 00001000=8 in decimal
			if(encodedSSCC64[0] != 8)
			{
                errorMessage = "Invalid header value for SSCC 64";
                if(throwException)
				    throw new InvalidHeaderValue(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
			}

			//Xtract the contents of the byte array to a string
			StringBuilder strbEncodeString = new StringBuilder();

			for(int byteArrayIndex = 0; byteArrayIndex < encodedSSCC64.Length; byteArrayIndex++)
			{
				strbEncodeString.Append(RFUtils.AddReqdZeros(Convert.ToString(encodedSSCC64[byteArrayIndex],2).Trim(),8));
			}

			//xtract the filter value
			filterValue = Convert.ToByte(RFUtils.ConvertBinaryToDecimal(strbEncodeString.ToString().Substring(8,3)));

			//Xtract the company prefix index
			string companyPrefixIndex = RFUtils.ConvertBinaryToDecimal(strbEncodeString.ToString().Substring(11,14));

			companyPrefix = RFUtils.FetchCompanyPrefix(Convert.ToUInt16(companyPrefixIndex)) ;
			//Validate this company reference with Copnay Prefix translation table
			int companyPrefixLength = companyPrefix.Length ;//Assign the company prefix length here

			//Xtract the serial reference
			serialReference = RFUtils.ConvertBinaryToDecimal(strbEncodeString.ToString().Substring(25,39));

			//Check for serial reference validity
			if(Convert.ToUInt64(serialReference) >= Math.Pow(10, 17 - companyPrefixLength))
			{
                errorMessage = "DecodeSSCC64() serialReference exceeds max limits.Value: " + serialReference;
                if(throwException)
				    throw new InvalidSSCC64EncodingException(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
			}


			if( serialReference.Length < (17-companyPrefixLength))
			{
				serialReference = RFUtils.AddReqdZeros(serialReference,(17-companyPrefixLength));	 //16 length as no extension digit and no check digit yet
			}

			extensionDigit = serialReference.Substring(0,1) ;

			serialReference = serialReference.Substring(1);

			//Construct a 17-digit check digit string
			StringBuilder strbChkDigitString = new StringBuilder();

			strbChkDigitString.Append(extensionDigit) ;			
			strbChkDigitString.Append(companyPrefix);
			strbChkDigitString.Append(serialReference);

			//Calculate the checkdigit
			string calculatedCheckDigit = RFUtils.CalculateCheckDigit(strbChkDigitString.ToString(),Constants.SSCC18_LEN-1) ;
			
			//Xtract the next string
			SCC = String.Concat(strbChkDigitString.ToString(), calculatedCheckDigit);
            return true;
		}

		/// <summary>
		///  Method to Decode encoded SSCC64 string and send the hex string
		/// </summary>
		/// <param name="encodedSSCC64">Encoded SSCC64 8 byte byte array</param>
		/// <returns>Decoded hex string</returns>
        public static string DecodeSSCC64(byte[] encodedSSCC64)
		{
			
			StringBuilder strbSSCC = new StringBuilder();
			//Extract the Byte array into a string
			for(int byteArrayIndex = 0; byteArrayIndex < encodedSSCC64.Length; byteArrayIndex ++)
			{
				strbSSCC.Append(RFUtils.AddReqdZeros(Convert.ToString(encodedSSCC64[byteArrayIndex],2),8));
			}
			
			StringBuilder strbConverted = new StringBuilder();
	
			for(int startValue = 0; startValue < strbSSCC.ToString().Length;  startValue+=4)
			{
				strbConverted.Append(Convert.ToString(Convert.ToByte(RFUtils.ConvertBinaryToDecimal(strbSSCC.ToString().Substring(startValue,4))),16));
			}
			return strbConverted.ToString().ToUpper();
		}


        public static bool DecodeSSCC64(byte[] encodedSSCC64, out string extensionDigit, out string companyPrefix, out string serialRef, out byte filterValue,
            bool throwException, out string errorMessage)
		{
			try
			{
                errorMessage = string.Empty;
                string SCC = string.Empty;
                if (!DecodeSSCC64(encodedSSCC64, out SCC, out extensionDigit, out companyPrefix, out serialRef, out filterValue,
                    throwException, out errorMessage))
                    return false;
			}
			catch(Exception e)
			{
				throw e ;
			}
            return true;
		}

		#endregion

		#region Method to Decode SCC to SSCC96
		/// <summary>
		/// Method to De serialize 96 bit SSCC
		/// </summary>
		/// <param name="encodedSSCC96">Encoded SSCC96 12 byte byte array</param>
		/// <param name="SCC">Output SCC string</param>
		/// <param name="filterValue">Output filter value</param>
        public static bool DecodeSSCC96(byte[] encodedSSCC96, out string SCC, out byte filterValue, out int partValue,
            bool throwException, out string errorMessage)	
		{
            errorMessage = string.Empty;
            SCC = string.Empty;
            filterValue = 0;
            partValue = 0;
			//Check for the encoded SSCC array length
			if(encodedSSCC96.Length != 12)
			{
                errorMessage = "This is not a valid SSCC96 string.";
                if (throwException)
                    throw new InvalidSSCC96EncodingException(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                
                 return false;
			}

			//Check for header value validity header value is 00110001=49 in decimal
			if(encodedSSCC96[0] != 49)
			{
                errorMessage = "Invalid header value for SSCC 96";
                if (throwException)
    				throw new InvalidHeaderValue(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
			}

			//Xtract the encoded string from the array
			StringBuilder strbEncodedString = new StringBuilder();

			for(int byteArrayIndex = 0; byteArrayIndex < encodedSSCC96.Length; byteArrayIndex++)
			{
				strbEncodedString.Append(RFUtils.AddReqdZeros(Convert.ToString(encodedSSCC96[byteArrayIndex],2).Trim(),8));
			}

			//Xtract the filter values
			filterValue = Convert.ToByte(RFUtils.AddReqdZeros(RFUtils.ConvertBinaryToDecimal(strbEncodedString.ToString().Substring(8,3)), 3));

			//Xtract the partition value
			byte partitionValue = Convert.ToByte(RFUtils.AddReqdZeros(RFUtils.ConvertBinaryToDecimal(strbEncodedString.ToString().Substring(11,3)), 3));
			partValue =	 Convert.ToInt32(partitionValue) ;

			//Xtract the number of digits and number of bits for the company prefix
			int companyPrefixBitLength = 0;
			int companyPrefixDigitLength = 0;
			int serialReferenceBitLength = 0;
			int serialReferenceDigitLength = 0;

			switch(partitionValue)
			{
				case 0:
					companyPrefixBitLength = 40;
					companyPrefixDigitLength = 12;
					serialReferenceBitLength = 18;
					serialReferenceDigitLength = 5;
					break;
				case 1:
					companyPrefixBitLength = 37;
					companyPrefixDigitLength = 11;
					serialReferenceBitLength = 21;
					serialReferenceDigitLength = 6;
					break;
				case 2:
					companyPrefixBitLength = 34;
					companyPrefixDigitLength = 10;
					serialReferenceBitLength = 24;
					serialReferenceDigitLength = 7;
					break;
				case 3:
					companyPrefixBitLength = 30;
					companyPrefixDigitLength = 9;
					serialReferenceBitLength = 28;
					serialReferenceDigitLength = 8;
					break;
				case 4:
					companyPrefixBitLength = 27;
					companyPrefixDigitLength = 8;
					serialReferenceBitLength = 31;
					serialReferenceDigitLength = 9;
					break;
				case 5:
					companyPrefixBitLength = 24;
					companyPrefixDigitLength = 7;
					serialReferenceBitLength = 34;
					serialReferenceDigitLength = 10;
					break;
				case 6:
					companyPrefixBitLength = 20;
					companyPrefixDigitLength = 6;
					serialReferenceBitLength = 38;
					serialReferenceDigitLength = 11;
					break;
				default:
                    errorMessage = "Invalid SSCC96 partition value.";
                    if (throwException)
					    throw new InvalidSSCC96EncodingException(errorMessage);
                    log.Trace("Decode Error:", errorMessage);
                    return false;
			}

			//XTract the company prefix index
			string companyPrefixIndex = RFUtils.AddReqdZeros(RFUtils.ConvertBinaryToDecimal(strbEncodedString.ToString().Substring(14, companyPrefixBitLength)), companyPrefixDigitLength);

			//Check for company prefix validity
			if(Convert.ToInt64(companyPrefixIndex) >= Math.Pow(10, companyPrefixDigitLength))
			{
                errorMessage = "The input bit string is not a legal SSCC96 encode string.";
                if (throwException)
				    throw new InvalidSSCC96EncodingException(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
			}

			//Xtract the serial reference
			string serialReference = RFUtils.AddReqdZeros(RFUtils.ConvertBinaryToDecimal(strbEncodedString.ToString().Substring(14 + companyPrefixBitLength, serialReferenceBitLength)), serialReferenceDigitLength);

			//Check for serial reference validity 
			if(Convert.ToInt64(serialReference) >= Math.Pow(10, 17 - companyPrefixDigitLength))
			{
                errorMessage = "The input bit string is not a legal SSCC-96 encoding";
                if (throwException)
				    throw new InvalidSSCC96EncodingException(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
			}

			//Construct the checkdigit number
			StringBuilder strbChkDigitString = new StringBuilder();

			strbChkDigitString.Append(serialReference.Substring(0,1));
			strbChkDigitString.Append(companyPrefixIndex.Substring(0));
			strbChkDigitString.Append(serialReference.Substring(1));

			//Calculate the checkdigit

			System.Int32 oddDigitTotal = 0;
			//first extract the odd digits from chkDigitString
			for(int oddIndex= 0; oddIndex <= 17; oddIndex+=2)
			{
				oddDigitTotal += Convert.ToInt32(strbChkDigitString.ToString().Substring(oddIndex,1));
			}

			//Xtract the even digits from chkDigitString
			System.Int32 evenDigitTotal = 0;
			for(int evenIndex=1; evenIndex < 17; evenIndex+=2)
			{
				evenDigitTotal += Convert.ToInt32(strbChkDigitString.ToString().Substring(evenIndex,1));
			}

			string calculatedCheckDigit = Convert.ToString((1000 - ((3 *oddDigitTotal) + evenDigitTotal))% 10).Trim();//Check digit value

			SCC = String.Concat(strbChkDigitString.ToString(), calculatedCheckDigit.Trim());//SCC string value
            return true;
		}

		/// <summary>
		///  Method to Decode encoded SSCC96 string and send the hex string
		/// </summary>
		/// <param name="encodedSSCC96">Encoded SSCC96 12 byte byte array</param>
		/// <returns>Decoded hex string</returns>
        public static string DecodeSSCC96(byte[] encodedSSCC96)
		{
			
			StringBuilder strbSSCC = new StringBuilder();
			//Extract the Byte array into a string
			for(int byteArrayIndex = 0; byteArrayIndex < encodedSSCC96.Length; byteArrayIndex ++)
			{
				strbSSCC.Append(RFUtils.AddReqdZeros(Convert.ToString(encodedSSCC96[byteArrayIndex],2),8));
			}
			
			StringBuilder strbConverted = new StringBuilder();
	
			for(int startValue = 0; startValue < strbSSCC.ToString().Length;  startValue+=4)
			{
				strbConverted.Append(Convert.ToString(Convert.ToByte(RFUtils.ConvertBinaryToDecimal(strbSSCC.ToString().Substring(startValue,4))),16));
			}
			return strbConverted.ToString().ToUpper();
		}


        public static bool DecodeSSCC96(byte[] encodedSSCC96, out string companyPrefix, out string serialRef, out byte filterValue,
            bool throwException, out string errorMessage)
		{
            try
            {
                errorMessage = string.Empty;
                companyPrefix = string.Empty;
                serialRef = string.Empty;
                filterValue = 0;
                string SCC = string.Empty;
                int partitionVal = 0;


                if (!DecodeSSCC96(encodedSSCC96, out SCC, out filterValue, out partitionVal, throwException, out errorMessage))
                    return false;
                BreakSCC(SCC, partitionVal, out companyPrefix, out serialRef);
            }
            catch (Exception)
            {
                throw;
            }
            return true;
		}
		#endregion
	}
}
