
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
using System.Text ;

namespace KTone.RFIDGlobal.EPCTagEncoding
{
	/// <summary>
	/// This class provides API to encode two formats ;64,96 bits specified by EPCGlobal.
	/// </summary>
	public class EPCEncoding
	{
		
		#region Public Methods

		private EPCEncoding()
		{
			//
			// TODO: Add constructor logic here
			//
		}


		#region Attributes

       static NLog.Logger m_log
        = KTLogger.KTLogManager.GetLogger();

		#endregion  Attributes ENDS

		#region 64-bit Encoding 

		/// <summary>
		/// Encode a GTIN barcode number to the EPCGlobal specified SGTIN 64-bit standard.
		/// </summary>
		/// <param name="actualGTIN">string that contains actual GTIN,a 13 digit number</param>
		/// <param name="serialNo">a 25 bit unique serial number</param>
		/// <param name="companyPrefixLength">An index into the company prefix table that determines the actual company Id</param>
		/// <param name="filterValue">The filter value associated with the GTIN number</param>
		/// <returns></returns>
		public static byte[] EncodeGTINtoSGTIN64(string actualGTIN,  System.Int32 serialNo, System.Int16 companyPrefixLength, byte filterValue)
		{
			byte[] byteArray = EncodeGTIN.EncodeGTIN14toSGTIN64(actualGTIN,serialNo,companyPrefixLength,filterValue) ;
			
			string byteArrayInBits = RFUtils.ByteArrayToString(byteArray) ;
			
			m_log.Debug("EPCEncoding::EncodeGTINtoSGTIN64() : The bit representation of byte Array is : "+byteArrayInBits);
			m_log.Debug("EPCEncoding::EncodeGTINtoSGTIN64() : actualGTIN is : "+actualGTIN);
			m_log.Debug("EPCEncoding::EncodeGTINtoSGTIN64() : serialNo is : "+serialNo);
			m_log.Debug("EPCEncoding::EncodeGTINtoSGTIN64() : filterValue is : "+filterValue);

			return byteArray ;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="indicatorDigit"></param>
		/// <param name="companyPrefix"></param>
		/// <param name="itemReference"></param>
		/// <param name="serialNo"></param>
		/// <param name="filterValue"></param>
		/// <returns></returns>
		public static byte[] EncodeGTINtoSGTIN64(string indicatorDigit,string companyPrefix,string itemReference,System.Int32 serialNo,byte filterValue)
		{
			byte[] byteArray = EncodeGTIN.EncodeGTIN14toSGTIN64(indicatorDigit,companyPrefix,itemReference,serialNo,filterValue);
			
			string byteArrayInBits = RFUtils.ByteArrayToString(byteArray) ;
			
			m_log.Debug("EPCEncoding::EncodeGTINtoSGTIN64() : The bit representation of byte Array is : "+byteArrayInBits);
			m_log.Debug("EPCEncoding::EncodeGTINtoSGTIN64() : indicatorDigit is : "+indicatorDigit);
			m_log.Debug("EPCEncoding::EncodeGTINtoSGTIN64() : companyPrefix is : "+companyPrefix);
			m_log.Debug("EPCEncoding::EncodeGTINtoSGTIN64() : itemReference is : "+itemReference);
			m_log.Debug("EPCEncoding::EncodeGTINtoSGTIN64() : serialNo is : "+serialNo);
			m_log.Debug("EPCEncoding::EncodeGTINtoSGTIN64() : filterValue is : "+filterValue);

			return byteArray ;
		}
		

		/// <summary>
		/// 
		/// </summary>
		/// <param name="filterValue"></param>
		/// <param name="companyPrefix"></param>
		/// <param name="locationRef"></param>
		/// <param name="serialNo"></param>
		/// <returns></returns>
		public static byte[] EncodeGLNtoSGLN64(byte filterValue,string companyPrefix,string locationRef,Int32 serialNo)
		{
			
			byte[] byteArray = EncodeGLN.EncodeGLNtoSGLN64(filterValue,companyPrefix,locationRef,serialNo) ;
			
			string byteArrayInBits = RFUtils.ByteArrayToString(byteArray) ;
			
			m_log.Debug("EPCEncoding::EncodeGLNtoSGLN64() : The bit representation of byte Array is : "+byteArrayInBits);
			m_log.Debug("EPCEncoding::EncodeGLNtoSGLN64() : companyPrefix is : "+companyPrefix);
			m_log.Debug("EPCEncoding::EncodeGLNtoSGLN64() : locationRef is : "+locationRef);
			m_log.Debug("EPCEncoding::EncodeGLNtoSGLN64() : serialNo is : "+serialNo);
			m_log.Debug("EPCEncoding::EncodeGLNtoSGLN64() : filterValue is : "+filterValue);

			return byteArray ;
		}


		public static byte[] EncodeSCCtoSSCC64(byte filterValue,string extensionDigit,string companyPrefix,string serialRef)
		{
			byte[] byteArray  = EncodeSSCC.EncodeSCCtoSSCC64(filterValue,extensionDigit,companyPrefix,serialRef) ;
			
			string byteArrayInBits = RFUtils.ByteArrayToString(byteArray) ;
			
			m_log.Debug("EPCEncoding::EncodeSCCtoSSCC64() : The bit representation of byte Array is : "+byteArrayInBits);
			m_log.Debug("EPCEncoding::EncodeSCCtoSSCC64() : extensionDigit is : "+extensionDigit);
			m_log.Debug("EPCEncoding::EncodeSCCtoSSCC64() : companyPrefix is : "+companyPrefix);
			m_log.Debug("EPCEncoding::EncodeSCCtoSSCC64() : serialRef is : "+serialRef);
			m_log.Debug("EPCEncoding::EncodeSCCtoSSCC64() : filterValue is : "+filterValue);

			return byteArray ;
		}

		public static byte[] EncodeGIAItoGIAI64(string companyPrefix,string assetRef, byte filterValue)
		{
			byte[] byteArray = EncodeGIAI.EncodeGIAItoGIAI64(companyPrefix,assetRef,filterValue) ;
			
			string byteArrayInBits = RFUtils.ByteArrayToString(byteArray) ;
			
			m_log.Debug("EPCEncoding::EncodeGIAItoGIAI64() : The bit representation of byte Array is : "+byteArrayInBits);
			m_log.Debug("EPCEncoding::EncodeGIAItoGIAI64() : companyPrefix is : "+companyPrefix);
			m_log.Debug("EPCEncoding::EncodeGIAItoGIAI64() : assetRef is : "+assetRef);
			m_log.Debug("EPCEncoding::EncodeGIAItoGIAI64() : filterValue is : "+filterValue);

			return byteArray ;
		}

		public static byte[] EncodeGRAItoGRAI64(byte filterValue,string companyPrefix,string assetType,string serialNo)
		{
			
			byte[] byteArray = EncodeGRAI.EncodeGRAItoGRAI64(filterValue,companyPrefix,assetType,serialNo) ;

			string byteArrayInBits = RFUtils.ByteArrayToString(byteArray) ;
			
			m_log.Debug("EPCEncoding::EncodeGRAItoGRAI64() : The bit representation of byte Array is : "+byteArrayInBits);
			m_log.Debug("EPCEncoding::EncodeGRAItoGRAI64() : companyPrefix is : "+companyPrefix);
			m_log.Debug("EPCEncoding::EncodeGRAItoGRAI64() : assetType is : "+assetType);
			m_log.Debug("EPCEncoding::EncodeGRAItoGRAI64() : serialNo is : "+serialNo);
			m_log.Debug("EPCEncoding::EncodeGRAItoGRAI64() : filterValue is : "+filterValue);

			return byteArray ;
		}

		public static byte[] EncodeUSDoD64(string cageNo, System.Int64 serialNo, byte filterValue)
		{
			
			byte[] byteArray = EncodeDoD.EncodeDoDtoDoD64(cageNo,serialNo,filterValue) ;

			string byteArrayInBits = RFUtils.ByteArrayToString(byteArray) ;
			
			m_log.Debug("EPCEncoding::EncodeDoDtoDoD64() : The bit representation of byte Array is : "+byteArrayInBits);
			m_log.Debug("EPCEncoding::EncodeDoDtoDoD64() : Cage Number is : "+cageNo);
			m_log.Debug("EPCEncoding::EncodeDoDtoDoD64() : Filter Value is : "+filterValue);
			m_log.Debug("EPCEncoding::EncodeDoDtoDoD64() : serialNo is : "+serialNo);

			return byteArray ;
		}


		#endregion 64-bit Encoding ENDS

		#region 96-bit Encoding 

		public static byte[] EncodeGTINtoSGTIN96(string actualGTIN,  System.Int64 serialNo,int partValue, byte filterValue)
		{
			byte[] byteArray =  EncodeGTIN.EncodeGTIN14toSGTIN96(actualGTIN,serialNo,partValue,filterValue) ;

			string byteArrayInBits = RFUtils.ByteArrayToString(byteArray) ;
			
			m_log.Debug("EPCEncoding::EncodeGTINtoSGTIN96() : The bit representation of byte Array is : "+byteArrayInBits);
			m_log.Debug("EPCEncoding::EncodeGTINtoSGTIN96() : actualGTIN is : "+actualGTIN);
			m_log.Debug("EPCEncoding::EncodeGTINtoSGTIN96() : partValue is : "+partValue);
			m_log.Debug("EPCEncoding::EncodeGTINtoSGTIN96() : serialNo is : "+serialNo);
			m_log.Debug("EPCEncoding::EncodeGTINtoSGTIN96() : filterValue is : "+filterValue);

			return byteArray ;
		}

		public static byte[] EncodeGTINtoSGTIN96(byte filterValue,string gtinIndicatorDigit,string companyPrefix,string itemReference,System.Int64 serialNo)
		{
			byte[] byteArray =  EncodeGTIN.EncodeGTIN14toSGTIN96(filterValue,gtinIndicatorDigit,companyPrefix,itemReference,serialNo) ;
			
			string byteArrayInBits = RFUtils.ByteArrayToString(byteArray) ;
			
			m_log.Debug("EPCEncoding::EncodeGTINtoSGTIN96() : The bit representation of byte Array is : "+byteArrayInBits);
			m_log.Debug("EPCEncoding::EncodeGTINtoSGTIN96() : gtinIndicatorDigit is : "+gtinIndicatorDigit);
			m_log.Debug("EPCEncoding::EncodeGTINtoSGTIN96() : companyPrefix is : "+companyPrefix);
			m_log.Debug("EPCEncoding::EncodeGTINtoSGTIN96() : itemReference is : "+itemReference);
			m_log.Debug("EPCEncoding::EncodeGTINtoSGTIN96() : serialNo is : "+serialNo);
			m_log.Debug("EPCEncoding::EncodeGTINtoSGTIN96() : filterValue is : "+filterValue);

			return byteArray ; 
		}

		public static byte[] EncodeSCCtoSSCC96(byte filterValue,string extensionDigit,string companyPrefix,string serialRef)
		{
			byte[] byteArray = EncodeSSCC.EncodeSCCtoSSCC96(filterValue,extensionDigit,companyPrefix,serialRef) ;
			
			string byteArrayInBits = RFUtils.ByteArrayToString(byteArray) ;
			
			m_log.Debug("EPCEncoding::EncodeSCCtoSSCC96() : The bit representation of byte Array is : "+byteArrayInBits);
			m_log.Debug("EPCEncoding::EncodeSCCtoSSCC96() : extensionDigit is : "+extensionDigit);
			m_log.Debug("EPCEncoding::EncodeSCCtoSSCC96() : companyPrefix is : "+companyPrefix);
			m_log.Debug("EPCEncoding::EncodeSCCtoSSCC96() : serialRef is : "+serialRef);
			m_log.Debug("EPCEncoding::EncodeSCCtoSSCC96() : filterValue is : "+filterValue);

			return byteArray ; 
		}

		public static byte[] EncodeGRAItoGRAI96(byte filterValue,string companyPrefix,string assetType,string serialNo)
		{
			byte[] byteArray = EncodeGRAI.EncodeGRAItoGRAI96(filterValue,companyPrefix,assetType,serialNo) ;
			
			string byteArrayInBits = RFUtils.ByteArrayToString(byteArray) ;
			
			m_log.Debug("EPCEncoding::EncodeGRAItoGRAI96() : The bit representation of byte Array is : "+byteArrayInBits);
			m_log.Debug("EPCEncoding::EncodeGRAItoGRAI96() : serialNo is : "+serialNo);
			m_log.Debug("EPCEncoding::EncodeGRAItoGRAI96() : companyPrefix is : "+companyPrefix);
			m_log.Debug("EPCEncoding::EncodeGRAItoGRAI96() : assetType is : "+assetType);
			m_log.Debug("EPCEncoding::EncodeGRAItoGRAI96() : filterValue is : "+filterValue);

			return byteArray ;  
		}

		public static byte[] EncodeGIAItoGIAI96(string companyPrefix,string assetRef, byte filterValue)
		{
			byte[] byteArray = EncodeGIAI.EncodeGIAItoGIAI96(companyPrefix,assetRef,filterValue) ;
			
			string byteArrayInBits = RFUtils.ByteArrayToString(byteArray) ;
			
			m_log.Debug("EPCEncoding::EncodeGIAItoGIAI96() : The bit representation of byte Array is : "+byteArrayInBits);
			m_log.Debug("EPCEncoding::EncodeGIAItoGIAI96() : companyPrefix is : "+companyPrefix);
			m_log.Debug("EPCEncoding::EncodeGIAItoGIAI96() : assetRef is : "+assetRef);
			m_log.Debug("EPCEncoding::EncodeGIAItoGIAI96() : filterValue is : "+filterValue);

			return byteArray ;  
		}

		public static byte[] EncodeGIAItoGIAI96(string companyPrefix,string assetRef,Int32 companyPrefixIndexLength, byte filterValue)
		{
			byte[] byteArray =  EncodeGIAI.EncodeGIAItoGIAI96(companyPrefix,assetRef,companyPrefix.Length,filterValue) ;
			
			string byteArrayInBits = RFUtils.ByteArrayToString(byteArray) ;
			
			m_log.Debug("EPCEncoding::EncodeGIAItoGIAI96() : The bit representation of byte Array is : "+byteArrayInBits);
			m_log.Debug("EPCEncoding::EncodeGIAItoGIAI96() : companyPrefix is : "+companyPrefix);
			m_log.Debug("EPCEncoding::EncodeGIAItoGIAI96() : assetRef is : "+assetRef);
			m_log.Debug("EPCEncoding::EncodeGIAItoGIAI96() : filterValue is : "+filterValue);

			return byteArray ;  
		}

		public static byte[] EncodeGLNtoSGLN96(byte filterValue,string companyPrefix,string locationRef,Int64 serialNo)
		{
			byte[] byteArray = EncodeGLN.EncodeGLNtoSGLN96(filterValue,companyPrefix,locationRef,serialNo) ;
			
			string byteArrayInBits = RFUtils.ByteArrayToString(byteArray) ;
			
			m_log.Debug("EPCEncoding::EncodeGLNtoSGLN96() : The bit representation of byte Array is : "+byteArrayInBits);
			m_log.Debug("EPCEncoding::EncodeGLNtoSGLN96() : companyPrefix is : "+companyPrefix);
			m_log.Debug("EPCEncoding::EncodeGLNtoSGLN96() : locationRef is : "+locationRef);
			m_log.Debug("EPCEncoding::EncodeGLNtoSGLN96() : filterValue is : "+filterValue);

			return byteArray ; 
		}

        public static byte[] EncodeLCTNtoLCTN96(string companyPrefix,Int64 serialNo)
        {
            byte[] byteArray = EncodeLCTN.EncodeLCTNtoLCTN96(LCTNHelper.FILTER_VALUE, companyPrefix, 
                LCTNHelper.LOCATION_REF, serialNo);

            string byteArrayInBits = RFUtils.ByteArrayToString(byteArray);

            m_log.Debug("EPCEncoding::EncodeLCTNtoLCTN96() : The bit representation of byte Array is : " + byteArrayInBits);
            m_log.Debug("EPCEncoding::EncodeLCTNtoLCTN96() : companyPrefix is : " + companyPrefix);
            m_log.Debug("EPCEncoding::EncodeLCTNtoLCTN96() : locationRef is : " + LCTNHelper.LOCATION_REF);
            m_log.Debug("EPCEncoding::EncodeLCTNtoLCTN96() : filterValue is : " + LCTNHelper.FILTER_VALUE);

            return byteArray;
        }

        public static byte[] EncodeASETtoASET96(string companyPrefix, Int64 serialNo)
        {
            byte filterValue = 0;
            string locationRef = "0";
            byte[] byteArray = EncodeASET.EncodeASETtoASET96(filterValue, companyPrefix, locationRef, serialNo);

            string byteArrayInBits = RFUtils.ByteArrayToString(byteArray);

            m_log.Debug("EPCEncoding::EncodeLCTNtoLCTN96() : The bit representation of byte Array is : " + byteArrayInBits);
            m_log.Debug("EPCEncoding::EncodeLCTNtoLCTN96() : companyPrefix is : " + companyPrefix);
            m_log.Debug("EPCEncoding::EncodeLCTNtoLCTN96() : locationRef is : " + locationRef);
            m_log.Debug("EPCEncoding::EncodeLCTNtoLCTN96() : filterValue is : " + filterValue);

            return byteArray;
        }

		public static byte[] EncodeGID96(string manufacturerId, 
			string productId,string serialNo )
		{
			byte[] byteArray = GID.Encode96(manufacturerId,productId,serialNo) ;
			
			string byteArrayInBits = RFUtils.ByteArrayToString(byteArray) ;
			
			m_log.Debug("EPCEncoding::EncodeGID96() : The bit representation of byte Array is : "+byteArrayInBits);
			m_log.Debug("EPCEncoding::EncodeGID96() : companyPrefix is : "+manufacturerId);
			m_log.Debug("EPCEncoding::EncodeGID96() : productId is : "+productId);
			m_log.Debug("EPCEncoding::EncodeGID96() : serialNo is : "+serialNo);

			return byteArray ; 
		}

		public static byte[] EncodeUSDOD96(string cageNo, System.Int64 serialNo, byte filterValue)
		{
			byte[] byteArray = EncodeDoD.EncodeDoDtoDoD96(cageNo, serialNo, filterValue) ;

			string byteArrayInBits = RFUtils.ByteArrayToString(byteArray) ;

			m_log.Debug("EPCEncoding::EncodeUSDOD96() : The bit representation of byte Array is : " + byteArrayInBits);
			m_log.Debug("EPCEncoding::EncodeUSDOD96() : Cage no is : " + cageNo);
			m_log.Debug("EPCEncoding::EncodeUSDOD96() : Serial no is : " + cageNo);
			m_log.Debug("EPCEncoding::EncodeUSDOD96() : Filter value is : " + filterValue);

			return byteArray ; 
		}

		#endregion 96-bit encoding ENDS

        #region 96-bit Encoding - EPC urn

        public static string EncodeGTINtoSGTIN96URN(string actualGTIN, System.Int64 serialNo, int partValue, byte filterValue)
        {
            return Decode(EncodeGTINtoSGTIN96(actualGTIN, serialNo, partValue, filterValue)); 
        }
        
        public static string EncodeGTINtoSGTIN96URN(byte filterValue, string gtinIndicatorDigit, string companyPrefix, string itemReference, System.Int64 serialNo)
        {
            return Decode(EncodeGTINtoSGTIN96(filterValue, gtinIndicatorDigit, companyPrefix, itemReference, serialNo));
        }
        
        public static string EncodeSCCtoSSCC96URN(byte filterValue, string extensionDigit, string companyPrefix, string serialRef)
        {
            return Decode(EncodeSCCtoSSCC96(filterValue, extensionDigit, companyPrefix, serialRef));
        }
        
        public static string EncodeGRAItoGRAI96URN(byte filterValue, string companyPrefix, string assetType, string serialNo)
        {
            return Decode(EncodeGRAItoGRAI96(filterValue, companyPrefix, assetType, serialNo));
        }

        public static string EncodeGIAItoGIAI96URN(string companyPrefix, string assetRef, byte filterValue)
        {
            return Decode(EncodeGIAItoGIAI96(companyPrefix, assetRef, filterValue));
        }

        public static string EncodeGIAItoGIAI96URN(string companyPrefix, string assetRef, Int32 companyPrefixIndexLength, byte filterValue)
        {
            return Decode(EncodeGIAItoGIAI96(companyPrefix, assetRef, companyPrefixIndexLength, filterValue));
        }

        public static string EncodeGLNtoSGLN96URN(byte filterValue, string companyPrefix, string locationRef, Int64 serialNo)
        {
            return Decode(EncodeGLNtoSGLN96(filterValue, companyPrefix, locationRef, serialNo));
        }

        public static string EncodeLCTNtoLCTN96URN(string companyPrefix, Int64 serialNo)
        {
            return Decode(EncodeLCTNtoLCTN96(companyPrefix, serialNo));
        }

        public static string EncodeASETtoASET96URN(string companyPrefix, Int64 serialNo)
        {
            return Decode(EncodeASETtoASET96(companyPrefix, serialNo));
        }

        public static string EncodeGID96URN(string manufacturerId,
            string productId, string serialNo)
        {
            return Decode(EncodeGID96(manufacturerId,productId, serialNo));
        }

        public static string EncodeUSDOD96URN(string cageNo, System.Int64 serialNo, byte filterValue)
        {
            return Decode(EncodeUSDOD96(cageNo, serialNo, filterValue));
        }
        
        #endregion 96-bit encoding ENDS - EPC urn

        public static string Decode(byte[] toDecodeBytes)
		{
			return EPCBytes.Decode(toDecodeBytes) ;
		}

		public static bool ValidateCompPrefix(string compPrefix)
		{
			bool bValidate = false ;
			
			ICompanyPrefixLookup icompPrefix = CompanyPrefixLookupImpl.GetInstanceOf()  ;

			UInt16 compPrefixIndex = 0 ;
			
			try
			{	
				bValidate =  icompPrefix.Lookup(compPrefix,out compPrefixIndex) ;
			}
			catch
			{
				bValidate = false ;
			}

			return bValidate ;
		}

		public static UInt16 Insert(string companyPrefix)
		{
			ICompanyPrefixLookup icompPrefix = CompanyPrefixLookupImpl.GetInstanceOf()  ;

			UInt16 compPrefixIndex = 0 ;

			try
			{	
				compPrefixIndex = icompPrefix.Insert(companyPrefix) ;
			}
			catch(CompanyPrefixLookupImplException ex)
			{
			 throw ex ;
			}
			return compPrefixIndex ;
		}

		#endregion Public Methods ENDS
	}
}
