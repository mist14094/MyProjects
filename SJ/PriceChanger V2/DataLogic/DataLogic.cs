﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using NLog;

namespace DataLogic
{
    public class DataLogic
    {
        internal Logger nlog = LogManager.GetCurrentClassLogger();

        public DataTable GetTableSearchCriteria()
        {
            nlog.Trace("DataLogic:DataLogic:GetTableSearchCriteria::Entering");
            var dt = new DataTable();
            SqlConnection connection = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ToString()))
                {
                    SqlCommand sqlCommand;
                    using (sqlCommand = new SqlCommand())
                    {
                        int totalRowsAfected;
                        sqlCommand.CommandText = "[prc_GetTableSearchCriteria]";
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        //sqlCommand.Parameters.Add(new SqlParameter("@sample", sample));
                        sqlCommand.Connection = connection;
                        connection.Open();
                        SqlDataReader sqlDataReader;
                        using (sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            dt.Load(reader: sqlDataReader);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                nlog.Error("DataLogic:DataLogic:GetTableSearchCriteria::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace("DataLogic:DataLogic:GetTableSearchCriteria::Leaving");
                if (connection != null) connection.Close();
            }
            return dt;

        }

        public DataTable UpdatePrice(string UPC, string SKU, int StoreID, decimal? OldPrice, decimal NewPrice, string OldCost, string newCost, string OldDesc, string NewDesc, int ModifiedBy, string catlog )
        {
            nlog.Trace("DataLogic:DataLogic:UpdatePrice::Entering");
            var dt = new DataTable();
            SqlConnection connection = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ToString()))
                {
                    SqlCommand sqlCommand;
                    using (sqlCommand = new SqlCommand())
                    {
                        int totalRowsAfected;
                        sqlCommand.CommandText = "[prc_UpdateProducts]";
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Add(new SqlParameter("@upc", UPC));
                        sqlCommand.Parameters.Add(new SqlParameter("@sku", SKU));
                        sqlCommand.Parameters.Add(new SqlParameter("@storeid", StoreID));
                        sqlCommand.Parameters.Add(new SqlParameter("@oldprice", OldPrice));
                        sqlCommand.Parameters.Add(new SqlParameter("@newprice", NewPrice)); 
                        sqlCommand.Parameters.Add(new SqlParameter("@oldcost", OldCost));
                        sqlCommand.Parameters.Add(new SqlParameter("@newcost", newCost));
                        sqlCommand.Parameters.Add(new SqlParameter("@oldDesc", OldDesc));
                        sqlCommand.Parameters.Add(new SqlParameter("@newDesc", NewDesc));
                        sqlCommand.Parameters.Add(new SqlParameter("@modifiedBy", ModifiedBy));
                        sqlCommand.Parameters.Add(new SqlParameter("@catag", catlog));
                        sqlCommand.Connection = connection;
                        connection.Open();
                        SqlDataReader sqlDataReader;
                        using (sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            dt.Load(reader: sqlDataReader);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                nlog.Error("DataLogic:DataLogic:UpdatePrice::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace("DataLogic:DataLogic:UpdatePrice::Leaving");
                if (connection != null) connection.Close();
            }
            return dt;

        }

        public DataTable UpdatePriceWOCategory(string UPC, string SKU, int StoreID, decimal? OldPrice, decimal NewPrice, string OldCost, string newCost, string OldDesc, string NewDesc, int ModifiedBy)
        {
            nlog.Trace("DataLogic:DataLogic:UpdatePriceWOCategory::Entering");
            var dt = new DataTable();
            SqlConnection connection = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ToString()))
                {
                    SqlCommand sqlCommand;
                    using (sqlCommand = new SqlCommand())
                    {
                        int totalRowsAfected;
                        sqlCommand.CommandText = "[prc_UpdateProductsWOCategory]";
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Add(new SqlParameter("@upc", UPC));
                        sqlCommand.Parameters.Add(new SqlParameter("@sku", SKU));
                        sqlCommand.Parameters.Add(new SqlParameter("@storeid", StoreID));
                        sqlCommand.Parameters.Add(new SqlParameter("@oldprice", OldPrice));
                        sqlCommand.Parameters.Add(new SqlParameter("@newprice", NewPrice));
                        sqlCommand.Parameters.Add(new SqlParameter("@oldcost", OldCost));
                        sqlCommand.Parameters.Add(new SqlParameter("@newcost", newCost));
                        sqlCommand.Parameters.Add(new SqlParameter("@oldDesc", OldDesc));
                        sqlCommand.Parameters.Add(new SqlParameter("@newDesc", NewDesc));
                        sqlCommand.Parameters.Add(new SqlParameter("@modifiedBy", ModifiedBy));
                        sqlCommand.Connection = connection;
                        connection.Open();
                        SqlDataReader sqlDataReader;
                        using (sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            dt.Load(reader: sqlDataReader);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                nlog.Error("DataLogic:DataLogic:UpdatePriceWOCategory::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace("DataLogic:DataLogic:UpdatePriceWOCategory::Leaving");
                if (connection != null) connection.Close();
            }
            return dt;

        }
        public DataTable GetProductSearchResult(string columnName, string searchText)
        {
            nlog.Trace("DataLogic:DataLogic:GetSearchResult::Entering");
            var dt = new DataTable();
            SqlConnection connection = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ToString()))
                {
                    SqlCommand sqlCommand;
                    using (sqlCommand = new SqlCommand())
                    {
                        int totalRowsAfected;
                        sqlCommand.CommandText = "SELECT * FROM TrackerRetail.dbo.Products WHERE ["+ columnName+"] LIKE '%"+ searchText+"%' order by upc";
                        //sqlCommand.CommandType = CommandType.StoredProcedure;
                        //sqlCommand.Parameters.Add(new SqlParameter("@sample", sample));
                        sqlCommand.Connection = connection;
                        connection.Open();
                        SqlDataReader sqlDataReader;
                        using (sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            dt.Load(reader: sqlDataReader);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                nlog.Error("DataLogic:DataLogic:GetSearchResult::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace("DataLogic:DataLogic:GetSearchResult::Leaving");
                if (connection != null) connection.Close();
            }
            return dt;
        }


        public DataTable GetAllCategory()
        {
            nlog.Trace("DataLogic:DataLogic:GetAllCategory::Entering");
            var dt = new DataTable();
            SqlConnection connection = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ToString()))
                {
                    SqlCommand sqlCommand;
                    using (sqlCommand = new SqlCommand())
                    {
                        int totalRowsAfected;
                        sqlCommand.CommandText = "SELECT sno,[Result]  FROM [Jarvis].[dbo].[Catalog] where isvalid=1 ORDER BY sno";
                        //sqlCommand.CommandType = CommandType.StoredProcedure;
                        //sqlCommand.Parameters.Add(new SqlParameter("@sample", sample));
                        sqlCommand.Connection = connection;
                        connection.Open();
                        SqlDataReader sqlDataReader;
                        using (sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            dt.Load(reader: sqlDataReader);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                nlog.Error("DataLogic:DataLogic:GetAllCategory::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace("DataLogic:DataLogic:GetAllCategory::Leaving");
                if (connection != null) connection.Close();
            }
            return dt;
        } 
        
        
        
        public DataTable GetSimpleProductDetails(string columnName, string searchText)
        {
            nlog.Trace("DataLogic:DataLogic:GetSimpleProductDetails::Entering");
            var dt = new DataTable();
            SqlConnection connection = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ToString()))
                {
                    SqlCommand sqlCommand;
                    using (sqlCommand = new SqlCommand())
                    {
                        int totalRowsAfected;
                        sqlCommand.CommandText = "SELECT DISTINCT upc,  sku,[Desc],Price,custom1,vendorname,stylecode, StyleDesc,SizeDesc,ColorCode,ColorDesc,SizeCode,SizeDesc FROM TrackerRetail.dbo.Products WHERE [" + columnName + "] LIKE '%" + searchText + "%' ORDER BY [desc] ";
                        //sqlCommand.CommandType = CommandType.StoredProcedure;
                        //sqlCommand.Parameters.Add(new SqlParameter("@sample", sample));
                        sqlCommand.Connection = connection;
                        connection.Open();
                        SqlDataReader sqlDataReader;
                        using (sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            dt.Load(reader: sqlDataReader);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                nlog.Error("DataLogic:DataLogic:GetSimpleProductDetails::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace("DataLogic:DataLogic:GetSimpleProductDetails::Leaving");
                if (connection != null) connection.Close();
            }
            return dt;
        }


        public string GetCategoryForUPCSKU(string UPC, string SKU)
        {
            nlog.Trace("DataLogic:DataLogic:GetCategoryForUPCSKU::Entering");
            var dt = new DataTable();
            
            SqlConnection connection = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ToString()))
                {
                    SqlCommand sqlCommand;
                    using (sqlCommand = new SqlCommand())
                    {
                        int totalRowsAfected;
                        sqlCommand.CommandText = "SELECT TOP 1 CatagoryID  FROM [Jarvis].[dbo].[prc_ProductCatagory] WHERE UPC='"+UPC+"'and SKU='"+SKU+"'";
                        //sqlCommand.CommandType = CommandType.StoredProcedure;
                        //sqlCommand.Parameters.Add(new SqlParameter("@sample", sample));
                        sqlCommand.Connection = connection;
                        connection.Open();
                        SqlDataReader sqlDataReader;
                        using (sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            dt.Load(reader: sqlDataReader);
                            if (dt != null)
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    return dt.Rows[0][0].ToString();
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                nlog.Error("DataLogic:DataLogic:GetCategoryForUPCSKU::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace("DataLogic:DataLogic:GetCategoryForUPCSKU::Leaving");
                if (connection != null) connection.Close();
            }
           return "0";
        }

        public DataTable GETUPCSKUDetails(string upc, string sku)
        {
            nlog.Trace("DataLogic:DataLogic:GETUPCSKUDetails::Entering");
            var dt = new DataTable();
            SqlConnection connection = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ToString()))
                {
                    SqlCommand sqlCommand;
                    using (sqlCommand = new SqlCommand())
                    {
                        int totalRowsAfected;
                        sqlCommand.CommandText = "SELECT * FROM TrackerRetail.dbo.Products WHERE upc = '" + upc + "' AND sku='" + sku + "'";
                        //sqlCommand.CommandType = CommandType.StoredProcedure;
                        //sqlCommand.Parameters.Add(new SqlParameter("@sample", sample));
                        sqlCommand.Connection = connection;
                        connection.Open();
                        SqlDataReader sqlDataReader;
                        using (sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            dt.Load(reader: sqlDataReader);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                nlog.Error("DataLogic:DataLogic:GETUPCSKUDetails::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace("DataLogic:DataLogic:GETUPCSKUDetails::Leaving");
                if (connection != null) connection.Close();
            }
            return dt;
        }
    }


}