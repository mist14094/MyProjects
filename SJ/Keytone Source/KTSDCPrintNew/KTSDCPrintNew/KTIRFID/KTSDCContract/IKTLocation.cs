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
    public interface IKTLocation
    { 
        /// <summary>
        ///  GetAllLocation
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        List<KTLocationDetails> GetAllLocation(string userID, string password);
        
        /// <summary>
        ///  GetAllLocationByCategory
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        List<KTLocationDetails>GetAllLocationByCategory(string userID, string password,int CategoryID);

        /// <summary>
        ///  Get Item Type Printer
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        List<KTLocationDetails> GetItemPrinter(string userID, string password);


        /// <summary>
        /// Get Location Type Printer
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        List<KTLocationDetails> GetLocationPrinter(string userID, string password);

    }
}
