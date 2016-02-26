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
    public class InventoryReport : DBInteractionBase
    {
        #region Class Member Declarations
            private int _companyId, _locationId,_dataownerId;
            private long _productId, _skuid;
            private DateTime _fromDate, _toDate;
            private string _dateCriteria;
            
        #endregion Class Member Declarations


            public DataTable InventorySummary()
            {
                SqlCommand cmdToExecute = new SqlCommand();
                cmdToExecute.CommandText = "dbo.[pr_rptphilips_InventorySummary_ToExcel]";                
                cmdToExecute.CommandType = CommandType.StoredProcedure;
                DataTable toReturn = new DataTable("InventorySummary");
                SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

                //Connection Object

                cmdToExecute.Connection = _mainConnection;             
                
                try
                {
                    cmdToExecute.Parameters.Add(new SqlParameter("@COMPANYID",SqlDbType.Int,4,ParameterDirection.Input,false,0,0,"",DataRowVersion.Proposed,_companyId));
                    cmdToExecute.Parameters.Add(new SqlParameter("@PRODUCTID", SqlDbType.BigInt, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed,_productId));
                    cmdToExecute.Parameters.Add(new SqlParameter("@SKUID", SqlDbType.BigInt, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _skuid));
                    cmdToExecute.Parameters.Add(new SqlParameter("@LOCATIONID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _locationId));
                    cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _dataownerId));
                    cmdToExecute.Parameters.Add(new SqlParameter("@FromDate", SqlDbType.DateTime, 4, ParameterDirection.Input,true, 0, 0, "", DataRowVersion.Proposed, _fromDate));
                    cmdToExecute.Parameters.Add(new SqlParameter("@Todate", SqlDbType.DateTime, 4, ParameterDirection.Input, true , 0, 0, "", DataRowVersion.Proposed, _toDate));
                    cmdToExecute.Parameters.Add(new SqlParameter("@DateCriteria", SqlDbType.VarChar, 5, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _dateCriteria));
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
                    adapter.Fill(toReturn);
                    _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                    if (_errorCode != 0)
                    {
                        // Throw error.
                        throw new Exception("Stored Procedure 'pr_rptphilips_InventorySummary_ToExcel' reported the ErrorCode: " + _errorCode);
                    }

                    return toReturn;

                }
                catch (Exception ex)
                {
                    throw new Exception("InventoryReport::InventorySummary::Error occured.", ex);
                }
                finally
                {
                    if (_mainConnectionIsCreatedLocal)
                    {
                        // Close connection.
                        _mainConnection.Close();
                    }
                    cmdToExecute.Dispose();
                    adapter.Dispose();
                }                

            }

            public DataTable InventoryDetails()
            {
                SqlCommand cmdToExecute = new SqlCommand();
                cmdToExecute.CommandText = "dbo.[pr_rptphilips_InventoryDetails_ToExcel]";
                cmdToExecute.CommandType = CommandType.StoredProcedure;
                DataTable toReturn = new DataTable("InventorySummary");
                SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

                //Connection Object

                cmdToExecute.Connection = _mainConnection;

                try
                {
                    cmdToExecute.Parameters.Add(new SqlParameter("@PRODUCTID", SqlDbType.BigInt, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _productId));
                    cmdToExecute.Parameters.Add(new SqlParameter("@SKUID", SqlDbType.BigInt, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _skuid));
                    cmdToExecute.Parameters.Add(new SqlParameter("@LOCATIONID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _locationId));
                    cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _dataownerId));
                    cmdToExecute.Parameters.Add(new SqlParameter("@FromDate", SqlDbType.DateTime, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _fromDate));
                    cmdToExecute.Parameters.Add(new SqlParameter("@Todate", SqlDbType.DateTime, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _toDate));
                    cmdToExecute.Parameters.Add(new SqlParameter("@DateCriteria", SqlDbType.VarChar, 5, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _dateCriteria));
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
                    adapter.Fill(toReturn);
                    _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                    if (_errorCode != 0)
                    {
                        // Throw error.
                        throw new Exception("Stored Procedure 'pr_rptphilips_InventoryDetails_ToExcel' reported the ErrorCode: " + _errorCode);
                    }

                    return toReturn;

                }
                catch (Exception ex)
                {
                    throw new Exception("InventoryReport::InventoryDetails::Error occured.", ex);
                }
                finally
                {
                    if (_mainConnectionIsCreatedLocal)
                    {
                        // Close connection.
                        _mainConnection.Close();
                    }
                    cmdToExecute.Dispose();
                    adapter.Dispose();
                }
            }


        #region Properties           
            public int DataOwnerID
            {
                get
                {
                    return _dataownerId;
                }
                set
                {
                    _dataownerId = value;
                }
            }
            
            public long SKU_ID
            {
                get { return _skuid; }
                set { _skuid = value; }
            }
            public long ProductID
            {
                get { return _productId; }
                set { _productId = value; }
            }
            public int CompanyID
            {
                get { return _companyId; }
                set { _companyId = value; }
            }            
           

            public DateTime FromDate
            {
                get { return _fromDate; }
                set { _fromDate = value; }
            }

            public DateTime ToDate
            {
                get { return _toDate; }
                set { _toDate = value; }
            }

            public int LocationID
            {
                get { return _locationId; }
                set { _locationId = value; }
            }
            public string DateCriteria
            {
                get { return _dateCriteria; }
                set { _dateCriteria = value; }
            }
            

        #endregion

    }
}
