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
    public class KT_ReplensihmentRequestDetails : DBInteractionBase
    {
        #region Constructors
        public KT_ReplensihmentRequestDetails() { }
        #endregion

        #region Private Fields
        private int _RRD_ID;
        private int _RR_ID;
        private string _RRD_Status;
        private DateTime _GenerationTime;
        private string _UPC;
        private string _SKU;
        private int _StoreID;
        private int _OrderedQty;
        private int _ShippedQty;
        private string _Comments;
        private int _CreatedBy;
        private DateTime _CreatedDate;
        private int _UpdatedBy;
        private DateTime _UpdateDate;
        #endregion

        #region Public Properties
        public int RRD_ID { get { return _RRD_ID; } set { _RRD_ID = value; } }
        public int RR_ID { get { return _RR_ID; } set { _RR_ID = value; } }
        public string RRD_Status { get { return _RRD_Status; } set { _RRD_Status = value; } }
        public DateTime GenerationTime { get { return _GenerationTime; } set { _GenerationTime = value; } }
        public string UPC { get { return _UPC; } set { _UPC = value; } }
        public string SKU { get { return _SKU; } set { _SKU = value; } }
        public int StoreID { get { return _StoreID; } set { _StoreID = value; } }
        public int OrderedQty { get { return _OrderedQty; } set { _OrderedQty = value; } }
        public int ShippedQty { get { return _ShippedQty; } set { _ShippedQty = value; } }
        public string Comments { get { return _Comments; } set { _Comments = value; } }
        public int CreatedBy { get { return _CreatedBy; } set { _CreatedBy = value; } }
        public DateTime CreatedDate { get { return _CreatedDate; } set { _CreatedDate = value; } }
        public int UpdatedBy { get { return _UpdatedBy; } set { _UpdatedBy = value; } }
        public DateTime UpdateDate { get { return _UpdateDate; } set { _UpdateDate = value; } }
        #endregion


        #region [PublicMethod]

        public DataTable GetCategoryReplenishment_BeforeScan(int storeID, long RR_ID)
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_SelectProductsForCheckOut_BeforeScan]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable dtToReturn = new DataTable("ProductCategory");
            SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);
            scmCmdToExecute.CommandTimeout = 20 * 60;

            //SqlConnection _mainConnection = new SqlConnection(AppConfigSettings.GetInstance().TrackRetailConnectoinString);
            // Use base class' connection object
            scmCmdToExecute.Connection = _trackerRetailConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@STOREID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, storeID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@RR_Number", SqlDbType.BigInt, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, RR_ID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@Error", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _trackerRetailConnection.Open();
                }

                // Execute query.
                sdaAdapter.Fill(dtToReturn);
                _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@Error"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_SelectProductsForCheckOut_BeforeScan' reported the ErrorCode: " + _errorCode);
                }

            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("KT_ReplensihmentRequestDetails::GetCategoryReplenishment_BeforeScan::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _trackerRetailConnection.Close();
                scmCmdToExecute.Dispose();
                sdaAdapter.Dispose();
            }
            return dtToReturn;
        }


        #endregion
    }
}
