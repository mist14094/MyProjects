using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using KTWPFAppBase;
using KTWPFAppBase.Controls;
using System.Threading;
using KTone.Core.SDCBusinessLogic;
using KTone.DAL.KTDAGlobaApplLib;


namespace KTone.Win.KTSDCPrint
{
    /// <summary>
    /// Interaction logic for FrmLogin.xaml
    /// </summary>
    public partial class FrmLogin : Window
    {

        User mUsr;
        UserClass usl;
        public static string UserRole = string.Empty;      
        public static int RoleID = 0;
        public static int dataOwnerID = 0;
        public static string userName = string.Empty;
        
        public FrmLogin()
        {
            string Header = "FrmLogin::btnLogin_Click: ";
            KTWPFAppBase.BaseAppSettings.m_Log.Debug(Header + "Entering.. ");
            try
            {
                WindowSize WindowSize = new WindowSize();                
                InitializeComponent();               
                this.btnCancel.Click += new RoutedEventHandler(btnCancel_Click);
                this.KeyDown += new System.Windows.Input.KeyEventHandler(FrmLogin_KeyDown);

                txtUserID.Focus();

                usl = new UserClass();
                mUsr = new User();


            }
            catch (Exception ex)
            {
                KTWPFAppBase.BaseAppSettings.m_Log.ErrorException(String.Format("{0}Error occured.{1}", Header, ex.Message), ex);
                KTWPFAppBase.BaseAppSettings.m_Log.Error(Header + ":: "+ Environment.NewLine + "Error:: " + ex.StackTrace);           
            }
            finally
            {
                KTWPFAppBase.BaseAppSettings.m_Log.Debug(Header + "Leaving..");
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string Header = "FrmLogin::btnLogin_Click: ";
            KTWPFAppBase.BaseAppSettings.m_Log.Trace(Header + "Entering.. ");
            try
            {
                this.Cursor = System.Windows.Input.Cursors.Wait;
                string userID = txtUserID.Text.Trim();
                string password = txtPassword.Password;

                mUsr.UserName = userID;
                mUsr.Password = password;
                if (mUsr.Validate())
                {
                    DialogResult = true;
                    //AppConfigSettings.RoleID = mUsr.RoleID;
                    //AppConfigSettings.DataOwnerID = mUsr.DataOwnerID;
                    //AppConfigSettings.DataOwnerName = mUsr.DataOwnerName;
                    AppConfigSettings.UserName = mUsr.UserName;
                    AppConfigSettings.UserID = mUsr.UserID;

                    //ApplicationHelper objLogin = new ApplicationHelper();

                    //try
                    //{
                    //    objLogin.ConnectRemoteService();
                    //}
                    //catch (Exception ex)
                    //{
                    //    CustomMessageBox.Show("RFService does not accessible, please contact Administrator.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Information);
                    //    KTWPFAppBase.BaseAppSettings.m_Log.ErrorException(Header + "Error occured." + ex.Message, ex);
                    //   // Application.Current.Shutdown();
                    //}


                    this.Close();
                }
                else
                {
                    txtUserID.Text = "";
                    txtPassword.Password = "";
                    txtUserID.Focus();
                }
            }
            catch (Exception ex)
            {
                txtUserID.Text = "";
                txtPassword.Password = "";
                txtUserID.Focus();

                //isLoginSuccess = false;
                if (ex.Message.Contains("error: 26"))
                {
                    CustomMessageBox.Show("Database connection failed, Please call administrator for further details.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    CustomMessageBox.Show("User could not be authenticated. " + ex.Message + " Please provide proper user name and Password.", "Login", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                KTWPFAppBase.BaseAppSettings.m_Log.ErrorException(Header + "Error occured." + ex.Message, ex);
                //Application.Current.Shutdown();
            }
            finally
            {
                this.Cursor = System.Windows.Input.Cursors.Arrow;
                KTWPFAppBase.BaseAppSettings.m_Log.Trace(Header + "Leaving..");
            }
            
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            string Header = "FrmLogin::btnCancel_Click: ";
            KTWPFAppBase.BaseAppSettings.m_Log.Debug(Header + "Entering.. ");
            try
            {
                DialogResult = false;
                this.Close();
            }
            catch (Exception ex)
            {
                DialogResult = false;
                KTWPFAppBase.BaseAppSettings.m_Log.ErrorException(String.Format("{0}Error occured.{1}", Header, ex.Message), ex);
                KTWPFAppBase.BaseAppSettings.m_Log.Error(Header + ":: " + Environment.NewLine + "Error:: " + ex.StackTrace);
            }
        }

        private void FrmLogin_KeyDown(object sender, System.Windows.Input.KeyEventArgs args)
        {
            if (args.Key == System.Windows.Input.Key.Enter)
                btnLogin_Click(null, null);
        }

        private void lnkRemoteSettings_Click(object sender, RoutedEventArgs e)
        {
            string Header = "Login::Hyperlink_Click: ";
            KTWPFAppBase.BaseAppSettings.m_Log.Debug(Header + "Entering.. ");
            try
            {
                KTRemoteSettings frmRemoteSettings = new KTRemoteSettings();

                if (frmRemoteSettings.ShowDialog().Value == true)
                {
                    System.Windows.Application.Current.Shutdown();
                    Thread.Sleep(5000);
                    System.Windows.Forms.Application.Restart();
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show(this,"Error:" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                KTWPFAppBase.BaseAppSettings.m_Log.ErrorException(String.Format("{0}Error occured.{1}", Header, ex.Message), ex);
                KTWPFAppBase.BaseAppSettings.m_Log.Error(Header + ":: " + Environment.NewLine + "Error:: " + ex.StackTrace);
            }
            finally
            {
                KTWPFAppBase.BaseAppSettings.m_Log.Debug(Header + "Leaving..");
            }
        }
       
    }
}
