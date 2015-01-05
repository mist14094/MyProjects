using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BreakEvenConstant;


namespace BreakEvenDAL
{
    public class DA_UpdateTimeStamp : BEDALBase
    {
        public string  GetLastUpdatedTime()
        {
            nlog.Trace("BreakEvenDAL:DA_UpdateTimeStamp:GetLastUpdatedTime::Entering");
            try
            {

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand SQLCmd = new SqlCommand();
                SQLCmd.CommandType = CommandType.StoredProcedure;
                SQLCmd.CommandText = BEconst.pr_GetUpdateTime;
                SQLCmd.Connection = Conn;
                da.SelectCommand = SQLCmd;
                da.Fill(ds);

                string dt = ds.Tables[0].Rows[0][0].ToString();

                return dt;
            }
            catch (Exception ex)
            {
                nlog.ErrorException("BreakEvenDAL:DA_UpdateTimeStamp:GetLastUpdatedTime::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace("BreakEvenDAL:DA_UpdateTimeStamp:GetLastUpdatedTime::Leaving");

            }


        }

    }
}
