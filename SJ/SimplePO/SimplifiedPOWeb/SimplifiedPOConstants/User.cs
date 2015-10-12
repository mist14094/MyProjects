using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifiedPOConstants
{
    public class User
    {
        public int UserID { get; set; }
        public string  UserName { get; set; }
        public string Email{ get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public bool NeedsChange { get; set; }
        public int RoleID { get; set; }
        public string Status { get; set; }
        public string Role { get; set; }
        public string CombinedName { get; set; }

        public User()
        {
            Address = @"SMOKIN JOES   " + Environment.NewLine + "2293 SAUNDERS SETTLEMENT RD   " + Environment.NewLine + "SANBORN NY  " + Environment.NewLine + "14132   " + Environment.NewLine + "ATTN:";
        }

    }
}
