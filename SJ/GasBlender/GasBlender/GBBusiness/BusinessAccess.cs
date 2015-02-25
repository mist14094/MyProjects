using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
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

        public DataSet GetBOL(string LoadTableID)
        {
            return _access.GetBOL(LoadTableID);
        }

        public DataSet BOLLog(DateTime startTime, DateTime endTime)
        {
            return _access.BOLLog(startTime, endTime);
        }

        public DataTable LoadTBLData(string loadId)
        {
            return _access.LoadTBLData(loadId);
        }

        public DataTable SelectLoadTBL(string loadId)
        {
            return _access.SelectLoadTBL(loadId);
        }

        public DataTable DeleteLoadTBL(string loadId)
        {
            return _access.DeleteLoadTBL(loadId);
        }

        public DataTable GetLoad()
        {
            return _access.GetLoad();
        }

        public DataTable UpdateLoadTBL(string refNum, string loadType, string trailerID, string C1Type, string C1Amount, string C2Type, string C2Amount, string C3Type, string C3Amount, string C4Type, string C4Amount, string C5Type, string C5Amount, string sumRegular, string sumSuper, string sumEthanol, string C1LocationID, string C2LocationID, string C3LocationID, string C4LocationID, string C5LocationID, string driver, string truck, string carrierID, string note, string loadID)
        {
            return _access.UpdateLoadTBL(refNum, loadType, trailerID, C1Type, C1Amount, C2Type, C2Amount, C3Type, C3Amount, C4Type, C4Amount, C5Type, C5Amount, sumRegular, sumSuper, sumEthanol, C1LocationID, C2LocationID, C3LocationID, C4LocationID, C5LocationID, driver, truck, carrierID, note, loadID);
        }

        public DataTable UpdateLineTBL(string CType, string CAmount, string CLocationID, string RAdd, string SAdd,
            string EAdd, string combined, string loadid, string compartment)
        {
            return _access.UpdateLineTBL( CType,  CAmount,  CLocationID,  RAdd,  SAdd,
             EAdd,  combined,  loadid,  compartment);
        }

        public DataTable UpdateAfterSetupTable(string regularStored, string superStored, string ethanolStored)
        {
            return _access.UpdateAfterSetupTable(regularStored, superStored, ethanolStored);
        }

        private string Encrypt(string clearText)
        {
            string EncryptionKey = "GASBLENDERSMOKINJOE2293";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        private string Decrypt(string cipherText)
        {
            string EncryptionKey = "GASBLENDERSMOKINJOE2293";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        public DataTable InsertUser(string username, string Name, string Password, string Email, bool isadmin, DateTime DateCreated)
        {
            return _access.InsertUser(username, Name, Encrypt(Password), Email, isadmin, DateCreated);
        }

        public DataTable CheckUser(string username, string password)
        {
          return  _access.CheckUser(username, Encrypt(password));
            //DataTable dt = new DataTable();
            //dt = _access.CheckUser(username,Encrypt(password));
            //if (dt != null)
            //{
            //    if (dt.Rows.Count > 0)
            //    {
            //        return int.Parse(dt.Rows[0]["ID"].ToString());
            //    }
            //    else
            //    {
            //        return null;
            //    }
            //}
            //else
            //{
            //    return null;
            //}
        }

        public DataTable GetUser()
        {
            return _access.GetUser();
        }

        public DataTable EditUser(string username, string Name,  string Email, bool isadmin, string ID)
        {
            return _access.EditUser(username,Name,   Email, isadmin, ID);
        }

        public DataTable ChangePassword(string password, string ID)
        {
            return _access.ChangePassword(Encrypt(password), ID);
        }

        public DataTable DeleteUser(string ID)
        {
            return _access.DeleteUser(ID);
        }

    }
}
