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
    public partial class ReprintBOL : System.Web.UI.Page
    {
        private GBBusiness.BusinessAccess _businessAccess = new BusinessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Master != null)
            {
                Label l1 = (Label)Master.FindControl("LV1");
                l1.Text = "Reprint - BOL";
                Label l2 = (Label)Master.FindControl("LV2");
                l2.Text = "BOL";
                Label l3 = (Label)Master.FindControl("LV3");
                l3.Text = "Home";
                HtmlAnchor CurrentMenu;
                CurrentMenu = (HtmlAnchor)Master.FindControl("Tab5");
                CurrentMenu.Attributes.Add("class", "active");
                _businessAccess.InsertLog(Session["ID"].ToString(),
                  System.Reflection.MethodBase.GetCurrentMethod().Name, this.Page.ToString(), DateTime.Now);
                
            }
            if (!IsPostBack)
            {
                GridView1.DataSource = GetResult(txtBolID.Text);
                GridView1.DataBind();
            }
        }

      
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GridView1.DataSource = GetResult(txtBolID.Text);
            GridView1.DataBind();
        }


        protected void Edit(object sender, EventArgs e)
        {
            using (GridViewRow row = (GridViewRow)((ImageButton)sender).Parent.Parent)
            {
               // ClientScript.RegisterStartupScript(this.GetType(), "newWindow", "<script>window.open('BOLReport.aspx?ID=" + row.Cells[0].Text +
               //                "','Report','left=20,top=20,width=1000,height=600,toolbar=0,resizable=0,scrollbars=yes');</script>");
                Response.Redirect("LoadTruck.aspx?ID=" +row.Cells[0].Text);
       
            }
        }

        protected void RePrint(object sender, EventArgs e)
        {
            using (GridViewRow row = (GridViewRow)((ImageButton)sender).Parent.Parent)
            {
                // ClientScript.RegisterStartupScript(this.GetType(), "newWindow", "<script>window.open('BOLReport.aspx?ID=" + row.Cells[0].Text +
                //                "','Report','left=20,top=20,width=1000,height=600,toolbar=0,resizable=0,scrollbars=yes');</script>");
                //int intId = 100;

                //string strPopup = "<script language='javascript' ID='script1'>"

                //// Passing intId to popup window.
                //+ "window.open('BOLReport.aspx?ID=" + row.Cells[0].Text

                //+ "','Report', 'top=90, left=200, width=700, height=700, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"

                //+ "</script>";

                //ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);

                Response.Redirect("BOLReport.aspx?ID=" + row.Cells[0].Text);
            }
        }

        protected void Delete(object sender, EventArgs e)
        {
            using (GridViewRow row = (GridViewRow)((ImageButton)sender).Parent.Parent)
            {
                // ClientScript.RegisterStartupScript(this.GetType(), "newWindow", "<script>window.open('BOLReport.aspx?ID=" + row.Cells[0].Text +
                //                "','Report','left=20,top=20,width=1000,height=600,toolbar=0,resizable=0,scrollbars=yes');</script>");
                Response.Redirect("DeleteBOLHome.aspx?ID=" + row.Cells[0].Text);

            }
        }



        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataSource = GetResult(txtBolID.Text);
            GridView1.DataBind();
        }

        public DataTable GetResult(string BOLid)
        {
            DataTable dt = new DataTable();
            if (BOLid != "")
            {
                return _businessAccess.SelectLoadTBL(txtBolID.Text);
            }
            else
            {
                return _businessAccess.GetLoad();
            }
        }


    }
}