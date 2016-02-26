using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KTone.DAL.KTDBBaseLib;

namespace TrackerRetailDataAccess
{
    public class KT_ProductItemStatus : DBInteractionBase
    {
        #region Constructors
        public KT_ProductItemStatus() { }
        #endregion

        #region Private Fields
        private int _ProductItemID;
        private string _ProductItemStatus;
        private int _LastSeenLocation;
        private DateTime _LastSeenTime;
        #endregion

        #region Public Properties
        public int ProductItemID { get { return _ProductItemID; } set { _ProductItemID = value; } }
        public string ProductItemStatus { get { return _ProductItemStatus; } set { _ProductItemStatus = value; } }
        public int LastSeenLocation { get { return _LastSeenLocation; } set { _LastSeenLocation = value; } }
        public DateTime LastSeenTime { get { return _LastSeenTime; } set { _LastSeenTime = value; } }
        #endregion
    }
}
