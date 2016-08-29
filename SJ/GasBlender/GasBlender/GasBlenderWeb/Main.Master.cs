using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using GBBusiness;

namespace GasBlenderWeb
{
    public partial class Main : System.Web.UI.MasterPage
    {
        private GBBusiness.BusinessAccess _businessAccess = new BusinessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {







        DataTable dt = new DataTable();
            dt = _businessAccess.CheckUser("admin", "Smokin2293");
            if (dt != null)

                if (dt.Rows.Count > 0)
                {
                    Session["Username"] = "admin";
                    Session["ID"] = dt.Rows[0]["ID"].ToString();
                    Session["IsAdmin"] = dt.Rows[0]["IsAdmin"].ToString();
                    Session["Name"] = dt.Rows[0]["Name"].ToString();
                    _businessAccess.InsertLog(Session["ID"].ToString(),
                      System.Reflection.MethodBase.GetCurrentMethod().Name, this.Page.ToString(), DateTime.Now);
               //     Response.Redirect("Default.aspx");

                }
               



            if (Session["Username"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (Session["IsAdmin"].ToString() == "True")
            {
               //  HtmlGenericControl li = new HtmlGenericControl("User");
               //  li.Attributes.Add("style", "display:none");//.Add("display","none");
                 HtmlGenericControl li = new HtmlGenericControl("li");
                 ULMain.Controls.Add(li);
                 HtmlGenericControl anchor = new HtmlGenericControl("a");
                 anchor.Attributes.Add("href", "SetupUser.aspx");
                   anchor.Attributes.Add("id", "Tab6");
                   anchor.Attributes.Add("runat","server");
                HtmlGenericControl span = new HtmlGenericControl("span");
                span.InnerText="User Management";
                anchor.Controls.Add(span);
                 //anchor.InnerText = "Tab Text";
                 li.Controls.Add(anchor);
            }

            lnkUserName.Text = Session["Name"].ToString();
        }

        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }
    }
}