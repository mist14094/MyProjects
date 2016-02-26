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
    public class ItemClass
    {
        NLog.Logger _log = NLog.LogManager.GetCurrentClassLogger();
        public enum ItemIDType
        {
            RfTagID,
            CustomerID,
        }
        public KTItemDetails GetItemDetailsForCustomerID(string CustomerUniqueID, int dataOwnerID)
        {

            long ItemID = 0;

            KTItemDetails objitem = null;
            try
            {
                _log.Trace("ItemClass:GetItemDetailsForCustomerID:Entering");

                IKTSDCCache sdcCache = SDCWSHelper.GetSDCCache();
                //if (sdcCache == null)
                //{
                    _log.Trace("ItemClass:GetItemDetailsForCustomerID:sdcCache  null");
                    
                    objitem = FillItemDetails(ItemIDType.CustomerID, CustomerUniqueID, dataOwnerID);


                    if (objitem != null)
                    {
                        _log.Trace("ItemClass:GetItemDetailsForRFTAGID:objitem not null and productName is " + objitem.ProductName);
                    }
                //}
                //else
                //{
                //    _log.Trace("ItemClass:GetItemDetailsForCustomerID:sdcCache not null");
                //    ItemID = sdcCache.GetItemDetailsForCustomerID(CustomerUniqueID);
                //    return objitem = sdcCache.GetItemsForID(dataOwnerID, ItemID);
                //}
            }
            catch (Exception ex)
            {
                _log.Error("Error:ItemClass:GetItemDetaisForCustomerID:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);

            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("ItemClass:GetItemDetailsForCustomerID:Leaving");
            }
            return objitem;
        }

        public KTItemDetails GetItemDetailsForRFTAGID(string RFTAGID, int dataOwnerID)
        {
            long ItemID = 0;

            KTItemDetails objitem = null;
            try
            {
                _log.Trace("ItemClass:GetItemDetailsForRFTAGID Entering");
                IKTSDCCache sdcCache = SDCWSHelper.GetSDCCache();
                //if (sdcCache == null)
                //{
                    _log.Trace("ItemClass:GetItemDetailsForRFTAGID:sdcCache  null");
                    objitem = FillItemDetails(ItemIDType.RfTagID, RFTAGID, dataOwnerID);
                

                    if (objitem != null)
                    {
                        _log.Trace("ItemClass:GetItemDetailsForRFTAGID:objitem not null and productName is " + objitem.ProductName);
                    }
                //}
                //else
                //{
                //    _log.Trace("ItemClass:GetItemDetailsForRFTAGID:sdcCache not  null");
                //    ItemID = sdcCache.GetItemDetailsForRFTAGID(RFTAGID);
                //    return objitem = sdcCache.GetItemsForID(dataOwnerID, ItemID);
                //}
            }
            catch (Exception ex)
            {
                _log.Error("Error:ItemClass:GetItemDetailsForRFTAGID:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("ItemClass:GetItemDetailsForRFTAGID Leaving");
            }
            return objitem;
        }


        public List<KTItemDetails> GetItemDetailsForRFTAGIDs(List<string> RFTAGIDs, int dataOwnerID)
        {
            long ItemID = 0;

            List<KTItemDetails> objitems = null;
            try
            {
                _log.Trace("ItemClass:GetItemDetailsForRFTAGIDs Entering");
                IKTSDCCache sdcCache = SDCWSHelper.GetSDCCache();
                //if (sdcCache == null)
               // {
                    _log.Trace("ItemClass:GetItemDetailsForRFTAGIDs:sdcCache  null");
                    objitems = FillItemDetailsForRFTags(RFTAGIDs, dataOwnerID);
                //}
                //else
                //{
                //    _log.Trace("ItemClass:GetItemDetailsForRFTAGID:sdcCache not  null");
                //    ItemID = sdcCache.GetItemDetailsForRFTAGID(RFTAGID);
                //    return objitem = sdcCache.GetItemsForID(dataOwnerID, ItemID);
                //}
            }
            catch (Exception ex)
            {
                _log.Error("Error:ItemClass:GetItemDetailsForRFTAGIDs:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("ItemClass:GetItemDetailsForRFTAGIDs Leaving");
            }
            return objitems;
        }

        public Dictionary<string, List<KTItemDetails>> GetItemDetailsForBin_Part(string BinCat, string PartNumber, int dataOwnerID)
        {
            long ItemID = 0;

            Dictionary<string, List<KTItemDetails>> _objitemlist = null;
            try
            {
                _log.Trace("ItemClass:GetItemDetailsForBin_Part Entering");
                //IKTSDCCache sdcCache = SDCWSHelper.GetSDCCache();
                //if (sdcCache == null)
                // {
                _log.Trace("ItemClass:GetItemDetailsForBin_Part:sdcCache  null");
                _objitemlist = FillItemDetailsForBin_Part(BinCat, PartNumber, dataOwnerID);
                //}
                //else
                //{
                //    _log.Trace("ItemClass:GetItemDetailsForRFTAGID:sdcCache not  null");
                //    ItemID = sdcCache.GetItemDetailsForRFTAGID(RFTAGID);
                //    return objitem = sdcCache.GetItemsForID(dataOwnerID, ItemID);
                //}
            }
            catch (Exception ex)
            {
                _log.Error("Error:ItemClass:GetItemDetailsForBin_Part:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("ItemClass:GetItemDetailsForBin_Part Leaving");
            }
            return _objitemlist;
        }


        public Dictionary<string, List<KTBinQuantity>> GetItemDetailsForBin_Part_New(string BinCat, string PartNumber, int dataOwnerID)
        {          

            Dictionary<string, List<KTBinQuantity>> _objitemlist = null;
            try
            {
                _log.Trace("ItemClass:GetItemDetailsForBin_Part_New Entering");
                //IKTSDCCache sdcCache = SDCWSHelper.GetSDCCache();
                //if (sdcCache == null)
                // {
                _log.Trace("ItemClass:GetItemDetailsForBin_Part_New:sdcCache  null");
                _objitemlist = FillItemDetailsForBin_Part_New(BinCat, PartNumber, dataOwnerID);
                //}
                //else
                //{
                //    _log.Trace("ItemClass:GetItemDetailsForRFTAGID:sdcCache not  null");
                //    ItemID = sdcCache.GetItemDetailsForRFTAGID(RFTAGID);
                //    return objitem = sdcCache.GetItemsForID(dataOwnerID, ItemID);
                //}
            }
            catch (Exception ex)
            {
                _log.Error("Error:ItemClass:GetItemDetailsForBin_Part_New:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("ItemClass:GetItemDetailsForBin_Part_New Leaving");
            }
            return _objitemlist;
        }



        public KTItemDetails FillItemDetails(ItemIDType Type, string id, int dataOwnerID)
        {
            KTItemDetails itemdetails = null;
            try
            {
                _log.Trace("ItemClass:ItemDetails:Entering");
                DataSet dtItem = null;
                ItemMaster clsItem = new ItemMaster();
                if (Type == ItemIDType.CustomerID)
                {
                    clsItem.CustomerUniqueID = id;
                    clsItem.DataOwnerID = dataOwnerID;
                    dtItem = clsItem.SelectOneSRNO();
                }
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

                if (dtItem.Tables.Count > 0 && dtItem.Tables[0] != null && dtItem.Tables[0].Rows.Count > 0)
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
                        _log.Trace("Error:ItemClass:GetItemForSkuID::Tag Is InActive");
                    }
                    else
                    {
                        DateTime createddate = DateTime.MinValue; DateTime updateddate = DateTime.MinValue;
                        DateTime LastSeenTime = DateTime.MinValue;
                        int createdby = 0, updatedby = 0, DataOwnerId = 0, LastSeenLocation = 0;
                        long SKU_ID = 0;
                        string Status = "", CustomerUniqueID = "", ItemStatus = "", ProductName = "", ProductSKU = "", LocationName = "";

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
                        if (dItemRow["ProductName"] != null && dItemRow["ProductName"].ToString() != string.Empty)
                            ProductName = dItemRow["ProductName"].ToString();
                        if (dItemRow["PRODUCTSKU"] != null && dItemRow["PRODUCTSKU"].ToString() != string.Empty)
                            ProductSKU = dItemRow["PRODUCTSKU"].ToString();
                        if (dItemRow["LocationName"] != null && dItemRow["LocationName"].ToString() != string.Empty)
                            LocationName = dItemRow["LocationName"].ToString();
                        itemdetails = new KTItemDetails(DataOwnerId,
                            long.Parse(dItemRow["ID"].ToString()), SKU_ID, Status,
                            CustomerUniqueID, customColumnDetails, createdby, updatedby,
                            createddate, updateddate, LocationName);
                        itemdetails.LastSeenTime = LastSeenTime;
                        itemdetails.LastSeenLocation = LastSeenLocation;
                        itemdetails.ProductName = ProductName;
                        itemdetails.ProductSKU = ProductSKU;
                        itemdetails.LocationName = LocationName;
                        itemdetails.ItemStatus = ItemStatus;
                        itemdetails.IsActive = IsActive;
                        itemdetails.Comments = Comments;
                        itemdetails.TagDetails = tagDetails;
                        itemdetails.TagDetails = tagDetails;

                    }

                }

            }
            catch (Exception ex)
            {
                _log.Error("Error:ItemClass:ItemDetails:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);

            }
            finally
            {
                _log.Trace("ItemClass:ItemDetails:Leaving");
            }

            return itemdetails;

        }

        public long GetItemDetailsCountForRFTAGIDs(List<string> RFTagIDs, int dataOwnerID)
        {
            long ItemCount = 0;
            try
            {
                _log.Trace("ItemClass:GetItemDetailsCountForRFTAGIDs:Entering");
                ItemMaster clsItem = new ItemMaster();
                string tagIDs = string.Empty;
               
                foreach (string rftag in RFTagIDs)
                {
                    tagIDs += rftag + ",";
                }

                tagIDs = tagIDs.Remove(tagIDs.LastIndexOf(','));

                clsItem.RFTagIDURN = tagIDs;
                clsItem.DataOwnerID = dataOwnerID;
                ItemCount = clsItem.GetItemCountForRFTagIDs();

            }
            catch (Exception ex)
            {
                _log.Error("Error:ItemClass:GetItemDetailsCountForRFTAGIDs:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("ItemClass:GetItemDetailsCountForRFTAGIDs Leaving");
            }
            return ItemCount;
        }

        public Dictionary<string, List<KTItemDetails>> FillItemDetailsForBin_Part(string BinCat, string PartNumber, int dataOwnerID)
        {
            List<KTItemDetails> _lstItemDetails = new List<KTItemDetails>();// = new List<KTItemDetails>();
            DataTable dtItemDetails = new DataTable();
            KTItemDetails objItem = null;

            Dictionary<string, List<KTItemDetails>> _lstItemDetailsOnLoc = new Dictionary<string, List<KTItemDetails>>();

            List<string> _locations = new List<string>();

            try
            {
                _log.Trace("ItemClass:FillItemDetailsForRFTags:Entering");

                ItemMaster clsItem = new ItemMaster();
                clsItem.DataOwnerID = dataOwnerID;

                dtItemDetails = clsItem.SelectOnBinCat_Part(BinCat, PartNumber);

                CustomFieldCatagory CFcategory = new CustomFieldCatagory();
                CompanyCustom clsItemCustom = new CompanyCustom();

                if (dtItemDetails != null && dtItemDetails.Rows.Count > 0)
                {
                    DataView dtView = new DataView(dtItemDetails);
                    DataTable dtDistinct = dtView.ToTable(true, "LocationName");

                    //DataRow[] dtRows = dtItemDetails.Select("DISTINCT LocationName");

                    foreach (DataRow drItem in dtDistinct.Rows)
                    {
                        DataTable dtLocItemDetails = new DataTable();

                        string locationName = Convert.ToString(drItem["LocationName"]);
                        dtLocItemDetails = dtItemDetails.Select("LocationName ='" + locationName + "'").CopyToDataTable();
                        if (dtLocItemDetails != null && dtLocItemDetails.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtLocItemDetails.Rows)
                            {
                                {
                                    objItem = new KTItemDetails();
                                    objItem.ID = Convert.ToInt64(dr["ID"]);
                                    objItem.CustomerUniqueID = Convert.ToString(dr["CustomerUniqueId"]);
                                    objItem.ItemStatus = Convert.ToString(dr["ItemStatus"]);
                                    //objItem.ProductName = Convert.ToString(dr["ProductName"]);
                                    objItem.ProductSKU = Convert.ToString(dr["ProductSKU"]);
                                    objItem.SKU_ID = Convert.ToInt64(dr["SKU_ID"]);
                                    objItem.Status = Convert.ToString(dr["Status"]);
                                    List<SDCTagData> tagDetails = new List<SDCTagData>();
                                    if (!string.IsNullOrEmpty(Convert.ToString(dr["TAGType"])))
                                    {
                                        SDCTagData tag = new SDCTagData(Convert.ToInt32(dr["TAGType"]), Convert.ToString(dr["RFTagID"]));
                                        tagDetails.Add(tag);
                                    }
                                    else
                                    {
                                        SDCTagData tag = new SDCTagData(0, Convert.ToString(dr["RFTagID"]));
                                        tagDetails.Add(tag);
                                    }

                                    objItem.TagDetails = tagDetails;

                                    if (!string.IsNullOrEmpty(Convert.ToString(dr["LastSeenTime"])))
                                    {
                                        objItem.LastSeenTime = Convert.ToDateTime(dr["LastSeenTime"]);
                                    }
                                    if (!string.IsNullOrEmpty(Convert.ToString(dr["LastSeenLocation"])))
                                    {
                                        objItem.LastSeenLocation = Convert.ToInt32(dr["LastSeenLocation"]);
                                    }
                                    if (!string.IsNullOrEmpty(Convert.ToString(dr["CreatedDate"])))
                                    {
                                        objItem.TimeStamp = Convert.ToDateTime(dr["CreatedDate"]);
                                    }

                                    if (!string.IsNullOrEmpty(Convert.ToString(dr["CreatedBy"])))
                                    {
                                        objItem.UpdatedBy = Convert.ToInt32(dr["CreatedBy"]);
                                    }
                                    if (!string.IsNullOrEmpty(Convert.ToString(dr["UpdatedBy"])))
                                    {
                                        objItem.UpdatedBy = Convert.ToInt32(dr["UpdatedBy"]);
                                    }
                                    if (!string.IsNullOrEmpty(Convert.ToString(dr["DataOwnerID"])))
                                    {
                                        objItem.DataOwnerID = Convert.ToInt32(dr["DataOwnerID"]);
                                    }



                                    DataTable dtCat = CFcategory.SelectAll();
                                    DataRow[] dtRow = dtCat.Select("CategoryID ='" + Convert.ToString(dr["CategoryId"]) + "_ItemCustom'");
                                    for (int i = 0; i < dtRow.Length; i++)
                                    {
                                        clsItemCustom.CustTableName = dtRow[i]["TableName"].ToString();
                                    }

                                    DataTable dtAllCustomColumns = clsItemCustom.GetCustomColumnSchema();
                                    List<string> allItemList = new List<string>();
                                    foreach (DataRow drField in dtAllCustomColumns.Rows)
                                        allItemList.Add(drField["name"].ToString());

                                    Dictionary<string, string> customColumnDetails = new Dictionary<string, string>();
                                    List<string> userFields = new List<string>();

                                    foreach (string colName in allItemList)
                                    {
                                        userFields.Add(colName);
                                    }
                                    if (userFields != null && userFields.Count > 0)
                                    {
                                        foreach (string field in userFields)
                                        {
                                            if (!customColumnDetails.ContainsKey(field))
                                                customColumnDetails[field] = dr[field].ToString();

                                        }
                                    }

                                    objItem.CustomColumnDetails = customColumnDetails;

                                    _lstItemDetails.Add(objItem);

                                }
                            }

                            _lstItemDetailsOnLoc.Add(locationName, _lstItemDetails);

                        }
                    }

                }


            }
            catch (Exception ex)
            {
                _log.Error("Error:ItemClass:FillItemDetailsForRFTags:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                _log.Trace("ItemClass:FillItemDetailsForRFTags:Leaving");
            }



            return _lstItemDetailsOnLoc;

        }

        public Dictionary<string, List<KTBinQuantity>> FillItemDetailsForBin_Part_New(string BinCat, string PartNumber, int dataOwnerID)
        {
            List<KTBinQuantity> _lstItemDetails = new List<KTBinQuantity>();// = new List<KTItemDetails>();
            DataTable dtItemDetails = new DataTable();
            KTBinQuantity objItem = null;

            Dictionary<string, List<KTBinQuantity>> _lstItemDetailsOnLoc = new Dictionary<string, List<KTBinQuantity>>();

            List<string> _locations = new List<string>();

            try
            {
                _log.Trace("ItemClass:FillItemDetailsForRFTags:Entering");

                ItemMaster clsItem = new ItemMaster();
                clsItem.DataOwnerID = dataOwnerID;

                dtItemDetails = clsItem.SelectOnBinCat_Part_New(BinCat, PartNumber);

                CustomFieldCatagory CFcategory = new CustomFieldCatagory();
                CompanyCustom clsItemCustom = new CompanyCustom();

                if (dtItemDetails != null && dtItemDetails.Rows.Count > 0)
                {
                    DataView dtView = new DataView(dtItemDetails);
                    DataTable dtDistinct = dtView.ToTable(true, "LocationName");

                    //DataRow[] dtRows = dtItemDetails.Select("DISTINCT LocationName");

                    foreach (DataRow drItem in dtDistinct.Rows)
                    {
                        DataTable dtLocItemDetails = new DataTable();

                        string locationName = Convert.ToString(drItem["LocationName"]);
                        dtLocItemDetails = dtItemDetails.Select("LocationName ='" + locationName + "'").CopyToDataTable();
                        _lstItemDetails = new List<KTBinQuantity>();
                        if (dtLocItemDetails != null && dtLocItemDetails.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtLocItemDetails.Rows)
                            {
                                {
                                    string BinTape = string.Empty;
                                    long Quantity = 0;

                                    BinTape =Convert.ToString (dr["CustomerUniqueId"]);
                                    Quantity = Convert.ToInt64(dr["QUANTITY"]);

                                    objItem = new KTBinQuantity(BinTape , Quantity);                                    

                                    _lstItemDetails.Add(objItem);

                                }
                            }

                            _lstItemDetailsOnLoc.Add(locationName, _lstItemDetails);

                        }
                    }

                }


            }
            catch (Exception ex)
            {
                _log.Error("Error:ItemClass:FillItemDetailsForRFTags:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                _log.Trace("ItemClass:FillItemDetailsForRFTags:Leaving");
            }



            return _lstItemDetailsOnLoc;

        }

        public List<KTItemDetails> FillItemDetailsForRFTags(List<string> RFTagIDs, int dataOwnerID)
        {
            List<KTItemDetails> _lstItemDetails = new List<KTItemDetails>();// = new List<KTItemDetails>();
            DataTable dtItemDetails = new DataTable();
            KTItemDetails objItem = null;

            try
            {
                _log.Trace("ItemClass:FillItemDetailsForRFTags:Entering");

                ItemMaster clsItem = new ItemMaster();
                string tagIDs = string.Empty;

                foreach (string rftag in RFTagIDs)
                {
                    tagIDs += rftag + ",";
                }

                tagIDs = tagIDs.Remove(tagIDs.LastIndexOf(','));

                clsItem.RFTagIDURN = tagIDs;
                clsItem.DataOwnerID = dataOwnerID;

                dtItemDetails = clsItem.GetItemDetailsForRFTagIDs();

                CustomFieldCatagory CFcategory = new CustomFieldCatagory();
                CompanyCustom clsItemCustom = new CompanyCustom();

                if (dtItemDetails != null && dtItemDetails.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtItemDetails.Rows)
                    {
                        objItem = new KTItemDetails();
                        objItem.ID = Convert.ToInt64(dr["ID"]);
                        objItem.CustomerUniqueID = Convert.ToString(dr["CustomerUniqueId"]);
                        objItem.ItemStatus = Convert.ToString(dr["ItemStatus"]);
                        objItem.ProductName = Convert.ToString(dr["ProductName"]);
                        objItem.ProductSKU = Convert.ToString(dr["ProductSKU"]);
                        objItem.SKU_ID = Convert.ToInt64(dr["SKU_ID"]);
                        objItem.Status = Convert.ToString(dr["Status"]);
                        List<SDCTagData> tagDetails = new List<SDCTagData>();
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["TAGType"])))
                        {
                            SDCTagData tag = new SDCTagData(Convert.ToInt32(dr["TAGType"]), Convert.ToString(dr["RFTagID"]));
                            tagDetails.Add(tag);
                        }
                        else
                        {
                            SDCTagData tag = new SDCTagData(0, Convert.ToString(dr["RFTagID"]));
                            tagDetails.Add(tag);
                        }

                        objItem.TagDetails = tagDetails;

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["LastSeenTime"])))
                        {
                            objItem.LastSeenTime = Convert.ToDateTime(dr["LastSeenTime"]);
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["LastSeenLocation"])))
                        {
                            objItem.LastSeenLocation = Convert.ToInt32(dr["LastSeenLocation"]);
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["CreatedDate"])))
                        {
                            objItem.TimeStamp = Convert.ToDateTime(dr["CreatedDate"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["CreatedBy"])))
                        {
                            objItem.UpdatedBy = Convert.ToInt32(dr["CreatedBy"]);
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["UpdatedBy"])))
                        {
                            objItem.UpdatedBy = Convert.ToInt32(dr["UpdatedBy"]);
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["DataOwnerID"])))
                        {
                            objItem.DataOwnerID = Convert.ToInt32(dr["DataOwnerID"]);
                        }

                        //Dictionary<string, string> custColDetails = new Dictionary<string, string>();

                        //if (!string.IsNullOrEmpty(Convert.ToString(dr["LOT_ID"])))
                        //{
                        //    objItem.DataOwnerID = Convert.ToInt32(dr["LOT_ID"]);
                        //}


                        DataTable dtCat = CFcategory.SelectAll();
                        DataRow[] dtRow = dtCat.Select("CategoryID ='" + Convert.ToString(dr["CategoryId"]) + "_ItemCustom'");
                        for (int i = 0; i < dtRow.Length; i++)
                        {
                            clsItemCustom.CustTableName = dtRow[i]["TableName"].ToString();
                        }

                        DataTable dtAllCustomColumns = clsItemCustom.GetCustomColumnSchema();
                        List<string> allItemList = new List<string>();
                        foreach (DataRow drField in dtAllCustomColumns.Rows)
                            allItemList.Add(drField["name"].ToString());

                        Dictionary<string, string> customColumnDetails = new Dictionary<string, string>();
                        List<string> userFields = new List<string>();

                        foreach (string colName in allItemList)
                        {
                            userFields.Add(colName);
                        }
                        if (userFields != null && userFields.Count > 0)
                        {
                            foreach (string field in userFields)
                            {
                                if (!customColumnDetails.ContainsKey(field))
                                    customColumnDetails[field] = dr[field].ToString();

                            }
                        }

                        objItem.CustomColumnDetails = customColumnDetails;

                        _lstItemDetails.Add(objItem);

                    }
                }


            }
            catch (Exception ex)
            {
                _log.Error("Error:ItemClass:FillItemDetailsForRFTags:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                _log.Trace("ItemClass:FillItemDetailsForRFTags:Leaving");
            }



            return _lstItemDetails;

        }

        private int GetKey(string xmlString, string value)
        {
            try
            {
                _log.Trace("ItemClass:GetKey:Entering");

                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(xmlString);


                XmlNodeList xNodeList = xDoc.SelectNodes("lookup/lookupValue");

                foreach (XmlNode xNode in xNodeList)
                {
                    if (xNode.Attributes["value"].Value == value)
                    {
                        int result = Convert.ToInt32(xNode.Attributes["key"].Value);
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error:ItemClass:GetKey:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                _log.Trace("ItemClass:GetKey:Leaving");
            }
            return 0;
        }

        public void GetLookupDB(out string Company, out string Country)
        {
            Company = string.Empty;
            Country = string.Empty;
            try
            {
                _log.Trace("ItemClass:GetLookupDB:Entering");

                ItemMaster objItem = new ItemMaster();
                DataTable dtItem = objItem.GetLookup();
                if (dtItem != null && dtItem.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtItem.Rows)
                    {
                        if ((dr["Name"].ToString()).ToUpper().Trim() == "COMPANY")
                        {
                            Company = dr["Value"].ToString();

                        }
                        else if ((dr["Name"].ToString()).ToUpper().Trim() == "COUNTRY")
                        {
                            Country = dr["Value"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error:ItemClass:GetLookupDB:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                _log.Trace("ItemClass:GetLookupDB:Leaving");
            }
        }


        public byte[] ConvertToByte(KTItemDetails Itemdetails)
        {
            string valueLKey = string.Empty;
            string value = string.Empty;
            int hexValueint = 0;
            string hexValueFinal = string.Empty;

            byte[] arrByte = Enumerable.Repeat((byte)0, 64).ToArray();

            string Company;
            string Country;
            GetLookupDB(out Company, out Country);
            try
            {
                _log.Trace("ItemClass:ConvertToByte:Entering");
                foreach (KeyValuePair<string, string> pair in Itemdetails.CustomColumnDetails)
                {
                    DateTime date = new DateTime();
                    int month = 0;
                    int IvCellsInSeries = 0;
                    decimal wattage = 0;
                    decimal current = 0;
                    decimal voltage = 0;
                    decimal resistance = 0;
                    decimal fillfactor = 0;
                    int modelNo = 0;
                    int i1 = 0;
                    int i2 = 0;
                    int res = 0;
                    string hexValue = string.Empty;


                    valueLKey = Convert.ToString(pair.Key).ToUpper().Trim();
                    value = Convert.ToString(pair.Value);

                    switch (valueLKey)
                    {

                        case ("PV_MFG"):
                            if (!string.IsNullOrEmpty(value))
                            {
                                res = GetKey(Company, value);
                                arrByte[0] = Convert.ToByte(res);
                            }
                            break;
                        case ("CELL_MFG"):
                            if (!string.IsNullOrEmpty(value))
                            {
                                res = GetKey(Company, value);
                                arrByte[1] = Convert.ToByte(res);
                            }
                            break;
                        case ("PV_COUNTRY"):
                            if (!string.IsNullOrEmpty(value))
                            {
                                res = GetKey(Country, value);
                                arrByte[6] = Convert.ToByte(res);

                            }
                            break;
                        case ("CELL_COUNTRY"):
                            if (!string.IsNullOrEmpty(value))
                            {
                                res = GetKey(Country, value);
                                arrByte[7] = Convert.ToByte(res);
                            }
                            break;
                        case ("IEC_TEST_LAB"):
                            if (!string.IsNullOrEmpty(value))
                            {
                                res = GetKey(Company, value);
                                arrByte[20] = Convert.ToByte(res);
                            }
                            break;
                        case ("DTSTAMP_PV"):
                            if (!string.IsNullOrEmpty(value))
                            {
                                date = Convert.ToDateTime(value);
                                month = Convert.ToInt32(date.Month);
                                arrByte[2] = Convert.ToByte(month);
                                arrByte[3] = Convert.ToByte(date.ToString("yy"));
                            }
                            break;
                        case ("DTSTAMP_CELL"):
                            if (!string.IsNullOrEmpty(value))
                            {
                                date = Convert.ToDateTime(value);
                                month = Convert.ToInt32(date.Month);
                                arrByte[4] = Convert.ToByte(month);
                                arrByte[5] = Convert.ToByte(date.ToString("yy"));
                            }
                            break;
                        case ("PV_WATTAGE"):
                            if (!string.IsNullOrEmpty(value))
                            {
                                wattage = Convert.ToDecimal(value) * 100;
                                hexValue = Convert.ToInt32(wattage).ToString("X");
                                if (hexValue.Length < 4)
                                {
                                    hexValueint = hexValue.Length + (4 - hexValue.Length);
                                    hexValueFinal = Convert.ToInt32(wattage).ToString("X" + hexValueint.ToString());
                                }
                                else
                                {
                                    hexValueFinal = hexValue;
                                }

                                i1 = Int32.Parse(hexValueFinal.Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
                                i2 = Int32.Parse(hexValueFinal.Substring(2, 2), System.Globalization.NumberStyles.AllowHexSpecifier);

                                arrByte[8] = Convert.ToByte(i1);
                                arrByte[9] = Convert.ToByte(i2);
                            }
                            break;
                        case ("PV_CURRENT"):
                            if (!string.IsNullOrEmpty(value))
                            {
                                current = Convert.ToDecimal(value) * 1000;
                                hexValue = Convert.ToInt32(current).ToString("X");
                                if (hexValue.Length < 4)
                                {
                                    hexValueint = hexValue.Length + (4 - hexValue.Length);
                                    hexValueFinal = Convert.ToInt32(current).ToString("X" + hexValueint.ToString());
                                }
                                else
                                {
                                    hexValueFinal = hexValue;
                                }

                                i1 = Int32.Parse(hexValueFinal.Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
                                i2 = Int32.Parse(hexValueFinal.Substring(2, 2), System.Globalization.NumberStyles.AllowHexSpecifier);

                                arrByte[10] = Convert.ToByte(i1);
                                arrByte[11] = Convert.ToByte(i2);
                            }
                            break;
                        case ("PV_VOLTAGE"):
                            if (!string.IsNullOrEmpty(value))
                            {
                                voltage = Convert.ToDecimal(value) * 1000;
                                hexValue = Convert.ToInt32(voltage).ToString("X");
                                if (hexValue.Length < 4)
                                {
                                    hexValueint = hexValue.Length + (4 - hexValue.Length);
                                    hexValueFinal = Convert.ToInt32(voltage).ToString("X" + hexValueint.ToString());
                                }
                                else
                                {
                                    hexValueFinal = hexValue;
                                }

                                i1 = Int32.Parse(hexValueFinal.Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
                                i2 = Int32.Parse(hexValueFinal.Substring(2, 2), System.Globalization.NumberStyles.AllowHexSpecifier);

                                arrByte[12] = Convert.ToByte(i1);
                                arrByte[13] = Convert.ToByte(i2);
                            }
                            break;
                        case ("PV_FILLFACTOR"):
                            if (!string.IsNullOrEmpty(value))
                            {
                                fillfactor = Convert.ToDecimal(value) * 100;
                                hexValue = Convert.ToInt32(fillfactor).ToString("X");
                                if (hexValue.Length < 4)
                                {
                                    hexValueint = hexValue.Length + (4 - hexValue.Length);
                                    hexValueFinal = Convert.ToInt32(fillfactor).ToString("X" + hexValueint.ToString());
                                }
                                else
                                {
                                    hexValueFinal = hexValue;
                                }

                                i1 = Int32.Parse(hexValueFinal.Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
                                i2 = Int32.Parse(hexValueFinal.Substring(2, 2), System.Globalization.NumberStyles.AllowHexSpecifier);

                                arrByte[14] = Convert.ToByte(i1);
                                arrByte[15] = Convert.ToByte(i2);
                            }
                            break;
                        case ("PV_MODEL"):
                            if (!string.IsNullOrEmpty(value))
                            {
                                bool result = Int32.TryParse(value, out modelNo);

                                if (result == true)
                                {
                                    hexValue = Convert.ToInt32(modelNo).ToString("X");
                                    if (hexValue.Length < 4)
                                    {
                                        hexValueint = hexValue.Length + (4 - hexValue.Length);
                                        hexValueFinal = Convert.ToInt32(modelNo).ToString("X" + hexValueint.ToString());
                                    }
                                    else
                                    {
                                        hexValueFinal = hexValue;
                                    }

                                    i1 = Int32.Parse(hexValueFinal.Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
                                    i2 = Int32.Parse(hexValueFinal.Substring(2, 2), System.Globalization.NumberStyles.AllowHexSpecifier);

                                    arrByte[16] = Convert.ToByte(i1);
                                    arrByte[17] = Convert.ToByte(i2);
                                }
                                else
                                {
                                    throw new Exception("Data is not supplied properly.");

                                }
                            }
                            break;
                        case ("DTSTAMP_PV_IEC"):
                            if (!string.IsNullOrEmpty(value))
                            {
                                date = Convert.ToDateTime(value);
                                month = Convert.ToInt32(date.Month);
                                arrByte[18] = Convert.ToByte(month);
                                arrByte[19] = Convert.ToByte(date.ToString("yy"));
                            }
                            break;
                        case ("IV_SHORTCIRCUITCURRENT"):
                            if (!string.IsNullOrEmpty(value))
                            {
                                current = Convert.ToDecimal(value) * 1000;
                                hexValue = Convert.ToInt32(current).ToString("X");
                                if (hexValue.Length < 4)
                                {
                                    hexValueint = hexValue.Length + (4 - hexValue.Length);
                                    hexValueFinal = Convert.ToInt32(current).ToString("X" + hexValueint.ToString());
                                }
                                else
                                {
                                    hexValueFinal = hexValue;
                                }

                                i1 = Int32.Parse(hexValueFinal.Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
                                i2 = Int32.Parse(hexValueFinal.Substring(2, 2), System.Globalization.NumberStyles.AllowHexSpecifier);

                                arrByte[21] = Convert.ToByte(i1);
                                arrByte[22] = Convert.ToByte(i2);
                            }
                            break;
                        case ("IV_OPENCIRCUITVOLTAGE"):
                            if (!string.IsNullOrEmpty(value))
                            {
                                voltage = Convert.ToDecimal(value) * 1000;
                                hexValue = Convert.ToInt32(voltage).ToString("X");
                                if (hexValue.Length < 4)
                                {
                                    hexValueint = hexValue.Length + (4 - hexValue.Length);
                                    hexValueFinal = Convert.ToInt32(voltage).ToString("X" + hexValueint.ToString());
                                }
                                else
                                {
                                    hexValueFinal = hexValue;
                                }

                                i1 = Int32.Parse(hexValueFinal.Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
                                i2 = Int32.Parse(hexValueFinal.Substring(2, 2), System.Globalization.NumberStyles.AllowHexSpecifier);

                                arrByte[23] = Convert.ToByte(i1);
                                arrByte[24] = Convert.ToByte(i2);
                            }
                            break;
                        case ("IV_SERIESRESISTANCE"):
                            if (!string.IsNullOrEmpty(value))
                            {
                                resistance = Convert.ToDecimal(value) * 1000;
                                hexValue = Convert.ToInt32(resistance).ToString("X");
                                if (hexValue.Length < 4)
                                {
                                    hexValueint = hexValue.Length + (4 - hexValue.Length);
                                    hexValueFinal = Convert.ToInt32(resistance).ToString("X" + hexValueint.ToString());
                                }
                                else
                                {
                                    hexValueFinal = hexValue;
                                }

                                i1 = Int32.Parse(hexValueFinal.Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
                                i2 = Int32.Parse(hexValueFinal.Substring(2, 2), System.Globalization.NumberStyles.AllowHexSpecifier);

                                arrByte[25] = Convert.ToByte(i1);
                                arrByte[26] = Convert.ToByte(i2);
                            }
                            break;
                        case ("IV_CELLSINSERIES"):
                            if (!string.IsNullOrEmpty(value))
                            {
                                IvCellsInSeries = Convert.ToInt32(value);
                                hexValue = Convert.ToInt32(IvCellsInSeries).ToString("X");
                                if (hexValue.Length < 2)
                                {
                                    hexValueint = hexValue.Length + (2 - hexValue.Length);
                                    hexValueFinal = Convert.ToInt32(IvCellsInSeries).ToString("X" + hexValueint.ToString());
                                }
                                else
                                {
                                    hexValueFinal = hexValue;
                                }

                                i1 = Int32.Parse(hexValueFinal.Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier);

                                arrByte[27] = Convert.ToByte(i1);
                            }
                            break;
                        case ("IV_THERMALVOLTAGE"):
                            if (!string.IsNullOrEmpty(value))
                            {
                                voltage = Convert.ToDecimal(value) * 1000;
                                hexValue = Convert.ToInt32(voltage).ToString("X");
                                if (hexValue.Length < 4)
                                {
                                    hexValueint = hexValue.Length + (4 - hexValue.Length);
                                    hexValueFinal = Convert.ToInt32(voltage).ToString("X" + hexValueint.ToString());
                                }
                                else
                                {
                                    hexValueFinal = hexValue;
                                }

                                i1 = Int32.Parse(hexValueFinal.Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
                                i2 = Int32.Parse(hexValueFinal.Substring(2, 2), System.Globalization.NumberStyles.AllowHexSpecifier);

                                arrByte[28] = Convert.ToByte(i1);
                                arrByte[29] = Convert.ToByte(i2);
                            }
                            break;

                        default:
                            byte[] bArrSerialNo = System.Text.ASCIIEncoding.UTF8.GetBytes(Itemdetails.CustomerUniqueID);

                            if (bArrSerialNo.Length > 20)
                            {
                                Array.Copy(bArrSerialNo, 0, arrByte, 30, 20);
                            }
                            else
                            {
                                Array.Copy(bArrSerialNo, 0, arrByte, 30, bArrSerialNo.Length);
                            }
                            break;

                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error:ItemClass:ConvertToByte:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                _log.Trace("ItemClass:ConvertToByte:Leaving");
            }
            return arrByte;
        }

        public List<KTItemDetails> GetItemForSkuID(int dataOwnerID, long SkuID, int NoOfItems, ItemType itemType)
        {
            List<KTItemDetails> lstitemdetails = new List<KTItemDetails>();

            try
            {
                _log.Trace("ItemClass:GetItemForSkuID:Entering");
                ItemMaster clsItem = new ItemMaster();
                clsItem.SKU_ID = SkuID;
                clsItem.DataOwnerID = dataOwnerID;
                if (itemType == ItemType.UnPrinted)
                {
                    clsItem.ItemType = 1;
                }
                if (itemType == ItemType.Printed)
                {
                    clsItem.ItemType = 2;
                }
                DataSet dtallItemMaster = clsItem.SelectAllForSKUID(NoOfItems);
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
                        string Status = "", CustomerUniqueID = "", ItemStatus = "", LocationName="";

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

        public int FillAllItemsHH()
        {
            int ItemsCount = 0;
            try
            {
                SKUMaster objsku = new SKUMaster();

                ItemsCount = objsku.SelectALLHHItemDetailsOnCount();
            }
            catch (Exception ex)
            {
                _log.Error("Error:ItemClass:FillAllItemsHH:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                _log.Trace("ItemClass:FillAllItemsHH:Leaving");
            }
            return ItemsCount;
        }

        public DataTable FillAllItemsbyBlockHH(int Count, int Block)
        {
            DataTable dtitems = new DataTable();
            try
            {
                SKUMaster objsku = new SKUMaster();
                objsku.GtinCount = Count;
                objsku.Block = Block;
                dtitems = objsku.SelectALLbyBlockHHItemDetailsOnCount();
            }
            catch (Exception ex)
            {
                _log.Error("Error:ItemClass:FillAllItemsbyBlockHH:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                _log.Trace("ItemClass:FillAllItemsbyBlockHH:Leaving");
            }
            return dtitems;
        }
    }
}
