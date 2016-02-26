using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using System.Windows;

namespace KTone.DAL.KTDAGlobaApplLib
{
    public class WinAppRoleSettings
    {
        

        #region Attributes
        private static Form appFrm;
       
        private static bool enabled = false;
        #endregion

        #region Public

        public virtual bool ApplyRoleSettings(Form mainFrm, int roleID)
        {
            //_log.Trace("Enering WinAppRoleSettings.ApplyRoleSettings with roleID : {0}", roleID.ToString());
            try
            {
                appFrm = mainFrm;
                int appID = GetAppID();
                if (appID > 0)
                {

                    ApplyRoles(appID, roleID);
                }
            }
            catch (Exception ex)
            {
                //_log.Error("Error: " + ex.Message);
                throw ex;
            }
            //_log.Trace("Exiting WinAppRoleSettings.ApplyRoleSettings ");
            return true;
        }

        /// <summary>
        /// Function used to enable & disable menus according to the user role.
        /// </summary>
        /// <param name="mainFrm">Form ID on which menu is present</param>
        /// <param name="userName">Name of the user logged in the system</param>
        /// <returns></returns>
        public bool ApplyRoleSettings(Form mainFrm, string userName)
        {
            //_log.Trace("Enering WinAppRoleSettings.ApplyRoleSettings with user : {0}", userName);
            try
            {
                appFrm = mainFrm;
                int appID = GetAppID();
                if (appID > 0)
                {
                    int roleID = GetRoleID(userName);
                    if (roleID > 0)
                    {
                        ApplyRoles(appID, roleID);
                    }
                }
            }
            catch (Exception ex)
            {
                //_log.Error("Error: " + ex.Message);
                throw ex;
            }
            //_log.Trace("Exiting WinAppRoleSettings.ApplyRoleSettings ");
            return true;
        }

        
        #endregion

        #region Private
        /// <summary>
        /// Function used to get the Application ID from database using processname.
        /// </summary>
        /// <returns>appID i.e. Application ID in which user is logged in</returns>
        private int GetAppID()
        {
            //_log.Trace("Enering WinAppRoleSettings.GetAppID ");

            int appID = -1;

            string appName = Process.GetCurrentProcess().ProcessName;

            string[] tempAppName = appName.Split(new char[] { '.' });

            appName = tempAppName[0];

            WinMenuRole mnuRole = new WinMenuRole();
            DataTable tblRl = mnuRole.SelectAllWinApplication();
            DataRow[] rows = tblRl.Select("ExeName = '" + appName + "'");
            if (rows.Length > 0)
            {
                appID = int.Parse(rows[0]["AppID"].ToString());
            }
            //_log.Trace("Exiting WinAppRoleSettings.GetAppID ");
            return appID;
        }

        /// <summary>
        /// Function used to get the Role ID of the user logged in the system.
        /// </summary>
        /// <param name="userName">User Name of the user logged in the system</param>
        /// <returns>roleID i.e. Role ID</returns>
        private int GetRoleID(string userName)
        {
            int roleID = -1;
            //_log.Trace("Entering WinAppRoleSettings.GetRoleID ");

            User usr = new User();
            DataTable tbl = usr.SelectAll();
            DataRow[] rows = tbl.Select("UserName = '" + userName.Replace("'", "''") + "'");
            if (rows.Length > 0)
            {
                int usrID = int.Parse(rows[0]["UserID"].ToString());
                roleID = int.Parse(rows[0]["RoleID"].ToString());
            }
            //_log.Trace("Exiting WinAppRoleSettings.GetRoleID ");
            return roleID;
        }

        /// <summary>
        /// Function used to enable & disable menus according to application & role ID.
        /// </summary>
        /// <param name="appID">Application ID in which user is logged in</param>
        /// <param name="roleID">Role ID of the logged in user</param>
        private void ApplyRoles(int appID, int roleID)
        {
            //_log.Trace("Entering WinAppRoleSettings.ApplyRoles AppID :{0} RoleID : {1}", appID, roleID);
            WinMenuRole cls = new WinMenuRole();
            cls.AppId = appID;
            cls.RoleId = roleID;
            DataTable tbl = cls.SelectAllAppMenuWAppID();

            DataRow[] enabledRows = tbl.Select("Selected = 1");
            foreach (DataRow row in enabledRows)
            {
                string ctrlName = row["MenuName"].ToString();
                enabled = false;
                EnableControl(ctrlName, appFrm);
            }
            //_log.Trace("Exiting WinAppRoleSettings.ApplyRoles ");
        }

        /// <summary>
        /// Function used to enable menus according to menu id find in the database against the logged in user.
        /// </summary>
        /// <param name="ctrlName">Menu name which is assigned to the specific user</param>
        /// <param name="parentControl">Form name on which menu is present</param>
        private void EnableControl(string ctrlName,System.Windows.Forms.Control parentControl)
        {
            //_log.Trace("Entering WinAppRoleSettings.EnableControl ControlName :{0}", ctrlName);
            System.Windows.Forms.Control mainCtrl = parentControl;
            if (!enabled)
            {

                if (mainCtrl is MenuStrip)
                {
                    foreach (ToolStripMenuItem itm in ((MenuStrip)mainCtrl).Items)
                    {
                        if (itm.HasDropDownItems)
                        {
                            foreach (ToolStripMenuItem subItm in itm.DropDownItems)
                            {
                                EnableToolStripMenuItems(ctrlName, subItm);
                            }
                        }
                        if (itm.Name.Equals(ctrlName))
                        {
                            itm.Enabled = true;
                            enabled = true;
                            return;
                        }
                    }
                }
                else
                {
                    foreach (System.Windows.Forms.Control ctrl in mainCtrl.Controls)
                    {
                        EnableControl(ctrlName, ctrl);
                        if (ctrl.Name.Equals(ctrlName))
                        {
                            ctrl.Enabled = true;
                            enabled = true;
                            return;
                        }
                    }
                }
            }
            //_log.Trace("Exiting WinAppRoleSettings.EnableControl");
        }

        /// <summary>
        /// Function used to enable sub menus according to menu id find in the database against the logged in user.
        /// </summary>
        /// <param name="ctrlName">Sub menu name which is assigned to the specific user</param>
        /// <param name="itm">Main menu name under which sub menu is present</param>
        private void EnableToolStripMenuItems(string ctrlName, ToolStripMenuItem itm)
        {
            if (itm.HasDropDownItems)
            {
                foreach (ToolStripMenuItem subItm in itm.DropDownItems)
                {
                    EnableToolStripMenuItems(ctrlName, subItm);
                }
            }
            if (itm.Name.Equals(ctrlName))
            {
                itm.Enabled = true;
                enabled = true;
                return;
            }
            return;
        }


        #endregion


        
    }
}
