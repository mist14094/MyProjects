using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdDataLayer;
using NLog;
using System.Reflection;
namespace AdBsnsLayer
{
    public class Log
    {
        private readonly Logger _nlog = LogManager.GetCurrentClassLogger();
        private AdDataLayer.DataAccess Access = new DataAccess();
        public Log()
        {
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");

            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Exit");

        }
        public int Sno { get; set; }
        public string Message { get; set; }
        public string TagNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public void InsertLogMessage(string TagNumber, string Message)
        {
            Access.InsertLogMessage(TagNumber, Message);
        }
        public List<Log> GetAllLogs()
        {
            return DataTabletoLog( Access.GetAllLogs());
        }
        public List<Log> GetLogsforTag(string TagNumber)
        {
            return DataTabletoLog(Access.GetLogsforTag(TagNumber));
        }
        private List<Log> DataTabletoLog(DataTable dt)
        {
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return (from DataRow drRow in dt.Rows
                select new Log
                {   Sno         = int.Parse(drRow["Sno"].ToString()),
                    Message     = drRow["Message"].ToString(),
                    TagNumber   = drRow["TagNumber"].ToString(),
                    CreatedDate = DateTime.Parse(drRow["CreatedDate"].ToString()),
                }).ToList();
        }


    }
}
