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
    public partial class LoadBOLHome : System.Web.UI.Page
    {
        private GBBusiness.BusinessAccess _businessAccess = new BusinessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Master != null)
            {
                Label l1 = (Label)Master.FindControl("LV1");
                l1.Text = "Load - BOL";
                Label l2 = (Label)Master.FindControl("LV2");
                l2.Text = "BOL";
                Label l3 = (Label)Master.FindControl("LV3");
                l3.Text = "Home";
                HtmlAnchor CurrentMenu;
                CurrentMenu = (HtmlAnchor)Master.FindControl("Tab3");
                CurrentMenu.Attributes.Add("class", "active");
            }
            if (!IsPostBack)
            {
                if (Request.QueryString["ID"] != null)
                {
                    TextBox1.Text = Request.QueryString["ID"];
                    GridView1.DataSource = _businessAccess.SelectLoadTBL(TextBox1.Text);
                    GridView1.DataBind();
                }
            }
        }

      
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                GridView1.DataSource = _businessAccess.SelectLoadTBL(TextBox1.Text);
                GridView1.DataBind();
            }
            catch (Exception)
            {
                
               
            }
           
        }


        protected void Edit(object sender, EventArgs e)
        {
            using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
            {
               // ClientScript.RegisterStartupScript(this.GetType(), "newWindow", "<script>window.open('BOLReport.aspx?ID=" + row.Cells[0].Text +
               //                "','Report','left=20,top=20,width=1000,height=600,toolbar=0,resizable=0,scrollbars=yes');</script>");
                Response.Redirect("LoadTruck.aspx?ID=" +row.Cells[0].Text);
       
            }
        }
    }
}