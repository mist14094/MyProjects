using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KTone.DAL.KTDBBaseLib;

namespace TrackerRetailDataAccess
{
    public class KT_CICOTransaction : DBInteractionBase
    {
        #region Constructors
        public KT_CICOTransaction() { }
        #endregion

        #region Private Fields
        private int _CICOTrans_ID;
        private int _CICOMaster_ID;
        private DateTime _ChekOutTime;
        private int _CheckOutLocation;
        private string _CheckOutUser;
        private DateTime _CheckInTime;
        private int _CheckInLocation;
        private string _CheckInUser;
        private int _CreatedBy;
        private DateTime _CreatedDate;
        private int _UpdatedBy;
        private DateTime _UpdateDate;
        private string _Comments;
        #endregion

        #region Public Properties
        public int CICOTrans_ID { get { return _CICOTrans_ID; } set { _CICOTrans_ID = value; } }
        public int CICOMaster_ID { get { return _CICOMaster_ID; } set { _CICOMaster_ID = value; } }
        public DateTime ChekOutTime { get { return _ChekOutTime; } set { _ChekOutTime = value; } }
        public int CheckOutLocation { get { return _CheckOutLocation; } set { _CheckOutLocation = value; } }
        public string CheckOutUser { get { return _CheckOutUser; } set { _CheckOutUser = value; } }
        public DateTime CheckInTime { get { return _CheckInTime; } set { _CheckInTime = value; } }
        public int CheckInLocation { get { return _CheckInLocation; } set { _CheckInLocation = value; } }
        public string CheckInUser { get { return _CheckInUser; } set { _CheckInUser = value; } }
        public int CreatedBy { get { return _CreatedBy; } set { _CreatedBy = value; } }
        public DateTime CreatedDate { get { return _CreatedDate; } set { _CreatedDate = value; } }
        public int UpdatedBy { get { return _UpdatedBy; } set { _UpdatedBy = value; } }
        public DateTime UpdateDate { get { return _UpdateDate; } set { _UpdateDate = value; } }
        public string Comments { get { return _Comments; } set { _Comments = value; } }
        #endregion
    }
}
