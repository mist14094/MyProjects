using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LotControlDataAccess;

namespace LotControlBusiness
{
    
    public class Label
    {
        public int LabelId { get; set; }
        public string StockCode { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string Uom { get; set; }
        public decimal CostValue { get; set; }
        public string Warehouse { get; set; }
        public int PurchaseOrderLin { get; set; }
        public string LotNumber { get; set; }
        public DateTime DateTime { get; set; }
        public string GrnNumber { get; set; }
        public string  PoNumber { get; set; }
        public string  Supplier { get; set; }
        public int TotalPrintedTickets { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string Notes { get; set; }
        public int TagsNeededForThisLine { get; set; }
        public float AltUOM { get; set; }

        private string _tagqty = "N/A";
        
    
        public List<Label> GetLablelsForPo(DataTable dt)
        {
           List<Label> labels = new List<Label>();
            try
            {

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        var lb = new Label
                        {
                            LabelId = int.Parse(row["Sno"].ToString().Trim()),
                            StockCode = row["StockCode"].ToString().Trim(),
                            Description = row["Description"].ToString().Trim(),
                            Quantity = int.Parse(row["Quantity"].ToString().Trim()),
                            CostValue = decimal.Parse(row["CostValue"].ToString().Trim()),
                            PurchaseOrderLin = int.Parse(row["PurchaseOrderLin"].ToString().Trim()),
                            LotNumber = row["Lotnumber"].ToString().Trim(),
                            DateTime = DateTime.Parse(row["Date"].ToString().Trim()),
                            GrnNumber = row["GRNNumber"].ToString().Trim(),
                            PoNumber = row["PONumber"].ToString().Trim(),
                            Supplier = row["Supplier"].ToString().Trim(),
                            CreatedDateTime = DateTime.Parse(row["CreatedDate"].ToString()),
                            Notes = row["Notes"].ToString().Trim(),
                            TotalPrintedTickets = int.Parse(row["TotalPrintedTickets"].ToString().Trim()),
                            Uom = row["UOM"].ToString().Trim(),
                            Warehouse = row["Warehouse"].ToString().Trim(),
                            AltUOM = float.Parse(row["AltUOM"].ToString().Trim())

                        };

                        //switch (row["Warehouse"].ToString().Trim())
                        //{
                        //    case "TB":
                        //        lb.Warehouse = Warehouses.TB;
                        //        break;
                        //    case "RM":
                        //        lb.Warehouse = Warehouses.RM;
                        //        break;
                        //    default:
                        //        lb.Warehouse = Warehouses.OTHERS;
                        //        break;
                        //}
                        //switch (row["UOM"].ToString().Trim())
                        //{
                        //    case "KG":
                        //        lb.Uom = UnitOfMeasure.KG;
                        //        break;
                        //    case "LBS":
                        //        lb.Uom = UnitOfMeasure.LBS;
                        //        break;
                        //    case "EA":
                        //        lb.Uom = UnitOfMeasure.EA;
                        //        break;
                        //    default:
                        //        lb.Uom = UnitOfMeasure.OTHERS;
                        //        break;
                        //}

                        if (lb.AltUOM >= 100)
                        {
                            lb.TagsNeededForThisLine = (int) Math.Round(lb.Quantity/lb.AltUOM, MidpointRounding.ToEven);
                        }
                        else
                        {
                            
                        }

                        labels.Add(lb);
                   }
                }

                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                return null;
                
            }

            return labels;
        }

        public void PrintLog(int numberoftags, bool chkPrint, int barcode,
            string stockCode, string description, string quantity, string warehouse,
            string lotnumber, string grnNumber, string supplier, string poNumber, string counts)
        {
            LcBusiness  business = new LcBusiness();
            var result = business.InsertLabelPrintLog(numberoftags, chkPrint, barcode,
                stockCode, description, quantity, warehouse,
                lotnumber, grnNumber, supplier, poNumber, counts);
        }


    }

    public enum UnitOfMeasure
    {
       KG=1,LBS=2, EA=3,OTHERS=4
    }

    public enum Warehouses
    {
        TB=1,RM=2, OTHERS = 3
    }
}
