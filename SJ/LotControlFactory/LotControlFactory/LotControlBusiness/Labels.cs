using System;
using System.Collections.Generic;
using System.Data;

namespace LotControlBusiness
{
    public class Labels
    {
        public int LabelId { get; set; }
        public int MasterLineId { get; set; }
        public string StockCode { get; set; }
        public string  Description { get; set; }
        public decimal Quantity { get; set; }
        public string  Unitofmeasure { get; set; }
        public string  Warehouse { get; set; }
        public int PurchaseOrderLin { get; set; }
        public string Lotnumber { get; set; }
        public DateTime Date { get; set; }
        public string GrnNumber { get; set; }
        public string PoNumber { get; set; }
        public string Supplier { get; set; }
        public int LineNumberInOrder { get; set; }
        public bool IsPrinted { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Notes { get; set; }

        public List<Labels> GetLabelForPO(string PoNumber)
        {
            LcBusiness business = new LcBusiness();
            List<Labels> list = new List<Labels>();
            DataTable dt = new DataTable();
            dt = business.GetLabelForPO(PoNumber);

            foreach (DataRow row in dt.Rows)
            {
                var lb = new Labels()
                {
                    LabelId = int.Parse(row["Sno"].ToString().Trim()),
                    MasterLineId = int.Parse(row["MasterLineItemId"].ToString().Trim()),
                    StockCode = row["StockCode"].ToString().Trim(),
                    Description = row["Description"].ToString().Trim(),
                    Quantity = decimal.Parse(row["Quantity"].ToString().Trim()),
                    Unitofmeasure = row["UOM"].ToString().Trim(),
                    PurchaseOrderLin = int.Parse(row["PurchaseOrderLin"].ToString().Trim()),
                    Lotnumber = row["Lotnumber"].ToString().Trim(),
                    Date = DateTime.Parse(row["Date"].ToString().Trim()),
                    GrnNumber = row["GRNNumber"].ToString().Trim(),
                    PoNumber = row["PONumber"].ToString().Trim(),
                    Supplier = row["Supplier"].ToString().Trim(),
                    CreatedDate = DateTime.Parse(row["CreatedDate"].ToString()),
                    Notes = row["Notes"].ToString().Trim(),
                    LineNumberInOrder = int.Parse(row["LineNumberInOrder"].ToString().Trim()),
                    Warehouse = row["Warehouse"].ToString().Trim(),
                    IsPrinted = bool.Parse(row["IsPrinted"].ToString().Trim())

                };
              

                list.Add(lb);

            }
            return list;
        }


        public void PrintLog(int numberoftags, bool chkPrint, int barcode,
         string stockCode, string description, string quantity, string warehouse,
         string lotnumber, string grnNumber, string supplier, string poNumber, string counts)
        {
            LcBusiness business = new LcBusiness();
            var result = business.InsertLabelPrintLog(numberoftags, chkPrint, barcode,
                stockCode, description, quantity, warehouse,
                lotnumber, grnNumber, supplier, poNumber, counts);
        }

    }
}