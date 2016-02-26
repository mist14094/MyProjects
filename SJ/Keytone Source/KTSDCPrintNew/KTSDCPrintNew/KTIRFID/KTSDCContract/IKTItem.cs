using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Net.Security;
using KTone.Core.KTIRFID;
using KTone.WebServer;
using System.Data;

namespace KTone.Core.KTIRFID
{
    [ServiceContract(Namespace = "http://www.keytonetech.com/KTSmartDC")]
    public interface IKTItem
    {

        /// <summary>
        ///  GetItemDetails
        /// </summary>
        /// <param name="CustomerUniqueID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        KTItemDetails GetItemDetailsForCustomerID(string CustomerUniqueID, string userID, string password);
       
        
        /// <summary>
        ///  GetItemDetails
        /// </summary>
        /// <param name="RFTAGID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        KTItemDetails GetItemDetailsForRFTAGID(string RFTAGID, string userID, string password);

        /// <summary>
        /// Get All Items for HH
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        int FillAllItemsHH(string userID, string password );

         /// <summary>
        /// Get All Items for HH By Block
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        DataTable FillAllItemsbyBlockHH(string userID, string password, int Count, int Block);




        /// <summary>
        ///  List of ItemDetails
        /// </summary>
        /// <param name="List<string>RFTAGIDs"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        List<KTItemDetails> GetItemDetailsForRFTAGIDs(List<string> RFTAGIDs, string userID, string password);

        /// <summary>
        ///  List of ItemDetails
        /// </summary>
        /// <param name="List<string>RFTAGIDs"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        Dictionary<string, List<KTItemDetails>> GetItemDetailsForBin_Part(string BinCat, string PartNumber, string userID, string password);

        /// <summary>
        ///  List of BinTape Quantity
        /// </summary>
        /// <param name="List<string>RFTAGIDs"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        Dictionary<string, List<KTBinQuantity>> GetItemDetailsForBin_Part_New(string BinCat, string PartNumber, string userID, string password);
    }
}
