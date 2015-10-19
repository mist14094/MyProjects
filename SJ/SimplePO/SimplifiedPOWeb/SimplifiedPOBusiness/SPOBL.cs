using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using NLog;
using SimplifiedPOConstants;
using SimplifiedPODataAccess;

namespace SimplifiedPOBusiness
{
    public class SPOBL
    {
        private readonly Logger _nlog = LogManager.GetCurrentClassLogger();
        private SimplifiedPODataAccess.SPODL _access = new SPODL();
        private SimplifiedPOConstants.POClass _poClass = new POClass();
        private SimplifiedPOConstants.User _user = new User();
        public SPOBL()
        {
            _nlog.Trace(message:
                this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            _access = new SPODL();
            _nlog.Trace(message:
                this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "::Exit");
        }

        public DataTable SalesTransationsGroupedByItem(DateTime startdate, DateTime enddate, int strNbr)
        {
            _nlog.Trace(message:
                this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return _access.Test(startdate, enddate, strNbr);

        }

        public string TestMethod(string testString)
        {
            _nlog.Trace(message:
    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return _access.Test(testString);
        }

        public User Login(string Username, string Password)
        {
            _nlog.Trace(message:
                this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return LoginUser(_access.Login(Username, HashString(Password)));
            
        }

        private User LoginUser(DataTable dt)
        {
            User user = new User();
            if (dt.Rows.Count > 0)
            {
                user.Email = dt.Rows[0]["Email"].ToString();
                user.FirstName = dt.Rows[0]["Firstname"].ToString();
                user.LastName = dt.Rows[0]["LastName"].ToString();
                user.NeedsChange = bool.Parse(dt.Rows[0]["needsChange"].ToString());
                user.UserID = int.Parse(dt.Rows[0]["UserID"].ToString());
                user.UserName = dt.Rows[0]["Username"].ToString();
               
            }
            else
            {
                user = null;
            }
            return user;
        }

        public List<User> GetAllUsers()
        {
            _nlog.Trace(message:
                this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return GetUserList(_access.GetAllUsers());

        }
        private List<User> GetUserList(DataTable dt)
        {
            List<User> users = new List<User>();
            foreach (var eachUser in dt.Rows)
            {
                User user = new User();
                user.UserID = int.Parse(((System.Data.DataRow)(eachUser)).ItemArray[dt.Columns["UserID"].Ordinal].ToString());
                user.UserName = (string)((System.Data.DataRow)(eachUser)).ItemArray[dt.Columns["UserName"].Ordinal];
                user.FirstName = (string)((System.Data.DataRow)(eachUser)).ItemArray[dt.Columns["FirstName"].Ordinal];
                user.LastName = (string)((System.Data.DataRow)(eachUser)).ItemArray[dt.Columns["LastName"].Ordinal];
                user.Email = (string)((System.Data.DataRow)(eachUser)).ItemArray[dt.Columns["Email"].Ordinal];
                user.Status = (string)((System.Data.DataRow)(eachUser)).ItemArray[dt.Columns["Status"].Ordinal];
                user.RoleID = int.Parse(((System.Data.DataRow)(eachUser)).ItemArray[dt.Columns["RoleID"].Ordinal].ToString());
                user.Role = (string)((System.Data.DataRow)(eachUser)).ItemArray[dt.Columns["Role"].Ordinal];

                users.Add(user);
                
             
            }
          
            
            return users;
        }

        public string HashString(string Password)
        {
            byte[] asciiBytes = ASCIIEncoding.ASCII.GetBytes(Password);
            byte[] hashedBytes = MD5CryptoServiceProvider.Create().ComputeHash(asciiBytes);
            string hashedString = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            return hashedString;
        }

        public DataTable GetAllEntities()
        {
            _nlog.Trace(message:
                this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return _access.GetAllEntities();
        }

        public DataTable GetSuppliers(string EntitesName)
        {
            _nlog.Trace(message:
                this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return _access.GetSuppliers(EntitesName);
        }

        public DataTable GetSupplierAddress(string EntitesName, string SupplierName)
        {
            _nlog.Trace(message:
                this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return _access.GetSupplierAddress(EntitesName,SupplierName);
        }



        public string CreateTempPO(int loginUserId, string loginUserName, string postFor, string buyerName, string buyerAddress, string buyerContactNumber, int priority, string supplierEntity, string supplierId, string supplierName, string supplierAddress, string supplierContactNumber, string notes)
        {
            _nlog.Trace(message:
      this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
      System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return _access.CreateTempPO(loginUserId, loginUserName, postFor, buyerName, buyerAddress, buyerContactNumber,
                priority, supplierEntity, supplierId, supplierName, supplierAddress, supplierContactNumber, notes);
;
        }

        public DataTable GetUnSubmittedPo(string userId)
        {
            _nlog.Trace(message:
                this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return _access.GetUnSubmittedPo(userId);
        }

        public DataTable DeleteTempPo(string sno)
        {
            _nlog.Trace(message:
                this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return _access.DeleteTempPo(sno);
        }

        public DataTable SearchProducts(string searchString, string companyAlphabet)
        {
            _nlog.Trace(message:
               this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
               System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return _access.SearchProducts(searchString.ToUpper(), companyAlphabet);
        }

        public DataTable GetTempPoDetails(string sno)
        {
            _nlog.Trace(message:
                this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return _access.GetTempPoDetails(sno);
        }

        public string AddItemsTempPo(string StockCode, string POMasterNo, string TotalQuantity)
        {
            _nlog.Trace(message:
                this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return _access.AddItemsTempPo(StockCode,  POMasterNo,  TotalQuantity);
        }

    }
}
