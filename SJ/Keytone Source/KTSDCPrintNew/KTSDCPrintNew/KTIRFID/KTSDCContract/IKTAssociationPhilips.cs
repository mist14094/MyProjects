using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Net.Security;
using KTone.Core.KTIRFID;
using KTone.WebServer;


namespace KTone.Core.KTIRFID
{
   [ServiceContract(Namespace = "http://www.keytonetech.com/KTSmartDC")]
    public interface IKTAssociationPhilips
    { 
        /// <summary>
        /// Get SKUIDs for Associated Location
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        List<KTSKUlocationAssociation> GetSKUIDsforAssociatedLocation(string userID, string password, int locationID);


        /// <summary>
        /// Get LocationIds for Associated SKU
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        List<KTSKUlocationAssociation> GetLocationIDsforAssociatedSKU(string userID, string password, long SkuID);


        /// <summary>
        /// Get All ItemDetails for Products and SKU
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        List<KTItemDetails> GetItemDetailsForProductandSKU(string userID, string password );

        /// <summary>
        /// Get All ItemDetails for RFTAGID
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        KTItemDetails GetItemDetailsForRFTAGIDForAssociation(string userID, string password, string RFTAGID);
    }
}
