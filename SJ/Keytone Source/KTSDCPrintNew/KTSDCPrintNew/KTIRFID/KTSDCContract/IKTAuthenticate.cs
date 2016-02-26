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
   public interface IKTAuthenticate
    {
        /// <summary>
        /// Validates the user
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <param name="userName"></param>
        /// <param name="roleID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        bool ValidateUser(string userID, string password, out string userName, out Int32 roleID, out Int32 dataOwnerID, out string dataOwnerName);

         /// <summary>
        /// Validate User HH
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <param name="MacAddress"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        bool ValidateUserHH(string userID, string password, string MacAddress,out int HHID);

        /// <summary>
        /// Returns the menus accessible for an application based on roleID
        /// </summary>
        /// <param name="applicationName"> Exe Name</param>
        /// <param name="roleID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        List<string> GetAccessibleMenus(string userID, string password, string applicationName, Int32 roleID);

        /// <summary>
        /// Based on the application it will return all the menus and role for whcih the menu is accessible.
        /// </summary>
        /// <param name="appName"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        List<MenuAccessData> GetAccessibleMenuDetails(string userID, string password, string appName);

        /// <summary>
        /// Returns all the users defined in the system
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        List<UserData> GetAllUser(string userID, string password);


        /// <summary>
        /// Return product name of dataowner id
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <param name="productName"></param>
        /// <returns></returns>
        //[OperationContract]
        //[FaultContract(typeof(KTSDSeviceException))]
        //void GetProductByDataOwnerID(string userID, string password, out string productName);

        /// <summary>
        /// Return window application setting by userid and application name.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <param name="applicationName"></param>
        /// <param name="xmlSettings"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        bool GetWinAppSettingByUserID(string userID, string password, string applicationName, out string xmlSettings);

        /// <summary>
        /// Update windows application setting by userid and application name.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <param name="xmlSettings"></param>
        /// <param name="applicationName"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        bool UpdateWinAppSettingByUserID(string userID, string password, string xmlSettings, string applicationName);

        [OperationContract]
        [FaultContract(typeof(KTSDSeviceException))]
        bool TestConnection();
    }
}
