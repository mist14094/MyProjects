using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using NLog;

namespace KTone.RFIDGlobal
{
    public class KTRFServiceConfigHelper
    {
        private Logger log = KTone.RFIDGlobal.KTLogger.KTLogManager.GetLogger();
        private string tcpChannelName = "KTRFTCPCHANNEL_AT";
        private string tcpChannelPort = "21500";
        private string ipcChannelName = "KTRFIPCCHANNEL_AT";
        private string ipcChannelPort = "KTRFIPCCHANNELPort_AT";
        private string httpChannelName = "KTRFHTTPCHANNEL_AT";
        private string httpChannelPort = "22500";
        private string serviceName = "KTRFServiceA";

        private static KTRFServiceConfigHelper singleton = null;
        #region Properties


        public string TcpChannelName
        {
            get
            {
                return this.tcpChannelName;
            }
        }

        public string TcpChannelPort
        {
            get
            {
                return this.tcpChannelPort;
            }
        }

        public string IpcChannelName
        {
            get
            {
                return this.ipcChannelName;
            }
        }

        public string IpcChannelPort
        {
            get
            {
                return this.ipcChannelPort;
            }
        }

        public string HttpChannelName
        {
            get
            {
                return this.httpChannelName;
            }
        }

        public string HttpChannelPort
        {
            get
            {
                return this.httpChannelPort;
            }
        }

        public string ServiceName
        {
            get 
            {
                return this.serviceName;
            }
        }

        #endregion

        #region Constructor
        private KTRFServiceConfigHelper(string fileName)
        {
            ReadAssemblyConfig(fileName);
        }
        
        public static KTRFServiceConfigHelper GetInstance(string fileName)
        {
            if (singleton == null)
                singleton = new KTRFServiceConfigHelper(fileName);
            return singleton;
        }

        public static void RemoveInstance()
        {
            singleton = null;
        }

        public static KTRFServiceConfigHelper GetInstance()
        {
            return singleton;
        }
        #endregion Constructor

        private void ReadAssemblyConfig(string fileName)
        {
            if (!File.Exists(fileName))
                return;


            XmlDocument xDoc = new XmlDocument();
            //MemoryStream stream = null;
            try
            {
                // stream = new MemoryStream(new ASCIIEncoding().GetBytes(fileName));
                xDoc.Load(fileName);
                XmlNode appNode = xDoc.SelectSingleNode("configuration/system.runtime.remoting/application");
                if (appNode != null)
                {
                    XmlAttribute attribute = appNode.Attributes["name", ""];
                    if (attribute != null)
                        this.serviceName = attribute.InnerText;
                }

                string channelPath = "configuration/system.runtime.remoting/application/channels";
                XmlNode channels = xDoc.SelectSingleNode(channelPath);
                XmlNodeList xList = channels.SelectNodes("channel");
                foreach (XmlNode xNode in xList)
                {
                    XmlAttributeCollection xmlattr = xNode.Attributes;
                    string type = xmlattr["ref"].Value;
                    switch (type)
                    {
                        case "tcp":
                            {
                                this.tcpChannelName = xmlattr["name"].Value;
                                this.tcpChannelPort = xmlattr["port"].Value;
                                break;
                            }
                        case "ipc":
                            {
                                this.ipcChannelName = xmlattr["name"].Value;
                                this.ipcChannelPort = xmlattr["portName"].Value;
                                break;
                            }
                        case "http":
                            {
                                this.httpChannelName = xmlattr["name"].Value;
                                this.httpChannelPort = xmlattr["port"].Value;
                                break;
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
    }
}
