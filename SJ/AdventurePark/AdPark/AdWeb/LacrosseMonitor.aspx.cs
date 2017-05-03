using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdBsnsLayer;
namespace AdWeb
{
    public partial class LacrosseMonitor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

        }

        protected void Timer1_Tick(object sender, EventArgs e)

        {
            AdBsnsLayer.Monitor monitor = new AdBsnsLayer.Monitor();
            var Result = monitor.LacrosseMonitor();

            if (Result.Tables.Count >= 5)
            {
                if (Result.Tables[4].Rows.Count >= 1)
                {
                    divPopU1p.Attributes.Add("style", "display: block;");
                    lblMessage.Text = Result.Tables[4].Rows[0]["FirstName"].ToString() + " - " +
                                      Result.Tables[4].Rows[0]["Message"].ToString();
                   
                }
                else
                {

                    divPopU1p.Attributes.Add("style", "display: none;");
                    lblMessage.Text = "asdasdasdas";
                   
                }
                if (Result.Tables[0].Rows.Count >= 1)
                {
                    if (Result.Tables[0].Rows[0]["CurrentState"].ToString().Equals(String.Empty))
                    {
                        lblName.Text = "Welcome " + Result.Tables[0].Rows[0]["FirstName"].ToString() + ", You have";
                        SetPanelVisibility();
                        pnlTimer.Visible = true;
                        lblTimer.Text= Result.Tables[0].Rows[0]["TimeDiff"].ToString();
                       
                    }
                    else
                    {

                        if (
                            (DateTime.Now - DateTime.Parse(Result.Tables[0].Rows[0]["CreatedDate"].ToString()))
                            .TotalSeconds > 120)
                        {
                            SetPanelVisibility();
                            pnlRecord.Visible = true;
                            grdTopAllRecord.DataSource = Result.Tables[2];
                            grdTopAllRecord.DataBind();
                            grdTopTodayRecord.DataSource = Result.Tables[3];
                            grdTopTodayRecord.DataBind();

                        }
                        else
                        {
                            SetPanelVisibility();
                            pnlPersonRecord.Visible = true;
                            if (Result.Tables[0].Rows[0]["CurrentState"].ToString() == "Captured")
                            {
                                lblPersonRecordSpeed.Text = Result.Tables[0].Rows[0]["FirstName"].ToString() +
                                                            ", Your Speed is ";
                                lblSpeed.Text=Result.Tables[0].Rows[0]["Value"].ToString();
                                lblSwipeAgain.Text = "";
                            }
                            else
                            {
                                lblPersonRecordSpeed.Text = Result.Tables[0].Rows[0]["FirstName"].ToString() +
                                                            ", Your time is ";
                                lblSpeed.Text = "EXPIRED";

                               
                                lblSwipeAgain.Text = "Please swipe again.";

                            }
                            grdPersonalHistory.DataSource = Result.Tables[1];
                            grdPersonalHistory.DataBind();
                        }

                    }

                }
            }
            //grdTop5Today.DataSource = Result.Tables[1];
            //grdTop5Today.DataBind();

            //grdTop5AllDay.DataSource = Result.Tables[2];
            //grdTop5AllDay.DataBind();
        }

        private void SetPanelVisibility()
        {
            pnlTimer.Visible = false;
            pnlRecord.Visible = false;
            pnlPersonRecord.Visible = false;
        }
    }
}