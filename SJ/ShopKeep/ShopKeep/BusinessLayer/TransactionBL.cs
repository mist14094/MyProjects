using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Constants;
using DataLayer;
using NLog;
namespace BusinessLayer
{
    public class TransactionBL
    {
        private Logger _nlog = LogManager.GetCurrentClassLogger();
        private Constants.ConstantsLayer _constants;//= new Constants.ConstantsLayer();
        private Constants.Transaction _transaction;
        private DataLayer.DataClass _access;
        public TransactionBL()
        {
            _nlog.Trace(message:
               this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
               System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            _access = new DataClass();
            _nlog.Trace(message:
                this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "::Exit");
        }

        public List<Transaction> ConvertTransactionFromDataset(DataSet dataset)
        {
            List<Transaction> tsTransactions = new List<Transaction>();

            if (dataset != null)
            {
                foreach (DataTable tables in dataset.Tables)
                {
                    foreach (DataRow row in tables.Rows)
                    {
                        Transaction tsTransaction = new Transaction();
                        try
                        {
                            tsTransaction.TransactionId = long.Parse(row["Transaction ID"].ToString());
                            tsTransaction.TransactionType = row["Transaction Type"].ToString();
                            tsTransaction.StoreCode = row["Store Code"].ToString();
                            tsTransaction.ItemDescription = row["Item Description"].ToString();
                            tsTransaction.Category = row["Category"].ToString();
                            tsTransaction.Department = row["Department"].ToString();
                            tsTransaction.Supplier = row["Supplier"].ToString();
                            tsTransaction.SupplierCode = row["Supplier Code"].ToString();
                            tsTransaction.Cost = decimal.Parse(row["Cost"].ToString());
                            tsTransaction.Price = decimal.Parse(row["Price"].ToString());
                            tsTransaction.Quantity = decimal.Parse(row["Quantity"].ToString());
                            tsTransaction.Modifiers = decimal.Parse(row["Modifiers"].ToString());
                            tsTransaction.Subtotal = decimal.Parse(row["Subtotal"].ToString());
                            tsTransaction.Tax = decimal.Parse(row["Tax"].ToString());
                            tsTransaction.Discount = decimal.Parse(row["Discount"].ToString());
                            tsTransaction.Total = decimal.Parse(row["Total"].ToString());
                            tsTransaction.Cashier = row["Cashier"].ToString();
                            tsTransaction.Time = DateTime.Parse(row["Time"].ToString());
                            tsTransaction.Register = row["Register"].ToString();
                        }
                        catch (Exception ex)
                        {

                            //  throw;
                        }
                        tsTransactions.Add(tsTransaction);
                    }
                }

                InsertEachData(tsTransactions);
            }
            return tsTransactions;
        }

        public void InsertEachData(List<Transaction> transactions)
        {
            foreach (var trans in transactions)
            {
                _access.InsertIntoDatabase(trans);
            }
        }


        //public string InsertIntoDatabase(Transaction TransData)
        //{
        //    string _connectionString = "server=192.168.1.17;database=ShopKeep;user id=RFIDUser;password=RFIDpr0sp3r1ty;";
        //    var dataTable = new DataTable();
        //    var selectCommand = new SqlCommand
        //    {
        //        CommandText = string.Format("ImportSalesData")
        //        ,
        //        CommandTimeout = 180,
        //        CommandType = CommandType.StoredProcedure
        //    };
        //    selectCommand.Parameters.AddWithValue("@TransactionId", TransData.TransactionId);
        //    selectCommand.Parameters.AddWithValue("@TransactionType", TransData.TransactionType);
        //    selectCommand.Parameters.AddWithValue("@StoreCode", TransData.StoreCode);
        //    selectCommand.Parameters.AddWithValue("@ItemDescription", TransData.ItemDescription);
        //    selectCommand.Parameters.AddWithValue("@Category", TransData.Category);
        //    selectCommand.Parameters.AddWithValue("@Department", TransData.Department);
        //    selectCommand.Parameters.AddWithValue("@Supplier", TransData.Supplier);
        //    selectCommand.Parameters.AddWithValue("@SupplierCode", TransData.SupplierCode);
        //    selectCommand.Parameters.AddWithValue("@Cost", TransData.Cost);
        //    selectCommand.Parameters.AddWithValue("@Price", TransData.Price);
        //    selectCommand.Parameters.AddWithValue("@Quantity", TransData.Quantity);
        //    selectCommand.Parameters.AddWithValue("@Modifiers", TransData.Modifiers);
        //    selectCommand.Parameters.AddWithValue("@Subtotal", TransData.Subtotal);
        //    selectCommand.Parameters.AddWithValue("@Tax", TransData.Tax);
        //    selectCommand.Parameters.AddWithValue("@Discount", TransData.Discount);
        //    selectCommand.Parameters.AddWithValue("@Total", TransData.Total);
        //    selectCommand.Parameters.AddWithValue("@Cashier", TransData.Cashier);
        //    selectCommand.Parameters.AddWithValue("@Time", TransData.Time);
        //    selectCommand.Parameters.AddWithValue("@Register", TransData.Register);

        //    var adapter = new SqlDataAdapter(selectCommand);
        //    var connection = new SqlConnection(_connectionString);
        //    selectCommand.Connection = connection;
        //    try
        //    {
        //        connection.Open();
        //        adapter.Fill(dataTable);
        //        //  return dataTable;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //    finally
        //    {
        //        connection.Close();
        //        connection.Dispose();

        //    }
        //    return "";
        //}
    }
}
