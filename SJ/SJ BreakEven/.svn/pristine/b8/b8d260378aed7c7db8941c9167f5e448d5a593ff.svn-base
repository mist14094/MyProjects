using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BreakEvenDAL;
using NLog;
using System.Data;


namespace BreakEvenBAL
{
    public class SalesDetail
    {
        NLog.Logger nlog = LogManager.GetCurrentClassLogger();

        public DataSet GetSalesDetail(long PCID)
        {
            nlog.Trace("BreakEvenBAL:SalesDetail:GetSalesDetail::Entering");
            try
            {
                DA_SalesDetail objDASalesDetail = new DA_SalesDetail();
                DataSet ds = objDASalesDetail.GetAllSalesDetail(PCID);

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

        public DataSet GetSalesDetail(long PCID, int storeID)
        {
            nlog.Trace("BreakEvenBAL:SalesDetail:GetSalesDetail::Entering");
            try
            {
                DA_SalesDetail objDASalesDetail = new DA_SalesDetail();
                DataSet ds = objDASalesDetail.GetAllSalesDetail(PCID, storeID);

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


        public DataSet GetSalesTrend (long PCID)
        {
            nlog.Trace("BreakEvenBAL:SalesDetail:GetSalesTrend::Entering");
            try
            {
                DA_SalesDetail objDASalesDetail = new DA_SalesDetail();
                DataSet ds = objDASalesDetail.Get_SalesTrend(PCID);

                return ds;
            }
            catch (Exception ex)
            {
                nlog.ErrorException("BreakEvenBAL:SalesDetail:GetSalesTrend::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace("BreakEvenBAL:SalesDetail:GetSalesTrend::Leaving");

            }
        }
    }
}
