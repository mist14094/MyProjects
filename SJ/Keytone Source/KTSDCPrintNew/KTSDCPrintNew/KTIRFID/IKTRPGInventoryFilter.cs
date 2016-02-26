using System;
using System.Collections.Generic;
using System.Text;

namespace KTone.Core.KTIRFID
{
    /// <summary>
    /// class used to store tag Details as object
    /// </summary>
    [Serializable]
    public class RPGInventoryTagDetails
    {
        #region Attributes

        protected string tagId = string.Empty;
        protected DateTime firstSeenTime = DateTime.Now;
        protected DateTime lastSeenTime = DateTime.Now;
        private string curReaderId = string.Empty;
        private HashSet<string> tagSeenReaderIds = new HashSet<string>();
        private string curReaderName = string.Empty;
        private int readerGroupId = -1;
        protected string filterId = string.Empty;
        protected string filterName = string.Empty;
        #endregion Attributes

        #region Constructor
        public RPGInventoryTagDetails()
        {
        }
        public RPGInventoryTagDetails(string tagId, string readerId, string readerName, int readerGroupId)
        {
            this.tagId = tagId;
            this.firstSeenTime = DateTime.Now;
            this.lastSeenTime = DateTime.Now;
            this.curReaderId = readerId;
            this.curReaderName = readerName;
            this.readerGroupId = readerGroupId;
            this.tagSeenReaderIds.Add(readerId);
        }
        #endregion Constructor

        #region Properties

        public string TagId
        {
            get
            {
                return this.tagId;
            }
        }

        public string CurReaderName
        {
            get
            {
                return this.curReaderName;
            }
        }

        public string CurReaderId
        {
            get
            {
                return this.curReaderId;
            }
        }

        public int ReaderGroupId
        {
            set
            {
                this.readerGroupId = value;
            }
            get
            {
                return this.readerGroupId;
            }
        }

        public DateTime FirstSeenTime
        {
            set
            {
                this.firstSeenTime = value;
            }
            get
            {
                return this.firstSeenTime;
            }
        }

        public DateTime LastSeenTime
        {
            get
            {
                return this.lastSeenTime;
            }
            set
            {
                this.lastSeenTime = value;
            }
        }

        public string[] TagSeenReaderIds
        {
            get
            {
                if (this.tagSeenReaderIds.Count > 0)
                {
                    string[] tagSeenReaderIdsArray = new string[this.tagSeenReaderIds.Count];
                    this.tagSeenReaderIds.CopyTo(tagSeenReaderIdsArray);
                    return tagSeenReaderIdsArray;
                }

                return null;
            }
        }

        public string FilterId
        {
            get
            {
                return this.filterId;
            }
        }

        public string FilterName
        {
            get
            {
                return this.filterName;
            }
        }
        #endregion Properties

        #region Public Methods
        public bool IsTagSeenByReader(string readerId)
        {
            return this.tagSeenReaderIds.Contains(readerId);
        }

        public void AddReaderId(string readerId)
        {
            if (!this.tagSeenReaderIds.Contains(readerId))
                this.tagSeenReaderIds.Add(readerId);
        }

        /// <summary>
        /// It will return true if the tag is removed from all the readers.
        /// </summary>
        /// <param name="readerId"></param>
        /// <returns></returns>
        public bool RemoveReaderId(string readerId)
        {
            this.tagSeenReaderIds.Remove(readerId);
            if (this.tagSeenReaderIds.Count == 0)
                return true;
            return false;
        }


        public void UpdateCurrentReaderForSameReadPoint(string readerId, string readerName)
        {
            this.lastSeenTime = DateTime.Now;
            this.curReaderId = readerId;
            this.curReaderName = readerName;
            AddReaderId(readerId);
        }

        public void UpdateReadPoint(string readerId, string readerName, int readerGroupId)
        {
            this.firstSeenTime = DateTime.Now;
            this.lastSeenTime = DateTime.Now;
            this.curReaderId = readerId;
            this.curReaderName = readerName;
            this.readerGroupId = readerGroupId;
            this.tagSeenReaderIds.Clear();
            AddReaderId(readerId);
        }

        #endregion Public Methods
    }

    [Serializable]
    public class RPGInventoryTagDetailsFilterBased : RPGInventoryTagDetails
    {
        public RPGInventoryTagDetailsFilterBased(string tagId, string filterId, string filterName)
        {
            this.tagId = tagId;
            this.firstSeenTime = DateTime.Now;
            this.lastSeenTime = DateTime.Now;
            this.filterId = filterId;
            this.filterName = filterName;
        }

        public void UpdateReadPointFilterBased(string filter, string filterName)
        {
            this.filterId = filter;
            this.filterName = filterName;
        }
    }

    [Serializable]
    public class KTRPGInventoryTagSeenEventArgs : KTComponentEventArgs
    {
        #region Attributes
        private string tagId = string.Empty;
        private string readerId = string.Empty;
        private string readerName = string.Empty;
        private string readPointName = string.Empty;
        private List<string> inventory = null;
        private string filterID = string.Empty;
        private string filterName = string.Empty;
        private bool isFilterBased = false;
        #endregion Attributes

        #region Constructor
        public KTRPGInventoryTagSeenEventArgs(IKTComponent component, string eventId, string tagId,
                    string readerId, string readerName, string readPointName, List<string> inventory, DateTime timeStamp) :
            base(component, eventId, timeStamp)
        {
            this.tagId = tagId;
            this.readerId = readerId;
            this.readerName = readerName;
            this.readPointName = readPointName;
            this.inventory = inventory;
        }

        public KTRPGInventoryTagSeenEventArgs(IKTComponent component, string eventId, string tagId,
                    string filterId, string filterName, string readPointName, List<string> inventory, DateTime timeStamp, bool isFilterBased) :
            base(component, eventId, timeStamp)
        {
            this.tagId = tagId;
            this.filterID = filterId;
            this.filterName = filterName;
            this.readPointName = readPointName;
            this.inventory = inventory;
            this.isFilterBased = isFilterBased;
        }
        #endregion Constructor

        #region Properties
        public string FilterID
        {
            get
            {
                return this.filterID;
            }
        }
        public string FilterName
        {
            get
            {
                return this.filterName;
            }
        }
        public bool IsFilterBased
        {
            get
            {
                return this.isFilterBased;
            }
        }
        public string TagID
        {
            get
            {
                return this.tagId;
            }
        }
        public string ReaderId
        {
            get
            {
                return this.readerId;
            }
        }

        public string ReaderName
        {
            get
            {
                return this.readerName;
            }
        }

        public string ReadPointName
        {
            get
            {
                return this.readPointName;
            }
        }

        public List<string> Inventory
        {
            get
            {
                return this.inventory;
            }
        }
        #endregion Properties
    }

    public interface IKTRPGInventoryFilter
    {
        #region Events
        /// <summary>
        /// This event will be fired when the tag is seen for the first time and when it is added to the inventory.
        /// </summary>
        event EventHandler<KTRPGInventoryTagSeenEventArgs> TagEntered;
        /// <summary>
        /// This event will be fired when the tag is removed from inventory
        /// </summary>
        event EventHandler<KTRPGInventoryTagSeenEventArgs> TagLeft;
        event EventHandler<KTTagMovedEventArgs> TagMoved;
        #endregion Events

        #region Methods
        /// <summary>
        /// This method will return tags per read point.
        /// </summary>
        /// <returns></returns>
        Dictionary<string, List<RPGInventoryTagDetails>> GetInventoryPerReadPoint();

        /// <summary>
        /// This method will return tag count per read point.
        /// </summary>
        /// <returns></returns>
        Dictionary<string, int> GetInventoryCountPerReadPoint();

        /// <summary>
        /// This method will return inventory for a given read point.
        /// </summary>
        /// <param name="readPointName"></param>
        /// <returns></returns>
        List<RPGInventoryTagDetails> GetInventoryForReadPoint(string readPointName);

        /// <summary>
        /// Gives all Reade Point Id and Name
        /// </summary>
        /// <returns></returns>
        Dictionary<int, string> GetAllReadPointNames();
        /// <summary>
        /// This method will clear all the tags from the inventory.
        /// </summary>
        void ClearInventory();
        #endregion Methods

        #region Properties
        
        bool IsComponentFilterBased
        { get; }

        #endregion Properties
    }
}
