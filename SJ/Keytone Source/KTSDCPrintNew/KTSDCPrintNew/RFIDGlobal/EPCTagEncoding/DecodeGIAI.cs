
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
	/// Summary description for DecodeGIAI.
	/// </summary>
	public class DecodeGIAI
	{
        private static Logger log = LogManager.GetCurrentClassLogger();

		#region Private Methods
        public static void BerakGIAI(string strGIAI, int partValue, out string companyPrefix, out string assetRef)
		{
			companyPrefix = string.Empty ;
			assetRef	 = string.Empty ;
			try
			{
				PartitionTable.FillPartitionTables(EPCFORMAT.GIAI); 
				int companyPrefixDigitLength = PartitionTable.companyPrefixDigitLength[partValue] ;

				int assetRefLength	 = strGIAI.Length-companyPrefixDigitLength ;

				companyPrefix = strGIAI.Substring(0,companyPrefixDigitLength) ;
				assetRef = strGIAI.Substring(companyPrefixDigitLength,assetRefLength) ;
			}
			catch(Exception e)
			{
				throw e;
			}
		}
		#endregion Private Methods ENDS
		
		#region Public Methods

		private DecodeGIAI()
		{}

		#region Method to De Encode GIAI64
		
		/// <summary>
		/// Method to De serialize GIAI64
		/// </summary>
		/// <param name="encodedGIAI64">Encoded GIAI64 8 byte byte array</param>
		/// <param name="GIAI">Output GIAI string</param>
		/// <param name="filterValue">Output filter value</param>
        public static bool DecodeGIAI64(byte[] encodedGIAI64, out string GIAI, out string companyPrefix, out string individualAssetReference, out byte filterValue,
            bool throwException, out string errorMessage)
		{
            errorMessage = string.Empty;
            GIAI = string.Empty;
			companyPrefix = string.Empty ;
			individualAssetReference = string.Empty ;
			filterValue = 0 ;

			//Check for byte array validity
			if(encodedGIAI64.Length != 8)
			{
                errorMessage = "Not a valid GIAI64 string.";
                if(throwException)
				    throw new InvalidGIAI64EncodingException(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
			}

			//Check for header value validity header value is 00001011=11 in decimal
			if(encodedGIAI64[0] != 11)
			{
                errorMessage = "Invalid header value for GIAI 64";
                if(throwException)
				    throw new InvalidHeaderValue(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
			}

			//Xtract the contents of the byte array to a string
			StringBuilder strbEncodeString = new StringBuilder();

			for(int byteArrayIndex = 0; byteArrayIndex < encodedGIAI64.Length; byteArrayIndex++)
			{
				strbEncodeString.Append(RFUtils.AddReqdZeros(Convert.ToString(encodedGIAI64[byteArrayIndex],2).Trim(),8));
			}

			//Extract the filtervalue
			filterValue = Convert.ToByte(RFUtils.ConvertBinaryToDecimal(strbEncodeString.ToString().Substring(8,3)));

			//Check for filter value validity
			if(filterValue > 7)
			{
                errorMessage = "Invalid filter value.";
                if(throwException)
				    throw new InvalidGIAI64EncodingException(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
			}

			//Xtract the 14 bits company prefix index
			string companyPrefixIndex = RFUtils.ConvertBinaryToDecimal(strbEncodeString.ToString().Substring(11,14));
			companyPrefix = RFUtils.FetchCompanyPrefix(Convert.ToUInt16(companyPrefixIndex)) ;
			//Validate this company reference with Copnay Prefix translation table
			int companyPrefixLength = companyPrefix.Length ;//Assign the company prefix length here

			//Xtract  the Individual Asset reference
			individualAssetReference = RFUtils.ConvertBinaryToDecimal(strbEncodeString.ToString().Substring(25));

			//Check for Company prefix validity
			if(Convert.ToInt64(individualAssetReference) >= Math.Pow(10, (30 - companyPrefixLength)))
			{
                errorMessage = "DecodeGIAI64():individualAssetReference exceeds limits.Error Values: " + individualAssetReference;
                if(throwException)
				    throw new InvalidGIAI64EncodingException(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
			}

			GIAI = String.Concat(companyPrefix,individualAssetReference);

			//Check for valid GIAI
			if(GIAI.Length > 30)
			{
                errorMessage = "Invalid GIAI string.";
                if(throwException)
				    throw new InvalidGIAI64EncodingException(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
			}
            return true;
		}

		/// <summary>
		///  Method to Decode encoded GIAI64 string and send the hex string
		/// </summary>
		/// <param name="encodedGIAI64">Encoded GIAI64 8 byte byte array</param>
		/// <returns>Decoded hex string</returns>
        public static string DecodeGIAI64(byte[] encodedGIAI64)
		{
			
			StringBuilder strbGIAI = new StringBuilder();
			//Extract the Byte array into a string
			for(int byteArrayIndex = 0; byteArrayIndex < encodedGIAI64.Length; byteArrayIndex ++)
			{
				strbGIAI.Append(RFUtils.AddReqdZeros(Convert.ToString(encodedGIAI64[byteArrayIndex],2),8));
			}
			
			StringBuilder strbConverted = new StringBuilder();
	
			for(int startValue = 0; startValue < strbGIAI.ToString().Length;  startValue+=4)
			{
				strbConverted.Append(Convert.ToString(Convert.ToByte(RFUtils.ConvertBinaryToDecimal(strbGIAI.ToString().Substring(startValue,4))),16));
			}
			return strbConverted.ToString().ToUpper();
		}


        public static bool DecodeGIAI64(byte[] encodedGIAI64, out string companyPrefix, out string individualAssetReference, out byte filterValue,
            bool throwWxception, out string errorMessage)
		{
			string strGIAI = string.Empty ;
            errorMessage = string.Empty;
            return DecodeGIAI64(encodedGIAI64, out strGIAI, out companyPrefix, out individualAssetReference, out filterValue, throwWxception, out errorMessage);
		}

		#endregion

		#region Method to De Encode GIAI96
		/// <summary>
		/// Method to De serialize GIAI96
		/// </summary>
		/// <param name="encodedGIAI96">Encoded GIAI96 12 byte byte array</param>
		/// <param name="GIAI">Output GIAI string</param>
		/// <param name="filterValue">Output filter value</param>
        public static bool DecodeGIAI96(byte[] encodedGIAI96, out string GIAI, out byte filterValue, out int partValue,
            bool throwException, out string errorMessage)
		{
            errorMessage = string.Empty;
            GIAI = string.Empty;
            filterValue = 0;
            partValue = 0;
			//Check for validate encoded byte array
			if(encodedGIAI96.Length != 12)
			{
                errorMessage = "Invalid GIAI96 encoded string";
                if(throwException)
				    throw new InvalidGIAI96EncodingException(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
			}
			//Check for header value validity header value is 00110100=52 in decimal
			if(encodedGIAI96[0] != 52)
			{
                errorMessage = "Invalid header value for GIAI 96";
                if(throwException)
				    throw new InvalidHeaderValue(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
			}

			//Xtract the contents of the byte array to a string
			StringBuilder strbEncodeString = new StringBuilder();

			for(int byteArrayIndex = 0; byteArrayIndex < encodedGIAI96.Length; byteArrayIndex++)
			{
				strbEncodeString.Append(RFUtils.AddReqdZeros(Convert.ToString(encodedGIAI96[byteArrayIndex],2).Trim(),8));
			}

			//Xtract the filter value
			filterValue = Convert.ToByte(RFUtils.ConvertBinaryToDecimal(strbEncodeString.ToString().Substring(8,3)));
			
			//Check for filter value validity
			if(filterValue < 0 || filterValue > 7)
			{
                errorMessage = "Invalid filter value.";
                if(throwException)
				    throw new InvalidGIAI96EncodingException(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
			}


			int  companyPrefixBitsLength = 0,companyPrefixDigitsLength = 0,partitionValue = 0;//,assetTypeBitsLength =0;variable not being used anywhere

			//Xtract the partition value
			partitionValue = Convert.ToInt32(RFUtils.ConvertBinaryToDecimal(strbEncodeString.ToString().Substring(11,3)));
			partValue = partitionValue ;
			//Lookup the partition value depending on the company prefix length from the partition table
			switch(partitionValue)
			{
				case 0:
					companyPrefixBitsLength = 40;
					companyPrefixDigitsLength = 12;
					//assetTypeBitsLength = 42;
					break;
				case 1:
					companyPrefixBitsLength = 37;
					companyPrefixDigitsLength = 11;
					//assetTypeBitsLength = 45;
					break;
				case 2:
					companyPrefixBitsLength = 34;
					companyPrefixDigitsLength = 10;
					//assetTypeBitsLength = 48;
					break;
				case 3:
					companyPrefixBitsLength = 30;
					companyPrefixDigitsLength = 9;
					//assetTypeBitsLength = 52;
					break;
				case 4:
					companyPrefixBitsLength = 27;
					companyPrefixDigitsLength = 8;
					//assetTypeBitsLength = 55;
					break;
				case 5:
					companyPrefixBitsLength = 24;
					companyPrefixDigitsLength = 7;
					//assetTypeBitsLength = 58;
					break;
				case 6:
					companyPrefixBitsLength = 20;
					companyPrefixDigitsLength = 6;
					//assetTypeBitsLength = 62;
					break;
				default:
                    errorMessage = "Invalid company prefix length.";
                    if(throwException)
					    throw new InvalidCompanyPrefixLengthException(errorMessage);
                    log.Trace("Decode Error:", errorMessage);
                    return false;
			}

			//Xtract the company prefix
			string companyPrefix =RFUtils.AddReqdZeros(RFUtils.ConvertBinaryToDecimal(strbEncodeString.ToString().Substring(14, companyPrefixBitsLength)),companyPrefixDigitsLength);

			//Check for company prefix validity
			if(Convert.ToInt64(companyPrefix) >= Math.Pow(10, companyPrefixDigitsLength))
			{
                errorMessage = "the input bit string is not a legal GIAI-96 encoding";
                if(throwException)
				    throw new InvalidGIAI96EncodingException(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
			}

			//Xtract the individual asset reference
			string individualAssetReference = RFUtils.ConvertBinaryToDecimal(strbEncodeString.ToString().Substring(14 + companyPrefixBitsLength));

			GIAI = String.Concat(companyPrefix, individualAssetReference);
            return true;
		}
		
		/// <summary>
		///  Method to Decode encoded GIAI96 string and send the hex string
		/// </summary>
		/// <param name="encodedGIAI96">Encoded GIAI96 12 byte byte array</param>
		/// <returns>Decoded hex string</returns>
        public static string DecodeGIAI96(byte[] encodedGIAI96)
		{
			
			StringBuilder strbGIAI = new StringBuilder();
			//Extract the Byte array into a string
			for(int byteArrayIndex = 0; byteArrayIndex < encodedGIAI96.Length; byteArrayIndex ++)
			{
				strbGIAI.Append(RFUtils.AddReqdZeros(Convert.ToString(encodedGIAI96[byteArrayIndex],2),8));
			}
			
			StringBuilder strbConverted = new StringBuilder();
	
			for(int startValue = 0; startValue < strbGIAI.ToString().Length;  startValue+=4)
			{
				strbConverted.Append(Convert.ToString(Convert.ToByte(RFUtils.ConvertBinaryToDecimal(strbGIAI.ToString().Substring(startValue,4))),16));
			}
			return strbConverted.ToString().ToUpper();
		}

        public static bool DecodeGIAI96(byte[] encodedGIAI96, out string companyPrefix, out string assetRef, out byte filterValue, 
            bool throwException, out string errorMessage)
		{
			try
			{
                errorMessage = string.Empty;
                filterValue = 0;
                companyPrefix = string.Empty;
                assetRef = string.Empty;
                string GIAINumber = string.Empty;
				int partValue = 0 ;

                if (!DecodeGIAI96(encodedGIAI96, out GIAINumber, out filterValue, out partValue, throwException, out errorMessage))
                    return false;

				BerakGIAI(GIAINumber,partValue,out companyPrefix,out assetRef) ;

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
