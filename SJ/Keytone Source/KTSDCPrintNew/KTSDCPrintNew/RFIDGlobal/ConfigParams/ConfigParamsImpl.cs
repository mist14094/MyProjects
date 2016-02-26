
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
using System.Xml ;
using System.Xml.Schema; 
using System.IO;
using System.Collections.Generic;

namespace KTone.RFIDGlobal.ConfigParams
{
	/// <summary>
	/// This class holds the implementation of IConfigParams interface.
	/// </summary>
    public class ConfigParamsImpl : BaseConfigParams
	{
        private static Hashtable m_globalFileInstances = new Hashtable();
        private static string m_baseDirPath = null; 

		#region [Attributes]

		private XmlDocument xDoc = null ;
		private Dictionary<string, string> m_configParamsHash = null ;
		private string m_xsdFilePath = string.Empty ;	
		private string m_className = string.Empty ;
		private string m_componentId = string.Empty ;
		private string m_xmlFilePath = string.Empty ;
		private static readonly string XSDFILENAME = "ConfigParams.xsd";
        private static string XML_BASE_PATH = @"\Data\ConfigParams\";
        private static string XSD_BASE_PATH = @"\XSD\ConfigParams\";
		private bool m_IsConfigFilePresent = false;
		private string m_SaveFileName = string.Empty;

		#endregion [Attributes] ENDS		
		
		#region [Constructors]

        static ConfigParamsImpl()
        {
            m_baseDirPath = AppDomain.CurrentDomain.BaseDirectory;
            // Check for the directory Data\ConfigParams\GlobalConfigs\KTone.Global.config
            if (!File.Exists(m_baseDirPath + XML_BASE_PATH + @"GlobalConfigs\KTone.Global.config"))
            {
                m_baseDirPath = GetDevBaseDir(m_baseDirPath);

            }
        }

        public string BaseDirPath
        {
            get
            {
                return m_baseDirPath;
            }
        }

		/// <summary>
		/// This constructor accepts the class name from where it was invoked.
		/// </summary>
		/// <param name="type">Type from where this constructor is called.</param>
		public ConfigParamsImpl(Type type)
		{
			if(type!= null)
			{
				string xsdPath	= string.Empty ;
				string xmlPath = string.Empty ;
				try
				{
					m_className = type.ToString() ;
					InitCtor(m_className,XSDFILENAME,true) ;
				}
				catch(ConfigParamsExceptionBase ex)
				{
					m_log.Error("ConfigParamsImpl(): ConfigParamsExceptionBase:Exception caught! Reason : "+ex.Message); 
					throw ex ;
				}
				catch(Exception ex)
				{
					m_log.Error("ConfigParamsImpl(): Exception caught! Reason : "+ex.Message); 
					throw ex ;
				}
			}
			else
			{
				//throw new ConfigParamExceptionConfigFileMissing() ;
				m_log.Error("ConfigParamsImpl(Type type) : Config File NOT found " ) ;
			}
		}


		/// <summary>
		/// This constructor accepts the class name from where it was invoked.
		/// </summary>
		/// <param name="type">Type from where this constructor is called.</param>
		/// <param name="componentId"></param>
		public ConfigParamsImpl(Type type,string componentId)
		{
			if(type!= null)
			{
				string xsdPath	= string.Empty ;
				string xmlPath = string.Empty ;
				try
				{
					m_className = type.ToString() ;
					m_componentId = componentId.ToUpper();
					InitCtor(m_className,XSDFILENAME,true) ;
				}
				catch(ConfigParamsExceptionBase ex)
				{
					m_log.Error("ConfigParamsImpl(): ConfigParamsExceptionBase:Exception caught! Reason : "+ex.Message); 
					throw ex ;
				}
				catch(Exception ex)
				{
					m_log.Error("ConfigParamsImpl(): Exception caught! Reason : "+ex.Message); 
					throw ex ;
				}
			}
			else
			{
				//throw new ConfigParamExceptionConfigFileMissing() ;
				m_log.Error("ConfigParamsImpl(Type type) : Config File NOT found " ) ;
			}
		}

		public ConfigParamsImpl(string nameConfigXmlFile)
		{
			try
			{
				m_className = GetClassName(nameConfigXmlFile) ;
				InitCtor(nameConfigXmlFile,m_xsdFilePath,false) ;
			}
			catch(ConfigParamsExceptionBase ex)
			{
				m_log.Error("ConfigParamsImpl(): ConfigParamsExceptionBase:Exception caught! Reason : "+ex.Message); 
				throw ex ;
			}
			catch(Exception ex)
			{
				m_log.Error("ConfigParamsImpl(): Exception caught! Reason : "+ex.Message); 
				throw ex ;
			}
        }

        #region GlobalConfig Read
        /// <summary>
        /// Used for Global File Config instances
        /// </summary>
        /// <param name="nameConfigXmlFile"></param>
        /// <param name="subDirectory"></param>
        private ConfigParamsImpl(string nameConfigXmlFile, string subDirectory)
        {
            try
            {
                m_className = GetClassName(nameConfigXmlFile);
                InitGlobalParamCtor(nameConfigXmlFile, m_xsdFilePath, subDirectory);
            }
            catch (ConfigParamsExceptionBase ex)
            {
                m_log.Error("ConfigParamsImpl(): ConfigParamsExceptionBase:Exception caught! Reason : " + ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                m_log.Error("ConfigParamsImpl(): Exception caught! Reason : " + ex.Message);
                throw ex;
            }
        }

        private void InitGlobalParamCtor(string className, string nameConfigXSDFile, string subDirectory)
        {
            try
            {
                m_log.Trace("[ConfigParamsImpl::ConfigParamsImpl] Entered in constructor.");

                m_IsConfigFilePresent = false;//At start config FILE NOT FOUND
                //chk-param------------------------

                m_xsdFilePath = nameConfigXSDFile;

                string pathXmlFile = string.Empty;
                string pathXsdFile = string.Empty;
                if (m_baseDirPath == null)
                {
                  //  m_baseDirPath = AppDomain.CurrentDomain.BaseDirectory;
                    new ConfigParamExceptionConfigFileMissing("[ConfigParamsImpl::ConfigParamsImpl] Error: Directory structure/KTone.Global.config missing . ");
                }

                //chk param complete---------------
                string baseDirPath = m_baseDirPath; 
                char[] slash = { '\\' };
                baseDirPath = baseDirPath.TrimEnd(slash);


                pathXmlFile = baseDirPath + XML_BASE_PATH + subDirectory;
                pathXmlFile += "\\";
                pathXmlFile += className; //user specified file name

                pathXsdFile = baseDirPath + XSD_BASE_PATH;
                pathXsdFile += XSDFILENAME;
                //LoadXml -----------------

                CheckFilePaths(pathXmlFile, pathXsdFile);

                m_xsdFilePath = pathXsdFile; //assign the new fully qualified path name
                m_xmlFilePath = pathXmlFile; //assign the new fully qualified path name

                string sError;

                if (LoadXmlFile(pathXmlFile, out sError) == false)
                {
                    m_log.Trace("[ConfigParamsImpl::LoadXmlFile] failed. " + sError);
                    throw new ConfigParamExceptionConfigFileMissing("[ConfigParamsImpl::ConfigParamsImpl] Error: Loading config xml file failed." + sError);
                }


                sError = string.Empty;
                //HashTable
                if (FillHashTbl(out sError) == false)
                {
                    m_log.Trace("[ConfigParamsImpl::FillHashTbl] failed. " + sError);
                    throw new ConfigParamsExceptionBase("[ConfigParamsImpl::FillHashTbl] Error: creating FillHashTbl failed." + sError);
                }

                m_log.Trace("[ConfigParamsImpl::InitCtor()] constructor is complete.");
            }
            catch (ConfigParamExceptionConfigFileMissing ex)
            {
                //m_log.Trace("[ConfigParamsImpl::ConfigParamsImpl] Failed: " + ex.Message);
                m_log.Error("[ConfigParamsImpl::ConfigParamsImpl] ConfigParamExceptionConfigFileMissing :Failed: " + ex.Message);
                //throw new ConfigParamsExceptionBase("[ConfigParamsImpl::ConfigParamsImpl] Error: " + ex.Message);
            }
            catch (ConfigParamsExceptionBase ex)
            {
                //m_log.Trace("[ConfigParamsImpl::ConfigParamsImpl] Failed: " + ex.Message);
                m_log.Error("[ConfigParamsImpl::ConfigParamsImpl] Failed: " + ex.Message);
                //throw new ConfigParamsExceptionBase("[ConfigParamsImpl::ConfigParamsImpl] Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                //m_log.Trace("[ConfigParamsImpl::ConfigParamsImpl] Failed: " + ex.Message);
                m_log.Error("[ConfigParamsImpl::ConfigParamsImpl] Failed: " + ex.Message);
                //throw new ConfigParamsExceptionBase("[ConfigParamsImpl::ConfigParamsImpl] Error: " + ex.Message);
            }
        }


        /// <summary>
        /// Singleton instance to lookup the values in the file name supplied
        /// </summary>
        /// <param name="configFileName"></param>
        /// <returns></returns>
        public static ConfigParamsImpl GetGlobalParamInstace(string configFileName)
        {
            ConfigParamsImpl objConfigParams = null;
            if( !m_globalFileInstances.ContainsKey(configFileName.ToUpper()) )
            {
                objConfigParams = new ConfigParamsImpl(configFileName, "GlobalConfigs");
                m_globalFileInstances[configFileName.ToUpper()] = objConfigParams;
            }
            objConfigParams = (ConfigParamsImpl)m_globalFileInstances[configFileName.ToUpper()];
            return objConfigParams;
        }

        #endregion GlobalConfig Read

        //
		//		public ConfigParamsImpl(string nameConfigXmlFile, string nameConfigXSDFile)
		//		{
		//			InitCtor(nameConfigXmlFile,nameConfigXSDFile) ;
		//		}

		internal string GetClassName(string xmlFileName)
		{
			string retClassName = string.Empty ;

			int lastIndex = xmlFileName.LastIndexOf(".config") ;

			if(lastIndex != -1)
			{
				retClassName = xmlFileName.Substring(0,lastIndex) ;
			}

			return retClassName ;
		}

		private void CreatePaths(string className,out string xmlFilePath,out string xsdFilePath,bool createFileName)
		{
			string baseDirPath = m_baseDirPath  ;
            if (baseDirPath == null)
            {
                new ConfigParamExceptionConfigFileMissing("[ConfigParamsImpl::ConfigParamsImpl] Error: Directory structure/KTone.Global.config missing . ");
            }  
			char[] slash = {'\\'};
			baseDirPath = baseDirPath.TrimEnd(slash) ;

            xmlFilePath = baseDirPath + XML_BASE_PATH;

            xsdFilePath = baseDirPath + XSD_BASE_PATH;

            if (createFileName)
                xmlFilePath += className + ".config";
            else
                xmlFilePath += className; //user specified file name

			xsdFilePath += XSDFILENAME ;
		}

        internal static string GetDevBaseDir(string path)
        {
            try
            {
                if (path == null)
                    return null;

                DirectoryInfo info = new DirectoryInfo(path);
                // If the path is root it self
                if (info.FullName == info.Root.FullName)
                {
                    return null;
                }
                DirectoryInfo parentInfo = Directory.GetParent(path);
                if (parentInfo == null) // it should not happen as we have checked that curDir is not root
                    return null;
                if (File.Exists(parentInfo.FullName + XML_BASE_PATH + @"GlobalConfigs\KTone.Global.config"))
                    return parentInfo.FullName;
                else
                    return GetDevBaseDir(parentInfo.FullName);
            }
            catch (Exception )
            {
                return null; 
            }
        }

		private void InitCtor(string className, string nameConfigXSDFile,bool createFileName)
		{
			try
			{
				m_log.Trace("[ConfigParamsImpl::ConfigParamsImpl] Entered in constructor.");

                //At start config FILE NOT FOUND
				m_IsConfigFilePresent = false ;
				//chk-param------------------------
				
				m_xsdFilePath = nameConfigXSDFile ;

				string pathXmlFile = string.Empty ;
				string pathXsdFile = string.Empty;

                CreatePaths(className, out pathXmlFile, out pathXsdFile, createFileName);
				//chk param complete---------------

				//LoadXml -----------------

                CheckFilePaths(pathXmlFile, pathXsdFile);

				m_xsdFilePath = pathXsdFile ; //assign the new fully qualified path name
				m_xmlFilePath = pathXmlFile ; //assign the new fully qualified path name

				string sError ;

				if ( LoadXmlFile(pathXmlFile, out sError) == false) 
				{
					m_log.Trace("[ConfigParamsImpl::LoadXmlFile] failed. " + sError);
					throw new ConfigParamExceptionConfigFileMissing("[ConfigParamsImpl::ConfigParamsImpl] Error: Loading config xml file failed." +  sError);
				}


				sError = string.Empty ;
				//HashTable
				if ( FillHashTbl(out sError) == false) 
				{
					m_log.Trace("[ConfigParamsImpl::FillHashTbl] failed. " + sError);
					throw new ConfigParamsExceptionBase("[ConfigParamsImpl::FillHashTbl] Error: creating FillHashTbl failed." +  sError);
				}

				m_log.Trace("[ConfigParamsImpl::InitCtor()] constructor is complete.");
			}
			catch(ConfigParamExceptionConfigFileMissing ex)
			{
				//m_log.Trace("[ConfigParamsImpl::ConfigParamsImpl] Failed: " + ex.Message);
				m_log.Error("[ConfigParamsImpl::ConfigParamsImpl] ConfigParamExceptionConfigFileMissing :Failed: " + ex.Message);
				//throw new ConfigParamsExceptionBase("[ConfigParamsImpl::ConfigParamsImpl] Error: " + ex.Message);
			}
			catch(ConfigParamsExceptionBase ex)
			{
				//m_log.Trace("[ConfigParamsImpl::ConfigParamsImpl] Failed: " + ex.Message);
				m_log.Error("[ConfigParamsImpl::ConfigParamsImpl] Failed: " + ex.Message);
				//throw new ConfigParamsExceptionBase("[ConfigParamsImpl::ConfigParamsImpl] Error: " + ex.Message);
			}
			catch(Exception ex)
			{
				//m_log.Trace("[ConfigParamsImpl::ConfigParamsImpl] Failed: " + ex.Message);
				m_log.Error("[ConfigParamsImpl::ConfigParamsImpl] Failed: " + ex.Message);
				//throw new ConfigParamsExceptionBase("[ConfigParamsImpl::ConfigParamsImpl] Error: " + ex.Message);
			}
		}

        private void CheckFilePaths(string pathXmlFile, string pathXsdFile)
        {
            if (pathXmlFile == null || pathXmlFile.Length <= 0)
                throw new ConfigParamExceptionConfigFileMissing("[ConfigParamsImpl::ConfigParamsImpl] Error: Invalid(Empty) parameter 'pathXmlFile' found. ");

            if (System.IO.File.Exists(pathXmlFile) == false)
                throw new ConfigParamExceptionConfigFileMissing("[ConfigParamsImpl::ConfigParamsImpl] Error: Invalid filePath \r\n"
                    + pathXmlFile + "\r\n found for Config xml file, File not found. path should be "
                    + "\r\n['directory name' + 'file name'].");

            if (pathXsdFile == null || pathXsdFile.Length <= 0)
                throw new ConfigParamExceptionConfigFileMissing("[ConfigParamsImpl::ConfigParamsImpl] Error: Invalid(Empty) parameter 'pathXsdFile' found. ");

            if (System.IO.File.Exists(pathXsdFile) == false)
                throw new ConfigParamExceptionConfigFileMissing("[ConfigParamsImpl::ConfigParamsImpl] Error: Invalid XSD filePath \r\n"
                    + pathXsdFile + "\r\n found for Config xml file, File not found. path should be "
                    + "\r\n['directory name' + 'file name'].");
        }


		/// <summary>
		/// Validate current opened Config Xml.
		/// </summary>
		/// <param name="logStr">error string if any invalid structure found otherwise return empty.</param>
		/// <returns>return false if any invalid structure found otherwise return true.</returns>
		private bool ValidateXml(out string logStr)
		{
			try
			{ 	
				m_log.Trace("[ConfigParamsImpl::Validate] IN");
				if(this.xDoc != null)
				{
					string xmlTmplt = this.xDoc.OuterXml; 
					System.IO.MemoryStream configStream = new System.IO.MemoryStream(System.Text.Encoding.ASCII.GetBytes(xmlTmplt)); 
					//					System.IO.FileStream fStream = new System.IO.FileStream(m_xmlFilePath,System.IO.FileMode.Open) ;
                    ValidateXMLLocal(m_xsdFilePath, new System.IO.StreamReader(configStream));
					logStr = string.Empty;
					if(m_log.IsTraceEnabled)
					{
						m_log.Trace("[ConfigParamsImpl::Validate] The XSD schema File Path is : ") ;
					    m_log.Trace(m_xsdFilePath) ;
						m_log.Trace("[ConfigParamsImpl::Validate] The XML File Path is : ") ;
						m_log.Trace(m_xmlFilePath) ;
					}
					m_log.Trace("[ConfigParamsImpl::Validate] OUT");
					return true;
				}
				else
					throw new ConfigParamsExceptionBase("[ConfigParamsImpl::Validate] failed : Null xDoc object found.Template is not open.");
			}
			catch(ConfigParamsExceptionBase ex)
			{
				logStr = ex.Message;
				m_log.Error("[ConfigParamsImpl::Validate] Failed: " + ex.Message);
				return false;
			}
			catch(Exception ex)
			{				
				logStr = ex.Message;
				m_log.Error("[ConfigParamsImpl::Validate] Failed: " + ex.Message);
				return false;
			}			
		}//Validate


		private bool FillHashTbl(out string err)
		{
			err = string.Empty ;
			m_log.Trace("[ConfigParamsImpl::FillHashTbl] IN.");

			//this.mHshIdxComPrefix = new Hashtable();

			m_configParamsHash = new Dictionary<string, string>() ;

			try
			{
				if(this.xDoc == null)
					throw new ConfigParamsExceptionBase("[ConfigParamsImpl::FillHashTable] Error: Null xml object found.");

				//Scanning for the matching class name depending 
				//on the name of class provided through the constructor.
				
				XmlNodeList ndListClassName;
				ndListClassName = this.xDoc.GetElementsByTagName("Class");
				
				//The parameter name is made case INSENSITIVE. as well as checking 
				//against the class name which too the user can enter in the config file
				// in any way he wants...
				if((ndListClassName!=null) && (ndListClassName.Count > 0))
				{
					#region Search ComponentId
					//Check if there are settings available specific to given ComponentId.
					//If not, use default settings.
					bool useDefaultSettings = true;
					if(m_componentId != string.Empty)
					{
						foreach(XmlNode xNd in ndListClassName)
						{
							try
							{
								string className = xNd.Attributes.GetNamedItem("name").Value.ToUpper();
								if((m_className != null) &&(m_className.ToUpper().Equals(className)))
								{
									XmlAttributeCollection attribs =  xNd.Attributes;
								
									XmlNode idNode = attribs.GetNamedItem("ComponentId");
									if(idNode != null && idNode.InnerText == m_componentId)
									{
										useDefaultSettings = false;
										break;
									}
								}
							}
							catch(Exception ex)
							{
								m_log.Error("ConfigParamsImpl:FillHashTbl() Exception caught While component id! Reason : "+ex.Message) ;
								continue ;
							}
						}
					}
					#endregion Search ComponentId


					foreach(XmlNode xNd in ndListClassName)
					{
						try
						{
							string className = string.Empty ;
							className = xNd.Attributes.GetNamedItem("name").Value;
							className = className.ToUpper() ;

							if((m_className != null) &&(m_className.ToUpper().Equals(className)))
							{
								XmlAttributeCollection attribs =  xNd.Attributes;
								
								XmlNode idNode = attribs.GetNamedItem("ComponentId");
								if(!useDefaultSettings)
								{
									if(idNode == null || idNode.InnerText != m_componentId)
										continue;
								}
								else
								{
									if(idNode != null) 
										continue;
								}

								XmlNodeList attribNodesList =  xNd.ChildNodes ;
								if((attribNodesList != null)&&(attribNodesList.Count > 0))
								{
									foreach(XmlNode attribNode in attribNodesList)
									{
										if(attribNode.NodeType != XmlNodeType.Element)
											continue;
										string paramName = string.Empty ;
										string paramValue = string.Empty ;
										try
										{
											
											paramName = attribNode.Attributes.GetNamedItem("name").Value ;

											bool newAttrPresent = checkForAttrNewvalue(paramName,attribNode );
											if(newAttrPresent)
											{
												paramValue = attribNode.Attributes.GetNamedItem("newvalue").Value ;
											}
											else
											{
												paramValue = attribNode.Attributes.GetNamedItem("value").Value ;
											}
											paramName = paramName.ToUpper() ;//Make it case insensitive.
											//If hashtable already has the same parameter then we should 
											//ignore the second attribute with same parameter name
											if(!m_configParamsHash.ContainsKey(paramName))
											{
												m_configParamsHash[paramName] = paramValue ;
											}
											//m_configParamsHash[paramName] = paramValue ;
										}
										catch(Exception ex)
										{
											m_log.Error("ConfigParamsImpl:FillHashTbl() Exception caught While Extracting Name/Value! Reason : "+ex.Message) ;
											continue ;
										}
									}
									break;
								}
							}
						}
						catch(Exception ex)
						{
							m_log.Error("ConfigParamsImpl:FillHashTbl() Exception caught! Reason : "+ex.Message) ;
							continue ;
						}
					}
				}

				//				if(ndList != null)
				//				{
				//					foreach(XmlNode xNd in ndList)
				//					{
				//						UInt16 companyIdx = UInt16.Parse(xNd.Attributes.GetNamedItem("index").Value);
				//						if(this.mMaxIndex < companyIdx)
				//							this.mMaxIndex = companyIdx ;
				//
				//						string sComPrefix = xNd.Attributes.GetNamedItem("companyPrefix").Value;
				//						this.mHshIdxComPrefix.Add(companyIdx, sComPrefix);
				//					}
				//				}
				//				
				//				if(this.mMaxIndex <= 1023 )
				//					this.mMaxIndex = 1023 ;
				//
				m_log.Trace("[ConfigParamsImpl::FillHashTbl] OUT.");
				return true ;
			}
			catch(Exception ex)
			{
				err = ex.Message ;
				m_log.Error("[ConfigParamsImpl::FillHashTbl] Error: " + ex.Message);
				return false ;
			}
		}

		private bool checkForAttrNewvalue(string paramName, XmlNode curAttribNode)
		{
			try
			{
				if(curAttribNode.Attributes.GetNamedItem("newvalue") == null)
					return false;
				string localParamValue = curAttribNode.Attributes.GetNamedItem("newvalue").Value ;
			}
			catch(Exception e)
			{
				string errMsg = e.Message + "\n"+ "The newvalue attribute is not present for this Attribute Node." ;
				return false;

			}
			return true;
		}

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
				m_log.Trace("[ConfigParamsImpl::LoadXmlFile] IN." );
				
				xDoc = new XmlDocument();
				xDoc.Load(xmlFile);		
				m_IsConfigFilePresent = true ; // The config file is present set only here.
				m_log.Debug("LoadXmlFile() : Is config file Present ? : "+m_IsConfigFilePresent) ;
				if( ValidateXml(out sError) == false )
					throw new ConfigParamsExceptionBase("Validation failed for Class Specific XML config file = " + xmlFile + " Error = " + sError);
				
				m_log.Trace("[ConfigParamsImpl::LoadXmlFile] OUT." );
				m_SaveFileName = xmlFile;
				return true ;
			}
			catch(XmlException ex)
			{
				sError = ex.Message ;
				m_log.Error("[ConfigParamsImpl::LoadXmlFile] Failed: " + ex.Message);
				return false ;
			}
			catch(ConfigParamsExceptionBase ex)
			{
				sError = ex.Message;
				m_log.Error("[ConfigParamsImpl::LoadXmlFile] Failed: " + ex.Message);
				return false;
			}
			catch(Exception ex)
			{
				sError = ex.Message ;
				m_log.Error("[ConfigParamsImpl::LoadXmlFile] Failed: " + ex.Message);
				return false ;
			}
		}


		#endregion [Constructors] ENDS


		#region [IConfigParams Overridden Members]
		
		#region LookUp Methods
		//		public override bool IsPresent(string name)
		//		{
		//			// TODO:  Add ConfigParams.IsPresent implementation
		//			return false;
		//		}

		public override void Lookup(string name, out string paramVal)
		{
			// TODO:  Add ConfigParams.Lookup implementation
			paramVal = null;

			object tempVal = null ;
			
			GetValFromHash_ThrowEx(name,out tempVal) ;
			try
			{
				if(tempVal == null)
				{
					throw new ConfigParamExceptionValueMissing("Lookup(): Value of Parameter "+name+" is NULL") ;
				}
				else
				{
					paramVal = Convert.ToString(tempVal) ;
				}
			}
			catch(Exception e)
			{
				m_log.Error("Lookup(): Error while casting to outparam : "+e.Message) ;
				throw new ConfigParamsExceptionBase("Lookup(): Error while casting to outparam : "+e.Message) ;
			}

			m_log.Debug("[ConfigParamsImpl::Lookup] Value for parameter "+name+" is set to "+paramVal) ;
		}

		public override void Lookup(string name, out bool paramVal)
		{
			// TODO:  Add ConfigParams.Lookup implementation
			paramVal = false;

			object tempVal = null ;
			
			GetValFromHash_ThrowEx(name,out tempVal) ;
			try
			{
				if(tempVal == null)
				{
					throw new ConfigParamExceptionValueMissing("Lookup(): Value of Parameter "+name+" is NULL") ;
				}
				else
				{
					paramVal = Convert.ToBoolean(tempVal) ;
				}
			}
			catch(Exception e)
			{
				m_log.Error("Lookup(): Error while casting to outparam : "+e.Message) ;
				throw new ConfigParamsExceptionBase("Lookup(): Error while casting to outparam : "+e.Message) ;
			}

			m_log.Debug("[ConfigParamsImpl::Lookup] Value for parameter "+name+" is set to "+paramVal) ;

		}

        public override void Lookup(string name, out byte paramVal)
        {
            // TODO:  Add ConfigParams.Lookup implementation
            paramVal = new Byte();

            object tempVal = null;

            GetValFromHash_ThrowEx(name, out tempVal);
            try
            {
                if (tempVal == null)
                {
                    throw new ConfigParamExceptionValueMissing("Lookup(): Value of Parameter " + name + " is NULL");
                }
                else
                {
                    paramVal = Convert.ToByte(tempVal);
                }
            }
            catch (Exception e)
            {
                m_log.Error("Lookup(): Error while casting to outparam : " + e.Message);
                throw new ConfigParamsExceptionBase("Lookup(): Error while casting to outparam : " + e.Message);
            }

            m_log.Debug("[ConfigParamsImpl::Lookup] Value for parameter " + name + " is set to " + paramVal);
        }

		public override void Lookup(string name, out Int16 paramVal)
		{
			// TODO:  Add ConfigParams.Lookup implementation
			paramVal = new Int16 ();

			object tempVal = null ;
			
			GetValFromHash_ThrowEx(name,out tempVal) ;
			try
			{
				if(tempVal == null)
				{
					throw new ConfigParamExceptionValueMissing("Lookup(): Value of Parameter "+name+" is NULL") ;
				}
				else
				{
					paramVal = Convert.ToInt16(tempVal) ;
				}
			}
			catch(Exception e)
			{
				m_log.Error("Lookup(): Error while casting to outparam : "+e.Message) ;
				throw new ConfigParamsExceptionBase("Lookup(): Error while casting to outparam : "+e.Message) ;
			}

			m_log.Debug("[ConfigParamsImpl::Lookup] Value for parameter "+name+" is set to "+paramVal) ;
		}

		public override void Lookup(string name, out Int32 paramVal)
		{
			// TODO:  Add ConfigParams.Lookup implementation
			paramVal = new Int32 ();

			object tempVal = null ;
			
			GetValFromHash_ThrowEx(name,out tempVal) ;
			try
			{
				if(tempVal == null)
				{
					throw new ConfigParamExceptionValueMissing("Lookup(): Value of Parameter "+name+" is NULL") ;
				}
				else
				{
					paramVal = Convert.ToInt32(tempVal) ;
				}
			}
			catch(Exception e)
			{
				m_log.Error("Lookup(): Error while casting to outparam : "+e.Message) ;
				throw new ConfigParamsExceptionBase("Lookup(): Error while casting to outparam : "+e.Message) ;
			}

			m_log.Debug("[ConfigParamsImpl::Lookup] Value for parameter "+name+" is set to "+paramVal) ;
		}

		public override void Lookup(string name, out Int64 paramVal)
		{
			// TODO:  Add ConfigParams.Lookup implementation
			paramVal = new Int64 ();

			object tempVal = null ;
			
			GetValFromHash_ThrowEx(name,out tempVal) ;
			try
			{
				if(tempVal == null)
				{
					throw new ConfigParamExceptionValueMissing("Lookup(): Value of Parameter "+name+" is NULL") ;
				}
				else
				{
					paramVal = Convert.ToInt64(tempVal) ;
				}
			}
			catch(Exception e)
			{
				m_log.Error("Lookup(): Error while casting to outparam : "+e.Message) ;
				throw new ConfigParamsExceptionBase("Lookup(): Error while casting to outparam : "+e.Message) ;
			}

			m_log.Debug("[ConfigParamsImpl::Lookup] Value for parameter "+name+" is set to "+paramVal) ;
		}

		public override void Lookup(string name, out UInt16 paramVal)
		{
			// TODO:  Add ConfigParams.Lookup implementation
			paramVal = new UInt16 ();
			object tempVal = null ;
			
			GetValFromHash_ThrowEx(name,out tempVal) ;
			try
			{
				if(tempVal == null)
				{
					throw new ConfigParamExceptionValueMissing("Lookup(): Value of Parameter "+name+" is NULL") ;
				}
				else
				{
					paramVal = Convert.ToUInt16(tempVal) ;
				}
			}
			catch(Exception e)
			{
				m_log.Error("Lookup(): Error while casting to outparam : "+e.Message) ;
				throw new ConfigParamsExceptionBase("Lookup(): Error while casting to outparam : "+e.Message) ;
			}

			m_log.Debug("[ConfigParamsImpl::Lookup] Value for parameter "+name+" is set to "+paramVal) ;
		}

		
		public override void Lookup(string name, out UInt32 paramVal)
		{
			// TODO:  Add ConfigParams.Lookup implementation
			paramVal = new UInt32 ();
			object tempVal = null ;
			
			GetValFromHash_ThrowEx(name,out tempVal) ;
			try
			{
				if(tempVal == null)
				{
					throw new ConfigParamExceptionValueMissing("Lookup(): Value of Parameter "+name+" is NULL") ;
				}
				else
				{
					paramVal = Convert.ToUInt32(tempVal) ;
				}
			}
			catch(Exception e)
			{
				m_log.Error("Lookup(): Error while casting to outparam : "+e.Message) ;
				throw new ConfigParamsExceptionBase("Lookup(): Error while casting to outparam : "+e.Message) ;
			}

			m_log.Debug("[ConfigParamsImpl::Lookup] Value for parameter "+name+" is set to "+paramVal) ;
		}


		public override void Lookup(string name, out UInt64 paramVal)
		{
			// TODO:  Add ConfigParams.Lookup implementation
			paramVal = new UInt64 ();

			object tempVal = null ;
			
			GetValFromHash_ThrowEx(name,out tempVal) ;
			try
			{
				if(tempVal == null)
				{
					throw new ConfigParamExceptionValueMissing("Lookup(): Value of Parameter "+name+" is NULL") ;
				}
				else
				{
					paramVal = Convert.ToUInt64(tempVal) ;
				}
			}
			catch(Exception e)
			{
				m_log.Error("Lookup(): Error while casting to outparam : "+e.Message) ;
				throw new ConfigParamsExceptionBase("Lookup(): Error while casting to outparam : "+e.Message) ;
			}

			m_log.Debug("[ConfigParamsImpl::Lookup] Value for parameter "+name+" is set to "+paramVal) ;
		}

		public override void Lookup(string name, out float paramVal)
		{
			// TODO:  Add ConfigParams.Lookup implementation
			paramVal = 0;

			object tempVal = null ;
			
			GetValFromHash_ThrowEx(name,out tempVal) ;
			try
			{
				if(tempVal == null)
				{
					throw new ConfigParamExceptionValueMissing("Lookup(): Value of Parameter "+name+" is NULL") ;
				}
				else
				{
					paramVal = (float)(tempVal) ;
				}
			}
			catch(Exception e)
			{
				m_log.Error("Lookup(): Error while casting to outparam : "+e.Message) ;
				throw new ConfigParamsExceptionBase("Lookup(): Error while casting to outparam : "+e.Message) ;
			}

			m_log.Debug("[ConfigParamsImpl::Lookup] Value for parameter "+name+" is set to "+paramVal) ;
		}

		public override void Lookup(string name, out double paramVal)
		{
			// TODO:  Add ConfigParams.Lookup implementation
			paramVal = 0;

			object tempVal = null ;
			
			GetValFromHash_ThrowEx(name,out tempVal) ;
			try
			{
				if(tempVal == null)
				{
					throw new ConfigParamExceptionValueMissing("Lookup(): Value of Parameter "+name+" is NULL") ;
				}
				else
				{
					paramVal = Convert.ToDouble(tempVal) ;
				}
			}
			catch(Exception e)
			{
				m_log.Error("Lookup(): Error while casting to outparam : "+e.Message) ;
				throw new ConfigParamsExceptionBase("Lookup(): Error while casting to outparam : "+e.Message) ;
			}

			m_log.Debug("[ConfigParamsImpl::Lookup] Value for parameter "+name+" is set to "+paramVal) ;
		}

		public override void Lookup(string name, string defaultVal, out string paramVal)
		{
			// TODO:  Add ConfigParams.Lookup implementation
			paramVal = null;

			//Anything happens , make sure default value is assigned to the parameter value.
			paramVal  =	 defaultVal ;
			object tempVal = null ;
			
			try
			{
				GetValFromHash_CatchEx(name,out tempVal) ;
				if(tempVal == null)
				{
					throw new ConfigParamExceptionValueMissing("Lookup(): Value of Parameter "+name+" is NULL") ;
				}
				else
				{
					paramVal = Convert.ToString(tempVal) ;
				}
			}
			catch(Exception e)
			{
				//Anything happens , make sure default value is assigned to the parameter value.
				paramVal  =	 defaultVal ;
				m_log.Error("Lookup(): Error while casting to outparam : "+e.Message) ;
			}

			m_log.Debug("[ConfigParamsImpl::Lookup] Value for parameter "+name+" is set to "+paramVal) ;
		}

		public override void Lookup(string name, bool defaultVal, out bool paramVal)
		{
			// TODO:  Add ConfigParams.Lookup implementation
			paramVal = false;

			//Anything happens , make sure default value is assigned to the parameter value.
			paramVal  =	 defaultVal ;
			object tempVal = null ;
			
			try
			{
				GetValFromHash_CatchEx(name,out tempVal) ;
				if(tempVal == null)
				{
					throw new ConfigParamExceptionValueMissing("Lookup(): Value of Parameter "+name+" is NULL") ;
				}
				else
				{
					paramVal = Convert.ToBoolean(tempVal) ;
				}
			}
			catch(Exception e)
			{
				//Anything happens , make sure default value is assigned to the parameter value.
				paramVal  =	 defaultVal ;
				m_log.Error("Lookup(): Error while casting to outparam : "+e.Message) ;
			}

			m_log.Debug("[ConfigParamsImpl::Lookup] Value for parameter "+name+" is set to "+paramVal) ;
		}

        public override void Lookup(string name, Byte defaultVal, out Byte paramVal)
        {
            // TODO:  Add ConfigParams.Lookup implementation
            paramVal = new Byte();

            //Anything happens , make sure default value is assigned to the parameter value.
            paramVal = defaultVal;
            object tempVal = null;

            try
            {
                GetValFromHash_CatchEx(name, out tempVal);
                if (tempVal == null)
                {
                    throw new ConfigParamExceptionValueMissing("Lookup(): Value of Parameter " + name + " is NULL");
                }
                else
                {
                    paramVal = Convert.ToByte(tempVal);
                }
            }
            catch (Exception e)
            {
                //Anything happens , make sure default value is assigned to the parameter value.
                paramVal = defaultVal;
                m_log.Error("Lookup(): Error while casting to outparam : " + e.Message);
            }

            m_log.Debug("[ConfigParamsImpl::Lookup] Value for parameter " + name + " is set to " + paramVal);
        }

		public override void Lookup(string name, Int16 defaultVal, out Int16 paramVal)
		{
			// TODO:  Add ConfigParams.Lookup implementation
			paramVal = new Int16 ();

			//Anything happens , make sure default value is assigned to the parameter value.
			paramVal  =	 defaultVal ;
			object tempVal = null ;
			
			try
			{
				GetValFromHash_CatchEx(name,out tempVal) ;
				if(tempVal == null)
				{
					throw new ConfigParamExceptionValueMissing("Lookup(): Value of Parameter "+name+" is NULL") ;
				}
				else
				{
					paramVal = Convert.ToInt16(tempVal) ;
				}
			}
			catch(Exception e)
			{
				//Anything happens , make sure default value is assigned to the parameter value.
				paramVal  =	 defaultVal ;
				m_log.Error("Lookup(): Error while casting to outparam : "+e.Message) ;
			}

			m_log.Debug("[ConfigParamsImpl::Lookup] Value for parameter "+name+" is set to "+paramVal) ;
		}

		public override void Lookup(string name, Int32 defaultVal, out Int32 paramVal)
		{
			// TODO:  Add ConfigParams.Lookup implementation
			paramVal = new Int32 ();

			//Anything happens , make sure default value is assigned to the parameter value.
			paramVal  =	 defaultVal ;
			object tempVal = null ;
			
			try
			{
				GetValFromHash_CatchEx(name,out tempVal) ;
				if(tempVal == null)
				{
					throw new ConfigParamExceptionValueMissing("Lookup(): Value of Parameter "+name+" is NULL") ;
				}
				else
				{
					paramVal = Convert.ToInt32(tempVal) ;
				}
			}
			catch(Exception e)
			{
				//Anything happens , make sure default value is assigned to the parameter value.
				paramVal  =	 defaultVal ;
				m_log.Error("Lookup(): Error while casting to outparam : "+e.Message) ;
			}

			m_log.Debug("[ConfigParamsImpl::Lookup] Value for parameter "+name+" is set to "+paramVal) ;
		}

		public override void Lookup(string name, Int64 defaultVal, out Int64 paramVal)
		{
			// TODO:  Add ConfigParams.Lookup implementation
			paramVal = new Int64 ();

			//Anything happens , make sure default value is assigned to the parameter value.
			paramVal  =	 defaultVal ;
			object tempVal = null ;
			
			try
			{
				GetValFromHash_CatchEx(name,out tempVal) ;
				if(tempVal == null)
				{
					throw new ConfigParamExceptionValueMissing("Lookup(): Value of Parameter "+name+" is NULL") ;
				}
				else
				{
					paramVal = Convert.ToInt64(tempVal) ;
				}
			}
			catch(Exception e)
			{
				//Anything happens , make sure default value is assigned to the parameter value.
				paramVal  =	 defaultVal ;
				m_log.Error("Lookup(): Error while casting to outparam : "+e.Message) ;
			}

			m_log.Debug("[ConfigParamsImpl::Lookup] Value for parameter "+name+" is set to "+paramVal) ;
		}

		public override void Lookup(string name, UInt16 defaultVal, out UInt16 paramVal)
		{
			// TODO:  Add ConfigParams.Lookup implementation
			//Anything happens , make sure default value is assigned to the parameter value.
			paramVal  =	 defaultVal ;
			object tempVal = null ;
			
			try
			{
				GetValFromHash_CatchEx(name,out tempVal) ;
				if(tempVal == null)
				{
					throw new ConfigParamExceptionValueMissing("Lookup(): Value of Parameter "+name+" is NULL") ;
				}
				else
				{
					paramVal = Convert.ToUInt16(tempVal) ;
				}
			}
			catch(Exception e)
			{
				//Anything happens , make sure default value is assigned to the parameter value.
				paramVal  =	 defaultVal ;
				m_log.Error("Lookup(): Error while casting to outparam : "+e.Message) ;
			}

			m_log.Debug("[ConfigParamsImpl::Lookup] Value for parameter "+name+" is set to "+paramVal) ;
		}

		
		public override void Lookup(string name, UInt32 defaultVal, out UInt32 paramVal)
		{
			// TODO:  Add ConfigParams.Lookup implementation
			//Anything happens , make sure default value is assigned to the parameter value.
			paramVal  =	 defaultVal ;
			object tempVal = null ;
			
			try
			{
				GetValFromHash_CatchEx(name,out tempVal) ;
				if(tempVal == null)
				{
					throw new ConfigParamExceptionValueMissing("Lookup(): Value of Parameter "+name+" is NULL") ;
				}
				else
				{
					paramVal = Convert.ToUInt32(tempVal) ;
				}
			}
			catch(Exception e)
			{
				//Anything happens , make sure default value is assigned to the parameter value.
				paramVal  =	 defaultVal ;
				m_log.Error("Lookup(): Error while casting to outparam : "+e.Message) ;
			}

			m_log.Debug("[ConfigParamsImpl::Lookup] Value for parameter "+name+" is set to "+paramVal) ;
		}

		public override void Lookup(string name, UInt64 defaultVal, out UInt64 paramVal)
		{
			// TODO:  Add ConfigParams.Lookup implementation
			paramVal = new UInt64 ();

			//Anything happens , make sure default value is assigned to the parameter value.
			paramVal  =	 defaultVal ;
			object tempVal = null ;
			
			try
			{
				GetValFromHash_CatchEx(name,out tempVal) ;
				if(tempVal == null)
				{
					throw new ConfigParamExceptionValueMissing("Lookup(): Value of Parameter "+name+" is NULL") ;
				}
				else
				{
					paramVal = Convert.ToUInt64(tempVal) ;
				}
			}
			catch(Exception e)
			{
				//Anything happens , make sure default value is assigned to the parameter value.
				paramVal  =	 defaultVal ;
				m_log.Error("Lookup(): Error while casting to outparam : "+e.Message) ;
			}

			m_log.Debug("[ConfigParamsImpl::Lookup] Value for parameter "+name+" is set to "+paramVal) ;
		}

		public override void Lookup(string name, float defaultVal, out float paramVal)
		{
			// TODO:  Add ConfigParams.Lookup implementation
			paramVal = 0;

			//Anything happens , make sure default value is assigned to the parameter value.
			paramVal  =	 defaultVal ;
			object tempVal = null ;
			
			try
			{
				GetValFromHash_CatchEx(name,out tempVal) ;
				if(tempVal == null)
				{
					throw new ConfigParamExceptionValueMissing("Lookup(): Value of Parameter "+name+" is NULL") ;
				}
				else
				{
					paramVal = (float)(tempVal) ;
				}
			}
			catch(Exception e)
			{
				//Anything happens , make sure default value is assigned to the parameter value.
				paramVal  =	 defaultVal ;
				m_log.Error("Lookup(): Error while casting to outparam : "+e.Message) ;
			}

			m_log.Debug("[ConfigParamsImpl::Lookup] Value for parameter "+name+" is set to "+paramVal) ;
		}

		public override void Lookup(string name, double defaultVal, out double paramVal)
		{
			// TODO:  Add ConfigParams.Lookup implementation
			paramVal = 0;

			//Anything happens , make sure default value is assigned to the parameter value.
			paramVal  =	 defaultVal ;
			object tempVal = null ;
			
			try
			{
				GetValFromHash_CatchEx(name,out tempVal) ;
				if(tempVal == null)
				{
					throw new ConfigParamExceptionValueMissing("Lookup(): Value of Parameter "+name+" is NULL") ;
				}
				else
				{
					paramVal = Convert.ToDouble(tempVal) ;
				}
			}
			catch(Exception e)
			{
				//Anything happens , make sure default value is assigned to the parameter value.
				paramVal  =	 defaultVal ;
				m_log.Error("Lookup(): Error while casting to outparam : "+e.Message) ;
			}

			m_log.Debug("[ConfigParamsImpl::Lookup] Value for parameter "+name+" is set to "+paramVal) ;
		}
		

		#endregion LookUp Methods

        public override List<string> GetAttributeNames()
        {
            if (this.m_configParamsHash != null)
            {
                List<string> attributeNames = new List<string>(this.m_configParamsHash.Keys);
                return attributeNames;
            }
            return null;
        }

		public override void GetLimits(out string defVal,out string minVal,out string maxVal)
		{
			defVal = null ;
			minVal = null ;
			maxVal = null ;
		}


		#region Save Methods
		

		/// <summary>
		/// This method will add newValue attribute for the given parameter name if it exist in the config xml.
		/// If given parameter does not exist in the config xml then it will create new attribue with that name and value.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="paramVal"></param>
		public override void Save(string name, string paramVal)
		{
			#region Code
			m_log.Trace("[ConfigParamsImpl::Save] IN.");
			try
			{
				if(this.xDoc == null)
					throw new ConfigParamsExceptionBase("[ConfigParamsImpl::FillHashTable] Error: Null xml object found.");

				//Scanning for the matching class name depending 
				//on the name of class provided through the constructor.
				
				XmlNodeList ndListClassName;
				ndListClassName = this.xDoc.GetElementsByTagName("Class");
				
				//The parameter name is made case INSENSITIVE. as well as checking 
				//against the class name which too the user can enter in the config file
				// in any way he wants...
				if((ndListClassName!=null) && (ndListClassName.Count > 0))
				{
					foreach(XmlNode xNd in ndListClassName)
					{
						StreamWriter streamWriterMod = null;
						try
						{
							string className = string.Empty ;
							className = xNd.Attributes.GetNamedItem("name").Value;
							className = className.ToUpper() ;
							string paramName = string.Empty ;
							if((m_className != null) &&(m_className.ToUpper().Equals(className)))
							{
								XmlNodeList attribNodesList =  xNd.ChildNodes ;
								if((attribNodesList != null)&&(attribNodesList.Count > 0))
								{
									bool flag = false;
									foreach(XmlNode attribNode in attribNodesList)
									{
										if(attribNode.NodeType != XmlNodeType.Element)
											continue;
										try
										{
											
											paramName = attribNode.Attributes.GetNamedItem("name").Value ;
											if (paramName == name)
											{
												flag = true;
												streamWriterMod = new StreamWriter( m_xmlFilePath);
												XmlAttribute newAttr = xDoc.CreateAttribute("newvalue");
												newAttr.Value = paramVal.ToString () ;
												attribNode.Attributes.Append(newAttr);
												xDoc.Save(streamWriterMod);
												streamWriterMod.Close();
												break;
											}								
																					
										}
										catch(Exception ex)
										{
											m_log.Error("ConfigParamsImpl:FillHashTbl() Exception caught While Extracting Name/Value! Reason : "+ex.Message) ;
											continue ;
										}
									}
									if (flag == false)
									{
										streamWriterMod = new StreamWriter( m_xmlFilePath);
										XmlElement addNewAttriElement = xDoc.CreateElement("Attribute");
										addNewAttriElement.SetAttribute("name",name);
										addNewAttriElement.SetAttribute("value",paramVal.ToString());
										xNd.AppendChild(addNewAttriElement);
										xDoc.Save(streamWriterMod);
										streamWriterMod.Close();												
									}
									break;
								}
								
							}
							
						}
						catch(Exception ex)
						{
							m_log.Error("ConfigParamsImpl:FillHashTbl() Exception caught! Reason : "+ex.Message) ;
							continue ;
						}
						finally
						{
							if(streamWriterMod != null)
							{
								streamWriterMod.Close();
							}
						}
					}
				}

				m_log.Trace("[ConfigParamsImpl::FillHashTbl] OUT.");
				string sError;
				if ( FillHashTbl(out sError) == false) 
				{
					m_log.Trace("[ConfigParamsImpl::FillHashTbl] failed. " + sError);
					throw new ConfigParamsExceptionBase("[ConfigParamsImpl::FillHashTbl] Error: creating FillHashTbl failed." +  sError);
				}
				Console.WriteLine(sError);
			}
			catch(Exception ex)
			{
				m_log.Error("[ConfigParamsImpl::Save] Error: " + ex.Message);
			}
			#endregion Code
		}
		
		#endregion Save Methods

		/// <summary>
		/// This method will reset the attribute for given parameter.
		/// i.e. It will delete the newvalue attribute if it exist for the given parameter
		/// othewise it will not do anything. 
		/// </summary>
		/// <param name="name"></param>
		public override void SetDefault(string name)
		{
			m_log.Trace("[ConfigParamsImpl::SetDefault] IN.");

			try
			{
				if(this.xDoc == null)
					throw new ConfigParamsExceptionBase("[ConfigParamsImpl::FillHashTable] Error: Null xml object found.");

				//Scanning for the matching class name depending 
				//on the name of class provided through the constructor.
				
				XmlNodeList ndListClassName;
				ndListClassName = this.xDoc.GetElementsByTagName("Class");
				
				//The parameter name is made case INSENSITIVE. as well as checking 
				//against the class name which too the user can enter in the config file
				// in any way he wants...
				if((ndListClassName!=null) && (ndListClassName.Count > 0))
				{
					foreach(XmlNode xNd in ndListClassName)
					{
						try
						{
							StreamWriter streamWriterMod = null;
							string className = string.Empty ;
							className = xNd.Attributes.GetNamedItem("name").Value;
							className = className.ToUpper() ;
							string paramName = string.Empty ;
							if((m_className != null) &&(m_className.ToUpper().Equals(className)))
							{
								XmlNodeList attribNodesList =  xNd.ChildNodes ;
								if((attribNodesList != null)&&(attribNodesList.Count > 0))
								{
									bool flag = false;
									foreach(XmlNode attribNode in attribNodesList)
									{
										if(attribNode.NodeType != XmlNodeType.Element)
											continue;			
										try
										{
											
											paramName = attribNode.Attributes.GetNamedItem("name").Value ;
											if (paramName == name)
											{
												flag = true;
												XmlNode newValueNode = attribNode.Attributes.GetNamedItem("newvalue");
												if(newValueNode!= null)
												{
													XmlAttribute newValueAttribute = (XmlAttribute)newValueNode;
													streamWriterMod = new StreamWriter( m_xmlFilePath);
													attribNode.Attributes.Remove(newValueAttribute);
													xDoc.Save(streamWriterMod);
													streamWriterMod.Close();													
												}
												break;
												//sXmlAttributeCollection attrColl = attribNode.Attributes ;
												/*foreach(XmlAttribute  attribute in attrColl)
												{
													if(attribute.Name.ToString() == "newvalue")
													{
														streamWriterMod = new StreamWriter( m_xmlFilePath);
														attribNode.Attributes.Remove (attribute);
														xDoc.Save(streamWriterMod);
														streamWriterMod.Close();
														break;
													}
												}	
												
												break;
												*/
											}				

											
										}
										catch(Exception ex)
										{
											m_log.Error("ConfigParamsImpl:FillHashTbl() Exception caught While Extracting Name/Value! Reason : "+ex.Message) ;
											continue ;
										}
										finally
										{
											if(streamWriterMod != null)
											{
												streamWriterMod.Close();
											}
										}
									}
									if (flag == false)
									{
										string errMsg = "Given parameter does not exist: "+ name;
										throw new ApplicationException(errMsg );
									}
									break;
								}
								
							}
							
						}
						catch(Exception ex)
						{
							m_log.Error("ConfigParamsImpl:FillHashTbl() Exception caught! Reason : "+ex.Message) ;
							continue ;
						}
						
					}
				}

				m_log.Trace("[ConfigParamsImpl::SetDefault] OUT.");
				string sError;
				if ( FillHashTbl(out sError) == false) 
				{
					m_log.Trace("[ConfigParamsImpl::FillHashTbl] failed. " + sError);
					throw new ConfigParamsExceptionBase("[ConfigParamsImpl::FillHashTbl] Error: creating FillHashTbl failed." +  sError);
				}
				Console.WriteLine(sError);
				
			}
			catch(Exception ex)
			{
				m_log.Error("[ConfigParamsImpl::SetDefault] Error: " + ex.Message);				
			}
			

		}

		
		#endregion [IConfigParams Overridden Members] 

		#region [Private Methods]

		private bool IsHashTableEmpty()
		{
			bool isEmpty = false; 
			try
			{
				if((m_configParamsHash == null) || (m_configParamsHash.Count == 0))
				{
					m_log.Trace("[ConfigParamsImpl::IsHashTableEmpty] XML Values hashtable is empty") ;
					isEmpty = true ;
					throw new Exception("[ConfigParamsImpl::IsHashTableEmpty] XML Values hashtable is empty") ;
				}
				else
					isEmpty = false; 
			}
			catch(Exception e)
			{
				m_log.Trace("IsHashTableEmpty() : Exception caught ! "+e.Message) ;
				isEmpty = true ;
				if(m_IsConfigFilePresent)
				{
					throw new ConfigParamExceptionParamMissing("[ConfigParamsImpl::IsHashTableEmpty] Config File for class "+m_className+" does NOT contain any param/value pairs") ;
				}
				else
					throw new ConfigParamExceptionConfigFileMissing("[ConfigParamsImpl::IsHashTableEmpty] Config File for class : "+m_className+" missing. Details : "+e.Message) ;
			}
			return isEmpty ;
		}


		private bool IsValuePresent(string paramName,out object paramVal)
		{
			bool isPresent = false; 
			paramVal = null ;
			
			if(IsHashTableEmpty())
			{
				if(m_IsConfigFilePresent)
				{
					throw new ConfigParamExceptionParamMissing("[ConfigParamsImpl::IsValuePresent] Config File for class "+m_className+" does NOT contain any param/value pairs") ;
				}
				else
					throw new ConfigParamExceptionConfigFileMissing("IsValuePresent() :Class-specific Config File IS Missing !") ;
			}

			if(m_configParamsHash.ContainsKey(paramName))
			{
				paramVal = m_configParamsHash[paramName] ;
				isPresent = true ;
			}
			else
				throw new ConfigParamExceptionParamMissing("Parameter : "+paramName+" is missing from the XML file.");

			if(paramVal == null)
			{
				throw new ConfigParamExceptionValueMissing("Parameter Value is NULL for parameter : "+paramName) ;
			}
			return isPresent ;
		}

		/// <summary>
		/// This method is used only when default paramter is specified.All exception thrown are caught.
		/// </summary>
		/// <param name="paramName"></param>
		/// <param name="paramVal"></param>
		private void GetValFromHash_CatchEx(string paramName,out object paramVal)
		{
			paramVal = null ;
			try
			{
				FetchValFromHash(paramName,out paramVal) ;
			}
			catch(ConfigParamExceptionParamMissing ex)
			{
				m_log.Error("[ConfigParamsImpl::Lookup] ConfigParamExceptionParamMissing Caught! Reason : "+ex.Message) ;
			}
			catch(ConfigParamExceptionValueMissing ex)
			{
				m_log.Error("[ConfigParamsImpl::Lookup] ConfigParamExceptionValueMissing Caught! Reason : "+ex.Message) ;
			}
			catch(ConfigParamExceptionConfigFileMissing ex)
			{
				m_log.Error("[ConfigParamsImpl::Lookup] ConfigParamExceptionConfigFileMissing Caught! Reason : "+ex.Message) ;
			}
			catch(ConfigParamsExceptionBase ex)
			{
				m_log.Error("[ConfigParamsImpl::Lookup] ConfigParamsExceptionBase Caught! Reason : "+ex.Message) ;
			}
			catch(Exception e)
			{
				m_log.Error("[ConfigParamsImpl::Lookup] Exception Caught! Reason : "+e.Message) ;
			}
		}

		/// <summary>
		/// Below method is common to both the implementations
		/// </summary>
		private void FetchValFromHash(string paramName,out object paramVal)
		{
			paramVal = null ;

			if(paramName == null)
			{
				m_log.Trace("[ConfigParamsImpl::FetchValFromHash] ConfigParamExceptionParamMissing Caught! Reason : Parameter "+paramName+" is NULL");
				throw new ConfigParamExceptionParamMissing("[ConfigParamsImpl::FetchValFromHash] Parameter "+paramName+" is NULL") ;
			}
				
			//This conditon is checked for coz:
			//1.File NOT found error
			//2.Parsing XML data error
			//if any of the above results in hashtable NOT being populated
			//we want to assign the default value entered by the user and quit.
			//Make the parameter name all uppercase.
			paramName = paramName.ToUpper() ;
			if(!IsValuePresent(paramName,out paramVal))
			{
				throw new ConfigParamExceptionValueMissing("[ConfigParamsImpl::FetchValFromHash] Value requested NOT present in  Config File") ;
			}
		}



		private void GetValFromHash_ThrowEx(string paramName,out object paramVal)
		{
			paramVal = null ;
			try
			{
				FetchValFromHash(paramName,out paramVal) ;
			}
			catch(ConfigParamExceptionParamMissing ex)
			{
				m_log.Error("[ConfigParamsImpl::Lookup] ConfigParamExceptionParamMissing Caught! Reason : "+ex.Message) ;
				throw new ConfigParamExceptionParamMissing("[ConfigParamsImpl::Lookup] ConfigParamExceptionParamMissing Caught! Reason : "+ex.Message);
			}
			catch(ConfigParamExceptionValueMissing ex)
			{
				m_log.Error("[ConfigParamsImpl::Lookup] ConfigParamExceptionValueMissing Caught! Reason : "+ex.Message) ;
				throw new ConfigParamExceptionValueMissing("[ConfigParamsImpl::Lookup] ConfigParamExceptionValueMissing Caught! Reason : "+ex.Message);
			}
			catch(ConfigParamExceptionConfigFileMissing ex)
			{
				m_log.Error("[ConfigParamsImpl::Lookup] ConfigParamExceptionConfigFileMissing Caught! Reason : "+ex.Message) ;
				throw new ConfigParamExceptionConfigFileMissing("[ConfigParamsImpl::Lookup] ConfigParamExceptionConfigFileMissing Caught! Reason : "+ex.Message) ;
			}
			catch(ConfigParamsExceptionBase ex)
			{
				m_log.Error("[ConfigParamsImpl::Lookup] ConfigParamsExceptionBase Caught! Reason : "+ex.Message) ;
				throw new ConfigParamsExceptionBase("[ConfigParamsImpl::Lookup] ConfigParamsExceptionBase Caught! Reason : "+ex.Message) ;
			}
			catch(Exception e)
			{
				m_log.Error("[ConfigParamsImpl::Lookup] Exception Caught! Reason : "+e.Message) ;
				throw new ConfigParamsExceptionBase("[ConfigParamsImpl::Lookup] Exception Caught! Reason : "+e.Message);
			}
		}
		#endregion [Private Methods] ENDS

        #region Repeat Code 
        
        /// <summary>
        ///  Validate THE XML file against Schema 
        /// </summary>
        /// <param name="xsdFileName"></param>
        /// <param name="sr"></param>
        private  static void ValidateXMLLocal(string xsdFileName, StreamReader sr)
        {
            try
            {
                if (File.Exists(xsdFileName) != true)
                {
                    throw new FileNotFoundException("File NOT Found: " + xsdFileName);
                }


                XmlSchemaSet schemaSet = new XmlSchemaSet();
                XmlSchema x = new XmlSchema();

                schemaSet.ValidationEventHandler +=
                    new ValidationEventHandler(ValidationCallBack);

                schemaSet.Add(null, xsdFileName);


                if (schemaSet.Count > 0)
                {
                    if (sr.BaseStream.CanSeek)
                        sr.BaseStream.Seek(0, SeekOrigin.Begin);

                    XmlTextReader reader = new XmlTextReader(sr);
                    
                    XmlReaderSettings settings = new XmlReaderSettings();
                    settings.ValidationType = ValidationType.Schema;
                    settings.Schemas = schemaSet;
                    settings.ValidationEventHandler +=
                        new ValidationEventHandler(ValidationCallBack);

                    XmlReader validReader = XmlReader.Create(reader, settings);
                    // read	all	nodes and print	out

                    while (validReader.Read())
                    {
                        // Do nothing 
                    } // end of while 

                }
                else
                    throw new ApplicationException("MalFormed XSDfile:" + xsdFileName);
            }
            catch (ApplicationException ex)
            {
                throw (ex);
            }
            catch (FileNotFoundException fileEx)
            {
                throw (fileEx);
            }
            catch (Exception e)
            {
                throw new ApplicationException("MalFormed XSDfile:" + xsdFileName, e);
            }
        }

        private static void ValidationCallBack(object sender, System.Xml.Schema.ValidationEventArgs e)
        {
            throw new ApplicationException("Validation Failed:" + e.Message);

        }
        #endregion
    }
}
