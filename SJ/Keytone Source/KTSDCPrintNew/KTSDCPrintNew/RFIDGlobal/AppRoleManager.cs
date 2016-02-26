using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using System.Text;
using System.Collections;
using System.Collections.Specialized;
using System.Xml;
using System.IO;
using System.Security.Principal;
using System.Configuration;

using System.Xml.XPath;

namespace KTone.RFIDGlobal
{
    public static class AppRoleManager
    {

        #region [ CONSTANTS (xml) ]
        private const string ROLENAME = "RoleName";
        private const string NAME = "Name";
        private const string ISENABLE = "IsEnable";
        private const string XPATH_ROLESETTING = @"AppRoleMgrConfig/RoleSettings/RoleSetting";

        #endregion [ CONSTANTS (xml) ]

        #region [ private fields ]
        private static bool s_IsInit = false;
        private static string s_RoleName = string.Empty;
        private static Hashtable s_NavPageHash = null;
        private static StringDictionary s_NavPageNameStatusHash = null;
        private static string s_navPageTagStr = string.Empty;
        #endregion [ private fields ]

        #region [ Property REGION ]

        public static bool IsInit
        {
            get
            {
                return s_IsInit;
            }
        }
        public static string RoleName
        {
            get
            {
                return s_RoleName;
            }

        }

        #endregion [ Property REGION ]

        #region [ Public STATIC Methods ]

        public static void Load(string Role, string FileName)
        {
            #region [ Param check ]
            if (Role == null || Role == string.Empty)
            {
                throw new ArgumentException(@"Invalid Parameter. 'Role' should not be Null or Empty ");
            }

            if (FileName == null || FileName == string.Empty)
            {
                throw new ArgumentException(@"Invalid Parameter. 'fileName' should not be Null or Empty ");
            }

            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory +FileName))
            {
                throw new ArgumentException(@"Invalid Parameter. file not found : " + FileName);
            }
            #endregion [ Param check ]

            /////To init all private static fields.
            Init();
            XmlDocument xDoc = null;
            #region [Load xml File ]
            try
            {
                try
                {
                    xDoc = new XmlDocument();
                    xDoc.Load(FileName);
                }
                catch (Exception ex)
                {
                    throw new AppRoleManagerException(@"Load operation failed : Could not load app configuration file : " + FileName, ex);
                }

                s_NavPageHash = new Hashtable();
                s_NavPageNameStatusHash = new StringDictionary();

                string xPathRoleName = XPATH_ROLESETTING + "[attribute::RoleName = '" + Role + "']";

                 XmlNode ndRole = xDoc.SelectSingleNode(xPathRoleName);

                if (ndRole == null)
                    throw new AppRoleManagerException(@"Load operation failed. Could load given role from config. Role = " + Role);

                foreach (XmlNode navPageNode in ndRole.ChildNodes)
                {
                    StringDictionary tagNameHash = new StringDictionary();

                    string navPageName = navPageNode.Attributes.GetNamedItem(NAME).Value;
                    string navPageStatus = navPageNode.Attributes.GetNamedItem(ISENABLE).Value;

                    foreach (XmlNode tagNameNode in navPageNode.ChildNodes)
                    {
                        string tagName = tagNameNode.Attributes.GetNamedItem(NAME).Value;
                        string tagStatus = tagNameNode.Attributes.GetNamedItem(ISENABLE).Value;
                        tagNameHash.Add(tagName.Trim().ToUpper(), tagStatus.Trim().ToUpper());
                    }

                    s_NavPageNameStatusHash.Add(navPageName.Trim().ToUpper(), navPageStatus.Trim().ToUpper());
                    s_NavPageHash.Add(navPageName.Trim().ToUpper(), tagNameHash);
                }
            }
            catch (AppRoleManagerException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                string msg = @"Load operation failed. Error : " + ex.Message;
                throw new AppRoleManagerException(msg, ex);
            }

            #endregion [Load xml File ]

            //// successfully loaded
            s_IsInit = true;
            s_RoleName = Role;
            

        }

        public static string GetAllTagStatus(string NavPage)
        {
            #region [ Param check ]
            if (NavPage == null || NavPage == string.Empty)
            {
                throw new ArgumentException(@"Invalid Parameter. NavPage paramater should not be Null or Empty.");
            }
            #endregion [ Param check ]

            try
            {
                #region [ IsInit check ]
                if (IsInit == false)
                    throw new AppRoleManagerException(@"AppRoleMgr is in invalid state. Please first call Load method.");
                #endregion [ IsInit check ]

                string allTagStatus = "";
                StringDictionary stringDict = null;
                NavPage = NavPage.Trim().ToUpper();
                if (s_NavPageHash.ContainsKey(NavPage))
                {
                    stringDict = (StringDictionary)s_NavPageHash[NavPage];
                    foreach (DictionaryEntry entry in stringDict)
                    {
                        allTagStatus += entry.Key + "$%" + entry.Value;
                        allTagStatus += ",";
                    }

                    return allTagStatus;
                }
                else
                    return string.Empty;
            }
            catch (AppRoleManagerException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                string message = "GetAllTagStatus Failed : Error" + ex.Message;
                throw new AppRoleManagerException(message, ex);
            }
        }

        /// <summary>
        /// Get the status of a particular tag's status as per App Role setting configuration.
        /// </summary>
        /// <param name="NavPage"></param>
        /// <param name="TagName"></param>
        /// <returns>Default returns true (If given entry not found return true else the status from config file).</returns>
        public static bool GetTagStatus(string NavPage, string TagName)
        {
            #region [ Param check ]
            if (NavPage == null || NavPage == string.Empty)
            {
                throw new ArgumentException(@"Invalid parametere found. NavPage should not be null/empty.");
            }
            if (TagName == null || TagName == string.Empty)
            {
                throw new ArgumentException(@"Invalid parametere found. TagName should not be null/empty.");
            }
            #endregion [ Param check ]


            bool status = true;
            try
            {


                #region [ IsInit check ]
                if (IsInit == false)
                    throw new AppRoleManagerException(@"AppRoleMgr is in invalid state. Please first call Load method.");
                #endregion [ IsInit check ]

                NavPage = NavPage.Trim().ToUpper();
                if (s_NavPageHash.ContainsKey(NavPage))
                {
                    StringDictionary stringDict = null;
                    stringDict = (StringDictionary)s_NavPageHash[NavPage];

                    TagName = TagName.Trim().ToUpper();
                    if (stringDict.ContainsKey(TagName))
                    {
                        string s = stringDict[TagName];
                        status = Convert.ToBoolean(s);
                    }
                    else return status;
                }
                else return status;
            }
            catch (AppRoleManagerException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                string message = "GetTagStatus Failed : Error " + ex.Message;
                throw new AppRoleManagerException(message, ex);
            }
            return status;
        }

        #region UnUsedCode
        /*
        /// <summary>
        /// Get the status of the navigation object, is it enable or disable.
        /// </summary>
        /// <param name="NavPage">Navigation object Name</param>
        /// <returns>Default return is true.Returns the status of a Navigation object.</returns>
        public static bool IsAllTagEnable(string NavPage)
        {
            bool result = true;
            try
            {
                #region [ Param check ]
                if (NavPage == null || NavPage == string.Empty)
                {
                    throw new AppRoleManagerException(@"Invalid paramater found. NavPage should not be null/empty.");
                }
                #endregion [ Param check ]                

                if (IsInit == false)
                    throw new AppRoleManagerException(@"AppRoleMgr is in invalid state. Please first call Load method.");

                if (s_NavPageNameStatusHash.ContainsKey(NavPage))
                    result = bool.Parse(s_NavPageNameStatusHash[NavPage]);
            }
            catch (AppRoleManagerException ex)
            {
                //throw ex;
                return result;
            }
            catch (Exception ex)
            {
                //string message = "IsAllTagEnable Failed : Error " + ex.Message;
                //throw new AppRoleManagerException(message);
                return result;
            }

            return result;
        }
        */

        /*
        /// <summary>
        /// Get the status of a particular navigatin object, is it enable or disable.
        /// </summary>
        /// <param name="NavPage">NavPage object name</param>
        /// <returns>Default returns false otherwise status of nav object from the AppRole setting config.</returns>
        public static bool IsAllTagDisable(string NavPage)
        {
            bool result = false;
            try
            {
                #region [ Param check ]
                if (NavPage == null || NavPage == string.Empty)
                {
                    throw new AppRoleManagerException(@"Invalid paramater found. NavPage should not be null/empty.");
                }
                #endregion [ Param check ]

                if (IsInit == false)
                    throw new AppRoleManagerException(@"AppRoleMgr is in invalid state. Please first call Load method.");

                if (s_NavPageNameStatusHash.ContainsKey(NavPage))
                    result = bool.Parse(s_NavPageNameStatusHash[NavPage]);
            }
            catch (AppRoleManagerException ex)
            {
                //throw ex;
                return result;
            }
            catch (Exception ex)
            {
                //string message = "IsAllTagEnable Failed : Error " + ex.Message;
                //throw new AppRoleManagerException(message);
                return result;
            }

            return result;

        }
        */
        #endregion UnUsedCode

        public static bool IsNavObjectEnable(string NavPageName)
        {
            #region [ Param check ]
            if (NavPageName == null || NavPageName.Length <= 0)
                throw new AppRoleManagerException(@"Invalid parameter found. NavePageName should not be null/empty.");
            #endregion [ Param check ]

            bool result = true;

            try
            {
                #region [ IsInit check ]
                if (IsInit == false)
                    throw new AppRoleManagerException(@"AppRoleMgr is in invalid state. Please first call Load method.");
                #endregion [ IsInit check ]

                NavPageName = NavPageName.Trim().ToUpper();
                if (s_NavPageNameStatusHash.ContainsKey(NavPageName))
                    return bool.Parse(s_NavPageNameStatusHash[NavPageName]);
                else
                    return result;
            }
            catch (AppRoleManagerException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new AppRoleManagerException(@"IsNavObjectEnable Failed : Error " + ex.Message, ex);
            }
        }

        public static string[] GetAllNavPage()
        {
            try
            {
                #region [ IsInit check ]
                if (IsInit == false)
                    throw new AppRoleManagerException(@"AppRoleMgr is in invalid state. Please first call Load method.");
                #endregion [ IsInit check ]

                string[] resultArr = new string[s_NavPageNameStatusHash.Keys.Count];
                s_NavPageNameStatusHash.Keys.CopyTo(resultArr, 0);
                return resultArr;
            }
            catch (AppRoleManagerException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new AppRoleManagerException("GetAllNavPage Failed : Error " + ex.Message, ex);
            }
        }

        public static bool IsNavPageExist(string NavPage)
        {
            #region [ Param check ]
            if (NavPage == null || NavPage == string.Empty)
            {
                throw new AppRoleManagerException(@"Invalid parameter found. NavPage should not be Null/empty.");
            }
            #endregion [ Param check ]

            try
            {
                #region [ IsInit check ]
                if (IsInit == false)
                    throw new AppRoleManagerException(@"AppRoleMgr is in invalid state. Please first call Load method.");
                #endregion [ IsInit check ]

                NavPage = NavPage.Trim().ToUpper();
                return s_NavPageHash.ContainsKey(NavPage);
            }
            catch (Exception ex)
            {
                string msg = "IsNavPageExist Failed : Error " + ex.Message;
                throw new AppRoleManagerException(msg, ex);
            }

        }

        public static StringDictionary GetAllTagStatusHash(string NavPage)
        {
            #region [ Param check ]
            if (NavPage == null || NavPage == string.Empty)
            {
                throw new AppRoleManagerException(@"Invalid parameter found. NavPage should not be null/empty.");
            }
            #endregion [ Param check ]

            StringDictionary strDictionary = null;
            try
            {
                #region [ IsInit check ]
                if (IsInit == false)
                    throw new AppRoleManagerException(@"AppRoleMgr is in invalid state. Please first call Load method.");
                #endregion [ IsInit check ]

                NavPage = NavPage.Trim().ToUpper();
                if (s_NavPageHash.ContainsKey(NavPage))
                    strDictionary = (StringDictionary)s_NavPageHash[NavPage];
            }
            catch (Exception ex)
            {
                string message = "GetAllTagStatusHash Failed. Error : " + ex.Message;
                throw new AppRoleManagerException(message, ex);
            }

            return strDictionary;
        }

        public static void ApplyRoleSettingsOnForm(Form objForm)
        {
            try
            {
                #region [ Param check ]
                if (objForm == null || objForm.IsDisposed == true)
                    throw new ArgumentException("Invalid parameter found. objForm should not be Null or Disposed object.");
                #endregion [ Param check ]

                #region [ IsInit check ]
                if (IsInit == false)
                    throw new AppRoleManagerException(@"AppRoleMgr is in invalid state. Please first call Load method.");
                #endregion [ IsInit check ]

                string navPageTagStr = string.Empty;
                if (objForm.Tag != null)
                    navPageTagStr = objForm.Tag.ToString();
                else
                    throw new AppRoleManagerException(@"Tag property found null. (" + objForm.Name + ".Tag)");

                objForm.Enabled = IsNavObjectEnable(navPageTagStr);

                if (objForm.Enabled == false)
                    return;

                if (objForm.MainMenuStrip != null)
                    SetRoleOnFormMenuStrip(navPageTagStr, objForm.MainMenuStrip);

                SetRoleOnFormsOtherCtl(objForm);
            }
            catch (AppRoleManagerException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                string msg = @"ApplyRoleSettings operation failed. Error : " + ex.Message;
                throw new AppRoleManagerException(msg, ex);
            }
        }

        #endregion [ STATIC Method ]

        #region [ Private methods ]
        private static void Init()
        {
            s_IsInit = true;
            s_RoleName = string.Empty;
            s_NavPageHash = null;
            s_NavPageNameStatusHash = null;
        }

        private static void SetRoleOnFormMenuStrip(string FName, MenuStrip mainMenuStrip)
        {
            foreach (ToolStripDropDownItem itm in mainMenuStrip.Items)
            {

                if (itm.Tag != null && itm.Tag.ToString().Trim().Length > 0)
                {
                    string imstr = itm.Tag.ToString();
                    itm.Enabled = GetTagStatus(FName, imstr);
                    if (itm.Enabled)
                        ProcessMenuitems(itm, FName);
                }
            }
        }
        private static void ProcessMenuitems(ToolStripDropDownItem itm, string FName)
        {
            try
            {
                if (itm != null)
                {
                    foreach (object objMenuitem in itm.DropDownItems)
                    {
                        if (objMenuitem is ToolStripMenuItem || objMenuitem is ToolStripDropDownMenu)
                        {
                            ToolStripDropDownItem subitm = (ToolStripDropDownItem)objMenuitem;
                            ProcessMenuitems((ToolStripDropDownItem)subitm, FName);
                        }
                    }
                    string imstr = itm.Tag.ToString();
                    itm.Enabled = GetTagStatus(FName, imstr);
                }
            }
            catch { }
        }



        private static void SetRoleOnFormsOtherCtl(Form objForm)
        {
            if (objForm.Tag != null)
                s_navPageTagStr = objForm.Tag.ToString();
            else
                throw new AppRoleManagerException(@"Tag property found null. (" + objForm.Name + ".Tag)");

            foreach (Control ctl in objForm.Controls)
            {
                if (ctl.Tag != null && ctl.Tag.ToString().Trim().Length > 0)
                {
                    string ctlTagStr = ctl.Tag.ToString();
                    ctl.Enabled = GetTagStatus(s_navPageTagStr, ctlTagStr);
                }

                RecursiveSetControl(ctl.Controls);
            }
        }
        private static void RecursiveSetControl(System.Windows.Forms.Control.ControlCollection ctls)
        {
            foreach (Control ctl in ctls)
            {
                if (ctl.Tag != null && ctl.Tag.ToString().Trim().Length > 0)
                {
                    string ctlTagStr = ctl.Tag.ToString();
                    ctl.Enabled = GetTagStatus(s_navPageTagStr, ctlTagStr);
                }

                if (ctl.Controls.Count > 0)
                    RecursiveSetControl(ctl.Controls);
            }
        }
        #endregion [ Private methods ]

    }

    #region AppRoleManagerException

    public class AppRoleManagerException : Exception
    {
        public AppRoleManagerException()
            : base()
        {
        }

        public AppRoleManagerException(string msg)
            : base(msg)
        {
        }

        public AppRoleManagerException(string msg, Exception ex)
            : base(msg, ex)
        {
        }
    }

    #endregion AppRoleManagerException
}
