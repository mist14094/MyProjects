using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using KTone.DAL.SmartDCDataAccess;
using System.Configuration;
using System.Data.SqlTypes;
using KTone.DAL.KTDBBaseLib;
using TrackerRetailDataAccess;
using KTone.Core.KTIRFID;



namespace KTone.Core.SDCBusinessLogic
{
   
    public class UserClass
    {
        NLog.Logger _log = NLog.LogManager.GetCurrentClassLogger();
        private string _productName = string.Empty;


        public UserClass()
        {
            //string connString = System.Configuration.ConfigurationManager.ConnectionStrings["TrackerRetailConnectionString"].ConnectionString;//@"database=KTSmartDC;server=localhost\keytoneexpress;user id=sa;password=Server05!;";
            string trackerRetailConnString = System.Configuration.ConfigurationManager.ConnectionStrings["TrackerRetailConnectionString"].ConnectionString;
            _log.Trace("Connection String: " + trackerRetailConnString);
            DBInteractionBase.DBConnString = trackerRetailConnString;
            ConnectionProvider.DBConnString = trackerRetailConnString;
            DBInteractionBase.TrackerDBConnString = trackerRetailConnString;

        }

        public bool ValidateUser(string userID, string password, out string userName, out int roleID, out int dataOwnerID, out string dataOwnerName)
        {
            roleID = 0;
            dataOwnerID = 0;
            userName = string.Empty;
            dataOwnerName = string.Empty;
            bool useLocalCredential = false;
            try
            {
                _log.Trace("UserClass:ValidateUser:Entering");
               
                User mUsr = null;

                if (IsUserValidAgainstAD(userID, password, out useLocalCredential, out mUsr))
                {

                }
                else
                {
                    mUsr = new TrackerRetailDataAccess.User();
                    mUsr.UserName = userID;
                    mUsr.Password = password;

                    if (useLocalCredential)
                    {
                        bool authenticated = mUsr.Validate();
                        if (!authenticated)
                        {
                           throw new Exception("Invalid data supplied.");
                        }
                    }
                    else
                    {
                        //throw new Exception("User unchecked for local authentication");
                        _log.Error("UserClass:user unchecked for any authentication(Local or Active Directory");
                        throw new Exception("Invalid data supplied.");
                    }
                }
                roleID = mUsr.RoleID;
                userName = mUsr.Name;
                dataOwnerID = mUsr.DataOwnerID;
                dataOwnerName = mUsr.DataOwnerName;
                _productName = mUsr.ProductName;


                //KTone.DAL.SmartDCDataAccess.SmartDCLD lic = KTone.DAL.SmartDCDataAccess.SmartDCLD.GetInstance(_productName.ToString().ToUpper(), DBInteractionBase.DBConnString);
                //string errMsg = string.Empty;

                //if (!lic.CheckLicenseExpiry(out errMsg))
                //{
                //    _log.Error("License Expired: " + errMsg);
                //    return false;
                   
                //}



                _log.Trace("UserClass:ValidateUser:Leaving");
            }
           
            catch (Exception ex)
            {
                _log.Error("Error:UserClass:ValidateUser:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return true;
        }
        public bool ValidateUser(string userID, string password, out string userName, out int roleID, out int dataOwnerID, out string dataOwnerName, out int UserID)
        {
            roleID = 0;
            dataOwnerID = 0;
            userName = string.Empty;
            dataOwnerName = string.Empty;
            bool useLocalCredential = false;
            try
            {
                _log.Trace("UserClass:ValidateUser:Entering");

                User mUsr = null;

                if (IsUserValidAgainstAD(userID, password, out useLocalCredential, out mUsr))
                {

                }
                else
                {
                    mUsr = new TrackerRetailDataAccess.User();
                    mUsr.UserName = userID;
                    mUsr.Password = password;

                    if (useLocalCredential)
                    {
                        bool authenticated = mUsr.Validate();
                        if (!authenticated)
                        {
                            throw new Exception("Invalid data supplied.");
                        }
                    }
                    else
                    {
                        //throw new Exception("User unchecked for local authentication");
                        _log.Error("UserClass:user unchecked for any authentication(Local or Active Directory");
                        throw new Exception("Invalid data supplied.");
                    }
                }
                roleID = mUsr.RoleID;
                userName = mUsr.Name;
                dataOwnerID = mUsr.DataOwnerID;
                dataOwnerName = mUsr.DataOwnerName;
                _productName = mUsr.ProductName;
                UserID = mUsr.UserID;


                //KTone.DAL.SmartDCDataAccess.SmartDCLD lic = KTone.DAL.SmartDCDataAccess.SmartDCLD.GetInstance(_productName.ToString().ToUpper(), DBInteractionBase.DBConnString);
                //string errMsg = string.Empty;

                //if (!lic.CheckLicenseExpiry(out errMsg))
                //{
                //    _log.Error("License Expired: " + errMsg);
                //    return false;

                //}



                _log.Trace("UserClass:ValidateUser:Leaving");
            }

            catch (Exception ex)
            {
                _log.Error("Error:UserClass:ValidateUser:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return true;
        }

        public bool ValidateUserHH(string userID, string password,string MacAddress , out int HHID )
        {
         
         int roleID = 0;
         string  userName = string.Empty;
         string  dataOwnerName = string.Empty;
         int dataOwnerID = 0; bool result = false;
         bool isValidateUser = false;
         HHID = 0; 


            try
            {
                _log.Trace("UserClass:ValidateUserHH:Entering");

               
                HandheldDevice objHH = new HandheldDevice();

                isValidateUser=ValidateUser(userID, password, out userName, out  roleID, out  dataOwnerID, out  dataOwnerName);

                if (isValidateUser)
                {
                    objHH.MacAddress = MacAddress;
                    objHH.DataOwnerID = dataOwnerID;
                    DataSet dtcount = objHH.ValidateHH();

                    if (dtcount != null && dtcount.Tables[0] != null && dtcount.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dtcount.Tables[0].Rows[0]["Match"].ToString()) > 0)
                        {
                            HHID = Convert.ToInt32(dtcount.Tables[0].Rows[0]["HHID"].ToString());
                            result = true;
                            return result;
                        }
                        else
                        {
                            _log.Trace("UserClass:ValidateUserHH:HandHeld not Registered");
                            return result;
                            throw new Exception("HandHeld not Registered.");
                        }
                    }
                }
                else
                {
                    //throw new Exception("User unchecked for local authentication");
                    _log.Error("UserClass:ValidateUserHH:user unchecked for any authentication(Local or Active Directory");
                    return result;
                    throw new Exception("Invalid data supplied.");
                   
                }              
                _log.Trace("UserClass:ValidateUserHH:Leaving");
                return result;
            }

            catch (Exception ex)
            {
                _log.Error("Error:UserClass:ValidateUser:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
           
        }
       
       
       
        //added to validate user against active directory
        private bool IsUserValidAgainstAD(string userName, string password, out bool useLocalCredential, out User oUser)
        {
            oUser = new User();
            User objUser = new User();
            objUser.UserName = userName;
            DataTable dt = objUser.SelectByUserName();
            _log.Trace("UserClass:IsUserValidAgainstAD:Entering");
            if (dt == null || dt.Rows.Count == 0)
            {
                _log.Debug("UserClass:user does not exist in local system");
                throw new Exception("User name is invalid.");
            }
            useLocalCredential = objUser.UseLocalCredential;

            if (objUser.UseADCredential)
            {
                Role objRole = new Role();
                objRole.RoleID = objUser.RoleID;
                objRole.SelectOne();
                string _adPath = objUser.ADPath;


                ActiveDirectory adObj = new ActiveDirectory();

                try
                {

                    if (!adObj.IsActiveDirectoryExists(_adPath))
                    {
                        _log.Debug("UserClass:active directory path is invalid");
                        throw new Exception("Error validating user");
                    }

                    if (adObj.Authenticate(userName, password, _adPath))
                    {
                        oUser = objUser;
                        return true;
                    }
                    else
                    {
                        _log.Debug("UserClass:active directory validation : user name and password does not match");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    if (objUser.UseLocalCredential)
                        return false;
                    else
                    {
                        throw new Exception(ex.Message);
                       // ThrowFaultError(ex.Message, KTErrorCode.DATA_NOT_FOUND);
                       // return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }

        public List<string> GetAccessibleMenus(string appName, int roleID)
        {
            List<string> mList = new List<string>();
            try
            {
                _log.Trace("UserClass:GetAccessibleMenus:Entering");
              
                int applicationID = 0;
                WinMenuRole mnuRole = new WinMenuRole();
                DataTable tblRl = mnuRole.SelectAllWinApplication();
                DataRow[] rows = tblRl.Select("ExeName = '" + appName.Replace("'", "''") + "'");
                if (rows.Length > 0)
                {
                    applicationID = int.Parse(rows[0]["AppID"].ToString());
                }
                else
                    throw new Exception("Invalid data supplied.");

                WinMenuRole menuRole = new WinMenuRole();
                menuRole.AppId = applicationID;
                menuRole.RoleId = roleID;
                DataTable dt = menuRole.SelectAllAppMenuWAppID();

                var menuList = from drList in dt.AsEnumerable()
                               where drList.Field<string>("Selected") == "1"
                               select drList.Field<string>("MenuName");

                mList = menuList.ToList<string>();
                _log.Trace("UserClass:GetAccessibleMenus:Leaving");
            }
            catch (Exception ex)
            {
                _log.Error("Error:UserClass:GetAccessibleMenus:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
                // ThrowFaultError(ex.Message, KTErrorCode.DATA_NOT_FOUND);
            }
            return mList;
        }

        public List<MenuAccessData> GetAccessibleMenuDetails( string appName, int dataOwnerID)
        {
            List<MenuAccessData> mList = new List<MenuAccessData>();
            try
            {
                _log.Trace("UserClass:GetAccessibleMenuDetails:Entering");

                int applicationID = 0;
                WinMenuRole mnuRole = new WinMenuRole();
                DataTable tblRl = mnuRole.SelectAllWinApplication();
                DataRow[] rows = tblRl.Select("ExeName = '" + appName.Replace("'", "''") + "'");
                if (rows.Length > 0)
                {
                    applicationID = int.Parse(rows[0]["AppID"].ToString());
                }
                else
                    throw new Exception("Invalid application name supplied.");

                WinMenuRole menuRole = new WinMenuRole();
                menuRole.DataOwnerID = dataOwnerID;
                DataTable dt = menuRole.SelectAllWinAppMenuSetting();

                var tempMenuList = from drList in dt.AsEnumerable()
                                   where drList.Field<Int32>("AppID") == applicationID
                                   select drList;
                EnumerableRowCollection<DataRow> dtMenuDetails = (EnumerableRowCollection<DataRow>)tempMenuList;
                foreach (DataRow drCurr in dtMenuDetails)
                {
                    MenuAccessData menu = new MenuAccessData();
                    menu.RoleID = Convert.ToInt32(drCurr["RoleID"].ToString());
                    menu.MenuName = drCurr["MenuName"].ToString();
                    mList.Add(menu);
                }
                _log.Trace("UserClass:GetAccessibleMenuDetails:Leaving");
            }
           
            catch (Exception ex)
            {
                _log.Error("Error:UserClass:GetAccessibleMenuDetails:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return mList;
        }

        public List<UserData> GetAllUser()
        {
            List<UserData> userData = new List<UserData>();
            try
            {
                _log.Trace("UserClass:GetAllUser:Entering");
                User dbUser = new User();
                DataTable dtUser = dbUser.SelectAll();
                if (dtUser.Rows.Count <= 0)
                    return userData;

                foreach (DataRow dr in dtUser.Rows)
                {
                    UserData data = new UserData();
                    data.Name = dr["Name"].ToString();
                    data.UserName = dr["UserName"].ToString();
                    data.Password = dr["Password"].ToString();
                    data.EmailID = dr["EmailID"].ToString();

                    bool active = false;

                    if (dr["Active"].ToString() == "Active")
                        active = true;
                    else if (dr["Active"].ToString() == "InActive")
                        active = false;

                    data.Active = active;
                    data.UserID = Convert.ToInt32(dr["UserID"].ToString());
                    data.RoleID = Convert.ToInt32(dr["RoleID"].ToString());
                    data.RoleName = dr["RoleName"].ToString();
                    userData.Add(data);
                }
                _log.Trace("UserClass:GetAllUser:Leaving");
            }
            catch (Exception ex)
            {
                _log.Error("Error:UserClass:GetAllUser:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return userData;
        }

        public bool GetWinAppSettingByUserID(string userID, string applicationName, out string xmlSettings)
        {   
            try
            {
                _log.Trace("UserClass:GetWinAppSettingByUserID:Entering");
                
                User mUsr = new User();
                mUsr.Name = userID;
                xmlSettings = mUsr.GetWinAppSettingByUserID(applicationName);
                _log.Trace("UserClass:GetWinAppSettingByUserID:Leaving");
            }
            catch (Exception ex)
            {
                _log.Error("Error:UserClass:GetWinAppSettingByUserID:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }

            return true;
        }

        public bool UpdateWinAppSettingByUserID(string userID, string xmlSettings, string applicationName)
        {
            bool success = false;
            try
            {
                _log.Trace("UserClass:UpdateWinAppSettingByUserID:Entering");
               
                User mUsr = new User();
                mUsr.Name = userID;
                success = mUsr.UpdateWinAppSettingByUserID(applicationName, xmlSettings);
                _log.Trace("UserClass:UpdateWinAppSettingByUserID:Leaving");
            }
            catch (Exception ex)
            {
                _log.Error("Error:UpdateWinAppSettingByUserID:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }

            return success;
        }


       // ------------ New Method for LookUp Setting ---------------------------------//

        public List<LookUpSettings> GetLookUpSettings(int RoleId)
        {
            List<LookUpSettings> userLookUpSettings = new List<LookUpSettings>();
            try
            {
                _log.Trace("UserClass:GetAllUser:Entering");
                User dbUser = new User();
                dbUser.RoleID = RoleId;
                DataTable dtLookUpSettings = dbUser.GetLookUpSettingForRole();
                if (dtLookUpSettings.Rows.Count <= 0)
                    return userLookUpSettings;

                foreach (DataRow dr in dtLookUpSettings.Rows)
                {
                    LookUpSettings settings = new LookUpSettings();
                    settings.SettingParameter = Convert.ToString(dr["Name"]);
                    settings.SettingValue = Convert.ToString(dr["Value"]); ;
                    userLookUpSettings.Add(settings);
                }
                _log.Trace("UserClass:GetLookUpSettings:Leaving");
            }
            catch (Exception ex)
            {
                _log.Error("Error:UserClass:GetLookUpSettings:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return userLookUpSettings;
        }
    }
}
