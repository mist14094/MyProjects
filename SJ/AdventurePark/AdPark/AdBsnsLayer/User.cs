using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Configuration;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AdDataLayer;
using NLog;

namespace AdBsnsLayer
{
    public class User
    {

        private readonly Logger _nlog = LogManager.GetCurrentClassLogger();
        private readonly AdDataLayer.DataAccess _access = new DataAccess();
        public int Sno { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string  Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zipcode { get; set; }
        public string ContactNumber { get; set; }
        public string EmailId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedDate { get; set; }
        public string TagNumber { get; set; }
        public int LoginUsers { get; set; }
        public bool IsImported { get; set; }
        public User()
        {
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");

            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Exit");

        }

        private List<User> DataTabletoUser(DataTable dt)
        {
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            List < User > Users = new List<User>();



            foreach (DataRow drRow  in dt.Rows)
            {
                var user = new User();
                user.Sno = int.Parse(drRow["Sno"].ToString());
                user.FirstName = drRow["FirstName"].ToString();
                user.LastName = drRow["LastName"].ToString();
                user.EmailId = drRow["EmailID"].ToString();
                user.Address = drRow["Address"].ToString();
                user.City = drRow["City"].ToString();
                user.State = drRow["State"].ToString();
                user.Country = drRow["Country"].ToString();
                user.Zipcode = drRow["Zipcode"].ToString();
                user.ContactNumber = drRow["ContactNumber"].ToString();
                user.TagNumber = drRow["TagNumber"].ToString();
                user.CreatedDate = DateTime.Parse(drRow["CreatedDate"].ToString());
                user.DateOfBirth = DateTime.Parse(drRow["DateOfBirth"].ToString());
                user.LoginUsers = int.Parse(drRow["LoginUsers"].ToString());
                Users.Add(user);
            }
          
           
            return Users;
        }


        private List<User> DataTabletoWaiveredUser(DataTable dt)
        {
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            List<User> Users = new List<User>();



            foreach (DataRow drRow in dt.Rows)
            {
                var user = new User();
                user.Sno = int.Parse(drRow["Sno"].ToString());
                user.FirstName = drRow["FirstName"].ToString();
                user.LastName = drRow["LastName"].ToString();
                user.EmailId = drRow["EmailID"].ToString();
                user.Address = drRow["Address"].ToString();
                user.City = drRow["City"].ToString();
                user.State = drRow["State"].ToString();
                user.Country = drRow["Country"].ToString();
                user.Zipcode = drRow["Zipcode"].ToString();
                user.ContactNumber = drRow["ContactNumber"].ToString();
                user.CreatedDate = DateTime.Parse(drRow["CreatedDate"].ToString());
                user.DateOfBirth = DateTime.Parse(drRow["DateOfBirth"].ToString());
                user.IsImported = bool.Parse(drRow["IsImported"].ToString());
                Users.Add(user);
            }


            return Users;
        }

        public List<User> GetAllUsers()
        {

            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return DataTabletoUser(_access.GetAllUsers());
        }

        public int? InsertUser(string FirstName, string LastName, string Address, string City, string State, string Country, string Zipcode,
            string ContactNumber, string EmailID, DateTime DateOfBirth, string TagNumber,int LoginUsers,int ActiveMenu,string  ActiveMenuName)
        {

            DataAccess access = new DataAccess();

            try
            {
                var val = access.InsertUser( FirstName,  LastName,  Address,  City,  State,  Country,  Zipcode,
             ContactNumber,  EmailID,  DateOfBirth,  TagNumber, LoginUsers, ActiveMenu);

                if (val.Rows.Count > 0)
                {
                    Log log = new Log();
                    log.InsertLogMessage(TagNumber, 
                        string.Format("Tag created - ID ={0}, Plan - {1}", val.Rows[0][0].ToString(), ActiveMenuName));
                    return int.Parse(val.Rows[0][0].ToString());

                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }
 public int? InsertUserWaiver(string FirstName, string LastName, string Address, string City, string State, string Country, string Zipcode,
            string ContactNumber, string EmailID, string ParticipantID, DateTime DateOfBirth, DateTime CreatedDate, bool isMinor)
        {

            DataAccess access = new DataAccess();

            try
            {
           //   //  var val = access.InsertUserWaiver( FirstName,  LastName,  Address,  City,  State,  Country,  Zipcode,
           //// ContactNumber,  EmailID,  ParticipantID,  DateOfBirth,  CreatedDate, isMinor);

           //     if (val.Rows.Count > 0)
           //     {
                   
           //         return int.Parse(val.Rows[0][0].ToString());

           //     }
           //     else
           //     {
           //         return null;
           //     }
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }

        public List<User> GetUserDetailsWithWaiver()
        {

            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return DataTabletoWaiveredUser(_access.GetUserDetailsWithWaiver());
        }

        public int? updateUserDetailsWithWaiver(string Sno)
        {

            DataAccess access = new DataAccess();

            try
            {
                var val = access.updateUserDetailsWithWaiver(Sno);

                if (val.Rows.Count > 0)
                {
                   return int.Parse(val.Rows[0][0].ToString());

                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }










    }


}
