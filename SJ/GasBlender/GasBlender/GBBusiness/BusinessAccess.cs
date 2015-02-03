using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using GBData;
using NLog;

namespace GBBusiness
{
    public class BusinessAccess
    {
         private readonly Logger _nlog = LogManager.GetCurrentClassLogger();
        private GBData.DataAccess _access;

        public BusinessAccess()
        {
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            _access = new DataAccess();
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Exit");
        }

        public DataTable GetAllTrailer()
        {
            return _access.GetAllTrailer();
        }
    }
}
