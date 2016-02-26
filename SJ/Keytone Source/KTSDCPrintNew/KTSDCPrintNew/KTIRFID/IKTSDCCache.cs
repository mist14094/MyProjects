using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml; 

namespace KTone.Core.KTIRFID
{
  
    /// <summary>
    /// Interface to access the SDC cache
    /// </summary>
    /// 
    public interface IKTSDCCache
    {
        #region [Company]
        /// <summary>
        /// Returns all the Company available in the cache.
        /// </summary>
        /// <returns>List of KTCompanyDetails</returns>
        List<KTCompanyDetails> GetAllCompanyDetails(int dataOwnerID);

        /// <summary>
        /// Returns Comapany details for the Company having the specified Company id
        /// </summary>
        /// <param name="CompanyID">Company id of the required Company</param>
        /// <returns>KTCompanyDetails</returns>
        KTCompanyDetails GetCompanyForCompanyID(int dataOwnerID, int companyID);        
        #endregion [Company]

        #region [Product]
        /// <summary>
        /// Returns all the Product available in the cache.
        /// </summary>
        /// <returns>List of KTProductDetails</returns>
        List<KTProductDetails> GetAllProductDetails(int dataOwnerID);

        /// <summary>
        /// Return  the Product for a given CompanyID.
        /// </summary>
        /// <param name="CompanyID">Company id of the required Product</param>
        /// <returns>List of KTProductDetails</returns>
        List<KTProductDetails> GetProductForCompanyID(int dataOwnerID, int companyID);

        /// <summary>
        /// Return  the Product for a given CompanyID.
        /// </summary>
        /// <param name="ProductID">Product id</param>
        /// <returns>List of KTProductDetails</returns>
        KTProductDetails GetProductForProductID(int dataOwnerID, long ProductID);
        #endregion [Product]

        #region [Item]
        /// <summary>
        /// Returns all the Items available in the cache.
        /// </summary>
        /// <returns>List of KTItemDetails</returns>
        List<KTItemDetails> GetAllItemDetails(int dataOwnerID);

        /// <summary>
        /// Return  the Item for a given ID.
        /// </summary>
        /// <param name="ID">Id </param>
        /// <returns>List of KTItemDetails</returns>
        KTItemDetails GetItemsForID(int dataOwnerID, long ID);

        /// <summary>
        /// Return  the Item for a given SKUID.
        /// </summary>
        /// <param name="SKUID">SKUID id </param>
        /// <returns>List of KTItemDetails</returns>
        List<KTItemDetails> GetItemForSkuID(int dataOwnerID, long SkuID);


        /// <summary>
        /// Return  the Item for a given SKUID.
        /// </summary>
        /// <param name="SKUID">SKUID id </param>
        /// <returns>List of KTItemDetails</returns>
        List<KTItemDetails> GetItemForSkuID(int dataOwnerID, long SkuID,int NoOfItems, ItemType itemType);

        /// <summary>
        /// Return  the Item for a given ProductID.
        /// </summary>
        /// <param name="ProductID">ProductID</param>
        /// <returns>List of KTItemDetails</returns>
        List<KTItemDetails> GetItemForProductID(int dataOwnerID, long ProductID);

        /// <summary>
        /// Return  the Item for a given CompanyID.
        /// </summary>
        /// <param name="CompanyID">CompanyID</param>
        /// <returns>List of KTItemDetails</returns>
        List<KTItemDetails> GetItemForCompanyID(int dataOwnerID, int CompanyID);

        /// <summary>
        /// Return  the Item for a given CustomerUniqueID.
        /// </summary>
        /// <param name="CustunoiqueID">CustunoiqueID</param>
        /// <returns> KTItemDetails</returns>
        long GetItemDetailsForCustomerID(string CustunoiqueID);


        /// <summary>
        /// Return  the Item for a given RFTAGID.
        /// </summary>
        /// <param name="RFTAGID">RFTAGID</param>
        /// <returns> KTItemDetails</returns>
        long GetItemDetailsForRFTAGID(string RFTAGID);

        long GetItemsForIDFromCache(int dataOwnerID, long ID);
        #endregion [Item]

        #region [SKU]
        /// <summary>
        /// Returns all the SKU's available in the cache.
        /// </summary>
        /// <returns>List of KTSKUDetails</returns>
        List<KTSKUDetails> GetAllSKUDetails(int dataOwnerID);

        /// <summary>
        /// Return  the SKU for a given SKUID.
        /// </summary>
        /// <param name="SKUID">SKUID id </param>
        /// <returns>List of KTSKUDetails</returns>
        KTSKUDetails GetSKUForSkuID(int dataOwnerID, long SkuID);

        /// <summary>
        /// Return  the SKU for a given ProductID.
        /// </summary>
        /// <param name="ProductID">ProductID</param>
        /// <returns>List of KTSKUDetails</returns>
        List<KTSKUDetails> GetSKUForProductID(int dataOwnerID, long ProductID);

        /// <summary>
        /// Return  the SKU for a given CompanyID.
        /// </summary>
        /// <param name="CompanyID">CompanyID</param>
        /// <returns>List of KTSKUDetails</returns>
        List<KTSKUDetails> GetSKUForCompanyID(int dataOwnerID, long CompanyID);
        #endregion [SKU]

        #region [Location]
        /// <summary>
        /// Return  the Location for a given DataOwnerID.
        /// </summary>
        /// <returns>List of KTLocationDetails</returns>
        List<KTLocationDetails> GetAllLocationDetails(int dataOwnerID);

        /// <summary>
        ///  Return  the Location for a given CategoryID.
        /// </summary>
        /// <param name="CategoryID">CategoryID</param>
        /// <returns>List of KTLocationDetails</returns>
        List<KTLocationDetails> GetAllLocationByCategory(int dataOwnerID, int CategoryID);

        /// <summary>
        ///  Return  the Location for a given LocationID.
        /// </summary>
        /// <param name="LocationID">LocationID</param>
        /// <returns>List of KTLocationDetails</returns>
        KTLocationDetails GetAllLocationByLocationID(int dataOwnerID, int LocationID);

        /// <summary>
        ///  Return  the Location for a given RFValue.
        /// </summary>
        /// <param name="RFValue">RFValue</param>
        /// <returns>List of KTLocationDetails</returns>
        KTLocationDetails GetAllLocationByRFvalue(int dataOwnerID, string RFValue);

         /// <summary>
        ///  Return  the SKUID for a given locationID.
        /// </summary>
        /// <param name="locationID">locationID</param>
        /// <returns>List of KTSKUlocationAssociation</returns>
        List<KTSKUlocationAssociation> GetSKUIDsforAssociatedLocation(int dataOwnerID, int locationID);


        /// <summary>
        ///  Return  the locationID for a given SKUID.
        /// </summary>
        /// <param name="SkuID">SkuID</param>
        /// <returns>List of KTSKUlocationAssociation</returns>
        List<KTSKUlocationAssociation> GetLocationIDsforAssociatedSKU(int dataOwnerID, long SkuID);
        
        
        /// <summary>
        ///  Return  the CategoryName for a given categoryID.
        /// </summary>
        /// <param name="CategoryId">CategoryId</param>
        /// <returns>Category Name in string</returns>
        string GetLocationCategoryByCategoryID(int dataOwnerID, int CategoryId);
        #endregion [Location]
       

        #region [LocationMonitor]

        /// <summary>
        ///  Return  the LocationMonitorCompProdSku for a given LocationId.
        /// </summary>
        /// <param name="LocationID">LocationId</param>
        /// <returns>List of KTLocationMonitor</returns>
         List<KTLocationMonitor > GetAllCompProdSkuCount(int dataOwnerID, int LocationID);

         /// <summary>
         ///  Return  the LocationMonitorItemDetais for a given LocationId and CompProdSkuID.
         /// </summary>
         /// <param name="LocationID">ID</param>
         /// <returns>List of KTItemDetails</returns>
         List<KTItemDetails> GetAllLocationMonitorItemDetails(int dataOwnerID, int LocationID, ItemCategory ItemCat, long ID);
        #endregion [LocationMonitor]

        /// <summary>
        /// Clears the cache and fills it again.
        /// </summary>
        void RefreshCache();
        KTSDCCacheStatus CacheStatus
        {
            get;
        }

        string LastRefreshResult
        {
            get;
        }
    }
    public enum KTSDCCacheStatus
    {
        /// <summary>
        /// cache has refreshed successfully
        /// </summary>
        Ready,

        /// <summary>
        /// cache failed to refresh
        /// </summary>
        Failed,

        /// <summary>
        /// cache is refreshing.
        /// </summary>
        Refreshing
    }
    
    
  }