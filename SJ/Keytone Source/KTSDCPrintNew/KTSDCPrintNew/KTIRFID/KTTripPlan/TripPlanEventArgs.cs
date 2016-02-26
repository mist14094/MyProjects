using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KTone.Core.KTIRFID
{
    [Serializable]
    public class TripPlanEventArgs : EventArgs
    {
        KTAssetDetails _asset;
        string _movedToReadPoint;
        string _tripState;
        DateTime _movedTime;
        bool _isManual = false;

        public TripPlanEventArgs(KTAssetDetails asset, string movedToReadPoint, string tripState, DateTime movedtime)
        {
            _asset = asset;
            _movedToReadPoint = movedToReadPoint;
            _tripState = tripState;
            _movedTime = movedtime;
        }

        public TripPlanEventArgs(KTAssetDetails asset, string movedToReadPoint, string tripState, DateTime movedtime, bool isManaul)
        {
            _asset = asset;
            _movedToReadPoint = movedToReadPoint;
            _tripState = tripState;
            _movedTime = movedtime;
            _isManual = isManaul;
        }

        public bool IsManual
        {
            get
            {
                return _isManual;
            }
            set
            {
                if (_isManual == value)
                    return;
                _isManual = value;
            }
        }

        public KTAssetDetails Asset
        {
            get
            {
                return _asset;
            }
        }
        public string MovedToReadPoint
        {
            get
            {
                return _movedToReadPoint;
            }
        }
        public string TripState
        {
            get
            {
                return _tripState;
            }
        }
        public DateTime MovedTime
        {
            get
            {
                return _movedTime;
            }

        }

    }

    [Serializable]
    public class TripAlertEventArgs : EventArgs
    {
        TripAlertType _alertType;
        KTAssetDetails _asset;
        string _readPointName;
        DateTime _timestamp;
        string _remarks;

        public TripAlertEventArgs(TripAlertType alertType, KTAssetDetails asset, string readPointName, DateTime timestamp, string remark)
        {
            _alertType = alertType;
            _asset = asset;
            _readPointName = readPointName;
            _timestamp = timestamp;
            _remarks = remark;
        }

        public TripAlertType AlertType
        {
            get
            {
                return _alertType;
            }
        }
        public KTAssetDetails Asset
        {
            get
            {
                return _asset;
            }
        }
        public string ReadPointName
        {
            get
            {
                return _readPointName;
            }
        }
        public DateTime Timestamp
        {
            get
            {
                return _timestamp;
            }
            
        }
        public string Remarks
        {
            get
            {
                return _remarks;
            }
        }

    }

    public enum TripAlertType
    {
        MissedTerminal,
        IncorrectRoute,
        ImproperStartingPoint,
        MoreTimeSpentAtTerminal,
        TimeDelayBetweenTerminals,
        UnknownAsset
    }
}
