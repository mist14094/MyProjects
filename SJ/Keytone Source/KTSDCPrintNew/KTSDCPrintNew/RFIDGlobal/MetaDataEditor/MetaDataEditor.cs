using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.IO;
using System.Text;
using NLog;
using KTone.RFIDGlobal.KTLogger;

namespace KTone.RFIDGlobal.MetaDataEditor
{
    public enum ByteEndian
    {
        LittleEndian,
        BigEndian
    }

    public class MetaData
    {
        #region [ MemberVars ]
        private XmlDocument _xDoc = null;
        private XmlNode _CurrentTAG = null;
        private XmlNode _CurrentElement = null;
        private XmlDocument _newTemplate = null;
        private string _SaveTemplateID = string.Empty;
        private Dictionary<string, TemplateInfo> _templates = new Dictionary<string, TemplateInfo>();

        //statics
        private static Logger m_Log = KTone.RFIDGlobal.KTLogger.KTLogManager.GetLogger();
        private static bool isInstaciated = false;
        private static MetaData objSingle = null; //Singleton Object
        private static string _xsdFileLoc = string.Empty;
        private static System.Collections.Hashtable _htTemplateIDLoc = null; //Key = TemplateID, Val = xDoc of template

        #endregion MemberVars

        #region [ Constructors ]

        /// <summary>
        /// All constructor aare private. GetInstance method is public for getting instance.
        /// It is singelton object.
        /// </summary>
        /// <param name="templateDir"></param>
        /// <param name="xsdFile"></param>
        private MetaData(Dictionary<string, TemplateInfo> templates, string xsdFile)
        {
            try
            {
                m_Log.Trace("Entering");
                InitAll(templates, xsdFile);
                this._templates = templates;
            }
            catch (MetaDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MetaDataException("[MetaDataEditor::MetaDataEditor] failed : Exception ={0}", ex.Message);
            }
            finally
            {
                m_Log.Trace("Finally Leaving ");
            }

        }//MetaDataEditor

        /// <summary>
        /// Get instance of MetaDataEditor.
        /// </summary>
        /// <param name="templates">Templates dictionary where key is teplate name and template as value</param>
        /// <param name="xsdFile">Name of xsd file for template with path</param>
        /// <returns>returns MetaDataEditor's SINGLETON OBJECT</returns>
        public static MetaData GetInstance(Dictionary<string, TemplateInfo> templates, string xsdFile)
        {
            try
            {
                m_Log.Trace("[MetaDataEditor::GetInstance]");
                //Parameter chk 
                if (templates == null)
                    throw new MetaDataException("NULL parameter found : Templates can't be null.");

                if (xsdFile == null || xsdFile.Equals(String.Empty) == true)
                    throw new MetaDataException("NULL parameter found : xsd file path can't be null/empty. xsdFile = '{0}'", xsdFile);

                if (isInstaciated == true)
                {
                    InitAll(templates, xsdFile);
                    objSingle._templates = templates;
                    return objSingle;
                }
                else
                {
                    objSingle = new MetaData(templates, xsdFile);
                    isInstaciated = true;
                    return objSingle;
                }
            }
            catch (MetaDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MetaDataException("Failed : Exception ={0}", ex.Message);
            }

        }//GetInstance

        #endregion [ Constructors ]

        #region [ Util fxns ]

        /// <summary>
        /// Initialized (or ReInitialised when refresh) metaData object with templateDir and xsd file.        
        /// </summary>
        /// <param name="templateDir"></param>
        /// <param name="xsdFile"></param>
        private static void InitAll(Dictionary<string, TemplateInfo> templates, string xsdFile)
        {
            try
            {
                m_Log.Trace("[MetaDataEditor::InitAll]");

                if (templates == null)
                    throw new MetaDataException("XSD File not found for Tag Data Template. Please verify parameter- xsdFile = '{0}", xsdFile);

                _xsdFileLoc = xsdFile;
                //---------------------------------------------

                _htTemplateIDLoc = new System.Collections.Hashtable();

                XmlDocument xDocTmpl;

                foreach (KeyValuePair<string, TemplateInfo> pair in templates)
                {
                    xDocTmpl = new XmlDocument();
                    xDocTmpl.Load(new MemoryStream(UTF8Encoding.UTF8.GetBytes(pair.Value.Template)));

                    _htTemplateIDLoc.Add(pair.Key, xDocTmpl);
                    SortedDictionary<int, int> dataBreaks = new SortedDictionary<int, int>();
                    CheckBlockSequence(xDocTmpl, out dataBreaks);
                    templates[pair.Key].DataBlockInfo = dataBreaks;
                }
            }
            catch (MetaDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MetaDataException("[MetaDataEditor::InitAll] failed : Exception = '{0}'", ex.Message);
            }
        }//InitAll(string templateDir, string xsdFile)


        /// <summary>
        /// set the template's 1st tag to CurrentTag as default tag.
        /// </summary>
        private void SetDefaultCurrTag()
        {
            try
            {
                m_Log.Trace("[MetaDataEditor::SetDefaultCurrTag]");
                if (this._xDoc != null)
                {
                    this._CurrentTAG = this._xDoc.GetElementsByTagName("TemplateElements").Item(0);

                    //Reset Curr Element to the 1stElement of newly assigned CurrentTag.
                    this.ResetCurrElementAfterSetngCurrTAG();
                }
                else
                    throw new MetaDataException("[MetaDataEditor::SetDefaultCurrTag] failed : Null xDoc object found.Template is not open.");
            }
            catch (MetaDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MetaDataException("[MetaDataEditor::SetDefaultCurrTag] failed : Exception = '{0}'", ex.Message);
            }
        }//SetDefaultCurrTag


        /// <summary>
        /// Read a xml info of TemplateElements and convert it to the Element strcuture object.
        /// </summary>
        /// <param name="xmlElem"></param>
        /// <returns>TemplateElement's struct object equivalent to xml TemplateElement</returns>
        private TemplateElement ReadTemplateElement(XmlElement xmlElem)
        {
            m_Log.Trace("[MetaDataEditor::ReadTemplateElement]");
            TemplateElement tmpltElement = new TemplateElement();

            try
            {
                if (xmlElem != null && xmlElem.HasAttribute("Name") == true)
                {
                    tmpltElement.Name = xmlElem.Attributes.GetNamedItem("Name").InnerText;
                    tmpltElement.DataType = xmlElem.Attributes.GetNamedItem("DataType").InnerText;
                    tmpltElement.StartByteIndex = System.Convert.ToInt32(xmlElem.Attributes.GetNamedItem("StartByteIndex").InnerText);
                    tmpltElement.Length = System.Convert.ToInt32(xmlElem.Attributes.GetNamedItem("Length").InnerText);
                    tmpltElement.StartBitIndex = System.Convert.ToInt32(xmlElem.Attributes.GetNamedItem("StartBitIndex").InnerText);
                }
            }
            catch (MetaDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MetaDataException("[MetaDataEditor::ReadTemplateElement] failed : Exception = {0}", ex);
            }

            return tmpltElement;
        }//ReadTemplateElement(XmlElement xmlElem)


        /// <summary>
        /// Read multiple xml TemplateElement and return arrya of struct TemplateElement object.
        /// </summary>
        /// <param name="ndElements"></param>
        /// <returns>TemplateElement[] equivqlent to the childNodes of xml's TemplateElements node.</returns>
        private TemplateElement[] ReadTemplateElements(XmlNode ndElements)
        {
            m_Log.Trace("[MetaDataEditor::ReadTemplateElements]");
            int idx = 0;
            TemplateElement[] TmpltElements = null;

            try
            {
                if (ndElements != null && ndElements.HasChildNodes == true)
                {
                    TmpltElements = new TemplateElement[ndElements.ChildNodes.Count];
                    foreach (XmlNode inLoopNd in ndElements.ChildNodes)
                    {
                        TmpltElements[idx].Name = inLoopNd.Attributes.GetNamedItem("Name").Value;
                        TmpltElements[idx].DataType = inLoopNd.Attributes.GetNamedItem("DataType").Value;
                        TmpltElements[idx].StartByteIndex = System.Convert.ToInt32(inLoopNd.Attributes.GetNamedItem("StartByteIndex").Value);
                        TmpltElements[idx].Length = System.Convert.ToInt32(inLoopNd.Attributes.GetNamedItem("Length").Value);
                        TmpltElements[idx].StartBitIndex = System.Convert.ToInt32(inLoopNd.Attributes.GetNamedItem("StartBitIndex").InnerText);
                        idx++;
                    }
                }
            }
            catch (MetaDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MetaDataException("[MetaDataEditor::ReadTemplateElements] failed : Exception = {0}", ex);
            }

            return TmpltElements;

        }


        /// <summary>
        /// Maintain Current Element pointer to first element.
        /// whenever the CurrentTag pointer is changed it will set 1st element as a default CurrentElement.
        /// </summary>
        private void ResetCurrElementAfterSetngCurrTAG()
        {
            try
            {
                m_Log.Trace("[MetaDataEditor::ResetCurrElementAfterSetngCurrTAG]");
                if (this._CurrentTAG != null && this._CurrentTAG.HasChildNodes == true)
                    if (this._CurrentTAG.FirstChild.HasChildNodes == true)
                    {
                        this._CurrentElement = this._CurrentTAG.FirstChild.FirstChild;
                        return;
                    }

                //If there is no Element attached with the Tag.
                this._CurrentElement = null;
            }
            catch (MetaDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MetaDataException("[MetaDataEditor::ResetCurrElementAfterSetngCurrTAG] failed : Exception = '{0}'", ex.Message);
            }
        }//ResetCurrElementAfterSetngCurrTAG()


        /// <summary>
        /// Refresh Template will verify the given TemplateID and take appropriate action: - 
        /// TEMPLATE_ADDED , TEMPLATE_MODIFIED, TEMPLATE_DELETED
        /// According to action taken it will refresh memory objects and set out param.		
        /// </summary>
        /// <param name="templateID"></param>
        /// <param name="operationToPerform"></param>
        private void RefreshTemplate(string templateID, TemplateOperation operationToPerform)
        {
            try
            {
                m_Log.Trace("[MetaDataEditor::RefreshTemplate]");
                if (templateID == null || templateID == string.Empty)
                    return;

                templateID = templateID.Trim();
                switch (operationToPerform)
                {
                    case TemplateOperation.TEMPLATE_ADDED:
                        {
                            //Create new ref of xDoc
                            XmlDocument xDocTmpl = new XmlDocument();
                            //xDocTmpl.Load(this.TemplateDir + "\\" + templateID + ".config");

                            //Add new one 
                            _htTemplateIDLoc.Add(templateID, xDocTmpl);
                            break;
                        }

                    case TemplateOperation.TEMPLATE_MODIFIED:
                        {
                            //remove old refrence
                            _htTemplateIDLoc.Remove(templateID);

                            //Create new ref of xDoc
                            XmlDocument xDocTmpl = new XmlDocument();
                            //xDocTmpl.Load(this.TemplateDir + "\\" + templateID + ".config");

                            //Add new one 
                            _htTemplateIDLoc.Add(templateID, xDocTmpl);
                            break;
                        }

                    case TemplateOperation.TEMPLATE_DELETED:
                        {
                            _htTemplateIDLoc.Remove(templateID);
                            break;
                        }
                }//switch(operationToPerform)
            }
            catch (MetaDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MetaDataException("[MetaDataEditor::RefreshTemplate] Exception = {0}", ex.Message);
            }

        }//RefreshTemplate(string templateID, TemplateOperation operationToPerform)


        #endregion [ Util fxns ]

        #region [ properties ]

        /// <summary>
        /// Check whether the CurrentTag is first Tag of Opned Template.
        /// </summary>
        public bool IsFirstTag
        {
            get
            {
                try
                {
                    m_Log.Trace("[MetaDataEditor::IsFirstTag]");
                    if (this._CurrentTAG == null)
                        throw new MetaDataException("[MetaDataEditor::IsFirstTag] failed : Pointer to CurrentTag found null.");

                    if (this._CurrentTAG.Attributes.GetNamedItem("TagType").InnerText ==
                        this._xDoc.GetElementsByTagName("TagDataTemplate").Item(0).Attributes.GetNamedItem("TagType").Value)
                    {
                        // [ comparing current node with the 1st node ]
                        // If current nd is the first node then dont move up.
                        return true;
                    }
                }
                catch (MetaDataException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw new MetaDataException("[MetaDataEditor::IsFirstTag] failed :", ex);
                }
                return false;
            }
        }//IsFirstTag


        /// <summary>
        /// Check whether the CurrentTag is last Tag of Opned Template.
        /// </summary>
        public bool IsLastTag
        {
            get
            {
                try
                {
                    m_Log.Trace("[MetaDataEditor::IsLastTag]");
                    if (this._CurrentTAG == null)
                        throw new MetaDataException("[MetaDataEditor::IsLastTag] failed : Pointer to CurrentTag found null.");

                    if (this._CurrentTAG.Attributes.GetNamedItem("TagType").InnerText ==
                        this._xDoc.GetElementsByTagName("TagDataTemplate").Item(this._xDoc.GetElementsByTagName("TagDataTemplate").Count - 1)
                        .Attributes.GetNamedItem("TagType").Value
                        )
                    {
                        // [comparing current nd with last node]
                        // if current node is the last node then dont move next.
                        return true;
                    }
                }
                catch (MetaDataException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw new MetaDataException("[MetaDataEditor::IsLastTag] failed : Exception = ", ex.Message);
                }
                return false;
            }
        }//IsLastTag


        /// <summary>
        /// returns XSD file name with location for TagDataTemplate(s).
        /// </summary>
        public string XSDFilePath
        {
            get { m_Log.Trace("[MetaDataEditor::XSDFilePath]"); return _xsdFileLoc; }
        }

        public Dictionary<string, TemplateInfo> Templates
        {
            get
            {
                return _templates;
            }
        }
        #endregion [ properties ]

        #region [ basic tmplt fxns ] : Opn/creat/sv/validate/UpdateTemplateWith/GetTemplateXml

        /// <summary>
        /// Open a Template and set CurrentTag and CurrentElement for further use.
        /// </summary>
        /// <param name="TemplateID">Unique name of template to open</param>
        public void Open(string templateID)
        {
            try
            {
                m_Log.Trace("[MetaDataEditor::Open]");
                templateID = templateID.Trim();
                if (_htTemplateIDLoc.ContainsKey(templateID))
                    this._xDoc = (XmlDocument)_htTemplateIDLoc[templateID];
                else
                {
                    m_Log.Error("MetaDataEditor::Open:Error [Open failed] : Template Not found to Open.");
                    m_Log.Trace("MetaDataEditor::Open:xsdFile; : " + _xsdFileLoc);
                    m_Log.Trace("MetaDataEditor::Open templates");
                    foreach (DictionaryEntry entry in _htTemplateIDLoc)
                        m_Log.Trace(entry.Key);
                    throw new MetaDataException("[MetaDataEditor::Open] failed : Template Not found to Open. Please verify parameter templateID = '{0}'", templateID);
                }

                this.SetDefaultCurrTag();
                this._SaveTemplateID = templateID;
            }
            catch (MetaDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MetaDataException("[MetaDataEditor::Open] failed : Exception = '{0}", ex.Message);
            }
        }//Open(string templateID)


        /// <summary>
        /// Create a new MeataData file containing elements from TagType in existing TemplateID
        /// </summary>
        /// <param name="existingTemplateID">Already exist template</param>
        /// <param name="newTemplateID">Unique name of newly created template</param>
        /// <param name="tagTypes">Array of tags for new template</param>
        /// <param name="Name">Template name to store inside template</param>
        /// <param name="Description">Description of template</param>
        public void Create(string existingTemplateID, string newTemplateID, TagType[] tagTypes, string name, string description, ByteEndian endianNess)
        {
            m_Log.Trace("[MetaDataEditor::Create]");
            existingTemplateID = existingTemplateID.Trim();
            newTemplateID = newTemplateID.Trim();
            if (_htTemplateIDLoc.ContainsKey(newTemplateID))
                throw new MetaDataException("[MetaDataEditor::Create] failed : Given 'newTemplateID={0}' already exists in the system.", newTemplateID);

            try
            {
                _SaveTemplateID = newTemplateID; //To Operate save fxn with this given TemplateID

                if (_htTemplateIDLoc.ContainsKey(existingTemplateID) == false)
                {
                    m_Log.Trace("[MetaDataEditor::Create] failed : xsdFile; : " + _xsdFileLoc);
                    m_Log.Trace("[MetaDataEditor::Create] failed : templates in memory: -");
                    foreach (DictionaryEntry entry in _htTemplateIDLoc)
                        m_Log.Trace(entry.Key);

                    throw new MetaDataException("[MetaDataEditor::Create] failed : Given 'existingTemplateID={0}' not found in system.", existingTemplateID);
                }

                XmlDocument xDocExist = (XmlDocument)_htTemplateIDLoc[existingTemplateID];

                //Creating a _newTemplate
                _newTemplate = new XmlDocument();
                _newTemplate.AppendChild(_newTemplate.CreateXmlDeclaration("1.0", "utf-8", "no"));
                XmlElement RootElement = _newTemplate.CreateElement("RFIDTagDataConfig");
                _newTemplate.AppendChild(RootElement);

                //Adding Header Element
                XmlElement elmTmpltHeader = _newTemplate.CreateElement("TemplateHeader");
                XmlElement elmName = _newTemplate.CreateElement("Name");
                elmName.InnerText = name;
                elmTmpltHeader.AppendChild(elmName);

                XmlElement elmDescription = _newTemplate.CreateElement("Description");
                elmDescription.InnerText = description;
                elmTmpltHeader.AppendChild(elmDescription);

                XmlElement elmTmpltID = _newTemplate.CreateElement("TemplateID");
                elmTmpltID.InnerText = newTemplateID;
                elmTmpltHeader.AppendChild(elmTmpltID);

                //Define data entry in which Endianness
                XmlElement elmEndian = _newTemplate.CreateElement("ByteEndian");
                elmEndian.InnerText = endianNess.ToString();
                elmTmpltHeader.AppendChild(elmEndian);

                RootElement.AppendChild(elmTmpltHeader);


                //Creating TemplateBody Node
                XmlElement elmTmpltBody = _newTemplate.CreateElement("TemplateBody");

                foreach (TagType tag in tagTypes)
                {
                    XmlElement elemTagDataTmplt = _newTemplate.CreateElement("TagDataTemplate");//, "TagType", tag.ToString());
                    elemTagDataTmplt.SetAttribute("TagType", tag.ToString());

                    if (xDocExist.OuterXml.IndexOf(tag.ToString()) >= 0)
                    {//If Tag present in the existing template then copy.								


                        string xPath = "/RFIDTagDataConfig/TemplateBody/TagDataTemplate[@TagType='" + tag.ToString() + "']";
                        XmlNode existTagNode = xDocExist.SelectSingleNode(xPath);

                        if (tag.ToString().IndexOf("EPC") >= 0)
                        {
                            string sUCCNoStd = existTagNode.Attributes.GetNamedItem("UCCNumberingStandard").Value;
                            elemTagDataTmplt.SetAttribute("UCCNumberingStandard", sUCCNoStd);
                        }
                        else//if(tag.ToString().IndexOf("EPCClass1") >= 0)
                        {
                            if (existTagNode.FirstChild != null)
                            {
                                TemplateElement[] tmpltElem = ReadTemplateElements(existTagNode.FirstChild); //FirstChild is '<TemplateElements>'

                                XmlElement templateElemS = _newTemplate.CreateElement("TemplateElements");
                                for (int idx = 0; idx < tmpltElem.Length; idx++)
                                {
                                    XmlElement templateElem = _newTemplate.CreateElement("TemplateElement");
                                    templateElem.SetAttribute("Name", tmpltElem[idx].Name);
                                    templateElem.SetAttribute("DataType", tmpltElem[idx].DataType);
                                    templateElem.SetAttribute("StartByteIndex", tmpltElem[idx].StartByteIndex.ToString());
                                    templateElem.SetAttribute("Length", tmpltElem[idx].Length.ToString());
                                    templateElem.SetAttribute("StartBitIndex", tmpltElem[idx].StartBitIndex.ToString());

                                    templateElemS.AppendChild(templateElem);
                                }

                                elemTagDataTmplt.AppendChild(templateElemS);
                            }
                        }//if(tag.ToString().IndexOf("EPCClass1") >= 0)

                    }//if(xDocExist.OuterXml.IndexOf(tag.ToString()) >= 0 )

                    elmTmpltBody.AppendChild(elemTagDataTmplt);

                }//foreach(TagType tag in tagTypes)					

                //Adding TemplateBody to Root
                RootElement.AppendChild(elmTmpltBody);

                //Opening a newly created TemplateID.
                this._xDoc = this._newTemplate;

                this.SetDefaultCurrTag();
                this._SaveTemplateID = newTemplateID; //To Operate save fxn with this given TemplateID
            }
            catch (MetaDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MetaDataException("[MetaDataEditor::Create] failed : Exception = {0}", ex.Message);
            }

        }//Create(string existingTemplateID, string newTemplateID, TagType[] tagTypes)


        /// <summary>
        /// Create a new MeataData file containing elements from TagType enumarated array. 
        /// </summary>
        /// <param name="tagTypes">Array of tags for new template</param>
        /// <param name="newTemplateID">Unique name of newly created template</param>
        /// <param name="Name">Template name to store inside template</param>
        /// <param name="Description">Description of template</param>
        public void Create(TagType[] tagTypes, string newTemplateID, string name, string description, ByteEndian endianNess)
        {
            m_Log.Trace("[MetaDataEditor::Create]");
            newTemplateID = newTemplateID.Trim();
            if (_htTemplateIDLoc.ContainsKey(newTemplateID))
                throw new MetaDataException("[MetaDataEditor::Create] failed : Given 'newTemplateID={0}' already exists in the system.", newTemplateID);

            try
            {
                //Creating a _newTemplate
                _newTemplate = new XmlDocument();
                _newTemplate.AppendChild(_newTemplate.CreateXmlDeclaration("1.0", "utf-8", "no"));
                XmlElement RootElement = _newTemplate.CreateElement("RFIDTagDataConfig");
                _newTemplate.AppendChild(RootElement);

                //Adding Header Element
                XmlElement elmTmpltHeader = _newTemplate.CreateElement("TemplateHeader");
                XmlElement elmName = _newTemplate.CreateElement("Name");
                elmName.InnerText = name;
                elmTmpltHeader.AppendChild(elmName);

                XmlElement elmDescription = _newTemplate.CreateElement("Description");
                elmDescription.InnerText = description;
                elmTmpltHeader.AppendChild(elmDescription);

                XmlElement elmTmpltID = _newTemplate.CreateElement("TemplateID");
                elmTmpltID.InnerText = newTemplateID;
                elmTmpltHeader.AppendChild(elmTmpltID);

                //Define data entry in which Endianness
                XmlElement elmEndian = _newTemplate.CreateElement("ByteEndian");
                elmEndian.InnerText = endianNess.ToString();
                elmTmpltHeader.AppendChild(elmEndian);

                RootElement.AppendChild(elmTmpltHeader);

                //Creating TemplateBody Node
                XmlElement elmTmpltBody = _newTemplate.CreateElement("TemplateBody");

                //Adding TagDataTemplate
                foreach (TagType tag in tagTypes)
                {
                    XmlElement elemTagDataTmplt = _newTemplate.CreateElement("TagDataTemplate");
                    elemTagDataTmplt.SetAttribute("TagType", tag.ToString());
                    elmTmpltBody.AppendChild(elemTagDataTmplt);

                }//foreach(TagType tag in tagTypes)	

                //Adding TmpltBody
                RootElement.AppendChild(elmTmpltBody);

                //Opening a newly created TemplateID.
                this._xDoc = this._newTemplate;

                this.SetDefaultCurrTag();
                this._SaveTemplateID = newTemplateID; //To Operate save fxn with this given TemplateID
            }
            catch (MetaDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MetaDataException("[MetaDataEditor::Create] failed : Exception =", ex.Message);
            }
        }//Create(TagType[] enumTags, string newTemplateID)


        /// <summary>
        /// Save newly created MetaData file
        /// </summary>
        /// <returns></returns>
        public void Save()
        {
            m_Log.Trace("[MetaDataEditor::Save]");
            try
            {
                if (this._xDoc == null)
                    throw new MetaDataException("[MetaDataEditor::Save] failed : Null doc object found while saving.");

                if (_SaveTemplateID.Length > 0)
                {
                    string logStr;
                    if (Validate(out logStr) == true)
                        this._xDoc.Save("");
                    else
                        throw new MetaDataException("[MetaDataEditor::Save] failed: logstr={0}", logStr);
                }
                else
                    throw new MetaDataException("[MetaDataEditor::Save] failed: Empty TemplateID Name found while saving. Saving TemplateName='{0}'", this._SaveTemplateID);
            }
            catch (MetaDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MetaDataException("[MetaDataEditor::Save] failed : Exception = '{0}'", ex.Message);
            }
        }//Save


        /// <summary>
        /// Close currently opened template.
        /// </summary>
        public void Close()
        {
            m_Log.Trace("[MetaDataEditor::Close]");
            this._xDoc = null;
            this._CurrentElement = null;
            this._CurrentTAG = null;
            this._SaveTemplateID = null;
            this._newTemplate = null;
        }//Close

        /// <summary>
        /// 
        /// </summary>
        /// <param name="templateInfo"></param>
        public void AddTemplate(TemplateInfo templateInfo)
        {
            XmlDocument xDocTmpl = new XmlDocument();
            xDocTmpl.Load(new MemoryStream(UTF8Encoding.UTF8.GetBytes(templateInfo.Template)));

            _htTemplateIDLoc.Add(templateInfo.TemplateName, xDocTmpl);
            SortedDictionary<int, int> dataBreaks = new SortedDictionary<int, int>();
            CheckBlockSequence(xDocTmpl, out dataBreaks);
            this._templates[templateInfo.TemplateName] = templateInfo;
            this._templates[templateInfo.TemplateName].DataBlockInfo = dataBreaks;
        }

        /// <summary>
        /// Delete given template from the system.
        /// </summary>
        /// <param name="templateID">Unique templateID to delete from system.</param>
        public void DeleteTemplate(string templateID)
        {
            try
            {
                m_Log.Trace("[MetaDataEditor::DeleteTemplate]");
                //Param chk
                if (templateID.Equals(string.Empty) == true)
                    throw new MetaDataException("[MetaDataEditor::DeleteTemplate] failed : Null parameter found for delete.");

                templateID = templateID.Trim();
                if (templateID.Equals(this._SaveTemplateID))
                    throw new MetaDataException("[MetaDataEditor::DeleteTemplate] failed : Given Template='{0}' is in use", templateID);

                if (_htTemplateIDLoc.ContainsKey(templateID) == true)
                    _htTemplateIDLoc.Remove(templateID);

                if (this._templates.ContainsKey(templateID))
                {
                    this._templates.Remove(templateID);
                }

              
                //System.IO.File.Delete(_tmpltDir + templateID + ".config");
            }
            catch (MetaDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MetaDataException("[MetaDataEditor::DeleteTemplate] failed: Exception ={0}", ex.Message);
            }
        }//DeleteTemplate


        /// <summary>
        /// UpdateTemplateWith :
        /// It will changed the Opened Template with only having the tags which is passed 
        /// as a parameter. It Means :-
        /// 
        /// 1> If 'tags' contain new Tag(s) then it is added.
        /// 2> If already exist TagType(s) is not present in the parameter 'tags' then 
        ///		-> it will be deleted from Template.
        ///	
        ///	CAUTION :- 
        ///	You always need to call save after UpdateTemplateWith method call otherwise 
        /// Updated Template is not get saved and you have older version of Template in sys dir.
        /// </summary>
        /// <param name="tags"></param>
        public void UpdateTemplateWith(TagType[] tags)
        {
            try
            {

                m_Log.Trace("[MetaDataEditor::UpdateTemplateWith]");

                if (this._xDoc == null)
                    throw new MetaDataException("[MetaDataEditor::UpdateTemplateWith] failed : Null xDoc object found.Template is not open.");

                //Deleting Tag(s)
                int count = this._xDoc.GetElementsByTagName("TagDataTemplate").Count;
                string[] tagsMarkForDeletion = new string[count];//Count is the max Tag Count which can be deleted from the Template		

                //Mark nodes which are going to delete as per the 'tags' array.
                string sTag;
                int idx_tagsMarkForDeletion = 0;

                XmlNodeList lstTagDataTmplt = this._xDoc.GetElementsByTagName("TagDataTemplate");

                foreach (XmlNode NdTagData in lstTagDataTmplt)
                {
                    sTag = NdTagData.Attributes.GetNamedItem("TagType").Value;

                    bool GotIt = false;
                    foreach (TagType tag in tags)
                    {
                        if (tag.ToString() == sTag)
                        {
                            GotIt = true;
                            break;
                        }
                    }

                    if (GotIt == false)
                    {
                        tagsMarkForDeletion[idx_tagsMarkForDeletion] = sTag;
                        idx_tagsMarkForDeletion++;
                    }

                }//foreach(XmlNode NdTagData in NdTagDataConfig.ChildNodes)

                //After marking delete it from the Template
                XmlNodeList ndTemplateBody = this._xDoc.GetElementsByTagName("TemplateBody");

                XmlNode Nd4DelTagData = null;
                foreach (string TagForDel in tagsMarkForDeletion)
                {
                    if (TagForDel != null)
                    {
                        XmlNodeList ndList = this._xDoc.GetElementsByTagName("TagDataTemplate");


                        foreach (XmlNode nd4Curr in ndList)
                        {
                            if (TagForDel == nd4Curr.Attributes.GetNamedItem("TagType").Value)
                            {
                                Nd4DelTagData = nd4Curr;
                            }
                        }

                        ndTemplateBody[0].RemoveChild(Nd4DelTagData);
                    }
                }

                //Deleting Tag(s) *COMPLETE

                //Adding new tags
                foreach (TagType tag in tags)
                {
                    if (ndTemplateBody[0].OuterXml.IndexOf(tag.ToString()) < 0)
                    {
                        XmlElement newElem = this._xDoc.CreateElement("TagDataTemplate");
                        newElem.SetAttribute("TagType", tag.ToString());
                        ndTemplateBody[0].AppendChild(newElem);
                    }
                }

                //This call will set the CurrentTag and CurrentElem refrences to default position.
                //This is essential after Updating Tags otherwise it will have old refrence
                this.GotoStartTagType();
            }
            catch (MetaDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MetaDataException("[MetaDataEditor::UpdateTemplateWith] failed : Exception = {0}", ex.Message);
            }


        }//public void UpdateTemplateWith(TagType[] tags)


        /// <summary>
        /// Validate current opened Template with Xml Schema Definition.
        /// </summary>
        /// <param name="logStr">error string if any invalid structure found otherwise return empty.</param>
        /// <returns>return false if any invalid structure found otherwise return true.</returns>
        public bool Validate(out string logStr)
        {
            try
            {
                m_Log.Trace("[MetaDataEditor::Validate]");
                if (this._xDoc != null)
                {
                    string xmlTmplt = this._xDoc.OuterXml;
                    MemoryStream configStream = new MemoryStream(System.Text.Encoding.ASCII.GetBytes(xmlTmplt));
                    KTone.RFIDGlobal.RFUtils.ValidateXML(_xsdFileLoc, new StreamReader(configStream));
                    logStr = string.Empty;
                    return true;
                }
                else
                    throw new MetaDataException("[MetaDataEditor::Validate] failed : Null xDoc object found.Template is not open.");
            }
            catch (MetaDataException eMetaData)
            {
                logStr = eMetaData.Message;
                return false;
            }
            catch (Exception ex)
            {
                logStr = ex.Message;
                return false;
            }
        }//Validate


        /// <summary>
        /// Get Xml of opened TagDataTemplate.
        /// </summary>
        /// <returns>returns Xml string representaion of current opened Template.</returns>
        public string GetTemplateXml()
        {
            m_Log.Trace("[MetaDataEditor::GetTemplateXml]");
            try
            {
                if (this._xDoc != null)
                    return this._xDoc.OuterXml;
                else
                    throw new MetaDataException("[MetaDataEditor::GetTemplateXml] failed : Null xDoc object found.Template is not open.");
            }
            catch (MetaDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MetaDataException("[MetaDataEditor::GetTemplateXml] failed : Exception = {0} ", ex.Message);
            }
        }//GetTemplateXml


        #endregion [ basic tmplt fxns ] : Opn/creat/sv/validate/UpdateTemplateWith/GetTemplateXml

        #region [ fxns TagType ] : nxt/prev/start/end/GotoTagType/GetCurrTagType

        /// <summary>
        /// Move CurrentTag pointer to next Tag
        /// </summary>
        public void GotoNextTagType()
        {
            try
            {
                m_Log.Trace("[MetaDataEditor::GotoNextTagType]");
                if (this.IsLastTag)// if current node is the last node then dont move next.
                    return;

                if (this._CurrentTAG == null)
                    throw new MetaDataException("[MetaDataEditor::GotoNextTagType] failed : Null CurrentTag found.Template is not open.");

                if (this._CurrentTAG.NextSibling == null)
                    return;

                this._CurrentTAG = this._CurrentTAG.NextSibling;

                //Reset Curr Element to the 1stElement of newly assigned CurrentTag.
                this.ResetCurrElementAfterSetngCurrTAG();
            }
            catch (Exception ex)
            {
                throw new MetaDataException("[MetaDataEditor::GotoNextTagType] failed : Exception = {0}", ex.Message);
            }
        }//GotoNextTagType


        /// <summary>
        /// Move CurrentTag pointer to previous Tag
        /// </summary>
        public void GotoPrevTagType()
        {
            try
            {
                m_Log.Trace("[MetaDataEditor::GotoPrevTagType]");
                if (this.IsFirstTag)
                {
                    // [ comparing current node with the 1st node ]
                    // If current nd is the first node then dont move up.
                    return;
                }

                if (this._CurrentTAG == null)
                    throw new MetaDataException("[MetaDataEditor::GotoNextTagType] failed : Null CurrentTag found.Template is not open.");

                if (this._CurrentTAG.PreviousSibling == null)
                    return;

                this._CurrentTAG = this._CurrentTAG.PreviousSibling;

                //Reset Curr Element to the 1stElement of newly assigned CurrentTag.
                this.ResetCurrElementAfterSetngCurrTAG();
            }
            catch (MetaDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MetaDataException("[MetaDataEditor::GotoPrevTagType] failed : Exception = '{0}'", ex.Message);
            }
        }//GotoPrevTagType


        /// <summary>
        /// Move CurrentTag pointer to last TAG
        /// </summary>
        public void GotoEndTagType()
        {
            try
            {
                m_Log.Trace("[MetaDataEditor::GotoEndTagType]");

                if (this._xDoc != null)
                    this._CurrentTAG = this._xDoc.GetElementsByTagName("TagDataTemplate").Item(this._xDoc.GetElementsByTagName("TagDataTemplate").Count - 1);
                else
                    throw new MetaDataException("[MetaDataEditor::GotoEndTagType] failed : Null xDoc object found.Template is not open.");

                //Reset Curr Element to the 1stElement of newly assigned CurrentTag.
                this.ResetCurrElementAfterSetngCurrTAG();
            }
            catch (MetaDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MetaDataException("[MetaDataEditor::GotoEndTagType] failed : Exception = '{0}'", ex.Message);
            }
        }//GotoEndTagType


        /// <summary>
        /// Move CurrentTag pointer to first Tag
        /// </summary>
        public void GotoStartTagType()
        {
            try
            {
                m_Log.Trace("[MetaDataEditor::GotoStartTagType]");
                if (this._xDoc != null)
                    this._CurrentTAG = this._xDoc.GetElementsByTagName("TagDataTemplate").Item(0);
                else
                    throw new MetaDataException("[MetaDataEditor::GotoStartTagType] failed : Null xDoc object found.Template is not open.");

                //Reset Curr Element to the 1stElement of newly assigned CurrentTag.
                this.ResetCurrElementAfterSetngCurrTAG();
            }
            catch (MetaDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MetaDataException("[MetaDataEditor::GotoStartTagType] failed : Exception = '{0}'", ex.Message);
            }
        }//GotoStartTagType


        /// <summary>
        /// move CurrentTag pointer to the specified TagType
        /// </summary>
        /// <param name="tagType"></param>
        public void GotoTagType(TagType tagType)
        {
            try
            {
                m_Log.Trace("[MetaDataEditor::GotoTagType]");

                XmlNodeList ndList;
                if (this._xDoc != null)
                    ndList = this._xDoc.GetElementsByTagName("TagDataTemplate");
                else
                    throw new MetaDataException("[MetaDataEditor::GotoTagType] failed : Null xDoc object found.Template is not open.");

                bool IsCurrentSet = false;

                foreach (XmlNode nd4Curr in ndList)
                {
                    if (tagType.ToString() == nd4Curr.Attributes.GetNamedItem("TagType").Value)
                    {
                        this._CurrentTAG = nd4Curr;
                        IsCurrentSet = true;
                    }
                }

                if (IsCurrentSet == false)
                    throw new MetaDataException("[MetaDataEditor::GotoTagType] failed : Given TagType='{0}' not found.", tagType.ToString());

                //Reset Curr Element to the 1stElement of newly assigned CurrentTag.
                this.ResetCurrElementAfterSetngCurrTAG();
            }
            catch (MetaDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MetaDataException("[MetaDataEditor::GotoTagType] failed : Exception = '{0}'", ex.Message);
            }

        }//GotoTagType(TagType tagType)


        /// <summary>
        /// Get Tag of Current Node.
        /// </summary>
        /// <returns>returns TagType enum</returns>
        public TagType GetCurrentTagType()
        {
            try
            {
                m_Log.Trace("[MetaDataEditor::GetCurrentTagType]");
                return (TagType)Enum.Parse(typeof(TagType), this._CurrentTAG.Attributes.GetNamedItem("TagType").InnerText, true);
            }
            catch (MetaDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MetaDataException("[MetaDataEditor::GetCurrentTagType] failed : Exception = '{0}'", ex.Message);
            }
        }//GetCurrentTagType

        public ByteEndian GetByteEndian()
        {
            string strEndianNess = string.Empty;
            try
            {
                strEndianNess = _xDoc.SelectSingleNode("RFIDTagDataConfig/TemplateHeader/ByteEndian").InnerText;
            }
            catch { }
            if (strEndianNess == null || strEndianNess == "")
                return ByteEndian.LittleEndian;
            return (ByteEndian)Enum.Parse(typeof(ByteEndian), strEndianNess);
        }

        /// <summary>
        /// Get all Tags defind in opened template.
        /// </summary>
        /// <returns>returns array of TagType enum</returns>
        public TagType[] GetAllTagTypes()
        {
            try
            {
                m_Log.Trace("[MetaDataEditor::GetAllTagTypes]");

                XmlNodeList ndList;
                if (this._xDoc != null)
                    ndList = this._xDoc.GetElementsByTagName("TagDataTemplate");
                else
                    throw new MetaDataException("[MetaDataEditor::GetAllTagTypes] failed : Null xDoc object found.Template is not open.");

                TagType[] tagType = new TagType[ndList.Count];

                int idx = 0;
                foreach (XmlNode nd4Curr in ndList)
                {
                    tagType[idx] = (TagType)Enum.Parse(typeof(TagType), nd4Curr.Attributes.GetNamedItem("TagType").Value, true);
                    idx++;
                }
                return tagType;
            }
            catch (MetaDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MetaDataException("[MetaDataEditor::GetAllTagTypes] failed : Exception = '{0}'", ex.Message);
            }


        }//GetAllTagTypes


        #endregion [ fxns TagType ] : nxt/prev/start/end/GotoTagType/GetCurrTagType

        #region [ fxns TemplateElement ] : GotoTop/GotoNext/GotoPrevios/GotoLast/GetCount/GetCurrentTemplateElement/GetEnumerator/GetAllTemplateElements/SetAllTemplateElement/GetAllTemplateElementsAt/GetUCCNumberingStandard/SetUCCNumberingStandard/

        /// <summary>
        /// Get UCCNumbering Std. for EPC tag type defined in the CurrentTag
        /// </summary>
        /// <returns>returns UCCNumberingStandard enum </returns>
        public UCCNumberingStandard GetUCCNumberingStandard()
        {
            try
            {
                m_Log.Trace("[MetaDataEditor::GetUCCNumberingStandard]");
                UCCNumberingStandard na = new UCCNumberingStandard();

                if (this._CurrentTAG == null)
                    throw new MetaDataException("[MetaDataEditor::GetUCCNumberingStandard] failed : Null CurrentTag found.Template is not open.");

                if (this._CurrentTAG.Attributes.Count >= 2 && this._CurrentTAG.Attributes.Item(1).Name == "UCCNumberingStandard")
                    return (UCCNumberingStandard)Enum.Parse(typeof(UCCNumberingStandard), this._CurrentTAG.Attributes.GetNamedItem("UCCNumberingStandard").Value, true);
                else
                    return na; //returns default=unknown otherwise
            }
            catch (MetaDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MetaDataException("[MetaDataEditor::GetUCCNumberingStandard] failed : Exception = '{0}'", ex.Message);
            }

        }//GetUCCNumberingStandard


        /// <summary>
        /// Get UCCNumbering Std. for EPC tag type passed
        /// </summary>
        /// <returns>returns UCCNumberingStandard enum </returns>
        public UCCNumberingStandard GetUCCNumberingStandard(TagType tag)
        {
            try
            {
                m_Log.Trace("[MetaDataEditor::GetUCCNumberingStandard]");
                UCCNumberingStandard na = new UCCNumberingStandard();
                XmlNode xNode = _xDoc.SelectSingleNode("/RFIDTagDataConfig/TemplateBody/TagDataTemplate[@TagType='" + tag.ToString() + "']/@UCCNumberingStandard");

                if (xNode == null || xNode.Value == "")
                    return na; //returns default=unknown otherwise
                else
                    return (UCCNumberingStandard)Enum.Parse(typeof(UCCNumberingStandard), xNode.Value, true);

            }
            catch (MetaDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MetaDataException("[MetaDataEditor::GetUCCNumberingStandard] failed : Exception = '{0}'", ex.Message);
            }

        }//GetUCCNumberingStandard


        /// <summary>
        /// Set UCCNumbering Std. for CurrentTag (If it is of Type EPCClass1).
        /// </summary>
        /// <param name="UccStd">enumaratoin of MetaData.UCCNumberingStandard to set</param>
        public void SetUCCNumberingStandard(UCCNumberingStandard UccStd)
        {
            try
            {
                m_Log.Trace("[MetaDataEditor::SetUCCNumberingStandard]");
                if (this.GetCurrentTagType().ToString().IndexOf("EPC") >= 0)
                {
                    if (this._xDoc != null)
                    {
                        XmlElement newTagDataTemplate = this._xDoc.CreateElement("TagDataTemplate");
                        newTagDataTemplate.SetAttribute("TagType", this.GetCurrentTagType().ToString());
                        newTagDataTemplate.SetAttribute("UCCNumberingStandard", UccStd.ToString());
                        XmlNodeList elmTempltBody = this._xDoc.GetElementsByTagName("TemplateBody");

                        if (this._CurrentTAG == null)
                            throw new MetaDataException("[MetaDataEditor::SetUCCNumberingStandard] failed : Null CurrentTag found.Template is not open.");

                        elmTempltBody[0].ReplaceChild(newTagDataTemplate, this._CurrentTAG);
                        this._CurrentTAG = newTagDataTemplate;
                        this.ResetCurrElementAfterSetngCurrTAG();
                    }
                    else
                        throw new MetaDataException("[MetaDataEditor::SetUCCNumberingStandard] failed : Null xDoc object found.Template is not open.");
                }
            }
            catch (MetaDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MetaDataException("[MetaDataEditor::SetUCCNumberingStandard] failed : Exception = '{0}'", ex.Message);
            }

        }//SetUCCNumberingStandard



        public void SetTemplateHeaderDetails(string tmplName, string tmplDesc, string byteEndian)
        {
            try
            {
                m_Log.Trace("[MetaDataEditor::SetTemplateHeaderDetails] Entered");
                string xPath = "RFIDTagDataConfig/TemplateHeader/";


                _xDoc.SelectSingleNode(xPath + "Name").InnerText = tmplName;
                _xDoc.SelectSingleNode(xPath + "Description").InnerText = tmplDesc;
                _xDoc.SelectSingleNode(xPath + "ByteEndian").InnerText = byteEndian;

                m_Log.Trace("[MetaDataEditor::SetTemplateHeaderDetails] Leaving");

            }
            catch (Exception ex)
            {
                m_Log.Error("[MetaDataEditor::SetTemplateHeaderDetails] Failed: " + ex.Message);
                throw new MetaDataException("[MetaDataEditor::SetTemplateHeaderDetails] failed : Exception = '{0}'", ex.Message);
            }
        }
        /// <summary>
        /// Move CurrentElement pointer to the first element of CurrentTag.
        /// </summary>
        public void GotoTopTemplateElement()
        {
            try
            {
                m_Log.Trace("[MetaDataEditor::GotoTopTemplateElement]");

                if (this._CurrentTAG == null)
                    throw new MetaDataException("[MetaDataEditor::GotoTopTemplateElement] failed : Null CurrentTag found.Template is not open.");

                if (this._CurrentTAG.HasChildNodes == true)
                    if (this._CurrentTAG.HasChildNodes == true)
                        this._CurrentElement = this._CurrentTAG.FirstChild;
            }
            catch (MetaDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MetaDataException("[MetaDataEditor::GotoTopTemplateElement] failed : Exception = '{0}'", ex.Message);
            }
        }//GotoTopTemplateElement


        /// <summary>
        /// Move CurrentElement pointer to its next element.
        /// </summary>
        public void GotoNextTemplateElement()
        {
            try
            {
                m_Log.Trace("[MetaDataEditor::GotoNextTemplateElement]");
                if (this._CurrentElement != null)
                {
                    if (this._CurrentElement.NextSibling != null)
                        this._CurrentElement = this._CurrentElement.NextSibling;
                }
                else
                    throw new MetaDataException("[MetaDataEditor::GotoNextTemplateElement] failed : Null CurrentElement found.");

            }
            catch (MetaDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MetaDataException("[MetaDataEditor::GotoNextTemplateElement] failed : Exception = '{0}'", ex.Message);
            }
        }//GotoNextTemplateElement


        /// <summary>
        /// Move CurrentElement pointer to its previous element.
        /// </summary>
        public void GotoPreviosTemplateElement()
        {
            try
            {
                m_Log.Trace("[MetaDataEditor::GotoPreviosTemplateElement]");
                if (this._CurrentElement != null)
                {
                    if (this._CurrentElement.PreviousSibling != null)
                        this._CurrentElement = this._CurrentElement.PreviousSibling;
                }
                else
                    throw new MetaDataException("[MetaDataEditor::GotoPreviosTemplateElement] failed : Null CurrentElement found.");

            }
            catch (MetaDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MetaDataException("[MetaDataEditor::GotoPreviosTemplateElement] failed : Exception = '{0}'", ex.Message);
            }
        }//GotoPreviosTemplateElement


        /// <summary>
        /// Move CurrentElement pointer to the last element of CurrentTag.
        /// </summary>
        public void GotoLastTemplateElement()
        {
            try
            {
                m_Log.Trace("[MetaDataEditor::GotoLastTemplateElement]");

                if (this._CurrentTAG == null)
                    throw new MetaDataException("[MetaDataEditor::GotoLastTemplateElement] failed : Null CurrentTag found.Template is not open.");

                if (this._CurrentTAG.HasChildNodes == true)
                    if (this._CurrentTAG.FirstChild.HasChildNodes == true)
                        this._CurrentElement = this._CurrentTAG.FirstChild.ChildNodes.Item(this._CurrentTAG.FirstChild.ChildNodes.Count - 1);
            }
            catch (MetaDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MetaDataException("[MetaDataEditor::GotoLastTemplateElement] failed : Exception = '{0}'", ex.Message);
            }
        }//GotoLastTemplateElement


        /// <summary>
        /// Get TemplateElement where the CurrentElement pointing.
        /// </summary>
        /// <returns>returns object of TemplateElement.</returns>
        public TemplateElement GetCurrentTemplateElement()
        {
            try
            {
                m_Log.Trace("[MetaDataEditor::GetCurrentTemplateElement]");

                TemplateElement tmpltElement = this.ReadTemplateElement((XmlElement)this._CurrentElement);
                return tmpltElement;
            }
            catch (MetaDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MetaDataException("[MetaDataEditor::GetCurrentTemplateElement] failed : Exception = '{0}'", ex.Message);
            }
        }//GetCurrentTemplateElement


        /// <summary>
        /// Get count of available Elements under CurrentTag.
        /// </summary>
        /// <returns>returns no of elements</returns>
        public int GetCountOfCurrentTagElements()
        {
            try
            {
                m_Log.Trace("[MetaDataEditor::GetCountOfCurrentTagElements]");
                if (this._CurrentElement != null)
                {
                    int count = this._CurrentElement.ParentNode.ChildNodes.Count;
                    return count;
                }
                //				else
                //					throw new MetaDataException("[MetaDataEditor::GetCountOfCurrentTagElements] failed : Null CurrentElement found.");

                return 0;
            }
            catch (MetaDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MetaDataException("[MetaDataEditor::GetCountOfCurrentTagElements] failed : Exception = '{0}'", ex.Message);
            }
        }//GetCountOfCurrentTagElements


        /// <summary>
        /// Get enumarator for Tag's Elements.
        /// </summary>
        /// <returns>Inumerator obj for elements defined.</returns>
        public System.Collections.IEnumerator GetEnumeratorForTemplateElements()
        {
            try
            {
                m_Log.Trace("[MetaDataEditor::GetEnumeratorForTemplateElements]");
                if (this._CurrentElement != null)
                    return this._CurrentElement.ParentNode.FirstChild.GetEnumerator();
                else
                    throw new MetaDataException("[MetaDataEditor::GetEnumeratorForTemplateElements] failed : Null CurrentElement found.");
            }
            catch (MetaDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MetaDataException("[MetaDataEditor::GetEnumeratorForTemplateElements] failed : Exception = '{0}'", ex.Message);
            }
        }//GetEnumeratorForTemplateElements


        /// <summary>
        /// Get all Elements of current Tag.
        /// </summary>
        /// <returns>return array of TemplateElement</returns>
        public TemplateElement[] GetAllTemplateElements()
        {
            try
            {
                m_Log.Trace("[MetaDataEditor::GetAllTemplateElements]");
                TemplateElement[] TmpltElements = null;

                if (this._CurrentElement != null)
                    TmpltElements = ReadTemplateElements(this._CurrentElement.ParentNode);

                return TmpltElements;
            }
            catch (MetaDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MetaDataException("[MetaDataEditor::GetAllTemplateElements] failed : Exception = '{0}'", ex.Message);
            }
        }//GetAllTemplateElements


        /// <summary>
        /// Updating all elements of current Tag
        /// </summary>
        /// <param name="elements"></param>
        /// <returns>returns bool value whether operation is failed or succeeded.</returns>
        public bool SetAllTemplateElement(TemplateElement[] elements)
        {
            try
            {
                m_Log.Trace("[MetaDataEditor::SetAllTemplateElement]");
                if (this._CurrentTAG != null)
                {
                    if (this._CurrentTAG.HasChildNodes == true)
                        this._CurrentTAG.FirstChild.RemoveAll();
                    else
                    {
                        XmlElement TmpltElems = this._xDoc.CreateElement("TemplateElements");
                        this._CurrentTAG.AppendChild(TmpltElems);
                    }

                    XmlElement elem;
                    foreach (TemplateElement tmpltElem in elements)
                    {
                        elem = this._xDoc.CreateElement("TemplateElement");
                        elem.SetAttribute("Name", tmpltElem.Name);
                        elem.SetAttribute("DataType", tmpltElem.DataType);
                        elem.SetAttribute("StartByteIndex", tmpltElem.StartByteIndex.ToString());
                        elem.SetAttribute("Length", tmpltElem.Length.ToString());
                        elem.SetAttribute("StartBitIndex", tmpltElem.StartBitIndex.ToString());
                        this._CurrentTAG.FirstChild.AppendChild(elem);
                    }

                    this.ResetCurrElementAfterSetngCurrTAG();
                    return true;
                }
                else
                    throw new MetaDataException("[MetaDataEditor::SetAllTemplateElement] failed : Pointer to CurrentTag is Null.");
            }
            catch (MetaDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MetaDataException("[MetaDataEditor::SetAllTemplateElement] failed : Exception='{0}", ex.Message);
            }
        }//SetAllTemplateElement(TemplateElement[] elements)


        /// <summary>
        /// Get all TemplateElements of given Tag 
        /// </summary>
        /// <param name="tag"></param>
        /// <returns>array of TemplateElement</returns>
        public TemplateElement[] GetAllTemplateElementsAt(TagType tag)
        {
            try
            {
                m_Log.Trace("[MetaDataEditor::GetAllTemplateElementsAt]");
                TemplateElement[] tElement = null;

                if (this._xDoc == null)
                    throw new MetaDataException("[MetaDataEditor::GetAllTemplateElementsAt] failed : Null xDoc object found.Template is not open.");

                XmlNodeList ndList = this._xDoc.GetElementsByTagName("TagDataTemplate");
                bool IsCurrentSet = false;

                foreach (XmlNode nd4Curr in ndList)
                {
                    if (tag.ToString() == nd4Curr.Attributes.GetNamedItem("TagType").Value)
                    {
                        if (nd4Curr.FirstChild == null)
                            return tElement;
                        tElement = this.ReadTemplateElements(nd4Curr.FirstChild);
                        IsCurrentSet = true;
                    }
                }

                if (IsCurrentSet == false)
                    throw new MetaDataException("[MetaDataEditor::GetAllTemplateElementsAt] failed : Given TagType='{0}' not found in the current opened Template='{0}'.", tag.ToString(), this._SaveTemplateID);

                return tElement;
            }
            catch (MetaDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MetaDataException("[MetaDataEditor::GetAllTemplateElementsAt] failed : Exception='{0}", ex.Message);
            }

        }//GetAllTemplateElementsAt(TagType tag)
        #endregion [ fxns TemplateElement ] : ...

        #region [ MetaDataEditor level fxns ]

        /// <summary>
        /// Get all templateIDs loaded in memory.
        /// </summary>
        /// <returns>string array containing templateIds</returns>
        public string[] GetAllTemplateIDs()
        {
            try
            {
                m_Log.Trace("[MetaDataEditor::GetAllTemplateIDs]");
                string[] sTmpltIDs = new string[_htTemplateIDLoc.Count];

                int idx = 0;
                foreach (string sTmpltID in _htTemplateIDLoc.Keys)
                {
                    sTmpltIDs[idx] = sTmpltID;
                    idx++;
                }
                return sTmpltIDs;
            }
            catch (MetaDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MetaDataException("[MetaDataEditor::GetAllTemplateIDs] failed: Exception =", ex.Message);
            }
        }//GetAllTemplateIDs()


        /// <summary>
        /// Validate given Template with Xml Schema Definition. 
        /// </summary>
        /// <param name="templateID">Unique template id to validate with xsd.</param>
        /// <returns>return T/F whether template validation is succeeded or failed against schema definition.</returns>
        public bool IsValid(string templateID)
        {
            try
            {
                m_Log.Trace("[MetaDataEditor::IsValid]");
                templateID = templateID.Trim();
                if (_htTemplateIDLoc.ContainsKey(templateID) == false)
                    return false;

                XmlDocument validateTemplate = (XmlDocument)_htTemplateIDLoc[templateID];
                string xmlTmplt = validateTemplate.OuterXml;

                MemoryStream configStream = new MemoryStream(System.Text.Encoding.ASCII.GetBytes(xmlTmplt));
                KTone.RFIDGlobal.RFUtils.ValidateXML(_xsdFileLoc, new StreamReader(configStream));
                return true;
            }
            catch (MetaDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                m_Log.Error("MetaDataEditor::IsValid:" + ex.Message);
                return false;
            }
        }//IsValid


        /// <summary>
        /// Refresh all TagDataTemplate(s). It will close the currently opened template and re initialised all.
        /// </summary>
        public void Refresh()
        {
            m_Log.Trace("[MetaDataEditor::Refresh]");
            this._xDoc = null;
            this._CurrentElement = null;
            this._CurrentTAG = null;
            this._SaveTemplateID = null;
            this._newTemplate = null;
            //InitAll(_tmpltDir, _xsdFileLoc);
        }


        /// <summary>
        /// Refresh only one TAG Template identified by templateID. Throws exception for Invalid , non-existing 
        /// or Malformed XML This method performs Add, Delete or Update based on if 
        /// Meta-definition file is added, deleted or modified. 
        /// </summary>
        /// <param name="templateID">Unique template id</param>
        public void Refresh(string templateID)
        {
            try
            {
                m_Log.Trace("[MetaDataEditor::Refresh]");
                if (templateID.Equals(String.Empty))
                    throw new MetaDataException("[MetaDataEditor::Refresh] failed : Null parameter found for Refresh. templateID = '{0}'", templateID);

                templateID = templateID.Trim();
                if (System.IO.File.Exists(templateID + ".config") == true)
                {
                    if (_htTemplateIDLoc.ContainsKey(templateID) == true)
                    {
                        //Operation = Modify
                        //-----------------------------------------------------------------------
                        RefreshTemplate(templateID, TemplateOperation.TEMPLATE_MODIFIED);
                    }
                    else
                    {
                        //Operation = Add
                        //-----------------------------------------------------------------------
                        RefreshTemplate(templateID, TemplateOperation.TEMPLATE_ADDED);
                    }
                }
                else if (_htTemplateIDLoc.ContainsKey(templateID) == true)
                {
                    //Operation = delete
                    //-----------------------------------------------------------------------
                    RefreshTemplate(templateID, TemplateOperation.TEMPLATE_DELETED);
                }
                else
                {
                    throw new MetaDataException("[MetaDataEditor::Refresh] failed : Given TempplateID='{0}' not found in System dir as well as in system's collection object to perform any of operation ADD/DELETE/MODIFY.", templateID);
                }
            }
            catch (MetaDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MetaDataException("[MetaDataEditor::Refresh] failed : Exception ={0}", ex.Message);
            }

        }//Refresh( string templateID)


        /// <summary>
        /// Refresh only one TAG Template identified by templateID. Throws exception for Invalid , non-existing 
        /// or Malformed XML This method performs Add, Delete or Update based on if 
        /// Meta-definition file is added, deleted or modified. Optionally, returns the operation as ADDED/MODIFIED/DELETED 
        /// </summary>
        /// <param name="templateID">Unique template id</param>
        /// <param name="status">out param to indicate operation performed during refresh for given template</param>
        public void Refresh(string templateID, out TemplateOperation operation)
        {
            try
            {
                m_Log.Trace("[MetaDataEditor::Refresh]");
                if (templateID.Equals(String.Empty))
                    throw new MetaDataException("[MetaDataEditor::Refresh] failed : Parameter templateID='{0}' should not be NULL for Refresh.", templateID);

                templateID = templateID.Trim();
                if (System.IO.File.Exists(templateID + ".config") == true)//this._htTmpltXDoc.ContainsKey(templateID) )
                {
                    if (_htTemplateIDLoc.ContainsKey(templateID) == true)
                    {
                        //Operation = Modify
                        //-----------------------------------------------------------------------
                        operation = TemplateOperation.TEMPLATE_MODIFIED;
                        RefreshTemplate(templateID, operation);
                    }
                    else
                    {
                        //Operation = Add
                        //-----------------------------------------------------------------------
                        operation = TemplateOperation.TEMPLATE_ADDED;
                        RefreshTemplate(templateID, operation);
                    }
                }
                else if (_htTemplateIDLoc.ContainsKey(templateID) == true)
                {
                    //Operation = delete
                    //-----------------------------------------------------------------------
                    operation = TemplateOperation.TEMPLATE_DELETED;
                    RefreshTemplate(templateID, operation);
                }
                else
                {
                    throw new MetaDataException("[MetaDataEditor::Refresh] failed : Given TempplateID='{0}' not found in System dir as well as in system's collection object to perform any of operation ADD/DELETE/MODIFY.", templateID);
                }
            }
            catch (MetaDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MetaDataException("[MetaDataEditor::Refresh] failed : Exception ={0}", ex.Message);
            }

        }//Refresh( string templateID, out TemplateOperation operation ) 



        #endregion [ MetaDataEditor level fxns ]

        #region Methods

        /// <summary>
        /// This collects all unsorted TemplateElement from configuration and then 
        /// checks weather template is in sequence.Also gives sorted dictionary of startindex and length.
        /// <para>Key is startindex </para>
        /// <para>Value is length</para>
        /// </summary>
        /// <param name="xDocTmpl"></param>
        /// <param name="breaks"></param>
        /// <returns></returns>
        private static int CheckBlockSequence(XmlDocument xDocTmpl, out SortedDictionary<int, int> breaks)
        {
            XmlNode node = xDocTmpl.GetElementsByTagName("TemplateElements").Item(0);

            #region Variables

            int currStartIndex = 0;
            int lenght = 0;
            string dataType = string.Empty;
            breaks = new SortedDictionary<int, int>();
            int prevIndex = 0;
            Dictionary<int, int> boolBreaks = new Dictionary<int, int>();

            #endregion Variables

            #region Loop collects unsorted elements
            foreach (XmlNode element in node.ChildNodes)
            {
                currStartIndex = System.Convert.ToInt32(element.Attributes.GetNamedItem("StartByteIndex").InnerText);
                lenght = System.Convert.ToInt32(element.Attributes.GetNamedItem("Length").InnerText);
                dataType = element.Attributes.GetNamedItem("DataType").InnerText.ToUpper();


                if (dataType == "BOOL")
                {
                    if (boolBreaks.ContainsKey(currStartIndex))
                        boolBreaks[currStartIndex] = boolBreaks[currStartIndex] + lenght;
                    else
                    {
                        boolBreaks.Add(currStartIndex, lenght);

                        if (breaks.ContainsKey(currStartIndex))
                            breaks[currStartIndex] = breaks[currStartIndex] + lenght;
                        else
                            breaks.Add(currStartIndex, lenght);
                    }
                }
                else
                {
                    if (breaks.ContainsKey(currStartIndex))
                        breaks[currStartIndex] = breaks[currStartIndex] + lenght;
                    else
                        breaks.Add(currStartIndex, lenght);
                }
            }
            #endregion Loop collect unsorted elements

            #region Find if they are in sequence and break up to fire command
            SortedDictionary<int, int> finalBreaks = new SortedDictionary<int, int>();
            SortedDictionary<int, int>.Enumerator rotor = breaks.GetEnumerator();

            int iteration = breaks.Count;
            bool inSequence = false;
            while (rotor.MoveNext())
            {
                iteration--;

                if (finalBreaks.Count == 0)
                    prevIndex = rotor.Current.Key;

                if (!inSequence)
                    prevIndex = rotor.Current.Key;

                if (breaks.ContainsKey(rotor.Current.Key + rotor.Current.Value))
                {
                    inSequence = true;
                }
                else
                {
                    if (iteration == 0)
                    {
                        inSequence = true;
                    }
                    else
                    {
                        inSequence = false;
                        finalBreaks[rotor.Current.Key] = rotor.Current.Value;
                    }
                }
                if (inSequence)
                {
                    if (finalBreaks.ContainsKey(prevIndex))
                        finalBreaks[prevIndex] = finalBreaks[prevIndex] + rotor.Current.Value;
                    else
                        finalBreaks[prevIndex] = rotor.Current.Value;
                }
            }
            #endregion

            return finalBreaks.Count;
        }

        public int CheckSerialData(string templateId, out SortedDictionary<int, int> dataBreaks)
        {
            if (_templates.ContainsKey(templateId))
            {
                dataBreaks = _templates[templateId].DataBlockInfo;
                return _templates[templateId].DataBlockInfo.Count;
            }
            else
            {
                throw new MetaDataException("Template id is not present");
            }
        }

        public TemplateElement[] GetTemplateElements(string templateName)
        {
            TemplateElement[] TmpltElements = null;
            try
            {                

                m_Log.Trace("[MetaDataEditor::GetTemplateElements]");
                int idx = 0;
                XmlDocument xmlDoc = null;
                if (_htTemplateIDLoc.ContainsKey(templateName))
                    xmlDoc = (XmlDocument)_htTemplateIDLoc[templateName];

                XmlNode ndElements = xmlDoc.SelectSingleNode(@"/RFIDTagTemplate/TemplateBody/TemplateElements");
                if (ndElements != null && ndElements.HasChildNodes)
                {
                    TmpltElements = new TemplateElement[ndElements.ChildNodes.Count];
                    foreach (XmlNode inLoopNd in ndElements.ChildNodes)
                    {
                        TmpltElements[idx].Name = inLoopNd.Attributes.GetNamedItem("Name").Value;
                        TmpltElements[idx].DataType = inLoopNd.Attributes.GetNamedItem("DataType").Value;
                        TmpltElements[idx].StartByteIndex = System.Convert.ToInt32(inLoopNd.Attributes.GetNamedItem("StartByteIndex").Value);
                        TmpltElements[idx].Length = System.Convert.ToInt32(inLoopNd.Attributes.GetNamedItem("Length").Value);
                        TmpltElements[idx].StartBitIndex = System.Convert.ToInt32(inLoopNd.Attributes.GetNamedItem("StartBitIndex").InnerText);
                        idx++;
                    }
                }

            }
            catch (MetaDataException mdEx)
            {
                m_Log.Error(mdEx.Message);
                throw;
            }
            catch (Exception ex)
            {
                m_Log.Error(ex);
                throw new MetaDataException("[MetaDataEditor::ReadTemplateElement] failed : Exception = {0}", ex);
            }

            return TmpltElements;
        }
        #endregion
    }
}
