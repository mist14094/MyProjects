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
    public class JarvisBusinessLr
    {

        private readonly Logger _nlog = LogManager.GetCurrentClassLogger();
        private AUDataAccess.JarvisDataLr _access;

        public JarvisBusinessLr()
        {
            _nlog.Trace(message:
                this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            _access = new JarvisDataLr();
            _nlog.Trace(message:
                this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "::Exit");
        }

        public DataTable TshirtSalesandQOH(DateTime startdate, DateTime enddate)
        {
            _nlog.Trace(message:
                this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return _access.TshirtSalesandQOH(startdate, enddate);
           
        } 
        
        public DataTable  atm_GetEmailList()
        {
            _nlog.Trace(message:
                this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return _access.atm_GetEmailList();
           
        }

        public DataTable LogEmail(int EmailSno, string EmailTO, string EmailSubject, string EmailMessage, string result)
        {
            _nlog.Trace(message:
               this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
               System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return _access.LogEmail(EmailSno,EmailTO, EmailSubject,EmailMessage, result);
        }


    }
}
