using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GBBusiness;
using System.Net;

namespace GasBlenderWeb
{
    public partial class SetupLocation : System.Web.UI.Page
    {
        private GBBusiness.BusinessAccess _businessAccess = new BusinessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindData();
                Session["Option"] = "";
            }
        }

        private void BindData()
        {

            GridView1.DataSource = _businessAccess.GetLocation();
            GridView1.DataBind();
        }

       
        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindData();
        }

        protected void Add(object sender, EventArgs e)
        {
            txtaddLocationName.Text = string.Empty;
            txtaddAddress.Text = string.Empty;
            txtaddState.Text = string.Empty;
            txtaddCity.Text = string.Empty;
            txtaddZip.Text = string.Empty;
            popupAdd.Show();
        }

        protected void SaveAdd(object sender, EventArgs e)
        {
              try
            {
                _businessAccess.InsertLocation(txtaddLocationName.Text, txtaddAddress.Text,   txtaddCity.Text, txtaddState.Text,
                  txtaddZip.Text);
                BindData();
            }
            catch (Exception)
            {
                //lblAddError.Text = "* Check Values";
                 throw;
            }
        }


        protected void Edit(object sender, EventArgs e)
        {
            using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
            {

                lblLocationID.Text = row.Cells[0].Text;
                txtLocationName.Text = HttpUtility.HtmlDecode(row.Cells[1].Text);
                txtAddress.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
                txtCity.Text = HttpUtility.HtmlDecode(row.Cells[3].Text); 
                txtState.Text = HttpUtility.HtmlDecode(row.Cells[4].Text);
                txtZip.Text = HttpUtility.HtmlDecode(row.Cells[5].Text);
                lblEditError.Text = "";
                popupEdit.Show();
            }
        }

        protected void SaveEdit(object sender, EventArgs e)
        {
            try
            {
                _businessAccess.UpdateLocation(txtLocationName.Text, txtAddress.Text, txtCity.Text,
                    txtState.Text, txtZip.Text, lblLocationID.Text);
                BindData();
            }
            catch (Exception)
            {
                lblEditError.Text = "* Check Values";
                throw;
            }
        }


        protected void Remove(object sender, EventArgs e)
        {
            lblRemove.Text = "";
            using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
            {
                lblRemove.Text = row.Cells[0].Text;
                popupDelete.Show();
            }
        }

        protected void SaveRemove(object sender, EventArgs e)
        {
            try
            {
                _businessAccess.RemoveLocation(lblRemove.Text);
                BindData();
            }
            catch (Exception)
            {
                lblEditError.Text = "* Check Values";
                throw;
            }
        } 
        protected void Cancel(object sender, EventArgs e)
        {
            try
            {
               popupAdd.Hide();
                popupDelete.Hide();
                popupEdit.Hide();
            }
            catch (Exception)
            {
                lblEditError.Text = "* Check Values";
                throw;
            }
        }
    }

}