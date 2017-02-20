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
    }
}
