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
    public partial class AssociateActv : System.Web.UI.Page
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
            // var asd = menu.GetAllTagActivities();

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }



        protected void txtTagNumber_TextChanged(object sender, EventArgs e)
        {
            CheckTags(txtTagNumber.Text);
        }

        protected void btnScan_Click(object sender, EventArgs e)
        {
            CheckTags(txtTagNumber.Text);
        }

        public void CheckTags(string TagNumber)
        {
            var TagDetails = Tag.GetTagDetails(TagNumber);
            if (TagDetails.Count > 0)
            {
                txtTagNumber.Enabled = false;
                txtRopeCourseOrg.Text = TagDetails[0].RopeCourseInMinutes.ToString();
                txtZipLineOrg.Text= TagDetails[0].ZipLine.ToString();
                txtJumpZoneOrg.Text = TagDetails[0].JumpZone.ToString();
                txtTubingOrg.Text = TagDetails[0].Tubing.ToString();
                txtLacrosseOrg.Text = TagDetails[0].LacrosseThrow.ToString();
                txtMazeOrg.Text = TagDetails[0].Maze.ToString();
                txtSoftBallOrg.Text = TagDetails[0].SoftBallThrow.ToString();
                txtSoccerDartOrg.Text = TagDetails[0].SoccerDarts.ToString();
                txtKidsZoneOrg.Text = TagDetails[0].KidsZone.ToString();

                txtHeavyBallOrg.Text = TagDetails[0].HeavyBallThrow.ToString();
                txtBullRideOrg.Text = TagDetails[0].BullRide.ToString();

                txtExtraAct1Org.Text = TagDetails[0].ExtraAct1InCount.ToString();
                txtExtraAct2Org.Text = TagDetails[0].ExtraAct2InCount.ToString();
                txtExtraAct3Org.Text = TagDetails[0].ExtraAct3InCount.ToString();
                txtExtraAct4Org.Text = TagDetails[0].ExtraAct4InCount.ToString();
                txtExtraAct5Org.Text = TagDetails[0].ExtraAct5InCount.ToString();


                txtExtraTime1Org.Text = TagDetails[0].ExtraAct1InTime.ToString();
                txtExtraTime2Org.Text = TagDetails[0].ExtraAct2InTime.ToString();
                txtExtraTime3Org.Text = TagDetails[0].ExtraAct3InTime.ToString();
                txtExtraTime4Org.Text = TagDetails[0].ExtraAct4InTime.ToString();
                txtExtraTime5Org.Text = TagDetails[0].ExtraAct5InTime.ToString();


                txtRopeCourseMod.Text = "0";
                txtZipLineMod.Text = "0";
                txtJumpZoneMod.Text = "0";
                txtTubingMord.Text = "0";
                txtLacrosseMod.Text = "0";
                txtMazeMod.Text = "0";
                txtSoftBallMod.Text = "0";
                txtSoccerDartMod.Text = "0";
                txtKidsZoneMod.Text = "0";

                txtHeavyBallMod.Text = "0";
                txtBullRideMod.Text = "0";

                txtExtraAct1Mod.Text = "0";
                txtExtraAct2Mod.Text = "0";
                txtExtraAct3Mod.Text = "0";
                txtExtraAct4Mod.Text = "0";
                txtExtraAct5Mod.Text = "0";


                txtExtraTime1Mod.Text = "0";
                txtExtraTime2Mod.Text = "0";
                txtExtraTime3Mod.Text = "0";
                txtExtraTime4Mod.Text = "0";
                txtExtraTime5Mod.Text = "0";



            }
            else
            {
                lblModalTitle.Text = "Invalid Tag - " +TagNumber;
                lblModalBody.Text = "This Tag is not in the system.";
                lblModalBody.ForeColor = Color.Red;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                upModal.Update();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
             string Message=   Tag.UpdateActivitiesForTag(txtTagNumber.Text, int.Parse(txtLacrosseMod.Text),
                    int.Parse(txtSoftBallMod.Text), int.Parse(txtHeavyBallMod.Text), int.Parse(txtMazeMod.Text),
                    int.Parse(txtBullRideMod.Text)
                    ,int.Parse(txtKidsZoneMod.Text), int.Parse(txtSoccerDartMod.Text), int.Parse(txtRopeCourseMod.Text),
                    int.Parse(txtZipLineMod.Text), int.Parse(txtTubingMord.Text), int.Parse(txtJumpZoneMod.Text),
                    int.Parse(txtExtraAct1Mod.Text), int.Parse(txtExtraAct2Mod.Text), int.Parse(txtExtraAct3Mod.Text),
                    int.Parse(txtExtraAct4Mod.Text), int.Parse(txtExtraAct5Mod.Text),
                    int.Parse(txtExtraTime1Mod.Text), int.Parse(txtExtraTime2Mod.Text), int.Parse(txtExtraTime3Mod.Text),
                    int.Parse(txtExtraTime4Mod.Text), int.Parse(txtExtraTime5Mod.Text),
                    ((LoginUser)Session["User"]).UserName);
                lblModalTitle.Text = "Tag Updated";
                lblModalBody.Text = Message.Replace("\n","<br/>");
                lblModalBody.ForeColor = Color.Green;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                upModal.Update();

                CheckTags(txtTagNumber.Text);
                    
                ;

            }
            catch (Exception ex)
            {

                lblModalTitle.Text = "Error in Processing";
                lblModalBody.Text = ex.Message;
                lblModalBody.ForeColor = Color.Red;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                upModal.Update();
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("AssociateActv.aspx");
        }
    }
}