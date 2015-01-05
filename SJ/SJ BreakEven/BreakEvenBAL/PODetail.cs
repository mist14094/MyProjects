using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BreakEvenDAL;
using NLog;
using System.Data;
namespace BreakEvenBAL
{
    public class PODetail
    {
        NLog.Logger nlog = LogManager.GetCurrentClassLogger();

        public DataSet GetPODetail(long PCID)
        {
            nlog.Trace("BreakEvenBAL:SalesDetail:GetSalesDetail::Entering");
            try
            {
                DA_PODetails objDASalesDetail = new DA_PODetails();
                DataSet ds = objDASalesDetail.GetAllPODetail(PCID);

                return ds;
            }
            catch (Exception ex)
            {
                nlog.ErrorException("BreakEvenBAL:SalesDetail:GetSalesDetail::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace("BreakEvenBAL:SalesDetail:GetSalesDetail::Leaving");

            }
        }
    }
}
