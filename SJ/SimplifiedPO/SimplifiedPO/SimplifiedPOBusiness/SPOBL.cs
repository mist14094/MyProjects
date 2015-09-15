using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NLog;
using SimplifiedPODataAccess;

namespace SimplifiedPOBusiness
{
    public class SPOBL
    {
        private readonly Logger _nlog = LogManager.GetCurrentClassLogger();
        private SimplifiedPODataAccess.SPODL _access = new SPODL();
        public SPOBL()
        {
            _nlog.Trace(message:
                this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            _access = new SPODL();
            _nlog.Trace(message:
                this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "::Exit");
        }

        public DataTable SalesTransationsGroupedByItem(DateTime startdate, DateTime enddate, int strNbr)
        {
            _nlog.Trace(message:
                this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return _access.Test(startdate, enddate, strNbr);

        }

        public string TestMethod(string testString)
        {
            _nlog.Trace(message:
    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return _access.Test(testString);
        }


    }
}
