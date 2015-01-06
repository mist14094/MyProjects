using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JarvisReportsConstant;
using NLog;
namespace JarvisDataAccess
{
    public class JarvisDataAccessBase
    {
        internal SqlConnection RFIDSQLConnection, SYSPROSQLConnection, EPMSQLConnection;
        internal GlobalConstants JarvisConstant = new GlobalConstants();
        internal Logger nlog = LogManager.GetCurrentClassLogger();
        public JarvisDataAccessBase()
        {
            nlog.Trace("JarvisDataAccess:JarvisDataAccessBase:JarvisDataAccessBase::Entering");
            try
            {

                RFIDSQLConnection = new SqlConnection(JarvisConstant.RFIDConnection);
                SYSPROSQLConnection = new SqlConnection(JarvisConstant.SysproConnection);
                EPMSQLConnection = new SqlConnection(JarvisConstant.EPMConnection);
            }
            catch (Exception ex)
            {
                nlog.Error("JarvisDataAccess:JarvisDataAccessBase:JarvisDataAccessBase::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace("JarvisDataAccess:JarvisDataAccessBase:JarvisDataAccessBase::Leaving");

            }
        }
    }
}
