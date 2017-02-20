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
    public class ActivitiesMenu
    {
        private readonly Logger _nlog = LogManager.GetCurrentClassLogger();
        private AdDataLayer.DataAccess Access = new DataAccess();
        public int Sno { get; set; }
        public string PlanName { get; set; }
        public string PlanDescription { get; set; }
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
        public int HowManyDaysValidFor { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsEditable { get; set; }


        public ActivitiesMenu()
        {
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");

            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Exit");

        }


        public List<ActivitiesMenu> GetAllActivitiesMenu()
        {

            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return DataTabletoActivitiesMenu(Access.GetAllActivitiesMenu());
        }

        private List<ActivitiesMenu> DataTabletoActivitiesMenu(DataTable dt)
        {
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            List<ActivitiesMenu> ActivitiesMenu = new List<ActivitiesMenu>();



            foreach (DataRow drRow in dt.Rows)
            {
                var ActivityMenu = new ActivitiesMenu();
                ActivityMenu.Sno = int.Parse(drRow["Sno"].ToString());
                ActivityMenu.PlanName = drRow["PlanName"].ToString();
                ActivityMenu.PlanDescription = drRow["PlanDescription"].ToString();
                ActivityMenu.LacrosseThrow = int.Parse(drRow["LacrosseThrow"].ToString());
                ActivityMenu.SoftBallThrow = int.Parse(drRow["SoftBallThrow"].ToString());
                ActivityMenu.HeavyBallThrow = int.Parse(drRow["HeavyBallThrow"].ToString());
                ActivityMenu.Maze = int.Parse(drRow["Maze"].ToString());
                ActivityMenu.BullRide = int.Parse(drRow["BullRide"].ToString());
                ActivityMenu.KidsZone = int.Parse(drRow["KidsZone"].ToString());
                ActivityMenu.SoccerDarts = int.Parse(drRow["SoccerDarts"].ToString());
                ActivityMenu.RopeCourseInMinutes = int.Parse(drRow["RopeCourseInMinutes"].ToString());
                ActivityMenu.ZipLine = int.Parse(drRow["ZipLine"].ToString());
                ActivityMenu.Tubing = int.Parse(drRow["Tubing"].ToString());
                ActivityMenu.ExtraAct1InCount = int.Parse(drRow["ExtraAct1InCount"].ToString());
                ActivityMenu.ExtraAct2InCount = int.Parse(drRow["ExtraAct2InCount"].ToString());
                ActivityMenu.ExtraAct3InCount = int.Parse(drRow["ExtraAct3InCount"].ToString());
                ActivityMenu.ExtraAct4InCount = int.Parse(drRow["ExtraAct4InCount"].ToString());
                ActivityMenu.ExtraAct5InCount = int.Parse(drRow["ExtraAct5InCount"].ToString());
                ActivityMenu.ExtraAct1InTime = int.Parse(drRow["ExtraAct1InTime"].ToString());
                ActivityMenu.ExtraAct2InTime = int.Parse(drRow["ExtraAct2InTime"].ToString());
                ActivityMenu.ExtraAct3InTime = int.Parse(drRow["ExtraAct3InTime"].ToString());
                ActivityMenu.ExtraAct4InTime = int.Parse(drRow["ExtraAct4InTime"].ToString());
                ActivityMenu.ExtraAct5InTime = int.Parse(drRow["ExtraAct5InTime"].ToString());

                ActivityMenu.JumpZone = int.Parse(drRow["JumpZone"].ToString());
                ActivityMenu.HowManyDaysValidFor = int.Parse(drRow["HowManyDaysValidFor"].ToString());
                ActivityMenu.CreatedDate = DateTime.Parse(drRow["CreatedDate"].ToString());
                ActivityMenu.IsActive = bool.Parse(drRow["IsActive"].ToString());
                ActivityMenu.IsEditable = bool.Parse(drRow["IsEditable"].ToString());
                ActivitiesMenu.Add(ActivityMenu);
            }


            return ActivitiesMenu;
        }
    }
}
