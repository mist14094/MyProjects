using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using GBBusiness;

namespace GasBlenderWeb
{
    public partial class SetupUser : System.Web.UI.Page
    { 
        private GBBusiness.BusinessAccess _businessAccess = new BusinessAccess();

        private void Page_PreRender(object sender, EventArgs e)
        {
            if (Master != null)
            {
                Label l1 = (Label)Master.FindControl("LV1");
                l1.Text = "Setup User";
                Label l2 = (Label)Master.FindControl("LV2");
                l2.Text = "User Management";
                Label l3 = (Label)Master.FindControl("LV3");
                l3.Text = "Home";
                HtmlAnchor CurrentMenu;
                CurrentMenu = (HtmlAnchor)Page.FindControl("Tab6");
                //    CurrentMenu.Attributes.Add("class", "active");

            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
          

            if (!IsPostBack)
            {
                GridView1.DataSource = _businessAccess.GetUser();
                GridView1.DataBind();
            }
        }


        protected void btnInsert_Click(object sender, EventArgs e)
        {
           // _businessAccess.InsertUser("Vivek", "123", "vramalingam@smokinjoe.com", true, DateTime.Now);
           //int? User = _businessAccess.CheckUser("Vivek", "1243");
            Response.Redirect("InsertUser.aspx");
        }

        public void Remove(object sender, EventArgs e)
        {
            using (GridViewRow row = (GridViewRow) ((LinkButton) sender).Parent.Parent)
            {
                _businessAccess.DeleteUser(row.Cells[0].Text);
            }
            GridView1.DataSource = _businessAccess.GetUser();
            GridView1.DataBind();
            _businessAccess.InsertLog(Session["ID"].ToString(),
  System.Reflection.MethodBase.GetCurrentMethod().Name, this.Page.ToString(), DateTime.Now);
        }

        public void Edit(object sender, EventArgs e)
        {
            using (GridViewRow row = (GridViewRow) ((LinkButton) sender).Parent.Parent)
            {
                Response.Redirect("EditUser.aspx?ID="+row.Cells[0].Text);
            }
            GridView1.DataSource = _businessAccess.GetUser();
            GridView1.DataBind();
            _businessAccess.InsertLog(Session["ID"].ToString(),
  System.Reflection.MethodBase.GetCurrentMethod().Name, this.Page.ToString(), DateTime.Now);
        }
    }
}