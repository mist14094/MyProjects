using System;
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


        public DataTable SearchCategory(string Category)
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
                        sqlCommand.CommandText =  string.Format("SELECT sno,[Result]  FROM [Jarvis].[dbo].[Catalog] where isvalid=1 and  Result LIKE '%{0}%' ORDER BY sno",Category);
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
                        sqlCommand.CommandText = "SELECT DISTINCT p1.upc,  p1.sku,[Desc],Price,custom1,vendorname,stylecode, StyleDesc,SizeDesc,ColorCode,ColorDesc,SizeCode,SizeDesc, ISNULL(Catagories.TotalCatagCount,0) AS TotalCatagCount FROM TrackerRetail.dbo.Products P1 LEFT OUTER JOIN  (SELECT UPC,SKU, COUNT(*)AS TotalCatagCount FROM (SELECT DISTINCT UPC, sku, CatagoryID  FROM [Jarvis].[dbo].[prc_ProductCatagory])DistinctCatag GROUP BY upc, SKU)Catagories ON P1.UPC = Catagories.UPC AND P1.UPC = Catagories.UPC WHERE p1.[" + columnName + "] LIKE '%" + searchText + "%' ORDER BY [desc] ";
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
        public DataTable GetCatagoriesIDforUPCSKU(string upc, string sku)
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
                        sqlCommand.CommandText = "SELECT SNO,[Result]  FROM [Jarvis].[dbo].[Catalog] WHERE SNO IN (SELECT [CatagoryID]  FROM [Jarvis].[dbo].[prc_ProductCatagory]  WHERE UPC ='"+upc+"' AND SKU='"+sku+"')";
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
        
        
        
        public DataTable DeleteCatagory(string upc, string sku, string catagoryId)
        {
            nlog.Trace("DataLogic:DataLogic:DeleteCatagory::Entering");
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
                        sqlCommand.CommandText = "DELETE FROM [prc_ProductCatagory] WHERE upc='"+ upc+"' AND sku='"+sku+"' AND catagoryID='"+catagoryId+"'";
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
                nlog.Error("DataLogic:DataLogic:DeleteCatagory::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace("DataLogic:DataLogic:DeleteCatagory::Leaving");
                if (connection != null) connection.Close();
            }
            return dt;
        }

        public DataTable GetProductsCatagory(string catagoryId)
        {
            nlog.Trace("DataLogic:DataLogic:GetProductsCatagory::Entering");
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
                        sqlCommand.CommandText = "SELECT DISTINCT [CatagoryID], ProductCatg.upc, ProductCatg.sku,[desc] FROM [Jarvis].[dbo].[prc_ProductCatagory] ProductCatg LEFT OUTER JOIN (SELECT DISTINCT UPC, SKU, [DESC] from TrackerRetail.dbo.Products ) ProductsTrack ON ProductCatg.SKU = ProductsTrack.SKU AND ProductCatg.UPC = ProductsTrack.UPC  WHERE CatagoryID='"+catagoryId+"'";
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
                nlog.Error("DataLogic:DataLogic:GetProductsCatagory::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace("DataLogic:DataLogic:GetProductsCatagory::Leaving");
                if (connection != null) connection.Close();
            }
            return dt;
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


        public DataTable GetCatagIDUPC(string CatagID)
        {
            nlog.Trace("DataLogic:DataLogic:GetCatagIDUPC::Entering");
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
                        sqlCommand.CommandText = "SELECT DISTINCT [CatagoryID], ProductCatg.upc, ProductCatg.sku,[desc] FROM [Jarvis].[dbo].[prc_ProductCatagory] ProductCatg LEFT OUTER JOIN (SELECT DISTINCT UPC, SKU, [DESC] from TrackerRetail.dbo.Products ) ProductsTrack ON ProductCatg.SKU = ProductsTrack.SKU AND ProductCatg.UPC = ProductsTrack.UPC  WHERE CatagoryID='"+CatagID+"'";
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
                nlog.Error("DataLogic:DataLogic:GetCatagIDUPC::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace("DataLogic:DataLogic:GetCatagIDUPC::Leaving");
                if (connection != null) connection.Close();
            }
            return dt;
        }

        public DataTable CatagoryDetails(string CatagID)
        {
            nlog.Trace("DataLogic:DataLogic:CatagoryDetails::Entering");
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
                        sqlCommand.CommandText = "SELECT [Result]  FROM [Jarvis].[dbo].[Catalog] WHERE sno ='"+CatagID+"'";
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
                nlog.Error("DataLogic:DataLogic:CatagoryDetails::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace("DataLogic:DataLogic:CatagoryDetails::Leaving");
                if (connection != null) connection.Close();
            }
            return dt;
        }


        public DataTable InsertCatagory(DataTable CatagoriesInsert, int Createdby, DateTime ModifiedDate)
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
                        sqlCommand.CommandText = "InsertCatagory";
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Add(new SqlParameter("@CatagoriesInsert", CatagoriesInsert));
                        sqlCommand.Parameters.Add(new SqlParameter("@Createdby", Createdby));
                        sqlCommand.Parameters.Add(new SqlParameter("@ModifiedDate", ModifiedDate));
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
