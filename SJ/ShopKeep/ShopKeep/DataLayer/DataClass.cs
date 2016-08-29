using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NLog;
using Constants;
namespace DataLayer
{
    public class DataClass
    {
        internal Logger Nlog = LogManager.GetCurrentClassLogger();
        private Constants.ConstantsLayer _constants= new Constants.ConstantsLayer();
        private Constants.Transaction _transaction;
        private string _connectionString;

        public DataClass()
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            _connectionString = _constants.DefaultConnectionString;
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Exit");
        }

        public DataTable InsertIntoDatabase(Transaction transData)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(ConstantsLayer.ImportSalesData)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.StoredProcedure
            };

            selectCommand.Parameters.AddWithValue("@TransactionId", transData.TransactionId);
            selectCommand.Parameters.AddWithValue("@TransactionType", transData.TransactionType);
            selectCommand.Parameters.AddWithValue("@StoreCode", transData.StoreCode);
            selectCommand.Parameters.AddWithValue("@ItemDescription", transData.ItemDescription);
            selectCommand.Parameters.AddWithValue("@Category", transData.Category);
            selectCommand.Parameters.AddWithValue("@Department", transData.Department);
            selectCommand.Parameters.AddWithValue("@Supplier", transData.Supplier);
            selectCommand.Parameters.AddWithValue("@SupplierCode", transData.SupplierCode);
            selectCommand.Parameters.AddWithValue("@Cost", transData.Cost);
            selectCommand.Parameters.AddWithValue("@Price", transData.Price);
            selectCommand.Parameters.AddWithValue("@Quantity", transData.Quantity);
            selectCommand.Parameters.AddWithValue("@Modifiers", transData.Modifiers);
            selectCommand.Parameters.AddWithValue("@Subtotal", transData.Subtotal);
            selectCommand.Parameters.AddWithValue("@Tax", transData.Tax);
            selectCommand.Parameters.AddWithValue("@Discount", transData.Discount);
            selectCommand.Parameters.AddWithValue("@Total", transData.Total);
            selectCommand.Parameters.AddWithValue("@Cashier", transData.Cashier);
            selectCommand.Parameters.AddWithValue("@Time", transData.Time);
            selectCommand.Parameters.AddWithValue("@Register", transData.Register);
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.DefaultConnectionString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", exception: ex);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message:this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }
    }
}
