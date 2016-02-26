using System;
using System.Collections.Generic;
using System.Text;

namespace KTone.Core.KTIRFID
{
    [Serializable]
    public class TagLocation
    {
        #region Attributes
        private string uniqueTagId;
                
        private string xPos;
                
        private string yPos;
        
        private string zone;
        #endregion Attributes

        #region Constructor
        public TagLocation(string uniqueTagId,string xPos,string yPos,string zone)
        {
            this.uniqueTagId = uniqueTagId;
            this.xPos = xPos;
            this.yPos = yPos;
            this.zone = zone;
        }
        #endregion Constructor

        #region Properties
        public string UniqueTagId
        {
            get { return uniqueTagId; }
            set { uniqueTagId = value; }
        }

        public string XPos
        {
            get { return xPos; }
            set { xPos = value; }
        }

        public string YPos
        {
            get { return yPos; }
            set { yPos = value; }
        }

        public string Zone
        {
            get { return zone; }
            set { zone = value; }
        }
        #endregion Properties
    }

    [Serializable]
    public class TagBeepDetails
    {
        private string readerId = string.Empty;
        private uint tagId = 0;
        private int maxBeepCount = 0;
        private DateTime timeStamp = DateTime.Now;
        private bool isLEDON = true;
        private bool isSpeakerON = true;
        private int pauseTimeForTagBeep = 15;

        public string ReaderId
        {
            get { return readerId; }
        }

        public uint TagId
        {
            get { return tagId; }
        }

        public int MaxBeepCount
        {
            get { return maxBeepCount; }
            set { maxBeepCount = value; }
        }

        public bool ISLEDON
        {
            get { return isLEDON; }
        }
        public bool ISSpeakerON
        {
            get { return isSpeakerON; }
        }
        public int PauseTimeForTagBeep
        {
            get
            {
                return this.pauseTimeForTagBeep;
            }
        }
        public TagBeepDetails(string readerId, uint tagId, int maxBeepCount, bool isLEDOn, bool isSpeakerOn, int pauseTimeForTagBeep)
        {
            this.readerId = readerId;
            this.tagId = tagId;
            this.maxBeepCount = maxBeepCount;
            this.isLEDON = isLEDOn;
            this.isSpeakerON = isSpeakerOn;
            this.pauseTimeForTagBeep = pauseTimeForTagBeep;
        }

        public double GetSleepForCallTag()
        {
            if (DateTime.Now > this.timeStamp)
                return 0;

            TimeSpan ts = this.timeStamp - DateTime.Now;
            return ts.TotalSeconds;
        }

        public void UpdateNextBeepTime()
        {
            this.timeStamp = DateTime.Now.AddSeconds(this.pauseTimeForTagBeep);
        }
    }

    public interface IKTAWFilter
    {
        event EventHandler<AWTagEventArgs> TagsAdded;

        event EventHandler<AWTagEventArgs> TagsRemoved;

        event EventHandler<AWTagEventArgs> TagsCurrent;

        /// <summary>
        /// Fired when the tag is moved across the FGens added in the filter
        /// </summary>
        event EventHandler<AWTagMovedEventArgs> TagMoved;

        /// <summary>
        /// get the all Field gennrators associate with with filter 
        /// </summary>
        string[] FGens
        {
            get;
        }

        /// <summary>
        /// Returns the total tags present in each zone 
        /// </summary>
        /// <returns></returns>
        int InventoryCount();

        /// <summary>
        /// Returns the current tags present in all zones
        /// </summary>
        /// <returns></returns>
        Dictionary<string, ActiveWaveTag> Inventory();

        /// <summary>
        /// Returns the total tags present in each reader
        /// </summary>
        /// <returns></returns>
        Dictionary<string, int> InventoryCountPerReader();

        /// <summary>
        /// Returns the current tags present in each reader
        /// </summary>
        /// <returns></returns>
        Dictionary<string, ActiveWaveTag[]> InventoryPerReader();

        /// <summary>
        /// Returns the total tags present in each field generator
        /// </summary>
        /// <returns></returns>
        Dictionary<string, int> InventoryCountPerFGen();

        /// <summary>
        /// Returns the current tags present in each field generator
        /// </summary>
        /// <returns></returns>
        Dictionary<string, ActiveWaveTag[]> InventoryPerFGen();

        /// <summary>
        /// Returns the readerId of the zone in which the tag is present
        /// </summary>
        /// <param name="tagId"></param>
        /// <returns></returns>
        string GetZone(string tagId);
        /// <summary>
        /// Returns Inventory of the Removed Tag.
        /// </summary>
        /// <returns></returns>
        Dictionary<string, ActiveWaveTag> RemovedTagInventory();


        /// <summary>
        ///  Gets Tag Info depending on the configuration.
        /// </summary>
        /// <param name="tagId">Tag IDs to call</param>
        /// <param name="isLEDOn">Status of the LED when call</param>
        /// <param name="isSpeakerOn">Status of the Speaker.</param>
        /// <param name="repeatCount"> repeatCount</param>
        /// <param name="dueTimeMS"> dueTimeMS</param>
        /// <returns>ActiveWaveTag[]</returns>
        ActiveWaveTag CallTags(uint tagId, bool isLEDOn, bool isSpeakerOn,
                                string readerId, ushort fGenId, int repeatCount, int dueTimeMS);

        /// <summary>
        ///  Gets Tag Info depending on the configuration.
        /// </summary>
        /// <param name="tagId">Tag IDs to call</param>
        /// <param name="isLEDOn">Status of the LED when call</param>
        /// <param name="isSpeakerOn">Status of the Speaker.</param>
        /// <param name="repeatCount"> repeatCount</param>
        /// <param name="dueTimeMS"> dueTimeMS</param>
        /// <returns>ActiveWaveTag[]</returns>
        ActiveWaveTag CallTags(uint tagId, bool isLEDOn, bool isSpeakerOn,
                                string readerId, ushort fGenId, int repeatCount, int dueTimeMS, bool isAsync);

        /// <summary>
        /// It will set TagType-TamperMapping for all the readers of the filter.
        /// </summary>
        /// <param name="tagTypeTamperMapping"></param>
        /// <param name="errorString">Error details</param>
        void SetTagTypeTamperMapping(Dictionary<int, TamperWrapper> tagTypeTamperMapping, out string errorString);

        /// <summary>
        /// It will return TagType-TamperMapping for all the readers of the filter.
        /// </summary>
        /// <param name="errorString">Error details</param>
        Dictionary<string, Dictionary<int, TamperWrapper>> GetTagTypeTamperMapping(out string errorString);

        /// <summary>
        /// Add tag to the given reader
        /// </summary>
        /// <param name="toReaderId">reader id</param>
        /// <param name="toFGenId">FGen id</param>
        /// <param name="tagId">tag id</param>
        /// <param name="awTagType">Type of the tag to be added,ASSET,ACCESS,PERSON, etc.</param>
        /// <returns>returns true if add is successful else false</returns>
        bool AddTag(string toReaderId, ushort toFGenId, string tagId, byte awTagType, out string errorMessage);

        /// <summary>
        /// Removes tag from the filter
        /// </summary>
        /// <param name="tagId">tag id</param>
        /// <param name="readerId">reader id from which tag is removed</param>
        /// <param name="awTagType">Type of the removed tag</param>
        /// <returns>returns true if remove is successful else false</returns>
        bool RemoveTag(string tagId, out string readerId, out ushort fgenId, out byte awTagType, out string errorMessage);

        /// <summary>
        /// Moves tag from current reader to given reader
        /// </summary>
        /// <param name="moveToReaderId">reader id</param>
        /// <param name="moveToFGenId">Fgen id</param>
        /// <param name="tagId">tag id to move</param>
        /// <returns>returns true if tag move is successful else false</returns>
        bool MoveTag(string moveToReaderId, ushort moveToFGenId, string tagId, out string errorMessage);

        bool RemoveTagFromRemoveInventory(string tagId);

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

        #region Alert Beep Commands

        void AddTagForBeepAlert(string readerId, string tagId, int maxBeepCount, bool isLEDOn, bool isSpeakerOn);
        void AddTagForBeepAlert(string readerId, string tagId, int maxBeepCount, bool isLEDOn, bool isSpeakerOn,int pauseTimeForTagBeep);

        void RemoveTagForBeepAlert(string readerId, string tagId);

        Dictionary<string, List<TagBeepDetails>> GetAllDetailsOfTagBeepAlert();
        List<string> GetAllCurrentBeepTags();

        #endregion Alert Beep Commands

        #region Properties
        /// <summary>
        /// Returns the list of Redaer Ids added to the filter 
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
        #region Autopolling Configuration
        /// <summary>
        /// Polling Interval for Current tags reporting in seconds
        /// All the readers added in the filter are polled after CurrentTagCycleSec
        /// Calculated internally depending on the number of tags and as per the given limits
        /// </summary>
        int CurrentTagPollingCycleSec
        { get; }

        /// <summary>
        /// Polling Interval for Moving tags reporting in seconds
        /// The tag movement across the readers is checked after MovingTagCycleSec
        /// Calculated internally depending on the number of tags and as per the given limits
        /// </summary>
        int MovingTagPollingCycleSec
        { get; }

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

        #endregion Autopolling Configuration
        /// <summary>
        /// Get the TagType Definitions form one of the selected reader.
        /// </summary>
        Dictionary<int, string> TagTypesNameDefinitions
        {
            get;
        }

        /// <summary>
        /// Readerids and Fgens
        /// </summary>
        Dictionary<string, Dictionary<int, string>> ReaderFGens
        {
            get;
        }
        #endregion Properties
    }
}
