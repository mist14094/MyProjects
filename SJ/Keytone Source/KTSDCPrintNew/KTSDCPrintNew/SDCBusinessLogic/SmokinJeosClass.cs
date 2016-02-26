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
using TrackerRetailDataAccess;
using System.Drawing;


namespace KTone.Core.SDCBusinessLogic
{
    public class SmokinJeosClass
    {
        NLog.Logger _log = NLog.LogManager.GetCurrentClassLogger();

        public List<Stores> GetAllStores()
        {
            DataTable dtStores = new DataTable();
            List<Stores> lstStores = null;
            string storeName = string.Empty;
            int storeid;
            try
            {
                _log.Trace("SmokinJeosClass:GetAllStores Entering");
                StoresMaster stMaster = new StoresMaster();
                dtStores = stMaster.GetAllStores();
                if (dtStores != null && dtStores.Rows.Count > 0)
                {
                    lstStores = new List<Stores>();
                    foreach (DataRow drStore in dtStores.Rows)
                    {
                        storeid = Convert.ToInt32(drStore["StoreID"].ToString());
                        storeName = drStore["KT_StoreName"].ToString();
                        Stores store = new Stores(storeid, storeName);
                        lstStores.Add(store);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error:SmokinJeosClass:GetAllStores:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("SmokinJeosClass:GetAllStores Leaving");
            }
            return lstStores;
        }

        public List<Devices> GetAllDevices()
        {
            DataTable dtDevices = new DataTable();
            List<Devices> lstDevices = null;
            string readerName = string.Empty;
            string readerlocation = string.Empty;
            int deviceId, storeId;
            try
            {
                _log.Trace("SmokinJeosClass:GetAllDevices Entering");
                StoresMaster stMaster = new StoresMaster();
                dtDevices = stMaster.GetAllDevices();


                if (dtDevices != null && dtDevices.Rows.Count > 0)
                {
                    DataRow[] drdevices = dtDevices.Select("ReaderType = 'Mobile'");
                    if (drdevices != null && drdevices.Length > 0)
                    {
                        lstDevices = new List<Devices>();
                        foreach (DataRow drd in drdevices)
                        {
                            readerName = drd["ReaderName"].ToString();
                            readerlocation = drd["ReaderLocation"].ToString();
                            deviceId = Convert.ToInt32(drd["DeviceID"].ToString());
                            storeId = Convert.ToInt32(drd["StoreID"].ToString());
                            Devices Dev = new Devices(deviceId, storeId, readerlocation, readerName);
                            lstDevices.Add(Dev);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error:SmokinJeosClass:GetAllDevices:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("SmokinJeosClass:GetAllDevices Leaving");
            }
            return lstDevices;
        }

        public List<KTCategoryDetails> GetRFIDDetails_OnRFIDs(List<string> RFTagIDs)
        {
            DataTable dtCategories = new DataTable();
            List<KTCategoryDetails> lstCategories = null;
            try
            {
                _log.Trace("SmokinJeosClass:GetRFIDDetails_OnRFIDs Entering");
                Products products = new Products();
                string strRfTagIds = string.Join(",", new List<string>(RFTagIDs).ToArray());
                dtCategories = products.GetRFIDDetails_OnRFIDs(strRfTagIds);


                if (dtCategories != null && dtCategories.Rows.Count > 0)
                {
                    lstCategories = new List<KTCategoryDetails>();
                    foreach (DataRow dr in dtCategories.Rows)
                    {
                        KTCategoryDetails category = new KTCategoryDetails();
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["PRODUCTITEMID"])))
                        {
                            category.ProductItemID = Convert.ToInt64(dr["PRODUCTITEMID"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["LastSeenTime"])))
                        {
                            category.LastSeenTime = Convert.ToDateTime(dr["LastSeenTime"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["Status"])))
                        {
                            category.Status = Convert.ToString(dr["Status"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["RFID"])))
                        {
                            category.RFID = Convert.ToString(dr["RFID"]);
                        }
                        lstCategories.Add(category);
                    }
                }


            }
            catch (Exception ex)
            {
                _log.Error("Error:SmokinJeosClass:GetRFIDDetails_OnRFIDs:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("SmokinJeosClass:GetRFIDDetails_OnRFIDs Leaving");
            }
            return lstCategories;
        }

        public List<KTCategoryDetails> GetProducts_OnRFID_CICOAdhoc(List<string> RFTagIDs, int storeID, string OperationType)
        {
            DataTable dtCategories = new DataTable();
            List<KTCategoryDetails> lstCategories = null;
            try
            {
                _log.Trace("SmokinJeosClass:GetProducts_OnRFID_CICOAdhoc Entering");
                Products products = new Products();
                string strRfTagIds = string.Join(",", new List<string>(RFTagIDs).ToArray());
                dtCategories = products.GetProducts_OnRFID_CICOAdhoc(strRfTagIds, storeID, OperationType);


                if (dtCategories != null && dtCategories.Rows.Count > 0)
                {
                    lstCategories = new List<KTCategoryDetails>();
                    foreach (DataRow dr in dtCategories.Rows)
                    {
                        List<string> RFIDTags = new List<string>();
                        List<string> ProductItemIDs = new List<string>();
                        KTCategoryDetails category = new KTCategoryDetails();
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["UPC"])))
                        {
                            category.UPC = Convert.ToString(dr["UPC"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["SKU"])))
                        {
                            category.SKU = Convert.ToString(dr["SKU"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["STOREID"])))
                        {
                            category.StoreID = Convert.ToInt32(dr["STOREID"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["ITEMDESCRIPTION"])))
                        {
                            category.ItemDescription = Convert.ToString(dr["ITEMDESCRIPTION"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["STYLECODE"])))
                        {
                            category.StyleCode = Convert.ToString(dr["STYLECODE"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["SIZECODE"])))
                        {
                            category.SizeCode = Convert.ToString(dr["SIZECODE"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["CNT"])))
                        {
                            category.Count = Convert.ToInt32(dr["CNT"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["Category"])))
                        {
                            category.Category = Convert.ToString(dr["Category"]);
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["DecCount"])))
                        {
                            category.DecommissionCnt = Convert.ToInt32(dr["DecCount"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["RFTAGIDs"])))
                        {
                            RFIDTags.Add(Convert.ToString(dr["RFTAGIDs"]));

                            category.RFIDS = RFIDTags;
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["ProductItemIDs"])))
                        {
                            ProductItemIDs.Add(Convert.ToString(dr["ProductItemIDs"]));

                            category.PRODUCTITEMIDS = ProductItemIDs;
                        }
                        lstCategories.Add(category);
                    }
                }


            }
            catch (Exception ex)
            {
                _log.Error("Error:SmokinJeosClass:GetProducts_OnRFID_CICOAdhoc:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("SmokinJeosClass:GetProducts_OnRFID_CheckIN Leaving");
            }
            return lstCategories;
        }


        public List<KTCategoryDetails> GetAllCategoriesForRFTagID_AdHoc(List<string> RFTagIDs, int storeID)
        {
            DataTable dtCategories = new DataTable();
            List<KTCategoryDetails> lstCategories = null;
            try
            {
                _log.Trace("SmokinJeosClass:GetAllCategoriesForRFTagID_AdHoc Entering");
                Products products = new Products();
                string strRfTagIds = string.Join(",", new List<string>(RFTagIDs).ToArray());
                dtCategories = products.GetCategoryForRFID_AdHoc(strRfTagIds, storeID);


                if (dtCategories != null && dtCategories.Rows.Count > 0)
                {
                    lstCategories = new List<KTCategoryDetails>();
                    foreach (DataRow dr in dtCategories.Rows)
                    {
                        List<string> RFIDTags = new List<string>();
                        KTCategoryDetails category = new KTCategoryDetails();
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["UPC"])))
                        {
                            category.UPC = Convert.ToString(dr["UPC"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["SKU"])))
                        {
                            category.SKU = Convert.ToString(dr["SKU"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["STOREID"])))
                        {
                            category.StoreID = Convert.ToInt32(dr["STOREID"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["ITEMDESCRIPTION"])))
                        {
                            category.ItemDescription = Convert.ToString(dr["ITEMDESCRIPTION"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["STYLECODE"])))
                        {
                            category.StyleCode = Convert.ToString(dr["STYLECODE"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["SIZECODE"])))
                        {
                            category.SizeCode = Convert.ToString(dr["SIZECODE"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["CNT"])))
                        {
                            category.Count = Convert.ToInt32(dr["CNT"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["Category"])))
                        {
                            category.Category = Convert.ToString(dr["Category"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["RFTAGIDs"])))
                        {
                            RFIDTags.Add(Convert.ToString(dr["RFTAGIDs"]));

                            category.RFIDS = RFIDTags;
                        }
                        lstCategories.Add(category);
                    }
                }


            }
            catch (Exception ex)
            {
                _log.Error("Error:SmokinJeosClass:GetAllCategoriesForRFTagID_AdHoc:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("SmokinJeosClass:GetAllCategoriesForRFTagID_AdHoc Leaving");
            }
            return lstCategories;
        }


        public List<KTCategoryDetails> GetAllCategoriesForRFTagID_Replenishment(List<string> RFTagIDs, int storeID, long RRID)
        {
            DataTable dtCategories = new DataTable();
            List<KTCategoryDetails> lstCategories = null;
            try
            {
                _log.Trace("SmokinJeosClass:GetAllCategoriesForRFTagID_Replenishment Entering");
                Products products = new Products();
                string strRfTagIds = string.Join(",", new List<string>(RFTagIDs).ToArray());
                dtCategories = products.GetCategoryForRFID_Replenishment(strRfTagIds, storeID, RRID);
                if (dtCategories != null && dtCategories.Rows.Count > 0)
                {
                    lstCategories = new List<KTCategoryDetails>();
                    foreach (DataRow dr in dtCategories.Rows)
                    {
                        KTCategoryDetails category = new KTCategoryDetails();
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["UPC"])))
                        {
                            category.UPC = Convert.ToString(dr["UPC"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["SKU"])))
                        {
                            category.SKU = Convert.ToString(dr["SKU"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["STOREID"])))
                        {
                            category.StoreID = Convert.ToInt32(dr["STOREID"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["ITEMDESCRIPTION"])))
                        {
                            category.ItemDescription = Convert.ToString(dr["ITEMDESCRIPTION"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["STYLECODE"])))
                        {
                            category.StyleCode = Convert.ToString(dr["STYLECODE"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["SIZECODE"])))
                        {
                            category.SizeCode = Convert.ToString(dr["SIZECODE"]);
                        }

                        if (dtCategories.Columns.Contains("ScannedQuantity") && !string.IsNullOrEmpty(Convert.ToString(dr["ScannedQuantity"])))
                        {
                            category.ScannedQuantity = Convert.ToInt32(dr["ScannedQuantity"]);
                        }

                        if (dtCategories.Columns.Contains("OrderedQuantity") && !string.IsNullOrEmpty(Convert.ToString(dr["OrderedQuantity"])))
                        {
                            category.OrderedQuantity = Convert.ToInt32(dr["OrderedQuantity"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["Category"])))
                        {
                            category.Category = Convert.ToString(dr["Category"]);
                        }


                        if (!string.IsNullOrEmpty(Convert.ToString(dr["RFTAGIDs"])))
                        {
                            string strTags = Convert.ToString(dr["RFTAGIDs"]);
                            string[] tags = strTags.Split(',');
                            category.RFIDS = tags.ToList<string>();
                        }

                        lstCategories.Add(category);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error:SmokinJeosClass:GetAllCategoriesForRFTagID_Replenishment:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("SmokinJeosClass:GetAllCategoriesForRFTagID_Replenishment Leaving");
            }
            return lstCategories;
        }

        public List<KTCategoryDetails> GetAllCategoriesForRFTagID_Replenishment_OnRFID(List<string> RFTagIDs, int storeID, long RRID)
        {
            DataTable dtCategories = new DataTable();
            List<KTCategoryDetails> lstCategories = null;
            try
            {
                _log.Trace("SmokinJeosClass:GetAllCategoriesForRFTagID_Replenishment_OnRFID Entering");
                Products products = new Products();
                string strRfTagIds = string.Join(",", new List<string>(RFTagIDs).ToArray());
                // dtCategories = products.GetCategoryForRFID_Replenishment(strRfTagIds, storeID, RRID);
                dtCategories = products.GetCategoryForRFID_Replenishment_OnRFID(strRfTagIds, storeID, RRID);
                if (dtCategories != null && dtCategories.Rows.Count > 0)
                {
                    lstCategories = new List<KTCategoryDetails>();
                    foreach (DataRow dr in dtCategories.Rows)
                    {
                        List<string> ProductItemIDs = new List<string>();
                        KTCategoryDetails category = new KTCategoryDetails();
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["UPC"])))
                        {
                            category.UPC = Convert.ToString(dr["UPC"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["SKU"])))
                        {
                            category.SKU = Convert.ToString(dr["SKU"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["STOREID"])))
                        {
                            category.StoreID = Convert.ToInt32(dr["STOREID"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["ITEMDESCRIPTION"])))
                        {
                            category.ItemDescription = Convert.ToString(dr["ITEMDESCRIPTION"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["STYLECODE"])))
                        {
                            category.StyleCode = Convert.ToString(dr["STYLECODE"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["SIZECODE"])))
                        {
                            category.SizeCode = Convert.ToString(dr["SIZECODE"]);
                        }

                        if (dtCategories.Columns.Contains("ScannedQuantity") && !string.IsNullOrEmpty(Convert.ToString(dr["ScannedQuantity"])))
                        {
                            category.ScannedQuantity = Convert.ToInt32(dr["ScannedQuantity"]);
                        }

                        if (dtCategories.Columns.Contains("OrderedQuantity") && !string.IsNullOrEmpty(Convert.ToString(dr["OrderedQuantity"])))
                        {
                            category.OrderedQuantity = Convert.ToInt32(dr["OrderedQuantity"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["Category"])))
                        {
                            category.Category = Convert.ToString(dr["Category"]);
                        }


                        if (!string.IsNullOrEmpty(Convert.ToString(dr["RFTAGIDs"])))
                        {
                            string strTags = Convert.ToString(dr["RFTAGIDs"]);
                            string[] tags = strTags.Split(',');
                            category.RFIDS = tags.ToList<string>();
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["ProductItemIDs"])))
                        {
                            ProductItemIDs.Add(Convert.ToString(dr["ProductItemIDs"]));

                            category.PRODUCTITEMIDS = ProductItemIDs;
                        }

                        lstCategories.Add(category);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error:SmokinJeosClass:GetAllCategoriesForRFTagID_Replenishment_OnRFID:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("SmokinJeosClass:GetAllCategoriesForRFTagID_Replenishment_OnRFID Leaving");
            }
            return lstCategories;
        }

        public List<KTReplensihmentRequest> GetAllReplenishmentsForStore(int storeID)
        {
            DataTable dtReplenishmnets = new DataTable();
            List<KTReplensihmentRequest> lstReplenishments = null;
            try
            {
                _log.Trace("SmokinJeosClass:GetAllReplenishmentsForStore Entering");
                KT_ReplensihmentRequest repreq = new KT_ReplensihmentRequest();
                repreq.FromLocation = storeID;
                dtReplenishmnets = repreq.GetAllReplenishmentsRequest();

                if (dtReplenishmnets != null && dtReplenishmnets.Rows.Count > 0)
                {
                    lstReplenishments = new List<KTReplensihmentRequest>();
                    foreach (DataRow dr in dtReplenishmnets.Rows)
                    {
                        KTReplensihmentRequest RepRequest = new KTReplensihmentRequest();
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["RR_ID"])))
                        {
                            RepRequest.RR_ID = Convert.ToInt32(dr["RR_ID"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["RR_Number"])))
                        {
                            RepRequest.RR_Number = Convert.ToString(dr["RR_Number"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["FromLocation"])))
                        {
                            RepRequest.FromLocation = Convert.ToInt32(dr["FromLocation"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["ToLocation"])))
                        {
                            RepRequest.ToLocation = Convert.ToInt32(dr["ToLocation"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["RR_Status"])))
                        {
                            RepRequest.RR_Status = Convert.ToString(dr["RR_Status"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["GenerationTime"])))
                        {
                            RepRequest.GenerationTime = Convert.ToDateTime(dr["GenerationTime"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["FulfillmentDate"])))
                        {
                            RepRequest.FulfillmentDate = Convert.ToDateTime(dr["FulfillmentDate"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["CreatedBy"])))
                        {
                            RepRequest.CreatedBy = Convert.ToInt32(dr["CreatedBy"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["CreatedDate"])))
                        {
                            RepRequest.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["UpdatedBy"])))
                        {
                            RepRequest.UpdatedBy = Convert.ToInt32(dr["UpdatedBy"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["UpdateDate"])))
                        {
                            RepRequest.UpdateDate = Convert.ToDateTime(dr["UpdateDate"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["Comments"])))
                        {
                            RepRequest.Comments = Convert.ToString(dr["Comments"]);
                        }

                        lstReplenishments.Add(RepRequest);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error:SmokinJeosClass:GetAllReplenishmentsForStore:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("SmokinJeosClass:GetAllReplenishmentsForStore Leaving");
            }
            return lstReplenishments;
        }

        public List<KTReplenishmentRequestDetails> GetReplenishmentDetailsForReplenishment(long rrid, int storeID)
        {
            DataTable dtReplenishmnets = new DataTable();
            List<KTReplenishmentRequestDetails> lstReplenishmentDetails = null;
            try
            {
                _log.Trace("SmokinJeosClass:GetReplenishmentDetailsForReplenishment Entering");
                KT_ReplensihmentRequestDetails repreq = new KT_ReplensihmentRequestDetails();
                dtReplenishmnets = repreq.GetCategoryReplenishment_BeforeScan(storeID, rrid);

                if (dtReplenishmnets != null && dtReplenishmnets.Rows.Count > 0)
                {
                    lstReplenishmentDetails = new List<KTReplenishmentRequestDetails>();
                    foreach (DataRow dr in dtReplenishmnets.Rows)
                    {
                        KTReplenishmentRequestDetails RepRequest = new KTReplenishmentRequestDetails();

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["RRD_ID"])))
                        {
                            RepRequest.RRD_ID = Convert.ToInt32(dr["RRD_ID"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["RR_ID"])))
                        {
                            RepRequest.RR_ID = Convert.ToInt32(dr["RR_ID"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["RRD_Status"])))
                        {
                            RepRequest.RRD_Status = Convert.ToString(dr["RRD_Status"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["GenerationTime"])))
                        {
                            RepRequest.GenerationTime = Convert.ToDateTime(dr["GenerationTime"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["UPC"])))
                        {
                            RepRequest.UPC = Convert.ToString(dr["UPC"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["SKU"])))
                        {
                            RepRequest.SKU = Convert.ToString(dr["SKU"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["StoreID"])))
                        {
                            RepRequest.StoreID = Convert.ToInt32(dr["StoreID"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["Category"])))
                        {
                            RepRequest.Category = Convert.ToString(dr["Category"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["ITEMDESCRIPTION"])))
                        {
                            RepRequest.ItemDescription = Convert.ToString(dr["ITEMDESCRIPTION"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["OrderedQuantity"])))
                        {
                            RepRequest.OrderedQty = Convert.ToInt32(dr["OrderedQuantity"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["ShippedQty"])))
                        {
                            RepRequest.ShippedQty = Convert.ToInt32(dr["ShippedQty"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["CreatedBy"])))
                        {
                            RepRequest.CreatedBy = Convert.ToInt32(dr["CreatedBy"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["CreatedDate"])))
                        {
                            RepRequest.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["UpdatedBy"])))
                        {
                            RepRequest.UpdatedBy = Convert.ToInt32(dr["UpdatedBy"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["UpdateDate"])))
                        {
                            RepRequest.UpdateDate = Convert.ToDateTime(dr["UpdateDate"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["Comments"])))
                        {
                            RepRequest.Comments = Convert.ToString(dr["Comments"]);
                        }

                        lstReplenishmentDetails.Add(RepRequest);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error:SmokinJeosClass:GetReplenishmentDetailsForReplenishment:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("SmokinJeosClass:GetReplenishmentDetailsForReplenishment Leaving");
            }
            return lstReplenishmentDetails;
        }

        public bool CheckOut(int fromLocation, int toLoacation, long RRID, string ReplenishmentRequest, int checkOutBy, List<string> RFIDs, bool isAdhoc, out string packagingID)
        {
            bool isSuccess = false;
            packagingID = string.Empty;
            try
            {
                _log.Trace("SmokinJeosClass:CheckOut Entering");
                KT_CICOMaster cicoMaster = new KT_CICOMaster();
                cicoMaster.SourceStoreID = fromLocation;
                cicoMaster.ShippedStoreId = toLoacation;
                if (!isAdhoc)
                {
                    cicoMaster.RR_ID = RRID;
                    cicoMaster.RR = ReplenishmentRequest;
                }

                cicoMaster.CreatedBy = checkOutBy;
                cicoMaster.ChekOutTime = DateTime.Now;
                string strRfTagIds = string.Join(",", new List<string>(RFIDs).ToArray());
                isSuccess = cicoMaster.CheckInReplenishment_Confirm(strRfTagIds, isAdhoc, out packagingID);
            }
            catch (Exception ex)
            {
                _log.Error("Error:SmokinJeosClass:CheckOut:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("SmokinJeosClass:CheckOut Leaving");
            }
            return isSuccess;
        }

        public bool CheckOut_OnPID(int fromLocation, int toLoacation, long RRID, string ReplenishmentRequest, int checkOutBy, List<string> ProductItemIDs, bool isAdhoc, string packagingID, out string UniquePackingId)
        {
            bool isSuccess = false;
            UniquePackingId = string.Empty;
            try
            {
                _log.Trace("SmokinJeosClass:CheckOut_OnPID Entering");
                KT_CICOMaster cicoMaster = new KT_CICOMaster();
                cicoMaster.SourceStoreID = fromLocation;
                cicoMaster.ShippedStoreId = toLoacation;
                if (!isAdhoc)
                {
                    cicoMaster.RR_ID = RRID;
                    cicoMaster.RR = ReplenishmentRequest;
                }

                cicoMaster.CreatedBy = checkOutBy;
                cicoMaster.ChekOutTime = DateTime.Now;
                //  string strRfTagIds = string.Join(",", new List<string>(RFIDs).ToArray());
                string stproductItemIds = string.Join(",", new List<string>(ProductItemIDs).ToArray());
                //isSuccess = cicoMaster.CheckInReplenishment_Confirm(strRfTagIds, isAdhoc, out packagingID);
                isSuccess = cicoMaster.CheckOut_OnPID_HH(isAdhoc, stproductItemIds, packagingID, out UniquePackingId);
            }
            catch (Exception ex)
            {
                _log.Error("Error:SmokinJeosClass:CheckOut_OnPID:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("SmokinJeosClass:CheckOut_OnPID Leaving");
            }
            return isSuccess;
        }


        public bool CheckIn(int toLoacation, string packageslip, int checkInBy, List<string> RFIDs, bool isAdhoc, string userName)
        {
            bool isSuccess = false;
            try
            {
                _log.Trace("SmokinJeosClass:CheckIn Entering");
                KT_CICOMaster cicoMaster = new KT_CICOMaster();
                cicoMaster.ShippedStoreId = toLoacation;
                cicoMaster.CreatedBy = checkInBy;
                cicoMaster.ChekOutTime = DateTime.Now;
                string strRfTagIds = string.Join(",", new List<string>(RFIDs).ToArray());
                if (isAdhoc)
                {
                    isSuccess = cicoMaster.CheckIn(strRfTagIds, string.Empty, true, userName);
                }
                else
                {
                    isSuccess = cicoMaster.CheckIn(strRfTagIds, packageslip, false, userName);
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error:SmokinJeosClass:CheckIn:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("SmokinJeosClass:CheckOut Leaving");
            }
            return isSuccess;
        }

        public bool CheckIn_OnPID(int toLoacation, string packageslip, int checkInBy, List<string> ProductItemIDs, bool isAdhoc, string userName, List<string> OverrideProductIDs, out string PackingIDonAdhoc)
        {
            bool isSuccess = false;
            try
            {
                _log.Trace("SmokinJeosClass:CheckIn_OnPID Entering");
                PackingIDonAdhoc = string.Empty;
                KT_CICOMaster cicoMaster = new KT_CICOMaster();
                cicoMaster.ShippedStoreId = toLoacation;
                cicoMaster.CreatedBy = checkInBy;
                cicoMaster.ChekOutTime = DateTime.Now;
                //   string strRfTagIds = string.Join(",", new List<string>(RFIDs).ToArray());
                string strproductItemIds = string.Join(",", new List<string>(ProductItemIDs).ToArray());
                string strOverridePIds = "";
                if (OverrideProductIDs != null)
                {
                    strOverridePIds = string.Join(",", new List<string>(OverrideProductIDs).ToArray());
                }

                if (isAdhoc)
                {
                    isSuccess = cicoMaster.CheckIn_OnPID_HH(packageslip, true, userName, strproductItemIds, strOverridePIds, out PackingIDonAdhoc);
                }
                else
                {
                    isSuccess = cicoMaster.CheckIn_OnPID_HH(packageslip, false, userName, strproductItemIds, strOverridePIds, out PackingIDonAdhoc);
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error:SmokinJeosClass:CheckIn_OnPID:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("SmokinJeosClass:CheckIn_OnPID Leaving");
            }
            return isSuccess;
        }


        public bool validatePackageSlip(string packagingID, int storeID)
        {
            bool isSuccess = false;
            try
            {
                _log.Trace("SmokinJeosClass:validatePackageSlip Entering");
                KT_CICOMaster cicoMaster = new KT_CICOMaster();
                cicoMaster.SourceStoreID = storeID;
                isSuccess = cicoMaster.ValidatePackaging(packagingID);
            }
            catch (Exception ex)
            {
                _log.Error("Error:SmokinJeosClass:validatePackageSlip:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("SmokinJeosClass:validatePackageSlip Leaving");
            }
            return isSuccess;
        }

        public List<KTCategoryDetails> GetCategoriesOnPackagingID(string packagingID)
        {
            DataTable dtCategories = new DataTable();
            List<KTCategoryDetails> lstCategories = null;
            try
            {
                _log.Trace("SmokinJeosClass:GetCategoriesOnPackagingID Entering");
                KT_CICOMaster cico = new KT_CICOMaster();
                cico.PackageSlip = packagingID;
                dtCategories = cico.GetCategoriesOnPackagingID();
                if (dtCategories != null && dtCategories.Rows.Count > 0)
                {
                    lstCategories = new List<KTCategoryDetails>();
                    foreach (DataRow dr in dtCategories.Rows)
                    {
                        KTCategoryDetails category = new KTCategoryDetails();
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["UPC"])))
                        {
                            category.UPC = Convert.ToString(dr["UPC"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["SKU"])))
                        {
                            category.SKU = Convert.ToString(dr["SKU"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["STOREID"])))
                        {
                            category.StoreID = Convert.ToInt32(dr["STOREID"]);
                        }

                        if (dtCategories.Columns.Contains("CNT") && !string.IsNullOrEmpty(Convert.ToString(dr["CNT"])))
                        {
                            category.Count = Convert.ToInt32(dr["CNT"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["Category"])))
                        {
                            category.Category = Convert.ToString(dr["Category"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["DESCRIPTION"])))
                        {
                            category.ItemDescription = Convert.ToString(dr["DESCRIPTION"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["RFIDS"])))
                        {
                            string rfids = dr["RFIDS"].ToString();
                            //rfids = rfids.Remove(0, 1);
                            string[] str = rfids.Split(',');
                            category.RFIDS = str.ToList<string>();
                        }

                        lstCategories.Add(category);
                    }
                }
                else
                {
                    throw new Exception("Checkin operation against this packing slip already done. ");
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error:SmokinJeosClass:GetCategoriesOnPackagingID:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("SmokinJeosClass:GetCategoriesOnPackagingID Leaving");
            }
            return lstCategories;
        }


        public List<KTCategoryDetails> GetCategoriesOnPackagingID_PID(string packagingID)
        {
            DataSet dsCategories = new DataSet();
            List<KTCategoryDetails> lstCategories = null;
            try
            {
                _log.Trace("SmokinJeosClass:GetCategoriesOnPackagingID_PID Entering");
                KT_CICOMaster cico = new KT_CICOMaster();
                cico.PackageSlip = packagingID;
                dsCategories = cico.GetCategoriesOnPackagingID_PID();
               
                if (dsCategories.Tables[0] != null && dsCategories.Tables[0].Rows.Count > 0)
                {
                    lstCategories = new List<KTCategoryDetails>();
                    foreach (DataRow dr in dsCategories.Tables[0].Rows)
                    {
                        List<string> RFIDs = new List<string>();
                        List<string> ProductItemIDs = new List<string>();
                        KTCategoryDetails category = new KTCategoryDetails();
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["UPC"])))
                        {
                            category.UPC = Convert.ToString(dr["UPC"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["SKU"])))
                        {
                            category.SKU = Convert.ToString(dr["SKU"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["STOREID"])))
                        {
                            category.StoreID = Convert.ToInt32(dr["STOREID"]);
                        }

                        if (dsCategories.Tables[0].Columns.Contains("CNT") && !string.IsNullOrEmpty(Convert.ToString(dr["CNT"])))
                        {
                            category.Count = Convert.ToInt32(dr["CNT"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["Category"])))
                        {
                            category.Category = Convert.ToString(dr["Category"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["DESCRIPTION"])))
                        {
                            category.ItemDescription = Convert.ToString(dr["DESCRIPTION"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["RFIDS"])))//Actually Expected RFIDs 
                        {
                            RFIDs.Add(Convert.ToString(dr["RFIDS"]));
                            category.RFIDS = RFIDs;
                            //string rfids = dr["RFIDS"].ToString();
                            //rfids = rfids.Remove(0, 1);
                            //string[] str = rfids.Split(',');
                            //category.RFIDS = str.ToList<string>();
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["ProductItemIDs"])))//Actually Expected ProductItemIDs 
                        {
                            ProductItemIDs.Add(Convert.ToString(dr["ProductItemIDs"]));
                            category.PRODUCTITEMIDS = ProductItemIDs;
                        }
                        if (dsCategories.Tables[1] != null && dsCategories.Tables[1].Rows.Count > 0)
                        {
                            foreach (DataRow row in dsCategories.Tables[1].Rows)
                            {
                                if (!string.IsNullOrEmpty(Convert.ToString(row["ActualCheckOutCnt"])))
                                {
                                    category.ActualCheckOutCnt = Convert.ToInt32(row["ActualCheckOutCnt"]);//Actually Expected CheckOUT Count 
                                }
                            }
                        }
                        lstCategories.Add(category);
                    }
                }
                else
                {
                    throw new Exception("Checkin operation against this packing slip already done. ");
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error:SmokinJeosClass:GetCategoriesOnPackagingID_PID:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("SmokinJeosClass:GetCategoriesOnPackagingID_PID Leaving");
            }
            return lstCategories;
        }

        public List<KTCategoryDetails> GetCategoriesOnPackagingID_OnRFID(string packagingID, List<string> RFTagIDs)
        {
            DataTable dtCategories = new DataTable();
            List<KTCategoryDetails> lstCategories = null;
            try
            {
                _log.Trace("SmokinJeosClass:GetCategoriesOnPackagingID_OnRFID Entering");
                KT_CICOMaster cico = new KT_CICOMaster();
                cico.PackageSlip = packagingID;
                string strRfTagIds = string.Join(",", new List<string>(RFTagIDs).ToArray());
                dtCategories = cico.GetCategoriesOnPackagingID_OnRFID(strRfTagIds);
                if (dtCategories != null && dtCategories.Rows.Count > 0)
                {
                    lstCategories = new List<KTCategoryDetails>();
                    foreach (DataRow dr in dtCategories.Rows)
                    {
                        List<string> RFIDs = new List<string>();
                        List<string> ProductItemIDs = new List<string>();
                        List<string> ExpRFIDTags = new List<string>();
                        List<string> ExpRFProductItemIDs = new List<string>();
                        KTCategoryDetails category = new KTCategoryDetails();
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["UPC"])))
                        {
                            category.UPC = Convert.ToString(dr["UPC"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["SKU"])))
                        {
                            category.SKU = Convert.ToString(dr["SKU"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["STOREID"])))
                        {
                            category.StoreID = Convert.ToInt32(dr["STOREID"]);
                        }

                        if (dtCategories.Columns.Contains("CNT") && !string.IsNullOrEmpty(Convert.ToString(dr["CNT"])))
                        {
                            category.Count = Convert.ToInt32(dr["CNT"]);
                        }

                        if (dtCategories.Columns.Contains("SCANCNT") && !string.IsNullOrEmpty(Convert.ToString(dr["SCANCNT"])))
                        {
                            category.ScanCnt = Convert.ToInt32(dr["SCANCNT"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["Category"])))
                        {
                            category.Category = Convert.ToString(dr["Category"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["DESCRIPTION"])))
                        {
                            category.ItemDescription = Convert.ToString(dr["DESCRIPTION"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["RFIDS"])))
                        {
                            //string rfids = dr["RFIDS"].ToString();
                            //rfids = rfids.Remove(0, 1);
                            //string[] str = rfids.Split(',');
                            //category.RFIDS = str.ToList<string>();
                            RFIDs.Add(Convert.ToString(dr["RFIDS"]));
                            category.RFIDS = RFIDs;
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["DecCount"])))
                        {
                            category.DecommissionCnt = Convert.ToInt32(dr["DecCount"]);
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["ProductItemIDs"])))
                        {
                            ProductItemIDs.Add(Convert.ToString(dr["ProductItemIDs"]));

                            category.PRODUCTITEMIDS = ProductItemIDs;
                        }


                        if (!string.IsNullOrEmpty(Convert.ToString(dr["ExpectedRFIDs"])))
                        {
                            ExpRFIDTags.Add(Convert.ToString(dr["ExpectedRFIDs"]));

                            category.ExpRFIDs = ExpRFIDTags;
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["ExpectedProductItemIDs"])))
                        {
                            ExpRFProductItemIDs.Add(Convert.ToString(dr["ExpectedProductItemIDs"]));

                            category.ExpPRODUCTITEMIDS = ExpRFProductItemIDs;
                        }

                        lstCategories.Add(category);
                    }
                }
                else
                {
                    throw new Exception("Checkin operation against this packing slip already done. ");
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error:SmokinJeosClass:GetCategoriesOnPackagingID_OnRFID:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("SmokinJeosClass:GetCategoriesOnPackagingID_OnRFID Leaving");
            }
            return lstCategories;
        }


        public Stores GetSourceStoreForPackageSlip(string packingSlip)
        {
            DataTable dtStores = new DataTable();
            Stores Store = null;
            string storeName = string.Empty;
            int storeid;
            try
            {
                _log.Trace("SmokinJeosClass:GetSourceStoreForPackageSlip Entering");
                KT_CICOMaster cico = new KT_CICOMaster();
                dtStores = cico.GetSourceStoreForPackageSlip(packingSlip);
                if (dtStores != null && dtStores.Rows.Count > 0)
                {
                    //Store = new Stores();
                    foreach (DataRow drStore in dtStores.Rows)
                    {
                        storeid = Convert.ToInt32(drStore["StoreID"].ToString());
                        storeName = drStore["KT_StoreName"].ToString();
                        Store = new Stores(storeid, storeName);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error:SmokinJeosClass:GetSourceStoreForPackageSlip:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("SmokinJeosClass:GetSourceStoreForPackageSlip Leaving");
            }
            return Store;
        }

        public bool UpdateProductsOnUPC(string UPC, int storeID, int SearchOnType)
        {
            bool isSuccess = false;

            try
            {
                _log.Trace("SmokinJeosClass:UpdateProductsOnUPC Entering");
                Products objproduct = new Products();
                objproduct.UPC = UPC;
                objproduct.StoreID = storeID;

                isSuccess = objproduct.UpdateProductsOnUPC(SearchOnType);
            }
            catch (Exception ex)
            {
                _log.Error("Error:SmokinJeosClass:UpdateProductsOnUPC:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("SmokinJeosClass:UpdateProductsOnUPC Leaving");
            }
            return isSuccess;
        }

        public List<KTCategoryDetails> GetProductdetailsForUPC(string UPC, int storeID)
        {
            DataTable dtCategories = new DataTable();
            List<KTCategoryDetails> KTCategories = null;
            try
            {
                _log.Trace("SmokinJeosClass:GetProductdetailsForUPC Entering");
                Products objProducts = new Products();
                objProducts.UPC = UPC;
                objProducts.StoreID = storeID;
                dtCategories = objProducts.GetProductdetailsForUPC();
                if (dtCategories != null && dtCategories.Rows.Count > 0)
                {
                    KTCategories = new List<KTCategoryDetails>();
                    foreach (DataRow dr in dtCategories.Rows)
                    {
                        KTCategoryDetails category = new KTCategoryDetails();
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["SKU"])))
                        {
                            category.SKU = Convert.ToString(dr["SKU"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["StyleCode"])))
                        {
                            category.StyleCode = Convert.ToString(dr["StyleCode"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["SizeCode"])))
                        {
                            category.SizeCode = Convert.ToString(dr["SizeCode"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["Desc"])))
                        {
                            category.ItemDescription = Convert.ToString(dr["Desc"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["VendorName"])))
                        {
                            category.VendorName = Convert.ToString(dr["VendorName"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["Gender"])))
                        {
                            category.Gender = Convert.ToString(dr["Gender"]);
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["StoreID"])))
                        {
                            category.StoreID = Convert.ToInt32(dr["StoreID"]);
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["QtyMin"])))
                        {
                            category.MinQty = Convert.ToInt32(dr["QtyMin"]);
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["QtyMax"])))
                        {
                            category.MaxQty = Convert.ToInt32(dr["QtyMax"]);
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["Price"])))
                        {
                            category.Price = Convert.ToDouble(dr["Price"]);
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["QOH"])))
                        {
                            category.QOH = Convert.ToInt32(dr["QOH"]);
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["KT_StoreName"])))
                        {
                            category.StoreName = Convert.ToString(dr["KT_StoreName"]);
                        }
                        KTCategories.Add(category);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error:SmokinJeosClass:GetProductdetailsForUPC:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("SmokinJeosClass:GetProductdetailsForUPC Leaving");
            }
            return KTCategories;
        }

        public bool UpdateBulkAssociatedProductItems(string UPC, string SKU, int StoreId, int DeviceID, List<string> RFIDs, out int Added, out int Rejected)
        {
            bool isSuccess = false;
            try
            {
                _log.Trace("SmokinJeosClass:UpdateBulkAssociatedProductItems Entering");
                Products objproduct = new Products();
                objproduct.SKU = SKU;
                objproduct.UPC = UPC;
                objproduct.StoreID = StoreId;

                Added = 0;
                Rejected = 0;

                string strRFIDs = string.Empty;
                for (int i = 0, j = 0; i < RFIDs.Count; i++, j++)
                {


                    if (strRFIDs != string.Empty)
                        strRFIDs += ",";
                    strRFIDs += RFIDs[i];

                    if (i == RFIDs.Count - 1 || j == 1000)
                    {
                        int cntAdd = 0, cntRej = 0;
                        isSuccess = objproduct.InsertProductItems(DeviceID, strRFIDs, out  cntRej, out cntAdd);
                        Added += cntAdd;
                        Rejected += cntRej;
                        strRFIDs = string.Empty;
                        j = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error:SmokinJeosClass:UpdateBulkAssociatedProductItems:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("SmokinJeosClass:UpdateBulkAssociatedProductItems Leaving");
            }
            return isSuccess;
        }

        public bool UpdateSingleAssociatedProductItem(string UPC, string SKU, int StoreId, int DeviceID, bool IsReturned, string RFIDTagID)
        {
            bool isSuccess = false;
            try
            {
                _log.Trace("SmokinJeosClass:UpdateSingleAssociatedProductItem Entering");
                Products objproduct = new Products();
                objproduct.SKU = SKU;
                objproduct.UPC = UPC;
                objproduct.StoreID = StoreId;

                isSuccess = objproduct.UpdateSingleAssociatedItem(DeviceID, IsReturned, RFIDTagID);
            }
            catch (Exception ex)
            {
                _log.Error("Error:SmokinJeosClass:UpdateSingleAssociatedProductItem:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("SmokinJeosClass:UpdateSingleAssociatedProductItem Leaving");
            }
            return isSuccess;
        }

        //public List<Locations> GetAllLocationsOnStore(int StoreID)
        //{
        //    DataTable dtLoc = new DataTable();
        //    List<Locations> lstlocations = null;
        //    string locationName = string.Empty;
        //    int locationid;
        //    try
        //    {
        //        _log.Trace("SmokinJeosClass:GetAllLocations Entering");
        //        StoresMaster stMaster = new StoresMaster();
        //        stMaster.StoreID = StoreID;
        //        dtLoc = stMaster.GetAllLocations();
        //        if (dtLoc != null && dtLoc.Rows.Count > 0)
        //        {
        //            lstlocations = new List<Locations>();
        //            foreach (DataRow drLoc in dtLoc.Rows)
        //            {
        //                locationid = Convert.ToInt32(drLoc["LocationID"].ToString());
        //                locationName = drLoc["LocationName"].ToString();
        //                Locations Loc = new Locations(locationid, locationName);
        //                lstlocations.Add(Loc);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _log.Error("Error:SmokinJeosClass:GetAllLocations:: " + ex.Message + Environment.NewLine + ex.StackTrace);
        //        throw new Exception(ex.Message);
        //    }
        //    finally
        //    {
        //        SDCWSHelper.ReleaseRemoteObject();
        //        _log.Trace("SmokinJeosClass:GetAllLocations Leaving");
        //    }
        //    return lstlocations;
        //}

        public string GetLastseenLocationOnUPC(int StoreID, string UPC)
        {
            string LastseenLocation = string.Empty;
            try
            {
                _log.Trace("SmokinJeosClass:GetLastseenLocationOnUPC Entering");
                KT_PutPickDetails objputpick = new KT_PutPickDetails();
                objputpick.StoreID = StoreID;
                objputpick.UPC = UPC;

                LastseenLocation = objputpick.GetLastSeenLocationOnUPC();
            }
            catch (Exception ex)
            {
                _log.Error("Error:SmokinJeosClass:GetLastseenLocationOnUPC:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("SmokinJeosClass:GetLastseenLocationOnUPC Leaving");
            }
            return LastseenLocation;
        }

        public bool UpdatePutAwayItems(int UpdatedBy, int StoreID, string UPC, string Location, int PutaWayQty, string Operation, string ReceiptNo, out string ERRORMSG, out string CompleteMSG)
        {
            bool isSuccess = false;
            ERRORMSG = string.Empty;
            CompleteMSG = string.Empty;
            try
            {
                _log.Trace("SmokinJeosClass:UpdatePutAwayItems Entering");
                KT_PutPickDetails objputpick = new KT_PutPickDetails();
                objputpick.UPC = UPC;
                objputpick.StoreID = StoreID;
                objputpick.Quantity = PutaWayQty;
                objputpick.LocationName = Location;
                objputpick.UpdatedBy = UpdatedBy;

                isSuccess = objputpick.UpdatePutAwayItems(Operation, ReceiptNo, out  ERRORMSG, out  CompleteMSG);
            }
            catch (Exception ex)
            {
                _log.Error("Error:SmokinJeosClass:UpdatePutAwayItems:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("SmokinJeosClass:UpdatePutAwayItems Leaving");
            }
            return isSuccess;
        }

        public bool UpdatePickedItems(int UpdatedBy, int StoreID, string UPC, string Location, int PickedQty, string Operation, string ReceiptNo, out string ERRORMSG, out string CompleteMSG)
        {
            bool isSuccess = false;
            ERRORMSG = string.Empty;
            CompleteMSG = string.Empty;
            try
            {
                _log.Trace("SmokinJeosClass:UpdatePickedItems Entering");
                KT_PutPickDetails objputpick = new KT_PutPickDetails();
                objputpick.UPC = UPC;
                objputpick.StoreID = StoreID;
                objputpick.Quantity = PickedQty;
                objputpick.LocationName = Location;
                objputpick.UpdatedBy = UpdatedBy;

                isSuccess = objputpick.UpdatePickedItems(Operation, ReceiptNo, out ERRORMSG, out CompleteMSG);
            }
            catch (Exception ex)
            {
                _log.Error("Error:SmokinJeosClass:UpdatePickedItems:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("SmokinJeosClass:UpdatePickedItems Leaving");
            }
            return isSuccess;
        }

        public List<Locations> GetLocQuantityOnUPC(string UPC, int StoreID)
        {
            DataTable dtLoc = new DataTable();
            List<Locations> lstlocations = null;
            string locationName = string.Empty;
            string Upc = string.Empty;
            int Qty;
            try
            {
                _log.Trace("SmokinJeosClass:GetLocQuantityOnUPC Entering");
                KT_PutPickDetails objputpick = new KT_PutPickDetails();
                objputpick.UPC = UPC;
                objputpick.StoreID = StoreID;
                dtLoc = objputpick.GetLocationQuantityOnUPC();
                if (dtLoc != null && dtLoc.Rows.Count > 0)
                {
                    lstlocations = new List<Locations>();
                    foreach (DataRow drLoc in dtLoc.Rows)
                    {
                        Qty = Convert.ToInt32(drLoc["Quantity"].ToString());
                        locationName = drLoc["LocationName"].ToString();
                        Upc = drLoc["UPC"].ToString();
                        Locations Loc = new Locations(locationName, UPC, Qty);
                        lstlocations.Add(Loc);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error:SmokinJeosClass:GetLocQuantityOnUPC:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("SmokinJeosClass:GetLocQuantityOnUPC Leaving");
            }
            return lstlocations;
        }

        public List<string> GetActivePutPickList(string ListType, int StoreID)
        {
            DataTable dtPutPick = new DataTable();
            List<string> activelst = null;
            string Putpick = string.Empty;
            try
            {
                _log.Trace("SmokinJeosClass:GetActivePutPickList Entering");
                KT_PutPickDetails objputpick = new KT_PutPickDetails();
                objputpick.StoreID = StoreID;
                dtPutPick = objputpick.GetActivePutPickList(ListType);
                if (dtPutPick != null && dtPutPick.Rows.Count > 0)
                {
                    activelst = new List<string>();
                    foreach (DataRow dr in dtPutPick.Rows)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["ListNo"])))
                        {
                            Putpick = Convert.ToString(dr["ListNo"]);
                        }
                        activelst.Add(Putpick);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error:SmokinJeosClass:GetActivePutPickList:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("SmokinJeosClass:GetActivePutPickList Leaving");
            }
            return activelst;
        }

        public List<KTPutPickDetails> GetItemDetailsOnPutPickReceiptNo(string ListType, string ReceiptNo, int StoreID)
        {
            DataTable dtPutPick = new DataTable();
            List<KTPutPickDetails> Detailslst = null;
            try
            {
                _log.Trace("SmokinJeosClass:GetItemDetailsOnPutPickReceiptNo Entering");
                KT_PutPickDetails objputpick = new KT_PutPickDetails();
                objputpick.StoreID = StoreID;
                objputpick.ListNo = ReceiptNo;
                objputpick.ListType = ListType;
                dtPutPick = objputpick.GetItemDetailsOnPutPickReceiptNo();
                if (dtPutPick != null && dtPutPick.Rows.Count > 0)
                {
                    Detailslst = new List<KTPutPickDetails>();
                    foreach (DataRow dr in dtPutPick.Rows)
                    {
                        KTPutPickDetails putpickDetail = new KTPutPickDetails();
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["ListNo"])))
                        {
                            putpickDetail.ListNo = Convert.ToString(dr["ListNo"]);
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["UPC"])))
                        {
                            putpickDetail.UPC = Convert.ToString(dr["UPC"]);
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["desc"])))
                        {
                            putpickDetail.Desc = Convert.ToString(dr["desc"]);
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["PutAwayQty"])))
                        {
                            putpickDetail.PutAwayQty = Convert.ToInt32(dr["PutAwayQty"].ToString());
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["Qty"])))
                        {
                            putpickDetail.Qty = Convert.ToInt32(dr["Qty"].ToString());
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["ActuallyPutAwayQty"])))
                        {
                            putpickDetail.ActuallyPutAwayQty = Convert.ToInt32(dr["ActuallyPutAwayQty"].ToString());
                        }

                        Detailslst.Add(putpickDetail);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error:SmokinJeosClass:GetItemDetailsOnPutPickReceiptNo:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("SmokinJeosClass:GetItemDetailsOnPutPickReceiptNo Leaving");
            }
            return Detailslst;
        }

        public bool InsertPackingSlip(string PackingSlip, string ListType, int StoreID, string Status, int UserID)
        {
            bool isSuccess = false;
            try
            {
                _log.Trace("SmokinJeosClass:InsertPackingSlip Entering");
                KT_PutPickDetails objputpick = new KT_PutPickDetails();
                objputpick.StoreID = StoreID;
                objputpick.Status = Status;
                objputpick.ListType = ListType;
                isSuccess = objputpick.InsertPackingSlip(PackingSlip, UserID);
            }
            catch (Exception ex)
            {
                _log.Error("Error:SmokinJeosClass:InsertPackingSlip:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("SmokinJeosClass:InsertPackingSlip Leaving");
            }
            return isSuccess;
        }

        public List<KTCycleCount> GetDeptsAndZones(int StoreID)
        {
            DataTable dtDepts = new DataTable();
            List<KTCycleCount> Detailslst = null;
            try
            {
                _log.Trace("SmokinJeosClass:GetDeptsAndZones Entering");
                KT_CycleCount objdept = new KT_CycleCount();
                objdept.StoreID = StoreID;
                dtDepts = objdept.GetDeptsAndZones();
                if (dtDepts != null && dtDepts.Rows.Count > 0)
                {
                    Detailslst = new List<KTCycleCount>();
                    foreach (DataRow dr in dtDepts.Rows)
                    {
                        KTCycleCount CCDetail = new KTCycleCount();
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["StoreID"])))
                        {
                            CCDetail.StoreID = Convert.ToInt32(dr["StoreID"].ToString());
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["DeptID"])))
                        {
                            CCDetail.DeptID = Convert.ToInt32(dr["DeptID"].ToString());
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["DeptName"])))
                        {
                            CCDetail.DeptName = Convert.ToString(dr["DeptName"]);
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["ZoneID"])))
                        {
                            CCDetail.ZoneID = Convert.ToInt32(dr["ZoneID"].ToString());
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["ZoneName"])))
                        {
                            CCDetail.ZoneName = Convert.ToString(dr["ZoneName"]);
                        }
                        Detailslst.Add(CCDetail);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error:SmokinJeosClass:GetDeptsAndZones:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("SmokinJeosClass:GetDeptsAndZones Leaving");
            }
            return Detailslst;
        }

        public bool UpdateCycleCount(int StoreID, int ZoneID, DateTime Startdate, List<string> RFIDs, int CCountID, out int Cyclecount, out int Unknown, int UserID)
        {
            bool isSuccess = false; Cyclecount = 0; Unknown = 0;
            try
            {
                _log.Warn("SmokinJeosClass:UpdateCycleCount Entering");
                KT_CycleCount CCMaster = new KT_CycleCount();
                CCMaster.StoreID = StoreID;
                CCMaster.CreatedDate = Startdate;
                CCMaster.UpdatedBy = UserID;
                CCMaster.ZoneID = ZoneID;
                CCMaster.CycleCountID = CCountID;
                // string strRfTagIds = string.Join(",", new List<string>(RFIDs).ToArray());

                StringBuilder strRFTAG = new StringBuilder();

                foreach (string TagId in RFIDs)
                {
                    strRFTAG.Append(TagId).Append(",");
                }

                //strlst = strRFTAG.ToString();
                //strlst = strlst.Remove(strlst.Length - 1,1);

                strRFTAG = strRFTAG.Remove(strRFTAG.Length - 1, 1);
                _log.Error("Tag received : " + strRFTAG);
                _log.Warn("SmokinJeosClass:UpdateCycleCount Web sercive call");
                isSuccess = CCMaster.UpdateCycleCount(strRFTAG.ToString(), out Cyclecount, out Unknown);

                _log.Warn("SmokinJeosClass:UpdateCycleCount Web sercive END");
            }
            catch (Exception ex)
            {
                _log.Error("Error:SmokinJeosClass:UpdateCycleCount:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("SmokinJeosClass:UpdateCycleCount Leaving");
            }
            return isSuccess;
        }

        public bool UpdateCycleCount_LastCall(int StoreID, int ZoneID, DateTime Startdate, List<string> RFIDs, int CCountID, bool? IsLastCall, out int Cyclecount, out int Unknown, int UserID)
        {
            bool isSuccess = false; Cyclecount = 0; Unknown = 0;
            try
            {
                _log.Warn("SmokinJeosClass:UpdateCycleCount_LastCall Entering");
                KT_CycleCount CCMaster = new KT_CycleCount();
                CCMaster.StoreID = StoreID;
                CCMaster.CreatedDate = Startdate;
                CCMaster.UpdatedBy = UserID;
                CCMaster.ZoneID = ZoneID;
                CCMaster.CycleCountID = CCountID;
                // string strRfTagIds = string.Join(",", new List<string>(RFIDs).ToArray());

                StringBuilder strRFTAG = new StringBuilder();

                foreach (string TagId in RFIDs)
                {
                    strRFTAG.Append(TagId).Append(",");
                }

                //strlst = strRFTAG.ToString();
                //strlst = strlst.Remove(strlst.Length - 1,1);

                strRFTAG = strRFTAG.Remove(strRFTAG.Length - 1, 1);
                _log.Error("Tag received : " + strRFTAG);
                _log.Warn("SmokinJeosClass:UpdateCycleCount_LastCall Web sercive call");
                isSuccess = CCMaster.UpdateCycleCount_LastCall(strRFTAG.ToString(), IsLastCall, out Cyclecount, out Unknown);
                _log.Warn("SmokinJeosClass:UpdateCycleCount_LastCall Web sercive END");
            }
            catch (Exception ex)
            {
                _log.Error("Error:SmokinJeosClass:UpdateCycleCount_LastCall:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("SmokinJeosClass:UpdateCycleCount_LastCall Leaving");
            }
            return isSuccess;
        }


        public bool UpdateCycleCountforDC(int StoreID, int ZoneID, DateTime Startdate, List<string> RFIDs, int CCountID, int UserID, int DeviceID)
        {
            bool isSuccess = false;
            try
            {
                _log.Trace("SmokinJeosClass:UpdateCycleCountforDC Entering");
                KT_CycleCount CCMaster = new KT_CycleCount();
                CCMaster.StoreID = StoreID;
                CCMaster.CreatedDate = Startdate;
                CCMaster.UpdatedBy = UserID;
                CCMaster.ZoneID = ZoneID;
                CCMaster.CycleCountID = CCountID;
                CCMaster.DeviceID = DeviceID;
                string strRfTagIds = string.Join(",", new List<string>(RFIDs).ToArray());
                isSuccess = CCMaster.UpdateCycleCountForDC(strRfTagIds);
            }
            catch (Exception ex)
            {
                _log.Error("Error:SmokinJeosClass:UpdateCycleCountforDC:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("SmokinJeosClass:UpdateCycleCountforDC Leaving");
            }
            return isSuccess;
        }



        public bool UpdateDeCommissionedItems(int StoreID, int DeviceID, List<string> RFIDs, bool IsDamaged, out int Decommissioned, out int Rejected)
        {
            bool isSuccess = false; Decommissioned = 0; Rejected = 0;
            try
            {
                _log.Trace("SmokinJeosClass:UpdateDeCommissionedItems Entering");
                KT_CycleCount CCMaster = new KT_CycleCount();
                CCMaster.StoreID = StoreID;
                string strRfTagIds = string.Join(",", new List<string>(RFIDs).ToArray());
                isSuccess = CCMaster.UpdateDeCommissionedItems(strRfTagIds, DeviceID, IsDamaged, out  Decommissioned, out  Rejected);
            }
            catch (Exception ex)
            {
                _log.Error("Error:SmokinJeosClass:UpdateDeCommissionedItems:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("SmokinJeosClass:UpdateDeCommissionedItems Leaving");
            }
            return isSuccess;
        }

        public bool UndoDecommissionRFID(List<string> ProductItemIDs, List<string> RFIDs)
        {
            bool isSuccess = false;
            try
            {
                _log.Trace("SmokinJeosClass:UndoDecommissionRFID Entering");
                Products prdMaster = new Products();
                string DecomProdIds = string.Join(",", new List<string>(ProductItemIDs).ToArray());
                string DecomRfTagIds = string.Join(",", new List<string>(RFIDs).ToArray());
                isSuccess = prdMaster.UndoDecommission_OnRFIDs(DecomProdIds, DecomRfTagIds);
            }
            catch (Exception ex)
            {
                _log.Error("Error:SmokinJeosClass:UndoDecommissionRFID:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("SmokinJeosClass:UndoDecommissionRFID Leaving");
            }
            return isSuccess;
        }


        public List<KTCycleCount> CheckCycleCountForDay(int ZoneID, DateTime Date)
        {
            DataTable dtCC = new DataTable();
            List<KTCycleCount> Detailslst = null;
            try
            {
                _log.Trace("SmokinJeosClass:CheckCycleCountForDay Entering");
                KT_CycleCount objCC = new KT_CycleCount();
                objCC.ZoneID = ZoneID;
                objCC.CreatedDate = Date;
                dtCC = objCC.CheckCycleCountForDay();
                if (dtCC != null && dtCC.Rows.Count > 0)
                {
                    Detailslst = new List<KTCycleCount>();
                    foreach (DataRow dr in dtCC.Rows)
                    {
                        KTCycleCount CCDetail = new KTCycleCount();
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["CycleCountID"])))
                        {
                            CCDetail.CycleCountID = Convert.ToInt32(dr["CycleCountID"].ToString());
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["StartDate"])))
                        {
                            CCDetail.StartDate = Convert.ToDateTime(dr["StartDate"].ToString());
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["ZoneID"])))
                        {
                            CCDetail.ZoneID = Convert.ToInt32(dr["ZoneID"].ToString());
                        }

                        Detailslst.Add(CCDetail);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error:SmokinJeosClass:CheckCycleCountForDay:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("SmokinJeosClass:CheckCycleCountForDay Leaving");
            }
            return Detailslst;
        }


        public List<KTLookUP> GetLookUpSettingForRole(int RoleID)
        {
            DataTable dtLookUp = new DataTable();
            List<KTLookUP> Detailslst = null;
            try
            {
                _log.Trace("SmokinJeosClass:GetLookUpSettingForRole Entering");
                TrackerRetailDataAccess.User objlookup = new TrackerRetailDataAccess.User();
                objlookup.RoleID = RoleID;
                dtLookUp = objlookup.GetLookUpSettingForRole();
                if (dtLookUp != null && dtLookUp.Rows.Count > 0)
                {
                    Detailslst = new List<KTLookUP>();
                    foreach (DataRow dr in dtLookUp.Rows)
                    {
                        KTLookUP LUDetail = new KTLookUP();
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["LookupID"])))
                        {
                            LUDetail.LookUpId = Convert.ToInt32(dr["LookupID"].ToString());
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["Name"])))
                        {
                            LUDetail.Name = Convert.ToString(dr["Name"].ToString());
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["Value"])))
                        {
                            LUDetail.Value = Convert.ToString(dr["Value"].ToString());
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["RoleID"])))
                        {
                            LUDetail.RoleID = Convert.ToInt32(dr["RoleID"].ToString());
                        } 
                        Detailslst.Add(LUDetail);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error:SmokinJeosClass:GetLookUpSettingForRole:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("SmokinJeosClass:GetLookUpSettingForRole Leaving");
            }
            return Detailslst;
        }

        public List<KTLookUP> GetLookUpSettingForBarCode()
        {
            DataTable dtLookUp = new DataTable();
            List<KTLookUP> Detailslst = null;
            try
            {
                _log.Trace("SmokinJeosClass:GetLookUpSettingForBarCode Entering");
                TrackerRetailDataAccess.User objlookup = new TrackerRetailDataAccess.User();
                dtLookUp = objlookup.GetLookUpSettingForBarCode();
                if (dtLookUp != null && dtLookUp.Rows.Count > 0)
                {
                    Detailslst = new List<KTLookUP>();
                    foreach (DataRow dr in dtLookUp.Rows)
                    {
                        KTLookUP LUDetail = new KTLookUP();
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["LookupID"])))
                        {
                            LUDetail.LookUpId = Convert.ToInt32(dr["LookupID"].ToString());
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["Name"])))
                        {
                            LUDetail.Name = Convert.ToString(dr["Name"].ToString());
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["Value"])))
                        {
                            LUDetail.Value = Convert.ToString(dr["Value"].ToString());
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["RoleID"])))
                        {
                            LUDetail.RoleID = Convert.ToInt32(dr["RoleID"].ToString());
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["StoreID"])))
                        {
                            LUDetail.StoreID = Convert.ToString(dr["StoreID"].ToString());
                        }
                        Detailslst.Add(LUDetail);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error:SmokinJeosClass:GetLookUpSettingForBarCode:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("SmokinJeosClass:GetLookUpSettingForBarCode Leaving");
            }
            return Detailslst;
        }

        //Method For Seperate HH Application(KTSmartDCHH_PutAway)
        public bool UpdateBinProductMaster_OnPutAway(string UPC, string Location, int StoreID, int UserID, out string ERRORMSG)
        {
            bool isSuccess = false;
            ERRORMSG = string.Empty;
            try
            {
                _log.Trace("SmokinJeosClass:UpdateBinProductMaster_OnPutAway Entering");
                KT_PutPickDetails objputpick = new KT_PutPickDetails();
                objputpick.UPC = UPC;
                objputpick.StoreID = StoreID;
                objputpick.LocationName = Location;
                isSuccess = objputpick.UpdateBinProductMasterOnPutAway(UserID, out ERRORMSG);
            }
            catch (Exception ex)
            {
                _log.Error("Error:SmokinJeosClass:UpdateBinProductMaster_OnPutAway:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("SmokinJeosClass:UpdateBinProductMaster_OnPutAway Leaving");
            }
            return isSuccess;
        }

        #region [Imager]

        public byte[] GetUPCImage(string UPC, int storeId)
        {
            byte[] image = null;
            DataTable dtImage = new DataTable();
            try
            {
                _log.Trace("SmokinJeosClass:GetUPCImage Entering");
                Products p = new Products();
                dtImage = p.GetUPCImage(UPC, storeId);
                if (dtImage != null && dtImage.Rows.Count > 0)
                {
                    if (!DBNull.Value.Equals(dtImage.Rows[0]["ProductImage"]))
                        image = (byte[])dtImage.Rows[0]["ProductImage"];
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error:SmokinJeosClass:GetUPCImage:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("SmokinJeosClass:GetUPCImage Leaving");
            }
            return image;
        }

        public bool SaveImageForUPC(byte[] img, string UPC, int StoreID)
        {
            bool isSuccess = false;
            try
            {
                _log.Trace("SmokinJeosClass:SaveImageForUPC Entering");
                Products prod = new Products();
                isSuccess = prod.SaveImageForUPC(img, UPC, StoreID);
            }
            catch (Exception ex)
            {
                _log.Error("Error:SmokinJeosClass:SaveImageForUPC:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("SmokinJeosClass:SaveImageForUPC Leaving");
            }
            return isSuccess;
        }

        public bool UpdateUPC(string UPC, int storeID, string Desc, string vendorName, double price, int minQty, int maxQty)
        {
            bool isSuccess = false;
            try
            {
                _log.Trace("SmokinJeosClass:UpdateUPC Entering");
                Products prod = new Products();
                isSuccess = prod.UpdateUPC(UPC, storeID, Desc, vendorName, price, minQty, maxQty);
            }
            catch (Exception ex)
            {
                _log.Error("Error:SmokinJeosClass:SaveImageForUPC:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("SmokinJeosClass:UpdateUPC Leaving");
            }
            return isSuccess;
        }
        #endregion [Imager]

    }
}
