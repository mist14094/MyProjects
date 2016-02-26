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
    public class LocationMonitorClass
    {
        NLog.Logger _log = NLog.LogManager.GetCurrentClassLogger();

        public List<KTLocationMonitor> GetAllCompProdSkuCount(int dataOwnerID, int LocationID)
        {
            List<KTLocationMonitor> locationMonitorDetails = new List<KTLocationMonitor>();
            try
            {
                _log.Trace("LocationMonitor:GetAllCompProdSkuCount:Entering");
                //IKTSDCCache sdcCache = SDCWSHelper.GetSDCCache();
                //if (sdcCache == null)
                //{
                    _log.Trace("LocationMonitor:GetAllCompProdSkuCount:sdcCache  null");
                    locationMonitorDetails = FillAllCompProdSkuCount(dataOwnerID, LocationID);
                //}
                //else
                //{
                //    _log.Trace("LocationMonitor:GetAllCompProdSkuCount:sdcCache not null");
                //    locationMonitorDetails = sdcCache.GetAllCompProdSkuCount(dataOwnerID, LocationID);
                //}

            }
            catch (Exception ex)
            {
                _log.Error("Error:LocationMonitor:GetAllCompProdSkuCount:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("LocationMonitor:GetAllCompProdSkuCount:Leaving");
            }
            return locationMonitorDetails;
        }
        public List<KTItemDetails> GetAllItemDetails(int dataOwnerID, int LocationID, ItemCategory ItemCat, long ID , bool Locationmode)
        {
            List<KTItemDetails> objItemDetails = new List<KTItemDetails>();
            try
            {
                _log.Trace("LocationMonitor:GetAllItemDetails:Entering");
                //IKTSDCCache sdcCache = SDCWSHelper.GetSDCCache();
                //if (sdcCache == null)
                //{
                    _log.Trace("LocationMonitor:GetAllItemDetails:sdcCache  null");
                    objItemDetails = FillAllLocationMonitorItemDetails(dataOwnerID, LocationID, ItemCat, ID, Locationmode);
                //}
                //else
                //{
                //    _log.Trace("LocationMonitor:GetAllItemDetails:sdcCache not null");
                //    objItemDetails = sdcCache.GetAllLocationMonitorItemDetails(dataOwnerID, LocationID, ItemCat, ID);
                //}

            }
            catch (Exception ex)
            {
                _log.Error("Error:LocationMonitor:GetAllItemDetails:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("LocationMonitor:GetAllItemDetails:Leaving");
            }
            return objItemDetails;
        }
        public List<KTLocationMonitor> FillAllCompProdSkuCount(int dataOwnerID, int LocationID)
        {
            List<KTLocationMonitor> locationMonitorDetails = new List<KTLocationMonitor>();
            try
            {
                _log.Trace("LocationMonitor:FillAllCompProdSkuCount:Entering");

                LocationMonitor objLocMonitor = new LocationMonitor();
                objLocMonitor.DataOwnerID = dataOwnerID;
                objLocMonitor.LocationID = LocationID;
                DataTable dtLocation = objLocMonitor.SelectAll();
                if (dtLocation != null && dtLocation.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtLocation.Rows)
                    {
                        int companyID = 0, count = 0,locationID=0;
                        long productID = 0, skuID = 0;
                        string companyName = string.Empty, productName = string.Empty,
                            skuName = string.Empty,locationName=string.Empty ,stencilData =string.Empty,locationzone=string.Empty;
                        byte[] locationimage = new byte[8];
                        if (dr["COMPANYID"] != null && dr["COMPANYID"].ToString() != string.Empty)
                            companyID = int.Parse(dr["COMPANYID"].ToString());
                        if (dr["ITEMCOUNT"] != null && dr["ITEMCOUNT"].ToString() != string.Empty)
                            count = int.Parse(dr["ITEMCOUNT"].ToString());
                        if (dr["PRODUCTID"] != null && dr["PRODUCTID"].ToString() != string.Empty)
                            productID = long.Parse(dr["PRODUCTID"].ToString());
                        if (dr["SKU_ID"] != null && dr["SKU_ID"].ToString() != string.Empty)
                            skuID = long.Parse(dr["SKU_ID"].ToString());
                        if (dr["COMPANYNAME"] != null && dr["COMPANYNAME"].ToString() != string.Empty)
                            companyName = dr["COMPANYNAME"].ToString();
                        if (dr["PRODUCTNAME"] != null && dr["PRODUCTNAME"].ToString() != string.Empty)
                            productName = dr["PRODUCTNAME"].ToString();
                        if (dr["PRODUCTSKU"] != null && dr["PRODUCTSKU"].ToString() != string.Empty)
                            skuName = dr["PRODUCTSKU"].ToString();
                        if (dr["LOCATIONID"] != null && dr["LOCATIONID"].ToString() != string.Empty)
                            locationID =  int.Parse(dr["LOCATIONID"].ToString());
                         if (dr["LOCATIONNAME"] != null && dr["LOCATIONNAME"].ToString() != string.Empty)
                            locationName = dr["LOCATIONNAME"].ToString();
                         if (dr["STENCILDATA"] != null && dr["STENCILDATA"].ToString() != string.Empty)
                             stencilData = dr["STENCILDATA"].ToString();
                         if (dr["LOCATIONZONE"] != null && dr["LOCATIONZONE"].ToString() != string.Empty)
                             locationzone = dr["LOCATIONZONE"].ToString();
                         if (dr["LOCATIONIMAGE"] != null && dr["LOCATIONIMAGE"].ToString() != string.Empty)
                             locationimage = (byte[])(dr["LOCATIONIMAGE"]);
                        
                        KTLocationMonitor objKTlocMonitor = new KTLocationMonitor(companyID, productID, skuID,
                                                           companyName, productName, skuName, count, locationName, locationID,stencilData,locationzone,locationimage);

                        locationMonitorDetails.Add(objKTlocMonitor);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error:LocationMonitor:FillAllCompProdSkuCount:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                _log.Trace("LocationMonitor:FillAllCompProdSkuCount:Leaving");
            }

            return locationMonitorDetails;

        }

        public List<KTItemDetails> FillAllLocationMonitorItemDetails(int dataOwnerID, int LocationID, ItemCategory ItemCat, long ID, bool Locationmode)
        {
            List<KTItemDetails> objKTItemDetails = new List<KTItemDetails>();
            try
            {
                _log.Trace("LocationMonitor:FillAllLocationMonitorItemDetails:Entering");
                DataSet dtallItemMaster = null;
                LocationMonitor objLocMonitor = new LocationMonitor();
             
                if (ItemCat == ItemCategory.Company)
                {
                    objLocMonitor.LocationID = LocationID;
                    objLocMonitor.DataOwnerID = dataOwnerID;
                    objLocMonitor.CompanyID = Convert.ToInt32(ID);
                    objLocMonitor.ProductID = 0;
                    objLocMonitor.SKUID = 0;
                    objLocMonitor.LocationMode = Locationmode;
                    dtallItemMaster = objLocMonitor.SelectAllItem();
                }
                if (ItemCat == ItemCategory.Product)
                {
                    objLocMonitor.LocationID = LocationID;
                    objLocMonitor.DataOwnerID = dataOwnerID;
                    objLocMonitor.CompanyID = 0;
                    objLocMonitor.ProductID = Convert.ToInt64(ID);
                    objLocMonitor.SKUID = 0;
                    objLocMonitor.LocationMode = Locationmode;
                    dtallItemMaster = objLocMonitor.SelectAllItem();
                }
                if (ItemCat == ItemCategory.SKU)
                {
                    objLocMonitor.LocationID = LocationID;
                    objLocMonitor.DataOwnerID = dataOwnerID;
                    objLocMonitor.CompanyID = 0;
                    objLocMonitor.ProductID = 0;
                    objLocMonitor.SKUID = Convert.ToInt64(ID);
                    objLocMonitor.LocationMode = Locationmode;
                    dtallItemMaster = objLocMonitor.SelectAllItem();
                }

                CompanyCustom clsItemCustom = new CompanyCustom();
                clsItemCustom.DataOwnerID = 0;
                clsItemCustom.CategoryID = 3;

                CustomFieldCatagory CFcategory = new CustomFieldCatagory();
                DataTable dtCat = CFcategory.SelectAll();
                DataRow[] dr = dtCat.Select("CategoryID ='" + Convert.ToString(clsItemCustom.CategoryID) + "_ItemCustom'");
                for (int i = 0; i < dr.Length; i++)
                {
                    clsItemCustom.CustTableName = dr[i]["TableName"].ToString(); 
                }
                DataTable dtAllCustomColumns = clsItemCustom.GetCustomColumnSchema();
                List<string> allItemList = new List<string>();
                foreach (DataRow drField in dtAllCustomColumns.Rows)
                    allItemList.Add(drField["name"].ToString());

                if (dtallItemMaster != null && dtallItemMaster.Tables.Count > 0)
                {

                    foreach (DataRow dItemRow in dtallItemMaster.Tables[0].Rows)
                    {
                        string RfTagId = string.Empty;
                        bool IsActive = false;
                        string Comments = string.Empty;
                        Dictionary<string, string> customColumnDetails = new Dictionary<string, string>();
                        List<string> userFields = new List<string>();

                        foreach (string colName in allItemList)
                        {
                            if (dtallItemMaster.Tables[0].Columns.Contains(colName))
                                userFields.Add(colName);
                        }
                        if (userFields != null && userFields.Count > 0)
                        {
                            foreach (string field in userFields)
                            {
                                //DataRow[] customColumn = dtAllCustomColumns.Select("ID = " + dItemRow["ID"].ToString() + " AND Name = '" + field + "'");
                                //if (customColumn.Length > 0)
                                //{
                                if (!customColumnDetails.ContainsKey(field))
                                    customColumnDetails[field] = dItemRow[field].ToString();
                                //}
                            }
                        }

                        List<SDCTagData> tagDetails = new List<SDCTagData>();
                        DataRow[] drTags = dtallItemMaster.Tables[1].Select("ID = " + dItemRow["ID"].ToString());

                        if (drTags != null && drTags.Length > 0)
                        {
                            foreach (DataRow drt in drTags)
                            {
                                SDCTagData tag = null;
                                int type = Convert.ToInt32(drt["TAGType"].ToString());
                                tag = new SDCTagData(type, drt["RFTagID"].ToString());
                                tagDetails.Add(tag);
                                RfTagId = drt["RFTagID"].ToString();
                                if (drt["IsActive"] == null || drt["IsActive"].ToString() == string.Empty)
                                {
                                    IsActive = false;
                                }
                                else
                                {
                                    IsActive = Convert.ToBoolean(drt["IsActive"].ToString());
                                }
                                if (drt["Comments"] != null && drt["Comments"].ToString() != string.Empty)
                                    Comments = drt["Comments"].ToString();

                            }
                        }


                        DateTime createddate = DateTime.MinValue; DateTime updateddate = DateTime.MinValue;
                        DateTime LastSeenTime = DateTime.MinValue;
                        int createdby = 0, updatedby = 0, DataOwnerId = 0, LastSeenLocation = 0;
                        long SKU_ID = 0;
                        string Status = "", CustomerUniqueID = "", ItemStatus = "", LocationName = "";

                        if (dItemRow["CreatedDate"] != null && dItemRow["CreatedDate"].ToString() != string.Empty)
                            createddate = Convert.ToDateTime(dItemRow["CreatedDate"].ToString());
                        if (dItemRow["UpdatedDate"] != null && dItemRow["UpdatedDate"].ToString() != string.Empty)
                            updateddate = Convert.ToDateTime(dItemRow["UpdatedDate"].ToString());
                        if (dItemRow["CreatedBy"] != null && dItemRow["CreatedBy"].ToString() != string.Empty)
                            createdby = int.Parse(dItemRow["CreatedBy"].ToString());
                        if (dItemRow["UpdatedBy"] != null && dItemRow["UpdatedBy"].ToString() != string.Empty)
                            updatedby = int.Parse(dItemRow["UpdatedBy"].ToString());
                        if (dItemRow["DataOwnerId"] != null && dItemRow["DataOwnerId"].ToString() != string.Empty)
                            DataOwnerId = int.Parse(dItemRow["DataOwnerId"].ToString());
                        if (dItemRow["SKU_ID"] != null && dItemRow["SKU_ID"].ToString() != string.Empty)
                            SKU_ID = long.Parse(dItemRow["SKU_ID"].ToString());
                        if (dItemRow["Status"] != null && dItemRow["Status"].ToString() != string.Empty)
                            Status = dItemRow["Status"].ToString();
                        if (dItemRow["CustomerUniqueID"] != null && dItemRow["CustomerUniqueID"].ToString() != string.Empty)
                            CustomerUniqueID = dItemRow["CustomerUniqueID"].ToString();
                        if (dItemRow["LastSeenLocation"] != null && dItemRow["LastSeenLocation"].ToString() != string.Empty)
                            LastSeenLocation = int.Parse(dItemRow["LastSeenLocation"].ToString());
                        if (dItemRow["LastSeenTime"] != null && dItemRow["LastSeenTime"].ToString() != string.Empty)
                            LastSeenTime = Convert.ToDateTime(dItemRow["LastSeenTime"].ToString());
                        if (dItemRow["ItemStatus"] != null && dItemRow["ItemStatus"].ToString() != string.Empty)
                            ItemStatus = dItemRow["ItemStatus"].ToString();
                        if (dItemRow["LocationName"] != null && dItemRow["LocationName"].ToString() != string.Empty)
                            LocationName = dItemRow["LocationName"].ToString();


                        KTItemDetails itemdetails = new KTItemDetails(DataOwnerId,
                            long.Parse(dItemRow["ID"].ToString()), SKU_ID, Status,
                            CustomerUniqueID, customColumnDetails, createdby, updatedby,
                            createddate, updateddate);
                        itemdetails.LastSeenTime = LastSeenTime;
                        itemdetails.LastSeenLocation = LastSeenLocation;
                        
                        //itemdetails.TagType =int.Parse(dItemRow["TagType"].ToString());
                        itemdetails.ItemStatus = ItemStatus;
                        // itemdetails.RFTagID=dItemRow["RFTagID"].ToString();
                        itemdetails.IsActive = IsActive;
                        itemdetails.Comments = Comments;
                        itemdetails.TagDetails = tagDetails;

                        objKTItemDetails.Add(itemdetails);
                    }
                }
            }

            catch (Exception ex)
            {
                _log.Error("Error:LocationMonitor:FillAllLocationMonitorItemDetails:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                _log.Trace("LocationMonitor:FillAllLocationMonitorItemDetails:Leaving");
            }
            return objKTItemDetails;
        }
    }
}
