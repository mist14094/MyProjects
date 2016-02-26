using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KTone.DAL.KTDBBaseLib;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;

namespace KTone.DAL.KTDAGlobaApplLib
{
   public class AssetCustomField : DBInteractionBase
    {
        #region Class Member Declarations
        //private Int16 _assetCustomColID;
        private String _AssetCustomFieldXML;
        //private String _customColName,
        //private Boolean _isMandatory, _webVisibility, _appVisibility;
        private Int32 _dataOwnerID;
        //Int32 _customFieldGroupID;
        #endregion

        public override bool Insert()
        {
            _log.Trace("Entering Insert - Table:AssetCustomField AssetCustomFieldXML :{0}" +_AssetCustomFieldXML);
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "[dbo].[pr_AssetCustomField_Insert]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            cmdToExecute.Connection = _mainConnection;
            string errorMsg = string.Empty;
            try
            {
                
                if (_AssetCustomFieldXML != string.Empty)
                {
                    cmdToExecute.Parameters.Add(new SqlParameter("@AssetCustomFieldXML", SqlDbType.Xml));
                    cmdToExecute.Parameters["@AssetCustomFieldXML"].Value = _AssetCustomFieldXML;
                }
                else
                    throw new ApplicationException("Supply proper data.");
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorMsg", SqlDbType.VarChar, 1000, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, errorMsg));

                if (_mainConnectionProvider.IsTransactionPending)
                {
                    cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                }

                // Execute query.
                cmdToExecute.ExecuteNonQuery();
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;
                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Error Occured: " + cmdToExecute.Parameters["@ErrorMsg"].Value.ToString() + ": " + _errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Insert:{0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("AssetCustomField::Insert::Error occured.", ex);
            }
            finally
            {
                cmdToExecute.Dispose();
                _log.Trace("Exiting Insert");
            }
        }


        public String AssetCustomFieldXML
        {
            get
            {
                return _AssetCustomFieldXML;
            }
            set
            {
                _AssetCustomFieldXML = value;
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
    }
}
