using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SimplifiedPOBusiness;
using SimplifiedPOConstants;

namespace SimplifiedPOWeb
{
    public partial class NewPO : System.Web.UI.Page
    {
        SPOBL _spobl= new SPOBL();
        private static User user = new User();
       private static  List<User> Userlist = new List<User>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                user = (User)Session["Login"];
                Userlist = _spobl.GetAllUsers();
                InitializePage();
            }
         
        }

        private void InitializePage()
        {
            lblUserName.Text = user.UserName;

            var displayUsers = Userlist.Select(u => new { UserID = u.UserID, DisplayText = u.LastName.ToString() + "," + u.FirstName });
            ddlBuyerPostFor.DataSource = displayUsers;
            ddlBuyerPostFor.DataTextField = "DisplayText";
            ddlBuyerPostFor.DataValueField = "UserID";
            ddlBuyerPostFor.DataBind();
            ddlBuyerPostFor.Items.Insert(0, new ListItem("- New Buyer -",""));
            var firstOrDefault = Userlist.FirstOrDefault(user1 => user1.UserName == user.UserName);
            if (firstOrDefault != null)
                ddlBuyerPostFor.SelectedValue = firstOrDefault.UserID.ToString();

            try
            {
                var selectedUser = Userlist.FirstOrDefault(user1 => user1.UserID == int.Parse(ddlBuyerPostFor.SelectedValue));
                if (selectedUser != null)
                {
                    txtBuyerName.Text = selectedUser.FirstName + " " + selectedUser.LastName;
                    txtBuyerAddress.Text = selectedUser.Address + selectedUser.FirstName + " " +
                                           selectedUser.LastName;
                }
                txtBuyerContact.Text = "";
            }
            catch (Exception)
            {
                txtBuyerName.Text = "";
                txtBuyerAddress.Text = "";
                txtBuyerContact.Text = "";

            }

            ddlEntity.DataSource = _spobl.GetAllEntities();
            ddlEntity.DataValueField = "EntityID";
            ddlEntity.DataTextField = "EntityName";
            ddlEntity.DataBind();
            ddlEntity.SelectedValue = "D";

            ddlSupplier.DataSource = _spobl.GetSuppliers(ddlEntity.SelectedValue);
            ddlSupplier.DataValueField = "Supplier";
            ddlSupplier.DataTextField = "SupplierName";
            ddlSupplier.DataBind();
            ddlSupplier.Items.Insert(0, new ListItem("-New Supplier-", "-New Supplier-"));

            if (ddlSupplier.Text != "-New Supplier-")
            {
                DataTable dt = _spobl.GetSupplierAddress(ddlEntity.SelectedValue, ddlSupplier.SelectedValue);
                if (dt.Rows.Count > 0)
                {
                    txtSupplierName.Text = dt.Rows[0]["SupplierName"].ToString();
                    txtSupplierAddress.Text = dt.Rows[0]["SupAddr1"].ToString()
                                              + Environment.NewLine + dt.Rows[0]["SupAddr2"].ToString() +
                                              Environment.NewLine +
                                              dt.Rows[0]["SupAddr3"].ToString();
                    txtSupplierContact.Text = "";
                }
            }
            else
            {
                txtSupplierName.Text = "";
                txtSupplierAddress.Text = "";
                txtSupplierContact.Text = "";
            }
            ddlPriority.Items.Add(new ListItem("Low","1"));
            ddlPriority.Items.Add(new ListItem("Normal","2"));
            ddlPriority.Items.Add(new ListItem("High", "3"));
            ddlPriority.Items.Add(new ListItem("Urgent", "4"));

        }
        protected void btnNext_Click(object sender, EventArgs e)
        {

           string tempPoNumber=  _spobl.CreateTempPO ( user.UserID, lblUserName.Text, ddlBuyerPostFor.SelectedValue, txtBuyerName.Text, txtBuyerAddress.Text,
                txtBuyerContact.Text,int.Parse(ddlPriority.SelectedValue), ddlEntity.SelectedValue, ddlSupplier.SelectedValue,
                txtSupplierName.Text, txtSupplierAddress.Text, txtSupplierContact.Text,txtNotes.Text);
            Response.Redirect("POLineItems.aspx?sno="+tempPoNumber);
        }

        protected void ddlBuyerPostFor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var selectedUser = Userlist.FirstOrDefault(user1 => user1.UserID ==int.Parse( ddlBuyerPostFor.SelectedValue));
                if (selectedUser != null)
                {
                    txtBuyerName.Text = selectedUser.FirstName + " " + selectedUser.LastName;
                    txtBuyerAddress.Text = selectedUser.Address +  selectedUser.FirstName + " " +
                                           selectedUser.LastName;
                }
                txtBuyerContact.Text = "";
            }
            catch (Exception)
            {
                txtBuyerName.Text = "";
                txtBuyerAddress.Text = "";
                txtBuyerContact.Text = "";

            }
          
        }

        protected void ddlEntity_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSupplier.DataSource = _spobl.GetSuppliers(ddlEntity.SelectedValue);
            ddlSupplier.DataValueField = "Supplier";
            ddlSupplier.DataTextField = "SupplierName";
            ddlSupplier.DataBind();
            ddlSupplier.Items.Insert(0, new ListItem("-New Supplier-", "-New Supplier-")); 
            txtSupplierName.Text = "";
            txtSupplierAddress.Text = "";
            txtSupplierContact.Text = "";
        }

        protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSupplier.Text != "-New Supplier-")
            {
                DataTable dt = _spobl.GetSupplierAddress(ddlEntity.SelectedValue, ddlSupplier.SelectedValue);
                if (dt.Rows.Count > 0)
                {
                    txtSupplierName.Text = dt.Rows[0]["SupplierName"].ToString();
                    txtSupplierAddress.Text = dt.Rows[0]["SupAddr1"].ToString()
                                              + Environment.NewLine + dt.Rows[0]["SupAddr2"].ToString() +
                                              Environment.NewLine +
                                              dt.Rows[0]["SupAddr3"].ToString();
                    txtSupplierContact.Text = "";
                }
            }
            else
            {
                txtSupplierName.Text    = "";
                txtSupplierAddress.Text = "";
                txtSupplierContact.Text = "";
            }
        }

       
    }
}