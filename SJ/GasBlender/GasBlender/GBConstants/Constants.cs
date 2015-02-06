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
    }
}
