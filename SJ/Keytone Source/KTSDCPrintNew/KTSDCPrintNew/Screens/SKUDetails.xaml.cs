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
using KTone.Win.KTSDCWS_DAL;
using System.Text.RegularExpressions;
using KTone.Core.KTIRFID;
using System.Data;
using System.ComponentModel;
using KTWPFAppBase.Controls;
using System.Configuration;

namespace KTone.Win.KTSDCPrint
{
    /// <summary>
    /// Interaction logic for SKUDetails.xaml
    /// </summary>
    public partial class SKUDetails : Window
    {

        public string userId = string.Empty;
        public string password = string.Empty;

        private List<KTCompanyDetails> lstCompany = null;
        private List<KTProductDetails> lstProduct = null;
        private List<KTSKUDetails> lstSKU = null;

        public static int skuId = 0;
        public static int productId = 0;
        public static int companyId = 0;
        public static string skuName = string.Empty;
        public static string productName = string.Empty;
        public static string companyName = string.Empty;
        public static string skuDescription = string.Empty;
        public static string skuDetailSearchVal = string.Empty;
        public static int records = 0;
        public static int _noOfItems = 0;


        private KTItemDetails objSelectedItem = null;

        public KTItemDetails SelectedItem
        {
            get
            {
                return objSelectedItem;
            }
        }

        public int NoOfRecords
        {
            get
            {
                return records;
            }
        }


        public SKUDetails()
        {
            InitializeComponent();
        }


        #region [private_method]
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

            string Header = "SKUDetails::btnSearch_Click: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");

            try
            {
                skuDetailSearchVal = txtSKU.Text.Trim();

                SearchSKU searchSKU = new SearchSKU();
                // searchSKU.Topmost = true;
                searchSKU.Owner = this;

                Nullable<bool> result = searchSKU.ShowDialog();

                if (result == true)
                {
                    
                    objSelectedItem = new KTItemDetails(App.DataOwnerId, DateTime.Now, AppConfigSettings.LocLocationID, "Printed");

                    objSelectedItem = searchSKU.SelectedItem;

                    this.Height = 320;
                    dckpnlCustomer.Visibility = Visibility.Visible;

                    skuId = searchSKU.SelectedItemList.SKU_ID;
                    txtSKU.Text = searchSKU.SelectedItemList.ProductSKU;
                    productId = searchSKU.SelectedItemList.ProductID;
                    companyId = searchSKU.SelectedItemList.CompanyID;
                    Company.Content = searchSKU.SelectedItemList.CompanyName;
                    Product.Content = searchSKU.SelectedItemList.ProductName;
                    SKUName.Content = searchSKU.SelectedItemList.ProductSKU;
                    companyName = Convert.ToString(Company.Content);
                    productName = Convert.ToString(Product.Content);
                    skuName = Convert.ToString(SKUName.Content);
                    Description.Content = searchSKU.SelectedItemList.SKUDescription;
                    skuDescription = Convert.ToString(Description.Content);
                    
                    btnNext.IsEnabled = true;
                }
                else
                {                                  
                    searchSKU.Close();
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

        private void txtRecords_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }

        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            string Header = "SKUDetails::btnSearch_Click: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");

            try
            {
                if (string.IsNullOrEmpty(txtRecords.Text.Trim()))
                {
                    CustomMessageBox.Show(this, "Number of Item should not be empty.");
                    txtRecords.Text = "1";
                    return;
                }              

                records = Convert.ToInt32(txtRecords.Text.Trim());
                
                _noOfItems = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("NoOfItem_BatchPrint"));

                if (records == 0)
                {
                    CustomMessageBox.Show(this, "Number of records must be greater than 0");
                    txtRecords.Text = "1";
                    return;
                }
                if (records > _noOfItems)
                {
                    CustomMessageBox.Show(this, "Number of records must be less than or equal to 10000");
                    txtRecords.Text = "1";
                    return;
                }

                this.DialogResult = true;
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

        private void txtSKU_LostFocus(object sender, RoutedEventArgs e)
        {

            string Header = "SKUDetails::btnSearch_Click: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");

            try
            {
                userId = KTone.Win.KTSDCWS_DAL.UserAunthetication.UserID;
                password = KTone.Win.KTSDCWS_DAL.UserAunthetication.Password;

                ItemPrint itemPrint = new ItemPrint();
                lstSKU = itemPrint.GetAllSKU(userId, password);
                lstProduct = itemPrint.GetAllProducts(userId, password);
                lstCompany = itemPrint.GetAllCompanies(userId, password);


                string skuName = txtSKU.Text.Trim().ToUpper();

                if (lstSKU != null && lstProduct != null && lstCompany != null)
                {
                    var itemDetails = from listItem in lstSKU
                                      join listProduct in lstProduct
                                      on listItem.ProductID equals listProduct.ProductID
                                      join listComp in lstCompany on listProduct.CompanyID equals listComp.CompanyID
                                      where listItem.ProductSKU.ToUpper() == skuName
                                      select new
                                        {
                                            listItem.SKU_ID,
                                            listComp.CompanyName,
                                            listProduct.ProductName,
                                            listItem.ProductSKU,
                                            listItem.SKUDescription
                                        };


                    if (itemDetails != null && itemDetails.Count() > 0)
                    {
                        foreach (var resCompany in itemDetails)
                        {
                            this.Height = 320;
                            dckpnlCustomer.Visibility = Visibility.Visible;

                            skuId = Convert.ToInt32(resCompany.SKU_ID);
                            Company.Content = resCompany.CompanyName;
                            Product.Content = resCompany.ProductName;
                            SKUName.Content = resCompany.ProductSKU;
                            Description.Content = resCompany.SKUDescription;
                            btnNext.IsEnabled = true;

                        }
                    }

                    else
                    {
                        skuDetailSearchVal = txtSKU.Text.Trim();
                        btnSearch_Click(null, null);
                    }
                }
                else
                {
                    skuDetailSearchVal = txtSKU.Text.Trim();
                    btnSearch_Click(null, null);
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

        #endregion

        private void txtRecords_KeyDown(object sender, KeyEventArgs e)
        {
            if (!((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ||
                           ((e.Key >= Key.D0 && e.Key <= Key.D9) && (!(e.Key == Key.LeftShift) || !(e.Key == Key.RightShift))) ||
                           e.Key == Key.Back || e.Key == Key.Delete || e.Key ==
                           Key.LeftAlt || e.Key == Key.Left || e.Key == Key.Right ||
                           e.Key == Key.LeftShift || e.Key == Key.RightShift || e.Key == Key.Home || e.Key ==
                           Key.End || e.Key == Key.Tab))
                e.Handled = true;// .SuppressKeyPress = true;

        }

        
        
    }
}
