using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdDataLayer;

namespace AdBsnsLayer
{

    public class Logr
    {
        AdDataLayer.DataAccess _logic = new DataAccess();

        public string InsertLog(string logType, string logDesc, string logWebPage, int userId, string ipAddress, string deviceType,
             bool isError, bool isInternalError)
        {
            try
            {

                _logic.InsertUserLog(logType, logDesc, logWebPage, userId, ipAddress, deviceType, isError, isInternalError);
            }
            catch (Exception ex)
            {

            }
            return "";
        }

        public string InsertInternalLog(string logType, string logDesc, bool isError, bool isInternalError)
        {
            try
            {
                _logic.InsertInternalLog(logType, logDesc, isError, isInternalError);
            }
            catch (Exception ex)
            {

            }

            return "";
        }
    }
}
