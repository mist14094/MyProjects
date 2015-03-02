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
    public partial class Default : System.Web.UI.Page
    {
        private GBBusiness.BusinessAccess _businessAccess = new BusinessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Master != null)
            {
                Label l1 = (Label)Master.FindControl("LV1");
                l1.Text = "Holding Tank Status";
                Label l2 = (Label)Master.FindControl("LV2");
                l2.Text = "Tank Status";
                Label l3 = (Label)Master.FindControl("LV3");
                l3.Text = "Home";
                HtmlAnchor CurrentMenu;
                CurrentMenu = (HtmlAnchor)Master.FindControl("Tab1");
                CurrentMenu.Attributes.Add("class","active");
            }
            if (!IsPostBack)
            {
                BindData();
            }
            
        }

        public void BindData()
        {
            lblError.Text = "";
            DataTable dt = _businessAccess.GetSetup();
            lblRTankSize.Text = dt.Rows[0]["regularCapacity"].ToString();
            lblRGasTotal.Text = dt.Rows[0]["regularStored"].ToString();
            lblRTotal.Text = dt.Rows[0]["ResultRegular"].ToString()+" %";

            lblSTankSize.Text = dt.Rows[0]["superCapacity"].ToString();
            lblSGasTotal.Text = dt.Rows[0]["superStored"].ToString();
            lblSTotal.Text = dt.Rows[0]["ResultSuper"].ToString() + " %";

            lblETankSize.Text = dt.Rows[0]["ethanolCapacity"].ToString();
            lblEGasTotal.Text = dt.Rows[0]["ethanolStored"].ToString();
            lblETotal.Text = dt.Rows[0]["ResultEthanol"].ToString() + " %";

            txtRGasAdj.Text = lblRGasTotal.Text;
            txtSGasAdj.Text = lblSGasTotal.Text;
            txtEAdj.Text = lblEGasTotal.Text;


            txtRGasSetup.Text = lblRTankSize.Text;
            txtSGasSetup.Text = lblSTankSize.Text;
            txtEthSetup.Text  = lblETankSize.Text;
        }
        protected void btnAdjustment_Click(object sender, EventArgs e)
        {
           

        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            try
            {
                     _businessAccess.UpdateStoredTank(txtRGasAdj.Text, txtSGasAdj.Text, txtEAdj.Text);
                    BindData();
                    _businessAccess.InsertLog(Session["ID"].ToString(),
                      System.Reflection.MethodBase.GetCurrentMethod().Name, this.Page.ToString(), DateTime.Now);
                
            }
            catch (Exception)
            {
                lblError.Text = "*Save unsuccessful";
                //    throw;
            }
           
        }

        protected void btnEditHolding_Click(object sender, EventArgs e)
        {
            try
            {
            _businessAccess.UpdateTankSize(txtRGasSetup.Text, txtSGasSetup.Text, txtEthSetup.Text);
            BindData();
            _businessAccess.InsertLog(Session["ID"].ToString(),
              System.Reflection.MethodBase.GetCurrentMethod().Name, this.Page.ToString(), DateTime.Now);
                
            }
            catch (Exception)
            {
                lblError.Text = "*Save unsuccessful";
             //   throw;
            }
           
        }
    }
}