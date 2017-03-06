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
    public class JustOnceProcessor
    {
        public class MonitorJusstOnce
        {
            public string Name { get; set; }
            public DateTime CreatedDate { get; set; }

        }

        private readonly Logger _nlog = LogManager.GetCurrentClassLogger();
        private AdDataLayer.DataAccess Access = new DataAccess();
        public JustOnceProcessor()
        {
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Exit");

        }

        public DataTable InsertJustOnce(string TableName, string TagNumber, string DeviceId, string LoginId)
        {
            return Access.InsertJustOnceValue(TableName, TagNumber, DeviceId, LoginId);
        }

        public List<MonitorJusstOnce> MonitorJustCount(string TableName)
        {
            return DataTabletoMonitorJusstOnce(Access.MonitorJustCount(TableName));
        }

        private List<MonitorJusstOnce> DataTabletoMonitorJusstOnce(DataTable dt)
        {
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            List<MonitorJusstOnce> monitorJusstOnce = new List<MonitorJusstOnce>();



            foreach (DataRow drRow in dt.Rows)
            {
                var _monitorJusstOnce = new MonitorJusstOnce();
                _monitorJusstOnce.Name = drRow["FirstName"].ToString();
                _monitorJusstOnce.CreatedDate = DateTime.Parse(drRow["CreatedDate"].ToString());
                monitorJusstOnce.Add(_monitorJusstOnce);
            }


            return monitorJusstOnce;
        }
    }

  
}
