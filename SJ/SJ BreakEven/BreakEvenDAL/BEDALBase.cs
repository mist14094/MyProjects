using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BreakEvenConstant;
using NLog;


namespace BreakEvenDAL
{
    public class BEDALBase
    {
       internal SqlConnection Conn, TrackerRetailConnection;
       internal GlobalConstants BEconst = new GlobalConstants();
       internal Logger nlog = LogManager.GetCurrentClassLogger();

        public BEDALBase()
        {
            nlog.Trace("BreakEvenDAL:BEDALBase:BEDALBase::Entering");
            try
            {

                Conn = new SqlConnection(BEconst.Conn_BreakEven);
                TrackerRetailConnection = new SqlConnection(BEconst.Conn_KTTrackerRetail);
            }
            catch (Exception ex)
            {
                nlog.ErrorException("BreakEvenDAL:BEDALBase:BEDALBase::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace("BreakEvenDAL:BEDALBase:BEDALBase::Leaving");

            }
        }
    }
}
