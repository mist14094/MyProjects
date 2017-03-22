using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Services;
using AdBsnsLayer; 
namespace AdWeb
{
    /// <summary>
    /// Summary description for AdvPark
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AdvPark : System.Web.Services.WebService
    {
      

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string Process(int DeviceID, string TagNumber, string DeviceValue, string LoginID)
        {
            AdBsnsLayer.Engine engine = new AdBsnsLayer.Engine();
            return engine.Process(DeviceID, TagNumber, DeviceValue, LoginID);
        }

        [WebMethod]
        public void SmartWaiverIntegration()
        {
            AdBsnsLayer.SmartWaiverIntegration Integration = new SmartWaiverIntegration();
            using (WebClient client = new WebClient())
            {
                SmartWaiverIntegration.xml WaiverValues =
                    Integration.Deserialize(client.DownloadString(WebConfigurationManager.AppSettings["SmartWaiverLink"]));

                //   User.InsertUserWaiver();
            }
        }
    }
}
