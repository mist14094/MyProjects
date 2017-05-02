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
    public partial class CountExpire : System.Web.UI.Page
    {
        readonly AdBsnsLayer.Engine _engine = new AdBsnsLayer.Engine();
        readonly AdBsnsLayer.Devices _devices = new AdBsnsLayer.Devices();
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
                var AllDevices = _devices.GetAllDevices();
                List<AdBsnsLayer.Devices> deviceListed = AllDevices.Where(devices => devices.DeviceID == int.Parse(v.ToString()) && (devices.DeviceType == AdBsnsLayer.Devices._DeviceType.CountExpire_In || devices.DeviceType == AdBsnsLayer.Devices._DeviceType.CountExpire_Out)).ToList();
                if (deviceListed.Any())
                {
                    lblTitle.Text = "Welcome to " + deviceListed[0].DisplayName;
                    lblDeviceCounter.Text = AllDevices.Where(devices => devices.DeviceID == int.Parse(v.ToString()) && (devices.DeviceType == AdBsnsLayer.Devices._DeviceType.CountExpire_In)).ToList()[0].DeviceID.ToString();
                    lblDeviceSpeed.Text = AllDevices.Where(devices => (devices.DeviceType == AdBsnsLayer.Devices._DeviceType.CountExpire_Out) && (devices.DisplayName == deviceListed[0].DisplayName)).ToList().ToList()[0].DeviceID.ToString();

                    txtTagNumber.Focus();
                }
                else
                { Response.Redirect("DeviceNotFound.aspx"); }

            }

            // var asd = menu.GetAllTagActivities();

        }

     
        protected void btnScan_Click(object sender, EventArgs e)
        {
            CheckTags(txtTagNumber.Text);
        }
        protected void txtTagNumber_TextChanged(object sender, EventArgs e)
        {
            CheckTags(txtTagNumber.Text);
        }

        private void CheckTags(string tagNumber)
        {
            int deviceId = 0;

            if (rblInandOut.SelectedIndex == 0)
            {
                deviceId = int.Parse(lblDeviceCounter.Text);
            }
            else
            {
                deviceId = int.Parse(lblDeviceSpeed.Text);
            }

            try
            {
                var engineResult = _engine.Process(deviceId, txtTagNumber.Text, "0".ToString(), ((LoginUser)Session["User"]).Sno.ToString());
                lblResult.Text = engineResult.ToString();
                if(lblResult.Text.Contains("Congratulations"))
                {
                    lblModalTitle.Text = "Congratulations";
                    lblModalBody.Text = engineResult.ToString();
                   lblModalBody.ForeColor = Color.Green;
                }
                else
                {
                    lblModalTitle.Text = "Error";
                    lblModalBody.Text = engineResult.ToString();
                    lblModalBody.ForeColor = Color.Red;
                }
                txtTagNumber.Text = "";

            }
            catch (Exception exception)
            {
                lblModalTitle.Text = "Error";
                lblModalBody.Text = exception.Message;
                lblModalBody.ForeColor = Color.Red;
               
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
            upModal.Update();



        }


    }
}