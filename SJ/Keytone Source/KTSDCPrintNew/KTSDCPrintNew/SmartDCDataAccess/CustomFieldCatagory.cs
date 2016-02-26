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
   public class CustomFieldCatagory:DBInteractionBase
    {
       public override DataTable SelectAll()
       {
           SqlCommand cmdToExecute = new SqlCommand();
           cmdToExecute.CommandText = "[dbo].[pr_CustomFieldCatagory_SelectAll]";
           cmdToExecute.CommandType = CommandType.StoredProcedure;
           DataTable toReturn = new DataTable("CustomFieldCatagory");
           SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);
           // Use base class' connection object
           cmdToExecute.Connection = _mainConnection;
           try
           {
               //cmdToExecute.Parameters.Add(new SqlParameter("@CustomFieldName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _customColName));
               //cmdToExecute.Parameters.Add(new SqlParameter("@Module", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _module));
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
                   throw new Exception("Stored Procedure '[dbo].[pr_CustomFieldCatagory_SelectAll]' reported the ErrorCode: " + _errorCode);
               }
               return toReturn;
           }
           catch (Exception ex)
           {
               // some error occured. Bubble it to caller and encapsulate Exception object
               throw new Exception("CustomFieldCatagory::SelectAll::Error occured.", ex);
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

       public override DataTable SelectOne()
       {
           SqlCommand cmdToExecute = new SqlCommand();
           cmdToExecute.CommandText = "[dbo].[pr_CustomFieldCatagory_SelectOne]";
           cmdToExecute.CommandType = CommandType.StoredProcedure;
           DataTable toReturn = new DataTable("CustomFieldCatagory");
           SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);
           // Use base class' connection object
           cmdToExecute.Connection = _mainConnection;
           try
           {
               //cmdToExecute.Parameters.Add(new SqlParameter("@CustomFieldName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _customColName));
               //cmdToExecute.Parameters.Add(new SqlParameter("@Module", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _module));
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
                   throw new Exception("Stored Procedure '[dbo].[pr_CustomFieldCatagory_SelectOne]' reported the ErrorCode: " + _errorCode);
               }
               return toReturn;
           }
           catch (Exception ex)
           {
               // some error occured. Bubble it to caller and encapsulate Exception object
               throw new Exception("CustomFieldCatagory::SelectOne::Error occured.", ex);
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
