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

    /// <summary>
    /// Class is being used for storing manual print data to KT_ManualPrint and KT_ManualPrintTags
    /// </summary>

    public class KT_ManualPrint : DBInteractionBase
    {
        public bool InsertManualPrintDetail( string UPC, string SKU, string Description, float Cost, float Price, string Rfids, int userId)
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_InsertManualPrintDetails]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = 20 * 60;
            _log.Debug("KT_ManualPrint:InsertManualPrintDetail:: Entering");

            //SqlConnection _mainConnection = new SqlConnection(AppConfigSettings.GetInstance().TrackRetailConnectoinString);
            // Use base class' connection object
            scmCmdToExecute.Connection = _trackerRetailConnection;

            _errorCode = 0;
            try
            {
                //          

               
                scmCmdToExecute.Parameters.Add(new SqlParameter("@UPC", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, UPC));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@SKU", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, SKU));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 500, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, Description));            
                scmCmdToExecute.Parameters.Add(new SqlParameter("@Cost", SqlDbType.Float, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, Cost));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@Price", SqlDbType.Float, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, Price));               
                scmCmdToExecute.Parameters.Add(new SqlParameter("@RFIDs", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, Rfids));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, userId));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@Error", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, 0));

                // Open connection.
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _trackerRetailConnection.Open();
                }

                // Execute query.

                scmCmdToExecute.ExecuteNonQuery();
                _errorCode = 0;
                _errorCode = SqlInt32.Parse(scmCmdToExecute.Parameters["@Error"].Value.ToString());

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_InsertManualPrintDetails' reported the ErrorCode: " + _errorCode);
                }


                return true;

            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                _log.Error("KT_ManualPrint::InsertManualPrintDetail::Error occured.", ex.Message);
                throw new Exception("KT_ManualPrint::InsertManualPrintDetail::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _trackerRetailConnection.Close();
                scmCmdToExecute.Dispose();

                _log.Debug("KT_ManualPrint:InsertManualPrintDetail:: Exiting");

            }
        }

    }
}
