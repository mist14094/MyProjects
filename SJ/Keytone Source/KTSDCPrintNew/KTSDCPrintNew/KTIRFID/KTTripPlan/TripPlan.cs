using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KTone.Core.KTIRFID
{
    [Serializable]
    public class TripAsset
    {
        long _assetId ;
        KTAssetDetails _asset;
        KTTripPlanDetails _assetTrip;
        string _currentReadPoint;
        DateTime _firstSeenTimeAtCurrentRP;
        string _lastReadPoint = "";
        DateTime _seenTimeAtLastRP;
        DateTime _lastSeentime;
        TripState _tripState;
        string _remarks = "";
        int _currentSeqNo = 0;
       


        public long AssetTripId { get; set; }
        public bool IsManual { get; set; } 

        public long AssetId
        {
            get
            {
                return _assetId;
            }
            set
            {
                if (_assetId == value)
                    return;
                _assetId = value;
            }
        }
        public KTAssetDetails Asset
        {
            get
            {
                return _asset;
            }
            set
            {
                if (_asset == value)
                    return;
                _asset = value;
            }
        }
        public KTTripPlanDetails AssetTrip
        {
            get
            {
                return _assetTrip;
            }
            set
            {
                if (_assetTrip == value)
                    return;
                _assetTrip = value;
            }
        }
        public string CurrentReadPoint
        {
            get
            {
                return _currentReadPoint;
            }
            set
            {
                if (_currentReadPoint == value)
                    return;
                _currentReadPoint = value;
            }
        }
        public DateTime FirstSeenTimeAtCurrentRP
        {
            get
            {
                return _firstSeenTimeAtCurrentRP;
            }
            set
            {
                if (_firstSeenTimeAtCurrentRP == value)
                    return;
                _firstSeenTimeAtCurrentRP = value;
            }
        }


        public string LastReadPoint
        {
            get
            {
                return _lastReadPoint;
            }
            set
            {
                if (_lastReadPoint == value)
                    return;
                _lastReadPoint = value;
            }
        }
        public DateTime SeenTimeAtLastRP
        {
            get
            {
                return _seenTimeAtLastRP;
            }
            set
            {
                if (_seenTimeAtLastRP == value)
                    return;
                _seenTimeAtLastRP = value;
            }
        }

        public DateTime LastSeentime
        {
            get
            {
                return _lastSeentime;
            }
            set
            {
                if (_lastSeentime == value)
                    return;
                _lastSeentime = value;
            }
        }

        public TripState TripState
        {
            get
            {
                return _tripState;
            }
            set
            {
                if (_tripState == value)
                    return;
                _tripState = value;
            }
        }

        public TripStatus TripStatus { get; set; }
        public string Remarks
        {
            get
            {
                return _remarks;
            }
            set
            {
                if (_remarks == value)
                    return;
                _remarks = value;
            }
        }
        public int CurrentSeqNo
        {
            get
            {
                return _currentSeqNo;
            }
            set
            {
                if (_currentSeqNo == value)
                    return;
                _currentSeqNo = value;
            }
        }
    }

     public enum TripState
    {
        STARTED,
        COMPLETED,
        VIOLATED,
        MOVED,
        SEEN
    }

    public enum TripStatus
    {
        STARTED,
        COMPLETED,
        IN_TRANSIT,
    }

}
