using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace AdConstants
{
   
    public class SqlConstants
    {
        public static string DefaultString = ConfigurationManager.ConnectionStrings["AdventurePark"].ConnectionString;
        public string DefaultConnectionString
        {
            get
            {
                return DefaultString;
            }
            set
            {
                DefaultString = value;
            }
        }
        public string GetAllUsers = "SELECT  [Sno],[FirstName],[LastName],[Address],[City],[State],[Country],[Zipcode],[ContactNumber],[EmailID],[DateOfBirth],[CreatedDate],[TagNumber] FROM [AdventurePark].[dbo].[adv_UserDetails]";
        public string InsertUser = "InsertUser";
        public string CheckLogin = "CheckLogin";
        public string InsertUserLog = "InsertUserLog";
        public string InsertInternalLog = "InsertInternalLog";
        public string GetAllActivitiesMenu = "SELECT * FROM AdventurePark.dbo.adv_ActivitiesMenu";
        public string GetAllTagActivities  = "SELECT * FROM [AdventurePark].[dbo].[adv_ActivitiesForTag]";
        public string GetTagDetails = "SELECT * FROM [dbo].[adv_ActivitiesForTag] where TagNumber ='{0}'";
        public string UpdateActivitiesForTag = "UpdateActivitiesForTag";
        public string InsertLogMessage = "INSERT INTO [dbo].[adv_ActivityTagLog]([TagNumber],[Message]) VALUES ('{0}','{1}')";
        public string GetAllLogs = "SELECT * FROM [dbo].[adv_ActivityTagLog]";
        public string GetLogsforTag = "SELECT * FROM [dbo].[adv_ActivityTagLog] where [TagNumber]='{0}'";
        public string GetAllDevices = "SELECT * FROM [AdventurePark].[dbo].[adv_Devices]";
        public string InsertJustOnceValue = "INSERT INTO [dbo].[{0}]([TagNumber],[DeviceID],[LoginID])VALUES('{1}',{2},{3})";
        public string UseTagForActivity = "UPDATE [dbo].[adv_ActivitiesForTag] SET [{0}] = [{0}]-1 WHERE TAGNUMBER ='{1}'";

        public string InsertEngineLog =
            "INSERT INTO [dbo].[adv_DeviceValues]([DeviceID],[TagNumber],[DeviceValue],[LoginID]) VALUES({0},'{1}','{2}','{3}')";

        public string InsertCountInAndOut = "INSERT INTO [dbo].[{0}]([TagNumber],[{1}]) VALUES('{2}',GETDATE())";
        public string SelectCountInAndOut = "SELECT * FROM [dbo].[{0}]";
        public string SelectCountInAndOutWithTagNumber = "SELECT * FROM [dbo].[{0}] WHERE Tagnumber='{1}'";
        public string UpdateCountInAndOut_Out = "UPDATE [dbo].[{0}] SET {1} = Getdate(),[TotalDurationInMinutes]=( SELECT DATEDIFF(MINUTE, {3}, GETDATE())) where [Sno]={2};   SELECT * from {0} where SNO={2} ";

        public string InsertCountAndExpire = "INSERT INTO [dbo].[{0}]([TagNumber],[{1}]) VALUES('{2}',GETDATE())";
        public string SelectCountAndExpire = "SELECT * FROM [dbo].[{0}]";
        public string SelectCountAndExpireWithTagNumber = "SELECT* FROM [dbo].[{0}] WHERE Tagnumber='{1}'";
        public string UpdateCountAndExpire_Out = "UPDATE [dbo].[{0}] SET {1} = Getdate(),[TotalDurationInMinutes]=( SELECT DATEDIFF(MINUTE, {3}, GETDATE())) where [Sno]={2};   SELECT * from {0} where SNO={2} ";
        public string UpdateCountAndExpire_UseTagForActivity = "UPDATE [dbo].[adv_ActivitiesForTag] SET [{0}] = {2} WHERE TAGNUMBER ='{1}'";

        public string SelectCountAndWait = "SELECT * FROM [dbo].[{0}]";
        public string SelectCountAndWaitWithTagNumber = "SELECT * FROM [dbo].[{0}]";
        public string InsertCountAndWaitCounter = "INSERT INTO [dbo].[{0}]([TagNumber]) VALUES('{1}')";
        public string UpdateExpiredCountAndWait = "UPDATE [{0}] SET [Value] =0,[CurrentState]='Expired' WHERE  DATEDIFF(ss,[CreatedDate],GETDATE())>60";
        public string UpdateExpiredCountAndWait_Out = "UPDATE [{0}] SET [Value] ={1},[CurrentState]='Captured' WHERE [SNO]={2}";

        public string InsertThrowLeaderBoard =
            "INSERT INTO [dbo].[adv_ThrowLeaderBoard]([DeviceID] ,[TagNumber] ,[Value])VALUES({0},'{1}',{2})";

        public string SelectThrowLeaderBoard =
           "SELECT * from  [dbo].[adv_ThrowLeaderBoard]";

        public string InsertEngineLogWithResults =
            "INSERT INTO [dbo].[adv_EngineLog]([DeviceID],[TagNumber],[DeviceValue],[LoginId],[Message]) VALUES( {0},'{1}','{2}','{3}','{4}')";

        public string MonitorJustCount =
            "SELECT FirstName,dbo.[{0}].CreatedDate  FROM [{0}] INNER JOIN dbo.adv_UserDetails ON dbo.adv_UserDetails.TagNumber = dbo.[{0}].TagNumber ORDER BY dbo.[{0}].CreatedDate desc";

        public string GetUserDetailsWithWaiver = "SELECT * FROM [adv_UserDetailsWithWaiver]";

        public string updateUserDetailsWithWaiver =
            " UPDATE[dbo].[adv_UserDetailsWithWaiver] SET[IsImported] = '1' WHERE sno = '{0}'";

        public string InsertUserWaiver =
            "InsertUserWaiver";
    }
}
