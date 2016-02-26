using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace KTone.Core.KTIRFID
{

     /// <summary>
    /// Order Details
    /// </summary>
    [Serializable]
    [DataContract]

    public class KTOrderMaster
    {

        #region Attributes

            private long _OrderID;
            private int _OrderTypeID, _ApprovedBy, _CreatedBy, _UpdatedBy;
            private int  _OrderStatusID, _DataOwnerId , _AssignedTo;
            private string _OrderNo, _LineOrderNo, _Comment, _OrderStatus, _OrderType, _AssignedToUser;
            private DateTime _SchedulePickDate, _CreatedDate, _UpdatedDate;
            private bool _IsApproved;

        #endregion Attrubutes

        #region Contructor    
        

        public KTOrderMaster(int orderID, string orderType, int orderTypeID, string orderNo, string lineorderNo, DateTime schedulePickDate, string orderStatus, int orderStatusID
                             , bool _isApproved , int _approvedBy , DateTime _createdDate , int _createdBy , DateTime _updatedDate , int _updatedBy , int _assignedTo , string _assignedToUser )
        {
            this._OrderID = orderID;            
            this._OrderType = orderType;            
            this._OrderTypeID = orderTypeID;
            this._OrderNo = orderNo;
            this._LineOrderNo = lineorderNo;
            this._SchedulePickDate = schedulePickDate;
            this._OrderStatus = orderStatus;
            this._OrderStatusID = orderStatusID;
            this._IsApproved = _isApproved;
            this._ApprovedBy = _approvedBy;
            this._CreatedDate = _createdDate;
            this._CreatedBy = _createdBy;
            this._UpdatedDate = _updatedDate;
            this._UpdatedBy = _updatedBy;
            this._AssignedTo = _assignedTo;
            this._AssignedToUser = _assignedToUser;
        }

        #endregion Contructor


        #region properties

        /// <summary>
        /// Returns OrderID
        /// </summary>
        /// 
        [DataMember]
        public long OrderID
        {
            get { return _OrderID; }
            set { _OrderID = value; }
        }

        /// <summary>
        /// Returns OrderType
        /// </summary>
        /// 
        [DataMember]
        public string OrderType
        {
            get { return _OrderType; }
            set { _OrderType = value; }
        }


        /// <summary>
        /// Returns OrderTypeID
        /// </summary>
        /// 
        [DataMember]
        public int OrderTypeID
        {
            get { return _OrderTypeID; }
            set { _OrderTypeID = value; }
        }


        /// <summary>
        /// Returns OrderNo
        /// </summary>
        /// 
        [DataMember]
        public string OrderNo
        {
            get { return _OrderNo; }
            set { _OrderNo = value; }
        }
                

        /// <summary>
        /// Returns DesignPatternNo
        /// </summary>
        /// 
        [DataMember]
        public string LineOrderNo
        {
            get { return _LineOrderNo; }
            set { _LineOrderNo = value; }
        }

        /// <summary>
        /// Returns SchedulePickDate
        /// </summary>
        /// 
        [DataMember]
        public DateTime SchedulePickDate
        {
            get { return _SchedulePickDate; }
            set { _SchedulePickDate = value; }
        }


        /// <summary>
        /// Returns OrderStatus
        /// </summary>
        /// 
        [DataMember]
        public string OrderStatus
        {
            get { return _OrderStatus; }
            set { _OrderStatus = value; }
        }

        /// <summary>
        /// Returns OrderStatusID
        /// </summary>
        /// 
        [DataMember]
        public int OrderStatusID
        {
            get { return _OrderStatusID; }
            set { _OrderStatusID = value; }
        }

        /// <summary>
        /// Returns Comment
        /// </summary>
        /// 
        [DataMember]
        public string Comment
        {
            get { return _Comment; }
            set { _Comment = value; }
        }

        /// <summary>
        /// Returns IsApproved
        /// </summary>
        /// 
        [DataMember]
        public bool IsApproved
        {
            get { return _IsApproved; }
            set { _IsApproved = value; }
        }

        /// <summary>
        /// Returns ApprovedBy
        /// </summary>
        /// 
        [DataMember]
        public int ApprovedBy
        {
            get { return _ApprovedBy; }
            set { _ApprovedBy = value; }
        }

        /// <summary>
        /// Returns DataOwnerId
        /// </summary>
        /// 
        [DataMember]
        public int DataOwnerId
        {
            get { return _DataOwnerId; }
            set { _DataOwnerId = value; }
        }

        /// <summary>
        /// Returns CreatedDate
        /// </summary>
        /// 
        [DataMember]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }

        /// <summary>
        /// Returns CreatedBy
        /// </summary>
        /// 
        [DataMember]
        public int CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }

        /// <summary>
        /// Returns UpdatedDate
        /// </summary>
        /// 
        [DataMember]
        public DateTime UpdatedDate
        {
            get { return _UpdatedDate; }
            set { _UpdatedDate = value; }
        }

        /// <summary>
        /// Returns UpdatedBy
        /// </summary>
        /// 
        [DataMember]
        public int UpdatedBy
        {
            get { return _UpdatedBy; }
            set { _UpdatedBy = value; }
        }


        #endregion


    }


    /// <summary>
    /// Internal Order  Details
    /// </summary>
    [Serializable]
    [DataContract]
    public class KTInternalOrderDetails
    {
        #region Attributes
        private string _binTapeId, _Location;
        private long _OrderID, _OrderDetailId, _ProductID, _SKU_ID ;
        private string _binCat, _partnumber;
        private int _PickQty;
      //  private List<string> _binTapeIds = new List<string>();
        #endregion

        #region Contructor

        #endregion

        public KTInternalOrderDetails(string BintapeId, string location, int pickQty, string PartNumber, string Bincat)         
        {
            this._binTapeId = BintapeId;
            this._Location = location;
            this._PickQty = pickQty;
            this._partnumber = PartNumber;
            this._binCat = Bincat; 
        }

        #region properties

        /// <summary>
        /// Returns OrderID
        /// </summary>
        /// 
        [DataMember]
        public long OrderID
        {
            get { return _OrderID; }
            set { _OrderID = value; }
        }

        /// <summary>
        /// Returns OrderDetailId
        /// </summary>
        /// 
        [DataMember]
        public long OrderDetailId
        {
            get { return _OrderDetailId; }
            set { _OrderDetailId = value; }
        }

        /// <summary>
        /// Returns BinTapeID
        /// </summary>
        /// 
        [DataMember]
        public string BinTapeID
        {
            get { return _binTapeId; }
            set { _binTapeId = value; }
        }
        /// <summary>
        /// Returns Location
        /// </summary>
        /// 
        [DataMember]
        public string Location
        {
            get { return _Location; }
            set { _Location = value; }
        }
        /// <summary>
        /// Returns PartNnmberID
        /// </summary>
        /// 
        [DataMember]
        public long PartNnmberID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }
        /// <summary>
        /// Returns BincatID
        /// </summary>
        /// 
        [DataMember]
        public long BincatID
        {
            get { return _SKU_ID; }
            set { _SKU_ID = value; }
        }

        /// <summary>
        /// Returns Partnumber
        /// </summary>
        /// 
        [DataMember]
        public string Partnumber
        {
            get { return _partnumber; }
            set { _partnumber = value; }
        }

        /// <summary>
        /// Returns Bincat
        /// </summary>
        /// 
        [DataMember]
        public string Bincat 
        {
            get { return _binCat; }
            set { _binCat = value; }
        }

        /// <summary>
        /// Returns Bincat
        /// </summary>
        /// 
        [DataMember]
        public int PickQty
        {
            get { return _PickQty; }
            set { _PickQty = value; }
        }
        #endregion
    }



    /// <summary>
    /// Order Details
    /// </summary>
    [Serializable]
    [DataContract]

    public class KTOrderDetails
    {

        #region Attributes

        private long _OrderID, _OrderDetailId, _ProductID, _SKU_ID , _PresentQuantity;
        private int _OrderTypeID, _ApprovedBy, _CreatedBy, _UpdatedBy, _PickQty, _ActuallyPickedQty;
        private int _PickedBy, _OrderStatusID, _TraceCriteria, _DataOwnerId;
        private string  _OrderNo, _LineOrderNo, _Comment, _WorkOrderNo, _TraceValue, _OrderStatus, _OrderType;
        private string _binCat, _productFamily;
        private DateTime _SchedulePickDate, _CreatedDate, _UpdatedDate;
        private bool _IsApproved, _IsTopUp;
        private List<OrderTopUp> _TopUpOrder = new List<OrderTopUp>();


        private List<string> _workOrderNos = new List<string>();
        private List<long> _productFamilys = new List<long>();
        private List<long> _binCats = new List<long>();
        private List<int> _quantity = new List<int>();
        private List<string> _binTapeIds = new List<string>();
        private Dictionary<string, KTInternalOrderDetails> _binTapes_LocationDetails = new Dictionary<string, KTInternalOrderDetails>();

        #endregion Attrubutes

        #region Contructor       


        public KTOrderDetails(string salesOrderNo, string lineOrderNo, DateTime schedulePickDate, List<string> workorderNos,
            List<long> productFamilys, List<long> binCats, List<int> quantity)
        {
            this._OrderNo = salesOrderNo;
            this._LineOrderNo = lineOrderNo;
            this._SchedulePickDate = schedulePickDate;
            this._workOrderNos = workorderNos;
            this._productFamilys = productFamilys;
            this._binCats = binCats;
            this._quantity = quantity;
        }


        public KTOrderDetails(long orderID, string orderType, int orderTypeId, string orderNo, string lineOrderNo, DateTime schedulePickDate, 
            string orderStatus,int orderStatusId ,  string comment, bool isApproved, int approvedBy, long orderDetailId, string workOrderNo,
            int pickQty, bool isTopUp, long presentQuantity, long productID,string productFamily, long SKU_ID,string binCat, List<string> BinTapeIDs,
            Dictionary<string, KTInternalOrderDetails> BinTapes_LocationDetails,int pickedBy, int dataOwnerId, DateTime createdDate, int createdBy, DateTime updatedDate, int updatedBy)
        {
            _OrderID = orderID;
            _OrderType = orderType;
            _OrderTypeID = orderTypeId;
            _OrderNo = orderNo;
            _LineOrderNo = lineOrderNo;
            _SchedulePickDate = schedulePickDate;
            _OrderStatus = orderStatus;
            _OrderStatusID = orderStatusId;
            _Comment = comment;
            _IsApproved = isApproved;
            _ApprovedBy = approvedBy;
            _OrderDetailId = orderDetailId;
            _WorkOrderNo = workOrderNo;
            _OrderID = orderID;
            _PickQty = pickQty;
            _IsTopUp = isTopUp;
            _PresentQuantity = presentQuantity;
            _ProductID = productID;
            _productFamily = productFamily;
            _SKU_ID = SKU_ID;
            _binCat = binCat;
            _binTapeIds = BinTapeIDs;
            _binTapes_LocationDetails = BinTapes_LocationDetails;
           // _TraceCriteria = traceCriteria;
            //_TraceValue = traceValue;
            _PickedBy = pickedBy;
            _OrderStatus = orderStatus;
            _DataOwnerId = dataOwnerId;
            _CreatedDate = createdDate;
            _CreatedBy = createdBy;
            _UpdatedDate = updatedDate;
            _UpdatedBy = updatedBy;
        }
           

        #endregion constructor

        #region properties

        /// <summary>
        /// Returns OrderID
        /// </summary>
        /// 
        [DataMember]
        public long OrderID
        {
            get { return _OrderID; }
            set { _OrderID = value; }
        }


        /// <summary>
        /// Returns Bintapes and Location Details
        /// </summary>
        /// 
        [DataMember]
        public Dictionary<string,KTInternalOrderDetails> _BinTapes_LocationDetails
        {
            get { return _binTapes_LocationDetails; }
            set { _binTapes_LocationDetails = value; }
        }

        /// <summary>
        /// Returns OrderType
        /// </summary>
        /// 
        [DataMember]
        public string OrderType
        {
            get { return _OrderType; }
            set { _OrderType = value; }
        }


        /// <summary>
        /// Returns OrderTypeID
        /// </summary>
        /// 
        [DataMember]
        public int OrderTypeID
        {
            get { return _OrderTypeID; }
            set { _OrderTypeID = value; }
        }


        /// <summary>
        /// Returns OrderNo
        /// </summary>
        /// 
        [DataMember]
        public string OrderNo
        {
            get { return _OrderNo; }
            set { _OrderNo = value; }
        }
                

        /// <summary>
        /// Returns DesignPatternNo
        /// </summary>
        /// 
        [DataMember]
        public string LineOrderNo
        {
            get { return _LineOrderNo; }
            set { _LineOrderNo = value; }
        }

        /// <summary>
        /// Returns SchedulePickDate
        /// </summary>
        /// 
        [DataMember]
        public DateTime SchedulePickDate
        {
            get { return _SchedulePickDate; }
            set { _SchedulePickDate = value; }
        }


        /// <summary>
        /// Returns OrderStatus
        /// </summary>
        /// 
        [DataMember]
        public string OrderStatus
        {
            get { return _OrderStatus; }
            set { _OrderStatus = value; }
        }

        /// <summary>
        /// Returns OrderStatusID
        /// </summary>
        /// 
        [DataMember]
        public int OrderStatusID
        {
            get { return _OrderStatusID; }
            set { _OrderStatusID = value; }
        }

        /// <summary>
        /// Returns Comment
        /// </summary>
        /// 
        [DataMember]
        public string Comment
        {
            get { return _Comment; }
            set { _Comment = value; }
        }

        /// <summary>
        /// Returns IsApproved
        /// </summary>
        /// 
        [DataMember]
        public bool IsApproved
        {
            get { return _IsApproved; }
            set { _IsApproved = value; }
        }

        /// <summary>
        /// Returns ApprovedBy
        /// </summary>
        /// 
        [DataMember]
        public int ApprovedBy
        {
            get { return _ApprovedBy; }
            set { _ApprovedBy = value; }
        }

        /// <summary>
        /// Returns DataOwnerId
        /// </summary>
        /// 
        [DataMember]
        public int DataOwnerId
        {
            get { return _DataOwnerId; }
            set { _DataOwnerId = value; }
        }

        /// <summary>
        /// Returns CreatedDate
        /// </summary>
        /// 
        [DataMember]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }

        /// <summary>
        /// Returns CreatedBy
        /// </summary>
        /// 
        [DataMember]
        public int CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }

        /// <summary>
        /// Returns UpdatedDate
        /// </summary>
        /// 
        [DataMember]
        public DateTime UpdatedDate
        {
            get { return _UpdatedDate; }
            set { _UpdatedDate = value; }
        }

        /// <summary>
        /// Returns UpdatedBy
        /// </summary>
        /// 
        [DataMember]
        public int UpdatedBy
        {
            get { return _UpdatedBy; }
            set { _UpdatedBy = value; }
        }


        /// <summary>
        /// Returns OrderDetailId
        /// </summary>
        /// 
        [DataMember]
        public long OrderDetailId
        {
            get { return _OrderDetailId; }
            set { _OrderDetailId = value; }
        }

        /// <summary>
        /// Returns WorkOrderNo
        /// </summary>
        /// 
        [DataMember]
        public string WorkOrderNo
        {
            get { return _WorkOrderNo; }
            set { _WorkOrderNo = value; }
        }

        /// <summary>
        /// Returns PickQty
        /// </summary>
        /// 
        [DataMember]
        public int PickQty
        {
            get { return _PickQty; }
            set { _PickQty = value; }
        }

        /// <summary>
        /// Returns IsTopUp
        /// </summary>
        /// 
        [DataMember]
        public bool IsTopUp
        {
            get { return _IsTopUp; }
            set { _IsTopUp = value; }
        }

        /// <summary>
        /// Returns ActuallyPickedQty
        /// </summary>
        /// 
        [DataMember]
        public int ActuallyPickedQty
        {
            get { return _ActuallyPickedQty; }
            set { _ActuallyPickedQty = value; }
        }
    
        /// <summary>
        /// Returns PresentQuantity
        /// </summary>
        /// 
        [DataMember]
        public long PresentQuantity
        {
            get { return _PresentQuantity; }
            set { _PresentQuantity = value; }
        }

        /// <summary>
        /// Returns ProductID
        /// </summary>
        /// 
        [DataMember]
        public long ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }

        /// <summary>
        /// Returns SKU_ID
        /// </summary>
        /// 
        [DataMember]
        public long SKU_ID
        {
            get { return _SKU_ID; }
            set { _SKU_ID = value; }
        }

        /// <summary>
        /// Returns TraceCriteria
        /// </summary>
        /// 
        [DataMember]
        public int TraceCriteria
        {
            get { return _TraceCriteria; }
            set { _TraceCriteria = value; }
        }

        /// <summary>
        /// Returns TraceValue
        /// </summary>
        /// 
        [DataMember]
        public string TraceValue
        {
            get { return _TraceValue; }
            set { _TraceValue = value; }
        }
        /// <summary>
        /// Returns ProductFamily
        /// </summary>
        /// 
        [DataMember]
        public string ProductFamily
        {
            get { return _productFamily; }
            set { _productFamily = value; }
        }
        /// <summary>
        /// Returns BinCat
        /// </summary>
        /// 
        [DataMember]
        public string BinCat
        {
            get { return _binCat; }
            set { _binCat = value; }
        }

        /// <summary>
        /// Returns BinTapeIDs
        /// </summary>
        /// 
        [DataMember]
        public List<string> BinTapeIDs
        {
            get { return _binTapeIds; }
            set { _binTapeIds = value; }
        }


        /// <summary>
        /// Returns PickedBy
        /// </summary>
        /// 
        [DataMember]
        public int PickedBy
        {
            get { return _PickedBy; }
            set { _PickedBy = value; }
        }

        /// <summary>
        /// Returns TopUpOrder
        /// </summary>
        /// 
        [DataMember]
        public List<OrderTopUp> TopUpOrder
        {
            get { return _TopUpOrder; }
            set { _TopUpOrder = value; }
        }


        /// <summary>
        /// Returns WorkOrderNos
        /// </summary>
        /// 
        [DataMember]
        public List<string> WorkOrderNos
        {
            get
            {
                return this._workOrderNos;
            }
            set
            {
                this._workOrderNos = value;
            }
        }
        /// <summary>
        /// Returns BinCat
        /// </summary>
        /// 
        [DataMember]
        public List<long> BinCats
        {
            get
            {
                return this._binCats;
            }
            set
            {
                this._binCats = value;
            }
        }
        /// <summary>
        /// Returns ProductFamily
        /// </summary>
        /// 
        [DataMember]
        public List<long> ProductFamilys
        {
            get
            {
                return this._productFamilys;
            }
            set
            {
                this._productFamilys = value;
            }
        }
        /// <summary>
        /// Returns Quantity
        /// </summary>
        /// 
        [DataMember]
        public List<int> Quantity
        {
            get
            {
                return this._quantity;
            }
            set
            {
                this._quantity = value;
            }
        }

        #endregion

    }

    /// <summary>
    /// Company details
    /// </summary>
    [Serializable]
    [DataContract]

    public class OrderTopUp
    {

        #region Attributes

        private int _OrderTopUpId, _TopUpQuantity, _DataOwnerId, _OrderStatusId, _PickedBy;
        private long _OrderDetailId, _BinCatId, _ProductId, _PresentQuantity;
        private DateTime _PickedTime;
        private string _Comments, _OrderStatus , _BinCat , _ProductFamily;

        #endregion Attributes


        #region Constructor
        public OrderTopUp(int orderTopUpId, long orderDetailId, int topUpQuantity, long presentQuantity, long binCatId, string binCat, long productId, 
                           string productFamily, int dataOwnerId, int orderStatusId, string orderStatus,  int pickedBy, DateTime pickedTime, string comments)
        {
            _OrderTopUpId = orderTopUpId;
            _OrderDetailId = orderDetailId;
            _TopUpQuantity = topUpQuantity;
            _PresentQuantity = presentQuantity;
            _BinCatId = binCatId;
            _BinCat = binCat;
            _ProductFamily = productFamily;
            _ProductId = productId;
            _DataOwnerId = dataOwnerId;
            _OrderStatus = orderStatus;
            _OrderStatusId = orderStatusId;
            _PickedBy = pickedBy;
            _PickedTime = pickedTime;
            _Comments = comments;

        }

        #endregion Constructor


        #region properties
        /// <summary>
        /// OrderTopUpId
        /// </summary>
        [DataMember]
        public int OrderTopUpId
        {
            get { return _OrderTopUpId; }
            set { _OrderTopUpId = value; }
        }

        /// <summary>
        /// OrderDetailId
        /// </summary>
        [DataMember]
        public long OrderDetailId
        {
            get { return _OrderDetailId; }
            set { _OrderDetailId = value; }
        }

        /// <summary>
        /// TopUpQuantity
        /// </summary>
        [DataMember]
        public int TopUpQuantity
        {
            get { return _TopUpQuantity; }
            set { _TopUpQuantity = value; }
        }

        /// <summary>
        /// PresentQuanity
        /// </summary>
        [DataMember]
        public long PresentQuanity
        {
            get { return _PresentQuantity; }
            set { _PresentQuantity = value; }
        }

        /// <summary>
        /// SKU_ID
        /// </summary>
        [DataMember]
        public long BinCatID
        {
            get { return _BinCatId; }
            set { _BinCatId = value; }
        }

        /// <summary>
        /// ProductId
        /// </summary>
        [DataMember]
        public long ProductId
        {
            get { return _ProductId; }
            set { _ProductId = value; }
        }

        /// <summary>
        /// DataOwnerId
        /// </summary>
        [DataMember]
        public int DataOwnerId
        {
            get { return _DataOwnerId; }
            set { _DataOwnerId = value; }
        }

        /// <summary>
        /// OrderStatus
        /// </summary>
        [DataMember]
        public string OrderStatus
        {
            get { return _OrderStatus; }
            set { _OrderStatus = value; }
        }

        /// <summary>
        /// BinCat
        /// </summary>
        [DataMember]
        public string BinCat
        {
            get { return _BinCat; }
            set { _BinCat = value; }
        }


        /// <summary>
        /// BinCat
        /// </summary>
        [DataMember]
        public string ProductFamily
        {
            get { return _ProductFamily; }
            set { _ProductFamily = value; }
        }

        /// <summary>
        /// OrderStatusID
        /// </summary>
        [DataMember]
        public int OrderStatusID
        {
            get { return _OrderStatusId; }
            set { _OrderStatusId = value; }
        }


        /// <summary>
        /// PickedBy
        /// </summary>
        [DataMember]
        public int PickedBy
        {
            get { return _PickedBy; }
            set { _PickedBy = value; }
        }


        /// <summary>
        /// PickedTime
        /// </summary>
        [DataMember]
        public DateTime PickedTime
        {
            get { return _PickedTime; }
            set { _PickedTime = value; }
        }


        /// <summary>
        /// Comments
        /// </summary>
        [DataMember]
        public string Comments
        {
            get { return _Comments; }
            set { _Comments = value; }
        }

        #endregion

    }



}
