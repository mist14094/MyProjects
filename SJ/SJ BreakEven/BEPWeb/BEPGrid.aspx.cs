using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BreakEvenBAL;
using System.Linq.Expressions;
using NLog;
using System.Web.Caching;
using System.Web.UI.HtmlControls;


namespace BEPWeb
{
    public partial class BEPGrid : System.Web.UI.Page
    {

        private NLog.Logger nlog = LogManager.GetCurrentClassLogger();
        private static List<BEP_Detail> lstBEP, lstBEP_Profit, lstBEP_Loss, lstBEP_Even, lstQOH;


        protected void Page_Load(object sender, EventArgs e)
        {
            nlog.Trace("BEPGrid:Page_Load::Entering");
            try
            {
                //if (!Page.IsPostBack)
                {
                    int qtyOnHand = int.Parse(txtQtyToBeBrokeEven.Text);
                    BindGrid(txtUPC.Text, txtVendor.Text, txtDesc.Text, dLstPercentage.SelectedValue, qtyOnHand);
                }
            }
            catch (Exception ex)
            {
                nlog.ErrorException("BEPGrid:Page_Load::Error", ex);
            }
            finally
            {
                nlog.Trace("BEPGrid:Page_Load::Leaving");
            }
        }

        private void BindGrid(string upc, string vendor, string desc, string precentageCriteria, int qtyOnHand)
        {
            nlog.Trace("BEPGrid:BindGrid::Entering");
            try
            {
                string SortExp = GridSortExpression;
                if (SortExp == "")
                { GridSortExpression = "UPC"; SortExp = "UPC"; }

                var param = Expression.Parameter(typeof(BEP_Detail), SortExp);
                var sortExpression = Expression.Lambda<Func<BEP_Detail, object>>(Expression.Convert(Expression.Property(param, SortExp), typeof(object)), param);


                CacheWrapper obj = CacheWrapper.GetInstance();
                lstBEP = obj.GetAllBEPItems(upc, vendor, desc, precentageCriteria, qtyOnHand);
                GridViewHierachical.DataSource = lstBEP;
                if (SortExp != "")
                {
                    if (GridViewSortDirection == SortDirection.Ascending)
                    {
                        GridViewHierachical.DataSource = (lstBEP.AsQueryable<BEP_Detail>().OrderBy(sortExpression)).ToList<BEP_Detail>();
                        //GridViewSortDirection = SortDirection.Descending;
                    }
                    else
                    {
                        GridViewHierachical.DataSource = (lstBEP.AsQueryable<BEP_Detail>().OrderByDescending(sortExpression)).ToList<BEP_Detail>();
                        //GridViewSortDirection = SortDirection.Ascending;
                    }
                }
                GridViewHierachical.DataBind();

                lblTotalItems.Text = lstBEP.Count.ToString();

                lstBEP_Profit = obj.GetAllProfitBEPItems(upc, vendor, desc, precentageCriteria, qtyOnHand);
                GV_Profit.DataSource = lstBEP_Profit;
                if (SortExp != "")
                {
                    if (GridViewSortDirection == SortDirection.Ascending)
                    {
                        GV_Profit.DataSource = (lstBEP_Profit.AsQueryable<BEP_Detail>().OrderBy(sortExpression)).ToList<BEP_Detail>();
                        //GridViewSortDirection = SortDirection.Descending;
                    }
                    else
                    {
                        GV_Profit.DataSource = (lstBEP_Profit.AsQueryable<BEP_Detail>().OrderByDescending(sortExpression)).ToList<BEP_Detail>();
                        //GridViewSortDirection = SortDirection.Ascending;
                    }
                }

                GV_Profit.DataBind();

                lblProfitItemCnt.Text = lstBEP_Profit.Count.ToString();

                lstBEP_Loss = obj.GetAllLossBEPItems(upc, vendor, desc, precentageCriteria, qtyOnHand);
                GV_Loss.DataSource = lstBEP_Loss;
                if (SortExp != "")
                {
                    if (GridViewSortDirection == SortDirection.Ascending)
                    {
                        GV_Loss.DataSource = (lstBEP_Loss.AsQueryable<BEP_Detail>().OrderBy(sortExpression)).ToList<BEP_Detail>();
                        //GridViewSortDirection = SortDirection.Descending;
                    }
                    else
                    {
                        GV_Profit.DataSource = (lstBEP_Loss.AsQueryable<BEP_Detail>().OrderByDescending(sortExpression)).ToList<BEP_Detail>();
                        //GV_Loss = SortDirection.Ascending;
                    }
                }
                GV_Loss.DataBind();

                lstBEP_Even = obj.GetAllEvenBEPItems(upc, vendor, desc, precentageCriteria, qtyOnHand);
                gdViewEvenItems.DataSource = lstBEP_Even;
                if (SortExp != "")
                {
                    if (GridViewSortDirection == SortDirection.Ascending)
                    {
                        gdViewEvenItems.DataSource = (lstBEP_Even.AsQueryable<BEP_Detail>().OrderBy(sortExpression)).ToList<BEP_Detail>();
                        //GridViewSortDirection = SortDirection.Descending;
                    }
                    else
                    {
                        gdViewEvenItems.DataSource = (lstBEP_Even.AsQueryable<BEP_Detail>().OrderByDescending(sortExpression)).ToList<BEP_Detail>();
                        //GV_Loss = SortDirection.Ascending;
                    }
                }
                gdViewEvenItems.DataBind();

                lstQOH = obj.GetAllQOHmorethnZeroItems(upc, vendor, desc, precentageCriteria, qtyOnHand);
                gdViewQOH.DataSource = lstQOH;
                if (SortExp != "")
                {
                    if (GridViewSortDirection == SortDirection.Ascending)
                    {
                        gdViewQOH.DataSource = (lstQOH.AsQueryable<BEP_Detail>().OrderBy(sortExpression)).ToList<BEP_Detail>();
                        //GridViewSortDirection = SortDirection.Descending;
                    }
                    else
                    {
                        gdViewQOH.DataSource = (lstQOH.AsQueryable<BEP_Detail>().OrderByDescending(sortExpression)).ToList<BEP_Detail>();
                        //GV_Loss = SortDirection.Ascending;
                    }
                }
                gdViewQOH.DataBind();


                lblLossItemCnt.Text = lstBEP_Loss.Count.ToString();
                lblEvenItems.Text = lstBEP_Even.Count.ToString();
                lblNotEvenItems.Text = lstQOH.Count.ToString();

                double totalCost = (from s in lstBEP select s.ProfitMargin).Sum();
                lblProfitMargin.Text = totalCost.ToString("C");


                if (totalCost < 0)
                    lblProfitMargin.BackColor = System.Drawing.Color.Red;
                else
                    lblProfitMargin.BackColor = System.Drawing.Color.Green;

              


                //GridViewHierachical 
            }
            catch (Exception ex)
            {
                nlog.ErrorException("BEPGrid:BindGrid::Error", ex);
            }
            finally
            {
                nlog.Trace("BEPGrid:BindGrid::Leaving");
            }

        }

        protected void GridViewHierachical_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            nlog.Trace("BEPGrid:GridViewHierachical_PageIndexChanging::Entering");
            try
            {
                GridViewHierachical.PageIndex = e.NewPageIndex;
                int qtyOnHand = int.Parse(txtQtyToBeBrokeEven.Text);
                BindGrid(txtUPC.Text, txtVendor.Text, txtDesc.Text, dLstPercentage.SelectedValue, qtyOnHand);
            }
            catch (Exception ex)
            {
                nlog.ErrorException("BEPGrid:GridViewHierachical_PageIndexChanging::Error", ex);
            }
            finally
            {
                nlog.Trace("BEPGrid:GridViewHierachical_PageIndexChanging::Leaving");
            }
        }

        protected void GridProfit_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            nlog.Trace("BEPGrid:GridProfit_PageIndexChanging::Entering");
            try
            {
                GV_Profit.PageIndex = e.NewPageIndex;
                int qtyOnHand = int.Parse(txtQtyToBeBrokeEven.Text);
                BindGrid(txtUPC.Text, txtVendor.Text, txtDesc.Text, dLstPercentage.SelectedValue, qtyOnHand);
            }
            catch (Exception ex)
            {
                nlog.ErrorException("BEPGrid:GridProfit_PageIndexChanging::Error", ex);
            }
            finally
            {
                nlog.Trace("BEPGrid:GridProfit_PageIndexChanging::Leaving");
            }
        }

        protected void GridLoss_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            nlog.Trace("BEPGrid:GridLoss_PageIndexChanging::Entering");
            try
            {
                GV_Loss.PageIndex = e.NewPageIndex;
                int qtyOnHand = int.Parse(txtQtyToBeBrokeEven.Text);
                BindGrid(txtUPC.Text, txtVendor.Text, txtDesc.Text, dLstPercentage.SelectedValue, qtyOnHand);
            }
            catch (Exception ex)
            {
                nlog.ErrorException("BEPGrid:GridLoss_PageIndexChanging::Error", ex);
            }
            finally
            {
                nlog.Trace("BEPGrid:GridLoss_PageIndexChanging::Leaving");
            }
        }

        protected void GridEven_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            nlog.Trace("BEPGrid:GridLoss_PageIndexChanging::Entering");
            try
            {
                gdViewEvenItems.PageIndex = e.NewPageIndex;
                int qtyOnHand = int.Parse(txtQtyToBeBrokeEven.Text);
                BindGrid(txtUPC.Text, txtVendor.Text, txtDesc.Text, dLstPercentage.SelectedValue, qtyOnHand);
            }
            catch (Exception ex)
            {
                nlog.ErrorException("BEPGrid:GridLoss_PageIndexChanging::Error", ex);
            }
            finally
            {
                nlog.Trace("BEPGrid:GridLoss_PageIndexChanging::Leaving");
            }
        }

        protected void GridQOH_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            nlog.Trace("BEPGrid:GridLoss_PageIndexChanging::Entering");
            try
            {
                gdViewQOH.PageIndex = e.NewPageIndex;
                int qtyOnHand = int.Parse(txtQtyToBeBrokeEven.Text);
                BindGrid(txtUPC.Text, txtVendor.Text, txtDesc.Text, dLstPercentage.SelectedValue, qtyOnHand);
            }
            catch (Exception ex)
            {
                nlog.ErrorException("BEPGrid:GridLoss_PageIndexChanging::Error", ex);
            }
            finally
            {
                nlog.Trace("BEPGrid:GridLoss_PageIndexChanging::Leaving");
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


        public string GridSortExpression
        {
            get
            {
                if (ViewState["sortExpression"] == null)
                    return "";
                else
                    return ViewState["sortExpression"].ToString();
            }
            set { ViewState["sortExpression"] = value; }
        }

        protected void gridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            nlog.Trace("BEPGrid:gridView_Sorting::Entering");
            try
            {
                GridSortExpression = e.SortExpression;
                if (GridViewSortDirection == SortDirection.Ascending)
                    GridViewSortDirection = SortDirection.Descending;
                else
                    GridViewSortDirection = SortDirection.Ascending;
                int qtyOnHand = int.Parse(txtQtyToBeBrokeEven.Text);

                BindGrid(txtUPC.Text, txtVendor.Text, txtDesc.Text, dLstPercentage.SelectedValue, qtyOnHand);
            }
            catch (Exception ex)
            {
                nlog.ErrorException("BEPGrid:gridView_Sorting::Error", ex);
            }
            finally
            {
                nlog.Trace("BEPGrid:gridView_Sorting::Leaving");
            }
        }

        private void ApplySorting(string SortExp)
        {
            nlog.Trace("BEPGrid:gridView_Sorting::Entering");
            try
            { //re-run the query, use linq to sort the objects based on the arg.
                //perform a search using the constraints given 
                //you could have this saved in Session, rather than requerying your datastore
                List<BEP_Detail> myGridResults = CacheWrapper.GetInstance().GetAllBEPItems();
                var param = Expression.Parameter(typeof(BEP_Detail), SortExp);
                var sortExpression = Expression.Lambda<Func<BEP_Detail, object>>(Expression.Convert(Expression.Property(param, SortExp), typeof(object)), param);

                if (myGridResults != null)
                {


                    if (GridViewSortDirection == SortDirection.Ascending)
                    {
                        GridViewHierachical.DataSource = (myGridResults.AsQueryable<BEP_Detail>().OrderBy(sortExpression)).ToList<BEP_Detail>();
                        //GridViewSortDirection = SortDirection.Descending;
                    }
                    else
                    {
                        GridViewHierachical.DataSource = (myGridResults.AsQueryable<BEP_Detail>().OrderByDescending(sortExpression)).ToList<BEP_Detail>();
                        //GridViewSortDirection = SortDirection.Ascending;
                    };


                    GridViewHierachical.DataBind();
                }

                List<BEP_Detail> myGridResultsProfit = CacheWrapper.GetInstance().GetAllProfitBEPItems();

                if (myGridResults != null)
                {


                    if (GridViewSortDirection == SortDirection.Ascending)
                    {
                        GV_Profit.DataSource = (myGridResultsProfit.AsQueryable<BEP_Detail>().OrderBy(sortExpression)).ToList<BEP_Detail>();
                        //GridViewSortDirection = SortDirection.Descending;
                    }
                    else
                    {
                        GV_Profit.DataSource = (myGridResultsProfit.AsQueryable<BEP_Detail>().OrderByDescending(sortExpression)).ToList<BEP_Detail>();
                        //GridViewSortDirection = SortDirection.Ascending;
                    };


                    GV_Profit.DataBind();
                }

                List<BEP_Detail> myGridResultsloss = CacheWrapper.GetInstance().GetAllLossBEPItems();

                if (myGridResults != null)
                {


                    if (GridViewSortDirection == SortDirection.Ascending)
                    {
                        GV_Loss.DataSource = (myGridResultsloss.AsQueryable<BEP_Detail>().OrderBy(sortExpression)).ToList<BEP_Detail>();
                        GridViewSortDirection = SortDirection.Descending;
                        GridSortExpression = SortExp;
                    }
                    else
                    {
                        GV_Loss.DataSource = (myGridResultsloss.AsQueryable<BEP_Detail>().OrderByDescending(sortExpression)).ToList<BEP_Detail>();
                        GridViewSortDirection = SortDirection.Ascending;
                        GridSortExpression = SortExp;
                    };


                    GV_Loss.DataBind();
                }
            }
            catch (Exception ex)
            {
                nlog.ErrorException("BEPGrid:gridView_Sorting::Error", ex);
            }
            finally
            {
                nlog.Trace("BEPGrid:gridView_Sorting::Leaving");
            }
        }

        protected void GridViewHierachical_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            nlog.Trace("BEPGrid:GridViewHierachical_RowDataBound::Entering");
            try
            {
                string o = DataBinder.Eval(e.Row.DataItem, "ProfitMargin").ToString();

                double i = 0;
                bool flag = double.TryParse(o, out i);
                string textColor = "";

                if (i < -200)
                {
                    e.Row.BackColor = System.Drawing.Color.Red;
                    e.Row.ForeColor = System.Drawing.Color.Black;
                    textColor = "Red";
                }
                else if (i > 200)
                {
                    e.Row.BackColor = System.Drawing.Color.LightGreen;
                    e.Row.ForeColor = System.Drawing.Color.Black;
                    textColor = "LightGreen";
                }
                else
                {
                    textColor = "White";
                }


                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    // LinkButton lbtngrdDetails = (LinkButton)e.Row.Cells[7].FindControl(
                    //     "lbtngrdDetails");
                    // LinkButton lbtngrdTrend = (LinkButton)e.Row.Cells[7].FindControl(
                    //     "lbtngrdTrend");
                    // LinkButton lbtngrdPO = (LinkButton)e.Row.Cells[7].FindControl(
                    //     "lbtngrdPO");
                    // LinkButton lbtngrdSales = (LinkButton)e.Row.Cells[7].FindControl(
                    //     "lbtngrdSales");
                    // HtmlGenericControl panel =
                    //     (HtmlGenericControl)e.Row.Cells[7].FindControl(
                    //     "gridPopup");

                    //  string showPopup = "ShowPopup('" + lbtngrdDetails.ClientID +
                    //"','" + lbtngrdTrend.ClientID + "','" + lbtngrdPO.ClientID + "','" +
                    // lbtngrdSales.ClientID + "','" +
                    //panel.ClientID + "','" + e.Row.ClientID + "', this)";
                    // string hidePopup = "HidePopup('" + lbtngrdDetails.ClientID +
                    //     "','" + lbtngrdTrend.ClientID + "','" + lbtngrdPO.ClientID + "','" +
                    //     lbtngrdSales.ClientID
                    //     + "','" + panel.ClientID + "','" +
                    //     e.Row.ClientID + "', this)";
                    // e.Row.Attributes.Add("onmouseover",
                    // "javascript:" + showPopup);

                    // e.Row.Attributes.Add("onmouseout",
                    //     "javascript:" + hidePopup);

                    e.Row.Attributes.Add("onMouseOver", "this.style.background='#eeff00'");
                    e.Row.Attributes.Add("onMouseOut", "this.style.background='" + textColor + "'");

                    e.Row.Attributes.Add("oncontextmenu", "JavaScript:return showContextMenu();");

                }

            }
            catch (Exception ex)
            {
                nlog.ErrorException("BEPGrid:GridViewHierachical_RowDataBound::Error", ex);
            }
            finally
            {
                nlog.Trace("BEPGrid:GridViewHierachical_RowDataBound::Leaving");
            }
        }

        protected void GridViewHierachical_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "BEPDETAIL":
                        {
                            //string strScript = string.Format("window.open('/BEPDetails.aspx?PID="+e.CommandArgument.ToString()+"','_self');");

                            Response.Redirect(Request.RawUrl.Replace("BEPGrid.aspx", "") + "BEPDetails.aspx?PID=" + e.CommandArgument.ToString(), false);
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "nothing", strScript, true);
                        }
                        break;
                    case "SALETREND":
                        {
                            // string strScript = string.Format("window.open('/BEPDetails.aspx?PID=" + e.CommandArgument.ToString() + "','_self');");
                            Response.Redirect(Request.RawUrl.Replace("BEPGrid.aspx", "") + "SalesTrend.aspx?PID=" + e.CommandArgument.ToString(), false);
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "nothing", strScript, true);
                        }
                        break;
                    case "PODETAIL":
                        {
                            Response.Redirect(Request.RawUrl.Replace("BEPGrid.aspx", "") + "PODetails.aspx?PID=" + e.CommandArgument.ToString(), false);
                            // string strScript = string.Format("window.open('/BEPDetails.aspx?PID=" + e.CommandArgument.ToString() + "','_self');");
                            // ScriptManager.RegisterStartupScript(this, this.GetType(), "nothing", strScript, true);
                        }
                        break;

                    case "SALEDETAIL":
                        {
                            Response.Redirect(Request.RawUrl.Replace("BEPGrid.aspx", "") + "SalesDetails.aspx?PID=" + e.CommandArgument.ToString(), false);

                            //string strScript = string.Format("window.open('/BEPDetails.aspx?PID=" + e.CommandArgument.ToString() + "','_self');");
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "nothing", strScript, true);
                        }
                        break;
                    default:
                        { return; }

                }

            }
            catch
            {
            }
        }

        protected void GV_Profit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            nlog.Trace("BEPGrid:GV_Profit_RowDataBound::Entering");
            try
            {



                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string o = DataBinder.Eval(e.Row.DataItem, "ProfitMargin").ToString();

                    double i = 0;
                    bool flag = double.TryParse(o, out i);

                    if (i < -200)
                    {
                        e.Row.BackColor = System.Drawing.Color.Red;
                        e.Row.ForeColor = System.Drawing.Color.Black;
                        e.Row.Attributes.Add("onMouseOver", "this.style.background='#eeff00'");
                        e.Row.Attributes.Add("onMouseOut", "this.style.background='RED'");
                    }
                    else if (i > 200)
                    {
                        e.Row.BackColor = System.Drawing.Color.LightGreen;
                        e.Row.ForeColor = System.Drawing.Color.Black;
                        e.Row.Attributes.Add("onMouseOver", "this.style.background='#eeff00'");
                        e.Row.Attributes.Add("onMouseOut", "this.style.background='LIGHTGREEN'");
                    }
                    else
                    {
                        e.Row.Attributes.Add("onMouseOver", "this.style.background='#eeff00'");
                        e.Row.Attributes.Add("onMouseOut", "this.style.background='WHITE'");
                    }



                }
            }
            catch (Exception ex)
            {
                nlog.ErrorException("BEPGrid:GV_Profit_RowDataBound::Error", ex);
            }
            finally
            {
                nlog.Trace("BEPGrid:GV_Profit_RowDataBound::Leaving");
            }
        }

        protected void GV_Loss_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            nlog.Trace("BEPGrid:GV_Loss_RowDataBound::Entering");
            try
            {
                string o = DataBinder.Eval(e.Row.DataItem, "ProfitMargin").ToString();

                double i = 0;
                bool flag = double.TryParse(o, out i);
                string textColor = "";

                if (i < -200)
                {
                    e.Row.BackColor = System.Drawing.Color.Red;
                    e.Row.ForeColor = System.Drawing.Color.Black;
                    textColor = "Red";
                }
                else if (i > 200)
                {
                    e.Row.BackColor = System.Drawing.Color.LightGreen;
                    e.Row.ForeColor = System.Drawing.Color.Black;
                    textColor = "LightGreen";
                }
                else
                {
                    textColor = "White";
                }


                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    // LinkButton lbtngrdDetails = (LinkButton)e.Row.Cells[7].FindControl(
                    //     "lbtngrdDetails");
                    // LinkButton lbtngrdTrend = (LinkButton)e.Row.Cells[7].FindControl(
                    //     "lbtngrdTrend");
                    // LinkButton lbtngrdPO = (LinkButton)e.Row.Cells[7].FindControl(
                    //     "lbtngrdPO");
                    // LinkButton lbtngrdSales = (LinkButton)e.Row.Cells[7].FindControl(
                    //     "lbtngrdSales");
                    // HtmlGenericControl panel =
                    //     (HtmlGenericControl)e.Row.Cells[7].FindControl(
                    //     "gridPopup");

                    //  string showPopup = "ShowPopup('" + lbtngrdDetails.ClientID +
                    //"','" + lbtngrdTrend.ClientID + "','" + lbtngrdPO.ClientID + "','" +
                    // lbtngrdSales.ClientID + "','" +
                    //panel.ClientID + "','" + e.Row.ClientID + "', this)";
                    // string hidePopup = "HidePopup('" + lbtngrdDetails.ClientID +
                    //     "','" + lbtngrdTrend.ClientID + "','" + lbtngrdPO.ClientID + "','" +
                    //     lbtngrdSales.ClientID
                    //     + "','" + panel.ClientID + "','" +
                    //     e.Row.ClientID + "', this)";
                    // e.Row.Attributes.Add("onmouseover",
                    // "javascript:" + showPopup);

                    // e.Row.Attributes.Add("onmouseout",
                    //     "javascript:" + hidePopup);

                    e.Row.Attributes.Add("onMouseOver", "this.style.background='#eeff00'");
                    e.Row.Attributes.Add("onMouseOut", "this.style.background='" + textColor + "'");

                    e.Row.Attributes.Add("oncontextmenu", "JavaScript:return showContextMenu();");

                }
            }
            catch (Exception ex)
            {
                nlog.ErrorException("BEPGrid:GV_Loss_RowDataBound::Error", ex);
            }
            finally
            {
                nlog.Trace("BEPGrid:GV_Loss_RowDataBound::Leaving");
            }
        }

        protected void GV_Even_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            nlog.Trace("BEPGrid:GV_Even_RowDataBound::Entering");
            try
            {
                string o = DataBinder.Eval(e.Row.DataItem, "ProfitMargin").ToString();

                double i = 0;
                bool flag = double.TryParse(o, out i);
                string textColor = "";

                if (i < -200)
                {
                    e.Row.BackColor = System.Drawing.Color.Red;
                    e.Row.ForeColor = System.Drawing.Color.Black;
                    textColor = "Red";
                }
                else if (i > 200)
                {
                    e.Row.BackColor = System.Drawing.Color.LightGreen;
                    e.Row.ForeColor = System.Drawing.Color.Black;
                    textColor = "LightGreen";
                }
                else
                {
                    textColor = "White";
                }


                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                   
                    e.Row.Attributes.Add("onMouseOver", "this.style.background='#eeff00'");
                    e.Row.Attributes.Add("onMouseOut", "this.style.background='" + textColor + "'");

                    e.Row.Attributes.Add("oncontextmenu", "JavaScript:return showContextMenu();");

                }
            }
            catch (Exception ex)
            {
                nlog.ErrorException("BEPGrid:GV_Even_RowDataBound::Error", ex);
            }
            finally
            {
                nlog.Trace("BEPGrid:GV_Even_RowDataBound::Leaving");
            }
        }

        protected void GV_QOH_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            nlog.Trace("BEPGrid:GV_QOH_RowDataBound::Entering");
            try
            {
                string o = DataBinder.Eval(e.Row.DataItem, "ProfitMargin").ToString();

                double i = 0;
                bool flag = double.TryParse(o, out i);
                string textColor = "";

                if (i < -200)
                {
                    e.Row.BackColor = System.Drawing.Color.Red;
                    e.Row.ForeColor = System.Drawing.Color.Black;
                    textColor = "Red";
                }
                else if (i > 200)
                {
                    e.Row.BackColor = System.Drawing.Color.LightGreen;
                    e.Row.ForeColor = System.Drawing.Color.Black;
                    textColor = "LightGreen";
                }
                else
                {
                    textColor = "White";
                }


                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    e.Row.Attributes.Add("onMouseOver", "this.style.background='#eeff00'");
                    e.Row.Attributes.Add("onMouseOut", "this.style.background='" + textColor + "'");

                    e.Row.Attributes.Add("oncontextmenu", "JavaScript:return showContextMenu();");

                }
            }
            catch (Exception ex)
            {
                nlog.ErrorException("BEPGrid:GV_QOH_RowDataBound::Error", ex);
            }
            finally
            {
                nlog.Trace("BEPGrid:GV_QOH_RowDataBound::Leaving");
            }
        }


        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetUPCData(string prefixText, int count)
        {

            CacheWrapper cw = CacheWrapper.GetInstance();
            List<BEP_Detail> lstUPC = cw.GetAllBEPItems().Where(b => b.UPC.Contains(prefixText)).ToList<BEP_Detail>();
            List<string> lstStrUPC = new List<string>();
            foreach (BEP_Detail obj in lstUPC)
            {
                lstStrUPC.Add(obj.UPC);
            }
            return lstStrUPC;
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetVendorData(string prefixText, int count)
        {
            prefixText = prefixText.ToUpper();
            CacheWrapper cw = CacheWrapper.GetInstance();
            List<BEP_Detail> lstUPC = cw.GetAllBEPItems().Where(b => b.Vendor.ToUpper().Contains(prefixText)).ToList<BEP_Detail>();
            List<string> lstStrUPC = new List<string>();
            foreach (BEP_Detail obj in lstUPC)
            {
                if (!lstStrUPC.Contains(obj.Vendor))
                    lstStrUPC.Add(obj.Vendor);
            }
            return lstStrUPC;
        }

        protected void btnApplyFilter_Click(object sender, EventArgs e)
        {
            nlog.Trace("BEPGrid:btnApplyFilter_Click::Entering");
            try
            {
                int minQtyonHand = int.Parse(txtQtyToBeBrokeEven.Text);

                BindGrid(txtUPC.Text, txtVendor.Text, txtDesc.Text, dLstPercentage.SelectedValue, minQtyonHand);
            }
            catch (Exception ex)
            {
                nlog.ErrorException("BEPGrid:btnApplyFilter_Click::Error", ex);
            }
            finally
            {
                nlog.Trace("BEPGrid:btnApplyFilter_Click::Leaving");
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            nlog.Trace("BEPGrid:btnClear_Click::Entering");
            try
            {
                txtUPC.Text = "";
                txtVendor.Text = "";
                txtDesc.Text = "";
                dLstPercentage.SelectedIndex = 0;
                txtQtyToBeBrokeEven.Text = "0";
                BindGrid("","","", "NONE", 0);
            }
            catch (Exception ex)
            {
                nlog.ErrorException("BEPGrid:btnClear_Click::Error", ex);
            }
            finally
            {
                nlog.Trace("BEPGrid:btnClear_Click::Leaving");
            }
        }



    }
}