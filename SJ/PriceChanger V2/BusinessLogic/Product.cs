using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class Product
    {

        public String UPC { get; set; }

        public String SKU { get; set; }

        public Int32 StoreID { get; set; }

        public String Desc { get; set; }

        public String VendorName { get; set; }

        public String MfgName { get; set; }

        public String StyleCode { get; set; }

        public String StyleDesc { get; set; }

        public String ColorCode { get; set; }

        public String ColorDesc { get; set; }

        public String SizeCode { get; set; }

        public String SizeDesc { get; set; }

        public Int32? QtyOnHand { get; set; }

        public Int32? QtyMin { get; set; }

        public Int32? QtyMax { get; set; }

        public String Loc1 { get; set; }

        public String Loc2 { get; set; }

        public Decimal? Price { get; set; }

        public Boolean? Clearance { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public DateTime? DateRemoved { get; set; }

        public Boolean IsActive { get; set; }

        public String Custom1 { get; set; }

        public String Custom2 { get; set; }

        public String Custom3 { get; set; }

        public String Custom4 { get; set; }

        public String Custom5 { get; set; }

        public String Custom6 { get; set; }

        public String Custom7 { get; set; }

        public String Custom8 { get; set; }

        public String Custom9 { get; set; }

        public String Custom10 { get; set; }

        public String Custom11 { get; set; }

        public String Custom12 { get; set; }

        public String Custom13 { get; set; }

        public String Custom14 { get; set; }

        public String Custom15 { get; set; }

        public String Custom16 { get; set; }

        public String Custom17 { get; set; }

        public String Custom18 { get; set; }

        public String Custom19 { get; set; }

        public String Custom20 { get; set; }

        public String Gender { get; set; }

        public Boolean? DisplayCompliance { get; set; }

        public Boolean? SalesPromo { get; set; }

        public Int32? BatchUOM { get; set; }

        public Byte[] ProductImage { get; set; }

        public Guid? rowguid { get; set; }

        public Int32? Department { get; set; }

        public Int32? DefaultZone { get; set; } 
    }
}
