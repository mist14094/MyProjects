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
using System.Drawing.Printing;
using System.Management;
using KTWPFAppBase;
using KTWPFAppBase.Controls;
using KTone.Core.KTIRFID;
using KTone.Win.KTSDCWS_DAL;

namespace KTone.Win.KTSDCPrint
{
    /// <summary>
    /// Interaction logic for LocationDetails.xaml
    /// </summary>
    public partial class LocationDetails : Window
    {

        private string userId = KTone.Win.KTSDCWS_DAL.UserAunthetication.UserID;
        private string password = KTone.Win.KTSDCWS_DAL.UserAunthetication.Password;
        private int dataOwnerID = App.DataOwnerId;


        private int locationID = 0;
        private int categoryID = 0;
        public LocationDetails()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            getPrinter();
        }

        #region [Private Methods]
           
            private void txtLocation_LostFocus(object sender, RoutedEventArgs e)
            {   
                if(!string.IsNullOrEmpty(txtLocation.Text))
                {
                    string location = txtLocation.Text.Trim();


                }
            }

            private void btnSearch_Click(object sender, RoutedEventArgs e)
            {  
                SearchLocation searchLocation = new SearchLocation();

                Nullable<bool> result = searchLocation.ShowDialog();


                if (result == true)
                {
                    this.Height = 310;                    
                   
                    btnNext.IsEnabled = true;

                }

            }


            #region commented

            //private void btnPrint_Click(object sender, RoutedEventArgs e)
            //{
            //    string Header = "PrintItem::btnPrint_Click: ";
            //    BaseAppSettings.m_Log.Trace(Header + "Entering.. ");



            //    bool result = false;
            //    int locationId = App.LocationId;

            //    if (string.IsNullOrEmpty(txt.Text.Trim()))
            //    {
            //        CustomMessageBox.Show("No. of copies should not be left blank.", "Validation", MessageBoxButton.OK, MessageBoxImage.Information);
            //        return;
            //    }

            //    KTItemDetails ItemDetails = null;

            //    try
            //    {
            //        ItemDetails = new KTItemDetails(dataOwnerID, DateTime.Now, 2, "Printed");//  (2, 2, DateTime.Now, 0);

            //        ItemPrint itemPrint = new ItemPrint();

            //        //ItemDetails value setting

            //        ItemDetails.ItemStatus = "Printed";                  
            //        ItemDetails.DataOwnerID = dataOwnerID;                    
            //        ItemDetails.LastSeenTime = DateTime.Now;
            //        ItemDetails.LastSeenLocation = locationId;
            //        ItemDetails.Status = "Printed";
            //        ItemDetails.Comments = "";
            //        ItemDetails.UpdatedBy = dataOwnerID;
            //        ItemDetails.UpdatedDate = DateTime.Now;

            //        int noOfCopies = Convert.ToInt32(txtCopies.Text.Trim());

            //        foreach (SDCTagData tagData in ItemDetails.TagDetails)
            //        {
            //            tagData.TagID = txtRFTag.Text.Trim();
            //           // tagData.TagType = Convert.ToInt32(txtTagType.Text.Trim());
            //        }

            //        int locationID = AppConfigSettings.LocLocationID;

            //        result = itemPrint.Print(ItemDetails, locationID, noOfCopies, PrintOperation.Duplicate);

            //        if (result)
            //        {

            //            CustomMessageBox.Show("Item Successfully Printed", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            //           // clear();

            //        }
            //        else
            //        {

            //        }

            //    }
            //    catch (Exception ex)
            //    {

            //        CustomMessageBox.Show("Error:" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //        BaseAppSettings.m_Log.ErrorException(String.Format("{0}Error occured.{1}", Header, ex.Message), ex);
            //    }
            //    finally
            //    {
            //        BaseAppSettings.m_Log.Trace(Header + "Leaving..");
            //    }
            //}

            #endregion
           

        #endregion

        #region [Public Method]

            public void getPrinter()
            {
                //string strDefaultPrinter = string.Empty;

                //cmbPrinter.Items.Clear();

                //foreach (String strPrinter in PrinterSettings.InstalledPrinters)
                //{

                //    cmbPrinter.Items.Add(strPrinter);
                //    if (strPrinter == strDefaultPrinter)
                //    {
                //        cmbPrinter.SelectedIndex = cmbPrinter.Items.IndexOf(strPrinter);
                //    }
                //}
                //cmbPrinter.SelectedIndex = 0;

            }

        #endregion

            private void btnNext_Click(object sender, RoutedEventArgs e)
            {
                if (categoryID != 0 && locationID != 0)
                {
                    PrintLocation locationPrint = new PrintLocation();

                    Nullable<bool> result = locationPrint.ShowDialog();


                    if (result == true)
                    { 
                        
                    }

                }
            }
   
           
    }
}
