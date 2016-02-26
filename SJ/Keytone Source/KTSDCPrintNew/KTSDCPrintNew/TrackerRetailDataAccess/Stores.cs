using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KTone.DAL.KTDBBaseLib;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace TrackerRetailDataAccess
{
    public class StoresMaster : DBInteractionBase
    {
        #region Constructors
        public StoresMaster() { }
        #endregion

        #region Private Fields
        private int _StoreID;
        private string _StoreName;
        #endregion

        #region Public Properties
        public int StoreID { get { return _StoreID; } set { _StoreID = value; } }
        public string StoreName { get { return _StoreName; } set { _StoreName = value; } }
        #endregion

        #region [Methods]

        public DataTable GetAllStores()
        {
            _log.Trace("Entering GetAllStores()");

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_Stores_SelectAll]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);
            DataTable dtProduct = new DataTable("StoresTable");
            cmdToExecute.CommandTimeout = 20 * 60;

            cmdToExecute.Connection = _trackerRetailConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _trackerRetailConnection.Open();
                }
                

                // Fill table
                adapter.Fill(dtProduct);

                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_Stores_SelectAll' reported the ErrorCode: " + _errorCode);
                }

            }
            catch (Exception ex)
            {
                _log.Error("Error:GetAllStores:{0}", ex.Message);
            }
            finally
            {
                _log.Trace("Leaving GetAllStores");

                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    //close connection
                    _trackerRetailConnection.Close();
                }
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
            return dtProduct;
        }

        public DataTable GetAllDevices()
        {
            _log.Trace("Entering GetAllDevices()");

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_SelectAllDevices]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);
            DataTable dtProduct = new DataTable("Devices");
            cmdToExecute.CommandTimeout = 20 * 60;

            cmdToExecute.Connection = _trackerRetailConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@Error", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _trackerRetailConnection.Open();
                }


                // Fill table
                adapter.Fill(dtProduct);

                _errorCode = (SqlInt32)cmdToExecute.Parameters["@Error"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_Stores_SelectAll' reported the ErrorCode: " + _errorCode);
                }

            }
            catch (Exception ex)
            {
                _log.Error("Error:GetAllDevices:{0}", ex.Message);
            }
            finally
            {
                _log.Trace("Leaving GetAllDevices");

                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    //close connection
                    _trackerRetailConnection.Close();
                }
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
            return dtProduct;
        }


        public DataTable GetAllLocations()
        {
            _log.Trace("Entering GetAllLocations()");

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_Locations_SelectAll]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);
            DataTable dtProduct = new DataTable("LocationsTable");
            cmdToExecute.CommandTimeout = 20 * 60;

            cmdToExecute.Connection = _trackerRetailConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _trackerRetailConnection.Open();
                }


                // Fill table
                adapter.Fill(dtProduct);

                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_Locations_SelectAll' reported the ErrorCode: " + _errorCode);
                }

            }
            catch (Exception ex)
            {
                _log.Error("Error:GetAllLocations():{0}", ex.Message);
            }
            finally
            {
                _log.Trace("Leaving GetAllLocations()");

                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    //close connection
                    _trackerRetailConnection.Close();
                }
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
            return dtProduct;
        }


        #endregion [Methods]
    }
}
