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
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return _access.GetAllTrailer();

        }

        public DataTable GetActiveTrailer()
        {
            return _access.GetAllTrailer().AsEnumerable().Where(row => row["isactive"].ToString().ToUpper()=="T").CopyToDataTable();
        }

        public DataTable UpdateTrailer(string trailerNumber, string cmpt1, string cmpt2, string cmpt3, string cmpt4,
            string cmpt5, string trailerID)
        {
            return _access.UpdateTrailer(trailerNumber, cmpt1, cmpt2, cmpt3, cmpt4, cmpt5, trailerID);
        } 
        
        public DataTable AddTrailer(string trailerNumber, string cmpt1, string cmpt2, string cmpt3, string cmpt4,
            string cmpt5)
        {
            return _access.AddTrailer(trailerNumber, cmpt1, cmpt2, cmpt3, cmpt4, cmpt5);
        } 
        
        public DataTable RemoveTrailer(string trailerNumber)
        {
            return _access.RemoveTrailer(trailerNumber);
        }
    }
}
