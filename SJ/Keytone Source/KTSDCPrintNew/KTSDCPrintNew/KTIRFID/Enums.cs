

namespace KTone.Core.KTIRFID
{
    /// <summary>
    /// <exclude/>
    /// </summary>
    public enum KTElementState
    {
        Unknown,
        Created,
        Initialized,
        Published,
        Discovered,
        Connected,
        Started,
        Stopped,
        Disconnected,
        Saved,
        Closed
    }

    /// <summary>
    /// <exclude/>
    /// </summary>
    public enum KTComponentState
    {
        /// <summary>
        /// Component active
        /// </summary>
        Active,
        /// <summary>
        /// Component inactive
        /// </summary>
        Inactive,

        /// <summary>
        /// Component in error state
        /// </summary>
        Error,

        /// <summary>
        /// Component in disabled state
        /// </summary>
        Disabled
    }
    /// <summary>
    /// 
    /// </summary>
    public enum TriggerType
    {
        continuous,
        timer,
        ioEdge,
        ioValue
    }
    /// <summary>
    /// <exclude/>
    /// </summary>
    public enum KTComponentCategory
    {
        /// <summary>
        /// Unknown
        /// </summary>
        Unknown,
        /// <summary>
        /// Reader
        /// </summary>
        Reader,
        /// <summary>
        /// Filter
        /// </summary>
        Filter,
        /// <summary>
        /// Agent
        /// </summary>
        Agent,
        /// <summary>
        /// Messaging
        /// </summary>
        Messaging,
        /// <summary>
        /// System Monitor
        /// </summary>
        SystemMonitor,

        /// <summary>
        /// Mail
        /// </summary>
        Mail,
        /// <summary>
        /// Trigger
        /// </summary>
        Trigger,

        /// <summary>
        /// Printer
        /// </summary>
        Printer,

        /// <summary>
        /// Import Data
        /// </summary>
        ImportData,
        /// <summary>
        /// AssetCache
        /// </summary>
        AssetCache,

        /// <summary>
        /// Stores data related to products, companies and SKUs
        /// </summary>
        SDCCache,
        /// <summary>
        /// Monitors the components
        /// </summary>
        ComponentMonitor,
        /// <summary>
        /// Generates Alerts 
        /// </summary>
        Alert,
        /// <summary>
        /// Generates Email Alerts 
        /// </summary>
        EmailAlert,
        /// <summary>
        /// Generates  CheckInCheckOut Alerts 
        /// </summary>
        AutoCheckInCheckOut,
        /// <summary>
        /// Monitors AutoCheckInCheckOut components 
        /// </summary>
        AutoCICOMonitor,
        /// <summary>
        /// Purges configured data from tables
        /// </summary>
        PurgeMonitor,
        /// <summary>
        /// Hosts custom components
        /// </summary>
        CustomComponent,
        /// <summary>
        /// Image Processor
        /// </summary>
        ImageProcessor,
        /// <summary>
        /// Monitors templates used to interpret the tag data
        /// </summary>
        TemplateMonitor,

        /// <summary>
        /// Generates SMS
        /// </summary>
        SMSGenerator,

        /// <summary>
        /// Trip Management
        /// </summary>
        KTTripManagement,

        /// <summary>
        ///  VoilationPurge
        /// </summary>
        ViolationPurge
    }

    /// <summary>
    /// <exclude/>
    /// Indicates the System State.Used in MonitorEventArgs.
    /// </summary>
    public enum SystemState
    {
        /// <summary>
        /// System idle
        /// </summary>
        IDLE,
        /// <summary>
        /// System active
        /// </summary>
        ACTIVE
    }


    /// <summary>
    ///   <exclude/>
    /// Defines the communication mode of the device.
    /// </summary>
    public enum CommunicationMode
    {
        /// <summary>
        /// Serial communication
        /// </summary>
        Serial,
        /// <summary>
        /// Ethernet communication
        /// </summary>
        Ethernet,

        /// <summary>
        /// Custom
        /// </summary>
        Custom,

        /// <summary>
        /// Http
        /// </summary>
        Http
    }

    /// <summary>
    /// <exclude/>
    /// Defines the type of the packet which is sent as a part of RFIDReaderEventArgs
    /// </summary>
    public enum Action
    {
        /// <summary>
        /// Command successfully sent. 
        /// </summary>
        CommandSent,

        /// <summary>
        /// Response received 
        /// </summary>
        ResponseReceived,

        /// <summary>
        /// ResponseNext received (continuous mode)
        /// </summary>
        ResponseReceivedNext,


        /// <summary>
        /// Error occurred while executing a reader command.
        /// </summary>
        Error,
    }

    /// <summary>
    /// <exclude/>
    /// Enumerations for mesaage delevery modes
    /// </summary>
    public enum DeliveryMode
    {
        /// <summary>
        /// Reliable Message Sending Mode
        /// This is the basic mode you can rely on for sending any message.
        /// </summary>
        Reliable,

        /// <summary>
        /// Certified Message Sending Mode
        /// </summary>
        Certified
    }

    /// <summary>
    /// 
    /// </summary>
    public enum AgentCategory
    {
        /// <summary>
        /// 
        /// </summary>
        OTHER,

        /// <summary>
        /// 
        /// </summary>
        DBAGENT,

        /// <summary>
        /// 
        /// </summary>
        NETWORKAGENT,

        /// <summary>
        /// 
        /// </summary>
        FILEAGENT
    }


    /// <summary>
    /// 
    /// </summary>
    public enum ImportCategory
    {
        /// <summary>
        /// 
        /// </summary>
        ASSETTRACE,

       
    }


    /// <summary>
    /// Defines the communication status of the underlying drivers.
    /// </summary>
    public enum CommunicationStatus
    {
        /// <summary>
        /// Communication channel opened.
        /// </summary>
        Started,
        /// <summary>
        /// Communication channel closed.
        /// </summary>
        Closed
    }



    /// <summary>
    /// Defines Error code for the errors given by the Wavetrend reader firmware.
    /// </summary>
    public enum WTReaderErrors
    {
        /// <summary>
        /// No errors encountered							
        /// </summary>
        NO_ERRORS = 0,
        /// <summary>
        /// Unknown reader command received			
        /// </summary>
        UNKNOWN_READER_COMMAND = 1,

        /// <summary>
        /// Tag Table underflow error				
        /// </summary>
        TAGTABLE_UNDERFLOW_ERROR = 2,

        /// <summary>
        /// Command Packet checksum error			
        /// </summary>
        CHECKSUM_ERROR = 3,

        /// <summary>
        /// RF Module - Unknown command response	 
        /// </summary>
        UNKNOWN_COMMAND_RESPONSE = 4,

        /// <summary>
        /// RF Module - Unknown general response	
        /// </summary>
        UNKNOWN_GENERAL_RESPONSE = 5,

        /// <summary>
        /// RF Module - Re-sync failure				
        /// </summary>
        RESYNC_FAILURE = 6,

        /// <summary>
        /// RF Module - Command response failure			
        /// </summary>
        COMMAND_RESPONSE_FAILURE = 7,

        /// <summary>
        /// RF Module - Receive response failure			
        /// </summary>
        RECEIVE_RESPONSE_FAILURE = 8,

        /// <summary>
        ///No response packet received from polled reader			
        /// </summary>
        NO_RESPONSE = 9,

    }

    /// <summary>
    /// Message level
    /// </summary>
    public enum MessageLevel
    {
        /// <summary>
        /// Fatal message
        /// </summary>
        Fatal,

        /// <summary>
        /// Error message
        /// </summary>
        Error,

        /// <summary>
        /// Info message
        /// </summary>
        Info
    }
    /// <summary>
    /// RFIDReaderMode
    /// </summary>
    public enum RFIDReaderMode
    {
        /// <summary>
        /// OnDemand
        /// </summary>
        OnDemand,

        /// <summary>
        /// Auto
        /// </summary>
        Auto
    }

    /// <summary>
    /// 
    /// </summary>
    public enum TagProtocol
    {
        /// <summary>
        /// EPC 0 tags
        /// </summary>
        EPC0,
        /// <summary>
        /// EPC 1 GEN 1 tags
        /// </summary>
        EPC1,
        /// <summary>
        /// EPC 1 GEN 2 tags
        /// </summary>
        GEN2,
        /// <summary>
        /// ISO tags 
        /// </summary>
        ISO18000_6B
    }

    /// <summary>
    /// Thing Magic GPIO pins
    /// </summary>
    public enum TMGCGPIOPins
    {
        /// <summary>
        /// Output Pin (mask 0x04)
        /// </summary>
        GPIO_0 = 6,
        /// <summary>
        /// Output Pin (mask 0x08)
        /// </summary>
        GPIO_1 = 1,
        /// <summary>
        /// Output Pin (mask 0x10)
        /// </summary>
        GPIO_2 = 9,
        /// <summary>
        /// Input Pin  (mask 0x02)
        /// </summary>
        GPIO_3 = 4,
        /// <summary>
        /// Input Pin  (mask 0x20)
        /// </summary>
        GPIO_4 = 7
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ComponentActions
    {
        /// <summary>
        /// Component added in Service
        /// </summary>
        Added,
        /// <summary>
        /// Component Removed from Service
        /// </summary>
        Removed,
        /// <summary>
        /// Component Marked as Deleted
        /// </summary>
        MarkedAsDeleted
    }

    /// <summary>
    /// Event type
    /// </summary>
    public enum AWEventType
    {
        NOT_SPECIFIED,
        TAGS_ADDED_EVENT,
        TAGS_REMOVED_EVENT,
        TAGS_CURRENT_EVENT,
        TAGS_MOVED_EVENT,
        TAGS_SENSED_EVENT,
        TAGS_TAMPERED_EVENT,
        TAG_READ_EVENT,
        LOW_BATTERY_EVENT
    }

    ///<summary>
    /// Enum for event type
    /// </summary>
    public enum JunctionRadialFilterEventType
    {
        TAG_SEEN,
        TAG_ADDED,
        TAG_REMOVED
    }

    /// <summary>
    /// Status of ActiveWave OnDemandInventory
    /// </summary>
    public enum AWInventoryStatus
    {
        Started,
        Complete,
        NotStarted,
        Error
    }

    public enum KTServerStatus
    {
        Starting,
        Started,
        Stopping,
        Stopped,
        Unknown,
        Error
    }


    public enum FieldName
    {
        SerialNumber
        //CompanyName,
        //BatchNumber
    }


    public enum VerificationAlerts
    {
        None = 0,
        AlreadyVerifiedItem,
        NotReceivedItem,
        ShippedItem,
        UnrecognizedTag
    }

    public enum TimeOutType
    {
        INTERMIDIATE,
        FINAL
    }

    /// <summary>
    /// Verify Station Status: station can be in one of 3 state, IDLE, READING, OR UPDATING
    /// </summary>

    public enum StationStatus
    {
        /// <summary>
        /// STATION IS IDLE.(INITIAL)
        /// </summary>
        IDLE,
        /// <summary>
        /// STATION IS IN READING RF TAG.(ON START CLICK)
        /// </summary>
        READING,
        /// <summary>
        /// STATION IS IN LOADED RF TAG.(ON STOP CLICK)
        /// </summary>
        LOADING,
        /// <summary>
        /// AFTER COMPLETELY READING THE TAGS, IT UPDATING THE INFO IN DATABASE.(ON SAVE CLICK)
        /// </summary>
        UPDATING,
        /// <summary>
        /// AFTER COMPLETELY READING THE TAGS AND SAVING THE INFO IN DATABASE.(ON SAVE CLICK)
        /// </summary>
        UPDATED
    }

    public enum AgentMode
    {
        Manual,
        Auto
    }

    public enum ReceiveingAlertType
    {
        AlreadyReceivedItem,
        UnrecognizedTag,
        VerifiedOrShippedItem,

    }

    public enum DockDoorAlertType
    {
        AlreadyShippedItem,
        NotVerifiedItem,
        UnrecognizedTag,
        AssociatedSSCCNotFound,
        NotShippedItems
    }

    public enum LocationAgentAlertType
    {
        NotShippedItems,
        UnrecognizedTag
    }

    public enum FileSaveMode
    {
        Daily,
        Weekly,
        Monthly,
        Quarterly,
        Yearly
    }

    public enum ReadWriteStatus
    {
        ReadSuccessful,
        ReadFailed,
        WriteSuccessful,
        WriteFailed,
    }

    public enum ImageReadStatus
    {
        None,
        Partial,
        Complete
    }

}
