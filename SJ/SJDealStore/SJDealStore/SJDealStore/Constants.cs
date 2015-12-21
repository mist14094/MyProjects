using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SJDealStore
{
    public class Constants
    {
        public string RFIDString = ConfigurationManager.ConnectionStrings["TrackerConnectionString"].ConnectionString;
        public string StoreID = "404";
        public string pr_UpdateDecommissionedItems = "pr_UpdateDecommissionedItems";
        public string pr_SelectProducts_OnRFID = "pr_SelectProducts_OnRFID";
        public string pr_GetProductDetailsForUPC = "pr_GetProductDetailsForUPC";
        public string pr_InsertProducts_OnSingleAssociation = "pr_InsertProducts_OnSingleAssociation";
        public string SalesIsDamaged = ConfigurationManager.AppSettings["SalesIsDamaged"].ToString();
        public string DeviceID = ConfigurationManager.AppSettings["DeviceID"].ToString();
    }
}