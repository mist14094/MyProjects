using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Net.Security;
using KTone.Core.KTIRFID;
using KTone.WebServer;
using System.Data;

namespace KTone.Core.KTIRFID
{
    [ServiceContract(Namespace = "http://www.keytonetech.com/KTSmartDC")]
    public interface IKTOrders
    {
        /// <summary>
        ///  List of OrderDetails
        /// </summary>
        /// <param name="SalesOrderNo"></param>
        /// <param name="LineOrderNo"></param>
        /// <param name="SchedulePickdate"></param>
        /// <param name="List<string>WorkOrderNo"></param>
        /// <param name="List<long>ProductFamily"></param>
        /// <param name="List<long>BinCat"></param>
        /// <param name="List<int>Quantity"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        bool UploadOrderDetails(string SalesOrderNo,string LineOrderNo,DateTime SchedulePickdate,List<string> WorkOrderNo,
            List<long> ProductFamily,List<long> BinCat,List<int> Quantity, string userID, string password);

        
        ///// <summary>
        ///// Returns list of order details
        ///// </summary>
        ///// <param name="userID"></param>
        ///// <param name="password"></param>
        ///// <returns></returns>
        //[OperationContract]
        //[FaultContract(typeof(KTSDSeviceException))]
        //List<KTOrderDetails> GetNextWODetails(long OrderId, string userID, string password, out string ErrMessage);

        /// <summary>
        /// Returns list of order details
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        KTOrderDetails GetNextWODetails(long OrderId, int HHID, string userID, string password, out string ErrMessage);

        /// <summary>
        /// Returns list of orders
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        List<KTOrderMaster> GetOrderList( string userID, string password , int HHID);


        /// <summary>
        /// Save Picked orders
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        bool SavePickedOrderItems(long OrderDetailId, List<string> lstCustomerUniqueIds, int HHID, int ActuallyPickedQty, string userID, string password);

        
        /// <summary>
        /// Returns list of Picked orderitems
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        List<KTOrderDetails> GetUnPickedOrderItems(string SalesOrderNo, string LineOrderNo, string WorkOrderNo, string userID, string password);

         
        /// <summary>
        /// Save UnPicked orderitems
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        bool SaveUnPickedOrderItems(long OrderDetailId, List<string> lstCustomerUniqueIds, string userID, string password);

        /// <summary>
        /// Get OrderDetailList
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        List<KTOrderDetails> GetWorkOrderDetailList(string userID, string password, long OrderID, int HHID, out string ErrMessage);

        /// <summary>
        /// Save OrderDetailList
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        bool AssignWorkOrderToHH(string userID, string password, long OrderDetailID, int HHID, out string ErrMessage);

        /// <summary>
        /// Get Assigned OrderDetailList
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        KTOrderDetails CheckWorkOrderbyHH(int HHID, string userID, string password);

        /// <summary>
        /// Update Status of OrderItems
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        bool UpdateUnPickedOrders(List<string> lstCustomerUniqueIds, string userID, string password);


        /// <summary>
        /// Get Picked WorkOrders
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        //[OperationContract]
       // [FaultContract(typeof(KTSDSeviceException))]
       // List<KTOrderDetails> GetUnpickWorkOrderDetails(string userID, string password, string SalesOrderNo, string LineOrderNo);
    }
}
