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
    public class Devices
    {
        private readonly Logger _nlog = LogManager.GetCurrentClassLogger();
        private AdDataLayer.DataAccess Access = new DataAccess();
        public Devices()
        {
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");

            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Exit");

        }

        public int DeviceID { get; set; }
        public string DeviceName { get; set; }
        public string  DeviceDescription { get; set; }
        public string ActivitiesTagColumnName { get; set; }
        public enum _DeviceType 
        {
            JustCount = 1,
            CountAndWait = 2,
            CountInAndOut_In = 3,
            CountInAndOut_Out = 4,
            CountExpire = 5,
            Others=6
        }
        public _DeviceType DeviceType { get; set; }
        public string DeviceColumn { get; set; }
        public string DeviceTable { get; set; }
        public DateTime CreatedDate { get; set; }

        private List<Devices> DataTabletoDevices(DataTable dt)
        {
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            List<Devices> lstDevices = new List<Devices>();



            foreach (DataRow drRow in dt.Rows)
            {
                var Device = new Devices();
                Device.DeviceID = int.Parse(drRow["DeviceID"].ToString());
                Device.DeviceName = drRow["DeviceName"].ToString();
                Device.DeviceDescription = drRow["DeviceDescription"].ToString();
                Device.ActivitiesTagColumnName = drRow["ActivitiesTagColumnName"].ToString();
                Device.DeviceType =(_DeviceType) int.Parse(drRow["DeviceType"].ToString());
                Device.DeviceColumn = drRow["DeviceColumn"].ToString();
                Device.DeviceTable = drRow["DeviceTable"].ToString();
                Device.CreatedDate = DateTime.Parse(drRow["CreatedDate"].ToString());
                lstDevices.Add(Device);
            }


            return lstDevices;
        }

        public List<Devices> GetAllDevices()
        {

            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return DataTabletoDevices(Access.GetAllDevices());
        }

    }
}
