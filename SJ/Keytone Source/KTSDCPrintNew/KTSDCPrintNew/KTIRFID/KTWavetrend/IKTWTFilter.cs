using System;
using System.Collections.Generic;
using System.Text;

namespace KTone.Core.KTIRFID
{
    public interface IKTWTFilter
    {
        /// <summary>
        /// Fired when the tag is moved across the readers added in the filter
        /// </summary>
        event EventHandler<WTTagMovedEventArgs> TagMoved;
      
        /// <summary>
        /// Event raised when the tag is tampered, cloned or missing.
        /// Data is passed using WTTagTamperedEventArgs
        /// </summary>
        event EventHandler<WTTagTamperedEventArgs> OnTagTampered;

        /// <summary>
        /// Event raised when the tag read with fob alarm
        /// Data is passed using WTFOBSeenEventArgs
        /// </summary>
        event EventHandler<WTFOBSeenEventArgs> OnFOBSeen;

        /// <summary>
        /// Returns the total tags present in each zone 
        /// </summary>
        /// <returns></returns>
        int InventoryCount();

        /// <summary>
        /// Returns the current tags present in all zones
        /// </summary>
        /// <returns></returns>
        Dictionary<string, WTTag> Inventory();

        /// <summary>
        /// Returns the total tags present in each zone 
        /// </summary>
        /// <returns></returns>
        Dictionary<string, int> InventoryCountPerReader();

        /// <summary>
        /// Returns the current tgas present in all zones
        /// </summary>
        /// <returns></returns>
        Dictionary<string, WTTag[]> InventoryPerReader();

        /// <summary>
        /// Returns the readerId of the zone in which the tag is present
        /// </summary>
        /// <param name="tagId"></param>
        /// <returns></returns>
        string GetZone(string tagId);
        /// <summary>
        /// RSSI value indicating the inner boundary of the reader/zone.
        /// Tag with RSSI above InnerZoneRSSILimit  is detected by the reader for sure.
        /// so that tag will be reported as added immediately.
        /// </summary>
        /// <param name="innerZoneRSSILimit">Inner Zone RSSI Limit value to be set</param>
        /// <param name="errorString">string containing error message</param>
        void SetInnerZoneRSSILimit(byte innerZoneRSSILimit, out Dictionary<string, string> errorString);
        /// <summary>
        /// RSSI value indicating the inner boundary of the reader/zone.
        /// Tag with RSSI above InnerZoneRSSILimit  is detected by the reader for sure.
        /// so that tag will be reported as added immediately.
        /// </summary>
        /// <returns>Inner Zone RSSI Limit</returns>
        Dictionary<string,byte> GetInnerZoneRSSILimit();
        /// <summary>
        /// Sets RSSI value indicating the outer boundary of the reader/zone.
        /// Tag with RSSI below OuterZoneRSSILimit will be ignored
        /// i.e. The tag is reported by the reader only if the RSSI is more than OuterZoneRSSILimit 
        /// If the tag has RSSI more than OuterZoneRSSILimit but less than InnerZoneRSSILimit
        /// it is reported as added only after checking the RSSI History of the tag.
        /// </summary>
        /// <param name="outerZoneRSSILimit">Outer Zone RSSI Limit value to be set</param>
        /// <param name="errorString">string containing error message</param>
        void SetOuterZoneRSSILimit(byte outerZoneRSSILimit, out Dictionary<string, string> errorString);
        /// <summary>
        /// Gets RSSI value indicating the outer boundary of the reader/zone.
        /// Tag with RSSI below OuterZoneRSSILimit will be ignored
        /// i.e. The tag is reported by the reader only if the RSSI is more than OuterZoneRSSILimit 
        /// If the tag has RSSI more than OuterZoneRSSILimit but less than InnerZoneRSSILimit
        /// it is reported as added only after checking the RSSI History of the tag.
        /// </summary>
        /// <returns>Outer Zone RSSI Limit</returns>
        Dictionary<string, byte> GetOuterZoneRSSILimit();
        /// <summary>
        /// <summary>
        ///Polling Interval for Current tags reporting in seconds
        ///All the readers added in the filter are polled after CurrentTagCycleSec
        ///Calculated internally depending on the number of tags and as per the given limits";
        /// </summary>
        /// <param name="MinCurTagPollingCycleSec">MinCurTagPollingCycleSec</param>
        /// <param name="MaxCurTagPollingCycleSec">MaxCurTagPollingCycleSec</param>
        /// <param name="errorString">string containing error message</param>
        void SetCurrentTagPollingCycleLimits(int minCurTagPollingCycleSec, int maxCurTagPollingCycleSec, out Dictionary<string, string> errorString);
        /// <summary>
        /// <summary>
        ///Polling Interval for Current tags reporting in seconds
        ///All the readers added in the filter are polled after CurrentTagCycleSec
        ///Calculated internally depending on the number of tags and as per the given limits";
        /// </summary>
        /// <returns>Minimum and Minimum Current Tag Polling Cycle Limits</returns>
        Dictionary<string, int[]> GetCurrentTagPollingCycleLimits();
        /// <summary>
        /// Polling Interval for Moving tags reporting in seconds
        /// The tag movement across the readers is checked after MovingTagCycleSec
        /// Calculated internally depending on the number of tags and as per the given limits.
        /// </summary>
        /// <param name="MinMovingTagPollingCycleSec">MinMovingTagPollingCycleSec</param>
        /// <param name="MaxMovingTagPollingCycleSec">MinMovingTagPollingCycleSec</param>
        /// <param name="errorString">string containing error message</param>
        void SetMovingTagPollingCycleLimits(int minMovingTagPollingCycleSec, int maxMovingTagPollingCycleSec, out Dictionary<string, string> errorString);
        /// <summary>
        /// Polling Interval for Moving tags reporting in seconds
        /// The tag movement across the readers is checked after MovingTagCycleSec
        /// Calculated internally depending on the number of tags and as per the given limits.
        /// </summary>
        /// <returns>Minimum and Minimum Moving Tag Polling Cycle Limits</returns>
        Dictionary<string, int[]> GetMovingTagPollingCycleLimits();
        //void SetCurrentTagPollingCycleLimits(byte minVal,
        /// <summary>
        /// Set Minimum history count for the tags in Inner zone
        /// A tag in inner zone is marked as added after it is reported to the reader for InnerZoneMinHistoryCnt
        /// </summary>
        /// <param name="innerZoneMinimumHistoryCount">Inner Zone Minimum History Count value to be set</param>
        /// <param name="errorString">string containing error message</param>
        void SetInnerZoneMinimumHistoryCount(byte innerZoneMinimumHistoryCount, out Dictionary<string, string> errorString);
        /// <summary>
        /// Get Minimum history count for the tags in Inner zone
        /// A tag in inner zone is marked as added after it is reported to the reader for InnerZoneMinHistoryCnt
        /// </summary>
        /// <returns>Inner Zone Minimum History Count</returns>
        Dictionary<string, byte> GetInnerZoneMinimumHistoryCount();
        /// <summary>
        /// Set Minimum history count for the tags in Outer zone
        /// A tag in outer zone is marked as added after it is reported to the reader for OuterZoneMinHistoryCnt
        /// </summary>
        /// <param name="innerZoneMinimumHistoryCount">Outer Zone Minimum History Count value to be set</param>
        /// <param name="errorString">string containing error message</param>
        void SetOuterZoneMinimumHistoryCount(byte outerZoneMinimumHistoryCount, out Dictionary<string, string> errorString);
        /// <summary>
        /// Get Minimum history count for the tags in Outer zone
        /// A tag in outer zone is marked as added after it is reported to the reader for OuterZoneMinHistoryCnt
        /// </summary>
        /// <returns>Outer Zone Minimum History Count</returns>
        Dictionary<string, byte> GetOuterZoneMinimumHistoryCount();
        /// <summary>
        /// Set TagMove Report Delay Factor
        /// This is Multiple of  tagRepeatRate 
        /// Tag is reported as added and checked for movement after 
        /// waiting for a time equal to   * tagRepeatRate
        /// </summary>
        /// <param name="tagMoveReportDelayFactor">Tag Move Report Delay Factor value to be set</param>
        /// <param name="errorString">string containing error message</param>
        void SetTagMoveReportDelayFactor(byte tagMoveReportDelayFactor, out Dictionary<string, string> errorString);
        /// <summary>
        /// Get TagMove Report Delay Factor
        /// This is Multiple of  tagRepeatRate 
        /// Tag is reported as added and checked for movement after 
        /// waiting for a time equal to   * tagRepeatRate
        /// </summary>
        /// <returns>Tag Move Report Delay Factor</returns>
        Dictionary<string, byte> GetTagMoveReportDelayFactor();
        /// <summary>
        /// We can set only 1 site code on the reader.This command allows us 
        /// to filter the tags based on multiple site codes.
        /// </summary>
        /// <param name="multipleSites">Multiple site codes to be set</param>
        /// <param name="errorString">string containing error message</param>
        void SetMultipleSiteCodes(uint[] multipleSites, out Dictionary<string, string> errorString);
        /// <summary>
        /// We can set only 1 site code on the reader.This command allows us 
        /// to filter the tags based on multiple site codes.
        /// </summary>
        /// <returns>Multiple site codes</returns>
        Dictionary<string, uint[]> GetMultipleSiteCodes();
        /// <summary>
        /// Returns Inventory of the Removed Tag.
        /// </summary>
        /// <returns></returns>
        Dictionary<string, WTTag> RemovedTagInventory();

        /// <summary>
        /// Removes the Tag from the RemoveInventory
        /// </summary>
        /// <param name="tagId"></param>
        void RemoveTagFromRemovedInventory(string tagId);
        #region TagLocation Commands
        /// <summary>
        /// These commands are supporeted by Split Filter only.
        /// </summary>
        /// <param name="uniqueTagId"></param>
        /// <param name="xPos"></param>
        /// <param name="yPos"></param>
        /// <param name="zone"></param>
        void UpdateTagLocation(string uniqueTagId, string xPos, string yPos, string zone);
        void RemoveTagLocation(string uniqueTagId);
        Dictionary<string, TagLocation> GetAllTagLocations();
        TagLocation GetTagLocation(string uniqueTagId);
        #endregion TagLocation Commands
        /// <summary>
        /// Gives current Temperature of the tag
        /// </summary>
        /// <param name="tagId"></param>
        /// <returns></returns>
        int GetCurrentTemperature(string tagId);
        /// <summary>
        /// Gives All Temperatures associated with the Tag
        /// </summary>
        /// <param name="tagId"></param>
        /// <param name="currentTemp"></param>
        /// <param name="minTemp"></param>
        /// <param name="maxTemp"></param>
        void GetTemperatures(string tagId,out int currentTemp,out int minTemp,out int maxTemp);

        /// <summary>
        /// Returns tag temperature history
        /// </summary>
        /// <param name="tagId"></param>
        /// <returns></returns>
        SortedList<DateTime, int> GetTemperatureHistory(string tagId);

        /// <summary>
        /// Returns reader groups associated with reader present in the filter
        /// Key in dictionary is Reader Id
        /// Value against the key is ReaderGroup name
        /// </summary>
        /// <returns></returns>
        Dictionary<string, string> GetReaderGroups();


        #region Properties
        /// <summary>
        /// Returns the list of Reader Ids added to the filter 
        /// </summary>
        string[] Readers
        {
            get;
        }

        /// <summary>
        /// Returns a dictionary of reader ids and names
        /// </summary>
        Dictionary<string, string> ReaderNames
        {
            get;
        }
        /// <summary>
        ///This is Multiple of  tagRepeatRate 
        ///Tag is reported as added and checked for movement after 
        ///waiting for a time equal to   * tagRepeatRate
        /// </summary>
        byte TagMoveReportDelayFactor
        { get;set;}

        #region Autopolling Configuration
        /// <summary>
        /// Polling Interval for Current tags reporting in seconds
        /// All the readers added in the filter are polled after CurrentTagCycleSec
        /// Calculated internally depending on the number of tags and as per the given limits
        /// </summary>
        int CurrentTagPollingCycleSec
        { get;}

        /// <summary>
        /// Polling Interval for Moving tags reporting in seconds
        /// The tag movement across the readers is checked after MovingTagCycleSec
        /// Calculated internally depending on the number of tags and as per the given limits
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
        #endregion Properties
    }
}
