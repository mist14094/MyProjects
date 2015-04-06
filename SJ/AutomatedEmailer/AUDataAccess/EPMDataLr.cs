using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace AUDataAccess
{
    public class EPMDataLr
    {
        internal Logger Nlog = LogManager.GetCurrentClassLogger();
        AUConstants.EPMConstants _constants = new AUConstants.EPMConstants();
        private string _connectionString;

        public EPMDataLr()
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            _connectionString = _constants.DefaultConnectionString;
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Exit");
        }

        public DataTable SalesTransationsGroupedByItem(DateTime startdate, DateTime enddate, int strNbr)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.SalesTransationsGroupedByItem)
                ,CommandTimeout = 180,
                CommandType = CommandType.StoredProcedure
            };
            selectCommand.Parameters.AddWithValue("@startdate", startdate);
            selectCommand.Parameters.AddWithValue("@enddate", enddate);
            selectCommand.Parameters.AddWithValue("@str_nbr", strNbr);
            
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.DefaultConnectionString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Nlog.Trace(
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message:
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }





    }
}
