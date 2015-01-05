using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BreakEvenDAL;
using NLog;
using System.Data;

namespace BreakEvenBAL
{
    public class UpdateTimeStamp
    {
        NLog.Logger nlog = LogManager.GetCurrentClassLogger();

        public String LastUpdatetime()
        {
            nlog.Trace("BreakEvenBAL:UpdateTimeStamp:LastUpdatetime::Entering");
            try
            {
                DA_UpdateTimeStamp objDASalesDetail = new DA_UpdateTimeStamp();
                string ds = objDASalesDetail.GetLastUpdatedTime();

                return ds;
            }
            catch (Exception ex)
            {
                nlog.ErrorException("BreakEvenBAL:UpdateTimeStamp:LastUpdatetime::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace("BreakEvenBAL:UpdateTimeStamp:LastUpdatetime::Leaving");

            }
        }
    }
}
