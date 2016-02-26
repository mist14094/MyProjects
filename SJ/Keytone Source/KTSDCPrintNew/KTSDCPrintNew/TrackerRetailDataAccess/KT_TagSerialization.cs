using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KTone.DAL.KTDBBaseLib;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;


namespace TrackerRetailDataAccess
{
    public class KT_TagSerialization : DBInteractionBase
    {
        #region Private Fields
            private string _NextSerialNumber;
            private DateTime _ModifiedOn;
        #endregion

        #region Constructors
            public KT_TagSerialization() { }
        #endregion

        #region [Properties]
            public string NextSerialNumber { get { return _NextSerialNumber; } set { _NextSerialNumber = value; } }
            public DateTime ModifiedOn { get { return _ModifiedOn; } set { _ModifiedOn = value; } }
        #endregion



        #region [Method]

            public bool GetNextSerialNumber(out string SerialNumber , int ToUpdate)
            {
                SqlCommand scmCmdToExecute = new SqlCommand();
                scmCmdToExecute.CommandText = "dbo.[pr_GetNextSerialNumber]";
                scmCmdToExecute.CommandType = CommandType.StoredProcedure;
                scmCmdToExecute.CommandTimeout = 20 * 60;
                _log.Debug("KT_TagSerialization:pr_GetNextSerialNumber:: Entering");

                //SqlConnection _mainConnection = new SqlConnection(AppConfigSettings.GetInstance().TrackRetailConnectoinString);
                // Use base class' connection object
                scmCmdToExecute.Connection = _trackerRetailConnection;

                SerialNumber = string.Empty;

                try
                {
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@SerialNumber", SqlDbType.VarChar, 50, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, NextSerialNumber));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@IncrementTo", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, ToUpdate));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@Error", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                    // Open connection.
                    if (_trackerRetailConnectionIsCreatedLocal)
                    {
                        // Open connection.
                        _trackerRetailConnection.Open();
                    }

                    // Execute query.

                    scmCmdToExecute.ExecuteNonQuery();

                    _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@Error"].Value;

                    if (_errorCode != 0 && _errorCode != -1)
                    {
                        // Throw error.
                        throw new Exception("Stored Procedure 'pr_GetNextSerialNumber' reported the ErrorCode: " + _errorCode);
                        return false;
                    }

                    SerialNumber = Convert.ToString(scmCmdToExecute.Parameters["@SerialNumber"].Value);
                    
                    return true;
                }
                catch (Exception ex)
                {
                    // some error occured. Bubble it to caller and encapsulate Exception object
                    throw new Exception("KT_TagSerialization::GetNextSerialNumber::Error occured.", ex);
                }
                finally
                {
                    // Close connection.
                    _trackerRetailConnection.Close();
                    scmCmdToExecute.Dispose();

                    _log.Debug("KT_TagSerialization:GetNextSerialNumber:: Exiting");

                }
            }


            public bool UpdateNextSerialNumber()
            {
                SqlCommand scmCmdToExecute = new SqlCommand();
                scmCmdToExecute.CommandText = "dbo.[pr_UpdateNextSerialNumber]";
                scmCmdToExecute.CommandType = CommandType.StoredProcedure;
                scmCmdToExecute.CommandTimeout = 20 * 60;
                _log.Debug("KT_TagSerialization:pr_UpdateNextSerialNumber:: Entering");

                //SqlConnection _mainConnection = new SqlConnection(AppConfigSettings.GetInstance().TrackRetailConnectoinString);
                // Use base class' connection object
                scmCmdToExecute.Connection = _trackerRetailConnection;
                               

                try
                {
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@SerialNumber", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, NextSerialNumber));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@Error", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                    // Open connection.
                    if (_trackerRetailConnectionIsCreatedLocal)
                    {
                        // Open connection.
                        _trackerRetailConnection.Open();
                    }

                    // Execute query.

                    scmCmdToExecute.ExecuteNonQuery();

                    _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@Error"].Value;

                    if (_errorCode != 0 && _errorCode != -1)
                    {
                        // Throw error.
                        throw new Exception("Stored Procedure 'pr_UpdateNextSerialNumber' reported the ErrorCode: " + _errorCode);
                    //    return false;
                    }

                   // SerialNumber = Convert.ToString(scmCmdToExecute.Parameters["@SerialNumber"].Value);

                    return true;
                }
                catch (Exception ex)
                {
                    // some error occured. Bubble it to caller and encapsulate Exception object

                    throw new Exception("KT_TagSerialization::UpdateNextSerialNumber::Error occured.", ex);
                }
                finally
                {
                    // Close connection.
                    _trackerRetailConnection.Close();
                    scmCmdToExecute.Dispose();

                    _log.Debug("KT_TagSerialization:UpdateNextSerialNumber:: Exiting");

                }
            }
            
        #endregion


    }
}
