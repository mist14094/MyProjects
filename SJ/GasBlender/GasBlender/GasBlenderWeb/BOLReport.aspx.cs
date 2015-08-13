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
    public partial class BOLReport : System.Web.UI.Page
    {
     //   SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["gasblendConnectionString"].ConnectionString);
        ReportDocument rdoc = new ReportDocument();
        private GBBusiness.BusinessAccess _businessAccess = new BusinessAccess();
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["ID"] != null)
                {
                    string queryID = Request.QueryString["ID"].ToString();
                    rdoc.Load(Server.MapPath("rptBOL.rpt"));
                    DataSet DS = new DataSet();
                    DS = _businessAccess.GetBOL(queryID);
                    rdoc.SetDataSource(DS.Tables[0]);
                    Reports.HasPrintButton = true;
                    Reports.PDFOneClickPrinting = true;
                    Reports.PrintMode = PrintMode.ActiveX;
                    //rdoc.SetParameterValue("@LoadID", "16549");
                    Reports.ReportSource = rdoc;
                    Reports.DataBind();
                    // CrystalReportViewer1.HasToggleGroupTreeButton = true;
                    Reports.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;
                    //CrystalReportViewer1.GroupTreeStyle.
                    _businessAccess.InsertLog(Session["ID"].ToString(),
                  System.Reflection.MethodBase.GetCurrentMethod().Name, this.Page.ToString(), DateTime.Now);

                }
            }
            catch (Exception ex)
            {

            }
        }
       
        protected void Page_Load(object sender, EventArgs e)
        {
            //try
            //{
            //    if (Request.QueryString["ID"]!=null)
            //    {
            //        string queryID = Request.QueryString["ID"].ToString(); 
            //            rdoc.Load(Server.MapPath("rptBOL.rpt"));
            //            DataSet DS = new DataSet();
            //            DS = _businessAccess.GetBOL(queryID);
            //            rdoc.SetDataSource(DS.Tables[0]);
            //            Reports.HasPrintButton = true;
            //            Reports.PDFOneClickPrinting = true;
            //            Reports.PrintMode = PrintMode.ActiveX;
            //            //rdoc.SetParameterValue("@LoadID", "16549");
            //            Reports.ReportSource = rdoc;
            //            Reports.DataBind();
            //        // CrystalReportViewer1.HasToggleGroupTreeButton = true;
            //            Reports.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;
            //        //CrystalReportViewer1.GroupTreeStyle.
            //            _businessAccess.InsertLog(Session["ID"].ToString(),
            //          System.Reflection.MethodBase.GetCurrentMethod().Name, this.Page.ToString(), DateTime.Now);
                
            //    }
            //}
            //catch (Exception ex)
            //{
             
            //}
         
        }

        protected void PrntButton_Click(object sender, EventArgs e)
        {
            rdoc.PrintToPrinter(1, false, 0, 0);
        }
    }
}