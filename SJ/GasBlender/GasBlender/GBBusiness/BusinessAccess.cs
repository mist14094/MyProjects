using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using GBData;
using NLog;

namespace GBBusiness
{
    public class BusinessAccess
    {
        private readonly Logger _nlog = LogManager.GetCurrentClassLogger();
        private GBData.DataAccess _access;

        public BusinessAccess()
        {
            _nlog.Trace(message:
                this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            _access = new DataAccess();
            _nlog.Trace(message:
                this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "::Exit");
        }

        public DataTable GetAllTrailer()
        {
            _nlog.Trace(message:
                this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return _access.GetAllTrailer();

        }

        public DataTable GetCastedTruck()
        {
            _nlog.Trace(message:
                this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return _access.GetCastedTruck();

        }

        public DataTable GetActiveTrailer()
        {
            return
                _access.GetAllTrailer()
                    .AsEnumerable()
                    .Where(row => row["isactive"].ToString().ToUpper() == "T")
                    .CopyToDataTable();
        }

        public DataTable UpdateTrailer(string trailerNumber, string cmpt1, string cmpt2, string cmpt3, string cmpt4,
            string cmpt5, string trailerID)
        {
            return _access.UpdateTrailer(trailerNumber, cmpt1, cmpt2, cmpt3, cmpt4, cmpt5, trailerID);
        }

        public DataTable AddTrailer(string trailerNumber, string cmpt1, string cmpt2, string cmpt3, string cmpt4,
            string cmpt5)
        {
            return _access.AddTrailer(trailerNumber, cmpt1, cmpt2, cmpt3, cmpt4, cmpt5);
        }

        public DataTable RemoveTrailer(string trailerNumber)
        {
            return _access.RemoveTrailer(trailerNumber);
        }

        public DataTable GetSetup()
        {
            return _access.GetSetup();
        }

        public DataTable UpdateStoredTank(string regularStored, string superStored, string ethanolStored)
        {
            return _access.UpdateStoredTank(regularStored, superStored, ethanolStored);
        }

        public DataTable UpdateTankSize(string regularCapacity, string superCapacity, string ethanolCapacity)
        {
            return _access.UpdateTankSize(regularCapacity, superCapacity, ethanolCapacity);
        }

        public DataTable GetLocation()
        {
            return
                _access.GetLocation()
                    .AsEnumerable()
                    .Where(row => row["isactive"].ToString().ToUpper() == "T")
                    .CopyToDataTable();
        }

        public DataTable InsertLocation(string locationName, string locationAddress, string city, string state,
            string zip)
        {
            return _access.InsertLocation(locationName, locationAddress, city, state, zip);
        }

        public DataTable UpdateLocation(string locationName, string locationAddress, string city, string state,
            string zip, string locationId)
        {
            return _access.UpdateLocation(locationName, locationAddress, city, state, zip, locationId);
        }

        public DataTable RemoveLocation(string locationId)
        {
            return _access.RemoveLocation(locationId);
        }

        public DataTable GetCarrier()
        {
            return _access.GetCarrier();
        }

        public DataTable InsertCarrier(string name, string tollnumber, string localContactName,
            string localContactNumber)
        {
            return _access.InsertCarrier(name, tollnumber, localContactName, localContactNumber);
        }

        public DataTable UpdateCarrier(string name, string tollnumber, string localContactName,
            string localContactNumber, string carrierID)
        {
            return _access.UpdateCarrier(name, tollnumber, localContactName, localContactNumber, carrierID);
        }

        public DataTable RemoveCarrier(string carrierID)
        {
            return _access.RemoveCarrier(carrierID);
        }

        public DataTable GetTractor()
        {
            return
                _access.GetTractor()
                    .AsEnumerable()
                    .Where(row => row["isactive"].ToString().ToUpper() == "T")
                    .CopyToDataTable();
        }

        public DataTable InsertTractor(string Name)
        {
            return _access.InsertTractor(Name);
        }

        public DataTable UpdateTractor(string Name, string TractorID)
        {
            return _access.UpdateTractor(Name, TractorID);
        }

        public DataTable RemoveTractor(string TractorID)
        {
            return _access.RemoveTractor(TractorID);
        }



        public DataTable GetDriver()
        {
            return
                _access.GetDriver()
                    .AsEnumerable()
                    .Where(row => row["isactive"].ToString().ToUpper() == "T")
                    .CopyToDataTable();
        }

        public DataTable InsertDriver(string Name)
        {
            return _access.InsertDriver(Name);
        }

        public DataTable UpdateDriver(string Name, string DriverID)
        {
            return _access.UpdateDriver(Name, DriverID);
        }

        public DataTable RemoveDriver(string DriverID)
        {
            return _access.RemoveDriver(DriverID);
        }

        public DataTable ReceiveTruckLoad(string carrierID, string refNum, string loadType, string trailerID,
            string c1Type, string c1Amount, string c2Type, string c2Amount, string c3Type, string c3Amount,
            string c4Type, string c4Amount,
            string c5Type, string c5Amount, string sumRegular, string sumSuper, string sumEthanol, string c1LocationID,
            string c2LocationID, string c3LocationID, string c4LocationID, string c5LocationID,
            string driver, string truck, string note)
        {
            return _access.ReceiveTruckLoad(carrierID, refNum, loadType, trailerID, c1Type, c1Amount, c2Type, c2Amount,
                c3Type, c3Amount, c4Type, c4Amount, c5Type, c5Amount, sumRegular, sumSuper, sumEthanol, c1LocationID,
                c2LocationID, c3LocationID, c4LocationID, c5LocationID, driver, truck, note);
        }

        public DataTable InsertLineData(string loadId, string cType, string cAmount, string cLocation, string rAdd,
            string sAdd, string eAdd, string compartment, string combined, string note)
        {
            return _access.InsertLineData(loadId, cType, cAmount, cLocation, rAdd, sAdd,
                eAdd, compartment, combined, note);

        }

        public DataTable UpdateSetupTable(string regularStored, string superStored, string ethanolStored)
        {
            return _access.UpdateSetupTable(regularStored, superStored, ethanolStored);
        }
    }
}
