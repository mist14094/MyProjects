using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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
        public string GetAllUsers = "SELECT  [Sno],[FirstName],[LastName],[Address],[City],[State],[Country],[Zipcode],[ContactNumber],[EmailID],[DateOfBirth],[CreatedDate],[TagNumber] FROM [AdventurePark].[dbo].[UserDetails]";
        public string InsertUser = "InsertUser";
    }
}
