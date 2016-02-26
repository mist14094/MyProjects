
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
	/// Summary description for EncodeGLN.
	/// </summary>
	public class EncodeGLN
	{
		#region Private Methods

        public static string CreateGLN13(string companyPrefix, string locationRef)
		{
			string strGLN = string.Empty ;
			int companyPrefixLength = companyPrefix.Length ;
			int locationRefLength = locationRef.Length ;
			if((companyPrefixLength+locationRefLength) > 12)
			{
				throw new EPCTagExceptionBase("CreateGLN13(): GLN number more than 12 digits. (without the checkdigit)");
			}
			if(companyPrefixLength == Constants.SGLN13_LEN-1)
			{
				strGLN = companyPrefix ;
			}
			else
			{
				int locationRefExpectedLength = 12-(companyPrefixLength) ;

				if(locationRef.Length < locationRefExpectedLength)
				{
					locationRef = locationRef.PadLeft(locationRefExpectedLength,'0') ;
				}
				strGLN =   companyPrefix +locationRef ;
			}
			strGLN+= RFUtils.CalculateCheckDigit(strGLN,Constants.SGLN13_LEN) ;
			return strGLN ;
		}
		
		#endregion Private methods ENDS

		private EncodeGLN()
		{}
		#region Method to Encode GLN to SGLN64
		/// <summary>
		/// Method to convert GLN to SGLN64
		/// </summary>
		/// <param name="actualGLN">Supplied GLN string</param>
		/// <param name="companyPrefixLength">Supplied Company Prefix Index Length</param>
		/// <param name="serialNo">Supplied serial no</param>
		/// <param name="filterValue">Supplied filter value</param>
		/// <returns>A 8 byte byte array containing serialized GLN64</returns>
        public static byte[] EncodeGLNtoSGLN64(string actualGLN, Int32 companyPrefixLength, Int32 serialNo, byte filterValue)
		{
			//Check for validity of the GLN
			if(actualGLN.Length != 13 )
			{
				throw new InvalidSGLN64EncodingException("This input string cannot be converted to valid SGLN64 string.");
			}
			//Check for valid serial no
			if(serialNo >= Math.Pow(2, 19))
			{
				throw new InvalidSGLN64EncodingException("The serial no is invalid.");
			}

			//Declare the main string to hold the values
			StringBuilder strbEncodeString = new StringBuilder();

			//For SGLN64 the header value is 00001001 in binary
			strbEncodeString.Append(Constants.SGLN64_HEADER);

			//Insert the filter value
			strbEncodeString.Append(RFUtils.AddReqdZeros(Convert.ToString(filterValue,2), 3));

            //Check the company prefix translation lookup table for company prefix index
			//string companyPrefix = String.Empty; //This will be filled up by the value from the company prefix translation table
			string companyPrefix = actualGLN.Substring(0,companyPrefixLength) ;

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

			
			if(companyPrefixIndex == 0)
			{
				throw new CompanyPrefixLookupImplException("EncodeGLNtoSGLN64() : Value of CompanyPrefixIndex is :"+companyPrefixIndex) ;
			}

			//Validate copany prefix
			if(Convert.ToInt32(companyPrefixIndex) > 16383)
			{
				throw new InvalidSGLN64EncodingException("EncodeGLNtoSGLN64() This is not a valid company prefix value for GTIN");
			}

			strbEncodeString.Append(RFUtils.AddReqdZeros(Convert.ToString(Convert.ToUInt32(companyPrefixIndex),2),14));

			string locationReference = string.Empty ;
			try
			{
				//Construct the 20 bits location reference
				if(companyPrefixLength != 12) //if length =12 dont enter this if loop.
				{
					locationReference = actualGLN.Substring(companyPrefixLength,12 - companyPrefixLength);
				}
				else
					locationReference = "0";
			}
			catch(Exception e)
			{
				throw e ;
			}

//			if(locationReference == string.Empty)
//			{
//				throw new InvalidSGLN64EncodingException("EncodeGLNtoSGLN64() : locationReference Invalid Value : "+locationReference) ;
//			}

			//Check for valid location reference
			if(Convert.ToUInt32(locationReference) >= Math.Pow(2,20))
			{
				throw new InvalidSGLN64EncodingException("EncodeGLNtoSGLN64() : LocationReference value exceeds limits");
			}

			strbEncodeString.Append(RFUtils.AddReqdZeros(Convert.ToString(Convert.ToUInt32(locationReference),2),20));

			//Extract the 19 bits serial no
			strbEncodeString.Append(RFUtils.AddReqdZeros(Convert.ToString(Convert.ToUInt32(serialNo),2),19));

			//Declare the byte array
			byte[] encodedSGLN64 = new byte[8];
			
			//Insert the string values to byte array
			for(int arrIndex=0, startValue = 0; arrIndex < 8; arrIndex++, startValue+=8)
			{
				encodedSGLN64[arrIndex] = Convert.ToByte(RFUtils.ConvertBinaryToDecimal(strbEncodeString.ToString().Substring(startValue,8)));
			}
			return encodedSGLN64;

		}

		/// <summary>
		/// Method to serialize GLN hex string to 64 bit SGLN
		/// </summary>
		/// <param name="GLNHexValue">GLN hex string</param>
		/// <returns>A 8 byte byte array containing  SGLN64</returns>
        public static byte[] EncodeGLNtoSGLN64(string GLNHexValue)
		{
			//Declare the string bulder to contain the encoded bits
			StringBuilder strbConverted = new StringBuilder();

			for(int stringIndex=0; stringIndex < GLNHexValue.Length; stringIndex++)
			{
				strbConverted.Append(RFUtils.AddReqdZeros(Convert.ToString(Convert.ToByte(RFUtils.ConvertHexToDecimal(GLNHexValue.Substring(stringIndex,1))),2),4));
			}
			
			//Declare the byte array to return
			byte[] encodedSGLN64 = new byte[8];//SGLN64 will return this 8 bytes array

			//Insert the string values to byte array
			for(int arrIndex=0, startValue = 0; arrIndex < 8; arrIndex++, startValue+=8)
			{
				encodedSGLN64[arrIndex] = Convert.ToByte(RFUtils.ConvertBinaryToDecimal(strbConverted.ToString().Substring(startValue,8)));
			}
			return encodedSGLN64;
		}


        public static byte[] EncodeGLNtoSGLN64(byte filterValue, string companyPrefix, string locationRef, Int32 serialNo)
		{
			byte[] encodedSGLN64 = null ;
 
			string actualGLN = CreateGLN13(companyPrefix,locationRef) ;

			encodedSGLN64  =  EncodeGLNtoSGLN64(actualGLN,companyPrefix.Length,serialNo,filterValue) ;

			return encodedSGLN64;
		}

		#endregion

		#region Method to Encode GLN to SGLN96
		/// <summary>
		/// Method to convert GLN to SGLN96
		/// </summary>
		/// <param name="actualGLN">Supplied GLN string</param>
		/// <param name="companyPrefixLength">Supplied Company Prefix Index Length</param>
		/// <param name="serialNo">Supplied serial no</param>
		/// <param name="filterValue">Supplied filter value</param>
		/// <returns>A 12 byte byte array containing serialized GLN96</returns>
        public static byte[] EncodeGLNtoSGLN96(byte filterValue, string actualGLN, Int32 companyPrefixLength, Int64 serialNo)
		{
			//Check for GNL validity
			if(actualGLN.Length != 13)
			{
				throw new InvalidSGLN96EncodingException("Invalid GLN string.");
			}
			
			//Check for serial no validity
			if(serialNo < 0 || serialNo >= Math.Pow(2, 41))
			{
				throw new InvalidSGLN96EncodingException("Invalid serial no.");
			}
			
			//Check for filter value validity
			if(filterValue <0 || filterValue > 7)
			{
				throw new InvalidSGLN96EncodingException("Invalid filter value.");
			}

			int companyPrefixBitsLength = 0, companyPrefixDigitsLength = 0,partitionValue = 0, locationReferenceBitsLength = 0;
			//Lookup the partition value depending on the company prefix length from the partition table
			switch(companyPrefixLength)
			{
				case 12:
					partitionValue = 0;
					companyPrefixBitsLength = 40;
					companyPrefixDigitsLength = 12;
					locationReferenceBitsLength = 1;
					break;
				case 11:
					partitionValue = 1;
					companyPrefixBitsLength = 37;
					companyPrefixDigitsLength = 11;
					locationReferenceBitsLength = 4;
					break;
				case 10:
					partitionValue = 2;
					companyPrefixBitsLength = 34;
					companyPrefixDigitsLength = 10;
					locationReferenceBitsLength = 7;
					break;
				case 9:
					partitionValue = 3;
					companyPrefixBitsLength = 30;
					companyPrefixDigitsLength = 9;
					locationReferenceBitsLength = 11;
					break;
				case 8:
					partitionValue = 4;
					companyPrefixBitsLength = 27;
					companyPrefixDigitsLength = 8;
					locationReferenceBitsLength = 14;
					break;
				case 7:
					partitionValue = 5;
					companyPrefixBitsLength = 24;
					companyPrefixDigitsLength = 7;
					locationReferenceBitsLength = 17;
					break;
				case 6:
					partitionValue = 6;
					companyPrefixBitsLength = 20;
					companyPrefixDigitsLength = 6;
					locationReferenceBitsLength = 21;
					break;
				default:
					throw new InvalidCompanyPrefixLengthException("Invalid company prefix length.");
			}
			
			//Declare the main encode string
			StringBuilder strbEncodeString = new StringBuilder();

			//Add the header value
			//Header value is 00110010 in binary
			strbEncodeString.Append(Constants.SGLN96_HEADER);

			//filter value
			strbEncodeString.Append(RFUtils.AddReqdZeros(Convert.ToString(filterValue,2), 3));

			//Partition value
			strbEncodeString.Append(RFUtils.AddReqdZeros(Convert.ToString(partitionValue,2),3));
			
			//Xtrcat the company prefix
			string companyPrefix = RFUtils.AddReqdZeros(Convert.ToString(Convert.ToInt64(actualGLN.Substring(0, companyPrefixDigitsLength)),2), companyPrefixBitsLength);

			strbEncodeString.Append(companyPrefix);

			//Construct the location reference
			string locationReference = string.Empty ;

			if(companyPrefixDigitsLength == 12)
			{
				locationReference = "0";
			}
			else
				locationReference = RFUtils.AddReqdZeros(Convert.ToString(Convert.ToInt64(actualGLN.Substring(companyPrefixDigitsLength , 12 - companyPrefixDigitsLength)),2), locationReferenceBitsLength);

				strbEncodeString.Append(locationReference);

			//Serial No
			strbEncodeString.Append(RFUtils.AddReqdZeros(Convert.ToString(serialNo,2),41));

			//Declare the byte array
			byte[] encodedSGLN96 = new byte[12];
			//Insert the string values to byte array
			for(int arrIndex=0, startValue = 0; arrIndex < 12; arrIndex++, startValue+=8)
			{
				encodedSGLN96[arrIndex] = Convert.ToByte(RFUtils.ConvertBinaryToDecimal(strbEncodeString.ToString().Substring(startValue,8)));
			}
			return encodedSGLN96;
		}

		/// <summary>
		/// Method to convert GLN hex to SGLN96
		/// </summary>
		/// <param name="GLNHexValue">GLN hex value</param>
		/// <returns>A 12 byte byte array containing serialized GLN96</returns>
        public static byte[] EncodeGLNtoSGLN96(string GLNHexValue)
		{
			//Declare the string bulder to contain the encoded bits
			StringBuilder strbConverted = new StringBuilder();

			for(int stringIndex=0; stringIndex < GLNHexValue.Length; stringIndex++)
			{
				strbConverted.Append(RFUtils.AddReqdZeros(Convert.ToString(Convert.ToByte(RFUtils.ConvertHexToDecimal(GLNHexValue.Substring(stringIndex,1))),2),4));
			}
			
			//Declare the byte array to return
			byte[] encodedSGLN96 = new byte[12];//SGLN96 will return this 12 bytes array

			//Insert the string values to byte array
			for(int arrIndex=0, startValue = 0; arrIndex < 12; arrIndex++, startValue+=8)
			{
				encodedSGLN96[arrIndex] = Convert.ToByte(RFUtils.ConvertBinaryToDecimal(strbConverted.ToString().Substring(startValue,8)));
			}
			return encodedSGLN96;
		}


        public static byte[] EncodeGLNtoSGLN96(byte filterValue, string companyPrefix, string locationRef, Int32 companyPrefixLength, Int64 serialNo)
		{
			//Declare the byte array to return
			byte[] encodedSGLN96 = null ;
 
			string actualGLN = CreateGLN13(companyPrefix,locationRef) ;
			encodedSGLN96 =  EncodeGLNtoSGLN96(filterValue,actualGLN,companyPrefixLength,serialNo) ;
			return encodedSGLN96;
		}

        public static byte[] EncodeGLNtoSGLN96(byte filterValue, string companyPrefix, string locationRef, Int64 serialNo)
		{
			//Declare the byte array to return
			byte[] encodedSGLN96 = null ;
			encodedSGLN96 =  EncodeGLNtoSGLN96(filterValue,companyPrefix,locationRef,companyPrefix.Length,serialNo) ;
			return encodedSGLN96;
		}

		#endregion

		#region TestCases
		
		#region TestCases for SGLN64

		#endregion

		

		#endregion
	}
}
