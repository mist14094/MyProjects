using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using GBBusiness;

namespace GasBlenderWeb
{
    public partial class SetupTrailer : System.Web.UI.Page
    {
        private GBBusiness.BusinessAccess _businessAccess = new BusinessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindData();
                Session["Option"] = "";

                if (Master != null)
                {
                    Label l1 = (Label)Master.FindControl("LV1");
                    l1.Text = "Setup - Trailer";
                    Label l2 = (Label)Master.FindControl("LV2");
                    l2.Text = "Setup";
                    Label l3 = (Label)Master.FindControl("LV3");
                    l3.Text = "Home";
                    HtmlAnchor CurrentMenu;
                    CurrentMenu = (HtmlAnchor)Master.FindControl("Tab4");
                    CurrentMenu.Attributes.Add("class", "active");
                }
            }
        }

        private void BindData()
        {

            GridView1.DataSource = _businessAccess.GetActiveTrailer();
            GridView1.DataBind();
        }

       
        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindData();
        }

        protected void Add(object sender, EventArgs e)
        {
            txtaddTrailerNumber.Text = string.Empty;
            txtaddCompartment1.Text = "0.0";
            txtaddCompartment2.Text = "0.0";
            txtaddCompartment3.Text = "0.0";
            txtaddCompartment4.Text = "0.0";
            txtaddCompartment5.Text = "0.0"; 
            lblAddError.Text = "";
            popupAdd.Show();
        }

        protected void SaveAdd(object sender, EventArgs e)
        {
              try
            {
                _businessAccess.AddTrailer(txtaddTrailerNumber.Text, txtaddCompartment1.Text, txtaddCompartment2.Text,
                    txtaddCompartment3.Text, txtaddCompartment4.Text, txtaddCompartment5.Text);
                _businessAccess.InsertLog(Session["ID"].ToString(),
System.Reflection.MethodBase.GetCurrentMethod().Name, this.Page.ToString(), DateTime.Now);
                BindData();
            }
            catch (Exception)
            {
                lblAddError.Text = "* Check Values";
                 throw;
            }
        }


        protected void Edit(object sender, EventArgs e)
        {
            using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
            {

                lblValueTrailerID.Text = row.Cells[0].Text;
                txtTrailerNumber.Text = HttpUtility.HtmlDecode(row.Cells[1].Text);
                txtcompartment1Size.Text = row.Cells[2].Text;
                txtcompartment2Size.Text = row.Cells[3].Text;
                txtcompartment3Size.Text = row.Cells[4].Text;
                txtcompartment4Size.Text = row.Cells[5].Text;
                txtcompartment5Size.Text = row.Cells[6].Text;
                lblEditError.Text = "";
                //txtCustomerID.ReadOnly = true;
                //txtCustomerID.Text = row.Cells[0].Text;
                //txtContactName.Text = row.Cells[1].Text;
                //txtCompany.Text = row.Cells[2].Text;
                popupEdit.Show();
            }
        }

        protected void SaveEdit(object sender, EventArgs e)
        {
            try
            {
                _businessAccess.UpdateTrailer(txtTrailerNumber.Text, txtcompartment1Size.Text, txtcompartment2Size.Text,
                    txtcompartment3Size.Text, txtcompartment4Size.Text, txtcompartment5Size.Text, lblValueTrailerID.Text);
                _businessAccess.InsertLog(Session["ID"].ToString(),
  System.Reflection.MethodBase.GetCurrentMethod().Name, this.Page.ToString(), DateTime.Now);
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
                _businessAccess.RemoveTrailer(lblRemove.Text);
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