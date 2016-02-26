using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Serialization.Formatters;
using KTone.Core.KTIRFID;
using System.Collections;


namespace KTone.Core.SDCBusinessLogic
{
    public class ComponentProvider
    {
        private static ComponentProvider objFilterProvider;
        public static IKTComponentFactory ktComponentfactory;
        public static Dictionary<string, string> ComponentNames;
        public static bool IsConnectedToFactory = false;
        static RFServerConnParam channel;
        private int iHostPort = 9000;
      

        public ComponentProvider(RFServerConnParam serverParams)
        {
            string Header = "ComponentProvider::ComponentProvider: ";
            BaseAppSettings.m_Log.Trace(Header + "Entering.. ");
            try
            {
                ktComponentfactory = GetRemoteObjects(serverParams);
                IsConnectedToFactory = true;
                BaseAppSettings.DecodedString(ktComponentfactory.ProductDbConnectionString);
              
            }
            catch (Exception ex)
            {
                BaseAppSettings.m_Log.ErrorException(Header + "Error occured." + ex.Message, ex);
                IsConnectedToFactory = false;
                throw ex;
            }
            finally
            {
                BaseAppSettings.m_Log.Trace(Header + "Leaving..");
            }
        }

        public static ComponentProvider GetInstance(RFServerConnParam serverParams)
        {
            string Header = "ComponentProvider::GetInstance: ";
            BaseAppSettings.m_Log.Trace(Header + "Entering.. ");
            try
            {
                if (objFilterProvider == null)
                {
                    objFilterProvider = new ComponentProvider(serverParams);
                }
                return objFilterProvider;
            }
            catch (Exception ex)
            {
                BaseAppSettings.m_Log.ErrorException(Header + "Error occured." + ex.Message, ex);
                throw ex;
            }
            finally
            {
                BaseAppSettings.m_Log.Trace(Header + "Leaving..");
            }
        }

        private IKTComponentFactory GetRemoteObjects(RFServerConnParam channelParam)
        {
            string Header = "ComponentProvider::GetRemoteObjects: ";
            BaseAppSettings.m_Log.Trace(Header + "Entering.. ");
            try
            {
                //curFactoryManager = null;
                //URL = @"tcp://desktop27:21500/KTComponentFactory";
                string URL = channelParam.protocol + @"://" + channelParam.ipAddr + ":" + channelParam.remotePort + @"/" + channelParam.URI;

                //BaseAppSettings.m_Log.Trace("KTRFFactory: Creating channel for:: " + URL);
                IDictionary props = new Hashtable();
                bool flag = true;
                int i = 0;
                TcpChannel chan = null;
                Random rnd = new Random(200);
                while(flag && i < 10)
                {
                    try
                    {
                      

                        iHostPort = rnd.Next(8000, 9000);


                        props["port"] = iHostPort;
                        props["name"] = "KTRFTCPCHANNELClient" + iHostPort.ToString();
                        props["typeUtilsLevel"] = TypeFilterLevel.Full;


                        BinaryClientFormatterSinkProvider clientProvider = null;
                        BinaryServerFormatterSinkProvider serverProvider = new BinaryServerFormatterSinkProvider();


                        serverProvider.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;


                        chan = new TcpChannel(props, clientProvider, serverProvider);
                        ChannelServices.RegisterChannel(chan, false);
                        flag = false;
                    }
                    catch (Exception ex)  
                    {
                        i++;
                        BaseAppSettings.m_Log.ErrorException(Header + "Error occured." + ex.Message, ex);
                        BaseAppSettings.m_Log.Error(Header + "Error occured. Retry Count " + i.ToString());
                    }
                }
               
                //BaseAppSettings.m_Log.Trace("KTRFFactory: channel Resistered for:: " + URL);

                object state = null;
                IKTComponentFactory curComponentFactory = (IKTComponentFactory)Activator.GetObject(typeof(IKTComponentFactory), URL, state);
                string str = curComponentFactory.GetVersion();
                //BaseAppSettings.m_Log.Trace("KTRFFactory: ComponentFactory object created for :: " + URL);
                channel = channelParam;
                //string factoryManagerURL = channelParam.protocol + @"://" + channelParam.ipAddr + ":" + channelParam.remotePort + @"/KTFactoryManager";
                //curFactoryManager = (IKTFactoryManager)Activator.GetObject(typeof(IKTFactoryManager), factoryManagerURL, state);
                return curComponentFactory;
            }
            catch (Exception ex)
            {
                BaseAppSettings.m_Log.ErrorException(Header + "Error occured." + ex.Message, ex);
                TcpChannel remChannel = (TcpChannel)ChannelServices.GetChannel("KTRFTCPCHANNELClient" + iHostPort.ToString());
                if (remChannel != null)
                    ChannelServices.UnregisterChannel(remChannel);
                throw ex;
            }
            finally
            {
                BaseAppSettings.m_Log.Trace(Header + "Leaving..");
            }
        }

        public IKTSDCCache GetSDCCache()
        {
            IKTComponent[] sdcCaches = null;
            string Header = "ComponentProvider::GetSDCCache: ";
            BaseAppSettings.m_Log.Trace(Header + "Entering.. ");

            try
            {
                sdcCaches = ktComponentfactory.GetAllComponents(KTComponentCategory.SDCCache);

                if (sdcCaches.Length >= 1)
                {
                    return (IKTSDCCache)sdcCaches[0];
                }
                else
                {
                    throw new Exception("SDC cache not found");
                }
            }
            catch (Exception ex)
            {
                BaseAppSettings.m_Log.ErrorException(Header + "Error occured." + ex.Message, ex);
                throw ex;

            }
            finally
            {
                BaseAppSettings.m_Log.Trace(Header + "Leaving..");
            }
           
        }

        public List<IKTComponent> GetAllPrinters()
        {
            IKTComponent[] printers = null;
            List<IKTComponent> lstPrinters = new List<IKTComponent>();
            string Header = "ComponentProvider::GetAllPrinters: ";
            BaseAppSettings.m_Log.Trace(Header + "Entering.. ");
            try
            {

                printers = ktComponentfactory.GetAllComponents(KTComponentCategory.Printer);

                if (printers.Length >= 1)
                {
                    foreach (IKTComponent printer in printers)
                    {
                        if (printer is IKTPrinter)
                        {
                            lstPrinters.Add(printer);
                        }
                    }
                    if (lstPrinters != null)
                        return lstPrinters;
                    else
                    {
                        return new List<IKTComponent>();
                    }


                }
                else
                {
                    return new List<IKTComponent>();
                }

            }
            catch (Exception kExp)
            {
                BaseAppSettings.m_Log.ErrorException(Header + "Error occured." + kExp.Message, kExp);
                throw kExp;
            }
            finally
            {
                BaseAppSettings.m_Log.Trace(Header + "Leaving..");
            }
        }

        public IKTComponent GetComponents(string compId)
        {
            IKTComponent aComponent = null;
            string Header = "ComponentProvider::GetAgentComponent: ";
            BaseAppSettings.m_Log.Trace(Header + "Entering.. ");
            try
            {
                aComponent = ktComponentfactory.GetComponent(compId);
                return aComponent;
            }
            catch (Exception kExp)
            {
                BaseAppSettings.m_Log.ErrorException(Header + "Error occured." + kExp.Message, kExp);
                throw kExp;
            }
            finally
            {
                BaseAppSettings.m_Log.Trace(Header + "Leaving..");
            }

        }


        public bool UnregisterChannel()
        {
            TcpChannel remChannel = (TcpChannel)ChannelServices.GetChannel("KTRFTCPCHANNELClient" + iHostPort.ToString());
            //if (remChannel != null)
            //    ChannelServices.UnregisterChannel(remChannel);
            return true;
        }

    }

    [Serializable]
    public struct RFServerConnParam
    {
        /// <summary>
        /// Display name for server.
        /// </summary>
        public string serverDisplayName;

        /// <summary>
        /// IP Address where service is installed.
        /// </summary>
        public string ipAddr;

        /// <summary>
        /// URI of the component factory
        /// </summary>
        public string URI;

        /// <summary>
        /// Remote port at which the factory component is working
        /// </summary>
        public int remotePort;

        /// <summary>
        /// Host at which application want to listen to the factory component
        /// </summary>
        public int hostPort;

        /// <summary>
        /// protocol by which to connect to factory component
        /// </summary>
        public string protocol;
    }
}
