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
    public partial class InsertUser : System.Web.UI.Page
    {
        private GBBusiness.BusinessAccess _businessAccess = new BusinessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Master != null)
            {
                Label l1 = (Label)Master.FindControl("LV1");
                l1.Text = "Insert User";
                Label l2 = (Label)Master.FindControl("LV2");
                l2.Text = "User Management";
                Label l3 = (Label)Master.FindControl("LV3");
                l3.Text = "Home";
             
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                _businessAccess.InsertUser(txtUserName.Text, txtName.Text, txtPassword.Text, txtEmail.Text, chkAdmin.Checked, DateTime.Now);
                lblError.Text = "*Save Succesful";
                btnSave.Enabled = false;
            }
            catch (Exception ex)
            {
                lblError.Text = "*Username already exist";
            }
            
            
        }
    }
}