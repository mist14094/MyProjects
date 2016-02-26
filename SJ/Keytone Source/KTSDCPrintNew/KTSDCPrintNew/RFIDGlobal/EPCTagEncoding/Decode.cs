
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
using System.Collections.Generic;

namespace KTone.RFIDGlobal.EPCTagEncoding
{
	/// <summary>
	/// This class provides an API for decoding the bytes into 
	/// EPC Global standards compliant categories of tags.
	/// </summary>
	public class EPCBytes
	{
		#region Attributes
		static URIFORMAT m_URIFormat  = URIFORMAT.TAG; 
		static NLog.Logger m_log 
			= KTLogger.KTLogManager.GetLogger();
		#endregion Attributes ENDS
		
		#region Private Methods

        public static EPCFORMAT FindFormat(byte headerByte, out string noOfBits)
		{
			EPCFORMAT format = EPCFORMAT.UNKNOWN ;
			noOfBits	= string.Empty ;
			try 
			{
                byte lctnHeader = Convert.ToByte(LCTNHelper.LCTN_HEADER,2);
                byte asetHeader = Convert.ToByte(LCTNHelper.ASET_HEADER, 2);
                if (headerByte == lctnHeader)
                {
                    format = EPCFORMAT.LCTN;
                    noOfBits = "96";
                    return format;
                }
                if (headerByte == asetHeader)
                {
                    format = EPCFORMAT.ASET;
                    noOfBits = "96";
                    return format;
                }
			
				int header = Convert.ToInt32(headerByte) ;

				switch(header)
				{
					case 48: //30 0011 0000
					{
						format = EPCFORMAT.SGTIN ;
						noOfBits = "96";
						break;
					}
					case 49: //31 0011 0001
					{
						format = EPCFORMAT.SSCC ;
						noOfBits = "96";
						break;
					}
					case 50: //32 0011 0010
					{
						format = EPCFORMAT.SGLN ;
						noOfBits = "96";
						break;
					}
					case 51: //33 0011 0011
					{
						format = EPCFORMAT.GRAI;
						noOfBits = "96";
						break;
					}
					case 52: //34 0011 0100
					{
						format = EPCFORMAT.GIAI;
						noOfBits = "96";
						break;
					}
					case 53: //35 0011 0101 96 bit
					{
						format = EPCFORMAT.GID;
						noOfBits = "96";
						break;
					}
					case 47: //47 00101111	 96 bit
					{
						format = EPCFORMAT.USDOD ;
						noOfBits = "96";
						break;
					}
					case 8: //8 0000 1000 64 bit
					{
						format = EPCFORMAT.SSCC ;
						noOfBits = "64";
						break;
					}
					case 9: //9 0000 1001 64-bit
					{
						format = EPCFORMAT.SGLN;
						noOfBits = "64";
						break;
					}
					case 10: //10 0000 1010 64-bit
					{
						format = EPCFORMAT.GRAI;
						noOfBits = "64";
						break;
					}
				
					case 11: //11 0000 1011 64-bit
					{
						format = EPCFORMAT.GIAI ;
						noOfBits = "64";
						break;
					}

					case 206: //    1100 1110 64-bit
					{
						format = EPCFORMAT.USDOD ;
						noOfBits = "64";
						break;
					}
					default:
					{
						format = GetUndefinedFormat(headerByte,out noOfBits) ;
					
						if(format == EPCFORMAT.UNKNOWN)
						{
                            //Exception not needed
							//throw new InvalidEPCFormatException("FindFormat() : Byte Array not in EPC Format, value"+header.ToString());
						}
						break;
					}
				}
			}
			catch ( Exception ex ) 
			{
				m_log.Trace("EPCFORMAT:: FINDFORMAT" + ex.Message ) ; 
					// ignore any exception and just return the unknown 
			}
			return format ;
		}

        public static EPCFORMAT GetUndefinedFormat(byte headerByte, out string noOfBits)
		{
			EPCFORMAT undefFormat = EPCFORMAT.UNKNOWN ;
			string firstByte = Convert.ToString(headerByte,2).Trim();
			noOfBits = string.Empty ;
			
			if(firstByte.StartsWith("10")) //for SGTIN-64
			{
				undefFormat = EPCFORMAT.SGTIN ;
				noOfBits = Constants.STRREPRESENT64;
			}
			return undefFormat ;
		}

        public static string GetFormatSpecificString()
		{
			string uriFormattedString = string.Empty;
			switch(m_URIFormat)
			{
				case URIFORMAT.TAG:
				{
					uriFormattedString = Constants.URITAG ;
					break;
				}
				case URIFORMAT.ID:
				{
					uriFormattedString = Constants.URIID ;
					break;
				}
				case URIFORMAT.RAW:
				{
					uriFormattedString = Constants.URIRAW ;
					break;
				}
				case URIFORMAT.PAT:
				{
					uriFormattedString = Constants.URIPAT ;
					break;
				}
				default:
				{
					m_log.Warn("FormatSpecificString()");
					break;
				}
			}
			return uriFormattedString ;
		}

        private static string IncrementSerialNoInt(string serialRef, int serialNoDiff)
        {
            int serialRefLen = serialRef.Length;
            int serialRefNext = Convert.ToInt32(serialRef) + serialNoDiff;
            serialRef = serialRefNext.ToString().PadLeft(serialRefLen, '0');
            return serialRef;
        }

        private static string IncrementSerialNoLong(string serialRef, int serialNoDiff)
        {
            int serialRefLen = serialRef.Length;
            Int64 serialRefNext = Convert.ToInt64(serialRef) + serialNoDiff;
            serialRef = serialRefNext.ToString().PadLeft(serialRefLen, '0');
            return serialRef;
        }

		#endregion Private Methods ENDS

		#region Public Methods

		
		private EPCBytes()
		{
			//
			// TODO: Add constructor logic here
			//
		}

        public static string Decode(byte[] toDecodeBytes)
        { 
            string errorMessage = string.Empty;
            return Decode(toDecodeBytes, false, out errorMessage);
        }

        public static string Decode(byte[] toDecodeBytes, bool throwException)
        {
            string errorMessage = string.Empty;
            return Decode(toDecodeBytes, throwException, out errorMessage);
        }

		/// <summary>
		/// Converts 8 or 12 byte data to as per EPC Global URN SSCC,GTIN,SGTIN,GID etc. 
		/// The structure of urn string is given in EPC Gloabal Standard Manual for every specific standard
		/// urn:epc:tag:sgtin-64:3.0652642.800031.400 
		/// </summary>
		/// <param name="toDecodeBytes"></param>
		/// <returns></returns>
        public static string Decode(byte[] toDecodeBytes, bool throwException, out string errorMessage)
		{
			StringBuilder strURIBldr = new StringBuilder();
			string strURI = string.Empty ;
			string numOfBits = string.Empty ;
			string literalsString = string.Empty ;
            errorMessage = string.Empty;
            
			try
			{
				EPCFORMAT epcFormat = FindFormat(toDecodeBytes[0],out numOfBits) ;
				
				if(epcFormat == EPCFORMAT.UNKNOWN)
				{
					//throw new InvalidEPCFormatException("Decode(): Invalid EPC tag Format: Exception Value :"+epcFormat.ToString()) ;
                    return GetRawURI(toDecodeBytes);
				}

				strURIBldr.Append(Constants.EPCURIPREFIX );
				literalsString+= "-"+numOfBits+":";
				switch(epcFormat)
				{
					case EPCFORMAT.SSCC:
					{
						strURIBldr.Append(GetFormatSpecificString());
						strURIBldr.Append(Constants.SSCCURI);
						strURIBldr.Append(literalsString);
						string	companyPrefixRet = string.Empty ;
						string serialRefRet = string.Empty ;
						byte  filterVal = 0; 
						string extensionDigit = string.Empty ;

						if(numOfBits.Equals(Constants.STRREPRESENT96))
						{
                            if (!DecodeSSCC.DecodeSSCC96(toDecodeBytes, out companyPrefixRet, out serialRefRet, out filterVal, throwException, out errorMessage))
                                return GetRawURI(toDecodeBytes);
						}
						else
						{
                            if (!DecodeSSCC.DecodeSSCC64(toDecodeBytes, out extensionDigit, out companyPrefixRet, out serialRefRet, out filterVal, throwException, out errorMessage))
                                return GetRawURI(toDecodeBytes);
							serialRefRet = extensionDigit+serialRefRet ;
						}
						strURIBldr.Append(filterVal.ToString());
						strURIBldr.Append(".");
						strURIBldr.Append(companyPrefixRet);
						strURIBldr.Append(".");
						strURIBldr.Append(serialRefRet);
						break;
					}

					case EPCFORMAT.SGTIN:
					{
						strURIBldr.Append(GetFormatSpecificString());
						strURIBldr.Append(Constants.SGTINURI);
						strURIBldr.Append(literalsString);
						string	companyPrefixRet = string.Empty ;
						string serialNoRet = string.Empty ;
						string itemRef = string.Empty ;
						byte  filterVal = 0; 
						string indicatorDigit = string.Empty ;

						if(numOfBits.Equals(Constants.STRREPRESENT96))
						{
                            if (!DecodeSGTIN.DecodeSGTIN96(toDecodeBytes, out companyPrefixRet, out itemRef, out serialNoRet, out filterVal, throwException, out errorMessage))
                                return GetRawURI(toDecodeBytes);
						}
						else
						{
                            if (!DecodeSGTIN.DecodeSGTIN64(toDecodeBytes, out companyPrefixRet,
                                out itemRef, out serialNoRet, out filterVal, throwException, out errorMessage))
                                return GetRawURI(toDecodeBytes);
						}
						strURIBldr.Append(filterVal.ToString());
						strURIBldr.Append(".");
						strURIBldr.Append(companyPrefixRet);
						strURIBldr.Append(".");
						strURIBldr.Append(itemRef);
						strURIBldr.Append(".");
						strURIBldr.Append(serialNoRet);
						break;
					}
					case EPCFORMAT.SGLN:
					{
						strURIBldr.Append(GetFormatSpecificString());
						strURIBldr.Append(Constants.SGLNURI);
						strURIBldr.Append(literalsString);
						string	companyPrefixRet = string.Empty ;
						string serialNoRet = string.Empty ;
						string locationRef = string.Empty ;
						byte  filterVal = 0; 

						if(numOfBits.Equals(Constants.STRREPRESENT96))
						{
                            if (!DecodeSGLN.DecodeSGLN96(toDecodeBytes, out companyPrefixRet, out locationRef, out serialNoRet, out filterVal, throwException, out errorMessage)) 
                                return GetRawURI(toDecodeBytes);
						}
						else
						{
                            if (!DecodeSGLN.DecodeSGLN64(toDecodeBytes, out companyPrefixRet, out locationRef, out serialNoRet, out filterVal, throwException, out errorMessage))
                                return GetRawURI(toDecodeBytes);
						}
						strURIBldr.Append(filterVal.ToString());
						strURIBldr.Append(".");
						strURIBldr.Append(companyPrefixRet);
						strURIBldr.Append(".");
						strURIBldr.Append(locationRef);
						strURIBldr.Append(".");
						strURIBldr.Append(serialNoRet);
						break;
					}
					case EPCFORMAT.GID:
					{
						strURIBldr.Append(GetFormatSpecificString());
						strURIBldr.Append(Constants.GIDURI);
						strURIBldr.Append(literalsString);
						string	managerNumberRet = string.Empty ;
						string objectClassRet = string.Empty ;
						string serialNumRet = string.Empty ;

                        if (!GID.Decode96(toDecodeBytes, out managerNumberRet, out objectClassRet, out serialNumRet, throwException, out errorMessage))
                                return GetRawURI(toDecodeBytes);
						strURIBldr.Append(managerNumberRet);
						strURIBldr.Append(".");
						strURIBldr.Append(objectClassRet);
						strURIBldr.Append(".");
						strURIBldr.Append(serialNumRet);
						break;
					}
					case EPCFORMAT.GIAI:
					{
						strURIBldr.Append(GetFormatSpecificString());
						strURIBldr.Append(Constants.GIAIURI);
						strURIBldr.Append(literalsString);
						string	companyPrefixRet = string.Empty ;
						string assetRefRet = string.Empty ;
						byte  filterVal = 0; 

						if(numOfBits.Equals(Constants.STRREPRESENT96))
						{
                            if (!DecodeGIAI.DecodeGIAI96(toDecodeBytes, out companyPrefixRet, out assetRefRet, out filterVal, throwException, out errorMessage))
                                return GetRawURI(toDecodeBytes);
						}
						else
						{
                            if (!DecodeGIAI.DecodeGIAI64(toDecodeBytes, out companyPrefixRet, out assetRefRet, out filterVal, throwException, out errorMessage))
                                return GetRawURI(toDecodeBytes);
						}

						strURIBldr.Append(filterVal.ToString());
						strURIBldr.Append(".");
						strURIBldr.Append(companyPrefixRet);
						strURIBldr.Append(".");
						strURIBldr.Append(assetRefRet);
						break;
					}
					case EPCFORMAT.GRAI:
					{
						strURIBldr.Append(GetFormatSpecificString());
						strURIBldr.Append(Constants.GRAIURI);
						strURIBldr.Append(literalsString);
						string	companyPrefixRet = string.Empty ;
						string assetType = string.Empty ;
						byte  filterVal = 0; 
						string serialNumRet = string.Empty ;

						if(numOfBits.Equals(Constants.STRREPRESENT96))
						{
                            if (!DecodeGRAI.DecodeGRAI96(toDecodeBytes, out companyPrefixRet, out assetType, out filterVal, out serialNumRet, throwException, out errorMessage)) 
                                return GetRawURI(toDecodeBytes);
						}
						else
						{
                            if (!DecodeGRAI.DecodeGRAI64(toDecodeBytes, out companyPrefixRet, out assetType, out serialNumRet, out filterVal, throwException, out errorMessage))
                                return GetRawURI(toDecodeBytes);
						}

						strURIBldr.Append(filterVal.ToString());
						strURIBldr.Append(".");
						strURIBldr.Append(companyPrefixRet);
						strURIBldr.Append(".");
						strURIBldr.Append(assetType);
						strURIBldr.Append(".");
						strURIBldr.Append(serialNumRet);
						break;
					}
					case EPCFORMAT.USDOD:
					{
						strURIBldr.Append(GetFormatSpecificString());
						strURIBldr.Append(Constants.USDODURI);
						strURIBldr.Append(literalsString);
						string	strFilterVal = string.Empty ;
						string strCageNum = string.Empty ;
						string  strSerialNum = string.Empty ;
						
						if(numOfBits.Equals(Constants.STRREPRESENT96))
						{
                            if (!DecodeDoD.DecodeDoD96(toDecodeBytes, out strFilterVal, out strCageNum, out strSerialNum, throwException, out errorMessage)) 
                                return GetRawURI(toDecodeBytes);
						}
						else
						{
                            if (!DecodeDoD.DecodeDoD64(toDecodeBytes, out strFilterVal, out strCageNum, out strSerialNum, throwException, out errorMessage))
                                return GetRawURI(toDecodeBytes);
						}

						strURIBldr.Append(strFilterVal);
						strURIBldr.Append(".");
						strURIBldr.Append(strCageNum);
						strURIBldr.Append(".");
						strURIBldr.Append(strSerialNum);
						break;
					}
                    case EPCFORMAT.LCTN:
                    {
                        strURIBldr.Append(GetFormatSpecificString());
                        strURIBldr.Append(LCTNHelper.LCTNURI);
                        strURIBldr.Append(literalsString);
                        string companyPrefixRet = string.Empty;
                        string serialNoRet = string.Empty;
                        string locationRef = string.Empty;
                        byte filterVal = 0;

                        if (numOfBits.Equals(Constants.STRREPRESENT96))
                        {
                            if (!DecodeLCTN.DecodeLCTN96(toDecodeBytes, out companyPrefixRet, out locationRef, out serialNoRet, out filterVal, throwException, out errorMessage))
                                return GetRawURI(toDecodeBytes);
                        }
                        else
                        {
                            if (!DecodeLCTN.DecodeLCTN64(toDecodeBytes, out companyPrefixRet, out locationRef, out serialNoRet, out filterVal, throwException, out errorMessage))
                                return GetRawURI(toDecodeBytes);
                        }
                        strURIBldr.Append(filterVal.ToString());
                        strURIBldr.Append(".");
                        strURIBldr.Append(companyPrefixRet);
                        strURIBldr.Append(".");
                        strURIBldr.Append(locationRef);
                        strURIBldr.Append(".");
                        strURIBldr.Append(serialNoRet);
                        break;
                    }
                    case EPCFORMAT.ASET:
                    {
                        strURIBldr.Append(GetFormatSpecificString());
                        strURIBldr.Append(LCTNHelper.ASETURI);
                        strURIBldr.Append(literalsString);
                        string companyPrefixRet = string.Empty;
                        string serialNoRet = string.Empty;
                        string locationRef = string.Empty;
                        byte filterVal = 0;

                        if (numOfBits.Equals(Constants.STRREPRESENT96))
                        {
                            if (!DecodeASET.DecodeASET96(toDecodeBytes, out companyPrefixRet, out locationRef, out serialNoRet, out filterVal, throwException, out errorMessage))
                                return GetRawURI(toDecodeBytes);
                        }
                        else
                        {
                            if (!DecodeASET.DecodeASET64(toDecodeBytes, out companyPrefixRet, out locationRef, out serialNoRet, out filterVal, throwException, out errorMessage))
                                return GetRawURI(toDecodeBytes);
                        }
                        strURIBldr.Append(filterVal.ToString());
                        strURIBldr.Append(".");
                        strURIBldr.Append(companyPrefixRet);
                        strURIBldr.Append(".");
                        strURIBldr.Append(locationRef);
                        strURIBldr.Append(".");
                        strURIBldr.Append(serialNoRet);
                        break;
                    }
					default:
					{
						m_log.Warn("Decode(): Unacceptable value returned after calling FindFormat().Error Value: "+epcFormat.ToString()) ;
                        try
                        {
                            if(toDecodeBytes!=null)
                                m_log.Warn(Encoding.ASCII.GetString(toDecodeBytes));
                        }
                        catch { }
                        //Exception not needed, return raw urn
						//throw new InvalidEPCFormatException("Decode(): Unacceptable value calling FindFormat().Error Value: "+epcFormat.ToString());
                        return GetRawURI(toDecodeBytes);
					}
				}
			}
			catch(Exception ex)
			{
                m_log.TraceException("Error:", ex);
                return GetRawURI(toDecodeBytes);
			}
			return strURIBldr.ToString() ;
		}

        public static void DecodeURN(string strEPCurn,out string uccStandard, out string bitLength,
            out string compPrefix, out string filter, out string extDigit, out string srNo,
            out string assetRef, out string cageNo, out string indDigit, out string itemRef,out string assetType,
            out string locRef, out string manufactureId, out string productId)
        {
            uccStandard = string.Empty;
            filter = string.Empty;
            compPrefix = string.Empty;
            extDigit = string.Empty;
            srNo = string.Empty;
            bitLength = string.Empty;
            assetRef = string.Empty;
            indDigit = string.Empty;
            itemRef = string.Empty;
            assetType = string.Empty;
            locRef = string.Empty;
            manufactureId = string.Empty;
            productId = string.Empty;
            cageNo = string.Empty;

            string[] urnParts = strEPCurn.Split(new char[] { ':' }, 5);
            uccStandard = urnParts[3];
            bitLength = uccStandard.Substring(uccStandard.Length - 2, 2);

            switch (uccStandard.Substring(0, uccStandard.Length - 3))
            {
                case "sscc":
                    {
                        DecodeSSCCURN(strEPCurn,out uccStandard, out bitLength,out compPrefix, 
                            out filter, out extDigit, out srNo);
                        break;
                    }
                case "giai":
                    {
                        DecodeGIAIURN(strEPCurn, out uccStandard, out bitLength,out compPrefix, 
                            out filter, out assetRef);
                        break;
                    }
                case "usdod":
                    {
                        DecodeUSDODURN(strEPCurn, out uccStandard, out bitLength,out filter, 
                            out srNo,out cageNo);
                        break;
                    }

                case "sgtin":
                    {
                        DecodeSGTINURN(strEPCurn, out uccStandard, out bitLength,out compPrefix, 
                            out filter, out srNo,out indDigit, out itemRef);
                        break;
                    }
                case "grai":
                    {
                        DecodeGRAIURN(strEPCurn, out uccStandard, out bitLength, out compPrefix,
                            out filter, out srNo, out assetType);
                        break;
                    }
                case "sgln":
                    {
                       DecodeSGLNURN(strEPCurn, out uccStandard, out bitLength,out compPrefix, 
                           out filter, out srNo,out locRef);
                        break;
                    }
                case "gid":
                    {
                        DecodeGIDURN(strEPCurn, out uccStandard, out bitLength,out srNo,out manufactureId, 
                            out productId);
                        break;
                    }
                case "lctn":
                    {
                        DecodeLCTNURN(strEPCurn, out uccStandard, out bitLength, out compPrefix,out srNo,out locRef);
                        break;
                    }
                case "aset":
                    {
                        DecodeASETURN(strEPCurn, out uccStandard, out bitLength, out compPrefix,out srNo, out filter);
                        break;
                    }
            }
        }

        public static void DecodeURN(string strEPCurn, out Dictionary<string, string> decodedData)
        {
            decodedData = new Dictionary<string, string>();
            string filter = string.Empty;
            string compPrefix = string.Empty;
            string extDigit = string.Empty;
            string srNo = string.Empty;
            string assetRef = string.Empty;
            string indDigit = string.Empty;
            string itemRef = string.Empty;
            string assetType = string.Empty;
            string locRef = string.Empty;
            string manufactureId = string.Empty;
            string productId = string.Empty;
            string cageNo = string.Empty;

            string[] urnParts = strEPCurn.Split(new char[] { ':' }, 5);
            string uccStandard = urnParts[3];
            decodedData.Add("uccStandard", uccStandard);
            string bitLength = uccStandard.Substring(uccStandard.Length - 2, 2);
            decodedData.Add("bitLength", bitLength);

            switch (uccStandard.Substring(0, uccStandard.Length - 3))
            {
                case "sscc":
                    {
                        DecodeSSCCURN(strEPCurn, out uccStandard, out bitLength, out compPrefix,
                            out filter, out extDigit, out srNo);
                        decodedData.Add("compPrefix", compPrefix);
                        decodedData.Add("filter", filter);
                        decodedData.Add("extDigit", extDigit);
                        decodedData.Add("srNo", srNo);
                        break;
                    }
                case "giai":
                    {
                        DecodeGIAIURN(strEPCurn, out uccStandard, out bitLength, out compPrefix,
                            out filter, out assetRef);
                        decodedData.Add("compPrefix", compPrefix);
                        decodedData.Add("filter", filter);
                        decodedData.Add("assetRef", assetRef);
                        break;
                    }
                case "usdod":
                    {
                        DecodeUSDODURN(strEPCurn, out uccStandard, out bitLength, out filter,
                            out srNo, out cageNo);
                        decodedData.Add("filter", filter);
                        decodedData.Add("srNo", srNo);
                        decodedData.Add("cageNo", cageNo);
                        break;
                    }

                case "sgtin":
                    {
                        DecodeSGTINURN(strEPCurn, out uccStandard, out bitLength, out compPrefix,
                            out filter, out srNo, out indDigit, out itemRef);
                        decodedData.Add("compPrefix", compPrefix);
                        decodedData.Add("filter", filter);
                        decodedData.Add("srNo", srNo);
                        decodedData.Add("indDigit", indDigit);
                        decodedData.Add("itemRef", itemRef);
                        break;
                    }
                case "grai":
                    {
                        DecodeGRAIURN(strEPCurn, out uccStandard, out bitLength, out compPrefix,
                            out filter, out srNo, out assetType);
                        decodedData.Add("compPrefix", compPrefix);
                        decodedData.Add("filter", filter);
                        decodedData.Add("srNo", srNo);
                        decodedData.Add("assetType", assetType);
                        break;
                    }
                case "sgln":
                    {
                        DecodeSGLNURN(strEPCurn, out uccStandard, out bitLength, out compPrefix,
                            out filter, out srNo, out locRef);
                        decodedData.Add("compPrefix", compPrefix);
                        decodedData.Add("filter", filter);
                        decodedData.Add("srNo", srNo);
                        decodedData.Add("locRef", locRef);
                        break;
                    }
                case "gid":
                    {
                        DecodeGIDURN(strEPCurn, out uccStandard, out bitLength, out srNo, out manufactureId,
                            out productId);
                        decodedData.Add("srNo", srNo);
                        decodedData.Add("manufactureId", manufactureId);
                        decodedData.Add("productId", productId);
                        break;
                    }
                case "lctn":
                    {
                        DecodeLCTNURN(strEPCurn, out uccStandard, out bitLength, out compPrefix,out srNo,out locRef);
                        decodedData.Add("compPrefix", compPrefix);
                        decodedData.Add("srNo", srNo);
                        break;
                    }
                case "aset":
                    {
                        DecodeASETURN(strEPCurn, out uccStandard, out bitLength, out compPrefix,out srNo, out filter);
                        decodedData.Add("compPrefix", compPrefix);
                        decodedData.Add("srNo", srNo);
                        decodedData.Add("filter", filter);
                        break;
                    }
            }
        }

        public static void DecodeSSCCURN(string strEPCurn,out string uccStandard, out string bitLength,
            out string compPrefix, out string filter, out string extDigit, out string srNo)
        {
            string[] urnParts = strEPCurn.Split(new char[] { ':' }, 5);
            uccStandard = urnParts[3];
            bitLength = uccStandard.Substring(uccStandard.Length - 2, 2);

            string[] urnfields = urnParts[4].Split(new char[] { '.' }, 3);
            filter = urnfields[0];
            compPrefix = urnfields[1];
            extDigit = urnfields[2].Substring(0, 1);
            srNo = urnfields[2].Substring(1, urnfields[2].ToString().Length - 1);
            
            
        }

        public static void DecodeGIAIURN(string strEPCurn, out string uccStandard, out string bitLength,
            out string compPrefix, out string filter, out string assetRef)
        {
            string[] urnParts = strEPCurn.Split(new char[] { ':' }, 5);
            uccStandard = urnParts[3];
            bitLength = uccStandard.Substring(uccStandard.Length - 2, 2);
            string[] urnfields = urnParts[4].Split(new char[] { '.' }, 3);
            filter = urnfields[0];
            compPrefix = urnfields[1];
            assetRef = urnfields[2];
        }

        public static void DecodeUSDODURN(string strEPCurn, out string uccStandard, out string bitLength,
            out string filter, out string srNo,out string cageNo)
        {
            string[] urnParts = strEPCurn.Split(new char[] { ':' }, 5);
            uccStandard = urnParts[3];
            bitLength = uccStandard.Substring(uccStandard.Length - 2, 2);
            string[] urnfields = urnParts[4].Split(new char[] { '.' }, 3);
            filter = urnfields[0];
            cageNo = urnfields[1];
            srNo = urnfields[2];
        }

        public static void DecodeSGTINURN(string strEPCurn, out string uccStandard, out string bitLength,
            out string compPrefix, out string filter, out string srNo,out string indDigit, out string itemRef)
        {
            string[] urnParts = strEPCurn.Split(new char[] { ':' }, 5);
            uccStandard = urnParts[3];
            bitLength = uccStandard.Substring(uccStandard.Length - 2, 2);
            string[] urnfields = urnParts[4].Split(new char[] { '.' }, 4);
            filter = urnfields[0];
            compPrefix = urnfields[1];
            indDigit = urnfields[2].Substring(0, 1);
            itemRef = urnfields[2].Substring(1, urnfields[2].ToString().Length - 1);
            srNo = urnfields[3];
        }

        public static void DecodeGRAIURN(string strEPCurn, out string uccStandard, out string bitLength,
            out string compPrefix, out string filter, out string srNo,out string assetType)
            
        {
            string[] urnParts = strEPCurn.Split(new char[] { ':' }, 5);
            uccStandard = urnParts[3];
            bitLength = uccStandard.Substring(uccStandard.Length - 2, 2);
            string[] urnfields = urnParts[4].Split(new char[] { '.' }, 4);
            filter = urnfields[0];
            compPrefix = urnfields[1];
            assetType = urnfields[2];
            srNo = urnfields[3];
        }

        public static void DecodeSGLNURN(string strEPCurn, out string uccStandard, out string bitLength,
            out string compPrefix, out string filter, out string srNo,out string locRef)
        {
            string[] urnParts = strEPCurn.Split(new char[] { ':' }, 5);
            uccStandard = urnParts[3];
            bitLength = uccStandard.Substring(uccStandard.Length - 2, 2);
            string[] urnfields = urnParts[4].Split(new char[] { '.' }, 4);
            filter = urnfields[0];
            compPrefix = urnfields[1];
            locRef = urnfields[2];
            srNo = urnfields[3];
        }


        public static void DecodeGIDURN(string strEPCurn, out string uccStandard, out string bitLength,
            out string srNo,out string manufactureId, out string productId)
        {
            string[] urnParts = strEPCurn.Split(new char[] { ':' }, 5);
            uccStandard = urnParts[3];
            bitLength = uccStandard.Substring(uccStandard.Length - 2, 2);
            string[] urnfields = urnParts[4].Split(new char[] { '.' }, 3);
            manufactureId = urnfields[0];
            productId = urnfields[1];
            srNo = urnfields[2];
        }

        public static void DecodeLCTNURN(string strEPCurn, out string uccStandard, out string bitLength,
            out string compPrefix, out string srNo,out string locRef)
        {
            string[] urnParts = strEPCurn.Split(new char[] { ':' }, 5);
            uccStandard = urnParts[3];
            bitLength = uccStandard.Substring(uccStandard.Length - 2, 2);
            string[] urnfields = urnParts[4].Split(new char[] { '.' }, 4);
            string filter = urnfields[0];
            compPrefix = urnfields[1];
            locRef = urnfields[2];
            srNo = urnfields[3];
        }

        public static void DecodeASETURN(string strEPCurn, out string uccStandard, out string bitLength,
                    out string compPrefix, out string srNo, out string filter)
        {
            string[] urnParts = strEPCurn.Split(new char[] { ':' }, 5);
            uccStandard = urnParts[3];
            bitLength = uccStandard.Substring(uccStandard.Length - 2, 2);
            string[] urnfields = urnParts[4].Split(new char[] { '.' }, 4);
            filter = urnfields[0];
            compPrefix = urnfields[1];
            string locRef = urnfields[2];
            srNo = urnfields[3];
        } 

        public static string GetRawURI(byte[] toDecodeBytes)
		{
			string rawURI = string.Empty ;
			string unrecognizedFormat = ".UNRECOGNIZED TAG FORMAT";
			//string tempStr = Constants.EPCURIPREFIX+Constants.URIKEYTONE;
			string tempStr =Constants.EPCURIPREFIX+Constants.URIRAW;
			
			
			try
			{
				int bitLength = toDecodeBytes.Length*8 ;
				string errorData = string.Empty ;
//				if(bitLength == 64)
//					errorData = Convert.ToString(BitConverter.ToUInt64(toDecodeBytes,0));
//				else
					errorData = BitConverter.ToString(toDecodeBytes,0);
				string tempErrData = errorData.Replace("-","");
				rawURI = tempStr+bitLength+"."+tempErrData ;
			}
			catch(Exception e)
			{
				rawURI = tempStr+unrecognizedFormat ;
				m_log.Warn("GetRawURI(): Exception caught : "+e.Message);
			}
			return rawURI ;
		}

	
		/// <summary>
		/// It will return Byte array represendtation of the URN supplied
		/// </summary>
		/// <param name="strEPCurn"></param>
		/// <returns></returns>
		public static byte[] GetByteArrFmURN(string strEPCurn)
		{
			string[] urnParts = null;
			string[] urnfields = null;
			byte[] outBytesFmEpcURN = null ;
			urnParts = strEPCurn.Split(new char[] {':'}, 5);
			string uccStandard = urnParts[3];
			short byteLen = Convert.ToInt16( uccStandard.Substring(uccStandard.Length - 2, 2) );
			
			int srNo ;
			byte filter;
			string compPrefix = "";
			switch( uccStandard.Substring(0, uccStandard.Length-3) )
			{
				case "sscc":
					urnfields = urnParts[4].Split(new char[]{'.'}, 3);

					filter = Convert.ToByte( urnfields[0] );
					compPrefix = urnfields[1];
					string extDigit = urnfields[2].Substring(0,1);
					string srRef = urnfields[2].Substring(1, urnfields[2].ToString().Length-1);

					if(byteLen == 64)
						outBytesFmEpcURN = EncodeSSCC.EncodeSCCtoSSCC64(filter, extDigit, compPrefix, srRef);
					else if (byteLen == 96)
						outBytesFmEpcURN = EncodeSSCC.EncodeSCCtoSSCC96(filter, extDigit, compPrefix, srRef);
					break;

				

				case "giai":
					urnfields = urnParts[4].Split(new char[]{'.'}, 3);
					filter = Convert.ToByte( urnfields[0] );
					compPrefix = urnfields[1];
					string assetRef =  urnfields[2];
					
					if(byteLen == 64)
						outBytesFmEpcURN = EncodeGIAI.EncodeGIAItoGIAI64(compPrefix, assetRef, filter);
					else if (byteLen == 96)
						outBytesFmEpcURN = EncodeGIAI.EncodeGIAItoGIAI96(compPrefix, assetRef, filter);
					break;
			
				case "usdod":
					urnfields = urnParts[4].Split(new char[]{'.'}, 3);
					filter = Convert.ToByte( urnfields[0] );
					string cageNo = urnfields[1];
					srNo = Convert.ToInt32( urnfields[2] );
					
					
					if(byteLen == 64)
						outBytesFmEpcURN = EncodeDoD.EncodeDoDtoDoD64(cageNo, srNo, filter);
					else if (byteLen == 96)
						outBytesFmEpcURN = EncodeDoD.EncodeDoDtoDoD96(cageNo, srNo, filter);
					break;

				case "sgtin":
					urnfields = urnParts[4].Split(new char[]{'.'}, 4);
					
					filter = Convert.ToByte( urnfields[0] );
					compPrefix = urnfields[1];
					string indDigit = urnfields[2].Substring(0,1);
					string itemRef = urnfields[2].Substring(1, urnfields[2].ToString().Length-1);
					
					if(byteLen == 64)
					{
						srNo = Convert.ToInt32( urnfields[3] );
						outBytesFmEpcURN = EncodeGTIN.EncodeGTIN14toSGTIN64(indDigit, compPrefix, itemRef, srNo, filter);

					}
					else if (byteLen == 96)
					{
						Int64 SrNo_SGTIN = Convert.ToInt64(urnfields[3] ) ;
						outBytesFmEpcURN = EncodeGTIN.EncodeGTIN14toSGTIN96(filter, indDigit, compPrefix, itemRef, SrNo_SGTIN );
					}
					break;

				case "grai":
					urnfields = urnParts[4].Split(new char[]{'.'}, 4);
					filter = Convert.ToByte( urnfields[0] );
					compPrefix = urnfields[1];
					string assetType = urnfields[2];
					string strSrNo = urnfields[3];

					if(byteLen == 64)
						outBytesFmEpcURN = EncodeGRAI.EncodeGRAItoGRAI64(filter, compPrefix, assetType, strSrNo);
					else if (byteLen == 96)
						outBytesFmEpcURN = EncodeGRAI.EncodeGRAItoGRAI96(filter, compPrefix, assetType, strSrNo);
					break;

				case "sgln":
					urnfields = urnParts[4].Split(new char[]{'.'}, 4);
					filter = Convert.ToByte( urnfields[0] );
					compPrefix = urnfields[1];
					string locRef = urnfields[2];
					srNo = Convert.ToInt32( urnfields[3] );
					
					
					if(byteLen == 64)
						outBytesFmEpcURN = EncodeGLN.EncodeGLNtoSGLN64(filter, compPrefix, locRef, srNo);
					else if (byteLen == 96)
						outBytesFmEpcURN = EncodeGLN.EncodeGLNtoSGLN96(filter, compPrefix, locRef, srNo);
					break;


				case "gid":
					urnfields = urnParts[4].Split(new char[]{'.'}, 3);
					int manufactureId = Convert.ToInt32( urnfields[0] );
					int prodId = Convert.ToInt32( urnfields[1] );
					srNo = Convert.ToInt32( urnfields[2] );
					
					outBytesFmEpcURN = GID.Encode96(manufactureId, prodId, srNo);
					break;



                case "lctn":
                    {
                        urnfields = urnParts[4].Split(new char[] { '.' }, 4);

                        filter = Convert.ToByte(urnfields[0]);
                        compPrefix = urnfields[1];
                        string locref = urnfields[2];
                        Int64 lctnSrNo = Convert.ToInt64(urnfields[3]);
					
                        if (byteLen == 96)
                            outBytesFmEpcURN = EncodeLCTN.EncodeLCTNtoLCTN96(filter, compPrefix, locref, lctnSrNo);
                        break;
                    }

			}

			return outBytesFmEpcURN;
		}

        /// <summary>
        /// Method to convert epc urn to byte array.
        /// </summary>
        /// <param name="epcURN"></param>
        /// <returns></returns>
        public static byte[] GetTagIdBytesFromEPCURN(string epcURN)
        {
            byte[] outBytesFmEpcURN = GetNextTagIdBytesFromEPCURN(epcURN, 0);
            
            return outBytesFmEpcURN;
        }
        
        /// <summary>
        /// Increments serial no if serialNoDiff > 0
        /// </summary>
        /// <param name="epcURN"></param>
        /// <param name="serialNoDiff"></param>
        /// <returns></returns>
        public static string GetNextEPCUrn(string epcURN, int serialNoDiff)
        { 
            byte[] tagIdBytes = GetNextTagIdBytesFromEPCURN(epcURN, serialNoDiff);
            return Decode(tagIdBytes);
        }
        /// <summary>
        /// Method to convert epc urn to byte array.Increments serial no if serialNoDiff > 0
        /// </summary>
        /// <param name="epcURN"></param>
        /// <returns></returns>
        public static byte[] GetNextTagIdBytesFromEPCURN(string epcURN, int serialNoDiff)
        {
            byte[] outBytesFmEpcURN = null;
            try
            {
                string uccStandard = string.Empty;
                string bitLength = string.Empty;
                string compPrefix = string.Empty;
                string filterStr = string.Empty;
                string extDigit = string.Empty;
                string srRef = string.Empty;
                string assetRef = string.Empty;
                string cageNo = string.Empty;
                string indDigit = string.Empty;
                string itemRef = string.Empty;
                string assetType = string.Empty;
                string locRef = string.Empty;
                string manufactureIdStr = string.Empty;
                string productId = string.Empty;

                EPCBytes.DecodeURN(epcURN, out uccStandard, out bitLength, out compPrefix, out filterStr, out extDigit,
                    out srRef, out assetRef, out cageNo, out indDigit, out itemRef, out assetType, out locRef,
                    out manufactureIdStr, out productId);

                short byteLen = Convert.ToInt16(uccStandard.Substring(uccStandard.Length - 2, 2));
                byte filter = 0;
                if (filterStr != null && filterStr != string.Empty)
                    filter = Convert.ToByte(filterStr);

                switch (uccStandard.Substring(0, uccStandard.Length - 3))
                {
                    case "sscc":
                        {
                            #region Increment Sr No
                            if (serialNoDiff > 0)
                                srRef = IncrementSerialNoInt(srRef, serialNoDiff);
                            #endregion Increment Sr No
                            
                            if (byteLen == 64)
                                outBytesFmEpcURN = EncodeSSCC.EncodeSCCtoSSCC64(filter, extDigit, compPrefix, srRef);
                            else if (byteLen == 96)
                                outBytesFmEpcURN = EncodeSSCC.EncodeSCCtoSSCC96(filter, extDigit, compPrefix, srRef);
                            break;
                        }
                    case "giai":
                        {
                            #region Increment Sr No
                            if (serialNoDiff > 0)
                                srRef = IncrementSerialNoInt(srRef, serialNoDiff);
                            #endregion Increment Sr No
                            if (byteLen == 64)
                                outBytesFmEpcURN = EncodeGIAI.EncodeGIAItoGIAI64(compPrefix, assetRef, filter);
                            else if (byteLen == 96)
                                outBytesFmEpcURN = EncodeGIAI.EncodeGIAItoGIAI96(compPrefix, assetRef, filter);
                            break;
                        }
                    case "usdod":
                        {
                            #region Increment Sr No
                            if (serialNoDiff > 0)
                                srRef = IncrementSerialNoInt(srRef, serialNoDiff);
                            #endregion Increment Sr No
                            Int64 srNo = Convert.ToInt64(srRef);

                            if (byteLen == 64)
                                outBytesFmEpcURN = EncodeDoD.EncodeDoDtoDoD64(cageNo, srNo, filter);
                            else if (byteLen == 96)
                                outBytesFmEpcURN = EncodeDoD.EncodeDoDtoDoD96(cageNo, srNo, filter);
                            break;
                        }
                    case "sgtin":
                        {
                            if (byteLen == 64)
                            {
                                #region Increment Sr No
                                if (serialNoDiff > 0)
                                    srRef = IncrementSerialNoInt(srRef, serialNoDiff);
                                #endregion Increment Sr No
                                int srNo = Convert.ToInt32(srRef);
                                outBytesFmEpcURN = EncodeGTIN.EncodeGTIN14toSGTIN64(indDigit, compPrefix, itemRef, srNo, filter);

                            }


                          
                            else if (byteLen == 96)
                            {
                                #region Increment Sr No
                                if (serialNoDiff > 0)
                                    srRef = IncrementSerialNoLong(srRef, serialNoDiff);
                                #endregion Increment Sr No
                                Int64 SrNo_SGTIN = Convert.ToInt64(srRef);
                                outBytesFmEpcURN = EncodeGTIN.EncodeGTIN14toSGTIN96(filter, indDigit, compPrefix, itemRef, SrNo_SGTIN);
                            }
                            break;
                        }
                    case "grai":
                        {
                            #region Increment Sr No
                            if (serialNoDiff > 0)
                                srRef = IncrementSerialNoInt(srRef, serialNoDiff);
                            #endregion Increment Sr No
                            if (byteLen == 64)
                                outBytesFmEpcURN = EncodeGRAI.EncodeGRAItoGRAI64(filter, compPrefix, assetType, srRef);
                            else if (byteLen == 96)
                                outBytesFmEpcURN = EncodeGRAI.EncodeGRAItoGRAI96(filter, compPrefix, assetType, srRef);
                            break;
                        }

                    case "sgln":
                        {
                            #region Increment Sr No
                            if (serialNoDiff > 0)
                                srRef = IncrementSerialNoInt(srRef, serialNoDiff);
                            #endregion Increment Sr No
                            int srNo = Convert.ToInt32(srRef);

                            if (byteLen == 64)
                                outBytesFmEpcURN = EncodeGLN.EncodeGLNtoSGLN64(filter, compPrefix, locRef, srNo);
                            else if (byteLen == 96)
                                outBytesFmEpcURN = EncodeGLN.EncodeGLNtoSGLN96(filter, compPrefix, locRef, srNo);
                            break;
                        }

                    case "gid":
                        {
                            #region Increment Sr No
                            if (serialNoDiff > 0)
                                srRef = IncrementSerialNoInt(srRef, serialNoDiff);
                            #endregion Increment Sr No
                            int manufactureId = Convert.ToInt32(manufactureIdStr);
                            int prodId = Convert.ToInt32(productId);
                            int srNo = Convert.ToInt32(srRef);

                            outBytesFmEpcURN = GID.Encode96(manufactureId, prodId, srNo);
                            break;
                        }
                    case "lctn":
                        {
                            #region Increment Sr No
                            if (serialNoDiff > 0)
                                srRef = IncrementSerialNoLong(srRef, serialNoDiff);
                            #endregion Increment Sr No
                            Int64 srNo = Convert.ToInt64(srRef);
                            if (byteLen == 96)
                                outBytesFmEpcURN = EncodeLCTN.EncodeLCTNtoLCTN96(filter, compPrefix, locRef, srNo);
                            break;
                        }
                    case "aset":
                        {
                            #region Increment Sr No
                            if (serialNoDiff > 0)
                                srRef = IncrementSerialNoLong(srRef, serialNoDiff);
                            #endregion Increment Sr No
                            Int64 srNo = Convert.ToInt64(srRef);

                            if (byteLen == 96)
                                outBytesFmEpcURN = EncodeASET.EncodeASETtoASET96(filter, compPrefix, locRef, srNo);
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                m_log.Error("Failed to get tag id byte array", ex);
            }
            return outBytesFmEpcURN;
        }


		#endregion Public Methods ENDS
        
		#region Properties
		public URIFORMAT URIFormat
		{
			set
			{
				m_URIFormat = value ;
			}
		}
		#endregion
	}
}
