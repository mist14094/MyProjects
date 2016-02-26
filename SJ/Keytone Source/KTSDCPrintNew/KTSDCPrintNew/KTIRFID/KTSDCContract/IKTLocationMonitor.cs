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
    public interface IKTLocationMonitor
    {
        ///// <summary>
        /////  GetAllLocation
        ///// </summary>
        ///// <param name="userID"></param>
        ///// <param name="password"></param>
        ///// <returns></returns>
        //[OperationContract]
        //[FaultContract(typeof(KTSDSeviceException))]
        //List<KTLocationDetails> GetAllLocationMonitor(string userID, string password);

        /// <summary>
        ///  GetAllCompProdSku
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <param name="LocId"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        List<KTLocationMonitor> GetAllCompProdSkuCount(string userID, string password,int LocationID);

        /// <summary>
        ///  GetAllItemDetails
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <param name="LocId"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        List<KTItemDetails> GetAllItemDetails(string userID, string password, int LocationID, ItemCategory ItemCat, long ID, bool Locationmode);
    }
}
