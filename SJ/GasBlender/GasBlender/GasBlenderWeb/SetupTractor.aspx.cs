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
    public partial class SetupTractor : System.Web.UI.Page
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

            GridView1.DataSource = _businessAccess.GetTractor();
            GridView1.DataBind();
        }

       
        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindData();
        }

        protected void Add(object sender, EventArgs e)
        {
            txtaddName.Text = string.Empty;
            
            popupAdd.Show();
        }

        protected void SaveAdd(object sender, EventArgs e)
        {
              try
            {
                _businessAccess.InsertTractor(txtaddName.Text);
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

                lblTractorID.Text = row.Cells[0].Text;
                txtName.Text = HttpUtility.HtmlDecode(row.Cells[1].Text);
                lblEditError.Text = "";
                popupEdit.Show();
            }
        }

        protected void SaveEdit(object sender, EventArgs e)
        {
            try
            {
               _businessAccess.UpdateTractor(txtName.Text, lblTractorID.Text);
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
                _businessAccess.RemoveTractor(lblRemove.Text);
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