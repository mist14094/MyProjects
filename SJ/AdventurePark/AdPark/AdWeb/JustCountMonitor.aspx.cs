using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdBsnsLayer;

namespace AdWeb
{
    public partial class JustCountMonitor : System.Web.UI.Page
    {
        readonly AdBsnsLayer.Engine _engine = new AdBsnsLayer.Engine();
        readonly AdBsnsLayer.Devices _devices = new AdBsnsLayer.Devices();
        readonly AdBsnsLayer.JustOnceProcessor _justOnceProcessor = new JustOnceProcessor();
        protected void Page_Load(object sender, EventArgs e)
        {
            var user = Session["User"];
            var v = Request.QueryString["DeviceID"];
            if (!IsPostBack)
            {
                if (user == null)
                {
                    Response.Redirect("Login.aspx");
                }
            }
            if (v == null)
            {
                Response.Redirect("DeviceNotFound.aspx");
            }
            else
            {
               List<AdBsnsLayer.Devices> deviceListed = _devices.GetAllDevices().Where(devices => devices.DeviceID == int.Parse(v.ToString()) && devices.DeviceType == AdBsnsLayer.Devices._DeviceType.JustCount ).ToList();
                if (deviceListed.Any())
                {
                    lblTitle.Text = "Dashboard - " + deviceListed[0].DisplayName;
                    GridView1.DataSource = _justOnceProcessor.MonitorJustCount(deviceListed[0].DeviceTable);
                    GridView1.DataBind();
                    }
                else
                { Response.Redirect("DeviceNotFound.aspx"); }

            }

            // var asd = menu.GetAllTagActivities();

        }

     
     


    }
}