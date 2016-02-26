using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace KTone.Core.KTIRFID
{
    /// <summary>
    /// Tag model series
    /// </summary>
    public enum WTTagSeries
    {
        /// <summary>
        /// Tag for L series reader.
        /// </summary>
        LSeries,

        /// <summary>
        /// Tag for W series reader.
        /// </summary>
        WSeries
    }


	/// <summary>
	/// Defines the tag model set on the wavetrend tag
	/// </summary>
	public enum TagModel
	{
        /// <summary>
        /// L series TagModelTG 501
        /// </summary>
        LTG501,

        /// <summary>
        /// W Series Tag Model W-TwG100
        /// </summary>
		WTG100 = 0x0A,
        /// <summary>
        /// W Series Tag Model W-TG501
        /// </summary>
        WTG501 = 0x33,
        /// <summary>
        /// W Series Tag Model W-TG700
        /// </summary>
        WTG700 = 0x46,
        /// <summary>
        /// W Series Tag Model W-TG800
        /// </summary>
        WTG800 = 0x50,
        /// <summary>
        /// W Series Tag Model W-TG801
        /// </summary>
        WTG801 = 0x51,
        /// <summary>
        /// W Series Tag Model W-TG850
        /// </summary>
        WTG850 = 0x55,
        /// <summary>
        /// W Series Tag Model W-TG1000
        /// </summary>
        WTG1000 = 0x64,
        /// <summary>
        /// W Series Tag Model W-TG1200
        /// </summary>
        WTG1200 = 0x78
         
	}

	/// <summary>
	/// Defines the tag type of wavetrend tag
	/// </summary>
	public enum WTTagType
	{
        /// <summary>
        /// FUSED
        /// </summary>
		FUSED			=0x33,
        /// <summary>
        /// Not Fused
        /// </summary>
		NOTFUSED		=0x30
	}

	/// <summary>
	/// Defines the reed status of wavetrend tag
	/// </summary>
	public enum ReedStatus
	{
        /// <summary>
        /// Open
        /// </summary>
		OPEN			=0x00,
        /// <summary>
        /// Close
        /// </summary>
		CLOSE			=0x01
	}

	/// <summary>
	/// Defines the repeat rate of wavetrend Lseries tag
	/// </summary>
	public enum RepeatRateLSeries
	{
		/// <summary>
		/// Reapeat Rate 30 seconds
		/// </summary>
		Repeat_Rate_30sec	=0x30,

		/// <summary>
		/// Reapeat Rate 1 second
		/// </summary>
		Repeat_Rate_1_5sec	=0x31,

		/// <summary>
		/// Reapeat Rate 0.8 second
		/// </summary>
		Repeat_Rate_0_8sec	=0x32,

		/// <summary>
		/// Reapeat Rate 0.4 second
		/// </summary>
		Repeat_Rate_0_4sec	=0x33,

		/// <summary>
		/// Reapeat Rate 15 second
		/// </summary>
		Repeat_Rate_15sec	=0x20
	}
	/// <summary>
	/// Defines the repeat rate of wavetrend Wseries tag
	/// </summary>
	public enum RepeatRateWSeries
	{
		/// <summary>
		/// Reapeat Rate 0.4 second
		/// </summary>
		Repeat_Rate_0_4sec	=0x01,

		/// <summary>
		/// Reapeat Rate 0.8 second
		/// </summary>
		Repeat_Rate_0_8sec	=0x02,

		/// <summary>
		/// Reapeat Rate 1 second
		/// </summary>
		Repeat_Rate_1_0sec	=0x03,

		/// <summary>
		/// Reapeat Rate 1.5 second
		/// </summary>
		Repeat_Rate_1_5sec	=0x04,

		/// <summary>
		/// Reapeat Rate 2 seconds
		/// </summary>
		Repeat_Rate_2sec	=0x05,

		/// <summary>
		/// Reapeat Rate 5 seconds
		/// </summary>
		Repeat_Rate_5sec	=0x06,

		/// <summary>
		/// Reapeat Rate 10 seconds
		/// </summary>
		Repeat_Rate_10sec	=0x07,

		/// <summary>
		/// Reapeat Rate 15 seconds
		/// </summary>
		Repeat_Rate_15sec	=0x08,

		/// <summary>
		/// Reapeat Rate 20 seconds
		/// </summary>
		Repeat_Rate_20sec	=0x09,

		/// <summary>
		/// Reapeat Rate 30 seconds
		/// </summary>
		Repeat_Rate_30sec	=0x0A,

		/// <summary>
		/// Reapeat Rate 45 seconds
		/// </summary>
		Repeat_Rate_45sec	=0x0B,

		/// <summary>
		/// Reapeat Rate 1 minute
		/// </summary>
		Repeat_Rate_1min	=0x0C,

		/// <summary>
        /// Reapeat Rate 1.5 minutes
		/// </summary>
		Repeat_Rate_1_5min	=0x0D,

		/// <summary>
        /// Reapeat Rate 2 minutes
		/// </summary>
		Repeat_Rate_2min	=0x0E,

		/// <summary>
        /// Reapeat Rate 3 minutes
		/// </summary>
		Repeat_Rate_3min	=0x0F,

		/// <summary>
        /// Reapeat Rate 5 minutes
		/// </summary>
		Repeat_Rate_5min	=0x10,

		/// <summary>
		/// Reapeat Rate 10 minutes
		/// </summary>
		Repeat_Rate_10min	=0x11
	}
	
	/// <summary>
	/// Defines the alarm type of wavetrend tag
	/// </summary>
	public enum LTagAlarm
	{
		/// <summary>
		/// No Alarm
		/// </summary>
		NoAlarm =  0x50,

		/// <summary>
		/// Alarm present
		/// </summary>
		Alarm	 =	0x51
	}

	/// <summary>
	/// Defines the alarm type of wavetrend tag
	/// </summary>
	public enum WTagAlarm
	{
		/// <summary>
		/// No Alarm
		/// </summary>
		NoAlarm =  0x0,

		/// <summary>
		/// Alarm present
		/// </summary>
		Alarm	 =	0x1
	}
	
	/// <summary>
	/// Defines the class types of wavetrend W series tag
	/// </summary>
	public enum WT_WTagClassTypes
	{
        /// <summary>
        /// if tag other than w series
        /// </summary>
        NA,

        /// <summary>
        /// Class Type 1
        /// </summary>
		ClassType1 = 0,
        /// <summary>
        /// Class Type 2
        /// </summary>
		ClassType2 = 0x1,
        /// <summary>
        /// Class Type 3
        /// </summary>
		ClassType3 = 0x2,
        /// <summary>
        /// Class Type 4
        /// </summary>
		ClassType4 = 0x3,
        /// <summary>
        /// Class Type 5
        /// </summary>
		ClassType5 = 0x4,
        /// <summary>
        /// Class Type 6
        /// </summary>
		ClassType6 = 0x5,
        /// <summary>
        /// Class Type 7
        /// </summary>
		ClassType7 = 0x6,
        /// <summary>
        /// Class Type 8
        /// </summary>
		ClassType8 = 0x7,
		

	}

	/// <summary>
	/// Defines the status of tag seen in Auto polling mode with 
	/// respect to previous polling cycle.
	/// </summary>
    //public enum TagStatus
    //{
        
    //    /// <summary>
    //    /// Tags newly seen in the current cycle
    //    /// </summary>
    //    ADDED,

    //    /// <summary>
    //    /// Tags  seen in the previous cycle but not seen in the current cycle
    //    /// </summary>
    //    REMOVED,

    //    /// <summary>
    //    /// Tags  seen in the previous cycle and current cycle.
    //    /// </summary>
    //    EXISTING
    //}

	/// <summary>
	/// Defines Tags Polling mode status 
	/// </summary>
	public enum PollEnum
	{
        /// <summary>
        /// Not Polling
        /// </summary>
		Nopoll	= 0x00,
        /// <summary>
        ///  Polling
        /// </summary>
		Poll	= 0x01
	}


    /// <summary>
    /// Defines Latch Mode
    /// </summary>
    public enum LatchModeEnum
    {
        /// <summary>
        /// Latched bit is 0
        /// </summary>
        Unlatch = 0x00,
        /// <summary>
        /// Latched bit is 1
        /// </summary>
        Latch = 0x01
    }

    /// <summary>
    /// Defines Scramble bit
    /// </summary>
	public enum ScrambleEnum
	{
        /// <summary>
        /// Scramble bit is 0
        /// </summary>
		Disabled = 0x00,
        /// <summary>
        /// Scramble bit is 1
        /// </summary>
		Enabled = 0x01
	}


    /// <summary>
    /// Defines Tag Sleep/Wake bit
    /// </summary>
    public enum TagWakeState
    {
        /// <summary>
        /// Tag beacon transmission disable
        /// </summary>
        Sleep = 0x00,
        /// <summary>
        /// Tag beacon transmission enabled
        /// </summary>
        Wake = 0x01
    }

	/// <summary>
	/// Defines Tag Mode of tag
	/// </summary>
	public enum TagModeEnum
	{
        /// <summary>
        /// Tag Mode is Reconfigure
        /// </summary>
		Reconfigure = 0x00,
        /// <summary>
        /// Tag Mode is Non Reconfigure
        /// </summary>
		Nonreconfigure = 0x01
	}
    /// <summary>
    /// Defines Tamper Counter/Movement Counter TX bit
    /// </summary>
	public enum TXEnum
	{
        /// <summary>
        /// Tamper Counter or Movement Counter TX bit is 0
        /// </summary>
		NOTTX	=	0x00,
        /// <summary>
        /// Tamper Counter or Movement Counter TX bit is 1
        /// </summary>
		TX		=	0x01
	}
	/// <summary>
	/// Holds the data read from a  Wavetrend  tag and exposes 
	/// the tag attributes.
	/// </summary>
    [Serializable]
	public class WTTag : RFIDTag, ICloneable
	{

        #region attributes

        /// <summary>
        /// Time stamp of last time tag read.
        /// </summary>
        protected DateTime m_LastReadTime;
        /// <summary>
        /// Node Id through which tag is read.
        /// </summary>
        protected int m_nodeId = 0;

        /// <summary>
        /// PUK COde
        /// </summary>
        protected uint m_PUKCode = 0;

        /// <summary>
        /// Tag ID
        /// </summary>
        protected uint m_TagId;

        /// <summary>
        /// Site Code
        /// </summary>
        protected int m_SiteCode;

        /// <summary>
        /// Age
        /// </summary>
        protected uint m_Age;

        /// <summary>
        /// RSSI value
        /// </summary>
        protected byte m_RSSI;

        /// <summary>
        /// 
        /// </summary>
        protected bool m_InMotion = false;

        /// <summary>
        /// 
        /// </summary>
        protected bool m_IsTampered = false;
        /// <summary>
        /// 
        /// </summary>
        protected bool m_IsCloned = false;
        /// <summary>
        /// Repeat rate in secs
        /// </summary>
        protected double m_strRepeatRate;
        
        /// <summary>
        /// Tamper Counter
        /// </summary>
        protected byte m_TamperCounter = 0;
        /// <summary>
        /// Movement Counter
        /// </summary>
        protected byte m_MovementCounter = 0;

        /// <summary>
        /// tags that support tamper monitoring
        /// </summary>
        protected bool m_CanSupportTamperCount = false;

        /// <summary>
        /// tags that support movement count
        /// </summary>
        protected bool m_CanSupportMovementCount = false;

        /// <summary>
        /// userData
        /// </summary>
        protected byte[] m_UserData = null;

        /// <summary>
        /// Polling
        /// </summary>
        protected TagWakeState m_TagWakeState;

 
        /// <summary>
        /// can reprogram tag
        /// </summary>
        protected bool m_CanReprogram;

        /// <summary>
        /// TagSeries L or W
        /// </summary>
        protected WTTagSeries m_TagSeries;

        /// <summary>
        /// Tag Protocol
        /// </summary>
        protected WT_WTagClassTypes m_TagProtocol;

        /// <summary>
        /// TagModel
        /// </summary>
        protected TagModel m_TagModel;

        ///// <summary>
        ///// Open/Close
        ///// </summary>
        //protected string m_ReadStatus;

        /// <summary>
        /// Alarmable
        /// </summary>
        protected bool m_CanAlarm;
        /// <summary>
        /// ReadStatus
        /// </summary>
        protected ReedStatus m_ReedStatus;

        //Used only in EPCWT filter
        private byte averageRSSI = 0;

        //If the RSSI of the tag is more than strong RSSI threshold of the reader,
        //strongRSSI  will be true i.e. Tag is in reader field for sure
        private bool strongRSSI = false;

        private DateTime strongRSSITime = DateTime.MinValue;

        //lstTagHistory : Maintains the Tag RSSI history 
        private static byte maxTagHistoryCnt = 6;

        private List<WTTagHistory> lstTagHistory = new List<WTTagHistory>(maxTagHistoryCnt);

        private byte averageRSSIHistory = 0;
        

        /// <summary>
        /// unique Id for the Tag
        /// </summary>
        protected string m_WTUniqueId = "UNKNOWN-TAG-ID";

        /// <summary>
        /// repeatRate enum value
        /// </summary>
        protected byte m_RepeatRate = 0;
        //protected Queue<DateTime> m_SeenTime = new Queue<DateTime>(20);
        #endregion attributes

        
        #region Properties

        /// <summary>
        /// Gets a unique ID for the tag
        /// </summary>
        public string UniqueID
        {
            get { return m_WTUniqueId; }
        }


        /// <summary>
        ///  Last Time Tag read
        /// </summary>
        public DateTime LastReadTime
        {
            get
            {
                return m_LastReadTime;
            }
            set
            {
                m_LastReadTime = value;
                LastSeenTime = value.ToString();
            }
        }

        /// <summary>
        /// Node id of reader by which tag has been read.
        /// </summary>
        /// 
        public int NodeId
        {
            get
            {
                return m_nodeId;
            }
            set
            {
                m_nodeId = value;
            }
        }

        /// <summary>
        /// Gets PUK Code
        /// </summary>
        public uint PUKCode
        {
            get
            {
                return m_PUKCode;
            }
        }

        /// <summary>
        /// Tag Id of Tag
        /// </summary>
        public virtual uint TagID
        {
            get
            {
                return m_TagId;
            }
        }

        /// <summary>
        /// Site Code
        /// </summary>
        public int SiteCode
        {
            get
            {
                return m_SiteCode;
            }
        }
        /// <summary>
        /// Age of Tag
        /// </summary>
        public uint Age
        {
            get
            {
                return m_Age;
            }
        }
        /// <summary>
        ///  RSSI value of Tag
        /// </summary>
        public byte RSSI
        {
            get
            {
                return m_RSSI;
            }
        }

        /// <summary>
        /// Returns true if tags is in Motion.
        /// </summary>
        public bool InMotion
        {
            get
            {
                return m_InMotion;
            }
            set
            {
                m_InMotion = value;
            }
        }

        /// <summary>
        /// Returns true if tags is tampered.
        /// </summary>
        public bool IsTampered
        {
            get
            {
                return m_IsTampered;
            }
            set
            {
                m_IsTampered = value;
            }
        }

        /// <summary>
        /// Returns true if tags is cloned.
        /// </summary>
        public bool IsCloned
        {
            get
            {
                return m_IsCloned;
            }
            set
            {
                m_IsCloned = value;
            }
        }

        /// <summary>
        /// Repeat Rate in seconds
        /// </summary>
        public Double RepeatRate
        {
            get
            {
                return m_strRepeatRate;
            }
        }

        /// <summary>
        /// Gets the value which indicates if Tamper Counter is transmitted.
        /// </summary>
        public byte TamperCounter { get { return m_TamperCounter; } }
        /// <summary>
        /// Gets Movement Counter
        /// </summary>
        public byte MovementCounter { get { return m_MovementCounter; } }

        /// <summary>
        /// User Data
        /// </summary>
        public byte[] UserData
        {
            get
            {
                return m_UserData;
            }
        }

        /// <summary>
        /// tags support tamper count
        /// </summary>
        public bool CanSupportTamperCount
        {
            get { return m_CanSupportTamperCount; }
        }

        /// <summary>
        /// tags support movement count
        /// </summary>
        public bool CanSupportMovementCount
        {
            get { return m_CanSupportMovementCount; }
        }

        /// <summary>
        /// Gets Polling type.
        /// </summary>
        public TagWakeState TagWakeStatus { get { return m_TagWakeState; } }

        /// <summary>
        /// Alarm capable
        /// </summary>
        public bool CanAlarm
        {
            get { return m_CanAlarm; }
             
        }

        /// <summary>
        /// Reprogrammable
        /// </summary>
        public bool CanReprogram
        {
            get { return m_CanReprogram; }
        }

        /// <summary>
        /// L or W series
        /// </summary>
        public WTTagSeries TagSeries
        {
            get { return m_TagSeries; }
        }

        /// <summary>
        /// Specific model
        /// </summary>
        public TagModel TagModel
        {
            get { return m_TagModel; }
        }

        /// <summary>
        /// Tag Protocol
     /// </summary>
        public WT_WTagClassTypes TagProtocol
        {
            get { return m_TagProtocol; }
        }

        /// <summary>
        /// Reed status of tag
        /// </summary>
        public ReedStatus Reedstatus
        {
            get { return m_ReedStatus; }
        }

        ///// <summary>
        ///// RSSI History
        ///// </summary>
        //public Queue<byte> RSSIHistory
        //{
        //    get { return m_RSSIHistory; }

        //    set { m_RSSIHistory = value; }
        //}

        //Used only in EPCWT filter
        public byte AverageRSSI
        {
            set { this.averageRSSI = value; }
            get { return this.averageRSSI; }
        }

        public bool StrongRSSI
        {
            set { this.strongRSSI = value; }
            get { return this.strongRSSI; }
        }

        public DateTime StrongRSSITime
        {
            get { return this.strongRSSITime;}
            set { this.strongRSSITime = value;}
        }
        /// <summary>
        /// 
        /// </summary>
        public byte RepeatRateEnum
        {
            get { return this.m_RepeatRate; }
        }

        ///// <summary>s
        ///// 
        ///// </summary>
        //public Queue<DateTime> SeenTimeHistory
        //{
        //    get { return this.m_SeenTime; }
        //    set { this.m_SeenTime = value; }
        //}

        #endregion Properties

        protected WTTag()
        {
            this.canRead = true;
            this.isProprietary = true;
            this.vendorName = "Wavetrend";
        }

        protected WTTag(WTTag tag)
        {
            this.ReaderId = tag.ReaderId;
            this.ReaderName = tag.ReaderName;
            m_LastReadTime = tag.m_LastReadTime;
            m_nodeId = tag.m_nodeId;
            m_PUKCode = tag.m_PUKCode;
            m_TagId = tag.m_TagId;
            m_SiteCode = tag.m_SiteCode;
            m_Age = tag.m_Age;
            m_RSSI = tag.m_RSSI;
            m_InMotion = tag.m_InMotion;
            m_IsCloned = tag.m_IsCloned;
            m_IsTampered = tag.m_IsTampered;

            m_MovementCounter = tag.m_MovementCounter;
            m_TamperCounter = tag.m_TamperCounter;

            m_CanSupportMovementCount = tag.m_CanSupportMovementCount;
            m_CanSupportTamperCount = tag.m_CanSupportTamperCount;

            m_UserData = tag.UserData;
            m_TagWakeState = tag.m_TagWakeState;

            m_CanReprogram = tag.m_CanReprogram;

            m_ReedStatus = tag.m_ReedStatus;

            averageRSSI = tag.averageRSSI;
            strongRSSI = tag.strongRSSI;
            strongRSSITime = tag.strongRSSITime;

            lstTagHistory = new List<WTTagHistory>(tag.lstTagHistory);
            averageRSSIHistory = tag.averageRSSIHistory;

            m_RepeatRate = tag.m_RepeatRate;
            m_strRepeatRate = tag.m_strRepeatRate;
            this.TagReadCount = tag.TagReadCount;
            m_CanAlarm = tag.m_CanAlarm;
            m_TagSeries = tag.m_TagSeries;
            m_TagModel = tag.m_TagModel;
            m_TagProtocol = tag.m_TagProtocol;
            m_WTUniqueId = tag.m_WTUniqueId;

            this.canRead = true;
            this.isProprietary = true;
            this.vendorName = "Wavetrend";
        }

        /// <summary>
        /// returns System.string that represent current system.object
        /// </summary>
        /// <returns></returns>
		public override string ToString()
		{
			string str = string.Empty;
			str += //"TagStatus : " + m_TagStatus +
				   "TagReadCount : " + this.TagReadCount +
				   ",LastReadTime : "+ m_LastReadTime +
                   ",StrongRSSI : " + this.strongRSSI.ToString()+
                   ",StrongRSSITime : " + this.strongRSSITime.ToString(); 
			return str;
					
		}

        /// <summary>
        /// returns byte array in string 
        /// </summary>
        /// <param name="byteArr"></param>
        /// <returns></returns>
		protected string GetByteArrString(byte[] byteArr)
		{
         
			string str=string.Empty ;
			try
			{ 
				if(byteArr!=null && byteArr.Length > 0)
				{
					foreach(byte byt in byteArr)
					{						
						str += "0x"+ byt.ToString("X") +" ";
					}
					
				}
				
	
			}
			catch{}
			return str;
		}


        #region ICloneable Members
        /// <summary>
        /// Make a deep copy
        /// </summary>
        /// <returns></returns>
        public virtual object Clone()
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
        public virtual WTTag CloneTag()
        {
            return (WTTag)Clone();
        }
        #endregion


        #region Maintain tagHistory
        public static byte MaxTagHistoryCnt
        {
            get { return maxTagHistoryCnt; }
            set { maxTagHistoryCnt = value; }
 
        }

        /*
        public WTTagHistory[] TagHistory
        {
            get {return this.lstTagHistory.ToArray(); }
            set 
            {
                this.lstTagHistory.Clear();
                lstTagHistory.AddRange(value);

            }
        }*/

        public List<WTTagHistory> TagHistory
        {
            get { return this.lstTagHistory; }
            set { this.lstTagHistory = value; }
        }
               

        public void AddTagHistory(WTTagHistory wtInfo)
        {
            if (this.lstTagHistory.Count >= maxTagHistoryCnt)
            {
                this.lstTagHistory.RemoveAt(0);
            }
            this.lstTagHistory.Add(wtInfo);
            
        }



        private byte CalculateAverageHistoryRSSI()
        {
            if (lstTagHistory == null || lstTagHistory.Count == 0)
                return this.RSSI;

            if (lstTagHistory.Count == 1)
                return lstTagHistory[0].RSSI;

            #region Old code
/*
            int rssiTotal = 0;
            foreach (WTTagHistory rssiHistory in lstTagHistory)
            {
                rssiTotal += rssiHistory.RSSI;

            }
            byte avgRSSI = (byte)(rssiTotal / lstTagHistory.Count);
 * */
            #endregion Old code

            #region New Code
            
            byte avgRSSI = 0;

            if (lstTagHistory.Count < 2)
            {
                int rssiTotal = 0;
                foreach (WTTagHistory rssiHistory in lstTagHistory)
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

            #endregion New Code
            return avgRSSI;


        }

        public byte AverageRSSIHistory
        {
            get
            {
                averageRSSIHistory = CalculateAverageHistoryRSSI();
                return averageRSSIHistory;
            }
        }
        #endregion Maintain tagHistory

    }

	#region L-Series Tags
	
	/// <summary>
	/// Holds the data read from a  Wavetrend L-Series tag and exposes 
	/// the tag attributes.
	/// </summary>
    [Serializable]
	public class WT_LTag : WTTag
	{
		#region Attributes
		
		private WTTagType m_TagType;// = fused/notfused(reprogrammable)
		private short m_MoveCount;
		//private ReedStatus m_ReedStatus;
		private int m_ReedCount = 0;
		private LTagAlarm m_AlarmType;
		//private byte m_Interval;
		private byte m_counter;
    	#endregion Attributes


		#region Constructors
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="tagId">Tag Id</param>
		/// <param name="siteCode">Site Code</param>
		/// <param name="age">Age</param>
		/// <param name="rssi">RSSI</param>
		/// <param name="tagtype">Tag Type</param>
		/// <param name="moveCount">Move Count</param>
		/// <param name="alarmType">Alarm Type</param>
		/// <param name="counter">Counter</param>
		/// <param name="interval">Interval</param>
		public WT_LTag(uint tagId,int siteCode,uint age,byte rssi,byte tagtype,
			short moveCount,byte alarmType,byte counter,byte interval)
		{
			m_TagId				= tagId; 
			m_SiteCode			= siteCode;
			m_Age				= age;
			m_RSSI				= rssi;
			m_TagType			= (WTTagType)tagtype;
			m_MoveCount			= moveCount;
			m_AlarmType			= (LTagAlarm)alarmType;
			//m_Interval			= interval;
            m_RepeatRate = interval;
			//m_TagStatus			= TagStatus.ADDED;
            this.TagReadCount = 1;
			m_counter			= counter;
			GetReedCount();
			GetRepeatRate();
            m_CanAlarm = true;
            m_TagSeries = WTTagSeries.LSeries ;
            m_TagModel = TagModel.LTG501;
            m_TagProtocol = WT_WTagClassTypes.NA;

            m_WTUniqueId = String.Format("L-{0:00000000}-{1:0000000000}", m_SiteCode, m_TagId);
		}
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tag">Tag</param>
       
        public WT_LTag(WT_LTag tag)
            : base (tag)
        {
            
            m_TagType = tag.Tagtype;
            m_MoveCount = tag.m_MoveCount;
            m_ReedCount = tag.m_ReedCount;
            m_AlarmType = tag.m_AlarmType;
            m_counter = tag.m_counter;
          

            
        }

		#endregion Constructors

       

        /// <summary>
        /// returns System.string that represent current system.object
        /// </summary>
        /// <returns></returns>
		public override string ToString()
		{		
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			
			sb.Append(" L Tag :");
			sb.Append(" TagId : "+ m_TagId);			
			sb.Append(",SiteCode: "+	m_SiteCode);					
			sb.Append(",Age: "+ m_Age);						
			sb.Append(",RSSI: "+	m_RSSI);						
			sb.Append(",TagType: "+ m_TagType);		
			sb.Append(",MoveCount: "+ m_MoveCount);		
			sb.Append(",AlarmType: "+ m_AlarmType);		
			//sb.Append(",Interval: "+ m_Interval);		
            sb.Append(",Interval: " + m_RepeatRate);
			//sb.Append(",TagStatus: "+ m_TagStatus);		
            sb.Append(",TagReadCount: " + this.TagReadCount);	
			sb.Append(",Counter: "+	m_counter);		
			sb.Append(",ReedStatus: "+ m_ReedStatus);
			sb.Append(",ReedCount: "+ m_ReedCount);	
			sb.Append(",RepeatRate: "+ m_strRepeatRate);
			sb.Append(",");
			sb.Append(base.ToString()); 
			return  sb.ToString();
					
		}

		private void GetReedCount()
		{
            m_ReedCount = BitConverter.ToInt16(new Byte[] { Convert.ToByte(m_counter & 127), 0 }, 0);	

			m_ReedStatus = (ReedStatus)( (m_counter >> 7) & 0x01);
			
		}

		private void GetRepeatRate()
		{
			//RepeatRate rRate= (RepeatRate) m_Interval;
			switch((RepeatRateLSeries) m_RepeatRate)
			{
				case RepeatRateLSeries.Repeat_Rate_30sec:
					m_strRepeatRate = 30;
					break;
				case RepeatRateLSeries.Repeat_Rate_1_5sec:
					m_strRepeatRate = 1.5 ;
					break;
				case RepeatRateLSeries.Repeat_Rate_0_8sec:
					m_strRepeatRate = 0.8;
					break;
				case RepeatRateLSeries.Repeat_Rate_0_4sec:
					m_strRepeatRate = 0.4;
					break;
				case RepeatRateLSeries.Repeat_Rate_15sec:
					m_strRepeatRate = 15;
					break;
				default:
					m_strRepeatRate = 0;
					break;
			}
		}

		#region Properties
	
        /// <summary>
        /// Tag Type
        /// </summary>
		public WTTagType Tagtype
		{
			get{ return m_TagType; }
		}
        /// <summary>
        /// Move Count
        /// </summary>
		public int MoveCount
		{
			get{ return m_MoveCount; }
		}
        ///// <summary>
        ///// Reed status of tag
        ///// </summary>
        //public ReedStatus Reedstatus
        //{
        //    get{ return m_ReedStatus; }
        //}
        /// <summary>
        /// Read Count of tag
        /// </summary>
		public int ReedCount
		{
			get{ return m_ReedCount; }
		}
		/// <summary>
		/// Alarm type
		/// </summary>
		public LTagAlarm  Alarmtype
		{
			get{ return m_AlarmType; }
		}
      
       
		#endregion Properties

        #region ICloneable Members
        /// <summary>
        /// Make a deep copy
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            return new WT_LTag(this);
        }

        /// <summary>
        ///  Make a deep copy
        /// </summary>
        /// <returns></returns>
        public override WTTag CloneTag()
        {
            return (WTTag)Clone();
        }
        #endregion
	
	}
	#endregion L-Series Tags

	#region W-Series Tags

	/// <summary>
	/// Holds the data read from a  Wavetrend W-Series Class Type1  tag 
	/// and exposes the tag attributes.
	/// </summary>
    [Serializable]
	public class WT_WTagClassType1 : WTTag
	{		
       
        /// <summary>
        /// Tag Class Type
        /// </summary>
		protected WT_WTagClassTypes m_TagClassType = 0;
       

		//protected TagModel m_TagModel = 0;//W-TG501
    
        /// <summary>
		/// Data Length in byte
		/// </summary>
        protected byte m_byteDataLength = 0;
        /// <summary>
        /// Tag Class in byte
        /// </summary>
		protected byte m_byteTagClass = 0;
        /// <summary>
        ///  Repitition Rate 
        /// </summary>
		protected byte m_byteRepitition = 0;
        /// <summary>
        /// Is PUK Code Transmit
        /// </summary>
		protected bool m_PUKCodeTransmit = false;
        ///// <summary>
        ///// Polling
        ///// </summary>
        //protected TagWakeState m_TagWakeState;
        /// <summary>
        ///  Scramble
        /// </summary>
        protected LatchModeEnum m_latchMode;
        /// <summary>
        /// Tag Mode
        /// </summary>
		protected TagModeEnum m_TagMode;
        /// <summary>
        /// Tamper Counter TX
        /// </summary>
		protected TXEnum m_TamperCounterTX ;
        /// <summary>
        /// Movement Counter
        /// </summary>
		protected TXEnum m_MovementCounterTX;
        ///// <summary>
        ///// Reed Status
        ///// </summary>
        //protected ReedStatus m_ReedStatus = ReedStatus.OPEN ;
        /// <summary>
        /// Reed Alarm
        /// </summary>
		protected WTagAlarm m_ReedAlarm = WTagAlarm.NoAlarm;
        /// <summary>
        ///  Movement Alarm
        /// </summary>
		protected WTagAlarm m_MovementAlarm = WTagAlarm.NoAlarm;
        /// <summary>
        ///  Repeat Rate
        /// </summary>
		//protected RepeatRateWSeries m_RepeatRate = 0;
        
        /// <summary>
        /// Data Length
        /// </summary>
		protected int m_DataLength = 0;
       

		/// <summary>
		/// Construtor
		/// </summary>
		/// <param name="RSSIVal">RSSI</param>
		/// <param name="repeatRate">Repeat Rate</param>
		/// <param name="tagClassType">Tag Class Type</param>
		/// <param name="tagModel">Tag Model</param>
		/// <param name="PUKCode">PUK Code</param>
		/// <param name="tagClass">Tag Class</param>
		/// <param name="DataLength">Data Length</param>
        /// <param name="tamperCounter">Tamper Counter</param>
        /// <param name="movementCounter">Movement Counter</param>
		public WT_WTagClassType1(byte RSSIVal,byte repeatRate,
			WT_WTagClassTypes tagClassType,byte tagModel,uint PUKCode,byte tagClass, byte DataLength,byte tamperCounter, byte movementCounter)
		{

			m_RSSI = RSSIVal;
			m_byteRepitition = repeatRate;
			m_byteTagClass = tagClass;
			m_byteDataLength = DataLength;
			m_TagClassType = tagClassType;
			m_TagModel =  (TagModel)tagModel;
			m_PUKCode = PUKCode;
			//m_TagStatus			= TagStatus.ADDED;
            this.TagReadCount = 1;
            m_TamperCounter = tamperCounter;
            m_MovementCounter = movementCounter;
			ParseDataByte();
			GetRepeatRate();
           
            m_TagSeries = WTTagSeries.WSeries;
            //m_TagModel = TagModel.;
            //m_CanAlarm = true;
            m_TagProtocol = WT_WTagClassTypes.ClassType1;
            m_WTUniqueId = String.Format("W-NA-{0:0000000000}", m_PUKCode);
		}

        public WT_WTagClassType1(WT_WTagClassType1 tag)
            : base(tag)
        {
            m_byteDataLength = tag.m_byteDataLength;
            m_byteRepitition = tag.m_byteRepitition;
            m_byteTagClass = tag.m_byteTagClass;
            m_PUKCodeTransmit = tag.m_PUKCodeTransmit;
            m_latchMode = tag.m_latchMode;
            m_TagMode = tag.m_TagMode;
            m_TamperCounterTX = tag.m_TamperCounterTX;
            m_MovementCounterTX = tag.m_MovementCounterTX;
            m_ReedAlarm = tag.m_ReedAlarm;
            m_MovementAlarm = tag.m_MovementAlarm;
            m_DataLength = tag.m_DataLength;
        }
        /// <summary>
        /// returns System.string that represent current system.object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
		{			
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			
			sb.Append(" W Tag :" + m_TagClassType);
			sb.Append(",PUKCode :"+ m_PUKCode);
			sb.Append(",RSSI :"+m_RSSI);
			sb.Append(",TagModel :"+m_TagModel);
			sb.Append(",");
			sb.Append(GetTagSpecificDataSring());
			sb.Append(",");
			sb.Append(base.ToString()); 
			return  sb.ToString();
					
		}
        /// <summary>
        /// Returns Tag Data in String format
        /// </summary>
        /// <returns></returns>
		protected virtual string GetTagSpecificDataSring()
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			sb.Append(",TagMode: "+  m_TagMode);
			sb.Append(",PollMode : "+ m_TagWakeState);
			sb.Append(",Scramble : "+  m_latchMode);
			sb.Append(",TamperCounterTX: "+  m_TamperCounterTX) ;	
			sb.Append(",MovementCounterTX: "+  m_MovementCounterTX);
            sb.Append(",TamperCounter: " + m_TamperCounter);
            sb.Append(",MovementCounter: " + m_MovementCounter);
			sb.Append(",ReedStatus: "+  m_ReedStatus);
			sb.Append(",ReedAlarm  : "+ m_ReedAlarm );
			sb.Append(",MovementAlarm  : "+ m_MovementAlarm );
			sb.Append(",RepeatRate : "+  m_RepeatRate );
			sb.Append(",RepeatRate : "+ m_strRepeatRate);
			sb.Append(",DataLength: "+ m_DataLength);
			return  sb.ToString();
		}	

		#region Properties
	
        /// <summary>
        /// Gets Tag Class Type.
        /// </summary>
		public WT_WTagClassTypes TagClassType 
		{
			get
			{
				return m_TagClassType;
			}
		}
        ///// <summary>
        ///// Gets Tag Model.
        ///// </summary>
        //public TagModel TagModel 
        //{
        //    get
        //    {
        //        return m_TagModel;
        //    }
        //}
     
        /// <summary>
        /// Gets Data Length
        /// </summary>
		public int DataLength{get{return m_DataLength;} }
        /// <summary>
        /// Gets bool value, Is PUK CODE Exists
        /// </summary>
		public bool PUKCodeExist{get{return m_PUKCodeTransmit;}}
        ///// <summary>
        ///// Gets Polling type.
        ///// </summary>
        //public TagWakeState TagWakeStatus{ get{return m_TagWakeState;} }
        /// <summary>
        /// Gets Scramble type.
        /// </summary>
		public LatchModeEnum  LatchMode {get{return m_latchMode;}}
        /// <summary>
        /// Gets Tag Mode.
        /// </summary>
		public TagModeEnum TagMode {get{return m_TagMode;}}
        /// <summary>
        /// Gets the  value which indicates if Tamper Counter is transmitted.
        /// </summary>
		public TXEnum TamperCounterTX{get{return m_TamperCounterTX;}}
        /// <summary>
        /// Gets the value which indicates if the movement counter is transmitted.
        /// </summary>
		public TXEnum MovementCounterTX{get{return m_MovementCounterTX;}}
     
        // <summary>
        // Reed Status
        // </summary>
        //public ReedStatus Reedstatus{get{return m_ReedStatus;}}
        /// <summary>
        /// Reed Alarm
        /// </summary>
		public WTagAlarm ReedAlarm{get{return m_ReedAlarm;}}
        /// <summary>
        /// Movement Type.
        /// </summary>
		public WTagAlarm MovementType{get{return m_MovementAlarm;}}

        
      
		#endregion Properties

		#region Private Methods
			
		private void ParseDataByte()
		{
			//	m_ReedStatus = (ReedStatus)( m_Interval & 0x80);
			//Parsing Data Length byte
            m_DataLength = BitConverter.ToInt16(new Byte[] { Convert.ToByte((m_byteDataLength & 127)), 0 }, 0);
            m_PUKCodeTransmit = Convert.ToBoolean((m_byteDataLength >> 7) & 0x01);
			

			//Parsing Tag Repitition Byte
            m_RepeatRate = Convert.ToByte(m_byteRepitition & 0x1F);
            m_TagWakeState = (TagWakeState)Convert.ToInt16((m_byteRepitition >> 5) & 0x01);
            m_latchMode = (LatchModeEnum)Convert.ToInt16((m_byteRepitition >> 6) & 0x01);
            m_TagMode = (TagModeEnum)Convert.ToInt16((m_byteRepitition >> 7) & 0x01);

			//Parsing Tag Class Byte
            m_TamperCounterTX = (TXEnum)Convert.ToInt16((m_byteTagClass >> 3) & 0x1);
            m_MovementCounterTX = (TXEnum)Convert.ToInt16((m_byteTagClass >> 4) & 0x1);
            m_ReedStatus = (ReedStatus)Convert.ToInt16((m_byteTagClass >> 5) & 0x1);
            m_ReedAlarm = (WTagAlarm)Convert.ToInt16((m_byteTagClass >> 6) & 0x1);
            m_MovementAlarm = (WTagAlarm)Convert.ToInt16((m_byteTagClass >> 7) & 0x1);

			//Byte[] getDataLenBit = GetBits(m_byteDataLength,0,6);
			//m_DataLength = BitConverter.ToInt16 (

		}


		private void GetRepeatRate()
		{
            switch ((RepeatRateWSeries)m_RepeatRate)
			{
				case RepeatRateWSeries.Repeat_Rate_30sec:
					m_strRepeatRate = 30;
					break;
				case RepeatRateWSeries.Repeat_Rate_1_5sec:
					m_strRepeatRate = 1.5;
					break;
				case RepeatRateWSeries.Repeat_Rate_0_8sec:
					m_strRepeatRate = 0.8;
					break;
				case RepeatRateWSeries.Repeat_Rate_0_4sec:
					m_strRepeatRate = 0.4;
					break;
				case RepeatRateWSeries.Repeat_Rate_15sec:
					m_strRepeatRate = 15;
					break;
				case RepeatRateWSeries.Repeat_Rate_1_0sec:
					m_strRepeatRate = 1;
					break;
				case RepeatRateWSeries.Repeat_Rate_2sec:
					m_strRepeatRate = 2;
					break;
				case RepeatRateWSeries.Repeat_Rate_5sec:
					m_strRepeatRate = 5;
					break;
				case RepeatRateWSeries.Repeat_Rate_10sec:
					m_strRepeatRate = 10;
					break;
				case RepeatRateWSeries.Repeat_Rate_45sec:
					m_strRepeatRate = 45;
					break;
				case RepeatRateWSeries.Repeat_Rate_1min:
					m_strRepeatRate = 60;
					break;
				case RepeatRateWSeries.Repeat_Rate_1_5min:
					m_strRepeatRate = 90;
					break;
				case RepeatRateWSeries.Repeat_Rate_2min:
					m_strRepeatRate = 120;
					break;
				case RepeatRateWSeries.Repeat_Rate_3min:
					m_strRepeatRate = 180;
					break;
				case RepeatRateWSeries.Repeat_Rate_5min:
					m_strRepeatRate = 300;
					break;
				case RepeatRateWSeries.Repeat_Rate_10min:
					m_strRepeatRate = 600;
					break;
				case RepeatRateWSeries.Repeat_Rate_20sec:
					m_strRepeatRate = 20;
					break;
				default:
					m_strRepeatRate = 0;
					break;
			}

		}
		#endregion Private Methods

        #region ICloneable Members
        /// <summary>
        /// Make a deep copy
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            return new WT_WTagClassType1(this);
        }

        /// <summary>
        ///  Make a deep copy
        /// </summary>
        /// <returns></returns>
        public override WTTag CloneTag()
        {
            return (WTTag)Clone();
        }
        #endregion
	}

	
	/// <summary>
	/// Holds the data read from a  Wavetrend W-Series Class Type2  tag 
	/// and exposes the tag attributes.
	/// </summary>
    [Serializable]
	public class WT_WTagClassType2 : WT_WTagClassType1
	{
	
	    /// <summary>
	    /// Constructor
	    /// </summary>
	    /// <param name="age">Age</param>
        /// <param name="RSSIVal">RSSI</param>
        /// <param name="repeatRate">Repeat Rate</param>
        /// <param name="tagClassType">Tag Class Type</param>
        /// <param name="tagModel">Tag Model</param>
        /// <param name="PUKCode">PUK Code</param>
        /// <param name="tagClass">Tag Class</param>
        /// <param name="DataLength">Data Length</param>
        /// <param name="tamperCounter">Tamper Counter</param>
        /// <param name="movementCounter">Movement Counter</param>
		public WT_WTagClassType2(uint age,
            byte RSSIVal, byte repeatRate, WT_WTagClassTypes tagClassType, byte tagModel, uint PUKCode, byte tagClass, byte DataLength, byte tamperCounter, byte movementCounter)	
			:base(RSSIVal,repeatRate,tagClassType,tagModel,PUKCode, tagClass,  DataLength, tamperCounter,  movementCounter)
		{
			m_Age			=	age;
            //m_CanAlarm = true;
            m_TagProtocol = WT_WTagClassTypes.ClassType2;
		}


        public WT_WTagClassType2(WT_WTagClassType2 tag)
            : base(tag)
        {
            
        }
        /// <summary>
        /// Returns Tag Data in String format
        /// </summary>
        /// <returns></returns>
	    protected override string GetTagSpecificDataSring()
		{
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
			sb.Append(" Age: "+ m_Age + ",");
			return sb.ToString();

		}

        #region ICloneable Members
        /// <summary>
        /// Make a deep copy
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            return new WT_WTagClassType2(this);
        }

        /// <summary>
        ///  Make a deep copy
        /// </summary>
        /// <returns></returns>
        public override WTTag CloneTag()
        {
            return (WTTag)Clone();
        }
        #endregion

	}

	/// <summary>
	/// Holds the data read from a  Wavetrend W-Series Class Type3  tag 
	/// and exposes the tag attributes.
	/// </summary>
    [Serializable]
	public class WT_WTagClassType3 : WT_WTagClassType1
	{
		
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tagId">Tag Id</param>
        /// <param name="siteCode">Customer Site Code</param>
        /// <param name="RSSIVal">RSSI</param>
        /// <param name="repeatRate">Repeat Rate</param>
        /// <param name="tagClassType">Tag Class Type</param>
        /// <param name="tagModel">Tag Model</param>
        /// <param name="PUKCode">PUK Code</param>
        /// <param name="tagClass">Tag Class</param>
        /// <param name="DataLength">Data Length</param>
        /// <param name="tamperCounter">Tamper Counter</param>
        /// <param name="movementCounter">Movement Counter</param>
		public WT_WTagClassType3(uint tagId ,int siteCode,
            byte RSSIVal, byte repeatRate, WT_WTagClassTypes tagClassType, byte tagModel, uint PUKCode, byte tagClass, byte DataLength, byte tamperCounter, byte movementCounter)	
			:base(RSSIVal,repeatRate,tagClassType,tagModel,PUKCode, tagClass,  DataLength,tamperCounter, movementCounter)
		{
			m_TagId		=	tagId;
			m_SiteCode		=	siteCode;
            //m_CanAlarm = true;
            m_TagProtocol = WT_WTagClassTypes.ClassType3;
            m_WTUniqueId = String.Format("W-{0:00000000}-{1:0000000000}", m_SiteCode,m_PUKCode);
		}

        public WT_WTagClassType3(WT_WTagClassType3 tag)
            : base(tag)
        {
            
        }
        /// <summary>
        /// Returns Tag Data in String format
        /// </summary>
        /// <returns></returns>
		protected override string GetTagSpecificDataSring()
		{
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
			sb.Append(" TagId: "+ m_TagId + ",");
			sb.Append(" SiteCode: "+ m_SiteCode + ",");
            return sb.ToString();
		}

        #region ICloneable Members
        /// <summary>
        /// Make a deep copy
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            return new WT_WTagClassType3(this);
        }

        /// <summary>
        ///  Make a deep copy
        /// </summary>
        /// <returns></returns>
        public override WTTag CloneTag()
        {
            return (WTTag)Clone();
        }
        #endregion
	}

	/// <summary>
	/// Holds the data read from a  Wavetrend W-Series Class Type4  tag 
	/// and exposes the tag attributes.
	/// </summary>
    [Serializable]
	public class WT_WTagClassType4 : WT_WTagClassType1
	{
		

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userData">User Data</param>
        /// <param name="RSSIVal">RSSI</param>
        /// <param name="repeatRate">Repeat Rate</param>
        /// <param name="tagClassType">Tag Class Type</param>
        /// <param name="tagModel">Tag Model</param>
        /// <param name="PUKCode">PUK Code</param>
        /// <param name="tagClass">Tag Class</param>
        /// <param name="DataLength">Data Length</param>
        /// <param name="tamperCounter">Tamper Counter</param>
        /// <param name="movementCounter">Movement Counter</param>
		
		public WT_WTagClassType4(byte[] userData ,
            byte RSSIVal, byte repeatRate, WT_WTagClassTypes tagClassType, byte tagModel, uint PUKCode, byte tagClass, byte DataLength, byte tamperCounter, byte movementCounter)
			:base(RSSIVal,repeatRate,tagClassType,tagModel,PUKCode, tagClass, DataLength, tamperCounter,  movementCounter)
		{
			if(userData != null)
			{
				m_UserData	= new byte[userData.Length]	; 
				Array.Copy(userData,m_UserData,userData.Length);
			}
            //m_CanAlarm = true;
            m_TagProtocol = WT_WTagClassTypes.ClassType4;
		}

        public WT_WTagClassType4(WT_WTagClassType4 tag)
            : base(tag)
        {
            
        }

        #region ICloneable Members
        /// <summary>
        /// Make a deep copy
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            return new WT_WTagClassType4(this);
        }

        /// <summary>
        ///  Make a deep copy
        /// </summary>
        /// <returns></returns>
        public override WTTag CloneTag()
        {
            return (WTTag)Clone();
        }
        #endregion
	}

	/// <summary>
	/// Holds the data read from a  Wavetrend W-Series Class Type5  tag 
	/// and exposes the tag attributes.
	/// </summary>
    [Serializable]
	public class WT_WTagClassType5 : WT_WTagClassType1
	{
		

        /// <summary>
        /// Constuctor
        /// </summary>
        /// <param name="tagId">Tag id</param>
        /// <param name="siteCode">Customer Site code</param>
        /// <param name="age">Age</param>
        /// <param name="RSSIVal">RSSI</param>
        /// <param name="repeatRate">Repeat Rate</param>
        /// <param name="tagClassType">Tag Class Type</param>
        /// <param name="tagModel">Tag Model</param>
        /// <param name="PUKCode">PUK Code</param>
        /// <param name="tagClass">Tag Class</param>
        /// <param name="DataLength">Data Length</param>
        /// <param name="tamperCounter">Tamper Counter</param>
        /// <param name="movementCounter">Movement Counter</param>
		
		public WT_WTagClassType5(uint tagId,int siteCode,uint age,
            byte RSSIVal, byte repeatRate, WT_WTagClassTypes tagClassType, byte tagModel, uint PUKCode, byte tagClass, byte DataLength, byte tamperCounter, byte movementCounter)	
			:base(RSSIVal,repeatRate,tagClassType,tagModel,PUKCode, tagClass, DataLength,tamperCounter, movementCounter)
		{
			m_TagId		=	tagId;
			m_SiteCode		=	siteCode;
			m_Age			=	age;
            //m_CanAlarm = true;
            m_TagProtocol = WT_WTagClassTypes.ClassType5;
            m_WTUniqueId = String.Format("W-{0:00000000}-{1:0000000000}", m_SiteCode, m_PUKCode);
		}

        public WT_WTagClassType5(WT_WTagClassType5 tag)
            : base(tag)
        {
            
        }

        /// <summary>
        /// Returns Tag Data in String format
        /// </summary>
        /// <returns></returns>
		protected override string GetTagSpecificDataSring()
		{
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
			sb.Append(" Age: "+ m_Age + ",");
			sb.Append(" TagId: "+ m_TagId + ",");
			sb.Append(" SiteCode: "+ m_SiteCode + ",");
            return sb.ToString();
		}

        #region ICloneable Members
        /// <summary>
        /// Make a deep copy
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            return new WT_WTagClassType5(this);
        }

        /// <summary>
        ///  Make a deep copy
        /// </summary>
        /// <returns></returns>
        public override WTTag CloneTag()
        {
            return (WTTag)Clone();
        }
        #endregion
	}

	/// <summary>
	/// Holds the data read from a  Wavetrend W-Series Class Type6  tag 
	/// and exposes the tag attributes.
	/// </summary>
    [Serializable]
	public class WT_WTagClassType6 : WT_WTagClassType1
	{
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userData">User Data</param>
        /// <param name="age">Age</param>
        /// <param name="RSSIVal">RSSI</param>
        /// <param name="repeatRate">Repeat Rate</param>
        /// <param name="tagClassType">Tag Class Type</param>
        /// <param name="tagModel">Tag Model</param>
        /// <param name="PUKCode">PUK Code</param>
        /// <param name="tagClass">Tag Class</param>
        /// <param name="DataLength">Data Length</param>
        /// <param name="tamperCounter">Tamper Counter</param>
        /// <param name="movementCounter">Movement Counter</param>
		public WT_WTagClassType6(byte[] userData ,uint age,
            byte RSSIVal, byte repeatRate, WT_WTagClassTypes tagClassType, byte tagModel, uint PUKCode, byte tagClass, byte DataLength, byte tamperCounter, byte movementCounter)	
			:base(RSSIVal,repeatRate,tagClassType,tagModel,PUKCode,tagClass,DataLength,tamperCounter, movementCounter)
		{
			m_Age			=	age;
			if(userData != null)
			{
				m_UserData	= new byte[userData.Length]	; 
				Array.Copy(userData,m_UserData,userData.Length);
			}
            //m_CanAlarm = true;
            m_TagProtocol = WT_WTagClassTypes.ClassType6;
		}

        public WT_WTagClassType6(WT_WTagClassType6 tag)
            : base(tag)
        {
            
        }
        /// <summary>
        /// Returns Tag Data in String format
        /// </summary>
        /// <returns></returns>
		protected override string GetTagSpecificDataSring()
		{
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
			sb.Append(" Age: "+ m_Age + ",");
			if(m_UserData != null)
			{
				sb.Append("UserData : " + GetByteArrString(m_UserData));
			}
            return sb.ToString();
		}

        public void GetAllTemperatures(out int curTemp, out int minTemp, out int maxTemp)
        {
            curTemp = 0;
            minTemp = 0;
            maxTemp = 0;
            

            if (this.UserData != null && this.UserData.Length == 3)
            {
                try
                {
                    sbyte scurTemp = (sbyte)this.UserData[0];
                    sbyte sminTemp = (sbyte)this.UserData[1];
                    sbyte smaxTemp = (sbyte)this.UserData[2];

                    curTemp = Convert.ToInt32(scurTemp);
                    minTemp = Convert.ToInt32(sminTemp);
                    maxTemp = Convert.ToInt32(smaxTemp);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Invalid user data", ex);
                }
            }
            else
            {
                throw new ApplicationException("No Temperature Related data found");
            }
        }

        #region ICloneable Members
        /// <summary>
        /// Make a deep copy
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            return new WT_WTagClassType6(this);
        }

        /// <summary>
        ///  Make a deep copy
        /// </summary>
        /// <returns></returns>
        public override WTTag CloneTag()
        {
            return (WTTag)Clone();
        }
        #endregion
	}

	/// <summary>
	/// Holds the data read from a  Wavetrend W-Series Class Type7  tag 
	/// and exposes the tag attributes.
	/// </summary>
    [Serializable]
	public class WT_WTagClassType7 : WT_WTagClassType1
	{
		
		
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tagId">Tag Id</param>
        /// <param name="siteCode">Site Code</param>
        /// <param name="userData">User Data</param>
        /// <param name="RSSIVal">RSSI</param>
        /// <param name="repeatRate">Repeat Rate</param>
        /// <param name="tagClassType">Tag Class Type</param>
        /// <param name="tagModel">Tag Model</param>
        /// <param name="PUKCode">PUK Code</param>
        /// <param name="tagClass">Tag Class</param>
        /// <param name="DataLength">Data Length</param>
        /// <param name="tamperCounter">Tamper Counter</param>
        /// <param name="movementCounter">Movement Counter</param>
		public WT_WTagClassType7(uint tagId,int siteCode,byte[] userData ,
            byte RSSIVal, byte repeatRate, WT_WTagClassTypes tagClassType, byte tagModel, uint PUKCode, byte tagClass, byte DataLength, byte tamperCounter, byte movementCounter)
			:base(RSSIVal,repeatRate,tagClassType,tagModel,PUKCode,tagClass,DataLength, tamperCounter,  movementCounter)
		{
			m_TagId		=	tagId;
			m_SiteCode		=	siteCode;
			
			if(userData != null)
			{
				m_UserData	= new byte[userData.Length]	; 
				Array.Copy(userData,m_UserData,userData.Length);
			}
            //m_CanAlarm = true;
            m_TagProtocol = WT_WTagClassTypes.ClassType7;
            m_WTUniqueId = String.Format("W-{0:00000000}-{1:0000000000}", m_SiteCode, m_PUKCode);
		}

        public WT_WTagClassType7(WT_WTagClassType7 tag)
            : base(tag)
        {
            
        }
        /// <summary>
        /// Returns Tag Data in String format
        /// </summary>
        /// <returns></returns>
		protected override string GetTagSpecificDataSring()
		{
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
			sb.Append(" TagId: "+ m_TagId + ",");
			sb.Append(" SiteCode: "+ m_SiteCode + ",");
			if(m_UserData != null)
			{
				sb.Append("UserData : " + GetByteArrString(m_UserData));
			}
			return sb.ToString();
		}

        #region ICloneable Members
        /// <summary>
        /// Make a deep copy
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            return new WT_WTagClassType7(this);
        }

        /// <summary>
        ///  Make a deep copy
        /// </summary>
        /// <returns></returns>
        public override WTTag CloneTag()
        {
            return (WTTag)Clone();
        }
        #endregion
	}

	
	/// <summary>
	/// Holds the data read from a  Wavetrend W-Series Class Type8  tag 
	/// and exposes the tag attributes.
	/// </summary>
    [Serializable]
	public class WT_WTagClassType8 : WT_WTagClassType1
	{

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="age">Age </param>
        /// <param name="tagId">Tag Id</param>
        /// <param name="siteCode">Site Code</param>
        /// <param name="userData">User Data</param>
        /// <param name="RSSIVal">RSSI</param>
        /// <param name="repeatRate">Repeat Rate</param>
        /// <param name="tagClassType">Tag Class Type</param>
        /// <param name="tagModel">Tag Model</param>
        /// <param name="PUKCode">PUK Code</param>
        /// <param name="tagClass">Tag Class</param>
        /// <param name="DataLength">Data Length</param>
        /// <param name="tamperCounter">Tamper Counter</param>
        /// <param name="movementCounter">Movement Counter</param>
		public WT_WTagClassType8(uint tagId,int siteCode,byte[] userData ,uint age,
            byte RSSIVal, byte repeatRate, WT_WTagClassTypes tagClassType, byte tagModel, uint PUKCode, byte tagClass, byte DataLength, byte tamperCounter, byte movementCounter)
			:base(RSSIVal,repeatRate,tagClassType,tagModel,PUKCode,tagClass,DataLength,tamperCounter, movementCounter)
		{
			m_TagId		=	tagId;
			m_SiteCode		=	siteCode;
			
			m_Age			=	age;
			if(userData != null)
			{
				m_UserData	= new byte[userData.Length]	; 
				Array.Copy(userData,m_UserData,userData.Length);
			}

            //m_CanAlarm = true;
            m_TagProtocol = WT_WTagClassTypes.ClassType8;
            m_WTUniqueId = String.Format("W-{0:00000000}-{1:0000000000}", m_SiteCode, m_PUKCode);
		}

        public WT_WTagClassType8(WT_WTagClassType8 tag)
            : base(tag)
        {
            
        }
        /// <summary>
        /// Returns Tag Data in String format
        /// </summary>
        /// <returns></returns>
		protected override string GetTagSpecificDataSring()
		{
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
			sb.Append(" Age: "+ m_Age + ",");
			sb.Append(" TagId: "+ m_TagId + ",");
			sb.Append(" SiteCode: "+ m_SiteCode + ",");
			if(m_UserData != null)
			{
				sb.Append("UserData : " + GetByteArrString(m_UserData));
			}
            return sb.ToString();
		}

        #region ICloneable Members
        /// <summary>
        /// Make a deep copy
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            return new WT_WTagClassType8(this);
        }

        /// <summary>
        ///  Make a deep copy
        /// </summary>
        /// <returns></returns>
        public override WTTag CloneTag()
        {
            return (WTTag)Clone();
        }
        #endregion
	}
	#endregion W-Series Tags


    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public struct WTTagHistory
    {
        /// <summary>
        /// 
        /// </summary>
        public byte RSSI;
        /// <summary>
        /// 
        /// </summary>
        public long LastReadTicks;

        public int MovmentCounter;

        public int CurrentTemperature;

        public WTTagHistory(WTTag tag)
        {
            this.RSSI = tag.RSSI;
            this.LastReadTicks = tag.LastReadTime.Ticks;
            if (tag is WT_LTag)
                this.MovmentCounter = ((WT_LTag)tag).MoveCount;
            else
                this.MovmentCounter = tag.MovementCounter;

            this.CurrentTemperature = 0;

            if (tag.TagProtocol == WT_WTagClassTypes.ClassType6)
            {
                int minimumTemperature = 0;
                int maximumTemperature = 0;

                try
                {
                    if (tag.UserData != null && tag.UserData.Length == 3)
                    {
                        ((WT_WTagClassType6)tag).GetAllTemperatures(out this.CurrentTemperature,
                            out minimumTemperature, out maximumTemperature);
                    }
                }
                catch
                { }
            }
        }
    }

    /*
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class WTTagWrapper
    {
        WTTag tagObj = null;
        List<WTTagHistory> lstTagHistory = new List<WTTagHistory>(5);
        /// <summary>
        /// 
        /// </summary>
        public static int ListCapacity = 5;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="wtInfo"></param>
        public WTTagWrapper(WTTag tag, WTTagHistory wtInfo)
        {
            this.tagObj = tag.CloneTag();
            this.lstTagHistory.Add(wtInfo);
        }
        /// <summary>
        /// 
        /// </summary>
        public WTTag TagObj
        {
            get { return this.tagObj.CloneTag(); }
            set { this.tagObj = value.CloneTag(); }
        }
        /// <summary>
        /// 
        /// </summary>
        public List<WTTagHistory> TagHistory
        {
            get { return this.lstTagHistory; }
            set { this.lstTagHistory = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wtInfo"></param>
        public void AddTagHistory(WTTagHistory wtInfo)
        {
            if (this.lstTagHistory.Count >= ListCapacity)
            {
                this.lstTagHistory.RemoveAt(0);
            }
            this.lstTagHistory.Add(wtInfo);
        }

        public byte AverageRSSI
        {
            get
            {
                if (lstTagHistory.Count == 1)
                    return lstTagHistory[0].RSSI;


                int rssiTotal = 0;
                foreach (WTTagHistory rssiHistory in lstTagHistory)
                {
                    rssiTotal += rssiHistory.RSSI;

                }

                byte avgRSSI = (byte)(rssiTotal / lstTagHistory.Count);
                return avgRSSI;

            }
        }

    }*/
}

