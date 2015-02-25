using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using GBBusiness;

namespace GasBlenderWeb
{

    public partial class TransactionReport : System.Web.UI.Page
    {
        ReportDocument rdoc = new ReportDocument();
        private GBBusiness.BusinessAccess _businessAccess = new BusinessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Request.QueryString["Start"] != null && Request.QueryString["End"] != null)
                {
                    string StartDate = Request.QueryString["Start"].ToString();
                    string EndDate = Request.QueryString["End"].ToString();

                    DateTime startDateTime = Convert.ToDateTime(StartDate);
                    DateTime endDateTime = Convert.ToDateTime(EndDate);


                    rdoc.Load(Server.MapPath("rptLogTransaction.rpt"));
                    DataSet DS = new DataSet();
                    DS = _businessAccess.BOLLog(startDateTime, endDateTime);
                    rdoc.SetDataSource(DS.Tables[0]);
                    TransactionLogReport.HasPrintButton = true;
                    TransactionLogReport.PDFOneClickPrinting = true;
                    TransactionLogReport.PrintMode = PrintMode.ActiveX;
                    rdoc.SetParameterValue("varStart", StartDate);
                    rdoc.SetParameterValue("varEnd", EndDate);
                    TransactionLogReport.HasToggleGroupTreeButton = false;
                    TransactionLogReport.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;

                    TransactionLogReport.ReportSource = rdoc;
                    TransactionLogReport.DataBind();

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void PrntButton_Click(object sender, EventArgs e)
        {
            rdoc.PrintToPrinter(1, false, 0, 0);
        }
    }
}