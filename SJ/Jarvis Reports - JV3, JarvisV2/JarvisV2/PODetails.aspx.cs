using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class _PODetails : System.Web.UI.Page
{
    public static string _RFIDSystem = ConfigurationManager.ConnectionStrings["SysproDString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string ponumber = Request.QueryString["PONumber"];
            lblPo.Text = ponumber;
            if (ponumber != null)
            {
                DataTable table = new DataTable();
                try
                {

                    table.Columns.Add("PO#", typeof(string));
                    table.Columns.Add("Line#", typeof(string));
                    table.Columns.Add("StockCode", typeof(string));
                    table.Columns.Add("UPC", typeof(string));
                    table.Columns.Add("Description", typeof(string));
                    table.Columns.Add("OrderQty", typeof(string));
                    table.Columns.Add("ReceivedQty", typeof(string));
                    table.Columns.Add("Price", typeof(string));
                    string query = "SELECT PO.PurchaseOrder as PO#, PO.Line AS Line#,PO.MStockCode as StockCode,invm.AlternateKey1 AS [UPC] ,PO.MStockDes AS [Description],PO.MOrderQty as [OrderQty],PO.MReceivedQty AS [ReceivedQty],PO.MPrice as [Price] from (SELECT PurchaseOrder,Line,LineType,MStockCode,MStockDes,MWarehouse,MOrderUom,MStockingUom,MOrderQty,MReceivedQty,MLatestDueDate,MLastReceiptDat,MSupCatalogue,MDiscPct1,MDiscPct2,MDiscPct3,MDiscValFlag,MDiscValue,MPrice,MForeignPrice,MDecimalsToPrt,MConvFactPrcUm,MMulDivPrc,MPriceUom,MTaxCode,MConvFactOrdUm,MMulDivAlloc,MProductClass,MCompleteFlag,MJob,MGlCode,MUserAuthReqn,MRequisition,MRequisitionLine,MSalesOrder,MSalesOrderLine,MOrigDueDate,MReschedDueDate,MLctConfirmed,MOriginalLine,MSubcontractOp,MEdiExtractFlag,MEdiActionFlag,MInspectionReqd,MNonsUnitMass,MNonsUnitVol,MVersion,MRelease,NComment,NCommentFromLin,NMscChargeValue,NMscChargePrint,NComPrintType,NMscChargeTax,NCommentFlag,NMscChargeFor,NMscChargeFLoc,NEdiExtract,MEccFlag,MBlanketDate,MBlanketContLine,AssetFlag,CapexCode,CapexLine,SelectionCode,SelectionType,User1,TimeStamp FROM               SysproCompanyD..PorMasterDetail    WITH (NOLOCK) WHERE (PurchaseOrder = 'poxxxx' AND Line >=    0)) AS PO LEFT outer join   SysproCompanyH..InvMaster invm on invm.StockCode = PO.MStockCode ORDER BY PO.Line";
                    
                     string newquery = query.Replace("poxxxx", ponumber);
                            DataTable dt = GetCouponSalesDetail(newquery);
                            if (dt.Rows.Count > 0)
                            {
                                for (int j = 0; j < dt.Rows.Count; j++)
                                {
                                    table.Rows.Add( dt.Rows[j]["PO#"], dt.Rows[j]["Line#"], dt.Rows[j]["StockCode"], dt.Rows[j]["UPC"], dt.Rows[j]["Description"], dt.Rows[j]["OrderQty"], dt.Rows[j]["ReceivedQty"], dt.Rows[j]["Price"]);
                                }
                            }
                    
                   
                }
                catch (Exception ex)
                {
                }


                GridView1.DataSource = table;
                GridView1.DataBind();


            }
      
      
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
       
    }

    public DataTable GetCouponSalesDetail(string query)
    {
        DataTable allData = new DataTable();
        SqlConnection connection = new SqlConnection(_RFIDSystem);
        try
        {
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.CommandType = CommandType.Text;
            //cmd.Parameters.Add(new SqlParameter("@numberofdays", NoOfDays));
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(allData);

            connection.Close();
        }
        catch
        {
            connection.Close();
        }
        return allData;
    }
    
}