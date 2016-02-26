
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
using System.Text ;

namespace KTone.RFIDGlobal.EPCTagEncoding
{
	/// <summary>
	/// This class generates byteArray for ALL formats like SGTIN,SSCC,SGLN,GIAI,GRAI
	/// </summary>
    public class GenerateTestData
	{
		#region Attributes
		
		private static readonly NLog.Logger m_log = KTLogger.KTLogManager.GetLogger();
			
		private EPCFORMAT m_format  ;

		private bool m_bIs64Bit ;
		#endregion Attributes

		#region Private Methods

		public GenerateTestData(EPCFORMAT format)
		{
			//
			// TODO: Add constructor logic here
			//
			InitCtor(format) ;
		}

		public GenerateTestData(EPCFORMAT format,bool is64Bits)
		{
			//
			// TODO: Add constructor logic here
			//
			InitCtor(format,is64Bits) ;
		}

		private void InitCtor(EPCFORMAT format)
		{
			InitCtor(format,false) ;
			PartitionTable.FillPartitionTables(m_format) ;
		}


		private void InitCtor(EPCFORMAT format,bool is64Bits)
		{
			m_format = format ; 
			m_bIs64Bit = is64Bits ;
		}


		private Hashtable GetFormatSpecificFieldsBitLength(Hashtable EPCHash)  
		{
			Hashtable formatSpecificBitLenHash = new Hashtable() ;

			switch(m_format)
			{
				case EPCFORMAT.SGTIN :
				{
					formatSpecificBitLenHash.Add(Constants.HEADER,8) ;
					formatSpecificBitLenHash.Add(Constants.FILTER,3) ;
					formatSpecificBitLenHash.Add(Constants.PARTITION,3) ;
					formatSpecificBitLenHash.Add(Constants.COMPANYPREFIX,0) ;//this varies
					formatSpecificBitLenHash.Add(Constants.ITEMREFERENCE,0) ;//this varies

					if(EPCHash.ContainsKey(Constants.PARTITION))
					{
						int partitionVal = Convert.ToInt32(EPCHash[Constants.PARTITION]) ;
						formatSpecificBitLenHash[Constants.COMPANYPREFIX] =  PartitionTable.companyPrefixBitLength[partitionVal] ;
						formatSpecificBitLenHash[Constants.ITEMREFERENCE] = PartitionTable.itemRefBitLength [partitionVal] ;
					}
					formatSpecificBitLenHash.Add(Constants.SERIALNO,38) ;
					break;
				}
				case EPCFORMAT.SSCC :
				{
					formatSpecificBitLenHash.Add(Constants.HEADER,8) ;
					formatSpecificBitLenHash.Add(Constants.FILTER,3) ;
					formatSpecificBitLenHash.Add(Constants.PARTITION,3) ;
					formatSpecificBitLenHash.Add(Constants.COMPANYPREFIX,0) ;//this varies
					formatSpecificBitLenHash.Add(Constants.SERIALREF,0) ;//this varies
					if(EPCHash.ContainsKey(Constants.PARTITION))
					{
						int partitionVal = Convert.ToInt32(EPCHash[Constants.PARTITION]) ;
						formatSpecificBitLenHash[Constants.COMPANYPREFIX] = PartitionTable.companyPrefixBitLength[partitionVal] ;
						formatSpecificBitLenHash[Constants.SERIALREF] = PartitionTable.serialRefBitLength[partitionVal] ;
					}
					formatSpecificBitLenHash.Add(Constants.UNALLOCATED,25) ;
					break;
				}
				case EPCFORMAT.SGLN :
				{
					formatSpecificBitLenHash.Add(Constants.HEADER,8) ;
					formatSpecificBitLenHash.Add(Constants.FILTER,3) ;
					formatSpecificBitLenHash.Add(Constants.PARTITION,3) ;
					formatSpecificBitLenHash.Add(Constants.COMPANYPREFIX,0) ;//this varies
					formatSpecificBitLenHash.Add(Constants.LOCATIONREF,0) ;//this varies
					if(EPCHash.ContainsKey(Constants.PARTITION))
					{
						int partitionVal = Convert.ToInt32(EPCHash[Constants.PARTITION]) ;
						formatSpecificBitLenHash[Constants.COMPANYPREFIX] = PartitionTable.companyPrefixBitLength[partitionVal] ;
						formatSpecificBitLenHash[Constants.LOCATIONREF] = PartitionTable.locationRefBitLength[partitionVal] ;
					}
					formatSpecificBitLenHash.Add(Constants.SERIALNO,41) ;
					break;
				}
				case EPCFORMAT.GRAI :
				{
					formatSpecificBitLenHash.Add(Constants.HEADER,8) ;
					formatSpecificBitLenHash.Add(Constants.FILTER,3) ;
					formatSpecificBitLenHash.Add(Constants.PARTITION,3) ;
					formatSpecificBitLenHash.Add(Constants.COMPANYPREFIX,0) ;//this varies
					formatSpecificBitLenHash.Add(Constants.ASSETTYPE,0) ;//this varies
					if(EPCHash.ContainsKey(Constants.PARTITION))
					{
						int partitionVal = Convert.ToInt32(EPCHash[Constants.PARTITION]) ;
						formatSpecificBitLenHash[Constants.COMPANYPREFIX] = PartitionTable.companyPrefixBitLength[partitionVal] ;
						formatSpecificBitLenHash[Constants.ASSETTYPE] = PartitionTable.assetTypeBitLength[partitionVal] ;
					}
					formatSpecificBitLenHash.Add(Constants.SERIALNO,38) ;
					break;
				}
				case EPCFORMAT.GIAI :
				{
					formatSpecificBitLenHash.Add(Constants.HEADER,8) ;
					formatSpecificBitLenHash.Add(Constants.FILTER,3) ;
					formatSpecificBitLenHash.Add(Constants.PARTITION,3) ;
					formatSpecificBitLenHash.Add(Constants.COMPANYPREFIX,0) ;//this varies
					formatSpecificBitLenHash.Add(Constants.ASSETREF,0) ;//this varies
					if(EPCHash.ContainsKey(Constants.PARTITION))
					{
						int partitionVal = Convert.ToInt32(EPCHash[Constants.PARTITION]) ;
						formatSpecificBitLenHash[Constants.COMPANYPREFIX] = PartitionTable.companyPrefixBitLength[partitionVal] ;
						formatSpecificBitLenHash[Constants.ASSETREF] = PartitionTable.assetRefBitLength[partitionVal] ;
					}
					break;
				}
				default:
				{
					m_log.Trace("Invalid Value of 'format' variable "+m_format.ToString()) ;
					break;
				}
			}
			return formatSpecificBitLenHash ;
		}


		private Hashtable GetFormatSpecificFieldsBitLen_64(Hashtable EPCHash)
		{
			Hashtable formatSpecificBitLenHash = new Hashtable() ;

			switch(m_format)
			{
				case EPCFORMAT.SGTIN :
				{
					formatSpecificBitLenHash.Add(Constants.HEADER,2) ;
					formatSpecificBitLenHash.Add(Constants.FILTER,3) ;
					formatSpecificBitLenHash.Add(Constants.COMPANYPREFIXINDEX,14) ;
					formatSpecificBitLenHash.Add(Constants.ITEMREFERENCE,20) ;
					formatSpecificBitLenHash.Add(Constants.SERIALNO,25) ;
					break;
				}
				case EPCFORMAT.SSCC:
				{
					formatSpecificBitLenHash.Add(Constants.HEADER,8) ;
					formatSpecificBitLenHash.Add(Constants.FILTER,3) ;
					formatSpecificBitLenHash.Add(Constants.COMPANYPREFIXINDEX,14) ;
					formatSpecificBitLenHash.Add(Constants.SERIALREF,39) ;
					break;
				}

				case EPCFORMAT.SGLN:
				{
					formatSpecificBitLenHash.Add(Constants.HEADER,8) ;
					formatSpecificBitLenHash.Add(Constants.FILTER,3) ;
					formatSpecificBitLenHash.Add(Constants.COMPANYPREFIXINDEX,14) ;
					formatSpecificBitLenHash.Add(Constants.LOCATIONREF,20) ;
					formatSpecificBitLenHash.Add(Constants.SERIALNO,19) ;
					break;
				}
				case EPCFORMAT.GIAI:
				{
					formatSpecificBitLenHash.Add(Constants.HEADER,8) ;
					formatSpecificBitLenHash.Add(Constants.FILTER,3) ;
					formatSpecificBitLenHash.Add(Constants.COMPANYPREFIXINDEX,14) ;
					formatSpecificBitLenHash.Add(Constants.ASSETREF,39) ;
					break;
				}
				case EPCFORMAT.GRAI:
				{
					formatSpecificBitLenHash.Add(Constants.HEADER,8) ;
					formatSpecificBitLenHash.Add(Constants.FILTER,3) ;
					formatSpecificBitLenHash.Add(Constants.COMPANYPREFIXINDEX,14) ;
					formatSpecificBitLenHash.Add(Constants.ASSETTYPE,20) ;
					formatSpecificBitLenHash.Add(Constants.SERIALNO,19) ;
					break;
				}
				default:
				{
					m_log.Trace("GetFormatSpecificFieldsBitLen_64():Invalid Value of 'format' variable "+m_format.ToString()) ;
					break;
				}
			}
			return formatSpecificBitLenHash ;
		}

		

		private byte[] ReturnEPCByteArray(ArrayList arrList)
		{
			byte[] EPCArray = null ;
			string strEPCByteArray = string.Empty ;
			
			for(int i = 0 ; i< arrList.Count ; i++)
			{
				strEPCByteArray+= arrList[i] ;
			}
			EPCArray = RFUtils.StringToByteArray(strEPCByteArray) ;
			return EPCArray ;
		}

		private byte[] GenerateEPCByteArray(Hashtable EPCHash,string[] epcArray)
		{
			if(epcArray == null)
			{
				throw new ApplicationException("Input EPC Array is NULL") ;
			}
			string epcArrayVal = string.Empty ;
			Hashtable epcFormatFieldsBitLength = null ;

			ArrayList epcList = new ArrayList(EPCHash.Count) ;

			if(m_bIs64Bit)
			{
				epcFormatFieldsBitLength = GetFormatSpecificFieldsBitLen_64(EPCHash) ;				
			}
			else
				epcFormatFieldsBitLength = GetFormatSpecificFieldsBitLength(EPCHash) ;
						
			for (int i = 0; i < epcArray.Length ; i++)
			{
				epcArrayVal = epcArray[i] ;
				switch(epcArrayVal)
				{
					case Constants.HEADER:
					{
						epcList.Insert(i,Convert.ToString(EPCHash[epcArrayVal]));
						break;
					}
					case Constants.FILTER:
					{
						int filterval  = Convert.ToInt32(EPCHash[epcArrayVal]) ;
						string tempFilterVal = Convert.ToString(filterval,2);
						int filterLen = Convert.ToInt32(epcFormatFieldsBitLength[epcArrayVal]) ;
						if(tempFilterVal.Length < filterLen)
						{
							tempFilterVal = tempFilterVal.PadLeft(filterLen,'0') ;
						}
						epcList.Insert(i,tempFilterVal ); 
						break;
					}
					case Constants.PARTITION:
					{
						int partVal  = Convert.ToInt32(EPCHash[epcArrayVal]) ;
						string tempPartVal = Convert.ToString(partVal,2);
						int partLen = Convert.ToInt32(epcFormatFieldsBitLength[epcArrayVal]) ;
						if(tempPartVal.Length < partLen)
						{
							tempPartVal = tempPartVal.PadLeft(partLen,'0') ;
						}
						epcList.Insert(i,tempPartVal) ;
						break;
					}
					case Constants.COMPANYPREFIXINDEX:
					case Constants.COMPANYPREFIX:
					{
						Int64 companyPrefixVal  = Convert.ToInt64(EPCHash[epcArrayVal]) ;
						string tempcompanyPrefixVal = Convert.ToString(companyPrefixVal,2);
						int companyPrefixLen = Convert.ToInt32(epcFormatFieldsBitLength[epcArrayVal]) ;
						
						if(tempcompanyPrefixVal.Length < companyPrefixLen)
						{
							tempcompanyPrefixVal = tempcompanyPrefixVal.PadLeft(companyPrefixLen,'0') ;
						}

						epcList.Insert(i,tempcompanyPrefixVal) ;
						break;
					}
					case Constants.ASSETREF://for GIAI
					case Constants.ASSETTYPE:// for GRAI
					case Constants.LOCATIONREF://for SGLN
					case Constants.SERIALREF://for SSCC
					case Constants.ITEMREFERENCE://for SGTIN
					{
						Int64 itemRefVal  = Convert.ToInt64(EPCHash[epcArrayVal]) ;
						string tempitemRefVal = Convert.ToString(itemRefVal,2);
						int itemRefValLen = Convert.ToInt32(epcFormatFieldsBitLength[epcArrayVal]) ;
						
						if(tempitemRefVal.Length < itemRefValLen)
						{
							tempitemRefVal = tempitemRefVal.PadLeft(itemRefValLen,'0') ;
						}

						epcList.Insert(i,tempitemRefVal) ;
						break;
					}
					case Constants.UNALLOCATED:// for SSCC
					{
						//Taking the number of unallocated digits from the EPChash instead of
						//fieldsLength hashtable
						int unAllocLen = Convert.ToInt32(epcFormatFieldsBitLength[epcArrayVal]) ;
						string unAllocValue = string.Empty ;
						unAllocValue = unAllocValue.PadLeft(unAllocLen,'0') ;
						epcList.Insert(i,unAllocValue) ;
						break;
					}
					case Constants.SERIALNO: //for SGTIN ,SGLN
					{
						int serialVal  = Convert.ToInt32(EPCHash[epcArrayVal]) ;
						string tempserialVal = Convert.ToString(serialVal,2);
						int serialValLen = Convert.ToInt32(epcFormatFieldsBitLength[epcArrayVal]) ;
						
						if(tempserialVal.Length < serialValLen)
						{
							tempserialVal = tempserialVal.PadLeft(serialValLen,'0') ;
						}

						epcList.Insert(i,tempserialVal) ;
						break;
					}
					default:
					{
						m_log.Warn("Parameter entered is invalid :"+epcArrayVal) ;
						break;
					}
				}//switch ends here
			}
			return ReturnEPCByteArray(epcList)  ;
		}

		private string[] GetParameters()
		{
			string[] paramArray = null ; 

			switch(m_format)
			{
				case EPCFORMAT.SGTIN:
				{
					if(m_bIs64Bit)
					{
						paramArray = new string[]
						{
							Constants.HEADER,Constants.FILTER,
							Constants.COMPANYPREFIXINDEX,Constants.ITEMREFERENCE,Constants.SERIALNO
						};
					}
					else
					{
							paramArray = new string[]
						{
							Constants.HEADER,Constants.FILTER,Constants.PARTITION,
							Constants.COMPANYPREFIX,Constants.ITEMREFERENCE,Constants.SERIALNO
						};
					}
					break;
				}
				case EPCFORMAT.SSCC:
				{
					if(m_bIs64Bit)
					{
						paramArray = new string[]
						{
							Constants.HEADER,Constants.FILTER,
							Constants.COMPANYPREFIXINDEX,Constants.SERIALREF,Constants.UNALLOCATED
						};
					}
					else
					{
						paramArray = new string[]
						{
							Constants.HEADER,Constants.FILTER,Constants.PARTITION,
							Constants.COMPANYPREFIX,Constants.SERIALREF,Constants.UNALLOCATED
						};
					}
					break;
				}
				case EPCFORMAT.SGLN:
				{
					if(m_bIs64Bit)
					{
						paramArray = new string[]
						{
							Constants.HEADER,Constants.FILTER,
							Constants.COMPANYPREFIXINDEX,Constants.LOCATIONREF,Constants.SERIALNO
						};
					}
					else
					{
						paramArray = new string[]
						{
							Constants.HEADER,Constants.FILTER,Constants.PARTITION,
							Constants.COMPANYPREFIX,Constants.LOCATIONREF,Constants.SERIALNO
						};
					}
					break;
				}
				case EPCFORMAT.GRAI:
				{
					if(m_bIs64Bit)
					{
						paramArray = new string[]
						{
							Constants.HEADER,Constants.FILTER,
							Constants.COMPANYPREFIXINDEX,Constants.ASSETTYPE,Constants.SERIALNO
						};
					}
					else
					{
						paramArray = new string[]
						{
							Constants.HEADER,Constants.FILTER,Constants.PARTITION,
							Constants.COMPANYPREFIX,Constants.ASSETTYPE,Constants.SERIALNO
						};
					}
					break;
				}
				case EPCFORMAT.GIAI:
				{
					if(m_bIs64Bit)
					{
						paramArray = new string[]
							{
								Constants.HEADER,Constants.FILTER,
								Constants.COMPANYPREFIXINDEX,Constants.ASSETREF
							};
					}
					else
					{
						paramArray = new string[]
							{
								Constants.HEADER,Constants.FILTER,Constants.PARTITION,
								Constants.COMPANYPREFIX,Constants.ASSETREF
							};
					}
					break;
				}
				default:
				{
					m_log.Warn("GetParameters(): Value not right for variable of type 'EPCFORMAT' :"+m_format.ToString()) ;
					break;
				}
			}
			return paramArray ;
		}
		#endregion Private Methods

		#region  Public Methods

		public byte[] EncodeTestEPCByteArray(Hashtable EPCHash)
		{
			//Generate the parameter array depending on EPC format.
			string[] epcFormatParamStrArray = GetParameters() ;
			byte[] epcFormatByteArr = GenerateEPCByteArray(EPCHash,epcFormatParamStrArray) ;
			return epcFormatByteArr ;
		}
		#endregion Public Methods
	}
}
