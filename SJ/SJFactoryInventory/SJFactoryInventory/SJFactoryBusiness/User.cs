using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NLog;
using SjFactoryDataAccess;
namespace SJFactoryBusiness
{
    public class User
    {
        private readonly Logger _nlog = LogManager.GetCurrentClassLogger();
        private SjFactoryDataAccess.DataAccess Access = new DataAccess();
        public int Sno { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int BaseLocation { get; set; }

        public User()
        {
            _nlog.Trace(message:this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");

            _nlog.Trace(message:this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +System.Reflection.MethodBase.GetCurrentMethod().Name + "::Exit");

        }
        public User CheckUser(string userName, string passWord)
        {
            Access.InsertInternalLog(
                this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                System.Reflection.MethodBase.GetCurrentMethod().Name, userName + "|" +passWord, false,false);
            _nlog.Trace(message:this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return DataTabletoUser(Access.CheckLogin(userName, passWord));
        }

        public User DataTabletoUser(DataTable dt)
        {
            _nlog.Trace(message:this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");

            var user = new User();
            if (dt?.Rows.Count > 0)
            {
                user.Sno= int.Parse(dt.Rows[0]["Sno"].ToString());
                user.Name = dt.Rows[0]["Name"].ToString();
                user.EmailId =  dt.Rows[0]["EmailID"].ToString();
                user.UserName = dt.Rows[0]["Username"].ToString();
                user.RoleId = int.Parse(dt.Rows[0]["RoleID"].ToString());
                user.Active = Boolean.Parse(dt.Rows[0]["Active"].ToString());
                user.CreatedDate = DateTime.Parse(dt.Rows[0]["CreatedDate"].ToString());
                user.ModifiedDate = DateTime.Parse(dt.Rows[0]["ModifiedDate"].ToString());
                user.BaseLocation = int.Parse(dt.Rows[0]["BaseLocation"].ToString()==""?  "0": dt.Rows[0]["BaseLocation"].ToString());
            }
            return user;
        }
    }

}
