using System;

namespace KTone.Core.KTIRFID
{
    /// <summary>
    /// Type of tamper
    /// </summary>
    public enum Tamper
    {
        /// <summary>
        /// Tag is tampered
        /// </summary>
        TAMPERED = 0x00,
        /// <summary>
        /// Tag is cloned
        /// </summary>
        CLONED   = 0x01,
        
        /// <summary>
        /// Tag movement
        /// </summary>
        MOVEMENT
    }
	/// <summary>
	/// Summary description for WTReaderEventArgs.
	/// </summary>
    [Serializable]
	public class WTReaderEventArgs : EventArgs
	{
		#region Attributes
        /// <summary>
        /// Reader Name
        /// </summary>
		protected string m_ReaderName = string.Empty;
		#endregion Attributes

		/// <summary>
		/// Initializes the object of WTReaderEventArgs
		/// </summary>
		/// <param name="readerName">string</param>
		public WTReaderEventArgs(string readerName)
		{
			m_ReaderName = readerName;

		}
		
		/// <summary>
		/// ReaderName
		/// </summary>
		public string ReaderName
		{
			get
			{
				return m_ReaderName;
			}
		}
		
	}

	/// <summary>
	/// Provides tag information whenever tag is reader
	/// </summary>
    [Serializable]
	public class WTReaderTagReadEventArgs : WTReaderEventArgs
	{
		#region Attributes
		private WTTag m_Tag= null;
		#endregion Attributes
		
		/// <summary>
		/// Initializes the object of WTReaderTagReadEventArgs
		/// </summary>
		/// <param name="readerName">string</param>
		/// <param name="tag">WTTag</param>
		public WTReaderTagReadEventArgs(string readerName,WTTag tag):base(readerName) 
		{	
			
			m_Tag = tag;
		}


		#region Properties
		/// <summary>
		/// WT tag
		/// </summary>
		public WTTag Tag
		{
			get
			{
				return m_Tag;
			}
		}
		#endregion Properties
	}
    /*
    /// <summary>
    /// Provides tag when any tag is moved
    /// </summary>
    [Serializable]
    public class WTTagMovementEventArgs : WTReaderEventArgs
    {
        #region Attributes
        private WTTag m_Tag = null;
        #endregion Attributes

        /// <summary>
        /// Initializes the object of WTReaderTagReadEventArgs
        /// </summary>
        /// <param name="readerName">string</param>
        /// <param name="tag">WTTag</param>
        public WTTagMovementEventArgs(string readerName, WTTag tag)
            : base(readerName)
        {

            m_Tag = tag;
        }


        #region Properties
        /// <summary>
        /// WT tag
        /// </summary>
        public WTTag Tag
        {
            get
            {
                return m_Tag;
            }
        }
        #endregion Properties
    }

    /// <summary>
    /// Provides tag when any tag is tampered. and type of tampering done
    /// </summary>
    [Serializable]
    public class WTTagTamperedEventArgs : WTReaderEventArgs
    {
        #region Attributes
        private WTTag m_Tag = null;
        private Tamper m_Type = Tamper.TAMPERED;
        #endregion Attributes

        /// <summary>
        /// Initializes the object of WTReaderTagReadEventArgs
        /// </summary>
        /// <param name="readerName">string</param>
        /// <param name="tag">WTTag</param>
        /// <param name="type">tamper type</param>
        public WTTagTamperedEventArgs(string readerName, WTTag tag, Tamper type)
            : base(readerName)
        {

            m_Tag = tag;
            m_Type = type;
        }


        #region Properties
        /// <summary>
        /// WT tag
        /// </summary>
        public WTTag Tag
        {
            get
            {
                return m_Tag;
            }
        }

        /// <summary>
        /// Tamper Type
        /// </summary>
        public Tamper Type
        {
            get
            {
                return m_Type;
            }
        }
        #endregion Properties
    }
    */
	/// <summary>
	/// Provides data for the Autopolling events
	/// </summary>
    [Serializable]
	public class WTReaderAutoPollngEventArgs : WTReaderEventArgs
	{
		#region Attributes
		private WTTag[]	m_TagArray = null;
		#endregion Attributes
		
		/// <summary>
		/// Initializs the object of WTReaderAutoPollngEventArgs
		/// </summary>
		/// <param name="readerName">string</param>
		/// <param name="tagArray">Array WTTag objects</param>
		public WTReaderAutoPollngEventArgs(string readerName,WTTag[] tagArray)
			:base(readerName) 
		{	
			m_ReaderName = readerName ;
			if(tagArray == null || tagArray.Length == 0)
				m_TagArray =  new WTTag[]{};
			else
				m_TagArray = new WTTag[tagArray.Length];
			Array.Copy(tagArray,m_TagArray,m_TagArray.Length);
			
		}


		#region Properties
		/// <summary>
		/// Array of WTTag objects
		/// </summary>
		public WTTag[] Tags
		{
			get
			{
				return m_TagArray;
			}
		}
		#endregion Properties
    }

#if !RX300 || COMPLETE
    /// <summary>
	/// Provides data for the Reader status Monitor events
    /// </summary>
#else
         /// <exclude/>
#endif
    [Serializable]
    public class WTReaderStatusMonitorEventArgs : WTReaderEventArgs
	{
		#region Attributes
		private bool m_IsConnected = false;
		private DateTime m_TimeStamp;

		#endregion Attributes
		/// <summary>
		/// Initializes the object of WTReaderStatusMonitorEventArgs
		/// </summary>
		/// <param name="readerName">string</param>
		/// <param name="isConnected">bool</param>
		public WTReaderStatusMonitorEventArgs(string readerName,bool isConnected)
			:base(readerName) 
		{
			m_ReaderName = readerName ;
			m_IsConnected = isConnected;
			m_TimeStamp = DateTime.Now;
			
		}

		#region Properties
		/// <summary>
		/// IsConnected
		/// </summary>
		public bool IsConnected
		{
			get
			{
				return m_IsConnected;
			}
		}

		/// <summary>
		/// TimeStamp
		/// </summary>
		public DateTime TimeStamp
		{
			get
			{
				return m_TimeStamp;
			}
		}

		#endregion Properties
    }

#if RX900 ||RX1000|| COMPLETE
    /// <summary>
	/// Provides data for the Reader Heart beat events
	/// </summary>
#else
    /// <exclude/>
#endif
    [Serializable]
	public class WTReaderHeartBeatEventArgs : WTReaderEventArgs
	{
		#region Attributes
		private AutoPollMode m_AutoPolling; 
		private GainMode m_Gain;
		private IOStatus m_Relay0;
		private IOStatus m_Relay1; 
		private IOStatus m_Input0; 
		private IOStatus m_Input1;
		
		private DateTime m_TimeStamp;

		#endregion Attributes
	
		/// <summary>
		/// Initializes the object of WTReaderHeartBeatEventArgs
		/// </summary>
		/// <param name="readerName">string</param>
		/// <param name="autoPoll">AutoPollMode</param>
		/// <param name="gain">GainMode</param>
		/// <param name="relay0">IOStatus</param>
		/// <param name="relay1">IOStatus</param>
		/// <param name="input0">IOStatus</param>
		/// <param name="input1">IOStatus</param>
		public WTReaderHeartBeatEventArgs(string readerName,
			AutoPollMode autoPoll, GainMode gain,
			IOStatus relay0,IOStatus relay1, IOStatus input0, IOStatus input1)
			:base(readerName) 
		{
			m_AutoPolling =	autoPoll;
			m_Gain =	gain ;
			m_Relay0 =	relay0 ;
			m_Relay1 = 	relay1 ;
			m_Input0 = 	input0 ;
			m_Input1 =	input1 ;

			m_TimeStamp = DateTime.Now;
			
		}


		#region Properties
		/// <summary>
		/// AutoPollingMode
		/// </summary>
		public AutoPollMode AutoPollingMode{ get { return m_AutoPolling; } }

		/// <summary>
		/// Gain
		/// </summary>
		public GainMode Gain{ get { return m_Gain; } }
		
		/// <summary>
		/// Relay0 status
		/// </summary>
		public IOStatus Relay0{ get { return m_Relay0; } }
		
		/// <summary>
		/// Relay1 status
		/// </summary>
		public IOStatus Relay1{ get { return m_Relay1; } }
		
		/// <summary>
		/// Input0 status
		/// </summary>
		public IOStatus Input0{ get { return m_Input0; } }
		
		/// <summary>
		/// Input0 status
		/// </summary>
		public IOStatus Input1{ get { return m_Input1; } }
		
		/// <summary>
		/// TimeStamp
		/// </summary>
		public DateTime TimeStamp{ get { return m_TimeStamp; } }
		#endregion Properties
	}

    

   
}
