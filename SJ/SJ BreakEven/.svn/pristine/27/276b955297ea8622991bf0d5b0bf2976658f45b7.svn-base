using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BreakEvenConstant;


namespace BreakEvenDAL
{
    public class DA_PODetails : BEDALBase
    {
        public DataSet GetAllPODetail(long PC_ID)
        {
            nlog.Trace("BreakEvenDAL:DA_PODetails:GetAllPODetail::Entering");
            try
            {

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand SQLCmd = new SqlCommand();
                SQLCmd.CommandType = CommandType.StoredProcedure;
                SQLCmd.CommandText = BEconst.pr_GetAllPODetail;
                SQLCmd.Parameters.Add(new SqlParameter("@PC_ID", SqlDbType.BigInt, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, PC_ID));
                SQLCmd.Connection = Conn;
                da.SelectCommand = SQLCmd;
                da.Fill(ds);

                return ds;
            }
            catch (Exception ex)
            {
                nlog.ErrorException("BreakEvenDAL:DA_PODetails:GetAllPODetail::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace("BreakEvenDAL:DA_PODetails:GetAllPODetail::Leaving");

            }


        }


    }
}
