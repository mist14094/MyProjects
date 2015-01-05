using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BreakEvenConstant;

namespace BreakEvenDAL
{
    public class BESanpshot : BEDALBase
    {

        public BESanpshot()
        {

        }

        public DataSet GetBreakEvenSnapShot()
        {
            nlog.Trace("BreakEvenDAL:BESanpshot:GetBreakEvenSnapShot::Entering");
            try
            {

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand SQLCmd = new SqlCommand();
                SQLCmd.CommandType = CommandType.StoredProcedure;
                SQLCmd.CommandText = BEconst.pr_GetBreakEvenSS;
                SQLCmd.Connection = Conn;
                da.SelectCommand = SQLCmd;
                da.Fill(ds);

                return ds;
            }
            catch (Exception ex)
            {
                nlog.ErrorException("BreakEvenDAL:BESanpshot:GetBreakEvenSnapShot::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace("BreakEvenDAL:BESanpshot:GetBreakEvenSnapShot::Leaving");

            }

           
        }
    }
}
