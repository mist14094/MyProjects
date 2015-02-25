using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace GasBlenderWeb
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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