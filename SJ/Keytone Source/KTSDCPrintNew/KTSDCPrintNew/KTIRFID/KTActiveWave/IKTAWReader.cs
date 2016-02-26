using System;
using System.Collections.Generic;
using System.Text;

namespace KTone.Core.KTIRFID
{
    public enum AWAutoMode
    {
        /// <summary>
        /// Reader will periodically call CallTags to get the tag inventory
        /// </summary>
        OnlyReader,
        /// <summary>
        /// One/more Field Generators will be connected to the reader.The Field generators will peridically 
        /// send the tag invetory to the reader
        /// </summary>
        OnlyFGen,
        /// <summary>
        /// Reader will peridically give command to FGen to get the tag inventory  
        /// </summary>
        ReaderAndFGen,
        /// <summary>
        /// Field Generators will send tag packets when Motion Sensors detect any movement.
        /// </summary>
        FGenAndMotionSensor,
        /// <summary>
        /// Tags will be in auto send mode and will send the tag packets to the reader periodically.
        /// </summary>
        TagBeacon
    }
    public interface IKTAWReader
    {
        #region Custom commands
        /// <summary>
        ///  Gets Tag Info depending on the configuration.
        /// </summary>
        IRFIDTag[] CallTags();
        /// <summary>
        ///  Gets Tag Info depending on the configuration.
        /// </summary>
        /// <param name="anyTagType">status of the TagType selection</param>
        /// <param name="anyTagId">status of the anyTagId selection</param>
        /// <param name="tagType">Type of the tag to Call</param>
        /// <param name="tagId">Tag ID to call</param>
        /// <param name="isLEDOn">Status of the LED when call</param>
        /// <param name="isSpeakerOn">Status of the Speaker.</param>
        /// <param name="isLongDelay"></param>
        /// <param name="fGenId"> Field Generator Id</param>
        /// <returns>ActiveWaveTag[]</returns>
        IRFIDTag[] CallTags(bool anyTagType, bool anyTagId, byte tagType, uint tagId, bool isLEDOn, bool isSpeakerOn, bool isLongDelay, ushort fGenId, bool isAsync);

        /// <summary>
        ///  Gets Tag Info depending on the configuration.
        /// </summary>
        /// <param name="anyTagType">status of the TagType selection</param>
        /// <param name="tagType">Type of the tag to Call</param>
        /// <param name="tagIds">Tag IDs to call</param>
        /// <param name="isLEDOn">Status of the LED when call</param>
        /// <param name="isSpeakerOn">Status of the Speaker.</param>
        /// <param name="isLongDelay"></param>
        /// <param name="fGenId"> Field Generator Id</param>
        /// <returns>ActiveWaveTag[]</returns>
        IRFIDTag[] CallTags(bool anyTagType, byte tagType, uint[] tagIds, bool isLEDOn, bool isSpeakerOn, bool isLongDelay, ushort fGenId);
        /// <summary>
        ///  Gets Tag Info depending on the configuration.
        /// </summary>
        /// <param name="startTagId">Tag index from where to start reading</param>
        /// <param name="rangeIndex">no of Tags to read from startTagId</param>
        IRFIDTag[] CallTags(uint startTagId, byte rangeIndex);

        /// <summary>
        ///  Gets Tag Info depending on the configuration.
        /// </summary>
        /// <param name="anyTagType">status of the TagType selection</param>
        /// <param name="anyTagId">status of the anyTagId selection</param>
        /// <param name="tagType">Type of the tag to Call</param>
        /// <param name="tagId">Tag ID to call</param>
        /// <param name="isLongDelay"></param>
        /// <returns>ActiveWaveTag[]</returns>
        IRFIDTag[] QueryTags(bool anyTagType, bool anyTagId, byte tagType, uint tagId, bool isLongDelay);

        /// <summary>
        ///  reads the data from the TagMemeory at specified Locations
        /// </summary>
        /// <param name="anyTagType"></param>
        /// <param name="anyTagId"></param>
        /// <param name="tagType"></param>
        /// <param name="tagId"></param>
        /// <param name="startingAddress"></param>
        /// <param name="noOfBytes"></param>
        /// <param name="isLongDelay"></param>
        /// <param name="multipleCommandCTBit"></param>
        /// <returns></returns>
        IRFIDTag[] ReadData(bool anyTagType, bool anyTagId, byte tagType, uint tagId,
             ushort startingAddress, byte noOfBytes, bool isLongDelay, bool multipleCommandCTBit);

        /// <summary>
        /// Write The data on tag at Specified Memory Lacation
        /// </summary>
        /// <param name="anyTagType"></param>
        /// <param name="anyTagId"></param>
        /// <param name="tagType"></param>
        /// <param name="tagId"></param>
        /// <param name="startingAddress"></param>
        /// <param name="isLongDelay"></param>
        /// <param name="data"></param>
        /// <param name="multipleCommandCTBit"></param>
        /// <returns></returns>
        IRFIDTag[] WriteData(bool anyTagType, bool anyTagId, byte tagType, uint tagId,
           ushort startingAddress, bool isLongDelay, byte[] data, bool multipleCommandCTBit);

        /// <summary>
        ///  Gets Tag Info depending on the configuration.
        /// </summary>
        IRFIDTag[] QueryTags();

        /// <summary>
        /// The Reset Reader command allows you to reset a Reader.  This reset is the same as a power reset.
        /// </summary>
        void ResetReader();

        /// <summary>
        /// This command allows you to enable a Reader.  An enabled Reader will behave with all features active.  
        /// By contrast, a disabled Reader will not transmit or receive any RF packets.  
        /// A disabled Reader will still communicate to the Host (Reader Query, Reader Configuration, etc.).
        /// </summary>
        void EnableReader(bool enable);
        /// <summary>
        /// This command Gievs the current settings about the reader.
        /// </summary>
        /// <param name="readerId"></param>
        /// <param name="readerType"></param>
        /// <param name="sendRSSI"></param>
        /// <param name="enableAtPowerUp"></param>
        /// <param name="broadcast"></param>
        void QueryReader(out ushort readerId, out string readerMode, out bool sendRSSI, out bool enableAtPowerUp,
                        out bool broadcast);
        /// <summary>
        /// It Gives the reader configuration as set
        /// </summary>
        /// <param name="readerId"></param>
        /// <param name="readerMode"></param>
        /// <param name="TX_Time"></param>
        void GetReaderConfig(out ushort readerId, out byte TX_Time);
        /// <summary>
        /// Get the Field strenth of the Reader
        /// </summary>
        /// <param name="fieldStrenth"></param>
        void GetReaderFS(out byte fieldStrenth, out bool isLongRange);
        /// <summary>
        /// Get the Information about the Version of the Reader.
        /// </summary>
        /// <param name="dataCodeVer"></param>
        /// <param name="progCodeVer"></param>
        /// <param name="hostCodeVer"></param>
        void GetReaderVersion(out byte dataCodeVer, out byte progCodeVer, out byte hostCodeVer);
        /// <summary>
        /// Set the Field Strenth of the reader depending on the Given configuration
        /// </summary>
        /// <param name="isAbsolute"></param>
        /// <param name="isIncreament"></param>
        /// <param name="fsvalue"></param>
        /// <param name="longRange"></param>
        void SetReaderFS(bool isAbsolute, bool isIncreament, byte fsvalue, bool longRange);
        /// <summary>
        /// Set the Reader Configuration  as input parameter
        /// </summary>
        /// <param name="respondToBroadCast"></param>
        /// <param name="enableAtPW"></param>
        /// <param name="sendRSSI"></param>
        void SetReaderConfig(bool respondToBroadCast, bool enableAtPW, bool sendRSSI);
        /// <summary>
        /// Set the TransM
        /// </summary>
        /// <param name="TX_Time"></param>
        void SetReaderTX_Time(ushort TX_Time);

        /// <summary>
        /// Set the new readerID to reader
        /// </summary>
        /// <param name="readerID"></param>
        void SetReaderId(ushort readerID);
        /// <summary>
        /// Set the new Host Id to the reader
        /// </summary>
        /// <param name="hostId"></param>
        void SetHostId(ushort hostId);


        #region Tag Configuration
        /// <summary>
        /// Enable Tags / Disable tags depending upon the  "enableTags" flag
        /// </summary>
        /// <param name="anyTagType"></param>
        /// <param name="anyTagId"></param>
        /// <param name="enableTags"></param>
        /// <param name="tagType"></param>
        /// <param name="tagId"></param>
        /// <param name="isLongDelay"></param>
        /// <returns></returns>
        IRFIDTag[] EnableTags(bool anyTagType, bool anyTagId, bool enableTags, byte tagType, uint tagId, bool isLongDelay);
        /// <summary>
        /// Set the new Time In Field and Group coount value
        /// GC =  no of packets to be send by the tag as response
        /// </summary>
        /// <param name="anyTagType"></param>
        /// <param name="anyTagId"></param>
        /// <param name="tagType"></param>
        /// <param name="tagId"></param>
        /// <param name="timeInField"></param>
        /// <param name="groupCount"></param>
        /// <param name="isLongDelay"></param>
        /// <returns></returns>
        IRFIDTag[] SetTagTIF_GC(bool anyTagType, bool anyTagId, byte tagType, uint tagId, byte timeInField,
                                byte groupCount, bool isLongDelay);
        /// <summary>
        /// Sets the Time in Min ,Sec or Hr depending on the settings.After which Tag should send the Tag-Packets.
        /// </summary>
        /// <param name="anyTagType"></param>
        /// <param name="anyTagId"></param>
        /// <param name="tagType"></param>
        /// <param name="tagId"></param>
        /// <param name="timeSp"></param>
        /// <param name="resentTime"></param>
        /// <param name="isLongDelay"></param>
        /// <returns></returns>
        IRFIDTag[] SetAutoSendTime(bool anyTagType, bool anyTagId, byte tagType, uint tagId, TimeSpan timeSp,
                                    bool isLongDelay);
        /// <summary>
        /// Sets the Tamper settings of the Tags which have this facility.
        /// </summary>
        /// <param name="anyTagType"></param>
        /// <param name="anyTagId"></param>
        /// <param name="tagType"></param>
        /// <param name="tagId"></param>
        /// <param name="disable"></param>
        /// <param name="reportRealTime"></param>
        /// <param name="isLongDelay"></param>
        /// <returns></returns>
        IRFIDTag[] SetTamperSettings(bool anyTagType, bool anyTagId, byte tagType, uint tagId, bool disable,
                                    bool reportRealTime, bool isLongDelay);
        /// <summary>
        /// Get the Beep configuration of the selected Tags
        /// </summary>
        /// <param name="anyTagType"></param>
        /// <param name="anyTagId"></param>
        /// <param name="tagType"></param>
        /// <param name="tagId"></param>
        /// <param name="isLongDelay"></param>
        /// <returns></returns>
        IRFIDTag[] GetTagsBeepSettings(bool anyTagType, bool anyTagId, byte tagType, uint tagId, bool isLongDelay);
        /// <summary>
        /// Get the LED configuration of the selected tags
        /// </summary>
        /// <param name="anyTagType"></param>
        /// <param name="anyTagId"></param>
        /// <param name="tagType"></param>
        /// <param name="tagId"></param>
        /// <param name="isLongDelay"></param>
        /// <returns></returns>
        IRFIDTag[] GetTagsLEDSettings(bool anyTagType, bool anyTagId, byte tagType, uint tagId, bool isLongDelay);
        /// <summary>
        /// Sets the Beepcount to be set for the Tag .
        /// </summary>
        /// <param name="anyTagType"></param>
        /// <param name="anyTagId"></param>
        /// <param name="tagType"></param>
        /// <param name="tagId"></param>
        /// <param name="beepCount"></param>
        /// <param name="isLongDelay"></param>
        /// <returns></returns>
        IRFIDTag[] SetTagsBeepSettings(bool anyTagType, bool anyTagId, byte tagType, uint tagId, ushort beepCount, bool isLongDelay);
        /// <summary>
        /// Set the LED flash count to be set for the Tag
        /// </summary>
        /// <param name="anyTagType"></param>
        /// <param name="anyTagId"></param>
        /// <param name="tagType"></param>
        /// <param name="tagId"></param>
        /// <param name="flashCount"></param>
        /// <param name="isLongDelay"></param>
        /// <returns></returns>
        IRFIDTag[] SetTagsLEDSettings(bool anyTagType, bool anyTagId, byte tagType, uint tagId, ushort flashCount, bool isLongDelay);
        #endregion Tag Configuration

        ActiveWaveTag[] GetInventory();

        /// <summary>
        /// This method is called by the ActiveWave filter to remove the tag from the reader's inventory
        /// if it is sensed by another reader.
        /// </summary>
        /// <param name="uniqueTagId">Tag to be removed</param>
        void RemoveTag(string uniqueTagId);

        /// <summary>
        /// This method is called by the ActiveWave filter to manually add the tag to the reader's inventory
        /// </summary>
        /// <param name="uniqueTagId">tag to be added</param>
        /// <param name="fGenId">FGen Id where tag is placed</param>
        /// <param name="verify">If verify = true, CallTag command is called to verify the tag presence</param>
        /// <param name="awTagType">Type of the tag to be added,ASSET,ACCESS,PERSON, etc.</param>
        /// <param name="errorMessage">Error message if AddTag fails</param>
        /// <returns>true if CallTag is successful for that tag else return false</returns>
        bool AddTag(string uniqueTagId, ushort fGenId, byte awTagType,bool verify,  out string errorMessage);

        /// <summary>
        /// Read data from tag based on template associated to reader.
        /// </summary>
        /// <param name="anyTagType">status of the TagType selection</param>
        /// <param name="anyTagId">status of the anyTagId selection</param>
        /// <param name="tagType">Type of the tag to Call</param>
        /// <param name="tagId">Tag ID to call</param>
        /// <param name="isLongDelay">
        /// The random number generator determines the amount of time between each tag packet transmitted.
        /// <para>If isLongDelay false then a short delay will take less time to transmit data and if isLongDelay true A long delay will take
        /// longer to transmit data, but will allow for better reception when many tags are transmitting simultaneously.</para>
        /// </param>
        /// <param name="multipleCommandCTBit">Flag to keep tag in awake mode for next command to read large data efficiently</param>
        /// <param name="segmentNames">Fields to read from tag data</param>
        /// <param name="retryCount">retry count for internal read data command</param>
        /// <returns>Gives dictionary if the field name and its value</returns>
        Dictionary<string, object> ReadDataSegments(bool anyTagType, bool anyTagId, byte tagType, uint tagID,
        bool isLongDelay, bool multipleCommandCTBit, string[] segmentNames, int retryCount);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="anyTagType">status of the TagType selection</param>
        /// <param name="anyTagId">status of the anyTagId selection</param>
        /// <param name="tagType">Type of the tag to Call</param>
        /// <param name="tagId">Tag ID to call</param>
        /// <param name="isLongDelay">
        /// The random number generator determines the amount of time between each tag packet transmitted.
        /// <para>If isLongDelay false then a short delay will take less time to transmit data and if isLongDelay true A long delay will take
        /// longer to transmit data, but will allow for better reception when many tags are transmitting simultaneously.</para>
        /// </param>
        /// <param name="multipleCommandCTBit">Flag to keep tag in awake mode for next command to write large data efficiently.</param>
        /// <param name="tagDataDictionary">Dictionary which contains fields and values to write on tag</param>
        /// <param name="retryCount">retry count for internal read data command</param>
        void WriteDataSegments(bool anyTagType, bool anyTagId, byte tagType, uint tagId,
            bool isLongDelay, bool multipleCommandCTBit, Dictionary<string, string> tagDataDictionary, int retryCount);

        #endregion Custom commands

        #region Properties
        /// <summary>
        /// Get or set the AWAutomodeType.
        /// </summary>
        AWAutoMode AWAutoModeType
        {
            get;
            set;
        }
        /// <summary>
        /// Checks that Tags which has Low Battery ,Irrespective we have enable/disable EnableLowBatteryCheck
        /// </summary>
        void CheckLowBattery();
        /// <summary>
        /// Get or set time pause interval between CallTags command in auto mode
        /// </summary>
        int CallTagsPauseIntervalSec
        { get; set; }

        /// <summary>
        /// Get or set time pause interval between auto mode cycle
        /// </summary>
        int AWAutoModePauseIntervalSec
        { get; set; }

        /// <summary>
        /// Get or set max retry count for CallTags command
        /// </summary>
        int CallTagsRetryCount
        { get; set; }

        /// <summary>
        /// Get or set commandTimeOut time
        /// </summary>
        int CommandTimeOut
        { get; set; }

        /// <summary>
        /// RSSI value indicating the outer boundary of the reader/zone.
        /// Tag with RSSI below OuterZoneRSSILimit will be ignored
        /// i.e. The tag is reported by the reader only if the RSSI is more than OuterZoneRSSILimit 
        /// If the tag has RSSI more than OuterZoneRSSILimit but less than InnerZoneRSSILimit
        /// it is reported as added only after checking the RSSI History of the tag.
        /// </summary>
        byte OuterZoneRSSILimit { get; set; }

        /// <summary>
        /// RSSI value indicating the inner boundary of the reader/zone.
        /// Tag with RSSI above InnerZoneRSSILimit  is detected by the reader for sure.
        /// so that tag will be reported as added immediately.
        /// </summary>
        byte InnerZoneRSSILimit { get; set; }

        #region Tagwatcher Configuration
        /// <summary>
        /// Minimum history count for the tags in Inner zone
        /// A tag in inner zone is marked as added after it is reported to the reader for InnerZoneMinHistoryCnt
        /// </summary>
        byte InnerZoneMinHistoryCnt
        { get; set; }

        /// <summary>
        /// Minimum history count for the tags in Outer zone
        /// A tag in outer zone is marked as added after it is reported to the reader for OuterZoneMinHistoryCnt
        /// </summary>
        byte OuterZoneMinHistoryCnt
        { get; set; }

        /// <summary>
        ///This is Multiple of  tagRepeatRate 
        ///Tag is reported as removed after 
        ///waiting for a time equal to   tagRemoveDelayFactor* tagRepeatRate
        /// </summary>
        byte TagRemoveDelayFactor
        { get; set; }

        #endregion Tagwatcher Configuration

        /// <summary>
        /// Minimum polling interval for Current Tags in Seconds
        /// </summary>
        int MinCurTagPollingCycleSec
        { get; set; }

        /// <summary>
        /// Maximum polling interval for Current Tags in Seconds
        /// </summary>
        int MaxCurTagPollingCycleSec
        { get; set; }

        /// <summary>
        /// Minimum polling interval for Moving Tags in Seconds
        /// </summary>
        int MinMovingTagPollingCycleSec
        { get; set; }

        /// <summary>
        /// Maximum polling interval for Moving Tags in Seconds
        /// </summary>
        int MaxMovingTagPollingCycleSec
        { get; set; }
        /// <summary>
        /// returns Fgen Ids associate with the reader.
        /// </summary>
        string[] FGens
        {
            get;
        }
        /// <summary>
        /// returns Fgen Ids associate with the reader with Fgen Type . 
        /// </summary>
        Dictionary<int, string> FGenerators
        {
            get;

        }

        /// <summary>
        /// This is the time interval after which OnDemand inventory will be done by the reader.
        /// It is applicable if the reader is in Automonomous mode and AWAutoMode is FGenAndMotionSensor.
        /// </summary>
        int OnDemandInventoryCycleMinutes
        {
            get;
            set;
        }

        /// <summary>
        /// CallTags command will be called these many no. of times for OnDemand inventory.
        /// It is applicable if the reader is in Automonomous mode and AWAutoMode is FGenAndMotionSensor.
        /// </summary>
        int OnDemandInventoryCmdIterations
        {
            get;
            set;
        }

        /// <summary>
        /// Current tags event will be fired after this time interval.
        /// It is applicable if the reader is in Automonomous mode and AWAutoMode is FGenAndMotionSensor.
        /// </summary>
        int CurrentTagPollingCycleSec
        {
            get;
            set;
        }
        /// <summary>
        /// Set the TagTypeTamperMapping  
        /// key as tagType   and Value as  "TamperType"
        /// </summary>
        Dictionary<int, string> TagTypeTamperMapping
        {
            get;
            set;
        }

        /// <summary>
        /// Set the TagTypeTamperMapping  
        /// key as tagType   and Value as  "TamperType"
        /// </summary>
        Dictionary<int, TamperWrapper> TagTypeTamperWrapperMapping
        {
            get;
            set;
        }


        Dictionary<int, string> TagTypesNameDefinitions
        {
            get;
        }

        /// <summary>
        /// Returns true if the command transmission is complete
        /// </summary>
        bool TransmissionComplete
        {
            get;
            set;
        }
        /// <summary>
        /// Health check Time Interval
        /// </summary>
        int HealthCheckIntervalSec
        {
            get;
            set;
        }
        /// <summary>
        /// Enables health check
        /// </summary>
        bool EnableHealthCheck
        {
            get;
            set;
        }
        /// <summary>
        /// Enables Battery check
        /// </summary>
        bool EnableBatteryCheck
        {
            get;
            set;
        }
        /// <summary>
        /// Low Battery check starts at this Time
        /// </summary>
        string BatteryCheckTime
        {
            get;
            set;
        }
        /// <summary>
        /// Command Iterations to find out the Low Battert Tags.
        /// </summary>
        int BatteryCheckCmdIterations
        {
            get;
            set;
        }
        #endregion Properties

        #region Event
        event EventHandler<TransmissionStateChangedEventArgs> TransmissionStateChanged;
        #endregion Event

    }

}
