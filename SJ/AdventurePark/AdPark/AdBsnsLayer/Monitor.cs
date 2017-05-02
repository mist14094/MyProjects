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
    public class Monitor
    {
        private readonly Logger _nlog = LogManager.GetCurrentClassLogger();
        private AdBsnsLayer.Devices devices = new Devices();
        private AdBsnsLayer.TagDetail tagDetail = new TagDetail();
        private AdDataLayer.DataAccess Access = new DataAccess();
        private AdBsnsLayer.JustOnceProcessor _justOnce = new JustOnceProcessor();
        private AdBsnsLayer.CountInAndOutProcessor _countInAndOutProcessor = new CountInAndOutProcessor();
        private AdBsnsLayer.CountAndExpireProcesssor _countAndExpireProcesssor = new CountAndExpireProcesssor();
        private AdBsnsLayer.CountAndWaitProcessor _countAndWaitProcessor = new CountAndWaitProcessor();
        private ThrowLeaderBoard _board = new ThrowLeaderBoard();

        public Monitor()
        {
            _nlog.Trace(message:
                this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");

            _nlog.Trace(message:
                this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "::Exit");

        }

        public DataTable RopeCourseMonitor()
        {
            return Access.RopeCourseMonitor();
        }

        public DataTable SoccerDartsMonitor()
        {
            return Access.SoccerDartsMonitor();
        }
        public DataSet LacrosseMonitor()
        {
            return Access.LacrosseMonitor();
        }
    }
}
