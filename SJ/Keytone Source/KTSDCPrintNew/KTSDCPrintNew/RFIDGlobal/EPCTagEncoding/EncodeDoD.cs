
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
	/// Summary description for EncodeDoD.
	/// </summary>
    public class EncodeDoD
	{
		#region Constructor
		private EncodeDoD()
		{}
		#endregion Constructor

		#region Public Methods

		#region Method to Encode DoD to DoD96

        public static byte[] EncodeDoDtoDoD96(string cageNo, System.Int64 serialNo, byte filterValue)
		{
			StringBuilder strbEncodeString = new StringBuilder();
			string tmpItemReference = string.Empty;

			//Checking for valid cage number length
			if ( cageNo.Length != 5 )
			{
				throw new DodStringToolLargeException("Invalid DoD cage number. Should be 5 characters long.");
			}

			int i = 1;
			byte[] byteCage = new byte[cageNo.Length + 1];
			byteCage[0] = Convert.ToByte(' ');

			//Checking for valid cage number
			foreach (char ch in cageNo.ToCharArray())
			{
				if ( ch == 'O' || ch == 'I')
				{
					throw new InvalidDoD96EncodingException("Invalid character in cage number.");
				}

                if ((ch < 'A' || ch > 'Z') && (ch < '0' || ch > '9') && (ch != ' '))
				{
					throw new InvalidDoD96EncodingException("Invalid character in cage number.");
				}

				byteCage[i++] = Convert.ToByte(ch);
			}

			//Checing for valid serial number
			if ( serialNo > Math.Pow(2, 36) - 1 )
			{
				throw new InvalidDoD96EncodingException("Serial number exceeds its limit.");
			}

			//Checing for valid filter value
			if ( filterValue > 2 )
			{
				throw new InvalidDoD96EncodingException("Invalid filter value.");
			}

			//Adding Header
			strbEncodeString.Append(Constants.USDOD96_HEADER);

			//Adding Filter
			tmpItemReference = RFUtils.AddReqdZeros(Convert.ToString(filterValue, 2), 4);
			strbEncodeString.Append(tmpItemReference);

			//Adding Cage Number
			for (int j = 0; j < byteCage.Length; j++)
			{
				string byteStr = Convert.ToString(byteCage[j], 2) ;
				tmpItemReference = RFUtils.AddReqdZeros(byteStr, 8);
				strbEncodeString.Append(tmpItemReference);
			}

			//Adding Serial Number
			tmpItemReference = RFUtils.AddReqdZeros(Convert.ToString(serialNo, 2), 36);
			strbEncodeString.Append(tmpItemReference);

			byte[] encodedDoD96 = new byte[12];

			//Insert the string values to byte array
			for(int arrIndex = 0, startValue = 0; arrIndex < 12; arrIndex++, startValue+=8)
			{
				encodedDoD96[arrIndex] = Convert.ToByte(RFUtils.ConvertBinaryToDecimal(strbEncodeString.ToString().Substring(startValue, 8)));
			}

			return encodedDoD96;
		}


        public static byte[] EncodeDoDtoDoD64(string cageNo, System.Int64 serialNo, byte filterValue)
		{

			byte[] encodedDoD64 = null ;
			StringBuilder strbEncodeString = new StringBuilder();
			

			//Checking for valid cage number length
			if ( cageNo.Length != 5 )
			{
				throw new DodStringToolLargeException("Invalid DoD cage number. Should be 5 characters long.");
			}

			int i = 0;
			byte[] byteCage = new byte[cageNo.Length];

			//Checking for valid cage number
			foreach (char ch in cageNo.ToCharArray())
			{
				if ( ch == 'O' || ch == 'I')
				{
					throw new InvalidDoD64EncodingException("Invalid character in cage number.");
				}

                if ((ch < 'A' || ch > 'Z') && (ch < '0' || ch > '9') && (ch != ' '))
				{
					throw new InvalidDoD64EncodingException("Invalid character in cage number.");
				}

				byteCage[i++] = Convert.ToByte(ch);
			}

			//Checing for valid serial number
			if ( serialNo > Math.Pow(2, 24) - 1 )
			{
				throw new InvalidDoD64EncodingException("Serial number exceeds its limit.");
			}

			//Checing for valid filter value
			if ( filterValue > 2 )
			{
				throw new InvalidDoD64EncodingException("Invalid filter value.");
			}

			//Adding Header
			strbEncodeString.Append(Constants.USDOD64_HEADER);

			//Adding Filter
			string tmpFilter = Convert.ToString(filterValue, 2).PadLeft(2,'0') ;
			strbEncodeString.Append(tmpFilter);

			//Adding Cage Number
			string strBytesCage = string.Empty ;
			for (int j = 0; j < byteCage.Length; j++)
			{
				string byteStr = Convert.ToString(byteCage[j], 2).PadLeft(8,'0') ;
				strBytesCage = byteStr.Substring(2) ;
				strbEncodeString.Append(strBytesCage);
			}

			//Adding Serial Number
			string strSerialNo = RFUtils.AddReqdZeros(Convert.ToString(serialNo, 2), 24);
			strbEncodeString.Append(strSerialNo);

			encodedDoD64 = new byte[8];

			//Insert the string values to byte array
			for(int arrIndex = 0, startValue = 0; arrIndex < 8 ; arrIndex++, startValue+=8)
			{
				encodedDoD64[arrIndex] = Convert.ToByte(RFUtils.ConvertBinaryToDecimal(strbEncodeString.ToString().Substring(startValue, 8)));
			}
			return encodedDoD64 ;
		}


		#endregion Method to Encode DoD to DoD96
		
		#endregion Public Methods Ends
	}
}
