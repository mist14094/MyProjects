using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class ProductSimple
    {
        public String UPC { get; set; }

        public String SKU { get; set; }

        public Int32 StoreID { get; set; }

        public String Desc { get; set; }

        public String VendorName { get; set; }
        
        public String StyleCode { get; set; }

        public String StyleDesc { get; set; }

        public String ColorCode { get; set; }

        public String ColorDesc { get; set; }

        public String SizeCode { get; set; }

        public String SizeDesc { get; set; }
        
        public Decimal? Price { get; set; }

        public string Cost { get; set; }

        public DateTime? DateModified { get; set; }
    }
}
