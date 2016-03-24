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
    
    public class LineItem
    {
        public int LineItemId { get; set; }
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
        public string TagDenominations { get; set; }
        private string _tagqty = "N/A";
        public bool OddNumberofTags { get; set; }
        public bool isFinalized { get; set; }
    
        public List<LineItem> GetLablelsForPo(DataTable dt)
        {
           List<LineItem> labels = new List<LineItem>();
            try
            {

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        var lb = new LineItem
                        {
                            LineItemId = int.Parse(row["Sno"].ToString().Trim()),
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
                            AltUOM = float.Parse(row["AltUOM"].ToString().Trim()),
                           isFinalized = bool.Parse(row["isFinalized"].ToString().Trim())

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
                            lb.TagsNeededForThisLine = (int) (lb.Quantity/lb.AltUOM);
                            lb.TagDenominations = lb.AltUOM.ToString() + " X " + lb.TagsNeededForThisLine.ToString();
                            lb.OddNumberofTags = false;
                            if (lb.Quantity%lb.AltUOM > 0)
                            {
                                lb.TagsNeededForThisLine++;
                                lb.TagDenominations = lb.TagDenominations + " + " + lb.Quantity%lb.AltUOM + " X 1";
                                lb.OddNumberofTags = true;
                            }
                            lb.TagDenominations = lb.TagDenominations +" "+ lb.Uom;
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

        public void UpdateAltUOM(int Barcode, float NewQuantity)
        {
            LcBusiness business = new LcBusiness();
            business.UpdateAltUOM(Barcode, NewQuantity);
        }

        public void FinalizeLine()
        {
            LcBusiness business = new LcBusiness();
            business.updateFinalized(this.LineItemId);
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
