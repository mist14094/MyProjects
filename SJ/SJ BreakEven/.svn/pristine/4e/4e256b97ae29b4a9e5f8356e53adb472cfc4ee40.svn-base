using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NLog;
using BreakEvenBAL;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.HtmlControls;
using System.Web.Caching;
using System.Data;

namespace BEPWeb
{
    public partial class SalesDetails : System.Web.UI.Page
    {
        private NLog.Logger nlog = LogManager.GetCurrentClassLogger();
        private static DataTable dtSaleDetail;
        private static BEP_Detail selectedBEP;
        private static int storeID;

        protected void Page_Load(object sender, EventArgs e)
        {
            nlog.Trace("SalesDetails:Page_Load::Entering");
            try
            {
                if (!Page.IsPostBack)
                {
                    string strPID = Request.QueryString["PID"];
                    string strStoreID = "";
                    if (Request.QueryString["[StoreID]"] != "")
                    {
                        strStoreID = Request.QueryString["StoreID"];
                    }

                    dtSaleDetail = null;
                    try
                    {
                        storeID = int.Parse(strStoreID);
                    }
                    catch { storeID = 0; }

                    selectedBEP = CacheWrapper.GetInstance().GetBEPItem(long.Parse(strPID));

                    FillData(selectedBEP, storeID);

                }

            }
            catch (Exception ex)
            {
                nlog.ErrorException("SalesDetails:Page_Load::Error", ex);
            }
            finally
            {
                nlog.Trace("SalesDetails:Page_Load::Leaving");
            }
        }


        private void FillData(BEP_Detail selectedBEP, int iStoreID)
        {
            try
            {
                nlog.Trace("SalesDetails:FillData::Leaving");

                lblDesc.Text = selectedBEP.Description;
                lblQtyRcvd.Text = selectedBEP.TotalRCVD.ToString();
                lblQtySold.Text = selectedBEP.TotalSold.ToString();
                lblSKU.Text = selectedBEP.SKU;
                lblStkCode.Text = selectedBEP.StockCode;
                lblTotCOGS.Text = selectedBEP.TotalCOGS.ToString("C");
                lblTotSale.Text = selectedBEP.TotalSales.ToString("C");
                lblUPC.Text = selectedBEP.UPC;
                lblVendor.Text = selectedBEP.Vendor;
                lblMargin.Text = selectedBEP.ProfitMargin.ToString("C");

                lblAvgCOGS.Text = selectedBEP.AvgCOGS.ToString("C");
                lblAvgSellPrice.Text = selectedBEP.AvgSalePrice.ToString("C");

                if (selectedBEP.ProfitMargin > 0)
                {
                    lblStatus.Text = "Profit";
                    lblStatus.BackColor = System.Drawing.Color.Green;
                    lblStatus.ForeColor = System.Drawing.Color.White;
                    tdStatus.BgColor = "GREEN";
                }
                else
                {
                    lblStatus.Text = "Loss";
                    lblStatus.BackColor = System.Drawing.Color.Red;
                    lblStatus.ForeColor = System.Drawing.Color.White;
                    tdStatus.BgColor = "RED";
                }

                SalesDetail objSaleDetail = new SalesDetail();
                DataSet ds;
                if (iStoreID != 0)
                {
                    ds = objSaleDetail.GetSalesDetail(selectedBEP.PC_ID, iStoreID);
                }
                else
                    ds = objSaleDetail.GetSalesDetail(selectedBEP.PC_ID);

                dtSaleDetail = ds.Tables[0];
                gv_SalesDetail.DataSource = dtSaleDetail.DefaultView;
                gv_SalesDetail.DataBind();


               // lkSalesDetail.PostBackUrl = "SalesDetails.aspx?PID=" + selectedBEP.PC_ID.ToString();
                lkSalesTrend.PostBackUrl = "SalesTrend.aspx?PID=" + selectedBEP.PC_ID.ToString();
                lkPODetails.PostBackUrl = "PODetails.aspx?PID=" + selectedBEP.PC_ID.ToString();
                lkBEPDetails.PostBackUrl = "BEPDetails.aspx?PID=" + selectedBEP.PC_ID.ToString();
               
            }
            catch (Exception ex)
            {
                nlog.ErrorException("SalesDetails:FillData::Error", ex);
            }
            finally
            {
                nlog.Trace("SalesDetails:FillData::Leaving");
            }
        }

        public SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                    ViewState["sortDirection"] = SortDirection.Ascending;

                return (SortDirection)ViewState["sortDirection"];
            }
            set { ViewState["sortDirection"] = value; }
        }


        protected void gridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            nlog.Trace("PODetails:gridView_Sorting::Entering");
            try
            {
                ApplySorting(e.SortExpression);
            }
            catch (Exception ex)
            {
                nlog.ErrorException("PODetails:gridView_Sorting::Error", ex);
            }
            finally
            {
                nlog.Trace("PODetails:gridView_Sorting::Leaving");
            }
        }

        private void ApplySorting(string SortExp)
        {
            nlog.Trace("PODetails:gridView_Sorting::Entering");
            try
            { //re-run the query, use linq to sort the objects based on the arg.
                //perform a search using the constraints given 
                //you could have this saved in Session, rather than requerying your datastore



                if (GridViewSortDirection == SortDirection.Ascending)
                {
                    DataView dv = dtSaleDetail.DefaultView;
                    dv.Sort = SortExp + " ASC";

                    gv_SalesDetail.DataSource = dv;
                    GridViewSortDirection = SortDirection.Descending;
                }
                else
                {
                    DataView dv = dtSaleDetail.DefaultView;
                    dv.Sort = SortExp + " DESC";
                    gv_SalesDetail.DataSource = dv;
                    GridViewSortDirection = SortDirection.Ascending;
                };


                gv_SalesDetail.DataBind();

            }
            catch (Exception ex)
            {
                nlog.ErrorException("PODetails:gridView_Sorting::Error", ex);
            }
            finally
            {
                nlog.Trace("PODetails:gridView_Sorting::Leaving");
            }
        }

        protected void GridViewHierachical_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            nlog.Trace("PODetails:GridViewHierachical_PageIndexChanging::Entering");
            try
            {
                gv_SalesDetail.PageIndex = e.NewPageIndex;
                FillData(selectedBEP, storeID);
            }
            catch (Exception ex)
            {
                nlog.ErrorException("PODetails:GridViewHierachical_PageIndexChanging::Error", ex);
            }
            finally
            {
                nlog.Trace("PODetails:GridViewHierachical_PageIndexChanging::Leaving");
            }
        }
    }
}