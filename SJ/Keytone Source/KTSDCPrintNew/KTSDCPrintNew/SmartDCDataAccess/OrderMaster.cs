using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using KTone.DAL.KTDBBaseLib;

namespace KTone.DAL.SmartDCDataAccess
{
    public class OrderMaster : DBInteractionBase
    {
        #region Class Member Declarations      
        private int _dataOwnerID, _createdBy, _orderTypeID, _topupQuantity, _userID, _hhID, _actualPickQty, _validateResult , _status , _orderStatusId;
        private string _salesorderNo, _lineOrderNo, _workOrderNo, _productfamily, _bincat, _quantity, _ordertype, _orderStatus, _errorMessege, _comment,_orderitems;
        private string _pickdate, _description, _createDate, _searchCriteria, _fromdate, _todate, _tnRPartNumber;
        
        private DateTime _schedulePickDate;
        private long _orderID, _orderDetailId, _productID, _binTapeID,_binCatID;
        private bool _isSinglePick;


        #endregion

        //INSERT THROUGH WEBSERVICE...
        public bool Insert()
        {
            _log.Trace("Entering Insert -  Table:OrderMaster  ");
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_OrderMaster_Insert]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            cmdToExecute.Connection = _mainConnection;

            try
            {

                cmdToExecute.Parameters.Add(new SqlParameter("@SalesOrderNo", SqlDbType.VarChar, 200, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _salesorderNo));
                cmdToExecute.Parameters.Add(new SqlParameter("@LineOrderNo", SqlDbType.VarChar, 200, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _lineOrderNo));
                cmdToExecute.Parameters.Add(new SqlParameter("@WorkOrderNo", SqlDbType.VarChar, 8000, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _workOrderNo));
                cmdToExecute.Parameters.Add(new SqlParameter("@Productfamily", SqlDbType.VarChar, 8000, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _productfamily));
                cmdToExecute.Parameters.Add(new SqlParameter("@BinCat", SqlDbType.VarChar, 8000, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _bincat));
                cmdToExecute.Parameters.Add(new SqlParameter("@Quantity", SqlDbType.VarChar, 8000, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _quantity));
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerId", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@CreatedBy", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _createdBy));
                cmdToExecute.Parameters.Add(new SqlParameter("@OrderType", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _ordertype));
                cmdToExecute.Parameters.Add(new SqlParameter("@OrderStatus", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _orderStatus));
                cmdToExecute.Parameters.Add(new SqlParameter("@SchedulePickDate", SqlDbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _schedulePickDate));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorMessege", SqlDbType.VarChar, 200, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorMessege));

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }

                cmdToExecute.ExecuteNonQuery();
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;
                _errorMessege = (String)cmdToExecute.Parameters["@ErrorMessege"].Value;

                if (_errorMessege != "")
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_OrderMaster_Insert' reported the Error: " + _errorMessege);
                }
                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_OrderMaster_Insert' reported the ErrorCode: " + _errorCode);
                }
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Insert : {0}", ex.Message);
                throw new Exception("OrderMaster::Insert::Error occured." + ex.Message, ex);
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                UpdateNotifyCacheUpdateTable();
                _log.Trace("Exiting Insert");
            }
        }



        public bool InsertOrderItems()
        {
            _log.Trace("Entering InsertOrderItems -  Table:OrderMaster  ");
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_OrderItemsPicked_Update]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            cmdToExecute.Connection = _mainConnection;

            try
            {
                
                cmdToExecute.Parameters.Add(new SqlParameter("@HHID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _hhID));
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerId", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _userID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ActuallyPickedQty", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _actualPickQty));
                cmdToExecute.Parameters.Add(new SqlParameter("@OrderDetailid", SqlDbType.BigInt, 8, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _orderDetailId));
                cmdToExecute.Parameters.Add(new SqlParameter("@OrderItems", SqlDbType.VarChar, 8000, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _orderitems));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode)); 
                 
               
                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }

                cmdToExecute.ExecuteNonQuery();
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;
                
                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_OrderItemsPicked_Update' reported the ErrorCode: " + _errorCode);
                }
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Insert : {0}", ex.Message);
                throw new Exception("OrderMaster::InsertOrderItems::Error occured." + ex.Message, ex);
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                UpdateNotifyCacheUpdateTable();
                _log.Trace("Exiting InsertOrderItems");
            }
        }


        public bool SaveUnPickedOrderItems()
        {
            _log.Trace("Entering SaveUnPickedOrderItems -  Table:OrderMaster  ");
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_OrderItemsUnPicked_Update]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerId", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _userID));
                cmdToExecute.Parameters.Add(new SqlParameter("@OrderDetailid", SqlDbType.BigInt, 8, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _orderDetailId));
                cmdToExecute.Parameters.Add(new SqlParameter("@BinTapeIDs", SqlDbType.VarChar, 8000, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _orderitems));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));


                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }

                cmdToExecute.ExecuteNonQuery();
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_OrderItemsPicked_Update' reported the ErrorCode: " + _errorCode);
                }
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Update : {0}", ex.Message);
                throw new Exception("OrderMaster::SaveUnPickedOrderItems::Error occured." + ex.Message, ex);
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                UpdateNotifyCacheUpdateTable();
                _log.Trace("Exiting SaveUnPickedOrderItems");
            }
        }

        public bool UpdateUnPickedOrderItems()
        {
            _log.Trace("Entering UpdateUnPickedOrderItems -  Table:OrderMaster  ");
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_OrderItems_UpdateStatus]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerId", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@BinTapeIDs", SqlDbType.VarChar, 8000, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _orderitems));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));


                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }

                cmdToExecute.ExecuteNonQuery();
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != 0)
                {
                    throw new Exception("Stored Procedure 'pr_OrderItems_UpdateStatus' reported the ErrorCode: " + _errorCode);
                }
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Update : {0}", ex.Message);
                throw new Exception("OrderMaster::UpdateUnPickedOrderItems::Error occured." + ex.Message, ex);
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                UpdateNotifyCacheUpdateTable();
                _log.Trace("Exiting UpdateUnPickedOrderItems");
            }
        }




        public DataTable SelectAllOrder_OnSearch()
        {
            _log.Trace("Entering SelectAllOrder_OnSearch()");

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_Order_SelectAll_OnSearch]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);
            DataTable dtProduct = new DataTable("OrderTable");

            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@OrderType", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _orderTypeID));
                cmdToExecute.Parameters.Add(new SqlParameter("@SalesOrderNo", SqlDbType.VarChar, 100, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _salesorderNo));
                cmdToExecute.Parameters.Add(new SqlParameter("@LineOrderNo", SqlDbType.VarChar, 100, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _lineOrderNo));
                cmdToExecute.Parameters.Add(new SqlParameter("@WorkOrderNo", SqlDbType.VarChar, 100, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _workOrderNo));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }


                // Fill table
                adapter.Fill(dtProduct);

                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_Order_SelectAll_OnSearch' reported the ErrorCode: " + _errorCode);
                }

            }
            catch (Exception ex)
            {
                _log.Error("Error:SelectAllOrder_OnSearch:{0}", ex.Message);
            }
            finally
            {
                _log.Trace("Leaving SelectAllOrder_OnSearch");

                if (_mainConnectionIsCreatedLocal)
                {
                    //close connection
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
            return dtProduct;
        }

        public DataSet GetOrders_OnSearch()
        {
            _log.Trace("Entering GetOrders_OnSearch()");

            DataSet dtApprovedORder = new DataSet();
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "[dbo].[pr_OrderMaster_SelectOnSearch]";
            cmdToExecute.CommandTimeout = 100;
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@CriteriaFor", SqlDbType.VarChar, 100, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _searchCriteria));

                cmdToExecute.Parameters.Add(new SqlParameter("@SalesOrderNo", SqlDbType.VarChar, 100, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _salesorderNo));
                cmdToExecute.Parameters.Add(new SqlParameter("@FromDate", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _fromdate));
                cmdToExecute.Parameters.Add(new SqlParameter("@ToDate", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _todate));
                cmdToExecute.Parameters.Add(new SqlParameter("@OrderStatus", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _status));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }


                // Fill table
                adapter.Fill(dtApprovedORder);

                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_OrderMaster_SelectOnSearch' reported the ErrorCode: " + _errorCode);
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error:GetOrders_OnSearch():{0}", ex.Message);
            }
            finally
            {
                _log.Trace("Leaving GetOrders_OnSearch()");

                if (_mainConnectionIsCreatedLocal)
                {
                    //close connection
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                adapter.Dispose();
            }

            return dtApprovedORder;
        }


        public bool ApproveOrdersFromSearch(string OrderIDs)
        {
            _log.Trace("Entering ApproveOrdersFromSearch");

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_OrderMaster_Approve]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            bool result = true;
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@OrderIDs", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, OrderIDs));
                cmdToExecute.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _userID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Proposed, _errorCode));

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }

                // Execute query.
                cmdToExecute.ExecuteNonQuery();

                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    result = false;
                    throw new Exception("Stored Procedure 'pr_OrderMaster_Approve' reported the ErrorCode: " + _errorCode);
                }

            }
            catch (Exception ex)
            {
                _log.Error("Error:ApproveOrdersFromSearch:{0}", ex.Message);
            }
            finally
            {
                _log.Trace("Leaving:ApproveOrdersFromSearch");
                if (_mainConnectionIsCreatedLocal)
                {
                    //close connection
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();

            }
            return result;
        }



        public bool ApproveOrder(string OrderDetailsIDs)
        {
            _log.Trace("Entering ApproveOrder");

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_Order_Approve]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            bool result = true;
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@OrderID", SqlDbType.BigInt, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _orderID));
                cmdToExecute.Parameters.Add(new SqlParameter("@OrderDetailsIDs", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, OrderDetailsIDs));
                cmdToExecute.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CreatedBy));
                cmdToExecute.Parameters.Add(new SqlParameter("@IsSinglePick", SqlDbType.Bit, 1, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _isSinglePick));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Proposed, _errorCode));

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }

                // Execute query.
                cmdToExecute.ExecuteNonQuery();

                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    result = false;
                    throw new Exception("Stored Procedure 'pr_Order_Approve' reported the ErrorCode: " + _errorCode);
                }

            }
            catch (Exception ex)
            {
                _log.Error("Error:ApproveOrder:{0}", ex.Message);
            }
            finally
            {
                _log.Trace("Leaving:ApproveOrder");
                if (_mainConnectionIsCreatedLocal)
                {
                    //close connection
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();

            }
            return result;
        }

        public bool CancelOrder()
        {
            _log.Trace("Entering CancelOrder");

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_Order_Cancel]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            bool result = true;
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@OrderID", SqlDbType.BigInt, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _orderID));
                cmdToExecute.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CreatedBy));
                cmdToExecute.Parameters.Add(new SqlParameter("@Comment", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _comment));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Proposed, _errorCode));

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }

                // Execute query.
                cmdToExecute.ExecuteNonQuery();

                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    result = false;
                    throw new Exception("Stored Procedure 'pr_Order_Cancel' reported the ErrorCode: " + _errorCode);
                }

            }
            catch (Exception ex)
            {
                _log.Error("Error:CancelOrder:{0}", ex.Message);
            }
            finally
            {
                _log.Trace("Leaving:CancelOrder");
            }
            return result;
        }

        public DataTable SelectOnWorkOrder()
        {
            _log.Trace("Entering SelectOnWorkOrder()");

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_OrderDetailOnWorkOrder_Select]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);
            DataTable dtProduct = new DataTable("OrderTable");

            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@WorkOrderNo", SqlDbType.VarChar, 100, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _workOrderNo));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }


                // Fill table
                adapter.Fill(dtProduct);

                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_OrderDetailOnWorkOrder_Select' reported the ErrorCode: " + _errorCode);
                }

            }
            catch (Exception ex)
            {
                _log.Error("Error:SelectOnWorkOrder:{0}", ex.Message);
            }
            finally
            {
                _log.Trace("Leaving SelectOnWorkOrder");

                if (_mainConnectionIsCreatedLocal)
                {
                    //close connection
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
            return dtProduct;
        }


        public DataTable SelectApprovedOrder()
        {
            _log.Trace("Entering SelectApprovedOrder()");

            DataTable dtApprovedORder = new DataTable("ApprovedOrder");
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "[dbo].[pr_OrderMaster_SelectAllApproved]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _userID));
                cmdToExecute.Parameters.Add(new SqlParameter("@HHID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _hhID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }


                // Fill table
                adapter.Fill(dtApprovedORder);

                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_OrderMaster_SelectAllApproved' reported the ErrorCode: " + _errorCode);
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error:SelectApprovedOrder:{0}", ex.Message);
            }
            finally
            {
                _log.Trace("Leaving SelectApprovedOrder");

                if (_mainConnectionIsCreatedLocal)
                {
                    //close connection
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                adapter.Dispose();
            }

            return dtApprovedORder;
        }

        public override DataTable SelectAll()
        {
            _log.Trace("Entering SelectAll()");

            DataTable dtApprovedORder = new DataTable("Orders");
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "[dbo].[pr_OrderMaster_SelectAll]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@OrderStatus", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _orderStatus));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }


                // Fill table
                adapter.Fill(dtApprovedORder);

                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_OrderMaster_SelectAll' reported the ErrorCode: " + _errorCode);
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error:SelectAll():{0}", ex.Message);
            }
            finally
            {
                _log.Trace("Leaving SelectAll()");

                if (_mainConnectionIsCreatedLocal)
                {
                    //close connection
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                adapter.Dispose();
            }

            return dtApprovedORder;
        }

        public bool AllotOrder()
        {
            _log.Trace("Entering ApproveOrder");

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_Order_Allotment]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            bool result = true;
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@OrderID", SqlDbType.BigInt, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _orderID));
                cmdToExecute.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _userID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Proposed, _errorCode));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrotMessage", SqlDbType.VarChar, 100, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Proposed, _errorMessege));

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }

                // Execute query.
                cmdToExecute.ExecuteNonQuery();

                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;
                _errorMessege = Convert.ToString(cmdToExecute.Parameters["@ErrotMessage"].Value);

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    result = false;
                    throw new Exception("Stored Procedure 'pr_Order_Approve' reported the ErrorCode: " + _errorCode);
                }

                if (!string.IsNullOrEmpty(_errorMessege))
                {
                    result = false;
                    throw new Exception(_errorMessege);
                }

            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(_errorMessege))
                {
                    throw new Exception(_errorMessege);
                }

                _log.Error("Error:ApproveOrder:{0}", ex.Message);
            }
            finally
            {
                _log.Trace("Leaving:ApproveOrder");
                if (_mainConnectionIsCreatedLocal)
                {
                    //close connection
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();

            }
            return result;
        }


         public DataTable SelectUnpickedOrderDetails()
         {
             _log.Trace("Entering SelectUnpickedOrderDetails()");
             DataTable dtUnPickedORder = new DataTable("UnPickedOrder");
             SqlCommand cmdToExecute = new SqlCommand();
             cmdToExecute.CommandText = "[dbo].[pr_OrderItemsUnPicked]";
             cmdToExecute.CommandType = CommandType.StoredProcedure;
             SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

             cmdToExecute.Connection = _mainConnection;

             try
             {
                 cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _dataOwnerID));                
                 cmdToExecute.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _userID));
                 cmdToExecute.Parameters.Add(new SqlParameter("@SalesOrderNo", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _salesorderNo));
                 cmdToExecute.Parameters.Add(new SqlParameter("@LineOrderNo", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _lineOrderNo));
                 cmdToExecute.Parameters.Add(new SqlParameter("@WorkOrderNo", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _workOrderNo));
                 cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode)); 

                 if (_mainConnectionIsCreatedLocal)
                 {
                     // Open connection.
                     _mainConnection.Open();
                 }
                 else
                 {
                     if (_mainConnectionProvider.IsTransactionPending)
                     {
                         cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                     }
                 }
                 // Fill table
                 adapter.Fill(dtUnPickedORder);

                 _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;
                  
                 if (_errorCode != (int)LLBLError.AllOk)
                 {
                     // Throw error.
                     throw new Exception("Stored Procedure 'pr_OrderItemsUnPicked' reported the ErrorCode: " + _errorCode);
                 }
                
             }
             catch (Exception ex)
             {
                 _log.Error("Error:SelectUnpickedOrderDetails:{0}", ex.Message);
             }
             finally
             {
                 _log.Trace("Leaving SelectUnpickedOrderDetails");

                 if (_mainConnectionIsCreatedLocal)
                 {
                     //close connection
                     _mainConnection.Close();
                 }
                 cmdToExecute.Dispose();
                 adapter.Dispose();
             }

             return dtUnPickedORder;
         }


         public DataTable SelectOrderDetailsList(out string ErrMessage)
         {
             _log.Trace("Entering SelectOrderDetailsList()");
             DataTable dtOrderDetail = new DataTable("OrderDetail");
             SqlCommand cmdToExecute = new SqlCommand();
             cmdToExecute.CommandText = "[dbo].[pr_OrderDetails_SelectAllList]";
             cmdToExecute.CommandType = CommandType.StoredProcedure;
             SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);
             ErrMessage = string.Empty;
             cmdToExecute.Connection = _mainConnection;

             try
             {
                 cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                 cmdToExecute.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _userID));
                 cmdToExecute.Parameters.Add(new SqlParameter("@HHID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _hhID));
                 cmdToExecute.Parameters.Add(new SqlParameter("@OrderID", SqlDbType.BigInt, 10, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _orderID));
                 cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                 cmdToExecute.Parameters.Add(new SqlParameter("@ErrorMessage", SqlDbType.VarChar, 100, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Proposed, _errorMessege));

                 if (_mainConnectionIsCreatedLocal)
                 {
                     // Open connection.
                     _mainConnection.Open();
                 }
                 else
                 {
                     if (_mainConnectionProvider.IsTransactionPending)
                     {
                         cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                     }
                 }
                 // Fill table
                 adapter.Fill(dtOrderDetail);
                 _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;
                 _errorMessege = Convert.ToString(cmdToExecute.Parameters["@ErrorMessage"].Value);
                 if (_errorCode != (int)LLBLError.AllOk)
                 {
                     // Throw error.
                     throw new Exception("Stored Procedure 'pr_OrderDetails_SelectAllList' reported the ErrorCode: " + _errorCode);
                 }
                 if (_errorMessege != "")
                 {
                     ErrMessage = _errorMessege; 
                 } 

             }
             catch (Exception ex)
             {
                 _log.Error("Error:SelectOrderDetailsList:{0}", ex.Message);
             }
             finally
             {
                 _log.Trace("Leaving SelectOrderDetailsList");

                 if (_mainConnectionIsCreatedLocal)
                 {
                     //close connection
                     _mainConnection.Close();
                 }
                 cmdToExecute.Dispose();
                 adapter.Dispose();
             }

             return dtOrderDetail;
         }

         public bool UpdateOrderDetailsList(out string ErrMessage)
         {
             _log.Trace("Entering UpdateOrderDetailsList -  Table:OrderDetail  ");
             SqlCommand cmdToExecute = new SqlCommand();
             cmdToExecute.CommandText = "dbo.[pr_OrderDetails_SelectAllList_Update]";
             cmdToExecute.CommandType = CommandType.StoredProcedure;

             cmdToExecute.Connection = _mainConnection;
             ErrMessage = string.Empty;
             try
             {

                 cmdToExecute.Parameters.Add(new SqlParameter("@HHID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _hhID));
                 cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerId", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                 cmdToExecute.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _userID));                 
                 cmdToExecute.Parameters.Add(new SqlParameter("@OrderDetailid", SqlDbType.BigInt, 8, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _orderDetailId));                
                 cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                 cmdToExecute.Parameters.Add(new SqlParameter("@ErrorMessage", SqlDbType.VarChar, 100, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Proposed, _errorMessege));


                 if (_mainConnectionIsCreatedLocal)
                 {
                     // Open connection.
                     _mainConnection.Open();
                 }
                 else
                 {
                     if (_mainConnectionProvider.IsTransactionPending)
                     {
                         cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                     }
                 }

                 cmdToExecute.ExecuteNonQuery();
                 _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;
                 _errorMessege = Convert.ToString(cmdToExecute.Parameters["@ErrorMessage"].Value);
                 if (_errorCode != 0)
                 {
                     return false;
                     throw new Exception("Stored Procedure '[pr_OrderDetails_SelectAllList_Update]' reported the ErrorCode: " + _errorCode);                     
                 }
                 if (_errorMessege != "")
                 {
                     ErrMessage = _errorMessege;
                     return false;
                 }
                 else
                 return true;
             }
             catch (Exception ex)
             {
                 _log.Error("Insert : {0}", ex.Message);
                 throw new Exception("OrderMaster::UpdateOrderDetailsList::Error occured." + ex.Message, ex);
             }
             finally
             {
                 if (_mainConnectionIsCreatedLocal)
                 {
                     // Close connection.
                     _mainConnection.Close();
                 }
                 cmdToExecute.Dispose();
                 UpdateNotifyCacheUpdateTable();
                 _log.Trace("Exiting UpdateOrderDetailsList");
             }
         }


         public DataTable SelectAssignedWorkOrders()
         {
             _log.Trace("Entering SelectAssignedWorkOrders()"); 
             DataTable dtAssignedOrder = new DataTable("AssignedWorkOrder");
             SqlCommand cmdToExecute = new SqlCommand();
             cmdToExecute.CommandText = "[dbo].[pr_OrderDetails_AssignedList]";
             cmdToExecute.CommandType = CommandType.StoredProcedure;
             SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

             cmdToExecute.Connection = _mainConnection;

             try
             {
                 cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _dataOwnerID));                
                 cmdToExecute.Parameters.Add(new SqlParameter("@HHID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _hhID));
                 cmdToExecute.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _userID));
                 cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode)); 

                 if (_mainConnectionIsCreatedLocal)
                 {
                     // Open connection.
                     _mainConnection.Open();
                 }
                 else
                 {
                     if (_mainConnectionProvider.IsTransactionPending)
                     {
                         cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                     }
                 }
                 // Fill table
                 adapter.Fill(dtAssignedOrder);

                 _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;
                 if (_errorCode != (int)LLBLError.AllOk)
                 {
                     // Throw error.
                     throw new Exception("Stored Procedure 'pr_OrderMaster_SelectAllApproved' reported the ErrorCode: " + _errorCode);
                 } 
             }
             catch (Exception ex)
             {
                 _log.Error("Error:SelectAssignedWorkOrders:{0}", ex.Message);
             }
             finally
             {
                 _log.Trace("Leaving SelectAssignedWorkOrders");

                 if (_mainConnectionIsCreatedLocal)
                 { 
                     _mainConnection.Close();
                 }
                 cmdToExecute.Dispose();
                 adapter.Dispose();
             }

             return dtAssignedOrder;
         }

        public DataTable SelectApprovedOrderDetails(out string ErrMessage)
        {
            _log.Trace("Entering SelectApprovedOrderDetails()");
            ErrMessage = string.Empty;
            DataTable dtApprovedORder = new DataTable("ApprovedOrder");
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "[dbo].[pr_OrderDetails_SelectAllApproved]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@OrderID", SqlDbType.BigInt, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _orderID));
                cmdToExecute.Parameters.Add(new SqlParameter("@HHID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _hhID));
                cmdToExecute.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _userID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorMessage", SqlDbType.VarChar, 100, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Proposed, _errorMessege));

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }
                // Fill table
                adapter.Fill(dtApprovedORder);

                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                _errorMessege = Convert.ToString(cmdToExecute.Parameters["@ErrorMessage"].Value);
                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_OrderMaster_SelectAllApproved' reported the ErrorCode: " + _errorCode);
                }
                if (_errorMessege != "")
                {
                    ErrMessage = _errorMessege;
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error:SelectApprovedOrderDetails:{0}", ex.Message);
            }
            finally
            {
                _log.Trace("Leaving SelectApprovedOrderDetails");

                if (_mainConnectionIsCreatedLocal)
                {
                    //close connection
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                adapter.Dispose();
            }

            return dtApprovedORder;
        }


        public DataTable SelectPickedOrderItems()
        {
            _log.Trace("Entering SelectPickedOrderItems()");

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_OrderItemsPicked_SelectAll]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);
            DataTable dtPickedOrders = new DataTable("OrderDetails");

            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _dataOwnerID));
               // cmdToExecute.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _userID));
                cmdToExecute.Parameters.Add(new SqlParameter("@SalesOrderNo", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _salesorderNo));
                cmdToExecute.Parameters.Add(new SqlParameter("@LineOrderNo", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _lineOrderNo));                 
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }
                // Fill table
                adapter.Fill(dtPickedOrders);

                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_OrderItemsPicked_SelectAll' reported the ErrorCode: " + _errorCode);
                }

            }
            catch (Exception ex)
            {
                _log.Error("Error:SelectPickedOrderItems:{0}", ex.Message);
            }
            finally
            {
                _log.Trace("Leaving SelectPickedOrderItems");

                if (_mainConnectionIsCreatedLocal)
                { 
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
            return dtPickedOrders;
        }
        

        public DataTable SelectOrderTopUp()
        {
            _log.Trace("Entering SelectOrderTopUp()");

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_OrderTopUp_SelectOnOrderDetail]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);
            DataTable dtProduct = new DataTable("OrderTopUp");

            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@OrderDetailID", SqlDbType.BigInt, 8, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _orderDetailId));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }


                // Fill table
                adapter.Fill(dtProduct);

                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_OrderTopUp_SelectOnOrderDetail' reported the ErrorCode: " + _errorCode);
                }

            }
            catch (Exception ex)
            {
                _log.Error("Error:SelectOrderTopUp:{0}", ex.Message);
            }
            finally
            {
                _log.Trace("Leaving SelectOrderTopUp");

                if (_mainConnectionIsCreatedLocal)
                {
                    //close connection
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
            return dtProduct;
        }

        public bool TopupOrder()
        {
            _log.Trace("Entering TopupOrder");

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_OrderTopUp_Insert]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            bool result = true;
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@OrderDetailID", SqlDbType.BigInt, Int32.MaxValue, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _orderDetailId));
                cmdToExecute.Parameters.Add(new SqlParameter("@TopUpQuantity", SqlDbType.Int, Int32.MaxValue, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _topupQuantity));
                cmdToExecute.Parameters.Add(new SqlParameter("@SKU_ID", SqlDbType.BigInt, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _binCatID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ProductId", SqlDbType.BigInt, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _productID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Proposed, _errorCode));

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }

                // Execute query.
                cmdToExecute.ExecuteNonQuery();

                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    result = false;
                    throw new Exception("Stored Procedure 'TopupOrder' reported the ErrorCode: " + _errorCode);
                }

            }
            catch (Exception ex)
            {
                _log.Error("Error:TopupOrder:{0}", ex.Message);
            }
            finally
            {
                _log.Trace("Leaving:TopupOrder");
                if (_mainConnectionIsCreatedLocal)
                {
                    //close connection
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();

            }
            return result;
        }
        public DataTable SelectWorkOrderForTopUp()
        {
            _log.Trace("Entering SelectWorkOrderForTopUp()");

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_OrderForTopUp]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);
            DataTable dtProduct = new DataTable("WorkOrderTopUp");

            cmdToExecute.Connection = _mainConnection;

            try
            {

                cmdToExecute.Parameters.Add(new SqlParameter("@OrderName", SqlDbType.VarChar, 100, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _salesorderNo));
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }


                // Fill table
                adapter.Fill(dtProduct);

                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_OrderForTopUp' reported the ErrorCode: " + _errorCode);
                }

            }
            catch (Exception ex)
            {
                _log.Error("Error:SelectWorkOrderForTopUp:{0}", ex.Message);
            }
            finally
            {
                _log.Trace("Leaving SelectWorkOrderForTopUp");

                if (_mainConnectionIsCreatedLocal)
                {
                    //close connection
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
            return dtProduct;
        }


        public override DataTable SelectOne()
        {
            _log.Trace("Entering SelectOne()");

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_OrderMaster_SelectOne]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);
            DataTable dtOrder = new DataTable("OrderMaster");

            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@OrderID", SqlDbType.BigInt, 8, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _orderID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }


                // Fill table
                adapter.Fill(dtOrder);

                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_OrderMaster_SelectOne' reported the ErrorCode: " + _errorCode);
                }

            }
            catch (Exception ex)
            {
                _log.Error("Error:SelectOne():{0}", ex.Message);
            }
            finally
            {
                _log.Trace("Leaving SelectOne()");

                if (_mainConnectionIsCreatedLocal)
                {
                    //close connection
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
            return dtOrder;


            //return base.SelectOne();
        }


        public DataTable SelectOrdersToApprove()
        {
            _log.Trace("Entering SelectOrdersToApprove()");

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_Order_SelectAll_ToApprove]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);
            DataTable dtProduct = new DataTable("OrderDetails");

            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@OrderID", SqlDbType.BigInt, 8, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _orderID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }


                // Fill table
                adapter.Fill(dtProduct);

                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_Order_SelectAll_ToApprove' reported the ErrorCode: " + _errorCode);
                }

            }
            catch (Exception ex)
            {
                _log.Error("Error:SelectOrdersToApprove:{0}", ex.Message);
            }
            finally
            {
                _log.Trace("Leaving SelectOrdersToApprove()");

                if (_mainConnectionIsCreatedLocal)
                {
                    //close connection
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
            return dtProduct;
        }

        public DataTable GetOrders_OnSearch1()
        {
            _log.Trace("Entering GetOrders_OnSearch()");

            DataTable dtApprovedORder = new DataTable("SeachedOrder");
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "[dbo].[pr_OrderMaster_SelectOnSearch]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@CriteriaFor", SqlDbType.VarChar, 100, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _searchCriteria));

                cmdToExecute.Parameters.Add(new SqlParameter("@SalesOrderNo", SqlDbType.VarChar, 100, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _salesorderNo));
                cmdToExecute.Parameters.Add(new SqlParameter("@FromDate", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _fromdate));
                cmdToExecute.Parameters.Add(new SqlParameter("@ToDate", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _todate));
                cmdToExecute.Parameters.Add(new SqlParameter("@OrderStatus", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _status));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }


                // Fill table
                adapter.Fill(dtApprovedORder);

                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_OrderMaster_SelectOnSearch' reported the ErrorCode: " + _errorCode);
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error:GetOrders_OnSearch():{0}", ex.Message);
            }
            finally
            {
                _log.Trace("Leaving GetOrders_OnSearch()");

                if (_mainConnectionIsCreatedLocal)
                {
                    //close connection
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                adapter.Dispose();
            }

            return dtApprovedORder;
        }

        public DataTable SelectOrders_OnSearch()
        {
            _log.Trace("Entering SelectOrders_OnSearch()");

            DataTable dtApprovedORder = new DataTable("ApprovedOrder");
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "[dbo].[pr_OrderMaster_SelectAllApproved]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _userID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }


                // Fill table
                adapter.Fill(dtApprovedORder);

                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_OrderMaster_SelectAllApproved' reported the ErrorCode: " + _errorCode);
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error:SelectOrders_OnSearch():{0}", ex.Message);
            }
            finally
            {
                _log.Trace("Leaving SelectOrders_OnSearch()");

                if (_mainConnectionIsCreatedLocal)
                {
                    //close connection
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                adapter.Dispose();
            }

            return dtApprovedORder;
        }
      
        public bool OrderUpload_Manual1(string ScheduledPickDates , out string Message)
        {
            _log.Trace("Entering OrderUpload_Manual()");

            Message = "";
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_Orders_ManualUpload]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            bool result = true;
            cmdToExecute.Connection = _mainConnection;

            SqlInt32 UpdatedCnt = 0, ErrCnt = 0, SucceedCnt = 0;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@SalesOrderNo", SqlDbType.VarChar, -1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _salesorderNo));
                cmdToExecute.Parameters.Add(new SqlParameter("@LineOrderNo", SqlDbType.VarChar, -1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _lineOrderNo));
                cmdToExecute.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, -1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _description));
                cmdToExecute.Parameters.Add(new SqlParameter("@WorkOrderNo", SqlDbType.VarChar, -1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _workOrderNo));
                cmdToExecute.Parameters.Add(new SqlParameter("@Productfamily", SqlDbType.VarChar, -1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _productfamily));
                cmdToExecute.Parameters.Add(new SqlParameter("@BinCat", SqlDbType.VarChar, -1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _bincat));
                cmdToExecute.Parameters.Add(new SqlParameter("@Quantity", SqlDbType.VarChar, -1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _quantity));
                cmdToExecute.Parameters.Add(new SqlParameter("@SchedulePickDate", SqlDbType.VarChar, -1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, ScheduledPickDates));
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerId", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@CreatedBy", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _createdBy));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorMessege", SqlDbType.VarChar, 200, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorMessege));
                cmdToExecute.Parameters.Add(new SqlParameter("@SucceedCnt", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, SucceedCnt));
                cmdToExecute.Parameters.Add(new SqlParameter("@UpdatedCnt", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, UpdatedCnt));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrCnt", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, ErrCnt));


                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }

                // Execute query.
                cmdToExecute.ExecuteNonQuery();

                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;
                _errorMessege = cmdToExecute.Parameters["@ErrorMessege"].Value.ToString();
                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    result = false;
                    throw new Exception("Stored Procedure 'pr_Orders_ManualUpload' reported the ErrorCode: " + _errorCode);
                }

                SucceedCnt = (SqlInt32)(cmdToExecute.Parameters["@SucceedCnt"].Value);
                UpdatedCnt = (SqlInt32)(cmdToExecute.Parameters["@UpdatedCnt"].Value);
                ErrCnt = (SqlInt32)(cmdToExecute.Parameters["@ErrCnt"].Value);

                Message = "<br />" + "New Work Order Uploaded :" + SucceedCnt;
                Message = Message + "<br />" + "Work Order Updated :" + UpdatedCnt;
                Message = Message + "<br />" + "Work Order With Error :" + ErrCnt + "<br />";

            }
            catch (Exception ex)
            {
                _log.Error("Error:OrderUpload_Manual:{0}", ex.Message);
                result = false;
                if (string.IsNullOrEmpty(_errorMessege))
                {
                    _errorMessege = ex.Message;
                }
            }
            finally
            {
                _log.Trace("Leaving:OrderUpload_Manual");
                if (_mainConnectionIsCreatedLocal)
                {
                    //close connection
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();

            }
            return result;
        }

        public DataTable OrderUpload_Manual(string ScheduledPickDates, out string Message, out string ErrMessage)
        {
            _log.Trace("Entering OrderUpload_Manual()");

            DataTable dtApprovedORder = new DataTable("Order");
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "[dbo].[pr_Orders_ManualUpload]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);
            ErrMessage = string.Empty;
            Message = string.Empty;
            cmdToExecute.Connection = _mainConnection;
            SqlInt32 UpdatedCnt = 0, ErrCnt = 0, SucceedCnt = 0,SOCnt = 0;
            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@SalesOrderNo", SqlDbType.VarChar, -1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _salesorderNo));
                cmdToExecute.Parameters.Add(new SqlParameter("@LineOrderNo", SqlDbType.VarChar, -1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _lineOrderNo));
                cmdToExecute.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, -1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _description));
                cmdToExecute.Parameters.Add(new SqlParameter("@WorkOrderNo", SqlDbType.VarChar, -1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _workOrderNo));
                cmdToExecute.Parameters.Add(new SqlParameter("@Productfamily", SqlDbType.VarChar, -1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _productfamily));
                cmdToExecute.Parameters.Add(new SqlParameter("@BinCat", SqlDbType.VarChar, -1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _bincat));
                cmdToExecute.Parameters.Add(new SqlParameter("@Quantity", SqlDbType.VarChar, -1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _quantity));
                cmdToExecute.Parameters.Add(new SqlParameter("@SchedulePickDate", SqlDbType.VarChar, -1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, ScheduledPickDates));
                cmdToExecute.Parameters.Add(new SqlParameter("@OrderTypeName", SqlDbType.VarChar, -1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, OrderType));
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerId", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@CreatedBy", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _createdBy));                
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorMessege", SqlDbType.VarChar, 200, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorMessege));
                cmdToExecute.Parameters.Add(new SqlParameter("@SucceedCnt", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, SucceedCnt));
                cmdToExecute.Parameters.Add(new SqlParameter("@UpdatedCnt", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, UpdatedCnt));
                cmdToExecute.Parameters.Add(new SqlParameter("@SalesCnt", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, SOCnt));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrCnt", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, ErrCnt));
                cmdToExecute.Parameters.Add(new SqlParameter("@TnRPartNumber", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tnRPartNumber));

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }


                // Fill table
                adapter.Fill(dtApprovedORder);

                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;
                _errorMessege = cmdToExecute.Parameters["@ErrorMessege"].Value.ToString();
                SucceedCnt = (SqlInt32)(cmdToExecute.Parameters["@SucceedCnt"].Value);
                UpdatedCnt = (SqlInt32)(cmdToExecute.Parameters["@UpdatedCnt"].Value);
                SOCnt = (SqlInt32)(cmdToExecute.Parameters["@SalesCnt"].Value);
                ErrCnt = (SqlInt32)(cmdToExecute.Parameters["@ErrCnt"].Value);
                Message = "<br />" + "Total Orders to Upload :" + (SucceedCnt + UpdatedCnt + ErrCnt);
                Message = Message + "<br />" + "New Sales Order Uploaded :" + SOCnt;
                Message = Message + "<br />" + "New Work Order Uploaded :" + SucceedCnt;
                Message = Message + "<br />" + "Work Order Updated :" + UpdatedCnt;
                if (ErrCnt != 0)
                {
                    ErrMessage = "<br />" + "Work Order With Error :" + ErrCnt + "<br />";
                }
                               
                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_OrderMaster_SelectAllApproved' reported the ErrorCode: " + _errorCode);
                    
                }
                else
                {
                    _errorCode = 0;
                }
               
            }
            catch (Exception ex)
            {
                _log.Error("Error:SelectOrders_OnSearch():{0}", ex.Message);
                _errorMessege = "Order Upload Failed";
            }
            finally
            {
                _log.Trace("Leaving SelectOrders_OnSearch()");

                if (_mainConnectionIsCreatedLocal)
                {
                    //close connection
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
            return dtApprovedORder;
           
        }
        public bool ModifyOrderWOValidate()
        {
            _log.Trace("Entering ModifyOrderWOValidate()");

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_OrderModifyWorkOrder_Validate]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            bool result = true;
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerId", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@OrderID", SqlDbType.BigInt , 8, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _orderID));
                cmdToExecute.Parameters.Add(new SqlParameter("@WorkOrderNo", SqlDbType.VarChar, -1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _workOrderNo));

                cmdToExecute.Parameters.Add(new SqlParameter("@validate", SqlDbType.Int, 4, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Proposed, _validateResult));
               
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }

                // Execute query.
                cmdToExecute.ExecuteNonQuery();

                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;
                
                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    result = false;
                    throw new Exception("Stored Procedure 'pr_OrderModifyWorkOrder_Validate' reported the ErrorCode: " + _errorCode);
                }
                else
                {
                    _validateResult = Int32.Parse(cmdToExecute.Parameters["@validate"].Value.ToString());
                }

            }
            catch (Exception ex)
            {
                _log.Error("Error:ModifyOrderWOValidate:{0}", ex.Message);
            }
            finally
            {
                _log.Trace("Leaving:ModifyOrderWOValidate");
                if (_mainConnectionIsCreatedLocal)
                {
                    //close connection
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();

            }
            return result;
        }


        public bool ModifyOrder(int Quantity)
        {
            _log.Trace("Entering ApproveOrder");

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_OrderDetail_Update]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            bool result = true;
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@OrderDetailId", SqlDbType.BigInt, Int32.MaxValue, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _orderDetailId));
                cmdToExecute.Parameters.Add(new SqlParameter("@Quantity", SqlDbType.Int, Int32.MaxValue, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, Quantity));
                cmdToExecute.Parameters.Add(new SqlParameter("@SKU_ID", SqlDbType.BigInt, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _binCatID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ProductId", SqlDbType.BigInt, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _productID));
                cmdToExecute.Parameters.Add(new SqlParameter("@WorkOrderNo", SqlDbType.VarChar, 8000, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _workOrderNo));
                cmdToExecute.Parameters.Add(new SqlParameter("@UpdatedBy", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _userID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Proposed, _errorCode));

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }

                // Execute query.
                cmdToExecute.ExecuteNonQuery();

                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    result = false;
                    throw new Exception("Stored Procedure 'pr_Order_Approve' reported the ErrorCode: " + _errorCode);
                }

            }
            catch (Exception ex)
            {
                _log.Error("Error:ApproveOrder:{0}", ex.Message);
            }
            finally
            {
                _log.Trace("Leaving:ApproveOrder");
                if (_mainConnectionIsCreatedLocal)
                {
                    //close connection
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();

            }
            return result;
        }
        public DataTable SelectWorkOrderForModify()
        {
            _log.Trace("Entering SelectWorkOrderForModify()");

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_OrderModifyWorkOrder_Select]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);
            DataTable dtProduct = new DataTable("WorkOrderModify");

            cmdToExecute.Connection = _mainConnection;

            try
            {

                cmdToExecute.Parameters.Add(new SqlParameter("@OrderName", SqlDbType.VarChar, 100, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _salesorderNo));
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }


                // Fill table
                adapter.Fill(dtProduct);

                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_OrderForTopUp' reported the ErrorCode: " + _errorCode);
                }

            }
            catch (Exception ex)
            {
                _log.Error("Error:SelectWorkOrderForTopUp:{0}", ex.Message);
            }
            finally
            {
                _log.Trace("Leaving SelectWorkOrderForTopUp");

                if (_mainConnectionIsCreatedLocal)
                {
                    //close connection
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
            return dtProduct;
        }

        public DataTable SelectOnLotId(string LotID)
        {
            _log.Trace("Entering SelectOnLotId()");

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_ItemCustom_SelectOnLotID]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);
            DataTable dtProduct = new DataTable("ItemCustom");

            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@LOTID", SqlDbType.VarChar, 100, ParameterDirection.Input,true, 10, 0, "", DataRowVersion.Proposed, LotID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ProductID", SqlDbType.BigInt, 8, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _productID));
                cmdToExecute.Parameters.Add(new SqlParameter("@BinCatID", SqlDbType.BigInt, 8, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _binCatID));
                cmdToExecute.Parameters.Add(new SqlParameter("@Criteria", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _searchCriteria));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }


                // Fill table
                adapter.Fill(dtProduct);

                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_ItemCustom_SelectOnLotID' reported the ErrorCode: " + _errorCode);
                }

            }
            catch (Exception ex)
            {
                _log.Error("Error:SelectOnLotId:{0}", ex.Message);
            }
            finally
            {
                _log.Trace("Leaving SelectOnLotId()");

                if (_mainConnectionIsCreatedLocal)
                {
                    //close connection
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
            return dtProduct;
        }


        public bool InsertInternalTraceOrder(string LotId)
        {
            _log.Trace("Entering InternalTraceOrder");

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_Order_InternalTrace_Insert]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            bool result = true;
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@LotID", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, LotId));
                cmdToExecute.Parameters.Add(new SqlParameter("@SalesOrderID", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _salesorderNo));
                cmdToExecute.Parameters.Add(new SqlParameter("@WorkOrderId", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _workOrderNo));
                cmdToExecute.Parameters.Add(new SqlParameter("@ItemIDs", SqlDbType.VarChar, 2000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _orderitems));
                cmdToExecute.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _userID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Proposed, _errorCode));

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }

                // Execute query.
                cmdToExecute.ExecuteNonQuery();

                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    result = false;
                    throw new Exception("Stored Procedure 'TopupOrder' reported the ErrorCode: " + _errorCode);
                }

            }
            catch (Exception ex)
            {
                _log.Error("Error:InternalTraceOrder:{0}", ex.Message);
            }
            finally
            {
                _log.Trace("Leaving:InternalTraceOrder");
                if (_mainConnectionIsCreatedLocal)
                {
                    //close connection
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();

            }
            return result;
        }

        public DataTable HomePageInventory()
        {
            _log.Trace("Entering SelectOnLotId()");

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_HomePage_Inventory]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);
            DataTable dtProduct = new DataTable("ItemCustom");

            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }


                // Fill table
                adapter.Fill(dtProduct);

                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_ItemCustom_SelectOnLotID' reported the ErrorCode: " + _errorCode);
                }

            }
            catch (Exception ex)
            {
                _log.Error("Error:SelectOnLotId:{0}", ex.Message);
            }
            finally
            {
                _log.Trace("Leaving SelectOnLotId()");

                if (_mainConnectionIsCreatedLocal)
                {
                    //close connection
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
            return dtProduct;
        }

        public bool UploadOrderFromOracle1(out string Message)
        {
            _log.Trace("Entering UploadOrderFromOracle");

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_Orders_UploadFromOracleView]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            bool result = true;
            cmdToExecute.Connection = _mainConnection;

            Message = "";
            SqlInt32 UpdatedCnt = 0, ErrCnt = 0, SucceedCnt = 0;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Proposed, _errorCode));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorMessege", SqlDbType.VarChar, 200, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorMessege));
                cmdToExecute.Parameters.Add(new SqlParameter("@SucceedCnt", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, SucceedCnt));
                cmdToExecute.Parameters.Add(new SqlParameter("@UpdatedCnt", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, UpdatedCnt));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrCnt", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, ErrCnt));
                
                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }

                // Execute query.
                cmdToExecute.ExecuteNonQuery();

                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;
                _errorMessege = cmdToExecute.Parameters["@ErrorMessege"].Value.ToString();
                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    result = false;
                    throw new Exception("Stored Procedure 'pr_Orders_UploadFromOracleView' reported the ErrorCode: " + _errorCode);
                }

                SucceedCnt = (SqlInt32)(cmdToExecute.Parameters["@SucceedCnt"].Value);
                UpdatedCnt = (SqlInt32)(cmdToExecute.Parameters["@UpdatedCnt"].Value);
                ErrCnt = (SqlInt32)(cmdToExecute.Parameters["@ErrCnt"].Value);

                Message = "<br />" + "New Work Order Uploaded :" + SucceedCnt;
                Message = Message + "<br />" + "Work Order Updated :" + UpdatedCnt;
                Message = Message + "<br />" + "Work Order With Error :" + ErrCnt + "<br />";
            }
            catch (Exception ex)
            {
                _log.Error("Error:UploadOrderFromOracle:{0}", ex.Message);
                if (string.IsNullOrEmpty(_errorMessege))
                {
                    _errorMessege = ex.Message;
                    result = false;
                }
            }
            finally
            {
                _log.Trace("Leaving:UploadOrderFromOracle");
                if (_mainConnectionIsCreatedLocal)
                {
                    //close connection
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();

            }
            return result;
        }

        public DataTable UploadOrderFromOracle(out string Message,out string ErrMessage)
        {
            _log.Trace("Entering UploadOrderFromOracle()");

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_Orders_UploadFromOracleView]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            cmdToExecute.CommandTimeout = 200;
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);
            DataTable dtOracleOrders = new DataTable("OracleOrders");
            Message = "";
            ErrMessage = string.Empty;
            SqlInt32 UpdatedCnt = 0, ErrCnt = 0, SucceedCnt = 0,SOCnt = 0 , RowCount = 0;
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Proposed, _errorCode));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorMessege", SqlDbType.VarChar, 200, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorMessege));
                cmdToExecute.Parameters.Add(new SqlParameter("@SucceedCnt", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, SucceedCnt));
                cmdToExecute.Parameters.Add(new SqlParameter("@UpdatedCnt", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, UpdatedCnt));
                cmdToExecute.Parameters.Add(new SqlParameter("@SalesCnt", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, SOCnt));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrCnt", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, ErrCnt));
                cmdToExecute.Parameters.Add(new SqlParameter("@RowCount", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, RowCount));
                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }


                // Fill table
                adapter.Fill(dtOracleOrders);

                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;
                _errorMessege = cmdToExecute.Parameters["@ErrorMessege"].Value.ToString();
                

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_Orders_UploadFromOracleView' reported the ErrorCode: " + _errorCode);

                }
                else
                {
                    _errorCode = 0;
                }

                RowCount = (SqlInt32)(cmdToExecute.Parameters["@RowCount"].Value);

                if (RowCount == 0)
                {
                    _errorMessege = "Order Uploading failed, no orders available for uploading.";
                }
                else
                {
                    SucceedCnt = (SqlInt32)(cmdToExecute.Parameters["@SucceedCnt"].Value);
                    UpdatedCnt = (SqlInt32)(cmdToExecute.Parameters["@UpdatedCnt"].Value);
                    SOCnt = (SqlInt32)(cmdToExecute.Parameters["@SalesCnt"].Value);
                    ErrCnt = (SqlInt32)(cmdToExecute.Parameters["@ErrCnt"].Value);
                    Message = "<br />" + "Total Orders to Upload :" + (SucceedCnt + UpdatedCnt + ErrCnt);
                    Message = Message + "<br />" + "New Sales Order Uploaded :" + SOCnt;
                    Message = Message + "<br />" + "New Work Order Uploaded :" + SucceedCnt;
                    Message = Message + "<br />" + "Work Order Updated :" + UpdatedCnt;
                    if (ErrCnt != 0)
                    {
                        ErrMessage = "<br />" + "Work Order With Error :" + ErrCnt + "<br />";
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error:UploadOrderFromOracle:{0}", ex.Message);
                _errorMessege = "RDC Order Upload Failed";
            }
            finally
            {
                _log.Trace("Leaving SelectAllOrderStatus");

                if (_mainConnectionIsCreatedLocal)
                {
                    //close connection
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
            return dtOracleOrders;
        }

        public DataTable SelectOrderStatusCount()
        {
            _log.Trace("Entering SelectOrderStatusCount()");

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_OrderStatusCount_OnOrder]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);
            DataTable dtOrderStatus = new DataTable("OrderStatus");

            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@OrderID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _orderID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }


                // Fill table
                adapter.Fill(dtOrderStatus);

                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_OrderStatusCount_OnOrder' reported the ErrorCode: " + _errorCode);
                }

            }
            catch (Exception ex)
            {
                _log.Error("Error:SelectOrderStatusCount():{0}", ex.Message);
            }
            finally
            {
                _log.Trace("Leaving SelectOrderStatusCount()");

                if (_mainConnectionIsCreatedLocal)
                {
                    //close connection
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
            return dtOrderStatus;
        }

        public DataTable SelectAllOrderStatus()
        {
            _log.Trace("Entering SelectAllOrderStatus()");

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_OrderStatus_SelectAll]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);
            DataTable dtOrderStatus = new DataTable("OrderStatus");

            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }


                // Fill table
                adapter.Fill(dtOrderStatus);

                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_OrderStatus_SelectAll' reported the ErrorCode: " + _errorCode);
                }

            }
            catch (Exception ex)
            {
                _log.Error("Error:SelectAllOrderStatus:{0}", ex.Message);
            }
            finally
            {
                _log.Trace("Leaving SelectAllOrderStatus");

                if (_mainConnectionIsCreatedLocal)
                {
                    //close connection
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
            return dtOrderStatus;
        }


        public bool ReleaseOrder(string OrderDetailIDs , bool Flag)
        {
            _log.Trace("Entering ReleaseOrder -  Table:OrderMaster  ");
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_Order_Release]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerId", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@OrderID", SqlDbType.BigInt, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _orderID));
                cmdToExecute.Parameters.Add(new SqlParameter("@OrderDetailIDs", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, OrderDetailIDs));
                cmdToExecute.Parameters.Add(new SqlParameter("@Flag", SqlDbType.Bit, 1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, Flag));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));


                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }

                cmdToExecute.ExecuteNonQuery();
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_Order_Release' reported the ErrorCode: " + _errorCode);
                }
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Insert : {0}", ex.Message);
                throw new Exception("OrderMaster::ReleaseOrder::Error occured." + ex.Message, ex);
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                UpdateNotifyCacheUpdateTable();
                _log.Trace("Exiting ReleaseOrder");
            }
        }

        public bool CancelOrOpenOrder(string OrderDetailIDs)
        {
            _log.Trace("Entering CancelOrOpenOrder -  Table:OrderMaster  ");
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_Order_CancelOpen]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerId", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@OrderID", SqlDbType.BigInt, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _orderID));
                cmdToExecute.Parameters.Add(new SqlParameter("@OrderDetailIDs", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, OrderDetailIDs));
                cmdToExecute.Parameters.Add(new SqlParameter("@OrderStatusId", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, OrderStatusID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));


                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }

                cmdToExecute.ExecuteNonQuery();
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_Order_CancelOpen' reported the ErrorCode: " + _errorCode);
                }
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Insert : {0}", ex.Message);
                throw new Exception("OrderMaster::CancelOrOpenOrder::Error occured." + ex.Message, ex);
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                UpdateNotifyCacheUpdateTable();
                _log.Trace("Exiting CancelOrOpenOrder");
            }
        }

        public DataTable SelectAllWorkOrders(string OrderIDs)
        {
            _log.Trace("Entering SelectAllWorkOrders()");

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_OrderMaster_SelectAll_WorkOrder]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);
            DataTable dtWorkOrders = new DataTable("OrderDetails");

            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@OrderIDs", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, OrderIDs));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }


                // Fill table
                
                adapter.Fill(dtWorkOrders);


                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_OrderMaster_SelectAll_WorkOrder' reported the ErrorCode: " + _errorCode);
                }

            }
            catch (Exception ex)
            {
                _log.Error("Error:SelectAllWorkOrders:{0}", ex.Message);
            }
            finally
            {
                _log.Trace("Leaving SelectAllWorkOrders()");

                if (_mainConnectionIsCreatedLocal)
                {
                    //close connection
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
            return dtWorkOrders;
        }


        public DataTable GetPresentQuantity(long SKUID)
        {
            _log.Trace("Entering GetPresentQuantity()");

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_Order_GetQuantityOnSKU]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);
            DataTable dtPresentQnty = new DataTable("OrderDetails");

            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@SKUID", SqlDbType.BigInt, 8, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, SKUID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }


                // Fill table
                adapter.Fill(dtPresentQnty);

                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_Order_GetQuantityOnSKU' reported the ErrorCode: " + _errorCode);
                }

            }
            catch (Exception ex)
            {
                _log.Error("Error:GetPresentQuantity:{0}", ex.Message);
            }
            finally
            {
                _log.Trace("Leaving GetPresentQuantity()");

                if (_mainConnectionIsCreatedLocal)
                {
                    //close connection
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
            return dtPresentQnty;
        }

        #region Class Property Declarations
        public int DataOwnerID
        {
            get
            {
                return _dataOwnerID;
            }
            set
            {
                _dataOwnerID = value;
            }
        }

        public int UserID
        {
            get
            {
                return _userID;
            }
            set
            {
                _userID = value;
            }
        }

        public long OrderID
        {
            get
            {
                return _orderID;
            }
            set
            {
                _orderID = value;
            }
        }

        public long OrderDetailID
        {
            get
            {
                return _orderDetailId;
            }
            set
            {
                _orderDetailId = value;
            }
        }
        public string Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                _quantity = value;
            }
        }
        public string TnRPartNumber
        {
            get
            {
                return _tnRPartNumber;
            }
            set
            {
                _tnRPartNumber = value;
            }
        }
        public string SalesOrderNo
        {
            get
            {
                return _salesorderNo;
            }
            set
            {
                _salesorderNo = value;
            }
        }
        public string WorkOrderNo
        {
            get
            {
                return _workOrderNo;
            }
            set
            {
                _workOrderNo = value;
            }
        }
        public string LineOrderNO
        {
            get
            {
                return _lineOrderNo;
            }
            set
            {
                _lineOrderNo = value;
            }
        }
        public string BinCat
        {
            get
            {
                return _bincat;
            }
            set
            {
                _bincat = value;
            }
        }
        public string ProductFamily
        {
            get
            {
                return _productfamily;
            }
            set
            {
                _productfamily = value;
            }
        }
        public long BinTapeID
        {
            get
            {
                return _binTapeID;
            }
            set
            {
                _binTapeID = value;
            }
        }

        public int Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
            }
        }
        public long ProductID
        {
            get
            {
                return _productID;
            }
            set
            {
                _productID = value;
            }
        }

        public string OrderType
        {
            get
            {
                return _ordertype;
            }
            set
            {
                _ordertype = value;
            }
        }

        public int OrderTypeID
        {
            get
            {
                return _orderTypeID;
            }
            set
            {
                _orderTypeID = value;
            }
        }
        public string OrderStatus
        {
            get
            {
                return _orderStatus;
            }
            set
            {
                _orderStatus = value;
            }
        }
        public string ErrorMessege
        {
            get
            {
                return _errorMessege;
            }
            set
            {
                _errorMessege = value;
            }
        }
        public DateTime SchedulePickDate
        {
            get
            {
                return _schedulePickDate;
            }
            set
            {
                _schedulePickDate = value;
            }
        }
        public int CreatedBy
        {
            get
            {
                return _createdBy;
            }
            set
            {
                _createdBy = value;
            }
        }

        public string Comment
        {
            get
            {
                return _comment;
            }
            set
            {
                _comment = value;
            }
        }

        public string PickDate
        {
            get
            {
                return _pickdate;
            }
            set
            {
                _pickdate = value;
            }
        }

        public string CreateDate
        {
            get
            {
                return _createDate;
            }
            set
            {
                _createDate = value;
            }
        }

        public int TopupQuantity
        {
            get
            {
                return _topupQuantity;
            }
            set
            {
                _topupQuantity = value;
            }
        }
        public int HHID
        {
            get
            {
                return _hhID;
            }
            set
            {
                _hhID = value;
            }
        }
        public int ActualPickQty
        {
            get
            {
                return _actualPickQty;
            }
            set
            {
                _actualPickQty = value;
            }
        }
        public bool IsSinglePick
        {
            get
            {
                return _isSinglePick;
            }
            set
            {
                _isSinglePick = value;
            }
        }
        public string OrderItemsLst
        {
            get
            {
                return _orderitems;
            }
            set
            {
                _orderitems = value;
            }
        }
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }
        public int ValidateResult
        {
            get
            {
                return _validateResult;
            }
            set
            {
                _validateResult = value;
            }
        }
        public long BinCatID
        {
            get
            {
                return _binCatID;
            }
            set
            {
                _binCatID = value;
            }
        }
        public string SearchCriteria
        {
            get
            {
                return _searchCriteria;
            }
            set
            {
                _searchCriteria = value;
            }
        }
        public string FromDate
        {
            get
            {
                return _fromdate;
            }
            set
            {
                _fromdate = value;
            }
        }
        public string ToDate
        {
            get
            {
                return _todate;
            }
            set
            {
                _todate = value;
            }
        }

        public int OrderStatusID
        {
            get
            {
                return _orderStatusId;
            }
            set
            {
                _orderStatusId = value;
            }
        }

        #endregion
    }
}
