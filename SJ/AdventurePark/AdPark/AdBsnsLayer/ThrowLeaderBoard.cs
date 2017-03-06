using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AdDataLayer;
using NLog;
namespace AdBsnsLayer
{
    public class ThrowLeaderBoard
    {
        private readonly Logger _nlog = LogManager.GetCurrentClassLogger();
        private AdDataLayer.DataAccess Access = new DataAccess();
        public int Sno { get; set; }
        public int  DeviceID { get; set; }
        public string  TagNumber { get; set; }
        public int Value { get; set; }
        public DateTime CreatedDate { get; set; }
        public ThrowLeaderBoard()
        {
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Exit");

        }

        private List<ThrowLeaderBoard> DataTabletoThrowLeaderBoard(DataTable dt)
        {
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            List<ThrowLeaderBoard> throwLeaderBoards = new List<ThrowLeaderBoard>();



            foreach (DataRow drRow in dt.Rows)
            {
                var throwLeaderBoard = new ThrowLeaderBoard();
                throwLeaderBoard.Sno = int.Parse(drRow["Sno"].ToString());
                throwLeaderBoard.TagNumber = drRow["TagNumber"].ToString();
                throwLeaderBoard.Value =  int.Parse(drRow["Value"].ToString());
                throwLeaderBoard.CreatedDate = DateTime.Parse(drRow["CreatedDate"].ToString());
                throwLeaderBoards.Add(throwLeaderBoard);
            }


            return throwLeaderBoards;
        }

        public List<ThrowLeaderBoard> SelectThrowLeaderBoard()
        {
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return DataTabletoThrowLeaderBoard(Access.SelectThrowLeaderBoard());
        }

        public List<ThrowLeaderBoard> InsertThrowLeaderBoard(string DeviceID, string TagNumber, string Value)
        {
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return DataTabletoThrowLeaderBoard(Access.InsertThrowLeaderBoard(DeviceID,TagNumber,Value));
        }
    }
}
