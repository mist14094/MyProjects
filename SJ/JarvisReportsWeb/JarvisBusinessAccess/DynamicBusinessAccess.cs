using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using JarvisDataAccess;
using NLog;

namespace JarvisBusinessAccess
{
    public class DynamicBusinessAccess
    {
        private readonly Logger _nlog = LogManager.GetCurrentClassLogger();
        private int _connectionString;
        JarvisDataAccess.DynamicDataAccess _access;
        public DynamicBusinessAccess(int connectionString)
        {
            _connectionString = connectionString;
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            _access = new DynamicDataAccess(this._connectionString);
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Exit");
        }


       
    }
}
