using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using GBBusiness;

namespace GasBlenderWeb
{
    public partial class WebForm1 : System.Web.UI.Page
    {
     //   SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["gasblendConnectionString"].ConnectionString);
        ReportDocument rdoc = new ReportDocument();
        private GBBusiness.BusinessAccess _businessAccess = new BusinessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                rdoc.Load(Server.MapPath("rptBOL.rpt"));
                DataSet DS = new DataSet();

                DS = _businessAccess.GetBOL("16545");
                rdoc.SetDataSource(DS.Tables[0]);
                CrystalReportViewer1.HasPrintButton = true;
                CrystalReportViewer1.PDFOneClickPrinting = true;
                CrystalReportViewer1.PrintMode = PrintMode.ActiveX;
                //rdoc.SetParameterValue("@LoadID", "16549");
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