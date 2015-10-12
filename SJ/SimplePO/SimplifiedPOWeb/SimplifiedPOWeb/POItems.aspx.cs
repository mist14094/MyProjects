using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SimplifiedPOWeb
{
    public partial class POItems : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string testString = "vivek@123";
            byte[] asciiBytes = ASCIIEncoding.ASCII.GetBytes(testString);
            byte[] hashedBytes = MD5CryptoServiceProvider.Create().ComputeHash(asciiBytes);
            string hashedString = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}