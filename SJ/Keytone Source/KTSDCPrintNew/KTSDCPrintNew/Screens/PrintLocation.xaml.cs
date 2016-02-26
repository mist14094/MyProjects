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
using KTone.Core.KTIRFID;
using KTone.Win.KTSDCWS_DAL;
using System.Threading;
using System.Configuration;

namespace KTone.Win.KTSDCPrint
{
    /// <summary>
    /// Interaction logic for PrintLocation.xaml
    /// </summary>
    public partial class PrintLocation : Window
    {

        private string userId = KTone.Win.KTSDCWS_DAL.UserAunthetication.UserID;
        private string password = KTone.Win.KTSDCWS_DAL.UserAunthetication.Password;
        private int dataOwnerID = App.DataOwnerId;


        private int locationID = 0;
        private int categoryID = 0;
        private string location = string.Empty;
        private int parentLocationId = 0;
        private string rfResource = string.Empty;
        private string rfValue = string.Empty;
        KTLocationDetails locationDetails = null;
        private List<KTLocationDetails> lstLocation = null;

        public static string LocationName = string.Empty;

        public bool isPrinted = false;
        private bool isExist = false;
        private int printStatus = 1;
        


        public PrintLocation()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            string Header = "PrintLocation::Window_Loaded: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");

            try
            {

                //string LocationPrinterName = AppConfigSettings.LocLocationName;
                //if (string.IsNullOrEmpty(LocationPrinterName.Trim()))
                //{
                //    CustomMessageBox.Show(this, "Please select default printer for Item Print", "Select Printer", MessageBoxButton.OK, MessageBoxImage.Information);
                //    BaseAppSettings.m_Log.Error(Header + ":: Error:: Default printer to print Item is not selected.");
                //    this.Close();
                //}
                //else
                txtPrinter.Text = AppConfigSettings.LocLocationName;

                txtCopies.Text = "1";
                locationID = AppConfigSettings.LocLocationID;
            }
            catch (Exception ex)
            {

                CustomMessageBox.Show(this, "Error:" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                BaseAppSettings.m_Log.ErrorException(String.Format("{0}Error occured.{1}", Header, ex.Message), ex);
                BaseAppSettings.m_Log.Error(Header + ":: " + Environment.NewLine + "Error:: " + ex.StackTrace);
            }
            finally
            {
                Mouse.OverrideCursor = Cursors.Arrow;
                BaseAppSettings.m_Log.Debug(Header + "Leaving..");
            }

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string Header = "PrintLocation::btnSearch_Click: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");

            try
            {

                locationDetails = null;
                LocationName = txtLocation.Text.Trim();

                SearchLocation searchLocation = new SearchLocation();
                searchLocation.Owner = this;
                stkRFTag.Visibility = Visibility.Hidden;
                txtCopies.Text = "1";
                txtDescription.Text = "";
                txtLocationName.Text = "";
                txtRFTag.Text = "";

                

                Nullable<bool> result = searchLocation.ShowDialog();

                if (result == true)
                {

                    if (searchLocation.SelectedLocation != null)
                    {
                        locationDetails = searchLocation.SelectedLocation;

                        if (locationDetails.RFValue != null && locationDetails.RFValue.ToString().Trim().Length > 0)
                        {
                            stkRFTag.Visibility = Visibility.Visible;
                            txtRFTag.Text = locationDetails.RFValue;
                            lblMsg.Content = "Duplicate : You are printing RFID which already have printed.";
                            lblMsg.Foreground = Brushes.Blue;
                            lblMsg.FontWeight = FontWeights.Bold;

                        }
                        else
                        {
                            lblMsg.Content = "";
                            stkRFTag.Visibility = Visibility.Hidden;
                            txtRFTag.Text = "";

                        }

                        categoryID = locationDetails.CategoryID;
                        location = locationDetails.LocationName;
                        txtLocationName.Text = locationDetails.LocationName;
                        txtLocation.Text = locationDetails.LocationName;
                        txtDescription.Text = locationDetails.Description;
                        rfResource = locationDetails.RFResource;

                        btnPrint.IsEnabled = true;
                    }
                    else
                    {
                        txtLocation.Text = "";
                        lblMsg.Content = "";
                        locationDetails = null;
                        btnPrint.IsEnabled = false;
                       
                    }
                }
                else
                {
                    locationDetails = null;
                    btnPrint.IsEnabled = false;
                    CustomMessageBox.Show(this, "Entered location does not exists.", "Location", MessageBoxButton.OK, MessageBoxImage.Question);
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show(this, "Error:" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                BaseAppSettings.m_Log.Error(String.Format("{0}Location Details.{1}", Header, locationDetails));
                BaseAppSettings.m_Log.ErrorException(String.Format("{0}Error occured.{1}", Header, ex.Message), ex);
                BaseAppSettings.m_Log.Error(Header + "::" + Environment.NewLine + "Error:: " + ex.StackTrace);
            }
            finally
            {
                BaseAppSettings.m_Log.Debug(Header + "Leaving..");
            }

        }

        private void txtLocation_LostFocus(object sender, RoutedEventArgs e)
        {
            string Header = "PrintLocation::txtLocation_LostFocus: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");

            txtRFTag.Text = string.Empty;

            LocationName = txtLocation.Text.Trim();
            
            try
            {

                stkRFTag.Visibility = Visibility.Hidden;
                txtLocationName.Text = "";
                txtDescription.Text = "";
                lblMsg.Content = "";

                if (!string.IsNullOrEmpty(LocationName))
                {
                    Location location = new Location();

                    lstLocation = location.GetAllLocation(userId, password);

                    var printerLocation = from printerLoc in lstLocation
                                          where (printerLoc.LocationName.Equals(LocationName)) && printerLoc.RFResource.ToUpper() == "RFTAG"
                                          select printerLoc;


                    if (printerLocation.Count() > 0)
                    {

                        foreach (var v in printerLocation)
                        {
                            stkRFTag.Visibility = Visibility.Hidden;

                            txtLocationName.Text = v.LocationName;
                            txtDescription.Text = v.Description;

                            if (!string.IsNullOrEmpty(v.RFValue.Trim()))
                            {
                                stkRFTag.Visibility = Visibility.Visible;
                                txtRFTag.Text = v.RFValue;

                            }

                            if (!string.IsNullOrEmpty(v.RFValue.Trim()))
                            {
                                lblMsg.Content = "Duplicate : You are printing RFID which already have printed.";
                            }
                            else
                            {
                                lblMsg.Content = "Print : RFTag ID does not exist.";
                            }
                            lblMsg.Foreground = Brushes.Blue;
                            lblMsg.FontWeight = FontWeights.Bold;

                            locationDetails = new KTLocationDetails(v.IsActive, v.LocationID, v.CategoryID, v.ParentLocationId, v.LocationName, v.Description, v.RFResource, v.RFValue, v.CreatedDate, v.ModifiedDate, v.DataOwnerID, v.StencilData, v.LocationZone, v.LocationImage);

                            btnPrint.IsEnabled = true;

                        }


                    }
                    else
                    {
                        if (isPrinted)
                        {
                            isPrinted = false;
                            return;
                        }
                        else
                        {
                            btnSearch_Click(null, null);
                        }
                    }

                }
                else
                {
                    btnSearch_Click(null, null);
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


        void txtCopies_KeyDown(object sender, KeyEventArgs e)
        {

            if (!((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ||
                ((e.Key >= Key.D0 && e.Key <= Key.D9) && (!(e.Key == Key.LeftShift) || !(e.Key == Key.RightShift))) ||
                e.Key == Key.Back || e.Key == Key.Delete || e.Key ==
                Key.LeftAlt || e.Key == Key.Left || e.Key == Key.Right ||
                e.Key == Key.LeftShift || e.Key == Key.RightShift || e.Key == Key.Home || e.Key ==
                Key.End || e.Key == Key.Tab))
                e.Handled = true;

        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            string Header = "PrintLocation::btnPrint_Click: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");
            int timeInterval = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("PrintInterval"));

            bool result = false;
            int numberofcopies = 0;

            

            if (string.IsNullOrEmpty(txtCopies.Text.Trim()))
            {
                CustomMessageBox.Show(this, "No. of copies should not be left blank.", "Validation", MessageBoxButton.OK, MessageBoxImage.Information);
                txtCopies.Text = "1";
                //txtCopies.Focus();
                return;
            }
            else if (Convert.ToInt32(txtCopies.Text.Trim()) == 0)
            {
                CustomMessageBox.Show(this, "No. of copies should be greater than 0.", "Validation", MessageBoxButton.OK, MessageBoxImage.Information);
                txtCopies.Text = "1";
                txtCopies.Focus();
                return;
            }

            try
            {
                string locationName = txtLocationName.Text.Trim();
                string description = txtDescription.Text.Trim();
                numberofcopies = Convert.ToInt32(txtCopies.Text);
                string errMsg = string.Empty;

                DateTime createdDate = DateTime.Now;
                DateTime updateDate = DateTime.Now;

                //locationDetails = new KTLocationDetails(true, locationID, categoryID, parentLocationId, locationName, description, rfResource, rfValue, createdDate, createdDate, dataOwnerID);

                Location locationPrint = new Location();


                if (string.IsNullOrEmpty(txtRFTag.Text))
                {
                    result = locationPrint.LocationPrint(locationDetails, locationID, numberofcopies, LocationPrintOperation.Update, userId, password);
                    isPrinted = true;
                    Cursor = Cursors.Wait;
                    Thread.Sleep(timeInterval);
                    txtLocation_LostFocus(null, null);
                    Cursor = Cursors.Arrow;
                }
                else
                {
                    result = locationPrint.LocationPrint(locationDetails, locationID, numberofcopies, LocationPrintOperation.Duplicate, userId, password);
                }

                if (result)
                {
                    BaseAppSettings.m_Log.Debug("RFTagID successfully printed for Location : {0} , Location Details {1}", txtLocationName.Text, locationDetails.ToString());// (Environment.NewLine + "Error:: " + ex.StackTrace);

                    if (printStatus == 1)
                    {
                        lblMsg.Content = "RFTagID Successfully Printed and available for Re-Print.";
                        isPrinted = true;
                        printStatus = 2;
                    }
                    else
                    {
                        if (isPrinted)
                        {
                            lblMsg.Content = "Re-Print : RFTagID Re-Printed Successfully";
                            isPrinted = false;
                            //CustomMessageBox.Show("Item Re-Printed Successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            lblMsg.Content = "Duplicate : RFTagID Successfully Printed";
                            // CustomMessageBox.Show("Item Successfully Printed", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    lblMsg.Foreground = Brushes.Blue;
                    lblMsg.FontWeight = FontWeights.Bold;
                }
                else
                {
                    BaseAppSettings.m_Log.Debug(Header + " :: Failed to print RFTagID Location : {0} , Location Details {1}", txtLocationName.Text, locationDetails.ToString());// (Environment.NewLine + "Error:: " + ex.StackTrace);

                    lblMsg.Content = "Failed to print RFTagID";
                    lblMsg.Foreground = Brushes.Red;
                }

            }
            catch (Exception ex)
            {
                CustomMessageBox.Show(this, "Error:" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                BaseAppSettings.m_Log.ErrorException(String.Format("{0}Error occured.{1}", Header, ex.Message), ex);
                BaseAppSettings.m_Log.Debug("RFTagID failed to  printe Location : {0} , Location Details {1}", txtLocationName.Text, locationDetails.ToString());// (Environment.NewLine + "Error:: " + ex.StackTrace);
                BaseAppSettings.m_Log.Error(Header + ":: " + Environment.NewLine + "Error:: " + ex.StackTrace);
            }
            finally
            {
                BaseAppSettings.m_Log.Debug(Header + "Leaving..");
            }

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
