using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml;
using KTone.DAL.SmartDCDataAccess;
using System.Configuration;
using System.Data.SqlTypes;
using KTone.DAL.KTDBBaseLib;
using KTone.DAL.KTDAGlobaApplLib;
using KTone.Core.KTIRFID;

namespace KTone.Core.SDCBusinessLogic
{
    public class OrderClass
    {
        NLog.Logger _log = NLog.LogManager.GetCurrentClassLogger();

        public bool UploadOrderDetails(string SalesOrderNo, string LineOrderNo, DateTime SchedulePickdate, List<string> WorkOrderNo,
            List<long> ProductFamily, List<long> BinCat, List<int> Quantity, int dataOwnerID, int userid)
        {
            bool insertorders = false;
            try
            {
                _log.Trace("OrderClass:UploadOrderDetails Entering");

                insertorders = InsertOrderDetails(SalesOrderNo, LineOrderNo, SchedulePickdate, WorkOrderNo,
                           ProductFamily, BinCat, Quantity, dataOwnerID, userid);

            }
            catch (Exception ex)
            {
                _log.Error("Error:OrderClass:UploadOrderDetails:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("OrderClass:UploadOrderDetails Leaving");
            }
            return insertorders;
        }

        public bool InsertOrderDetails(string SalesOrderNo, string DesignPatternNo, DateTime SchedulePickdate, List<string> WorkOrderNo,
            List<long> ProductFamily, List<long> BinCat, List<int> Quantity, int dataOwnerID, int userid)
        {
            List<KTOrderDetails> _lstOrderDetails = new List<KTOrderDetails>();
            bool result = false;
            try
            {
                _log.Trace("OrderClass:InsertOrderDetails Entering");
                OrderMaster clsOrder = new OrderMaster();
                string WorkOrders = string.Empty; string productIds = string.Empty; string SkuIds = string.Empty; string Qtys = string.Empty;

                WorkOrders = string.Join(",", WorkOrderNo.Select(i => i.ToString()).ToArray());
                productIds = string.Join(",", ProductFamily.Select(i => i.ToString()).ToArray());
                SkuIds = string.Join(",", BinCat.Select(i => i.ToString()).ToArray());
                Qtys = string.Join(",", Quantity.Select(i => i.ToString()).ToArray());

                clsOrder.SalesOrderNo = SalesOrderNo.ToString();
                clsOrder.LineOrderNO = DesignPatternNo.ToString();
                clsOrder.SchedulePickDate = Convert.ToDateTime(SchedulePickdate);
                clsOrder.WorkOrderNo = WorkOrders.ToString();
                clsOrder.ProductFamily = productIds.ToString();
                clsOrder.BinCat = SkuIds.ToString();
                clsOrder.Quantity = Qtys.ToString();
                clsOrder.OrderStatus = "OPEN";
                clsOrder.OrderType = "External";
                clsOrder.DataOwnerID = dataOwnerID;
                clsOrder.CreatedBy = Convert.ToInt32(userid);
                result = clsOrder.Insert();
                if (result == false)
                {
                    throw new Exception();
                }

            }
            catch (Exception ex)
            {
                _log.Error("Error:OrderClass:InsertOrderDetails:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                _log.Trace("OrderClass:InsertOrderDetails:Leaving");
            }
            return result;

        }


        public bool SavePickedOrderItems(long OrderDetailId, List<string> lstCustomerUniqueIds, int HHID, int ActuallyPickedQty, int dataOwnerID, int userid)
        {
            bool insertorders = false;
            try
            {
                _log.Trace("OrderClass:SavePickedOrderItems Entering");

                OrderMaster clsOrder = new OrderMaster();
                string OrderItems = string.Empty;

                OrderItems = string.Join(",", lstCustomerUniqueIds.Select(i => i.ToString()).ToArray());

                clsOrder.HHID = HHID;
                clsOrder.DataOwnerID = dataOwnerID;
                clsOrder.UserID = Convert.ToInt32(userid);
                clsOrder.ActualPickQty = ActuallyPickedQty;
                clsOrder.OrderDetailID = OrderDetailId;
                clsOrder.OrderItemsLst = OrderItems.ToString();
                insertorders = clsOrder.InsertOrderItems();
                if (insertorders == false)
                {
                    throw new Exception();
                }

            }
            catch (Exception ex)
            {
                _log.Error("Error:OrderClass:SavePickedOrderItems:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                _log.Trace("OrderClass:SavePickedOrderItems:Leaving");
            }
            return insertorders;
        }

        public bool SaveUnPickedOrderItems(long OrderDetailId, List<string> lstCustomerUniqueIds, int dataOwnerID, int userid)
        {
            bool insertorders = false;
            try
            {
                _log.Trace("OrderClass:SaveUnPickedOrderItems Entering");

                OrderMaster clsOrder = new OrderMaster();
                string OrderItems = string.Empty;

                OrderItems = string.Join(",", lstCustomerUniqueIds.Select(i => i.ToString()).ToArray());

                clsOrder.DataOwnerID = dataOwnerID;
                clsOrder.UserID = Convert.ToInt32(userid);
                clsOrder.OrderDetailID = OrderDetailId;
                clsOrder.OrderItemsLst = OrderItems.ToString();
                insertorders = clsOrder.SaveUnPickedOrderItems();
                if (insertorders == false)
                {
                    throw new Exception();
                }

            }
            catch (Exception ex)
            {
                _log.Error("Error:OrderClass:SaveUnPickedOrderItems:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                _log.Trace("OrderClass:SaveUnPickedOrderItems:Leaving");
            }
            return insertorders;
        }
        

        public bool UpdateUnPickedOrders(List<string> lstCustomerUniqueIds, int dataOwnerID)
        {
            bool updateorderitems = false;
            try
            {
                _log.Trace("OrderClass:UpdateUnPickedOrders Entering");

                OrderMaster clsOrder = new OrderMaster();
                string OrderItems = string.Empty;

                OrderItems = string.Join(",", lstCustomerUniqueIds.Select(i => i.ToString()).ToArray());

                clsOrder.DataOwnerID = dataOwnerID;
                //clsOrder.UserID = Convert.ToInt32(userid); 
                clsOrder.OrderItemsLst = OrderItems.ToString();
                updateorderitems = clsOrder.UpdateUnPickedOrderItems();
                if (updateorderitems == false)
                {
                    throw new Exception();
                }

            }
            catch (Exception ex)
            {
                _log.Error("Error:OrderClass:UpdateUnPickedOrders:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                _log.Trace("OrderClass:UpdateUnPickedOrders:Leaving");
            }
            return updateorderitems;
        }


        public List<KTOrderMaster> GetOrderList(int DataOwnerID, int UserID, int HHID)
        {
            List<KTOrderMaster> _lstOrderDetails = null;

            try
            {
                _log.Trace("OrderClass:GetOrderList Entering");
                _lstOrderDetails = FillOrderList(DataOwnerID, UserID, HHID);

            }
            catch (Exception ex)
            {
                _log.Error("Error:OrderClass:GetOrderList:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                _log.Trace("OrderClass:GetOrderList:Leaving");
            }

            return _lstOrderDetails;

        }

        public List<KTOrderDetails> GetWorkOrderDetailList(int DataOwnerID, long OrderID, int HHID, int UserID, out string ErrMessage)
        {
            List<KTOrderDetails> _lstOrderDetails = null;
            ErrMessage = string.Empty;
            try
            {
                _log.Trace("OrderClass:GetWorkOrderDetailList Entering");
                _lstOrderDetails = FillWorkOrderDetailslist(DataOwnerID, OrderID, HHID, UserID, out ErrMessage);

            }
            catch (Exception ex)
            {
                _log.Error("Error:OrderClass:GetWorkOrderDetailList:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                _log.Trace("OrderClass:GetWorkOrderDetailList:Leaving");
            }

            return _lstOrderDetails;

        }

        public KTOrderDetails GetNextWODetails(int UserID, int DataOwnerID, long OrderId, int HHID, out string ErrMessage)
        {
            KTOrderDetails _OrderDetails = null;
            ErrMessage = "";
            try
            {
                _log.Trace("OrderClass:GetNextWODetails Entering");
                _OrderDetails = FillOrderDetails(DataOwnerID, OrderId, HHID, UserID, out ErrMessage);
            }
            catch (Exception ex)
            {
                _log.Error("Error:OrderClass:GetNextWODetails:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                ErrMessage = ex.Message;
                throw new Exception(ex.Message);
            }
            finally
            {
                _log.Trace("OrderClass:GetNextWODetails:Leaving");
            }

            return _OrderDetails;
        }

        public List<KTOrderDetails> GetUnPickedOrderItems(int UserID, int DataOwnerID, string SalesOrderNo, string LineOrderNo, string WorkOrderNo)
        {
            List<KTOrderDetails> _OrderDetails = null;
            try
            {
                _log.Trace("OrderClass:GetUnPickedOrderItems Entering");
                _OrderDetails = FillUnPickOrderItems(UserID, DataOwnerID, SalesOrderNo, LineOrderNo, WorkOrderNo);
            }
            catch (Exception ex)
            {
                _log.Error("Error:OrderClass:GetUnPickedOrderItems:: " + ex.Message + Environment.NewLine + ex.StackTrace);

                throw new Exception(ex.Message);
            }
            finally
            {
                _log.Trace("OrderClass:GetUnPickedOrderItems:Leaving");
            }

            return _OrderDetails;
        }


        public bool AllottOrder(int DataOwnerId, int UserID, long OrderId, out string ErrMessage)
        {
            bool result = false;
            ErrMessage = "";

            try
            {
                _log.Trace("OrderClass:AllottOrder Entering");

                OrderMaster objOrderMaster = new OrderMaster();
                objOrderMaster.UserID = UserID;
                objOrderMaster.DataOwnerID = DataOwnerId;
                objOrderMaster.OrderID = OrderId;
                result = objOrderMaster.AllotOrder();

            }
            catch (Exception ex)
            {
                _log.Error("Error:OrderClass:AllottOrder:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                ErrMessage = ex.Message;
                throw new Exception(ex.Message);
            }
            finally
            {
                _log.Trace("OrderClass:AllottOrder:Leaving");
            }

            return true;
        }

        public List<KTOrderMaster> FillOrderList(int DataOwnerId, int UserID, int HHID)
        {
            List<KTOrderMaster> _lstOrderDetails = new List<KTOrderMaster>(); ;
            KTOrderMaster _orderDetail = null;

            try
            {
                _log.Trace("OrderClass:FillOrderList Entering");

                DataTable dtApporvedOrder = new DataTable();
                OrderMaster objOrderMaster = new OrderMaster();
                objOrderMaster.UserID = UserID;
                objOrderMaster.DataOwnerID = DataOwnerId;
                objOrderMaster.HHID = HHID;

                dtApporvedOrder = objOrderMaster.SelectApprovedOrder();

                if (dtApporvedOrder != null && dtApporvedOrder.Rows.Count > 0)
                {

                    int orderID = 0, orderStatusID = 0, orderTypeID = 0, approvedBy = 0, createdBy = 0, updatedBy = 0, assignedTo = 0;
                    string orderType = "", orderNo = "", lineOrderNo = "", orderStatus = "", assignedToUser = "";
                    DateTime schedulePickDate = DateTime.MinValue, createdDate = DateTime.MinValue, updatedDate = DateTime.MinValue;
                    bool isApproved = false;


                    foreach (DataRow dataRow in dtApporvedOrder.Rows)
                    {
                        if (dataRow["OrderID"] != null && dataRow["OrderID"].ToString() != string.Empty)
                            orderID = Convert.ToInt32(dataRow["OrderID"].ToString());
                        if (dataRow["OrderType"] != null && dataRow["OrderType"].ToString() != string.Empty)
                            orderType = Convert.ToString(dataRow["OrderType"]);
                        if (dataRow["OrderTypeId"] != null && dataRow["OrderTypeId"].ToString() != string.Empty)
                            orderTypeID = Convert.ToInt32(dataRow["OrderTypeId"].ToString());
                        if (dataRow["SalesOrderNo"] != null && dataRow["SalesOrderNo"].ToString() != string.Empty)
                            orderNo = Convert.ToString(dataRow["SalesOrderNo"]);
                        if (dataRow["LineOrderNo"] != null && dataRow["LineOrderNo"].ToString() != string.Empty)
                            lineOrderNo = Convert.ToString(dataRow["LineOrderNo"]);
                        if (dataRow["OrderStatus"] != null && dataRow["OrderStatus"].ToString() != string.Empty)
                            orderStatus = Convert.ToString(dataRow["OrderStatus"]);
                        if (dataRow["OrderStatusId"] != null && dataRow["OrderStatusId"].ToString() != string.Empty)
                            orderStatusID = Convert.ToInt32(dataRow["OrderStatusId"].ToString());
                        if (dataRow["SchedulePickDate"] != null && dataRow["SchedulePickDate"].ToString() != string.Empty)
                            schedulePickDate = DateTime.Parse(dataRow["SchedulePickDate"].ToString());
                        if (dataRow["IsApproved"] != null && dataRow["IsApproved"].ToString() != string.Empty)
                            isApproved = Convert.ToBoolean(dataRow["IsApproved"].ToString());
                        if (dataRow["ApprovedBy"] != null && dataRow["ApprovedBy"].ToString() != string.Empty)
                            approvedBy = Convert.ToInt32(dataRow["ApprovedBy"].ToString());
                        if (dataRow["CreatedDate"] != null && dataRow["CreatedDate"].ToString() != string.Empty)
                            createdDate = DateTime.Parse(dataRow["CreatedDate"].ToString());
                        if (dataRow["CreatedBy"] != null && dataRow["CreatedBy"].ToString() != string.Empty)
                            createdBy = Convert.ToInt32(dataRow["CreatedBy"].ToString());
                        if (dataRow["ApprovedBy"] != null && dataRow["ApprovedBy"].ToString() != string.Empty)
                            approvedBy = Convert.ToInt32(dataRow["ApprovedBy"].ToString());
                        if (dataRow["UpdatedDate"] != null && dataRow["UpdatedDate"].ToString() != string.Empty)
                            updatedDate = DateTime.Parse(dataRow["UpdatedDate"].ToString());
                        if (dataRow["UpdatedBy"] != null && dataRow["UpdatedBy"].ToString() != string.Empty)
                            updatedBy = Convert.ToInt32(dataRow["UpdatedBy"].ToString());
                        if (dataRow["AssignedTo"] != null && dataRow["AssignedTo"].ToString() != string.Empty)
                            assignedTo = Convert.ToInt32(dataRow["AssignedTo"]);
                        if (dataRow["UserName"] != null && dataRow["UserName"].ToString() != string.Empty)
                            assignedToUser = Convert.ToString(dataRow["UserName"]);

                        _orderDetail = new KTOrderMaster(orderID, orderType, orderTypeID, orderNo, lineOrderNo, schedulePickDate,
                                                            orderStatus, orderStatusID, isApproved, approvedBy, createdDate, createdBy, updatedDate, updatedBy, assignedTo, assignedToUser);

                        _lstOrderDetails.Add(_orderDetail);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error:OrderClass:FillOrderList:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                _log.Trace("OrderClass:FillOrderList:Leaving");
            }

            return _lstOrderDetails;



        }

        public List<KTOrderDetails> FillUnpickWorkOrderDetails(int DataOwnerId, string SalesOrderNo, string LineOrderNo)
        {
            List<KTOrderDetails> _lstOrderDetails = new List<KTOrderDetails>();
            KTOrderDetails _orderDetail = null;

            try
            {
                _log.Trace("OrderClass:FillUnpickWorkOrderDetails Entering");

                DataTable dtApporvedOrder = new DataTable();
                OrderMaster objOrderMaster = new OrderMaster();
                objOrderMaster.SalesOrderNo = SalesOrderNo;
                objOrderMaster.LineOrderNO = LineOrderNo;
                objOrderMaster.DataOwnerID = DataOwnerId;

                dtApporvedOrder = objOrderMaster.SelectPickedOrderItems();

                if (dtApporvedOrder != null && dtApporvedOrder.Rows.Count > 0)
                {

                    long orderDetailId = 0, productID = 0, binCatId = 0, presentQuantity = 0;
                    int orderID = 0, orderStatusID = 0, orderTypeID = 0, approvedBy = 0, createdBy = 0, updatedBy = 0, pickQty = 0, pickedBy = 0;
                    string orderType = "", orderNo = "", lineOrderNo = "", orderStatus = "", comment = "";
                    string workOrderNo = "", productName = "", binCat = "";
                    DateTime schedulePickDate = DateTime.MinValue, createdDate = DateTime.MinValue, updatedDate = DateTime.MinValue;
                    bool isApproved = false, IsTopUp = false;



                    foreach (DataRow dataRow in dtApporvedOrder.Rows)
                    {
                        if (dataRow["OrderID"] != null && dataRow["OrderID"].ToString() != string.Empty)
                            orderID = Convert.ToInt32(dataRow["OrderID"].ToString());
                        if (dataRow["OrderType"] != null && dataRow["OrderType"].ToString() != string.Empty)
                            orderType = Convert.ToString(dataRow["OrderType"]);
                        if (dataRow["OrderTypeId"] != null && dataRow["OrderTypeId"].ToString() != string.Empty)
                            orderTypeID = Convert.ToInt32(dataRow["OrderTypeId"].ToString());
                        if (dataRow["SalesOrderNo"] != null && dataRow["SalesOrderNo"].ToString() != string.Empty)
                            orderNo = Convert.ToString(dataRow["SalesOrderNo"]);
                        if (dataRow["LineOrderNo"] != null && dataRow["LineOrderNo"].ToString() != string.Empty)
                            lineOrderNo = Convert.ToString(dataRow["LineOrderNo"]);
                        if (dataRow["SchedulePickDate"] != null && dataRow["SchedulePickDate"].ToString() != string.Empty)
                            schedulePickDate = DateTime.Parse(dataRow["SchedulePickDate"].ToString());
                        if (dataRow["IsApproved"] != null && dataRow["IsApproved"].ToString() != string.Empty)
                            isApproved = Convert.ToBoolean(dataRow["IsApproved"]);
                        if (dataRow["ApprovedBy"] != null && dataRow["ApprovedBy"].ToString() != string.Empty)
                            approvedBy = Convert.ToInt32(dataRow["ApprovedBy"]);
                        if (dataRow["Comment"] != null && dataRow["Comment"].ToString() != string.Empty)
                            comment = Convert.ToString(dataRow["Comment"]);
                        if (dataRow["CreatedBy"] != null && dataRow["CreatedBy"].ToString() != string.Empty)
                            createdBy = Convert.ToInt32(dataRow["CreatedBy"]);
                        if (dataRow["CreatedDate"] != null && dataRow["CreatedDate"].ToString() != string.Empty)
                            createdDate = DateTime.Parse(dataRow["CreatedDate"].ToString());
                        if (dataRow["UpdatedBy"] != null && dataRow["UpdatedBy"].ToString() != string.Empty)
                            updatedBy = Convert.ToInt32(dataRow["UpdatedBy"]);
                        if (dataRow["UpdatedDate"] != null && dataRow["UpdatedDate"].ToString() != string.Empty)
                            updatedDate = DateTime.Parse(dataRow["UpdatedDate"].ToString());
                        if (dataRow["OrderDetailId"] != null && dataRow["OrderDetailId"].ToString() != string.Empty)
                            orderDetailId = Convert.ToInt64(dataRow["OrderDetailId"]);
                        if (dataRow["WorkOrderNo"] != null && dataRow["WorkOrderNo"].ToString() != string.Empty)
                            workOrderNo = Convert.ToString(dataRow["WorkOrderNo"]);
                        if (dataRow["OrderStatus"] != null && dataRow["OrderStatus"].ToString() != string.Empty)
                            orderStatus = Convert.ToString(dataRow["OrderStatus"]);
                        if (dataRow["OrderStatusId"] != null && dataRow["OrderStatusId"].ToString() != string.Empty)
                            orderStatusID = Convert.ToInt32(dataRow["OrderStatusId"].ToString());
                        if (dataRow["PickQty"] != null && dataRow["PickQty"].ToString() != string.Empty)
                            pickQty = Convert.ToInt32(dataRow["PickQty"].ToString());
                        if (dataRow["ProductID"] != null && dataRow["ProductID"].ToString() != string.Empty)
                            productID = Convert.ToInt64(dataRow["ProductID"]);
                        if (dataRow["ProductName"] != null && dataRow["ProductName"].ToString() != string.Empty)
                            productName = Convert.ToString(dataRow["ProductName"]);
                        if (dataRow["SKU_ID"] != null && dataRow["SKU_ID"].ToString() != string.Empty)
                            binCatId = Convert.ToInt64(dataRow["SKU_ID"]);
                        if (dataRow["ProductSKU"] != null && dataRow["ProductSKU"].ToString() != string.Empty)
                            binCat = Convert.ToString(dataRow["ProductSKU"]);
                        if (dataRow["IsTopUp"] != null && dataRow["IsTopUp"].ToString() != string.Empty)
                            IsTopUp = Convert.ToBoolean(dataRow["IsTopUp"]);
                        //if (dataRow["presentQuantity"] != null && dataRow["presentQuantity"].ToString() != string.Empty)
                        //    presentQuantity = Convert.ToInt64(dataRow["PresentQuantity"]);
                        if (dataRow["PickedBy"] != null && dataRow["PickedBy"].ToString() != string.Empty)
                            pickedBy = Convert.ToInt32(dataRow["PickedBy"]);

                        _orderDetail = new KTOrderDetails(orderID, orderType, orderTypeID, orderNo, lineOrderNo, schedulePickDate
                                                          , orderStatus, orderStatusID, comment, isApproved, approvedBy, orderDetailId, workOrderNo, pickQty
                                                          , IsTopUp, presentQuantity, productID, productName, binCatId, binCat, null, null, pickedBy, DataOwnerId, createdDate
                                                          , createdBy, updatedDate, updatedBy);

                        _lstOrderDetails.Add(_orderDetail);

                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error:OrderClass:FillUnpickWorkOrderDetails:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                _log.Trace("OrderClass:FillUnpickWorkOrderDetails:Leaving");
            }

            return _lstOrderDetails;
        }

        public List<KTOrderDetails> FillUnPickOrderItems(int UserID, int DataOwnerID, string SalesOrderNo, string LineOrderNo, string WorkOrderNo)
        {

            List<KTOrderDetails> _lstOrderDetails = new List<KTOrderDetails>();
            KTOrderDetails _orderDetail = null;
            try
            {
                DataTable dtUnpickOrder = new DataTable();
                OrderMaster objOrderMaster = new OrderMaster();
                objOrderMaster.WorkOrderNo = WorkOrderNo;
                objOrderMaster.UserID = UserID;
                objOrderMaster.SalesOrderNo = SalesOrderNo;
                objOrderMaster.DataOwnerID = DataOwnerID;
                objOrderMaster.LineOrderNO = LineOrderNo;

                dtUnpickOrder = objOrderMaster.SelectUnpickedOrderDetails();

                if (dtUnpickOrder != null && dtUnpickOrder.Rows.Count > 0)
                {

                    long orderDetailId = 0, productID = 0, binCatId = 0, presentQuantity = 0;
                    int orderID = 0, orderStatusID = 0, orderTypeID = 0, approvedBy = 0, createdBy = 0, updatedBy = 0, pickQty = 0, pickedBy = 0;
                    string orderType = "", orderNo = "", lineOrderNo = "", orderStatus = "", comment = "";
                    string workOrderNo = "", productName = "", binCat = "";
                    DateTime schedulePickDate = DateTime.MinValue, createdDate = DateTime.MinValue, updatedDate = DateTime.MinValue;
                    bool isApproved = false, IsTopUp = false;

                    Dictionary<long, List<string>> OrderdeatilBintapes = new Dictionary<long, List<string>>();

                    foreach (DataRow dataRow in dtUnpickOrder.Rows)
                    {
                        string binTapeId = string.Empty;

                        if (dataRow["OrderID"] != null && dataRow["OrderID"].ToString() != string.Empty)
                            orderID = Convert.ToInt32(dataRow["OrderID"].ToString());
                        if (dataRow["OrderType"] != null && dataRow["OrderType"].ToString() != string.Empty)
                            orderType = Convert.ToString(dataRow["OrderType"]);
                        if (dataRow["OrderTypeId"] != null && dataRow["OrderTypeId"].ToString() != string.Empty)
                            orderTypeID = Convert.ToInt32(dataRow["OrderTypeId"].ToString());
                        if (dataRow["SalesOrderNo"] != null && dataRow["SalesOrderNo"].ToString() != string.Empty)
                            orderNo = Convert.ToString(dataRow["SalesOrderNo"]);
                        if (dataRow["LineOrderNo"] != null && dataRow["LineOrderNo"].ToString() != string.Empty)
                            lineOrderNo = Convert.ToString(dataRow["LineOrderNo"]);
                        if (dataRow["SchedulePickDate"] != null && dataRow["SchedulePickDate"].ToString() != string.Empty)
                            schedulePickDate = DateTime.Parse(dataRow["SchedulePickDate"].ToString());
                        if (dataRow["IsApproved"] != null && dataRow["IsApproved"].ToString() != string.Empty)
                            isApproved = Convert.ToBoolean(dataRow["IsApproved"]);
                        if (dataRow["ApprovedBy"] != null && dataRow["ApprovedBy"].ToString() != string.Empty)
                            approvedBy = Convert.ToInt32(dataRow["ApprovedBy"]);
                        if (dataRow["Comment"] != null && dataRow["Comment"].ToString() != string.Empty)
                            comment = Convert.ToString(dataRow["Comment"]);
                        if (dataRow["CreatedBy"] != null && dataRow["CreatedBy"].ToString() != string.Empty)
                            createdBy = Convert.ToInt32(dataRow["CreatedBy"]);
                        if (dataRow["CreatedDate"] != null && dataRow["CreatedDate"].ToString() != string.Empty)
                            createdDate = DateTime.Parse(dataRow["CreatedDate"].ToString());
                        if (dataRow["UpdatedBy"] != null && dataRow["UpdatedBy"].ToString() != string.Empty)
                            updatedBy = Convert.ToInt32(dataRow["UpdatedBy"]);
                        if (dataRow["UpdatedDate"] != null && dataRow["UpdatedDate"].ToString() != string.Empty)
                            updatedDate = DateTime.Parse(dataRow["UpdatedDate"].ToString());
                        if (dataRow["OrderDetailId"] != null && dataRow["OrderDetailId"].ToString() != string.Empty)
                            orderDetailId = Convert.ToInt64(dataRow["OrderDetailId"]);

                        if (dataRow["WorkOrderNo"] != null && dataRow["WorkOrderNo"].ToString() != string.Empty)
                            workOrderNo = Convert.ToString(dataRow["WorkOrderNo"]);
                        if (dataRow["OrderStatus"] != null && dataRow["OrderStatus"].ToString() != string.Empty)
                            orderStatus = Convert.ToString(dataRow["OrderStatus"]);
                        if (dataRow["OrderStatusId"] != null && dataRow["OrderStatusId"].ToString() != string.Empty)
                            orderStatusID = Convert.ToInt32(dataRow["OrderStatusId"].ToString());
                        if (dataRow["PickQty"] != null && dataRow["PickQty"].ToString() != string.Empty)
                            pickQty = Convert.ToInt32(dataRow["PickQty"].ToString());
                        if (dataRow["ProductID"] != null && dataRow["ProductID"].ToString() != string.Empty)
                            productID = Convert.ToInt64(dataRow["ProductID"]);
                        if (dataRow["ProductName"] != null && dataRow["ProductName"].ToString() != string.Empty)
                            productName = Convert.ToString(dataRow["ProductName"]);
                        if (dataRow["SKU_ID"] != null && dataRow["SKU_ID"].ToString() != string.Empty)
                            binCatId = Convert.ToInt64(dataRow["SKU_ID"]);
                        if (dataRow["ProductSKU"] != null && dataRow["ProductSKU"].ToString() != string.Empty)
                            binCat = Convert.ToString(dataRow["ProductSKU"]);
                        if (dataRow["IsTopUp"] != null && dataRow["IsTopUp"].ToString() != string.Empty)
                            IsTopUp = Convert.ToBoolean(dataRow["IsTopUp"]);
                        //if (dataRow["presentQuantity"] != null && dataRow["presentQuantity"].ToString() != string.Empty)
                        //    presentQuantity = Convert.ToInt64(dataRow["PresentQuantity"]);
                        if (dataRow["PickedBy"] != null && dataRow["PickedBy"].ToString() != string.Empty)
                            pickedBy = Convert.ToInt32(dataRow["PickedBy"]);
                        if (dataRow["ItemID"] != null && dataRow["ItemID"].ToString() != string.Empty)
                            binTapeId = Convert.ToString(dataRow["ItemID"]);


                        if (OrderdeatilBintapes.ContainsKey(orderDetailId))
                        {
                            OrderdeatilBintapes[orderDetailId].Add(binTapeId);
                        }
                        else
                        {
                            List<string> binTapeIds = new List<string>();
                            binTapeIds.Add(binTapeId);
                            OrderdeatilBintapes[orderDetailId] = binTapeIds;
                        }

                        _orderDetail = new KTOrderDetails(orderID, orderType, orderTypeID, orderNo, lineOrderNo, schedulePickDate
                                                , orderStatus, orderStatusID, comment, isApproved, approvedBy, orderDetailId, workOrderNo, pickQty
                                                , IsTopUp, 0, productID, productName, binCatId, binCat, OrderdeatilBintapes[orderDetailId], null
                                                , pickedBy, DataOwnerID, createdDate, createdBy, updatedDate, updatedBy);
                        bool isPresent = false;
                        foreach (KTOrderDetails kt in _lstOrderDetails)
                        {
                            if (kt.OrderDetailId == _orderDetail.OrderDetailId)
                            {
                                isPresent = true;
                                break;
                            }
                        }
                        if (!isPresent)
                        {
                            _lstOrderDetails.Add(_orderDetail);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error:OrderClass:FillUnPickOrderItems:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                _log.Trace("OrderClass:FillUnPickOrderItems:Leaving");
            }

            return _lstOrderDetails;

        }

        public List<KTOrderDetails> FillWorkOrderDetailslist(int DataOwnerID, long OrderID, int HHID, int UserID, out string ErrMessage)
        {

            List<KTOrderDetails> _lstOrderDetails = new List<KTOrderDetails>();
            KTOrderDetails _orderDetail = null;
            KTInternalOrderDetails _internalOrderdetail = null;
            try
            {
                ErrMessage = string.Empty;
                _log.Trace("OrderClass:FillWorkOrderDetailslist Entering");
                DataTable dtOrder = new DataTable();
                OrderMaster objOrderMaster = new OrderMaster();
                objOrderMaster.OrderID = OrderID;
                objOrderMaster.UserID = UserID;
                objOrderMaster.DataOwnerID = DataOwnerID;
                objOrderMaster.HHID = HHID;

                dtOrder = objOrderMaster.SelectOrderDetailsList(out ErrMessage);

                if (dtOrder != null && dtOrder.Rows.Count > 0)
                {

                    long orderDetailId = 0, productID = 0, binCatId = 0, presentQuantity = 0;
                    int orderID = 0, orderStatusID = 0, orderTypeID = 0, approvedBy = 0, createdBy = 0, updatedBy = 0, pickQty = 0, pickedBy = 0;
                    string orderType = "", orderNo = "", lineOrderNo = "", orderStatus = "", comment = "",binTapeIds = "",location="";
                    string workOrderNo = "", productName = "", binCat = "";
                    DateTime schedulePickDate = DateTime.MinValue, createdDate = DateTime.MinValue, updatedDate = DateTime.MinValue;
                    bool isApproved = false, IsTopUp = false;

                    Dictionary<string, KTInternalOrderDetails> BinTapes_Locations = new Dictionary<string, KTInternalOrderDetails>();
                    List<string> bintapes = new List<string>();
                    List<string> BintapeIds = new List<string>();
                    foreach (DataRow dataRow in dtOrder.Rows)
                    {
                        string binTapeId = string.Empty;

                        if (dataRow["OrderID"] != null && dataRow["OrderID"].ToString() != string.Empty)
                            orderID = Convert.ToInt32(dataRow["OrderID"].ToString());
                        if (dataRow["OrderType"] != null && dataRow["OrderType"].ToString() != string.Empty)
                            orderType = Convert.ToString(dataRow["OrderType"]);
                        if (dataRow["OrderTypeId"] != null && dataRow["OrderTypeId"].ToString() != string.Empty)
                            orderTypeID = Convert.ToInt32(dataRow["OrderTypeId"].ToString());
                        if (dataRow["SalesOrderNo"] != null && dataRow["SalesOrderNo"].ToString() != string.Empty)
                            orderNo = Convert.ToString(dataRow["SalesOrderNo"]);
                        if (dataRow["LineOrderNo"] != null && dataRow["LineOrderNo"].ToString() != string.Empty)
                            lineOrderNo = Convert.ToString(dataRow["LineOrderNo"]);
                        if (dataRow["SchedulePickDate"] != null && dataRow["SchedulePickDate"].ToString() != string.Empty)
                            schedulePickDate = DateTime.Parse(dataRow["SchedulePickDate"].ToString());
                        if (dataRow["IsApproved"] != null && dataRow["IsApproved"].ToString() != string.Empty)
                            isApproved = Convert.ToBoolean(dataRow["IsApproved"]);
                        if (dataRow["ApprovedBy"] != null && dataRow["ApprovedBy"].ToString() != string.Empty)
                            approvedBy = Convert.ToInt32(dataRow["ApprovedBy"]);
                        if (dataRow["Comment"] != null && dataRow["Comment"].ToString() != string.Empty)
                            comment = Convert.ToString(dataRow["Comment"]);
                        if (dataRow["CreatedBy"] != null && dataRow["CreatedBy"].ToString() != string.Empty)
                            createdBy = Convert.ToInt32(dataRow["CreatedBy"]);
                        if (dataRow["CreatedDate"] != null && dataRow["CreatedDate"].ToString() != string.Empty)
                            createdDate = DateTime.Parse(dataRow["CreatedDate"].ToString());
                        if (dataRow["UpdatedBy"] != null && dataRow["UpdatedBy"].ToString() != string.Empty)
                            updatedBy = Convert.ToInt32(dataRow["UpdatedBy"]);
                        if (dataRow["UpdatedDate"] != null && dataRow["UpdatedDate"].ToString() != string.Empty)
                            updatedDate = DateTime.Parse(dataRow["UpdatedDate"].ToString());
                        if (dataRow["OrderDetailId"] != null && dataRow["OrderDetailId"].ToString() != string.Empty)
                            orderDetailId = Convert.ToInt64(dataRow["OrderDetailId"]);
                        if (dataRow["WorkOrderNo"] != null && dataRow["WorkOrderNo"].ToString() != string.Empty)
                            workOrderNo = Convert.ToString(dataRow["WorkOrderNo"]);
                        if (dataRow["OrderStatus"] != null && dataRow["OrderStatus"].ToString() != string.Empty)
                            orderStatus = Convert.ToString(dataRow["OrderStatus"]);
                        if (dataRow["OrderStatusId"] != null && dataRow["OrderStatusId"].ToString() != string.Empty)
                            orderStatusID = Convert.ToInt32(dataRow["OrderStatusId"].ToString());
                        if (dataRow["PickQty"] != null && dataRow["PickQty"].ToString() != string.Empty)
                            pickQty = Convert.ToInt32(dataRow["PickQty"].ToString());
                        if (dataRow["ProductID"] != null && dataRow["ProductID"].ToString() != string.Empty)
                            productID = Convert.ToInt64(dataRow["ProductID"]);
                        if (dataRow["ProductName"] != null && dataRow["ProductName"].ToString() != string.Empty)
                            productName = Convert.ToString(dataRow["ProductName"]);
                        if (dataRow["SKU_ID"] != null && dataRow["SKU_ID"].ToString() != string.Empty)
                            binCatId = Convert.ToInt64(dataRow["SKU_ID"]);
                        if (dataRow["ProductSKU"] != null && dataRow["ProductSKU"].ToString() != string.Empty)
                            binCat = Convert.ToString(dataRow["ProductSKU"]);
                        if (dataRow["IsTopUp"] != null && dataRow["IsTopUp"].ToString() != string.Empty)
                            IsTopUp = Convert.ToBoolean(dataRow["IsTopUp"]);
                        //if (dataRow["presentQuantity"] != null && dataRow["presentQuantity"].ToString() != string.Empty)
                        //    presentQuantity = Convert.ToInt64(dataRow["PresentQuantity"]);
                        if (dataRow["PickedBy"] != null && dataRow["PickedBy"].ToString() != string.Empty)
                            pickedBy = Convert.ToInt32(dataRow["PickedBy"]);
                        //if (dataRow["ItemID"] != null && dataRow["ItemID"].ToString() != string.Empty)
                        //    binTapeId = Convert.ToString(dataRow["ItemID"]);

                        if (orderType == "External")
                        {
                            BintapeIds = null;
                            _orderDetail = new KTOrderDetails(orderID, orderType, orderTypeID, orderNo, lineOrderNo, schedulePickDate
                                                    , orderStatus, orderStatusID, comment, isApproved, approvedBy, orderDetailId, workOrderNo, pickQty
                                                    , IsTopUp, 0, productID, productName, binCatId, binCat, BintapeIds, null, pickedBy, DataOwnerID, createdDate
                                                    , createdBy, updatedDate, updatedBy);

                        }
                        if (orderType == "Internal")
                        {
                              
                             if (dataRow["ItemID"] != null && dataRow["ItemID"].ToString() != string.Empty)
                                binTapeIds = Convert.ToString(dataRow["ItemID"]);
                             BintapeIds.Add(binTapeIds);


                              if (dataRow["Location"] != null && dataRow["Location"].ToString() != string.Empty)
                              location = Convert.ToString(dataRow["Location"]);

                              _internalOrderdetail = new KTInternalOrderDetails(binTapeIds, location, pickQty, productName, binCat);

                             if (dataRow["ItemID"] != null && dataRow["ItemID"].ToString() != string.Empty)
                               bintapes.Add(Convert.ToString(dataRow["ItemID"]));
                             if (bintapes != null && bintapes.Count > 0)
                             {
                                foreach (string field in bintapes)
                                {
                                   if (!BinTapes_Locations.ContainsKey(field))
                                       BinTapes_Locations[field] = _internalOrderdetail;
                                }
                            }                             

                            _orderDetail = new KTOrderDetails(orderID, orderType, orderTypeID, orderNo, lineOrderNo, schedulePickDate
                                                           , orderStatus, orderStatusID, comment, isApproved, approvedBy, orderDetailId, workOrderNo, pickQty
                                                           , IsTopUp, 0, productID, productName, binCatId, binCat, BintapeIds, BinTapes_Locations
                                                           , pickedBy, DataOwnerID, createdDate, createdBy, updatedDate, updatedBy);
                        }

                        _lstOrderDetails.Add(_orderDetail);

                    }

                }
            }
            catch (Exception ex)
            {
                _log.Error("Error:OrderClass:FillWorkOrderDetailslist:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                _log.Trace("OrderClass:FillWorkOrderDetailslist:Leaving");
            }

            return _lstOrderDetails;

        }

        public bool AssignWorkOrderToHH(int DataOwnerID, long OrderDetailID, int HHID, int UserID, out string ErrMessage)
        {
            
            bool result = false;
            try
            {
                _log.Trace("OrderClass:AssignWorkOrderToHH Entering");
                OrderMaster clsOrder = new OrderMaster();
                ErrMessage = string.Empty;
                clsOrder.HHID = HHID;
                clsOrder.OrderDetailID = OrderDetailID;
                clsOrder.DataOwnerID = DataOwnerID;
                clsOrder.UserID = UserID;
                result = clsOrder.UpdateOrderDetailsList(out ErrMessage);
                //if (result == false)
                //{
                //    throw new Exception();
                //}

            }
            catch (Exception ex)
            {
                _log.Error("Error:OrderClass:AssignWorkOrderToHH:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                _log.Trace("OrderClass:AssignWorkOrderToHH:Leaving");
            }
            return result;

        }

        public KTOrderDetails CheckWorkOrderbyHH(int UserID,int DataOwnerId, int HHID)
        {

            KTOrderDetails _orderDetail = null;
            KTInternalOrderDetails _internalOrderdetail = null;
            try
            {
                _log.Trace("OrderClass:CheckWorkOrderbyHH Entering");

                DataTable dtAssignedOrder = new DataTable();
                OrderMaster objOrderMaster = new OrderMaster();
                objOrderMaster.UserID = UserID;
                objOrderMaster.HHID = HHID;
                objOrderMaster.DataOwnerID = DataOwnerId;

                dtAssignedOrder = objOrderMaster.SelectAssignedWorkOrders();

                if (dtAssignedOrder != null && dtAssignedOrder.Rows.Count > 0)
                {

                    long orderDetailId = 0, productID = 0, binCatId = 0, presentQuantity = 0;
                    int orderID = 0, orderStatusID = 0, orderTypeID = 0, approvedBy = 0, createdBy = 0, updatedBy = 0, pickQty = 0, pickedBy = 0;
                    string orderType = "", orderNo = "", lineOrderNo = "", orderStatus = "", comment = "", binTapeIds = "", location = "";
                    string workOrderNo = "", productName = "", binCat = "";
                    DateTime schedulePickDate = DateTime.MinValue, createdDate = DateTime.MinValue, updatedDate = DateTime.MinValue;
                    bool isApproved = false, IsTopUp = false;

                    Dictionary<string, KTInternalOrderDetails> BinTapes_Locations = new Dictionary<string, KTInternalOrderDetails>();
                    List<string> bintapes = new List<string>();
                    List<string> BintapeIds = new List<string>();
                    foreach (DataRow dataRow in dtAssignedOrder.Rows)
                    {
                        if (dataRow["OrderID"] != null && dataRow["OrderID"].ToString() != string.Empty)
                            orderID = Convert.ToInt32(dataRow["OrderID"].ToString());
                        if (dataRow["OrderType"] != null && dataRow["OrderType"].ToString() != string.Empty)
                            orderType = Convert.ToString(dataRow["OrderType"]);
                        if (dataRow["OrderTypeId"] != null && dataRow["OrderTypeId"].ToString() != string.Empty)
                            orderTypeID = Convert.ToInt32(dataRow["OrderTypeId"].ToString());
                        if (dataRow["SalesOrderNo"] != null && dataRow["SalesOrderNo"].ToString() != string.Empty)
                            orderNo = Convert.ToString(dataRow["SalesOrderNo"]);
                        if (dataRow["LineOrderNo"] != null && dataRow["LineOrderNo"].ToString() != string.Empty)
                            lineOrderNo = Convert.ToString(dataRow["LineOrderNo"]);
                        if (dataRow["SchedulePickDate"] != null && dataRow["SchedulePickDate"].ToString() != string.Empty)
                            schedulePickDate = DateTime.Parse(dataRow["SchedulePickDate"].ToString());
                        if (dataRow["IsApproved"] != null && dataRow["IsApproved"].ToString() != string.Empty)
                            isApproved = Convert.ToBoolean(dataRow["IsApproved"]);
                        if (dataRow["ApprovedBy"] != null && dataRow["ApprovedBy"].ToString() != string.Empty)
                            approvedBy = Convert.ToInt32(dataRow["ApprovedBy"]);
                        if (dataRow["Comment"] != null && dataRow["Comment"].ToString() != string.Empty)
                            comment = Convert.ToString(dataRow["Comment"]);
                        if (dataRow["CreatedBy"] != null && dataRow["CreatedBy"].ToString() != string.Empty)
                            createdBy = Convert.ToInt32(dataRow["CreatedBy"]);
                        if (dataRow["CreatedDate"] != null && dataRow["CreatedDate"].ToString() != string.Empty)
                            createdDate = DateTime.Parse(dataRow["CreatedDate"].ToString());
                        if (dataRow["UpdatedBy"] != null && dataRow["UpdatedBy"].ToString() != string.Empty)
                            updatedBy = Convert.ToInt32(dataRow["UpdatedBy"]);
                        if (dataRow["UpdatedDate"] != null && dataRow["UpdatedDate"].ToString() != string.Empty)
                            updatedDate = DateTime.Parse(dataRow["UpdatedDate"].ToString());
                        if (dataRow["OrderDetailId"] != null && dataRow["OrderDetailId"].ToString() != string.Empty)
                            orderDetailId = Convert.ToInt64(dataRow["OrderDetailId"]);
                        if (dataRow["WorkOrderNo"] != null && dataRow["WorkOrderNo"].ToString() != string.Empty)
                            workOrderNo = Convert.ToString(dataRow["WorkOrderNo"]);
                        if (dataRow["OrderStatus"] != null && dataRow["OrderStatus"].ToString() != string.Empty)
                            orderStatus = Convert.ToString(dataRow["OrderStatus"]);
                        if (dataRow["OrderStatusId"] != null && dataRow["OrderStatusId"].ToString() != string.Empty)
                            orderStatusID = Convert.ToInt32(dataRow["OrderStatusId"].ToString());
                        if (dataRow["PickQty"] != null && dataRow["PickQty"].ToString() != string.Empty)
                            pickQty = Convert.ToInt32(dataRow["PickQty"].ToString());
                        if (dataRow["ProductID"] != null && dataRow["ProductID"].ToString() != string.Empty)
                            productID = Convert.ToInt64(dataRow["ProductID"]);
                        if (dataRow["ProductName"] != null && dataRow["ProductName"].ToString() != string.Empty)
                            productName = Convert.ToString(dataRow["ProductName"]);
                        if (dataRow["SKU_ID"] != null && dataRow["SKU_ID"].ToString() != string.Empty)
                            binCatId = Convert.ToInt64(dataRow["SKU_ID"]);
                        if (dataRow["ProductSKU"] != null && dataRow["ProductSKU"].ToString() != string.Empty)
                            binCat = Convert.ToString(dataRow["ProductSKU"]);
                        if (dataRow["IsTopUp"] != null && dataRow["IsTopUp"].ToString() != string.Empty)
                            IsTopUp = Convert.ToBoolean(dataRow["IsTopUp"]);
                        //if (dataRow["presentQuantity"] != null && dataRow["presentQuantity"].ToString() != string.Empty)
                        //    presentQuantity = Convert.ToInt64(dataRow["PresentQuantity"]);
                        if (dataRow["PickedBy"] != null && dataRow["PickedBy"].ToString() != string.Empty)
                            pickedBy = Convert.ToInt32(dataRow["PickedBy"]);


                        if (orderType == "External")
                        {
                            BintapeIds = null;
                            _orderDetail = new KTOrderDetails(orderID, orderType, orderTypeID, orderNo, lineOrderNo, schedulePickDate
                                                    , orderStatus, orderStatusID, comment, isApproved, approvedBy, orderDetailId, workOrderNo, pickQty
                                                    , IsTopUp, 0, productID, productName, binCatId, binCat, BintapeIds, null, pickedBy, DataOwnerId, createdDate
                                                    , createdBy, updatedDate, updatedBy);

                        }
                        if (orderType == "Internal")
                        {

                            if (dataRow["ItemID"] != null && dataRow["ItemID"].ToString() != string.Empty)
                                binTapeIds = Convert.ToString(dataRow["ItemID"]);
                            BintapeIds.Add(binTapeIds);


                            if (dataRow["Location"] != null && dataRow["Location"].ToString() != string.Empty)
                                location = Convert.ToString(dataRow["Location"]);

                            _internalOrderdetail = new KTInternalOrderDetails(binTapeIds, location, pickQty,  productName, binCat);

                            if (dataRow["ItemID"] != null && dataRow["ItemID"].ToString() != string.Empty)
                                bintapes.Add(Convert.ToString(dataRow["ItemID"]));
                            if (bintapes != null && bintapes.Count > 0)
                            {
                                foreach (string field in bintapes)
                                {
                                    if (!BinTapes_Locations.ContainsKey(field))
                                        BinTapes_Locations[field] = _internalOrderdetail;
                                }
                            }

                            _orderDetail = new KTOrderDetails(orderID, orderType, orderTypeID, orderNo, lineOrderNo, schedulePickDate
                                                           , orderStatus, orderStatusID, comment, isApproved, approvedBy, orderDetailId, workOrderNo, pickQty
                                                           , IsTopUp, 0, productID, productName, binCatId, binCat, BintapeIds, BinTapes_Locations
                                                           , pickedBy, DataOwnerId, createdDate, createdBy, updatedDate, updatedBy);
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                _log.Error("Error:OrderClass:CheckWorkOrderbyHH:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                _log.Trace("OrderClass:CheckWorkOrderbyHH:Leaving");
            }

            return _orderDetail;
        }
 


        public KTOrderDetails FillOrderDetails(int DataOwnerId, long OrderId, int HHID, int UserID, out string ErrMessage)
        {

            KTOrderDetails _orderDetail = null;
            KTInternalOrderDetails _internalOrderdetail = null;
             ErrMessage = "";
            try
            {
                _log.Trace("OrderClass:FillOrderDetails Entering");

                DataTable dtApporvedOrder = new DataTable();
                OrderMaster objOrderMaster = new OrderMaster();
                objOrderMaster.OrderID = OrderId;
                objOrderMaster.UserID = UserID;
                objOrderMaster.HHID = HHID;
                objOrderMaster.DataOwnerID = DataOwnerId;

                dtApporvedOrder = objOrderMaster.SelectApprovedOrderDetails(out ErrMessage);

                if (dtApporvedOrder != null && dtApporvedOrder.Rows.Count > 0)
                {

                    long orderDetailId = 0, productID = 0, binCatId = 0, presentQuantity = 0;
                    int orderID = 0, orderStatusID = 0, orderTypeID = 0, approvedBy = 0, createdBy = 0, updatedBy = 0, pickQty = 0, pickedBy = 0;
                    string orderType = "", orderNo = "", lineOrderNo = "", orderStatus = "", comment = "", binTapeIds = "",location="";
                    string workOrderNo = "", productName = "", binCat = "";
                    DateTime schedulePickDate = DateTime.MinValue, createdDate = DateTime.MinValue, updatedDate = DateTime.MinValue;
                    bool isApproved = false, IsTopUp = false;


                    List<string> BintapeIds = new List<string>();

                    Dictionary<string, KTInternalOrderDetails> BinTapes_Locations = new Dictionary<string, KTInternalOrderDetails>();
                    List<string> bintapes = new List<string>();
                    foreach (DataRow dataRow in dtApporvedOrder.Rows)
                    {
                        if (dataRow["OrderID"] != null && dataRow["OrderID"].ToString() != string.Empty)
                            orderID = Convert.ToInt32(dataRow["OrderID"].ToString());
                        if (dataRow["OrderType"] != null && dataRow["OrderType"].ToString() != string.Empty)
                            orderType = Convert.ToString(dataRow["OrderType"]);
                        if (dataRow["OrderTypeId"] != null && dataRow["OrderTypeId"].ToString() != string.Empty)
                            orderTypeID = Convert.ToInt32(dataRow["OrderTypeId"].ToString());
                        if (dataRow["SalesOrderNo"] != null && dataRow["SalesOrderNo"].ToString() != string.Empty)
                            orderNo = Convert.ToString(dataRow["SalesOrderNo"]);
                        if (dataRow["LineOrderNo"] != null && dataRow["LineOrderNo"].ToString() != string.Empty)
                            lineOrderNo = Convert.ToString(dataRow["LineOrderNo"]);
                        if (dataRow["SchedulePickDate"] != null && dataRow["SchedulePickDate"].ToString() != string.Empty)
                            schedulePickDate = DateTime.Parse(dataRow["SchedulePickDate"].ToString());
                        if (dataRow["IsApproved"] != null && dataRow["IsApproved"].ToString() != string.Empty)
                            isApproved = Convert.ToBoolean(dataRow["IsApproved"]);
                        if (dataRow["ApprovedBy"] != null && dataRow["ApprovedBy"].ToString() != string.Empty)
                            approvedBy = Convert.ToInt32(dataRow["ApprovedBy"]);
                        if (dataRow["Comment"] != null && dataRow["Comment"].ToString() != string.Empty)
                            comment = Convert.ToString(dataRow["Comment"]);
                        if (dataRow["CreatedBy"] != null && dataRow["CreatedBy"].ToString() != string.Empty)
                            createdBy = Convert.ToInt32(dataRow["CreatedBy"]);
                        if (dataRow["CreatedDate"] != null && dataRow["CreatedDate"].ToString() != string.Empty)
                            createdDate = DateTime.Parse(dataRow["CreatedDate"].ToString());
                        if (dataRow["UpdatedBy"] != null && dataRow["UpdatedBy"].ToString() != string.Empty)
                            updatedBy = Convert.ToInt32(dataRow["UpdatedBy"]);
                        if (dataRow["UpdatedDate"] != null && dataRow["UpdatedDate"].ToString() != string.Empty)
                            updatedDate = DateTime.Parse(dataRow["UpdatedDate"].ToString());
                        if (dataRow["OrderDetailId"] != null && dataRow["OrderDetailId"].ToString() != string.Empty)
                            orderDetailId = Convert.ToInt64(dataRow["OrderDetailId"]);
                        if (dataRow["WorkOrderNo"] != null && dataRow["WorkOrderNo"].ToString() != string.Empty)
                            workOrderNo = Convert.ToString(dataRow["WorkOrderNo"]);
                        if (dataRow["OrderStatus"] != null && dataRow["OrderStatus"].ToString() != string.Empty)
                            orderStatus = Convert.ToString(dataRow["OrderStatus"]);
                        if (dataRow["OrderStatusId"] != null && dataRow["OrderStatusId"].ToString() != string.Empty)
                            orderStatusID = Convert.ToInt32(dataRow["OrderStatusId"].ToString());
                        if (dataRow["PickQty"] != null && dataRow["PickQty"].ToString() != string.Empty)
                            pickQty = Convert.ToInt32(dataRow["PickQty"].ToString());
                        if (dataRow["ProductID"] != null && dataRow["ProductID"].ToString() != string.Empty)
                            productID = Convert.ToInt64(dataRow["ProductID"]);
                        if (dataRow["ProductName"] != null && dataRow["ProductName"].ToString() != string.Empty)
                            productName = Convert.ToString(dataRow["ProductName"]);
                        if (dataRow["SKU_ID"] != null && dataRow["SKU_ID"].ToString() != string.Empty)
                            binCatId = Convert.ToInt64(dataRow["SKU_ID"]);
                        if (dataRow["ProductSKU"] != null && dataRow["ProductSKU"].ToString() != string.Empty)
                            binCat = Convert.ToString(dataRow["ProductSKU"]);
                        if (dataRow["IsTopUp"] != null && dataRow["IsTopUp"].ToString() != string.Empty)
                            IsTopUp = Convert.ToBoolean(dataRow["IsTopUp"]);
                        //if (dataRow["presentQuantity"] != null && dataRow["presentQuantity"].ToString() != string.Empty)
                        //    presentQuantity = Convert.ToInt64(dataRow["PresentQuantity"]);
                        if (dataRow["PickedBy"] != null && dataRow["PickedBy"].ToString() != string.Empty)
                            pickedBy = Convert.ToInt32(dataRow["PickedBy"]);

                        if (orderType == "External")
                        {
                            BintapeIds = null;
                            _orderDetail = new KTOrderDetails(orderID, orderType, orderTypeID, orderNo, lineOrderNo, schedulePickDate
                                                    , orderStatus, orderStatusID, comment, isApproved, approvedBy, orderDetailId, workOrderNo, pickQty
                                                    , IsTopUp, 0, productID, productName, binCatId, binCat, BintapeIds, null, pickedBy, DataOwnerId, createdDate
                                                    , createdBy, updatedDate, updatedBy);

                        }
                        if (orderType == "Internal")
                        {

                            if (dataRow["ItemID"] != null && dataRow["ItemID"].ToString() != string.Empty)
                                binTapeIds = Convert.ToString(dataRow["ItemID"]);
                            BintapeIds.Add(binTapeIds);


                            if (dataRow["Location"] != null && dataRow["Location"].ToString() != string.Empty)
                              location = Convert.ToString(dataRow["Location"]);

                            _internalOrderdetail = new KTInternalOrderDetails(binTapeIds, location, pickQty, productName, binCat);

                            if (dataRow["ItemID"] != null && dataRow["ItemID"].ToString() != string.Empty)
                                bintapes.Add(Convert.ToString(dataRow["ItemID"]));
                            if (bintapes != null && bintapes.Count > 0)
                            {
                                foreach (string field in bintapes)
                                {
                                    if (!BinTapes_Locations.ContainsKey(field))
                                        BinTapes_Locations[field] = _internalOrderdetail;
                                }
                            }

                            _orderDetail = new KTOrderDetails(orderID, orderType, orderTypeID, orderNo, lineOrderNo, schedulePickDate
                                                           , orderStatus, orderStatusID, comment, isApproved, approvedBy, orderDetailId, workOrderNo, pickQty
                                                           , IsTopUp, 0, productID, productName, binCatId, binCat, BintapeIds, BinTapes_Locations
                                                           , pickedBy, DataOwnerId, createdDate, createdBy, updatedDate, updatedBy);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error:OrderClass:FillOrderDetails:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                _log.Trace("OrderClass:FillOrderDetails:Leaving");
            }

            return _orderDetail;
        }

    }
}
