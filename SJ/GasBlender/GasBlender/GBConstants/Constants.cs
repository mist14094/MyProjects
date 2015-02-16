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
            "SELECT [carrierID] ,[name] ,[tollNumber] ,[localContactName] ,[localContactNumber]  FROM [gasblend].[dbo].[carrierTBL]";

        public string InsertCarrier =
            "INSERT INTO carrierTBL ([name] ,[tollNumber] ,[localContactName] ,[localContactNumber] ) VALUES ('{0}','{1}','{2}','{3}')";

        public string UpdateCarrier = 
            "UPDATE carrierTBL SET Name='{0}',tollNumber='{1}',localContactName='{2}',localContactNumber='{3}' WHERE carrierID={4}";

        public string RemoveCarrier =
            "DELETE FROM [dbo].[carrierTBL] where [carrierID] ={0} ";

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
            "SELECT [trailerID] ,[trailerNumber] ,CAST ([compartment1Size]  AS FLOAT) compartment1Size ,CAST ([compartment2Size]  AS FLOAT) compartment2Size ,CAST ([compartment3Size]  AS FLOAT) compartment3Size ,CAST ([compartment4Size]  AS FLOAT) compartment4Size ,CAST ([compartment5Size]  AS FLOAT) compartment5Size ,[isactive]  FROM [gasblend].[dbo].[trailerTBL]";

        public string ReceiveTruckLoad =
            "INSERT INTO loadTBL (carrierID,refNum,loadType, trailerID, C1Type, C1Amount, C2Type, C2Amount, C3Type, C3Amount, C4Type, C4Amount, C5Type, C5Amount, sumRegular, sumSuper, sumEthanol,C1LocationID,C2LocationID,C3LocationID,C4LocationID,C5LocationID,driver,truck,note) VALUES ({0},'{1}','{2}',{3},'{4}',{5},'{6}',{7},'{8}',{9},'{10}',{11},'{12}',{13},{14},{15},{16},{17},{18},{19},{20},{21},'{22}','{23}','{24}' ) SELECT @@IDENTITY";

        public string InsertLineData =
            "INSERT INTO lineTBL (loadID,CType,CAmount,CLocationID,RAdd,SAdd,EAdd,compartment,combined,note) VALUES ({0},'{1}',{2},{3},{4},{5},{6},{7},'{8}','{9}')";

        public string UpdateSetupTable =
            "UPDATE setupTBL SET regularStored=regularStored + {0}, superStored=superStored + {1}, ethanolStored=ethanolStored + {2}";
    }
}
