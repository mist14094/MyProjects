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
using NLog;
using KTone.Win.KTSDCWS_DAL;
using KTone.Core.KTIRFID;
using System.Data;
using KTWPFAppBase;
using System.ComponentModel;
using KTWPFAppBase.Controls;

namespace KTone.Win.KTSDCPrint
{
    /// <summary>
    /// Interaction logic for SearchLocation.xaml
    /// </summary>
    public partial class SearchLocation : Window
    {
        string[] _lstSearchCriteria = new string[] { "Location Category", "RF Resource" };

        private string userId = KTone.Win.KTSDCWS_DAL.UserAunthetication.UserID;
        private string password = KTone.Win.KTSDCWS_DAL.UserAunthetication.Password;

        private List<KTLocationDetails> lstLocation = null;
        private KTLocationDetails objSelectedLocation = null;

        public KTLocationDetails SelectedLocation
        {
            get
            {
                return objSelectedLocation;
            }
        }
             

        public SearchLocation()
        {
            InitializeComponent();

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            string Header = "SearchLocation::Window_Loaded: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");
            try
            {
                cmbSearchCriteria.SelectedIndex = 0;
                if (!string.IsNullOrEmpty(PrintLocation.LocationName.Trim()))
                {
                    txtLocationName.Text = PrintLocation.LocationName.Trim();
                    btnSearch_Click(null, null);
                }

            }
            catch (Exception ex)
            {
                BaseAppSettings.m_Log.ErrorException(Header + ": " + String.Format("{0}Error occured.{1}", Header, ex.Message), ex);
                BaseAppSettings.m_Log.Error(Header + ": " + Environment.NewLine + "Error:: " + ex.StackTrace);
            }
            finally
            {
                Mouse.OverrideCursor = Cursors.Arrow;
                BaseAppSettings.m_Log.Debug(Header + "Leaving..");
            }
        }

        #region [Private Events]
       

        private void cmbSearchCriteria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string Header = "SearchLocation::cmbSearchCriteria_SelectionChanged: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");
            try
            {
                lblError.Visibility = Visibility.Hidden;
                foreach (ComboBoxItem item in e.AddedItems)
                {
                    if (string.Equals(item.Content, "Location Name"))
                    {
                        stkPnlLocName.Visibility = Visibility.Visible;
                        stkPnlResource.Visibility = Visibility.Collapsed;
                    }
                    else if (string.Equals(item.Content, "Resource"))
                    {
                        stkPnlLocName.Visibility = Visibility.Collapsed;
                        stkPnlResource.Visibility = Visibility.Visible;
                    }
                }
              
            }
            catch (Exception ex)
            {
                BaseAppSettings.m_Log.ErrorException(String.Format("{0}Error occured.{1}", Header, ex.Message), ex);
                BaseAppSettings.m_Log.Error(Header + ": " + Environment.NewLine + "Error:: " + ex.StackTrace);
            }
            finally
            {
                BaseAppSettings.m_Log.Debug(Header + "Leaving..");
            }

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string Header = "SearchLocation::btnSearch_Click: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");
            try
            {
                lblError.Visibility = Visibility.Hidden;

                if (isValid())
                {
                    lblError.Content = "";
                    bindData();
                    btnOk.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                BaseAppSettings.m_Log.ErrorException(String.Format("{0}Error occured.{1}", Header, ex.Message), ex);
                BaseAppSettings.m_Log.Error(Header + ": " + Environment.NewLine + "Error:: " + ex.StackTrace);
            }
            finally
            {
                BaseAppSettings.m_Log.Debug(Header + "Leaving..");
            }

        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            string Header = "SearchLocation::btnClear_Click: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");
            try
            {

            }
            catch (Exception ex)
            {
                BaseAppSettings.m_Log.ErrorException(String.Format("{0}Error occured.{1}", Header, ex.Message), ex);
                BaseAppSettings.m_Log.Error(Header + ": " + Environment.NewLine + "Error:: " + ex.StackTrace);
            }
            finally
            {
                BaseAppSettings.m_Log.Debug(Header + "Leaving..");
            }
        }


        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            string Header = "SearchLocation::btnOk_Click: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");
            try
            {
                if (lvLocationDetails.SelectedItems.Count > 0)
                {
                    objSelectedLocation = (KTLocationDetails)lvLocationDetails.SelectedItem;
                    this.DialogResult = true;
                }
                else
                    this.DialogResult = false;
            }
            catch (Exception ex)
            {
                this.DialogResult = false;
                BaseAppSettings.m_Log.ErrorException(String.Format("{0}Error occured.{1}", Header, ex.Message), ex);
                BaseAppSettings.m_Log.Error(":: ListView Selected Item :: " + lvLocationDetails.SelectedItem.ToString());
                BaseAppSettings.m_Log.Error(Header + ": " + Environment.NewLine + "Error:: " + ex.StackTrace);
            }
            finally
            {
                BaseAppSettings.m_Log.Debug(Header + "Leaving..");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            string Header = "SearchLocation::btnCancel_Click: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");
            try
            {
                objSelectedLocation = null;
                this.DialogResult = true;

               // this.Close();
            }
            catch (Exception ex)
            {
                BaseAppSettings.m_Log.ErrorException(String.Format("{0}Error occured.{1}", Header, ex.Message), ex);
                BaseAppSettings.m_Log.Error(Header + ": " + Environment.NewLine + "Error:: " + ex.StackTrace);
            }
            finally
            {
                BaseAppSettings.m_Log.Debug(Header + "Leaving..");
            }

        }

        #endregion


        #region [Public Method]

        public bool isValid()
        {
            string Header = "SearchLocation::isValid: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");

            bool isvalid = false;
            try
            {
                if (cmbSearchCriteria.Text.Equals("Location Name"))
                {
                    if (txtLocationName.Text.Trim() == string.Empty)
                    {
                        lblError.Content = "Please enter Location Name.";
                        lblError.Foreground = Brushes.Red;
                        lvLocationDetails.ItemsSource = null;
                        lblError.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        isvalid = true;
                    }
                }
                else if (cmbSearchCriteria.Text.Equals("Resource"))
                {
                    if (txtResource.Text.Trim() == string.Empty)
                    {
                        lblError.Content = "Please enter Resource value.";
                        lblError.Foreground = Brushes.Red;
                        lvLocationDetails.ItemsSource = null;
                        lblError.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        isvalid = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                BaseAppSettings.m_Log.Debug(Header + "Leaving..");
            }

            return isvalid;
        }

        public void bindData()
        {
            string Header = "SearchLocation::bindData: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");

            try
            {
                Location location = new Location();

                lstLocation = location.GetAllLocation(userId, password);

                if (cmbSearchCriteria.Text.Equals("Location Name"))
                {
                    string locationName = txtLocationName.Text.Trim().ToUpper();
                    var printerLocation = from printerLoc in lstLocation
                                          where printerLoc.LocationName.ToUpper().Contains(locationName) && printerLoc.RFResource.ToUpper() == "RFTAG"
                                          select printerLoc;

                    if (printerLocation.Count() > 0)
                    {

                        lvLocationDetails.ItemsSource = printerLocation;
                    }
                    else
                    {
                        lblError.Content = "Location details not available for selected location.";
                        lblError.Foreground = Brushes.Red;
                        lvLocationDetails.ItemsSource = null;
                        lblError.Visibility = Visibility.Visible;
                    }

                }
                else if (cmbSearchCriteria.Text.Equals("Resource"))
                {
                    string rfValue = txtResource.Text.Trim().ToUpper();
                    var printerLocation = from printerLoc in lstLocation
                                          where printerLoc.RFResource.ToUpper().Equals(rfValue) 
                                          select printerLoc;

                    if (printerLocation.Count() > 0)
                    {

                        lvLocationDetails.ItemsSource = printerLocation;
                    }
                    else
                    {
                        lblError.Content = "Location details not available for selected location.";
                        lblError.Foreground = Brushes.Red;
                        lvLocationDetails.ItemsSource = null;
                        lblError.Visibility = Visibility.Visible;
                    }
                }

                


            }
            catch (Exception ex)
            {
                BaseAppSettings.m_Log.Error(":: Error Occured :: " + Header + Environment.NewLine + ":: Error :: " + ex.StackTrace + Environment.NewLine + "LocationDetails:: " + lstLocation.ToString());
                throw ex;
            }
            finally
            {
                BaseAppSettings.m_Log.Debug(Header + "Leaving..");
            }
        }

        #endregion

        private void lvLocationDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string Header = "SearchLocation::lvLocationDetails_SelectionChanged: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");

            try
            {
                if (lvLocationDetails.SelectedItems.Count > 0)
                    objSelectedLocation = (KTLocationDetails)lvLocationDetails.SelectedItem;
                // selectedLocation = (((System.Data.DataRowView)(lvLocationDetails.SelectedItem))).Row;
                btnOk.IsEnabled = true;
            }
            catch (Exception ex)
            {
                BaseAppSettings.m_Log.ErrorException(String.Format("{0}Error occured.{1}", Header, ex.Message), ex);
                BaseAppSettings.m_Log.Error(":: ListView Selected Item :: " + lvLocationDetails.SelectedItem.ToString());
                BaseAppSettings.m_Log.Error(Header + ": " + Environment.NewLine + "Error:: " + ex.StackTrace);
            }
            finally
            {
                BaseAppSettings.m_Log.Debug(Header + "Leaving..");
            }
        }

    }

}
