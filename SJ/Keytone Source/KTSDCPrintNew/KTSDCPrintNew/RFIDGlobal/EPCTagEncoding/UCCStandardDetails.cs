
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
using System.Collections;

namespace KTone.RFIDGlobal.EPCTagEncoding
{
	
	public struct EPCFieldDetail
	{
		public string FieldName;
		public string FieldDataType;
		public string FieldLength;

		public EPCFieldDetail(string fieldName, string fieldDataType, string fieldLength)
		{
			FieldName = fieldName;
			FieldDataType = fieldDataType;
			FieldLength = fieldLength;
		}
	}

	/// <summary>
	/// Summary description for UCCStandardDetails.
	/// </summary>	

	public class UCCStandardDetails
	{
		private EPCFORMAT m_UCCStandard; 
		public UCCStandardDetails(EPCFORMAT UCCStandard)
		{
			//
			// TODO: Add constructor logic here
			//
			m_UCCStandard = UCCStandard;
			//PartitionTable.FillPartitionTables(UCCStandard) ;
		}

		public void get96BitTagFields(out ArrayList fieldList)
		{
			fieldList = new ArrayList();

			switch(m_UCCStandard)
			{
				case EPCFORMAT.SGTIN:
					fieldList.Add(new EPCFieldDetail(Constants.FILTER,"digit","1"));
					fieldList.Add(new EPCFieldDetail(Constants.COMPANYPREFIX,"digit","12"));
					fieldList.Add(new EPCFieldDetail(Constants.INDICATOR,"digit","1"));
					fieldList.Add(new EPCFieldDetail(Constants.ITEMREFERENCE,"digit","6"));
					fieldList.Add(new EPCFieldDetail(Constants.SERIALNO,"digit","12"));

					fieldList.Add(new EPCFieldDetail("Any_SGTIN", "bool", "1"));
					break;
				case EPCFORMAT.SSCC:
					fieldList.Add(new EPCFieldDetail(Constants.FILTER,"digit","1"));
					fieldList.Add(new EPCFieldDetail(Constants.COMPANYPREFIX,"digit","12"));
					fieldList.Add(new EPCFieldDetail(Constants.INDICATOR,"digit","1"));
					fieldList.Add(new EPCFieldDetail(Constants.SERIALREF,"digit","10"));

					fieldList.Add(new EPCFieldDetail("Any_SSCC", "bool", "1"));
					break;
				case EPCFORMAT.SGLN:
					fieldList.Add(new EPCFieldDetail(Constants.FILTER,"digit","1"));
					fieldList.Add(new EPCFieldDetail(Constants.COMPANYPREFIX,"digit","12"));
					fieldList.Add(new EPCFieldDetail(Constants.LOCATIONREF,"digit","6"));
					fieldList.Add(new EPCFieldDetail(Constants.SERIALNO,"digit","13"));

					fieldList.Add(new EPCFieldDetail("Any_SGLN", "bool", "1"));
					break;
				case EPCFORMAT.GRAI:
					fieldList.Add(new EPCFieldDetail(Constants.FILTER,"digit","1"));
					fieldList.Add(new EPCFieldDetail(Constants.COMPANYPREFIX,"digit","12"));
					fieldList.Add(new EPCFieldDetail(Constants.ASSETTYPE,"digit","6"));
					fieldList.Add(new EPCFieldDetail(Constants.SERIALNO,"digit","12"));
					
					fieldList.Add(new EPCFieldDetail("Any_GRAI", "bool", "1"));
					break;
				case EPCFORMAT.GIAI:
					fieldList.Add(new EPCFieldDetail(Constants.FILTER,"digit","1"));
					fieldList.Add(new EPCFieldDetail(Constants.COMPANYPREFIX,"digit","12"));
					fieldList.Add(new EPCFieldDetail(Constants.ASSETREF,"digit","18"));

					fieldList.Add(new EPCFieldDetail("Any_GIAI", "bool", "1"));
					break;
				case EPCFORMAT.GID:
					fieldList.Add(new EPCFieldDetail(Constants.MANUFACTID,"digit","9"));
					fieldList.Add(new EPCFieldDetail(Constants.PRODUCTID,"digit","8"));
					fieldList.Add(new EPCFieldDetail(Constants.SERIALNO,"digit","11"));
					
					fieldList.Add(new EPCFieldDetail("Any_GID", "bool", "1"));
					break;
				case EPCFORMAT.USDOD:
					fieldList.Add(new EPCFieldDetail(Constants.FILTER,"digit","2"));
					fieldList.Add(new EPCFieldDetail(Constants.CAGECODEORDODAAC,"string","40")); // 5 alpha numeric chars
					fieldList.Add(new EPCFieldDetail(Constants.SERIALNO,"digit","11"));
					
					fieldList.Add(new EPCFieldDetail("Any_USDOD", "bool", "1"));
					break;
                case EPCFORMAT.LCTN:
                    fieldList.Add(new EPCFieldDetail(Constants.COMPANYPREFIX, "digit", "12"));
                    fieldList.Add(new EPCFieldDetail(Constants.SERIALNO, "digit", "13"));

                    fieldList.Add(new EPCFieldDetail("Any_LCTN", "bool", "1"));
                    break;
                case EPCFORMAT.ASET:
                    fieldList.Add(new EPCFieldDetail(Constants.COMPANYPREFIX, "digit", "12"));
                    fieldList.Add(new EPCFieldDetail(Constants.SERIALNO, "digit", "13"));

                    fieldList.Add(new EPCFieldDetail("Any_ASET", "bool", "1"));
                    break;
				case EPCFORMAT.UNKNOWN:
					fieldList.Add(new EPCFieldDetail("Any_UNKNOWN", "bool", "1"));
					break;

			}
		}


		public void get64BitTagFields(out ArrayList fieldList)
		{
			fieldList = new ArrayList();

			switch(m_UCCStandard)
			{
				case EPCFORMAT.SGTIN:
					fieldList.Add(new EPCFieldDetail(Constants.FILTER,"digit","1"));
					fieldList.Add(new EPCFieldDetail(Constants.COMPANYPREFIX,"digit","12"));
					fieldList.Add(new EPCFieldDetail(Constants.INDICATOR,"digit","1"));
					fieldList.Add(new EPCFieldDetail(Constants.ITEMREFERENCE,"digit","6"));
					fieldList.Add(new EPCFieldDetail(Constants.SERIALNO,"digit","8"));

					fieldList.Add(new EPCFieldDetail("Any_SGTIN", "bool", "1"));
					break;
				case EPCFORMAT.SSCC:
					fieldList.Add(new EPCFieldDetail(Constants.FILTER,"digit","1"));
					fieldList.Add(new EPCFieldDetail(Constants.COMPANYPREFIX,"digit","12"));
					fieldList.Add(new EPCFieldDetail(Constants.INDICATOR,"digit","1"));
					fieldList.Add(new EPCFieldDetail(Constants.SERIALREF,"digit","10"));

					fieldList.Add(new EPCFieldDetail("Any_SSCC", "bool", "1"));
					break;
				case EPCFORMAT.SGLN:
					fieldList.Add(new EPCFieldDetail(Constants.FILTER,"digit","1"));
					fieldList.Add(new EPCFieldDetail(Constants.COMPANYPREFIX,"digit","12"));
					fieldList.Add(new EPCFieldDetail(Constants.LOCATIONREF,"digit","6"));
					fieldList.Add(new EPCFieldDetail(Constants.SERIALNO,"digit","6"));

					fieldList.Add(new EPCFieldDetail("Any_SGLN", "bool", "1"));
					break;
				case EPCFORMAT.GRAI:
					fieldList.Add(new EPCFieldDetail(Constants.FILTER,"digit","1"));
					fieldList.Add(new EPCFieldDetail(Constants.COMPANYPREFIX,"digit","12"));
					fieldList.Add(new EPCFieldDetail(Constants.ASSETTYPE,"digit","6"));
					fieldList.Add(new EPCFieldDetail(Constants.SERIALNO,"digit","6"));
					
					fieldList.Add(new EPCFieldDetail("Any_GRAI", "bool", "1"));
					break;
				case EPCFORMAT.GIAI:
					fieldList.Add(new EPCFieldDetail(Constants.FILTER,"digit","1"));
					fieldList.Add(new EPCFieldDetail(Constants.COMPANYPREFIX,"digit","12"));
					fieldList.Add(new EPCFieldDetail(Constants.ASSETREF,"digit","12"));

					fieldList.Add(new EPCFieldDetail("Any_GIAI", "bool", "1"));
					break;
				case EPCFORMAT.GID:
					fieldList.Add(new EPCFieldDetail(Constants.MANUFACTID,"digit","9"));
					fieldList.Add(new EPCFieldDetail(Constants.PRODUCTID,"digit","8"));
					fieldList.Add(new EPCFieldDetail(Constants.SERIALNO,"digit","11"));
					
					fieldList.Add(new EPCFieldDetail("Any_GID", "bool", "1"));
					break;
				case EPCFORMAT.USDOD:
					fieldList.Add(new EPCFieldDetail(Constants.FILTER,"digit","1"));
					fieldList.Add(new EPCFieldDetail(Constants.CAGECODEORDODAAC,"string","40")); //5 Alpha numeric characters
					fieldList.Add(new EPCFieldDetail(Constants.SERIALNO,"digit","8"));
					
					fieldList.Add(new EPCFieldDetail("Any_USDOD", "bool", "1"));
					break;
				case EPCFORMAT.UNKNOWN:
					fieldList.Add(new EPCFieldDetail("Any_UNKNOWN", "bool", "1"));
					break;
			}
		}

	}
}
