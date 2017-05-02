using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdBsnsLayer;

namespace AdWeb
{
    public partial class AddUserWithWaiver : System.Web.UI.Page
    {
        AdBsnsLayer.User User= new User();
        AdBsnsLayer.ActivitiesMenu Menu = new ActivitiesMenu();

        protected void Page_Load(object sender, EventArgs e)
        {
            var user = Session["User"];

            if (!IsPostBack)
            {
                if (user == null)
                {
                    Response.Redirect("Login.aspx");
                }
            }

            if (!IsPostBack)
            {
                rblMenuType.DataSource = Menu.GetAllActivitiesMenu();
                rblMenuType.DataValueField = "Sno";
                rblMenuType.DataTextField = "PlanName";
                rblMenuType.DataBind();

                var v = Request.QueryString["Sno"];
                if (v == null)
                {
                    Response.Redirect("~/SearchWaiver.aspx");
                }
                else
                {
                   var UserList= User.GetUserDetailsWithWaiver(v.ToString());
                    if (UserList.Count > 0)
                    {
                        txtFirstName.Text = UserList[0].FirstName;
                        txtLastName.Text= UserList[0].LastName;
                        txtEmailId.Text = UserList[0].EmailId;
                        if (UserList[0].DateOfBirth != null)
                        {
                            txtDob.Text = DateTime.Parse(UserList[0].DateOfBirth.ToString()).ToString("MM/dd/yyyy");
                        }
                        

                    }

                    
                }

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DateTime dt;
            try
            {
                dt = DateTime.Parse(txtDob.Text);
            }
            catch (Exception exception)
            {
               dt=DateTime.Now;
            }
          
          var userCreated= User.InsertUserWithWaiver(txtFirstName.Text, txtLastName.Text, txtAddress.Text, txtCity.Text,
                txtState.Text, txtCountry.Text, txtZipCode.Text, txtMobile.Text, txtEmailId.Text,dt
                , txtTagNumber.Text, ((LoginUser) Session["User"]).Sno, int.Parse(rblMenuType.SelectedItem.Value),rblMenuType.SelectedItem.Text, Request.QueryString["Sno"].ToString());
            if (userCreated == null)
            {
                lblModalTitle.Text = "Tag Already Associated";
                lblModalBody.Text = "This band is already in the system. Try with other tag";
                lblModalBody.ForeColor = Color.Red;
            }
            else
            {
                lblModalTitle.Text = "Tag Associated";
                lblModalBody.Text = "Enjoy your Adventure - ID# " + userCreated.ToString();
                lblModalBody.ForeColor = Color.DarkGreen;
                Response.Redirect(string.Format("AssociateActv.aspx?TagNumber={0}", txtTagNumber.Text));
            }
         
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
            upModal.Update();


        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
           Response.Redirect("AddUserWithWaiver.aspx");
        }

        protected void btnTop20Waiver_Click(object sender, EventArgs e)
        {

        }
    }
}