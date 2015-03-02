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
    public partial class SEditUser : System.Web.UI.Page
    {
        private GBBusiness.BusinessAccess _businessAccess = new BusinessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Master != null)
            {
                Label l1 = (Label)Master.FindControl("LV1");
                l1.Text = "User Management";
                Label l2 = (Label)Master.FindControl("LV2");
                l2.Text = "User";
                Label l3 = (Label)Master.FindControl("LV3");
                l3.Text = "Home";
                HtmlAnchor CurrentMenu;
               // CurrentMenu = (HtmlAnchor)Master.FindControl("Tab6");
               // CurrentMenu.Attributes.Add("class", "active");

            }

            if (!IsPostBack)
            {
                if (Session["ID"] != null)
                {
                 //   string
                    DataRow dr =
                        _businessAccess.GetUser()
                            .AsEnumerable().FirstOrDefault(row => row["ID"].ToString() == Session["ID"].ToString());
                    if (dr != null)
                    {
                        btnSave.Enabled = true;
                        btnPassword.Enabled = true;
                        txtUserName.Text = dr["Username"].ToString();
                        txtName.Text = dr["Name"].ToString();
                        txtEmail.Text = dr["Email"].ToString();
                        chkAdmin.Checked = bool.Parse(dr["IsAdmin"].ToString());
                    }

                  
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
            _businessAccess.EditUser(txtUserName.Text, txtName.Text, txtEmail.Text, chkAdmin.Checked, Session["ID"].ToString());
            lblError.Text = "*Saved";
            _businessAccess.InsertLog(Session["ID"].ToString(),
System.Reflection.MethodBase.GetCurrentMethod().Name, this.Page.ToString(), DateTime.Now);
            }
            catch (Exception ex)
            {
                lblError.Text = "*Username already exist";
            }
        }

        protected void btnPassword_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text != "")
            {
                _businessAccess.ChangePassword(txtPassword.Text, Session["ID"].ToString());
                _businessAccess.InsertLog(Session["ID"].ToString(),
  System.Reflection.MethodBase.GetCurrentMethod().Name, this.Page.ToString(), DateTime.Now);
                lblError.Text = "*Saved";
            }
            else
            {
                lblError.Text = "Password field empty";
            }
        }

    
    }
}