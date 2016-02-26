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
    public class LocationClass
    {
        NLog.Logger _log = NLog.LogManager.GetCurrentClassLogger();

        public List<KTLocationDetails> GetAllLocation(int dataOwnerID)
        {
            List<KTLocationDetails> locationDetails = new List<KTLocationDetails>();
            try
            {
                _log.Trace("LocationClass:GetAllLocation:Entering");
                //IKTSDCCache sdcCache = SDCWSHelper.GetSDCCache();
                //if (sdcCache == null)
                //{
                    _log.Trace("LocationClass:GetAllLocation:sdcCache  null");
                    locationDetails = FillAllLocation(dataOwnerID);
                //}
                //else
                //{
                //    _log.Trace("LocationClass:GetAllLocation:sdcCache not null");
                //    return locationDetails = sdcCache.GetAllLocationDetails(dataOwnerID);
                //}

            }
            catch (Exception ex)
            {
                _log.Error("Error:LocationClass:GetAllLocation:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("LocationClass:GetAllLocation:Leaving");
            }
            return locationDetails;
        }

        public List<KTLocationDetails> GetAllLocation_Philips(int dataOwnerID)
        {
            List<KTLocationDetails> locationDetails = new List<KTLocationDetails>();
            try
            {
                _log.Trace("LocationClass:GetAllLocation_Philips:Entering");
                //IKTSDCCache sdcCache = SDCWSHelper.GetSDCCache();

                _log.Trace("LocationClass:GetAllLocation_Philips:sdcCache  null");
                locationDetails = FillAllLocation_Philips(dataOwnerID);


            }
            catch (Exception ex)
            {
                _log.Error("Error:LocationClass:GetAllLocation:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("LocationClass:GetAllLocation:Leaving");
            }
            return locationDetails;
        }

        private List<KTLocationDetails> FillAllLocation(int dataOwnerID)
        {
            List<KTLocationDetails> locationDetails = new List<KTLocationDetails>();
            try
            {
                _log.Trace("LocationClass:FillAllLocation:Entering");
                Location objLoc = new Location();
                objLoc.DataOwnerId = dataOwnerID;
                DataTable dtLocation = objLoc.SelectAll();
                if (dtLocation != null && dtLocation.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtLocation.Rows)
                    {
                        bool isActive = false;
                        DateTime createdDate = DateTime.MinValue;
                        DateTime modifiedDate = DateTime.MinValue;
                        int CategoryId = 0, ParentLocationId = 0, DataOwnerID = 0; byte[] Locationimage = null;
                        string LocationName = "", Description = "", RFResource = "", RFValue = "", StencilData = "", LocationZone = "";
                        if (dr["IsActive"] != null && dr["IsActive"].ToString() != string.Empty)
                            isActive = Convert.ToBoolean(dr["IsActive"].ToString());
                        if (dr["CreatedDate"] != null && dr["CreatedDate"].ToString() != string.Empty)
                            createdDate = Convert.ToDateTime(dr["CreatedDate"].ToString());
                        if (dr["ModifiedDate"] != null && dr["ModifiedDate"].ToString() != string.Empty)
                            modifiedDate = Convert.ToDateTime(dr["ModifiedDate"].ToString());
                        if (dr["CategoryId"] != null && dr["CategoryId"].ToString() != string.Empty)
                            CategoryId = int.Parse(dr["CategoryId"].ToString());
                        if (dr["ParentLocationId"] != null && dr["ParentLocationId"].ToString() != string.Empty)
                            ParentLocationId = int.Parse(dr["ParentLocationId"].ToString());
                        if (dr["LocationName"] != null && dr["LocationName"].ToString() != string.Empty)
                            LocationName = dr["LocationName"].ToString();
                        if (dr["Description"] != null && dr["Description"].ToString() != string.Empty)
                            Description = dr["Description"].ToString();
                        if (dr["RFResource"] != null && dr["RFResource"].ToString() != string.Empty)
                            RFResource = dr["RFResource"].ToString();
                        if (dr["RFValue"] != null && dr["RFValue"].ToString() != string.Empty)
                            RFValue = dr["RFValue"].ToString();
                        if (dr["DataOwnerID"] != null && dr["DataOwnerID"].ToString() != string.Empty)
                            DataOwnerID = int.Parse(dr["DataOwnerID"].ToString());


                        if (dr["Stencildata"] != null && dr["Stencildata"].ToString() != string.Empty)
                            StencilData = dr["Stencildata"].ToString();
                        if (dr["Locationzone"] != null && dr["Locationzone"].ToString() != string.Empty)
                            LocationZone = dr["Locationzone"].ToString();
                        if (dr["LocationImage"] != null && dr["LocationImage"].ToString() != string.Empty)
                            Locationimage = (byte[])(dr["LocationImage"]);


                        KTLocationDetails objLocdetails = new KTLocationDetails(isActive, int.Parse(dr["LocationId"].ToString()), CategoryId,
                               ParentLocationId, LocationName, Description, RFResource,
                                RFValue, createdDate, modifiedDate, DataOwnerID, StencilData, LocationZone, Locationimage);

                        locationDetails.Add(objLocdetails);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error:LocationClass:FillAllLocation:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                _log.Trace("LocationClass:FillAllLocation:Leaving");
            }
            return locationDetails;
        }


        private List<KTLocationDetails> FillAllLocation_Philips(int dataOwnerID)
        {
            List<KTLocationDetails> locationDetails = new List<KTLocationDetails>();
            try
            {
                _log.Trace("LocationClass:FillAllLocation_Philips:Entering");
                Location objLoc = new Location();
                objLoc.DataOwnerId = dataOwnerID;
                DataTable dtLocation = objLoc.SelectAll_Philis();
                if (dtLocation != null && dtLocation.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtLocation.Rows)
                    {
                        bool isActive = false;
                        DateTime createdDate = DateTime.MinValue;
                        DateTime modifiedDate = DateTime.MinValue;
                        int CategoryId = 0, ParentLocationId = 0, DataOwnerID = 0; byte[] Locationimage = null;
                        string LocationName = "", Description = "", RFResource = "", RFValue = "", CategoryName = "", StencilData = "", LocationZone = "";
                        if (dr["IsActive"] != null && dr["IsActive"].ToString() != string.Empty)
                            isActive = Convert.ToBoolean(dr["IsActive"].ToString());
                        if (dr["CreatedDate"] != null && dr["CreatedDate"].ToString() != string.Empty)
                            createdDate = Convert.ToDateTime(dr["CreatedDate"].ToString());
                        if (dr["ModifiedDate"] != null && dr["ModifiedDate"].ToString() != string.Empty)
                            modifiedDate = Convert.ToDateTime(dr["ModifiedDate"].ToString());
                        if (dr["CategoryId"] != null && dr["CategoryId"].ToString() != string.Empty)
                            CategoryId = int.Parse(dr["CategoryId"].ToString());
                        if (dr["ParentLocationId"] != null && dr["ParentLocationId"].ToString() != string.Empty)
                            ParentLocationId = int.Parse(dr["ParentLocationId"].ToString());
                        if (dr["LocationName"] != null && dr["LocationName"].ToString() != string.Empty)
                            LocationName = dr["LocationName"].ToString();
                        if (dr["Description"] != null && dr["Description"].ToString() != string.Empty)
                            Description = dr["Description"].ToString();
                        if (dr["RFResource"] != null && dr["RFResource"].ToString() != string.Empty)
                            RFResource = dr["RFResource"].ToString();
                        if (dr["RFValue"] != null && dr["RFValue"].ToString() != string.Empty)
                            RFValue = dr["RFValue"].ToString();
                        if (dr["DataOwnerID"] != null && dr["DataOwnerID"].ToString() != string.Empty)
                            DataOwnerID = int.Parse(dr["DataOwnerID"].ToString());
                        if (dr["CategoryName"] != null && dr["CategoryName"].ToString() != string.Empty)
                            CategoryName = Convert.ToString(dr["CategoryName"]);

                        if (dr["Stencildata"] != null && dr["Stencildata"].ToString() != string.Empty)
                            StencilData = dr["Stencildata"].ToString();
                        if (dr["Locationzone"] != null && dr["Locationzone"].ToString() != string.Empty)
                            LocationZone = dr["Locationzone"].ToString();
                        if (dr["LocationImage"] != null && dr["LocationImage"].ToString() != string.Empty)
                            Locationimage = (byte[])(dr["LocationImage"]);



                        KTLocationDetails objLocdetails = new KTLocationDetails(isActive, int.Parse(dr["LocationId"].ToString()), CategoryId, CategoryName,
                               ParentLocationId, LocationName, Description, RFResource,
                                RFValue, createdDate, modifiedDate, DataOwnerID, StencilData, LocationZone, Locationimage);

                        locationDetails.Add(objLocdetails);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error:LocationClass:FillAllLocation_Philips:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                _log.Trace("LocationClass:FillAllLocation_Philips:Leaving");
            }
            return locationDetails;
        }


        private List<KTLocationDetails> FillAllLocationByCategory(int CategoryID, int dataOwnerID)
        {
            List<KTLocationDetails> locationDetails = new List<KTLocationDetails>();
            try
            {
                _log.Trace("LocationClass:FillAllLocationByCategory:Entering");
                Location objLoc = new Location();
                objLoc.DataOwnerId = dataOwnerID;
                DataTable dtLocation = objLoc.SelectAll();
                if (dtLocation != null && dtLocation.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtLocation.Rows)
                    {
                        bool isActive = false;
                        DateTime createdDate = DateTime.MinValue;
                        DateTime modifiedDate = DateTime.MinValue;
                        int CategoryId = 0, ParentLocationId = 0, DataOwnerID = 0; byte[] Locationimage = new byte[8];
                        string LocationName = "", Description = "", RFResource = "", RFValue = "", StencilData = "", LocationZone = "";
                        if (dr["IsActive"] != null && dr["IsActive"].ToString() != string.Empty)
                            isActive = Convert.ToBoolean(dr["IsActive"].ToString());
                        if (dr["CreatedDate"] != null && dr["CreatedDate"].ToString() != string.Empty)
                            createdDate = Convert.ToDateTime(dr["CreatedDate"].ToString());
                        if (dr["ModifiedDate"] != null && dr["ModifiedDate"].ToString() != string.Empty)
                            modifiedDate = Convert.ToDateTime(dr["ModifiedDate"].ToString());
                        if (dr["CategoryId"] != null && dr["CategoryId"].ToString() != string.Empty)
                            CategoryId = int.Parse(dr["CategoryId"].ToString());
                        if (dr["ParentLocationId"] != null && dr["ParentLocationId"].ToString() != string.Empty)
                            ParentLocationId = int.Parse(dr["ParentLocationId"].ToString());
                        if (dr["LocationName"] != null && dr["LocationName"].ToString() != string.Empty)
                            LocationName = dr["LocationName"].ToString();
                        if (dr["Description"] != null && dr["Description"].ToString() != string.Empty)
                            Description = dr["Description"].ToString();
                        if (dr["RFResource"] != null && dr["RFResource"].ToString() != string.Empty)
                            RFResource = dr["RFResource"].ToString();
                        if (dr["RFValue"] != null && dr["RFValue"].ToString() != string.Empty)
                            RFValue = dr["RFValue"].ToString();
                        if (dr["DataOwnerID"] != null && dr["DataOwnerID"].ToString() != string.Empty)
                            DataOwnerID = int.Parse(dr["DataOwnerID"].ToString());

                        if (dr["Stencildata"] != null && dr["Stencildata"].ToString() != string.Empty)
                            StencilData = dr["Stencildata"].ToString();
                        if (dr["Locationzone"] != null && dr["Locationzone"].ToString() != string.Empty)
                            LocationZone = dr["Locationzone"].ToString();
                        if (dr["LocationImage"] != null && dr["LocationImage"].ToString() != string.Empty)
                            Locationimage = (byte[])(dr["LocationImage"]);



                        KTLocationDetails objLocdetails = new KTLocationDetails(isActive, int.Parse(dr["LocationId"].ToString()), CategoryId,
                                ParentLocationId, LocationName, Description, RFResource,
                                 RFValue, createdDate, modifiedDate, DataOwnerID, StencilData, LocationZone, Locationimage);

                        if (objLocdetails.CategoryID == CategoryID)
                            locationDetails.Add(objLocdetails);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error:LocationClass:FillAllLocationByCategory:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                _log.Trace("LocationClass:FillAllLocationByCategory:Leaving");
            }
            return locationDetails;
        }
        public List<KTLocationDetails> GetAllLocationByCategory(int CategoryID, int dataOwnerID)
        {

            List<KTLocationDetails> locationDetails = new List<KTLocationDetails>();

            try
            {
                _log.Trace("LocationClass:GetAllLocationByCategory:Entering");

                //IKTSDCCache sdcCache = SDCWSHelper.GetSDCCache();
                //if (sdcCache == null)
                //{
                    _log.Trace("LocationClass:GetAllLocationByCategory:sdcCache  null");
                    locationDetails = FillAllLocationByCategory(CategoryID, dataOwnerID);
                //}
                //else
                //{
                //    _log.Trace("LocationClass:GetAllLocationByCategory:sdcCache not  null");
                //    return locationDetails = sdcCache.GetAllLocationByCategory(dataOwnerID, CategoryID);
                //}
            }
            catch (Exception ex)
            {
                _log.Error("Error:LocationClass:GetAllLocationByCategory:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("LocationClass:GetAllLocationByCategory:Leaving");
            }
            return locationDetails;

        }

        public KTLocationDetails GetLocationByLocationID(int dataOwnerID, int locationId)
        {
            KTLocationDetails objLocdetails = null;
            try
            {
                _log.Trace("LocationClass:Entering GetLocationByLocationID ... ");
                Location clsLocation = new Location();
                clsLocation.DataOwnerId = dataOwnerID;
                clsLocation.LocationId = locationId;
                DataTable dtLocation = clsLocation.SelectOne();


                bool isActive = false;
                DateTime createdDate = DateTime.MinValue;
                DateTime modifiedDate = DateTime.MinValue;
                int CategoryId = 0, ParentLocationId = 0, DataOwnerID = 0; byte[] Locationimage = new byte[8];
                string LocationName = "", Description = "", RFResource = "", RFValue = "", StencilData = "", LocationZone = "";
                if (dtLocation != null && dtLocation.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtLocation.Rows)
                    {
                        if (dr["IsActive"] != null && dr["IsActive"].ToString() != string.Empty)
                            isActive = Convert.ToBoolean(dr["IsActive"].ToString());
                        if (dr["CreatedDate"] != null && dr["CreatedDate"].ToString() != string.Empty)
                            createdDate = Convert.ToDateTime(dr["CreatedDate"].ToString());
                        if (dr["ModifiedDate"] != null && dr["ModifiedDate"].ToString() != string.Empty)
                            modifiedDate = Convert.ToDateTime(dr["ModifiedDate"].ToString());
                        if (dr["CategoryId"] != null && dr["CategoryId"].ToString() != string.Empty)
                            CategoryId = int.Parse(dr["CategoryId"].ToString());
                        if (dr["ParentLocationId"] != null && dr["ParentLocationId"].ToString() != string.Empty)
                            ParentLocationId = int.Parse(dr["ParentLocationId"].ToString());
                        if (dr["LocationName"] != null && dr["LocationName"].ToString() != string.Empty)
                            LocationName = dr["LocationName"].ToString();
                        if (dr["Description"] != null && dr["Description"].ToString() != string.Empty)
                            Description = dr["Description"].ToString();
                        if (dr["RFResource"] != null && dr["RFResource"].ToString() != string.Empty)
                            RFResource = dr["RFResource"].ToString();
                        if (dr["RFValue"] != null && dr["RFValue"].ToString() != string.Empty)
                            RFValue = dr["RFValue"].ToString();
                        if (dr["DataOwnerID"] != null && dr["DataOwnerID"].ToString() != string.Empty)
                            DataOwnerID = int.Parse(dr["DataOwnerID"].ToString());
                        if (dr["Stencildata"] != null && dr["Stencildata"].ToString() != string.Empty)
                            StencilData = dr["Stencildata"].ToString();
                        if (dr["Locationzone"] != null && dr["Locationzone"].ToString() != string.Empty)
                            LocationZone = dr["Locationzone"].ToString();
                        if (dr["LocationImage"] != null && dr["LocationImage"].ToString() != string.Empty)
                            Locationimage = (byte[])(dr["LocationImage"]);


                        objLocdetails = new KTLocationDetails(isActive, int.Parse(dr["LocationId"].ToString()), CategoryId,
                                 ParentLocationId, LocationName, Description, RFResource,
                                  RFValue, createdDate, modifiedDate, DataOwnerID, StencilData, LocationZone, Locationimage);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.ErrorException("LocationClass:Error in GetLocationByLocationID: " + ex.Message, ex);
                throw ex;
            }
            finally
            {
                _log.Trace("LocationClass:Leaving GetLocationByLocationID ... ");
            }
            return objLocdetails;
        }


        public List<KTLocationDetails> GetItemPrinter(int dataOwnerID)
        {
            List<KTLocationDetails> locationDetails = new List<KTLocationDetails>();

            Location objLocation = new Location();

            try
            {
                _log.Trace("LocationClass:GetItemPrinter:Entering");
                objLocation.DataOwnerId = dataOwnerID;
                DataTable dtItemPrinter = objLocation.ItemPrinterSelectAll();

                //IKTSDCCache sdcCache = SDCWSHelper.GetSDCCache();
                foreach (DataRow dr in dtItemPrinter.Rows)
                {
                    KTLocationDetails objKTLocationDetails = null;
                    //if (sdcCache == null)
                    //{
                        objKTLocationDetails = GetLocationByLocationID(dataOwnerID, int.Parse(dr["LocationId"].ToString()));
                    //}
                    //else
                    //{
                    //    objKTLocationDetails = sdcCache.GetAllLocationByLocationID(dataOwnerID, int.Parse(dr["LocationId"].ToString()));
                    //}
                    locationDetails.Add(objKTLocationDetails);

                }

            }
            catch (Exception ex)
            {
                _log.Error("Error:LocationClass:GetItemPrinter:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("LocationClass:GetItemPrinter:Leaving");
            }
            return locationDetails;
        }

        public List<KTLocationDetails> GetLocationPrinter(int dataOwnerID)
        {
            List<KTLocationDetails> locationDetails = new List<KTLocationDetails>();

            Location objLocation = new Location();

            try
            {
                _log.Trace("LocationClass:GetLocationPrinter:Entering");
                objLocation.DataOwnerId = dataOwnerID;
                DataTable dtItemPrinter = objLocation.LocationPrinterSelectAll();

                //IKTSDCCache sdcCache = SDCWSHelper.GetSDCCache();
                foreach (DataRow dr in dtItemPrinter.Rows)
                {
                    KTLocationDetails objKTLocationDetails = null;
                   // if (sdcCache == null)
                   // {
                        objKTLocationDetails = GetLocationByLocationID(dataOwnerID, int.Parse(dr["LocationId"].ToString()));
                  //  }
                  //  else
                   // {
                   //     objKTLocationDetails = sdcCache.GetAllLocationByLocationID(dataOwnerID, int.Parse(dr["LocationId"].ToString()));
                   // }
                    locationDetails.Add(objKTLocationDetails);

                }

            }
            catch (Exception ex)
            {
                _log.Error("Error:LocationClass:GetLocationPrinter:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("LocationClass:GetLocationPrinter:Leaving");
            }
            return locationDetails;
        }

        public List<KTSKUlocationAssociation> GetSKUIDsforAssociatedLocation(int dataOwnerID, int locationID)
        {
            List<KTSKUlocationAssociation> skulocationDetails = new List<KTSKUlocationAssociation>();

            try
            {
                _log.Trace("LocationClass:GetSKUIDsforAssociatedLocation:Entering");
                //IKTSDCCache sdcCache = SDCWSHelper.GetSDCCache();

                //if (sdcCache == null)
                //{
                    skulocationDetails = FillSKULocationAssociationbyLocationId(dataOwnerID, locationID);
                //}
                //else
                //{
                //    return skulocationDetails = sdcCache.GetSKUIDsforAssociatedLocation(dataOwnerID, locationID);
                //}
            }
            catch (Exception ex)
            {
                _log.Error("Error:LocationClass:GetSKUIDsforAssociatedLocation:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("LocationClass:GetSKUIDsforAssociatedLocation:Leaving");
            }
            return skulocationDetails;
        }
        public List<KTSKUlocationAssociation> GetLocationIDsforAssociatedSKU(int dataOwnerID, long SkuID)
        {
            List<KTSKUlocationAssociation> skulocationDetails = new List<KTSKUlocationAssociation>();

            try
            {
                _log.Trace("LocationClass:GetLocationIDsforAssociatedSKU:Entering");
                //IKTSDCCache sdcCache = SDCWSHelper.GetSDCCache();

                //if (sdcCache == null)
                //{
                    skulocationDetails = FillSKULocationAssociationbySKUID(dataOwnerID, SkuID);
                //}
                //else
                //{
                //    return skulocationDetails = sdcCache.GetLocationIDsforAssociatedSKU(dataOwnerID, SkuID);
                //}
            }
            catch (Exception ex)
            {
                _log.Error("Error:LocationClass:GetLocationIDsforAssociatedSKU:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                SDCWSHelper.ReleaseRemoteObject();
                _log.Trace("LocationClass:GetLocationIDsforAssociatedSKU:Leaving");
            }
            return skulocationDetails;
        }
        private List<KTSKUlocationAssociation> FillSKULocationAssociationbySKUID(int dataOwnerID, long SKUID)
        {
            List<KTSKUlocationAssociation> skulocationDetails = new List<KTSKUlocationAssociation>();
            try
            {
                _log.Trace("LocationClass:FillSKULocationAssociationbySKUID:Entering");
                SKUMaster objsku = new SKUMaster();
                objsku.DataOwnerID = dataOwnerID;
                DataTable dtskuLocation = objsku.SelectALLSKULocationAssociation();
                if (dtskuLocation != null && dtskuLocation.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtskuLocation.Rows)
                    {
                        int LocationID = 0, DataOwnerID = 0;
                        long SkuID = 0;
                        if (dr["LocationId"] != null && dr["LocationId"].ToString() != string.Empty)
                            LocationID = Convert.ToInt32(dr["LocationId"]);
                        if (dr["DataOwnerID"] != null && dr["DataOwnerID"].ToString() != string.Empty)
                            DataOwnerID = int.Parse(dr["DataOwnerID"].ToString());
                        if (dr["SKU_ID"] != null && dr["SKU_ID"].ToString() != string.Empty)
                            SkuID = Convert.ToInt64(dr["SKU_ID"]);

                        KTSKUlocationAssociation objlocskudetails = new KTSKUlocationAssociation(LocationID, DataOwnerID, SkuID);

                        if (objlocskudetails.SKUID == SKUID)
                            skulocationDetails.Add(objlocskudetails);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error:LocationClass:FillSKULocationAssociationbySKUID:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                _log.Trace("LocationClass:FillSKULocationAssociationbySKUID:Leaving");
            }
            return skulocationDetails;
        }
        private List<KTSKUlocationAssociation> FillSKULocationAssociationbyLocationId(int dataOwnerID, int LocationID)
        {
            List<KTSKUlocationAssociation> skulocationDetails = new List<KTSKUlocationAssociation>();
            try
            {
                _log.Trace("LocationClass:FillSKULocationAssociationbyLocationId:Entering");
                SKUMaster objsku = new SKUMaster();
                objsku.DataOwnerID = dataOwnerID;
                DataTable dtskuLocation = objsku.SelectALLSKULocationAssociation();
                if (dtskuLocation != null && dtskuLocation.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtskuLocation.Rows)
                    {
                        int LocationId = 0, DataOwnerID = 0;
                        long SkuID = 0;
                        if (dr["LocationId"] != null && dr["LocationId"].ToString() != string.Empty)
                            LocationId = Convert.ToInt32(dr["LocationId"]);
                        if (dr["DataOwnerID"] != null && dr["DataOwnerID"].ToString() != string.Empty)
                            DataOwnerID = int.Parse(dr["DataOwnerID"].ToString());
                        if (dr["SKU_ID"] != null && dr["SKU_ID"].ToString() != string.Empty)
                            SkuID = Convert.ToInt64(dr["SKU_ID"]);

                        KTSKUlocationAssociation objlocskudetails = new KTSKUlocationAssociation(LocationId, DataOwnerID, SkuID);

                        if (objlocskudetails.LocationID == LocationID)
                            skulocationDetails.Add(objlocskudetails);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error:LocationClass:FillSKULocationAssociationbyLocationId:: " + ex.Message + Environment.NewLine + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                _log.Trace("LocationClass:FillSKULocationAssociationbyLocationId:Leaving");
            }
            return skulocationDetails;
        }
    }
}