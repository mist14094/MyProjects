using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KTWPFAppBase.Controls;
using System.Windows;
using System.Diagnostics;

namespace KTWPFAppBase
{
    public class ApplicationHelper
    {
        public ApplicationHelper()
        {

        }

        /// <summary>
        /// This method is used to check whether multiple instances of 
        /// same application are running or not.
        /// </summary>
        /// <param name="AppTitle">Name of application for which process has to be checked.</param>
        /// <returns></returns>
        public static void CheckProcess(string AppTitle)
        {
            string Header = "ApplicationHelper::CheckProcess: ";
            BaseAppSettings.m_Log.Trace(Header + "Entering.. ");
            try
            {
                Process prcCurrentProcess = Process.GetCurrentProcess();
                string message = "There is already one instance of " + AppTitle + " running, Do you want to close existing one and start fresh instance?";

                if (Process.GetProcessesByName(prcCurrentProcess.ProcessName).Length > 1)
                {
                    if (MessageBoxResult.Yes == CustomMessageBox.Show(null, message, "Process Monitor", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.None, MessageBoxOptions.None, 200, 500, 14, false))
                    {
                        Process[] activeProcesses = Process.GetProcessesByName(prcCurrentProcess.ProcessName);

                        foreach (Process prc in activeProcesses)
                        {
                            if (prcCurrentProcess.Id != prc.Id)
                                prc.Kill();
                        }
                    }
                    else
                    {
                        prcCurrentProcess.Kill();
                    }
                }
            }
            catch (Exception ex)
            {
                BaseAppSettings.m_Log.ErrorException(Header + "Error occured." + ex.Message, ex);
            }
            finally
            {
                BaseAppSettings.m_Log.Trace(Header + "Leaving..");
            }
        }

        public void ConnectRemoteService()
        {
            string Header = "LoginHelper::ConnectRemoteService: ";
            BaseAppSettings.m_Log.Trace(Header + "Entering.. ");
            try
            {
                RFServerConnParam serverParam = new RFServerConnParam();
                serverParam.hostPort = BaseAppSettings.HostPort;
                serverParam.ipAddr = BaseAppSettings.IPAddress;
                serverParam.protocol = BaseAppSettings.Protocol;
                serverParam.remotePort = BaseAppSettings.RemotePort;
                serverParam.serverDisplayName = BaseAppSettings.ServerName;
                serverParam.URI = BaseAppSettings.URI;

                ComponentProvider.DisposeObject();
                ComponentProvider CompProvider = ComponentProvider.GetInstance(serverParam);

                if (CompProvider == null || ComponentProvider.IsConnectedToFactory == false)
                {
                    throw new ApplicationException("Connection failed! Please check the remote connection setting and login again.");
                }

                BaseAppSettings.RFCompProvider = CompProvider;
            }
            catch (Exception ex)
            {
                BaseAppSettings.m_Log.ErrorException(Header + "Error occured." + ex.Message, ex);
                throw ex;
            }
        }
    }
}
