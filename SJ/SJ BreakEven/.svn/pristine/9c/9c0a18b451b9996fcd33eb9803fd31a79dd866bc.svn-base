using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BreakEvenConstant;

namespace BreakEvenDAL
{
    public class DA_SalesDetail : BEDALBase
    {
        public DA_SalesDetail()
        {

        }


        public DataSet GetAllSalesDetail(long PC_ID)
        {
            nlog.Trace("BreakEvenDAL:DA_SalesDetail:GetAllSalesDetail::Entering");
            try
            {

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand SQLCmd = new SqlCommand();
                SQLCmd.CommandType = CommandType.StoredProcedure;
                SQLCmd.CommandText = BEconst.pr_GetAllSalesDetail;
                SQLCmd.Parameters.Add(new SqlParameter("@PC_ID",SqlDbType.BigInt, 8,ParameterDirection.Input,false,0,0,"",DataRowVersion.Proposed,PC_ID));
                SQLCmd.Parameters.Add(new SqlParameter("@StoreID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, null));
                SQLCmd.Connection = Conn;
                da.SelectCommand = SQLCmd;
                da.Fill(ds);

                return ds;
            }
            catch (Exception ex)
            {
                nlog.ErrorException("BreakEvenDAL:DA_SalesDetail:GetAllSalesDetail::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace("BreakEvenDAL:DA_SalesDetail:GetAllSalesDetail::Leaving");

            }


        }

        public DataSet GetAllSalesDetail(long PC_ID, int storeID)
        {
            nlog.Trace("BreakEvenDAL:DA_SalesDetail:GetAllSalesDetail::Entering");
            try
            {

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand SQLCmd = new SqlCommand();
                SQLCmd.CommandType = CommandType.StoredProcedure;
                SQLCmd.CommandText = BEconst.pr_GetAllSalesDetail;
                SQLCmd.Parameters.Add(new SqlParameter("@PC_ID", SqlDbType.BigInt, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, PC_ID));
                SQLCmd.Parameters.Add(new SqlParameter("@StoreID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, storeID));
                SQLCmd.Connection = Conn;
                da.SelectCommand = SQLCmd;
                da.Fill(ds);

                return ds;
            }
            catch (Exception ex)
            {
                nlog.ErrorException("BreakEvenDAL:DA_SalesDetail:GetAllSalesDetail::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace("BreakEvenDAL:DA_SalesDetail:GetAllSalesDetail::Leaving");

            }


        }


        public DataSet Get_SalesTrend(long PC_ID)
        {
            nlog.Trace("BreakEvenDAL:DA_SalesDetail:Get_SalesTrend::Entering");
            try
            {

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand SQLCmd = new SqlCommand();
                SQLCmd.CommandType = CommandType.StoredProcedure;
                SQLCmd.CommandText = BEconst.pr_GetSalesTrend;
                SQLCmd.Parameters.Add(new SqlParameter("@PC_ID",SqlDbType.BigInt, 8,ParameterDirection.Input,false,0,0,"",DataRowVersion.Proposed,PC_ID));
                SQLCmd.Connection = Conn;
                da.SelectCommand = SQLCmd;
                da.Fill(ds);

                return ds;
            }
            catch (Exception ex)
            {
                nlog.ErrorException("BreakEvenDAL:DA_SalesDetail:Get_SalesTrend::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace("BreakEvenDAL:DA_SalesDetail:Get_SalesTrend::Leaving");

            }


        }


    }
}
