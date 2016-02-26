
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
	/// This class contains methods for encoding GTIN numbers into SGTIN values
	/// </summary>
    public class EncodeGTIN
	{
		#region Attributes
		private static byte m_filterValue = Constants.FILTERVALUE ;
		#endregion


		#region Private Methods
		private static Int32 GetCompanyPrefixLength(int partitionValue)
		{
			Int32 compPrefixLenDigits = 0 ;
			switch(partitionValue)
			{
				case 0:
				{
					compPrefixLenDigits =   12 ;
					break;
				}
				case 1:
				{
					compPrefixLenDigits =   11 ;
					break;
				}
				case 2:
				{
					compPrefixLenDigits =   10 ;
					break;
				}
				case 3:
				{
					compPrefixLenDigits =   9 ;
					break;
				}
				case 4:
				{
					compPrefixLenDigits =   8 ;
					break;
				}
				case 5:
				{
					compPrefixLenDigits =   7 ;
					break;
				}
				case 6:
				{
					compPrefixLenDigits =   6 ;
					break;
				}
				default:
					throw new InvalidSGTINPartitionValue("GetCompanyPrefixLength():Entered Value: "+partitionValue) ;
			}
			return compPrefixLenDigits ;
		}


		private static int GetPartitionValue(int companyPrefixLength)
		{
			int partValue = 0 ;
			switch(companyPrefixLength)
			{
				case 6:
				{
					partValue = 6 ;
					break;
				}
				case 7:
				{
					partValue = 5 ;
					break;
				}
				case 8:
				{
					partValue = 4 ;
					break;
				}
				case 9:
				{
					partValue = 3 ;
					break;
				}
				case 10:
				{
					partValue = 2 ;
					break;
				}
				case 11:
				{
					partValue = 1 ;
					break;
				}
				case 12:
				{
					partValue = 0 ;
					break;
				}
				default:
				{
					throw new InvalidCompanyPrefixLengthException("GetPartitionValue() Value: "+companyPrefixLength) ;
				}
			}
			return partValue ;
		}

        public static string CreateGTIN14Number(string gtinIndicatorDigit, string companyPrefix, string itemReference)
		{
			if(gtinIndicatorDigit.Length > 1)
				throw new InvalidGTINIndicator("Indicator digit was found to be greater than 1!!") ;

			int gtinIndicatorDigitLength = gtinIndicatorDigit.Length ;
			int companyPrefixLength = companyPrefix.Length ;
			int itemRefExpectedLength = Constants.SGTIN14_LEN-(gtinIndicatorDigitLength+companyPrefixLength) ;
												   
			if((itemReference.Length+gtinIndicatorDigitLength) < itemRefExpectedLength)
			{
				itemReference = itemReference.PadLeft(itemRefExpectedLength-gtinIndicatorDigitLength,'0') ;
			}
			string concatedGTIN =   gtinIndicatorDigit+	companyPrefix +itemReference ;
			concatedGTIN+= RFUtils.CalculateCheckDigit(concatedGTIN,Constants.SGTIN14_LEN-1) ;
			return concatedGTIN ;
		}
		#endregion

		private EncodeGTIN()
		{}

		#region Methods to convert GTIN to 64 bit SGTIN
		
		/// <summary>
		/// Method to encode supplied GTIN to 64 bit SGTIN.
		/// The maimum number of digits of company prefix length is 6 only.Anything 
		/// after that exceeds the maximum value of company prefix to be represented in 14 bits.
		/// </summary>
		/// <param name="actualGTIN">Supplied GTIN</param>
		/// <param name="serialNo">Supplied Serial No</param>
		/// <param name="companyPrefixLength">Supplied company prefix length</param>
		/// <param name="filterValue">Supplied filter value</param>
		/// <returns>A 8 byte byte array containing SGTIN64</returns>
        public static byte[] EncodeGTIN14toSGTIN64(string actualGTIN, System.Int32 serialNo, System.Int16 companyPrefixLength, byte filterValue)
		{
			//Check for GTIN string validity
			if(actualGTIN.Length != 14 )
			{
				throw new InvalidSGTIN64EncodingException("EncodeGTIN14toSGTIN64() This is not a valid GTIN.");
			}

			//Check for serial no validity
			if(serialNo >= Math.Pow(2,25))
			{
				throw new InvalidSGTIN64EncodingException("EncodeGTIN14toSGTIN64() This is not a valid serial number for GTIN.");
			}

			//Check for filter value validity
			if(filterValue > 7)
			{
				throw new InvalidSGTIN64EncodingException("EncodeGTIN14toSGTIN64() This is not a valid filter value for GTIN");
			}

			//Check for serial no validity
			if(serialNo > 33554431)
			{
				throw new InvalidSGTIN64EncodingException("EncodeGTIN14toSGTIN64() Invalid serial number.");
			}

			//Extract the company prefix
			string companyPrefix = actualGTIN.Substring(1, companyPrefixLength);
			
			//Validate this company prefix with Company prefix translation table
			ICompanyPrefixLookup iLookup = CompanyPrefixLookupImpl.GetInstanceOf() ;
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
				throw new CompanyPrefixLookupImplException("EncodeGTIN14toSGTIN64() : Value of CompanyPrefixIndex is :"+companyPrefixIndex) ;
			}

			//Validate copany prefix
			if(Convert.ToInt32(companyPrefixIndex) > 16383)
			{
				throw new InvalidSGTIN64EncodingException("EncodeGTIN14toSGTIN64() This is not a valid company prefix value for GTIN");
			}

			//Construct the Item reference 
			StringBuilder strbItemReference = new StringBuilder();

			strbItemReference.Append(actualGTIN.Substring(0,1));
			strbItemReference.Append(actualGTIN.Substring(companyPrefixLength + 1,  (actualGTIN.Length - 1) - (companyPrefixLength+1)));

			//Validate the Item reference
			if(Convert.ToInt64(strbItemReference.ToString()) > Math.Pow(2,20))
			{
				throw new InvalidSGTIN64EncodingException("EncodeGTIN14toSGTIN64()Itemreference length greater than max limit.");
			}
			
			//Construct the main string
			StringBuilder strbEncodeString = new StringBuilder();

			//Insert the header value 10 in binary
			strbEncodeString.Append(Constants.SGTIN64_HEADER);

			//Insert the filter value
			strbEncodeString.Append(RFUtils.AddReqdZeros(Convert.ToString(filterValue,2),3));

			//Insert the CompanyPrefix Index
			strbEncodeString.Append(RFUtils.AddReqdZeros(Convert.ToString(Convert.ToUInt32(companyPrefixIndex),2),14));

			//Insert the Item reference
			Int64 itemRefInt = Convert.ToInt64(strbItemReference.ToString()) ;
			string tempItemRef = Convert.ToString((itemRefInt),2) ;
			strbEncodeString.Append(RFUtils.AddReqdZeros(tempItemRef, 20));

			//Insert the serial no
			strbEncodeString.Append(RFUtils.AddReqdZeros(Convert.ToString(serialNo,2),25));

			//Declare the byte array
			byte[] encodedSGTIN64 = new byte[8];

			
			//Insert the string values to byte array
			for(int arrIndex=0, startValue = 0; arrIndex < 8; arrIndex++, startValue+=8)
			{
				encodedSGTIN64[arrIndex] = Convert.ToByte(RFUtils.ConvertBinaryToDecimal(strbEncodeString.ToString().Substring(startValue,8)));
			}
			return encodedSGTIN64;
		}
		/// <summary>
		/// Method to encode GTIN hex string to 64 bit SGTIN
		/// </summary>
		/// <param name="GTINHexValue">GTIN hex string</param>
		/// <returns>A 8 byte byte array containing  SGTIN64</returns>
        public static byte[] EncodeGTIN14toSGTIN64(string GTINHexValue)
		{
			//Declare the string bulder to contain the encoded bits
			StringBuilder strbConverted = new StringBuilder();

			for(int stringIndex=0; stringIndex < GTINHexValue.Length; stringIndex++)
			{
				strbConverted.Append(RFUtils.AddReqdZeros(Convert.ToString(Convert.ToByte(RFUtils.ConvertHexToDecimal(GTINHexValue.Substring(stringIndex,1))),2),4));
			}
			
			//Declare the byte array to return
			byte[] encodedSGTIN64 = new byte[8];//SGTIN64 will return this 8 bytes array

			//Insert the string values to byte array
			for(int arrIndex=0, startValue = 0; arrIndex < 8; arrIndex++, startValue+=8)
			{
				encodedSGTIN64[arrIndex] = Convert.ToByte(RFUtils.ConvertBinaryToDecimal(strbConverted.ToString().Substring(startValue,8)));
			}
			return encodedSGTIN64;
		}


        public static byte[] EncodeGTIN14toSGTIN64(string indicatorDigit, string companyPrefix, string itemReference, System.Int32 serialNo, byte filterValue)
		{
			byte[] encodedSGTIN64 = null; 

			if(!EPCEncoding.ValidateCompPrefix(companyPrefix))
			{
				throw new CompanyPrefixLookupImplException("EncodeGTIN14toSGTIN64(): Exception ::Error value of Company Prefix: "+companyPrefix) ;
			}
			string actualGTIN14 = CreateGTIN14Number(indicatorDigit,companyPrefix,itemReference) ;

			encodedSGTIN64 = EncodeGTIN14toSGTIN64(actualGTIN14,serialNo,Convert.ToInt16(companyPrefix.Length),filterValue) ;

			return encodedSGTIN64;
		}


		
		#endregion

		#region Methods to convert GTIN to 96 bit SGTIN
		/// <summary>
		/// Method to encode supplied 14 digit GTIN to 96bit SGTIN.
		/// </summary>
		/// <param name="actualGTIN">supplied GTIN</param>
		/// <param name="serialNo">38 bit serial no</param>
		/// <param name="companyPrefixLength">company prefix</param>
		/// <param name="filterValue">filterValue</param>
        public static byte[] EncodeGTIN14toSGTIN96(string actualGTIN, System.Int64 serialNo, int partValue, byte filterValue)
		{

			System.Int32 companyPrefixLength = 0 ;
			try
			{
				companyPrefixLength = GetCompanyPrefixLength(partValue) ;
			}
			catch(Exception e)
			{
				throw e ;
			}
			//Check for GTIN string validity
			if(actualGTIN.Length > 14)
			{
				throw new InvalidSGTIN96EncodingException("This is not a valid GTIN.");
			}

			//Check for serial no validity
			if(serialNo >= Math.Pow(2,38))
			{
				throw new InvalidSGTIN96EncodingException("This is not a valid serial number for GTIN.");
			}

			//Check for filter value validity
			if(filterValue > 7)
			{
				throw new InvalidSGTIN96EncodingException("This is not a valid filter value for GTIN");
			}
			m_filterValue = filterValue ;
			//Get the indicator digit
			byte indicatorDigit = Convert.ToByte(actualGTIN.Substring(0,1));

			int companyPrefixBitsLength = 0, companyPrefixDigitsLength = 0,partitionValue = 0;
						
			//Get the company prefix
			System.Int64 companyPrefixIndex = 0;
			
			//Validate the company prefix here
			//code to validate company prefix ends


			//Get the itemreference
			System.Int32 itemReference = 0;
			
 
			//Lookup the partition value depending on the company prefix length from the partition table
			switch(companyPrefixLength)
			{
				case 12:
					partitionValue = 0;
					companyPrefixBitsLength = 40;
					companyPrefixDigitsLength = 12;
					break;
				case 11:
					partitionValue = 1;
					companyPrefixBitsLength = 37;
					companyPrefixDigitsLength = 11;
					break;
				case 10:
					partitionValue = 2;
					companyPrefixBitsLength = 34;
					companyPrefixDigitsLength = 10;
					break;
				case 9:
					partitionValue = 3;
					companyPrefixBitsLength = 30;
					companyPrefixDigitsLength = 9;
					break;
				case 8:
					partitionValue = 4;
					companyPrefixBitsLength = 27;
					companyPrefixDigitsLength = 8;
					break;
				case 7:
					partitionValue = 5;
					companyPrefixBitsLength = 24;
					companyPrefixDigitsLength = 7;
					break;
				case 6:
					partitionValue = 6;
					companyPrefixBitsLength = 20;
					companyPrefixDigitsLength = 6;
					break;
				default:
					throw new InvalidCompanyPrefixLengthException("Invalid company prefix length.");
			}

			companyPrefixIndex = Convert.ToInt64(actualGTIN.Substring(1, companyPrefixDigitsLength));
			StringBuilder strbItemReference = new StringBuilder();

			strbItemReference.Append(actualGTIN.Substring((companyPrefixDigitsLength + 1), ((actualGTIN.Length - 1) - (companyPrefixDigitsLength + 1))));

			itemReference = Convert.ToInt32(indicatorDigit.ToString() +  strbItemReference.ToString());

			//Get the partition value
			//System.Int16 partitionValue = 0;

			StringBuilder strbEncodeString = new StringBuilder();

			byte[] encodedSGTIN96 = new byte[12];//SGTIN96 will return this 12 bytes array
			//Header value is 00110000 in binary
			//set the first byte of the byte array with the header value
			strbEncodeString.Append(Constants.SGTIN96_HEADER);

			//Code to encode SGTIN96
			//Set the filter value
			strbEncodeString.Append(RFUtils.AddReqdZeros(Convert.ToString(filterValue,2),3));
		
			//Assign the partition value
			strbEncodeString.Append(RFUtils.AddReqdZeros(Convert.ToString(partitionValue,2),3));

			
			#region Insertion of Company Index and Item refernce number
			StringBuilder strbCompanyIndex = new StringBuilder();
			//StringBuilder strbItemReference = new StringBuilder();
			string tmpCompanyPrefix = string.Empty;
			string tmpItemReference = string.Empty;
			
			switch(companyPrefixBitsLength)
			{
				case 40 :
					//Extract the values for 40 bit company index and 4 bit Item refernce no
					tmpCompanyPrefix = RFUtils.AddReqdZeros(Convert.ToString(companyPrefixIndex,2), 40);
					strbCompanyIndex.Append(tmpCompanyPrefix);

					strbEncodeString.Append(strbCompanyIndex.ToString());
					//Insertion of 40 bits company index ends

					//Insertion of 4 bit Item refernce number
					//7th byte of byte array has 6 bits occupied
					tmpItemReference = RFUtils.AddReqdZeros(Convert.ToString(itemReference,2), 4);

					strbEncodeString.Append(tmpItemReference);
					//Insertion of 4 bit Item refernce number ends
					break;
					
				case 37:
					//Insertion of 37 bits company Index
					tmpCompanyPrefix = RFUtils.AddReqdZeros(Convert.ToString(companyPrefixIndex,2), 37);
					strbCompanyIndex.Append(tmpCompanyPrefix);

					strbEncodeString.Append(strbCompanyIndex.ToString());
					//Insertion of 37 bits company index ends

					//Insertion of 7 bit Item refernce number
					//7th byte of byte array has 3 bit occupied
					tmpItemReference = RFUtils.AddReqdZeros(Convert.ToString(itemReference,2), 7);

					strbEncodeString.Append(tmpItemReference);
					//Insertion of 7 bit Item refernce number ends
					break;
			

				case 34:
					//Insertion of 34 bits company index
					tmpCompanyPrefix =RFUtils.AddReqdZeros(Convert.ToString(companyPrefixIndex,2), 34);
					strbCompanyIndex.Append(tmpCompanyPrefix);

					strbEncodeString.Append(strbCompanyIndex.ToString());
					//Insertion of 34 bits company index ends

					//Insertion of 10 bit Item refernce number
					//7th byte of byte array has 0 bit occupied
					tmpItemReference = RFUtils.AddReqdZeros(Convert.ToString(itemReference,2), 10);

					strbEncodeString.Append(tmpItemReference);
					//Insertion of 10 bit Item refernce number ends
					break;

				case 30:
					//Insertion of 30 bits company Index
					tmpCompanyPrefix =RFUtils.AddReqdZeros(Convert.ToString(companyPrefixIndex,2), 30);
					strbCompanyIndex.Append(tmpCompanyPrefix);

					strbEncodeString.Append(strbCompanyIndex.ToString());
					//Insertion of 30 bits company index ends

					//Insertion of 14 bit Item refernce number
					//6th byte of byte array has 4 bits occupied
					tmpItemReference = RFUtils.AddReqdZeros(Convert.ToString(itemReference,2), 14);

					strbEncodeString.Append(tmpItemReference);
					//Insertion of 14 bit Item refernce number ends
					break;

				case 27:
					//Insertion of 27 bits company Index
					tmpCompanyPrefix =RFUtils.AddReqdZeros(Convert.ToString(companyPrefixIndex,2), 27);
					strbCompanyIndex.Append(tmpCompanyPrefix);

					strbEncodeString.Append(strbCompanyIndex.ToString());
					//Insertion of 27 bits company index ends

					//Insertion of 17 bit Item refernce number
					//6th byte of byte array has 1 bit occupied
					tmpItemReference = RFUtils.AddReqdZeros(Convert.ToString(itemReference,2), 17);

					strbEncodeString.Append(tmpItemReference);
					//Insertion of 17 bit Item refernce number ends
					break;

				case 24:
					//Insertion of 24 bits company index
					tmpCompanyPrefix =RFUtils.AddReqdZeros(Convert.ToString(companyPrefixIndex,2), 24);
					strbCompanyIndex.Append(tmpCompanyPrefix);

					strbEncodeString.Append(strbCompanyIndex.ToString());
					//Insertion of 24 bits company index ends

					//Insertion of 20 bit Item refernce number
					//5th byte of byte array has 6 bits occupied
					tmpItemReference = RFUtils.AddReqdZeros(Convert.ToString(itemReference,2), 20);

					strbEncodeString.Append(tmpItemReference);
					//Insertion of 20 bit Item refernce number ends
					break;

				case 20:
					//Insertion of 20 bits company index
					tmpCompanyPrefix =RFUtils.AddReqdZeros(Convert.ToString(companyPrefixIndex,2), 20);
					strbCompanyIndex.Append(tmpCompanyPrefix);

					strbEncodeString.Append(strbCompanyIndex.ToString());
					//Insertion of 20 bits company index ends
					
					//Insertion of 24 bit Item refernce number
					//5th byte of byte array has 2 bits occupied
					tmpItemReference = RFUtils.AddReqdZeros(Convert.ToString(itemReference,2), 24);

					strbEncodeString.Append(tmpItemReference);
					//Insertion of 24 bit Item refernce number ends
					break;
			}
			
			//Insertion of 38 bits Serial No
			strbEncodeString.Append(RFUtils.AddReqdZeros(Convert.ToString(serialNo,2), 38));
			//Insertion of 38 bits serial no ends

			#endregion

			//Insert the string values to byte array
			for(int arrIndex=0, startValue = 0; arrIndex < 12; arrIndex++, startValue+=8)
			{
				encodedSGTIN96[arrIndex] = Convert.ToByte(RFUtils.ConvertBinaryToDecimal(strbEncodeString.ToString().Substring(startValue,8)));
			}

			return encodedSGTIN96;
		}


		/// <summary>
		/// Creates an SGTIN byte array from just 3 input values of companyPrefix,itemReference, serialNo.
		/// Note: The header for SGTIN is "0011 0000 Binary",filter value is 3,indicator digit is 1.
		/// </summary>
		/// <param name="companyPrefix">The index into the company prefix table that points to a particular company</param>
		/// <param name="itemReference">The itemreference number for that item</param>
		/// <param name="serialNo">The unique serial number</param>
		/// <returns></returns>
        public static byte[] EncodeGTIN14toSGTIN96(string companyPrefix, string itemReference, System.Int64 serialNo)
		{
			byte[] encodedSGTIN96 = null ; 
			encodedSGTIN96 = EncodeGTIN14toSGTIN96(m_filterValue,Constants.INDICATORDIGIT,companyPrefix,itemReference,serialNo) ;
			return encodedSGTIN96;
		}
		/// <summary>
		/// Overloaded function that lets you specify indicator digit,filter value in addition to input values of companyPrefix,itemReference, serialNo 
		/// Creates a 12-byte SGTIN array.Note: The header for SGTIN is "0011 0000 Binary"
		/// </summary>
		/// <param name="filterValue">for tag filtering purposes</param>
		/// <param name="gtinIndicatorDigit">The first digit in the itemreference of SGTIN number</param>
		/// <param name="companyPrefix">The index into the company prefix table that points to a particular company</param>
		/// <param name="itemReference">The itemreference number for that item</param>
		/// <param name="serialNo">The unique serial number</param>
		/// <returns></returns>
        public static byte[] EncodeGTIN14toSGTIN96(byte filterValue, string gtinIndicatorDigit, string companyPrefix, string itemReference, System.Int64 serialNo)
		{
			byte[] encodedSGTIN96 = null ; 
			string actualGTIN = CreateGTIN14Number(gtinIndicatorDigit,companyPrefix,itemReference) ;
			if(actualGTIN.Length < 14)
				throw new InvalidGTIN14NumberException("EncodeGTIN14toSGTIN96() :Value :"+actualGTIN) ;
			int partValue  =  GetPartitionValue(companyPrefix.Length) ;
			encodedSGTIN96 =  EncodeGTIN14toSGTIN96(actualGTIN,serialNo,partValue,filterValue) ;
			return encodedSGTIN96;
		}
		/// <summary>
		/// Method to encode the supplied GTIN in Hex format to SGTIN96
		/// </summary>
		/// <param name="GTINHexValue">The hex string</param>
		/// <returns>A 12 byte byte array containing the encoded string</returns>
        public static byte[] EncodeGTIN14toSGTIN96(string GTINHexValue)
		{
			//Check for valid GTNHex string
			if(GTINHexValue.Length != 24)
			{
				throw new GTINStringTooLargeException("GTIN string is not of correct size.");
			}
			//Declare the string bulder to contain the encoded bits
			StringBuilder strbConverted = new StringBuilder();

			for(int stringIndex=0; stringIndex < GTINHexValue.Length; stringIndex++)
			{
				strbConverted.Append(RFUtils.AddReqdZeros(Convert.ToString(Convert.ToByte(RFUtils.ConvertHexToDecimal(GTINHexValue.Substring(stringIndex,1))),2),4));
			}
			
			//Declare the byte array to return
			byte[] encodedSGTIN96 = new byte[12];//SGTIN96 will return this 12 bytes array

			//Insert the string values to byte array
			for(int arrIndex=0, startValue = 0; arrIndex < 12; arrIndex++, startValue+=8)
			{
				encodedSGTIN96[arrIndex] = Convert.ToByte(RFUtils.ConvertBinaryToDecimal(strbConverted.ToString().Substring(startValue,8)));
			}
			return encodedSGTIN96;
		}
		#endregion

		

	}
}
