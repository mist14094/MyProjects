
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
	/// Summary description for EncodeGIAI.
	/// </summary>
    public class EncodeGIAI
	{
		#region Public Methods

		private EncodeGIAI()
		{}

		#region Method to Encode GIAI to GIAI64
		/// <summary>
		/// Method to Encode GIAI to GIAI 64
		/// </summary>
		/// <param name="actualGIAI">Supplied GIAI string</param>
		/// <param name="companyPrefixIndexLength">Supplied Company prefix index length</param>
		/// <param name="filterValue">Supplied Filter value</param>
		/// <returns>A 8 byte byte array containing serialized GIAI64 value</returns>
        public static byte[] EncodeGIAItoGIAI64(string actualGIAI, Int32 companyPrefixIndexLength, byte filterValue)
		{
			//Check for GIAI string validity
			if(actualGIAI.Length > 30)
			{
				throw new GIAIStringTooLargeException("Invalid GIAI input string.");
			}

			//Check for filter value validity
			if(filterValue > 7)
			{
				throw new InvalidGIAI64EncodingException("Invalid filter value.");
			}

			//Xtract the company prefix 
			string companyPrefix = actualGIAI.Substring(0, companyPrefixIndexLength);

			//Validate this with company prefix translation table lookup
			ICompanyPrefixLookup iLookup = CompanyPrefixLookupImpl.GetInstanceOf()  ;
			UInt16 companyPrefixIndex = 0 ;
			
			try
			{
				bool isValid =  iLookup.Lookup(companyPrefix,out companyPrefixIndex) ;

				if(!isValid)
					throw new CompanyPrefixLookupImplException("EncodeGIAItoGIAI64() : Company Prefix not listed in Translation Table : Value of Comp Prefix :"+companyPrefix) ;
			}
			catch(CompanyPrefixLookupImplException e)
			{
				throw e ;
			}

			if(companyPrefixIndex == 0)
			{
				throw new CompanyPrefixLookupImplException("EncodeGIAItoGIAI64() : Value of CompanyPrefixIndex is :"+companyPrefixIndex) ;
			}
			//Validate copany prefix
			if(Convert.ToInt32(companyPrefixIndex) > 16383)
			{
				throw new InvalidSGLN64EncodingException("EncodeGLNtoSGLN64() This is not a valid company prefix value for GTIN");
			}

			//Construct the individual asset reference
			string individualAssetReference = actualGIAI.Substring(companyPrefixIndexLength);

			//Check for valid individual Asset reference
			if(Convert.ToInt64(individualAssetReference) >= Math.Pow(2,39))
			{
				throw new InvalidGIAI64EncodingException("individualAssetReference of GIAI exceeds limits.Error Value :"+individualAssetReference);
			}
			if(actualGIAI.Length > (companyPrefixIndexLength + 1) && actualGIAI.Substring(companyPrefixIndexLength,1).Equals("0"))
			{
				throw new InvalidGIAI64EncodingException("this GIAI cannot be encoded in the GIAI-64 encoding (because leading zeros are not permitted except in the case where the Individual Asset Reference consists of a single zero digit).");
			}

			//Declare the main encoded string
			StringBuilder strbEncodedString = new StringBuilder();

			//Header value for GIAI64 is 00001011 in binary
			strbEncodedString.Append(Constants.GIAI64_HEADER);

			//Insert the filter value
			strbEncodedString.Append(RFUtils.AddReqdZeros(Convert.ToString(filterValue,2),3));

			//Insert the company Prefix Index
			strbEncodedString.Append(RFUtils.AddReqdZeros(Convert.ToString(Convert.ToUInt32(companyPrefixIndex),2),14));

			//Insert the Individual asset reference
			strbEncodedString.Append(RFUtils.AddReqdZeros(Convert.ToString(Convert.ToInt64(individualAssetReference),2),39));

			//Declare the encoded byte array
			byte[] encodedGIAI64 = new byte[8];
			//Insert the string values to byte array
			for(int arrIndex=0, startValue = 0; arrIndex < 8; arrIndex++, startValue+=8)
			{
				encodedGIAI64[arrIndex] = Convert.ToByte(RFUtils.ConvertBinaryToDecimal(strbEncodedString.ToString().Substring(startValue,8)));
			}			
			return encodedGIAI64;
		}

		/// <summary>
		/// Method to serialize GIAI hex string to 64 bit GIAI
		/// </summary>
		/// <param name="GIAIHexValue">GIAI hex string</param>
		/// <returns>A 8 byte byte array containing  GIAI64</returns>
        public static byte[] EncodeGIAItoGIAI64(string GIAIHexValue)
		{
			//Declare the string bulder to contain the encoded bits
			StringBuilder strbConverted = new StringBuilder();

			for(int stringIndex=0; stringIndex < GIAIHexValue.Length; stringIndex++)
			{
				strbConverted.Append(RFUtils.AddReqdZeros(Convert.ToString(Convert.ToByte(RFUtils.ConvertHexToDecimal(GIAIHexValue.Substring(stringIndex,1))),2),4));
			}
			
			//Declare the byte array to return
			byte[] encodedGIAI64 = new byte[8];//GIAI64 will return this 8 bytes array

			//Insert the string values to byte array
			for(int arrIndex=0, startValue = 0; arrIndex < 8; arrIndex++, startValue+=8)
			{
				encodedGIAI64[arrIndex] = Convert.ToByte(RFUtils.ConvertBinaryToDecimal(strbConverted.ToString().Substring(startValue,8)));
			}
			return encodedGIAI64;
		}


        public static byte[] EncodeGIAItoGIAI64(string companyPrefix, string assetRef, byte filterValue)
		{
			byte[] encodedGIAI64 = null ;
			string strGIAI = CreateGIAI(companyPrefix,assetRef) ;
			encodedGIAI64 = EncodeGIAItoGIAI64(strGIAI,companyPrefix.Length,filterValue) ;
			return encodedGIAI64;
		}

		#endregion

		#region Method to Encode GIAI to GIAI96
		/// <summary>
		/// Method to Encode GIAI to GIAI 96
		/// </summary>
		/// <param name="actualGIAI">Supplied GIAI string</param>
		/// <param name="companyPrefixIndexLength">Supplied Company prefix index length</param>
		/// <param name="filterValue">Supplied Filter value</param>
		/// <returns>A 12 byte byte array containing serialized GIAI96 value</returns>
        public static byte[] EncodeGIAItoGIAI96(string actualGIAI, Int32 companyPrefixIndexLength, byte filterValue)
		{
			//Check for GIAI string validity
			if(actualGIAI.Length > 30)
			{
				throw new GIAIStringTooLargeException("Invalid GIAI input string.Should be 30 digits long.");
			}

			//Check for filter value validity
			if(filterValue > 7)
			{
				throw new InvalidGIAI96EncodingException("Invalid filter value.");
			}

			int  companyPrefixBitsLength = 0,companyPrefixDigitsLength = 0, individualAssetTypeBitsLength =0,partitionValue = 0;
			//Lookup the partition value depending on the company prefix length from the partition table
			switch(companyPrefixIndexLength)
			{
				case 12:
					partitionValue = 0;
					companyPrefixBitsLength= 40;
					companyPrefixDigitsLength = 12;
					individualAssetTypeBitsLength = 42;
					break;
				case 11:
					partitionValue = 1;
					companyPrefixBitsLength = 37;
					companyPrefixDigitsLength = 11;
					individualAssetTypeBitsLength = 45;
					break;
				case 10:
					partitionValue = 2;
					companyPrefixBitsLength = 34;
					companyPrefixDigitsLength = 10;
					individualAssetTypeBitsLength = 48;
					break;
				case 9:
					partitionValue = 3;
					companyPrefixBitsLength = 30;
					companyPrefixDigitsLength = 9;
					individualAssetTypeBitsLength = 52;
					break;
				case 8:
					partitionValue = 4;
					companyPrefixBitsLength = 27;
					companyPrefixDigitsLength = 8;
					individualAssetTypeBitsLength = 55;
					break;
				case 7:
					partitionValue = 5;
					companyPrefixBitsLength = 24;
					companyPrefixDigitsLength = 7;
					individualAssetTypeBitsLength = 58;
					break;
				case 6:
					partitionValue = 6;
					companyPrefixBitsLength = 20;
					companyPrefixDigitsLength = 6;
					individualAssetTypeBitsLength = 62;
					break;
				default:
					throw new InvalidCompanyPrefixLengthException("Invalid company prefix length.");
			}

			//Construct the company prefix
			string companyPrefix = actualGIAI.Substring(0, companyPrefixDigitsLength);

			//Construct the individual asset reference
			string individualAssetReference = actualGIAI.Substring(companyPrefixDigitsLength);

			//Check the validity of the individual asset reference
			if(Convert.ToInt64(individualAssetReference)  >= Math.Pow(2, individualAssetTypeBitsLength))
			{
				throw new InvalidGIAI96EncodingException("this GIAI cannot be encoded in the GIAI-96 encoding.");
			}
			if(actualGIAI.Length >(companyPrefixDigitsLength + 1) && individualAssetReference.Substring(0,1).Equals("0"))
			{
				throw new InvalidGIAI96EncodingException("this GIAI cannot be encoded in the GIAI-96 encoding.");
			}

			//Declare the main encoded string
			StringBuilder strbEncodedString = new StringBuilder();

			//Header value for GIAI96 is 00110100 in binary
			strbEncodedString.Append(Constants.GIAI96_HEADER);

			//Insert the filter value
			strbEncodedString.Append(RFUtils.AddReqdZeros(Convert.ToString(filterValue,2),3));

			//Insert the partition value
			strbEncodedString.Append(RFUtils.AddReqdZeros(Convert.ToString(partitionValue,2),3));

			//Insert the company prefix
			strbEncodedString.Append(RFUtils.AddReqdZeros(Convert.ToString(Convert.ToInt64(companyPrefix),2), companyPrefixBitsLength));

			//Insert the individual asset reference
			strbEncodedString.Append(RFUtils.AddReqdZeros(Convert.ToString(Convert.ToInt64(individualAssetReference),2),individualAssetTypeBitsLength));

			//Declare the encoded byte array
			byte[] encodedGIAI96 = new byte[12];
			//Insert the string values to byte array
			for(int arrIndex=0, startValue = 0; arrIndex < 12; arrIndex++, startValue+=8)
			{
				encodedGIAI96[arrIndex] = Convert.ToByte(RFUtils.ConvertBinaryToDecimal(strbEncodedString.ToString().Substring(startValue,8)));
			}			
			return encodedGIAI96;
		}
		
		/// <summary>
		/// Method to serialize GIAI hex string to 96 bit GIAI
		/// </summary>
		/// <param name="GIAIHexValue">GIAI hex string</param>
		/// <returns>A 12 byte byte array containing  GIAI96</returns>
        public static byte[] EncodeGIAItoGIAI96(string GIAIHexValue)
		{
			//Declare the string bulder to contain the encoded bits
			StringBuilder strbConverted = new StringBuilder();

			for(int stringIndex=0; stringIndex < GIAIHexValue.Length; stringIndex++)
			{
				strbConverted.Append(RFUtils.AddReqdZeros(Convert.ToString(Convert.ToByte(RFUtils.ConvertHexToDecimal(GIAIHexValue.Substring(stringIndex,1))),2),4));
			}
			
			//Declare the byte array to return
			byte[] encodedGIAI96 = new byte[12];//GIAI will return this 12 bytes array

			//Insert the string values to byte array
			for(int arrIndex=0, startValue = 0; arrIndex < 12; arrIndex++, startValue+=8)
			{
				encodedGIAI96[arrIndex] = Convert.ToByte(RFUtils.ConvertBinaryToDecimal(strbConverted.ToString().Substring(startValue,8)));
			}
			return encodedGIAI96;
		}

        public static byte[] EncodeGIAItoGIAI96(string companyPrefix, string assetRef, Int32 companyPrefixLength, byte filterValue)
		{
			byte[] encodedGIAIArray = null; 
			string strGIAI = CreateGIAI(companyPrefix,assetRef) ;
			encodedGIAIArray = EncodeGIAItoGIAI96(strGIAI,companyPrefixLength,filterValue) ;
			return encodedGIAIArray ;
		}
        public static byte[] EncodeGIAItoGIAI96(string companyPrefix, string assetRef, byte filterValue)
		{
			return EncodeGIAItoGIAI96(companyPrefix,assetRef,companyPrefix.Length,filterValue);
		}
		#endregion
	
		#endregion Public Methods Ends

		#region Private Methods

        public static string CreateGIAI(string companyPrefix, string assetRef)
		{
			string GIAInumber = string.Empty ;
			if(companyPrefix.Length < 6 )
			{
			companyPrefix = companyPrefix.PadLeft(6,'0') ;
			}
			GIAInumber = companyPrefix+assetRef ;
			if(GIAInumber.Length > 30)
			{
				throw new InvalidGIAI96EncodingException("CreateGIAI() Length of created GIAI number exceeds 30.Length : "+GIAInumber.Length);
			}
			return GIAInumber ;
		}
		#endregion Private Methods ENDS
	}
}
