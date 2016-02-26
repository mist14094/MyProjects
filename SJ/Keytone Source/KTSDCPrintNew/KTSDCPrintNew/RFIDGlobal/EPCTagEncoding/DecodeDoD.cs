
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
	/// Summary description for DecodeDoD.
	/// </summary>
	public class DecodeDoD
	{
		private const string dod96UrnHeader = "urn:epc:tag:usdod-96:";
        private static Logger log = LogManager.GetCurrentClassLogger();

		#region Constructor
		private DecodeDoD()
		{
		}
		#endregion Constructor

		#region Method to Decode DoD96

        public static bool DecodeDoD96(byte[] encodedDoD96, out string filterVal, out string cageNumber, out string serialNum,
            bool throwException, out string errorMessage)
		{
            errorMessage = string.Empty;
            cageNumber = string.Empty;
			filterVal  = string.Empty ;
			serialNum  = string.Empty ;

			if ((encodedDoD96== null) || (encodedDoD96.Length != 12 ))
			{
                errorMessage = "Invalid encoded byte array. Array should be of length 12.";
                if(throwException)
				    throw new InvalidDoD96DecodingException(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
			}

			StringBuilder strbDoD = new StringBuilder();

			//Extract the Byte array into a string
			for(int byteArrayIndex = 0; byteArrayIndex < encodedDoD96.Length; byteArrayIndex ++)
			{
				strbDoD.Append(RFUtils.AddReqdZeros(Convert.ToString(encodedDoD96[byteArrayIndex],2),8));
			}

//			StringBuilder strbConverted = new StringBuilder();
//			strbConverted.Append(dod96UrnHeader);

			filterVal = Convert.ToString(Convert.ToByte(RFUtils.ConvertBinaryToDecimal(strbDoD.ToString().Substring(8, 4))), 10);
			//strbConverted.Append(filter);
			//strbConverted.Append(".");

			for (int i = 0; i < 5; i++)
			{
				string cageByte = Convert.ToString(Convert.ToByte(RFUtils.ConvertBinaryToDecimal(strbDoD.ToString().Substring(20 + (i * 8), 8))));
				//strbConverted.Append(Convert.ToChar( Convert.ToInt16(cageByte) ).ToString().Trim());
				cageNumber+=Convert.ToChar( Convert.ToInt16(cageByte) ).ToString();
			}

			//strbConverted.Append(".");

			serialNum = RFUtils.ConvertBinaryToDecimal(strbDoD.ToString().Substring(60, 36));
			//strbConverted.Append(serialNo);

			//return strbConverted.ToString().ToUpper();
            return true;
		}


		public static bool DecodeDoD64(byte[] encodedDoD64,out string filterVal,out string cageNumber,out string serialNum,
            bool throwException, out string errorMessage)
		{
            errorMessage = string.Empty;
            cageNumber = string.Empty;
			filterVal  = string.Empty ;
			serialNum  = string.Empty ;

			if ((encodedDoD64== null) || (encodedDoD64.Length != 8 ))
			{
                errorMessage = "Invalid encoded byte array. Array should be of length 8.";
                if(throwException)
				    throw new InvalidDoD64DecodingException(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
			}

			StringBuilder strbDoD = new StringBuilder();

			//Extract the Byte array into a string
			for(int byteArrayIndex = 0; byteArrayIndex < encodedDoD64.Length; byteArrayIndex ++)
			{
				strbDoD.Append(RFUtils.AddReqdZeros(Convert.ToString(encodedDoD64[byteArrayIndex],2),8));
			}

			//			StringBuilder strbConverted = new StringBuilder();
			//			strbConverted.Append(dod96UrnHeader);

			filterVal = Convert.ToString(Convert.ToByte(RFUtils.ConvertBinaryToDecimal(strbDoD.ToString().Substring(8, 2))), 10);
			//strbConverted.Append(filter);
			//strbConverted.Append(".");
			string strDec = string.Empty ;

			for (int i = 0; i < 5; i++)
			{
				strDec = RFUtils.ConvertBinaryToDecimal(strbDoD.ToString().Substring(10 + (i * 6),6)) ;
				cageNumber+=FetchCharacter(strDec) ;
//				string cageByte = Convert.ToString(Convert.ToByte(strDec));
//				//strbConverted.Append(Convert.ToChar( Convert.ToInt16(cageByte) ).ToString().Trim());
//				cageNumber+=Convert.ToChar( Convert.ToInt16(cageByte) ).ToString().Trim() ;
			}

			//strbConverted.Append(".");

			serialNum = RFUtils.ConvertBinaryToDecimal(strbDoD.ToString().Substring(40, 24));
			//strbConverted.Append(serialNo);

			//return strbConverted.ToString().ToUpper();
            return true;
		}

		private static string FetchCharacter(string strNumber)
		{
			string retChar = string.Empty ;
			
			switch(Convert.ToInt16(strNumber))
			{
				case 1:
				{
					retChar = "A";
					break;
				}
				case 2:
				{
					retChar = "B";
					break;
				}
				case 3:
				{
					retChar = "C";
					break;
				}
				case 4:
				{
					retChar = "D";
					break;
				}
				case 5:
				{
					retChar = "E";
					break;
				}
				case 6:
				{
					retChar = "F";
					break;
				}
				case 7:
				{
					retChar = "G";
					break;
				}
				case 8:
				{
					retChar = "H";
					break;
				}
				case 9:
				{
					throw new InvalidDoD64DecodingException("FetchCharacter() Invalid CAGE Character! I ");
				}
				case 10:
				{
					retChar = "J";
					break;
				}
				case 11:
				{
					retChar = "K";
					break;
				}
				case 12:
				{
					retChar = "L";
					break;
				}
				case 13:
				{
					retChar = "M";
					break;
				}
				case 14:
				{
					retChar = "N";
					break;
				}
				case 15:
				{
					throw new InvalidDoD64DecodingException("FetchCharacter() Invalid CAGE Character! O ");
				}
				case 16:
				{
					retChar = "P";
					break;
				}
				case 17:
				{
					retChar = "Q";
					break;
				}
				case 18:
				{
					retChar = "R";
					break;
				}
				case 19:
				{
					retChar = "S";
					break;
				}
				case 20:
				{
					retChar = "T";
					break;
				}
				case 21:
				{
					retChar = "U";
					break;
				}
				case 22:
				{
					retChar = "V";
					break;
				}
				case 23:
				{
					retChar = "W";
					break;
				}
				case 24:
				{
					retChar = "X";
					break;
				}
				case 25:
				{
					retChar = "Y";
					break;
				}
				case 26:
				{
					retChar = "Z";
					break;
				}
				case 48:
				{
					retChar = "0";
					break;
				}
				case 49:
				{
					retChar = "1";
					break;
				}
				case 50:
				{
					retChar = "2";
					break;
				}
				case 51:
				{
					retChar = "3";
					break;
				}
				case 52:
				{
					retChar = "4";
					break;
				}
				case 53:
				{
					retChar = "5";
					break;
				}
				case 54:
				{
					retChar = "6";
					break;
				}
				case 55:
				{
					retChar = "7";
					break;
				}
				case 56:
				{
					retChar = "8";
					break;
				}
				case 57:
				{
					retChar = "9";
					break;
				}
				default:
				{
					throw new InvalidDoD64DecodingException("FetchCharacter() Invalid CAGE Character! Decimal Value : "+strNumber);
				}

			}
			return retChar ;
		}
		#endregion Method to Decode DoD96
	}
}
