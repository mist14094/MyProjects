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
using System.Xml;
using System.Threading;
using System.Text.RegularExpressions;

namespace KTWPFAppBase.Controls
{
    /// <summary>
    /// Interaction logic for KTRemoteSettings.xaml
    /// </summary>
    public partial class KTRemoteSettings : Window
    {
        private string appConfigPath = string.Empty;

        public KTRemoteSettings()
        {
            string Header = "KTRemoteSettings::KTRemoteSettings: ";
            BaseAppSettings.m_Log.Trace(Header + "Entering.. ");
            try
            {
                this.InitializeComponent();
                string appName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
                appConfigPath = AppDomain.CurrentDomain.BaseDirectory + appName + ".exe.config";
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string Header = "KTRemoteSettings::Window_Loaded: ";
            BaseAppSettings.m_Log.Trace(Header + "Entering.. ");
            try
            {
                string ipAddress = string.Empty;
                GetWebServerIPAddress(out ipAddress);
                txtIPAddress.Text = ipAddress;
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
            string Header = "KTRemoteSettings::btnOK_Click: ";
            BaseAppSettings.m_Log.Trace(Header + "Entering.. ");
            try
            {
                if (isValid())
                {
                    if (CustomMessageBox.Show("Changing remote setting will restart the application, are you sure?", "Setup Service Info", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                    {
                        this.DialogResult = false;
                        this.Close();
                        return;
                    }

                    SaveWebServerIPAddress(txtIPAddress.Text.Trim());
                    BaseAppSettings.IPAddress = txtIPAddress.Text.Trim();

                    this.DialogResult = true;
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

        void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            string Header = "KTRemoteSettings::btnCancel_Click: ";
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

        private bool isValid()
        {
            string Header = "KTRemoteSettings::isValid: ";
            BaseAppSettings.m_Log.Trace(Header + "Entering.. ");
            bool isValid = false;
            try
            {
                if (this.txtIPAddress.Text == string.Empty)
                {
                    CustomMessageBox.Show(this, "Please enter WebServer IP Address.", this.Title, MessageBoxButton.OK,MessageBoxImage.Information);
                    //sCustomMessageBox.Show("Please enter WebServer IP Address.", this.Title, MessageBoxButton.OK, MessageBoxImage.Information);
                    return isValid;
                }

                //if (this.txtIPAddress.Text.ToUpper() != "LOCALHOST")
                //{
                //    if (!IsValidIP(this.txtIPAddress.Text.Trim()))
                //    {
                //        CustomMessageBox.Show("Entered WebServer IP Address is not valid.", this.Title, MessageBoxButton.OK, MessageBoxImage.Information);
                //        return isValid;
                //    }
                //}

                isValid = true;
            }
            catch (Exception ex)
            {
                BaseAppSettings.m_Log.ErrorException(Header + "Error occured." + ex.Message, ex);
            }
            return isValid;
        }

        private void GetWebServerIPAddress(out string ipAddress)
        {
            string header = "KTRemoteSettings::GetWebServerIPAddress:";
            BaseAppSettings.m_Log.Trace(header + "    Entering ....   ");

            ipAddress = string.Empty;
            XmlDocument xmlDoc = null;
            try
            {
                xmlDoc = new XmlDocument();
                xmlDoc.Load(appConfigPath);

                XmlNodeList nodeList = xmlDoc.SelectNodes("configuration/system.serviceModel/client/endpoint");
                if (nodeList == null || nodeList.Count < 1)
                    return;

                string wsEndPointAddress = nodeList[0].Attributes["address"].Value.Trim();
                if (wsEndPointAddress.Equals(string.Empty))
                    return;
                string[] protocolAddressParts = wsEndPointAddress.Split(new string[] { "//" }, StringSplitOptions.None);
                if (protocolAddressParts.Length < 2)
                    return;
                string[] addressParts = protocolAddressParts[1].Split(new char[] { '/' });
                ipAddress = addressParts[0];
            }
            catch (Exception ex)
            {
                BaseAppSettings.m_Log.ErrorException(header + ex.Message, ex);
            }
            finally
            {
                xmlDoc = null;
            }
        }

        private void SaveWebServerIPAddress(string ipAddress)
        {
            string header = "KTRemoteSettings::SaveWebServerIPAddress:";
            BaseAppSettings.m_Log.Trace(header + "    Entering ....   ");

            XmlTextReader reader = null;
            XmlTextWriter writer = null;
            XmlDocument xmlDoc = new XmlDocument();

            try
            {
                xmlDoc.Load(appConfigPath);

                XmlNodeList nodeList = xmlDoc.SelectNodes("configuration/system.serviceModel/client/endpoint");
                if (nodeList == null || nodeList.Count < 1)
                    return;

                for (int i = 0; i < nodeList.Count; i++)
                {
                    string wsEndPointAddress = nodeList[i].Attributes["address"].Value.Trim();
                    if (wsEndPointAddress.Equals(string.Empty))
                        return;
                    string[] protocolAddressParts = wsEndPointAddress.Split(new string[] { "//" }, StringSplitOptions.None);
                    if (protocolAddressParts.Length < 2)
                        return;
                    string[] addressParts = protocolAddressParts[1].Split(new char[] { '/' });
                    StringBuilder finalWSAddress = new StringBuilder(protocolAddressParts[0]);
                    finalWSAddress.Append("//");
                    finalWSAddress.Append(ipAddress);
                    finalWSAddress.Append("/");
                    for (int j = 1; j < addressParts.Length; j++)
                    {
                        finalWSAddress.Append(addressParts[j]);
                        if (addressParts.Length - 1 != j)
                            finalWSAddress.Append("/");
                    }
                    nodeList[i].Attributes["address"].Value = finalWSAddress.ToString();
                }

                writer = new XmlTextWriter(appConfigPath, null);
                writer.Formatting = Formatting.Indented;
                xmlDoc.WriteTo(writer);
                writer.Flush();
            }
            catch (Exception ex)
            {
                BaseAppSettings.m_Log.ErrorException(header + ex.Message, ex);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
                if (reader != null)
                    reader.Close();
                xmlDoc = null;
            }
        }

        /// <summary>
        /// method to validate an IP address
        /// using regular expressions. The pattern
        /// being used will validate an ip address
        /// with the range of 1.0.0.0 to 255.255.255.255
        /// </summary>
        /// <param name="addr">Address to validate</param>
        /// <returns></returns>
        public bool IsValidIP(string addr)
        {
            //create our match pattern
            string pattern = @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$";
            //create our Regular Expression object
            Regex check = new Regex(pattern);
            //boolean variable to hold the status
            bool valid = false;
            //check to make sure an ip address was provided
            if (addr == "")
            {
                //no address provided so return false
                valid = false;
            }
            else
            {
                //address provided so use the IsMatch Method
                //of the Regular Expression object
                valid = check.IsMatch(addr, 0);
            }
            //return the results
            return valid;
        }
    }
}
