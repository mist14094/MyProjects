using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using KTWPFAppBase;
using System.Xml;
using KTone.Core.KTIRFID;
using System.IO;
using KTWPFAppBase.Controls;
using System.Drawing.Printing;

namespace KTone.Win.KTSDCPrint
{
    /// <summary>
    /// Interaction logic for LocalSetting.xaml
    /// </summary>
    public partial class LocalSetting : Window
    {
        private string appConfigPath = string.Empty;
             
        private int locationId = 0;
        private string locationName = string.Empty;
        private string instanceName = string.Empty;



        private int ItemLocationID = 0;
        private string ItemLocationName = string.Empty;
        private string ItemInstanceName = string.Empty;


        private int LocLocationID = 0;
        private string LocLocationName = string.Empty;
        private string LocInstanceName = string.Empty;

        private List<KTLocationDetails> lstLocation = null;
        private List<KTLocationDetails> lstItem = null;

        private string configPath = string.Empty;
        private Configuration appConfig;

        public LocalSetting()
        {
            InitializeComponent();
        }

        #region [Private_Methods]

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string Header = "LocalSetting::Window_Loaded: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;               

               // var printerLocation = from printerLoc in lstLocation
                                    //  where printerLoc.RFResource.Equals("Printer")
                                  //    select printerLoc;

                //cmbLocPrinter.ItemsSource = lstLocation;
                //cmbLocPrinter.SelectedValuePath = "LocationID";
                //cmbLocPrinter.DisplayMemberPath = "LocationName";

                //if (AppConfigSettings.LocLocationID != null && AppConfigSettings.LocLocationID > 0)
                //{
                //    cmbLocPrinter.Text = AppConfigSettings.LocLocationName;
                //    //AppConfigSettings.LocLocationName;
                //}
                //else
                //{
                //    cmbLocPrinter.SelectedIndex = 0;
                //}

                configPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                appConfig = ConfigurationManager.OpenExeConfiguration(configPath);


                foreach (string strPrinter in PrinterSettings.InstalledPrinters)
                {
                    ComboBoxItem cmbItem = new ComboBoxItem();
                    string[] pname = strPrinter.Split('\\');
                    cmbItem.Content = Convert.ToString( pname[pname.Length-1]).Trim();
                    string item = Convert.ToString(pname[pname.Length - 1]).Trim();
                    cmbItemPrinter.Items.Add(cmbItem);
                }

                if (cmbItemPrinter.Items.Count > 0)
                {

                    if (!string.IsNullOrEmpty(Convert.ToString(appConfig.AppSettings.Settings["Printer"].Value)))
                    {
                        try
                        {
                            cmbItemPrinter.Text = Convert.ToString(appConfig.AppSettings.Settings["Printer"].Value).Trim();
                        }
                        catch
                        {
                        }
                    }

                    else
                    {
                        cmbItemPrinter.SelectedIndex = 0;
                    }
                }

               
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show(this, "Error:" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                BaseAppSettings.m_Log.ErrorException(String.Format("{0}Error occured.{1}", Header, ex.Message), ex);
            }
            finally
            {
                Mouse.OverrideCursor = Cursors.Arrow;
                BaseAppSettings.m_Log.Debug(Header + "Leaving..");
            }

        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            string header = "LocalSettings::SaveLocalSetting:";
            BaseAppSettings.m_Log.Debug(header + "    Entering ....   ");

            bool isSucceed = false;

            try
            {

                if (cmbItemPrinter.Items.Count > 0)
                {
                    string printerName = Convert.ToString(((System.Windows.Controls.ContentControl)(cmbItemPrinter.SelectedValue)).Content);// Convert.ToString(((System.Data.DataRowView)(cmbPrinter.SelectedValue)).Row.ItemArray[1]);
                    appConfig.AppSettings.Settings["Printer"].Value = printerName;

                    AppConfigSettings.PrinterName = printerName;
                }

                appConfig.Save();
                DialogResult = true;

                //string app = Convert.ToString(ConfigurationManager.AppSettings["LocationName"]);

                //string xmlSetting = string.Empty;
                //string appName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
                //appConfigPath = AppDomain.CurrentDomain.BaseDirectory + appName + ".exe.config";
                //// appConfigPath = "../App.config";
                //xmlSetting = SaveLocalSetting();

                //UserAunthetication clsUserAunthetication = new UserAunthetication();
                //isSucceed = clsUserAunthetication.UpdateWinAppSettingByUserID(userId, password, xmlSetting, appName);

                //if (isSucceed)
                //{
                //    CustomMessageBox.Show(this, "Local setting for user " + userId + " have saved successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                //}
                //DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                BaseAppSettings.m_Log.ErrorException(header + ex.Message, ex);
                BaseAppSettings.m_Log.Error(header + ": " + Environment.NewLine + "Error:: " + ex.StackTrace);
                CustomMessageBox.Show(this, "Error : " + ex.Message);
            }
            finally
            {
                BaseAppSettings.m_Log.Debug(header + "Leaving..");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();

        }

        private void cmbLocPrinter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string header = "LocalSettings::cmbLocPrinter_SelectionChanged:";
            BaseAppSettings.m_Log.Debug(header + "    Entering ....   ");

            try
            {
                //int locationId = 8;// Convert.ToInt32(cmbLocPrinter.SelectedValue);

                //var locationDetails = from locDetails in lstLocation
                //                      where locDetails.LocationID == locationId
                //                      select locDetails;

                //foreach (var locDetails in locationDetails)
                //{


                //    LocLocationID = locDetails.LocationID;
                //    LocLocationName = locDetails.LocationName;
                //    LocInstanceName = locDetails.RFValue;
                //}
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show(this, "Error:" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                BaseAppSettings.m_Log.ErrorException(String.Format("{0}Error occured.{1}", header, ex.Message), ex);
                BaseAppSettings.m_Log.Error(header + ": " + Environment.NewLine + "Error:: " + ex.StackTrace);
            }
            finally
            {
                BaseAppSettings.m_Log.Debug(header + "Leaving..");
            }
        }

        private void cmbItemPrinter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string header = "LocalSettings::cmbItemPrinter_SelectionChanged:";
            BaseAppSettings.m_Log.Debug(header + "    Entering ....   ");

            try
            {


                //appConfig.AppSettings.Settings["Printer"].Value = ((System.Windows.Controls.ComboBox)(sender)).Text;

                //appConfig.Save();

                
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show(this,"Error:" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                BaseAppSettings.m_Log.ErrorException(String.Format("{0}Error occured.{1}", header, ex.Message), ex);
                BaseAppSettings.m_Log.Error(header + ": " + Environment.NewLine + "Error:: " + ex.StackTrace);
            }
            finally
            {
                BaseAppSettings.m_Log.Debug(header + "Leaving..");
            }

        }

        #endregion

        #region [public_Methods]

        private void userSetting()
        {
            string AttributeXML = string.Empty;
            MemoryStream stream = new MemoryStream();
            XmlWriterSettings xs = new XmlWriterSettings();
            xs.Encoding = Encoding.UTF8;
            xs.Indent = false;
            xs.ConformanceLevel = ConformanceLevel.Document;
            xs.CheckCharacters = true;
            XmlWriter xmlWriter = XmlWriter.Create(stream, xs);

            xmlWriter.WriteStartDocument();

            xmlWriter.WriteStartElement("WinAppSetting");
            xmlWriter.WriteStartElement("Attributes");

        }

        private string SaveLocalSetting()
        {
            string header = "LocalSettings::SaveLocalSetting:";
            BaseAppSettings.m_Log.Debug(header + "    Entering ....   ");

            string xmlSetting = string.Empty;

            //XmlTextReader reader = null;
            //XmlTextWriter writer = null;
            XmlDocument xmlDoc = new XmlDocument();

            try
            {
                //xmlDoc.Load(appConfigPath);
                xmlDoc.LoadXml(AppConfigSettings.xmlString);

                XmlNodeList nodeList = xmlDoc.SelectNodes("WinAppSetting/Attributes/Attribute");
                if (nodeList == null || nodeList.Count < 1)
                    return xmlSetting;

                foreach (XmlNode ChildNode in nodeList)
                {
                    //string SelectedValue = ChildNode.Attributes["Value"].Value.ToString();
                    String userSettingName = ChildNode.Attributes["Name"].Value.ToString().ToUpper();
                    Attributes userSetting = (Attributes)Enum.Parse(typeof(Attributes), userSettingName);

                    switch (userSetting)
                    {
                        case Attributes.AAPLICATION_NAME:
                            ChildNode.Attributes["Value"].Value = AppConfigSettings.ApplicationName;
                            break;
                        case Attributes.LOC_LOCATION_ID:
                            ChildNode.Attributes["Value"].Value = Convert.ToString(LocLocationID);
                            AppConfigSettings.LocLocationID = LocLocationID;
                            //AppConfigSettings.LocationID = Convert.ToInt32(2);
                            break;
                        case Attributes.LOC_LOCATION_NAME:
                            ChildNode.Attributes["Value"].Value = Convert.ToString(LocLocationName);
                            AppConfigSettings.LocLocationName = LocLocationName;
                            // AppConfigSettings.InstanceName = "";
                            break;
                        case Attributes.LOC_INSTANCE_NAME:
                            ChildNode.Attributes["Value"].Value = Convert.ToString(LocInstanceName);
                            AppConfigSettings.LocInstanceName = LocInstanceName;
                            break;
                        case Attributes.ITEM_LOCATION_ID:
                            ChildNode.Attributes["Value"].Value = Convert.ToString(ItemLocationID);
                            AppConfigSettings.ItemLocationID = ItemLocationID;
                            //AppConfigSettings.LocationID = Convert.ToInt32(2);
                            break;
                        case Attributes.ITEM_LOCATION_NAME:
                            ChildNode.Attributes["Value"].Value = Convert.ToString(ItemLocationName);
                            AppConfigSettings.ItemLocationName = ItemLocationName;
                            // AppConfigSettings.InstanceName = "";
                            break;
                        case Attributes.ITEM_INSTANCE_NAME:
                            ChildNode.Attributes["Value"].Value = Convert.ToString(ItemInstanceName);
                            AppConfigSettings.ItemInstanceName = ItemInstanceName;
                            break;
                    }
                }

                #region [Commented_Code]



                #endregion

                xmlSetting = xmlDoc.InnerXml;


            }
            catch (Exception ex)
            {
                BaseAppSettings.m_Log.ErrorException(header + ex.Message, ex);
                BaseAppSettings.m_Log.Error(header + ": " + Environment.NewLine + "Error:: " + ex.StackTrace);
            }
            finally
            {
                //if (writer != null)
                //    writer.Close();
                //if (reader != null)
                //    reader.Close();
                xmlDoc = null;
                BaseAppSettings.m_Log.Debug(header + "Leaving..");
            }

            return xmlSetting;
        }

        #endregion


    }
}
