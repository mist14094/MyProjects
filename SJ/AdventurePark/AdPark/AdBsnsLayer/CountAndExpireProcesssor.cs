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
    public class CountAndExpireProcesssor
    {
        private readonly Logger _nlog = LogManager.GetCurrentClassLogger();
        private AdDataLayer.DataAccess Access = new DataAccess();
        public int Sno { get; set; }
        public string TagNumber { get; set; }
        public DateTime? InTime { get; set; }
        public DateTime? OutTime { get; set; }
        public int TotalDurationInMinutes { get; set; }
        public CountAndExpireProcesssor()
        {
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Exit");

        }

        public DataTable CountAndExpire_In(string TableName, string ColumnName, string TagNumber)
        {
            return Access.InsertCountAndExpire(TableName, ColumnName, TagNumber);
        }

        private List<CountAndExpireProcesssor> DataTabletoCountAndExpire(DataTable dt)
        {
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            List<CountAndExpireProcesssor> CountAndExpireProcesssors = new List<CountAndExpireProcesssor>();



            foreach (DataRow drRow in dt.Rows)
            {
                var CountAndExpireProcessor = new CountAndExpireProcesssor();
                CountAndExpireProcessor.Sno = int.Parse(drRow["Sno"].ToString());
                CountAndExpireProcessor.TagNumber = drRow["TagNumber"].ToString();
                CountAndExpireProcessor.TotalDurationInMinutes = int.Parse(drRow["TotalDurationInMinutes"].ToString() == "" ? "0" : drRow["TotalDurationInMinutes"].ToString());
                CountAndExpireProcessor.InTime = drRow["InTime"].ToString() == "" ? (DateTime?)null : DateTime.Parse(drRow["InTime"].ToString());
                CountAndExpireProcessor.OutTime = drRow["OutTime"].ToString() == "" ? (DateTime?)null : DateTime.Parse(drRow["OutTime"].ToString());
                CountAndExpireProcesssors.Add(CountAndExpireProcessor);
            }


            return CountAndExpireProcesssors;
        }

        public List<CountAndExpireProcesssor> SelectCountAndExpireWithTagNumber(string TableName, string TagNumber)
        {
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return DataTabletoCountAndExpire(Access.SelectCountAndExpireWithTagNumber(TableName, TagNumber));
        }

        public List<CountAndExpireProcesssor> SelectCountAndExpire(string TableName)
        {
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return DataTabletoCountAndExpire(Access.SelectCountAndExpire(TableName));
        }

        public List<CountAndExpireProcesssor> UpdateCountAndExpire_Out(string TableName, string OutColumnName, string Sno, string InColumnName)
        {
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return DataTabletoCountAndExpire(Access.UpdateCountAndExpire_Out(TableName, OutColumnName, Sno, InColumnName));
        }

        public void UpdateCountAndExpire_UseTagForActivity(string TagNumber, string ColumnName, string Value)
        {
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            DataTabletoCountAndExpire(Access.UpdateCountAndExpire_UseTagForActivity(TagNumber, ColumnName, Value));

        }
    }
}
