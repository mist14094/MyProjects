using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KTone.DAL.KTDBBaseLib;

namespace TrackerRetailDataAccess
{
    public class KT_CICOProductItem : DBInteractionBase
    {
        #region Constructors
        public KT_CICOProductItem() { }
        #endregion
        #region Private Fields
        private int _CICOProductTransID;
        private int _CICOTransDetail_ID;
        private int _ProductItemID;
        private int _TransactionType;
        #endregion
        #region Public Properties
        public int CICOProductTransID { get { return _CICOProductTransID; } set { _CICOProductTransID = value; } }
        public int CICOTransDetail_ID { get { return _CICOTransDetail_ID; } set { _CICOTransDetail_ID = value; } }
        public int ProductItemID { get { return _ProductItemID; } set { _ProductItemID = value; } }
        public int TransactionType { get { return _TransactionType; } set { _TransactionType = value; } }
        #endregion
    }
}
