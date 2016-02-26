using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using KTone.WebServer;
using KTone.Core.KTIRFID;

namespace KTone.Core.KTIRFID
{
    [ServiceContract(Namespace = "http://www.keytonetech.com/KTSmartDC")]
    public interface IKTSmokinJoes
    {
        /// <summary>
        ///  GetAllStores
        /// </summary>
        /// <param name="CustomerUniqueID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        List<Stores> GetAllStores(string userID, string password);

        /// <summary>
        ///  GetAllCategoriesForRFTagID_AdHoc
        /// </summary>
        /// <param name="CustomerUniqueID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        List<KTCategoryDetails> GetAllCategoriesForRFTagID_AdHoc(string userID, string password, List<string> RFTagIDs, int storeID);

        /// <summary>
        ///  GetAllCategoriesForRFTagID_Replenishment
        /// </summary>
        /// <param name="CustomerUniqueID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        List<KTCategoryDetails> GetAllCategoriesForRFTagID_Replenishment(string userID, string password, List<string> RFTagIDs, int storeID, long RRID);

           /// <summary>
        ///  GetAllCategoriesForRFTagID_Replenishment_OnRFID
        /// </summary>
        /// <param name="CustomerUniqueID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        List<KTCategoryDetails> GetAllCategoriesForRFTagID_Replenishment_OnRFID(string userID, string password, List<string> RFTagIDs, int storeID, long RRID);

         /// <summary>
        ///  GetAllReplenishmentsForStore
        /// </summary>
        /// <param name="CustomerUniqueID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        List<KTReplensihmentRequest> GetAllReplenishmentsForStore(string userID, string password, int storeID);

        /// <summary>
        ///  GetReplenishmentDetailsForReplenishment
        /// </summary>
        /// <param name="CustomerUniqueID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        List<KTReplenishmentRequestDetails> GetReplenishmentDetailsForReplenishment(string userID, string password, long rrid, int storeID);

        /// <summary>
        ///  CheckOut
        /// </summary>
        /// <param name="CustomerUniqueID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        bool CheckOut(string userID, string password, int fromLocation, int toLoacation, long RRID, string ReplenishmentRequest, List<string> RFIDs, bool isAdhoc, out string packagingID);

        /// <summary>
        ///  CheckIn
        /// </summary>
        /// <param name="CustomerUniqueID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        bool CheckIn(string userID, string password, int toLoacation, string packageslip, List<string> RFIDs, bool isAdhoc, string userName);

        /// <summary>
        ///  validatePackageSlip
        /// </summary>
        /// <param name="CustomerUniqueID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        bool validatePackageSlip(string userID, string password, string packagingID, int storeID);

        /// <summary>
        ///  GetCategoriesOnPackagingID
        /// </summary>
        /// <param name="CustomerUniqueID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        List<KTCategoryDetails> GetCategoriesOnPackagingID(string userID, string password, string packagingID);

        /// <summary>
        ///  GetCategoriesOnPackagingID_PID
        /// </summary>
        /// <param name="CustomerUniqueID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        List<KTCategoryDetails> GetCategoriesOnPackagingID_PID(string userID, string password, string packagingID);

        /// <summary>
        ///  GetSourceStoreForPackageSlip
        /// </summary>
        /// <param name="CustomerUniqueID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        Stores GetSourceStoreForPackageSlip(string userID, string password, string packingSlip);

         /// <summary>
        ///  GetProductdetailsForUPC
        /// </summary>
        /// <param name="CustomerUniqueID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        List<KTCategoryDetails> GetProductdetailsForUPC(string userID, string password, string UPC, int storeID);

         /// <summary>
        ///  UpdateBulkAssociatedProductItems
        /// </summary>
        /// <param name="CustomerUniqueID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        bool UpdateBulkAssociatedProductItems(string userID, string password, string UPC, string SKU, int StoreId,int DeviceID, List<string> RFIDs, out int Added, out int Rejected);

        /// <summary>
        ///  UpdateSingleAssociatedProductItem
        /// </summary>
        /// <param name="CustomerUniqueID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        bool UpdateSingleAssociatedProductItem(string userID, string password, string UPC, string SKU, int StoreId, int DeviceID, bool IsReturned, string RFIDTagID);

        /// <summary>
        ///  UpdateProductsOnUPC
        /// </summary>
        /// <param name="CustomerUniqueID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        bool UpdateProductsOnUPC(string userID, string password, string UPC, int storeID, int SearchOnType);

        ///// <summary>
        /////  GetAllLocationsOnStore
        ///// </summary>
        ///// <param name="CustomerUniqueID"></param>
        ///// <returns></returns>
        //[OperationContract]
        //[FaultContract(typeof(KTSDSeviceException))]
        //List<Locations> GetAllLocationsOnStore(string userID, string password, int StoreID);

          /// <summary>
        ///  GetLastseenLocationOnUPC
        /// </summary>
        /// <param name="CustomerUniqueID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        string GetLastseenLocationOnUPC(string userID, string password, int StoreID, string UPC);


        /// <summary>
        ///  UpdatePutAwayItems
        /// </summary>
        /// <param name="CustomerUniqueID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        bool UpdatePutAwayItems(string userID, string password, string UPC, int StoreID, string location, int PutaWayQty, string Operation, string ReceiptNo, out string ERRORMSG, out string CompleteMSG);


        /// <summary>
        ///  UpdatePickedItems
        /// </summary>
        /// <param name="CustomerUniqueID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        bool UpdatePickedItems(string userID, string password, string UPC, int StoreID, string location, int PickedQty, string Operation, string ReceiptNo, out string ERRORMSG, out string CompleteMSG);


        /// <summary>
        ///  GetLocQuantityOnUPC
        /// </summary>
        /// <param name="CustomerUniqueID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        List<Locations> GetLocQuantityOnUPC(string userID, string password, string UPC, int StoreID);

          /// <summary>
        ///  GetActivePutPickList
        /// </summary>
        /// <param name="CustomerUniqueID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        List<string> GetActivePutPickList(string userID, string password, string ListType, int StoreID);


        /// <summary>
        ///  GetItemDetailsOnPutPickReceiptNo
        /// </summary>
        /// <param name="CustomerUniqueID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        List<KTPutPickDetails> GetItemDetailsOnPutPickReceiptNo(string userID, string password, string ListType, string ReceiptNo, int StoreID);

        /// <summary>
        ///  InsertPackagingSlip
        /// </summary>
        /// <param name="CustomerUniqueID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        bool InsertPackingSlip(string userID, string password, string packingSlip, string ListType, int StoreID, string Status);

         /// <summary>
        ///  GetDeptsAndZones
        /// </summary>
        /// <param name="CustomerUniqueID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        List<KTCycleCount> GetDeptsAndZones(string userID, string password, int StoreID);

        /// <summary>
        ///  UpdateCycleCountDetails
        /// </summary>
        /// <param name="CustomerUniqueID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        bool UpdateCycleCountDetails(string userID, string password, int StoreID, int ZoneID, DateTime Startdate, List<string> RFIDs, int CCountID, out int Cyclecount, out int Unknown);


        /// <summary>
        ///  UpdateCycleCountDetails_LastCall
        /// </summary>
        /// <param name="CustomerUniqueID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        bool UpdateCycleCountDetails_LastCall(string userID, string password, int StoreID, int ZoneID, DateTime Startdate, List<string> RFIDs, int CCountID, bool? IsLastCall, out int Cyclecount, out int Unknown);

        
        /// <summary>
        ///  UpdateDeCommissionedItems
        /// </summary>
        /// <param name="CustomerUniqueID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        bool UpdateDeCommissionedItems(string userID, string password, int StoreID, int DeviceID, List<string> RFIDs, bool IsDamaged, out int Decommissioned, out int Rejected);


        /// <summary>
        ///  CheckCycleCountForDay
        /// </summary>
        /// <param name="CustomerUniqueID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        List<KTCycleCount> CheckCycleCountForDay(string userID, string password, int ZoneID, DateTime Date);

         /// <summary>
        ///  UpdateBinProductMaster_OnPutAway
        /// </summary>
        /// <param name="CustomerUniqueID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        bool UpdateBinProductMaster_OnPutAway(string userID, string password, string UPC, string Location, int StoreID, out string ERRORMSG);

        /// <summary>
        ///  Get size of product Image
        /// </summary>
        /// <param name="CustomerUniqueID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        int GetSizeOfImageForUPC(string userID, string password,string upc, int storeID);

        /// <summary>
        ///  Download image from database.
        /// </summary>
        /// <param name="CustomerUniqueID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        byte[] GetProductImageForUPC(string UPC, int storeID, int offset, out int noOfBytes);

        /// <summary>
        ///  Save image to DB
        /// </summary>
        /// <param name="CustomerUniqueID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        bool SaveImageForUPC(string userID, string password, string UPC, int StoreID, byte[] image);

         /// <summary>
        /// Save product details to DB
        /// </summary>
        /// <param name="CustomerUniqueID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        bool UpdateUPC(string userID, string password, string UPC, int storeID, string Desc, string vendorName, double price, int minQty, int maxQty);


          /// <summary>
        /// Select devices from DB
        /// </summary>
        /// <param name="CustomerUniqueID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        List<Devices> GetAllDevicesForStore(string userID, string password);


          /// <summary>
        /// Select RFID Details On RFIDs
        /// </summary>
        /// <param name="CustomerUniqueID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        List<KTCategoryDetails> GetRFIDDetails_OnRFIDs(string userID, string password, List<string> RFTagIDs);

        //  /// <summary>
        ///// Select RFID Details On RFIDs
        ///// </summary>
        ///// <param name="CustomerUniqueID"></param>
        ///// <returns></returns>
        //[OperationContract]
        //[FaultContract(typeof(KTSDSeviceException))]
        //List<KTCategoryDetails> GetProducts_OnRFID_CheckIN(string userID, string password, List<string> RFTagIDs, int storeID);

        /// <summary>
        /// Select RFID Details On RFIDs
        /// </summary>
        /// <param name="CustomerUniqueID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        List<KTCategoryDetails> GetProducts_OnRFID_CICOAdhoc(string userID, string password, List<string> RFTagIDs, int storeID, string OperationType);
        

           /// <summary>
        /// Select RFID Details On Packing Slip for CheckIN
        /// </summary>
        /// <param name="CustomerUniqueID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        List<KTCategoryDetails> GetCategoriesOnPackagingID_OnRFID(string userID, string password, string packagingID, List<string> RFTagIDs);

           /// <summary>
        /// Undo (By Mistake)Decommissioned Items for CheckIN
        /// </summary>
        /// <param name="CustomerUniqueID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        bool UndoDecommissionRFID(string userID, string password, List<string> ProductItemIDs, List<string> RFIDs);

             /// <summary>
        ///   CheckIN on productItemIDs
        /// </summary>
        /// <param name="CustomerUniqueID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        bool CheckIn_OnPID(string userID, string password, int toLoacation, string packageslip, List<string> ProductItemIDs, bool isAdhoc, string userName,List<string> OverrideProductIDs, out string PackingIDonAdhoc);

        /// <summary>
        ///   CheckOUT on productItemIDs
        /// </summary>
        /// <param name="CustomerUniqueID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        bool CheckOut_OnPID(string userID, string password, int fromLocation, int toLoacation, long RRID, string ReplenishmentRequest, List<string> PRODUCTITEMIDs, bool isAdhoc, string packagingID, out string UniquePackingId);


        
        /// <summary>
        ///  Get The look Up Settings
        /// </summary>
        /// <param name="CustomerUniqueID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        List<KTLookUP> GetLookUpSettingForRole(string userID, string password, int RoleID);

        /// <summary>
        ///  Get The look Up Settings for MaxDigit of Barcode
        /// </summary>
        /// <param name="CustomerUniqueID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        List<KTLookUP> GetLookUpSettingForBarCode(string userID, string password);
    }
}
