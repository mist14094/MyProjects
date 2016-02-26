using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using NLog;
using KTone.RFIDGlobal.KTLogger;

namespace KTone.Core.SDCBusinessLogic
{
    public static class BaseAppSettings
        {
            private static string _dbServerName = string.Empty;
            private static string _dbName = string.Empty;
            private static string _userID = string.Empty;
            private static string _password = string.Empty;
            private static string _assetConnString = string.Empty;
            public static Logger m_Log = null;
            private static ComponentProvider compProvider = null;
            // Remote Settings variables
            private static string _remoteServerName = string.Empty;
            private static string _remoteIP = string.Empty;
            private static string _remoteProtocol = string.Empty;
            private static string _remoteURI = string.Empty;
            private static Int32 _remotePort = 0;
            private static Int32 _hostPort = 0;

            private static string _productBrand = string.Empty;
            private static string _webSite = string.Empty;
            private static string _copyright = string.Empty;

            static BaseAppSettings()
            {

                m_Log = KTLogManager.GetLogger();
                LoadConfigParams();
            }

            public static string DBServerName
            {
                get
                {
                    return _dbServerName;
                }
            }

            public static string DBName
            {
                get
                {
                    return _dbName;
                }
            }

            public static string UID
            {
                get
                {
                    return _userID;
                }
            }

            public static string PWD
            {
                get
                {
                    return _password;
                }
            }

            public static string ConnectionString
            {
                get
                {
                    return _assetConnString;
                }

            }

            public static void DecodedString(string ConnString)
            {
                try
                {
                    _assetConnString = ConnString;

                    _assetConnString = KTone.RFIDGlobal.RFUtils.Decode(_assetConnString);
                    //KTone.RFIDGlobal.ConfigParams.GlobalConfigParams.LookupDecoded("KTProductConn", out _assetConnString);
                    if (_assetConnString == string.Empty)
                        throw new ApplicationException("SmartDC database connection string not found.");

                    KTone.RFIDGlobal.RFUtils.SplitDBConnectionStr(_assetConnString, out _dbName, out _dbServerName, out _userID, out _password);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("From base" + ex.Message);
                }
            }

            public static string ProductBrand
            {
                get
                {
                    return _productBrand;
                }
            }

            public static string WebSite
            {
                get
                {
                    return _webSite;
                }
            }

            public static string Copyright
            {
                get
                {
                    return _copyright;
                }
            }

            private static void LoadConfigParams()
            {
                Uri p = new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
                Configuration config = ConfigurationManager.OpenExeConfiguration(p.LocalPath);

                _remoteServerName = ConfigurationManager.AppSettings["ServerName"];
                _remoteIP = ConfigurationManager.AppSettings["IPAddress"];
                _remoteProtocol = ConfigurationManager.AppSettings["Protocol"];
                _remoteURI = ConfigurationManager.AppSettings["URI"];
                _remotePort = Convert.ToInt32(ConfigurationManager.AppSettings["RemotePort"]);
                _hostPort = Convert.ToInt32(ConfigurationManager.AppSettings["HostPort"]);
              //  _productBrand = config.AppSettings.Settings["ProductBrand"].Value;
              //  _webSite = config.AppSettings.Settings["WebSite"].Value;
              //  _copyright = config.AppSettings.Settings["Copyright"].Value;
                //_remoteIP = config.AppSettings.Settings["IPAddress"].Value;
                //_remoteProtocol = config.AppSettings.Settings["Protocol"].Value;
                //_remoteURI = config.AppSettings.Settings["URI"].Value;
                //_remotePort = Convert.ToInt32(config.AppSettings.Settings["RemotePort"].Value);
                //_hostPort = Convert.ToInt32(config.AppSettings.Settings["HostPort"].Value);
                //_productBrand = config.AppSettings.Settings["ProductBrand"].Value;
                //_webSite = config.AppSettings.Settings["WebSite"].Value;
                //_copyright = config.AppSettings.Settings["Copyright"].Value;
            }

            public static void SaveNewConfigParams(RFServerConnParam serverParam)
            {
                Uri p = new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
                Configuration config = ConfigurationManager.OpenExeConfiguration(p.LocalPath);

                config.AppSettings.Settings["ServerName"].Value = serverParam.serverDisplayName;
                config.AppSettings.Settings["IPAddress"].Value = serverParam.ipAddr;
                config.AppSettings.Settings["Protocol"].Value = serverParam.protocol;
                config.AppSettings.Settings["URI"].Value = serverParam.URI;
                config.AppSettings.Settings["RemotePort"].Value = serverParam.remotePort.ToString();
                config.AppSettings.Settings["HostPort"].Value = serverParam.hostPort.ToString();

                config.Save(ConfigurationSaveMode.Modified);

                // Force a reload of a changed section.
                ConfigurationManager.RefreshSection("appSettings");

                // On save load config parameters
                LoadConfigParams();

                // Add KTRFWebServiceURL parameter in application Config if not present.
                Uri appPath = new Uri(System.Reflection.Assembly.GetEntryAssembly().CodeBase);
                Configuration appConfig = ConfigurationManager.OpenExeConfiguration(appPath.LocalPath);

                string webServiceUrlValue = "http://" + _remoteIP + ":22500/KTWebPrinter";

                if (appConfig.AppSettings.Settings["KTRFWebServiceURL"] == null)
                    appConfig.AppSettings.Settings.Add("KTRFWebServiceURL", webServiceUrlValue);
                else
                    appConfig.AppSettings.Settings["KTRFWebServiceURL"].Value = webServiceUrlValue;

                appConfig.Save(ConfigurationSaveMode.Modified);
            }

            public static void SaveHostPort(string hostPort)
            {
                try
                {
                    Uri p = new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
                    Configuration config = ConfigurationManager.OpenExeConfiguration(p.LocalPath);
                    config.AppSettings.Settings["HostPort"].Value = hostPort.ToString();
                    _hostPort = Convert.ToInt32(hostPort);
                    config.Save(ConfigurationSaveMode.Modified);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Unable to save the Host port in config file. Error:" + ex.Message);
                }
                // Force a reload of a changed section.

            }

            #region Remote Setting
            public static string ServerName
            {
                get
                {
                    return _remoteServerName;
                }
            }

            public static string IPAddress
            {
                get
                {
                    return _remoteIP;
                }
            }

            public static string Protocol
            {
                get
                {
                    return _remoteProtocol;
                }
            }

            public static string URI
            {
                get
                {
                    return _remoteURI;
                }
            }

            public static Int32 RemotePort
            {
                get
                {
                    return _remotePort;
                }
            }

            public static Int32 HostPort
            {
                get
                {
                   // LoadConfigParams();
                    return _hostPort;
                }
                set
                {
                    _hostPort = value;
                }
            }
            #endregion

            public static ComponentProvider RFCompProvider
            {
                get
                {
                    return compProvider;
                }
                set
                {
                    compProvider = value;
                }
            }
        }

}
