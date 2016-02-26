using System;
using System.Collections.Generic;
using System.Text;
using KTone.Core.KTIRFID;

namespace KTone.Core.KTIRFID
{
    /// <summary>
    /// Event type
    /// </summary>
    public enum WTEvent_Type
    {
        NOT_SPECIFIED,
        TAGS_ADDED_EVENT,
        TAGS_REMOVED_EVENT,
        TAGS_CURRENT_EVENT,
        TAGS_MOVED_EVENT,
        TAGS_TAMPERED_EVENT
    }


    /// <summary>
    /// Event argument for tag added/removed/current event
    /// </summary>
    [Serializable]
    public class WTTagEventArgs : KTComponentEventArgs
    {
        #region Attributes
        private WTTag[] tagDetails = null;
        WTEvent_Type typeOfEvent = WTEvent_Type.NOT_SPECIFIED; 
        #endregion Attributes

        #region Constructors
        /// <summary>
        /// Initializes an instance of WTTagEventArgs class.
        /// </summary>
        /// <param name="component">originator of the packet event</param>
        /// <param name="eventId">event Id</param>
        public WTTagEventArgs(WTTag[] tagDetails, IKTComponent component, string eventId )
            : base(component, eventId)
        {
            this.tagDetails = tagDetails;
        }

        /// <summary>
        /// Initializes an instance of WTTagEventArgs class.
        /// </summary>
        /// <param name="componentId">Id of the originator of the packet event</param>
        /// <param name="eventId">event Id</param>
        public WTTagEventArgs(WTTag[] tagDetails, IKTComponent component, string eventId,WTEvent_Type eventType )
            : base(component, eventId)
        {
            this.tagDetails = tagDetails;
            this.typeOfEvent = eventType;
        }

        /// <summary>
        /// Initializes an instance of WTTagEventArgs class.
        /// </summary>
        /// <param name="componentId">Id of the originator of the packet event</param>
        /// <param name="eventId">event Id</param>
        public WTTagEventArgs(WTTag[] tagDetails, IKTComponent component, string eventId, DateTime timestamp)
            : base(component, eventId, timestamp)
        {
            this.tagDetails = tagDetails;

        }
        #endregion

        #region Properties
        public WTTag[] TagDetails
        {
            get { return this.tagDetails; }
        }

        public WTEvent_Type WTEventType
        {
            get { return typeOfEvent; }
            set { typeOfEvent = value; }
        }
        #endregion Properties

    }

    /// <summary>
    /// Event argument for tag moved event
    /// </summary>
    [Serializable]
    public class WTTagMovedEventArgs : KTTagMovedEventArgs
    {
        #region Attributes
        private WTTag tag = null;
        #endregion Attributes

        #region Constructors

        /// <summary>
        /// Initializes an instance of WTTagMovedEventArgs class.
        /// </summary>
        public WTTagMovedEventArgs(WTTag tag, string movedFrom, string movedFromName, int movedFromGroupId, string movedFromGroupName,
                                    string movedTo, string movedToName, int movedToGroupId, string movedToGroupName, DateTime tagMovedTime,
                                    IKTComponent component, string eventId)
            : base(tag.UniqueID, movedFrom, movedFromName, movedFromGroupId, movedFromGroupName, movedTo, movedToName, movedToGroupId, movedToGroupName, tagMovedTime, component, eventId)
        {
            this.tag = tag;
        }

        /// <summary>
        /// Initializes an instance of WTTagMovedEventArgs class.
        /// </summary>
        public WTTagMovedEventArgs(WTTag tag, string movedFrom, string movedFromName, int movedFromGroupId, string movedFromGroupName,
                                    string movedTo, string movedToName, int movedToGroupId, string movedToGroupName, DateTime tagMovedTime,
                                    IKTComponent component, string eventId, DateTime timestamp)
            : base(tag.UniqueID, movedFrom, movedFromName, movedFromGroupId, movedFromGroupName, movedTo, movedToName, movedToGroupId, movedToGroupName, tagMovedTime, component, eventId, timestamp)
        {
            this.tag = tag;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Returns Tag moved
        /// </summary>
        public WTTag Tag
        {
            get { return this.tag; }
        }
        #endregion Properties
    }


    /// <summary>
    /// Provides tag when any tag is moved
    /// </summary>
    [Serializable]
    public class WTTagMovementEventArgs : KTComponentEventArgs
    {
        #region Attributes
        private WTTag m_Tag = null;
        #endregion Attributes

        /// <summary>
        /// Initializes the object of WTReaderTagReadEventArgs
        /// </summary>
        /// <param name="readerName">string</param>
        /// <param name="tag">WTTag</param>
        public WTTagMovementEventArgs(IKTComponent component, string eventId, WTTag tag)
            : base(component, eventId)
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
    public class WTTagTamperedEventArgs : KTComponentEventArgs
    {
        #region Attributes
        
        private WTTag tag = null;
        private Tamper type = Tamper.TAMPERED;
        private bool checkMovementCounter = false;
        private bool checkAlarmType = false;
        private int movementCounter = 0;

        #endregion Attributes

        /// <summary>
        /// Initializes the object of WTReaderTagReadEventArgs
        /// </summary>
        /// <param name="readerName">string</param>
        /// <param name="tag">WTTag</param>
        /// <param name="type">tamper type</param>
        public WTTagTamperedEventArgs(IKTComponent component, string eventId, WTTag tag, Tamper type)
            : base(component, eventId)
        {

            this.tag = tag;
            this.type = type;
            this.checkMovementCounter = false;
            this.checkAlarmType = false;
            this.movementCounter = 0;
        }

        /// <summary>
        /// Initializes the object of WTReaderTagReadEventArgs
        /// </summary>
        /// <param name="component">component object</param>
        /// <param name="eventId">event id</param>
        /// <param name="tag">tag object</param>
        /// <param name="type">Tamper type</param>
        /// <param name="checkMovementCounter">Movement Counter Enabled or disabled</param>
        /// <param name="checkAlarmType">check alarm type</param>
        /// <param name="movementCounter">movement counter</param>
        public WTTagTamperedEventArgs(IKTComponent component, string eventId, WTTag tag, Tamper type,
            bool checkMovementCounter, bool checkAlarmType, int movementCounter)
            : base(component, eventId)
        {
            this.tag = tag;
            this.type = type;
            this.checkMovementCounter = checkMovementCounter;
            this.checkAlarmType = checkAlarmType;
            this.movementCounter = movementCounter;
        }


        #region Properties
        /// <summary>
        /// WT tag
        /// </summary>
        public WTTag Tag
        {
            get
            {
                return this.tag;
            }
        }

        /// <summary>
        /// Tamper Type
        /// </summary>
        public Tamper Type
        {
            get
            {
                return this.type;
            }
        }

        /// <summary>
        /// CheckMovementCounter flag
        /// </summary>
        public bool CheckMovementCounter
        {
            get
            {
                return this.checkMovementCounter;
            }
        }

        /// <summary>
        /// AlarmType flag
        /// </summary>
        public bool CheckAlarmType
        {
            get
            {
                return this.checkAlarmType;
            }
        }

        /// <summary>
        /// Movement Counter
        /// </summary>
        public int MovementCounter
        {
            get
            {
                return this.movementCounter;
            }
        }

        #endregion Properties
    }


    /// <summary>
    /// Provides tag when any fob alarm is present.
    /// </summary>
    [Serializable]
    public class WTFOBSeenEventArgs : KTComponentEventArgs
    {
        #region Attributes
        private WTTag tag = null;        
        #endregion Attributes
          
        /// <summary>
        /// Initializes the object of WTFOBSeenEventArgs
        /// </summary>
        /// <param name="component">Sender component</param>
        /// <param name="eventId">event id</param>
        /// <param name="tag">WTtag with fob alarm</param>
        public WTFOBSeenEventArgs(IKTComponent component, string eventId, WTTag tag)
            : base(component, eventId)
        {
            this.tag = tag;            
        }


        #region Properties
        /// <summary>
        /// WT tag with
        /// </summary>
        public WTTag Tag
        {
            get
            {
                return this.tag;
            }
        }
        
        #endregion Properties
    }
}
