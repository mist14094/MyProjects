using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using DataLogic;
using NLog;

namespace BusinessLogic
{
    public class BusinessLayer
    {
        private DataLogic.DataLogic dl = new DataLogic.DataLogic();
        internal Logger nlog = LogManager.GetCurrentClassLogger();

        public DataTable GetTableSearchCriteria()
        {
            nlog.Trace("BusinessLogic:BusinessLayer:GetTableSearchCriteria::Entering");

            try
            {
                return dl.GetTableSearchCriteria();
            }
            catch (Exception exception)
            {
                nlog.Error("DataLogic:DataLogic:GetTableSearchCriteria::Error", exception);
                throw;
            }
            finally
            {
                nlog.Trace("BusinessLogic:BusinessLayer:GetTableSearchCriteria::Exiting");
            }


        }


        public List<Product> GetProductSearchResult(string columnName, string searchText)
        {
            nlog.Trace("BusinessLogic:BusinessLayer:GetProductSearchGetSearchResult::Entering");

            try
            {
                var ds = dl.GetProductSearchResult(columnName, searchText);
                var pr = ToProduct(ds);
                return pr;
            }
            catch (Exception exception)
            {
                nlog.Error("DataLogic:DataLogic:GetProductSearchGetSearchResult::Error", exception);
                throw;
            }
            finally
            {
                nlog.Trace("BusinessLogic:BusinessLayer:GetProductSearchGetSearchResult::Exiting");
            }
        }

        public List<Product> GETUPCSKUDetails(string UPC, string SKU)
        {
            nlog.Trace("BusinessLogic:BusinessLayer:GETUPCSKUDetails::Entering");

            try
            {
                var ds = dl.GETUPCSKUDetails(UPC, SKU);
                var pr = ToProduct(ds);
                return pr;
            }
            catch (Exception exception)
            {
                nlog.Error("DataLogic:DataLogic:GETUPCSKUDetails::Error", exception);
                throw;
            }
            finally
            {
                nlog.Trace("BusinessLogic:BusinessLayer:GETUPCSKUDetails::Exiting");
            }
        }

        public string GetCategoryForUPCSKU(string UPC, string SKU)
        {
            nlog.Trace("BusinessLogic:BusinessLayer:GetCategoryForUPCSKU::Entering");

            try
            {
                var ds = dl.GetCategoryForUPCSKU(UPC, SKU);
                return ds;
            }
            catch (Exception exception)
            {
                nlog.Error("DataLogic:DataLogic:GetCategoryForUPCSKU::Error", exception);
                throw;
            }
            finally
            {
                nlog.Trace("BusinessLogic:BusinessLayer:GetCategoryForUPCSKU::Exiting");
            }
        }



        private List<Product> ToProduct(DataTable ds)
        {
            return ds.AsEnumerable()
                .Select(row => new Product
                {
                    BatchUOM = row.Field<int?>("BatchUOM"),
                    Clearance = row.Field<bool?>("Clearance"),
                    ColorCode = row.Field<string>("ColorCode"),
                    ColorDesc = row.Field<string>("ColorDesc"),
                    Custom1 = row.Field<string>("Custom1"),
                    Custom10 = row.Field<string>("Custom10"),
                    Custom11 = row.Field<string>("Custom11"),
                    Custom12 = row.Field<string>("Custom12"),
                    Custom13 = row.Field<string>("Custom13"),
                    Custom14 = row.Field<string>("Custom14"),
                    Custom15 = row.Field<string>("Custom15"),
                    Custom16 = row.Field<string>("Custom16"),
                    Custom17 = row.Field<string>("Custom17"),
                    Custom18 = row.Field<string>("Custom18"),
                    Custom19 = row.Field<string>("Custom19"),
                    Custom2 = row.Field<string>("Custom2"),
                    Custom20 = row.Field<string>("Custom20"),
                    Custom3 = row.Field<string>("Custom3"),
                    Custom4 = row.Field<string>("Custom4"),
                    Custom5 = row.Field<string>("Custom5"),
                    Custom6 = row.Field<string>("Custom6"),
                    Custom7 = row.Field<string>("Custom7"),
                    Custom8 = row.Field<string>("Custom8"),
                    Custom9 = row.Field<string>("Custom9"),
                    DateCreated = row.Field<DateTime>("DateCreated"),
                    DateModified = row.Field<DateTime?>("DateModified"),
                    DateRemoved = row.Field<DateTime?>("DateRemoved"),
                    DefaultZone = row.Field<int?>("DefaultZone"),
                    Department = row.Field<int?>("Department"),
                    Desc = row.Field<string>("Desc"),
                    DisplayCompliance = row.Field<bool?>("DisplayCompliance"),
                    Gender = row.Field<string>("Gender"),
                    IsActive = row.Field<bool>("IsActive"),
                    Loc1 = row.Field<string>("Loc1"),
                    Loc2 = row.Field<string>("Loc2"),
                    MfgName = row.Field<string>("MfgName"),
                    Price = row.Field<decimal?>("Price"),
                    QtyMax = row.Field<int?>("QtyMax"),
                    QtyMin = row.Field<int?>("QtyMin"),
                    QtyOnHand = row.Field<int?>("QtyOnHand"),
                    SKU = row.Field<string>("SKU"),
                    SalesPromo = row.Field<bool?>("SalesPromo"),
                    SizeCode = row.Field<string>("SizeCode"),
                    SizeDesc = row.Field<string>("SizeDesc"),
                    StoreID = row.Field<int>("StoreID"),
                    StyleCode = row.Field<string>("StyleCode"),
                    StyleDesc = row.Field<string>("StyleDesc"),
                    UPC = row.Field<string>("UPC"),
                    VendorName = row.Field<string>("VendorName") //,
                    //      rowguid = row.Field<Guid>("rowguid")
                }).ToList();
        }

        public DataTable GetAllCategory()
        {
            return dl.GetAllCategory();
        }

        public DataTable UpdatePriceWOCategory(string UPC, string SKU, int StoreID, decimal? OldPrice, decimal NewPrice,
            string OldCost, string newCost, string OldDesc, string NewDesc, int ModifiedBy)
        {
            nlog.Trace("BusinessLogic:BusinessLayer:UpdatePriceWOCategory::Entering");

            try
            {
                return dl.UpdatePriceWOCategory(UPC, SKU, StoreID, OldPrice, NewPrice, OldCost, newCost, OldDesc,
                    NewDesc, ModifiedBy);


            }
            catch (Exception exception)
            {
                nlog.Error("DataLogic:DataLogic:UpdatePriceWOCategory::Error", exception);
                throw;
            }
            finally
            {
                nlog.Trace("BusinessLogic:BusinessLayer:UpdatePriceWOCategory::Exiting");
            }
        }


        public DataTable UpdatePrice(string UPC, string SKU, int StoreID, decimal? OldPrice, decimal NewPrice,
            string OldCost, string newCost, string OldDesc, string NewDesc, int ModifiedBy, string catlog)
        {
            nlog.Trace("BusinessLogic:BusinessLayer:PriceChangeLog::Entering");

            try
            {
                return dl.UpdatePrice(UPC, SKU, StoreID, OldPrice, NewPrice, OldCost, newCost, OldDesc, NewDesc,
                    ModifiedBy, catlog);


            }
            catch (Exception exception)
            {
                nlog.Error("DataLogic:DataLogic:PriceChangeLog::Error", exception);
                throw;
            }
            finally
            {
                nlog.Trace("BusinessLogic:BusinessLayer:PriceChangeLog::Exiting");
            }
        }

        public List<ProductSimple> GetSimpleProductDetails(string columnName, string searchText)
        {
            nlog.Trace("BusinessLogic:BusinessLayer:GetSimpleProductDetails::Entering");

            try
            {
                var ds = dl.GetSimpleProductDetails(columnName, searchText);
                var pr = ds.AsEnumerable()
                    .Select(row => new ProductSimple
                    {
                        ColorCode = row.Field<string>("ColorCode"),
                        ColorDesc = row.Field<string>("ColorDesc"),
                        //DateModified = row.Field<DateTime?>("DateModified"),
                        Desc = row.Field<string>("Desc"),
                        Price = row.Field<int?>("TotalCatagCount"),
                        SKU = row.Field<string>("SKU"),
                        SizeCode = row.Field<string>("SizeCode"),
                        SizeDesc = row.Field<string>("SizeDesc"),
                        //StoreID = row.Field<int>("StoreID"),
                        StyleCode = row.Field<string>("StyleCode"),
                        StyleDesc = row.Field<string>("StyleDesc"),
                        UPC = row.Field<string>("UPC"),
                        VendorName = row.Field<string>("VendorName"),
                        Cost = row.Field<string>("Custom1")
                    }).ToList();
                return pr;
            }
            catch (Exception exception)
            {
                nlog.Error("DataLogic:DataLogic:GetSimpleProductDetails::Error", exception);
                throw;
            }
            finally
            {
                nlog.Trace("BusinessLogic:BusinessLayer:GetSimpleProductDetails::Exiting");
            }
        }


        public DataTable InsertCatagory(DataTable CatagoriesInsert, int Createdby, DateTime ModifiedDate)
        {
            nlog.Trace("BusinessLogic:BusinessLayer:PriceChangeLog::Entering");

            try
            {
                return dl.InsertCatagory(CatagoriesInsert, Createdby, ModifiedDate);


            }
            catch (Exception exception)
            {
                nlog.Error("DataLogic:DataLogic:PriceChangeLog::Error", exception);
                throw;
            }
            finally
            {
                nlog.Trace("BusinessLogic:BusinessLayer:PriceChangeLog::Exiting");
            }
        }



        public DataTable SearchCategory(string Category)
        {
            nlog.Trace("BusinessLogic:BusinessLayer:PriceChangeLog::Entering");

            try
            {
                return dl.SearchCategory(Category);
            }
            catch (Exception exception)
            {
                nlog.Error("DataLogic:DataLogic:PriceChangeLog::Error", exception);
                throw;
            }
            finally
            {
                nlog.Trace("BusinessLogic:BusinessLayer:PriceChangeLog::Exiting");
            }
        }

        public DataTable GetCatagoriesIDforUPCSKU(string upc, string sku)
        {
            nlog.Trace("BusinessLogic:BusinessLayer:PriceChangeLog::Entering");

            try
            {
                return dl.GetCatagoriesIDforUPCSKU(upc, sku);
            }
            catch (Exception exception)
            {
                nlog.Error("DataLogic:DataLogic:PriceChangeLog::Error", exception);
                throw;
            }
            finally
            {
                nlog.Trace("BusinessLogic:BusinessLayer:PriceChangeLog::Exiting");
            }
        }

        public DataTable DeleteCatagory(string upc, string sku, string catagoryID)
        {
            nlog.Trace("BusinessLogic:BusinessLayer:DeleteCatagory::Entering");

            try
            {
                return dl.DeleteCatagory(upc, sku, catagoryID);
            }
            catch (Exception exception)
            {
                nlog.Error("DataLogic:DataLogic:DeleteCatagory::Error", exception);
                throw;
            }
            finally
            {
                nlog.Trace("BusinessLogic:BusinessLayer:DeleteCatagory::Exiting");
            }
        }


        public DataTable GetCatagIDUPC(string CatagID)
        {
            nlog.Trace("BusinessLogic:BusinessLayer:GetCatagIDUPC::Entering");

            try
            {
                return dl.GetCatagIDUPC(CatagID);
            }
            catch (Exception exception)
            {
                nlog.Error("DataLogic:DataLogic:GetCatagIDUPC::Error", exception);
                throw;
            }
            finally
            {
                nlog.Trace("BusinessLogic:BusinessLayer:GetCatagIDUPC::Exiting");
            }
        }

        public DataTable GetProductsCatagory(string catagoryId)
        {
            nlog.Trace("BusinessLogic:BusinessLayer:DeleteCatagory::Entering");

            try
            {
                return dl.GetProductsCatagory(catagoryId);
            }
            catch (Exception exception)
            {
                nlog.Error("DataLogic:DataLogic:DeleteCatagory::Error", exception);
                throw;
            }
            finally
            {
                nlog.Trace("BusinessLogic:BusinessLayer:DeleteCatagory::Exiting");
            }
        }

        public DataTable CatagoryDetails(string CatagID)
        {
            nlog.Trace("BusinessLogic:BusinessLayer:CatagoryDetails::Entering");

            try
            {
                return dl.CatagoryDetails(CatagID);
            }
            catch (Exception exception)
            {
                nlog.Error("DataLogic:DataLogic:CatagoryDetails::Error", exception);
                throw;
            }
            finally
            {
                nlog.Trace("BusinessLogic:BusinessLayer:CatagoryDetails::Exiting");
            }
        }
    }
}
