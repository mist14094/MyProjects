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
    public class ReaderGroups : DBInteractionBase
    {
        Int32 _id, _dataOwnerId, _userId;
        String _groupName;


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
        public String GroupName
        {
            get
            {
                return _groupName;
            }
            set
            {
                _groupName = value;
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


        //SelectOne()

        public override DataTable SelectOne()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_ReaderGroup_SelectOne]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerId", SqlDbType.Int, 8, ParameterDirection.Input, false, 19, 0, "", DataRowVersion.Proposed, _dataOwnerId));
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
                    throw new Exception("Stored Procedure 'pr_ReaderGroup_SelectOne' reported the ErrorCode: " + _errorCode);
                }

                if (toReturn.Rows.Count > 0)
                {
                    _id = Convert.ToInt32(toReturn.Rows[0]["Id"]);
                    _groupName = Convert.ToString(toReturn.Rows[0]["GroupName"].ToString());
                    _dataOwnerId = Convert.ToInt32(toReturn.Rows[0]["DataOwnerId"]);
                    _userId = Convert.ToInt32(toReturn.Rows[0]["UserId"].ToString());

                }
                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("ReaderGroup::SelectOne::Error occured.", ex);
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


    }
}
