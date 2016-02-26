using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Diagnostics;
using KTWPFAppBase;
using NLog;
using System.Xml;
using System.IO;
using KTWPFAppBase.Controls;

namespace KTone.Win.KTSDCPrint
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        #region Attributes

        private static int m_DataOwnerID = 0;
        private static string m_DataOwnerName = string.Empty;
        private static int m_UserID = 0;
        private static int m_RoleID = 0;
        private static string m_UserName = string.Empty;
        private static int locationId = 0;
        public static string instanceName = string.Empty;
        public static string locationName = string.Empty;
        public static string resource = string.Empty;
        public static string rfValue = string.Empty;

        public static Logger m_Log = null;


        #endregion

        public App()
        {
            
           

            Process[] process = Process.GetProcessesByName("KTSDCPrint");

            //if (process.Length > 1)
            //{
            //    //CustomMessageBox.Show("Application already running.", "", MessageBoxButton.OK, MessageBoxImage.Stop);
            //    Process.GetCurrentProcess().Kill();
            //}
            //else
            //{
                Process prCurrent = Process.GetCurrentProcess();

                m_Log = KTWPFAppBase.BaseAppSettings.m_Log;
                m_Log.Info("Application Started:----------------  " + prCurrent.ProcessName + " Time : " + DateTime.Today.ToString() + "-------------------");
            //}
        }

        #region Properties

        public static int DataOwnerId
        {
            get
            {
                return m_DataOwnerID;
            }
            set
            {
                m_DataOwnerID = value;
            }
        }

        public static string DataOwnerName
        {
            get
            {
                return m_DataOwnerName;
            }
            set
            {
                m_DataOwnerName = value;
            }
        }

        public int UserID
        {
            get
            {
                return m_UserID;
            }
            set
            {
                m_UserID = value;
            }
        }

        public static string Resource
        {
            get
            {
                return resource;
            }
            set
            {
                resource = value;
            }
        }

        public static string RFValue
        {
            get
            {
                return rfValue;
            }
            set
            {
                rfValue = value;
            }
        }

        public static string LocationName
        {
            get
            {
                return locationName;
            }
            set
            {
                locationName = value;
            }
        }

        public static string InstanceName
        {
            get
            {
                return instanceName;
            }
            set
            {
                instanceName = value;
            }
        }

        public static int RoleID
        {
            get
            {
                return m_RoleID;
            }
            set
            {
                m_RoleID = value;
            }
        }

        public static int LocationId
        {
            get
            {
                return locationId;
            }
            set
            {
                locationId = value;
            }
        }

        public string UserName
        {
            get
            {
                return m_UserName;
            }
            set
            {
                m_UserName = value;
            }
        }
        #endregion
    }
}
