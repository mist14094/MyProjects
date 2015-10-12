using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifiedPOConstants
{
    public class POClass
    {
        public string VendorName { get; set; }
        public string VendorAddress { get; set; }
        public string VendorContactNumber { get; set; }
        public string PoNumber { get; set; }
        public bool IsReOrder { get; set; }
        public string BuyerName { get; set; }
        public string BuyerAddress { get; set; }
        public string BuyerContactNumber { get; set; }
        public decimal TotalCost { get; set; }
        public string SysproPoNumber { get; set; }
        public string OnlinePoNumber { get; set; }
        public List<POLineItems> PoLineItems { get; set; }

    }

    public class POLineItems
    {
        public string VendorCode { get; set; }
        public string StockCode { get; set; }
        public string Description { get; set; }
        public string UPC { get; set; }
        public string SKU { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string Style { get; set; }
        public string Gender { get; set; }
        public string UnitOfMeasure { get; set; }
        public string Quantity { get; set; }
        public string Cost { get; set; }
        public string Retail { get; set; }
    }
}
