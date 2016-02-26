using System;
using System.Collections.Generic;
using System.Text;

namespace KTone.Core.KTIRFID
{
    #region ActiveWaveTagMoved
    /// <summary>
    /// Event argument for tag moved event
    /// </summary>
    [Serializable]
    public class AWTagMovedEventArgs : KTTagMovedEventArgs
    {
        #region Attributes
        private ActiveWaveTag tag = null;
        private ushort movedFromFGenId = 0;
        private ushort movedToFGenId = 0;
        #endregion Attributes

        #region Constructors

        /// <summary>
        /// Initializes an instance of AWTagMovedEventArgs class.
        /// </summary>
        public AWTagMovedEventArgs(ActiveWaveTag tag, string movedFrom, string movedFromName, ushort movedFromFGenId, int movedFromGroupId, string movedFromGroupName,
            string movedTo, string movedToName, ushort movedToFGenId, int movedToGroupId, string movedToGroupName, 
            DateTime tagMovedTime,IKTComponent component, string eventId)
            : base(tag.UniqueId, movedFrom, movedFromName, movedFromGroupId, movedFromGroupName, movedTo, movedToName,movedToGroupId, movedToGroupName, tagMovedTime, component, eventId)
        {
            this.tag = tag;
            this.movedFromFGenId = movedFromFGenId;
            this.movedToFGenId = movedToFGenId;
        }

        public AWTagMovedEventArgs(ActiveWaveTag tag, string movedFrom, string movedFromName,int movedFromGroupId, string movedFromGroupName,
                                 string movedTo, string movedToName, int movedToGroupId, string movedToGroupName, DateTime tagMovedTime,
                                 IKTComponent component, string eventId)
            : base(tag.UniqueId, movedFrom, movedFromName,movedFromGroupId, movedFromGroupName, movedTo, movedToName, movedToGroupId, movedToGroupName, tagMovedTime, component, eventId)
        {
            this.tag = tag;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Returns Tag moved
        /// </summary>
        public ActiveWaveTag Tag
        {
            get { return this.tag; }
        }

        public ushort MovedFromFGenId
        {
            get
            {
                return this.movedFromFGenId;
            }
        }

        public ushort MovedToFGenId
        {
            get
            {
                return this.movedToFGenId;
            }
        }
        #endregion Properties
    }
    #endregion ActiveWaveTagMoved

    #region AWTagEventArgs
    /// <summary>
    /// Event argument for tag added/removed/current event
    /// </summary>
    [Serializable]
    public class AWTagEventArgs : KTComponentEventArgs
    {
        #region Attributes
        private ActiveWaveTag[] tagDetails = null;
        AWEventType typeOfEvent = AWEventType.NOT_SPECIFIED;
        bool ignoreTag = false;
        string ignoreTagReason = string.Empty;
        #endregion Attributes

        #region Constructors
        /// <summary>
        /// Initializes an instance of AWTagEventArgs class.
        /// </summary>
        /// <param name="componentId">Id of the originator of the packet event</param>
        /// <param name="eventId">event Id</param>
        public AWTagEventArgs(ActiveWaveTag[] tagDetails, IKTComponent component, string eventId)
            : base(component, eventId)
        {
            this.tagDetails = tagDetails;
        }

        /// <summary>
        /// Initializes an instance of AWTagEventArgs class.
        /// </summary>
        /// <param name="component">Component that generated the event</param>
        /// <param name="eventId">event Id</param>
        public AWTagEventArgs(ActiveWaveTag[] tagDetails, IKTComponent component, string eventId, AWEventType eventType)
            : base(component, eventId)
        {
            this.tagDetails = tagDetails;
            this.typeOfEvent = eventType;
        }

        /// <summary>
        /// Initializes an instance of AWTagEventArgs class.
        /// </summary>
        /// <param name="component">Component that generated the event</param>
        /// <param name="eventId">event Id</param>
        public AWTagEventArgs(ActiveWaveTag[] tagDetails, IKTComponent component, string eventId, DateTime timestamp)
            : base(component, eventId, timestamp)
        {
            this.tagDetails = tagDetails;

        }
        #endregion

        #region Properties
        public ActiveWaveTag[] TagDetails
        {
            get { return this.tagDetails; }
        }
        public bool IgnoreTag
        {
            get { return ignoreTag; }
            set { this.ignoreTag = value; }
        }
        public string IgnoreTagReason
        {
            get { return ignoreTagReason; }
            set { this.ignoreTagReason = value; }
        }
        public AWEventType WTEventType
        {
            get { return typeOfEvent; }
            set { typeOfEvent = value; }
        }
       
        #endregion Properties

    }
    #endregion AWTagEventArgs

    #region AWInventoryStatusEventArgs
    /// <summary>
    /// Event argument for tag added/removed/current event
    /// </summary>
    [Serializable]
    public class AWInventoryStatusEventArgs : KTComponentEventArgs
    {
        #region Attributes
        private AWInventoryStatus inventoryStatus;
        #endregion Attributes

        #region Constructors
        /// <summary>
        /// Initializes an instance of AWInventoryStatusEventArgs class.
        /// </summary>
        /// <param name="componentId">Id of the originator of the packet event</param>
        /// <param name="eventId">event Id</param>
        public AWInventoryStatusEventArgs(AWInventoryStatus inventoryStatus, IKTComponent component, string eventId)
            : base(component, eventId)
        {
            this.inventoryStatus = inventoryStatus;
        }
        #endregion

        #region Properties
        public AWInventoryStatus InventoryStatus
        {
            get { return this.inventoryStatus; }
        }
        #endregion Properties
    }
    #endregion AWInventoryStatusEventArgs

    #region TagTamperedEventArg
    /// <summary>
    /// Event argument TagTamperedEvent
    /// </summary>
    [Serializable]
    public class TagTamperedEventArg : AWTagEventArgs
    {
        #region Attributes
        string tamperType = string.Empty;
        #endregion Attributes

        #region Constructors
        /// <summary>
        /// Initializes an instance of TagTamperedEventArg class.
        /// </summary>
        /// <param name="componentId">Id of the originator of the packet event</param>
        /// <param name="eventId">event Id</param>
        public TagTamperedEventArg(ActiveWaveTag[] tagDetails, string tamperType, IKTComponent component, string eventId)
            : base(tagDetails, component, eventId)
        {
            this.tamperType = tamperType;
        }
        #endregion

        #region Properties
        public string TamperType
        {
            get { return this.tamperType; }
        }
        #endregion Properties
    }
    #endregion TagTamperedEventArg


    #region TransmissionStateChangedEventArgs
    /// <summary>
    /// Event argument TagTamperedEvent
    /// </summary>
    [Serializable]
    public class TransmissionStateChangedEventArgs : KTComponentEventArgs
    {
        #region Attributes
        bool transmissionState = false;
        #endregion Attributes

        #region Constructors
        /// <summary>
        /// Initializes an instance of TransmissionStateChangedEventArgs class.
        /// </summary>
        /// <param name="componentId">Id of the originator of the packet event</param>
        /// <param name="eventId">event Id</param>
        /// <param name="transmissionState">Transmission State</param>
        public TransmissionStateChangedEventArgs(bool transmissionState, IKTComponent component, string eventId)
            : base(component, eventId)
        {
            this.transmissionState = transmissionState;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Returns TransmissionState
        /// </summary>
        public bool TransmissionState
        {
            get
            {
                return this.transmissionState;
            }
        }
        #endregion Properties
    }
    #endregion TransmissionStateChangedEventArgs

}
