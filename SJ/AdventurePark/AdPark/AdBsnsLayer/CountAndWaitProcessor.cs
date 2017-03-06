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
    class CountAndWaitProcessor
    {
        private readonly Logger _nlog = LogManager.GetCurrentClassLogger();
        private AdDataLayer.DataAccess Access = new DataAccess();

        public int Sno { get; set; }
        public string TagNumber { get; set; }
        public int? Value { get; set; }
        public string CurrentState { get; set; }
        public DateTime CreatedDate { get; set; }
        public CountAndWaitProcessor()
        {
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Exit");

        }

        private List<CountAndWaitProcessor> DataTabletoCountAndWait(DataTable dt)
        {
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            List<CountAndWaitProcessor> CountAndWaitProcesssors = new List<CountAndWaitProcessor>();



            foreach (DataRow drRow in dt.Rows)
            {
                var CountAndwaitProcessor = new CountAndWaitProcessor();
                CountAndwaitProcessor.Sno = int.Parse(drRow["Sno"].ToString());
                CountAndwaitProcessor.TagNumber = drRow["TagNumber"].ToString();
                CountAndwaitProcessor.Value = drRow["Value"].ToString() == "" ? (int?)null : int.Parse(drRow["Value"].ToString());
                CountAndwaitProcessor.CurrentState = drRow["CurrentState"].ToString() == "" ? null : (drRow["CurrentState"].ToString());
                CountAndwaitProcessor.CreatedDate =  DateTime.Parse(drRow["CreatedDate"].ToString());
                CountAndWaitProcesssors.Add(CountAndwaitProcessor);
            }


            return CountAndWaitProcesssors;
        }

        public List<CountAndWaitProcessor> SelectCountAndWaitWithTagNumber(string TableName, string TagNumber)
        {
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return DataTabletoCountAndWait(Access.SelectCountAndWaitWithTagNumber(TableName, TagNumber));
        }

        public List<CountAndWaitProcessor> SelectCountAndWait(string TableName)
        {
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return DataTabletoCountAndWait(Access.SelectCountAndWait(TableName));
        }

        public DataTable InsertCountAndWaitCounter(string TableName, string TagNumber)
        {
            return Access.InsertCountAndWaitCounter(TableName, TagNumber);
        }
        public List<CountAndWaitProcessor> UpdateExpiredCountAndWait(string TableName)
        {
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return DataTabletoCountAndWait(Access.UpdateExpiredCountAndWait(TableName));
        }
        public List<CountAndWaitProcessor> UpdateExpiredCountAndWait_Out(string TableName, string Value, string Sno)
        {
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return DataTabletoCountAndWait(Access.UpdateExpiredCountAndWait_Out(TableName, Value,Sno));
        }

    }
}
