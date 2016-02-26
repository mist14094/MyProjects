
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
	/// Summary description for EncodeGRAI.
	/// </summary>
    public class EncodeGRAI
	{
		#region Private Methods

        public static string CreateGRAI(string companyPrefix, string assetType)
		{
			
			/// Reverted the changes from latest version to this one that adds 
            /// ly the "0" digit
			string strGRAI = string.Empty ;
			string tempCompPrefix = "0"+companyPrefix ;
		
			int companyPrefixLen = companyPrefix.Length ;

			if(companyPrefixLen < 6)
			{
				throw new InvalidCompanyPrefixException("CreateGRAI(): invalid length of company prefix.Value : "+companyPrefixLen) ;
			}

			companyPrefix = tempCompPrefix ;
			companyPrefixLen = companyPrefix.Length ; //new company prefix length includes 0 length
			int assetTypeLen = Constants.GRAI_LEN-companyPrefixLen ;
			
			if(assetType.Length < assetTypeLen)
			{
				assetType = assetType.PadLeft(assetTypeLen,'0'); 
			}
			strGRAI = companyPrefix + assetType ; //0 is standard prefix.
			strGRAI+=RFUtils.CalculateCheckDigit(strGRAI,Constants.GRAI_LEN-1) ;
			return strGRAI ;
		}
		#endregion Private Methods ENDS

		
		
		#region Public Methods

		private EncodeGRAI()
		{}

		
		#region Method to Encode GRAI to GRAI64
		/// <summary>
		/// Method to Encode GRAI to GRAI64
		/// </summary>
		/// <param name="actualGRAI">Supplied GRAI string</param>
		/// <param name="companyPrefixLength">Supplied company prefix length</param>
		/// <param name="filterValue">Supplied filter value</param>
		/// <returns>A 8 byte byte array containing GRAI64</returns>
        public static byte[] EncodeGRAItoGRAI64(byte filterValue, string actualGRAI, Int32 companyPrefixLength)
		{
			//Check for GRAI string validity
			if(actualGRAI.Length< 15 || actualGRAI.Length > 30)
			{
				throw new InvalidGRAI64EncodingException("GRAI string should be of length between 15 to 30 digits");
			}
			if(!actualGRAI.Substring(0,1).Equals("0"))
			{
				throw new InvalidGRAI64EncodingException("Invalid GRAI string.");
			}

			//Check for filter value validity
			if(filterValue < 0 || filterValue > 7)
			{
				throw new InvalidGRAI64EncodingException("Invalid filter value for GRAI64.");
			}

			//xtract the company prefix
			string companyPrefix = actualGRAI.Substring(1,companyPrefixLength);

			//Do a reverse lookup in the company prefix translation table to check the validity of this field
			//Validate this with company prefix translation table lookup
			ICompanyPrefixLookup iLookup = CompanyPrefixLookupImpl.GetInstanceOf()  ;
			UInt16 companyPrefixIndex = 0 ;
			
			try
			{
				bool isValid =  iLookup.Lookup(companyPrefix,out companyPrefixIndex) ;

				if(!isValid)
					throw new CompanyPrefixLookupImplException("EncodeGRAItoGRAI64() : Company Prefix not listed in Translation Table : Value of Comp Prefix :"+companyPrefix) ;
			}
			catch(CompanyPrefixLookupImplException e)
			{
				throw e ;
			}

			if(companyPrefixIndex == 0)
			{
				throw new CompanyPrefixLookupImplException("EncodeGRAItoGRAI64() : Value of CompanyPrefixIndex is :"+companyPrefixIndex) ;
			}

			//Validate copany prefix
			if(Convert.ToInt32(companyPrefixIndex) > 16383)
			{
				throw new InvalidGRAI64EncodingException("EncodeGRAItoGRAI64() This is not a valid company prefix value for GRAI");
			}

			//Construct the 20 bits asset type reference
			string assetTypeReference = actualGRAI.Substring(companyPrefixLength +1,13-(companyPrefixLength+1));
			string tempAsseTypeReference = "0";
			//Check validity for asset type
				if(assetTypeReference!=string.Empty) //checking done only when ther is any value.
				{
					if(Convert.ToInt32(assetTypeReference) >= Math.Pow(2,20))
					{
						throw new InvalidGRAI64EncodingException("assetTypeReference value exceeds limits : Error Value: "+assetTypeReference);
					}
				}
				else
					assetTypeReference = tempAsseTypeReference;


			//Construct the Serial no
			string serialNo = actualGRAI.Substring(14,(actualGRAI.Length - 14));
	
			//Check serial no validity
			if(Convert.ToInt32(serialNo) >= Math.Pow(2, 19))
			{
				throw new InvalidGRAI64EncodingException("serialNo value exceeds maximum limits :Error Value "+serialNo);
			}

			if((serialNo.Length >15) && (serialNo.Substring(0,1).Equals("0")))
			{
				throw new InvalidGRAI64EncodingException("this GRAI cannot be encoded in the GRAI-64 encoding(because leading zeros are not permitted except in the case where the Serial Number consists of a single zero digit).");
			}
			
			//Declare the mail encode string
			StringBuilder strbEncodedString = new StringBuilder();
			
			//Insert the header
			//Header for GRAI64 is 00001010 in binary
			strbEncodedString.Append(Constants.GRAI64_HEADER);

			//Insert the filter value
			strbEncodedString.Append(RFUtils.AddReqdZeros(Convert.ToString(filterValue,2),3));

			strbEncodedString.Append(RFUtils.AddReqdZeros(Convert.ToString(companyPrefixIndex,2),14)); //insert the company prefix Index

			strbEncodedString.Append(RFUtils.AddReqdZeros(Convert.ToString(Convert.ToUInt32(assetTypeReference),2),20)); //Insert the asset type reference

			strbEncodedString.Append(RFUtils.AddReqdZeros(Convert.ToString(Convert.ToUInt32(serialNo),2),19));//Insert the serial no

			//Declare the byte array
			byte[] encodedGRAI64 = new byte[8];

			//Insert the string values to byte array
			for(int arrIndex=0, startValue = 0; arrIndex < 8; arrIndex++, startValue+=8)
			{
				encodedGRAI64[arrIndex] = Convert.ToByte(RFUtils.ConvertBinaryToDecimal(strbEncodedString.ToString().Substring(startValue,8)));
			}

			return encodedGRAI64;
		}

		/// <summary>
		/// Method to serialize GRAI hex string to 64 bit GRAI
		/// </summary>
		/// <param name="GRAIHexValue">GRAI hex string</param>
		/// <returns>A 8 byte byte array containing  GRAI64</returns>
        public static byte[] EncodeGRAItoGRAI64(string GRAIHexValue)
		{
			//Declare the string bulder to contain the encoded bits
			StringBuilder strbConverted = new StringBuilder();

			for(int stringIndex=0; stringIndex < GRAIHexValue.Length; stringIndex++)
			{
				strbConverted.Append(RFUtils.AddReqdZeros(Convert.ToString(Convert.ToByte(RFUtils.ConvertHexToDecimal(GRAIHexValue.Substring(stringIndex,1))),2),4));
			}
			
			//Declare the byte array to return
			byte[] encodedGRAI64 = new byte[8];//GRAI64 will return this 8 bytes array

			//Insert the string values to byte array
			for(int arrIndex=0, startValue = 0; arrIndex < 8; arrIndex++, startValue+=8)
			{
				encodedGRAI64[arrIndex] = Convert.ToByte(RFUtils.ConvertBinaryToDecimal(strbConverted.ToString().Substring(startValue,8)));
			}
			return encodedGRAI64;
		}


        public static byte[] EncodeGRAItoGRAI64(byte filterValue, string companyPrefix, string assetType, string serialNo)
		{
			byte[] encodedGRAI64 = null ;
			try
			{
				string actualGRAI = CreateGRAI(companyPrefix,assetType) ;
				actualGRAI+=serialNo ;
				encodedGRAI64 = EncodeGRAItoGRAI64(filterValue,actualGRAI,companyPrefix.Length) ;
			}
			catch(EPCTagExceptionBase e)
			{
				throw e ;
			}
			return encodedGRAI64 ;
		}


		#endregion

		#region Method to Encode GRAI to GRAI96
		/// <summary>
		/// Method to Encode GRAI to GRAI96
		/// </summary>
		/// <param name="actualGRAI">Supplied GRAI string</param>
		/// <param name="companyPrefixLength">Supplied company prefix length</param>
		/// <param name="filterValue">Supplied filter value</param>
		/// <returns>A 12 byte byte array containing GRAI96</returns>
        public static byte[] EncodeGRAItoGRAI96(byte filterValue, string actualGRAI, Int32 companyPrefixLength)
		{
			//Check for GRAI string validity
			if(actualGRAI.Length< 15 || actualGRAI.Length > 30)
			{
				throw new InvalidGRAI96EncodingException("GRAI string should be of length between 15 to 30 digits");
			}
			if(!actualGRAI.Substring(0,1).Equals("0"))
			{
				throw new InvalidGRAI96EncodingException("Invalid GRAI string.");
			}

			//Check for filter value validity
			if(filterValue < 0 || filterValue > 7)
			{
				throw new InvalidGRAI96EncodingException("Invalid filter value for GRAI64.");
			}
		
			int  companyPrefixBitsLength = 0,companyPrefixDigitsLength = 0,partitionValue = 0, assetTypeDigitsLength =0,assetTypeBitsLength = 0;
			//Lookup the partition value depending on the company prefix length from the partition table
			switch(companyPrefixLength)
			{
				case 12:
					partitionValue = 0;
					companyPrefixBitsLength = 40;
					companyPrefixDigitsLength = 12;
					assetTypeBitsLength = 4;
					assetTypeDigitsLength = 0;
					break;
				case 11:
					partitionValue = 1;
					companyPrefixBitsLength = 37;
					companyPrefixDigitsLength = 11;
					assetTypeBitsLength = 7;
					assetTypeDigitsLength = 1;
					break;
				case 10:
					partitionValue = 2;
					companyPrefixBitsLength = 34;
					companyPrefixDigitsLength = 10;
					assetTypeBitsLength = 10;
					assetTypeDigitsLength = 2;
					break;
				case 9:
					partitionValue = 3;
					companyPrefixBitsLength = 30;
					companyPrefixDigitsLength = 9;
					assetTypeBitsLength = 14;
					assetTypeDigitsLength = 3;
					break;
				case 8:
					partitionValue = 4;
					companyPrefixBitsLength = 27;
					companyPrefixDigitsLength = 8;
					assetTypeBitsLength = 17;
					assetTypeDigitsLength = 4;
					break;
				case 7:
					partitionValue = 5;
					companyPrefixBitsLength = 24;
					companyPrefixDigitsLength = 7;
					assetTypeBitsLength = 20;
					assetTypeDigitsLength = 5;
					break;
				case 6:
					partitionValue = 6;
					companyPrefixBitsLength = 20;
					companyPrefixDigitsLength = 6;
					assetTypeBitsLength = 24;
					assetTypeDigitsLength = 6;
					break;
				default:
					throw new InvalidCompanyPrefixLengthException("Invalid company prefix length.");
			}

			//Construct the company prefix
			string tempCompPrefix = actualGRAI.Substring(1,companyPrefixDigitsLength) ;
			string companyPrefix = Convert.ToString(Convert.ToInt64(tempCompPrefix),2);
			
			string tempAssetRefType = "0"; 

			if(assetTypeDigitsLength != 0)
			{
				//Construct the asset reference
				tempAssetRefType = actualGRAI.Substring(companyPrefixDigitsLength + 1,assetTypeDigitsLength) ;
			}

			string assetTypeReference = Convert.ToString(Convert.ToInt64(tempAssetRefType),2);
			//Construct the serial no
			string serialNo = actualGRAI.Substring(14);
			
			//Check serial no validity
			if(Convert.ToInt64(serialNo) >= Math.Pow(2, 38))
			{
				throw new InvalidGRAI96EncodingException("this GRAI cannot be encoded in the GRAI-96 encoding.");
			}
			if(serialNo.Length >15 && serialNo.Substring(0,1).Equals("0"))
			{
				throw new InvalidGRAI96EncodingException("this GRAI cannot be encoded in the GRAI-96 encoding(because leading zeros are not permitted except in the case where the Serial Number consists of a single zero digit).");
			}

			//Declare the main encode string
			StringBuilder strbEncodedString = new StringBuilder();

			//Header for GRAI96 is 00110011 in binary
			strbEncodedString.Append(Constants.GRAI96_HEADER);

			//Add the filter value
			strbEncodedString.Append(RFUtils.AddReqdZeros(Convert.ToString(filterValue,2),3));

			//Add the partition value
			strbEncodedString.Append(RFUtils.AddReqdZeros(Convert.ToString(partitionValue,2),3));

			//Add the company prefix
			strbEncodedString.Append(RFUtils.AddReqdZeros(companyPrefix,companyPrefixBitsLength));

			//Add the asset type reference
			strbEncodedString.Append(RFUtils.AddReqdZeros(assetTypeReference,assetTypeBitsLength));

			//Add the serial no
			strbEncodedString.Append(RFUtils.AddReqdZeros(Convert.ToString(Convert.ToInt64(serialNo),2),38));

			//Declare the byte array
			byte[] encodedGRAI96 = new byte[12];
			
			//Insert the string values to byte array
			for(int arrIndex=0, startValue = 0; arrIndex < 12; arrIndex++, startValue+=8)
			{
				encodedGRAI96[arrIndex] = Convert.ToByte(RFUtils.ConvertBinaryToDecimal(strbEncodedString.ToString().Substring(startValue,8)));
			}			
			return encodedGRAI96;
		}

		/// <summary>
		/// Method to serialize GRAI hex string to 96bit GRAI
		/// </summary>
		/// <param name="GRAIHexValue">GRAI hex string</param>
		/// <returns>A 12 byte byte array containing  GRAI96</returns>
        public static byte[] EncodeGRAItoGRAI96(string GRAIHexValue)
		{
			//Declare the string bulder to contain the encoded bits
			StringBuilder strbConverted = new StringBuilder();

			for(int stringIndex=0; stringIndex < GRAIHexValue.Length; stringIndex++)
			{
				strbConverted.Append(RFUtils.AddReqdZeros(Convert.ToString(Convert.ToByte(RFUtils.ConvertHexToDecimal(GRAIHexValue.Substring(stringIndex,1))),2),4));
			}
			
			//Declare the byte array to return
			byte[] encodedGRAI96 = new byte[12];//GRAI96 will return this 12 bytes array

			//Insert the string values to byte array
			for(int arrIndex=0, startValue = 0; arrIndex < 12; arrIndex++, startValue+=8)
			{
				encodedGRAI96[arrIndex] = Convert.ToByte(RFUtils.ConvertBinaryToDecimal(strbConverted.ToString().Substring(startValue,8)));
			}
			return encodedGRAI96;
		}

        public static byte[] EncodeGRAItoGRAI96(byte filterValue, string companyPrefix, string assetType, string serialNo)
		{
			byte[] encodedGRAI96 = null ;
			
			try
			{
				string actualGRAI = CreateGRAI(companyPrefix,assetType) ;
				actualGRAI+=serialNo ;
				encodedGRAI96 = EncodeGRAItoGRAI96(filterValue,actualGRAI,companyPrefix.Length) ;
			}
			catch(EPCTagExceptionBase e)
			{
				throw e ;
			}

			return encodedGRAI96;
		}

		#endregion

		#endregion Public Methods	ENDS
	}
}
