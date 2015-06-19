using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AUBusinessAccess;
using System.Data;
namespace AutomatedEmail
{
    public partial class WebForm2 : System.Web.UI.Page
    {
      AUBusinessAccess.EPMBusinessLr BL = new  AUBusinessAccess.EPMBusinessLr();
      AUBusinessAccess.JarvisBusinessLr JL = new AUBusinessAccess.JarvisBusinessLr();
        protected void Page_Load(object sender, EventArgs e)
        {
           //public static 
               DataTable dt = new DataTable();
            dt = BL.SalesTransationsGroupedByItem(DateTime.Now.AddDays(-2), DateTime.Now, 10);
            DataTable catagoryWithImage = JL.GetAllCatagoryImageName();

            dt.Columns.Add("Image");
            foreach (DataRow variable in dt.Rows)
            {
                var imageName = catagoryWithImage.Select("Barcode = '"+ variable["UPC"].ToString()+"'");
                if (imageName.Count() > 0)
                {
                    variable["Image"] = " <img src=\"http://jarvis.smokinjoe.com/images/"+imageName[0]["Family"]+".jpg" +"\" style=\"width:42px;height:42px;border:0\">";
                }
                else
                {
                    
                }
              
            }
            dt.Columns.Remove("UPC");
            dt.Columns.Remove("SKU");
            dt.Columns.Remove("AvgCost");
            dt.Columns.Remove("AvgPrice");
            dt.Columns.Remove("GrossProfit");
            dt.Columns.Remove("TotalCost"); dt.Columns.Remove("Store");
   
           
            string strreadFile = readFile("/" + "HtmlPage.html");
            dt.DefaultView.Sort = "Qty asc";
            string tab = TableCreator(dt);

            strreadFile = strreadFile.Replace("tableHeader", tab);
            Response.Write(strreadFile);
        }

        public string TableCreator(DataTable dt)
        {
            string table = "";
            string tableStart = "<table cellspacing='0'>";
            string tableHeader = TableHeader(dt);
            string TRow = "";
            bool evenbl = true;
            foreach (DataRow rows in dt.Rows)
            {
                TRow = TRow + TableRow(rows, evenbl);
                evenbl = !evenbl;
            }
            string tableEnd = "    </table>";

            table = tableStart + tableHeader + TRow + tableEnd;
            return table;
        }
        public string TableRow(DataRow dr, bool Even)
        {
            string TableRow;
            if (Even)
            {
                TableRow = "<tr class='even'>";
            }
            else
            {
                TableRow = "<tr>";
            }
            foreach (var ItemStr in dr.ItemArray)
            {
                TableRow = TableRow + "<td>" + ItemStr.ToString() + "</td>";
            }
            return TableRow + "</tr>";
        }
        public string TableHeader(DataTable dt)
        {
            string Header = "";
            Header = " <tr>";
            foreach (DataColumn column in dt.Columns)
            {
                Header = Header + "  <th> " + column.ColumnName + " </th>";
            }
            Header = Header + "</tr>";
            return Header;
        }
        public string readFile(string FilePath)
        {
            string path = HttpContext.Current.Server.MapPath(FilePath);
            return System.IO.File.ReadAllText(path);
        }
    }
}