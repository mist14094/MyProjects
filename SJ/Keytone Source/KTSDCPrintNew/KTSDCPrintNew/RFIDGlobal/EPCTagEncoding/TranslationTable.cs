
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
using System.Xml ;
using System.Collections ;

using KTone.RFIDGlobal;

namespace KTone.RFIDGlobal.EPCTagEncoding
{
	/// <summary>
	/// This class serves as a processing unit for encoding/decoding 64-bit EPC tag data.
	/// The translation table holding company prefix index and company prefix value is being processed
	/// upon.
	/// </summary>
    public class CompanyPrefixLookupImpl : ICompanyPrefixLookup
	{
		#region [ Members ]
        NLog.Logger m_log  = KTLogger.KTLogManager.GetLogger();

		//--------
		private XmlDocument xDoc = null ;
		private Hashtable mHshIdxComPrefix = null ;
		private string mXsdFile = string.Empty ;	
		private UInt16 mMaxIndex = 0  ;
		private string fileTranslationTable = string.Empty ;

		private static object implLock = new object() ; 
		private static ICompanyPrefixLookup impl = null ;  
		#endregion [ Members ]		

		#region [ Constructor ]

		public static ICompanyPrefixLookup GetInstanceOf()
		{
			try 
			{
				lock(implLock) 
				{
					if ( impl == null ) 
					{
						impl = new CompanyPrefixLookupImpl() ; 
					}
					
				}
			}
			catch ( Exception ) {}
			return impl ;
		}

		// KLUDGE THIS constructor should be made private and there should be change in TEST cases before making 
		// it private 
		/// <summary>
		/// Create instance of CompanyPrefixLookupImpl depends on TranslationXmlFile.
		/// </summary>
		/// <param name="pathTranslationXmlFile">Fully qualified path of TranslationXmlFile</param>
		/// <param name="xsdFileTranslationXml">Fully qualified path of xsd for TranslationXmlFile</param>
        public CompanyPrefixLookupImpl(string pathTranslationXmlFile, string xsdFileTranslationXml)
		{
			InitCtor(pathTranslationXmlFile,xsdFileTranslationXml) ;
		}


		private CompanyPrefixLookupImpl()
		{
			string SysDirXML = AppDomain.CurrentDomain.BaseDirectory;
			char[] slash = {'\\'};
			string SysDirXSD = SysDirXML +"\\"+"XSD"+"\\"+"EPCFormatConfig";
			
			SysDirXML = SysDirXML.TrimEnd(slash);	
			SysDirXML += "\\";
			SysDirXSD += "\\";

			//if no constructor is provided local default path is accepted.
			InitCtor(SysDirXML +  @"ManagerTranslation.xml", SysDirXSD +  @"XmlTranslationTableSpec.xsd");
		}


		private  void InitCtor(string pathTranslationXmlFile, string xsdFileTranslationXml)
		{
			try
			{
				m_log.Trace("[CompanyPrefixLookupImpl::CompanyPrefixLookupImpl] Entered in constructor.");
				
				//chk-param------------------------
				if(pathTranslationXmlFile == null || pathTranslationXmlFile.Length<=0)
					throw new CompanyPrefixLookupImplException("[CompanyPrefixLookupImpl::CompanyPrefixLookupImpl] Error: Invalid(Empty) parameter 'pathTranslationXmlFile' found. ");

				if(System.IO.File.Exists(pathTranslationXmlFile) == false)
					throw new CompanyPrefixLookupImplException("[CompanyPrefixLookupImpl::CompanyPrefixLookupImpl] Error: Invalid filePath " + pathTranslationXmlFile + " found for Translation xml file, File not found. path should be ['directory name' + 'file name'].");

				if(xsdFileTranslationXml == null || xsdFileTranslationXml.Length<=0)
					throw new CompanyPrefixLookupImplException("[CompanyPrefixLookupImpl::CompanyPrefixLookupImpl] Error: Invalid(Empty) parameter 'xsdFileTranslationXml' found. ");

				if(System.IO.File.Exists(xsdFileTranslationXml) == false)
					throw new CompanyPrefixLookupImplException("[CompanyPrefixLookupImpl::CompanyPrefixLookupImpl] Error: Invalid filePath " + xsdFileTranslationXml + " found for Translation xml file, File not found. path should be ['directory name' + 'file name'].");

				//chk param complete---------------

				//LoadXml -----------------
				this.fileTranslationTable = pathTranslationXmlFile ;
				mXsdFile = xsdFileTranslationXml ;
		
				string sError ;
				if ( LoadXmlFile(pathTranslationXmlFile, out sError) == false) 
				{
					m_log.Trace("[CompanyPrefixLookupImpl::LoadXmlFile] failed. " + sError);
					throw new CompanyPrefixLookupImplException("[CompanyPrefixLookupImpl::CompanyPrefixLookupImpl] Error: Loading translation xml file failed." +  sError);
				}


				sError = string.Empty ;
				//HashTable
				if ( FillHashTbl(out sError) == false) 
				{
					m_log.Trace("[CompanyPrefixLookupImpl::FillHashTbl] failed. " + sError);
					throw new CompanyPrefixLookupImplException("[CompanyPrefixLookupImpl::FillHashTbl] Error: creating FillHashTbl failed." +  sError);
				}

				m_log.Trace("[CompanyPrefixLookupImpl::TranslationTable] constructor is complete.");
			}
			catch(CompanyPrefixLookupImplException ex)
			{
				//m_log.Trace("[CompanyPrefixLookupImpl::CompanyPrefixLookupImpl] Error: " + ex.Message);
				m_log.Error("[CompanyPrefixLookupImpl::CompanyPrefixLookupImpl] Error: " + ex.Message);
				throw ex;
			}
			catch(Exception ex)
			{
				//m_log.Trace("[CompanyPrefixLookupImpl::CompanyPrefixLookupImpl] Failed: " + ex.Message);
				m_log.Error("[CompanyPrefixLookupImpl::CompanyPrefixLookupImpl] Failed: " + ex.Message);
				throw new CompanyPrefixLookupImplException("[CompanyPrefixLookupImpl::CompanyPrefixLookupImpl] Error: " + ex.Message);
			}
		}


		#endregion [ Constructor ]

		#region [ Private Methods ]

		/// <summary>
		/// Load xml file.
		/// </summary>
		/// <param name="xmlFile">fully qualified path of xml file</param>
		/// <param name="sError">out param error string</param>
		/// <returns></returns>
		private bool LoadXmlFile(string xmlFile, out string sError)
		{
			try
			{
				sError = string.Empty ;
				m_log.Trace("[CompanyPrefixLookupImpl::LoadXmlFile] IN." );
				
				xDoc = new XmlDocument();
				xDoc.Load(xmlFile);		
		
				if( ValidateXml(out sError) == false )
					throw new CompanyPrefixLookupImplException("Validation failed for TransationTableXmlFile = " + xmlFile + " Error = " + sError);

				m_log.Trace("[CompanyPrefixLookupImpl::LoadXmlFile] OUT." );
				return true ;
			}
			catch(XmlException ex)
			{
				sError = ex.Message ;
				m_log.Error("[CompanyPrefixLookupImpl::LoadXmlFile] Failed: " + ex.Message);
				return false ;
			}
			catch(CompanyPrefixLookupImplException ex)
			{
				sError = ex.Message;
				m_log.Error("[CompanyPrefixLookupImpl::LoadXmlFile] Failed: " + ex.Message);
				return false;
			}
			catch(Exception ex)
			{
				sError = ex.Message ;
				m_log.Error("[CompanyPrefixLookupImpl::LoadXmlFile] Failed: " + ex.Message);
				return false ;
			}
		}


		/// <summary>
		/// Validate current opened TranslationXml.
		/// </summary>
		/// <param name="logStr">error string if any invalid structure found otherwise return empty.</param>
		/// <returns>return false if any invalid structure found otherwise return true.</returns>
		private bool ValidateXml(out string logStr)
		{
			try
			{ 	
				m_log.Trace("[CompanyPrefixLookupImpl::Validate] IN");
				if(this.xDoc != null)
				{
					string xmlTmplt = this.xDoc.OuterXml; 
					System.IO.MemoryStream configStream = new System.IO.MemoryStream(System.Text.Encoding.ASCII.GetBytes(xmlTmplt)); 
					RFUtils.ValidateXML( mXsdFile, new System.IO.StreamReader(configStream));
					logStr = string.Empty;

					m_log.Trace("[CompanyPrefixLookupImpl::Validate] OUT");
					return true;
				}
				else
					throw new CompanyPrefixLookupImplException("[CompanyPrefixLookupImpl::Validate] failed : Null xDoc object found.Template is not open.");
			}
			catch(CompanyPrefixLookupImplException ex)
			{
				logStr = ex.Message;
				m_log.Error("[CompanyPrefixLookupImpl::Validate] Failed: " + ex.Message);
				return false;
			}
			catch(Exception ex)
			{				
				logStr = ex.Message;
				m_log.Error("[CompanyPrefixLookupImpl::Validate] Failed: " + ex.Message);
				return false;
			}			
		}//Validate


		private bool FillHashTbl(out string err)
		{
			try
			{
				err = string.Empty ;
				m_log.Trace("[CompanyPrefixLookupImpl::FillHashTbl] IN.");

				this.mHshIdxComPrefix = new Hashtable();

				if(this.xDoc == null)
					throw new CompanyPrefixLookupImplException("[CompanyPrefixLookupImpl::FillHashTable] Error: Null xml object found.");

				XmlNodeList ndList;
				ndList = this.xDoc.GetElementsByTagName("entry");

				if(ndList != null)
				{
					foreach(XmlNode xNd in ndList)
					{
						UInt16 companyIdx = UInt16.Parse(xNd.Attributes.GetNamedItem("index").Value);
						if(this.mMaxIndex < companyIdx)
							this.mMaxIndex = companyIdx ;

						string sComPrefix = xNd.Attributes.GetNamedItem("companyPrefix").Value;
						this.mHshIdxComPrefix.Add(companyIdx, sComPrefix);
					}
				}
				
				if(this.mMaxIndex <= 1023 )
					this.mMaxIndex = 1023 ;

				m_log.Trace("[CompanyPrefixLookupImpl::FillHashTbl] OUT.");
				return true ;
			}
			catch(Exception ex)
			{
				err = ex.Message ;
				m_log.Error("[CompanyPrefixLookupImpl::FillHashTbl] Error: " + ex.Message);
				return false ;
			}
		}


		private UInt16 CreateEntry(string companyPrefixValue, out string sError)
		{			
			try
			{				
				m_log.Trace("[CompanyPrefixLookupImpl::CreateEntry] IN.");	
				sError = string.Empty ;

				//Creating in XmlFile

				UInt16 currentMax = ++this.mMaxIndex;				

				XmlElement xElmEntry = this.xDoc.CreateElement("entry");
				xElmEntry.SetAttribute("index", currentMax.ToString() );
				xElmEntry.SetAttribute("companyPrefix", companyPrefixValue);
				xElmEntry.SetAttribute("custom", "true");

				this.xDoc.DocumentElement.AppendChild(xElmEntry);
				if( this.ValidateXml(out sError) == false )
				{
					m_log.Trace("[CompanyPrefixLookupImpl::CreateEntry] failed: " + sError) ;
					throw new CompanyPrefixLookupImplException("Error : " + sError) ;
				}				

				//Creating in Hash
				this.mHshIdxComPrefix.Add(currentMax, companyPrefixValue);

				//Aftre saving to Hash saving Xml.
				this.xDoc.Save(this.fileTranslationTable);

				m_log.Trace("[CompanyPrefixLookupImpl::CreateEntry] succeeded.");	

				this.mMaxIndex = currentMax ;

				m_log.Trace("[CompanyPrefixLookupImpl::CreateEntry] OUT.");		
				return this.mMaxIndex ;
			}
			catch(CompanyPrefixLookupImplException ex)
			{
				m_log.Trace("[CompanyPrefixLookupImpl::LookupTable] Error: " + ex.Message);
				m_log.Error("[CompanyPrefixLookupImpl::LookupTable] Error: " + ex.Message);
				sError = "[CompanyPrefixLookupImpl::LookupTable] Error: " + ex.Message ;
				return 0 ;
			}
			catch(Exception ex)
			{
				m_log.Trace("[CompanyPrefixLookupImpl::LookupTable] Failed: " + ex.Message);
				m_log.Error("[CompanyPrefixLookupImpl::LookupTable] Failed: " + ex.Message);
				sError = "[CompanyPrefixLookupImpl::LookupTable] Error: " + ex.Message ;
				return 0 ;
			}
		}

		#endregion [ Private Methods ENDS ]

		#region [ Public Methods ]

		/// <summary>
		/// It will return company prefix value against its index.
		/// </summary>
		/// <param name="compPrefixIndex">company prefix index</param>
		/// <returns>string as a company prefix value in xml file against given index</returns>
		public /*static*/ string Lookup(UInt16 compPrefixIndex)
		{
			try
			{
				m_log.Trace("[CompanyPrefixLookupImpl::LookupTable] IN.");
				
				//chk-param---------------------------
				if( compPrefixIndex == 0 )
				{
					m_log.Trace("Invalid(zero value) parameter 'compPrefixIndex' found. ");
					throw new CompanyPrefixLookupImplException("[CompanyPrefixLookupImpl::LookUp] Error: Invalid(zero value) parameter 'compPrefixIndex' found. ");
				}
				//chk-param complete---------------------------
				
				string companyPrefixValue = string.Empty ;

				if( this.mHshIdxComPrefix.ContainsKey(compPrefixIndex) == true)
				{
					companyPrefixValue = this.mHshIdxComPrefix[compPrefixIndex].ToString();			
					m_log.Trace("[CompanyPrefixLookupImpl::LookUp] compPrefixIndex:" + compPrefixIndex.ToString() + "@CompanyPrefixValue: " + companyPrefixValue) ;                			
					m_log.Trace("[CompanyPrefixLookupImpl::LookUp] OUT.");
					return companyPrefixValue ;
				}
				else
				{
					m_log.Trace("[CompanyPrefixLookupImpl::LookUp] compPrefixIndex:" + compPrefixIndex.ToString() + "@CompanyPrefixValue: " + companyPrefixValue) ;                			
					throw new CompanyPrefixLookupImplException("[CompanyPrefixLookupImpl::LookUp] Error: given compPrefixIndex='" + compPrefixIndex.ToString() + "' not found.");
				}
			}
			catch(CompanyPrefixLookupImplException ex)
			{
				m_log.Trace(ex.Message);
				//m_log.Error(ex.Message);
				throw ex;
			}
			catch(Exception ex)
			{
				m_log.Trace("[CompanyPrefixLookupImpl::LookUp] Failed: " + ex.Message);
				//m_log.Error("[CompanyPrefixLookupImpl::LookUp] Failed: " + ex.Message);
				throw new CompanyPrefixLookupImplException("[CompanyPrefixLookupImpl::LookUp] Error: " + ex.Message);
			}
		}

		/// <summary>
		/// It will return company index as a out param against company prefix value.
		/// </summary>
		/// <param name="companyPrefixValue">string company prefix value</param>
		/// <param name="companyPrefixIndex">out company prefix index</param>
		/// <returns>return true if company prefix value exist else false</returns>
		public bool Lookup(string companyPrefixValue, out UInt16 companyPrefixIndex)
		{
			try
			{
				companyPrefixIndex = 0; 
				//Search company prefix value in Hash
				bool bCompPrefixValFound = false ;
				System.Collections.IDictionaryEnumerator iEr = this.mHshIdxComPrefix.GetEnumerator();

				while(iEr.MoveNext())
				{
					if(iEr.Value.ToString().Equals(companyPrefixValue) == true )
					{
						bCompPrefixValFound = true ;
						companyPrefixIndex = UInt16.Parse( iEr.Key.ToString() );
						break;
					}				
				}

				return bCompPrefixValFound ;
			}
			catch(CompanyPrefixLookupImplException ex)
			{
				m_log.Trace(ex.Message);
				//m_log.Error(ex.Message);
				throw ex;
			}
			catch(Exception ex)
			{
				m_log.Trace("[CompanyPrefixLookupImpl::LookUp] Failed: " + ex.Message);
				//m_log.Error("[CompanyPrefixLookupImpl::LookUp] Failed: " + ex.Message);
				throw new CompanyPrefixLookupImplException("[CompanyPrefixLookupImpl::LookUp] Error: " + ex.Message);
			}
		}
		

		/// <summary>
		/// Method insert a given Prefix value and return its index in xml file. If it is already exist in xml file then simply return its index.
		/// </summary>
		/// <param name="companyPrefixValue">string of company prefix value</param>
		/// <returns>Uint16 value indicating the index of given company prefix value</returns>
		public UInt16 Insert(string companyPrefixValue)
		{			
			try
			{
				//chk--param--------------
				if( companyPrefixValue == null || companyPrefixValue.Length <= 0)
				{
					m_log.Trace("[CompanyPrefixLookupImpl::Insert] Error: Invalid(null) parameter 'companyPrefixValue' found");
					throw new CompanyPrefixLookupImplException("[CompanyPrefixLookupImpl::Insert] Error: Invalid(null) parameter 'companyPrefixValue' found");
				}
				//chk--param complete--------------

				UInt16 compIndex = 0 ;
				//Search company prefix in Hash
				bool bCompPreFound = false ;
				System.Collections.IDictionaryEnumerator iEr = this.mHshIdxComPrefix.GetEnumerator();

				while(iEr.MoveNext())
				{
					if(iEr.Value.ToString().Equals(companyPrefixValue) == true )
					{
						bCompPreFound = true ;
						compIndex = UInt16.Parse( iEr.Key.ToString() );
						break;
					}				
				}

				if(bCompPreFound == true)
				{
					m_log.Trace("[CompanyPrefixLookupImpl::Insert] OUT");
					return compIndex ;
				}
				else
				{
					string sError ;
					compIndex = CreateEntry(companyPrefixValue, out sError) ;
					if( compIndex > 0 )
					{
						m_log.Trace("[CompanyPrefixLookupImpl::Insert] OUT");
						return compIndex ;
					}
					else
						throw new CompanyPrefixLookupImplException("[CompanyPrefixLookupImpl::Insert] Error: Failed to create new Entry. " + sError);
				}
			}
			catch(CompanyPrefixLookupImplException ex)
			{
				//m_log.Trace(ex.Message);
				m_log.Error(ex.Message);
				throw ex;
			}
			catch(Exception ex)
			{
				//m_log.Trace("[CompanyPrefixLookupImpl::Insert] Failed: " + ex.Message);
				m_log.Error("[CompanyPrefixLookupImpl::Insert] Failed: " + ex.Message);
				throw new CompanyPrefixLookupImplException("[CompanyPrefixLookupImpl::Insert] Error: " + ex.Message);
			}
		}

		#endregion [ Public Methods ENDS ]
	}
}
