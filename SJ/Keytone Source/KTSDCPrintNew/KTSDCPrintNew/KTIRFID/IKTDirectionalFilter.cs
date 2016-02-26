using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KTone.Core.KTIRFID
{
    #region[ KTDirectionalFilterTagMoved ]
    [Serializable]
    public class KTDirectionalFilterTagMoved : KTComponentEventArgs
    {
        #region Attributes

        private string status = "NOTDEFINED";
        private string tagID = string.Empty;
        private string prvresourceID = string.Empty;
        private string newresourceID = string.Empty;
        private DateTime timestamp = DateTime.Now;

        #endregion Attributes

        #region Constructors
        /// <summary>
        /// KTDirectionalFilterClass
        /// </summary>
        /// <param name="tagID"></param>
        /// <param name="status"></param>
        /// <param name="prvresourceID"></param>
        /// <param name="newresourceID"></param>
        /// <param name="timestamp"></param>
        /// <param name="component"></param>
        /// <param name="eventId"></param>

        public KTDirectionalFilterTagMoved(string status, string tagID, string prvresourceID, string newresourceID, DateTime timestamp, IKTComponent component, string eventId)
            : base(component, eventId)
        {
            this.status = status;
            this.tagID = tagID;
            this.prvresourceID = prvresourceID;
            this.newresourceID = newresourceID;
            this.timestamp = timestamp;
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        ///It will give status of Item.
        /// </summary>

        public string Status
        {
            get { return this.status; }
        }
        /// <summary>
        ///It will give TagID.
        /// </summary>
        public string TagID
        {
            get { return this.tagID; }
        }

        /// <summary>
        ///It will give Privious ResourceID.
        /// </summary>
        public string PrvresourceID
        {
            get { return this.prvresourceID ; }
        }

        /// <summary>
        ///It will give  New ResourceID.
        /// </summary>
        public string NewresourceID
        {
            get { return this.newresourceID; }
        }

      
        /// <summary>
        /// It will give you Timestamp
        /// </summary>
        public DateTime  Timestamp
        {
            get { return this.timestamp; }
        }

        #endregion Properties
    }

    #endregion[ KTDirectionalFilterTagMoved ]


    #region [ Classtucture ]
    public class DirectionalFilterItemTagDetails
        {
            #region Attributes
            string _tagId = string.Empty;
            string _Status = string.Empty;
            string _ComponentId = string.Empty;
            string _prvResourceId = string.Empty;
            string _newResourceId = string.Empty;

            DateTime _Time;
           
            #endregion Attributes

            #region Constructor
            public DirectionalFilterItemTagDetails(string tagId,string status, string componentId,string prvResourceId,string newResourceId,  DateTime time)
            {
                this._tagId = tagId;
                this._Status = status;
                this._ComponentId = componentId;
                this._Time = time;
                this._prvResourceId = prvResourceId;
                this._newResourceId = newResourceId;
            }
            #endregion Constructor
            #region Properties

            public String TagID
            {
                get
                {
                    return _tagId;
                }
                set
                {

                    _tagId = value;

                }
            }
            public String Status
            {
                get
                {
                    return _Status;
                }
                set
                {

                    _Status = value;

                }
            }
            public String ComponentId
            {
                get
                {
                    return _ComponentId;
                }
                set
                {

                    _ComponentId = value;

                }
            }
            public String NewResourceId
            {
                get
                {
                    return _newResourceId;
                }
                set
                {

                    _newResourceId = value;

                }
            }
            public String PrvResourceId
            {
                get
                {
                    return _prvResourceId;
                }
                set
                {

                    _prvResourceId = value;

                }
            }
            public DateTime Time
            {
                get
                {
                    return _Time;
                }
                set
                {

                    _Time = value;

                }
            }
            #endregion Properties
        }
        #endregion [ Classtucture ]

    /// <summary>
    /// On Directional Filter tag seen 
    /// </summary>
    #region [ KTTagSeenAtDirectinalFilterTagArgs ]
    [Serializable]
    public class KTTagSeenAtDirectinalFilterTagArgs : KTComponentEventArgs
    {
        #region Attributes
        private List<string> tagSeen;
      
        #endregion Attributes

        #region Constructors
        /// <summary>
        /// KTTagSeenAtDirectinalFilterTagArgs
        /// </summary>
        /// <param name="tagSeen"></param>
       
        public KTTagSeenAtDirectinalFilterTagArgs(List<string> tagSeen, IKTComponent component, string eventId)
            : base(component, eventId)
        {
            this.tagSeen = tagSeen;
         

        }
        #endregion Constructor

        #region Properties

        /// <summary>
        ///TagID seen in front of Reader/Filter.
        /// </summary>
        public List<string> TagSeen
        {
            get { return this.tagSeen; }
        }

     
        #endregion Properties
    }
    #endregion [ KTTagSeenAtDirectinalFilterTagArgs ]

    /// <summary>
    /// Event for exceptions and alerts at Directional filter
    /// </summary>
    #region [ KTDirectinalFilterExceptionsAndAlertsArgs ]
    [Serializable]
    public class KTDirectinalFilterExceptionsAndAlertsArgs : KTComponentEventArgs
    {
        #region Attributes
        private DateTime timeStamp;
        private string tagID = string.Empty;

        #endregion Attributes

        #region Constructors
        /// <summary>
        /// KTDirectinalFilterExceptionsAndAlertsArgs
        /// </summary>
        /// <param name="tagID"></param>
        /// <param name="timeStamp"></param>
        /// <param name="component"></param>
        /// <param name="eventId"></param>
        public KTDirectinalFilterExceptionsAndAlertsArgs(string tagID,  DateTime timeStamp, IKTComponent component, string eventId)
            : base(component, eventId)
        {
            this.tagID = tagID;
            this.timeStamp = timeStamp;
        }
        #endregion Constructor

        #region Properties

        /// <summary>
        ///TagID seen in front of Reader.
        /// </summary>
        public string TagID
        {
            get { return this.tagID; }
        }

        /// <summary>
        /// Timestamp.
        /// </summary>
        public DateTime TimeStamp
        {
            get { return this.timeStamp; }
        }

       

        #endregion Properties
    }
    #endregion [ KTDirectinalFilterExceptionsAndAlertsArgs ]


    public  interface IKTDirectionalFilter
    {
        #region Events

        /// <summary>
        /// Event raised when tag seen
        /// </summary>
        event EventHandler<KTTagSeenAtDirectinalFilterTagArgs> SeenTags;

        event EventHandler<KTDirectionalFilterTagMoved> TagMoved;

        event EventHandler<KTDirectinalFilterExceptionsAndAlertsArgs> ExceptionsAndAlertsSeen;

        #endregion Events

        #region Methods
        List<string> GetErrors();
        List<string> GetSuccessLogs();
        List<string> GetFilterStatics();
        #endregion Methods

        long GetRefreshTimeInterval { get; }

        long SetRefreshTimeInterval { set; }

        long ToatalTagCount { get; }


    }
}
