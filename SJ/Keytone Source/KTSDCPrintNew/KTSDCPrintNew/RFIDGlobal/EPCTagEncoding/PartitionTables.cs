
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
using System.Collections ;

namespace KTone.RFIDGlobal.EPCTagEncoding
{
		/// <summary>
		/// This is a structure for storing parition tables per EPC Global format like SGTIN,SSCC etc.
		/// </summary>
      public struct PartitionTable
		{
			#region Attributes
			public static int[] companyPrefixBitLength  ;
			public static int[] itemRefBitLength   ;
			public static int[] serialRefBitLength ;
			public static int[] locationRefBitLength ;
			public static int[] assetTypeBitLength ;
			public static int[] assetRefBitLength ;


			public static int[] companyPrefixDigitLength ;
			public static int[] serialRefDigitLength ;
			public static int[] itemRefDigitLength ;
			public static int[] locationRefDigitLength ;
			public static int[] assetRefDigitLength ;
			public static int[] assetTypeDigitLength ;

			#endregion

			#region	Public Methods
		
			public static void FillPartitionTables(EPCFORMAT format)
			{
				const int arraySize = 7 ;
				companyPrefixBitLength = new int[arraySize]  ;
				itemRefBitLength   = new int[arraySize]  ;
				serialRefBitLength = new int[arraySize]  ;
				locationRefBitLength = new int[arraySize]  ;
				assetTypeBitLength = new int[arraySize]  ;
				assetRefBitLength	= new int[arraySize] ;

				companyPrefixDigitLength = new int[arraySize] ;
				serialRefDigitLength	 = new int[arraySize] ;
				itemRefDigitLength		 = new int[arraySize] ;
				locationRefDigitLength	 = new int[arraySize] ;
				assetRefDigitLength		 = new int[arraySize] ;
				assetTypeDigitLength	 = new int[arraySize] ;

				switch(format)
				{
					case EPCFORMAT.SGTIN:
					{
						companyPrefixBitLength[0] = 40 ;
						companyPrefixBitLength[1] = 37 ;
						companyPrefixBitLength[2] = 34 ;
						companyPrefixBitLength[3] = 30 ;
						companyPrefixBitLength[4] = 27 ;
						companyPrefixBitLength[5] = 24 ;
						companyPrefixBitLength[6] = 20 ;

						itemRefBitLength[0] = 4 ; 
						itemRefBitLength[1] = 7 ;
						itemRefBitLength[2] = 10 ;
						itemRefBitLength[3] = 14 ;
						itemRefBitLength[4] = 17 ;
						itemRefBitLength[5] = 20 ;
						itemRefBitLength[6] = 24 ;

						companyPrefixDigitLength[0] = 12 ;
						companyPrefixDigitLength[1] = 11 ;
						companyPrefixDigitLength[2] = 10 ;
						companyPrefixDigitLength[3] = 9 ;
						companyPrefixDigitLength[4] = 8 ;
						companyPrefixDigitLength[5] = 7 ;
						companyPrefixDigitLength[6] = 6 ;

						itemRefDigitLength[0] = 1 ;
						itemRefDigitLength[1] = 2 ;
						itemRefDigitLength[2] = 3 ;
						itemRefDigitLength[3] = 4 ;
						itemRefDigitLength[4] = 5 ;
						itemRefDigitLength[5] = 6 ;
						itemRefDigitLength[6] = 7 ;
						break;
					}
					case EPCFORMAT.SSCC:
					{
						companyPrefixBitLength [0] = 40 ;
						companyPrefixBitLength [1] = 37 ;
						companyPrefixBitLength [2] = 34 ;
						companyPrefixBitLength [3] = 30 ;
						companyPrefixBitLength [4] = 27 ;
						companyPrefixBitLength [5] = 24 ;
						companyPrefixBitLength [6] = 20 ;

						serialRefBitLength [0]  = 18  ;
						serialRefBitLength [1]  = 21  ;
						serialRefBitLength [2]  = 24  ;
						serialRefBitLength [3]  = 28  ;
						serialRefBitLength [4]  = 31  ;
						serialRefBitLength [5]  = 34  ;
						serialRefBitLength [6]  = 38  ;

						companyPrefixDigitLength[0] = 12 ;
						companyPrefixDigitLength[1] = 11 ;
						companyPrefixDigitLength[2] = 10 ;
						companyPrefixDigitLength[3] = 9 ;
						companyPrefixDigitLength[4] = 8 ;
						companyPrefixDigitLength[5] = 7 ;
						companyPrefixDigitLength[6] = 6 ;

						serialRefDigitLength[0] = 5 ;
						serialRefDigitLength[1] = 6 ;
						serialRefDigitLength[2] = 7 ;
						serialRefDigitLength[3] = 8 ;
						serialRefDigitLength[4] = 9 ;
						serialRefDigitLength[5] = 10 ;
						serialRefDigitLength[6] = 11 ;
						break;
					}
					case EPCFORMAT.SGLN:
                    case EPCFORMAT.ASET:
                    case EPCFORMAT.LCTN:
					{
						companyPrefixBitLength[0]=40;
						companyPrefixBitLength[1]=37;
						companyPrefixBitLength[2]=34;
						companyPrefixBitLength[3]=30;
						companyPrefixBitLength[4]=27;
						companyPrefixBitLength[5]=24;
						companyPrefixBitLength[6]=20;

						locationRefBitLength[0]=1;
						locationRefBitLength[1]=4;
						locationRefBitLength[2]=7;
						locationRefBitLength[3]=11;
						locationRefBitLength[4]=14;
						locationRefBitLength[5]=17;
						locationRefBitLength[6]=21;

						companyPrefixDigitLength[0] = 12 ;
						companyPrefixDigitLength[1] = 11 ;
						companyPrefixDigitLength[2] = 10 ;
						companyPrefixDigitLength[3] = 9 ;
						companyPrefixDigitLength[4] = 8 ;
						companyPrefixDigitLength[5] = 7 ;
						companyPrefixDigitLength[6] = 6 ;

						locationRefDigitLength[0] = 0 ;
						locationRefDigitLength[1] = 1 ;
						locationRefDigitLength[2] = 2 ;
						locationRefDigitLength[3] = 3 ;
						locationRefDigitLength[4] = 4 ;
						locationRefDigitLength[5] = 5 ;
						locationRefDigitLength[6] = 6 ;
						break;
					}
					case EPCFORMAT.GRAI:
					{
						companyPrefixBitLength[0]=40;
						companyPrefixBitLength[1]=37;
						companyPrefixBitLength[2]=34;
						companyPrefixBitLength[3]=30;
						companyPrefixBitLength[4]=27;
						companyPrefixBitLength[5]=24;
						companyPrefixBitLength[6]=20;

						assetTypeBitLength[0]=4;
						assetTypeBitLength[1]=7;
						assetTypeBitLength[2]=10;
						assetTypeBitLength[3]=14;
						assetTypeBitLength[4]=17;
						assetTypeBitLength[5]=20;
						assetTypeBitLength[6]=24;

						companyPrefixDigitLength[0] = 12 ;
						companyPrefixDigitLength[1] = 11 ;
						companyPrefixDigitLength[2] = 10 ;
						companyPrefixDigitLength[3] = 9 ;
						companyPrefixDigitLength[4] = 8 ;
						companyPrefixDigitLength[5] = 7 ;
						companyPrefixDigitLength[6] = 6 ;

						assetTypeDigitLength[0] = 0 ;
						assetTypeDigitLength[1] = 1 ;
						assetTypeDigitLength[2] = 2 ;
						assetTypeDigitLength[3] = 3 ;
						assetTypeDigitLength[4] = 4 ;
						assetTypeDigitLength[5] = 5 ;
						assetTypeDigitLength[6] = 6 ;

						break;
					}
					case EPCFORMAT.GIAI:
					{
						companyPrefixBitLength[0]=40;
						companyPrefixBitLength[1]=37;
						companyPrefixBitLength[2]=34;
						companyPrefixBitLength[3]=30;
						companyPrefixBitLength[4]=27;
						companyPrefixBitLength[5]=24;
						companyPrefixBitLength[6]=20;

						assetRefBitLength[0]=42;
						assetRefBitLength[1]=45;
						assetRefBitLength[2]=48;
						assetRefBitLength[3]=52;
						assetRefBitLength[4]=55;
						assetRefBitLength[5]=58;
						assetRefBitLength[6]=62;

						companyPrefixDigitLength[0] = 12 ;
						companyPrefixDigitLength[1] = 11 ;
						companyPrefixDigitLength[2] = 10 ;
						companyPrefixDigitLength[3] = 9 ;
						companyPrefixDigitLength[4] = 8 ;
						companyPrefixDigitLength[5] = 7 ;
						companyPrefixDigitLength[6] = 6 ;

						assetRefDigitLength[0] = 12 ;
						assetRefDigitLength[1] = 13 ;
						assetRefDigitLength[2] = 14 ;
						assetRefDigitLength[3] = 15 ;
						assetRefDigitLength[4] = 16 ;
						assetRefDigitLength[5] = 17 ;
						assetRefDigitLength[6] = 18 ;

						break;
					}
					default:
					{
						throw new InvalidEPCFormatException("format variable with invalid value :"+format.ToString()) ;
					}
				}
			}
		#endregion
	}
}
