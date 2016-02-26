using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KTone.Core.KTIRFID;


namespace KTone.Core.SDCBusinessLogic
{
    public class SDCWSHelper
    {
        static   NLog.Logger _log = NLog.LogManager.GetCurrentClassLogger();
        static ComponentProvider compPvdr = null;
        public SDCWSHelper()
        {

        }

        public static IKTSDCCache GetSDCCache()
        {
            try
            {
                if (compPvdr == null || !ComponentProvider.IsConnectedToFactory)
                {
                    compPvdr = GetServiceConnection();
                }
                IKTSDCCache sdcCache = compPvdr.GetSDCCache();

                return sdcCache;
            }
            catch (Exception ex)
            {
                _log.Error("Error:SDCWSHelper:GetSDCCache:: Service Not Found.", ex.StackTrace);
                return null;
            }

        }

        public static ComponentProvider GetServiceConnection()
        {
            if (compPvdr == null || !ComponentProvider.IsConnectedToFactory)
            {
                RFServerConnParam connParam = new RFServerConnParam();
                connParam.hostPort = BaseAppSettings.HostPort;
                connParam.ipAddr = BaseAppSettings.IPAddress;
                connParam.protocol = BaseAppSettings.Protocol;
                connParam.remotePort = BaseAppSettings.RemotePort;
                connParam.URI = BaseAppSettings.URI;
                compPvdr = new ComponentProvider(connParam);
            }
            return compPvdr;
        }

        public static IKTComponent GetcomponentInstance(KTLocationDetails locationdetails, int DataOwnerID)
        {

            try
            {
                if (compPvdr == null || !ComponentProvider.IsConnectedToFactory)
                {
                    compPvdr = GetServiceConnection();
                }


                IKTComponent getcomponent = null;
                if (ComponentProvider.IsConnectedToFactory)
                {
                    getcomponent = compPvdr.GetComponents(Convert.ToString(locationdetails.RFValue));
                }
                return getcomponent;
            }
            catch (Exception ex)
            {
                _log.Error("Error:SDCWSHelper:GetcomponentInstance:: Service Not Found.", ex.StackTrace);
               // return null;
                throw ex;
            }

        }

        public static bool ReleaseRemoteObject()
        {
            try
            {
                if (compPvdr != null)
                {
                    compPvdr.UnregisterChannel();
                }
                return true;
            }
            catch { return false; }
        }

        //public static bool Print(IKTComponent getcomponent, Dictionary<string, string> Dictionarylableobj, int numOfCopies,out string  errorMsg)
        //{
        //    return ((IKTPrinter)getcomponent).Print(Dictionarylableobj, numOfCopies, out errorMsg);
        //}


    }
}
