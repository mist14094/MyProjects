using System;
using System.Collections.Generic;
using System.Text;

namespace KTone.Core.KTIRFID
{
    public interface IWTRFReaderSettings
    {
        #region Properties
        /// <summary>
        /// Returns Reader model
        /// </summary>
        WTReaderModel WTModel
        {
            get;
        }
        /// <summary>
        /// Returns Reader name assigned to the reader object
        /// </summary>
        string ReaderName
        {
            get;
        }

        /// <summary>
        /// Returns Reader description  assigned to the reader object
        /// </summary>
        string ReaderDescription
        {
            get;
        }

        /// <summary>
        /// Returns the flag  which indicates whether reader is autopolling mode
        /// </summary>
        bool IsAutoPollingOn
        {
            get;
        }

        /// <summary>
        /// Returns the flag  which indicates whether reader is connect or not.
        /// </summary>
        bool IsConnected
        {
            get;
        }

        /// <summary>
        /// Gets and Sets a flag which enables the display of command and response details from the reader
        /// </summary>
        bool EchoCmdResponse
        {
            get;
            set;
        }
              

        /// <summary>
        /// RSSI value indicating the outer boundary of the reader/zone.
        /// Tag with RSSI below OuterZoneRSSILimit will be ignored
        /// i.e. The tag is reported by the reader only if the RSSI is more than OuterZoneRSSILimit 
        /// If the tag has RSSI more than OuterZoneRSSILimit but less than InnerZoneRSSILimit
        /// it is reported as added only after checking the RSSI History of the tag.
        /// </summary>
        byte OuterZoneRSSILimit { get; set;}

        /// <summary>
        /// RSSI value indicating the inner boundary of the reader/zone.
        /// Tag with RSSI above InnerZoneRSSILimit  is detected by the reader for sure.
        /// so that tag will be reported as added immediately.
        /// </summary>
        byte InnerZoneRSSILimit { get; set; }
        /// <summary>
        /// Returns the Mode of WaveTrend Reader Ondemand or Auto Mode.
        /// </summary>
        RFIDReaderMode RFIDReaderMode
        {
            get;
        }
        /// <summary>
        /// Returns the CommunicationDetails of WaveTrend Reader Ondemand or Auto Mode.
        /// </summary>
        string CommunicationDetails
        {
            get;
        }

        #region Autopolling Configuration
        /// <summary>
        ///Polling Interval for Current tags reporting in seconds
        ///All the readers added in the filter are polled after CurrentTagCycleSec
        ///Calculated internally depending on the number of tags and as per the given limits";
        /// </summary>
        int CurrentTagPollingCycleSec
        { get;}

        /// <summary>
        /// Polling Interval for Moving tags reporting in seconds
        /// The tag movement across the readers is checked after MovingTagCycleSec
        /// Calculated internally depending on the number of tags and as per the given limits.
        /// </summary>
        int MovingTagPollingCycleSec
        { get;}

        /// <summary>
        /// Minimum polling interval for Current Tags in Seconds
        /// </summary>
        int MinCurTagPollingCycleSec
        { get;set;}

        /// <summary>
        /// Maximum polling interval for Current Tags in Seconds
        /// </summary>
        int MaxCurTagPollingCycleSec
        { get;set;}

        /// <summary>
        /// Minimum polling interval for Moving Tags in Seconds
        /// </summary>
        int MinMovingTagPollingCycleSec
        { get;set;}

        /// <summary>
        /// Maximum polling interval for Moving Tags in Seconds
        /// </summary>
        int MaxMovingTagPollingCycleSec
        { get;set;}

        #endregion Autopolling Configuration

        #region Tagwatcher Configuration
        /// <summary>
        /// Minimum history count for the tags in Inner zone
        /// A tag in inner zone is marked as added after it is reported to the reader for InnerZoneMinHistoryCnt
        /// </summary>
        byte InnerZoneMinHistoryCnt
        { get;set;}

        /// <summary>
        /// Minimum history count for the tags in Outer zone
        /// A tag in outer zone is marked as added after it is reported to the reader for OuterZoneMinHistoryCnt
        /// </summary>
        byte OuterZoneMinHistoryCnt
        { get;set;}

        /// <summary>
        ///This is Multiple of  tagRepeatRate 
        ///Tag is reported as removed after 
        ///waiting for a time equal to   tagRemoveDelayFactor* tagRepeatRate
        /// </summary>
        byte TagRemoveDelayFactor
        { get;set;}

        #endregion Tagwatcher Configuration
        /// <summary>
        /// We can set only 1 site code on the reader.This command allows us 
        /// to filter the tags based on multiple site codes.
        /// </summary>
        /// <param name="sitecodes"></param>
        uint[] MultipleSiteCodes { get; set; }

        #endregion Properties

        #region Custom Commands
        /// <summary>
        /// Returns the tags existing on the reader
        /// </summary>
        /// <returns></returns>
        WTTag[] GetInventory();

        /// <summary>
        /// Returns the tags existing on the reader,returns two sepatate arrays for inner and outer zone tags
        /// </summary>
        /// <param name="innerZoneTags"></param>
        /// <param name="outerZoneTags"></param>
        /// <returns></returns>
        void GetInventory(out WTTag[] innerZoneTags, out WTTag[] outerZoneTags);

        /// <summary>
        /// Returns the count of the tags existing on the reader
        /// </summary>
        /// <returns></returns>
        int GetInventoryCount();

        /// <summary>
        /// Returns tag if present in the tag history
        /// </summary>
        /// <param name="tagId"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        bool IsTagPresent(string tagId, out WTTag tag);


        /// <summary>
        ///  Returns current tag history
        /// </summary>
        /// <param name="uniqueId"></param>
        WTTagHistory[] GetTagHistory(string uniqueId);

        #region Monitoring specified tags
        /// <summary>
        /// Starts monitoring the specified tags,stores the tag history for given tagHistoryCnt
        /// </summary>
        /// <param name="uniqueTagIds"></param>
        /// /// <param name="tagHistoryCnt"></param>
        void MonitorTags(string[] uniqueTagIds, int tagHistoryCnt);

        /// <summary>
        /// Returns the tag history(RSSI and Timestamp) of the tags added for monitoring
        /// </summary>
        /// <returns>Dictionary of tag unique id Vs tag history </returns>
        Dictionary<string, Queue<WTTagHistory>> GetMonitoredTags();

        /// <summary>
        /// Adds the tag in the monitred tag list
        /// </summary>
        /// <param name="uniqueTagId"></param>
        void AddMonitorTag(string uniqueTagId);

        /// <summary>
        /// Removes the tag from the monitred tag list
        /// </summary>
        /// <param name="uniqueTagId"></param>
        void RemoveMonitoredTag(string uniqueTagId);

        /// <summary>
        /// Clears the monitoring tag list
        /// </summary>
        void ClearMonitoredTagList();

        #endregion Monitoring specified tags


        #endregion Custom Commands

        #region Events

        /// <summary>
        /// Event raised after each tag read when 
        /// the reader is in auto polling mode.
        /// Data is passed using WTReaderTagReadEventArgs
        /// </summary>
        event EventHandler<WTReaderEventArgs> OnWTReaderTagRead;
        event EventHandler<WTReaderEventArgs> OnFOBSeen;

        #endregion Events
    }
}
