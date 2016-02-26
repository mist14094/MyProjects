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
  public interface IKTCompProdSKU
    {
        /// <summary>
        ///  GetAllCompanies
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
       [OperationContract]
       [FaultContract(typeof(KTSDSeviceException))]
       List<KTCompanyDetails> GetAllCompanies(string userID, string password);
        
        /// <summary>
        ///  GetAllProducts
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
       [OperationContract]
       [FaultContract(typeof(KTSDSeviceException))]
       List<KTProductDetails> GetAllProducts(string userID, string password);

       /// <summary>
       ///  GetAllProductsByCompanyID
       /// </summary>
       ///  <param name="userID"></param>
       /// <param name="password"></param>
       /// <param name="CompanyId"></param>
       /// <returns></returns>
       [OperationContract]
       [FaultContract(typeof(KTSDSeviceException))]
       List<KTProductDetails> GetAllProductsByCompanyID(string userID, string password,int CompanyId);

       /// <summary>
       ///  GetAllSKU
       /// </summary>
       /// <param name="userID"></param>
       /// <param name="password"></param>
       /// <returns></returns>
       [OperationContract]
       [FaultContract(typeof(KTSDSeviceException))]
       List<KTSKUDetails> GetAllSKU(string userID, string password);

       /// <summary>
       ///  GetAllSKUByProductID
       /// </summary>
       /// <param name="ProductID"></param>
       /// <param name="userID"></param>
       /// <param name="password"></param>
       /// <returns></returns>
       [OperationContract]
       [FaultContract(typeof(KTSDSeviceException))]
       List<KTSKUDetails> GetAllSKUByProductID(string userID, string password,long ProductID);

       /// <summary>
       /// GetAllSKUByCompanyID
       /// </summary>
       /// <param name="CompanyId"></param>
       /// <param name="userID"></param>
       /// <param name="password"></param>
       /// <returns></returns>
       [OperationContract]
       [FaultContract(typeof(KTSDSeviceException))]
       List<KTSKUDetails> GetAllSKUByCompanyID(string userID, string password, int CompanyId);


       /// <summary>
       /// GetCustomColumnInfo
       /// </summary>
       /// <param name="CategoryId"></param>
       /// <param name="DataOwnerID"></param>
       /// <returns></returns>
       [OperationContract]
       [FaultContract(typeof(KTSDSeviceException))]
       List<CustomFeildInfo> GetCustomColumnInfo(int categoryID,string userID, string password);



    }
}
