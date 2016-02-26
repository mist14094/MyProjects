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
    public class KT_ReplensihmentRequest :DBInteractionBase
    {
        #region Constructors
        public KT_ReplensihmentRequest() { }
        #endregion

        #region Private Fields
        private int _RR_ID;
        private string _RR_Number;
        private int _FromLocation;
        private int _ToLocation;
        private string _RR_Status;
        private DateTime _GenerationTime;
        private DateTime _FulfillmentDate;
        private int _CreatedBy;
        private DateTime _CreatedDate;
        private int _UpdatedBy;
        private DateTime _UpdateDate;
        private string _Comments;
        #endregion

        #region Public Properties
        public int RR_ID { get { return _RR_ID; } set { _RR_ID = value; } }
        public string RR_Number { get { return _RR_Number; } set { _RR_Number = value; } }
        public int FromLocation { get { return _FromLocation; } set { _FromLocation = value; } }
        public int ToLocation { get { return _ToLocation; } set { _ToLocation = value; } }
        public string RR_Status { get { return _RR_Status; } set { _RR_Status = value; } }
        public DateTime GenerationTime { get { return _GenerationTime; } set { _GenerationTime = value; } }
        public DateTime FulfillmentDate { get { return _FulfillmentDate; } set { _FulfillmentDate = value; } }
        public int CreatedBy { get { return _CreatedBy; } set { _CreatedBy = value; } }
        public DateTime CreatedDate { get { return _CreatedDate; } set { _CreatedDate = value; } }
        public int UpdatedBy { get { return _UpdatedBy; } set { _UpdatedBy = value; } }
        public DateTime UpdateDate { get { return _UpdateDate; } set { _UpdateDate = value; } }
        public string Comments { get { return _Comments; } set { _Comments = value; } }
        #endregion



        #region [publicmethods]

        public DataTable GetAllReplenishmentsRequest()
        {
            _log.Trace("Entering GetAllReplenishmentsRequest()");

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[SelectAll_Replenishment]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);
            DataTable dtProduct = new DataTable("StoresTable");
            cmdToExecute.CommandTimeout = 20 * 60;

            cmdToExecute.Connection = _trackerRetailConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@StoreID", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _FromLocation));
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
                    throw new Exception("Stored Procedure 'SelectAll_Replenishment' reported the ErrorCode: " + _errorCode);
                }

            }
            catch (Exception ex)
            {
                _log.Error("Error:GetAllReplenishmentsRequest:{0}", ex.Message);
            }
            finally
            {
                _log.Trace("Leaving GetAllReplenishmentsRequest");

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


        #endregion [publicmethods]
    }
}
