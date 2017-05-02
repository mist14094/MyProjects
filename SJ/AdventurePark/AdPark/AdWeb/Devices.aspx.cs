using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdBsnsLayer;
namespace AdWeb
{
    public partial class Devices : System.Web.UI.Page
    {
        AdBsnsLayer.User User = new User();
        TagDetail Tag = new TagDetail();
        protected void Page_Load(object sender, EventArgs e)
        {
            var user = Session["User"];

            if (!IsPostBack)
            {
                if (user == null)
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddUser.aspx");
        }

        protected void btTagUpdate_Click(object sender, EventArgs e)
        {
            Response.Redirect("AssociateActv.aspx");
        }

        protected void btnTagLog_Click(object sender, EventArgs e)
        {
            Response.Redirect("TagLog.aspx");
        }

        protected void btnDevices_Click(object sender, EventArgs e)
        {

        }

        protected void btnTubing_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("JustCount.aspx?DeviceID={0}",9));
        }

        protected void btnZipLine_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("JustCount.aspx?DeviceID={0}", 10));
        }

        protected void btnJumpZone_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("JustCount.aspx?DeviceID={0}", 13));
        }

        protected void btnExtraAct1InCount_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("JustCount.aspx?DeviceID={0}", 14));
        }

        protected void btnKidZone_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("JustCount.aspx?DeviceID={0}", 12));
        }

        protected void btnBullRide_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("JustCount.aspx?DeviceID={0}", 11));
        }

        protected void btnMaze_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("CountInAndOut.aspx?DeviceID={0}", 15));
        }

        protected void btnRopeCourse_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("CountExpire.aspx?DeviceID={0}", 17));
        }

        protected void btnLacrosseThrow_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("CountAndWait.aspx?DeviceID={0}", 19));
        }

        protected void btnSoftballThrow_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("CountAndWait.aspx?DeviceID={0}", 21));
        }

        protected void btnHeaveyBallThrow_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("CountAndWait.aspx?DeviceID={0}", 23));
        }

        protected void btnSoccerDarts_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("CountExpire.aspx?DeviceID={0}", 25));
        }
    }
}