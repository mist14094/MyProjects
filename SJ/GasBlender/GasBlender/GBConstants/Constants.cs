using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace GBConstants
{
    public class Constants
    {
        public static string DefaultString = ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString;
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

        public string GetAllTrailer = "SELECT [trailerID] ,[trailerNumber] ,[compartment1Size] ,[compartment2Size] ,[compartment3Size] ,[compartment4Size] ,[compartment5Size] ,[isactive]  FROM [trailerTBL] ";

        public string UpdateTrailer =
            "UPDATE trailerTBL SET trailerNumber='{0}', compartment1Size={1}, compartment2Size={2}, compartment3Size={3}, compartment4Size={4}, compartment5Size={5} WHERE trailerID='{6}'";

        public string AddTrailer =
            "INSERT INTO trailerTBL (trailerNumber, compartment1Size, compartment2Size, compartment3Size, compartment4Size, compartment5Size)  VALUES ('{0}',{1},{2},{3},{4},{5} )";
        
        public string RemoveTrailer =
            "UPDATE trailerTBL SET isactive = 'F' WHERE trailerID={0}";

        public string GetSetup =
            "SELECT TOP 1 [setupTBL] ,CAST ([regularCapacity] AS FLOAT)[regularCapacity] ,CAST([superCapacity] AS FLOAT)[superCapacity],CAST([ethanolCapacity] AS FLOAT)[ethanolCapacity] ,CAST([regularStored] AS FLOAT)[regularStored] ,CAST([superStored] AS FLOAT)[superStored] ,CAST([ethanolStored] AS FLOAT)[ethanolStored],CAST((regularStored/regularCapacity)*100 AS decimal(10,1)) AS ResultRegular,CAST((superStored/superCapacity)*100 AS decimal(10,1)) AS ResultSuper,CAST((ethanolStored/ethanolCapacity)*100 AS decimal(10,1)) AS ResultEthanol  FROM [setupTBL]";

        public string UpdateStoredTank =
            "UPDATE setupTBL SET regularStored={0}, superStored={1}, ethanolStored={2}";

        public string UpdateTankSize =
            "UPDATE setupTBL SET regularCapacity={0}, superCapacity={1}, ethanolCapacity={2}";

        public string GetLocation =
            "SELECT [locationID] ,[locationName] ,[regularCapacity] ,[superCapacity] ,[ethanolCapacity] ,[regularStored] ,[superStored] ,[ethanolStored] ,[isactive] ,[address] ,[city] ,[state] ,[zip],address+','+city+','+state+',' AS CombinedAddress  FROM [locationTBL]";

        public string InsertLocation =
            "INSERT INTO locationTBL (locationName,address,city,state,zip) VALUES ('{0}','{1}','{2}','{3}','{4}')";

        public string UpdateLocation =
            "UPDATE locationTBL SET locationName='{0}',address='{1}',city='{2}',state='{3}',zip='{4}' WHERE locationID={5}";

        public string RemoveLocation =
            "UPDATE locationTBL SET isactive = 'F' WHERE locationID={0}";

        public string GetCarrier =
            "SELECT [carrierID] ,[name] ,[tollNumber] ,[localContactName] ,[localContactNumber]  FROM [carrierTBL]";

        public string InsertCarrier =
            "INSERT INTO carrierTBL ([name] ,[tollNumber] ,[localContactName] ,[localContactNumber] ) VALUES ('{0}','{1}','{2}','{3}')";

        public string UpdateCarrier = 
            "UPDATE carrierTBL SET Name='{0}',tollNumber='{1}',localContactName='{2}',localContactNumber='{3}' WHERE carrierID={4}";

        public string RemoveCarrier =
            "DELETE FROM [carrierTBL] where [carrierID] ={0} ";

        public string GetTractor =
            "SELECT [tractorID] ,[name] ,[isactive] FROM [tractorTBL]";

        public string InsertTractor =
            "INSERT INTO tractorTBL (name) values ('{0}')";

        public string UpdateTractor =
            "UPDATE [tractorTBL] SET [name] ='{0}'where tractorID ={1}";

        public string RemoveTractor =
            "UPDATE tractorTBL SET isactive = 'F' WHERE tractorID={0}";

        public string GetDriver =
           "SELECT [driverID] ,[name] ,[isactive] FROM [DriverTBL]";

        public string InsertDriver =
            "INSERT INTO DriverTBL (name) values ('{0}')";

        public string UpdateDriver =
            "UPDATE [DriverTBL] SET [name] ='{0}'where driverID ={1}";

        public string RemoveDriver =
            "UPDATE DriverTBL SET isactive = 'F' WHERE driverID={0}";

        public string GetCastedTruck =
            "SELECT [trailerID] ,[trailerNumber] ,CAST ([compartment1Size]  AS FLOAT) compartment1Size ,CAST ([compartment2Size]  AS FLOAT) compartment2Size ,CAST ([compartment3Size]  AS FLOAT) compartment3Size ,CAST ([compartment4Size]  AS FLOAT) compartment4Size ,CAST ([compartment5Size]  AS FLOAT) compartment5Size ,[isactive]  FROM [trailerTBL]";

        public string ReceiveTruckLoad =
            "INSERT INTO loadTBL (carrierID,refNum,loadType, trailerID, C1Type, C1Amount, C2Type, C2Amount, C3Type, C3Amount, C4Type, C4Amount, C5Type, C5Amount, sumRegular, sumSuper, sumEthanol,C1LocationID,C2LocationID,C3LocationID,C4LocationID,C5LocationID,driver,truck,note,stamp) VALUES ({0},'{1}','{2}',{3},'{4}',{5},'{6}',{7},'{8}',{9},'{10}',{11},'{12}',{13},{14},{15},{16},{17},{18},{19},{20},{21},'{22}','{23}','{24}','{25}' ) SELECT @@IDENTITY";

        public string InsertLineData =
            "INSERT INTO lineTBL (loadID,CType,CAmount,CLocationID,RAdd,SAdd,EAdd,compartment,combined,note) VALUES ({0},'{1}',{2},{3},{4},{5},{6},{7},'{8}','{9}')";

        public string UpdateSetupTable =
            "UPDATE setupTBL SET regularStored=regularStored + {0}, superStored=superStored + {1}, ethanolStored=ethanolStored + {2}";

        public string UpdateAfterSetupTable =
            "UPDATE setupTBL SET regularStored=regularStored - {0}, superStored=superStored - {1}, ethanolStored=ethanolStored - {2}    ";

        public string GetBOL =
            "GetBOL";

        public string BOLLog =
            "BOLLog";

        public string LoadTBLData =
            "SELECT *  FROM [loadTBL] LEFT OUTER JOIN Linetbl ON loadTBL.loadID = lineTBL.loadID WHERE loadTBL.loadID={0}";

        public string SelectLoadTBL =
            "SELECT *  FROM [loadTBL] where loadID= {0}";

        public string DeleteLoadTBL =
            @"UPDATE setupTBL SET regularStored=regularStored-(SELECT ISNULL(sum(Radd),0) as sumRadd FROM linetbl WHERE loadID = {0}), 
superStored=superStored-(SELECT ISNULL(sum(Sadd),0) as sumSadd FROM linetbl WHERE loadID = {0}), 
ethanolStored=ethanolStored-(SELECT ISNULL(sum(Eadd),0) as sumEadd FROM linetbl WHERE loadID = {0})
DELETE FROM loadTBL where loadid={0}
DELETE FROM lineTBL where loadid={0}";
 
//        public string DeleteLoadTBL =
//            @"UPDATE setupTBL SET regularStored=regularStored-(SELECT sum(Radd) as sumRadd FROM linetbl WHERE loadID = {0} group by loadID), 
//              superStored=superStored-(SELECT  sum(sadd) as sumEadd FROM linetbl WHERE loadID = {0} group by loadID), 
//              ethanolStored=ethanolStored-(SELECT  sum(Eadd) as sumEadd FROM linetbl WHERE loadID = {0 }group by loadID)
//              DELETE FROM loadTBL where loadid={0}
//              DELETE FROM lineTBL where loadid={0}";
        public string GetLoad = "SELECT [loadID],[refNum],[stamp]  FROM [loadTBL] ORDER BY loadID DESC";

        public string UpdateLoadTBL = @"UPDATE [loadTBL] SET [refNum] = '{0}',[loadType] = '{1}',[trailerID] = '{2}',[C1Type] = '{3}',[C1Amount] = '{4}',[C2Type] = '{5}',[C2Amount] = '{6}'
                                        ,[C3Type] = '{7}',[C3Amount] = '{8}',[C4Type] = '{9}',[C4Amount] = '{10}',[C5Type] = '{11}',[C5Amount] = '{12}',[sumRegular] = '{13}',[sumSuper] = '{14}',[sumEthanol] = '{15}'
                                        ,[C1LocationID] = '{16}',[C2LocationID] = '{17}',[C3LocationID] = '{18}',[C4LocationID] = '{19}',[C5LocationID] = '{20}',[driver] ='{21}',[truck] = '{22}'
                                        ,[carrierID] = '{23}',[note] = '{24}' WHERE loadID='{25}'";

        public string UpdateLineTBL = "UPDATE lineTBL set CType='{0}',CAmount={1},CLocationID={2},RAdd={3},SAdd={4},EAdd={5},combined='{6}'  WHERE loadid={7} and compartment = {8}";

        public string InsertUser =
            "INSERT INTO [userTBL]([Username],[Password],[Name] ,[Email] ,[IsAdmin] ,[DateCreated]) VALUES (@username, @Password,@Name, @Email, @IsAdmin, @DateCreated)";

        public string CheckUserName =
            "SELECT [ID] ,[Username] ,[Password] ,[Email] ,[IsAdmin],[Name] ,[DateCreated]  FROM [userTBL] WHERE Username=@Username AND Password = @Password";

        public string EditUser =
            "UPDATE [userTBL]   SET [Username] = @Username,[Name] = @Name ,[Email] = @Email ,[IsAdmin] = @IsAdmin WHERE id = @id";

        public string ChangePassword =
            "UPDATE [userTBL]   SET [Password] = @Password WHERE id = @id";

        public string DeleteUser =
            "DELETE FROM [userTBL] WHERE  id = @id";

        public string GetUser =
            "SELECT [ID] ,[Username],[Name] ,[Email] ,[IsAdmin] ,[DateCreated]  FROM userTBL order by ID";

        public string InsertLog =
            "INSERT INTO [logTBL] ([UserID] ,[Action] ,[Page] ,[DateCreated]) VALUES (@UserID, @Action, @Page, @DateCreated)";

        public string DeleteLine =
            "DELETE FROM [lineTBL] WHERE loadID={0}";
    }
}
