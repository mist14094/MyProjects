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
    public class ComponentInstance : DBInteractionBase
    {
        Int32 _id, _refComponentMasterId, _refServerId, _dataOwnerId, _userId , _locationId;
        String _instanceName, _name, _description, _componentConfig, _componentName;
        Boolean _isDisabled, _isDeleted, _isDefaultComponent;

        //selectAll()

        public override DataTable SelectAll()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_ComponentInstance_SelectAll]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("ComponentInstance");
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
                    throw new Exception("Stored Procedure 'pr_ComponentInstance_SelectAll' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("ComponentInstance::SelectAll::Error occured.", ex);
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

        public DataTable ComponentSelectAll()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_ComponentInstance_Component_SelectOne]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            { //@DataOwnerId
                cmdToExecute.Parameters.Add(new SqlParameter("@ComponentName",SqlDbType.VarChar, 4000, ParameterDirection.Input, false, 19, 0, "", DataRowVersion.Proposed, _componentName));
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerId", SqlDbType.Int, 4, ParameterDirection.Input, false, 19, 0, "", DataRowVersion.Proposed, _dataOwnerId));
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
                    throw new Exception("Stored Procedure 'pr_ComponentInstance_Component_SelectOne' reported the ErrorCode: " + _errorCode);
                }

                if (toReturn.Rows.Count > 0)
                {

                    _instanceName = toReturn.Rows[0]["InstanceName"].ToString();
                    _name = toReturn.Rows[0]["Name"].ToString();
                    _description = toReturn.Rows[0]["Description"].ToString();
                    _componentConfig = toReturn.Rows[0]["ComponentConfig"].ToString();
                    _isDisabled = Convert.ToBoolean(toReturn.Rows[0]["IsDisabled"].ToString());
                    _isDeleted = Convert.ToBoolean(toReturn.Rows[0]["IsDeleted"]);
                    _isDefaultComponent = Convert.ToBoolean(toReturn.Rows[0]["IsDefaultComponent"]);
                    _dataOwnerId = Convert.ToInt32(toReturn.Rows[0]["DataOwnerId"].ToString());
                    _userId = Convert.ToInt32(toReturn.Rows[0]["UserId"].ToString());

                }
                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("ComponentInstance::SelectOne::Error occured.", ex);
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
            cmdToExecute.CommandText = "dbo.[pr_ComponentInstance_SelectOne]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@RefComponentId", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _refComponentMasterId));
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerId", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _dataOwnerId));
                cmdToExecute.Parameters.Add(new SqlParameter("@LocationId", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed,_locationId ));               
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
                    throw new Exception("Stored Procedure 'pr_ComponentInstance_SelectOne' reported the ErrorCode: " + _errorCode);
                }

                if (toReturn.Rows.Count > 0)
                {

                    _instanceName = toReturn.Rows[0]["InstanceName"].ToString();
                    _name = toReturn.Rows[0]["Name"].ToString();
                    _description = toReturn.Rows[0]["Description"].ToString();
                    _componentConfig = toReturn.Rows[0]["ComponentConfig"].ToString();
                    _isDisabled = Convert.ToBoolean(toReturn.Rows[0]["IsDisabled"].ToString());
                    _isDeleted = Convert.ToBoolean(toReturn.Rows[0]["IsDeleted"]);
                    _isDefaultComponent = Convert.ToBoolean(toReturn.Rows[0]["IsDefaultComponent"]);
                    _dataOwnerId = Convert.ToInt32(toReturn.Rows[0]["DataOwnerId"].ToString());
                    _userId = Convert.ToInt32(toReturn.Rows[0]["UserId"].ToString());

                }
                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("ComponentInstance::SelectOne::Error occured.", ex);
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

        public Int32 Id
        {

            get
            {
                return _id;
            }
            set
            {
                _id = value;
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
        public Int32 RefComponentMasterId
        {

            get
            {
                return _refComponentMasterId;
            }
            set
            {
                _refComponentMasterId = value;
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

        public String ComponentName
        {

            get
            {
                return _componentName;
            }
            set
            {
                _componentName = value;
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
        public String ComponentConfig
        {

            get
            {
                return _componentConfig;
            }
            set
            {
                _componentConfig = value;
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
        public Boolean IsDeleted
        {

            get
            {
                return _isDeleted;
            }
            set
            {
                _isDeleted = value;
            }
        }
        public Boolean IsDefaultComponent
        {

            get
            {
                return _isDefaultComponent;
            }
            set
            {
                _isDefaultComponent = value;
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
