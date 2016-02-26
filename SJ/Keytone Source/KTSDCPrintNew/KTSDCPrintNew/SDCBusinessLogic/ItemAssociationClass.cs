using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml;
using KTone.DAL.SmartDCDataAccess;
using System.Configuration;
using System.Data.SqlTypes;
using KTone.DAL.KTDBBaseLib;
using KTone.DAL.KTDAGlobaApplLib;
using KTone.Core.KTIRFID;

namespace KTone.Core.SDCBusinessLogic
{
    public class ItemAssociationClass
    {
        NLog.Logger _log = NLog.LogManager.GetCurrentClassLogger();
        public enum ItemIDType
        {
            RfTagID,
            ProductORSku,
        }
        public List<KTItemDetails> GetItemDetailsForProductandSKU(int dataOwnerID)//CustomerUniqueID, int dataOwnerID)
        {
            long ItemID = 0;
            string prod_Sku = string.Empty;
            //prod_Sku = productName + "*" + productSKU;
            List<KTItemDetails> objitem = new List<KTItemDetails>(); ;
            try
            {
                _log.Trace("ItemAssociationClass:GetItemDetailsForProductandSKU:Entering");

                //IKTSDCCache sdcCache = SDCWSHelper.GetSDCCache();
               // if (sdcCache == null)
               // {
                _log.Trace("ItemAssociationClass:GetItemDetailsForProductandSKU:sdcCache  null");
                objitem = FillItemDetailsForProductSKU(ItemIDType.ProductORSku, dataOwnerID);
               // }
                //else
                //{
                //    _log.Trace("ItemAssociationClass:GetItemDetailsForCustomerID:sdcCache not null");
                //    ItemID = sdcCache.GetItemDetailsForCustomerID(GetItemDetailsForProductandSKU);
                //    return objitem = sdcCache.GetItemsForID(dataOwnerID, ItemID);
                //}
            }
            catch (Exception ex)
            {
                _log.Error("Error:ItemAssociationClass:GetItemDetailsForProductandSKU:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);

            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("ItemAssociationClass:GetItemDetailsForProductandSKU:Leaving");
            }
            return objitem;
        }

        public KTItemDetails GetItemDetailsForRFTAGIDForAssociation(string RFTAGID, int dataOwnerID)
        {
            long ItemID = 0;

            KTItemDetails objitem = null;
            try
            {
                _log.Trace("ItemAssociationClass:GetItemDetailsForRFTAGID Entering");
               // IKTSDCCache sdcCache = SDCWSHelper.GetSDCCache();
                //if (sdcCache == null)
                //{
                    _log.Trace("ItemAssociationClass:GetItemDetailsForRFTAGID:sdcCache  null");
                    objitem = FillItemDetails(ItemIDType.RfTagID, RFTAGID, dataOwnerID);
                //}
                //else
                //{
                //    _log.Trace("ItemAssociationClass:GetItemDetailsForRFTAGID:sdcCache not  null");
                //    ItemID = sdcCache.GetItemDetailsForRFTAGID(RFTAGID);
                //    return objitem = sdcCache.GetItemsForID(dataOwnerID, ItemID);
                //}
            }
            catch (Exception ex)
            {
                _log.Error("Error:ItemAssociationClass:GetItemDetailsForRFTAGID:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("ItemAssociationClass:GetItemDetailsForRFTAGID Leaving");
            }
            return objitem;
        }

        public List<KTItemDetails> FillItemDetailsForProductSKU(ItemIDType Type, int dataOwnerID)
        {
            List<KTItemDetails> lstitemdetails = new List<KTItemDetails>();
             try
           {
               _log.Trace("ItemClass:GetItemForSkuID:Entering");
               ItemMaster clsItem = new ItemMaster();
               DataSet dtallItemMaster = new DataSet(); ;
               if (Type == ItemIDType.ProductORSku)
               {
                   clsItem.DataOwnerID = dataOwnerID;
                   dtallItemMaster = clsItem.SelectAllProdandSKU();
               }
              
              
               CompanyCustom clsItemCustom = new CompanyCustom();
               clsItemCustom.DataOwnerID = dataOwnerID;
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
                               if (!customColumnDetails.ContainsKey(field))
                                   customColumnDetails[field] = dItemRow[field].ToString();
                           }
                       }

                       List<SDCTagData> tagDetails = new List<SDCTagData>();
                       DataRow[] drTags = dtallItemMaster.Tables[0].Select("ID = " + dItemRow["ID"].ToString());

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
                           if (dItemRow["ItemStatus"] != null && dItemRow["ItemStatus"].ToString() != string.Empty)
                               ItemStatus = dItemRow["ItemStatus"].ToString();                          

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

                           lstitemdetails.Add(itemdetails);
                      
                   }
                   _log.Trace("ItemClass:GetItemForSkuID:Leaving");
               }
           }
           catch (Exception ex)
           {
               _log.ErrorException("ItemClass:Error in GetItemForSkuID: " + ex.Message, ex);
               throw ex;
           }
           return lstitemdetails;
       
        }
        public KTItemDetails FillItemDetails(ItemIDType Type, string id, int dataOwnerID)
        {
            KTItemDetails itemdetails = null;
            try
            {
                _log.Trace("ItemAssociationClass:ItemDetails:Entering");
               
                DataSet dtItem = null;
                ItemMaster clsItem = new ItemMaster();
                
                if (Type == ItemIDType.RfTagID)
                {                  
                    clsItem.RFTagIDURN = id;
                    clsItem.DataOwnerID = dataOwnerID;
                    dtItem = clsItem.SelectOneRftagID();
                }

                CompanyCustom clsItemCustom = new CompanyCustom();
                clsItemCustom.DataOwnerID = dataOwnerID;
                clsItemCustom.CategoryID = 3;

                CustomFieldCatagory CFcategory = new CustomFieldCatagory();
                DataTable dtCat = CFcategory.SelectAll();
                DataRow[] dr = dtCat.Select("CategoryID ='" + Convert.ToString(clsItemCustom.CategoryID) + "_ItemCustom'");
                for (int i = 0; i < dr.Length; i++)
                {
                    clsItemCustom.CustTableName = dr[i]["TableName"].ToString(); ;
                }
                DataTable dtAllCustomColumns = clsItemCustom.GetCustomColumnSchema();
                List<string> allItemList = new List<string>();
                foreach (DataRow drField in dtAllCustomColumns.Rows)
                    allItemList.Add(drField["name"].ToString());

                if (dtItem.Tables[0].Rows.Count > 0)
                {

                    DataRow dItemRow = dtItem.Tables[0].Rows[0];

                    Dictionary<string, string> customColumnDetails = new Dictionary<string, string>();
                    List<string> userFields = new List<string>();

                    foreach (string colName in allItemList)
                    {
                        if (dtItem.Tables[0].Columns.Contains(colName))
                            userFields.Add(colName);
                    }
                    if (userFields != null && userFields.Count > 0)
                    {
                        foreach (string field in userFields)
                        {

                            if (!customColumnDetails.ContainsKey(field))
                                customColumnDetails[field] = dItemRow[field].ToString();

                        }
                    }
                    string RfTagId = string.Empty;
                    bool IsActive = true;
                    string Comments = string.Empty;
                    string ProductName = dItemRow["ProductName"].ToString();
                    string ProductSKU = dItemRow["ProductSKU"].ToString();
                    List<SDCTagData> tagDetails = new List<SDCTagData>();
                    DataRow[] drTags = dtItem.Tables[1].Select("ID = " + dItemRow["ID"].ToString());

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

                    if (Type == ItemIDType.RfTagID && IsActive == false)
                    {
                        itemdetails = null;
                        _log.Trace("Error:ItemAssociationClass:GetItemForSkuID::Tag Is InActive");
                    }
                    else
                    {
                        DateTime createddate = DateTime.MinValue; DateTime updateddate = DateTime.MinValue;
                        DateTime LastSeenTime = DateTime.MinValue;
                        int createdby = 0, updatedby = 0, DataOwnerId = 0, LastSeenLocation = 0;
                        long SKU_ID = 0;
                        string Status = "", CustomerUniqueID = "", ItemStatus = "",LocationName="";

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
                        if (dItemRow["ItemStatus"] != null && dItemRow["ItemStatus"].ToString() != string.Empty)
                            ItemStatus = dItemRow["ItemStatus"].ToString();                        

                        itemdetails = new KTItemDetails(DataOwnerId,
                            long.Parse(dItemRow["ID"].ToString()), SKU_ID, Status,
                            CustomerUniqueID, customColumnDetails, createdby, updatedby,
                            createddate, updateddate);
                        itemdetails.LastSeenTime = LastSeenTime;
                        itemdetails.LastSeenLocation = LastSeenLocation;
                        itemdetails.ItemStatus = ItemStatus;
                        itemdetails.IsActive = IsActive;
                        itemdetails.Comments = Comments;
                        itemdetails.TagDetails = tagDetails;
                        itemdetails.TagDetails = tagDetails;
                        itemdetails.ProductName = ProductName;
                        itemdetails.ProductSKU = ProductSKU;

                    }

                }

            }
            catch (Exception ex)
            {
                _log.Error("Error:ItemAssociationClass:ItemDetails:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);

            }
            finally
            {
                _log.Trace("ItemAssociationClass:ItemDetails:Leaving");
            }

            return itemdetails;

        }
    }
}
