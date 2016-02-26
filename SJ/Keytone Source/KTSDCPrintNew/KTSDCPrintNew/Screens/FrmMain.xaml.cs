using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Configuration;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WinInterop = System.Windows.Interop;
using KTWPFAppBase;
using KTWPFAppBase.Controls;
using System.Threading;
using System.ServiceModel;
using System.Xml;


namespace KTone.Win.KTSDCPrint
{
    /// <summary>
    /// Interaction logic for FrmMain.xaml
    /// </summary>
    public partial class FrmMain : Window
    {
        private static FrmMain m_frmMain = null;
        private WindowSize WindowSize = new WindowSize();
        public static string displayName = "SmartDC Print Application";
        public int roleId = 0;
        public static List<string> accessibleMenus = null;
        private string UserId = string.Empty;
        private string Password = string.Empty;
        private string appName = string.Empty;//

        public FrmMain()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(Window_Loaded);
            this.SourceInitialized += new EventHandler(win_SourceInitialized);

            ApplicationHelper.CheckProcess(this.Title.ToString());

            //string imagePath = @"Images\ATProvisionSplash.PNG";
            //Splash frmSplash = new Splash(imagePath);

            //frmSplash.AppDisplayName = "SmartDC Provision";
            //frmSplash.ShowDialog();
            m_frmMain = this;


            //DisableMenus();
        }


        #region WinSize
        void win_SourceInitialized(object sender, EventArgs e)
        {
            System.IntPtr handle = (new WinInterop.WindowInteropHelper(this)).Handle;
            WinInterop.HwndSource.FromHwnd(handle).AddHook(new WinInterop.HwndSourceHook(WindowSize.WindowProc));
        }
        #endregion

        private void DisableMenus()
        {
            //foreach (ItemsControl menuItem in MnuMain.Items)
            //{
            //    if (menuItem is MenuItem)
            //    {
            //        menuItem.IsEnabled = false;
            //    }
            //    foreach (ItemsControl subMenuItem in menuItem.Items)
            //    {
            //        if (subMenuItem is MenuItem)
            //        {
            //            subMenuItem.IsEnabled = false;
            //        }
            //    }
            //}
        }

        private void btnlogout_Click(object sender, RoutedEventArgs e)
        {
            string headr = "Frmmain::lnkLogout_Click:";
            App.m_Log.Trace(headr + "   Entering ....   ");
            try
            {
                lblUserName.Text = "";
                lblDateTime.Text = "";
                LoadLoginScreen();
                //unRegEvents();
                //lblUserName.Text = "Welcome - " + AppConfigSettings.UserName + " (Store: " + AppConfigSettings.StoreID.ToString() + ")";

            }
            catch (Exception exc)
            {
                App.m_Log.ErrorException(headr + exc.Message, exc);
            }
            finally
            {
                App.m_Log.Trace(headr + "   Leaving ....    ");
            }
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            string headr = "FrmMain::btnSettings_Click:";
            App.m_Log.Trace(headr + "    Entering ....   ");
            try
            {
                LocalSetting frmSettings = new LocalSetting();

                Nullable<bool> result = false;

                // brdMenu.Visibility = Visibility.Collapsed;                               

                result = frmSettings.ShowDialog();

                //if (result == true)
                //{
                    lblUserName.Text = "Welcome - " + AppConfigSettings.UserName;
                    MnuItemBatch_Click(null, null);


                //}
                //else
                //{
                //    MnuItemBatch_Click(null, null);
                    
                //}




            }
            catch (Exception ex)
            {
                App.m_Log.ErrorException(headr + ex.Message, ex);
            }
            finally
            {
                App.m_Log.Trace(headr + "    Leaving ....   ");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string Header = "FrmMain::Window_Loaded: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");
            try
            {
                this.WindowState = WindowState.Maximized;

                //  Connect connect = new Connect();
                // connect.UserServices();
                LoadLoginScreen();
                lblUserName.Text = "Welcome - " + AppConfigSettings.UserName;

                MnuItemBatch_Click(null, null);
                // SetUserSetting();

                //setAppMember();
            }

            catch (Exception ex)
            {
                CustomMessageBox.Show(this, "Error:" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                BaseAppSettings.m_Log.ErrorException(String.Format("{0}Error occured.{1}", Header, ex.Message), ex);
                BaseAppSettings.m_Log.Error(Header + ":: " + Environment.NewLine + "Error:: " + ex.StackTrace);
            }
            finally
            {
                BaseAppSettings.m_Log.Debug(Header + "Leaving..");
            }

        }

        private void LoadLoginScreen()
        {

            string Header = "FrmMain::LoadLoginScreen: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");

            try
            {

                FrmLogin frmLogin = new FrmLogin();
                Nullable<bool> result = false;

                result = frmLogin.ShowDialog();

                if (result == true)
                {
                    //string opName = FrmLogin.OperatorName;
                    int roleID = FrmLogin.RoleID;



                    string appName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
                    string[] tempAppName = appName.Split(new char[] { '.' });
                    appName = tempAppName[0].ToString();

                    //CtrlOperationCICO ctrlCICO = new CtrlOperationCICO();
                    //dckPanel.Children.Add(ctrlCICO);

                }
                else
                {
                    Application.Current.Shutdown();
                }

            }
            catch (Exception ex)
            {
                CustomMessageBox.Show(this, ex.Message, "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
                throw ex;
            }
            finally
            {
                BaseAppSettings.m_Log.Debug(Header + "Leaving..");
            }


        }

        private void MnuRemote_Click(object sender, RoutedEventArgs e)
        {
            string Header = "FrmMain::MnuRemote_Click: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");
            try
            {
                KTRemoteSettings frmRemoteSettings = new KTRemoteSettings();
                frmRemoteSettings.Owner = this;

                if (frmRemoteSettings.ShowDialog().Value == true)
                {
                    System.Windows.Application.Current.Shutdown();
                    Thread.Sleep(5000);
                    System.Windows.Forms.Application.Restart();
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show(this, "Error:" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                BaseAppSettings.m_Log.ErrorException(String.Format("{0}Error occured.{1}", Header, ex.Message), ex);
                BaseAppSettings.m_Log.Error(Header + ":: " + Environment.NewLine + "Error:: " + ex.StackTrace);
            }
            finally
            {
                BaseAppSettings.m_Log.Debug(Header + "Leaving..");
            }
        }

        private void MnuItem_Click(object sender, RoutedEventArgs e)
        {
            string Header = "FrmMain::MnuItem_Click: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");

            try
            {
                Cursor = Cursors.Wait;

                dckPanel.Children.Clear();

                //string itemLocationName = AppConfigSettings.ItemLocationName;
                //if (string.IsNullOrEmpty(itemLocationName.Trim()))
                //{
                //    CustomMessageBox.Show(this, "Please select default printer for Item Print", "Select Printer", MessageBoxButton.OK, MessageBoxImage.Information);
                //    BaseAppSettings.m_Log.Error(Header + ":: Error:: Default printer to print Item is not selected.");
                //    return;
                //}

                //ctrlPrintItem printItem = new ctrlPrintItem();


                // dckPanel.Children.Add(printItem);

            }
            catch (Exception ex)
            {
                CustomMessageBox.Show(this, "Error:" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                BaseAppSettings.m_Log.ErrorException(Header + "Error occured." + ex.Message, ex);
                BaseAppSettings.m_Log.Error(Header + ":: " + Environment.NewLine + "Error:: " + ex.StackTrace);
            }
            finally
            {
                Cursor = Cursors.Arrow;
                BaseAppSettings.m_Log.Debug(Header + "Leaving..");
            }
        }

        private void MnuLocal_Click(object sender, RoutedEventArgs e)
        {
            string Header = "FrmMain::MnuItem_Click: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");

            try
            {

                //  dckPanel.Children.Clear();

                LocalSetting localSetting = new LocalSetting();

                localSetting.Owner = this;

                Nullable<bool> result = localSetting.ShowDialog();

                if (result == true)
                {
                    CustomMessageBox.Show(this, "Setting updated successfully in Config file", "Success", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("Error:" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                BaseAppSettings.m_Log.ErrorException(String.Format("{0}Error occured.{1}", Header, ex.Message), ex);
                BaseAppSettings.m_Log.Error(Header + ":: " + Environment.NewLine + "Error:: " + ex.StackTrace);
            }
            finally
            {
                BaseAppSettings.m_Log.Debug(Header + "Leaving..");
            }

        }

        private void MnuLocation_Click(object sender, RoutedEventArgs e)
        {
            string Header = "FrmMain::MnuItem_Click: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");

            try
            {
                dckPanel.Children.Clear();

                string LocationPrinterName = AppConfigSettings.LocLocationName;
                if (string.IsNullOrEmpty(LocationPrinterName.Trim()))
                {
                    CustomMessageBox.Show(this, "Please select default printer for Location Print", "Select Printer", MessageBoxButton.OK, MessageBoxImage.Information);
                    BaseAppSettings.m_Log.Error(Header + ":: Error:: Default printer to print Location is not selected.");
                    return;
                }

                //PrintLocation printLocation = new PrintLocation();
                //printLocation.Owner = this;


                //Nullable<bool> result = printLocation.ShowDialog();

                //if (result == true)
                //{
                //    //dckPanel.Children.Clear();
                //}
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show(this, "Error:" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                BaseAppSettings.m_Log.ErrorException(String.Format("{0}Error occured.{1}", Header, ex.Message), ex);
                BaseAppSettings.m_Log.Error(Header + ":: " + Environment.NewLine + "Error:: " + ex.StackTrace);
            }
            finally
            {
                BaseAppSettings.m_Log.Debug(Header + "Leaving..");
            }


        }

        private void MnuShipping_Click(object sender, RoutedEventArgs e)
        {

        }


        private void MnuLogout_Click(object sender, RoutedEventArgs e)
        {
            string Header = "Frmmain::btnLogOut_Click:";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");
            try
            {
                // this.Title = displayName;
                lblUserName.Text = "";

                MessageBoxResult msgbxResult = CustomMessageBox.Show(this, "Do you want to logout from application.", "Logout", MessageBoxButton.YesNo);
                if (msgbxResult == MessageBoxResult.Yes)
                {
                    DisableMenus();
                    LoadLoginScreen();
                    // SetUserSetting();
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show(this, "Error:" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                BaseAppSettings.m_Log.ErrorException(String.Format("{0}Error occured.{1}", Header, ex.Message), ex);
                BaseAppSettings.m_Log.Error(Header + ":: " + Environment.NewLine + "Error:: " + ex.StackTrace);
            }
            finally
            {
                BaseAppSettings.m_Log.Debug(Header + "Leaving..");
            }
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void MnuItemBatch_Click(object sender, RoutedEventArgs e)
        {
            string Header = "Frmmain::btnLogOut_Click:";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");
            try
            {

                dckPanel.Children.Clear();

                ctrlItemBatchPrinting printItem = new ctrlItemBatchPrinting();

                dckPanel.Children.Add(printItem);



            }
            catch (Exception ex)
            {
                CustomMessageBox.Show(this, "Error:" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                BaseAppSettings.m_Log.ErrorException(String.Format("{0}Error occured.{1}", Header, ex.Message), ex);
                BaseAppSettings.m_Log.Error(Header + ":: " + Environment.NewLine + "Error:: " + ex.StackTrace);
            }
            finally
            {
                BaseAppSettings.m_Log.Debug(Header + "Leaving..");
            }
        }


    }


}
