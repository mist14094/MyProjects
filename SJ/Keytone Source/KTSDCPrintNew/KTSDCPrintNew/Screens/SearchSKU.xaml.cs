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

namespace KTone.Win.KTSDCPrint
{
    /// <summary>
    /// Interaction logic for SearchSKU.xaml
    /// </summary>
    public partial class SearchSKU : Window
    {

        private Regex regexExpression = new Regex("[a-zA-Z0-9._]");


        #region [private_member]
        private List<KTCompanyDetails> lstCompany = null;
        private List<KTProductDetails> lstProduct = null;
        private List<KTSKUDetails> lstSKU = null;

        private ItemDetailList prodList = null;


        private KTItemDetails objSelectedItem = null;

        public KTItemDetails SelectedItem
        {
            get
            {
                return objSelectedItem;
            }
        }

        public ItemDetailList SelectedItemList
        {
            get
            {
                return prodList;
            }
        }

        private object ItemDetails = null;

        #endregion

        private string userID = string.Empty;
        private string password = string.Empty;

        public bool isClose = true;

        public SearchSKU()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            string Header = "SearchSKU::Window_Loaded: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");
            try
            {

                fillCompany();
                fillProduct();
                fillSKU();
                cmbSearchCriteria.SelectedIndex = 0;
                if (!string.IsNullOrEmpty(SKUDetails.skuDetailSearchVal))
                {
                    txtSKUName.Text = SKUDetails.skuDetailSearchVal;
                    btnSearch_Click(null, null);
                }
                
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("Error:" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                BaseAppSettings.m_Log.ErrorException(String.Format("{0}Error occured.{1}", Header, ex.Message), ex);
                BaseAppSettings.m_Log.Error(Header + ": " + Environment.NewLine + "Error:: " + ex.StackTrace);
            }
            finally
            {
                Mouse.OverrideCursor = Cursors.Arrow;
                BaseAppSettings.m_Log.Debug(Header + "Leaving..");
            }
        }


        #region [public_method]

        public void fillCompany()
        {
            string Header = "SearchSKU::fillCompany: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");
            try
            {

                userID = KTone.Win.KTSDCWS_DAL.UserAunthetication.UserID;
                password = KTone.Win.KTSDCWS_DAL.UserAunthetication.Password;

                ItemPrint itemPrint = new ItemPrint();
                lstCompany = itemPrint.GetAllCompanies(userID, password);

                // cmbCompany.ItemsSource = lstCompany;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                BaseAppSettings.m_Log.Debug(Header + "Leaving..");
            }
        }

        public void fillProduct()
        {
            string Header = "SearchSKU::fillProduct: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");
            try
            {
                userID = KTone.Win.KTSDCWS_DAL.UserAunthetication.UserID;
                password = KTone.Win.KTSDCWS_DAL.UserAunthetication.Password;


                ItemPrint itemPrint = new ItemPrint();
                lstProduct = itemPrint.GetAllProducts(userID, password);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                BaseAppSettings.m_Log.Debug(Header + "Leaving..");
            }
            // cmbProduct.ItemsSource = lstProduct;
        }

        public void fillSKU()
        {
            string Header = "SearchSKU::fillProduct: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");
            try
            {
                userID = KTone.Win.KTSDCWS_DAL.UserAunthetication.UserID;
                password = KTone.Win.KTSDCWS_DAL.UserAunthetication.Password;

                //ProductSKU = "productSKUTest"
                //SKU_ID = 1

                ItemPrint itemPrint = new ItemPrint();
                lstSKU = itemPrint.GetAllSKU(userID, password);

                var result = from listItem in lstSKU
                             //where listItem.ProductSKU.ToUpper().Contains("SKU")
                             select listItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                BaseAppSettings.m_Log.Debug(Header + "Leaving..");
            }
            //cmbSKU.ItemsSource = lstSKU;
        }

        #endregion


        #region [private_methods]

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            string Header = "SearchSKU::btnCancel_Click: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");

            try
            {
                MessageBoxResult msgbxResult = CustomMessageBox.Show(this, "Do you want to cancel SKU search.", "Cancel", MessageBoxButton.YesNo);

                if (msgbxResult == MessageBoxResult.Yes)
                {
                    isClose = false;
                    this.DialogResult = false;
                    this.Close();
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
            string Header = "SearchSKU::btnSearch_Click: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");
            try
            {
                lblError.Content = "";
                lvProductDetails.ItemsSource = null;
                lvSKUDetails.ItemsSource = null;

                if (GetPageData())
                {
                    bindData();

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

        public void bindData()
        {
            string Header = "SearchSKU::btnSearch_Click: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");

            try
            {
                #region searchBySKU
                if (cmbSearchCriteria.Text.Trim() == "SKU Name")
                {

                    string skuName = txtSKUName.Text.Trim().ToUpper();

                    var itemDetails = from listItem in lstSKU
                                      join listProduct in lstProduct
                                      on listItem.ProductID equals listProduct.ProductID
                                      join listComp in lstCompany on listProduct.CompanyID equals listComp.CompanyID
                                      where listItem.ProductSKU.ToUpper().Contains(skuName)
                                      select new ItemDetailList
                                      {
                                          SKU_ID = Convert.ToInt32(listItem.SKU_ID),
                                          SKUDescription = listItem.SKUDescription,
                                          ProductSKU = listItem.ProductSKU,
                                          ProductID = Convert.ToInt32(listItem.ProductID),
                                          DataOwnerID = Convert.ToInt32(listItem.DataOwnerID),
                                          ProductName = listProduct.ProductName,
                                          ProductPrefix = listProduct.ProductPrefix,
                                          CompanyID = listComp.CompanyID,
                                          CompanyName = listComp.CompanyName,
                                          CompanyPrefix = listComp.CompanyPrefix,
                                          CreatedDate = listComp.CreatedDate,
                                          UpdatedDate = listComp.UpdatedDate,
                                          CreatedBy = listComp.CreatedBy
                                      };


                    lvSKUDetails.ItemsSource = itemDetails;


                    List<ListProduct> lstProdList = new List<ListProduct>();

                    bool itemExist = false;

                    foreach (var abc in itemDetails)
                    {
                        ListProduct lst = new ListProduct();
                        lst.ProductID = abc.ProductID;
                        lst.ProductName = abc.ProductName;
                        lst.ProductPrefix = abc.ProductPrefix;
                        lst.CompanyName = abc.CompanyName;

                        foreach (ListProduct lstExist in lstProdList)
                        {
                            if (lstExist.ProductID == lst.ProductID)
                            {
                                itemExist = true;
                                break;
                            }
                        }

                        if (!itemExist)
                        {
                            lstProdList.Add(lst);
                        }
                    }

                    if (lstProdList.Count > 0)
                    {

                        lvProductDetails.ItemsSource = lstProdList;

                        ItemDetails = itemDetails;
                    }

                    else
                    {
                        lblError.Content = "SKU details not available for selected SKU";
                        lblError.Visibility = Visibility.Visible;
                    }

                }
                #endregion


                #region [Search By Product]

                else if (cmbSearchCriteria.Text.Trim() == "Product Name")
                {


                    string productName = txtProductName.Text.Trim().ToUpper();


                    var itemDetails = from listItem in lstSKU
                                      join listProduct in lstProduct
                                      on listItem.ProductID equals listProduct.ProductID
                                      join listComp in lstCompany on listProduct.CompanyID equals listComp.CompanyID
                                      where listProduct.ProductName.ToUpper().Contains(productName)
                                      select new ItemDetailList
                                      {
                                          SKU_ID = Convert.ToInt32(listItem.SKU_ID),
                                          SKUDescription = listItem.SKUDescription,
                                          ProductSKU = listItem.ProductSKU,
                                          ProductID = Convert.ToInt32(listItem.ProductID),
                                          DataOwnerID = Convert.ToInt32(listItem.DataOwnerID),
                                          ProductName = listProduct.ProductName,
                                          ProductPrefix = listProduct.ProductPrefix,
                                          CompanyID = listComp.CompanyID,
                                          CompanyName = listComp.CompanyName,
                                          CompanyPrefix = listComp.CompanyPrefix,
                                          CreatedDate = listComp.CreatedDate,
                                          UpdatedDate = listComp.UpdatedDate,
                                          CreatedBy = listComp.CreatedBy
                                      };

                    lvSKUDetails.ItemsSource = itemDetails;


                    var ProductDetails = from listProduct in lstProduct
                                         join listComp in lstCompany
                                         on listProduct.CompanyID equals listComp.CompanyID
                                         where listProduct.ProductName.ToUpper().Contains(productName)
                                         select new ListProduct
                                         {
                                             ProductID = Convert.ToInt32(listProduct.ProductID),
                                             ProductName = listProduct.ProductName,
                                             ProductPrefix = listProduct.ProductPrefix,
                                             CompanyName = listComp.CompanyName
                                         };


                    List<ListProduct> lstProdList = new List<ListProduct>();

                    bool itemExist = false;

                    foreach (var product in ProductDetails)
                    {
                        ListProduct lst = new ListProduct();
                        lst.ProductID = product.ProductID;
                        lst.ProductName = product.ProductName;
                        lst.ProductPrefix = product.ProductPrefix;
                        lst.CompanyName = product.CompanyName;

                        foreach (ListProduct lstExist in lstProdList)
                        {
                            if (lstExist.ProductID == lst.ProductID)
                            {
                                itemExist = true;
                                break;
                            }
                        }

                        if (!itemExist)
                        {
                            lstProdList.Add(lst);
                        }
                    }

                    if (lstProdList.Count > 0)
                    {

                        lvProductDetails.ItemsSource = lstProdList;

                        ItemDetails = itemDetails;
                    }

                    else
                    {
                        lblError.Content = "SKU details not available for selected Product";
                        lblError.Visibility = Visibility.Visible;
                    }

                    //lvProductDetails.ItemsSource = lstProdList;




                }

                #endregion

                #region [porduct_prefix]
                else if (cmbSearchCriteria.Text.Trim() == "Product Prefix")
                {


                    string productPrefix = txtProductPrefix.Text.Trim().ToUpper();

                    var itemDetails = from listItem in lstSKU
                                      join listProduct in lstProduct
                                      on listItem.ProductID equals listProduct.ProductID
                                      join listComp in lstCompany on listProduct.CompanyID equals listComp.CompanyID
                                      where listProduct.ProductPrefix.ToUpper().Contains(productPrefix)
                                      select new
                                      {
                                          SKU_ID = Convert.ToInt32(listItem.SKU_ID),
                                          SKUDescription = listItem.SKUDescription,
                                          ProductSKU = listItem.ProductSKU,
                                          ProductID = Convert.ToInt32(listItem.ProductID),
                                          DataOwnerID = Convert.ToInt32(listItem.DataOwnerID),
                                          ProductName = listProduct.ProductName,
                                          ProductPrefix = listProduct.ProductPrefix,
                                          CompanyID = listComp.CompanyID,
                                          CompanyName = listComp.CompanyName,
                                          CompanyPrefix = listComp.CompanyPrefix,
                                          CreatedDate = listComp.CreatedDate,
                                          UpdatedDate = listComp.UpdatedDate,
                                          CreatedBy = listComp.CreatedBy
                                      };

                    lvSKUDetails.ItemsSource = itemDetails;


                    var productDetails = (
                                           from listProduct in itemDetails
                                           select new
                                           {
                                               listProduct.ProductID,
                                               listProduct.ProductName,
                                               listProduct.ProductPrefix,
                                               listProduct.CompanyName

                                           }).Distinct();


                    lvProductDetails.ItemsSource = productDetails;

                }

                #endregion


                #region [Company]
                else if (cmbSearchCriteria.Text.Trim() == "Company Name")
                {


                    string companyName = txtCompany.Text.Trim().ToUpper();

                    var itemDetails = from listItem in lstSKU
                                      join listProduct in lstProduct
                                      on listItem.ProductID equals listProduct.ProductID
                                      join listComp in lstCompany on listProduct.CompanyID equals listComp.CompanyID
                                      where listComp.CompanyName.ToUpper().Contains(companyName)
                                      select new ItemDetailList
                                      {
                                          SKU_ID = Convert.ToInt32(listItem.SKU_ID),
                                          SKUDescription = listItem.SKUDescription,
                                          ProductSKU = listItem.ProductSKU,
                                          ProductID = Convert.ToInt32(listItem.ProductID),
                                          DataOwnerID = Convert.ToInt32(listItem.DataOwnerID),
                                          ProductName = listProduct.ProductName,
                                          ProductPrefix = listProduct.ProductPrefix,
                                          CompanyID = listComp.CompanyID,
                                          CompanyName = listComp.CompanyName,
                                          CompanyPrefix = listComp.CompanyPrefix,
                                          CreatedDate = listComp.CreatedDate,
                                          UpdatedDate = listComp.UpdatedDate,
                                          CreatedBy = listComp.CreatedBy
                                      };


                    lvSKUDetails.ItemsSource = itemDetails;

                    var ProductDetails = from listProduct in lstProduct
                                         join listComp in lstCompany
                                         on listProduct.CompanyID equals listComp.CompanyID
                                         where listComp.CompanyName.ToUpper().Contains(companyName)
                                         select new ListProduct
                                         {
                                             ProductID = Convert.ToInt32(listProduct.ProductID),
                                             ProductName = listProduct.ProductName,
                                             ProductPrefix = listProduct.ProductPrefix,
                                             CompanyName = listComp.CompanyName
                                         };


                    List<ListProduct> lstProdList = new List<ListProduct>();

                    bool itemExist = false;

                    foreach (var product in ProductDetails)
                    {
                        ListProduct lst = new ListProduct();
                        lst.ProductID = product.ProductID;
                        lst.ProductName = product.ProductName;
                        lst.ProductPrefix = product.ProductPrefix;
                        lst.CompanyName = product.CompanyName;

                        foreach (ListProduct lstExist in lstProdList)
                        {
                            if (lstExist.ProductID == lst.ProductID)
                            {
                                itemExist = true;
                                break;
                            }
                        }

                        if (!itemExist)
                        {
                            lstProdList.Add(lst);
                        }
                    }

                    if (lstProdList.Count > 0)
                    {

                        lvProductDetails.ItemsSource = lstProdList;

                        ItemDetails = itemDetails;
                    }

                    else
                    {
                        lblError.Content = "SKU details not available for selected Company";
                        lblError.Visibility = Visibility.Visible;
                    }

                    //lvProductDetails.ItemsSource = lstProdList;

                    #region [Commented]


                    #endregion
                }
                #endregion


                #region [Company Prefix]

                else if (cmbSearchCriteria.Text.Trim() == "Company Prefix")
                {


                    string companyPrefix = txtCompanyPrefix.Text.Trim().ToUpper();

                    var itemDetails = from listItem in lstSKU
                                      join listProduct in lstProduct
                                      on listItem.ProductID equals listProduct.ProductID
                                      join listComp in lstCompany on listProduct.CompanyID equals listComp.CompanyID
                                      where listComp.CompanyPrefix.ToUpper().Contains(companyPrefix)
                                      select new
                                      {
                                          listItem.SKU_ID,
                                          listItem.SKUDescription,
                                          listItem.ProductSKU,
                                          listItem.ProductID,
                                          listItem.DataOwnerID,
                                          listProduct.ProductName,
                                          listProduct.ProductPrefix,
                                          listComp.CompanyID,
                                          listComp.CompanyName,
                                          listComp.CompanyPrefix,
                                          listComp.CreatedDate,
                                          listComp.UpdatedDate
                                      };


                    var productDetails = (
                                            from listProduct in itemDetails
                                            select new
                                            {
                                                listProduct.ProductID,
                                                listProduct.ProductName,
                                                listProduct.ProductPrefix,
                                                listProduct.CompanyName

                                            }).Distinct();


                    lvProductDetails.ItemsSource = productDetails;


                }

                #endregion
                #region [Bind to ListView]

                #endregion

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                BaseAppSettings.m_Log.Debug(Header + "Leaving..");
            }

        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            string Header = "SearchSKU::btnOk_Click: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");
            try
            {
                isClose = false;
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

        private void cmbSearchCriteria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string Header = "SearchSKU::cmbSearchCriteria_SelectionChanged: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");
            try
            {
                lblError.Visibility = Visibility.Hidden;
                foreach (ComboBoxItem item in e.AddedItems)
                {
                    if (string.Equals(item.Content, "SKU Name"))
                    {
                        lvProductDetails.ItemsSource = null;
                        lvSKUDetails.ItemsSource = null;
                        stkPnlSKUName.Visibility = Visibility.Visible;
                        stkPnlCompany.Visibility = Visibility.Collapsed;
                        stkPnlProductName.Visibility = Visibility.Collapsed;
                        stkPnlProductPrefix.Visibility = Visibility.Collapsed;
                        stkPnlCompanyPrefix.Visibility = Visibility.Collapsed;
                        txtCompany.Text = "";
                        txtProductName.Text = "";
                    }
                    else if (string.Equals(item.Content, "Product Name"))
                    {
                        lvProductDetails.ItemsSource = null;
                        lvSKUDetails.ItemsSource = null;
                        stkPnlSKUName.Visibility = Visibility.Collapsed;
                        stkPnlCompany.Visibility = Visibility.Collapsed;
                        stkPnlProductName.Visibility = Visibility.Visible;
                        stkPnlProductPrefix.Visibility = Visibility.Collapsed;
                        stkPnlCompanyPrefix.Visibility = Visibility.Collapsed;
                        txtCompany.Text = "";
                        txtSKUName.Text = "";

                    }
                    else if (string.Equals(item.Content, "Company Name"))
                    {
                        lvProductDetails.ItemsSource = null;
                        lvSKUDetails.ItemsSource = null;
                        stkPnlSKUName.Visibility = Visibility.Collapsed;
                        stkPnlCompany.Visibility = Visibility.Visible;
                        stkPnlProductName.Visibility = Visibility.Collapsed;
                        stkPnlProductPrefix.Visibility = Visibility.Collapsed;
                        stkPnlCompanyPrefix.Visibility = Visibility.Collapsed;
                        txtSKUName.Text = "";
                        txtProductName.Text = "";
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

        #endregion[private_methods]


        #region [ListView Code]

        private void lvProductDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string Header = "SearchSKU::lvProductDetails_SelectionChanged: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");

            try
            {
                ListProduct prodList = (ListProduct)lvProductDetails.SelectedItems[0];

                string productName = prodList.ProductName.ToUpper();


                var itemDetails = from listItem in lstSKU
                                  join listProduct in lstProduct
                                  on listItem.ProductID equals listProduct.ProductID
                                  join listComp in lstCompany on listProduct.CompanyID equals listComp.CompanyID
                                  where listProduct.ProductName.ToUpper().Contains(productName)
                                  select new ItemDetailList
                                  {
                                      SKU_ID = Convert.ToInt32(listItem.SKU_ID),
                                      SKUDescription = listItem.SKUDescription,
                                      ProductSKU = listItem.ProductSKU,
                                      ProductID = Convert.ToInt32(listItem.ProductID),
                                      DataOwnerID = Convert.ToInt32(listItem.DataOwnerID),
                                      ProductName = listProduct.ProductName,
                                      ProductPrefix = listProduct.ProductPrefix,
                                      CompanyID = listComp.CompanyID,
                                      CompanyName = listComp.CompanyName,
                                      CompanyPrefix = listComp.CompanyPrefix,
                                      CreatedDate = listComp.CreatedDate,
                                      UpdatedDate = listComp.UpdatedDate,
                                      CreatedBy = listComp.CreatedBy

                                  };

                lvSKUDetails.ItemsSource = itemDetails;


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


        private void lvSKUDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string Header = "SearchSKU::lvSKUDetails_SelectionChanged: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");

            try
            {

                if (lvSKUDetails.SelectedItems.Count > 0)
                {
                    prodList = (ItemDetailList)lvSKUDetails.SelectedItems[0];

                    objSelectedItem = new KTItemDetails(prodList.DataOwnerID, DateTime.Now, AppConfigSettings.LocLocationID, "Printed");

                    objSelectedItem.SKU_ID = prodList.SKU_ID;
                    objSelectedItem.LastSeenLocation = AppConfigSettings.LocLocationID;
                    objSelectedItem.UpdatedDate = prodList.UpdatedDate;
                    objSelectedItem.CreatedDate = prodList.CreatedDate;
                    objSelectedItem.CreatedBy = prodList.CreatedBy;
                    objSelectedItem.DataOwnerID = prodList.DataOwnerID;
                    objSelectedItem.LastSeenTime = DateTime.Now;
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

        #endregion




        #region [ GetPageData ]
        private bool GetPageData()
        {
            BaseAppSettings.m_Log.Debug(" Entering ...");
            lblError.Foreground = Brushes.Red;
            bool isvalid = false;
            try
            {
                if (cmbSearchCriteria.Text.Trim() == "SKU Name")
                {
                    if (txtSKUName.Text.Trim() == string.Empty)
                    {
                        lblError.Content = "Please enter SKU Name.";
                        lblError.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        isvalid = true;
                    }

                }
                else if (cmbSearchCriteria.Text.Trim() == "Product Name")
                {

                    if (txtProductName.Text.Trim() == string.Empty)
                    {
                        lblError.Content = "Please enter Product Name.";
                        lblError.Visibility = Visibility.Visible;
                    }
                    else
                    {

                        isvalid = true;
                    }

                }
                else if (cmbSearchCriteria.Text.Trim() == "Product Prefix")
                {
                    if (txtProductPrefix.Text.Trim() == string.Empty)
                    {
                        lblError.Content = "Please enter Product Prefix.";
                        lblError.Visibility = Visibility.Visible;
                    }
                    else if (!Regex.IsMatch(txtProductPrefix.Text.Trim(), "[0-9]"))
                    {
                        lblError.Content = "Enter proper product prifix code.";
                        lblError.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        isvalid = true;
                    }
                }
                else if (cmbSearchCriteria.Text.Trim() == "Company Name")
                {
                    if (txtCompany.Text.Trim() == string.Empty)
                    {
                        lblError.Content = "Please enter Company Name.";
                        lblError.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        isvalid = true;
                    }

                }
                else if (cmbSearchCriteria.Text.Trim() == "Company Prefix")
                {
                    if (txtCompanyPrefix.Text.Trim() == string.Empty)
                    {
                        lblError.Content = "Please enter Product Prefix.";
                        lblError.Visibility = Visibility.Visible;
                    }
                    else if (!Regex.IsMatch(txtCompanyPrefix.Text.Trim(), "[0-9]"))
                    {
                        lblError.Content = "Enter proper company prifix code.";
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
                BaseAppSettings.m_Log.ErrorException(ex.Message, ex);
                BaseAppSettings.m_Log.Error(Environment.NewLine + "Error:: " + ex.StackTrace);
            }
            finally
            {
                BaseAppSettings.m_Log.Debug(" Leaving ...");
            }
            return isvalid;
        }
        #endregion [ GetPageData ]

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            string Header = "SearchSKU::Window_Closing: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");

            try
            {
                //if (isClose)
                //{
                //    MessageBoxResult msgbxResult = CustomMessageBox.Show(this, "Do you want cancel SKU search.", "Cancel", MessageBoxButton.YesNo);

                //    if (msgbxResult == MessageBoxResult.Yes)
                //    {
                //        this.DialogResult = false;
                //        this.Close();
                //    }
                //    else
                //    {
                //        return;

                //    }

                //    this.Close();

                //}
             
                    return;
                    

              

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

    }



    public class ListProduct
    {
        public int ProductID
        {
            get;
            set;
        }

        public string ProductName
        {
            get;
            set;
        }

        public string ProductPrefix
        {
            get;
            set;
        }

        public string CompanyName
        {
            get;
            set;
        }

    }

    public class ItemDetailList
    {

        public int SKU_ID
        {
            get;
            set;
        }

        public string SKUDescription
        {
            get;
            set;
        }

        public string ProductSKU
        {
            get;
            set;
        }

        public int DataOwnerID
        {
            get;
            set;
        }

        public int ProductID
        {
            get;
            set;
        }

        public string ProductName
        {
            get;
            set;
        }

        public string ProductPrefix
        {
            get;
            set;
        }

        public int CompanyID
        {
            get;
            set;
        }

        public string CompanyName
        {
            get;
            set;
        }

        public string CompanyPrefix
        {
            get;
            set;
        }

        public DateTime CreatedDate
        {
            get;
            set;
        }
        public DateTime UpdatedDate
        {
            get;
            set;
        }

        public int CreatedBy
        {
            get;
            set;
        }


    }

}
