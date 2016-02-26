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
    public class Location : DBInteractionBase
    {

        private Int32 _locationId, _categoryId, _parentLocationId, _result, _dataOwnerId;
        private String _locationName, _description, _rfResource, _rfValue, _barcodeValue, _errorMsg, _stencilData, _zoneName, _skuIDs,_locationIds;
        private Boolean _isActive;
        private DateTime _modifiedDate, _createdDate;
        private byte[] _imageArr;

        public Location()
        {
        }

        #region select

        //SelectOne()

        public override DataTable SelectOne()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_Location_SelectOne]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@LocationId", SqlDbType.Int, 8, ParameterDirection.Input, false, 19, 0, "", DataRowVersion.Proposed, _locationId));
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 8, ParameterDirection.Input, false, 19, 0, "", DataRowVersion.Proposed, _dataOwnerId));
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
                    throw new Exception("Stored Procedure 'pr_Location_SelectOne' reported the ErrorCode: " + _errorCode);
                }

                if (toReturn.Rows.Count > 0)
                {
                    _locationName = toReturn.Rows[0]["LocationName"].ToString();
                    _description = toReturn.Rows[0]["Description"].ToString();
                    _isActive = Convert.ToBoolean(toReturn.Rows[0]["IsActive"]);
                    _categoryId = Convert.ToInt32(toReturn.Rows[0]["CategoryId"]);
                    _rfResource = toReturn.Rows[0]["RFResource"].ToString();
                    _rfValue = toReturn.Rows[0]["RFValue"].ToString();
                    _parentLocationId = Convert.ToInt32(toReturn.Rows[0]["ParentLocationId"]);
                    _stencilData = toReturn.Rows[0]["StencilData"].ToString();
                    if (!string.IsNullOrEmpty(Convert.ToString(toReturn.Rows[0]["LocationImage"])))
                    {
                        _imageArr = (byte[])toReturn.Rows[0]["LocationImage"];
                    }
                    _zoneName =Convert.ToString(toReturn.Rows[0]["LocationZone"]);
                    //_dataOwnerId = Convert.ToInt32(toReturn.Rows[0]["DataOwnerID"]);
                }
                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Location::SelectOne::Error occured.", ex);
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
             

        public DataTable SelectLocationByCategory()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_LocationByCategory_SelectAll]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                //sp start
                //cmdToExecute.Parameters.Add(new SqlParameter("@CategoryId", SqlDbType.Int, 8, ParameterDirection.Input, false, 19, 0, "", DataRowVersion.Proposed, _locationId));
                cmdToExecute.Parameters.Add(new SqlParameter("@CategoryId", SqlDbType.Int, 8, ParameterDirection.Input, false, 19, 0, "", DataRowVersion.Proposed, _categoryId));
                //sandeep end
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, false, 19, 0, "", DataRowVersion.Proposed, _dataOwnerId));
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
                    throw new Exception("Stored Procedure 'pr_LocationByCategory_SelectOne' reported the ErrorCode: " + _errorCode);
                }

                if (toReturn.Rows.Count > 0)
                {
                    _locationName = toReturn.Rows[0]["LocationName"].ToString();
                    _description = toReturn.Rows[0]["Description"].ToString();
                    _isActive = Convert.ToBoolean(toReturn.Rows[0]["IsActive"]);
                    _categoryId = Convert.ToInt32(toReturn.Rows[0]["CategoryId"]);
                    _rfResource = toReturn.Rows[0]["RFResource"].ToString();
                    _rfValue = toReturn.Rows[0]["RFValue"].ToString();
                    _parentLocationId = Convert.ToInt32(toReturn.Rows[0]["ParentLocationId"]);
                   // _dataOwnerId = Convert.ToInt32(toReturn.Rows[0]["DataOwnerID"]);
                }
                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Location::SelectLocationByCategory::Error occured.", ex);
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
        //selectAll()

        public new DataTable SelectAllForReport()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_rptLocation_SelectAll]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("Location");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 8, ParameterDirection.Input, false, 19, 0, "", DataRowVersion.Proposed, _dataOwnerId));
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

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_rptLocation_SelectAll' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Location::SelectAllForReport::Error occured.", ex);
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

        public override DataTable SelectAll()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_Location_SelectAll]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("Location");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 8, ParameterDirection.Input, false, 19, 0, "", DataRowVersion.Proposed, _dataOwnerId));
                cmdToExecute.Parameters.Add(new SqlParameter("@LocationID", SqlDbType.Int, 8, ParameterDirection.Input, false, 19, 0, "", DataRowVersion.Proposed, _locationId));
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

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_Location_SelectAll' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Location::SelectAll::Error occured.", ex);
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

        public KTLocationDetails GetAllLocationByRFvalue(int dataOwnerID, string rfValue)
        {
            KTLocationDetails objLocdetails = null;
            try
            {  
                this.DataOwnerId = dataOwnerID;
                this.RFValue = rfValue;
                DataTable dtLocation = this.SelectAllByRFValue();
                if (dtLocation != null && dtLocation.Rows.Count > 0)
                {
                    DataRow dr = dtLocation.Rows[0];
                    bool isActive = false;
                    DateTime createdDate = DateTime.MinValue;
                    DateTime modifiedDate = DateTime.MinValue;
                    int CategoryId = 0, ParentLocationId = 0, DataOwnerID = 0; byte[] Locationimage = new byte[8];
                    string LocationName = "", Description = "", RFResource = "", RFValue = "", StencilData = "", LocationZone = "";
                    if (dr["IsActive"] != null && dr["IsActive"].ToString() != string.Empty)
                        isActive = Convert.ToBoolean(dr["IsActive"].ToString());
                    if (dr["CreatedDate"] != null && dr["CreatedDate"].ToString() != string.Empty)
                        createdDate = Convert.ToDateTime(dr["CreatedDate"].ToString());
                    if (dr["ModifiedDate"] != null && dr["ModifiedDate"].ToString() != string.Empty)
                        modifiedDate = Convert.ToDateTime(dr["ModifiedDate"].ToString());
                    if (dr["CategoryId"] != null && dr["CategoryId"].ToString() != string.Empty)
                        CategoryId = int.Parse(dr["CategoryId"].ToString());
                    if (dr["ParentLocationId"] != null && dr["ParentLocationId"].ToString() != string.Empty)
                        ParentLocationId = int.Parse(dr["ParentLocationId"].ToString());
                    if (dr["LocationName"] != null && dr["LocationName"].ToString() != string.Empty)
                        LocationName = dr["LocationName"].ToString();
                    if (dr["Description"] != null && dr["Description"].ToString() != string.Empty)
                        Description = dr["Description"].ToString();
                    if (dr["RFResource"] != null && dr["RFResource"].ToString() != string.Empty)
                        RFResource = dr["RFResource"].ToString();
                    if (dr["RFValue"] != null && dr["RFValue"].ToString() != string.Empty)
                        RFValue = dr["RFValue"].ToString();
                    if (dr["DataOwnerID"] != null && dr["DataOwnerID"].ToString() != string.Empty)
                        DataOwnerID = int.Parse(dr["DataOwnerID"].ToString());

                    if (dr["Stencildata"] != null && dr["Stencildata"].ToString() != string.Empty)
                        StencilData = dr["Stencildata"].ToString();
                    if (dr["Locationzone"] != null && dr["Locationzone"].ToString() != string.Empty)
                        LocationZone = dr["Locationzone"].ToString();
                    if (dr["LocationImage"] != null && dr["LocationImage"].ToString() != string.Empty)
                        Locationimage = (byte[])(dr["LocationImage"]);

                    objLocdetails = new KTLocationDetails(isActive, int.Parse(dr["LocationId"].ToString()), CategoryId,
                          ParentLocationId, LocationName, Description, RFResource,
                           RFValue, createdDate, modifiedDate, DataOwnerID, StencilData, LocationZone, Locationimage);

                }
            }
            catch (Exception ex)
            { 
                throw new Exception(ex.Message);

            }
            finally
            {
              
            }

            return objLocdetails;
        }

        public DataTable SelectAllByRFValue()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_Location_SelectAllByRFValue]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("Location");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 8, ParameterDirection.Input, false, 19, 0, "", DataRowVersion.Proposed, _dataOwnerId));
                cmdToExecute.Parameters.Add(new SqlParameter("@RFValue", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 19, 0, "", DataRowVersion.Proposed, _rfValue));
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

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_Location_SelectAllByRFValue' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Location::SelectAllByRFValue::Error occured.", ex);
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

        public DataTable SelectAll_Philis()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_Location_SelectAll_Philips]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("Location");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 8, ParameterDirection.Input, false, 19, 0, "", DataRowVersion.Proposed, _dataOwnerId));
                cmdToExecute.Parameters.Add(new SqlParameter("@LocationID", SqlDbType.Int, 8, ParameterDirection.Input, false, 19, 0, "", DataRowVersion.Proposed, _locationId));
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

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_Location_SelectAll_Philips' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Location::SelectAll_Philis::Error occured.", ex);
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

        public new DataTable ItemPrinterSelectAll()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_ItemPrinter_SelectAll]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("Location");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, false, 19, 0, "", DataRowVersion.Proposed, _dataOwnerId));
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

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_ItemPrinter_SelectAll' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Location::ItemPrinterSelectAll::Error occured.", ex);
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
        public new DataTable LocationPrinterSelectAll()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_LocationPrinter_SelectAll]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("Location");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, false, 19, 0, "", DataRowVersion.Proposed, _dataOwnerId));
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

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_LocationPrinter_SelectAll' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Location::LocationPrinterSelectAll::Error occured.", ex);
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
        public DataTable SelectAllLocation()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_Location_SelectALLByRFResource]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("Location");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 8, ParameterDirection.Input, false, 19, 0, "", DataRowVersion.Proposed, _dataOwnerId));
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

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_Location_SelectALLByRFResource' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Location::pr_Location_SelectALLByRFResource::Error occured.", ex);
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


        // SelectAllLocationIdLogic()
        public DataTable SelectAllLocationIdLogic()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_Location_SelectAllLocationIdLogic]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("Location");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {

                cmdToExecute.Parameters.Add(new SqlParameter("@LocationId", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _locationId));
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

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_Location_SelectAllLocationIdLogic' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Location::SelectAllLocationIdLogic::Error occured.", ex);
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


        /// <summary>
        /// This method returns rows from trDCLocation table depending on whether that location is present in GTINMaster
        /// </summary>
        /// <returns></returns>
        public DataTable SelectLocationsForCycleCount(int CCInstanceID)
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_Location_SelectLocationsForCC]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable dtToReturn = new DataTable("trDCLocation");
            SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

            // Use base class' connection object
            scmCmdToExecute.Connection = _mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@CCInstaceID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, CCInstanceID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                _mainConnection.Open();

                // Execute query.
                sdaAdapter.Fill(dtToReturn);
                _errorCode = (Int32)((System.Data.SqlTypes.SqlInt32)(scmCmdToExecute.Parameters["@iErrorCode"].Value)).Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'sp_trDCLocation_SelectLocationsForCC' reported the ErrorCode: " + _errorCode);
                }

                return dtToReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("TrDCLocation::SelectLocationsForCC::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _mainConnection.Close();
                scmCmdToExecute.Dispose();
                sdaAdapter.Dispose();
            }
        }

        #endregion

        #region insert

        public override bool Insert()
        {
            _log.Trace("Entering Insert - Table:Location ; " +
            " LocationName:{0},Description:{1}", _locationName, _description);

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_Location_Insert]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@LocationName", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _locationName));
                cmdToExecute.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _description));
                cmdToExecute.Parameters.Add(new SqlParameter("@CategoryId", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _categoryId));
                cmdToExecute.Parameters.Add(new SqlParameter("@RFResource", SqlDbType.VarChar, 1000, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rfResource));
                cmdToExecute.Parameters.Add(new SqlParameter("@RFValue", SqlDbType.VarChar, 1000, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rfValue));
                cmdToExecute.Parameters.Add(new SqlParameter("@ParentLocId", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _parentLocationId));
                cmdToExecute.Parameters.Add(new SqlParameter("@isActive", SqlDbType.Bit, 1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _isActive));
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerId", SqlDbType.Int, 8, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerId));
                cmdToExecute.Parameters.Add(new SqlParameter("@LocationId", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _locationId));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                cmdToExecute.Parameters.Add(new SqlParameter("@Result", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _result));
                cmdToExecute.Parameters.Add(new SqlParameter("@StencilData", SqlDbType.VarChar, 2000, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _stencilData));
                cmdToExecute.Parameters.Add(new SqlParameter("@LocationImage", SqlDbType.Image, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _imageArr));
                cmdToExecute.Parameters.Add(new SqlParameter("@LocationZone", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _zoneName));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorMessage", SqlDbType.VarChar, 200, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Proposed, _errorMsg));
               // cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));

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

                if (!string.IsNullOrEmpty(cmdToExecute.Parameters["@ErrorMessage"].Value.ToString()))
                {
                    _errorMsg = Convert.ToString(cmdToExecute.Parameters["@ErrorMessage"].Value);

                    throw new Exception(_errorMsg);
                }


                if (cmdToExecute.Parameters["@LocationId"].Value.ToString() != "")
                {
                    _locationId = (Int32)cmdToExecute.Parameters["@LocationId"].Value;
                }
                else
                {
                    _locationId = 0;
                }
                if (cmdToExecute.Parameters["@Result"].Value.ToString() == "0")
                {
                    _locationId = (Int32)cmdToExecute.Parameters["@Result"].Value;
                }
                else
                {
                    _result = 1;
                }

                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_Location_Insert' reported the ErrorCode: " + _errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Insert:{0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception(ex.Message, ex);
               // throw new Exception("Location::Insert::Error occured.", ex);
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

        public override bool Update()
        {
            _log.Trace("Entering Update - Table:Location ; " +
            " LocationName:{0},Description:{1}", _locationName, _description);

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_Location_Update]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@LocationId", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _locationId));
                cmdToExecute.Parameters.Add(new SqlParameter("@LocationName", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _locationName));
                cmdToExecute.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _description));
                cmdToExecute.Parameters.Add(new SqlParameter("@CategoryId", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _categoryId));
                cmdToExecute.Parameters.Add(new SqlParameter("@RFResource", SqlDbType.VarChar, 1000, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rfResource));
                cmdToExecute.Parameters.Add(new SqlParameter("@RFValue", SqlDbType.VarChar, 1000, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rfValue));
                cmdToExecute.Parameters.Add(new SqlParameter("@ParentLocId", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _parentLocationId));
                cmdToExecute.Parameters.Add(new SqlParameter("@isActive", SqlDbType.Bit, 1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _isActive));
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerId", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerId));
                cmdToExecute.Parameters.Add(new SqlParameter("@LocationImage", SqlDbType.Image, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _imageArr));
                cmdToExecute.Parameters.Add(new SqlParameter("@LocationZone", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _zoneName));
                cmdToExecute.Parameters.Add(new SqlParameter("@Result", SqlDbType.Int, 4, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Proposed, _result));
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


                _result = (Int32)cmdToExecute.Parameters["@DataOwnerId"].Value;
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;


                if (_result == 0)
                {
                    throw new Exception("Choose different parent location ");
                }

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_Location_Update' reported the ErrorCode: " + _errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Insert:{0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                if (Result == 0)
                {
                    throw new Exception("Choose different parent location ");
                }
                else
                {
                    throw new Exception("Location::Update::Error occured.", ex);
                }
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

        public bool InsertLocationSKUAssociation()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_SKU_TO_LocationAssociation]"; //"dbo.[pr_Location_TO_SkuAssociation]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@ProductIDs", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _skuIDs));
                cmdToExecute.Parameters.Add(new SqlParameter("@LocationIDs", SqlDbType.Int, 4, ParameterDirection.Input,false, 0, 0, "", DataRowVersion.Proposed, _locationId));
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 8, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerId));
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
                    throw new Exception("Stored Procedure 'pr_Location_TO_SkuAssociation' reported the ErrorCode: " + _errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Insert:{0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Location::InsertLocationSKUAssociation::Error occured.", ex);
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                // UpdateNotifyCacheUpdateTable();
                _log.Trace("Exiting Insert");
            }
        }

        #endregion

        #region Delete Location

        public override bool Delete()
        {
            _log.Trace("Entering Delete - Table:Location ; LocationID:{0}", _locationId);

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_Location_Delete]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@LocationID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _locationId));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorMessage", SqlDbType.VarChar, 100, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorMsg));
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
                _errorMsg = Convert.ToString(cmdToExecute.Parameters["@ErrorMessage"].Value);

                if (_errorMsg != "")
                {
                    throw new Exception(_errorMsg);
                }
                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_Location_Delete' reported the ErrorCode: " + _errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Delete:{0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception(ex.Message, ex);
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
                _log.Trace("Exiting Delete.");
            }
        }

        #endregion



        public Int32 LocationId
        {
            get
            {
                return _locationId;

            }
            set
            {
                _locationId = value;
            }
        }

        public String SKUIDs
        {
            get { return _skuIDs; }
            set { _skuIDs = value; }
        }
        public String LocationIDs
        {
            get { return _locationIds; }
            set { _locationIds = value; }
        }
        public String LocationName
        {
            get
            {
                return _locationName;
            }
            set
            {
                _locationName = value;
            }
        }

        public String Description
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

        public Int32 CategoryId
        {
            get
            {
                return _categoryId;
            }

            set
            {
                _categoryId = value;
            }
        }

        public String RFResource
        {
            get
            {
                return _rfResource;
            }

            set
            {
                _rfResource = value;
            }
        }

        public String StencilData
        {
            get
            {
                return _stencilData;
            }

            set
            {
                _stencilData = value;
            }
        }

        public String ZoneName
        {
            get
            {
                return _zoneName;
            }
            set
            {
                _zoneName = value;
            }

        }

        public String RFValue
        {
            get
            {
                return _rfValue;
            }

            set
            {
                _rfValue = value;
            }
        }

        public String BarcodeValue
        {
            get
            {
                return _barcodeValue;
            }
            set
            {
                _barcodeValue = value;
            }
        }

        public byte[] ImageByteArr
        {
            get
            {
                return _imageArr;
            }
            set
            {
                byte[] imageArrTmp = value;
                //if (imageArrTmp.Length == 0)
                //{
                //    throw new ArgumentOutOfRangeException("Logo", "Log can't be NULL");
                //}
                _imageArr = value;
            }
        }

        public String ErrorMessage
        {
            get
            {
                return _errorMsg;
            }
            set
            {
                _errorMsg = value;
            }

        }

        public Int32 ParentLocationId
        {
            get
            {
                return _parentLocationId;
            }
            set
            {
                _parentLocationId = value;
            }
        }

        public Boolean IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                _isActive = value;
            }
        }

        public DateTime ModifiedDate
        {
            get
            {
                return _modifiedDate;
            }
            set
            {
                _modifiedDate = value;
            }
        }

        public DateTime CreatedDate
        {
            get
            {
                return _createdDate;
            }
            set
            {
                _createdDate = value;
            }
        }

        public Int32 Result
        {
            get
            {
                return _result;
            }
            set
            {
                _result = value;
            }
        }

        public Int32 DataOwnerId
        {
            get
            {
                return _dataOwnerId;
            }
            set
            {
                _dataOwnerId = value;
            }
        }
    }
}
