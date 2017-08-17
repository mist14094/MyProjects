using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace SJDealStore.Pages.Items
{
    public partial class DeleteDoc : System.Web.UI.Page
    {
      


        private DALayer _layer = new DALayer();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlFileSelect.DataSource = _layer.SelectImportMaster();
                ddlFileSelect.DataTextField = "FileName";
                ddlFileSelect.DataValueField = "Sno";
                ddlFileSelect.DataBind();
            
             

                IntitilaizeValues();
                
             
            }
        }

        private void IntitilaizeValues()
        {
            
        }

      

        public void SearchStringFunc()
        {
            if (TextBox1.Text == "DELETE")
            {
                _layer.SJDeals_DeleteReport(ddlFileSelect.SelectedValue);
                TextBox1.Text = "";
                lblreport.Text = "File Deleted on " + DateTime.Now.ToString("D");
                ddlFileSelect.DataSource = _layer.SelectImportMaster();
                ddlFileSelect.DataTextField = "FileName";
                ddlFileSelect.DataValueField = "Sno";
                ddlFileSelect.DataBind();
                IntitilaizeValues();
            }
            else
            {
                lblreport.Text = "TYPE DELETE";
            }
          
            
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchStringFunc();
        }

       
     
    }
}