using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace KTone.Core.KTIRFID
{
    [Serializable]
    public class ActiveWaveTag : RFIDTag, ICloneable
    {
        #region Attributes

        private const int MAX_DATA = 255;
        private uint id;
        private string uniqueId;
        private byte awTagType;
        private byte version;
        private AWTagStatus tagStatus;
        private AWTagTemp tagTemp;
        private byte timeInField;
        private byte groupCount;
        private ushort resendTime;
        private ushort resendTimeType;
        private byte[] data = new byte[MAX_DATA];
        private ushort dataLength;
        private ushort assignedReader;
        private ushort selectType;
        private ushort awReaderId;
        private ushort fGenId;
        private short rssiValue;

        //If the RSSI of the tag is more than strong RSSI threshold of the reader,
        //strongRSSI  will be true i.e. Tag is in reader field for sure
        private bool strongRSSI = false;

        private DateTime strongRSSITime = DateTime.MinValue;

        //lstTagHistory : Maintains the Tag RSSI history 
        private static byte maxTagHistoryCnt = 6;

        private List<AWTagHistory> lstTagHistory = new List<AWTagHistory>(maxTagHistoryCnt);

        #endregion Attributes

        #region Constructor
        /// <summary>
        /// ActiveWaveTag
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tagType"></param>
        /// <param name="version"></param>
        /// <param name="tagStatus"></param>
        /// <param name="tagTemp"></param>
        /// <param name="timeInField"></param>
        /// <param name="groupCount"></param>
        /// <param name="resendTime"></param>
        /// <param name="resendTimeType"></param>
        /// <param name="data"></param>
        /// <param name="dataLength"></param>
        /// <param name="assignedReader"></param>
        /// <param name="selectType"></param>
        public ActiveWaveTag(uint id, byte tagType,byte version,AWTagStatus tagStatus,AWTagTemp tagTemp,
            byte timeInField, byte groupCount, ushort resendTime, ushort resendTimeType, byte[] data,
            ushort dataLength, ushort assignedReader, ushort selectType, ushort readerId, ushort fGenId, short rssiValue)
        {
            this.id = id;
            this.uniqueId = string.Format("AW-{0:D5}", id);
            this.awTagType = tagType;
            this.version = version;
            this.tagStatus = tagStatus;
            this.tagTemp = tagTemp;
            this.timeInField = timeInField;
            this.groupCount = groupCount;
            this.resendTime = resendTime;
            this.resendTimeType = resendTimeType;
            this.data = data;
            this.dataLength = dataLength;
            this.assignedReader = assignedReader;
            this.selectType = selectType;
            this.rssiValue = rssiValue;
            this.awReaderId = readerId;
            this.fGenId = fGenId;

            this.canWrite = true;
            this.canRead = true;
            this.isProprietary = true;
            this.LastSeenTime = DateTime.Now.ToString();
            this.vendorName = "activeWAVE";
            this.AntennaId = "1";
            this.AntennaName = "DefaultAntenna";
            this.TagSN = ASCIIEncoding.ASCII.GetBytes(this.uniqueId);
            this.LastSeenTime = DateTime.Now.ToString();
        }

        public ActiveWaveTag(uint id, byte tagType, byte[] data)
        {
            this.id = id;
            this.uniqueId = string.Format("AW-{0:D5}", id);
            this.awTagType = tagType;
            this.tagDataArray = data;
            this.LastSeenTime = DateTime.Now.ToString();
        }

        public ActiveWaveTag(uint id, ushort readerId, byte tagType,bool tamperSwitch,bool enabled)
        {
            this.id = id;
            this.uniqueId = string.Format("AW-{0:D5}", id);
            this.awReaderId = readerId;
            this.awTagType = tagType;
            this.tagStatus = new AWTagStatus(false,tamperSwitch,false,false,false,enabled,tagType);
            this.LastSeenTime = DateTime.Now.ToString();
            this.tagDataArray = null;
        }

        public ActiveWaveTag(uint id, ushort readerId, byte tagType, byte[] data, short rssiValue)
        {
            this.id = id;
            this.awTagType = tagType;
            this.tagDataArray = data;
            this.uniqueId = string.Format("AW-{0:D5}", id);
            this.awReaderId = readerId;
            this.tagStatus = new AWTagStatus(false, false, false, false, false, true, 1);
            this.rssiValue = rssiValue;
            this.LastSeenTime = DateTime.Now.ToString();
        }

        #endregion Constructor

        #region Properties
        public uint Id
        {
            get { return this.id; }
        }
        public byte AWTagType
        {
            get { return this.awTagType; }
        }
        public byte Version
        {
            get { return this.version; }
        }
        public AWTagStatus TagStatus
        {
            get { return this.tagStatus; }
        }
        public AWTagTemp TagTemp
        {
            get { return this.tagTemp; }
        }
        public byte TimeInField
        {
            get { return this.timeInField; }
        }
        public byte GroupCount
        {
            get { return this.groupCount; }
        }
        public ushort ResendTime
        {
            get { return this.resendTime; }
        }
        public ushort ResendTimeType
        {
            get { return this.resendTimeType; }
        }
        public byte[] Data
        {
            get { return this.data; }
        }
        public ushort DataLength
        {
            get { return this.dataLength; }
        }
        public ushort AssignedReader
        {
            get { return this.assignedReader; }
        }
        public ushort SelectType
        {
            get { return this.selectType; }
        }
        public ushort AWReaderID
        {
            get { return this.awReaderId; }
        }
        public ushort FGenID
        {
            get 
            { 
                return this.fGenId; 
            }
            set 
            { 
                this.fGenId = value; 
            }
        }
        public short RSSI
        {
            get { return this.rssiValue; }
        }
        public string UniqueId
        {
            get
            {
                return this.uniqueId;
            }
        }
        /// <summary>
        /// Gets a string continaing reader id and FGen id
        /// </summary>
        public string ReaderFGenId
        {
            get
            {
                return string.Format("{0}-FGen{1}", this.ReaderId, this.fGenId);
            }
        }
        public short AverageRSSIHistory
        {
            get
            {
                return CalculateAverageHistoryRSSI();
            }
        }

        public bool StrongRSSI
        {
            set { this.strongRSSI = value; }
            get { return this.strongRSSI; }
        }

        public DateTime StrongRSSITime
        {
            get { return this.strongRSSITime; }
            set { this.strongRSSITime = value; }
        }
        public List<AWTagHistory> TagHistory
        {
            get { return this.lstTagHistory; }
            set { this.lstTagHistory = value; }
        }

        public static byte MaxTagHistoryCnt
        {
            get { return maxTagHistoryCnt; }
            set { maxTagHistoryCnt = value; }

        }

        #endregion Properties

        #region Public Methods
        /// <summary>
        /// Modify attributes of current object(this) using input parameter tag
        /// </summary>
        /// <param name="tag"></param>
        public override void UpdateTag(IRFIDTag tag)
        {
            ActiveWaveTag awTag = (ActiveWaveTag)tag;
            this.rssiValue = awTag.rssiValue;
        }

        public void AddTagHistory(AWTagHistory awInfo)
        {
            if (this.lstTagHistory.Count >= maxTagHistoryCnt)
            {
                this.lstTagHistory.RemoveAt(0);
            }
            this.lstTagHistory.Add(awInfo);

        }

        public string ToShortString()
        {
            return string.Format("{0}:{1} {2}", this.UniqueId, this.ReaderFGenId, this.LastSeenTime);
        }

        public static uint ParseTagUniqueId(string uniqueId)
        {
            try
            {
                uint id = 0;
                //uniqueId format should be = string.Format("AW-{0:D5}", id);
                uniqueId = uniqueId.Remove(0, 3).TrimStart(new char[]{'0'});
                id = Convert.ToUInt32(uniqueId);
                return id;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Invalid tag Id " + uniqueId, ex);
            }
        }
        #endregion Public Methods

        #region Private Methods

        private short CalculateAverageHistoryRSSI()
        {
            if (lstTagHistory == null || lstTagHistory.Count == 0)
                return this.RSSI;

            if (lstTagHistory.Count == 1)
                return lstTagHistory[0].RSSI;

            byte avgRSSI = 0;

            if (lstTagHistory.Count < 2)
            {
                int rssiTotal = 0;
                foreach (AWTagHistory rssiHistory in lstTagHistory)
                {
                    rssiTotal += rssiHistory.RSSI;

                }
                avgRSSI = (byte)(rssiTotal / lstTagHistory.Count);
            }
            else
            {
                int totalRSSIFirst = 0;
                int totalRSSISecond = 0;
                byte avgRSSIFirst = 0;
                byte avgRSSISecond = 0;

                float factorFirst = 0.9F;
                float factorSecond = 1.1F;
                byte count = (byte)(lstTagHistory.Count / 2);
                bool listCountEven = false;
                int middleIndex = 0;
                if (lstTagHistory.Count % 2 == 0)
                {
                    listCountEven = true;
                    middleIndex = lstTagHistory.Count / 2;
                }
                else
                {
                    listCountEven = false;
                    middleIndex = (lstTagHistory.Count / 2) + 1;
                }


                for (int i = 0; i < lstTagHistory.Count / 2; i++)
                    totalRSSIFirst += lstTagHistory[i].RSSI;

                for (int i = middleIndex; i < lstTagHistory.Count; i++)
                    totalRSSISecond += lstTagHistory[i].RSSI;

                avgRSSIFirst = (byte)(totalRSSIFirst / count);
                avgRSSIFirst = (byte)(factorFirst * avgRSSIFirst);

                avgRSSISecond = (byte)(totalRSSISecond / count);
                avgRSSISecond = (byte)(factorSecond * avgRSSISecond);

                if (listCountEven)
                {
                    avgRSSI = (byte)((avgRSSIFirst + avgRSSISecond) / 2);
                }
                else
                {
                    avgRSSI = (byte)((avgRSSIFirst + lstTagHistory[middleIndex - 1].RSSI + avgRSSISecond) / 3);
                }
            }

            return avgRSSI;
        }

        #endregion Private Methods

        #region ICloneable Members

        public object Clone()
        {
            MemoryStream ms = new MemoryStream();

            BinaryFormatter bf = new BinaryFormatter();

            bf.Serialize(ms, this);

            ms.Position = 0;

            object obj = bf.Deserialize(ms);

            ms.Close();

            return obj;
        }

        /// <summary>
        ///  Make a deep copy
        /// </summary>
        /// <returns></returns>
        public ActiveWaveTag CloneTag()
        {
            return (ActiveWaveTag)Clone();
        }

        #endregion
    }

    [Serializable]
    public class AWTagStatus
    {
        #region Attributes

         private bool batteryLow;
         private bool tamperSwitch;
         private bool continuousField;
         private bool bi_directional;
         private bool batteryRechargeable;
         private bool enabled;
         private byte type;

        #endregion Attributes

        #region Constructor
        /// <summary>
         /// AWTagStatus
        /// </summary>
        /// <param name="batteryLow"></param>
        /// <param name="tamperSwitch"></param>
        /// <param name="continuousField"></param>
        /// <param name="bi_directional"></param>
        /// <param name="batteryRechargeable"></param>
        /// <param name="enabled"></param>
        /// <param name="type"></param>
        public AWTagStatus( bool batteryLow,bool tamperSwitch,bool continuousField,
                            bool bi_directional,bool batteryRechargeable,bool enabled,byte type)
        {
            this.batteryLow = batteryLow;
            this.tamperSwitch = tamperSwitch;
            this.continuousField = continuousField;
            this.bi_directional = bi_directional;
            this.batteryRechargeable = batteryRechargeable;
            this.enabled = enabled;
            this.type = type;
        }
        #endregion Constructor

        #region Properties
        public bool BatteryLow
        {
            get { return this.batteryLow; }
        }
        public bool TamperSwitch
        {
            get { return this.tamperSwitch; }
        }
        public bool ContinuousField
        {
            get { return this.continuousField; }
        }
        public bool Bi_directional
        {
            get { return this.bi_directional; }
        }
        public bool BatteryRechargeable
        {
            get { return this.batteryRechargeable; }
        }
        public bool Enabled
        {
            get { return this.enabled; }
        }
        public byte Type
        {
            get { return this.type; }
        }
        #endregion Properties
    }

    [Serializable]
    public class AWTagTemp
    {
        #region Attributes

        private bool rptUnderLowerLimit;    // report under lower limit
        private bool rptOverUpperLimit;     // report over upper limit
        private bool rptPeriodicRead;       // report with each periodic read
        //private bool enableTempLogging;     // enable temperature logging
        // private bool logging;               // reports if the tag is logging temperature
        private ushort numReadAve;          // number of reads per average
        //private ushort wrapAround;          // temp logging will warp-around if memory full
        private ushort periodicRptTime;     // periodic report time
        private ushort periodicTimeType;    // periodic report time type
        private byte status;                // normal; low or high
        private float lowerLimitTemp;       // tag temperature lower limit
        private float upperLimitTemp;       // tag temperature upper limit
        private float temperature;          // tag temperature value

        #endregion Attributes

        #region Constructor
        public AWTagTemp()
        {
        }

        /// <summary>
        /// AWTagTemp
        /// </summary>
        /// <param name="rptUnderLowerLimit"></param>
        /// <param name="rptOverUpperLimit"></param>
        /// <param name="rptPeriodicRead"></param>
        /// <param name="numReadAve"></param>
        /// <param name="wrapAround"></param>
        //   /// <param name="periodicRptTime"></param>
        /// <param name="periodicTimeType"></param>
        /// <param name="status"></param>
        /// <param name="lowerLimitTemp"></param>
        /// <param name="upperLimitTemp"></param>
        /// <param name="temperature"></param>
        public AWTagTemp(bool rptUnderLowerLimit, bool rptOverUpperLimit, bool rptPeriodicRead,
                         ushort numReadAve, ushort periodicRptTime, ushort periodicTimeType, byte status,
            float lowerLimitTemp, float upperLimitTemp, float temperature)
        {
            this.rptUnderLowerLimit = rptUnderLowerLimit;
            this.rptOverUpperLimit = rptOverUpperLimit;
            this.rptPeriodicRead = rptPeriodicRead;
            this.numReadAve = numReadAve;
            //this.wrapAround = wrapAround;
            this.periodicRptTime = periodicRptTime;
            this.periodicTimeType = periodicTimeType;
            this.status = status;
            this.lowerLimitTemp = lowerLimitTemp;
            this.upperLimitTemp = upperLimitTemp;
            this.temperature = temperature;
        }
        #endregion Constructor

        #region Properties
        public bool RptUnderLowerLimit
        {
            get { return this.rptUnderLowerLimit; }
        }
        public bool RptOverUpperLimit
        {
            get { return this.rptOverUpperLimit; }
        }
        public bool RptPeriodicRead
        {
            get { return this.rptPeriodicRead; }
        }
        //public bool EnableTempLogging
        //{
        //    get { return this.enableTempLogging; }
        //}
        //public bool Logging
        //{
        //    get { return this.logging; }
        //}  
        public ushort NumReadAve
        {
            get { return this.numReadAve; }
        }
        //public ushort WrapAround
        //{
        //    get { return this.wrapAround; }
        //}
        public ushort PeriodicRptTime
        {
            get { return this.periodicRptTime; }
        }
        public ushort PeriodicTimeType
        {
            get { return this.periodicTimeType; }
        }
        public byte Status
        {
            get { return this.status; }
        }
        public float LowerLimitTemp
        {
            get { return this.lowerLimitTemp; }
        }
        public float UpperLimitTemp
        {
            get { return this.upperLimitTemp; }
        }
        public float Temperature
        {
            get { return this.temperature; }
        }
        #endregion Properties
    }

    [Serializable]
    public struct AWTagHistory
    {
        public short RSSI;
        public long LastReadTicks;
        public ushort FGenID;

        public AWTagHistory(ActiveWaveTag tag)
        {
            this.RSSI = tag.RSSI;
            this.LastReadTicks = DateTime.Parse(tag.LastSeenTime).Ticks;
            this.FGenID = tag.FGenID;
        }
    }
}
