using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace KTone.Core.KTIRFID

{

    //public enum RFTagType
    //{
    //    Active,
    //    Passive,
    //    Barcode,
    //}

    public enum PrintOperation
    {
        Insert,
        Update,
        Duplicate,
    }

    public enum LocationPrintOperation
    {
        Update,
        Duplicate,
    }

    public enum ItemType
    {
        Printed,
        UnPrinted,
        All,
    }




    public enum SDCRFTagType
    {
        Active,
        Passive,
        Barcode,
    }

    /// <summary>
    /// Company details
    /// </summary>
    [Serializable]
    [DataContract]
    public class KTCompanyDetails
    {
        #region Attributes
        private bool isEPC;
        private int companyID;
        private string companyName = string.Empty, companyPrefix = string.Empty;
        private Dictionary<string, string> customColumnDetails = new Dictionary<string, string>();
        private int dataOwnerID, createdBy, updatedBy;
        private DateTime createdDate, updatedDate;
        #endregion Attributes

        #region Constructor

        public KTCompanyDetails(bool isEPC, int companyID, string companyName, string companyPrefix, Dictionary<string, string> customColumnDetails, int dataOwnerID, int createdBy, int updatedBy, DateTime createdDate, DateTime updatedDate)
        {
            this.isEPC = isEPC;
            this.companyID = companyID;
            this.companyName = companyName;
            this.companyPrefix = companyPrefix;
            this.customColumnDetails = customColumnDetails;
            this.dataOwnerID = dataOwnerID;
            this.createdBy = createdBy;
            this.createdDate = createdDate;
            this.updatedBy = updatedBy;
            this.updatedDate = updatedDate;
        }
        #endregion Constructor

        #region Properties

        /// <summary>
        /// Returns IsEPC
        /// </summary>
        /// 
        [DataMember]
        public bool IsEPC
        {
            get
            {
                return this.isEPC;
            }
            set { isEPC = value; }
        }
        /// <summary>
        /// Returns Companyid
        /// </summary>
        [DataMember]
        public int CompanyID
        //public long CompanyID
       
        {
            get
            {
                return this.companyID;
            }
            set { companyID = value; }
        }
        /// <summary>
        /// Returns Companyname
        /// </summary>
       [DataMember]
        public string CompanyName
        {
            get
            {
                return this.companyName;
            }
            set {
               
                    companyName = value;
               
            }
        }
        /// <summary>
        /// Returns Companyname
        /// </summary>
        [DataMember]
        public string CompanyPrefix
        {
            get
            {
                return this.companyPrefix;
            }
            set 
            {
                
                    companyPrefix = value;
               
            }
        }
         [DataMember]
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
        /// <summary>
        /// Returns DataOwnerID
        /// </summary>
         [DataMember]
        public int DataOwnerID
        {
            get
            {
                return this.dataOwnerID;
            }
            set { dataOwnerID = value; }
        }
         [DataMember]
        public int CreatedBy
        {
            get
            {
                return this.createdBy;
            }
            set { createdBy = value; }
        }
         [DataMember]
        public int UpdatedBy
        {
            get
            {
                return this.updatedBy;
            }
            set { updatedBy = value; }
        }
         [DataMember]
        public DateTime UpdatedDate
        {
            get
            {
                return this.updatedDate;
            }
            set { updatedDate = value; }
        }
         [DataMember]
        public DateTime CreatedDate
        {
            get
            {
                return this.createdDate;
            }
            set { createdDate = value; }
        }
        #endregion Properties

        public string GetPropertyValue(string PropertyName)
        {
            switch (PropertyName.ToUpper())
            {
                case "ISEPC":
                    return this.isEPC.ToString();
                case "COMPANYNAME":
                    return this.companyName;
                case "COMPANYPREFIX":
                    return this.companyPrefix;
                case "COMPANYID":
                    return this.companyID.ToString();
                case "CREATEDBY":
                    return this.createdBy.ToString();
                case "CREATEDDATE":
                    return this.createdDate.ToString();
                case "UPDATEDBY":
                    return this.updatedBy.ToString();
                case "UPDATEDDATE":
                    return this.updatedDate.ToString();
            }
            return "";
        }



        /// <summary>
        /// Returns CompanyDetails in string format 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string strZone = string.Empty;
            StringBuilder str = new StringBuilder();

            str.Append("IsEPC:" + this.isEPC.ToString() + "\r\n");
            str.Append("CompanyID:" + this.companyID.ToString() + "\r\n");
            if (!string.IsNullOrEmpty(companyName))
            str.Append("CompanyName:" + this.companyName.ToString() + "\r\n");
            if (!string.IsNullOrEmpty(companyPrefix))
            str.Append("CompanyPrefix:" + this.companyPrefix.ToString() + "\r\n");
            str.Append("DataOwnerID:" + this.dataOwnerID.ToString() + "\r\n");
            str.Append("CreatedBy:" + this.createdBy.ToString() + "\r\n");
            str.Append("UpdatedBy:" + this.updatedBy.ToString() + "\r\n");
            str.Append("CreatedDate:" + this.createdDate.ToString() + "\r\n");
            str.Append("UpdatedDate:" + this.updatedDate.ToString() + "\r\n");
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



    }

    /// <summary>
    /// Used to store Product details.
    /// </summary>
    /// 
    [Serializable]
    [DataContract]
    public class KTProductDetails
    {
        #region Attributes 
        private long productID;
        private string productName = string.Empty, productPrefix = string.Empty;
        private Dictionary<string, string> customColumnDetails = new Dictionary<string, string>();
        private int dataOwnerID, createdBy, updatedBy, companyID;
        private DateTime createdDate, updatedDate;
        #endregion Attributes

        #region Constructor
        public KTProductDetails( int companyID, long productID, string productName, string productPrefix, Dictionary<string, string> customColumnDetails, int dataOwnerID, int createdBy, int updatedBy, DateTime createdDate, DateTime updatedDate)
        {
            this.companyID = companyID;
            this.productID = productID;
            this.productName = productName;
            this.productPrefix = productPrefix;
            this.customColumnDetails = customColumnDetails;
            this.dataOwnerID = dataOwnerID;
            this.createdBy = createdBy;
            this.createdDate = createdDate;
            this.updatedBy = updatedBy;
            this.updatedDate = updatedDate;
        }
        #endregion Constructor

        #region Properties 
        [DataMember]
        public int CompanyID
        {
            get
            {
                return this.companyID;
            }
            set { companyID = value; }
        }
        [DataMember]
        public long ProductID
        {
            get
            {
                return this.productID;
            }
            set { productID = value; }
        }
        [DataMember]
        public string ProductName
        {
            get
            {
                return this.productName;
            }
            set 
            {
                
                    productName = value;
               
            }
        }
        [DataMember]
        public string ProductPrefix
        {
            get
            {
                return this.productPrefix;
            }
            set
            {

                productPrefix = value;

            }
        }
        [DataMember]
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
        [DataMember]
        public int DataOwnerID
        {
            get
            {
                return this.dataOwnerID;
            }
            set { dataOwnerID = value; }
        }
        [DataMember]
        public int CreatedBy
        {
            get
            {
                return this.createdBy;
            }
            set { createdBy = value; }
        }
        [DataMember]
        public int UpdatedBy
        {
            get
            {
                return this.updatedBy;
            }
            set { updatedBy = value; }
        }
        [DataMember]
        public DateTime UpdatedDate
        {
            get
            {
                return this.updatedDate;
            }
            set { updatedDate = value; }
        }
        [DataMember]
        public DateTime CreatedDate
        {
            get
            {
                return this.createdDate;
            }
            set { createdDate = value; }
        }
        #endregion Properties

        /// <summary>
        /// Returns ProductDetails in string format 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string strZone = string.Empty;
            StringBuilder str = new StringBuilder();

            str.Append("CompanyID:" + this.companyID.ToString() + "\r\n");
            str.Append("ProductID:" + this.productID.ToString() + "\r\n");
            if (!string.IsNullOrEmpty(productName))
            str.Append("ProductName:" + this.productName.ToString() + "\r\n");
            if (!string.IsNullOrEmpty(productPrefix))
            str.Append("ProductPrefix:" + this.productPrefix.ToString() + "\r\n");
            str.Append("DataOwnerID:" + this.dataOwnerID.ToString() + "\r\n");
            str.Append("CreatedBy:" + this.createdBy.ToString() + "\r\n");
            str.Append("UpdatedBy:" + this.updatedBy.ToString() + "\r\n");
            str.Append("CreatedDate:" + this.createdDate.ToString() + "\r\n");
            str.Append("UpdatedDate:" + this.updatedDate.ToString() + "\r\n");
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
    }



    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [DataContract]
    public class KTBinQuantity
    {

        #region Attributes
            private string customerUniqueID = string.Empty;
            long quantity = 0;
        
        #endregion

            public KTBinQuantity(string customerUniqueID, long quantity)
            {
                this.customerUniqueID = customerUniqueID;
                this.quantity = quantity;
            }


        #region Properties           
            /// <summary>
            /// Returns BinTape
            /// </summary>
            [DataMember]
            public string BinTape
            {
                get
                {
                    return this.customerUniqueID;
                }
                set { customerUniqueID = value; }
            }

        /// <summary>
        /// Return Quantity
        /// </summary>
            [DataMember]
            public long Quantity
            {
                get
                {
                    return this.quantity;
                }
                set { quantity = value; }
            }

        #endregion


    }


    /// <summary>
    /// Used for ItemDetails.
    /// </summary>
    /// 
    [Serializable]
    [DataContract]
    public class KTItemDetails
    {
        #region Attributes
        private long iD, sku_ID;
        private string status = string.Empty, customerUniqueID = string.Empty, itemstatus = string.Empty,productname=string.Empty,productsku=string.Empty;
        private Dictionary<string, string> customColumnDetails = new Dictionary<string, string>();
        private int createdBy, updatedBy,dataOwnerID;
        private DateTime createdDate, updatedDate;//Same for CreatedOn and UpdatedOn
        /* Attributes of ItemDetails */
        private DateTime lastSeenTime;
        private int lastSeenLocation;
        private string locationName;
        /* Attributes of ItemMovement/Details */
        private DateTime timeStamp;
        private int movementID, movementDetailsID, locationID;
        /* Attributes of ItemRFDetails */
        // private DateTime updatedOn,CreatedOn;
        private List<SDCTagData> tagDetails = new List<SDCTagData>();
        //private int  tagType;
        private bool isActive;
        private string comments=string.Empty;
       // private string   rfTagID;


        #endregion Attributes

        #region Constructor

        //Constructor for ItemMaster Fields
        public KTItemDetails()
        { 
        
        }

        public KTItemDetails(int dataOwnerID, long iD, long sku_ID, string status, string customerUniqueID,
            Dictionary<string, string> customColumnDetails, int createdBy, int updatedBy,
            DateTime createdDate, DateTime updatedDate, string locationName)
        {
            this.iD = iD;
            this.sku_ID = sku_ID;
            this.status = status;
            this.customerUniqueID = customerUniqueID;
            this.customColumnDetails = customColumnDetails;
            this.createdBy = createdBy;
            this.createdDate = createdDate;
            this.updatedBy = updatedBy;
            this.updatedDate = updatedDate;
            this.dataOwnerID = dataOwnerID;
            this.locationName = locationName; 
        }

           public KTItemDetails(int dataOwnerID,long iD, long sku_ID, string status, string customerUniqueID,
            Dictionary<string, string> customColumnDetails, int createdBy, int updatedBy,
            DateTime createdDate, DateTime updatedDate)
        {
            this.iD = iD;
            this.sku_ID = sku_ID;
            this.status = status;
            this.customerUniqueID = customerUniqueID;
            this.customColumnDetails = customColumnDetails;
            this.createdBy = createdBy;
            this.createdDate = createdDate;
            this.updatedBy = updatedBy;
            this.updatedDate = updatedDate;
            this.dataOwnerID = dataOwnerID;
            
        }
        public KTItemDetails(long iD, DateTime lastSeenTime, int lastSeenLocation, string Itemstatus)
        {
            this.iD = iD;
            this.lastSeenTime = lastSeenTime;
            this.lastSeenLocation = lastSeenLocation;
            this.itemstatus = Itemstatus;
        }
        public KTItemDetails(long iD, int movementID, int locationID, DateTime timeStamp)
        {
            this.iD = iD;
            this.movementID = movementID;
            this.locationID = locationID;
            this.timeStamp = timeStamp;
        }
        public KTItemDetails(int movementDetailsID, int locationID, DateTime timeStamp, long iD)
        {
            this.movementDetailsID = movementDetailsID;
            this.locationID = locationID;
            this.timeStamp = timeStamp;
            this.iD = iD;
        }
        public KTItemDetails(long iD, bool isActive, string comments, int createdBy,
            int updatedBy, DateTime createdDate, DateTime updatedDate)
        {
            this.iD = iD;
           // this.rfTagID = rfTagID;
           // this.tagType = tagType;
            this.isActive = isActive;
            this.comments = comments;
            this.createdBy = createdBy;
            this.createdDate = createdDate;
            this.updatedBy = updatedBy;
            this.updatedDate = updatedDate;
        }

        #endregion Constructor

        #region Properties
        /// <summary>
        /// Returns ID
        /// </summary>
        [DataMember]
        public long ID
        {
            get
            {
                return this.iD;
            }
            set { iD = value; }
        }
        [DataMember]
        public long SKU_ID
        {
            get
            {
                return this.sku_ID;
            }
            set { sku_ID = value; }
        }
        [DataMember]
        public string Status
        {
            get
            {
                return this.status;
            }
            set 
            {
               
                    status = value;
              
            
            }
        }

        [DataMember]
        public string LocationName
        {
            get
            {
                return this.locationName;
            }
            set
            {

                locationName = value;


            }
        }

        [DataMember]
        public string CustomerUniqueID
        {
            get
            {
                return this.customerUniqueID;
            }
            set {
               
                    customerUniqueID = value;
              
            }
        }
        [DataMember]
        public List<SDCTagData> TagDetails
        {
            get
            {
                return this.tagDetails;
            }
            set { tagDetails = value;}
        }

        [DataMember]
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
        [DataMember]
        public int CreatedBy
        {
            get
            {
                return this.createdBy;
            }
            set { createdBy = value; }
        }
        [DataMember]
        public int UpdatedBy
        {
            get
            {
                return this.updatedBy;
            }
            set { updatedBy = value; }
        }
  [DataMember]

          public int DataOwnerID
        {
            get
            {
                return this.dataOwnerID;
            }
            set { dataOwnerID = value; }
        }
  [DataMember]
        public DateTime UpdatedDate
        {
            get
            {
                return this.updatedDate;
            }
            set { updatedDate = value; }
        }
        [DataMember]
        public DateTime CreatedDate
        {
            get
            {
                return this.createdDate;
            }
            set { createdDate = value; }
        }
        [DataMember]
        public DateTime LastSeenTime
        {
            get
            {
                return this.lastSeenTime;
            }
            set { lastSeenTime = value; }
        }
        [DataMember]
        public DateTime TimeStamp
        {
            get
            {
                return this.timeStamp;
            }
            set { timeStamp = value; }
        }
        [DataMember]
        public int LastSeenLocation
        {
            get
            {
                return this.lastSeenLocation;
            }
            set { lastSeenLocation = value; }
        }
        [DataMember]
        public int MovementID
        {
            get
            {
                return this.movementID;
            }
            set { movementID = value; }
        }
        [DataMember]
        public int MovementDetailsID
        {
            get
            {
                return this.movementDetailsID;
            }
            set { movementDetailsID = value; }
        }
        //[DataMember]
        //public string RFTagID
        //{
        //    get
        //    {
        //        return this.rfTagID;
        //    }
        //    set { rfTagID = value; }
        //}
        //[DataMember]
        //public int TagType
        //{
        //    get
        //    {
        //        return this.tagType;
        //    }
        //    set { tagType = value; }
        //}
        [DataMember]
        public bool IsActive
        {
            get
            {
                return this.isActive;
            }
            set { isActive = value; }
        }
        [DataMember]
        public string Comments
        {
            get
            {
                return this.comments;
            }
            set {
               
                    comments = value;
                
                }
        }
        [DataMember]
        public string ItemStatus
        {
            get
            {
                return this.itemstatus;
            }
            set {
                
                    itemstatus = value;
                
            }
        }
        [DataMember]
        public string ProductName
        {
            get
            {
                return this.productname;
            }
            set
            {
                productname = value;
            }
        }
        [DataMember]
        public string ProductSKU
        {
            get
            {
                return this.productsku;
            }
            set
            {
                productsku = value;
            }
        }
        #endregion Properties

        /// <summary>
        /// Returns ItemDetails in string format 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string strZone = string.Empty;
            StringBuilder str = new StringBuilder();
            str.Append("ID:" + this.iD.ToString() + "\r\n");
            str.Append("SKU_ID:" + this.sku_ID.ToString() + "\r\n");
            if (!string.IsNullOrEmpty(status))
            str.Append("Status:" + this.status.ToString() + "\r\n");
            if (!string.IsNullOrEmpty(customerUniqueID))
            str.Append("CustomerUniqueID:" + this.customerUniqueID.ToString() + "\r\n");
            str.Append("DataOwnerID:" + this.dataOwnerID.ToString() + "\r\n");
            str.Append("CreatedBy:" + this.createdBy.ToString() + "\r\n");
            str.Append("UpdatedBy:" + this.updatedBy.ToString() + "\r\n");
            str.Append("CreatedDate:" + this.createdDate.ToString() + "\r\n");
            str.Append("UpdatedDate:" + this.updatedDate.ToString() + "\r\n");
            str.Append("LastSeenTime:" + this.lastSeenTime.ToString() + "\r\n");
            str.Append("TimeStamp:" + this.timeStamp.ToString() + "\r\n");
            str.Append("LastSeenLocation:" + this.lastSeenLocation.ToString() + "\r\n");
            str.Append("MovementID:" + this.movementID.ToString() + "\r\n");
            str.Append("MovementDetailsID:" + this.movementDetailsID.ToString() + "\r\n");
            str.Append("TagDetails: \r\n");
            if (this.TagDetails != null)
            {
                foreach (SDCTagData key in this.TagDetails)
                {
                    str.Append(key.TagID + "\r\n"); ;

                }
            }

            str.Append("IsActive:" + this.isActive.ToString() + "\r\n");
            if(!string.IsNullOrEmpty (comments))
            str.Append("Comments:" + this.comments.ToString() + "\r\n");
            str.Append("ProductName:" + this.productname.ToString() + "\r\n");
            str.Append("ProductSKU:" + this.productsku.ToString() + "\r\n");
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
    }

    /// <summary>
    /// Used to store RFID tag details.
    /// </summary>
    /// 
    [Serializable]
    public class SDCTagData
    {
        #region Attributes
        int tagType;
        string rfTagID;
        #endregion Attributes

        #region Constructor

        /// <summary>
        /// Initializes an instance of TagData
        /// </summary>
        /// <param name="tagTypeID">Tag type id</param>
        /// <param name="rfTagID">RFTag ID</param>
        public SDCTagData(int tagType, string rfTagID)
        {
            this.tagType = tagType;
            this.rfTagID = rfTagID;
        }

        #endregion Constructor

        public int TagType
        {
            get
            {
                return tagType;
            }
            set
            { tagType = value; }
        }

        public String TagID
        {
            get
            {
                return rfTagID;
            }
            set {
               
                    rfTagID = value;
               
            }
        }
    }


    /// <summary>
    /// Class for Location.
    /// </summary>
    /// 
    [Serializable]
    [DataContract]
    public class KTLocationDetails
    {
        #region Attributes
        private bool isActive;
        private int locationID, categoryId, parentLocationId;
        private int dataOwnerID;
        private string locationName = string.Empty, description = string.Empty, rfResource = string.Empty, rfValue = string.Empty, categoryName = string.Empty, stencilData = string.Empty, locationzone = string.Empty;
        byte[] locationimage;
        private DateTime modifiedDate, createdDate;
        #endregion Attributes

        #region Constructor
        public KTLocationDetails(bool isActive, int locationID, int categoryId, string categoryName, int parentLocationId, string locationName, string description, string rfResource, string rfValue, DateTime createdDate, DateTime modifiedDate, int dataownerId, string stencilData, string locationzone, byte[] locationimage)
        {
            this.isActive = isActive;
            this.locationID = locationID;
            this.categoryId = categoryId;
            this.parentLocationId = parentLocationId;
            this.locationName = locationName;
            this.description = description;
            this.rfResource = rfResource;
            this.rfValue = rfValue;
            this.modifiedDate = modifiedDate;
            this.createdDate = createdDate;
            this.dataOwnerID = dataownerId;
            this.stencilData = stencilData;
            this.locationzone = locationzone;
            this.locationimage = locationimage;
            this.categoryName = categoryName;
        }

        public KTLocationDetails(bool isActive, int locationID, int categoryId, int parentLocationId, string locationName, string description, string rfResource, string rfValue, DateTime createdDate, DateTime modifiedDate, int dataownerId, string stencilData, string locationzone, byte[] locationimage)
        {
            this.isActive = isActive;
            this.locationID = locationID;
            this.categoryId = categoryId;
            this.parentLocationId = parentLocationId;
            this.locationName = locationName;
            this.description = description;
            this.rfResource = rfResource;
            this.rfValue = rfValue;
            this.modifiedDate = modifiedDate;
            this.createdDate = createdDate;
            this.dataOwnerID = dataownerId;
            this.stencilData = stencilData;
            this.locationzone = locationzone;
            this.locationimage = locationimage;
        }

        #endregion Constructor

        #region Properties
        [DataMember]
        public bool IsActive
        {
            get
            {
                return this.isActive;
            }
            set
            {
                isActive = value;
            }
        }
        [DataMember]
        public int LocationID
        {
            get
            {
                return this.locationID;
            }
            set
            {
                locationID = value;
            }
        }
        [DataMember]
        public int CategoryID
        {
            get
            {
                return this.categoryId;
            }
            set
            {
                categoryId = value;
            }
        }
        [DataMember]
        public int ParentLocationId
        {
            get
            {
                return this.parentLocationId;
            }
            set
            {
                parentLocationId = value;
            }
        }
        [DataMember]
        public string LocationName
        {
            get
            {
                return this.locationName;
            }
            set
            {

                locationName = value;

            }
        }

        [DataMember]
        public string CategoryName
        {
            get
            {
                return this.categoryName;
            }
            set
            {

                categoryName = value;

            }
        }
        [DataMember]
        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {

                description = value;

            }
        }
        [DataMember]
        public string RFResource
        {
            get
            {
                return this.rfResource;
            }
            set
            {

                rfResource = value;

            }
        }
        [DataMember]
        public string RFValue
        {
            get
            {
                return this.rfValue;
            }
            set
            {

                rfValue = value;

            }
        }
        [DataMember]
        public DateTime ModifiedDate
        {
            get
            {
                return this.modifiedDate;
            }
            set
            {
                modifiedDate = value;
            }
        }
        [DataMember]
        public DateTime CreatedDate
        {
            get
            {
                return this.createdDate;
            }
            set
            {
                createdDate = value;
            }
        }
        [DataMember]
        public int DataOwnerID
        {
            get
            {
                return this.dataOwnerID;
            }
            set
            {
                dataOwnerID = value;
            }
        }

        [DataMember]
        public string LocationZone
        {
            get
            {
                return this.locationzone;
            }
            set { locationzone = value; }
        }

        [DataMember]
        public string StencilData
        {
            get
            {
                return this.stencilData;
            }
            set { stencilData = value; }
        }

        [DataMember]
        public byte[] LocationImage
        {
            get
            {
                return this.locationimage;
            }
            set { locationimage = value; }
        }
        #endregion Properties

        /// <summary>
        /// Returns LocationDetails in string format 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string strZone = string.Empty;
            StringBuilder str = new StringBuilder();

            str.Append("IsActive:" + this.isActive.ToString() + "\r\n");
            str.Append("LocationID:" + this.locationID.ToString() + "\r\n");
            str.Append("CategoryID:" + this.categoryId.ToString() + "\r\n");
            str.Append("ParentLocationId:" + this.parentLocationId.ToString() + "\r\n");
            if (!string.IsNullOrEmpty(locationName))
                str.Append("LocationName:" + this.locationName.ToString() + "\r\n");
            if (!string.IsNullOrEmpty(description))
                str.Append("Description:" + this.description.ToString() + "\r\n");
            if (!string.IsNullOrEmpty(rfResource))
                str.Append("RFResource:" + this.rfResource.ToString() + "\r\n");
            if (!string.IsNullOrEmpty(rfValue))
                str.Append("RFValue:" + this.rfValue.ToString() + "\r\n");
            str.Append("ModifiedDate:" + this.modifiedDate.ToString() + "\r\n");
            str.Append("CreatedDate:" + this.createdDate.ToString() + "\r\n");
            str.Append("DataOwnerID:" + this.dataOwnerID.ToString() + "\r\n");
            str.Append("CategoryName:" + this.categoryName.ToString() + "\r\n");
            str.Append("StencilData:" + this.stencilData.ToString() + "\r\n");
            str.Append("LocationZone:" + this.locationzone.ToString() + "\r\n");
            str.Append("LocationImage:" + this.locationimage.ToString() + "\r\n");
            str.Append("Custom Info:" + "\r\n");


            return str.ToString();
        }
    }


    /// <summary>
    /// Class for SKUDetails.
    /// </summary>
    /// 
    [Serializable]
    [DataContract]
    public class KTSKUDetails
    {
        #region Attributes
        private long sku_ID, productID;
        private string productSKU = string.Empty, skuDescription = string.Empty,name=string.Empty;
        private int dataOwnerID, updatedBy, createdBy, packageID;
        private Dictionary<string, string> customColumnDetails = new Dictionary<string, string>();
        private DateTime updatedDate, createdDate;
        #endregion Attributes

        #region Constructor
        public KTSKUDetails(long productID, long sku_ID, string productSKU, string skuDescription, Dictionary<string, string> customColumnDetails, int dataOwnerID, int createdBy, int updatedBy, DateTime createdDate, DateTime updatedDate, int packageID,string name)
        {
            this.productID = productID;
            this.sku_ID = sku_ID;
            this.productSKU = productSKU;
            this.skuDescription = skuDescription;
            this.customColumnDetails = customColumnDetails;
            this.dataOwnerID = dataOwnerID;
            this.createdBy = createdBy;
            this.createdDate = createdDate;
            this.updatedBy = updatedBy;
            this.updatedDate = updatedDate;
            this.packageID = packageID;
            this.name = name;
        }
        #endregion Constructor

        #region Properties
        /// <summary>
        /// Returns ProductID
        /// </summary>
        /// 
        [DataMember]
        public long ProductID
        {
            get
            {
                return this.productID;
            }
            set { productID = value; }
        }
        [DataMember]
        public long SKU_ID
        {
            get
            {
                return this.sku_ID;
            }
            set { sku_ID = value; }
        }
        [DataMember]
        public string ProductSKU
        {
            get
            {
                return this.productSKU;
            }
            set {
                
                    productSKU = value;
               
            }
        }
        [DataMember]
        public string SKUDescription
        {
            get
            {
                return this.skuDescription;
            }
            set
            {
               
                    skuDescription = value;
                
            }
        }
        [DataMember]
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
        [DataMember]
        public int DataOwnerID
        {
            get
            {
                return this.dataOwnerID;
            }
            set { dataOwnerID = value; }
        }
        [DataMember]
        public int CreatedBy
        {
            get
            {
                return this.createdBy;
            }
            set { createdBy = value; }
        }
        [DataMember]
        public int UpdatedBy
        {
            get
            {
                return this.updatedBy;
            }
            set { updatedBy = value; }
        }
        [DataMember]
        public DateTime UpdatedDate
        {
            get
            {
                return this.updatedDate;
            }
            set { updatedDate = value; }
        }
        [DataMember]
        public DateTime CreatedDate
        {
            get
            {
                return this.createdDate;
            }
            set { createdDate = value; }
        }
        [DataMember]
        public int PackageID
        {
            get
            {
                return this.packageID;
            }
            set { packageID = value; }
        }
        [DataMember]
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {

                name = value;

            }
        }

        #endregion Properties

        /// <summary>
        /// Returns SKUDetails in string format 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string strZone = string.Empty;
            StringBuilder str = new StringBuilder();

            str.Append("ProductID:" + this.productID.ToString() + "\r\n");
            str.Append("SKU_ID:" + this.sku_ID.ToString() + "\r\n");
            if (!string.IsNullOrEmpty(productSKU))
            str.Append("ProductSKU:" + this.productSKU.ToString() + "\r\n");
            if (!string.IsNullOrEmpty(skuDescription))
            str.Append("SKUDescription:" + this.skuDescription.ToString() + "\r\n");
            str.Append("DataOwnerID:" + this.dataOwnerID.ToString() + "\r\n");
            str.Append("CreatedBy:" + this.createdBy.ToString() + "\r\n");
            str.Append("UpdatedBy:" + this.updatedBy.ToString() + "\r\n");
            str.Append("UpdatedDate:" + this.updatedDate.ToString() + "\r\n");
            str.Append("CreatedDate:" + this.createdDate.ToString() + "\r\n");
            str.Append("PackageID:" + this.packageID.ToString() + "\r\n");
            str.Append("Name:" + this.name.ToString() + "\r\n");
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
    }

    /// <summary>
    /// Class for CustomFeildInfo.
    /// </summary>

    [Serializable]
    [DataContract]
    public class CustomFeildInfo
    {
        #region Attributes
        private long maxLength;
        private string custColName = string.Empty, aliasName = string.Empty, regExp = string.Empty, groupName = string.Empty, dataType = string.Empty;
        private int dataOwnerID;
        bool isMandatory, isMultivalued;
        private List<string> lstcolvalues = new List<string>();

        #endregion Attributes

        #region Constructor
        public CustomFeildInfo(string custColName, string aliasName, string regExp, string groupName, string dataType, int dataOwnerID, long maxLength, bool IsMandatory, bool IsMultivalued, List<string> lstcolvalues)
        {
            this.custColName = custColName;
            this.aliasName = aliasName;
            this.regExp = regExp;
            this.groupName = groupName;
            this.dataType = dataType;
            this.dataOwnerID = dataOwnerID;
            this.maxLength = maxLength;
            this.isMandatory = IsMandatory;
            this.isMultivalued = IsMultivalued;
            this.lstcolvalues = lstcolvalues;
          
        }
        public CustomFeildInfo()
        {
        }
        #endregion Constructor

        #region Properties
        /// <summary>
        /// Returns ProductID
        /// </summary>
        /// 
        [DataMember]
        public string CustColName
        {
            get
            {
                return this.custColName;
            }
            set
            {
               
                    custColName = value;
                
            }
        }
        [DataMember]
        public string AliasName
        {
            get
            {
                return this.aliasName;
            }
            set 
            {
                
                    aliasName = value;
               
            }
        }
        [DataMember]
        public string RegExp
        {
            get
            {
                return this.regExp;
            }
            set 
            {
                    regExp = value;
               
            }
        }
        [DataMember]
        public string GroupName
        {
            get
            {
                return this.groupName;
            }
            set {
                  groupName = value;
                
            }
        }
        [DataMember]
        public string DataType
        {
            get
            {
                return this.dataType;
            }
            set {
               
                    dataType = value;
               
            }
        }
        [DataMember]
        public long MaxLength
        {
            get
            {
                return this.maxLength;
            }
            set { maxLength = value; }

        }
        [DataMember]
        public int DataOwnerID
        {
            get
            {
                return this.dataOwnerID;
            }
            set { dataOwnerID = value; }

        }
      
        [DataMember]
        public bool IsMandatory
        {
            get
            {
                return this.isMandatory;
            }
            set { isMandatory = value; }
        }
        [DataMember]
        public bool IsMultivalued
        {
            get
            {
                return this.isMultivalued;
            }
            set { isMultivalued = value; }
        }

        [DataMember]
        public List<string> ListColValues
        {
            get
            {
                return this.lstcolvalues;
            }
            set
            {
                this.lstcolvalues = value;
            }
        }
        #endregion Properties
    }

    /// <summary>
    /// Class for  Location SKU  Association.
    /// </summary>
    /// 
    [Serializable]
    [DataContract]
    public class KTSKUlocationAssociation
    {
        #region Attributes
        int locationId, dataownerID;
        long Skuid;
        #endregion Attributes

        #region Constructor

        public KTSKUlocationAssociation(int locationId, int DataownerID, long Skuid)
        {
            this.locationId = locationId;
            this.dataownerID = DataownerID;
            this.Skuid = Skuid;
        }
        #endregion Constructor

        #region Properties
        [DataMember]
        public int LocationID
        {
            get
            {
                return this.locationId;
            }
            set
            {
                locationId = value;
            }
        }
        [DataMember]
        public long SKUID
        {
            get
            {
                return this.Skuid;
            }
            set
            {
                Skuid = value;
            }
        }
        [DataMember]
        public int DataOwnerID
        {
            get
            {
                return this.dataownerID;
            }
            set
            {
                dataownerID = value;
            }
        }
        #endregion Properties
    }

}

