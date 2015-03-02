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
    public partial class DeleteBOLHome : System.Web.UI.Page
    {
        private GBBusiness.BusinessAccess _businessAccess = new BusinessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Master != null)
            {
                Label l1 = (Label)Master.FindControl("LV1");
                l1.Text = "Delete - BOL";
                Label l2 = (Label)Master.FindControl("LV2");
                l2.Text = "BOL";
                Label l3 = (Label)Master.FindControl("LV3");
                l3.Text = "Home";
                HtmlAnchor CurrentMenu;
                CurrentMenu = (HtmlAnchor)Master.FindControl("Tab3");
                CurrentMenu.Attributes.Add("class", "active");
                
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
            if (!IsPostBack)
            {
                lblBOLMess.Text = "";
            }
        }

      
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblBOLMess.Text = "";
            GridView1.DataSource = _businessAccess.SelectLoadTBL(TextBox1.Text);
            GridView1.DataBind();
            _businessAccess.InsertLog(Session["ID"].ToString(),
                  System.Reflection.MethodBase.GetCurrentMethod().Name, this.Page.ToString(), DateTime.Now);
                
        }


        protected void Delete(object sender, EventArgs e)
        {
            using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
            {
                _businessAccess.DeleteLoadTBL(row.Cells[0].Text);
                GridView1.DataSource = _businessAccess.SelectLoadTBL(TextBox1.Text);
                GridView1.DataBind();
                lblBOLMess.Text = "Delete successful!";
                // Response.Redirect("BOLReport.aspx?ID=" +row.Cells[0].Text);
                _businessAccess.InsertLog(Session["ID"].ToString(),
                  System.Reflection.MethodBase.GetCurrentMethod().Name, this.Page.ToString(), DateTime.Now);
                

            }
        }
    }
}