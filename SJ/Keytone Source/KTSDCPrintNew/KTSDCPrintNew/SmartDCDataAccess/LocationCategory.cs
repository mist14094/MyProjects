using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using KTone.DAL.KTDBBaseLib;


namespace KTone.DAL.SmartDCDataAccess
{
    public class LocationCategory : DBInteractionBase
    {

        private Int32 _categoryID, _locationID, _dataOwnerId;
       
        private Int32? _sequence=null;
        private String _locationName, _description, _RFResources, _site, _reportKey;

        private Boolean _isMandatory, _cacheRequired;
        private DateTime _modifiedDate, _createdDate;


        public LocationCategory()
        {
        }

        #region insert

        public override bool Insert()
        {
            _log.Trace("Entering Insert - Table:LocationCategory ; CategoryID:{0}," +
            "LocationID :{1}, LocationName:{2},Description:{3}", _categoryID, _locationID, _locationName, _description);

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_LocationCategory_Insert]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
               // cmdToExecute.Parameters.Add(new SqlParameter("@CategoryId", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _categoryID));
                //cmdToExecute.Parameters.Add(new SqlParameter("@LocationId", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed,_locationID));
                cmdToExecute.Parameters.Add(new SqlParameter("@CategoryName", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _locationName));
                cmdToExecute.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _description));
                cmdToExecute.Parameters.Add(new SqlParameter("@isMandatory", SqlDbType.Bit, 1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _isMandatory));
                cmdToExecute.Parameters.Add(new SqlParameter("@Sequence", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed,_sequence));
                cmdToExecute.Parameters.Add(new SqlParameter("@RFResource", SqlDbType.VarChar, 1000, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _RFResources));                
                cmdToExecute.Parameters.Add(new SqlParameter("@ModifiedDate", SqlDbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _modifiedDate));
                cmdToExecute.Parameters.Add(new SqlParameter("@CreatedDate", SqlDbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _createdDate));
                cmdToExecute.Parameters.Add(new SqlParameter("@CategoryId", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _categoryID));
                cmdToExecute.Parameters.Add(new SqlParameter("@cacheRequired", SqlDbType.Bit, 1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _cacheRequired));
                cmdToExecute.Parameters.Add(new SqlParameter("@ReportKey", SqlDbType.VarChar, 8000, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _reportKey));                
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
                if (cmdToExecute.Parameters["@CategoryId"].Value.ToString() != "")
                {
                    _categoryID = (Int32)cmdToExecute.Parameters["@CategoryId"].Value;
                }
                else
                {
                    _categoryID = 0;
                }
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_LocationCategory_Insert' reported the ErrorCode: " + _errorCode);
                }            
               
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Insert:{0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("LocationCategory::Insert::Error occured.", ex);
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                _log.Trace("Exiting Insert");
            }
        }


        public override bool Update()
        {
            _log.Trace("Entering Update - Table:LocationCategory ; CategoryID:{0}," +
            "LocationID :{1}, LocationName:{2},Description:{3}", _categoryID, _locationID, _locationName, _description);

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_LocationCategory_Update]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@CategoryId", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _categoryID));
                cmdToExecute.Parameters.Add(new SqlParameter("@CategoryName", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _locationName));
                cmdToExecute.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _description));
                cmdToExecute.Parameters.Add(new SqlParameter("@isMandatory", SqlDbType.Bit, 1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _isMandatory));
                cmdToExecute.Parameters.Add(new SqlParameter("@Sequence", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _sequence));
                cmdToExecute.Parameters.Add(new SqlParameter("@RFResource", SqlDbType.VarChar, 1000, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _RFResources));
                cmdToExecute.Parameters.Add(new SqlParameter("@ModifiedDate", SqlDbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _modifiedDate));
                cmdToExecute.Parameters.Add(new SqlParameter("@CreatedDate", SqlDbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _createdDate));
                cmdToExecute.Parameters.Add(new SqlParameter("@cacheRequired", SqlDbType.Bit, 1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _cacheRequired));
                cmdToExecute.Parameters.Add(new SqlParameter("@ReportKey", SqlDbType.VarChar, 8000, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _reportKey));                
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
                    throw new Exception("Stored Procedure 'pr_LocationCategory_Update' reported the ErrorCode: " + _errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Insert:{0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("LocationCategory::Update::Error occured.", ex);
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                _log.Trace("Exiting Update");
            }
        }

        #endregion


        #region Select Location

        //SelectOne()

        public override DataTable SelectOne()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_LocationCategory_SelectOne]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@CategoryId", SqlDbType.Int, 8, ParameterDirection.Input, false, 19, 0, "", DataRowVersion.Proposed, _categoryID));
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
                    throw new Exception("Stored Procedure 'pr_LocationCategory_SelectOne' reported the ErrorCode: " + _errorCode);
                }

                if (toReturn.Rows.Count > 0)
                {

                    _locationName = toReturn.Rows[0]["CategoryName"].ToString();
                    _description = toReturn.Rows[0]["Description"].ToString();
                    if (!string.IsNullOrEmpty(toReturn.Rows[0]["Sequence"].ToString()))
                        _sequence = Convert.ToInt32(toReturn.Rows[0]["Sequence"].ToString());
                    else
                        _sequence = null;
                    _isMandatory = Convert.ToBoolean(toReturn.Rows[0]["IsMandatory"]);
                    _RFResources = toReturn.Rows[0]["RFResources"].ToString();
                    _cacheRequired = Convert.ToBoolean(toReturn.Rows[0]["CacheRequired"]);

                }
                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("LocationCategory::SelectOne::Error occured.", ex);
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


        public  DataTable SelectAIESL()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_LocationCategory_AISLE_SelectAll]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 8, ParameterDirection.Input, false, 19, 0, "", DataRowVersion.Proposed, _dataOwnerId));
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
                    throw new Exception("Stored Procedure 'pr_LocationCategory_AISLE_SelectAll' reported the ErrorCode: " + _errorCode);
                }

                if (toReturn.Rows.Count > 0)
                {

                    _locationName = toReturn.Rows[0]["LocationName"].ToString();
                    _locationID = Convert.ToInt32(toReturn.Rows[0]["LocationID"].ToString());
                    
                  
                }
                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("LocationCategory::SelectAIESL::Error occured.", ex);
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


        public new DataTable SelectoneLocCategory()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_Report_SelectoneLocCategory]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@CategoryId", SqlDbType.Int, 8, ParameterDirection.Input, false, 19, 0, "", DataRowVersion.Proposed, _categoryID));
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
                    throw new Exception("Stored Procedure 'pr_Report_SelectoneLocCategory' reported the ErrorCode: " + _errorCode);
                }

               
                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("LocationCategory::SelectoneLocCategory::Error occured.", ex);
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


        //SelectAll()
        public override DataTable SelectAll()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_LocationCategory_SelectAll]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("LocationCategory");
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

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_LocationCategory_SelectAll' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("LocationCategory::SelectAll::Error occured.", ex);
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


        public string GetLocationCategoryByCategoryID(int dataOwnerID, int CategoryId)
        {
            try
            {
                 
                string CategoryName = string.Empty;
                LocationCategory objloccat = new LocationCategory();
                objloccat.CategoryID = CategoryId;
                DataTable dtlocacat = objloccat.SelectAllCategoryIdLogic();


                if (dtlocacat != null && dtlocacat.Rows.Count > 0)
                {
                    return CategoryName = dtlocacat.Rows[0]["CategoryName"].ToString();
                }
                else
                    return CategoryName;
            }

            catch (Exception ex)
            { 
                throw ex;
            }
            finally
            { 
            }
        }

        //SelectAllCategoryIdLogic()

        public DataTable SelectAllCategoryIdLogic()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_LocationCategory_SelectAllCategoryIdLogic]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("LocationCategory");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {

                cmdToExecute.Parameters.Add(new SqlParameter("@CategoryId", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _categoryID));
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
                    throw new Exception("Stored Procedure 'pr_LocationCategory_SelectAllCategoryIdLogic' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("LocationCategory::SelectAllCategoryIdLogic::Error occured.", ex);
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


        #endregion

        #region GETMAXCOUNT
        public int getMaxSequence()
        {
            int maxSequnce = 0;
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            cmdToExecute.CommandText = "dbo.[pr_LocationCategory_GetMaxSequence]";

            cmdToExecute.Connection = _mainConnection;
            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@MaxSequence", SqlDbType.Int, 4, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Proposed, maxSequnce));
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

                //if (string.IsNullOrEmpty(Convert.ToString(cmdToExecute.Parameters["@MaxSequence"].Value)))
                //{
                //    maxSequnce = 0;
                //}
                //else
                //{
                    maxSequnce = Convert.ToInt32(cmdToExecute.Parameters["@MaxSequence"].Value);
                //}
                
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_LocationCategory_GetMaxSequence' reported the ErrorCode: " + _errorCode);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("LocationCategory::pr_LocationCategory_GetMaxSequence::Error occured.", ex);
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

            return maxSequnce;
        }
        #endregion


        #region Delete LocationCategory

        public override bool Delete()
        {
            _log.Trace("Entering Delete - Table:LocationCategory ; CategoryID:{0}", _categoryID);

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_LocationCategory_Delete]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@CategoryID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _categoryID));
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
                    throw new Exception("Stored Procedure 'pr_LocationCategory_Delete' reported the ErrorCode: " + _errorCode);
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
                _log.Trace("Exiting Delete.");
            }
        }

        #endregion


        #region ClassProertyDeclaration



        public Int32 CategoryID
        {
            get
            {
                return _categoryID;
            }
            set
            {
                _categoryID = value;
            }
        }

        public Int32 LocationID
        {
            get
            {
                return _locationID;
            }
            set
            {
                _locationID = value;
            }
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

        public String RFResources
        {
            get
            {
                return _RFResources;
            }
            set
            {
                _RFResources = value;
            }
        }

        public String Site
        {
            get
            {
                return _site;
            }
            set
            {
                _site = value;
            }
        }

        public String ReportKey
        {
            get
            {
                return _reportKey;
            }
            set
            {
                _reportKey = value;
            }
        }
        public Boolean IsMandatory
        {
            get
            {
                return  _isMandatory;
            }
            set
            {
                _isMandatory = value;
            }
        }
        public Boolean CacheRequired
        {
            get
            {
                return _cacheRequired;
            }
            set
            {
                _cacheRequired = value;
            }
        }

        public Int32? Sequence
        {
            get
            {
                return _sequence;
            }
            set
            {
                _sequence = value;
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
       
        #endregion
    }
}
