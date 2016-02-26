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
    public class GTINSerialNo : DBInteractionBase
    {
       private Int64 _skuID;
       public  DataTable SelectOneSrno()
       {
           SqlCommand cmdToExecute = new SqlCommand();
           cmdToExecute.CommandText = "dbo.[pr_GTIN_SerialNo]";
           cmdToExecute.CommandType = CommandType.StoredProcedure;
           DataTable toReturn = new DataTable("GTIN_SerialNo");
           SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

           // Use base class' connection object
           cmdToExecute.Connection = _mainConnection;

           try
           {

               cmdToExecute.Parameters.Add(new SqlParameter("@SKUID", SqlDbType.Int, 10, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _skuID));
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
                   throw new Exception("Stored Procedure 'pr_GTIN_SerialNo' reported the ErrorCode: " + _errorCode);
               }

               return toReturn;
           }
           catch (Exception ex)
           {
               // some error occured. Bubble it to caller and encapsulate Exception object
               throw new Exception("GTIN_SerialNo::SelectOneSrno::Error occured.", ex);
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
       public DataTable GetSrno()
       {
           SqlCommand cmdToExecute = new SqlCommand();
           cmdToExecute.CommandText = "dbo.[pr_GetGTIN_SerialNo]";
           cmdToExecute.CommandType = CommandType.StoredProcedure;
           DataTable toReturn = new DataTable("GTIN_SerialNo");
           SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

           // Use base class' connection object
           cmdToExecute.Connection = _mainConnection;

           try
           {

               cmdToExecute.Parameters.Add(new SqlParameter("@SKUID", SqlDbType.Int, 10, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _skuID));
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
                   throw new Exception("Stored Procedure 'pr_GetGTIN_SerialNo' reported the ErrorCode: " + _errorCode);
               }

               return toReturn;
           }
           catch (Exception ex)
           {
               // some error occured. Bubble it to caller and encapsulate Exception object
               throw new Exception("GTIN_SerialNo::GetSrno::Error occured.", ex);
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
       #region Class Property Declarations
       public Int64  SKUID
        {
            get
            {
                return _skuID ;
            }
            set
            {
                _skuID = value;
            }
        }

         #endregion

    }


    
}
