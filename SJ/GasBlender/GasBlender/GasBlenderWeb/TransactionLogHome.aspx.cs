using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace GasBlenderWeb
{
    public partial class TransactionLogHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Master != null)
            {
                Label l1 = (Label)Master.FindControl("LV1");
                l1.Text = "Transaction Log";
                Label l2 = (Label)Master.FindControl("LV2");
                l2.Text = "Reports";
                Label l3 = (Label)Master.FindControl("LV3");
                l3.Text = "Home";
                HtmlAnchor CurrentMenu;
                CurrentMenu = (HtmlAnchor)Master.FindControl("Tab5");
                CurrentMenu.Attributes.Add("class", "active");
            }
            if (!IsPostBack)
            {
                LoadData();
            }
        }


        public void LoadData()
        {
            for (int i = 1; i <= 12; i++)
            {
                ddlHourStart.Items.Add(i.ToString("D2"));
                ddlHourEnd.Items.Add(i.ToString("D2"));
            }

           
            for (int i = 0; i < 60; i++)
            {
                ddlMinuteStart.Items.Add(i.ToString("D2"));
                ddlMinuteEnd.Items.Add(i.ToString("D2"));

                ddlSecondStart.Items.Add(i.ToString("D2"));
                ddlSecondEnd.Items.Add(i.ToString("D2"));
            }

            ddlAMStart.Items.Add("AM"); ddlAMStart.Items.Add("PM");
            ddlAMEnd.Items.Add("AM"); ddlAMEnd.Items.Add("PM");

            txtEndDate.Text = DateTime.Now.ToString("d");
            txtStartDate.Text = DateTime.Now.AddDays(-30).ToString("d");

            ddlHourStart.SelectedIndex = ddlHourStart.Items.IndexOf(new ListItem(DateTime.Now.ToString("hh")));
            ddlHourEnd.SelectedIndex = ddlHourEnd.Items.IndexOf(new ListItem(DateTime.Now.ToString("hh")));

            ddlMinuteEnd.SelectedIndex = ddlMinuteEnd.Items.IndexOf(new ListItem(DateTime.Now.ToString("mm")));
            ddlMinuteStart.SelectedIndex = ddlMinuteStart.Items.IndexOf(new ListItem(DateTime.Now.ToString("mm")));

            ddlSecondEnd.SelectedIndex = ddlSecondEnd.Items.IndexOf(new ListItem(DateTime.Now.ToString("ss")));
            ddlSecondStart.SelectedIndex = ddlSecondStart.Items.IndexOf(new ListItem(DateTime.Now.ToString("ss")));

            ddlAMStart.SelectedIndex = ddlAMStart.Items.IndexOf(new ListItem(DateTime.Now.ToString("tt")));
            ddlAMEnd.SelectedIndex = ddlAMEnd.Items.IndexOf(new ListItem(DateTime.Now.ToString("tt")));




        }

     

        protected void btnGetData_Click(object sender, EventArgs e)
        {
            int intId = 100;

            string strPopup = "<script language='javascript' ID='script1'>"

            // Passing intId to popup window.
            + "window.open('TransactionReport.aspx?Start=" + txtStartDate.Text+" "+ddlHourStart.SelectedValue+":"+ddlMinuteStart.SelectedValue+":"+ddlSecondStart.SelectedValue+" "+ddlAMStart.SelectedValue+"&End="
            + txtEndDate.Text + " " + ddlHourEnd.SelectedValue + ":" + ddlMinuteEnd.SelectedValue + ":" + ddlSecondEnd.SelectedValue + " " +ddlAMEnd.SelectedValue
            + "','Report', 'top=90, left=200, width=700, height=700, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"

            + "</script>";

            ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);


            //  Response.Redirect("Default.aspx");
       //     btnSave.Enabled = false;
        }

      
    }
}