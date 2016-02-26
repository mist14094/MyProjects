using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using KTone.DAL.KTDBBaseLib;
using KTone.Core.KTIRFID;

namespace KTone.DAL.SmartDCDataAccess
{
    public class ItemMaster : DBInteractionBase
    {
        #region Class Member Declarations
        private Int32 _crupUser, _categoryID, _dataOwnerID, _lastSeenLocation, _tagType, _companyID, _itemType,_maxItemCount;
        string _status = string.Empty, _customerUniqueID = string.Empty, _itemStatus = string.Empty, _comments = string.Empty, _RFTagIDURN = string.Empty, _PurgedDBITEMS=string.Empty;
        private long _Id, _skuID, _productID,_itemCount;
        private DateTime _lastSeenTime, _itemsNotInUseByDate, _cycleCountCleanUpByDate, _itemMovementCleanUpByDate;
        private bool _isActive;
        private string _fromdate, _todate;

        #endregion
        
        public new  DataSet SelectAll()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_ItemMaster_SelectAll]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataSet toReturn = new DataSet("ItemMaster");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@SKUId", SqlDbType.BigInt, 8, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _skuID));
                cmdToExecute.Parameters.Add(new SqlParameter("@MaxItemCount", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _maxItemCount));
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
                    throw new Exception("Stored Procedure 'pr_ItemMaster_SelectAll' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Itemmaster::SelectAll::Error occured.", ex);
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

        public new DataSet SelectAllForSKUID(int NoOfItems)
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_ItemMaster_SelectAllForSKUID]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataSet toReturn = new DataSet("ItemMaster");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@SKUId", SqlDbType.BigInt, 8, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _skuID));
                cmdToExecute.Parameters.Add(new SqlParameter("@NoOfItems", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, NoOfItems));
                cmdToExecute.Parameters.Add(new SqlParameter("@ItemType", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, ItemType));
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
                    throw new Exception("Stored Procedure 'pr_ItemMaster_SelectAllForSKUID' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Itemmaster::SelectAllForSKUID::Error occured.", ex);
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



        public new DataSet SelectOne()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_ItemMaster_SelectOne]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataSet toReturn = new DataSet("ItemMaster");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt, 10, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _Id));
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
                    throw new Exception("Stored Procedure 'pr_ItemMaster_SelectOne' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Itemmaster::SelectOne::Error occured.", ex);
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
        public new DataSet SelectCount()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_ItemMaster_Count]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataSet toReturn = new DataSet("ItemMaster");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@CompanyID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _companyID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ProductID", SqlDbType.BigInt, 8, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _productID));
                cmdToExecute.Parameters.Add(new SqlParameter("@SkuID", SqlDbType.BigInt, 8, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _skuID));
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
                    throw new Exception("Stored Procedure 'pr_ItemMaster_Count' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Itemmaster::SelectCount::Error occured.", ex);
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

        public DataTable GetItemDetailsForRFTagIDs()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "[dbo].[pr_ItemDetailsForSelectedRFTagIDs]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("ITEMDETAILS");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@RFTagIDs", SqlDbType.VarChar, 8000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _RFTagIDURN));
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
                    throw new Exception("Stored Procedure 'pr_ItemDetailsForSelectedRFTagIDs' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("GetItemDetailsForRFTagIDs::pr_GetSKUCount::Error occured.", ex);
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


        public long GetItemCountForRFTagIDs()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "[dbo].[pr_ItemDetailsCountForSelectedRFTagIDs]";
            cmdToExecute.CommandType = CommandType.StoredProcedure; 

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@RFTagIDs", SqlDbType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _RFTagIDURN));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                cmdToExecute.Parameters.Add(new SqlParameter("@ItemCount", SqlDbType.BigInt, 8, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _itemCount));

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
                _itemCount = Convert.ToInt64(cmdToExecute.Parameters["@ItemCount"].Value);
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_ItemDetailsCountForSelectedRFTagIDs' reported the ErrorCode: " + _errorCode);
                }

                return _itemCount;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("GetItemCountForRFTagIDs::pr_ItemDetailsCountForSelectedRFTagIDs::Error occured.", ex);
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose(); 
            }
        }

        public new DataTable SelectOnBinCat_Part(string binCat, string partNumber)
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_ItemMaster_SelectOnBin_Part]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("ItemMaster");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@BinCat", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, binCat));
                cmdToExecute.Parameters.Add(new SqlParameter("@PartNumber", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, partNumber));
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
                    throw new Exception("Stored Procedure 'pr_ItemMaster_SelectOnBin_Part' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Itemmaster::SelectOnBinCat_Part::Error occured.", ex);
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


        public new DataTable SelectOnBinCat_Part_New(string binCat, string partNumber)
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_ItemMaster_SelectOnBin_Part_New]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("ItemMaster");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@BinCat", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, binCat));
                cmdToExecute.Parameters.Add(new SqlParameter("@PartNumber", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, partNumber));
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
                    throw new Exception("Stored Procedure 'pr_ItemMaster_SelectOnBin_Part_New' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Itemmaster::SelectOnBinCat_Part_New::Error occured.", ex);
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

        public new DataSet SelectOneSRNO()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_ItemMaster_SelectOneSRNO]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataSet toReturn = new DataSet("ItemMaster");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@SRNO", SqlDbType.VarChar , 100, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _customerUniqueID));
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
                    throw new Exception("Stored Procedure 'pr_ItemMaster_SelectOneSRNO' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Itemmaster::SelectOneSRNO::Error occured.", ex);
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


        public new DataSet SelectOneSRNO_HH()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_ItemMaster_HH_SelectOneSRNO]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataSet toReturn = new DataSet("ItemMaster");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@SRNO", SqlDbType.VarChar, 100, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _customerUniqueID));
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
                    throw new Exception("Stored Procedure 'pr_ItemMaster_HH_SelectOneSRNO' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Itemmaster::SelectOneSRNO::Error occured.", ex);
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

        public new DataSet SelectAllProdandSKU()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_ItemMaster_SelectAllProductandSKU]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataSet toReturn = new DataSet("ItemMaster");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
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
                    throw new Exception("Stored Procedure 'pr_ItemMaster_SelectAllProductandSKU' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Itemmaster::SelectAllProdandSKU::Error occured.", ex);
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

        public new DataSet SelectOneRftagID()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_ItemMaster_SelectOneRftagID]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataSet toReturn = new DataSet("ItemMaster");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@RftagID", SqlDbType.VarChar, 200, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _RFTagIDURN));
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
                    throw new Exception("Stored Procedure 'pr_ItemMaster_SelectOneRftagID' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Itemmaster::SelectOneRftagID::Error occured.", ex);
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


        // Philips Specific Method

        public new DataSet SelectItemCountForLocationOnProduct(string _productName)
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_ItemCountForLocationOnProductName]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataSet toReturn = new DataSet("ProductMaster");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ProductName", SqlDbType.VarChar, 100, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed,_productName));
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
                    throw new Exception("Stored Procedure 'pr_ItemCountForLocationOnProductName' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Itemmaster::SelectItemCountForLocationOnProduct::Error occured.", ex);
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


        // Method End



        public bool Insert(string columns, string colunmvalues)
        {
            _log.Trace("Entering Insert -  Table:ItemMaster ; CustomerUniqueId:{0}," +
            "SKUID :{1},Status:{2}," + _customerUniqueID, _skuID, _status);

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_ItemMaster_Insert]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
               
                cmdToExecute.Parameters.Add(new SqlParameter("@CustomerUniqueId", SqlDbType.VarChar, 200, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _customerUniqueID));
                cmdToExecute.Parameters.Add(new SqlParameter("@Status", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _status));
                cmdToExecute.Parameters.Add(new SqlParameter("@SKUID", SqlDbType.BigInt, 8, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _skuID));
                cmdToExecute.Parameters.Add(new SqlParameter("@CreatedBy", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _crupUser));
                cmdToExecute.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt, 10, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _Id));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                cmdToExecute.Parameters.Add(new SqlParameter("@Columns", SqlDbType.VarChar, 8000, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, columns));
                cmdToExecute.Parameters.Add(new SqlParameter("@ColumnValues", SqlDbType.VarChar, 8000, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, colunmvalues));
                cmdToExecute.Parameters.Add(new SqlParameter("@CategoryId", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _categoryID));
                //cmdToExecute.Parameters.Add(new SqlParameter("@LastSeenTime", SqlDbType.DateTime,20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _lastSeenTime ));
                cmdToExecute.Parameters.Add(new SqlParameter("@LastSeenLocation", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _lastSeenLocation));
                cmdToExecute.Parameters.Add(new SqlParameter("@ItemStatus", SqlDbType.VarChar,100, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _itemStatus));
                cmdToExecute.Parameters.Add(new SqlParameter("@TagType", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tagType));
                cmdToExecute.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit, 1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _isActive));
                cmdToExecute.Parameters.Add(new SqlParameter("@Comments", SqlDbType.NVarChar,200, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _comments));
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerId", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@RFTagID", SqlDbType.VarChar, 200, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _RFTagIDURN));
                
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
                _Id = (Int64)cmdToExecute.Parameters["@ID"].Value;

                if (_errorCode == -558)
                {
                    throw new Exception("Item already exists.");
                }

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_ItemMaster_Insert' reported the ErrorCode: " + _errorCode);
                }
                if (_errorCode == 2601)
                {
                    throw new Exception("Key already Exists.");
                }
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Insert : {0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                if (ex.Message.Contains("IX_ItemMasterCustomerUniqueId"))
                {
                    throw new Exception("CustomerUniqueId already Exists.");
                }
                throw new Exception("ItemMaster::Insert::Error occured."+ ex.Message, ex);
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

        public bool UpdateItemDetails()
        {
            _log.Trace("Entering UpdateItemDetails -  Table: CustomerUniqueId:{0}," +
            "Status:{1}," + _customerUniqueID, _status);

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_ItemDetails_Update]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt, 10, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _Id));
                cmdToExecute.Parameters.Add(new SqlParameter("@LastSeenLocation", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _lastSeenLocation));
                //cmdToExecute.Parameters.Add(new SqlParameter("@LastSeenTime", SqlDbType.DateTime, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _lastSeenTime));
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


                if (_errorCode == -558)
                {
                    throw new Exception("Can not Update ItemDetails.");
                }

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_ItemDetails_Update' reported the ErrorCode: " + _errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Insert : {0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("pr_ItemDetails_Update::Update::Error occured.", ex);
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
                _log.Trace("Exiting UpdateItemDetails");
            }
        }


        public bool UpdateItemDetails_GateOperation(string IDs)
        {
            _log.Trace("Entering UpdateItemDetails_GateOperation -  Table: CustomerUniqueId:{0}," +
            "Status:{1}," + _customerUniqueID, _status);

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_ItemDetailsGateOperation_Update]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@IDs", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, IDs));
                cmdToExecute.Parameters.Add(new SqlParameter("@LastSeenLocation", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _lastSeenLocation));
                cmdToExecute.Parameters.Add(new SqlParameter("@LastSeenTime", SqlDbType.DateTime, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _lastSeenTime));
                cmdToExecute.Parameters.Add(new SqlParameter("@ItemStatus", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _itemStatus));
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
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


                if (_errorCode == -558)
                {
                    throw new Exception("Gate Operation failed.");
                }

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_ItemDetailsGateOperation_Update' reported the ErrorCode: " + _errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Insert : {0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("pr_ItemDetailsGateOperation_Update::Update::Error occured.", ex);
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
                _log.Trace("Exiting UpdateItemDetails_GateOperation");
            }
        }

        public bool UpdateItemDetailsForLocationAgent()
        {
            _log.Trace("Entering UpdateItemDetailsForLocationAgent -  Table: CustomerUniqueId:{0}," +
            "Status:{1}," + _customerUniqueID, _status);

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_ItemDetailsLocationAgent_Update]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt, 10, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _Id));
                cmdToExecute.Parameters.Add(new SqlParameter("@LastSeenLocation", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _lastSeenLocation));
                cmdToExecute.Parameters.Add(new SqlParameter("@LastSeenTime", SqlDbType.DateTime, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _lastSeenTime));
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


                if (_errorCode == -558)
                {
                    throw new Exception("Can not Update ItemDetails.");
                }

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_ItemDetailsLocationAgent_Update' reported the ErrorCode: " + _errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Insert : {0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("pr_ItemDetailsLocationAgent_Update::Update::Error occured.", ex);
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
                _log.Trace("Exiting UpdateItemDetailsForLocationAgent");
            }
        }

        public bool UpdateItemDetailsForReceivingAgent()
        {
            _log.Trace("Entering : UpdateItemDetailsForReceivingAgent");

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_ItemDetailsReceivingAgent_Update]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt, 10, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _Id));
                cmdToExecute.Parameters.Add(new SqlParameter("@LastSeenLocation", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _lastSeenLocation));
                cmdToExecute.Parameters.Add(new SqlParameter("@LastSeenTime", SqlDbType.DateTime, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _lastSeenTime));
                cmdToExecute.Parameters.Add(new SqlParameter("@ItemStatus", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _itemStatus));
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


                if (_errorCode == -558)
                {
                    throw new Exception("Can not Update ItemDetails.");
                }

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_ItemDetailsReceivingAgent_Update' reported the ErrorCode: " + _errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Insert : {0}", ex.Message);
                throw new Exception("pr_ItemDetailsReceivingAgent_Update::Update::Error occured.", ex);
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
                _log.Trace("Exiting UpdateItemDetailsForReceivingAgent");
            }
        }

        public bool UpdateItemDetailsForDisAssociationAgent()
        {
            _log.Trace("Entering : UpdateItemDetailsForDisAssociationAgent");

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_ItemDetailsDisAssociationAgent_Update]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt, 10, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _Id));
                cmdToExecute.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit, 1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _isActive));
                cmdToExecute.Parameters.Add(new SqlParameter("@LastSeenLocation", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _lastSeenLocation));
                cmdToExecute.Parameters.Add(new SqlParameter("@LastSeenTime", SqlDbType.DateTime, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _lastSeenTime));
                cmdToExecute.Parameters.Add(new SqlParameter("@ItemStatus", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _itemStatus));
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


                if (_errorCode == -558)
                {
                    throw new Exception("Can not Update ItemDetails.");
                }

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_ItemDetailsDisAssociationAgent_Update' reported the ErrorCode: " + _errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Insert : {0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("pr_ItemDetailsDisAssociationAgent_Update::Update::Error occured.", ex);
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
                _log.Trace("Exiting UpdateItemDetailsForDisAssociationAgent");
            }
        }
       
        //Added by Sameer for PHILIPS

        public bool ItemsPutAwayForRFTAGID(int HHID)
        {
            _log.Trace("Entering ItemsPutAwayForRFTAGID -  Table: CustomerUniqueId:{0}," +
            "Status:{1}," + _customerUniqueID, _status);

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_ItemsPut_Away_ForRFTAGID]";  //pr_UpdateItemDetailForRFTAGID
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object

            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@RFTagID", SqlDbType.VarChar, 8000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _RFTagIDURN));
                cmdToExecute.Parameters.Add(new SqlParameter("@LastSeenLocation", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _lastSeenLocation));
                cmdToExecute.Parameters.Add(new SqlParameter("@LastSeenTime", SqlDbType.DateTime, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _lastSeenTime));
                cmdToExecute.Parameters.Add(new SqlParameter("@Status", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _status));
                cmdToExecute.Parameters.Add(new SqlParameter("@UpdatedBy", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _crupUser));
                cmdToExecute.Parameters.Add(new SqlParameter("@DATAOWNERID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@HHID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, HHID));
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


                if (_errorCode == -558)
                {
                    throw new Exception("Can not Update ItemDetails.");
                }

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_ItemsPut_Away_ForRFTAGID' reported the ErrorCode: " + _errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Insert : {0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("pr_ItemsPut_Away_ForRFTAGID::Update::Error occured.", ex);
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
                _log.Trace("Exiting ItemsPutAwayForRFTAGID");
            }
        }


        
        public bool UpdateItemDetailsForRFTAGID()
        {
            _log.Trace("Entering UpdateItemDetailsForRFTAGID -  Table: CustomerUniqueId:{0}," +
            "Status:{1}," + _customerUniqueID, _status);

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_UpdateItemDetailForRFTAGID]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@RFTagIDs", SqlDbType.VarChar, 8000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _RFTagIDURN));
                cmdToExecute.Parameters.Add(new SqlParameter("@LastSeenLocation", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _lastSeenLocation));
                cmdToExecute.Parameters.Add(new SqlParameter("@LastSeenTime", SqlDbType.DateTime, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _lastSeenTime));
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


                if (_errorCode == -558)
                {
                    throw new Exception("Can not Update ItemDetails.");
                }

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_UpdateItemDetailForRFTAGID' reported the ErrorCode: " + _errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Insert : {0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("pr_UpdateItemDetailForRFTAGID::Update::Error occured.", ex);
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
                _log.Trace("Exiting UpdateItemDetailsForRFTAGID");
            }
        }

        public bool Update()
        {
            _log.Trace("Entering Update -  Table: CustomerUniqueId:{0}," +
            "Status:{1}," + _customerUniqueID, _status);

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_ItemMaster_Update]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt, 10, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _Id));
                cmdToExecute.Parameters.Add(new SqlParameter("@UpdatedBy", SqlDbType.Int, 10, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _crupUser));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                cmdToExecute.Parameters.Add(new SqlParameter("@TagType", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tagType));
                cmdToExecute.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit, 1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _isActive));
                cmdToExecute.Parameters.Add(new SqlParameter("@Comments", SqlDbType.NVarChar, 200, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _comments));
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerId", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@RFTagID", SqlDbType.VarChar, 200, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _RFTagIDURN));
                cmdToExecute.Parameters.Add(new SqlParameter("@LastSeenLocation", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _lastSeenLocation));
               // cmdToExecute.Parameters.Add(new SqlParameter("@LastSeenTime", SqlDbType.DateTime, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _lastSeenTime));
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


                if (_errorCode == -558)
                {
                    throw new Exception("Can not Update Item.");
                }

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_ItemMaster_Update' reported the ErrorCode: " + _errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Insert : {0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("ItemMaster::Update::Error occured.", ex);
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
                _log.Trace("Exiting Update");
            }
        }

        public DataTable GetSKUCountforRFTAGID()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "[dbo].[pr_GetSKUCount]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("ITEMMASTER");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@RFTagIDs", SqlDbType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _RFTagIDURN));
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
                    throw new Exception("Stored Procedure 'pr_GetSKUCount' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("GetSKUCountforRFTAGID::pr_GetSKUCount::Error occured.", ex);
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


        public new DataTable  GetLookup()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_GetLookupDB]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("LOOKUP");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
               
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
                    throw new Exception("Stored Procedure 'pr_GetLookupDB' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Itemmaster::GetLookup::Error occured.", ex);
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

        public bool UpdateLookup(string Name,string Data)
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_UpdateLookupDB]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            bool result = false;

            try
            {

                cmdToExecute.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 100, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, Name));
                cmdToExecute.Parameters.Add(new SqlParameter("@Data", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, Data));
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

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_GetLookupDB' reported the ErrorCode: " + _errorCode);
                }
                result = true;
                
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Itemmaster::GetLookup::Error occured.", ex);
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();                
            }

            return result;
        }


        public DataTable  BinTapeSnapShot()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_ItemDetails_ForBinTapeSnapshot]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("ItemDetails");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
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
                    throw new Exception("Stored Procedure 'pr_ItemDetails_ForBinTapeSnapshot' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Itemmaster::BinTapeSnapShot::Error occured.", ex);
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


        public KTItemDetails GetItemsForID(int dataOwnerID, long ID)
        {
            KTItemDetails itemdetails = null;
            try
            {
                 
                DataSet dtItem = null;
                ItemMaster clsItem = new ItemMaster();

                this.ID = ID;
                this.DataOwnerID = dataOwnerID;
                dtItem = this.SelectOne();

                if (dtItem.Tables.Count > 0)
                {
                    if (dtItem.Tables[0].Rows.Count > 0)
                    {

                        DataRow dItemRow = dtItem.Tables[0].Rows[0];
                        DateTime createddate = DateTime.MinValue; DateTime updateddate = DateTime.MinValue;
                        DateTime LastSeenTime = DateTime.MinValue;
                        int createdby = 0, updatedby = 0, DataOwnerId = 0, LastSeenLocation = 0;
                        long SKU_ID = 0;
                        string Status = "", CustomerUniqueID = "", ItemStatus = "";

                        if (dItemRow["CreatedDate"] != null && dItemRow["CreatedDate"].ToString() != string.Empty)
                            createddate = Convert.ToDateTime(dItemRow["CreatedDate"].ToString());
                        if (dItemRow["UpdatedDate"] != null && dItemRow["UpdatedDate"].ToString() != string.Empty)
                            updateddate = Convert.ToDateTime(dItemRow["UpdatedDate"].ToString());
                        if (dItemRow["CreatedBy"] != null && dItemRow["CreatedBy"].ToString() != string.Empty)
                            createdby = int.Parse(dItemRow["CreatedBy"].ToString());
                        if (dItemRow["UpdatedBy"] != null && dItemRow["UpdatedBy"].ToString() != string.Empty)
                            updatedby = int.Parse(dItemRow["UpdatedBy"].ToString());
                        if (dItemRow["DataOwnerId"] != null && dItemRow["DataOwnerId"].ToString() != string.Empty)
                            DataOwnerId = int.Parse(dItemRow["DataOwnerId"].ToString());
                        if (dItemRow["SKU_ID"] != null && dItemRow["SKU_ID"].ToString() != string.Empty)
                            SKU_ID = long.Parse(dItemRow["SKU_ID"].ToString());
                        if (dItemRow["Status"] != null && dItemRow["Status"].ToString() != string.Empty)
                            Status = dItemRow["Status"].ToString();
                        if (dItemRow["CustomerUniqueID"] != null && dItemRow["CustomerUniqueID"].ToString() != string.Empty)
                            CustomerUniqueID = dItemRow["CustomerUniqueID"].ToString();
                        if (dItemRow["LastSeenLocation"] != null && dItemRow["LastSeenLocation"].ToString() != string.Empty)
                            LastSeenLocation = int.Parse(dItemRow["LastSeenLocation"].ToString());
                        if (dItemRow["LastSeenTime"] != null && dItemRow["LastSeenTime"].ToString() != string.Empty)
                            LastSeenTime = Convert.ToDateTime(dItemRow["LastSeenTime"].ToString());
                        if (dItemRow["ItemStatus"] != null && dItemRow["ItemStatus"].ToString() != string.Empty)
                            ItemStatus = dItemRow["ItemStatus"].ToString();

                        //itemdetails = new KTItemDetails(DataOwnerId,
                        //    long.Parse(dItemRow["ID"].ToString()), SKU_ID, Status,
                        //    CustomerUniqueID, customColumnDetails, createdby, updatedby,
                        //    createddate, updateddate, LocationName);

                        itemdetails = new KTItemDetails(long.Parse(dItemRow["ID"].ToString()), LastSeenTime, LastSeenLocation, ItemStatus);
                        itemdetails.LastSeenTime = LastSeenTime;
                        itemdetails.LastSeenLocation = LastSeenLocation;
                        itemdetails.ItemStatus = ItemStatus;
                        itemdetails.CustomerUniqueID = CustomerUniqueID; 

                    }
                }
            }
            catch (Exception ex)
            {
               
                throw new Exception(ex.Message);

            }
            finally
            {
                
            }

            return itemdetails;
        }

        public long GetItemDetailsForRFTAGID(string RFTAGID)
        {
            long itemID = 0;
            try
            {
                DataSet dtItem = null;
              
                this.RFTagIDURN = RFTAGID;
                this.DataOwnerID = 0;
                dtItem = this.SelectOneRftagID();

                if (dtItem.Tables.Count > 0)
                {
                    if (dtItem.Tables[0].Rows.Count > 0)
                    {
                        DataRow dItemRow = dtItem.Tables[0].Rows[0];
                        string RfTagId = string.Empty;
                        bool IsActive = true;
                        List<SDCTagData> tagDetails = new List<SDCTagData>();
                        DataRow[] drTags = dtItem.Tables[1].Select("ID = " + dItemRow["ID"].ToString());

                        if (drTags != null && drTags.Length > 0)
                        {
                            foreach (DataRow drt in drTags)
                            {
                                SDCTagData tag = null;
                                int type = Convert.ToInt32(drt["TAGType"].ToString());
                                tag = new SDCTagData(type, drt["RFTagID"].ToString());
                                tagDetails.Add(tag);
                                RfTagId = drt["RFTagID"].ToString();
                                if (drt["IsActive"] == null || drt["IsActive"].ToString() == string.Empty)
                                {
                                    IsActive = false;
                                }
                                else
                                {
                                    IsActive = Convert.ToBoolean(drt["IsActive"].ToString());
                                }
                                if (IsActive)
                                {
                                    itemID = long.Parse(dItemRow["ID"].ToString());

                                }

                            }
                        }
                    }
                }
                return itemID;
            }

            catch (Exception ex)
            {
              
                throw ex;
            }
            finally
            {
               

            }
        }


        #region PurgeDBItems
        public string Purge_DBItems()
        {
            _log.Trace("Entering Purge_DBItems  ; ");
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_PurgeDBItems_ByDays]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            bool result = false;
            try
            {
             
                cmdToExecute.Parameters.Add(new SqlParameter("@ItemsNotInUseByDate", SqlDbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _itemsNotInUseByDate));
                cmdToExecute.Parameters.Add(new SqlParameter("@CycleCountCleanUpByDate", SqlDbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _cycleCountCleanUpByDate));
                cmdToExecute.Parameters.Add(new SqlParameter("@ItemMovementCleanUpByDate", SqlDbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _itemMovementCleanUpByDate));
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerId", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                cmdToExecute.Parameters.Add(new SqlParameter("@CountBinTapeIDs", SqlDbType.VarChar, 200, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Proposed, _PurgedDBITEMS));

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
                 
                _PurgedDBITEMS = Convert.ToString(cmdToExecute.Parameters["@CountBinTapeIDs"].Value);
                 
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;


                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_PurgeDBItems_ByDays' reported the ErrorCode: " + _errorCode);
                }
                result =  true;

                return _PurgedDBITEMS;
            }
            catch (Exception ex)
            {
                _log.Error("Purge_DBItems:{0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Violation::Purge_DBItems::Error occured.", ex);

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
                _log.Trace("Exiting Purge_DBItems");
            }
        }

        #endregion PurgeDBItems



        public DataTable ReconcellationALLBound_CSV(int boundType)
        {
            _log.Trace("Entering ReconcellationALLBound_CSV()");

            SqlCommand cmdToExecute = new SqlCommand();
            if(boundType == 1)
                cmdToExecute.CommandText = "dbo.[pr_rptphilips_ReconciliationALLBound_CSV]";
            if (boundType == 2)
                cmdToExecute.CommandText = "dbo.[pr_rptphilips_ReconciliationOutBound_CSV]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
             cmdToExecute.CommandTimeout = 0;
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);
            DataTable dtOrderStatus = new DataTable("BoundTable");

            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@BoundType", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, boundType));
                cmdToExecute.Parameters.Add(new SqlParameter("@FromDate", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _fromdate));
                cmdToExecute.Parameters.Add(new SqlParameter("@ToDate", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _todate));
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
                    throw new Exception("Stored Procedure 'pr_rptphilips_ReconciliationALLBound_CSV' reported the ErrorCode: " + _errorCode);
                }

            }
            catch (Exception ex)
            {
                _log.Error("Error:ReconcellationALLBound_CSV():{0}", ex.Message);
            }
            finally
            {
                _log.Trace("Leaving ReconcellationALLBound_CSV()");

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

        #region Class Property Declarations

        public long ItemCount
        {
            get
            {
                return _itemCount;
            }
            set
            {
                _itemCount = value;
            }
        }
        public Int32 DataOwnerID
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
        public Int32 ItemType
        {
            get
            {
                return _itemType;
            }
            set
            {
                _itemType = value;
            }
        }
        public  long SKU_ID
        {
            get { return _skuID ; }
            set { _skuID = value; }
        }
        public long ProductID
        {
            get { return _productID; }
            set { _productID = value; }
        }
        public int CompanyID
        {
            get { return _companyID; }
            set { _companyID = value; }
        }
        public long ID
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public Int32 CrupUser
        {
            get { return _crupUser; }
            set { _crupUser = value; }
        }
        public Int32 CategoryID
        {
            get { return _categoryID; }
            set { _categoryID = value; }
        }
       public string Status
        {
            get { return _status; }
            set { _status = value; }
        }
        public string CustomerUniqueID
        {
            get { return _customerUniqueID; }
            set { _customerUniqueID = value; }
        }

        public DateTime  LastSeenTime
        {
            get { return _lastSeenTime; }
            set { _lastSeenTime = value; }
        }
        public DateTime ItemsNotInUseByDate
        {
            get { return _itemsNotInUseByDate; }
            set { _itemsNotInUseByDate = value; }
        }
        public DateTime CycleCountCleanUpByDate
        {
            get { return _cycleCountCleanUpByDate; }
            set { _cycleCountCleanUpByDate = value; }
        }
        public DateTime ItemMovementCleanUpByDate
        {
            get { return _itemMovementCleanUpByDate; }
            set { _itemMovementCleanUpByDate = value; }
        }
       
        public Int32 LastSeenLocation
        {
            get { return _lastSeenLocation; }
            set { _lastSeenLocation = value; }
        }
        public string  ItemStatus
        {
            get { return _itemStatus; }
            set { _itemStatus = value; }
        }
        public Int32 TagType
        {
            get { return _tagType; }
            set { _tagType = value; }

        }
        public Int32 MaxItemCount
        {
            get { return _maxItemCount; }
            set { _maxItemCount = value; }

        }
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }
        public string  Comments
        {
            get { return _comments; }
            set { _comments = value; }
        }
        public string RFTagIDURN
        {
            get { return _RFTagIDURN; }
            set { _RFTagIDURN = value; }
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
	
        
     #endregion Class Property Declarations
}
}
