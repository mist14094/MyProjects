using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakEvenBAL
{
    public class BEP_Detail
    {
        public long PC_ID { get; set; }
        public string UPC { get; set; }
        public string SKU { get; set; }
        public string StockCode { get; set; }
        public string Description { get; set; }
        public string Vendor { get; set; }
        public string UOM { get; set; }

        public long TotalRCVD { get; set; }
        public float TotalCOGS { get; set; }
        public long TotalSold { get; set; }
        public float TotalSales { get; set; }
        public float SuggRetail { get; set; }
        public DateTime LastSaleDate { get; set; }
        public string LastSaleLocation { get; set; }
        public float AvgCOGS { get; set; }
        public float AvgSalePrice { get; set; }
        public float ProfitMargin { get; set; }
        public float ProfitPercentage { get; set; }
            
    }
}
