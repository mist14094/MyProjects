using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AUBusinessAccess;

namespace AutomatedEmail
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
         AUBusinessAccess.JarvisBusinessLr  lr = new JarvisBusinessLr();
       Response.Write( lr.CostcoSaleseport(readFile("~/" + "CostoSales.html"), DateTime.Now.AddDays(-7), DateTime.Now, "0", "4"));
        }
        public string readFile(string FilePath)
        {
            string path = HttpContext.Current.Server.MapPath(FilePath);
            return System.IO.File.ReadAllText(path);
        }
    }
}