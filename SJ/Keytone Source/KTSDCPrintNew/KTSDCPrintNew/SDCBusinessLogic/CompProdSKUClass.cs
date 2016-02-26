using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using KTone.DAL.SmartDCDataAccess;
using System.Configuration;
using System.Data.SqlTypes;
using KTone.DAL.KTDBBaseLib;
using KTone.DAL.KTDAGlobaApplLib;
using KTone.Core.KTIRFID;


namespace KTone.Core.SDCBusinessLogic
{
   public class CompProdSKUClass
    {
       NLog.Logger _log = NLog.LogManager.GetCurrentClassLogger();
       public List<KTCompanyDetails> GetAllCompanies(int dataOwnerID)
       {
           List<KTCompanyDetails> compDetails = new List<KTCompanyDetails>();
           try
           {
               _log.Trace("CompProdSKUClass:GetAllCompanies:Entering");
               //IKTSDCCache sdcCache = SDCWSHelper.GetSDCCache();
               //if (sdcCache == null)
               //{
               _log.Trace("CompProdSKUClass:GetAllCompanies:sdcCache null");
               compDetails = FillCompanies(dataOwnerID);
               //}

               //else
              // {
                //   _log.Trace("CompProdSKUClass:GetAllCompanies:sdcCache not null");
                 //  return compDetails = sdcCache.GetAllCompanyDetails(dataOwnerID);
              // }

           }
           catch (Exception ex)
           {
               _log.Error("Error:CompProdSKUClass:GetAllCompanies:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception (ex.Message);
           }
           finally
           {
               SDCWSHelper.ReleaseRemoteObject();
               _log.Trace("CompProdSKUClass:GetAllCompanies:Leaving");
           }
           return compDetails;
       }
       public List<KTCompanyDetails> FillCompanies(int dataOwnerID)
       {
           List<KTCompanyDetails> compDetails = new List<KTCompanyDetails>();
           try
           {
               _log.Trace("CompProdSKUClass:FillCompanies:Entering");
               CompanyMaster clsCompany = new CompanyMaster();
               clsCompany.DataOwnerID = dataOwnerID;
               DataTable dtAllCompanymaster = clsCompany.SelectAll();

               CompanyCustom clsCompanyCustom = new CompanyCustom();
               clsCompanyCustom.DataOwnerID = dataOwnerID;
               clsCompanyCustom.CategoryID = 1;

               CustomFieldCatagory CFcategory = new CustomFieldCatagory();
               DataTable dtCat = CFcategory.SelectAll();
               DataRow[] dr = dtCat.Select("CategoryID ='" + Convert.ToString(clsCompanyCustom.CategoryID) + "_CompanyCustom'");
               for (int i = 0; i < dr.Length; i++)
               {
                   clsCompanyCustom.CustTableName = dr[i]["TableName"].ToString(); ;
               }

               DataTable dtAllCustomColumns = clsCompanyCustom.GetCustomColumnSchema();
               List<string> allCompanyList = new List<string>();

               foreach (DataRow drField in dtAllCustomColumns.Rows)
                   allCompanyList.Add(drField["name"].ToString());

               if (dtAllCompanymaster != null && dtAllCompanymaster.Rows.Count > 0)
               {
                   foreach (DataRow dCompanyRow in dtAllCompanymaster.Rows)
                   {
                       Dictionary<string, string> customColumnDetails = new Dictionary<string, string>();
                       List<string> userFields = new List<string>();

                       foreach (string colName in allCompanyList)
                       {
                           if (dtAllCompanymaster.Columns.Contains(colName))
                               userFields.Add(colName);
                       }
                       if (userFields != null && userFields.Count > 0)
                       {
                           foreach (string field in userFields)
                           {
                               if (!customColumnDetails.ContainsKey(field))
                                   customColumnDetails[field] = dCompanyRow[field].ToString();
                           }
                       }
                       string companyprefix = ""; bool isepc = false;
                       DateTime createddate = DateTime.MinValue; DateTime updateddate = DateTime.MinValue;
                       int createdby = 0, updatedby = 0, DataOwnerId = 0;
                       string CompanyName = "";
                       if (dCompanyRow["CompanyName"] != null && dCompanyRow["CompanyName"].ToString() != string.Empty)
                           CompanyName = dCompanyRow["CompanyName"].ToString();
                       if (dCompanyRow["CompanyPrefix"] != null && dCompanyRow["CompanyPrefix"].ToString() != string.Empty)
                           companyprefix = dCompanyRow["CompanyPrefix"].ToString();
                       if (dCompanyRow["IsEPC"] != null && dCompanyRow["IsEPC"].ToString() != string.Empty)
                           isepc = Convert.ToBoolean(dCompanyRow["IsEPC"].ToString());
                       if (dCompanyRow["CreatedDate"] != null && dCompanyRow["CreatedDate"].ToString() != string.Empty)
                           createddate = Convert.ToDateTime(dCompanyRow["CreatedDate"].ToString());
                       if (dCompanyRow["UpdatedDate"] != null && dCompanyRow["UpdatedDate"].ToString() != string.Empty)
                           updateddate = Convert.ToDateTime(dCompanyRow["UpdatedDate"].ToString());
                       if (dCompanyRow["CreatedBy"] != null && dCompanyRow["CreatedBy"].ToString() != string.Empty)
                           createdby = int.Parse(dCompanyRow["CreatedBy"].ToString());
                       if (dCompanyRow["UpdatedBy"] != null && dCompanyRow["UpdatedBy"].ToString() != string.Empty)
                           updatedby = int.Parse(dCompanyRow["UpdatedBy"].ToString());
                       if (dCompanyRow["DataOwnerId"] != null && dCompanyRow["DataOwnerId"].ToString() != string.Empty)
                           DataOwnerId = int.Parse(dCompanyRow["DataOwnerId"].ToString());


                       KTCompanyDetails companydetails = new KTCompanyDetails(isepc, int.Parse(dCompanyRow["CompanyID"].ToString()),
                           CompanyName, companyprefix, customColumnDetails,
                           DataOwnerId, createdby, updatedby, createddate, updateddate);

                       compDetails.Add(companydetails);
                   }
               }
           }
           catch (Exception ex)
           {
               _log.Error("Error:CompProdSKUClass:FillCompanies:: " + ex.Message + Environment.NewLine + ex.StackTrace);
               throw new Exception(ex.Message);
           }
           finally
           {
               SDCWSHelper.ReleaseRemoteObject();
               _log.Trace("CompProdSKUClass:FillCompanies:Leaving");
           }
           return compDetails;
       }

       public List<KTProductDetails> GetAllProducts(int dataOwnerID)
       {
          
           List<KTProductDetails> prodDetails = new List<KTProductDetails>();
           try
           {
               _log.Trace("CompProdSKUClass:GetAllProducts:Entering");
               //IKTSDCCache sdcCache = SDCWSHelper.GetSDCCache();
               //if (sdcCache == null)
               //{
                _log.Trace("CompProdSKUClass:GetAllProducts:sdcCache null");
                prodDetails = FillProducts(dataOwnerID, 0);
               //}
               //else
               //{
               //    _log.Trace("CompProdSKUClass:GetAllProducts:sdcCache not null");
               //    return prodDetails = sdcCache.GetAllProductDetails(dataOwnerID);
               //}
           }
           catch (Exception ex)
           {
               _log.Error("Error:CompProdSKUClass:GetAllProducts:: " + ex.Message + Environment.NewLine + ex.StackTrace);
               throw new Exception(ex.Message);
           }
           finally
           {
               SDCWSHelper.ReleaseRemoteObject();
               _log.Trace("CompProdSKUClass:GetAllProducts:Leaving");
           }
           return prodDetails;
       }

       public List<KTProductDetails> FillProducts( int dataOwnerID, int CompanyID)
       {
           List<KTProductDetails> prodDetails = new List<KTProductDetails>();

           try
           {
               _log.Trace("CompProdSKUClass:FillProducts:Entering");
               ProductMaster clsproduct = new ProductMaster();
               clsproduct.DataOwnerID = dataOwnerID;
               clsproduct.CompanyID = CompanyID;
               DataTable dtAllProductMaster = clsproduct.SelectAll();

               CompanyCustom clsProductCustom = new CompanyCustom();
               clsProductCustom.DataOwnerID = dataOwnerID;
               clsProductCustom.CategoryID = 2;

               CustomFieldCatagory CFcategory = new CustomFieldCatagory();
               DataTable dtCat = CFcategory.SelectAll();
               DataRow[] dr = dtCat.Select("CategoryID ='" + Convert.ToString(clsProductCustom.CategoryID) + "_ProductCustom'");
               for (int i = 0; i < dr.Length; i++)
               {
                   clsProductCustom.CustTableName = dr[i]["TableName"].ToString(); ;
               }
               DataTable dtAllCustomColumns = clsProductCustom.GetCustomColumnSchema();
               List<string> allProductList = new List<string>();

               foreach (DataRow drField in dtAllCustomColumns.Rows)
                   allProductList.Add(drField["name"].ToString());
               if (dtAllProductMaster != null && dtAllProductMaster.Rows.Count > 0)
               {

                   foreach (DataRow dProductRow in dtAllProductMaster.Rows)
                   {
                       Dictionary<string, string> customColumnDetails = new Dictionary<string, string>();
                       List<string> userFields = new List<string>();

                       foreach (string colName in allProductList)
                       {
                           if (dtAllProductMaster.Columns.Contains(colName))
                               userFields.Add(colName);
                       }
                       if (userFields != null && userFields.Count > 0)
                       {
                           foreach (string field in userFields)
                           {

                               if (!customColumnDetails.ContainsKey(field))
                                   customColumnDetails[field] = dProductRow[field].ToString();

                           }
                       }

                       string productprefix = "", ProductName = "";
                       DateTime createddate = DateTime.MinValue; DateTime updateddate = DateTime.MinValue;
                       int createdby = 0, updatedby = 0, CompanyId = 0, DataOwnerId = 0;
                       if (dProductRow["ProductPrefix"] != null && dProductRow["ProductPrefix"].ToString() != string.Empty)
                           productprefix = dProductRow["ProductPrefix"].ToString();
                       if (dProductRow["CreatedDate"] != null && dProductRow["CreatedDate"].ToString() != string.Empty)
                           createddate = Convert.ToDateTime(dProductRow["CreatedDate"].ToString());
                       if (dProductRow["UpdatedDate"] != null && dProductRow["UpdatedDate"].ToString() != string.Empty)
                           updateddate = Convert.ToDateTime(dProductRow["UpdatedDate"].ToString());
                       if (dProductRow["CreatedBy"] != null && dProductRow["CreatedBy"].ToString() != string.Empty)
                           createdby = int.Parse(dProductRow["CreatedBy"].ToString());
                       if (dProductRow["UpdatedBy"] != null && dProductRow["UpdatedBy"].ToString() != string.Empty)
                           updatedby = int.Parse(dProductRow["UpdatedBy"].ToString());
                       if (dProductRow["CompanyId"] != null && dProductRow["CompanyId"].ToString() != string.Empty)
                           CompanyId = int.Parse(dProductRow["CompanyId"].ToString());
                       if (dProductRow["ProductName"] != null && dProductRow["ProductName"].ToString() != string.Empty)
                           ProductName = dProductRow["ProductName"].ToString();
                       if (dProductRow["DataOwnerId"] != null && dProductRow["DataOwnerId"].ToString() != string.Empty)
                           DataOwnerId = int.Parse(dProductRow["DataOwnerId"].ToString());

                       KTProductDetails productdetails = new KTProductDetails(CompanyId, long.Parse(dProductRow["ProductID"].ToString())
                           , ProductName, productprefix, customColumnDetails,
                           DataOwnerId, createdby, updatedby, createddate, updateddate);

                       prodDetails.Add(productdetails);
                   }
               }
           }
           catch (Exception ex)
           {
               _log.Error("Error:CompProdSKUClass:FillProducts:: " + ex.Message + Environment.NewLine + ex.StackTrace);
               throw new Exception(ex.Message);
           }
           finally
           {
               _log.Trace("CompProdSKUClass:FillProducts:Leaving");
           }

           return prodDetails;

       }

       public List<KTProductDetails> GetAllProductsByCompanyID(int CompanyId, int dataOwnerID)
       {
           List<KTProductDetails> prodDetails = new List<KTProductDetails>();
           try
           {
               _log.Trace("CompProdSKUClass:GetAllProductsByCompanyID:Entering");
                //IKTSDCCache sdcCache = SDCWSHelper.GetSDCCache();
                //if (sdcCache == null)
                //{
                    _log.Trace("CompProdSKUClass:GetAllProductsByCompanyID:sdcCache null");
                    prodDetails = FillProducts(dataOwnerID, CompanyId);
                //}
                //else
                //{
                //    _log.Trace("CompProdSKUClass:GetAllProductsByCompanyID:sdcCache not null");
                //    return prodDetails = sdcCache.GetProductForCompanyID(dataOwnerID, CompanyId); 
                //}
           }
           catch (Exception ex)
           {
               _log.Error("Error:CompProdSKUClass:GetAllProductsByCompanyID:: " + ex.Message + Environment.NewLine + ex.StackTrace);
               throw new Exception (ex.Message);
           }
           finally
           {
               SDCWSHelper.ReleaseRemoteObject();
               _log.Trace("CompProdSKUClass:GetAllProductsByCompanyID:Leaving");
           }
           return prodDetails;
       }

       public List<KTSKUDetails> FillSKU(int dataOwnerID, long ProductID)
       {
           List<KTSKUDetails> skuDetails = new List<KTSKUDetails>();
           try
           {
               _log.Trace("CompProdSKUClass:FillSKU:Entering");

               SKUMaster clsSKU = new SKUMaster();
               clsSKU.DataOwnerID = dataOwnerID;
               clsSKU.ProductID = Convert.ToInt32(ProductID);

               DataTable dtAllSKUMaster = clsSKU.SelectAll();
               CompanyCustom clsSKUCustom = new CompanyCustom();
               clsSKUCustom.DataOwnerID = dataOwnerID;
               clsSKUCustom.CategoryID = 4;

               CustomFieldCatagory CFcategory = new CustomFieldCatagory();
               DataTable dtCat = CFcategory.SelectAll();
               DataRow[] dr = dtCat.Select("CategoryID ='" + Convert.ToString(clsSKUCustom.CategoryID) + "_SKUCustom'");
               for (int i = 0; i < dr.Length; i++)
               {
                   clsSKUCustom.CustTableName = dr[i]["TableName"].ToString(); ;
               }
               DataTable dtAllCustomColumns = clsSKUCustom.GetCustomColumnSchema();
               List<string> allSKUList = new List<string>();
               foreach (DataRow drField in dtAllCustomColumns.Rows)
                   allSKUList.Add(drField["name"].ToString());

               if (dtAllSKUMaster != null && dtAllSKUMaster.Rows.Count > 0)
               {
                   foreach (DataRow dSkuRow in dtAllSKUMaster.Rows)
                   {
                       Dictionary<string, string> customColumnDetails = new Dictionary<string, string>();
                       List<string> userFields = new List<string>();

                       foreach (string colName in allSKUList)
                       {
                           if (dtAllSKUMaster.Columns.Contains(colName))
                               userFields.Add(colName);
                       }
                       if (userFields != null && userFields.Count > 0)
                       {
                           foreach (string field in userFields)
                           {
                               //DataRow[] customColumn = dtAllCustomColumns.Select("SKU_ID = " + dSkuRow["SKU_ID"].ToString() + " AND Name = '" + field + "'");
                               //if (customColumn.Length > 0)
                               //{
                               if (!customColumnDetails.ContainsKey(field))
                                   customColumnDetails[field] = dSkuRow[field].ToString();
                               //}
                           }
                       }

                       string skudescription = "", ProductSKU = "", Name="";
                       DateTime createddate = DateTime.MinValue; DateTime updateddate = DateTime.MinValue;
                       int createdby = 0, updatedby = 0, DataOwnerID = 0, PackageID=0;
                       long ProdcutID = 0;
                       if (dSkuRow["SKUDescription"] != null && dSkuRow["SKUDescription"].ToString() != string.Empty)
                           skudescription = dSkuRow["SKUDescription"].ToString();
                       if (dSkuRow["CreatedDate"] != null && dSkuRow["CreatedDate"].ToString() != string.Empty)
                           createddate = Convert.ToDateTime(dSkuRow["CreatedDate"].ToString());
                       if (dSkuRow["UpdatedDate"] != null && dSkuRow["UpdatedDate"].ToString() != string.Empty)
                           updateddate = Convert.ToDateTime(dSkuRow["UpdatedDate"].ToString());
                       if (dSkuRow["CreatedBy"] != null && dSkuRow["CreatedBy"].ToString() != string.Empty)
                           createdby = int.Parse(dSkuRow["CreatedBy"].ToString());
                       if (dSkuRow["UpdatedBy"] != null && dSkuRow["UpdatedBy"].ToString() != string.Empty)
                           updatedby = int.Parse(dSkuRow["UpdatedBy"].ToString());
                       if (dSkuRow["ProductSKU"] != null && dSkuRow["ProductSKU"].ToString() != string.Empty)
                           ProductSKU = dSkuRow["ProductSKU"].ToString();
                       if (dSkuRow["DataOwnerID"] != null && dSkuRow["DataOwnerID"].ToString() != string.Empty)
                           DataOwnerID = int.Parse(dSkuRow["DataOwnerID"].ToString());
                       if (dSkuRow["ProdcutID"] != null && dSkuRow["ProdcutID"].ToString() != string.Empty)
                           ProdcutID = long.Parse(dSkuRow["ProdcutID"].ToString());
                       if (dSkuRow["PackageID"] != null && dSkuRow["PackageID"].ToString() != string.Empty)
                           PackageID = int.Parse(dSkuRow["PackageID"].ToString());
                       if (dSkuRow["Name"] != null && dSkuRow["Name"].ToString() != string.Empty)
                           Name = dSkuRow["Name"].ToString();

                       KTSKUDetails skudetails = new KTSKUDetails(ProdcutID,
                           long.Parse(dSkuRow["SKU_ID"].ToString()), ProductSKU,
                           skudescription, customColumnDetails, DataOwnerID,
                           createdby, updatedby, createddate, updateddate, PackageID, Name);

                       skuDetails.Add(skudetails);
                   }
               }
           }
           catch (Exception ex)
           {
               _log.Error("Error:CompProdSKUClass:FillSKU:: " + ex.Message + Environment.NewLine + ex.StackTrace);
               throw new Exception(ex.Message);
           }
           finally
           {
               _log.Trace("CompProdSKUClass:FillSKU:Leaving");
           }
           return skuDetails;
       }

       public List<KTSKUDetails> FillSKUByCompID(int dataOwnerID, int CompanyId)
       {
           List<KTSKUDetails> skuDetails = new List<KTSKUDetails>();
           try
           {
               _log.Trace("CompProdSKUClass:FillSKU:Entering");

               SKUMaster clsSKU = new SKUMaster();
               clsSKU.DataOwnerID = dataOwnerID;
               clsSKU.CompanyID = CompanyId;

               DataTable dtAllSKUMaster = clsSKU.SelectAllByCompID();
               CompanyCustom clsSKUCustom = new CompanyCustom();
               clsSKUCustom.DataOwnerID = dataOwnerID;
               clsSKUCustom.CategoryID = 4;

               CustomFieldCatagory CFcategory = new CustomFieldCatagory();
               DataTable dtCat = CFcategory.SelectAll();
               DataRow[] dr = dtCat.Select("CategoryID ='" + Convert.ToString(clsSKUCustom.CategoryID) + "_SKUCustom'");
               for (int i = 0; i < dr.Length; i++)
               {
                   clsSKUCustom.CustTableName = dr[i]["TableName"].ToString(); ;
               }
               DataTable dtAllCustomColumns = clsSKUCustom.GetCustomColumnSchema();
               List<string> allSKUList = new List<string>();
               foreach (DataRow drField in dtAllCustomColumns.Rows)
                   allSKUList.Add(drField["name"].ToString());

               if (dtAllSKUMaster != null && dtAllSKUMaster.Rows.Count > 0)
               {
                   foreach (DataRow dSkuRow in dtAllSKUMaster.Rows)
                   {
                       Dictionary<string, string> customColumnDetails = new Dictionary<string, string>();
                       List<string> userFields = new List<string>();

                       foreach (string colName in allSKUList)
                       {
                           if (dtAllSKUMaster.Columns.Contains(colName))
                               userFields.Add(colName);
                       }
                       if (userFields != null && userFields.Count > 0)
                       {
                           foreach (string field in userFields)
                           {
                               //DataRow[] customColumn = dtAllCustomColumns.Select("SKU_ID = " + dSkuRow["SKU_ID"].ToString() + " AND Name = '" + field + "'");
                               //if (customColumn.Length > 0)
                               //{
                               if (!customColumnDetails.ContainsKey(field))
                                   customColumnDetails[field] = dSkuRow[field].ToString();
                               //}
                           }
                       }

                       string skudescription = "", ProductSKU = "", Name="";
                       DateTime createddate = DateTime.MinValue; DateTime updateddate = DateTime.MinValue;
                       int createdby = 0, updatedby = 0, DataOwnerID = 0, PackageID=0;
                       long ProdcutID = 0;
                       if (dSkuRow["SKUDescription"] != null && dSkuRow["SKUDescription"].ToString() != string.Empty)
                           skudescription = dSkuRow["SKUDescription"].ToString();
                       if (dSkuRow["CreatedDate"] != null && dSkuRow["CreatedDate"].ToString() != string.Empty)
                           createddate = Convert.ToDateTime(dSkuRow["CreatedDate"].ToString());
                       if (dSkuRow["UpdatedDate"] != null && dSkuRow["UpdatedDate"].ToString() != string.Empty)
                           updateddate = Convert.ToDateTime(dSkuRow["UpdatedDate"].ToString());
                       if (dSkuRow["CreatedBy"] != null && dSkuRow["CreatedBy"].ToString() != string.Empty)
                           createdby = int.Parse(dSkuRow["CreatedBy"].ToString());
                       if (dSkuRow["UpdatedBy"] != null && dSkuRow["UpdatedBy"].ToString() != string.Empty)
                           updatedby = int.Parse(dSkuRow["UpdatedBy"].ToString());
                       if (dSkuRow["ProductSKU"] != null && dSkuRow["ProductSKU"].ToString() != string.Empty)
                           ProductSKU = dSkuRow["ProductSKU"].ToString();
                       if (dSkuRow["DataOwnerID"] != null && dSkuRow["DataOwnerID"].ToString() != string.Empty)
                           DataOwnerID = int.Parse(dSkuRow["DataOwnerID"].ToString());
                       if (dSkuRow["ProdcutID"] != null && dSkuRow["ProdcutID"].ToString() != string.Empty)
                           ProdcutID = long.Parse(dSkuRow["ProdcutID"].ToString());
                       if (dSkuRow["PackageID"] != null && dSkuRow["PackageID"].ToString() != string.Empty)
                           PackageID = int.Parse(dSkuRow["PackageID"].ToString());
                       if (dSkuRow["Name"] != null && dSkuRow["Name"].ToString() != string.Empty)
                           Name = dSkuRow["Name"].ToString();

                       KTSKUDetails skudetails = new KTSKUDetails(ProdcutID,
                           long.Parse(dSkuRow["SKU_ID"].ToString()), ProductSKU,
                           skudescription, customColumnDetails, DataOwnerID,
                           createdby, updatedby, createddate, updateddate, PackageID,Name);

                       skuDetails.Add(skudetails);
                   }
               }
           }
           catch (Exception ex)
           {
               _log.Error("Error:CompProdSKUClass:FillSKU:: " + ex.Message + Environment.NewLine + ex.StackTrace);
               throw new Exception(ex.Message);
           }
           finally
           {
               _log.Trace("CompProdSKUClass:FillSKU:Leaving");
           }
           return skuDetails;
       }


       public List<KTSKUDetails> GetAllSKU(int dataOwnerID)
       {
           List<KTSKUDetails> skuDetails = new List<KTSKUDetails>();
           try
           {
               _log.Trace("CompProdSKUClass:GetAllSKU:Entering");
                //IKTSDCCache sdcCache = SDCWSHelper.GetSDCCache();
                //if (sdcCache == null)
                //{
                    _log.Trace("CompProdSKUClass:GetAllSKU:sdcCache null");
                   skuDetails =FillSKU(dataOwnerID, 0);
                //}
                //else
                //{
                //    _log.Trace("CompProdSKUClass:GetAllSKU:sdcCache not null");
                //    return skuDetails = sdcCache.GetAllSKUDetails(dataOwnerID);
                //}
           }
           catch (Exception ex)
           {
               _log.Error("Error:CompProdSKUClass:GetAllSKU:: " + ex.Message + Environment.NewLine + ex.StackTrace);
               throw new Exception(ex.Message);
           }
           finally
           {
               SDCWSHelper.ReleaseRemoteObject();
               _log.Trace("CompProdSKUClass:GetAllSKU:Leaving");
           }
           return skuDetails;
       }

       public List<KTSKUDetails> GetAllSKUByProductID(long ProductID, int dataOwnerID)
       {
           List<KTSKUDetails> skuDetails = new List<KTSKUDetails>();
           try
           {
               _log.Trace("CompProdSKUClass:GetAllSKUByProductID:Entering");
                //IKTSDCCache sdcCache = SDCWSHelper.GetSDCCache();
                //if (sdcCache == null)
                //{
                    _log.Trace("CompProdSKUClass:GetAllSKUByProductID:sdcCache null");
                    skuDetails = FillSKU(dataOwnerID, ProductID);
                //}
                //else
                //{
                //    _log.Trace("CompProdSKUClass:GetAllSKUByProductID:sdcCache not null");
                //    return skuDetails = sdcCache.GetSKUForProductID(dataOwnerID, ProductID);
                //}
           }

           catch (Exception ex)
           {
               _log.Error("Error:CompProdSKUClass:GetAllSKUByProductID:: " + ex.Message + Environment.NewLine + ex.StackTrace);
               throw new Exception(ex.Message);
           }
           finally
           {
               SDCWSHelper.ReleaseRemoteObject();
               _log.Trace("CompProdSKUClass:GetAllSKUByProductID:Leaving");
           }
           return skuDetails;
       }

       public List<KTSKUDetails> GetAllSKUByCompanyID(int CompanyId, int dataOwnerID)
       {
          
           List<KTSKUDetails> skuDetails = new List<KTSKUDetails>();
           try
           {
               _log.Trace("CompProdSKUClass:GetAllSKUByCompanyID:Entering");
               //IKTSDCCache sdcCache = SDCWSHelper.GetSDCCache();
               //if (sdcCache == null)
               //{
                   _log.Trace("CompProdSKUClass:GetAllSKUByCompanyID:sdcCache  null");
                   skuDetails = FillSKUByCompID(dataOwnerID, CompanyId);
               //}
               //else
               //{
               //    _log.Trace("CompProdSKUClass:GetAllSKUByCompanyID:sdcCache not  null");
               //    return skuDetails = sdcCache.GetSKUForCompanyID(dataOwnerID, CompanyId);
               //}
           }
           catch (Exception ex)
           {
               _log.Error("Error:CompProdSKUClass:GetAllSKUByCompanyID:: " + ex.Message + Environment.NewLine + ex.StackTrace);
               throw new Exception(ex.Message);

           }
           finally
           {
               SDCWSHelper.ReleaseRemoteObject();
               _log.Trace("CompProdSKUClass:GetAllSKUByCompanyID:Leaving");
           }
           return skuDetails;
       }

       public List<CustomFeildInfo> GetCustomColumnInfo(int categoryID, int dataOwnerID)
       {
          
           List<CustomFeildInfo> customInfo = new List<CustomFeildInfo>();

           CompanyCustom objcustomColumn = new CompanyCustom();
           UserdefiendValues objuserdefinedValues = new UserdefiendValues();

           try
           {
               _log.Trace("CompProdSKUClass:GetCustomColumnInfo:Entering");
             
               objcustomColumn.CategoryID = categoryID;
               objcustomColumn.DataOwnerID = dataOwnerID;
               objcustomColumn.CustTableName = string.Empty;
               DataTable dtCustcolumn = objcustomColumn.GetCustomColumnSchema();

               objuserdefinedValues.CategoryID = categoryID;
               objuserdefinedValues.DataOwnerID = dataOwnerID;
               DataTable dtuserValues = objuserdefinedValues.SelectAll();

               if (dtCustcolumn != null && dtCustcolumn.Rows.Count > 0)
               {
                   foreach (DataRow dtRow in dtCustcolumn.Rows)
                   {
                       CustomFeildInfo objCustomInfo = new CustomFeildInfo();
                       objCustomInfo.CustColName = dtRow["Name"].ToString();
                       objCustomInfo.AliasName = dtRow["AliasName"].ToString();
                       objCustomInfo.DataOwnerID = dataOwnerID;
                       objCustomInfo.GroupName = dtRow["GroupName"].ToString();
                       objCustomInfo.MaxLength = Convert.ToInt64(dtRow["Length"].ToString());
                       objCustomInfo.DataType = dtRow["Datatype"].ToString();
                       objCustomInfo.RegExp = dtRow["RegExpression"].ToString();
                       objCustomInfo.IsMultivalued = Convert.ToBoolean(dtRow["IsMultiValued"].ToString());
                       objCustomInfo.IsMandatory = Convert.ToBoolean(dtRow["IsMandatory"].ToString());

                       if (Convert.ToBoolean(dtRow["IsMultiValued"].ToString()))
                       {

                           DataRow[] dtRowuserval = dtuserValues.Select("CustomColName='" + dtRow["Name"].ToString().Trim().ToUpper() + "'");

                           if (dtRowuserval != null && dtRowuserval.Length > 0)
                           {
                               foreach (DataRow dtRowval in dtRowuserval)
                               {
                                   if (dtRow["Name"].ToString().Trim().ToUpper() == dtRowval["CustomColName"].ToString().Trim().ToUpper())
                                   {
                                       objCustomInfo.ListColValues.Add(dtRowval["CustomFieldValue"].ToString().Trim());
                                   }
                               }

                           }
                       }
                       customInfo.Add(objCustomInfo);
                   }
               }
           }
           catch (Exception ex)
           {
               _log.Error("Error:CompProdSKUClass:GetCustomColumnInfo:: " + ex.Message + Environment.NewLine + ex.StackTrace);
               throw new Exception(ex.Message);

           }
           finally
           {
               _log.Trace("CompProdSKUClass:GetCustomColumnInfo:Leaving");
           }
           return customInfo;
       }

       public KTSKUDetails GetSKUForSkuID(int dataOwnerID, long SKU_ID)
       {
           KTSKUDetails skudetails = null;
           try
           {
               _log.Trace("CompProdSKUClass:Entering GetSKUForSkuID ... ");
               SKUMaster clsSKU = new SKUMaster();
               clsSKU.DataOwnerID = dataOwnerID;
               clsSKU.SkuID = SKU_ID;

               DataTable dtSKU = clsSKU.SelectOne();
               CompanyCustom clsSKUCustom = new CompanyCustom();
               clsSKUCustom.DataOwnerID = dataOwnerID;
               clsSKUCustom.CategoryID = 4;


               if (dtSKU.Rows.Count > 0)
               {
                   CustomFieldCatagory CFcategory = new CustomFieldCatagory();
                   DataTable dtCat = CFcategory.SelectAll();
                   DataRow[] dr = dtCat.Select("CategoryID ='" + Convert.ToString(clsSKUCustom.CategoryID) + "_SKUCustom'");
                   for (int i = 0; i < dr.Length; i++)
                   {
                       clsSKUCustom.CustTableName = dr[i]["TableName"].ToString(); ;
                   }
                   DataTable dtAllCustomColumns = clsSKUCustom.GetCustomColumnSchema();
                   List<string> allSKUList = new List<string>();
                   foreach (DataRow drField in dtAllCustomColumns.Rows)
                       allSKUList.Add(drField["name"].ToString());

                 
                   DataRow dSkuRow = dtSKU.Rows[0];

                   Dictionary<string, string> customColumnDetails = new Dictionary<string, string>();
                   List<string> userFields = new List<string>();

                   foreach (string colName in allSKUList)
                   {
                       if (dtSKU.Columns.Contains(colName))
                           userFields.Add(colName);
                   }
                   if (userFields != null && userFields.Count > 0)
                   {
                       foreach (string field in userFields)
                       {

                           if (!customColumnDetails.ContainsKey(field))
                               customColumnDetails[field] = dSkuRow[field].ToString();

                       }
                   }
                   string skudescription = "", ProductSKU = "", Name="";
                   DateTime createddate = DateTime.MinValue; DateTime updateddate = DateTime.MinValue;
                   int createdby = 0, updatedby = 0, DataOwnerID = 0, PackageID=0;
                   long ProdcutID = 0;
                   if (dSkuRow["SKUDescription"] != null && dSkuRow["SKUDescription"].ToString() != string.Empty)
                       skudescription = dSkuRow["SKUDescription"].ToString();
                   if (dSkuRow["CreatedDate"] != null && dSkuRow["CreatedDate"].ToString() != string.Empty)
                       createddate = Convert.ToDateTime(dSkuRow["CreatedDate"].ToString());
                   if (dSkuRow["UpdatedDate"] != null && dSkuRow["UpdatedDate"].ToString() != string.Empty)
                       updateddate = Convert.ToDateTime(dSkuRow["UpdatedDate"].ToString());
                   if (dSkuRow["CreatedBy"] != null && dSkuRow["CreatedBy"].ToString() != string.Empty)
                       createdby = int.Parse(dSkuRow["CreatedBy"].ToString());
                   if (dSkuRow["UpdatedBy"] != null && dSkuRow["UpdatedBy"].ToString() != string.Empty)
                       updatedby = int.Parse(dSkuRow["UpdatedBy"].ToString());
                   if (dSkuRow["ProductSKU"] != null && dSkuRow["ProductSKU"].ToString() != string.Empty)
                       ProductSKU = dSkuRow["ProductSKU"].ToString();
                   if (dSkuRow["DataOwnerID"] != null && dSkuRow["DataOwnerID"].ToString() != string.Empty)
                       DataOwnerID = int.Parse(dSkuRow["DataOwnerID"].ToString());
                   if (dSkuRow["ProdcutID"] != null && dSkuRow["ProdcutID"].ToString() != string.Empty)
                       ProdcutID = long.Parse(dSkuRow["ProdcutID"].ToString());
                   if (dSkuRow["PackageID"] != null && dSkuRow["PackageID"].ToString() != string.Empty)
                       PackageID = int.Parse(dSkuRow["PackageID"].ToString());
                   if (dSkuRow["Name"] != null && dSkuRow["Name"].ToString() != string.Empty)
                       Name = dSkuRow["Name"].ToString();

                   skudetails = new KTSKUDetails(ProdcutID,
                       long.Parse(dSkuRow["SKU_ID"].ToString()), ProductSKU,
                       skudescription, customColumnDetails, DataOwnerID,
                       createdby, updatedby, createddate, updateddate, PackageID, Name);
               }
           }
           catch (Exception ex)
           {
               _log.ErrorException("CompProdSKUClass:Error in GetSKUForSkuID :" + ex.Message, ex);
               throw ex;
           }
            finally
            {
                _log.Trace("CompProdSKUClass:Leaving GetSKUForSkuID ... ");
            }
           return skudetails;
       }

       public KTProductDetails GetProductForProductID(int dataOwnerID, long Productid)
       {
           KTProductDetails productdetails = null;
           try
           {
               _log.Trace("CompProdSKUClass:Entering GetProductForProductID ... ");
               ProductMaster clsproduct = new ProductMaster();
               clsproduct.DataOwnerID = dataOwnerID;
               clsproduct.ProdID = Convert.ToInt64(Productid);
               DataTable dtProduct = clsproduct.SelectOne();

               if (dtProduct.Rows.Count > 0)
               {
                   CompanyCustom clsProductCustom = new CompanyCustom();
                   clsProductCustom.DataOwnerID = dataOwnerID;
                   clsProductCustom.CategoryID = 2;

                   CustomFieldCatagory CFcategory = new CustomFieldCatagory();
                   DataTable dtCat = CFcategory.SelectAll();
                   DataRow[] dr = dtCat.Select("CategoryID ='" + Convert.ToString(clsProductCustom.CategoryID) + "_ProductCustom'");
                   for (int i = 0; i < dr.Length; i++)
                   {
                       clsProductCustom.CustTableName = dr[i]["TableName"].ToString(); ;
                   }
                   DataTable dtAllCustomColumns = clsProductCustom.GetCustomColumnSchema();
                   List<string> allProductList = new List<string>();

                   foreach (DataRow drField in dtAllCustomColumns.Rows)
                       allProductList.Add(drField["name"].ToString());

                  
                   DataRow dProductRow = dtProduct.Rows[0];

                   Dictionary<string, string> customColumnDetails = new Dictionary<string, string>();
                   List<string> userFields = new List<string>();

                   foreach (string colName in allProductList)
                   {
                       if (dtProduct.Columns.Contains(colName))
                           userFields.Add(colName);
                   }
                   if (userFields != null && userFields.Count > 0)
                   {
                       foreach (string field in userFields)
                       {

                           if (!customColumnDetails.ContainsKey(field))
                               customColumnDetails[field] = dProductRow[field].ToString();

                       }
                   }
                   string productprefix = "", ProductName = "";
                   DateTime createddate = DateTime.MinValue; DateTime updateddate = DateTime.MinValue;
                   int createdby = 0, updatedby = 0, CompanyId = 0, DataOwnerId = 0;
                   if (dProductRow["ProductPrefix"] != null && dProductRow["ProductPrefix"].ToString() != string.Empty)
                       productprefix = dProductRow["ProductPrefix"].ToString();
                   if (dProductRow["CreatedDate"] != null && dProductRow["CreatedDate"].ToString() != string.Empty)
                       createddate = Convert.ToDateTime(dProductRow["CreatedDate"].ToString());
                   if (dProductRow["UpdatedDate"] != null && dProductRow["UpdatedDate"].ToString() != string.Empty)
                       updateddate = Convert.ToDateTime(dProductRow["UpdatedDate"].ToString());
                   if (dProductRow["CreatedBy"] != null && dProductRow["CreatedBy"].ToString() != string.Empty)
                       createdby = int.Parse(dProductRow["CreatedBy"].ToString());
                   if (dProductRow["UpdatedBy"] != null && dProductRow["UpdatedBy"].ToString() != string.Empty)
                       updatedby = int.Parse(dProductRow["UpdatedBy"].ToString());
                   if (dProductRow["CompanyId"] != null && dProductRow["CompanyId"].ToString() != string.Empty)
                       CompanyId = int.Parse(dProductRow["CompanyId"].ToString());
                   if (dProductRow["ProductName"] != null && dProductRow["ProductName"].ToString() != string.Empty)
                       ProductName = dProductRow["ProductName"].ToString();
                   if (dProductRow["DataOwnerId"] != null && dProductRow["DataOwnerId"].ToString() != string.Empty)
                       DataOwnerId = int.Parse(dProductRow["DataOwnerId"].ToString());

                   productdetails = new KTProductDetails(CompanyId, long.Parse(dProductRow["ProductID"].ToString())
                       , ProductName, productprefix, customColumnDetails,
                       DataOwnerId, createdby, updatedby, createddate, updateddate);
               }
           }
           catch (Exception ex)
           {
               _log.ErrorException("CompProdSKUClass:Error in GetProductForProductID :" + ex.Message, ex);
               throw ex;
           }
           finally
           {
               _log.Trace("CompProdSKUClass:Leaving GetProductForProductID ... ");
           }
           return productdetails;
       }

       public KTCompanyDetails GetCompanyForCompanyID(int dataOwnerID, int CompanyId)
       {
           KTCompanyDetails companydetails = null;
           try
           {
               _log.Trace("CompProdSKUClass:Entering GetCompanyForCompanyID ... ");
               CompanyMaster clsCompany = new CompanyMaster();
               clsCompany.DataOwnerID = dataOwnerID;
               clsCompany.CompID = Convert.ToInt32(CompanyId);
               DataTable dtCompany = clsCompany.SelectOne();


               CompanyCustom clsCompanyCustom = new CompanyCustom();
               clsCompanyCustom.DataOwnerID = dataOwnerID;
               clsCompanyCustom.CategoryID = 1;

               CustomFieldCatagory CFcategory = new CustomFieldCatagory();
               DataTable dtCat = CFcategory.SelectAll();
               DataRow[] dr = dtCat.Select("CategoryID ='" + Convert.ToString(clsCompanyCustom.CategoryID) + "_CompanyCustom'");

               if (dtCompany.Rows.Count > 0)
               {

                   for (int i = 0; i < dr.Length; i++)
                   {
                       clsCompanyCustom.CustTableName = dr[i]["TableName"].ToString(); ;
                   }

                   DataTable dtAllCustomColumns = clsCompanyCustom.GetCustomColumnSchema();
                   List<string> allCompanyList = new List<string>();

                   foreach (DataRow drField in dtAllCustomColumns.Rows)
                       allCompanyList.Add(drField["name"].ToString());

                 
                   DataRow dCompanyRow = dtCompany.Rows[0];

                   Dictionary<string, string> customColumnDetails = new Dictionary<string, string>();
                   List<string> userFields = new List<string>();
                   foreach (string colName in allCompanyList)
                   {
                       if (dtCompany.Columns.Contains(colName))
                           userFields.Add(colName);
                   }
                   if (userFields != null && userFields.Count > 0)
                   {
                       foreach (string field in userFields)
                       {
                           if (!customColumnDetails.ContainsKey(field))
                               customColumnDetails[field] = dCompanyRow[field].ToString();

                       }
                   }
                   string companyprefix = ""; bool isepc = false;
                   DateTime createddate = DateTime.MinValue; DateTime updateddate = DateTime.MinValue;
                   int createdby = 0, updatedby = 0, DataOwnerId = 0;
                   string CompanyName = "";
                   if (dCompanyRow["CompanyName"] != null && dCompanyRow["CompanyName"].ToString() != string.Empty)
                       CompanyName = dCompanyRow["CompanyName"].ToString();
                   if (dCompanyRow["CompanyPrefix"] != null && dCompanyRow["CompanyPrefix"].ToString() != string.Empty)
                       companyprefix = dCompanyRow["CompanyPrefix"].ToString();
                   if (dCompanyRow["IsEPC"] != null && dCompanyRow["IsEPC"].ToString() != string.Empty)
                       isepc = Convert.ToBoolean(dCompanyRow["IsEPC"].ToString());
                   if (dCompanyRow["CreatedDate"] != null && dCompanyRow["CreatedDate"].ToString() != string.Empty)
                       createddate = Convert.ToDateTime(dCompanyRow["CreatedDate"].ToString());
                   if (dCompanyRow["UpdatedDate"] != null && dCompanyRow["UpdatedDate"].ToString() != string.Empty)
                       updateddate = Convert.ToDateTime(dCompanyRow["UpdatedDate"].ToString());
                   if (dCompanyRow["CreatedBy"] != null && dCompanyRow["CreatedBy"].ToString() != string.Empty)
                       createdby = int.Parse(dCompanyRow["CreatedBy"].ToString());
                   if (dCompanyRow["UpdatedBy"] != null && dCompanyRow["UpdatedBy"].ToString() != string.Empty)
                       updatedby = int.Parse(dCompanyRow["UpdatedBy"].ToString());
                   if (dCompanyRow["DataOwnerId"] != null && dCompanyRow["DataOwnerId"].ToString() != string.Empty)
                       DataOwnerId = int.Parse(dCompanyRow["DataOwnerId"].ToString());



                   companydetails = new KTCompanyDetails(isepc, int.Parse(dCompanyRow["CompanyID"].ToString()),
                     CompanyName, companyprefix, customColumnDetails,
                      DataOwnerId, createdby, updatedby, createddate, updateddate);
               }
           }
           catch (Exception ex)
           {
               _log.ErrorException("CompProdSKUClass:Error in GetCompanyForCompanyID: " + ex.Message, ex);
               throw ex;
           }
           finally
           {
               _log.Trace("CompProdSKUClass:Leaving GetCompanyForCompanyID ... ");
           }
           return companydetails;

       }
       
    }
}
