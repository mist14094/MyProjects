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
    public class KT_PutPickDetails : DBInteractionBase
    {
        #region Private Fields 
        private int _StoreID;
        private int _LocationID;
        private string _LocationName;
        private int _CreatedBy;
        private DateTime _CreatedDate;
        private int _UpdatedBy;
        private DateTime _UpdateDate;
        private string _UPC;
        private string _SKU;
        private int _Quantity;
        private int _PuAwayQty;
        private int  _ActuallyPutAwayQty;
        private string _Status;
        private string _ListNo;
        private string _ListType;
        #endregion

        #region Constructors
         public KT_PutPickDetails() { }
        #endregion

        #region Methods

         public string GetLastSeenLocationOnUPC()
         {
             _log.Debug("KT_PutPickDetails:GetLastSeenLocationOnUPC:: Entering");
             SqlCommand scmCmdToExecute = new SqlCommand();
             scmCmdToExecute.CommandText = "dbo.[pr_GetLastSeenLocation]";
             scmCmdToExecute.CommandType = CommandType.StoredProcedure;
             DataTable dtToReturn = new DataTable("Product");
             SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

             scmCmdToExecute.Connection = _trackerRetailConnection;
             string Location = string.Empty;
             try
             {
                 scmCmdToExecute.Parameters.Add(new SqlParameter("@UPC", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, UPC));
                 scmCmdToExecute.Parameters.Add(new SqlParameter("@StoreID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, StoreID));
                 scmCmdToExecute.Parameters.Add(new SqlParameter("@ERRORCODE", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                 // Open connection.
                 if (_trackerRetailConnectionIsCreatedLocal)
                 {
                     // Open connection.
                     _trackerRetailConnection.Open();
                 }

                 // Execute query.
                 sdaAdapter.Fill(dtToReturn);
                 if (dtToReturn != null && dtToReturn.Rows.Count > 0)
                 {
                     Location = dtToReturn.Rows[0]["LocationName"].ToString();
                 }
                 _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ERRORCODE"].Value;

                 if (_errorCode != 0)
                 {
                     // Throw error.
                     throw new Exception("Stored Procedure 'pr_GetLastSeenLocation' reported the ErrorCode: " + _errorCode);
                 }

             }
             catch (Exception ex)
             {
                 // some error occured. Bubble it to caller and encapsulate Exception object
                 throw new Exception("KT_PutPickDetails::GetLastSeenLocationOnUPC::Error occured.", ex);
             }
             finally
             {
                 // Close connection.
                 _trackerRetailConnection.Close();
                 scmCmdToExecute.Dispose();
                 sdaAdapter.Dispose();
             }
             return Location;
         }

         public bool UpdatePutAwayItems(string Operation, string ReceiptNo, out string ERRORMSG, out string CompleteMSG)
         {
             SqlCommand scmCmdToExecute = new SqlCommand();
             scmCmdToExecute.CommandText = "dbo.[pr_UpdatePutAwayItems]";
             scmCmdToExecute.CommandType = CommandType.StoredProcedure;
             scmCmdToExecute.CommandTimeout = 20 * 60;
             _log.Debug("KT_PutPickDetails:UpdatePutAwayItems:: Entering");
              
             scmCmdToExecute.Connection = _trackerRetailConnection;
             ERRORMSG = string.Empty; CompleteMSG = string.Empty;
             try
             {
                 scmCmdToExecute.Parameters.Add(new SqlParameter("@UPC", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, UPC)); 
                 scmCmdToExecute.Parameters.Add(new SqlParameter("@StoreID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, StoreID));
                 scmCmdToExecute.Parameters.Add(new SqlParameter("@Quantity", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, Quantity));
                 scmCmdToExecute.Parameters.Add(new SqlParameter("@Location", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, LocationName));
                 scmCmdToExecute.Parameters.Add(new SqlParameter("@Operation", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, Operation));
                 scmCmdToExecute.Parameters.Add(new SqlParameter("@ReceiptNo", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, ReceiptNo));
                 scmCmdToExecute.Parameters.Add(new SqlParameter("@UpdatedBy", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, UpdatedBy));
                 scmCmdToExecute.Parameters.Add(new SqlParameter("@ERRORCODE", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                 scmCmdToExecute.Parameters.Add(new SqlParameter("@COMPLETEMSG", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Proposed, CompleteMSG));


                 // Open connection.
                 if (_trackerRetailConnectionIsCreatedLocal)
                 {
                     // Open connection.
                     _trackerRetailConnection.Open();
                 }

                 // Execute query.

                 scmCmdToExecute.ExecuteNonQuery();

                 _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ERRORCODE"].Value;


                 CompleteMSG = Convert.ToString(scmCmdToExecute.Parameters["@COMPLETEMSG"].Value);
                

                 if (_errorCode == -1)
                 {
                     return false;
                 }
                 if (_errorCode == 1)
                 {
                     ERRORMSG = "Location does not exist.";
                     return false;
                 }
                 if (_errorCode == 4)
                 {
                     ERRORMSG = "PutAway Receipt does not exist.";
                     return false;
                 }
                 return true;
             }
             catch (Exception ex)
             {
                 // some error occured. Bubble it to caller and encapsulate Exception object
                 throw new Exception("KT_PutPickDetails::UpdatePutAwayItems::Error occured.", ex);
             }
             finally
             {
                 // Close connection.
                 _trackerRetailConnection.Close();
                 scmCmdToExecute.Dispose();

                 _log.Debug("KT_PutPickDetails:UpdatePutAwayItems:: Exiting");

             }
         }

         public bool UpdatePickedItems(string Operation, string ReceiptNo, out string ERRORMSG, out string CompleteMSG)
         {
             SqlCommand scmCmdToExecute = new SqlCommand();
             scmCmdToExecute.CommandText = "dbo.[pr_UpdatePickedItems]";
             scmCmdToExecute.CommandType = CommandType.StoredProcedure;
             scmCmdToExecute.CommandTimeout = 20 * 60;
             _log.Debug("KT_PutPickDetails:UpdatePickedItems:: Entering");
             ERRORMSG = string.Empty;
             CompleteMSG = string.Empty;
             scmCmdToExecute.Connection = _trackerRetailConnection;

             try
             {
                 scmCmdToExecute.Parameters.Add(new SqlParameter("@UPC", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, UPC));
                 scmCmdToExecute.Parameters.Add(new SqlParameter("@StoreID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, StoreID));
                 scmCmdToExecute.Parameters.Add(new SqlParameter("@Quantity", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, Quantity));
                 scmCmdToExecute.Parameters.Add(new SqlParameter("@Location", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, LocationName));
                 scmCmdToExecute.Parameters.Add(new SqlParameter("@Operation", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, Operation));
                 scmCmdToExecute.Parameters.Add(new SqlParameter("@ReceiptNo", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, ReceiptNo));
                 scmCmdToExecute.Parameters.Add(new SqlParameter("@UpdatedBy", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, UpdatedBy));
                 scmCmdToExecute.Parameters.Add(new SqlParameter("@ERRORCODE", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                 scmCmdToExecute.Parameters.Add(new SqlParameter("@COMPLETEMSG", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Proposed, CompleteMSG));

                 // Open connection.
                 if (_trackerRetailConnectionIsCreatedLocal)
                 {
                     // Open connection.
                     _trackerRetailConnection.Open();
                 }

                 // Execute query.

                 scmCmdToExecute.ExecuteNonQuery();

                 _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ERRORCODE"].Value;


                 CompleteMSG = Convert.ToString(scmCmdToExecute.Parameters["@COMPLETEMSG"].Value);
                 //if (_errorCode != 0 && _errorCode != -1)
                 //{
                 //    // Throw error.
                 //    throw new Exception("Stored Procedure 'pr_UpdatePickedItems' reported the ErrorCode: " + _errorCode);
                 //}
                 if (_errorCode == -1)
                 {
                     return false;
                 }
                 if (_errorCode == 1)
                 {
                     ERRORMSG = "Location does not exist.";
                     return false;
                 }
                 if (_errorCode == 4)
                 {
                     ERRORMSG = "RR does not exist.";
                     return false;
                 }
                 if (_errorCode == 2)
                 {
                     ERRORMSG = "Available Quantity is less than given Pick Quantity.";//"Quantity exceeds the Pick quantity.";
                     return false;
                 }
                 return true;
             }
             catch (Exception ex)
             {
                 // some error occured. Bubble it to caller and encapsulate Exception object
                 throw new Exception("KT_PutPickDetails::UpdatePickedItems::Error occured.", ex);
             }
             finally
             {
                 // Close connection.
                 _trackerRetailConnection.Close();
                 scmCmdToExecute.Dispose();

                 _log.Debug("KT_PutPickDetails:UpdatePickedItems:: Exiting");

             }
         }

         public DataTable GetLocationQuantityOnUPC()
         {
             SqlCommand scmCmdToExecute = new SqlCommand();
             scmCmdToExecute.CommandText = "dbo.[pr_GetUPCQtyForLocation]";
             scmCmdToExecute.CommandType = CommandType.StoredProcedure;
             scmCmdToExecute.CommandTimeout = 20 * 60;
             DataTable dtToReturn = new DataTable("Product");
             SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

             scmCmdToExecute.Connection = _trackerRetailConnection;

             try
             {
                 scmCmdToExecute.Parameters.Add(new SqlParameter("@UPC", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, UPC));
                 scmCmdToExecute.Parameters.Add(new SqlParameter("@StoreID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, StoreID));
                 scmCmdToExecute.Parameters.Add(new SqlParameter("@ERRORCODE", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                 // Open connection.
                 if (_trackerRetailConnectionIsCreatedLocal)
                 {
                     // Open connection.
                     _trackerRetailConnection.Open();
                 }

                 // Execute query.
                 sdaAdapter.Fill(dtToReturn);
                 _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ERRORCODE"].Value;

                 if (_errorCode != 0)
                 {
                     // Throw error.
                     throw new Exception("Stored Procedure 'pr_GetUPCQtyForLocation' reported the ErrorCode: " + _errorCode);
                 }

             }
             catch (Exception ex)
             {
                 // some error occured. Bubble it to caller and encapsulate Exception object
                 throw new Exception("Products::GetLocationQuantityOnUPC::Error occured.", ex);
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

         public DataTable GetActivePutPickList(string LstType)
         {
             SqlCommand scmCmdToExecute = new SqlCommand();
             scmCmdToExecute.CommandText = "dbo.[pr_GetActivePutPickList]";
             scmCmdToExecute.CommandType = CommandType.StoredProcedure;
             scmCmdToExecute.CommandTimeout = 20 * 60;
             DataTable dtToReturn = new DataTable("ActiveList");
             SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

             scmCmdToExecute.Connection = _trackerRetailConnection;

             try
             {
                 scmCmdToExecute.Parameters.Add(new SqlParameter("@ListType", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, LstType));
                 scmCmdToExecute.Parameters.Add(new SqlParameter("@StoreID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, StoreID));
                 scmCmdToExecute.Parameters.Add(new SqlParameter("@ERRORCODE", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                 // Open connection.
                 if (_trackerRetailConnectionIsCreatedLocal)
                 {
                     // Open connection.
                     _trackerRetailConnection.Open();
                 }

                 // Execute query.
                 sdaAdapter.Fill(dtToReturn);
                 _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ERRORCODE"].Value;

                 if (_errorCode != 0)
                 {
                     // Throw error.
                     throw new Exception("Stored Procedure 'pr_GetActivePutPickList' reported the ErrorCode: " + _errorCode);
                 }

             }
             catch (Exception ex)
             {
                 // some error occured. Bubble it to caller and encapsulate Exception object
                 throw new Exception("Products::GetActivePutPickList::Error occured.", ex);
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

         public DataTable GetItemDetailsOnPutPickReceiptNo()
         {
             SqlCommand scmCmdToExecute = new SqlCommand();
             scmCmdToExecute.CommandText = "dbo.[pr_GetItemDetailsOnPutPickReceiptNo]";
             scmCmdToExecute.CommandType = CommandType.StoredProcedure;
             scmCmdToExecute.CommandTimeout = 20 * 60;
             DataTable dtToReturn = new DataTable("ActiveList");
             SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

             scmCmdToExecute.Connection = _trackerRetailConnection;

             try
             {
                 scmCmdToExecute.Parameters.Add(new SqlParameter("@ListType", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, ListType));
                 scmCmdToExecute.Parameters.Add(new SqlParameter("@ListNo", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, ListNo));
                 scmCmdToExecute.Parameters.Add(new SqlParameter("@StoreID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, StoreID));
                 scmCmdToExecute.Parameters.Add(new SqlParameter("@ERRORCODE", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                 // Open connection.
                 if (_trackerRetailConnectionIsCreatedLocal)
                 {
                     // Open connection.
                     _trackerRetailConnection.Open();
                 }

                 // Execute query.
                 sdaAdapter.Fill(dtToReturn);
                 _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ERRORCODE"].Value;

                 if (_errorCode != 0)
                 {
                     // Throw error.
                     throw new Exception("Stored Procedure 'pr_GetItemDetailsOnPutPickReceiptNo' reported the ErrorCode: " + _errorCode);
                 }

             }
             catch (Exception ex)
             {
                 // some error occured. Bubble it to caller and encapsulate Exception object
                 throw new Exception("Products::GetItemDetailsOnPutPickReceiptNo::Error occured.", ex);
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

         public bool InsertPackingSlip(string PackingSlip , int userid)
         {
             SqlCommand scmCmdToExecute = new SqlCommand();
             scmCmdToExecute.CommandText = "dbo.[pr_GeneratePutAwayList_OnPackingSlip]";
             scmCmdToExecute.CommandType = CommandType.StoredProcedure;
             scmCmdToExecute.CommandTimeout = 20 * 60;
             _log.Debug("KT_PutPickDetails:InsertPackingSlip:: Entering");

             scmCmdToExecute.Connection = _trackerRetailConnection;

             try
             {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ListNumber", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, PackingSlip));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ListType", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, ListType));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@StoreID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, StoreID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@Status", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, Status));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, userid));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ERRORCODE", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                 // Open connection.
                 if (_trackerRetailConnectionIsCreatedLocal)
                 {
                     // Open connection.
                     _trackerRetailConnection.Open();
                 }

                 // Execute query.

                 scmCmdToExecute.ExecuteNonQuery();

                 _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ERRORCODE"].Value;

                 if (_errorCode != 0 && _errorCode != -1)
                 {
                     // Throw error.
                     throw new Exception("Stored Procedure 'pr_GeneratePutAwayList_OnPackingSlip' reported the ErrorCode: " + _errorCode);
                 }


                 if (_errorCode == -1)
                 {
                     return false;
                 }
                 return true;
             }
             catch (Exception ex)
             {
                 // some error occured. Bubble it to caller and encapsulate Exception object
                 throw new Exception("KT_PutPickDetails::InsertPackingSlip::Error occured.", ex);
             }
             finally
             {
                 // Close connection.
                 _trackerRetailConnection.Close();
                 scmCmdToExecute.Dispose();

                 _log.Debug("KT_PutPickDetails:InsertPackingSlip:: Exiting");

             }
         }


         public bool UpdateBinProductMasterOnPutAway(int userid,out string ERRORMSG)
         {
             SqlCommand scmCmdToExecute = new SqlCommand();
             scmCmdToExecute.CommandText = "dbo.[pr_InsertPutAway_BinProductMaster]";
             scmCmdToExecute.CommandType = CommandType.StoredProcedure;
             scmCmdToExecute.CommandTimeout = 20 * 60;
             _log.Debug("KT_PutPickDetails:UpdateBinProductMasterOnPutAway:: Entering");

             scmCmdToExecute.Connection = _trackerRetailConnection;
             ERRORMSG = string.Empty;
             try
             {                 
                 scmCmdToExecute.Parameters.Add(new SqlParameter("@UPC", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, UPC));
                 scmCmdToExecute.Parameters.Add(new SqlParameter("@Location", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, LocationName));                  
                 scmCmdToExecute.Parameters.Add(new SqlParameter("@StoreID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, StoreID));
                 scmCmdToExecute.Parameters.Add(new SqlParameter("@UpdatedBy", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, userid));
                 scmCmdToExecute.Parameters.Add(new SqlParameter("@ERRORCODE", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                 // Open connection.
                 if (_trackerRetailConnectionIsCreatedLocal)
                 {
                     // Open connection.
                     _trackerRetailConnection.Open();
                 }

                 // Execute query.

                 scmCmdToExecute.ExecuteNonQuery();

                 _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ERRORCODE"].Value;

              
                 if (_errorCode == 1)
                 {
                     ERRORMSG = "Location not exist";
                     return false;
                 }
                 if (_errorCode == 2)
                 {
                     ERRORMSG = "Items not exists";
                     return false;
                 }
                 if (_errorCode == -1)
                 {
                     return false;
                 }
                 return true;
             }
             catch (Exception ex)
             {
                 // some error occured. Bubble it to caller and encapsulate Exception object
                 throw new Exception("KT_PutPickDetails::UpdateBinProductMasterOnPutAway::Error occured.", ex);
             }
             finally
             {
                 // Close connection.
                 _trackerRetailConnection.Close();
                 scmCmdToExecute.Dispose();

                 _log.Debug("KT_PutPickDetails:UpdateBinProductMasterOnPutAway:: Exiting");

             }
         }


         public bool ValidateRR(string RR)
         {
             bool result = true;

             SqlCommand scmCmdToExecute = new SqlCommand();
             scmCmdToExecute.CommandText = "dbo.[pr_ValidatePickList_RR]";
             scmCmdToExecute.CommandType = CommandType.StoredProcedure;
             scmCmdToExecute.CommandTimeout = 20 * 60;
             DataTable dtToReturn = new DataTable("RR");
             SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

             scmCmdToExecute.Connection = _trackerRetailConnection;

             try
             {
                 scmCmdToExecute.Parameters.Add(new SqlParameter("@RRNumber", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, RR));

                 // Open connection.
                 if (_trackerRetailConnectionIsCreatedLocal)
                 {
                     // Open connection.
                     _trackerRetailConnection.Open();
                 }

                 // Execute query.
                 sdaAdapter.Fill(dtToReturn);

                 if (dtToReturn != null && dtToReturn.Rows.Count > 0)
                 {
                     result = false;
                 }

             }
             catch (Exception ex)
             {
                 // some error occured. Bubble it to caller and encapsulate Exception object
                 throw new Exception("Products::ValidateRR::Error occured.", ex);
             }
             finally
             {
                 // Close connection.
                 _trackerRetailConnection.Close();
                 scmCmdToExecute.Dispose();
                 sdaAdapter.Dispose();
             }


             return result;

         }

         #endregion Methods

        #region Public Properties
            public string UPC { get { return _UPC; } set { _UPC = value; } }
            public string SKU { get { return _SKU; } set { _SKU = value; } }
            public int LocationID { get { return _LocationID; } set { _LocationID = value; } }
            public string LocationName { get { return _LocationName; } set { _LocationName = value; } }
            public int StoreID { get { return _StoreID; } set { _StoreID = value; } }
            public int CreatedBy { get { return _CreatedBy; } set { _CreatedBy = value; } }
            public DateTime CreatedDate { get { return _CreatedDate; } set { _CreatedDate = value; } }
            public int UpdatedBy { get { return _UpdatedBy; } set { _UpdatedBy = value; } }
            public DateTime UpdateDate { get { return _UpdateDate; } set { _UpdateDate = value; } }
            public int Quantity { get { return _Quantity; } set { _Quantity = value; } }
            public int PuAwayQty { get { return _PuAwayQty; } set { _PuAwayQty = value; } }
            public int ActuallyPutAwayQty { get { return _ActuallyPutAwayQty; } set { _ActuallyPutAwayQty = value; } }
            public string Status { get { return _Status; } set { _Status = value; } }
            public string ListNo { get { return _ListNo; } set { _ListNo = value; } }
            public string ListType { get { return _ListType; } set { _ListType = value; } }
        #endregion
    }
}
