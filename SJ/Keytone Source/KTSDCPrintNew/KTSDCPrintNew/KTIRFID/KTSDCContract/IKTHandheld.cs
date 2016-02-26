using System;
using System.Collections.Generic;
using System.Collections;
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
    public interface IKTHandheld
    {

        /// <summary>
        /// Returns the ProductSkuName and Listof CustomerUniqueId based on List of RFTAGID
        /// </summary>
        /// <param name="RFTAGID"> </param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        Dictionary<string, List<string>> GetSKUCount(List<string> lstRFTAGID, string userID, string password);
        
        /// <summary>
        /// Update Location and time for RFTAGID
        /// </summary>
        /// <param name="RFTAGID"> </param>
        /// <param name="LocationId"> </param>
        /// <param name="Timestamp"> </param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        bool UpdateLocation(List<string> RFTAGID, int LocationId, DateTime TimeStamp,string userID, string password);
                

        /// <summary>
        /// Get Item User Memory Data
        /// </summary>
        /// <param name="ItemStructre"> </param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        byte[] ItemUserMemoryData(string userID, string password,KTItemDetails Itemdtails);


        /// <summary>
        /// Returns Server DateTime
        /// </summary>
        /// 
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        void ServerDateTime(string userID, string password, out string serverTime, out TimeZoneInfo timezone);


        /// <summary>
        /// Returns Server DateTime
        /// </summary>
        /// 
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        void ServerDateTime_New(string userID, string password, out string serverTime, out int Offset);
    }
}
