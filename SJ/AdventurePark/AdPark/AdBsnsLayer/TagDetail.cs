using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AdDataLayer;
using NLog;

namespace AdBsnsLayer
{

    public class TagDetail
    {

        private readonly Logger _nlog = LogManager.GetCurrentClassLogger();
        private AdDataLayer.DataAccess Access = new DataAccess();
        public int Sno { get; set; }
        public string TagNumber { get; set; }
        public int LacrosseThrow { get; set; }
        public int SoftBallThrow { get; set; }
        public int HeavyBallThrow { get; set; }
        public int Maze { get; set; }
        public int BullRide { get; set; }
        public int KidsZone { get; set; }
        public int SoccerDarts { get; set; }
        public int RopeCourseInMinutes { get; set; }
        public int ZipLine { get; set; }
        public int JumpZone { get; set; }
        public int Tubing { get; set; }
        public int ExtraAct1InCount { get; set; }
        public int ExtraAct2InCount { get; set; }
        public int ExtraAct3InCount { get; set; }
        public int ExtraAct4InCount { get; set; }
        public int ExtraAct5InCount { get; set; }
        public int ExtraAct1InTime { get; set; }
        public int ExtraAct2InTime { get; set; }
        public int ExtraAct3InTime { get; set; }
        public int ExtraAct4InTime { get; set; }
        public int ExtraAct5InTime { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime CreatedDate { get; set; }

        public TagDetail()
        {
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");

            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Exit");

        }
        public List<TagDetail> GetAllTagActivities()
        {

            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return DataTabletoAllTagActivities(Access.GetAllTagActivities());
        }

        public List<TagDetail> GetTagDetails(string TagNumber)
        {

            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return DataTabletoAllTagActivities(Access.GetTagDetails(TagNumber));
        }

        private List<TagDetail> DataTabletoAllTagActivities(DataTable dt)
        {
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            List<TagDetail> AllTagActivities = new List<TagDetail>();



            foreach (DataRow drRow in dt.Rows)
            {
                var AllTagActivity = new TagDetail();
                AllTagActivity.Sno = int.Parse(drRow["Sno"].ToString());
                AllTagActivity.TagNumber = drRow["TagNumber"].ToString();
                AllTagActivity.LacrosseThrow = int.Parse(drRow["LacrosseThrow"].ToString());
                AllTagActivity.SoftBallThrow = int.Parse(drRow["SoftBallThrow"].ToString());
                AllTagActivity.HeavyBallThrow = int.Parse(drRow["HeavyBallThrow"].ToString());
                AllTagActivity.Maze = int.Parse(drRow["Maze"].ToString());
                AllTagActivity.BullRide = int.Parse(drRow["BullRide"].ToString());
                AllTagActivity.KidsZone = int.Parse(drRow["KidsZone"].ToString());
                AllTagActivity.SoccerDarts = int.Parse(drRow["SoccerDarts"].ToString());
                AllTagActivity.RopeCourseInMinutes = int.Parse(drRow["RopeCourseInMinutes"].ToString());
                AllTagActivity.ZipLine = int.Parse(drRow["ZipLine"].ToString());
                AllTagActivity.Tubing = int.Parse(drRow["Tubing"].ToString());
                AllTagActivity.ExtraAct1InCount = int.Parse(drRow["ExtraAct1InCount"].ToString());
                AllTagActivity.ExtraAct2InCount = int.Parse(drRow["ExtraAct2InCount"].ToString());
                AllTagActivity.ExtraAct3InCount = int.Parse(drRow["ExtraAct3InCount"].ToString());
                AllTagActivity.ExtraAct4InCount = int.Parse(drRow["ExtraAct4InCount"].ToString());
                AllTagActivity.ExtraAct5InCount = int.Parse(drRow["ExtraAct5InCount"].ToString());
                AllTagActivity.ExtraAct1InTime = int.Parse(drRow["ExtraAct1InTime"].ToString());
                AllTagActivity.ExtraAct2InTime = int.Parse(drRow["ExtraAct2InTime"].ToString());
                AllTagActivity.ExtraAct3InTime = int.Parse(drRow["ExtraAct3InTime"].ToString());
                AllTagActivity.ExtraAct4InTime = int.Parse(drRow["ExtraAct4InTime"].ToString());
                AllTagActivity.ExtraAct5InTime = int.Parse(drRow["ExtraAct5InTime"].ToString());
                AllTagActivity.JumpZone = int.Parse(drRow["JumpZone"].ToString());
                AllTagActivity.ExpirationDate = DateTime.Parse(drRow["ExpirationDate"].ToString());
                AllTagActivity.CreatedDate = DateTime.Parse(drRow["CreatedDate"].ToString());
                AllTagActivities.Add(AllTagActivity);
            }
            return AllTagActivities;
        }


        public string UpdateActivitiesForTag(string TagNumber, int LacrosseThrow, int SoftBallThrow, int HeavyBallThrow, int Maze, int BullRide, int KidsZone, int SoccerDarts, int RopeCourseInMinutes, int ZipLine, int Tubing, int JumpZone, int ExtraAct1InCount, int ExtraAct2InCount, int ExtraAct3InCount, int ExtraAct4InCount, int ExtraAct5InCount, int ExtraAct1InTime, int ExtraAct2InTime, int ExtraAct3InTime, int ExtraAct4InTime, int ExtraAct5InTime,string LoginName)
        {

            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");

            var TagDetails = GetTagDetails(TagNumber);
            string Message = "";
            if (TagDetails[0].TagNumber != TagNumber) { Message = Message + " TagNumber - " +TagNumber + Environment.NewLine ; }
            if (0 != LacrosseThrow) { Message = Message + " Lacrosse Throw - " +LacrosseThrow + Environment.NewLine ; }
            if (0 != SoftBallThrow) { Message = Message + " Soft Ball Throw - " +SoftBallThrow + Environment.NewLine ; }
            if (0 != HeavyBallThrow) { Message = Message + " Heavy Ball Throw - " +HeavyBallThrow + Environment.NewLine ; }
            if (0 != Maze) { Message = Message + " Maze - " +Maze + Environment.NewLine ; }
            if (0 != BullRide) { Message = Message + " Bull Ride - " +BullRide + Environment.NewLine ; }
            if (0 != KidsZone) { Message = Message + " Kids Zone - " +KidsZone + Environment.NewLine ; }
            if (0 != SoccerDarts) { Message = Message + " Soccer Darts - " +SoccerDarts + Environment.NewLine ; }
            if (0 != RopeCourseInMinutes) { Message = Message + " Rope Course - " +RopeCourseInMinutes + Environment.NewLine ; }
            if (0 != ZipLine) { Message = Message + " Zip Line - " +ZipLine + Environment.NewLine ; }
            if (0 != Tubing) { Message = Message + " Tubing - " +Tubing + Environment.NewLine ; }
            if (0 != JumpZone) { Message = Message + " Jump Zone - " +JumpZone + Environment.NewLine ; }
            if (0 != ExtraAct1InCount) { Message = Message + " ExtraAct1InCount - " +ExtraAct1InCount + Environment.NewLine ; }
            if (0 != ExtraAct2InCount) { Message = Message + " ExtraAct2InCount - " +ExtraAct2InCount + Environment.NewLine ; }
            if (0 != ExtraAct3InCount) { Message = Message + " ExtraAct3InCount - " +ExtraAct3InCount + Environment.NewLine ; }
            if (0 != ExtraAct4InCount) { Message = Message + " ExtraAct4InCount - " +ExtraAct4InCount + Environment.NewLine ; }
            if (0 != ExtraAct5InCount) { Message = Message + " ExtraAct5InCount - " +ExtraAct5InCount + Environment.NewLine ; }
            if (0 != ExtraAct1InTime) { Message = Message + " ExtraAct1InTime - " +ExtraAct1InTime + Environment.NewLine ; }
            if (0 != ExtraAct2InTime) { Message = Message + " ExtraAct2InTime - " +ExtraAct2InTime + Environment.NewLine ; }
            if (0 != ExtraAct3InTime) { Message = Message + " ExtraAct3InTime - " +ExtraAct3InTime + Environment.NewLine ; }
            if (0 != ExtraAct4InTime) { Message = Message + " ExtraAct4InTime - " +ExtraAct4InTime + Environment.NewLine ; }
            if (0 != ExtraAct5InTime) { Message = Message + " ExtraAct5InTime - " +ExtraAct5InTime + Environment.NewLine ; }

            Access.UpdateActivitiesForTag( TagNumber,  LacrosseThrow,  SoftBallThrow,  HeavyBallThrow,  Maze,  BullRide,  KidsZone,  SoccerDarts,  RopeCourseInMinutes,  ZipLine,  Tubing,  JumpZone,  ExtraAct1InCount,  ExtraAct2InCount,  ExtraAct3InCount,  ExtraAct4InCount,  ExtraAct5InCount,  ExtraAct1InTime,  ExtraAct2InTime,  ExtraAct3InTime,  ExtraAct4InTime,  ExtraAct5InTime);
            Log log = new Log();
            log.InsertLogMessage(TagNumber,
                string.Format("Tag updated - ID ={0}, Changes - {1}, Made by - {2}", TagNumber , Message,LoginName));
            return Message;
        }

        public void  UseTagForActivity(string TagNumber, string ColumnName)
        {
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            Access.UseTagForActivity(TagNumber, ColumnName);
            Log log = new Log();
            log.InsertLogMessage(TagNumber,
               string.Format("Tag Used - ID ={0}, in {1} - Activity Started ", TagNumber, ColumnName));
        }


        public void UseTagForActivityButExpired(string TagNumber, string ColumnName)
        {
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            Log log = new Log();
            log.InsertLogMessage(TagNumber,
               string.Format("Tag - ID ={0}, in {1} - But need to purchase More Tokens", TagNumber, ColumnName));
        }

        public void CountInAndOut_OutLog(string TagNumber, string ColumnName,string Minutes)
        {
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            Log log = new Log();
            log.InsertLogMessage(TagNumber,
               string.Format("Tag - ID ={0} - Finished the {1} in {2}", TagNumber, ColumnName,Minutes));
        }


    }
}

