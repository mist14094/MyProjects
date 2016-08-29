using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using NLog;
namespace SJDealStore
{
    public class ShopKeep
    {
        private Constants _constants = new Constants();
        internal static Logger Nlog = LogManager.GetCurrentClassLogger();

        public string ShopKeepImport(string StockItemRecordID, string Description, string Discountable, string UPCCode,
            string Price, string Cost, string Taxable, string PriceType, string RegisterDataStatus, string QuantityonHand, string InventoryMethod,
            string AssignedCost, string OrderTrigger, string RecommendedOrder, string Department, string Category, string Supplier, string SupplierCode,
            string Unit, string TaxGroup)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.ShopKeepImport),
                CommandTimeout = 180,
                CommandType = CommandType.StoredProcedure
            };

            selectCommand.Parameters.AddWithValue("@StockItemRecordID ", StockItemRecordID);
            selectCommand.Parameters.AddWithValue("@Description ", Description);
            selectCommand.Parameters.AddWithValue("@Discountable ", Discountable);
            selectCommand.Parameters.AddWithValue("@UPCCode ", UPCCode);
            selectCommand.Parameters.AddWithValue("@Price ", Price);
            selectCommand.Parameters.AddWithValue("@Cost ", Cost);
            selectCommand.Parameters.AddWithValue("@Taxable ", Taxable);
            selectCommand.Parameters.AddWithValue("@PriceType ", PriceType);
            selectCommand.Parameters.AddWithValue("@RegisterDataStatus ", RegisterDataStatus);
            selectCommand.Parameters.AddWithValue("@QuantityonHand ", QuantityonHand);
            selectCommand.Parameters.AddWithValue("@InventoryMethod ", InventoryMethod);
            selectCommand.Parameters.AddWithValue("@AssignedCost ", AssignedCost);
            selectCommand.Parameters.AddWithValue("@OrderTrigger ", OrderTrigger);
            selectCommand.Parameters.AddWithValue("@RecommendedOrder ", RecommendedOrder);
            selectCommand.Parameters.AddWithValue("@Department ", Department);
            selectCommand.Parameters.AddWithValue("@Category ", Category);
            selectCommand.Parameters.AddWithValue("@Supplier ", Supplier);
            selectCommand.Parameters.AddWithValue("@SupplierCode ", SupplierCode);
            selectCommand.Parameters.AddWithValue("@Unit ", Unit);
            selectCommand.Parameters.AddWithValue("@TaxGroup", TaxGroup);
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                var ID = selectCommand.ExecuteScalar();
                return ID.ToString();

            }
            catch (Exception ex)
            {
                Nlog.Trace(
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message:
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }

        public DataTable GetShopKeepItems(string StoreID)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.GetShopKeepItems)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.StoredProcedure
            };

            selectCommand.Parameters.AddWithValue("@StoreID", StoreID);
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Nlog.Trace(
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message:
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }

        public DataTable DeleteRecords()
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.ShopKeepDataDelete)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.Text
            };


            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Nlog.Trace(
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message:
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }

        public string GetShopKeepItemsCSV(string StoreID)
        {
            DataTable table = new DataTable();
            table = GetShopKeepItems(StoreID);
            var result = new StringBuilder();
            for (int i = 0; i < table.Columns.Count; i++)
            {
                result.Append(table.Columns[i].ColumnName);
                result.Append(i == table.Columns.Count - 1 ? "\n" : ",");
            }

            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    result.Append(row[i].ToString());
                    result.Append(i == table.Columns.Count - 1 ? "\n" : ",");
                }
            }

            return result.ToString();
        }



    }
}