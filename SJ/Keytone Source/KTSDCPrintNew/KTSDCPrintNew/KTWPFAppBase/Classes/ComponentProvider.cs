using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Serialization.Formatters;

using KTone.Core.KTIRFID;
using System.Collections;
using System.IO;

namespace KTWPFAppBase
{
    public class ComponentProvider
    {
        private static ComponentProvider objFilterProvider = null;
        public static IKTComponentFactory ktComponentfactory;
        public static Dictionary<string, string> ComponentNames;
        public static bool IsConnectedToFactory = false;
        static RFServerConnParam channel;

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
                props["port"] = channelParam.hostPort;
                props["name"] = "KTRFTCPCHANNELClient" + channelParam.hostPort.ToString();
                props["typeUtilsLevel"] = TypeFilterLevel.Full;

                BinaryClientFormatterSinkProvider clientProvider = null;
                BinaryServerFormatterSinkProvider serverProvider = new BinaryServerFormatterSinkProvider();

                serverProvider.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;

                TcpChannel chan = new TcpChannel(props, clientProvider, serverProvider);
                ChannelServices.RegisterChannel(chan, false);
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
                TcpChannel remChannel = (TcpChannel)ChannelServices.GetChannel("KTRFTCPCHANNELClient" + channelParam.hostPort.ToString());
                if (remChannel != null)
                    ChannelServices.UnregisterChannel(remChannel);
                throw ex;
            }
            finally
            {
                BaseAppSettings.m_Log.Trace(Header + "Leaving..");
            }
        }

        public IKTComponent[] GetAllFilters()
        {
            IKTComponent[] filters = null;
            string Header = "ComponentProvider::GetAllFilters: ";
            BaseAppSettings.m_Log.Trace(Header + "Entering.. ");            
            try
            {
                //filter = (IKTWTFilter)ktComponentfactory.GetComponent(KTComponentCategory.Filter, id);
                filters = ktComponentfactory.GetAllComponents(KTComponentCategory.Filter);

                if (filters.Length >= 1)
                {
                    return filters;
                }
                else
                {
                    throw new ApplicationException("Filter not found");
                }
            }
            catch (ApplicationException kExp)
            {
                BaseAppSettings.m_Log.ErrorException(Header + "Error occured." + kExp.Message, kExp);
                throw kExp;
                
            }
            catch (Exception exp)
            {
                BaseAppSettings.m_Log.ErrorException(Header + "Error occured." + exp.Message, exp);
                throw new ApplicationException(exp.Message);
               
            }
            finally
            {
                BaseAppSettings.m_Log.Trace(Header + "Leaving..");
            }
        }

        public IKTComponent[] GetAllReaders()
        {
            IKTComponent[] readers = null;
            string Header = "ComponentProvider::GetAllReaders: ";
            BaseAppSettings.m_Log.Trace(Header + "Entering.. ");   
            
            try
            {
                //filter = (IKTWTFilter)ktComponentfactory.GetComponent(KTComponentCategory.Filter, id);
                readers = ktComponentfactory.GetAllComponents(KTComponentCategory.Reader);

                if (readers.Length >= 1)
                {
                    return readers;
                }
                else
                {
                    throw new ApplicationException("Filter not found");
                }
            }
            catch (ApplicationException kExp)
            {
                BaseAppSettings.m_Log.ErrorException(Header + "Error occured." + kExp.Message, kExp);
                throw kExp;

            }
            catch (Exception exp)
            {
                BaseAppSettings.m_Log.ErrorException(Header + "Error occured." + exp.Message, exp);
                throw new ApplicationException(exp.Message);
            }
            finally
            {
                BaseAppSettings.m_Log.Trace(Header + "Leaving..");
            }
        }

        public IKTComponent GetReaderComponent(string compId)
        {
            IKTComponent aComponent = null;
            string Header = "ComponentProvider::GetReaderComponent: ";
            BaseAppSettings.m_Log.Trace(Header + "Entering.. ");
            try
            {
                aComponent = ktComponentfactory.GetComponent(compId);
                return aComponent;
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

        public static void DisposeObject()
        {
            BaseAppSettings.m_Log.Trace("Entering ComponentProvider:: disposeObject()");
            TcpChannel remChannel = (TcpChannel)ChannelServices.GetChannel("KTRFTCPCHANNELClient" + channel.hostPort.ToString());
            if (remChannel != null)
            {
                ChannelServices.UnregisterChannel(remChannel);
                System.Threading.Thread.Sleep(new TimeSpan(0, 0, 3));
            }
            objFilterProvider = null;
            BaseAppSettings.m_Log.Trace("Leaving ComponentProvider:: disposeObject()");
        }
    }
}