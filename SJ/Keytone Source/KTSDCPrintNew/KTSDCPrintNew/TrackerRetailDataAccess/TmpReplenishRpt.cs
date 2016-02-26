using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KTone.DAL.KTDBBaseLib;

namespace TrackerRetailDataAccess
{
    public class TmpReplenishRpt : DBInteractionBase
    {
        #region Constructors
        public TmpReplenishRpt() { }
        #endregion

        #region Private Fields
        private string _Dept;
        private string _Zone;
        private string _ExpirationDate;
        private string _VENDOR;
        private string _UPC;
        private string _DESCRIPTION;
        private string _COUNT;
        private string _Needed;
        private int _RNum;
        private string _SKU;
        private string _COLOR;
        private string _SIZE;
        private string _Category;
        private string _Vendor_Style;
        private object _CUR_PRC;
        #endregion

        #region Public Properties
        public string Dept { get { return _Dept; } set { _Dept = value; } }
        public string Zone { get { return _Zone; } set { _Zone = value; } }
        public string ExpirationDate { get { return _ExpirationDate; } set { _ExpirationDate = value; } }
        public string VENDOR { get { return _VENDOR; } set { _VENDOR = value; } }
        public string UPC { get { return _UPC; } set { _UPC = value; } }
        public string DESCRIPTION { get { return _DESCRIPTION; } set { _DESCRIPTION = value; } }
        public string COUNT { get { return _COUNT; } set { _COUNT = value; } }
        public string Needed { get { return _Needed; } set { _Needed = value; } }
        public int RNum { get { return _RNum; } set { _RNum = value; } }
        public string SKU { get { return _SKU; } set { _SKU = value; } }
        public string COLOR { get { return _COLOR; } set { _COLOR = value; } }
        public string SIZE { get { return _SIZE; } set { _SIZE = value; } }
        public string Category { get { return _Category; } set { _Category = value; } }
        public string Vendor_Style { get { return _Vendor_Style; } set { _Vendor_Style = value; } }
        public object CUR_PRC { get { return _CUR_PRC; } set { _CUR_PRC = value; } }
        #endregion
    }
}
