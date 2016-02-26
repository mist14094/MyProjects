using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KTone.DAL.KTDBBaseLib;

namespace TrackerRetailDataAccess
{
    public class KT_CICOTransDetails : DBInteractionBase
    {
        #region Constructors
        public KT_CICOTransDetails() { }
        #endregion

        #region Private Fields
        private int _CICOTransDetail_ID;
        private int _CICOTrans_ID;
        private int _RRD_ID;
        private string _UPC;
        private string _SKU;
        private int _StoreID;
        private int _OrderedQty;
        private int _CheckedOutQty;
        private int _CheckedInQty;
        private int _CreatedBy;
        private DateTime _CreatedDate;
        private int _UpdatedBy;
        private DateTime _UpdateDate;
        #endregion

        #region Public Properties
        public int CICOTransDetail_ID { get { return _CICOTransDetail_ID; } set { _CICOTransDetail_ID = value; } }
        public int CICOTrans_ID { get { return _CICOTrans_ID; } set { _CICOTrans_ID = value; } }
        public int RRD_ID { get { return _RRD_ID; } set { _RRD_ID = value; } }
        public string UPC { get { return _UPC; } set { _UPC = value; } }
        public string SKU { get { return _SKU; } set { _SKU = value; } }
        public int StoreID { get { return _StoreID; } set { _StoreID = value; } }
        public int OrderedQty { get { return _OrderedQty; } set { _OrderedQty = value; } }
        public int CheckedOutQty { get { return _CheckedOutQty; } set { _CheckedOutQty = value; } }
        public int CheckedInQty { get { return _CheckedInQty; } set { _CheckedInQty = value; } }
        public int CreatedBy { get { return _CreatedBy; } set { _CreatedBy = value; } }
        public DateTime CreatedDate { get { return _CreatedDate; } set { _CreatedDate = value; } }
        public int UpdatedBy { get { return _UpdatedBy; } set { _UpdatedBy = value; } }
        public DateTime UpdateDate { get { return _UpdateDate; } set { _UpdateDate = value; } }
        #endregion
    }
}
