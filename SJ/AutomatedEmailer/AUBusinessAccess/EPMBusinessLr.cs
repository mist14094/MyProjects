using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using AUDataAccess;
using NLog;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

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

        public DataTable CostcoSalesReport(DateTime startdate, DateTime enddate, int strNbr)
        {
            _nlog.Trace(message:
             this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
             System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return _access.CostcoSalesReport(startdate, enddate, strNbr);
        }



    }
}
