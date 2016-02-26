using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Net.Security;
using KTone.Core.KTIRFID;
using KTone.WebServer;
using System.Collections.Generic;

namespace KTone.Core.KTIRFID
{
    [ServiceContract(Namespace = "http://www.keytonetech.com/KTSmartDC")]
    public interface IKPrinter
    {
        /// <summary>
        /// Item Will Insert After Print
        /// </summary>
        /// <param name="KTItemDetails"></param>
        /// <param name="locationId"></param>
        /// <param name="numOfCopies"></param>
        /// <param name="OperationType:"Insert","Update","Duplicate""></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        bool Print(KTItemDetails ItemDetails, int locationId, int numOfCopies, out string errorMsg, PrintOperation OperationType, string userID, string password);


        /// <summary>
        /// Location Will Update After Print
        /// </summary>
        /// <param name="LocationDetails"></param>
        /// <param name="locationId"></param>
        /// <param name="OperationType:""Update","Duplicate""></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        bool LocationPrint(KTLocationDetails LocationDetails, int locationId, int numOfCopies, out string errorMsg, LocationPrintOperation OperationType, string userID, string password);

        /// <summary>
        ///Return XML string
        /// </summary>
        /// <param name="Company"></param>
        /// <param name="Country"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        void GetLookupDB(out string Company, out string Country, string userID, string password);


        /// <summary>
        ///Return List of Itemstructure for skuID
        /// </summary>
        /// <param name="Company"></param>
        /// <param name="Country"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        List<KTItemDetails> GetItemsForBatchPrinting(string userID, string password,long SkuID,int NoOfItems,ItemType itemType);


    }
}
