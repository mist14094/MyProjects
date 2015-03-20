using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AUDataAccess;
using NLog;
using System.Reflection;

namespace AUBusinessAccess
{
    public class EPMBusinessLr
    {

        private readonly Logger _nlog = LogManager.GetCurrentClassLogger();
        private AUDataAccess.EPMDataLr _access;

        public EPMBusinessLr()
        {
            _nlog.Trace(message:
                this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            _access = new EPMDataLr();
            _nlog.Trace(message:
                this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "::Exit");
        }

        public DataTable SalesTransationsGroupedByItem(DateTime startdate, DateTime enddate, int strNbr)
        {
            _nlog.Trace(message:
                this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return _access.SalesTransationsGroupedByItem(startdate, enddate, strNbr);
        }


    }
}
