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
using System.Web.Configuration;

namespace KTone.Core.SDCBusinessLogic
{
    public class HandheldClass
    {
        NLog.Logger _log = NLog.LogManager.GetCurrentClassLogger();

        public Dictionary<string, List<string>> GetSKUCount(List<string> lstRFTAGID)
        {
            string strlst = string.Empty;
            int lstcount;
            int j = 0;
            int i = 0;

            Dictionary<string, List<string>> objDictionarySKUCount = new Dictionary<string, List<string>>();
            try
            {
                _log.Trace("HandheldClass:GetSKUCount:Entering");

                lstcount = lstRFTAGID.Count();
                StringBuilder strRFTAG = new StringBuilder();
                for (i = 0; i < lstcount; i++)
                {
                    strRFTAG.Append(", ").Append(lstRFTAGID[i]);
                    j++;

                    if ((j == 100) || (i >= lstcount - 1))
                    {
                        ItemMaster objItem = new ItemMaster();
                        objItem.RFTagIDURN = strRFTAG.ToString();
                        DataTable dtCount = objItem.GetSKUCountforRFTAGID();


                        if (dtCount != null && dtCount.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtCount.Rows)
                            {

                                if (objDictionarySKUCount.ContainsKey(dr["PRODUCTSKU"].ToString()))
                                {
                                    objDictionarySKUCount[dr["PRODUCTSKU"].ToString()].Add(dr["CUSTOMERUNIQUEID"].ToString());
                                }
                                else
                                {
                                    objDictionarySKUCount.Add(dr["PRODUCTSKU"].ToString(), new List<string>());
                                    objDictionarySKUCount[dr["PRODUCTSKU"].ToString()].Add(dr["CUSTOMERUNIQUEID"].ToString());
                                }
                            }
                        }
                        j = 0;
                        strRFTAG.Length = 0;
                    }
                }
                _log.Trace("HandheldClass:GetSKUCount:Leaving");
            }

            catch (Exception ex)
            {
                _log.Error("Error:HandheldClass:GetSKUCount:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return objDictionarySKUCount;
        }

        public bool UpdateLocation(List<string> RFTAGID, int LocationId, DateTime TimeStamp)
        {
            string strlst = string.Empty;
            int lstcount;
            int j = 0;
            int i = 0;
            bool updateID = false;
            DateTime timestamp = DateTime.MinValue;
            bool Timestampval = Convert.ToBoolean(WebConfigurationManager.AppSettings["HandHeldTimestamp"].ToString());

            try
            {
                _log.Trace("HandheldClass:UpdateLocation:Entering");


                if (Timestampval)
                {
                    timestamp = TimeStamp;
                    _log.Trace("HandheldClass:UpdateLocation:Using HandHeld Timestamp");

                }
                else
                {
                    timestamp = DateTime.Now;
                    _log.Trace("HandheldClass:UpdateLocation:Using System Timestamp");
                }


                lstcount = RFTAGID.Count();
                StringBuilder strRFTAG = new StringBuilder();
                for (i = 0; i < lstcount; i++)
                {
                    strRFTAG.Append(", ").Append(RFTAGID[i]);
                    j++;

                    if ((j == 100) || (i >= lstcount - 1))
                    {
                        ItemMaster objItem = new ItemMaster();
                        objItem.RFTagIDURN = strRFTAG.ToString();
                        objItem.LastSeenLocation = LocationId;
                        objItem.LastSeenTime = Convert.ToDateTime(timestamp);
                        updateID = objItem.UpdateItemDetailsForRFTAGID();

                        j = 0;
                        strRFTAG.Length = 0;
                    }
                }
                _log.Trace("HandheldClass:UpdateLocation:Leaving");
            }
            catch (Exception ex)
            {
                _log.Error("Error:HandheldClass:UpdateLocation:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return updateID;
        }


        public bool ItemsPutAway(List<string> RFTAGID, int LocationId, DateTime TimeStamp, int DataOwnerID , int UserID , int HHID)
        {
            string strlst = string.Empty;
            int lstcount;
            int j = 0;
            int i = 0;
            bool updateID = false;
            DateTime timestamp = DateTime.MinValue;
            bool Timestampval = Convert.ToBoolean(WebConfigurationManager.AppSettings["HandHeldTimestamp"].ToString());

            try
            {
                _log.Trace("HandheldClass:ItemsPutAway:Entering");


                if (Timestampval)
                {
                    timestamp = TimeStamp;
                    _log.Trace("HandheldClass:ItemsPutAway:Using HandHeld Timestamp");

                }
                else
                {
                    timestamp = DateTime.Now;
                    _log.Trace("HandheldClass:ItemsPutAway:Using System Timestamp");
                }


                lstcount = RFTAGID.Count();
                StringBuilder strRFTAG = new StringBuilder();
                for (i = 0; i < lstcount; i++)
                {
                    strRFTAG.Append(", ").Append(RFTAGID[i]);
                    j++;

                    if ((j == 100) || (i >= lstcount - 1))
                    {
                        ItemMaster objItem = new ItemMaster();
                        objItem.RFTagIDURN = strRFTAG.ToString();
                        objItem.LastSeenLocation = LocationId;
                        objItem.LastSeenTime = Convert.ToDateTime(timestamp);
                        objItem.CrupUser = UserID;
                        objItem.Status = "PUT_AWAY";
                        objItem.DataOwnerID = DataOwnerID;                        
                        updateID = objItem.ItemsPutAwayForRFTAGID(HHID);

                        j = 0;
                        strRFTAG.Length = 0;
                    }
                }
                _log.Trace("HandheldClass:ItemsPutAway:Leaving");
            }
            catch (Exception ex)
            {
                _log.Error("Error:HandheldClass:ItemsPutAway:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return updateID;
        }



        public KTAuditInstance[] GetAuditInstances(string userID, string password, int DataOwnerID)
        {
            List<KTAuditInstance> objAuditInstance = new List<KTAuditInstance>();
            DataTable dtCycleCountInstance = null;


            try
            {
                _log.Trace("HandheldClass:GetAuditInstances:Entering");

                CycleCountInstance objCycleCountInstances = new CycleCountInstance();
                objCycleCountInstances.DataOwnerID = DataOwnerID;
                dtCycleCountInstance = objCycleCountInstances.SelectActiveInstances();

                if (dtCycleCountInstance != null && dtCycleCountInstance.Rows.Count > 0)
                {
                    foreach (DataRow cycleCount in dtCycleCountInstance.Rows)
                    {
                        KTAuditInstance _auditInstance = new KTAuditInstance();
                        _auditInstance.InstanceID = Convert.ToInt64(cycleCount["CCInstanceID"]);
                        _auditInstance.AuditName = Convert.ToString(cycleCount["Name"]);
                        objAuditInstance.Add(_auditInstance);
                    }
                }

                _log.Trace("HandheldClass:GetAuditInstances:Leaving");
            }
            catch (Exception ex)
            {
                _log.Error("Error:HandheldClass:GetAuditInstances:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return objAuditInstance.ToArray();
        }

        public KTAuditInstanceDetails GetAuditInstanceDetails(long CCInstanceID, int DataOwnerID)
        {
            KTAuditInstanceDetails objAuditInstanceDetails = null;
            DataTable dtInstanceDetails = null;


            try
            {
                _log.Trace("HandheldClass:GetAuditInstanceDetails:Entering");


                CycleCountInstanceDetail objCycleCountInstancesDetails = new CycleCountInstanceDetail();

                objCycleCountInstancesDetails.CCInstanceID = Convert.ToInt32(CCInstanceID);
                objCycleCountInstancesDetails.DataOwnerID = DataOwnerID;
                dtInstanceDetails = objCycleCountInstancesDetails.SelectOne();

                if (dtInstanceDetails != null && dtInstanceDetails.Rows.Count > 0)
                {
                    foreach (DataRow cycleCount in dtInstanceDetails.Rows)
                    {
                        objAuditInstanceDetails = new KTAuditInstanceDetails();
                        objAuditInstanceDetails.InstanceID = Convert.ToInt64(cycleCount["CCInstanceID"]);
                        objAuditInstanceDetails.AuditName = Convert.ToString(cycleCount["Name"]);
                        objAuditInstanceDetails.TotalItem = Convert.ToInt64(cycleCount["TotalGTINS"]);
                        objAuditInstanceDetails.FoundItem = Convert.ToInt64(cycleCount["FoundGTINS"]);

                    }
                }

                _log.Trace("HandheldClass:GetAuditInstanceDetails:Leaving");
            }
            catch (Exception ex)
            {
                _log.Error("Error:HandheldClass:GetAuditInstanceDetails:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return objAuditInstanceDetails;
        }


        public bool UpdateCycleCountInstanceDetails(List<string> RFTAGIDs, int LocationID, long CCInstanceID, int HHID, DateTime TimeStamp, int DataOwnerID ,int UserID)
        {
            string strlst = string.Empty;
            int lstcount;
            int j = 0;
            int i = 0;
            bool result = false;

            try
            {
                _log.Trace("HandheldClass:UpdateCycleCountInstanceDetails:Entering");


                lstcount = RFTAGIDs.Count();
                StringBuilder strRFTAG = new StringBuilder();

                foreach (string TagId in RFTAGIDs)
                {
                    strRFTAG.Append(TagId).Append(",");
                }

                //strlst = strRFTAG.ToString();
                //strlst = strlst.Remove(strlst.Length - 1,1);

                strRFTAG = strRFTAG.Remove(strRFTAG.Length - 1, 1);

                CycleCountInstanceDetail objCCInstanceDetails = new CycleCountInstanceDetail();
                objCCInstanceDetails.DataOwnerID = DataOwnerID;
                objCCInstanceDetails.HHID = HHID;
                objCCInstanceDetails.UpdatedBy = UserID;
                objCCInstanceDetails.CCInstanceID = CCInstanceID;
                objCCInstanceDetails.LocationId = LocationID;
                objCCInstanceDetails.LastSeenTimeStamp = TimeStamp;

                result = objCCInstanceDetails.UpdateCcycleCountInstanceForRFTAGID(strRFTAG.ToString());

                _log.Trace("HandheldClass:UpdateCycleCountInstanceDetails:Leaving");
            }
            catch (Exception ex)
            {
                _log.Error("Error:HandheldClass:UpdateCycleCountInstanceDetails:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return result;
        }


        public bool GateOperationOnItems(List<string> IDs, int LocationID, ItemState ItemStatus, DateTime TimeStamp, int DataOwnerID)
        {
            bool result = false;

            try
            {
                _log.Trace("HandheldClass:GateOperationOnItems:Entering");

                string _IDs = string.Empty;

                foreach (string id in IDs)
                {
                    _IDs = _IDs + "," + id;
                }

                _IDs = _IDs.Remove(0, 1);

                ItemMaster objItemMaster = new ItemMaster();
                objItemMaster.DataOwnerID = DataOwnerID;
                objItemMaster.LastSeenLocation = LocationID;
                objItemMaster.LastSeenTime = TimeStamp;
                objItemMaster.ItemStatus = Convert.ToString(ItemStatus);


                result = objItemMaster.UpdateItemDetails_GateOperation(_IDs);



                _log.Trace("HandheldClass:GateOperationOnItems:Leaving");
            }
            catch (Exception ex)
            {
                _log.Error("Error:HandheldClass:GateOperationOnItems:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }

            return result;
        }

        public void GetLookupSetting(out string Settings)
        {
            Settings = string.Empty;
            try
            {
                _log.Trace("HandHeldClass:GetLookupSetting:Entering");

                ItemMaster objItem = new ItemMaster();
                DataTable dtItem = objItem.GetLookup();
                if (dtItem != null && dtItem.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtItem.Rows)
                    {
                        if ((dr["Name"].ToString()).ToUpper().Trim() == "SETTINGS")
                        {
                            Settings = dr["Value"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error:HandHeldClass:GetLookupSetting:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                _log.Trace("HandHeldClass:GetLookupSetting:Leaving");
            }
        }

        public bool UpdateLookup(string Name, string Data)
        {
            bool result = false;

            try
            {
                _log.Trace("HandHeldClass:UpdateLookup:Entering");
                ItemMaster objItem = new ItemMaster();
                result = objItem.UpdateLookup(Name, Data);

            }
            catch (Exception ex)
            {
                _log.Error("Error:HandHeldClass:UpdateLookup:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                _log.Trace("HandHeldClass:UpdateLookup:Leaving");
            }

            return result;
        }


        public List<KTLocationItemCount> GetItemCountForLocation(string ProductName , int DataOwnerID)
        {
            DataSet dtItemLoc = null;
            // DataTable dtItemToLocCount = null;
            List<KTLocationItemCount> lstLocationItemCount = null;
            try
            {
                _log.Trace("HandHeldClass:GetItemCountForLocation:Entering");

                string LocationName;
                int ItemQuantity;
                bool IsAssociated;

                ItemMaster objItem = new ItemMaster();
                objItem.DataOwnerID = DataOwnerID;

                _log.Trace("HandHeldClass:GetItemCountForLocation:DataOwnerID" + DataOwnerID);

                _log.Trace("HandHeldClass:GetItemCountForLocation:ProductName" + ProductName);

                dtItemLoc = objItem.SelectItemCountForLocationOnProduct(ProductName);

                if (dtItemLoc != null && dtItemLoc.Tables.Count > 0)
                {
                    _log.Trace("HandHeldClass:GetItemCountForLocation:TableCount" + dtItemLoc.Tables.Count);

                    if (dtItemLoc.Tables[0] != null && dtItemLoc.Tables[0].Rows.Count > 0)
                    {
                       

                        _log.Trace("HandHeldClass:GetItemCountForLocation:BeforeLoop");


                        lstLocationItemCount = new List<KTLocationItemCount>();
                        foreach (DataRow dr in dtItemLoc.Tables[0].Rows)
                        {
                            LocationName = Convert.ToString(dr["LocationName"]);
                            ItemQuantity = Convert.ToInt32(dr["ITEMQUANTITY"]);
                            //DataOwnerId = Convert.ToInt32(dr["DataOwnerID"]);
                            IsAssociated = Convert.ToBoolean(dr["ISASSOCIATED"]);

                            KTLocationItemCount _locationItemCount = new KTLocationItemCount(LocationName, ItemQuantity, IsAssociated);

                            lstLocationItemCount.Add(_locationItemCount);
                        }


                        _log.Trace("HandHeldClass:GetItemCountForLocation:After Loop");
                        // dtItemToLocCount = dtItemLoc.Tables[0];
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error:HandHeldClass:GetItemCountForLocation:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                _log.Trace("HandHeldClass:GetItemCountForLocation:Leaving");
            }

            // return dtItemToLocCount;
            return lstLocationItemCount;
        }

        public string ServerDateTime()
        {
            return DateTime.Now.ToString();
        }


        public KTItemDetails GetItemDetailsForCustomerID_Philips(string CustomerUniqueID, int dataOwnerID)
        {

            long ItemID = 0;

            KTItemDetails objitem = null;
            try
            {
                _log.Trace("ItemClass:GetItemDetailsForCustomerID:Entering");
             
                 _log.Trace("ItemClass:GetItemDetailsForCustomerID:sdcCache  null");
                  objitem = FillItemDetails(CustomerUniqueID, dataOwnerID);
               
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




        public KTItemDetails FillItemDetails(string id, int dataOwnerID)
        {
            KTItemDetails itemdetails = null;
            try
            {
                _log.Trace("ItemClass:ItemDetails:Entering");
                DataSet dtItem = null;
                ItemMaster clsItem = new ItemMaster();

                clsItem.CustomerUniqueID = id;
                clsItem.DataOwnerID = dataOwnerID;
                dtItem = clsItem.SelectOneSRNO_HH();

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
                    if (dItemRow["LastSeenTime"] != null && dItemRow["LastSeenTime"].ToString() != string.Empty)
                        LastSeenTime = DateTime.Parse(dItemRow["LastSeenTime"].ToString());
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
                    itemdetails.LocationName = LocationName;
                    itemdetails.ProductSKU = ProductSKU;
                    itemdetails.ItemStatus = ItemStatus;
                    itemdetails.IsActive = IsActive;
                    itemdetails.Comments = Comments;
                    itemdetails.TagDetails = tagDetails;
                   // itemdetails.TagDetails = tagDetails;

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

    }
}
