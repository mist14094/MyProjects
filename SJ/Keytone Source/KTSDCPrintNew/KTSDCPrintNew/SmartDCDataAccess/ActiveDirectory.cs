using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices;
using System.Data;

namespace KTone.DAL.SmartDCDataAccess
{
    public class ActiveDirectory
    {
        NLog.Logger _log = NLog.LogManager.GetCurrentClassLogger();
        public ActiveDirectory()
        {
        }

        /// <summary>
        /// Retuns Users for a given AD Path
        /// </summary>
        /// <param name="ADPath"></param>
        /// <returns></returns>
        public DataTable GetUser(string ADPath)
        {
            DataTable dt =new DataTable ();

            DataColumn colName=new DataColumn ("Name",typeof(string));
            DataColumn colUserName=new DataColumn("UserName",typeof(string));
            DataColumn colemailID=new DataColumn ("EmailID",typeof(string));
            dt.Columns.Add(colName);
            dt.Columns.Add(colUserName);
            dt.Columns.Add(colemailID);

            GetADSearchResult(dt, ADPath);

            return dt;
        }

        private DataRow GetRowFromValue(DataRow dr, string name, string userName, string EmailID)
        {
            dr["Name"] = name;
            dr["UserName"] = userName;
            dr["EmailID"] = EmailID;

            return dr;
        }

        private void GetADSearchResult(DataTable dt, string _adPath)
        {

            DirectoryEntry dirEntry = new DirectoryEntry("LDAP://"+_adPath);
            DirectorySearcher ds = new DirectorySearcher(dirEntry);
           
            ds.Filter = GetFilterString();
            //ds.PropertyNamesOnly = true;
            ds.PropertiesToLoad.Add("samaccountname");
            ds.PropertiesToLoad.Add("mail");
            ds.PropertiesToLoad.Add("cn");
            ds.PropertiesToLoad.Add("userAccountControl");
            ds.PropertiesToLoad.Add("memberOf");
            ds.Sort = new SortOption("cn", SortDirection.Ascending);           
            SearchResultCollection src = ds.FindAll();

            DataRow dr = null;
            foreach (SearchResult sr in src)
            {

                int userAccountControl = Convert.ToInt32(GetProperty(sr, "userAccountControl"));
                bool disabled = ((userAccountControl & 2) > 0);
                if (!disabled)
                {                    
                    dr = GetRowFromValue(dt.NewRow(), GetProperty(sr, "cn"), GetProperty(sr, "samaccountname"), GetProperty(sr, "mail"));
                    dt.Rows.Add(dr);
                }                
            }
        }

        private static string GetProperty(SearchResult searchResult, string PropertyName)
         {
           if(searchResult.Properties.Contains(PropertyName))
           {
            return searchResult.Properties[PropertyName][0].ToString() ;
           }
           else
           {
            return string.Empty;
           }
        }
     
        private string GetFilterString()
        {
            return "(&(objectClass=user)(objectCategory=person))";
        }
        /// <summary>
        /// Checks if the active directory exists or not at the given path
        /// </summary>
        /// <param name="ADPath">Active Directory Path</param>
        /// <returns></returns>
        public bool IsActiveDirectoryExists(string ADPath)
        {            
            try
            {
                /*has been commented and by default returning true.
                coz it creates authentication problem in checking the directory path 
                */
                //return DirectoryEntry.Exists("LDAP://" + ADPath); 
                return true;
            }
            catch(Exception ex)
            {
                _log.Error("Error while checking the LDAP path: LDAP://" + ADPath + " ErrorMsg = " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Authenticate a user against a given active directory path
        /// </summary>
        /// <param name="userName">User name as per active directory</param>
        /// <param name="password">Password as per active directory</param>
        /// <param name="adPath">Path of the active directory</param>
        /// <returns></returns>
        public bool Authenticate(string userName,string password, string adPath)
        {
            bool authentic = false;
            try
            {
                DirectoryEntry entry = new DirectoryEntry("LDAP://" + adPath,
                    userName, password,AuthenticationTypes.Secure);
                object nativeObject = entry.NativeObject;
                authentic = true;
            }
            catch (DirectoryServicesCOMException)
            {
                throw ;
            }
            catch (Exception)
            {
                throw;               
            }
            return authentic;
        }

        /// <summary>
        /// is user whith given login name exists or not
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool IsUserExistInAD(string username,string _adPath)
        {
            try
            {
                DirectoryEntry dirEntry = new DirectoryEntry("LDAP://" + _adPath);
                DirectorySearcher search = new DirectorySearcher(dirEntry);
                search.Filter = String.Format("(SAMAccountName={0})", username);
                search.PropertiesToLoad.Add("cn");
                SearchResult result = search.FindOne();

                if (result == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
