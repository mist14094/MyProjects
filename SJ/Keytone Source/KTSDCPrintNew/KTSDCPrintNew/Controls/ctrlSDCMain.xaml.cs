using System;
using System.Windows;
using System.Windows.Controls;
using KTWPFAppBase.Controls;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using KTWPFAppBase;

namespace KTone.Win.KTSDCPrint
{
    /// <summary>
    /// Interaction logic for ctrlMain.xaml
    /// </summary>
    public partial class ctrlMainMenu : UserControl
    {
        private static ctrlMainMenu ctrlmain2 = null;

        public delegate void btnNewSDCDelegate(object sender, RoutedEventArgs args);
        public event btnNewSDCDelegate btnNewSDCEvent = null;
               
        public delegate void btnSearchAssetDelegate(object sender, RoutedEventArgs args);
        public event btnSearchAssetDelegate btnSearchAssetEvent = null;

        public delegate void btnDeActivateAssetDelegate(object sender, RoutedEventArgs args);
        public event btnDeActivateAssetDelegate btnDeActivateAssetEvent = null;

        public delegate void btnLocalSettDelegate(object sender, RoutedEventArgs args);
        public event btnLocalSettDelegate btnOptionEvent = null;

        public ctrlMainMenu()
        {
            InitializeComponent();
        }

        public static ctrlMainMenu GetInstance()
        {
            if (ctrlmain2 == null)
            {
                ctrlmain2 = new ctrlMainMenu();
            }
            //ctrlmain2.expConfig.IsExpanded = false; 
            return ctrlmain2;
        }

        private void btnRemoteSetting_Click(object sender, RoutedEventArgs e)
        {
            string headr = "ctrlMain::btnRegistration_Click:";
           // App.m_Log.Trace(headr + "    Entering .....   ");
            try
            {
                if (btnNewSDCEvent  != null)
                    btnNewSDCEvent(sender, e);
            }
            catch (Exception exc)
            {
        //        App.m_Log.ErrorException(headr + exc.Message, exc);
            }
            finally
            {
             //   App.m_Log.Trace(headr + "   Leaving ....   ");
            }
        }

        private void btnLocal_Click(object sender, RoutedEventArgs e)
        {
            string headr = "ctrlMainMenu::btnSearch_Click:";
           // App.m_Log.Trace(headr + "    Entering ....   ");
            try
            {
                if (btnSearchAssetEvent != null)
                    btnSearchAssetEvent(sender, e);
            }
            catch (Exception exc)
            {
              //  App.m_Log.ErrorException(headr + exc.Message, exc);
            }
            finally
            {
             //   App.m_Log.Trace(headr + "    Leaving ....   ");
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            string headr = "ctrlMain::btnDeActivate_Click:";
          //  App.m_Log.Trace(headr + "    Entering .....   ");
            try
            {
                if (btnDeActivateAssetEvent != null)
                    btnDeActivateAssetEvent(sender, e);
            }
            catch (Exception exc)
            {
              ///  App.m_Log.ErrorException(headr + exc.Message, exc);
            }
            finally
            {
               // App.m_Log.Trace(headr + "   Leaving ....   ");
            }
        }

        private void btnOptions_Click(object sender, RoutedEventArgs e)
        {
            string headr = "ctrlMain::btnOptions_Click:";
           // App.m_Log.Trace(headr + "    Entering .....   ");
            try
            {
                if (btnOptionEvent != null)
                    btnOptionEvent(sender, e);
            }
            catch (Exception exc)
            {
             //   App.m_Log.ErrorException(headr + exc.Message, exc);
            }
            finally
            {
              //  App.m_Log.Trace(headr + "   Leaving ....   ");
            }
        }

        private void ctrlMainMenu_Loaded(object sender, RoutedEventArgs e)
        {
            string headr = "ctrlMainMenu::ctrlMainMenu_Loaded:";
          //  App.m_Log.Trace(headr + "    Entering ....   ");
            try
            {
               // ApplicationPrivilege.EnumVisual(stkPnlMenus, FrmMain.accessibleMenus, true);
            }
            catch (Exception exc)
            {
              //  App.m_Log.ErrorException(headr + exc.Message, exc);
            }
            finally
            {
              //  App.m_Log.Trace(headr + "    Leaving ....   ");
            }
        }
    }
}
