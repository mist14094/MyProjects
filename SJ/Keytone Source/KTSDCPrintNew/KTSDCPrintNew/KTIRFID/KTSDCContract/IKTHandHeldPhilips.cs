using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using KTone.WebServer;
using System.Data;

namespace KTone.Core.KTIRFID
{
    [ServiceContract(Namespace = "http://www.keytonetech.com/KTSmartDC")]
    public interface IKTHandHeldPhilips
    {
        /// <summary>
        /// Update ItemDetails , Time , Status for RFTAGID
        /// </summary>
        /// <param name="RFTAGID"> </param>
        /// <param name="LocationId"> </param>
        /// <param name="Timestamp"> </param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        bool ItemsPutAway(List<string> RFTAGID, int LocationId, DateTime TimeStamp, string userID, string password,int HHID);

        /// <summary>
        /// Get AuditInstances and AuditName
        /// </summary>       
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        KTAuditInstance[] GetAuditInstances(string userID, string password);

        /// <summary>
        /// Get AuditInstance details 
        /// </summary>       
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        KTAuditInstanceDetails GetAuditInstanceDetails(long CCInstanceID, string userID, string password);

        /// <summary>
        /// Update Cycle Count instance details
        /// </summary>       
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        bool UpdateCycleCountInstanceDetails(List<string> RFTAGIDs, int LocationID, long CCInstanceID , int HHID , DateTime TimeStamp, string userID, string password);

        /// <summary>
        /// Returns ItemCount for location on ProductName
        /// </summary>
        /// 
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        List<KTLocationItemCount> GetItemCountForLocation(int Type, string TypeValue, string userID, string password, out string ProductName);
        //DataTable GetItemCountForLocation(int Type, string TypeValue, string userID, string password, out string ProductName );



        /// <summary>
        /// Returns Setting for Lookup
        /// </summary>
        /// 
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        void GetLookupSetting(out string Settings, string userID, string password);

        /// <summary>
        /// Update Setting for Lookup
        /// </summary>
        /// 
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        bool UpdateLookup(string Name,string Data, string userID, string password);


        /// <summary>
        ///  GetItemDetails
        /// </summary>
        /// <param name="CustomerUniqueID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        KTItemDetails GetItemDetailsForCustomerID_Philips(string CustomerUniqueID, string userID, string password);

        /// <summary>
        ///  GetAllLocation
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        List<KTLocationDetails> GetAllLocation_Philips(string userID, string password);


        /// <summary>
        ///  Count of ItemDetails
        /// </summary>
        /// <param name="List<string>RFTAGIDs"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        long GetItemDetailsCountForRFTAGIDs(List<string> RFTAGIDs, string userID, string password);

            /// <summary>
        /// Gate Operation on Items
        /// </summary>       
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        bool GateOperationOnItems(List<string> IDs, int LocationID, ItemState ItemStatus , DateTime TimeStamp, string userID, string password);
    
    }
}
