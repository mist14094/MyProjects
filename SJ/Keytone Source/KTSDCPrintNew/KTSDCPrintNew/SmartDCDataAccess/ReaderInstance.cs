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
    public class ReaderInstance : DBInteractionBase
    {

        Int32 _instance, _refMasterReaderId, _refServerId, _templateID, _refGroupId, _dataOwnerId, _userId,_locationId;
        String _instanceName, _name, _description, _readerConfig;
        Boolean _isDisabled;

        //selectAll()

        public override DataTable SelectAll()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.pr_ReaderInstance_SelectAll"; //[pr_ReaderInstance_SelectAll] // pr_Reader_SelectAll
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("ReaderInstance");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerId", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _dataOwnerId));
                cmdToExecute.Parameters.Add(new SqlParameter("@LocationId", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _locationId));               
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
                    throw new Exception("Stored Procedure 'pr_ReaderInstance_SelectAll' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("ReaderInstance::SelectAll::Error occured.", ex);
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


        //SelectOne()

        public override DataTable SelectOne()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_ReaderInstance_SelectOne]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@RefComponentId", SqlDbType.Int, 8, ParameterDirection.Input, false, 19, 0, "", DataRowVersion.Proposed, _refMasterReaderId));
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
                    throw new Exception("Stored Procedure 'pr_ReaderInstance_SelectOne' reported the ErrorCode: " + _errorCode);
                }

                if (toReturn.Rows.Count > 0)
                {

                    _instanceName = toReturn.Rows[0]["InstanceName"].ToString();
                    _name = toReturn.Rows[0]["Name"].ToString();
                    _description = toReturn.Rows[0]["Description"].ToString();
                    _refMasterReaderId = Convert.ToInt32(toReturn.Rows[0]["RefMasterReaderId"]);
                    _refServerId = Convert.ToInt32(toReturn.Rows[0]["RefServerId"]);
                    //_templateID = Convert.ToInt32(toReturn.Rows[0]["TemplateID"]);
                    _readerConfig = toReturn.Rows[0]["ReaderConfig"].ToString();
                    _isDisabled = Convert.ToBoolean(toReturn.Rows[0]["IsDisabled"].ToString());                   
                    _refGroupId = Convert.ToInt32(toReturn.Rows[0]["RefGroupId"].ToString());
                    _userId = Convert.ToInt32(toReturn.Rows[0]["UserId"].ToString());

                }
                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("ReaderInstance::SelectOne::Error occured.", ex);
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

        public Int32 Instance
        {

            get
            {
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }

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

        public String InstanceName
        {

            get
            {
                return _instanceName;
            }
            set
            {
                _instanceName = value;
            }
        }
        public String Name
        {

            get
            {
                return _name;
            }
            set
            {
                _name = value;
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
        public Int32 RefMasterReaderId
        {

            get
            {
                return _refMasterReaderId;
            }
            set
            {
                _refMasterReaderId = value;
            }
        }
        public Int32 RefServerId
        {

            get
            {
                return _refServerId;
            }
            set
            {
                _refServerId = value;
            }
        }
        public Int32 TemplateID
        {

            get
            {
                return _templateID;
            }
            set
            {
                _templateID = value;
            }
        }
        public String ReaderConfig
        {

            get
            {
                return _readerConfig;
            }
            set
            {
                _readerConfig = value;
            }
        }
        public Boolean IsDisabled
        {

            get
            {
                return _isDisabled;
            }
            set
            {
                _isDisabled = value;
            }
        }
        public Int32 RefGroupId
        {

            get
            {
                return _refGroupId;
            }
            set
            {
                _refGroupId = value;
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
        public Int32 UserId
        {

            get
            {
                return _userId;
            }
            set
            {
                _userId = value;
            }
        }

        #endregion
    }
}
