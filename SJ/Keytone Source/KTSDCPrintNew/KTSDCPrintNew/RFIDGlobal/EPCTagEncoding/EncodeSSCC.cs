
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


namespace KTone.RFIDGlobal.EPCTagEncoding
{
	/// <summary>
	/// Summary description for EncodeSSCC.
	/// </summary>
    public class EncodeSSCC
	{
		#region Attributes
		private static byte m_filterValue = Constants.FILTERVALUE ;
		#endregion


		#region	 Private Methods

		private EncodeSSCC()
		{}


        public static string CreateSSCC18(int extensionDigit, string companyPrefix, string serialRef)
		{
			//SSCC-18 code to be created
			StringBuilder output = null ;

			int companyPrefLen = companyPrefix.Length;

			//Company Prefix Length should be btw 6 and 12 both included
			if ((companyPrefLen<6)||(companyPrefLen>12))
				throw new InvalidSSCCCompanyPrefixException("Invalid company prefix length.");

			Int64 serialRefLen = serialRef.Length;
			try
			{
				// Length of (companyPrefLen + serialRefLen) should be equal to 16
				if((companyPrefLen+serialRefLen)!=16)
					throw new InvalidSSCC96EncodingException("'Company Prefix' and 'Serial Reference' are incompatible");
			
				if ((extensionDigit<0)||(extensionDigit>9))
					throw new InvalidSSCCExtensionDigitException("Invalid 'Extension Digit':Value:"+extensionDigit);

				if (!CheckSSCC18Validity(serialRef,extensionDigit,companyPrefLen))
					throw new InvalidSSCC96EncodingException("Invalid 'Serial Reference' or 'Company Prefix'");

				output = new StringBuilder();
				//Adding extension digit
				output.Append(extensionDigit);
				//Adding EAN.UCC Company Prefix
				output.Append(companyPrefix);
				//Adding Serial Reference Number
				output.Append(serialRef);
				//output.Append(Common.CreateCheckDigit(output,Constants.SSCC18_LEN-1));
				string checkDigit = RFUtils.CalculateCheckDigit(output.ToString(),Constants.SSCC18_LEN-1) ;
				output.Append(checkDigit) ;
				return output.ToString();
			}
			catch (Exception ex)
			{
				throw new InvalidSSCC96EncodingException("Unable to generate SSCC18 Code"+ex.Message);
			}
		}

		/// <summary>
		/// Checks whether the SSCC18 code is valid or not
		/// </summary>
		/// <param name="serialRef">Serial Reference Number</param>
		/// <param name="extensionDigit">Extension Digit</param>
		/// <param name="companyPrefixLength">EAN.UCC company prefix Length</param>
		/// <returns>true if valid, false if invalid</returns>
		private static bool CheckSSCC18Validity(string serialRef,int extensionDigit, int companyPrefixLength)
		{
			bool chkValidity = false;
			int serialReferenceBitsLength=0;
			switch(companyPrefixLength)
			{
				case 12:
					serialReferenceBitsLength = 18;
					break;
				case 11:
					serialReferenceBitsLength = 21;
					break;
				case 10:
					serialReferenceBitsLength = 24;
					break;
				case 9:
					serialReferenceBitsLength = 28;
					break;
				case 8:
					serialReferenceBitsLength = 31;
					break;
				case 7:
					serialReferenceBitsLength = 34;
					break;
				case 6:
					serialReferenceBitsLength = 38;
					break;
				default:
					return false;
			}
			
			StringBuilder slRef = new StringBuilder();
			slRef.Append(extensionDigit);
			slRef.Append(serialRef);

			string slRefBit = Convert.ToString(Convert.ToInt64(slRef.ToString()),2);
			if (slRefBit.Length > serialReferenceBitsLength)
				chkValidity = false;
			else
				chkValidity = true;

			return chkValidity;
		}

		#endregion Private Methods

		#region Method to Encode SSCC to SSCC64
		/// <summary>
		/// Method to encode SCC to 64 bit SSCC
		/// </summary>
		/// <param name="actualSCC">Supplied SCC string</param>
		/// <param name="companyPrefixLength">Supplied company prefix index length</param>
		/// <param name="filterValue">Supplied filter value</param>
		/// <returns>A 8 byte byte array containing  SSCC64</returns>
        public static byte[] EncodeSCCtoSSCC64(string actualSCC, Int32 companyPrefixLength, byte filterValue)
		{
			//Check for SCC string validity
			if(actualSCC.Length != 18)
			{
				throw new InvalidSSCC64EncodingException("Invalid SCC string.");
			}

			//Check for filter value
			if(filterValue > 7)
			{
				throw new InvalidSSCC64EncodingException("Invalid filter value.");
			}

			//Xtract the company Prefix
			string companyPrefix = actualSCC.Substring(1, companyPrefixLength);

			//Validate this company prefix with Company prefix translation table
			ICompanyPrefixLookup iLookup = CompanyPrefixLookupImpl.GetInstanceOf()  ;
			UInt16 companyPrefixIndex = 0 ;
			try
			
			{
				bool isValid =  iLookup.Lookup(companyPrefix,out companyPrefixIndex) ;

				if(!isValid)
					throw new CompanyPrefixLookupImplException("EncodeGTIN14toSGTIN64() : Company Prefix not listed in Translation Table : Value of Comp Prefix :"+companyPrefix) ;
			}
			catch(CompanyPrefixLookupImplException e)
			{
				throw e ;
			}
			
			//Validate this with the Company Prefix translation table to obtain the company prefix
			StringBuilder strbSerialReference = new StringBuilder();
			strbSerialReference.Append(actualSCC.Substring(0,1));
			strbSerialReference.Append(actualSCC.Substring(companyPrefixLength+1, 17 - (companyPrefixLength + 1)));

			//Check for serial reference validity
			if(Convert.ToInt64(strbSerialReference.ToString()) > Math.Pow(2,39))
			{
				throw new InvalidSSCC64EncodingException("Serial Ref Value greater than expected..");
			}

			//Construct the main encoded string
			StringBuilder strbEncodedString = new StringBuilder();

			//Insert the header value
			strbEncodedString.Append(Constants.SSCC64_HEADER);

			//Insert the filter value
			strbEncodedString.Append(RFUtils.AddReqdZeros(Convert.ToString(filterValue,2),3));

			//Insert the company prefix index
			strbEncodedString.Append(RFUtils.AddReqdZeros(Convert.ToString(Convert.ToInt16(companyPrefixIndex),2),14));

			//Insert the serial reference
			strbEncodedString.Append(RFUtils.AddReqdZeros(Convert.ToString(Convert.ToInt64(strbSerialReference.ToString()),2),39));


			byte[] encodedSSCC64 = new byte[8];
			//Insert the string values to byte array
			for(int arrIndex=0, startValue = 0; arrIndex < 8; arrIndex++, startValue+=8)
			{
				encodedSSCC64[arrIndex] = Convert.ToByte(RFUtils.ConvertBinaryToDecimal(strbEncodedString.ToString().Substring(startValue,8)));
			}
			return encodedSSCC64;
		}

		/// <summary>
		/// Method to serialize SCC hex string to 64 bit SSCC
		/// </summary>
		/// <param name="SCCHexValue">SCC hex string</param>
		/// <returns>A 8 byte byte array containing  SSCC64</returns>
        public static byte[] EncodeSCCtoSSCC64(string SCCHexValue)
		{
			//Check for SCC string validity
			if(SCCHexValue.Length != 16)
			{
				throw new InvalidSSCC64EncodingException("Invalid SCC string.");
			}
			//Declare the string bulder to contain the encoded bits
			StringBuilder strbConverted = new StringBuilder();

			for(int stringIndex=0; stringIndex < SCCHexValue.Length; stringIndex++)
			{
				strbConverted.Append(RFUtils.AddReqdZeros(Convert.ToString(Convert.ToByte(RFUtils.ConvertHexToDecimal(SCCHexValue.Substring(stringIndex,1))),2),4));
			}
			
			//Declare the byte array to return
			byte[] encodedSSCC64 = new byte[8];//SSCC64 will return this 8 bytes array

			//Insert the string values to byte array
			for(int arrIndex=0, startValue = 0; arrIndex < 8; arrIndex++, startValue+=8)
			{
				encodedSSCC64[arrIndex] = Convert.ToByte(RFUtils.ConvertBinaryToDecimal(strbConverted.ToString().Substring(startValue,8)));
			}
			return encodedSSCC64;
		}

        public static byte[] EncodeSCCtoSSCC64(byte filterValue, string extensionDigit, string companyPrefix, string serialRef)
		{
			byte[] encodedSSCC64 = null ;
			
			string actualSCC = CreateSSCC18(Convert.ToInt16(extensionDigit),companyPrefix,serialRef) ;

			encodedSSCC64 = EncodeSCCtoSSCC64(actualSCC,companyPrefix.Length,filterValue) ;

			return encodedSSCC64;
		}

		#endregion

		#region Method to Encode SSCC to SSCC96
		/// <summary>
		/// Method to encode 18-digit SCC to SSCC 96 bit encoding byte array.
		/// </summary>
		/// <param name="actualSCC">Supplied SCC string</param>
		/// <param name="companyPrefixLength">Supplied company prefix index length</param>
		/// <param name="filterValue">Supplied filter value</param>
		/// <returns>A 12 byte byte array containing SSCC96</returns>
        public static byte[] EncodeSCCtoSSCC96(byte filterValue, string actualSCC, Int32 companyPrefixLength)
		{
			//Check for SCC string validity
			if(actualSCC.Length != 18)
			{
				throw new InvalidSSCC96EncodingException ("Invalid SSCC string.");
			}

			//Check for filter value
			if(filterValue > 7)
			{
				throw new InvalidSSCC96EncodingException("Invalid filter value.");
			}

			m_filterValue = filterValue ;
			//Get the indicator digit
			byte indicatorDigit = Convert.ToByte(actualSCC.Substring(0,1));

			int companyPrefixBitsLength = 0, companyPrefixDigitsLength = 0,partitionValue = 0, serialReferenceBitsLength = 0,serialReferenceDigitsLength = 0;
						
			//Get the company prefix
			//System.Int64 companyPrefixIndex = 0;
			
			//Validate the company prefix here
			//code to validate company prefix ends


			//Lookup the partition value depending on the company prefix length from the partition table
			switch(companyPrefixLength)
			{
				case 12:
					partitionValue = 0;
					companyPrefixBitsLength = 40;
					companyPrefixDigitsLength = 12;
					serialReferenceBitsLength = 18;
					serialReferenceDigitsLength = 5;
					break;
				case 11:
					partitionValue = 1;
					companyPrefixBitsLength = 37;
					companyPrefixDigitsLength = 11;
					serialReferenceBitsLength = 21;
					serialReferenceDigitsLength = 6;
					break;
				case 10:
					partitionValue = 2;
					companyPrefixBitsLength = 34;
					companyPrefixDigitsLength = 10;
					//serialReferenceBitsLength = 23;//HOLE should be 24.Refer to partitiontable
					serialReferenceBitsLength = 24 ;
					serialReferenceDigitsLength = 7;
					break;
				case 9:
					partitionValue = 3;
					companyPrefixBitsLength = 30;
					companyPrefixDigitsLength = 9;
					serialReferenceBitsLength = 28;
					serialReferenceDigitsLength = 8;
					break;
				case 8:
					partitionValue = 4;
					companyPrefixBitsLength = 27;
					companyPrefixDigitsLength = 8;
					serialReferenceBitsLength = 31;
					serialReferenceDigitsLength = 9;
					break;
				case 7:
					partitionValue = 5;
					companyPrefixBitsLength = 24;
					companyPrefixDigitsLength = 7;
					//Dhananjay fixes
					//serialReferenceBitsLength = 33; BIG HOLE .Refer to the partitiontable in the manual
					serialReferenceBitsLength= 34 ;
					serialReferenceDigitsLength = 10;
					break;
				case 6:
					partitionValue = 6;
					companyPrefixBitsLength = 20;
					companyPrefixDigitsLength = 6;
					serialReferenceBitsLength = 38;
					serialReferenceDigitsLength = 11;
					break;
				default:
					throw new InvalidCompanyPrefixLengthException("Invalid company prefix length.");
			}

			//Construct the companyPrefix
			string companyPrefix = actualSCC.Substring(1, companyPrefixDigitsLength);

			//Construct the serial reference
			StringBuilder strbSerialRefernce = new StringBuilder();

			strbSerialRefernce.Append(actualSCC.Substring(0,1));
			//strbSerialRefernce.Append(actualSCC.Substring(companyPrefixDigitsLength+1, actualSCC.Length - serialReferenceDigitsLength + 1));
			strbSerialRefernce.Append(actualSCC.Substring(companyPrefixDigitsLength+1, serialReferenceDigitsLength-1));

			//Declare the encode string
			StringBuilder strbEncodeString = new StringBuilder();
            
			//Declare the byte array
			byte[] encodedSSCC96 = new byte[12];
			
			//Assign the header value 00110001 in binary
			strbEncodeString.Append(Constants.SSCC96_HEADER);

			//Assign the filter value
			strbEncodeString.Append(RFUtils.AddReqdZeros(Convert.ToString(filterValue,2),3));

			//Assign the partition value
			strbEncodeString.Append(RFUtils.AddReqdZeros(Convert.ToString(partitionValue,2), 3));

			//Assign the company prefix
			strbEncodeString.Append(RFUtils.AddReqdZeros(Convert.ToString(Convert.ToInt64(companyPrefix),2), companyPrefixBitsLength));

			//Assign the Serial reference
			strbEncodeString.Append(RFUtils.AddReqdZeros(Convert.ToString(Convert.ToInt64(strbSerialRefernce.ToString()),2),serialReferenceBitsLength));

			//Assign the rest 25 Unallocated bits with 0s
			strbEncodeString.Append('0',24);
			
			//Insert the string values to byte array
			for(int arrIndex=0, startValue = 0; arrIndex < 12; arrIndex++, startValue+=8)
			{
				encodedSSCC96[arrIndex] = Convert.ToByte(RFUtils.ConvertBinaryToDecimal(strbEncodeString.ToString().Substring(startValue,8)));
			}

			return encodedSSCC96;
		}
		/// <summary>
		/// Method to encode SCC hex  to SSCC 96 bit encoding
		/// </summary>
		/// <param name="SCCHexValue">SCC hex value</param>
		/// <returns>A 12 byte byte array containing SSCC96</returns>
        public static byte[] EncodeSCCtoSSCC96(string SCCHexValue)
		{
			//Declare the string bulder to contain the encoded bits
			StringBuilder strbConverted = new StringBuilder();

			for(int stringIndex=0; stringIndex < SCCHexValue.Length; stringIndex++)
			{
				strbConverted.Append(RFUtils.AddReqdZeros(Convert.ToString(Convert.ToByte(RFUtils.ConvertHexToDecimal(SCCHexValue.Substring(stringIndex,1))),2),4));
			}
			
			//Declare the byte array to return
			byte[] encodedSSCC96 = new byte[12];//SGTIN96 will return this 12 bytes array

			//Insert the string values to byte array
			for(int arrIndex=0, startValue = 0; arrIndex < 12; arrIndex++, startValue+=8)
			{
				encodedSSCC96[arrIndex] = Convert.ToByte(RFUtils.ConvertBinaryToDecimal(strbConverted.ToString().Substring(startValue,8)));
			}
			return encodedSSCC96;
		}


		/// <summary>
		/// This method accepts the three components that make an 18-digit SSCC number.Note:The default filtervalue remains the same as set by any 
		/// previous overloaded method of this same method.
		/// </summary>
		/// <param name="extensiondigit">A single digit extension number</param>
		/// <param name="companyPrefix">Company prefix part of the 18 digit SCC</param>
		/// <param name="serialRef">The serial reference number</param>
		/// <returns></returns>
        public static byte[] EncodeSCCtoSSCC96(string extensionDigit, string companyPrefix, string serialRef)
		{
			byte[] encodedSSCC96 = null ;
			encodedSSCC96  =   EncodeSCCtoSSCC96(m_filterValue,extensionDigit,companyPrefix,serialRef) ;
			return encodedSSCC96 ;
		}

		/// <summary>
		/// This method encodes 18-digit SCC number into 96-bit SSCC byte array value.This method accepts individudal components
		/// of the 18-digit SCC number.
		/// </summary>
		/// <param name="filterValue">A 3-bit filter value for this SCC value</param>
		/// <param name="extensionDigit">A single digit extension number</param>
		/// <param name="companyPrefix">Company prefix part of the 18 digit SCC</param>
		/// <param name="serialRef">The serial reference number</param>
		/// <returns></returns>
        public static byte[] EncodeSCCtoSSCC96(byte filterValue, string extensionDigit, string companyPrefix, string serialRef)
		{
			byte[] encodedSSCC96 = null ;
			string actualSCC = CreateSSCC18(Convert.ToInt16(extensionDigit),companyPrefix,serialRef) ;

			encodedSSCC96 = EncodeSCCtoSSCC96(filterValue,actualSCC,companyPrefix.Length) ;
			return encodedSSCC96 ;
		}
		#endregion

		
	}
}
