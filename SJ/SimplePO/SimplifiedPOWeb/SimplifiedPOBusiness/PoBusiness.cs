using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NLog;
using NLog.Internal;
using SimplifiedPOConstants;
using System.Configuration;
namespace SimplifiedPOBusiness
{
    public class PoBusiness
    {
        private readonly Logger _nlog = LogManager.GetCurrentClassLogger();

        public static string ReOrder = System.Configuration.ConfigurationManager.AppSettings["ReOrder"].ToString();
        public static string VendorNameRow = System.Configuration.ConfigurationManager.AppSettings["VendorNameRow"].ToString();
        public static string VendorAddressRow = System.Configuration.ConfigurationManager.AppSettings["VendorAddressRow"].ToString();
        public static string VendorContactRow = System.Configuration.ConfigurationManager.AppSettings["VendorContactRow"].ToString();
        public static string BuyerNameRow = System.Configuration.ConfigurationManager.AppSettings["BuyerNameRow"].ToString(); 
        public static string BuyerAddressRow = System.Configuration.ConfigurationManager.AppSettings["BuyerAddressRow"].ToString();
        public static string BuyerContactRow = System.Configuration.ConfigurationManager.AppSettings["BuyerContactRow"].ToString();
        public static string ItemsGrabber = System.Configuration.ConfigurationManager.AppSettings["ItemsGrabber"].ToString();
        public POClass ConvertDataTableToPoClass(DataTable dt)
        {
            _nlog.Trace(message:
              this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
              System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
          
            POClass poClass = new POClass();

            return ProcessPoFromTable(CleanTable(dt)); 
        }


        public POClass ProcessPoFromTable(DataTable dt)
        {
            _nlog.Trace(message:
             this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
             System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            POClass poClass = new POClass();
            if (dt.Rows.Count > 0)
            {
                poClass.VendorName = GetDataTableValue(VendorNameRow, dt);
                poClass.VendorAddress = GetDataTableValue(VendorAddressRow, dt);
                poClass.VendorContactNumber = GetDataTableValue(VendorContactRow, dt);
                poClass.BuyerName = GetDataTableValue(BuyerNameRow, dt);
                poClass.BuyerAddress = GetDataTableValue(BuyerAddressRow, dt);
                poClass.BuyerContactNumber = GetDataTableValue(BuyerContactRow, dt);
                poClass.IsReOrder = !GetDataTableValue(ReOrder, dt).ToUpper().Contains("NEW");
                poClass.PoLineItems = GetItemDetails(ItemsGrabber,dt);
            }
            return poClass;
            
        }

        public List<POLineItems> GetItemDetails(string CoOrdinates, DataTable dtTable)
        {
          List<POLineItems> listPoLineItemses = new List<POLineItems>();
            var v = CoOrdinates.Split(',');
            int rowBegin = int.Parse(v[0]);
            int columnBegin = int.Parse(v[1]);
            int rowEnd = int.Parse(v[2]);
            int columnEnd = int.Parse(v[3]);

            string Value = "";
            for (int i = rowBegin; i <= dtTable.Rows.Count-1; i++)
            {
                POLineItems item = new POLineItems();
                for (int j = columnBegin; j <= columnEnd; j++)
                {

                    switch (j)
                    {
                        case 0:
                            item.VendorCode = dtTable.Rows[i][j].ToString();
                            break;
                        case 1:
                            item.StockCode = dtTable.Rows[i][j].ToString();
                            break;
                        case 2:
                            item.Description = dtTable.Rows[i][j].ToString();
                            break;
                        case 3:
                            item.UPC = dtTable.Rows[i][j].ToString();
                            break;
                        case 4:
                            item.Manufacturer = dtTable.Rows[i][j].ToString();
                            break;
                        case 5:
                            item.Model = dtTable.Rows[i][j].ToString();
                            break;
                        case 6:
                            item.Color = dtTable.Rows[i][j].ToString();
                            break;
                        case 7:
                            item.Size = dtTable.Rows[i][j].ToString();
                            break;
                        case 8:
                            item.Style = dtTable.Rows[i][j].ToString();
                            break;
                        case 9:
                            item.Gender = dtTable.Rows[i][j].ToString();
                            break;
                        case 10:
                            item.UnitOfMeasure = dtTable.Rows[i][j].ToString();
                            break;
                        case 11:
                            item.Quantity = dtTable.Rows[i][j].ToString();
                            break;
                        case 12:
                            item.Cost = dtTable.Rows[i][j].ToString();
                            break;

                    }

                }
                listPoLineItemses.Add(item);

            }
            return listPoLineItemses;

        }

        public string GetDataTableValue(string CoOrdinates,DataTable dtTable)
        {
            var v = CoOrdinates.Split(',');
            int rowBegin = int.Parse(v[0]);
            int columnBegin = int.Parse(v[1]);
            int rowEnd = int.Parse(v[2]);
            int columnEnd = int.Parse(v[3]);

            string Value = "";
            for (int i = rowBegin; i <= rowEnd; i++)
            {
                for (int j = columnBegin; j <= columnEnd; j++)
                {
                    if (dtTable.Rows[i][j].ToString() != "")
                    {
                        Value = Value + " " + dtTable.Rows[i][j].ToString();
                    }
                }
            }
            return Value;

        }
        public DataTable CleanTable(DataTable dt)
        {
            _nlog.Trace(message:
             this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
             System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            DataTable cleanedTable  = new DataTable();
            cleanedTable = dt.Copy();
            try
            {
                if (dt.Rows.Count > 0)
                {
                    int deleterowscount = 0;
                    foreach (DataRow dr in dt.Rows)
                    {

                        string ConcatString = "";
                        foreach(var IA in dr.ItemArray)
                        {
                            ConcatString = IA.ToString() + ConcatString;
                        }

                        ConcatString = ConcatString.Trim();
                        ConcatString = ConcatString.Replace("0", "");

                        if (ConcatString == "")
                        {
                           
                            cleanedTable.Rows.RemoveAt( dt.Rows.IndexOf(dr) - deleterowscount);
                            deleterowscount++;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _nlog.Trace(message:
               this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
               System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error" +ex.StackTrace);
          
            }
            return cleanedTable;
        }


    }
}
