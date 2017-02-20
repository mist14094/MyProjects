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
    public class CountInAndOutProcessor
    {
        private readonly Logger _nlog = LogManager.GetCurrentClassLogger();
        private AdDataLayer.DataAccess Access = new DataAccess();
        public int Sno { get; set; }
        public string TagNumber { get; set; }
        public DateTime? InTime { get; set; }
        public DateTime? OutTime { get; set; }
        public int TotalDurationInMinutes { get; set; }
        public CountInAndOutProcessor()
        {
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Exit");

        }

        public DataTable CountInAndOut_In(string TableName, string ColumnName,string TagNumber)
        {
            return Access.InsertCountInAndOut(TableName, ColumnName, TagNumber);
        }

        private List<CountInAndOutProcessor> DataTabletoUser(DataTable dt)
        {
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            List<CountInAndOutProcessor> inAndOutProcessors = new List<CountInAndOutProcessor>();



            foreach (DataRow drRow in dt.Rows)
            {
                var inAndOutProcessor = new CountInAndOutProcessor();
                inAndOutProcessor.Sno = int.Parse(drRow["Sno"].ToString());
                inAndOutProcessor.TagNumber = drRow["TagNumber"].ToString();
                inAndOutProcessor.TotalDurationInMinutes = int.Parse(drRow["TotalDurationInMinutes"].ToString()==""?"0": drRow["TotalDurationInMinutes"].ToString());
                inAndOutProcessor.InTime = drRow["InTime"].ToString() == "" ? (DateTime?)null : DateTime.Parse(drRow["InTime"].ToString());
                inAndOutProcessor.OutTime = drRow["OutTime"].ToString() == "" ?  (DateTime?) null : DateTime.Parse( drRow["OutTime"].ToString());
                inAndOutProcessors.Add(inAndOutProcessor);
            }


            return inAndOutProcessors;
        }

        public List<CountInAndOutProcessor> SelectCountInAndOutWithTagNumber(string TableName, string TagNumber)
        {
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return DataTabletoUser(Access.SelectCountInAndOutWithTagNumber(TableName, TagNumber));
        }

        public List<CountInAndOutProcessor> SelectCountInAndOut(string TableName)
        {
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return DataTabletoUser(Access.SelectCountInAndOut(TableName));
        }

        public List<CountInAndOutProcessor> UpdateCountInAndOut_Out(string TableName, string OutColumnName, string Sno, string InColumnName)
        {
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return DataTabletoUser(Access.UpdateCountInAndOut_Out(TableName,OutColumnName,Sno,InColumnName));
        }


    }
}
