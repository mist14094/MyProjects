using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GBBusiness;
using System.Configuration;
namespace GasBlenderWeb
{
    public partial class Login : System.Web.UI.Page
    {
        private GBBusiness.BusinessAccess _businessAccess = new BusinessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] != null)
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = _businessAccess.CheckUser(txtUserID.Text, txtPassword.Text);
            if (dt != null)

                if (dt.Rows.Count > 0)
                {
                    Session["Username"] = txtUserID.Text;
                    Session["ID"] = dt.Rows[0]["ID"].ToString();
                    Session["IsAdmin"] = dt.Rows[0]["IsAdmin"].ToString();
                    Session["Name"] = dt.Rows[0]["Name"].ToString();
                    _businessAccess.InsertLog(Session["ID"].ToString(),
                      System.Reflection.MethodBase.GetCurrentMethod().Name, this.Page.ToString(), DateTime.Now);
                    Response.Redirect("Default.aspx");
                  
                }
                else
                {
                    lblPassword.Text = "Wrong username/password";

                }
        }
    }
}