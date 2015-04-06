using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AUDataAccess;
using NLog;
using System.Reflection;

namespace AUBusinessAccess
{
    public class EmailFunctions
    {

        private readonly Logger _nlog = LogManager.GetCurrentClassLogger();


        public EmailFunctions()
        {
            _nlog.Trace(message:
                this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            _nlog.Trace(message:
                this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "::Exit");
        }

      

        public string TableCreator(DataTable dt)
        {
            _nlog.Trace(message:
    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            string table = "";
            string tableStart = "<table cellspacing='0'>";
            string tableHeader = TableHeader(dt);
            string TRow = "";
            bool evenbl = true;
            foreach (DataRow rows in dt.Rows)
            {
                TRow = TRow + TableRow(rows, evenbl);
                evenbl = !evenbl;
            }
            string tableEnd = "    </table>";

            table = tableStart + tableHeader + TRow + tableEnd;
            return table;
        }
        private string TableRow(DataRow dr, bool Even)
        {

            _nlog.Trace(message:
    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            string TableRow;
            if (Even)
            {
                TableRow = "<tr class='even'>";
            }
            else
            {
                TableRow = "<tr>";
            }
            foreach (var ItemStr in dr.ItemArray)
            {
                TableRow = TableRow + "<td>" + ItemStr.ToString() + "</td>";
            }
            return TableRow + "</tr>";
        }
        private string TableHeader(DataTable dt)
        {
            _nlog.Trace(message:
    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            string Header = "";
            Header = " <tr>";
            foreach (DataColumn column in dt.Columns)
            {
                Header = Header + "  <th> " + column.ColumnName + " </th>";
            }
            Header = Header + "</tr>";
            return Header;
        }


    }
}
