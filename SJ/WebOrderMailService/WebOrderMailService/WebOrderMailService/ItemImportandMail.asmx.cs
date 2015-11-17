using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Services;

namespace WebOrderMailService
{
    /// <summary>
    /// Summary description for ItemImportandMail
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ItemImportandMail : System.Web.Services.WebService
    {
        private BusinessObject _businessObject = new BusinessObject();
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public string Import()
        {
            if (_businessObject != null)
            {
                Thread t = new Thread(new ThreadStart( _businessObject.SendEmail));
                t.Start();
                return "Import Thread Started";
            }
            else
            {
                return null;
            }
        }
    }
}
