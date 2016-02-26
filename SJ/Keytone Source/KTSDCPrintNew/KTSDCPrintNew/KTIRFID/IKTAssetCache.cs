using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Xml;

namespace KTone.Core.KTIRFID
{
    public enum AssetCategory
    {
        Physical,
        Person,
        Section,
    }

    public enum RFTagType
    {
        Active,
        Passive,
        Barcode,
    }

    /// <summary>
    /// Interface to access the asset cache
    /// </summary>
    public interface IKTAssetCache
    {
        #region [Methods]

        #region [Asset]

        /// <summary>
        /// Returns all the assets available in the cache for provided Asset Category.
        /// </summary>
        /// <returns>List of KTAssetDetails</returns>
        List<KTAssetDetails> GetAllAssets(int dataOwnerID, AssetCategory category);

        /// <summary>
        /// Returns asset details for the asset having the specified asset id
        /// </summary>
        /// <param name="assetID">asset id of the required asset</param>
        /// <returns>KTAssetDetails</returns>
        KTAssetDetails GetAssetForAssetID(int dataOwnerID, long assetID);

        /// <summary>
        /// Returns asset details for the asset having the specified asset id
        /// </summary>
        /// <param name="assetID">asset id of the required asset</param>
        /// <returns>KTAssetDetails</returns>
        KTAssetDetails GetAssetForAssetID(long assetID);


        /// <summary>
        /// Returns an asset for a given internalTagId.
        /// </summary>
        /// <param name="internalTagId">internal tag id i.e. id of the RFID tag.</param>
        /// <returns>KTAssetDetails</returns>
        KTAssetDetails GetAssetForRFTagId(int dataOwnerID, string rfTagId);

        /// <summary>
        /// Returns an asset for a given externalTagId.
        /// </summary>
        /// <param name="externalTagId">external tag id i.e. formatted asset tag id.</param>
        /// <returns>KTAssetDetails</returns>
        KTAssetDetails GetAssetForCompanyAssetId(int dataOwnerID, string CompanyAssetId);

        /// <summary>
        /// Returns list of a certain number of assets(count) KTAssetDetails present in cache,
        ///which have the specifed assetType.
        /// </summary>
        /// <param name="assetType"></param>
        /// <returns></returns>
        List<KTAssetDetails> GetAssetsForAssetClass(int dataOwnerID, string assetClass);

        /// <summary>
        /// Returns list of a certain number of assets(count) KTAssetDetails present in cache,
        ///which have the specifed assetClass and Asset Instance.
        /// </summary>
        /// <param name="assetClass"></param>
        /// <returns></returns>
        List<KTAssetDetails> GetAssetsForAssetClass(int dataOwnerID, string assetClass, string assetInstance);

        /// <summary>
        /// Returns list of child Asset IDs for provided AssetID
        /// </summary>
        /// <param name="assetID"></param>
        /// <returns></returns>
        List<long> GetAssetAssociation(long assetID);

        /// <summary>
        /// Returns all the section assets available in the cache.
        /// </summary>
        /// <param name="dataOwnerID"></param>
        /// <returns>List of KTAssetDetails</returns>
        List<KTAssetDetails> GetAllSections(int dataOwnerID);

        /// <summary>
        /// Returns section for provided resource id.
        /// </summary>
        /// <param name="dataOwnerID"></param>
        /// <returns></returns>
        KTAssetDetails GetSectionOnResourceID(int dataOwnerID, string resourceID);

        #endregion [Asset]

        #region [Asset Class]

        /// <summary>
        /// Returns dictionary of all Asset class details available with asset class name as key 
        /// </summary>
        /// <returns>Dictionary of AssetClassDetails</returns>
        Dictionary<string, KTAssetClassDetails> GetAllAssetClassDetails(int dataOwnerID);

        /// <summary>
        /// Gets Asset Class Detail for provided asset class name
        /// </summary>
        /// <param name="AssetClassName"></param>
        /// <returns></returns>
        KTAssetClassDetails GetAssetClassDetails(int dataOwnerID, string AssetClassName);

        /// <summary>
        /// Get all asset class instance based on Asset Class ID
        /// </summary>
        /// <param name="AssetClassID"></param>
        /// <returns></returns>
        List<KTAssetClassInstance> GetAssetClassInstance(int dataOwnerID, long AssetClassID);

        #endregion [Asset Class]

        #region [Zone]

        /// <summary>
        /// Update Current ZoneID & Default ZoneID for provided list of AssetID
        /// </summary>
        /// <param name="dataOwnerID"></param>
        /// <param name="zoneID"></param>
        /// <param name="assetIDs"></param>
        /// <param name="isCurrentZone"></param>
        /// <param name="isDefaultZone"></param>
        /// <returns></returns>
        bool UpdateZone(int dataOwnerID, long zoneID, List<long> assetIDs, bool isCurrentZone, bool isDefaultZone);

        /// <summary>
        /// Update Current ZoneID & Default ZoneID for provided list of RFTagID
        /// </summary>
        /// <param name="dataOwnerID"></param>
        /// <param name="zoneID"></param>
        /// <param name="tagIDs"></param>
        /// <param name="isCurrentZone"></param>
        /// <param name="isDefaultZone"></param>
        /// <returns></returns>
        bool UpdateZone(int dataOwnerID, long zoneID, List<string> tagIDs, bool isCurrentZone, bool isDefaultZone);

        #endregion [Zone]

        #region [Violations]

        /// <summary>
        /// Get all the enable Violations on DataOwnerID
        /// </summary>
        /// <param name="dataOwnerID"></param>
        /// <returns></returns>
        List<string> GetEnabledViolation(int dataOwnerID);

        #endregion [Violations]

        #region [TripPlan]

        /// <summary>
        /// Returns all the Trip Plan available in the cache.
        /// </summary>
        /// <returns>List of KTTripPlanDetails</returns>
        List<KTTripPlanDetails> GetAllTripPlan();

        /// <summary>
        /// Returns all the Trip Plan available in the cache.
        /// </summary>
        /// <returns>List of KTTripPlanDetails</returns>
        List<KTTripPlanDetails> GetAllTripPlan(int dataOwnerID);

        /// <summary>
        /// Returns Trip Plan details for provided Trip plan ID.
        /// </summary>
        /// <param name="dataOwnerID"></param>
        /// <param name="tripPlanID"></param>
        /// <returns></returns>
        KTTripPlanDetails GetTripPlan_ID(int dataOwnerID, int tripPlanID);

        /// <summary>
        /// Returns Trip Plan details for provided ReadPointID.
        /// </summary>
        /// <param name="dataOwnerID"></param>
        /// <param name="readPointID"></param>
        /// <returns></returns>
        KTTripPlanDetails GetTripPlan_ReadPointID(int dataOwnerID, int readPointID);

        /// <summary>
        /// Returns Trip Plan details for provided ReadPointName.
        /// </summary>
        /// <param name="dataOwnerID"></param>
        /// <param name="readPointName"></param>
        /// <returns></returns>
        KTTripPlanDetails GetTripPlan_ReadPointName(int dataOwnerID, string readPointName);

        /// <summary>
        /// Returns Trip Plan details for provided AssetID.
        /// </summary>
        /// <param name="dataOwnerID"></param>
        /// <param name="assetID"></param>
        /// <returns></returns>
        KTTripPlanDetails GetTripPlan_AssetID(int dataOwnerID, long assetID);

        /// <summary>
        /// Returns Trip Plan details for provided Asset SubClass.
        /// </summary>
        /// <param name="dataOwnerID"></param>
        /// <param name="assetSubClass"></param>
        /// <returns></returns>
        KTTripPlanDetails GetTripPlan_AssetSubClass(int dataOwnerID, string assetSubClass);

        #endregion [TripPlan]

        #region [Reader]

        /// <summary>
        /// Returns all the Reader Group Details available in the cache.
        /// </summary>
        /// <param name="dataOwnerID"></param>
        /// <returns></returns>
        List<KTReaderGroupDetails> GetAllReaderGroups(int dataOwnerID);

        /// <summary>
        /// Returns Reader Group Details for provided Reader Group ID
        /// </summary>
        /// <param name="dataOwnerID"></param>
        /// <param name="readerGroupID"></param>
        /// <returns></returns>
        KTReaderGroupDetails GetReaderGroup_ID(int dataOwnerID, int readerGroupID);

        /// <summary>
        /// Returns Reader Group Details for provided Reader Group Name
        /// </summary>
        /// <param name="dataOwnerID"></param>
        /// <param name="readerGroupName"></param>
        /// <returns></returns>
        KTReaderGroupDetails GetReaderGroup_Name(int dataOwnerID, string readerGroupName);

        /// <summary>
        /// Reaturns list of Readers for provided Reader Group ID
        /// </summary>
        /// <param name="dataOwnerID"></param>
        /// <param name="readerGroupID"></param>
        /// <returns></returns>
        List<KTReaderInstanceDetails> GetAllReaders_ReaderGroupID(int dataOwnerID, int readerGroupID);

        /// <summary>
        /// Reaturns list of Readers for provided Reader Group Name
        /// </summary>
        /// <param name="dataOwnerID"></param>
        /// <param name="readerGroupName"></param>
        /// <returns></returns>
        List<KTReaderInstanceDetails> GetAllReaders_ReaderGroupName(int dataOwnerID, string readerGroupName);

        #endregion [Reader]

        #endregion [Methods]

        /// <summary>
        /// Clears the cache and fills it again.
        /// </summary>
        void RefreshCache();

        KTAssetCacheStatus CacheStatus
        {
            get;
        }

        string LastRefreshResult
        {
            get;
        }

        event EventHandler<TripPlanUpdateEventArgs> TripPlanUpdateRefreshed;

        event EventHandler<ReaderGroupEventArgs> ReaderGroupRefreshed;

        event EventHandler<AssetTripEventArgs> AssetTripRefreshed;

    }

    /// <summary>
    /// Asset details
    /// </summary>
    [Serializable]
    public class KTAssetDetails
    {
        #region Attributes

        private bool isActive;
        private long assetId, assetClassId, imageId, defaultZoneID;
        private string name, description, companyAssetId, assetClass, assetClassInstance, assetStatus, defaultZoneName;
        private AssetCategory category;
        private List<TagData> tagDetails = new List<TagData>();
        private Dictionary<string, string> customColumnDetails = new Dictionary<string, string>();
        private int dataOwnerID;

        #endregion Attributes

        #region Constructor

        public KTAssetDetails(bool isActive, long assetId, long assetClassId, long imageId, string name, string description, string companyAssetId,
            string assetClass, string assetClassInstance, string assetStatus, AssetCategory category, List<TagData> tagDetails, Dictionary<string, string> customColumnDetails, int dataOwnerID, long defaultZoneID, string defaultZoneName)
        {
            this.isActive = isActive;
            this.assetId = assetId;
            this.assetClassId = assetClassId;
            this.imageId = imageId;
            this.name = name;
            this.description = description;
            this.companyAssetId = companyAssetId;
            this.assetClass = assetClass;
            this.assetClassInstance = assetClassInstance;
            this.assetStatus = assetStatus;
            this.category = category;
            this.tagDetails = tagDetails;
            this.customColumnDetails = customColumnDetails;
            this.dataOwnerID = dataOwnerID;
            this.defaultZoneID = defaultZoneID;
            this.defaultZoneName = defaultZoneName;
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Returns IsActive
        /// </summary>
        public bool IsActive
        {
            get
            {
                return this.isActive;
            }
        }

        /// <summary>
        /// Returns AssetId
        /// </summary>
        public long AssetId
        {
            get
            {
                return this.assetId;
            }
        }

        public long ImageID
        {
            get
            {
                return this.imageId;
            }
        }

        public long DefaultZoneID
        {
            get
            {
                return this.defaultZoneID;
            }
        }

        public long AssetClassId
        {
            get
            {
                return this.assetClassId;
            }
        }

        /// <summary>
        /// Returns name of the asset.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Returns description of the asset.
        /// </summary>
        public string Description
        {
            get
            {
                return this.description;
            }
        }

        /// <summary>
        /// Returns Company Asset ID.
        /// </summary>
        public string CompanyAssetId
        {
            get
            {
                return this.companyAssetId;
            }
        }

        public string AssetClass
        {
            get
            {
                return this.assetClass;
            }
        }

        public string AssetClassInstance
        {
            get
            {
                return this.assetClassInstance;
            }
        }

        public string AssetStatus
        {
            get
            {
                return this.assetStatus;
            }
        }

        public string DefaultZoneName
        {
            get
            {
                return this.defaultZoneName;
            }
        }

        public AssetCategory Category
        {
            get
            {
                return this.category;
            }
        }

        public List<TagData> TagDetails
        {
            get
            {
                return this.tagDetails;
            }
        }

        public Dictionary<string, string> CustomColumnDetails
        {
            get
            {
                return this.customColumnDetails;
            }
            set
            {
                this.customColumnDetails = value;
            }
        }

        public int DataOwnerID
        {
            get
            {
                return this.dataOwnerID;
            }
        }

        #endregion Properties

        #region Public Methods

        /// <summary>
        /// Returns AssetDetails in string format 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string strZone = string.Empty;
            StringBuilder str = new StringBuilder();

            str.Append("Name:" + this.name + "\r\n");
            str.Append("AssetId:" + this.assetId.ToString() + "\r\n");
            str.Append("Description:" + this.description + "\r\n");
            str.Append("CompanyAssetID:" + this.companyAssetId + "\r\n");
            str.Append("IsActive:" + this.isActive.ToString() + "\r\n");
            str.Append("ImageId:" + this.imageId.ToString() + "\r\n");
            str.Append("Status:" + this.assetStatus.ToString() + "\r\n");
            str.Append("AssetClassInstance:" + this.assetClassInstance.ToString() + "\r\n");
            str.Append("Custom Info:" + "\r\n");
            if (this.customColumnDetails != null)
            {
                foreach (KeyValuePair<string, string> key in this.customColumnDetails)
                {
                    str.Append(key.Key + ": " + key.Value + "\r\n"); ;

                }
            }

            return str.ToString();
        }

        public string ToXml()
        {
            XmlWriter xmlWriter = null;
            StringBuilder xmlStringBuilder = new StringBuilder();
            try
            {
                XmlWriterSettings xmlWritersettings = new XmlWriterSettings();
                xmlWritersettings.Indent = true;
                xmlWriter = XmlWriter.Create(xmlStringBuilder, xmlWritersettings);
                xmlWriter.WriteStartDocument();
                #region Asset
                xmlWriter.WriteStartElement("Asset");

                xmlWriter.WriteStartElement("Name");
                xmlWriter.WriteCData(this.name);
                xmlWriter.WriteEndElement();//"Name"

                xmlWriter.WriteStartElement("AssetId");
                xmlWriter.WriteCData(this.assetId.ToString());
                xmlWriter.WriteEndElement();//"AssetId"

                xmlWriter.WriteStartElement("Description");
                xmlWriter.WriteCData(this.description);
                xmlWriter.WriteEndElement();//"Description"

                xmlWriter.WriteStartElement("CompanyAssetId");
                xmlWriter.WriteCData(this.companyAssetId);
                xmlWriter.WriteEndElement();//"ExternalTagId"

                xmlWriter.WriteStartElement("IsActive");
                xmlWriter.WriteCData(this.isActive.ToString());
                xmlWriter.WriteEndElement();//"IsActive"

                xmlWriter.WriteStartElement("ImageId");
                xmlWriter.WriteCData(this.imageId.ToString());
                xmlWriter.WriteEndElement();//"ImageId"

                xmlWriter.WriteStartElement("Status");
                xmlWriter.WriteCData(this.assetStatus.ToString());
                xmlWriter.WriteEndElement();//"Status"

                xmlWriter.WriteStartElement("CustomFields");

                if (this.customColumnDetails != null)
                {
                    foreach (KeyValuePair<string, string> key in this.customColumnDetails)
                    {
                        xmlWriter.WriteStartElement("CustomField");
                        xmlWriter.WriteAttributeString("FieldName", key.Key);
                        xmlWriter.WriteAttributeString("FieldValue", key.Value);
                        xmlWriter.WriteEndElement();//"CustomField";
                    }
                }

                xmlWriter.WriteEndElement();//"CustomFields";
                xmlWriter.WriteEndElement();//"Asset"
                #endregion Asset
                xmlWriter.WriteEndDocument();
                xmlWriter.Flush();
                xmlWriter.Close();
            }
            catch { }

            return xmlStringBuilder.ToString();
        }

        #endregion Public Methods

        public string GetPropertyValue(string PropertyName)
        {
            switch (PropertyName.ToUpper())
            {
                case "ISACTIVE":
                    return this.isActive.ToString();
                case "NAME":
                    return this.name;
                case "DESCRIPTION":
                    return this.description;
                case "COMPANYASSETID":
                    return this.companyAssetId;
                case "ASSETCLASSNAME":
                    return this.assetClass;
                case "ASSETSTATUS":
                    return this.assetStatus;
                case "ASSETCLASSINSTANCE":
                    return this.assetClassInstance;
            }
            return "";
        }
    }

    /// <summary>
    /// Used to store RFID tag details.
    /// </summary>
    /// 
    [Serializable]
    public class TagData
    {
        #region Attributes
        RFTagType tagType;
        string rfTagID;
        #endregion Attributes

        #region Constructor

        /// <summary>
        /// Initializes an instance of TagData
        /// </summary>
        /// <param name="tagTypeID">Tag type id</param>
        /// <param name="rfTagID">RFTag ID</param>
        public TagData(RFTagType tagType, string rfTagID)
        {
            this.tagType = tagType;
            this.rfTagID = rfTagID;
        }

        #endregion Constructor

        public RFTagType TagType
        {
            get
            {
                return tagType;
            }
        }

        public String TagID
        {
            get
            {
                return rfTagID;
            }
        }
    }

    /// <summary>
    /// Asset class details
    /// </summary>
    [Serializable]
    public class KTAssetClassDetails
    {
        #region Attributes

        private string id, name, externalTagIdFormat;
        private List<ExternalTagIdField> externalTagIdFields;
        private string categoryID, description, categoryName, seperator;
        private int dataOwnerID;

        #endregion Attributes

        #region Constructor
        /// <summary>
        /// Initializes an instance of KTAssetClassDetails
        /// </summary>
        /// <param name="id">Asset Class Master ID</param>
        /// <param name="name">Asset Class name</param>
        /// <param name="externalTagIdFormat">Format of the external tag id.</param>
        /// <param name="externalTagIdFields">Fields in the external tag id.</param>
        public KTAssetClassDetails(string id, string name, string externalTagIdFormat, List<ExternalTagIdField> externalTagIdFields, string categoryID, int dataOwnerID)
        {
            this.id = id;
            this.name = name;
            this.externalTagIdFormat = externalTagIdFormat;
            this.externalTagIdFields = externalTagIdFields;
            this.categoryID = categoryID;
            this.dataOwnerID = dataOwnerID;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="externalTagIdFormat"></param>
        /// <param name="externalTagIdFields"></param>
        /// <param name="categoryID"></param>
        /// <param name="description"></param>
        public KTAssetClassDetails(string id, string name, string externalTagIdFormat, List<ExternalTagIdField> externalTagIdFields, string categoryID, string description, string categoryName, string seperator, int dataOwnerID)
        {
            this.id = id;
            this.name = name;
            this.externalTagIdFormat = externalTagIdFormat;
            this.externalTagIdFields = externalTagIdFields;
            this.categoryID = categoryID;
            this.description = description;
            this.categoryName = categoryName;
            this.seperator = seperator;
            this.dataOwnerID = dataOwnerID;
        }

        /// <summary>
        /// Returns all the details about the KTAssetClassDetails in sting format
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.Append("ExternalTagIdFormat:" + this.externalTagIdFormat + "\r\n");

            str.Append("ExternalTagIdFields:" + Format(this.externalTagIdFields) + "\r\n");

            return str.ToString();
        }
        #endregion Constructor

        #region Properties

        /// <summary>
        /// Returns format of the external tag id.
        /// </summary>
        public string ExternalTagIdFormat
        {
            get
            {
                return this.externalTagIdFormat;
            }
        }

        /// <summary>
        /// Fields in the external tag id.
        /// </summary>
        public List<ExternalTagIdField> ExternalTagIdFields
        {
            get
            {
                return this.externalTagIdFields;
            }
        }

        /// <summary>
        /// Name of Asset Class
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ID
        {
            get
            {
                return this.id;
            }
        }

        /// <summary>
        /// Category ID
        /// </summary>
        public string CategoryID
        {
            get
            {
                return this.categoryID;
            }
        }

        /// <summary>
        /// Description
        /// </summary>
        public string Description
        {
            get
            {
                return this.description;
            }
        }

        /// <summary>
        /// Category Name
        /// </summary>
        public string CategoryName
        {
            get
            {
                return this.categoryName;
            }
        }

        /// <summary>
        /// Seperator
        /// </summary>
        public string Seperator
        {
            get
            {
                return this.seperator;
            }
        }

        public int DataOwnerID
        {
            get
            {
                return this.dataOwnerID;
            }
        }

        #endregion Properties

        internal string Format(List<ExternalTagIdField> list)
        {
            string commaSep = string.Empty;

            foreach (ExternalTagIdField str in list)
            {
                if (str.ExtTagIdFieldName != "")
                {
                    commaSep += str.ExtTagIdFieldName + ",";
                }
            }
            commaSep = commaSep.TrimEnd(new char[] { ',' });
            return commaSep;
        }

        internal void ToXml(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("ExternalTagIdFormat");
            xmlWriter.WriteCData(this.externalTagIdFormat);
            xmlWriter.WriteEndElement();//"ExternalTagIdFormat"

            xmlWriter.WriteStartElement("ExternalTagIdFields");
            foreach (ExternalTagIdField externalTagIdField in this.externalTagIdFields)
            {
                if (externalTagIdField.ExtTagIdFieldName.Equals(string.Empty))
                    continue;
                xmlWriter.WriteStartElement("ExternalTagIdField");
                xmlWriter.WriteCData(externalTagIdField.ExtTagIdFieldName);
                xmlWriter.WriteEndElement();//"ExternalTagIdField"
            }
            xmlWriter.WriteEndElement();//"ExternalTagIdFields"
        }
    }

    /// <summary>
    /// ExternalTagIdFields for Asset Class details
    /// </summary>
    [Serializable]
    public class ExternalTagIdField
    {
        #region Attributes
        private string extTagIdFieldName = string.Empty;
        private string dataType = string.Empty;
        #endregion Attributes

        #region Constructor
        /// <summary>
        /// Initializes an instance of ExternalTagIdField
        /// </summary>
        /// <param name="tagIdFieldname">name of tag id field</param>
        /// <param name="dataType">datatype of the field</param>
        public ExternalTagIdField(string tagIdFieldname, string dataType)
        {
            this.extTagIdFieldName = tagIdFieldname;
            this.dataType = dataType;
        }

        #endregion Constructor

        #region Properties
        /// <summary>
        /// Name of external tag id field
        /// </summary>
        public string ExtTagIdFieldName
        {
            get { return extTagIdFieldName; }
        }

        /// <summary>
        /// Datatype for the field
        /// </summary>
        public string DataType
        {
            get { return dataType; }
        }

        #endregion Properties

        /// <summary>
        /// Returns all the details about the ExternalTagIdField in sting format
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("ExtTagIdFieldName:" + this.extTagIdFieldName + " \r\n");
            sb.Append("DataType:" + this.dataType + " \r\n");

            return sb.ToString();
        }
    }

    [Serializable]
    public class KTAssetClassInstance
    {
        long assetClassInstanceID = 0;
        string assetClassInstance = string.Empty;
        long assetClassID = 0;
        long imageId = 0;
        int dataOwnerID = 0;

        public KTAssetClassInstance(long assetClassInstanceID, string assetClassInstance, long assetClassID, long imageId, int dataOwnerID)
        {
            this.assetClassInstanceID = assetClassInstanceID;
            this.assetClassInstance = assetClassInstance;
            this.assetClassID = assetClassID;
            this.imageId = imageId;
            this.dataOwnerID = dataOwnerID;
        }

        public long AssetClassInstanceID
        {
            get { return this.assetClassInstanceID; }
        }

        public string AssetClassInstance
        {
            get { return this.assetClassInstance; }
        }

        public long AssetClassID
        {
            get { return this.assetClassID; }
        }

        public long ImageID
        {
            get { return this.imageId; }
        }

        public int DataOwnerID
        {
            get
            {
                return this.dataOwnerID;
            }
        }
    }

    [Serializable]
    public class KTTripPlanDetails
    {
        int tripPlanID = 0;
        int dataOwnerID = 0;
        string tripPlanName = string.Empty;
        string remarks = string.Empty;
        List<ReadPoint> readPointDetails = new List<ReadPoint>();
        List<long> overrideAssetIDs = new List<long>();
        List<string> assetSubClassList = new List<string>();

        public KTTripPlanDetails(int tripPlanID, int dataOwnerID, string tripPlanName, string remarks, List<ReadPoint> readPointDetails, List<long> overrideAssetIDs, List<string> assetSubClassList)
        {
            this.tripPlanID = tripPlanID;
            this.dataOwnerID = dataOwnerID;
            this.tripPlanName = tripPlanName;
            this.remarks = remarks;
            this.readPointDetails = readPointDetails;
            this.overrideAssetIDs = overrideAssetIDs;
            this.assetSubClassList = assetSubClassList;
        }

        public int TripPlanID
        {
            get { return this.tripPlanID; }
        }

        public int DataOwnerID
        {
            get { return this.dataOwnerID; }
        }

        public string TripPlanName
        {
            get { return this.tripPlanName; }
        }

        public string Remarks
        {
            get { return this.remarks; }
        }

        public List<ReadPoint> ReadPointDetails
        {
            get { return this.readPointDetails; }
            set { this.readPointDetails = value; }
        }

        public List<long> OverrideAssetIDs
        {
            get { return this.overrideAssetIDs; }
            set { this.overrideAssetIDs = value; }
        }

        public List<string> AssetSubClassList
        {
            get { return this.assetSubClassList; }
            set { this.assetSubClassList = value; }
        }
    }

    [Serializable]
    public class ReadPoint
    {
        string rfType = string.Empty;
        string rfResource = string.Empty;
        string readPointName = string.Empty;
        int readPointId = 0;
        string tripState = string.Empty;
        string readPointCategory = string.Empty;
        string remarks = string.Empty;
        int sequenceNo = 0;
        int maxTimeToReach = 0;
        int maxTimeToSpend = 0;

        public ReadPoint(string rfType, string rfResource, string readPointName, int readPointId, string tripState, string readPointCategory, string remarks, int sequenceNo, int maxTimeToReach, int maxTimeToSpend)
        {
            this.rfType = rfType;
            this.rfResource = rfResource;
            this.readPointName = readPointName;
            this.readPointId = readPointId;
            this.tripState = tripState;
            this.readPointCategory = readPointCategory;
            this.remarks = remarks;
            this.sequenceNo = sequenceNo;
            this.maxTimeToReach = maxTimeToReach;
            this.maxTimeToSpend = maxTimeToSpend;
        }

        public string RFType
        {
            get { return this.rfType; }
        }

        public string RFResource
        {
            get { return this.rfResource; }
        }

        public string ReadPointName
        {
            get { return this.readPointName; }
        }

        public int ReadPointId
        {
            get { return this.readPointId; }
        }

        public string TripState
        {
            get { return this.tripState; }
        }

        public string ReadPointCategory
        {
            get { return this.readPointCategory; }
        }

        public string Remarks
        {
            get { return this.remarks; }
        }

        public int SequenceNo
        {
            get { return this.sequenceNo; }
        }

        public int MaxTimeToReach
        {
            get { return this.maxTimeToReach; }
        }

        public int MaxTimeToSpend
        {
            get { return this.maxTimeToSpend; }
        }
    }

    [Serializable]
    public class KTReaderGroupDetails
    {
        int readerGroupID = 0;
        int dataOwnerID = 0;
        string groupName = string.Empty;
        List<KTReaderInstanceDetails> readerInstanceList = new List<KTReaderInstanceDetails>();

        public KTReaderGroupDetails(int readerGroupID, int dataOwnerID, string groupName, List<KTReaderInstanceDetails> readerInstanceList)
        {
            this.readerGroupID = readerGroupID;
            this.dataOwnerID = dataOwnerID;
            this.groupName = groupName;
            this.readerInstanceList = readerInstanceList;
        }

        public int ReaderGroupID
        {
            get { return this.readerGroupID; }
        }

        public int DataOwnerID
        {
            get { return this.dataOwnerID; }
        }

        public string GroupName
        {
            get { return this.groupName; }
        }

        public List<KTReaderInstanceDetails> ReaderInstanceList
        {
            get { return this.readerInstanceList; }
        }
    }

    [Serializable]
    public class KTReaderInstanceDetails
    {
        int readerInstanceID = 0;
        string instanceName = string.Empty;
        string readerName = string.Empty;

        public KTReaderInstanceDetails(int readerInstanceID, string instanceName, string readerName)
        {
            this.readerInstanceID = readerInstanceID;
            this.instanceName = instanceName;
            this.readerName = readerName;
        }

        public int ReaderInstanceID
        {
            get { return this.readerInstanceID; }
        }

        public string InstanceName
        {
            get { return this.instanceName; }
        }

        public string ReaderName
        {
            get { return this.readerName; }
        }
    }

    /// <summary>
    /// Status of the Asset Cache whether the cache is refreshed, refreshing or failed to refresh.
    /// </summary>
    public enum KTAssetCacheStatus
    {
        /// <summary>
        /// cache has refreshed successfully
        /// </summary>
        Ready,

        /// <summary>
        /// cache failed to refresh
        /// </summary>
        Failed,

        /// <summary>
        /// cache is refreshing.
        /// </summary>
        Refreshing
    }
}