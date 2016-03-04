using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Services;

namespace SJDealStore.Pages
{
    /// <summary>
    /// Summary description for SquareImport
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SquareImport : System.Web.Services.WebService
    {
        private DALayer _layer = new DALayer();
        private SquareSjApi Api = new SquareSjApi();

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }


        private string ThreadBeginImport()
        {
            //var result = Api.InsertProduct(txtCategoryName.Text, dr["MasterID"].ToString(),
            //                        WebUtility.HtmlDecode(dr["Descr"].ToString()), WebUtility.HtmlDecode(dr["UPCNumber"].ToString()),
            //                         WebUtility.HtmlDecode(dr["SJPrice"].ToString()).Replace("$", "").Replace(".", ""), WebUtility.HtmlDecode(dr["Quantity"].ToString()));

            DataTable dt = _layer.GetItemsToBeImportedToSquare();

            foreach (DataRow drDataRow in dt.Rows)
            {
                Api.InsertProduct(drDataRow["CategoryName"].ToString(), drDataRow["MasterID"].ToString(),
                    WebUtility.HtmlDecode(drDataRow["Descr"].ToString()),
                    WebUtility.HtmlDecode(drDataRow["UPCNumber"].ToString()),
                    WebUtility.HtmlDecode(drDataRow["SJPrice"].ToString()).Replace("$", "").Replace(".", ""),
                    WebUtility.HtmlDecode(drDataRow["Quantity"].ToString()));
            }
            return "";

        }

                [WebMethod]
        public string BeginImport()
        {
            Thread t = new Thread(o=> { ThreadBeginImport(); });
            t.Start();
            return "Import Thread Started";
        }
    }
}
