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

        public string GetAllTrailer = "SELECT [trailerID] ,[trailerNumber] ,[compartment1Size] ,[compartment2Size] ,[compartment3Size] ,[compartment4Size] ,[compartment5Size] ,[isactive]  FROM [trailerTBL]";

    }
}
