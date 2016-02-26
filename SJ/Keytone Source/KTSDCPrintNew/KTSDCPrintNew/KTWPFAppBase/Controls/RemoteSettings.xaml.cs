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
using System.Configuration;

namespace KTWPFAppBase.Controls
{
    /// <summary>
    /// Interaction logic for RemoteSettings.xaml
    /// </summary>
    public partial class RemoteSettings : Window
    {
        public RFServerConnParam RFParams;
        public bool isFromLogin = false;

        public RemoteSettings()
        {
            string Header = "RemoteSettings::RemoteSettings: ";
            BaseAppSettings.m_Log.Trace(Header + "Entering.. ");
            try
            {
                this.InitializeComponent();
                this.btnOK.Click += new RoutedEventHandler(btnOK_Click);
                this.btnCancel.Click += new RoutedEventHandler(btnCancel_Click);

                LoadConfigSetting();
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

        private void LoadConfigSetting()
        {
            txtServiceName.Text = BaseAppSettings.ServerName;
            txtIpAddr.Text = BaseAppSettings.IPAddress;
            txtProtocol.Text = BaseAppSettings.Protocol.ToLower();
            txtURI.Text = BaseAppSettings.URI;
            txtRemotePort.Text = BaseAppSettings.RemotePort.ToString();
            //Now Port is Read From App.config
            if (System.Configuration.ConfigurationManager.AppSettings["HostPort"].ToString() != "")
            {
                txtHostPort.Text = System.Configuration.ConfigurationManager.AppSettings["HostPort"].ToString();
            }
            else
            {
                txtHostPort.Text = BaseAppSettings.HostPort.ToString();
            }

            //txtHostPort.Text = BaseAppSettings.HostPort.ToString();

            try
            {
                if (txtServiceName.Text.Trim().Equals(string.Empty))
                    txtServiceName.Text = "localhost";
                if (txtIpAddr.Text.Trim().Equals(string.Empty))
                    txtIpAddr.Text = "localhost";
                if (txtProtocol.Text.Trim().Equals(string.Empty))
                    txtProtocol.Text = "tcp";
                if (txtURI.Text.Trim().Equals(string.Empty))
                    txtURI.Text = "KTComponentFactory";
                if (txtRemotePort.Text.Trim().Equals(string.Empty))
                    txtRemotePort.Text = "21500";
                if (txtHostPort.Text.Trim().Equals(string.Empty))
                    txtHostPort.Text = "9851";
            }
            catch
            {
                BaseAppSettings.m_Log.Error("Problem occured while Loading configuration setting.");
            }
        }

        void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            string Header = "RemoteSettings::btnCancel_Click: ";
            BaseAppSettings.m_Log.Trace(Header + "Entering.. ");
            try
            {
                this.DialogResult = false;
                this.Close();
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

        void btnOK_Click(object sender, RoutedEventArgs e)
        {
            string msg = string.Empty;
            string Header = "RemoteSettings::btnOK_Click: ";
            BaseAppSettings.m_Log.Trace(Header + "Entering.. ");
            try
            {
                if (isValid(out msg))
                {
                    if ((txtServiceName.Text.ToString().ToUpper() == BaseAppSettings.ServerName.ToString().ToUpper()) && (txtIpAddr.Text.ToString().ToUpper() == BaseAppSettings.IPAddress.ToString().ToUpper()) && (txtProtocol.Text.ToString().ToUpper() == BaseAppSettings.Protocol.ToString().ToUpper()) && (txtURI.Text.ToString().ToUpper() == BaseAppSettings.URI.ToString().ToUpper()) && (txtRemotePort.Text.ToString().ToUpper() == BaseAppSettings.RemotePort.ToString().ToUpper()) && (txtHostPort.Text.ToString().ToUpper() == BaseAppSettings.HostPort.ToString().ToUpper()))
                    {
                        this.DialogResult = false;
                        this.Close();
                    }
                    else
                    {
                        if (!isFromLogin)
                        {
                            if (CustomMessageBox.Show("Changing remote setting will restart the application, are you sure?", "Setup Service Info", MessageBoxButton.YesNo,
                             MessageBoxImage.Question) == MessageBoxResult.No)
                            {
                                this.DialogResult = false;
                                return;
                            }
                        }
                        RFParams = new RFServerConnParam();
                        RFParams.serverDisplayName = txtServiceName.Text.Trim();
                        RFParams.ipAddr = txtIpAddr.Text.Trim();
                        RFParams.protocol = txtProtocol.Text.Trim();
                        RFParams.URI = txtURI.Text.Trim();
                        RFParams.remotePort = Convert.ToInt32(txtRemotePort.Text.Trim());
                        RFParams.hostPort = Convert.ToInt32(txtHostPort.Text.Trim());

                        BaseAppSettings.SaveNewConfigParams(RFParams);

                        this.DialogResult = true;
                        this.Close();
                    }
                }
                else
                {
                    CustomMessageBox.Show(msg, "Setup Service Info", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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

        private bool isValid(out string msg)
        {
            msg = "";
            string Header = "RemoteSettings::isValid: ";
            BaseAppSettings.m_Log.Trace(Header + "Entering.. ");
            try
            {
                bool isValid = true;

                if (txtServiceName.Text.Trim().Equals(string.Empty))
                {
                    isValid = false;
                    msg = "Check service Name";
                }

                if (txtIpAddr.Text.Trim().Equals(string.Empty))
                {
                    isValid = false;
                    msg = "Check Ip Address";
                }

                if (txtProtocol.Text.Trim().Equals(string.Empty))
                {
                    isValid = false;
                    msg = "Check Protocol";
                }

                if (txtURI.Text.Trim().Equals(string.Empty))
                {
                    isValid = false;
                    msg = "Check URI";
                }

                if (txtRemotePort.Text.Trim().Equals(string.Empty))
                {
                    isValid = false;
                    msg = "Check remote port";
                }

                if (txtHostPort.Text.Trim().Equals(string.Empty))
                {
                    isValid = false;
                    msg = "Check host port";
                }

                return isValid;
            }
            catch (Exception ex)
            {
                BaseAppSettings.m_Log.ErrorException(Header + "Error occured." + ex.Message, ex);
                return false;
            }
            finally
            {
                BaseAppSettings.m_Log.Trace(Header + "Leaving..");
            }
        }
    }
}
