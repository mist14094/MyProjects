using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using KTone.DAL.KTDBBaseLib;

namespace KTone.DAL.SmartDCDataAccess
{
    public class UserDataView : DBInteractionBase
    {
        #region Class Member Declaration
        private Int32 _userID, _pageSize, _viewID, _dataOwnerID;
        private Int64 _assetTypeMasterID;
        private String _viewName, _module, _sortByCol, _sortByOrder, _filterCondition, _headerFields, _detailFields;
        private Boolean _isGeneric, _isAdded, _displayGroup;
        //private SqlInt32 _errorCode;
        
        #endregion

        public UserDataView()
		{
			// Nothing for now.
        }

        #region Class Method Declaration

        public override DataTable SelectAll()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_UserDataViewSetting_SelectAll]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("View Setting");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _userID));
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

                // Execute query.
                adapter.Fill(toReturn);
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_UserDataViewSetting_SelectAll' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Zone::SelectAll::Error occured.", ex);
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

        public override DataTable SelectOne()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "pr_GetUserViewSetting_SelectOne";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("View Setting");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _userID));
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

                // Execute query.
                adapter.Fill(toReturn);
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_UserDataViewSetting_SelectAll' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Zone::SelectAll::Error occured.", ex);
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

        public override bool Insert()
        {
            _log.Trace("Entering Insert - Table:UserDefinedDataView ; ViewName:{0}," +
             "Module :{1},UserID:{2},IsGeneric:{3},SortByCol:{4},SortByOrder:{5}," +
             "PageSize:{6},FilterCondition:{7},HeaderFields:{8},DetailFields:{9}, AssetTypeMasterID:{10}", _viewName, _module, _userID, _isGeneric, _sortByCol, _sortByOrder, _pageSize, _filterCondition, _headerFields, _detailFields, _assetTypeMasterID);

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_UserDataViewSetting_Insert]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@ViewName", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _viewName));
                cmdToExecute.Parameters.Add(new SqlParameter("@Module", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _module));
                cmdToExecute.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _userID));
                cmdToExecute.Parameters.Add(new SqlParameter("@IsGeneric", SqlDbType.Bit, 1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _isGeneric));
                cmdToExecute.Parameters.Add(new SqlParameter("@SortByCol", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _sortByCol));
                cmdToExecute.Parameters.Add(new SqlParameter("@SortByOrder", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _sortByOrder));
                cmdToExecute.Parameters.Add(new SqlParameter("@PageSize", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _pageSize));
                cmdToExecute.Parameters.Add(new SqlParameter("@FilterCondition", SqlDbType.VarChar, 5000, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _filterCondition));
                cmdToExecute.Parameters.Add(new SqlParameter("@HeaderFields", SqlDbType.VarChar, 5000, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _headerFields));
                cmdToExecute.Parameters.Add(new SqlParameter("@DetailFields", SqlDbType.VarChar, 5000, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _detailFields));
                cmdToExecute.Parameters.Add(new SqlParameter("@AssetTypeMasterID", SqlDbType.BigInt, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _assetTypeMasterID));
                cmdToExecute.Parameters.Add(new SqlParameter("@DisplayGroup", SqlDbType.Bit, 1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _displayGroup));

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

                if (_errorCode == 426)
                {
                    throw new Exception("426");
                }
               
                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_UserDataViewSetting_Insert' reported the ErrorCode: " + _errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Insert:{0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("UserDataView::Insert::Error occured." + ex.Message, ex);
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                
                _log.Trace("Exiting Insert.");
            }
        }


        public override bool Update()
        {
            _log.Trace("Entering Insert - Table:UserDefinedDataView ; ViewName:{0}," +
             "Module :{1},UserID:{2},IsGeneric:{3},SortByCol:{4},SortByOrder:{5}," +
             "PageSize:{6},FilterCondition:{7},HeaderFields:{8},DetailFields:{9}, ViewID:{10}", _viewName, _module, _userID, _isGeneric, _sortByCol, _sortByOrder, _pageSize, _filterCondition, _headerFields, _detailFields, _viewID);

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_UserDataViewSetting_Update]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@ViewName", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _viewName));
                cmdToExecute.Parameters.Add(new SqlParameter("@Module", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _module));
                cmdToExecute.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _userID));
                cmdToExecute.Parameters.Add(new SqlParameter("@IsGeneric", SqlDbType.Bit, 1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _isGeneric));
                cmdToExecute.Parameters.Add(new SqlParameter("@SortByCol", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _sortByCol));
                cmdToExecute.Parameters.Add(new SqlParameter("@SortByOrder", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _sortByOrder));
                cmdToExecute.Parameters.Add(new SqlParameter("@PageSize", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _pageSize));
                cmdToExecute.Parameters.Add(new SqlParameter("@FilterCondition", SqlDbType.VarChar, 5000, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _filterCondition));

                cmdToExecute.Parameters.Add(new SqlParameter("@HeaderFields", SqlDbType.VarChar, 5000, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _headerFields));
                cmdToExecute.Parameters.Add(new SqlParameter("@DetailFields", SqlDbType.VarChar, 5000, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _detailFields));
                cmdToExecute.Parameters.Add(new SqlParameter("@ViewID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _viewID));
                cmdToExecute.Parameters.Add(new SqlParameter("@DisplayGroup", SqlDbType.Bit, 1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _displayGroup));
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

                if (_errorCode == 426)
                {
                    throw new Exception("426");
                }

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_UserDataViewSetting_Update' reported the ErrorCode: " + _errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Update:{0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("UserDataView::Update::Error occured." + ex.Message, ex);
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                
                _log.Trace("Exiting Update.");
            }
        }


        public override bool Delete()
        {
            _log.Trace("Entering Delete - Table:UserDefinedDataView ; ViewID:{0}", _viewID);

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_UserDefinedDataView_Delete]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@ViewID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _viewID));
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
                    throw new Exception("Stored Procedure 'pr_UserDefinedDataView_Delete' reported the ErrorCode: " + _errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Delete:{0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("UserDataView::Delete::Error occured.", ex);
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                _log.Trace("Exiting Delete");
            }
        }

        #endregion


        #region Class Property Declaration

        public Int32 UserID
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

        public Int64 AssetTypeMasterID
        {
            get
            {
                return _assetTypeMasterID;
            }
            set
            {
                _assetTypeMasterID = value;
            }
        }

        public Int32 PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value;
            }
        }

        public Int32 ViewID
        {
            get
            {
                return _viewID;
            }
            set
            {
                _viewID = value;
            }
        }

        public String ViewName
        {
            get
            {
                return _viewName;
            }
            set
            {
                _viewName = value;
            }
        }


        public String Module
        {
            get
            {
                return _module;
            }
            set
            {
                _module = value;
            }
        }

        public String SortByCol
        {
            get
            {
                return _sortByCol;
            }
            set
            {
                _sortByCol = value;
            }
        }

        public String SortByOrder
        {
            get
            {
                return _sortByOrder;
            }
            set
            {
                _sortByOrder = value;
            }
        }

        public String FilterCondition
        {
            get
            {
                return _filterCondition;
            }
            set
            {
                _filterCondition = value;
            }
        }

        public String HeaderFields
        {
            get
            {
                return _headerFields;
            }
            set
            {
                _headerFields = value;
            }
        }

        public String DetailFields
        {
            get
            {
                return _detailFields;
            }
            set
            {
                _detailFields = value;
            }
        }

        public Boolean IsGeneric
        {
            get
            {
                return _isGeneric;
            }
            set
            {
                _isGeneric = value;
            }
        }

        public Boolean IsAdded
        {
            get
            {
                return _isAdded;
            }
            set
            {
                _isAdded = value;
            }
        }

        public Boolean DisplayGroup
        {
            get
            {
                return _displayGroup;
            }
            set
            {
                _displayGroup = value;
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

        #endregion

    }
}
