using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;

namespace KTone.Core.KTIRFID
{
    /// <summary>
    /// <exclude />
    /// Provides data for the FactoryManager events.
    /// </summary>
    [Serializable]
    public class KTFactoryManagerEventArgs : EventArgs
    {
        #region Attributes

        private DateTime timeStamp;
        private int timeIntervalMS = 0;
        #endregion Attributes

        #region Constructors
        /// <summary>
        /// Initializes the Originator of the event Id and current Date and Time.
        /// </summary>
        /// <param name="timeIntervalMS">Time interval in miliseconds after which the server will shut down</param>
        public KTFactoryManagerEventArgs(int timeIntervalMS)
        {
            this.timeStamp = DateTime.Now;
            this.timeIntervalMS = timeIntervalMS;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns the timestamp when the event was fired.
        /// </summary>
        public DateTime TimeStamp
        {
            get
            {
                return this.timeStamp;
            }
        }

        /// <summary>
        /// Time interval in miliseconds after which the server will shut down. 
        /// For the ServerUp event this value will be zero.
        /// </summary>
        public int TimeIntervalMS
        {
            get
            {
                return this.timeIntervalMS;
            }
        }
        #endregion
    }

    /// <summary>
    /// <exclude />
    /// Provides data for the component events.
    /// </summary>
    [Serializable]
    public class KTComponentEventArgs : EventArgs
    {
        #region Attributes

        private DateTime timeStamp;

        private string eventId;

        private string componentId;

        private string componentName;

        private KTComponentCategory componentCategory;

        #endregion Attributes

        #region Constructors

        /// <summary>
        /// Initializes the Originator of the event Id and current Date and Time.
        /// </summary>
        /// <param name="component">Originator of the event</param>
        /// <param name="eventId">event Id</param>
        public KTComponentEventArgs(IKTComponent component, string eventId)
        {
            this.eventId = eventId;
            this.componentId = component.ComponentId;
            this.componentName = component.ComponentName;
            this.componentCategory = component.ComponentCategory;
            this.timeStamp = DateTime.Now;
        }

        /// <summary>
        /// Initializes the Originator of the event Id and current Date and Time.
        /// </summary>
        /// <param name="component">Originator of the event</param>
        /// <param name="eventId">event Id</param>
        /// <param name="timeStamp">timestamp of the event</param>
        public KTComponentEventArgs(IKTComponent component, string eventId, DateTime timeStamp)
        {
            this.eventId = eventId;
            this.componentId = component.ComponentId;
            this.componentName = component.ComponentName;
            this.componentCategory = component.ComponentCategory;
            this.timeStamp = timeStamp;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets the Originator of the event Id.
        /// </summary>
        public string ComponentId
        {
            get
            {
                return this.componentId;
            }
        }

        /// <summary>
        /// Gets the name of the component that generated the event
        /// </summary>
        public string ComponentName
        {
            get
            {
                return this.componentName;
            }
        }

        /// <summary>
        /// Gets the category of the component that generated the event
        /// </summary>
        public KTComponentCategory ComponentCategory
        {
            get
            {
                return this.componentCategory;
            }
        }

        /// <summary>
        /// Gets the event Id.
        /// </summary>
        public string EventId
        {
            get
            {
                return this.eventId;
            }
        }

        /// <summary>
        /// Returns the timestamp when the event was fired.
        /// </summary>
        public DateTime TimeStamp
        {
            get
            {
                return this.timeStamp;
            }
        }
        #endregion
    }

    [Serializable]
    public class KTFactoryActionEventArgs : EventArgs
    {
        #region Attributes

        private DateTime timeStamp;

        private string componentId;

        private ComponentActions compAction = ComponentActions.Removed;
        private KTComponentCategory compCatagory = KTComponentCategory.Reader;
        #endregion Attributes

        #region Constrctor

        public KTFactoryActionEventArgs(string componentId, KTComponentCategory componentCatagory, ComponentActions action)
        {
            this.componentId = componentId;
            this.timeStamp = DateTime.Now;
            this.compCatagory = componentCatagory;
            this.compAction = action;
        }

        public KTFactoryActionEventArgs(string componentId, DateTime timeStamp, KTComponentCategory componentCatagory, ComponentActions action)
        {
            this.componentId = componentId;
            this.timeStamp = timeStamp;
            this.compCatagory = componentCatagory;
            this.compAction = action;
        }


        #endregion Constrctor

        #region Properties

        /// <summary>
        /// Component Id for which event is generated
        /// </summary>
        public string ComponentId
        {
            get
            {
                return this.componentId;
            }
        }

        /// <summary>
        /// Returns the timestamp when the event was fired.
        /// </summary>
        public DateTime TimeStamp
        {
            get
            {
                return this.timeStamp;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public ComponentActions Action
        {
            get
            {
                return this.compAction;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public KTComponentCategory ComponentCatagory
        {
            get
            {
                return this.compCatagory;
            }
        }

        #endregion Properties
    }

    /// <summary>
    /// <exclude />
    /// Provides data for the device events namely, CommandSent,
    /// ResponseReceived and ErrorOccurred.
    /// </summary>
    [Serializable]
    public class RFIDReaderEventArgs : KTComponentEventArgs
    {
        #region Attributes

        /// <summary>
        /// The action represented by the event 
        /// </summary>
        private Action action;

        /// <summary>
        /// The name of the reader,that generated this event 
        /// </summary>
        private string readerName = string.Empty;

        /// <summary>
        /// The parameter hash giving the respose or command parameters. 
        /// </summary>
        private Dictionary<string, string> paramHash = null;

        /// <summary>
        /// The parameter hash giving the respose or command parameters. 
        /// </summary>
        private Dictionary<string, object> paramDict = null;
        /// <summary>
        /// The exception causing a failure may be null 
        /// </summary>
        private Exception exception;

        #endregion Attributes

        #region Constructors

        /// <summary>
        /// Initializes an instance of RFIDReaderEventArgs class.
        /// </summary>
        /// <param name="action">Indicates the type of action which triggered the event</param>
        /// <param name="component">originator of the packet event</param>
        /// <param name="readerName">Name of originator of the packet event</param>
        /// <param name="paramHash">Hashtable containing the data inthe packet sent/received</param>
        /// <param name="eventId">event Id</param>
        public RFIDReaderEventArgs(Action action, IKTComponent component, string readerName,
            Dictionary<string, string> paramHash, string eventId)
            : base(component, eventId)
        {
            this.action = action;
            this.readerName = readerName;
            this.exception = null;
            this.paramHash = paramHash;
        }

        public RFIDReaderEventArgs(Action action, IKTComponent component, string readerName,
            Dictionary<string, string> paramHash, Dictionary<string, object> paramDict, string eventId)
            : base(component, eventId)
        {
            this.action = action;
            this.readerName = readerName;
            this.exception = null;
            this.paramDict = paramDict;
            this.paramHash = paramHash;
        }

        /// <summary>
        /// Initializes an instance of RFIDReaderEventArgs class.
        /// </summary>
        /// <param name="action">Indicates the type of action which triggered the event</param>
        /// <param name="componentId">Originator of the packet event</param>
        /// <param name="readerName">Name of originator of the packet event</param>
        /// <param name="exception">Exception object</param>
        /// <param name="eventId">event Id</param>
        public RFIDReaderEventArgs(Action action, IKTComponent component, string readerName,
            Exception exception, string eventId)
            : base(component, eventId)
        {
            this.action = action;
            this.readerName = readerName;
            this.exception = exception;
            this.paramHash = null;
        }

        /// <summary>
        /// Initializes an instance of RFIDReaderEventArgs class.
        /// </summary>
        /// <param name="action">Indicates the type of action which triggered the event</param>
        /// <param name="componentId">Id of the originator of the packet event</param>
        /// <param name="readerName">Name of originator of the packet event</param>
        /// <param name="paramHash">Hashtable containing the data inthe packet sent/received</param>
        /// <param name="eventId">event Id</param>
        public RFIDReaderEventArgs(Action action, IKTComponent component, string readerName,
            Dictionary<string, object> paramHash, string eventId)
            : base(component, eventId)
        {
            this.action = action;
            this.readerName = readerName;
            this.exception = null;
            this.paramDict = paramHash;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns the PacketAction
        /// </summary>
        public Action Action
        {
            get
            {
                return action;
            }
        }

        /// <summary>
        /// Returns the reader name representing the originator of the event 
        /// </summary>
        public string ReaderName
        {
            get
            {
                return readerName;
            }
        }

        /// <summary>
        /// Original Exception object which was thrown while executing a reader command.
        /// </summary>
        public Exception Exception
        {
            get
            {
                return exception;
            }
        }

        /// <summary>
        ///A dictionary containing the data in the packet sent/received
        /// </summary>
        public Dictionary<string, string> ParamHash
        {
            get
            {
                return paramHash;
            }
        }

        /// <summary>
        ///A dictionary containing the data in the packet sent/received
        /// </summary>
        public Dictionary<string, object> ParamDict
        {
            get
            {
                return paramDict;
            }
        }
        #endregion
    }


    /// <summary>
    /// <exclude />
    /// Provides data for the EPC + Wavtrend filter events
    /// </summary>
    [Serializable]
    public class KTEpcWavetrendFilterEventArgs : KTComponentEventArgs
    {
        #region Attributes
        private IRFIDTag[] epcTags = null;
        private WTTag[] wtTags = null;

        /// <summary>
        /// The exception causing a failure, may be null 
        /// </summary>
        private Exception exception;
        #endregion Attributes

        #region Constructors

        /// <summary>
        /// Initializes an instance of KTEpcWavetrendFilterEventArgs class.
        /// </summary>
        /// <param name="component">Component that generated the event</param>
        /// <param name="epcTags">Array of EPC tags</param>
        /// <param name="wtTags">Array of Wavetrend tags</param>
        /// <param name="eventId">event Id</param>
        public KTEpcWavetrendFilterEventArgs(IKTComponent component, IRFIDTag[] epcTags,
            WTTag[] wtTags, string eventId)
            : base(component, eventId)
        {
            this.epcTags = epcTags;
            this.wtTags = wtTags;
            this.exception = null;
        }

        /// <summary>
        /// Initializes an instance of KTEpcWavetrendFilterEventArgs class.
        /// </summary>
        /// <param name="component">Component that generated the event</param>
        /// <param name="epcTags">Array of EPC tags</param>
        /// <param name="wtTags">Array of Wavetrend tags</param>
        /// <param name="timeStamp">timestamp</param>
        /// <param name="eventId">event Id</param>
        public KTEpcWavetrendFilterEventArgs(IKTComponent component, string filterName,
            IRFIDTag[] epcTags, WTTag[] wtTags, DateTime timeStamp, string eventId)
            : base(component, eventId, timeStamp)
        {
            this.epcTags = epcTags;
            this.wtTags = wtTags;
            this.exception = null;
        }
        /// <summary>
        /// Initializes an instance of KTEpcWavetrendFilterEventArgs class.
        /// </summary>
        /// <param name="component">Component that generated the event</param>
        /// <param name="exception">Exception object</param>
        /// <param name="eventId">event Id</param>
        public KTEpcWavetrendFilterEventArgs(IKTComponent component, string filterName,
            Exception exception, string eventId)
            : base(component, eventId)
        {
            this.epcTags = null;
            this.wtTags = null;
            this.exception = exception;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Original Exception object which was thrown while executing a filter command.
        /// </summary>
        public Exception Exception
        {
            get
            {
                return exception;
            }
        }

        /// <summary>
        /// Array of EPC tags
        /// </summary>
        public IRFIDTag[] EPCTags
        {
            get
            {
                return this.epcTags;
            }
        }

        /// <summary>
        /// Array of Wavetrend tags
        /// </summary>
        public WTTag[] WTTags
        {
            get
            {
                return this.wtTags;
            }
        }

        #endregion
    }

    /// <summary>
    /// <exclude />
    /// Provides data for the filter events
    /// </summary>
    [Serializable]
    public class KTRFIDFilterEventArgs : KTComponentEventArgs
    {
        #region Attributes

        private IRFIDTag[] tags = null;

        /// <summary>
        /// The exception causing a failure, may be null 
        /// </summary>
        private Exception exception;
        #endregion Attributes

        #region Constructors
        /// <summary>
        /// Initializes an instance of KTRFIDFilterEventArgs class.
        /// </summary>
        /// <param name="component">Component that generated the event</param>
        /// <param name="tags">Array of tags</param>
        /// <param name="timeStamp">timestamp</param>
        /// <param name="eventId">event Id</param>
        public KTRFIDFilterEventArgs(IKTComponent component, IRFIDTag[] tags, DateTime timeStamp,
            string eventId)
            : base(component, eventId, timeStamp)
        {
            this.tags = tags;
            this.exception = null;
        }
        #endregion

        #region Properties

        /// <summary>
        /// Original Exception object which was thrown while executing a filter command.
        /// </summary>
        public Exception Exception
        {
            get
            {
                return exception;
            }
        }

        /// <summary>
        /// Array of tags
        /// </summary>
        public IRFIDTag[] Tags
        {
            get
            {
                return this.tags;
            }
        }

        #endregion
    }

    /// <summary>
    /// Provides data for IOPinStatusChanged event.
    /// </summary>
    [Serializable]
    public class IOPinEventArgs : KTComponentEventArgs
    {
        #region Attributes
        private IOPinStatus[] inputPinStatus = null;
        private IOPinStatus[] outputPinStatus = null;
        #endregion Attributes

        #region Constructors
        /// <summary>
        /// Initializes an instance of IOPinEventArgs class.
        /// </summary>
        /// <param name="component">Originator of the event</param>
        /// <param name="eventId">event Id</param>
        /// <param name="inputPinStatus">Input pin status</param>
        public IOPinEventArgs(IKTComponent component, string eventId, IOPinStatus[] inputPinStatus)
            : base(component, eventId)
        {
            this.inputPinStatus = inputPinStatus;
            this.outputPinStatus = null;
        }

        /// <summary>
        /// Initializes an instance of IOPinEventArgs class.
        /// </summary>
        /// <param name="component">Originator of the event</param>
        /// <param name="eventId">event Id</param>
        /// <param name="inputPinStatus">Input pin status</param>
        /// <param name="outputPinStatus">Output pin status</param>
        public IOPinEventArgs(IKTComponent component, string eventId, IOPinStatus[] inputPinStatus, IOPinStatus[] outputPinStatus)
            : base(component, eventId)
        {
            this.inputPinStatus = inputPinStatus;
            this.outputPinStatus = outputPinStatus;
        }

        #endregion Constructors

        #region Properties
        public IOPinStatus[] InputPinStatus
        {
            get
            {
                return this.inputPinStatus;
            }
        }

        public IOPinStatus[] OutputPinStatus
        {
            get
            {
                return this.outputPinStatus;
            }
        }

        #endregion Properties
    }

    /// <summary>
    /// Provides data for ReaderEventArgs event.
    /// </summary>
    [Serializable]
    public class ReaderModeChangedArgs : KTComponentEventArgs
    {
        #region Attributes

        private RFIDReaderMode readerMode;

        #endregion Attribute
        #region Constructor
        public ReaderModeChangedArgs(IKTComponent component, string eventId, RFIDReaderMode readerMode)
            : base(component, eventId)
        {
            this.readerMode = readerMode;
        }
        #endregion Constructor

        /// <summary>
        /// Returns component state
        /// </summary>
        #region Propertis
        public RFIDReaderMode ReaderMode
        {
            get
            {
                return readerMode;
            }
        }

        #endregion Properties

    }


    /// <summary>
    /// <exclude />
    /// Provides data for the gateway events namely, CommandSent,
    /// ResponseReceived and ErrorOccurred.
    /// </summary>
    [Serializable]
    public class RFIDGatewayEventArgs : KTComponentEventArgs
    {
        #region Attributes

        /// <summary>
        /// The action represented by the event 
        /// </summary>
        private Action action;

        /// <summary>
        /// The Id of the reader node,that generated this event 
        /// </summary>
        private byte nodeId = 0;

        /// <summary>
        /// The parameter hash giving the respose or command parameters. 
        /// </summary>
        private Dictionary<string, string> paramHash = null;

        /// <summary>
        /// The exception causing a failure may be null 
        /// </summary>
        private Exception exception;

        #endregion Attributes

        #region Constructors

        /// <summary>
        /// Initializes an instance of RFIDGatewayEventArgs class.
        /// </summary>
        /// <param name="action">Indicates the type of action which triggered the event</param>
        /// <param name="componentId">Id of the originator of the packet event</param>
        /// <param name="paramHash">Hashtable containing the data inthe packet sent/received</param>
        /// <param name="eventId">event Id</param>
        public RFIDGatewayEventArgs(Action action, IKTComponent component, byte nodeId,
            Dictionary<string, string> paramHash, string eventId)
            : base(component, eventId)
        {
            this.action = action;
            this.exception = null;
            this.nodeId = nodeId;
            this.paramHash = paramHash;
        }


        /// <summary>
        /// Initializes an instance of RFIDGatewayEventArgs class.
        /// </summary>
        /// <param name="action">Indicates the type of action which triggered the event</param>
        /// <param name="componentId">Originator of the packet event</param>
        /// <param name="readerName">Name of originator of the packet event</param>
        /// <param name="exception">Exception object</param>
        /// <param name="eventId">event Id</param>
        public RFIDGatewayEventArgs(Action action, IKTComponent component, byte nodeId, Exception exception,
            string eventId)
            : base(component, eventId)
        {
            this.action = action;
            this.exception = exception;
            this.nodeId = nodeId;
            this.paramHash = null;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns the PacketAction
        /// </summary>
        public Action Action
        {
            get
            {
                return action;
            }
        }
        /// <summary>
        /// Returns the node id
        /// </summary>
        public byte NodeId
        {
            get
            {
                return this.nodeId;
            }
        }

        /// <summary>
        /// Original Exception object which was thrown while executing a reader command.
        /// </summary>
        public Exception Exception
        {
            get
            {
                return exception;
            }
        }

        /// <summary>
        ///A dictionary containing the data in the packet sent/received
        /// </summary>
        public Dictionary<string, string> ParamHash
        {
            get
            {
                return paramHash;
            }
        }

        #endregion
    }

    /// <summary>
    /// <exclude />
    /// Provides data for the StateChanged event of IKTElement
    /// </summary>
    [Serializable]
    public class ComponentStateChangeEventArgs : KTComponentEventArgs
    {
        #region Attributes

        private KTComponentState componentState;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance ComponentStateChangeEventArgs class.
        /// </summary>
        /// <param name="component">component</param>
        /// <param name="eventId">event id</param>
        /// <param name="componentStateHash">Component state</param>
        public ComponentStateChangeEventArgs(IKTComponent component, string eventId,
            KTComponentState componentState)
            : base(component, eventId)
        {
            this.componentState = componentState;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns component state
        /// </summary>
        public KTComponentState KTComponentState
        {
            get
            {
                return this.componentState;
            }
        }


        #endregion
    }


    /// <summary>
    /// <exclude />
    /// Provides data for the StateChanged event of IKTElement
    /// </summary>
    [Serializable]
    public class CommunicationStateChangeEventArgs : KTComponentEventArgs
    {
        #region Attributes

        private bool isOnline;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance CommunicationStateChangeEventArgs class.
        /// </summary>
        /// <param name="eventId">event id</param>
        /// <param name="componentStateHash">Dictionary containing component ids and their new states 
        /// (<see cref="KTone.Core.KTIRFID.ComponentStates"/>)</param>
        public CommunicationStateChangeEventArgs(IKTComponent component, string eventId, bool isOnline)
            : base(component, eventId)
        {
            this.isOnline = isOnline;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns a flag indicating the communication status of the component
        /// </summary>
        public bool IsOnline
        {
            get
            {
                return this.isOnline;
            }
        }


        #endregion

        #region Public Methods
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.ComponentId);
            if (this.isOnline)
                sb.Append(": Online");
            else
                sb.Append(": Offline");

            return sb.ToString();
        }
        #endregion Public Methods
    }

    /// <summary>
    /// <exclude />
    /// Provides data for the StateChanged event of GatewayNode
    /// </summary>
    [Serializable]
    public class GWCommunicationStateChangeEventArgs : CommunicationStateChangeEventArgs
    {
        #region Attributes

        private byte nodeId;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance CommunicationStateChangeEventArgs class.
        /// </summary>
        /// <param name="eventId">event id</param>
        /// <param name="componentStateHash">Dictionary containing component ids and their new states 
        /// (<see cref="KTone.Core.KTIRFID.ComponentStates"/>)</param>
        public GWCommunicationStateChangeEventArgs(IKTComponent component, string eventId, byte nodeId, bool isOnline)
            : base(component, eventId, isOnline)
        {
            this.nodeId = nodeId;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns the node id 
        /// </summary>
        public byte NodeId
        {
            get
            {
                return this.nodeId;
            }
        }

        #endregion
    }

    /// <summary>
    /// <exclude />
    /// Provides data for the SystemMonitorEvent
    /// </summary>
    [Serializable]
    public class SystemMonitorManagerEventArgs : KTComponentEventArgs
    {
        #region Attributes

        private SystemStateInfo[] systemState;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance SystemMonitorManagerEventArgs class.
        /// </summary>
        /// <param name="systemState">State of the system - active/idle</param>
        public SystemMonitorManagerEventArgs(IKTComponent component, string eventId, SystemStateInfo[] systemState)
            : base(component, eventId)
        {
            this.systemState = systemState;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns the state of the system(<see cref="KTone.Core.KTIRFID.SystemStateInfo"/>)
        /// </summary>
        public SystemStateInfo[] SystemState
        {
            get
            {
                return systemState;
            }
        }

        #endregion

        #region Public Methods
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (SystemStateInfo systemStateInfo in this.systemState)
            {
                sb.Append(systemStateInfo.ComponentID);
                sb.Append(":");
                if (systemStateInfo.IsOnline)
                    sb.Append("Online,");
                else
                    sb.Append("Offline,");
                sb.Append(systemStateInfo.State);
                sb.Append(",");
                sb.Append(systemStateInfo.StartTime.ToString());
                sb.Append(" - ");
                sb.Append(systemStateInfo.EndTime.ToString());
                sb.Append("\r\n");
            }
            return sb.ToString();
        }
        #endregion Public Methods
    }

    /// <summary>
    /// <exclude />
    /// Provides data for the SystemMonitorEvent
    /// </summary>
    [Serializable]
    public class SystemMonitorEventArgs : KTComponentEventArgs
    {
        #region Attributes

        private SystemState systemState;
        private DateTime startTime;
        private DateTime endTime;
        private bool isOnline;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance SystemMonitorEventArgs class.
        /// </summary>
        /// <param name="systemState">State of the system - active/idle</param>
        public SystemMonitorEventArgs(IKTComponent component, string eventId, SystemState systemState,
            DateTime startTime, DateTime endTime)
            : base(component, eventId)
        {
            this.systemState = systemState;
            this.startTime = startTime;
            this.endTime = endTime;
            this.isOnline = component.IsOnline;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns the state of the system(<see cref="KTone.Core.KTIRFID.SystemState"/>)
        /// </summary>
        public SystemState SystemState
        {
            get
            {
                return systemState;
            }
        }

        public DateTime StartTime
        {
            get
            {
                return this.startTime;
            }
        }

        public DateTime EndTime
        {
            get
            {
                return this.endTime;
            }
        }

        public bool IsOnline
        {
            get
            {
                return this.isOnline;
            }
        }

        #endregion
    }

    /// <summary>
    /// <exclude />
    /// Provides data for the ComponentMonitorEvent
    /// </summary>
    [Serializable]
    public class ComponentMonitorEventArgs : KTComponentEventArgs
    {
        #region Attributes

        private string message;
        private MessageLevel messageLevel;
        private string monitoredComponentId;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance ComponentMonitorEventArgs class.
        /// </summary>
        /// <param name="systemState">State of the system - active/idle</param>
        public ComponentMonitorEventArgs(IKTComponent component, string eventId, string message,
            MessageLevel messageLevel, string monitoredComponentId)
            : base(component, eventId)
        {
            this.message = message;
            this.messageLevel = messageLevel;
            this.monitoredComponentId = monitoredComponentId;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns the message.
        /// </summary>
        public String Message
        {
            get
            {
                return this.message;
            }
        }

        /// <summary>
        /// Returns the message level
        /// </summary>
        public MessageLevel MessageLevel
        {
            get
            {
                return this.messageLevel;
            }
        }

        /// <summary>
        /// Returns the id of the monitored component.
        /// </summary>
        public String MonitoredComponentId
        {
            get
            {
                return this.monitoredComponentId;
            }
        }

        #endregion
    }
    /// <summary>
    /// Provides data for the TagReadEvent. It contains list of the tags and the timestamp of the read cycle 
    /// in hour, minute and second.
    /// </summary>
    [Serializable]
    public class TagReadEventArgs : KTComponentEventArgs
    {
        #region Attributes
        private IRFIDTag[] tags;
        private string antennaName = string.Empty;
        #endregion Attributes

        #region Constructor

        /// <summary>
        /// Initializes an instance TagReadEventArgs class.
        /// </summary>
        /// <param name="component">Originator of the event</param>
        /// <param name="eventId">event Id</param>
        /// <param name="tags">A list of tags</param>
        /// <param name="tagReadTimeStamp">Tag read time stamp</param>
        /// <param name="antennaName">antenna name</param>
        public TagReadEventArgs(IKTComponent component, string eventId, IRFIDTag[] tags,
            DateTime tagReadTimeStamp, string antennaName)
            : base(component, eventId, tagReadTimeStamp)
        {
            this.tags = tags;
            this.antennaName = antennaName;
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Returns a list of tags.
        /// </summary>
        public IRFIDTag[] Tags
        {
            get
            {
                return this.tags;
            }
        }

        /// <summary>
        /// Returns the antenna name for the event
        /// </summary>
        public string AntennaName
        {
            get
            {
                return this.antennaName;
            }
        }
        #endregion Properties
    }
    [Serializable]
    public class KTPrinterEventArgs : KTComponentEventArgs
    {
        #region [KTPrinterEventArgs Attributes]
        private bool printSuccessful = false;
        private int numOfLabels = 0;
        private int numOfCopies = 0;
        private string operatorName = string.Empty;
        private string machineName = string.Empty;
        private List<Dictionary<string, string>> printDataHashList = new List<Dictionary<string, string>>();
        #endregion [KTPrinterEventArgs Attributes]

        #region [KTPrinterEventArgs Constructor]
        public KTPrinterEventArgs(List<Dictionary<string, string>> printDataHashList, bool printSuccessful, int numOfLabels, int numOfCopies,
            IKTComponent component, string eventId, string operatorName, string machineName)
            : base(component, eventId)
        {
            this.printDataHashList = printDataHashList;
            this.printSuccessful = printSuccessful;
            this.numOfLabels = numOfLabels;
            this.numOfCopies = numOfCopies;
            this.operatorName = operatorName;
            this.machineName = machineName;
        }
        #endregion [KTPrinterEventArgs Constructor]

        #region [KTPrinterEventArgs Properties]
        public bool PrintSuccessful
        {
            get { return this.printSuccessful; }
        }
        public int NumOfLabels
        {
            get { return this.numOfLabels; }
        }
        public int NumOfCopies
        {
            get { return this.numOfCopies; }
        }
        public string OperatorName
        {
            get { return this.operatorName; }
        }
        public string MachineName
        {
            get { return this.machineName; }
        }
        public List<Dictionary<string, string>> PrintDataHashList
        {
            get { return this.printDataHashList; }
        }
        #endregion [KTPrinterEventArgs Properties]
    }

    [Serializable]
    public class CacheRefreshEventArgs : KTComponentEventArgs
    {
        private bool refreshSuccessful = false;
        private double timeRequired = 0;
        int prevAssetCnt = 0;
        int prevZoneCnt = 0;
        int newAssetCnt = 0;
        int newZoneCnt = 0;

        public CacheRefreshEventArgs(IKTComponent component, string eventId, bool refreshSuccessful, double timeRequiredSec,
            int prevAssetCnt, int prevZoneCnt, int newAssetCnt, int newZoneCnt)
            : base(component, eventId)
        {
            this.newAssetCnt = newAssetCnt;
            this.newZoneCnt = newZoneCnt;
            this.prevAssetCnt = prevAssetCnt;
            this.prevZoneCnt = prevZoneCnt;

            this.refreshSuccessful = refreshSuccessful;
            timeRequired = timeRequiredSec;
        }

        public bool RefreshSuccessful
        {
            get { return refreshSuccessful; }
        }

        public double TimeRequiredInSec
        {
            get { return timeRequired; }
        }

        public int PreviousAssetCount
        {
            get { return prevAssetCnt; }
        }

        public int PreviousZoneCount
        {
            get { return prevZoneCnt; }
        }

        public int NewAssetCount
        {
            get { return newAssetCnt; }
        }

        public int NewZoneCount
        {
            get { return newZoneCnt; }
        }
    }

    [Serializable]
    public class SDCCacheRefreshEventArgs : KTComponentEventArgs
    {
        private bool refreshSuccessful = false;
        //TODO: Add more attributes as per req similar to CacheRefreshEventArgs
        public SDCCacheRefreshEventArgs(IKTComponent component, string eventId, bool refreshSuccessful)
            : base(component, eventId)
        {

            this.refreshSuccessful = refreshSuccessful;

        }

        public bool RefreshSuccessful
        {
            get { return refreshSuccessful; }
        }


    }

    /// <summary>
    /// Provides data for the AssetMovedEvent. It contains list of the assets , 
    /// source zone id and destination zone id  and time stamp
    /// </summary>
    [Serializable]
    public class AutoCICOMonitorEventArgs : KTComponentEventArgs
    {
        #region Attributes
        private List<string> assetsList = null;
        private int movedFromZoneID = -1;
        private int movedToZoneID = -1;
        private string movedFromZoneName = string.Empty;
        private string movedToZoneName = string.Empty;

        #endregion Attributes

        #region Constructors
        /// <summary>
        /// Initializes an instance of AutoCICOMonitorEventArgs class.
        /// </summary>
        /// <param name="assetsList">List of assets</param>
        /// <param name="movedFromZoneID">Zone Id from which asset moved</param>
        /// <param name="movedFromZoneName">Zone name from which asset moved</param>
        /// <param name="movedToZoneID">Zone Id towards asset moved</param>
        /// <param name="movedToZoneName">Zone name towards asset moved</param>
        /// <param name="component">Component that generated the event</param>
        /// <param name="eventId">Event Id</param>
        public AutoCICOMonitorEventArgs(List<string> assetsList, int movedFromZoneID, string movedFromZoneName,
                                int movedToZoneID, string movedToZoneName, IKTComponent component, string eventId)
            : base(component, eventId)
        {
            this.assetsList = assetsList;
            this.movedFromZoneID = movedFromZoneID;
            this.movedToZoneID = movedToZoneID;
            this.movedFromZoneName = movedFromZoneName;
            this.movedToZoneName = movedToZoneName;
        }
        #endregion Constructor

        #region Properties
        /// <summary>
        /// Returns List of assets moved
        /// </summary>
        public List<string> AssetsList
        {
            get { return this.assetsList; }
        }

        /// <summary>
        /// Zone ID from which tag is moved
        /// </summary>
        public int MovedFromZoneID
        {
            get { return this.movedFromZoneID; }
        }

        /// <summary>
        /// Zone name from which tag is moved
        /// </summary>
        public string MovedFromZoneName
        {
            get { return this.movedFromZoneName; }
        }

        /// <summary>
        /// Zone ID to which tag is moved
        /// </summary>
        public int MovedToZoneID
        {
            get { return this.movedToZoneID; }
        }

        /// <summary>
        /// Zone name to which tag is moved
        /// </summary>
        public string MovedToZoneName
        {
            get { return this.movedToZoneName; }
        }

        #endregion Properties
    }

    [Serializable]
    public class CommDriverEventArgs : KTComponentEventArgs
    {
        #region Attributes
        byte[] bytes;
        #endregion Attributes

        #region Constructor
        public CommDriverEventArgs(byte[] bytes, IKTComponent component, string eventId, DateTime timestamp)
            : base(component, eventId, timestamp)
        {
            if (bytes != null)
            {
                this.bytes = new byte[bytes.Length];
                Array.Copy(bytes, this.bytes, bytes.Length);
            }
        }
        #endregion Constructors

        #region Properties
        public byte[] Bytes
        {
            get { return this.bytes; }
        }
        #endregion Properties
    }

    #region [ KTTagMovedEventArgs ]
    /// <summary>
    /// Event argument for tag moved event
    /// </summary>
    [Serializable]
    public class KTTagMovedEventArgs : KTComponentEventArgs
    {
        #region Attributes
        private string movedFrom = string.Empty;
        private string movedTo = string.Empty;
        private string movedFromName = string.Empty;
        private string movedToName = string.Empty;
        private int movedFromGroupId = 1;
        private int movedToGroupId = 1;
        private string movedFromGroupName = "Default";
        private string movedToGroupName = "Default";
        private string tagID = string.Empty;
        private DateTime tagMovedTime;

        #endregion Attributes

        #region Constructors
        /// <summary>
        /// Initializes an instance of AWTagMovedEventArgs class.
        /// </summary>
        public KTTagMovedEventArgs(string tagID,
            string movedFrom, string movedFromName, int movedFromGroupId, string movedFromGroupName,
            string movedTo, string movedToName, int movedToGroupId, string movedToGroupName,
            DateTime tagMovedTime, IKTComponent component, string eventId)
            : base(component, eventId)
        {
            Init(tagID, movedFrom, movedFromName, movedFromGroupId, movedFromGroupName, movedTo, movedToName,
                movedToGroupId, movedToGroupName, tagMovedTime);
        }

        /// <summary>
        /// Initializes an instance of AWTagMovedEventArgs class.
        /// </summary>
        public KTTagMovedEventArgs(string tagID,
            string movedFrom, string movedFromName, int movedFromGroupId, string movedFromGroupName,
            string movedTo, string movedToName, int movedToGroupId, string movedToGroupName,
            DateTime tagMovedTime, IKTComponent component, string eventId, DateTime timestamp)
            : base(component, eventId, timestamp)
        {
            Init(tagID, movedFrom, movedFromName, movedFromGroupId, movedFromGroupName, 
                movedTo, movedToName, movedToGroupId, movedToGroupName, tagMovedTime);
        }

        private void Init(string tagID, string movedFrom, string movedFromName, int movedFromGroupId, string movedFromGroupName,
            string movedTo, string movedToName, int movedToGroupId, string movedToGroupName, DateTime tagMovedTime)
        {
            this.movedFrom = movedFrom;
            this.movedTo = movedTo;
            this.movedFromName = movedFromName;
            this.movedToName = movedToName;
            this.movedFromGroupId = movedFromGroupId;
            this.movedToGroupId = movedToGroupId;
            this.movedFromGroupName = movedFromGroupName;
            this.movedToGroupName = movedToGroupName;
            this.tagMovedTime = tagMovedTime;
            this.tagID = tagID;
        }
        #endregion

        #region Properties

        /// <summary>
        /// Reader id from which tag is moved
        /// </summary>
        public string MovedFrom
        {
            get { return this.movedFrom; }
        }

        /// <summary>
        /// Reader id to which tag is moved
        /// </summary>
        public string MovedTo
        {
            get { return this.movedTo; }
        }

        /// <summary>
        /// Reader name from which tag is moved
        /// </summary>
        public string MovedFromName
        {
            get { return this.movedFromName; }
        }

        /// <summary>
        /// Reader name to which tag is moved
        /// </summary>
        public string MovedToName
        {
            get { return this.movedToName; }
        }

        public int MovedFromGroupId
        {
            get { return this.movedFromGroupId; }
        }

        public int MovedToGroupId
        {
            get { return this.movedToGroupId; }
        }

        public string MovedFromGroupName
        {
            get { return this.movedFromGroupName; }
        }

        public string MovedToGroupName
        {
            get { return this.movedToGroupName; }
        }

        /// <summary>
        /// Time stamp
        /// </summary>
        public DateTime TagMovedTime
        {
            get { return this.tagMovedTime; }
        }
        /// <summary>
        /// Tag ID
        /// </summary>
        public string TagID
        {
            get
            {
                return this.tagID;
            }
        }
        #endregion Properties

    }
    #endregion [ KTTagMovedEventArgs ]

    #region [ KTTagJunctionEventArgs ]
    /// <summary>
    /// Event argument for junction tag moved event
    /// </summary>
    [Serializable]
    public class KTTagJunctionEventArgs : KTTagMovedEventArgs
    {
        #region Attributes
        private DateTime movedFromTime;
        private DateTime movedToTime;
        private string movedToReadPointName = string.Empty;
        private string movedFromReadPointName = string.Empty;
        private string movedFromLocation = string.Empty;
        private string movedToLocation = string.Empty;
        #endregion Attributes

        #region Constructors
        /// <summary>
        /// Initializes an instance of AWTagMovedEventArgs class.
        /// </summary>
        public KTTagJunctionEventArgs(string tagID, string movedFrom, string movedFromName,string moveFromGroupName,int moveFromGroupId,
                                    string movedTo, string movedToName,string moveToGroupName,int moveToGroupId, DateTime tagMovedTime,
                                    DateTime movedFromTime, DateTime movedToTime, string movedToDetails,
                                    string movedFromDetails, string moveToLocation, string moveFromLocation,
                                    IKTComponent component, string eventId)
            : base(tagID, movedFrom, movedFromName,moveFromGroupId,moveFromGroupName, movedTo, movedToName,
            moveToGroupId,moveToGroupName, tagMovedTime, component, eventId)
        {
            this.movedFromTime = movedFromTime;
            this.movedToTime = movedToTime;
            this.movedToReadPointName = movedToDetails;
            this.movedFromReadPointName = movedFromDetails;
            this.movedFromLocation = moveFromLocation;
            this.movedToLocation = moveToLocation;
        }


        #endregion

        #region Properties
        /// <summary>
        /// gives MovedToLocation
        /// </summary>
        public string MovedToLocation
        {
            get
            {
                return this.movedToLocation;
            }
        }
        public string MovedFromLocation
        {
            get
            {
                return this.movedFromLocation;
            }
        }
        /// <summary>
        /// Reader Details from which tag is moved
        /// </summary>
        public string MovedFromReadPointName
        {
            get { return this.movedFromReadPointName; }
        }

        /// <summary>
        /// Reader Details to which tag is moved
        /// </summary>
        public string MovedToReadPointName
        {
            get { return this.movedToReadPointName; }
        }

        /// <summary>
        /// MovedToTime
        /// </summary>
        public DateTime MovedToTime
        {
            get { return this.movedToTime; }
        }

        /// <summary>
        /// MovedToTime
        /// </summary>
        public DateTime MovedFromTime
        {
            get { return this.movedFromTime; }
        }
        #endregion Properties

    }
    #endregion [ KTTagJunctionEventArgs ]

    #region [ KTTagSeenEventArgs ]
    /// <summary>
    /// Event argument for tag moved event
    /// </summary>
    [Serializable]
    public class KTTagSeenEventArgs : KTComponentEventArgs
    {
        #region Attributes
        private string readerID = string.Empty;
        private string readerName = string.Empty;
        private string tagID = string.Empty;
        private DateTime tagSeenTime;
        private string readerDetails = string.Empty;
        private string readPointName = string.Empty;
        private string eventType = string.Empty;
        private string readpointLocation = string.Empty;
        private string readerGroupName = string.Empty;
        private int readerGroupId = 1;
        #endregion Attributes

        #region Constructors
        /// <summary>
        /// Initializes an instance of KTTagSeenEventArgs class.
        /// </summary>
        public KTTagSeenEventArgs(string readerID, string readerName,string readerGroupName,int readerGroupId,
                                    string tagID, DateTime tagSeenTime, string readPointDetail, string eventType,
                                    string readPointLocation, IKTComponent component, string eventId)
            : base(component, eventId)
        {
            this.readerID = readerID;
            this.readerName = readerName;
            this.tagID = tagID;
            this.tagSeenTime = tagSeenTime;
            this.readerDetails = readPointDetail;
            this.readPointName = readPointDetail;
            this.eventType = eventType;
            this.readpointLocation = readPointLocation;
            this.readerGroupId = readerGroupId;
            this.readerGroupName = readerGroupName;
        }


        #endregion

        #region Properties
        /// <summary>
        /// Gets the ReadpointLocation
        /// </summary>
        public string ReadPointLocation
        {
            get
            { return this.readpointLocation; }
        }
        /// <summary>
        /// Reader id where the Tag is seen
        /// </summary>
        public string ReaderID
        {
            get { return this.readerID; }
        }
        /// <summary>
        /// Reader name where the Tag is seen
        /// </summary>
        public string ReaderName
        {
            get { return this.readerName; }
        }
        /// <summary>
        /// Time stamp
        /// </summary>
        public DateTime TagSeenTime
        {
            get { return this.tagSeenTime; }
        }
        /// <summary>
        /// Tag ID
        /// </summary>
        public string TagID
        {
            get
            {
                return this.tagID;
            }
        }
        public string ReaderDetails
        {
            get { return this.readerDetails; }
        }

        public string ReadPointName
        {
            get { return this.readPointName; }
        }
        public string EventType
        {

            get
            {
                return this.eventType;
            }
        }
        public string ReaderGroupName
        {
            get
            {
                return this.readerGroupName;
            }
        }

        public int ReaderGroupId
        {
            get
            {
                return this.readerGroupId;
            }
        }
        #endregion Properties

    }


    #endregion [ KTTagSeenEventArgs ]


    #region [OnMailSendEventArgs]
    /// <summary>
    /// Event genrated when any mail send to perticular Recipient
    /// </summary>
    [Serializable]
    public class OnMailSendEventArgs : KTComponentEventArgs
    {
        #region Attributes

        private string mailData = string.Empty;
        private string errorMsg = string.Empty;

        #endregion Attributes

        #region Constructors
        /// <summary>
        /// Initializes an instance of OnMailSendEventArgs class.
        /// </summary>
        public OnMailSendEventArgs(string mailMessageData, string errorMsg,
                                    IKTComponent component, string eventId)
            : base(component, eventId)
        {
            this.mailData = mailMessageData;
            this.errorMsg = errorMsg;
        }


        #endregion Constructors

        #region Properties


        public string MailMessageData
        {
            get { return this.mailData; }
        }

        public string ErrorMsg
        {
            get { return this.errorMsg; }
        }

        #endregion Properties
    }
    #endregion [OnMailSendEventArgs]

    #region [OnMonitorMailSentEventArgs]
    /// <summary>
    /// Event generated when monitoring service sends a state change mail
    /// </summary>
    [Serializable]
    public class OnMonitorMailSentEventArgs : EventArgs
    {
        #region Attributes

        public string from = string.Empty;
        public List<string> to = new List<string>();
        public List<string> cc = new List<string>();
        public List<string> bcc = new List<string>();
        public string subject = string.Empty;
        public string body = string.Empty;
        /// <summary>
        /// Idle,Active,Online,Offline
        /// </summary>
        public string status = string.Empty;

        private string errorMsg = string.Empty;

        #endregion Attributes

        #region Constructors
        /// <summary>
        /// Initializes an instance of OnMonitorMailSentEventArgs class.
        /// </summary>
        public OnMonitorMailSentEventArgs(MailMessage mailMsg, string status, string errorMsg)
            : base()
        {
            if (mailMsg != null)
            {
                if (mailMsg.From != null)
                    this.from = mailMsg.From.Address;
                if (mailMsg.To != null)
                {
                    foreach(MailAddress address in mailMsg.To)
                        this.to.Add(address.Address);
                }
                if (mailMsg.CC != null)
                {
                    foreach (MailAddress address in mailMsg.CC)
                        this.cc.Add(address.Address);
                }
                if (mailMsg.Bcc != null)
                {
                    foreach (MailAddress address in mailMsg.Bcc)
                        this.bcc.Add(address.Address);
                }

                this.subject = mailMsg.Subject;
                this.body = mailMsg.Body;
                this.status = status;
            }
            this.errorMsg = errorMsg;
        }


        #endregion Constructors

        #region Properties

        public string ErrorMsg
        {
            get { return this.errorMsg; }
        }

        public string From
        {
            get
            {
                return this.from;
            }
        }

        public List<string> To
        {
            get
            {
                return this.To;
            }
        }
        public List<string> CC
        {
            get
            {
                return this.cc;
            }
        }

        public List<string> Bcc
        {
            get
            {
                return this.bcc;
            }
        }

        public string Subject
        {
            get
            {
                return this.subject;
            }
        }

        public string Body
        {
            get
            {
                return this.body;
            }
        }

        public string Status
        {
            get
            {
                return this.status;
            }
        }
        #endregion Properties

        #region Public Methods
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            #region From
            sb.Append("From : ");
            sb.Append(this.from);
            sb.Append(Environment.NewLine);
            #endregion From

            #region To

            sb.Append("To : ");
            foreach (string address in this.to)
            {
                sb.Append(address);
                sb.Append(";");
            }
            sb.Append(Environment.NewLine);
            #endregion To

            #region CC
            sb.Append("CC : ");
            foreach (string address in this.cc)
            {
                sb.Append(address);
                sb.Append(";");
            }
            sb.Append(Environment.NewLine);
            #endregion CC

            #region Bcc
            sb.Append("Bcc : ");
            foreach (string address in this.bcc)
            {
                sb.Append(address);
                sb.Append(";");
            } 
            sb.Append(Environment.NewLine);
            #endregion Bcc

            #region Subject
            sb.Append("Subject : ");
            sb.Append(this.subject);
            sb.Append(Environment.NewLine);
            #endregion Subject

            #region Message
            sb.Append("Message : ");
            sb.Append(this.body);
            if (this.errorMsg != string.Empty)
            {
                sb.Append("Error : ");
                sb.Append(this.errorMsg);
            }
            #endregion Message

            return sb.ToString();
        }
        #endregion Public Methods
    }
    #endregion [OnMonitorMailSentEventArgs]

    #region [ A G E N T -- EPC Agent / WaveTrendDB Agent / KTAgentEpcHistory / Section - Inventory / ActiveWave / JunctionAgent]

    #region [ KTAgentEpcDB ]
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public abstract class OnKTAgentEventArgs : KTComponentEventArgs
    {
        #region [ Attributes ]
        string mAgentName;
        string mAgentCategory;
        string[] mTagSNList;
        string[] mTagURNList;
        #endregion [ Attributes ]

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AgentName"></param>
        /// <param name="AgentCategory"></param>
        /// <param name="TagSNList"></param>
        /// <param name="TagURNList"></param>
        /// <param name="AgentId"></param>
        /// <param name="AgentEventId"></param>
        public OnKTAgentEventArgs
            (
            string AgentCategory,
            string[] TagSNList,
            string[] TagURNList,
            IKTComponent component,
            string AgentEventId
            )
            : base(component, AgentEventId)
        {
            mAgentName = AgentName;
            mAgentCategory = AgentCategory;
            mTagURNList = TagURNList;
            mTagSNList = TagSNList;
        }

        #region [ Properties ]

        #region [ Properties From Base]
        ///// <summary>
        ///// 
        ///// </summary>
        //public string AgentId
        //{
        //    get
        //    {
        //        return base.ComponentId;
        //    }
        //}

        /// <summary>
        /// 
        /// </summary>
        public string AgentEventId
        {
            get
            {
                return base.EventId;
            }
        }

        ///// <summary>
        ///// Get the date and time as a string when the RFID-Tag is processed.
        ///// </summary>
        //public string AgentDataTime
        //{
        //    get { return base.TimeStamp.ToString(); }
        //}

        #endregion [ Properties From Base]

        /// <summary>
        /// 
        /// </summary>
        public string AgentName
        {
            get
            {
                return this.mAgentName;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public string AgentCategory
        {
            get
            {
                return this.mAgentCategory;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string[] TagURNArr
        {
            get
            {
                return this.mTagURNList;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public string[] TagSNArr
        {
            get
            {
                return this.mTagSNList;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int TagsCount
        {
            get { return this.TagURNArr.Length; }
        }

        #endregion [ Properties ]
    }


    /// <summary>
    /// Provides data for the KTAgentDataProcessed event. 
    /// </summary>
    [Serializable]
    public class OnKTAgentDataReceivedEventArgs : OnKTAgentEventArgs
    {
        string mAntennaName;
        string mOriginComponentId;
        string mOriginComponentEventId;
        string mOriginTagReadTimeStamp;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AgentEventId"></param>
        /// <param name="AgentId"></param>
        /// <param name="AgentName"></param>
        /// <param name="AgentCategory"></param>
        /// <param name="AntennaName"></param>
        /// <param name="OriginComponentId"></param>
        /// <param name="OriginComponentEventId"></param>
        /// <param name="OriginTagReadTimeStamp"></param>
        /// <param name="TagSNList"></param>
        /// <param name="TagUrns"></param>
        public OnKTAgentDataReceivedEventArgs
            (
                string AgentEventId, //base
                IKTComponent component,//base
                string AgentCategory,//base

                string AntennaName,
                string OriginComponentId,
                string OriginComponentEventId,
                string OriginTagReadTimeStamp,
                string[] TagSNList, //base
                string[] TagUrns//base
            )
            : base(AgentCategory, TagSNList, TagUrns, component, AgentEventId)
        {

            this.mAntennaName = AntennaName;
            this.mOriginComponentId = OriginComponentId;
            this.mOriginComponentEventId = OriginComponentEventId;
            this.mOriginTagReadTimeStamp = OriginTagReadTimeStamp;
        }


        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public string AntennaName
        {
            get
            {
                return this.mAntennaName;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string OriginComponentId
        {
            get { return this.mOriginComponentId; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string OriginComponentEventId
        {
            get { return this.mOriginComponentEventId; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string OriginTagReadTimeStamp
        {
            get { return this.mOriginTagReadTimeStamp; }
        }


        #endregion
    }


    /// <summary>
    /// Provides data for the KTAgentDataProcessed event. 
    /// </summary>
    [Serializable]
    public class OnKTAgentDataProcessedEventArgs : OnKTAgentEventArgs
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AgentEventId"></param>
        /// <param name="AgentCategory"></param>
        /// <param name="TagSNList"></param>
        /// <param name="TagURNList"></param>
        public OnKTAgentDataProcessedEventArgs(string AgentEventId,
            IKTComponent component, string AgentCategory,
            string[] TagSNList, string[] TagURNList)
            : base(AgentCategory, TagSNList, TagURNList, component, AgentEventId)
        {

        }

    }

    #endregion [ KTAgentEpcDB ]

    #region [ KTAgentWaveTrendDB ]

    /// <summary>
    /// <exclude />
    /// Provides data for the component events.
    /// </summary>
    [Serializable]
    public abstract class OnKTAgentWTEventArgs : KTComponentEventArgs
    {
        #region [ Attributes ]

        /// <summary>
        /// 
        /// </summary>
        string mAgentCategory;

        /// <summary>
        /// 
        /// </summary>
        string[] mTagPukCodeList;

        /// <summary>
        /// 
        /// </summary>
        string[] mTagIdList;

        /// <summary>
        /// 
        /// </summary>
        string[] mTagIdentityList;

        #endregion [ Attributes ]

        #region [ properties ]

        /// <summary>
        /// 
        /// </summary>
        public string[] TagIdentityList
        {
            get { return mTagIdentityList; }

        }

        /// <summary>
        /// 
        /// </summary>
        public string AgentName
        {
            get { return this.ComponentName; }

        }

        /// <summary>
        /// 
        /// </summary>
        public string AgentCategory
        {
            get { return mAgentCategory; }

        }

        /// <summary>
        /// 
        /// </summary>
        public string[] TagPukCodeList
        {
            get { return mTagPukCodeList; }

        }

        /// <summary>
        /// 
        /// </summary>
        public string[] TagIdList
        {
            get { return mTagIdList; }
        }

        #endregion [ properties ]

        #region [ Constructor ]
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="AgentCategory"></param>
        /// <param name="TagPukCodeList"></param>
        /// <param name="TagIdList"></param>
        /// <param name="AgentId"></param>
        /// <param name="AgentEventId"></param>
        /// <param name="TagIdentityList"></param>
        public OnKTAgentWTEventArgs
            (
            string AgentCategory,
            string[] TagPukCodeList,
            string[] TagIdList,
            IKTComponent component,
            string AgentEventId,
            string[] TagIdentityList)
            : base(
            component, AgentEventId)
        {
            mAgentCategory = AgentCategory;
            mTagPukCodeList = TagPukCodeList;
            mTagIdList = TagIdList;
            mTagIdentityList = TagIdentityList;
        }
        #endregion [ Constructor ]
    }


    /// <summary>
    /// <exclude />
    /// Provides data for the component events.
    /// </summary>
    [Serializable]
    public class OnKTAgentWTDataRecvEvtArgs : OnKTAgentWTEventArgs
    {
        #region [ Attributes ]
        /// <summary>
        /// 
        /// </summary>
        string mOriginComponentId;

        /// <summary>
        /// 
        /// </summary>
        string mOriginComponentEventId;

        /// <summary>
        /// 
        /// </summary>
        string mOriginTagReadTimeStamp;
        #endregion [ Attributes ]

        #region [ Properties ]


        /// <summary>
        /// 
        /// </summary>
        public string OriginComponentId
        {
            get { return mOriginComponentId; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string OriginComponentEventId
        {
            get { return mOriginComponentEventId; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string OriginTagReadTimeStamp
        {
            get { return mOriginTagReadTimeStamp; }
        }

        #endregion [ Properties ]

        #region [ Constructor ]
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="AgentEventId"></param>
        /// <param name="AgentId"></param>
        /// <param name="AgentName"></param>
        /// <param name="AgentCategory"></param>
        /// <param name="OriginComponentId"></param>
        /// <param name="OriginComponentEventId"></param>
        /// <param name="OriginTagReadTimeStamp"></param>
        /// <param name="TagPukCodeList"></param>
        /// <param name="TagIdList"></param>
        public OnKTAgentWTDataRecvEvtArgs
            (
                string AgentEventId, //base
                IKTComponent component,//base
                string AgentCategory,//base
                string OriginComponentId,
                string OriginComponentEventId,
                string OriginTagReadTimeStamp,
                string[] TagPukCodeList, //base
                string[] TagIdList,//base
                string[] TagIdentity //base
            )
            : base(AgentCategory, TagPukCodeList, TagIdList, component, AgentEventId, TagIdentity)
        {
            this.mOriginComponentId = OriginComponentId;
            this.mOriginComponentEventId = OriginComponentEventId;
            this.mOriginTagReadTimeStamp = OriginTagReadTimeStamp;
        }
        #endregion [ Constructor ]
    }


    /// <summary>
    /// <exclude />
    /// Provides data for the component events.
    /// </summary>
    [Serializable]
    public class OnKTAgentWTDataProcessedEvtArgs : OnKTAgentWTEventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        string mMsgFromDB = @"\r\n";

        /// <summary>
        /// 
        /// </summary>
        public string MsgFromDB
        {
            get { return mMsgFromDB; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="AgentEventId"></param>
        /// <param name="AgentId"></param>
        /// <param name="AgentCategory"></param>
        /// <param name="TagPukCodeList"></param>
        /// <param name="TagIdList"></param>
        /// <param name="MsgFromDB"></param>
        public OnKTAgentWTDataProcessedEvtArgs(string AgentEventId,
            IKTComponent component, string AgentCategory,
            string[] TagPukCodeList, string[] TagIdList, string[] tagIdentity, string MsgFromDB)
            : base(AgentCategory, TagPukCodeList, TagIdList, component, AgentEventId, tagIdentity)
        {
            mMsgFromDB = MsgFromDB;
        }
    }

    #endregion [ KTAgentWaveTrendDB ]

    #region [ KTAgentEpcHistory ]

    [Serializable]
    public class OnKTAgtEpcHistoryDataReceivedEventArgs : OnKTAgentDataReceivedEventArgs
    {
        public OnKTAgtEpcHistoryDataReceivedEventArgs
            (
                string AgentEventId,
                IKTComponent component,
                string AgentCategory,

                string AntennaName,
                string OriginComponentId,
                string OriginComponentEventId,
                string OriginTagReadTimeStamp,
                string[] TagSNList,
                string[] TagUrns
            )
            : base(
                AgentEventId,
                component,
                AgentCategory,

                AntennaName,
                OriginComponentId,
                OriginComponentEventId,
                OriginTagReadTimeStamp,
                TagSNList,
                TagUrns
            )
        {

        }

    }

    [Serializable]
    public class OnKTAgtEpcHistoryDataProcessedEventArgs : OnKTAgentEventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        string mMsgFromDB = @"\r\n";

        /// <summary>
        /// 
        /// </summary>
        public string MsgFromDB
        {
            get { return mMsgFromDB; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AgentEventId"></param>
        /// <param name="AgentId"></param>
        /// <param name="AgentName"></param>
        /// <param name="AgentCategory"></param>
        /// <param name="TagSNList"></param>
        /// <param name="TagURNList"></param>
        public OnKTAgtEpcHistoryDataProcessedEventArgs(string AgentEventId,
            IKTComponent component, string AgentCategory,
            string[] TagSNList, string[] TagURNList, string MsgFromDB)
            : base(AgentCategory, TagSNList, TagURNList, component, AgentEventId)
        {
            this.mMsgFromDB = MsgFromDB;
        }
    }

    #endregion [ KTAgentEpcHistory ]

    #region [ KTAgentActiveWavedDB ]

    /// <summary>
    /// <exclude />
    /// Provides data for the component events.
    /// </summary>
    [Serializable]
    public abstract class KTAWAgentEventArgs : KTComponentEventArgs
    {
        #region Constructor
        public KTAWAgentEventArgs(IKTComponent component, string eventId)
            : base(component, eventId)
        {

        }
        #endregion Constructor
    }

    /// <summary>
    /// <exclude />
    /// Provides data for the component events.
    /// </summary>
    [Serializable]
    public class KTAWAgentTagMovedEventArgs : KTAWAgentEventArgs
    {
        #region Attributes

        private string tagID = string.Empty;
        private ushort movedFromFieldGeneratorID = 0;
        private ushort movedToFieldGeneratorID = 0;
        private DateTime tagMovedTime;
        private string eventType = string.Empty;

        private string movedTo;
        private string movedFrom;
        #endregion Attributes

        #region Constructors Tag Moved
        /// <summary>
        /// Initializes an instance of KTAWAgentTagMovedEventArgs class.
        /// </summary>
        public KTAWAgentTagMovedEventArgs(string tagID, string movedFrom, ushort movedFromFieldGeneratorID,
            string movedTo, ushort movedToFieldGeneratorID, DateTime tagMovedTime, string eventType,
            IKTComponent component, string eventId)
            : base(component, eventId)
        {
            this.movedFrom = movedFrom;
            this.movedTo = movedTo;
            this.movedFromFieldGeneratorID = movedFromFieldGeneratorID;
            this.movedToFieldGeneratorID = movedToFieldGeneratorID;
            this.tagMovedTime = tagMovedTime;
            this.tagID = tagID;
        }
        #endregion Constructors Tag Moved

        #region Properties Tag Moved

        /// <summary>
        /// Moved from FieldGenerator id from which tag is moved
        /// </summary>
        public ushort MovedFromFieldGeneratorID
        {
            get { return this.movedFromFieldGeneratorID; }
        }

        /// <summary>
        /// Moved to FieldGenerator id to which tag is moved
        /// </summary>
        public ushort MovedToFieldGeneratorID
        {
            get { return this.movedToFieldGeneratorID; }
        }

        /// <summary>
        /// Time stamp
        /// </summary>
        public DateTime TagMovedTime
        {
            get { return this.tagMovedTime; }
        }
        /// <summary>
        /// Tag ID
        /// </summary>
        public string TagID
        {
            get
            {
                return this.tagID;
            }
        }
        /// <summary>
        /// EventType
        /// </summary>
        public string EventType
        {
            get { return this.eventType; }
        }

        /// <summary>
        /// MovedTo
        /// </summary>
        public string MovedTo
        {
            get { return this.movedTo; }
        }

        /// <summary>
        /// MovedFrom
        /// </summary>
        public string MovedFrom
        {
            get { return this.movedFrom; }
        }

        #endregion Properties Tag Moved
    }

    public class KTAWAgentTagSeenEventArgs : KTAWAgentEventArgs
    {
        #region Attributes

        private string tagID = string.Empty;
        private DateTime tagSeenTime;
        private string tagSeenFieldGeneratorID = string.Empty;
        private string eventType = string.Empty;

        #endregion Attributes

        #region Constructors Tag Seen
        /// <summary>

        /// </summary>
        public KTAWAgentTagSeenEventArgs(string tagID, DateTime tagSeenTime, string tagSeenFieldGeneratorID, string eventType,
                                    IKTComponent component, string eventId)
            : base(component, eventId)
        {
            this.tagID = tagID;
            this.tagSeenTime = tagSeenTime;
            this.tagSeenFieldGeneratorID = tagSeenFieldGeneratorID;
            this.eventType = eventType;
        }
        #endregion Constructed Tag Seen

        #region Properties Tag Seen

        /// <summary>
        /// Time stamp
        /// </summary>
        public DateTime TagSeenTime
        {
            get { return this.tagSeenTime; }
        }
        /// <summary>
        /// Tag ID
        /// </summary>
        public string TagID
        {
            get
            {
                return this.tagID;
            }
        }
        /// <summary>
        /// EventType
        /// </summary>
        public string EventType
        {
            get { return this.eventType; }
        }
        /// <summary>
        /// TagSeenFieldGeneratorID
        /// </summary>
        public string TagSeenFieldGeneratorID
        {
            get { return this.tagSeenFieldGeneratorID; }
        }
        #endregion Properties Tag Seen
    }

    #endregion [ KTAgentActiveWavedDB ]

    #region [ KTJunctionAgent ]
    [Serializable]
    public class KTJunctionAgentEventArgs : KTComponentEventArgs
    {
        #region Constructor
        public KTJunctionAgentEventArgs(IKTComponent component, string eventId)
            : base(component, eventId)
        {

        }
        #endregion Constructor
    }

    [Serializable]
    public class KTTagMovedAgentEventArgs : KTJunctionAgentEventArgs
    {
        #region Attributes

        private string movedFrom = string.Empty;
        private string movedTo = string.Empty;
        private string movedFromName = string.Empty;
        private string movedToName = string.Empty;
        private string tagID = string.Empty;
        private DateTime tagMovedTime;
        private string movedToReadPointName = string.Empty;
        private string movedFromReadPointName = string.Empty;

        #endregion Attributes

        #region Constructors Tag Moved
        /// <summary>
        /// Initializes an instance of KTJunctionAgentTagMovedEventArgs class.
        /// </summary>
        public KTTagMovedAgentEventArgs(string tagID, string movedFrom, string movedFromName,
                                    string movedTo, string movedToName, DateTime tagMovedTime, string movedFromReadPointName, string movedToReadPointName,
                                    IKTComponent component, string eventId)
            : base(component, eventId)
        {
            this.movedFrom = movedFrom;
            this.movedTo = movedTo;
            this.movedFromName = movedFromName;
            this.movedToName = movedToName;
            this.tagMovedTime = tagMovedTime;
            this.tagID = tagID;
            this.movedToReadPointName = movedToReadPointName;
            this.movedFromReadPointName = movedFromReadPointName;
        }
        #endregion Constructors Tag Moved

        #region Properties Tag Moved

        /// <summary>
        /// Reader id from which tag is moved
        /// </summary>
        public string MovedFrom
        {
            get { return this.movedFrom; }
        }

        /// <summary>
        /// Reader id to which tag is moved
        /// </summary>
        public string MovedTo
        {
            get { return this.movedTo; }
        }

        /// <summary>
        /// Reader name from which tag is moved
        /// </summary>
        public string MovedFromName
        {
            get { return this.movedFromName; }
        }

        /// <summary>
        /// Reader name to which tag is moved
        /// </summary>
        public string MovedToName
        {
            get { return this.movedToName; }
        }

        /// <summary>
        /// Time stamp
        /// </summary>
        public DateTime TagMovedTime
        {
            get { return this.tagMovedTime; }
        }
        /// <summary>
        /// Tag ID
        /// </summary>
        public string TagID
        {
            get
            {
                return this.tagID;
            }
        }
        /// <summary>
        /// MovedFromReadPointName
        /// </summary>
        public string MovedFromReadPointName
        {
            get
            {
                return this.movedFromReadPointName;
            }
        }
        /// <summary>
        /// MovedToReadPointName
        /// </summary>
        public string MovedToReadPointName
        {
            get
            {
                return this.movedToReadPointName;
            }
        }
        #endregion Properties Tag Moved
    }

    [Serializable]
    public class KTJunctionAgentTagSeenEventArgs : KTJunctionAgentEventArgs
    {
        #region Attributes

        private string readerID = string.Empty;
        private string readerName = string.Empty;
        private string tagID = string.Empty;
        private DateTime tagSeenTime;
        private string readerDetails = string.Empty;
        private string readPointName = string.Empty;
        private string eventType = string.Empty;

        #endregion Attributes

        #region Constructors Tag Seen
        /// <summary>

        /// </summary>
        public KTJunctionAgentTagSeenEventArgs(string readerID, string readerName,
                                    string tagID, DateTime tagSeenTime, string readPointDetails, string readPointName,
                                    IKTComponent component, string eventId, string eventType)
            : base(component, eventId)
        {
            this.readerID = readerID;
            this.readerName = readerName;
            this.tagID = tagID;
            this.tagSeenTime = tagSeenTime;
            this.readerDetails = readPointDetails;
            this.readPointName = readPointDetails;
            this.eventType = eventType;
        }
        #endregion Constructed Tag Seen

        #region Properties Tag Seen

        /// <summary>
        /// Reader id where the Tag is seen
        /// </summary>
        public string ReaderID
        {
            get { return this.readerID; }
        }
        /// <summary>
        /// Reader name where the Tag is seen
        /// </summary>
        public string ReaderName
        {
            get { return this.readerName; }
        }
        /// <summary>
        /// Time stamp
        /// </summary>
        public DateTime TagSeenTime
        {
            get { return this.tagSeenTime; }
        }
        /// <summary>
        /// Tag ID
        /// </summary>
        public string TagID
        {
            get
            {
                return this.tagID;
            }
        }
        /// <summary>
        /// ReadPointName
        /// </summary>
        public string ReadPointName
        {
            get { return this.readPointName; }
        }
        /// <summary>
        /// EventType
        /// </summary>
        public string EventType
        {
            get { return this.eventType; }
        }
        /// <summary>
        /// ReaderDetails
        /// </summary>
        public string ReadPointDetails
        {
            get { return this.readerDetails; }
        }
        #endregion Properties Tag Seen
    }

    #endregion [ KTJunctionAgent ]



    #region [ KTRPGAgent ]
    [Serializable]
    public class KTAssetInventoryAgentEventArgs : KTComponentEventArgs
    {
        RPGAssetStatus assetStatus = RPGAssetStatus.NONE;

        #region Constructor
        public KTAssetInventoryAgentEventArgs(IKTComponent component, string eventId, RPGAssetStatus assetStatus)
            : base(component, eventId)
        {
            this.assetStatus = assetStatus;
        }
        #endregion Constructor

        /// <summary>
        /// Reader id from which tag is moved
        /// </summary>
        public RPGAssetStatus AssetStatus
        {
            get { return this.assetStatus; }
        }
    }

    [Serializable]
    public class KTAssetInventoryAgentTagMovementEventArgs : KTAssetInventoryAgentEventArgs
    {
        #region Attributes
        private KTAssetDetails asset = null;
        private DateTime tagMovedTime;
        private KTAssetDetails movedFromLocation;
        private KTAssetDetails movedToLocation;
        #endregion Attributes

        #region Constructors Tag Moved
        /// <summary>
        /// Initializes an instance of KTJunctionAgentTagMovedEventArgs class.
        /// </summary>
        public KTAssetInventoryAgentTagMovementEventArgs(KTAssetDetails asset, DateTime tagMovedTime, KTAssetDetails fromLoc, KTAssetDetails toLoc,
            RPGAssetStatus status, IKTComponent component, string eventId)
            : base(component, eventId, status)
        {
            this.tagMovedTime = tagMovedTime;
            this.asset = asset;
            this.movedFromLocation = fromLoc;
            this.movedToLocation = toLoc;
           
        }
        #endregion Constructors Tag Moved

        #region Properties Tag Moved

        /// <summary>
        /// Location from which asset is moved
        /// </summary>
        public KTAssetDetails MovedFrom
        {
            get { return this.movedFromLocation; }
        }

        /// <summary>
        /// Location to which asset is moved
        /// </summary>
        public KTAssetDetails MovedTo
        {
            get { return this.movedToLocation; }
        }

     
        /// <summary>
        /// Time stamp
        /// </summary>
        public DateTime TagMovedTime
        {
            get { return this.tagMovedTime; }
        }
        /// <summary>
        /// Tag ID
        /// </summary>
        public KTAssetDetails Asset
        {
            get
            {
                return this.asset;
            }
        }
     
        #endregion Properties Tag Moved
    }

    [Serializable]
    public class KTAssetInventoryAgentTagSeenEventArgs : KTAssetInventoryAgentEventArgs
    {
        #region Attributes

        private KTAssetDetails asset = null;
        private DateTime tagSeenTime;
        private KTAssetDetails locDetail = null;
        

        #endregion Attributes

        #region Constructors Tag Seen
        /// <summary>

        /// </summary>
        public KTAssetInventoryAgentTagSeenEventArgs(KTAssetDetails asset, DateTime tagSeenTime, KTAssetDetails sectionDetails,
                              RPGAssetStatus status, IKTComponent component, string eventId)
            : base(component, eventId, status)
        {
            this.asset = asset;
            this.tagSeenTime = tagSeenTime;
            this.locDetail = sectionDetails;
        }
        #endregion Constructed Tag Seen

        #region Properties Tag Seen

  
        /// <summary>
        /// Time stamp
        /// </summary>
        public DateTime TagSeenTime
        {
            get { return this.tagSeenTime; }
        }
        /// <summary>
        /// Tag ID
        /// </summary>
        public KTAssetDetails Asset
        {
            get
            {
                return this.asset;
            }
        }

        public KTAssetDetails SeenLocationDetail
        {
            get
            {
                return this.locDetail;
            }
        }
        #endregion Properties Tag Seen
    }

    #endregion [ KTJunctionAgent ]


    #endregion [ A G E N T -- EPC Agent / WaveTrendDB Agent / KTAgentEpcHistory / Section - Inventory / ActiveWave / JunctionAgent]

    //Location agent
    #region Location Agent

    /// <summary>
    /// On Location Agent tag seen 
    /// </summary>
    #region [ KTTagSeenAtLocationAgentTagArgs ]
    [Serializable]
    public class KTTagSeenAtLocationAgentTagArgs : KTComponentEventArgs
    {
        #region Attributes
        private string tagUrn = string.Empty;
        private string rfValue = string.Empty;
        private DateTime timeStamp;

        #endregion Attributes

        #region Constructors
        /// <summary>
        /// KTTagSeenAtLocationAgentTagArgs
        /// </summary>
        /// <param name="tagUrn"></param>
        /// <param nam="rfvalue"></param>
        /// <param name="timeStamp"></param>
        /// <param name="component"></param>
        /// <param name="eventId"></param>
        public KTTagSeenAtLocationAgentTagArgs(string tagUrn, string rfValue, DateTime timeStamp, IKTComponent component, string eventId)
            : base(component, eventId)
        {
            this.tagUrn = tagUrn;
            this.rfValue = rfValue;
            this.timeStamp = timeStamp;

        }
        #endregion Constructor

        #region Properties

        /// <summary>
        ///TagUrn seen in front of Reader/Filter.
        /// </summary>
        public string TagUrn
        {
            get { return this.tagUrn; }
        }

        /// <summary>
        ///Reader/Filter by which TagUrn seen.
        /// </summary>
        public string RFValue
        {
            get { return this.rfValue; }
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
    #endregion [ KTTagSeenAtLocationAgentTagArgs ]

    /// <summary>
    /// On Location Agent timeout 
    /// </summary>
    #region [ KTLocationAgentTimeOut ]
    [Serializable]
    public class KTLocationAgentTimeOut : KTComponentEventArgs
    {
        #region Attributes
        private int timeLeft = 0;
        private TimeOutType timeOutType;

        #endregion Attributes

        #region Constructors
        /// <summary>
        /// KTLocationAgentTimeOut
        /// </summary>
        /// <param name="timeLeft"></param>
        /// <param name="timeOutType"></param>
        /// <param name="timeStamp"></param>
        /// <param name="component"></param>
        /// <param name="eventId"></param>
        public KTLocationAgentTimeOut(int timeLeft, TimeOutType timeOutType, IKTComponent component, string eventId)
            : base(component, eventId)
        {
            this.timeLeft = timeLeft;
            this.timeOutType = timeOutType;

        }
        #endregion Constructor


        #region Properties

        /// <summary>
        ///It will give remaning time information in seconds.
        /// </summary>
        public int TimeLeft
        {
            get { return this.timeLeft; }
        }

        /// <summary>
        /// Intermidate will be fire 60 sec before Final time out, and will called every 15 sec in last minute of Final Timeout.
        /// Whereas Final TimeOut will be Called when Verify Station will stop reading more tag and update the current inventory based on tags read.
        /// </summary>
        public TimeOutType CurrentTimeOutType
        {
            get { return this.timeOutType; }
        }

        #endregion Properties
    }

    #endregion [ KTLocationAgentTimeOut ]

    /// <summary>
    /// On Location Agent when tags updated
    /// </summary>
    #region[ KTLocationAgentItemsUpdated ]
    [Serializable]
    public class KTLocationAgentItemsUpdated : KTComponentEventArgs
    {
        #region Attributes
        private int totalItemCount = 0;
        private int updatedGTINCount = 0;
        private string errMsg = string.Empty;
        private List<string> updatedItemsDetails = new List<string>();
        private string rfValue = string.Empty;
        private string locationName = "NOTDEFINED";

        private string itemSrNo = string.Empty;
        private DateTime lastSeenTime = DateTime.Now;
         
        #endregion Attributes

        #region Constructors
        /// <summary>
        /// KTLocationAgentItemsUpdated
        /// </summary>
        /// <param name="totalItemCount"></param>
        /// <param name="updatedGTINCount"></param>
        /// <param name="rfValue"></param>
        /// <param name="locationName"></param>
        /// <param name="updatedItemsDetails"></param>
        /// <param name="errMsg"></param>
        /// <param name="timeStamp"></param>
        /// <param name="component"></param>
        /// <param name="eventId"></param>
        public KTLocationAgentItemsUpdated(int totalItemCount, int updatedGTINCount, List<string> updatedItemsDetails, string rfValue, string locationName, string errMsg, DateTime timeStamp,
                                           IKTComponent component, string eventId)
            : base(component, eventId)
        {
            this.totalItemCount = totalItemCount;
            this.updatedGTINCount = updatedGTINCount;
            this.updatedItemsDetails = updatedItemsDetails;
            this.rfValue = rfValue;
            this.locationName = locationName;
            this.errMsg = errMsg;
        }

        public KTLocationAgentItemsUpdated(string itemSrNo,string locName,DateTime lastSeenTime,IKTComponent component, string eventId)
            : base(component, eventId)
        {
            this.itemSrNo = itemSrNo;
            this.locationName = locName;
            this.lastSeenTime = lastSeenTime;
        }

        #endregion Constructor

        #region Properties
       
        /// <summary>
        ///It will give lastSeenTime of Item.
        /// </summary>

        public DateTime LastSeenTime
        {
            get { return this.lastSeenTime; }
        }
        /// <summary>
        ///It will give ItemSRNO which Updated.
        /// </summary>
        public string ItemSrNO
        {
            get { return this.itemSrNo; }
        }

        /// <summary>
        ///It will give total items found in location agent cycle.
        /// </summary>
        public int TotalItemCount
        {
            get { return this.totalItemCount; }
        }

        /// <summary>
        ///It will give total inserted count of Items in Database.
        /// </summary>
        public int UpdatedGTINCount
        {
            get { return this.updatedGTINCount; }
        }

        /// <summary>
        /// UpdatedItemsDetails
        /// </summary>
        public List<string> UpdatedItemsDetails
        {
            get { return this.updatedItemsDetails; }
        }

        /// <summary>
        /// Reader/Filter by which tag seen
        /// </summary>
        public string RFValue
        {
            get { return this.rfValue; }
        }
        /// <summary>
        /// Location to which tag seen
        /// </summary>
        public string LocationName
        {
            get { return this.locationName; }
        }

        /// <summary>
        /// Error msg
        /// </summary>
        public string ErrorMsg
        {
            get { return this.errMsg; }
        }

        #endregion Properties
    }

    #endregion[ KTLocationAgentItemsUpdated ]


    /// <summary>
    /// Event for exceptions and alerts at Location agent
    /// </summary>
    #region [ KTLocationAgentExceptionsAndAlertsArgs ]
    [Serializable]
    public class KTLocationAgentExceptionsAndAlertsArgs : KTComponentEventArgs
    {
        #region Attributes
        private string tagUrn = "";
        private DateTime timeStamp;
        private LocationAgentAlertType locationAlert;
        private string rfValue = string.Empty;

        #endregion Attributes

        #region Constructors
        /// <summary>
        /// KTLocationAgentExceptionsAndAlertsArgs
        /// </summary>
        /// <param name="tagUrn"></param>
        /// <param name="timeStamp"></param>
        ///  <param name ="LocationAgentAlertType"></param>
        ///  <param name ="rfValue"></param>
        /// <param name="component"></param>
        /// <param name="eventId"></param>
        public KTLocationAgentExceptionsAndAlertsArgs(string tagUrn, LocationAgentAlertType locationAlert, string rfValue, DateTime timeStamp, IKTComponent component, string eventId)
            : base(component, eventId)
        {
            this.tagUrn = tagUrn;
            this.locationAlert = locationAlert;
            this.rfValue = rfValue;
            this.timeStamp = timeStamp;
        }
        #endregion Constructor

        #region Properties

        /// <summary>
        ///TagUrn seen in front of Reader.
        /// </summary>
        public string TagUrn
        {
            get { return this.tagUrn; }
        }

        /// <summary>
        ///Reader/Filter by which TagUrn seen.
        /// </summary>
        public string RFValue
        {
            get { return this.rfValue; }
        }

        /// <summary>
        /// Timestamp.
        /// </summary>
        public DateTime TimeStamp
        {
            get { return this.timeStamp; }
        }

        /// <summary>
        ///LocationAgentAlertType
        /// </summary>
        public LocationAgentAlertType AlertType
        {
            get { return this.locationAlert; }
        }

        #endregion Properties
    }
    #endregion [ KTLocationAgentExceptionsAndAlertsArgs ]


    #endregion Location Agent

    //Receiving Agent
    #region Receiving Agent
    /// <summary>
    /// On Receiving Agent tag seen 
    /// </summary>
    #region [ KTTagSeenAtReceivingAgentTagArgs ]
    [Serializable]
    public class KTTagSeenAtReceivingAgentTagArgs : KTComponentEventArgs
    {
        #region Attributes
        private string tagUrn = string.Empty;
        private string rfValue = string.Empty;
        private DateTime timeStamp;

        #endregion Attributes

        #region Constructors
        /// <summary>
        /// KTTagSeenAtReceivingAgentTagArgs
        /// </summary>
        /// <param name="tagUrn"></param>
        /// <param nam="rfvalue"></param>
        /// <param name="timeStamp"></param>
        /// <param name="component"></param>
        /// <param name="eventId"></param>
        public KTTagSeenAtReceivingAgentTagArgs(string tagUrn, string rfValue, DateTime timeStamp, IKTComponent component, string eventId)
            : base(component, eventId)
        {
            this.tagUrn = tagUrn;
            this.rfValue = rfValue;
            this.timeStamp = timeStamp;

        }
        #endregion Constructor

        #region Properties

        /// <summary>
        ///TagUrn seen in front of Reader/Filter.
        /// </summary>
        public string TagUrn
        {
            get { return this.tagUrn; }
        }

        /// <summary>
        ///Reader/Filter by which TagUrn seen.
        /// </summary>
        public string RFValue
        {
            get { return this.rfValue; }
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
    #endregion [ KTTagSeenAtReceivingAgentTagArgs ]

    /// <summary>
    /// On Receving Agent Cycletimeout 
    /// </summary>
    #region [ KTReceivingAgentCycleTimeOut ]
    [Serializable]
    public class KTReceivingAgentTimeOut : KTComponentEventArgs
    {
        #region Attributes
        private int timeLeft = 0;
        private TimeOutType timeOutType;

        #endregion Attributes

        #region Constructors
        /// <summary>
        /// KTLocationAgentTimeOut
        /// </summary>
        /// <param name="timeLeft"></param>
        /// <param name="timeOutType"></param>
        /// <param name="timeStamp"></param>
        /// <param name="component"></param>
        /// <param name="eventId"></param>
        public KTReceivingAgentTimeOut(int timeLeft, TimeOutType timeOutType, IKTComponent component, string eventId)
            : base(component, eventId)
        {
            this.timeLeft = timeLeft;
            this.timeOutType = timeOutType;

        }
        #endregion Constructor


        #region Properties

        /// <summary>
        ///It will give remaning time information in seconds.
        /// </summary>
        public int TimeLeft
        {
            get { return this.timeLeft; }
        }

        /// <summary>
        /// Intermidate will be fire 60 sec before Final time out, and will called every 15 sec in last minute of Final Timeout.
        /// Whereas Final TimeOut will be Called when Verify Station will stop reading more tag and update the current inventory based on tags read.
        /// </summary>
        public TimeOutType CurrentTimeOutType
        {
            get { return this.timeOutType; }
        }

        #endregion Properties
    }

    #endregion [ KTReceivingAgentCycleTimeOut ]
    
    /// <summary>
    /// On Receiving Agent when tags updated
    /// </summary>
    #region[ KTReceivingAgentItemsUpdated ]
    [Serializable]
    public class KTReceivingAgentItemsUpdated : KTComponentEventArgs
    {
        #region Attributes
        private int totalItemCount = 0;
        private int updatedGTINCount = 0;
        private string errMsg = string.Empty;
        private List<string> updatedItemsDetails = new List<string>();
        private string rfValue = string.Empty;
        private string locationName = "NOTDEFINED";

        private string itemSrNo = string.Empty;
        private DateTime lastSeenTime = DateTime.Now;

        #endregion Attributes

        #region Constructors
        /// <summary>
        /// KTReceivingAgentItemsUpdated
        /// </summary>
        /// <param name="totalItemCount"></param>
        /// <param name="updatedGTINCount"></param>
        /// <param name="rfValue"></param>
        /// <param name="locationName"></param>
        /// <param name="updatedItemsDetails"></param>
        /// <param name="errMsg"></param>
        /// <param name="timeStamp"></param>
        /// <param name="component"></param>
        /// <param name="eventId"></param>
        public KTReceivingAgentItemsUpdated(int totalItemCount, int updatedGTINCount, List<string> updatedItemsDetails, string rfValue, string locationName, string errMsg, DateTime timeStamp,
                                           IKTComponent component, string eventId)
            : base(component, eventId)
        {
            this.totalItemCount = totalItemCount;
            this.updatedGTINCount = updatedGTINCount;
            this.updatedItemsDetails = updatedItemsDetails;
            this.rfValue = rfValue;
            this.locationName = locationName;
            this.errMsg = errMsg;
        }

        public KTReceivingAgentItemsUpdated(string itemSrNo, string locName, DateTime lastSeenTime, IKTComponent component, string eventId)
            : base(component, eventId)
        {
            this.itemSrNo = itemSrNo;
            this.locationName = locName;
            this.lastSeenTime = lastSeenTime;
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        ///It will give lastSeenTime of Item.
        /// </summary>

        public DateTime LastSeenTime
        {
            get { return this.lastSeenTime; }
        }
        /// <summary>
        ///It will give ItemSRNO which Updated.
        /// </summary>
        public string ItemSrNO
        {
            get { return this.itemSrNo; }
        }

        /// <summary>
        ///It will give total items found in location agent cycle.
        /// </summary>
        public int TotalItemCount
        {
            get { return this.totalItemCount; }
        }

        /// <summary>
        ///It will give total inserted count of Items in Database.
        /// </summary>
        public int UpdatedGTINCount
        {
            get { return this.updatedGTINCount; }
        }

        /// <summary>
        /// UpdatedItemsDetails
        /// </summary>
        public List<string> UpdatedItemsDetails
        {
            get { return this.updatedItemsDetails; }
        }

        /// <summary>
        /// Reader/Filter by which tag seen
        /// </summary>
        public string RFValue
        {
            get { return this.rfValue; }
        }
        /// <summary>
        /// Location to which tag seen
        /// </summary>
        public string LocationName
        {
            get { return this.locationName; }
        }

        /// <summary>
        /// Error msg
        /// </summary>
        public string ErrorMsg
        {
            get { return this.errMsg; }
        }

        #endregion Properties
    }

    #endregion[ KTReceivingAgentItemsUpdated ]

    /// <summary>
    /// Event for exceptions and alerts at Receving agent
    /// </summary>
    #region [ KTRecevingAgentExceptionsAndAlertsArgs ]
    [Serializable]
    public class KTReceivingAgentExceptionsAndAlertsArgs : KTComponentEventArgs
    {
        #region Attributes
        private string tagUrn = "";
        private DateTime timeStamp;
        private ReceiveingAlertType receivingAlert;
        private string rfValue = string.Empty;

        #endregion Attributes

        #region Constructors
        /// <summary>
        /// KTReceivingAgentExceptionsAndAlertsArgs
        /// </summary>
        /// <param name="tagUrn"></param>
        /// <param name="timeStamp"></param>
        ///  <param name ="ReceiveingAlertType"></param>
        ///  <param name ="rfValue"></param>
        /// <param name="component"></param>
        /// <param name="eventId"></param>
        public KTReceivingAgentExceptionsAndAlertsArgs(string tagUrn, ReceiveingAlertType receivingAlert, string rfValue, DateTime timeStamp, IKTComponent component, string eventId)
            : base(component, eventId)
        {
            this.tagUrn = tagUrn;
            this.receivingAlert = receivingAlert;
            this.rfValue = rfValue;
            this.timeStamp = timeStamp;
        }
        #endregion Constructor

        #region Properties

        /// <summary>
        ///TagUrn seen in front of Reader.
        /// </summary>
        public string TagUrn
        {
            get { return this.tagUrn; }
        }

        /// <summary>
        ///Reader/Filter by which TagUrn seen.
        /// </summary>
        public string RFValue
        {
            get { return this.rfValue; }
        }

        /// <summary>
        /// Timestamp.
        /// </summary>
        public DateTime TimeStamp
        {
            get { return this.timeStamp; }
        }

        /// <summary>
        ///ReceiveingAlertType
        /// </summary>
        public ReceiveingAlertType AlertType
        {
            get { return this.receivingAlert; }
        }

        #endregion Properties
    }
    #endregion [ KTReceivingAgentExceptionsAndAlertsArgs ]


    #endregion Receiving Agent

    //DisAssociation Agent
    #region DisAssociation Agent
    /// <summary>
    /// On DisAssociation Agent tag seen 
    /// </summary>
    #region [ KTTagSeenAtDisAssociationAgentTagArgs ]
    [Serializable]
    public class KTTagSeenAtDisAssociationAgentTagArgs : KTComponentEventArgs
    {
        #region Attributes
        private string tagUrn = string.Empty;
        private string rfValue = string.Empty;
        private DateTime timeStamp;

        #endregion Attributes

        #region Constructors
        /// <summary>
        /// KTTagSeenAtDisAssociationAgentTagArgs
        /// </summary>
        /// <param name="tagUrn"></param>
        /// <param nam="rfvalue"></param>
        /// <param name="timeStamp"></param>
        /// <param name="component"></param>
        /// <param name="eventId"></param>
        public KTTagSeenAtDisAssociationAgentTagArgs(string tagUrn, string rfValue, DateTime timeStamp, IKTComponent component, string eventId)
            : base(component, eventId)
        {
            this.tagUrn = tagUrn;
            this.rfValue = rfValue;
            this.timeStamp = timeStamp;

        }
        #endregion Constructor

        #region Properties

        /// <summary>
        ///TagUrn seen in front of Reader/Filter.
        /// </summary>
        public string TagUrn
        {
            get { return this.tagUrn; }
        }

        /// <summary>
        ///Reader/Filter by which TagUrn seen.
        /// </summary>
        public string RFValue
        {
            get { return this.rfValue; }
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
    #endregion [ KTTagSeenAtDisAssociationAgentTagArgs ]

    /// <summary>
    /// On DisAssociation Agent Cycletimeout 
    /// </summary>
    #region [ KTDisAssociationAgentCycleTimeOut ]
    [Serializable]
    public class KTDisAssociationAgentTimeOut : KTComponentEventArgs
    {
        #region Attributes
        private int timeLeft = 0;
        private TimeOutType timeOutType;

        #endregion Attributes

        #region Constructors
        /// <summary>
        /// KTDisAssociationAgentTimeOut
        /// </summary>
        /// <param name="timeLeft"></param>
        /// <param name="timeOutType"></param>
        /// <param name="timeStamp"></param>
        /// <param name="component"></param>
        /// <param name="eventId"></param>
        public KTDisAssociationAgentTimeOut(int timeLeft, TimeOutType timeOutType, IKTComponent component, string eventId)
            : base(component, eventId)
        {
            this.timeLeft = timeLeft;
            this.timeOutType = timeOutType;

        }
        #endregion Constructor


        #region Properties

        /// <summary>
        ///It will give remaning time information in seconds.
        /// </summary>
        public int TimeLeft
        {
            get { return this.timeLeft; }
        }

        /// <summary>
        /// Intermidate will be fire 60 sec before Final time out, and will called every 15 sec in last minute of Final Timeout.
        /// Whereas Final TimeOut will be Called when Verify Station will stop reading more tag and update the current inventory based on tags read.
        /// </summary>
        public TimeOutType CurrentTimeOutType
        {
            get { return this.timeOutType; }
        }

        #endregion Properties
    }

    #endregion [ KTDisAssociationAgentCycleTimeOut ]

    /// <summary>
    /// On DisAssociation Agent when tags updated
    /// </summary>
    #region[ KTDisAssociationAgentItemsUpdated ]
    [Serializable]
    public class KTDisAssociationAgentItemsUpdated : KTComponentEventArgs
    {
        #region Attributes
        private int totalItemCount = 0;
        private int updatedGTINCount = 0;
        private string errMsg = string.Empty;
        private List<string> updatedItemsDetails = new List<string>();
        private string rfValue = string.Empty;
        private string locationName = "NOTDEFINED";

        private string itemSrNo = string.Empty;
        private DateTime lastSeenTime = DateTime.Now;

        #endregion Attributes

        #region Constructors
        /// <summary>
        /// KTDisAssociationAgentItemsUpdated
        /// </summary>
        /// <param name="totalItemCount"></param>
        /// <param name="updatedGTINCount"></param>
        /// <param name="rfValue"></param>
        /// <param name="locationName"></param>
        /// <param name="updatedItemsDetails"></param>
        /// <param name="errMsg"></param>
        /// <param name="timeStamp"></param>
        /// <param name="component"></param>
        /// <param name="eventId"></param>
        public KTDisAssociationAgentItemsUpdated(int totalItemCount, int updatedGTINCount, List<string> updatedItemsDetails, string rfValue, string locationName, string errMsg, DateTime timeStamp,
                                           IKTComponent component, string eventId)
            : base(component, eventId)
        {
            this.totalItemCount = totalItemCount;
            this.updatedGTINCount = updatedGTINCount;
            this.updatedItemsDetails = updatedItemsDetails;
            this.rfValue = rfValue;
            this.locationName = locationName;
            this.errMsg = errMsg;
        }

        public KTDisAssociationAgentItemsUpdated(string itemSrNo, string locName, DateTime lastSeenTime, IKTComponent component, string eventId)
            : base(component, eventId)
        {
            this.itemSrNo = itemSrNo;
            this.locationName = locName;
            this.lastSeenTime = lastSeenTime;
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        ///It will give lastSeenTime of Item.
        /// </summary>

        public DateTime LastSeenTime
        {
            get { return this.lastSeenTime; }
        }
        /// <summary>
        ///It will give ItemSRNO which Updated.
        /// </summary>
        public string ItemSrNO
        {
            get { return this.itemSrNo; }
        }

        /// <summary>
        ///It will give total items found in location agent cycle.
        /// </summary>
        public int TotalItemCount
        {
            get { return this.totalItemCount; }
        }

        /// <summary>
        ///It will give total inserted count of Items in Database.
        /// </summary>
        public int UpdatedGTINCount
        {
            get { return this.updatedGTINCount; }
        }

        /// <summary>
        /// UpdatedItemsDetails
        /// </summary>
        public List<string> UpdatedItemsDetails
        {
            get { return this.updatedItemsDetails; }
        }

        /// <summary>
        /// Reader/Filter by which tag seen
        /// </summary>
        public string RFValue
        {
            get { return this.rfValue; }
        }
        /// <summary>
        /// Location to which tag seen
        /// </summary>
        public string LocationName
        {
            get { return this.locationName; }
        }

        /// <summary>
        /// Error msg
        /// </summary>
        public string ErrorMsg
        {
            get { return this.errMsg; }
        }

        #endregion Properties
    }

    #endregion[ KTDisAssociationAgentItemsUpdated ]

    #endregion DisAssociation Agent


    #region  ViolationPurge Monitor
    /// <summary>
    /// On Purge component will purge the record.
    /// </summary>
    [Serializable]
    public class KTViolationPurgeMonitorArgs : KTComponentEventArgs
    {
        #region Attributes
        private long purgeCount = 0;
        private DateTime timeStamp;
        private string errMsg = string.Empty;

        #endregion Attributes

        #region Constructors
        /// <summary>
        /// KTViolationPurgeMonitorArgs
        /// </summary>
        /// <param name="itemSrNos"></param>
        /// <param name="timeStamp"></param>
        /// <param name="component"></param>
        /// <param name="errMsg"></param>
        /// <param name="eventId"></param>
        public KTViolationPurgeMonitorArgs(long PurgeCount, DateTime timeStamp, string errMsg, IKTComponent component, string eventId)
            : base(component, eventId)
        {
            this.purgeCount = PurgeCount;
            this.timeStamp = timeStamp;
            this.errMsg = errMsg;
        }
        public KTViolationPurgeMonitorArgs(long PurgeCount, DateTime timeStamp, IKTComponent component, string eventId)
            : base(component, eventId)
        {
            this.purgeCount = PurgeCount;
            this.timeStamp = timeStamp; 
        }
        #endregion Constructor

        #region Properties

        /// <summary>
        ///Violated Item SrNo.(BinTape ID) purge
        /// </summary>
        public long PurgeCount
        {
            get { return this.purgeCount; }
        }

        /// <summary>
        /// Timestamp.
        /// </summary>
        public DateTime TimeStamp
        {
            get { return this.timeStamp; }
        }

        /// <summary>
        /// ErrMsg.
        /// </summary>
        public string ErrMsg
        {
            get { return this.errMsg; }
        }

        #endregion Properties
    }
    #endregion ViolationPurge Monitor

    #region  DBPurge Monitor
    /// <summary>
    /// On Purge component will purge the record.
    /// </summary>
    [Serializable]
    public class KTDBPurgeMonitorArgs : KTComponentEventArgs
    {
        #region Attributes
        private string dbPurgeditems = null;
        private DateTime timeStamp;
        private string errMsg = string.Empty;

        #endregion Attributes

        #region Constructors
        /// <summary>
        /// KTDBPurgeMonitorArgs
        /// </summary>
        /// <param name="itemSrNos"></param>
        /// <param name="timeStamp"></param>
        /// <param name="component"></param>
        /// <param name="errMsg"></param>
        /// <param name="eventId"></param>
        public KTDBPurgeMonitorArgs(string DBPurgeditems, DateTime timeStamp, string errMsg, IKTComponent component, string eventId)
            : base(component, eventId)
        {
            this.dbPurgeditems = DBPurgeditems;
            this.timeStamp = timeStamp;
            this.errMsg = errMsg;
        }
        public KTDBPurgeMonitorArgs(string DBPurgeditems, DateTime timeStamp, IKTComponent component, string eventId)
            : base(component, eventId)
        {
            this.dbPurgeditems = DBPurgeditems;
            this.timeStamp = timeStamp;
        }
        #endregion Constructor

        #region Properties

        /// <summary>
        ///Purged Items Count
        /// </summary>
        public string DBPurgeditems
        {
            get { return this.dbPurgeditems; }
        }

        /// <summary>
        /// Timestamp.
        /// </summary>
        public DateTime TimeStamp
        {
            get { return this.timeStamp; }
        }

        /// <summary>
        /// ErrMsg.
        /// </summary>
        public string ErrMsg
        {
            get { return this.errMsg; }
        }

        #endregion Properties
    }
    #endregion DBPurge Monitor


    /// <summary>
    /// Provides Tag data written or read from tag  
    /// </summary>
    [Serializable]
    public class TagDataArgs : KTComponentEventArgs
    {
        #region Attributes

        private string rawTagId = string.Empty;
        private int startAddress = 0;
        private byte[] data = null;
        private Dictionary<string, object> dataDictionary = new Dictionary<string, object>();
        private int retry = 0;
        private ReadWriteStatus status = ReadWriteStatus.ReadSuccessful;
        private bool isIntermediate = true;
        #endregion Attributes

        #region Constructor

        public TagDataArgs(string tagId, ReadWriteStatus status, int startAddress, byte[] data, Dictionary<string, object> dataDictionary, bool isIntermediate, int retry, IKTComponent component, string eventId)
            : base(component, eventId)
        {
            this.rawTagId = tagId;
            this.status = status;
            this.startAddress = startAddress;
            this.data = data;
            this.dataDictionary = dataDictionary;
            this.isIntermediate = isIntermediate;
            this.retry = retry;
        }
        #endregion

        #region Properties

        public string TagId
        {
            get { return this.rawTagId; }
        }

        public int StartAddress
        {
            get { return startAddress; }
        }

        public byte[] Data
        {
            get { return data; }
        }

        public Dictionary<string, object> DataDictionary
        {
            get { return dataDictionary; }
        }

        public bool IsIntermediate
        {
            get { return isIntermediate; }
        }

        public int CurrentRetryAttempt
        {
            get { return retry; }
        }

        public ReadWriteStatus Status
        {
            get { return status; }
        }
        #endregion Properties
    }

    [Serializable]
    public class ReaderGroupEventArgs : EventArgs
    {
        #region [Attributes]

        int readerGroupID = 0;
        string readerGroupName = string.Empty;

        #endregion [Attributes]

        #region [Constructor]

        public ReaderGroupEventArgs(int readerGroupID, string readerGroupName)
        {
            this.readerGroupID = readerGroupID;
            this.readerGroupName = readerGroupName;
        }

        #endregion [Constructor]

        #region [Properties]

        public int ReaderGroupID
        {
            get { return readerGroupID; }
        }

        public string ReaderGroupName
        {
            get { return readerGroupName; }
        }

        #endregion [Properties]
    }


    [Serializable]
    public class TripPlanUpdateEventArgs : EventArgs
    {
        #region [Attributes]

        int tripPlanID = 0;
        string tripPlanName = string.Empty;

        #endregion [Attributes]

        #region [Constructor]

        public TripPlanUpdateEventArgs(int tripPlanID, string tripPlanName)
        {
            this.tripPlanID = tripPlanID;
            this.tripPlanName = tripPlanName;
        }

        #endregion [Constructor]

        #region [Properties]

        public int TripPlanID
        {
            get { return tripPlanID; }
        }

        public string TripPlanName
        {
            get { return tripPlanName; }
        }

        #endregion [Properties]
    }

    [Serializable]
    public class AssetTripEventArgs : EventArgs
    {
        #region [Attributes]

        long assetTripId = 0;
        string operationType = string.Empty;

        #endregion [Attributes]

        #region [Constructor]

        public AssetTripEventArgs(long assetTripId, string operationType)
        {
            this.assetTripId = assetTripId;
            this.operationType = operationType;
        }

        #endregion [Constructor]

        #region [Properties]

        public long AssetTripId
        {
            get { return assetTripId; }
        }

        public string OperationType
        {
            get { return operationType; }
        }

        #endregion [Properties]
    }




}



