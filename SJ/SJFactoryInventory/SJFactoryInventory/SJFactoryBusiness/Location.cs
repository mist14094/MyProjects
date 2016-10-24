using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NLog;
using SjFactoryDataAccess;
namespace SJFactoryBusiness
{
    public class Location
    {
        private readonly Logger _nlog = LogManager.GetCurrentClassLogger();
        private SjFactoryDataAccess.DataAccess Access = new DataAccess();
        public int Sno { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsWarehouse { get; set; }
        public string Address { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Location()
        {
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");

            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Exit");

        }

        public List<Location> GetAllLocations()
        {
            List<Location> locations = new List<Location>();
            foreach (DataRow product in Access.GetLocations().Rows)
            {
                Location newlocation = new Location();
                try
                {
                    newlocation.Sno = int.Parse(product["Sno"].ToString());
                    newlocation.Address = product["Address"].ToString();
                    newlocation.Name = product["Name"].ToString();
                    newlocation.Description = product["Description"].ToString();
                    newlocation.IsWarehouse = bool.Parse(product["IsWarehouse"].ToString());
                    newlocation.CreatedDate = DateTime.Parse(product["CreatedDate"].ToString());
                    newlocation.ModifiedDate = DateTime.Parse(product["ModifiedDate"].ToString());
                }
                catch (Exception ex)
                { }
                locations.Add(newlocation);
            }
            return locations;
        }
    }
}
