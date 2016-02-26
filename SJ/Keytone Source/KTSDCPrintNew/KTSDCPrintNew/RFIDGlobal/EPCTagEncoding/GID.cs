
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
using NLog;

namespace KTone.RFIDGlobal.EPCTagEncoding
{
	/// <summary>
	/// Summary description for GID.
	/// </summary>
	public class GID
	{

		#region Attributes
        private static Logger log = LogManager.GetCurrentClassLogger();

		#endregion Attributes ENDS
		
		#region Private Methods
		
		#endregion Private Methods ENDS
		
		#region Public Methods

		private GID()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static bool Decode96(byte[] epcTagSerialNum,
			out string managerNo,out string objectClass,out string serialNum, bool throwException, out string errorMessage)
		{
			//Header : 2 bits
			//Company Id : 21 bits
			//Product Id : 17 bits
			//Serial No : 24 bits
            errorMessage = string.Empty;
            managerNo = string.Empty; 
			objectClass = string.Empty ; 
			serialNum = string.Empty ;
            errorMessage = string.Empty; 

			System.UInt32 manufacturerId = 0 ;
			System.UInt32 productId		 = 0; 
			System.UInt64 serialNo		 = 0 ;


			try
			{
                if (!Decode96(epcTagSerialNum, out manufacturerId, out productId, out serialNo, throwException, out errorMessage))
                    return false;
				managerNo = manufacturerId.ToString();
				objectClass = productId.ToString() ;
				serialNum	= serialNo.ToString() ;
			}
			catch ( Exception) 
			{
				throw;
			}
            return true;
		}

		public static bool Decode96(byte[] serialNoBytes,
			out System.UInt32 manufacturerId, out System.UInt32 productId,
            out System.UInt64 serialNo, bool throwException, out string errorMessage)
		{
			//Header : 2 bits
			//Company Id : 21 bits
			//Product Id : 17 bits
			//Serial No : 24 bits
            errorMessage = string.Empty;
            manufacturerId = 0;
			productId = 0;
			serialNo = 0;
            errorMessage = string.Empty; 

			try 
			{
				//Get 6 bits from 0th byte
				uint manufacturerId1stByte = serialNoBytes[1];
				manufacturerId |= manufacturerId1stByte ; 
				manufacturerId = manufacturerId << 20;

				uint manufacturerId2ndByte = serialNoBytes[2];
				manufacturerId2ndByte = manufacturerId2ndByte << 12;
				manufacturerId |= manufacturerId2ndByte ; 

				uint manufacturerId3rdByte = serialNoBytes[3];
				manufacturerId3rdByte = manufacturerId3rdByte << 4;
				manufacturerId |= manufacturerId3rdByte ; 

				uint manufacturerId4thByte = serialNoBytes[4] ; 
				manufacturerId4thByte = manufacturerId4thByte >> 4 ; 
				manufacturerId |= manufacturerId4thByte ; 

				uint productId1stByte = serialNoBytes[4];
				productId1stByte = (productId1stByte & 0xf);
				productId |= productId1stByte;
				productId = productId << 20;

				uint productId2ndByte = serialNoBytes[5];
				productId2ndByte = productId2ndByte << 12;
				productId |= productId2ndByte;

				uint productId3rdByte = serialNoBytes[6];
				productId3rdByte = productId3rdByte << 4;
				productId |= productId3rdByte;

				uint productId4thByte = serialNoBytes[7] ; 
				productId4thByte = productId4thByte >> 4 ; 
				productId |= productId4thByte ; 

				UInt64 serialNo1stByte = serialNoBytes[7];
				serialNo1stByte = (serialNo1stByte & 0xf);
				serialNo |= serialNo1stByte;
				serialNo = serialNo << 32;

				UInt64 serialNo2ndByte = serialNoBytes[8];
				serialNo2ndByte = serialNo2ndByte << 24;
				serialNo |= serialNo2ndByte;

				UInt64 serialNo3rdByte = serialNoBytes[9];
				serialNo3rdByte = serialNo3rdByte << 16;
				serialNo |= serialNo3rdByte;

				UInt64 serialNo4thByte = serialNoBytes[10] ; 
				serialNo4thByte = serialNo4thByte << 8 ; 
				serialNo |= serialNo4thByte ; 
			
				UInt64 serialNo5thByte = serialNoBytes[11] ; 
				//serialNo5thByte = serialNo5thByte; 
				serialNo |= serialNo5thByte ; 
			}
			catch ( Exception ex) 
			{
                if(throwException)
				    throw ex;
                log.TraceException("Decode Error:", ex);
                return false;
			}
            return true;
		}


		public static byte[] Encode96(System.UInt32 manufacturerId, 
			System.UInt32 productId,System.UInt64 serialNo) 
		{
			// EPC tags 96 bits = 12 bytes 
			byte[] serialArr  = new byte[12] ; 
			serialArr[0] = 0x35 ;  // EPC tag header 

			manufacturerId &= 0xfffffff ; // only 28 bit long 
			productId &= 0xffffff ; // only 24 bit long 
			serialNo &= 0xfffffffff;// only 36 bit long

			System.UInt32 shftManIndt = manufacturerId << 4  ; 
			shftManIndt |= productId >> 20 ; 
		
			serialArr[1] = (byte)((shftManIndt >> 24) & (System.UInt32) 0xff)  ; 
			serialArr[2] = (byte)(shftManIndt >>16  & (System.UInt32) 0xff)  ;
			serialArr[3] = (byte)(shftManIndt >>8  & (System.UInt32) 0xff)  ; 
			serialArr[4] = (byte)(shftManIndt & (System.UInt32) 0xff) ; 
 

			System.UInt32 nextInt = ( productId & 0xfffff ) << 4  ; 
			uint firstByteOfSerialNo = (uint)(serialNo >> 32 ); 
			nextInt |= firstByteOfSerialNo; 

			serialArr[5] = (byte)((nextInt >> 16) & (System.UInt32) 0xff)  ; 
			serialArr[6] = (byte)(nextInt >>8  & (System.UInt32) 0xff)  ;
			serialArr[7] = (byte)(nextInt & (System.UInt32) 0xff) ; 
			
			System.UInt64 snInt = ( serialNo & 0xffffffff );

			serialArr[8] = (byte)((snInt >> 24) & (System.UInt64) 0xff)  ; 
			serialArr[9] = (byte)((snInt >> 16) & (System.UInt64) 0xff)  ; 
			serialArr[10] = (byte)(snInt >>8  & (System.UInt64) 0xff)  ;
			serialArr[11] = (byte)(snInt & (System.UInt64) 0xff) ; 

			return serialArr ;
		}


		public static byte[] Encode96(string manufacturerId, 
			string productId,string serialNo )
		{
			// EPC tags 96 bits = 12 bytes 
			byte[] serialArr  = null ;
			System.Int32 manufacturerIdUInt = 0; 
			System.Int32 productIdUInt	 = 0 ;
			System.Int64 serialNoUInt = 0 ;
			
			

			string tempManId = string.Empty ;
			string tempProdId = string.Empty ;
			string tempSerialNo = string.Empty ;

			string manIdErr = "Encode96(): Invalid value of manufacturer Id: "+manufacturerId;
			string prodIdErr	= "Encode96(): Invalid value of product Id: "+productId ;
			string serialErr = "Encode96(): Invalid value of serial No: "+serialNo ;

			try
			{
				manufacturerIdUInt = Convert.ToInt32(manufacturerId);
			}
			catch
			{
				throw new EPCTagExceptionBase(manIdErr) ;
			}

			try
			{
				productIdUInt = Convert.ToInt32(productId);
			}
			catch
			{
				throw new EPCTagExceptionBase(prodIdErr) ;
			}

			try
			{
				serialNoUInt	= Convert.ToInt64(serialNo);
			}
			catch
			{
				throw new EPCTagExceptionBase(serialErr) ;
			}
			
			if(manufacturerIdUInt > 268435455)
			{
				throw new EPCTagExceptionBase(manIdErr) ;
			}

			if(productIdUInt > 16777215)
			{
				throw new EPCTagExceptionBase(prodIdErr) ;
			}	

			if(serialNoUInt > 68719476735)
			{
				throw new EPCTagExceptionBase(serialErr) ;
			}

			tempManId =	RFUtils.AddReqdZeros(Convert.ToString(manufacturerIdUInt,2),28) ;

			tempProdId = RFUtils.AddReqdZeros(Convert.ToString(productIdUInt,2),24) ;

			tempSerialNo = RFUtils.AddReqdZeros(Convert.ToString(serialNoUInt,2),36) ;

			string gidHeader = Constants.GID96_HEADER ;

			//Preparing the byte array
			string gidBitString = string.Empty ;

			gidBitString+=gidHeader+tempManId+tempProdId+tempSerialNo ;

			serialArr = RFUtils.StringToByteArray(gidBitString) ;
			
			return serialArr ;
		}

		public static byte[] Encode96(System.Int32 manufacturerId, 
			System.Int32 productId,System.Int64 serialNo) 
		{
			// EPC tags 96 bits = 12 bytes 
			byte[] serialArr  = new byte[12] ; 
			serialArr[0] = 0x35 ;  // EPC tag header 

			manufacturerId &= 0xfffffff ; // only 28 bit long 
			productId &= 0xffffff ; // only 24 bit long 
			serialNo &= 0xfffffffff;// only 36 bit long

			System.Int32 shftManIndt = manufacturerId << 4  ; 
			shftManIndt |= productId >> 20 ; 
		
			serialArr[1] = (byte)((shftManIndt >> 24) & (System.Int32) 0xff)  ; 
			serialArr[2] = (byte)(shftManIndt >>16  & (System.Int32) 0xff)  ;
			serialArr[3] = (byte)(shftManIndt >>8  & (System.Int32) 0xff)  ; 
			serialArr[4] = (byte)(shftManIndt & (System.Int32) 0xff) ; 
 

			System.Int32 nextInt = ( productId & 0xfffff ) << 4  ; 
			int firstByteOfSerialNo = (int)(serialNo >> 32 ); 
			nextInt |= firstByteOfSerialNo; 

			serialArr[5] = (byte)((nextInt >> 16) & (System.Int32) 0xff)  ; 
			serialArr[6] = (byte)(nextInt >>8  & (System.Int32) 0xff)  ;
			serialArr[7] = (byte)(nextInt & (System.Int32) 0xff) ; 
			
			System.Int64 snInt = ( serialNo & 0xffffffff );

			serialArr[8] = (byte)((snInt >> 24) & (System.Int64) 0xff)  ; 
			serialArr[9] = (byte)((snInt >> 16) & (System.Int64) 0xff)  ; 
			serialArr[10] = (byte)(snInt >>8  & (System.Int64) 0xff)  ;
			serialArr[11] = (byte)(snInt & (System.Int64) 0xff) ; 
			return serialArr ;
		}

		#endregion Public Methods ENDS
	}
}
