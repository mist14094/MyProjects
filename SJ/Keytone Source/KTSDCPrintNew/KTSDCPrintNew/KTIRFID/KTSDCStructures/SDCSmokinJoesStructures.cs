using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace KTone.Core.KTIRFID
{
    [Serializable]
    [DataContract]
    public class ReplenishmentRequest
    {
        #region Private Fields

        private int _RR_ID;
        private string _RR_Number;
        private int _FromLocation;
        private int _ToLocation;
        private string _RR_Status;
        private DateTime _GenerationTime;
        private DateTime _FulfillmentDate;
        private int _CreatedBy;
        private DateTime _CreatedDate;
        private int _UpdatedBy;
        private DateTime _UpdateDate;
        private string _Comments;

        #endregion

        #region Constructors
        public ReplenishmentRequest(int rRID, string rRNumber, int fromLocation, int toLocation, string rRStatus, DateTime generationTime, DateTime fulFillmentDate
                                    , int createdBy, DateTime createdTime, int updatedBy, DateTime updatedTime, string comments)
        {
            this._RR_ID = rRID;
            this._RR_Number = rRNumber;
            this._FromLocation = fromLocation;
            this._ToLocation = toLocation;
            this._RR_Status = rRStatus;
            this._GenerationTime = generationTime;
            this._FulfillmentDate = fulFillmentDate;
            this._CreatedBy = createdBy;
            this._CreatedDate = createdTime;
            this._UpdatedBy = updatedBy;
            this._UpdateDate = updatedTime;
            this._Comments = comments;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Returns RR_ID
        /// </summary>
        /// 
        [DataMember]
        public int RR_ID { get { return _RR_ID; } set { _RR_ID = value; } }

        /// <summary>
        /// Returns RR_Number
        /// </summary>
        /// 
        [DataMember]
        public string RR_Number { get { return _RR_Number; } set { _RR_Number = value; } }

        /// <summary>
        /// Returns FromLocation
        /// </summary>
        /// 
        [DataMember]
        public int FromLocation { get { return _FromLocation; } set { _FromLocation = value; } }

        /// <summary>
        /// Returns ToLocation
        /// </summary>
        /// 
        [DataMember]
        public int ToLocation { get { return _ToLocation; } set { _ToLocation = value; } }

        /// <summary>
        /// Returns RR_Status
        /// </summary>
        /// 
        [DataMember]
        public string RR_Status { get { return _RR_Status; } set { _RR_Status = value; } }

        /// <summary>
        /// Returns GenerationTime
        /// </summary>
        /// 
        [DataMember]
        public DateTime GenerationTime { get { return _GenerationTime; } set { _GenerationTime = value; } }

        /// <summary>
        /// Returns FulfillmentDate
        /// </summary>
        /// 
        [DataMember]
        public DateTime FulfillmentDate { get { return _FulfillmentDate; } set { _FulfillmentDate = value; } }

        /// <summary>
        /// Returns CreatedBy
        /// </summary>
        /// 
        [DataMember]
        public int CreatedBy { get { return _CreatedBy; } set { _CreatedBy = value; } }

        /// <summary>
        /// Returns CreatedDate
        /// </summary>
        /// 
        [DataMember]
        public DateTime CreatedDate { get { return _CreatedDate; } set { _CreatedDate = value; } }

        /// <summary>
        /// Returns UpdatedBy
        /// </summary>
        /// 
        [DataMember]
        public int UpdatedBy { get { return _UpdatedBy; } set { _UpdatedBy = value; } }

        /// <summary>
        /// Returns UpdateDate
        /// </summary>
        /// 
        [DataMember]
        public DateTime UpdateDate { get { return _UpdateDate; } set { _UpdateDate = value; } }

        /// <summary>
        /// Returns Comments
        /// </summary>
        /// 
        [DataMember]
        public string Comments { get { return _Comments; } set { _Comments = value; } }


        #endregion
    }


    [Serializable]
    [DataContract]
    public class KTReplenishmentRequestDetails
    {
        #region Private Fields
        private int _RRD_ID;
        private int _RR_ID;
        private string _RRD_Status;
        private string _itemDescription;
        private DateTime _GenerationTime;
        private string _UPC;
        private string _SKU;
        private int _StoreID;
        private string _Category;

        private int _OrderedQty;
        private int _ShippedQty;
        private string _Comments;
        private int _CreatedBy;
        private DateTime _CreatedDate;
        private int _UpdatedBy;
        private DateTime _UpdateDate;
        #endregion

        #region Constructors
        public KTReplenishmentRequestDetails(int rRDID, int rrID, string rRDStatus, DateTime generationTime, string upc, string sku, int storeID, int orderdQty, int shippedQty
                                           , string comments, int createdBy, DateTime createdTime, int updatedBy, DateTime updatedTime)
        {
            this._RRD_ID = rRDID;
            this._RR_ID = rrID;
            this._RRD_Status = rRDStatus;
            this._GenerationTime = generationTime;
            this._UPC = upc;
            this._SKU = sku;
            this._StoreID = storeID;
            this._OrderedQty = orderdQty;
            this._ShippedQty = shippedQty;
            this._Comments = comments;
            this._CreatedBy = createdBy;
            this._CreatedDate = createdTime;
            this._UpdateDate = updatedTime;
            this._UpdatedBy = updatedBy;
        }

        public KTReplenishmentRequestDetails()
        {
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// Returns RRD_ID
        /// </summary>
        /// 
        [DataMember]
        public int RRD_ID { get { return _RRD_ID; } set { _RRD_ID = value; } }

        /// <summary>
        /// Returns RR_ID
        /// </summary>
        /// 
        [DataMember]
        public int RR_ID { get { return _RR_ID; } set { _RR_ID = value; } }

        /// <summary>
        /// Returns RRD_Status
        /// </summary>
        /// 
        [DataMember]
        public string RRD_Status { get { return _RRD_Status; } set { _RRD_Status = value; } }

        /// <summary>
        /// Returns ItemDescription
        /// </summary>
        /// 
        [DataMember]
        public string ItemDescription { get { return _itemDescription; } set { _itemDescription = value; } }

        /// <summary>
        /// Returns GenerationTime
        /// </summary>
        /// 
        [DataMember]
        public DateTime GenerationTime { get { return _GenerationTime; } set { _GenerationTime = value; } }

        /// <summary>
        /// Returns UPC
        /// </summary>
        /// 
        [DataMember]
        public string UPC { get { return _UPC; } set { _UPC = value; } }

        /// <summary>
        /// Returns SKU
        /// </summary>
        /// 
        [DataMember]
        public string SKU { get { return _SKU; } set { _SKU = value; } }

        /// <summary>
        /// Returns StoreID
        /// </summary>
        /// 
        [DataMember]
        public int StoreID { get { return _StoreID; } set { _StoreID = value; } }

        /// <summary>
        /// Returns Category
        /// </summary>
        /// 
        [DataMember]
        public string Category
        {
            get { return _Category; }
            set { _Category = value; }
        }

        /// <summary>
        /// Returns OrderedQty
        /// </summary>
        /// 
        [DataMember]
        public int OrderedQty { get { return _OrderedQty; } set { _OrderedQty = value; } }

        /// <summary>
        /// Returns ShippedQty
        /// </summary>
        /// 
        [DataMember]
        public int ShippedQty { get { return _ShippedQty; } set { _ShippedQty = value; } }

        /// <summary>
        /// Returns Comments
        /// </summary>
        /// 
        [DataMember]
        public string Comments { get { return _Comments; } set { _Comments = value; } }

        /// <summary>
        /// Returns CreatedBy
        /// </summary>
        /// 
        [DataMember]
        public int CreatedBy { get { return _CreatedBy; } set { _CreatedBy = value; } }

        /// <summary>
        /// Returns CreatedDate
        /// </summary>
        /// 
        [DataMember]
        public DateTime CreatedDate { get { return _CreatedDate; } set { _CreatedDate = value; } }

        /// <summary>
        /// Returns UpdatedBy
        /// </summary>
        /// 
        [DataMember]
        public int UpdatedBy { get { return _UpdatedBy; } set { _UpdatedBy = value; } }

        /// <summary>
        /// Returns UpdateDate
        /// </summary>
        /// 
        [DataMember]
        public DateTime UpdateDate { get { return _UpdateDate; } set { _UpdateDate = value; } }
        #endregion
    }


    [Serializable]
    [DataContract]
    public class Stores
    {
        #region Private Fields
        private int _StoreID;
        private string _StoreName;
        #endregion

        #region Constructors
        
        public Stores(int storeid,string storeName)
        {
            this._StoreID = storeid;
            this._StoreName = storeName;
        }

        #endregion
        
        #region Public Properties

        /// <summary>
        /// Returns StoreID
        /// </summary>
        /// 
        [DataMember]
        public int StoreID { get { return _StoreID; } set { _StoreID = value; } }

        /// <summary>
        /// Returns StoreName
        /// </summary>
        /// 
        [DataMember]
        public string StoreName { get { return _StoreName; } set { _StoreName = value; } }

        #endregion
    }

    [Serializable]
    [DataContract]
    public class Devices
    {
        
        #region Private Fields
        private int _deviceID,_storeID; 
        private string _ReaderLocation;
        private string _ReaderName;
        #endregion

        #region Constructors

        public Devices(int deviceID,int storeID, string ReaderLocation, string ReaderName)
        {
            this._deviceID = deviceID;
            this._storeID = storeID;
            this._ReaderLocation = ReaderLocation;
            this._ReaderName = ReaderName;
        }

        #endregion
        
        #region Public Properties

        /// <summary>
        /// Returns StoreID
        /// </summary>
        /// 
        [DataMember]
        public int StoreID { get { return _storeID; } set { _storeID = value; } }

        /// <summary>
        /// Returns DeviceID
        /// </summary>
        /// 
        [DataMember]
        public int DeviceID { get { return _deviceID; } set { _deviceID = value; } }

        /// <summary>
        /// Returns ReaderLocation
        /// </summary>
        /// 
        [DataMember]
        public string ReaderLocation { get { return _ReaderLocation; } set { _ReaderLocation = value; } }

        /// <summary>
        /// Returns ReaderName
        /// </summary>
        /// 
        [DataMember]
        public string ReaderName { get { return _ReaderName; } set { _ReaderName = value; } }

        #endregion
    }

    [Serializable]
    [DataContract]
    public class Locations
    {
        #region Private Fields
        private int _locationID;
        private string _locationName;
        private string _UPC;
        private long _TotalQty;
        #endregion

        #region Constructors

        public Locations(int locationid, string locationName)
        {
            this._locationID = locationid;
            this._locationName = locationName;
        }

        public Locations(string locationName , string upc, long TotQty)
        { 
            this._locationName = locationName;
            this._UPC = upc;
            this._TotalQty = TotQty;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Returns LocationID
        /// </summary>
        /// 
        [DataMember]
        public int LocationID { get { return _locationID; } set { _locationID = value; } }

        /// <summary>
        /// Returns LocationName
        /// </summary>
        /// 
        [DataMember]
        public string LocationName { get { return _locationName; } set { _locationName = value; } }
        /// <summary>
        /// Returns UPC
        /// </summary>
        /// 
        [DataMember]
        public string UPC { get { return _UPC; } set { _UPC = value; } }

        /// <summary>
        /// Returns TotalQty
        /// </summary>
        /// 
        [DataMember]
        public long TotalQty { get { return _TotalQty; } set { _TotalQty = value; } }

        #endregion
    }


    [Serializable]
    [DataContract]
    public class KTCategoryDetails
    {
        #region Private Fields

        private string _UPC, _SKU, _ItemDescription, _StyleCode, _SizeCode, _Category, _VendorName, _Gender, _StoreName,_Status,_RFID;
        int _StoreID, _Count, _ScannedQuantity, _OrderedQuantity, _MinQty, _MaxQty,_QOH,_DecommissionCnt,_ScanCnt,_ActualCheckOutCnt;
        DateTime _lastSeenTime;

        long _ProductItemID;
        private List<string> _RFIDS;

        private List<string> _ExpRFIDs;
        private List<string> _ExpPRODUCTITEMIDS;
        private List<string> _PRODUCTITEMIDS;
        private double _Price;
        #endregion

        #region Public Properties

        /// <summary>
        /// Returns ProductItemID
        /// </summary>
        /// 
        [DataMember]
        public long ProductItemID
        {
            get { return _ProductItemID; }
            set { _ProductItemID = value; }
        }

        /// <summary>
        /// Returns ProductItemID
        /// </summary>
        /// 
        [DataMember]
        public int DecommissionCnt
        {
            get { return _DecommissionCnt; }
            set { _DecommissionCnt = value; }
        }

      
        /// <summary>
        /// Returns Status
        /// </summary>
        /// 
        [DataMember]
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        /// <summary>
        /// Returns RFID
        /// </summary>
        /// 
        [DataMember]
        public string RFID
        {
            get { return _RFID; }
            set { _RFID = value; }
        }

        /// <summary>
        /// Returns LastSeenTime
        /// </summary>
        /// 
        [DataMember]
        public DateTime LastSeenTime
        {
            get { return _lastSeenTime; }
            set { _lastSeenTime = value; }
        }

        /// <summary>
        /// Returns Category
        /// </summary>
        /// 
        [DataMember]
        public string Category
        {
            get { return _Category; }
            set { _Category = value; }
        }

        /// <summary>
        /// Returns Gender
        /// </summary>
        /// 
        [DataMember]
        public string Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }



        /// <summary>
        /// Returns SizeCode
        /// </summary>
        /// 
        [DataMember]
        public string SizeCode
        {
            get { return _SizeCode; }
            set { _SizeCode = value; }
        }

        /// <summary>
        /// Returns StyleCode
        /// </summary>
        /// 
        [DataMember]
        public string StyleCode
        {
            get { return _StyleCode; }
            set { _StyleCode = value; }
        }

        /// <summary>
        /// Returns ItemDescription
        /// </summary>
        /// 
        [DataMember]
        public string ItemDescription
        {
            get { return _ItemDescription; }
            set { _ItemDescription = value; }
        }

        /// <summary>
        /// Returns SKU
        /// </summary>
        /// 
        [DataMember]
        public string SKU
        {
            get { return _SKU; }
            set { _SKU = value; }
        }

        /// <summary>
        /// Returns UPC
        /// </summary>
        /// 
        [DataMember]
        public string UPC
        {
            get { return _UPC; }
            set { _UPC = value; }
        }

        /// <summary>
        /// Returns ActualCheckOutCnt
        /// </summary>
        /// 
        [DataMember]
        public int ActualCheckOutCnt
        {
            get { return _ActualCheckOutCnt; }
            set { _ActualCheckOutCnt = value; }
        }


        /// <summary>
        /// Returns Count
        /// This is will be used for adhoc only
        /// </summary>
        /// 
        [DataMember]
        public int Count
        {
            get { return _Count; }
            set { _Count = value; }
        }

          /// <summary>
        /// Returns _ScanCn
        /// This is will be used for adhoc only
        /// </summary>
        /// 
        [DataMember]
        public int ScanCnt
        {
            get { return _ScanCnt; }
            set { _ScanCnt = value; }
        }
        

        /// <summary>
        /// Returns QOH
        /// This is will be used for adhoc only
        /// </summary>
        /// 
        [DataMember]
        public int QOH
        {
            get { return _QOH; }
            set { _QOH = value; }
        }
        /// <summary>
        /// Returns StoreName
        /// This is will be used for adhoc only
        /// </summary>
        /// 
        [DataMember]
        public string StoreName
        {
            get { return _StoreName; }
            set { _StoreName = value; }
        }


        /// <summary>
        /// Returns ScannedCount
        /// </summary>
        /// 
        [DataMember]
        public int ScannedQuantity
        {
            get { return _ScannedQuantity; }
            set { _ScannedQuantity = value; }
        }

        /// <summary>
        /// Returns ScannedCount
        /// </summary>
        /// 
        [DataMember]
        public int OrderedQuantity
        {
            get { return _OrderedQuantity; }
            set { _OrderedQuantity = value; }
        }

        /// <summary>
        /// Returns StoreID
        /// </summary>
        /// 
        [DataMember]
        public int StoreID
        {
            get { return _StoreID; }
            set { _StoreID = value; }
        }

          /// <summary>
        /// Returns VendorName
        /// </summary>
        /// 
        [DataMember]
        public string VendorName
        {
            get { return _VendorName; }
            set { _VendorName = value; }
        }

        [DataMember]
        public List<string> RFIDS
        {
            get { return _RFIDS; }
            set { _RFIDS = value; }
        }

        [DataMember]
        public List<string> ExpRFIDs
        {
            get { return _ExpRFIDs; }
            set { _ExpRFIDs = value; }
        }


        [DataMember]
        public List<string> PRODUCTITEMIDS
        {
            get { return _PRODUCTITEMIDS; }
            set { _PRODUCTITEMIDS = value; }
        }


        [DataMember]
        public List<string> ExpPRODUCTITEMIDS
        {
            get { return _ExpPRODUCTITEMIDS; }
            set { _ExpPRODUCTITEMIDS = value; }
        }

        /// <summary>
        /// Returns MaxQty
        /// </summary>
        /// 
        [DataMember]
        public int MaxQty
        {
            get { return _MaxQty; }
            set { _MaxQty = value; }
        }

        /// <summary>
        /// Returns MinQty
        /// </summary>
        /// 
        [DataMember]
        public int MinQty
        {
            get { return _MinQty; }
            set { _MinQty = value; }
        }

        /// <summary>
        /// Returns Price
        /// </summary>
        /// 
        [DataMember]
        public double Price
        {
            get { return _Price; }
            set { _Price = value; }
        }
        #endregion

        #region constructors

        public KTCategoryDetails()
        {
        }

        public KTCategoryDetails( string Stylecode, string Sizecode, string SKU, string ItemDescription ,string VendorName, string Gender)
        {
            this._StyleCode = Stylecode;
            this._SizeCode = Sizecode;
            this._SKU = SKU;
            this._ItemDescription = ItemDescription;
            this._VendorName = VendorName;
            this._Gender = Gender;

        }
        #endregion constructors
    }


    [Serializable]
    [DataContract]
    public class KTPutPickDetails
    {
        #region Private Fields

        private string _UPC, _SKU, _ListNo, _Status, _ListType ,_Desc ;
        int _StoreID,  _PutAwayQty, _ActuallyPutAwayQty ,_BinLocation ,_Qty; 


        #endregion

        #region Public Properties

        /// <summary>
        /// Returns ListNo
        /// </summary>
        /// 
        [DataMember]
        public string ListNo
        {
            get { return _ListNo; }
            set { _ListNo = value; }
        }

        /// <summary>
        /// Returns Desc
        /// </summary>
        /// 
        [DataMember]
        public string Desc
        {
            get { return _Desc; }
            set { _Desc = value; }
        }

        /// <summary>
        /// Returns Status
        /// </summary>
        /// 
        [DataMember]
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        /// <summary>
        /// Returns ListType
        /// </summary>
        /// 
        [DataMember]
        public string ListType
        {
            get { return _ListType; }
            set { _ListType = value; }
        }
         
        /// <summary>
        /// Returns SKU
        /// </summary>
        /// 
        [DataMember]
        public string SKU
        {
            get { return _SKU; }
            set { _SKU = value; }
        }

        /// <summary>
        /// Returns UPC
        /// </summary>
        /// 
        [DataMember]
        public string UPC
        {
            get { return _UPC; }
            set { _UPC = value; }
        }

       
        /// <summary>
        /// Returns PutAwayQty
        /// </summary>
        /// 
        [DataMember]
        public int PutAwayQty
        {
            get { return _PutAwayQty; }
            set { _PutAwayQty = value; }
        }

        /// <summary>
        /// Returns Qty
        /// </summary>
        /// 
        [DataMember]
        public int Qty
        {
            get { return _Qty; }
            set { _Qty = value; }
        }

        /// <summary>
        /// Returns ActuallyPutAwayQty
        /// </summary>
        /// 
        [DataMember]
        public int ActuallyPutAwayQty
        {
            get { return _ActuallyPutAwayQty; }
            set { _ActuallyPutAwayQty = value; }
        }

        /// <summary>
        /// Returns StoreID
        /// </summary>
        /// 
        [DataMember]
        public int StoreID
        {
            get { return _StoreID; }
            set { _StoreID = value; }
        }

        /// <summary>
        /// Returns BinLocation
        /// </summary>
        /// 
        [DataMember]
        public int BinLocation
        {
            get { return _BinLocation; }
            set { _BinLocation = value; }
        }


        #endregion

        #region constructors

        public KTPutPickDetails(string UPC , string Desc,int PutawayQty)
        {
            this._UPC = UPC;
            this._Desc = Desc;
            this._PutAwayQty = PutawayQty;
        }

        public KTPutPickDetails()
        {

        }

        
        #endregion constructors
    }


    [Serializable]
    [DataContract]
    public class KTCycleCount
    {
        #region Private Fields

        private string _ZoneName, _DeptName ;
        int _StoreID, _ZoneID, _DeptID ,_CyclecountID ;
        DateTime _StartDate;

        #endregion

        #region Public Properties

        /// <summary>
        /// Returns StartDate
        /// </summary>
        /// 
        [DataMember]
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }

        /// <summary>
        /// Returns CycleCountID
        /// </summary>
        /// 
        [DataMember]
        public int CycleCountID
        {
            get { return _CyclecountID; }
            set { _CyclecountID = value; }
        }

        /// <summary>
        /// Returns ZoneName
        /// </summary>
        /// 
        [DataMember]
        public string ZoneName
        {
            get { return _ZoneName; }
            set { _ZoneName = value; }
        }

        /// <summary>
        /// Returns DeptName
        /// </summary>
        /// 
        [DataMember]
        public string DeptName
        {
            get { return _DeptName; }
            set { _DeptName = value; }
        }

        

        /// <summary>
        /// Returns ZoneID
        /// </summary>
        /// 
        [DataMember]
        public int ZoneID
        {
            get { return _ZoneID; }
            set { _ZoneID = value; }
        }

        /// <summary>
        /// Returns DeptID
        /// </summary>
        /// 
        [DataMember]
        public int DeptID
        {
            get { return _DeptID; }
            set { _DeptID = value; }
        }

        /// <summary>
        /// Returns StoreID
        /// </summary>
        /// 
        [DataMember]
        public int StoreID
        {
            get { return _StoreID; }
            set { _StoreID = value; }
        }

       

        #endregion

        #region constructors
        public KTCycleCount()
        {

        }
        #endregion constructors
    }


    [Serializable]
    [DataContract]
    public class KTReplensihmentRequest 
    {
        #region Private Fields
        private int _RR_ID;
        private string _RR_Number;
        private int _FromLocation;
        private int _ToLocation;
        private string _RR_Status;
        private DateTime _GenerationTime;
        private DateTime _FulfillmentDate;
        private int _CreatedBy;
        private DateTime _CreatedDate;
        private int _UpdatedBy;
        private DateTime _UpdateDate;
        private string _Comments;
        #endregion

        #region Constructors
        public KTReplensihmentRequest() { }
        #endregion

        #region Public Properties

        /// <summary>
        /// Returns RR_ID
        /// </summary>
        /// 
        [DataMember]
        public int RR_ID { get { return _RR_ID; } set { _RR_ID = value; } }

        /// <summary>
        /// Returns RR_Number
        /// </summary>
        /// 
        [DataMember]
        public string RR_Number { get { return _RR_Number; } set { _RR_Number = value; } }

        /// <summary>
        /// Returns FromLocation
        /// </summary>
        /// 
        [DataMember]
        public int FromLocation { get { return _FromLocation; } set { _FromLocation = value; } }

        /// <summary>
        /// Returns ToLocation
        /// </summary>
        /// 
        [DataMember]
        public int ToLocation { get { return _ToLocation; } set { _ToLocation = value; } }

        /// <summary>
        /// Returns RR_Status
        /// </summary>
        /// 
        [DataMember]
        public string RR_Status { get { return _RR_Status; } set { _RR_Status = value; } }

        /// <summary>
        /// Returns GenerationTime
        /// </summary>
        /// 
        [DataMember]
        public DateTime GenerationTime { get { return _GenerationTime; } set { _GenerationTime = value; } }

        /// <summary>
        /// Returns FulfillmentDate
        /// </summary>
        /// 
        [DataMember]
        public DateTime FulfillmentDate { get { return _FulfillmentDate; } set { _FulfillmentDate = value; } }

        /// <summary>
        /// Returns CreatedBy
        /// </summary>
        /// 
        [DataMember]
        public int CreatedBy { get { return _CreatedBy; } set { _CreatedBy = value; } }

        /// <summary>
        /// Returns CreatedDate
        /// </summary>
        /// 
        [DataMember]
        public DateTime CreatedDate { get { return _CreatedDate; } set { _CreatedDate = value; } }

        /// <summary>
        /// Returns UpdatedBy
        /// </summary>
        /// 
        [DataMember]
        public int UpdatedBy { get { return _UpdatedBy; } set { _UpdatedBy = value; } }

        /// <summary>
        /// Returns UpdateDate
        /// </summary>
        /// 
        [DataMember]
        public DateTime UpdateDate { get { return _UpdateDate; } set { _UpdateDate = value; } }

        /// <summary>
        /// Returns Comments
        /// </summary>
        /// 
        [DataMember]
        public string Comments { get { return _Comments; } set { _Comments = value; } }

        #endregion
    }

    [Serializable]
    [DataContract]
    public class KTLookUP
    {
        #region Private Fields

        private string _Name, _Value,_StoreID;
        int _LookUpId, _RoleID  ; 

        #endregion

        #region Public Properties

       
        /// <summary>
        /// Returns LookUpId
        /// </summary>
        /// 
        [DataMember]
        public int LookUpId
        {
            get { return _LookUpId; }
            set { _LookUpId = value; }
        }
        
        /// <summary>
        /// Returns RoleID
        /// </summary>
        /// 
        [DataMember]
        public int RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; }
        }

        /// <summary>
        /// Returns StoreID
        /// </summary>
        /// 
        [DataMember]
        public string StoreID
        {
            get { return _StoreID; }
            set { _StoreID = value; }
        }

        /// <summary>
        /// Returns Name
        /// </summary>
        /// 
        [DataMember]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        /// <summary>
        /// Returns Value
        /// </summary>
        /// 
        [DataMember]
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
         



        #endregion

        #region constructors
        public KTLookUP()
        {

        }
        #endregion constructors
    }
}
