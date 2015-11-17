using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
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
        private SimplifiedPOConstants.SPOConst _spoConst = new SPOConst();
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
            return _access.GetSupplierAddress(EntitesName, SupplierName);
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

        public string UpdateTempPO(int loginUserId, string loginUserName, string postFor, string buyerName, string buyerAddress, string buyerContactNumber, int priority, string supplierEntity, string supplierId, string supplierName, string supplierAddress, string supplierContactNumber, string notes, string sno)
        {
            _nlog.Trace(message:
      this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
      System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return _access.UpdateTempPO(loginUserId, loginUserName, postFor, buyerName, buyerAddress, buyerContactNumber,
                priority, supplierEntity, supplierId, supplierName, supplierAddress, supplierContactNumber, notes,sno);
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
            return _access.AddItemsTempPo(StockCode, POMasterNo, TotalQuantity);
        }

        public DataTable UpdateAttributesPo(string sno, float PoCost, float SubTotal, float Shipping, float Discount,
            float Total, bool CheckRequired, bool RFIDTagsPrint,
            string OrderType, string PReason)
        {
            _nlog.Trace(message:
               this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
               System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return _access.UpdateAttributesPo(sno, PoCost, SubTotal, Shipping, Discount,
             Total, CheckRequired, RFIDTagsPrint,
             OrderType, PReason);
        }
        public DataTable SubmitPOForApproval(string sno)
        { _nlog.Trace(message:
               this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
               System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
        var result= _access.SubmitPOForApproval(sno);
            
            if (result.Rows != null)
            {
                var inserstresult = InsertItemsIntoPO(sno, PONumber: result.Rows[0]["POID"].ToString());
                var postactivity  = _access.PostPoSubmissionActivity(sno, result.Rows[0]["PONUMBER"].ToString());
                var poaccesscode  = GetAccessCodeForPO(sno);
                try
                {
                    SendEmail(sno, poaccesscode.Rows[0]["AccessCode"].ToString());
                }
                catch (Exception)
                {
                    
             
                }
             
            }

            return result;
        }


        public string SendEmail(string sno,string accesscode)
        {

            try
            {
                var EmailID = GetEmailID(sno);
                var LoginEmails = string.Join(",", EmailID.AsEnumerable()
                    .Where(r => r.Field<string>("TYPE") == "LoginEmail")
                    .Select(r => r.Field<string>("EMAIL"))
                    .ToArray());
                var PostAsEmails = string.Join(",", EmailID.AsEnumerable()
                    .Where(r => r.Field<string>("TYPE") == "PostForEmail")
                    .Select(r => r.Field<string>("EMAIL"))
                    .ToArray());
                var ApprovalEmails = string.Join(",", EmailID.AsEnumerable()
                    .Where(r => r.Field<string>("TYPE") == "ApprovalEmail")
                    .Select(r => r.Field<string>("EMAIL"))
                    .ToArray());

                SendEmail(LoginEmails, "PO Sumbmitted For Approval", getPODetailsasHTML(_spoConst.MailPreviewURL + accesscode));
            }
            catch (Exception)
            {
              
            }
       
            return "";
        }

        private DataTable InsertItemsIntoPO(string sno, string PONumber)
        { _nlog.Trace(message:
               this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
               System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
        return _access.InsertItemsIntoPO(sno, PONumber);
        }

        public DataTable GetPOMasterDetail(string sno)
        {
            _nlog.Trace(message:
              this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
              System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return _access.GetPOMasterDetail(sno);
        }

        public DataTable GetPOItemsDetail(string sno)
        {
            _nlog.Trace(message:
              this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
              System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return _access.GetPOItemsDetail(sno);
        }

        public string GetOnlinePO(string accesscode)
        {
            _nlog.Trace(message:
           this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
           System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return _access.GetOnlinePO(accesscode);
        }

        public DataTable GetEmailID(string sno)
        {
            _nlog.Trace(message:
           this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
           System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return _access.GetEmailID(sno);
        }  
        
        
        public DataTable GetAccessCodeForPO(string PONumber)
        {
            _nlog.Trace(message:
           this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
           System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return _access.GetAccessCodeForPO(PONumber);
        }

        public string SendEmail(string strTo, string strSubject, string strMessage)
        {
            if (strTo.Trim() != "")
            {
                try
                {

                    SmtpClient client = new SmtpClient();
                    client.Port = 587;
                    client.Host = "smtp.office365.com";
                    client.EnableSsl = true;
                    client.Timeout = 10000;
                    client.ServicePoint.ConnectionLeaseTimeout = 5000;
                    client.ServicePoint.MaxIdleTime = 5000;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new System.Net.NetworkCredential(_spoConst.EMailID, _spoConst.EMailPass);
                    MailMessage mm = new MailMessage(_spoConst.EMailID, strTo, strSubject, strMessage);

                    mm.BodyEncoding = UTF8Encoding.UTF8;
                    mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                    ServicePointManager.ServerCertificateValidationCallback =
                                                                            delegate(object s, X509Certificate certificate,
                                                                              X509Chain chain, SslPolicyErrors sslPolicyErrors)
                                                                            { return true; };
                    mm.IsBodyHtml = true;
                    client.Send(mm);
                    client.Dispose();
                    return "yes";
                }

                catch (Exception ex)
                {
                    return ex.Message.ToString() + ex.StackTrace;
                }
            }
            else
            {
                return "No Active To Address";
            }

        }

        public string getPODetailsasHTML(string URL)
        {
            string urlAddress = URL;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }

                string data = readStream.ReadToEnd();

                response.Close();
                readStream.Close();
                return data;
            }
            return null;
        }


    }
}
