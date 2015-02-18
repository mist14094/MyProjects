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
        
    public partial class WebForm2 : System.Web.UI.Page
    {
        ReportDocument rdoc = new ReportDocument();
        private GBBusiness.BusinessAccess _businessAccess = new BusinessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                rdoc.Load(Server.MapPath("rptLogTransaction.rpt"));
                DataSet DS = new DataSet();
               DS = _businessAccess.BOLLog(DateTime.Now.AddDays(-10),DateTime.Now);
                rdoc.SetDataSource(DS.Tables[0]);
                CrystalReportViewer1.HasPrintButton = true;
                CrystalReportViewer1.PDFOneClickPrinting = true;
                CrystalReportViewer1.PrintMode = PrintMode.ActiveX;
                 rdoc.SetParameterValue("varStart", DateTime.Now.AddDays(-10).ToString("G"));
                rdoc.SetParameterValue("varEnd", DateTime.Now.ToString("G"));
                
             
                CrystalReportViewer1.ReportSource = rdoc;
                CrystalReportViewer1.DataBind();


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