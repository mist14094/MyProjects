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
using System.Windows.Navigation;
using System.Windows.Shapes;
using KTWPFAppBase;
using KTWPFAppBase.Controls;
using KTone.Core.KTIRFID;
using System.Collections.ObjectModel;
using System.IO;
using System.Data;
using Microsoft.Windows.Controls;
using System.Windows.Threading;
using System.Threading;
using System.Configuration;

namespace KTone.Win.KTSDCPrint
{
    /// <summary>
    /// Interaction logic for ctrlPrintItem.xaml
    /// </summary>
    public partial class ctrlPrintItem : UserControl
    {


        public string customerUniqueId = string.Empty;
               
        private int dataOwnerID = App.DataOwnerId;
        private int skuID = 0;
        private string _skuName = string.Empty;
        private string _productName = string.Empty;
        private int _companyId = 0;
        private string _companyName = string.Empty;
        private string _description = string.Empty;
        private string title = string.Empty;
        private bool _isExist = false;
        private bool _isFilledColumn = false;
        private bool _isPrinted = false;
        private int _locationId = 0;
        private bool _isNew = true;
        private bool _isNewPrint = false;

        private ItemPrintType _itemPrintType;

        private Dictionary<string, bool> _lstAttributes = new Dictionary<string, bool>();
        public string _cntNameChanged = string.Empty;

        private Dictionary<string, bool> _columnMandatoy = new Dictionary<string, bool>();
        private Dictionary<string, string> _columnDataType = new Dictionary<string, string>();
        private List<string> regControl = new List<string>();

        // SKUDetails SKUDetails = new SKUDetails();

        private KTItemDetails _objSelectedItem = null;

        public KTItemDetails SelectedItem
        {
            get
            {
                return _objSelectedItem;
            }
        }

        KTItemDetails ItemDetails = null;


        private KTItemDetails _itemDetails = null;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        #region [Private_Member]
        private List<KTCompanyDetails> _lstCompany = null;
        private List<KTProductDetails> _lstProduct = null;
        private List<KTSKUDetails> _lstSKU = null;
        private List<string> _tabExist = new List<string>();
        private List<CustomFeildInfo> _lstCustomFieldInfo = new List<CustomFeildInfo>();
        private List<CustomFeildInfo> _lstUniqueCustomFieldInfo = new List<CustomFeildInfo>();
        private Dictionary<string, List<CustomFeildInfo>> _lstGroupFields = new Dictionary<string, List<CustomFeildInfo>>();
        private Dictionary<string, string> _lstCustomColumn_sku = new Dictionary<string, string>();
        private Dictionary<string, string> _lstCustomColumn_product = new Dictionary<string, string>();
        private Dictionary<string, string> _lstCustomColumn_company = new Dictionary<string, string>();
        private Dictionary<string, string> lstCustomColumn_item = new Dictionary<string, string>();
        private Dictionary<string, string> lstTabFieldValues = new Dictionary<string, string>();
        #endregion

        public ctrlPrintItem()
        {
            InitializeComponent();
        }

        #region [Private_Method]

        private void btnGenerateSN_Click(object sender, RoutedEventArgs e)
        {
            Window parentWin = Window.GetWindow(this);
            string Header = "ctrlPrintItem::btnGenerateSN_Click: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");

            customerUniqueId = txtSerialNo.Text.Trim();

            try
            {
                

            }
            catch (Exception ex)
            {

                CustomMessageBox.Show(parentWin, "Error:" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                BaseAppSettings.m_Log.ErrorException(String.Format("{0}Error occured.{1}", Header, ex.Message), ex);
                BaseAppSettings.m_Log.Error(Header + ":: " + Environment.NewLine + "Error:: " + ex.StackTrace);
            }
            finally
            {
                BaseAppSettings.m_Log.Debug(Header + "Leaving..");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Window parentWin = Window.GetWindow(this);

            string Header = "ctrlPrintItem::btnClose_Click: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");
            try
            {
                dckItemPanel.Children.Clear();
            }
            catch (Exception ex)
            {

                CustomMessageBox.Show(parentWin, "Error:" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                BaseAppSettings.m_Log.ErrorException(String.Format("{0}Error occured.{1}", Header, ex.Message), ex);
                BaseAppSettings.m_Log.Error(Header + ":: " + Environment.NewLine + "Error:: " + ex.StackTrace);
            }
            finally
            {
                BaseAppSettings.m_Log.Debug(Header + "Leaving..");
            }

        }


        public void clearAll()
        {
            tabOtherInfo.Items.Clear();
            txtRFTag.Text = string.Empty;
            txtRFTag.Visibility = Visibility.Hidden;
            lblRFID.Visibility = Visibility.Hidden;
            txtCompany.Text = "";
            txtSKUName.Text = "";
            txtProduct.Text = "";
            txtDescription.Text = "";

            lstTabFieldValues.Clear();

            _tabExist.Clear();

            tabOtherInfo.Items.Clear();

            lstCustomColumn_item.Clear();

            lstCustomColumn_item.Clear();
          
            _isExist = false;

            foreach (CustomFeildInfo c in _lstCustomFieldInfo)
            {

                var columnExist = from grItems in _lstUniqueCustomFieldInfo
                                  where (grItems.CustColName == c.CustColName)
                                  select grItems;

                if (columnExist.Count() == 0)
                {
                    _lstUniqueCustomFieldInfo.Add(c);
                }



            }

            createTab(lstCustomColumn_item);
        }

        public void fillItemData()
        {
            Window parentWin = Window.GetWindow(this);
            string Header = "ctrlPrintItem::fillItemData(): ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");

            _isNewPrint = true;

            try
            {
                if (regControl.Count > 0)
                {
                    foreach (string str in regControl)
                    {
                        this.UnregisterName(str);
                    }
                }
                txtCopies.Text = "1";
                regControl.Clear();

                BaseAppSettings.m_Log.Debug(Header + " : Checking customerUniqueId for existance ");
                _isExist = checkExistance();

                BaseAppSettings.m_Log.Debug(Header + " : customerUniqueId - Exist ");

                txtRFTag.Visibility = Visibility.Visible;
                lblRFID.Visibility = Visibility.Visible;
                btnChangeSku.Visibility = Visibility.Hidden;

                txtRFTag.Text = string.Empty;


                tabOtherInfo.Items.Clear();
                _tabExist.Clear();
                fillItemDetails();
                btnPrint.IsEnabled = true;

                BaseAppSettings.m_Log.Debug(Header + " : customerUniqueId - Exist Leaving ");

            }
            catch (Exception ex)
            {
            }
            finally
            {
            }
        }

        private void txtSerialNo_LostFocus(object sender, RoutedEventArgs e)
        {
            Window parentWin = Window.GetWindow(this);
            string Header = "ctrlPrintItem::txtSerialNo_LostFocus: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");

            try
            {

                if (sender != null)
                {
                    lblMsg.Content = "";
                    _isPrinted = false;
                    _isNewPrint = false;
                }
                if (regControl.Count > 0)
                {
                    foreach (string str in regControl)
                    {
                        this.UnregisterName(str);
                    }
                }

                txtCopies.Text = "1";

                regControl.Clear();

                bool _serialNumber = false;

                if (customerUniqueId != "")
                {
                    if (!customerUniqueId.Equals(txtSerialNo.Text.ToUpper().Trim()))
                    {
                        _isPrinted = false;
                    }
                }

                customerUniqueId = txtSerialNo.Text.Trim();

                // 


                if (customerUniqueId.Contains(" "))
                {
                    lblSerMsg.Visibility = Visibility.Visible;
                    lblSerMsg.Content = "Enter Valid Serial Number.";
                    btnChangeSku.Visibility = Visibility.Hidden;
                    btnPrint.IsEnabled = false;
                    clearAll();
                    return;
                }

                for (int i = 0; i < customerUniqueId.Length; i++)
                {
                    char c = customerUniqueId[i];

                    if (Char.IsLetterOrDigit(c))
                    {
                        _serialNumber = true;
                        break;
                    }
                    else
                    {
                        _serialNumber = false;
                    }
                }

                if (_serialNumber)
                {
                    if (!string.IsNullOrEmpty(customerUniqueId))
                    {
                        BaseAppSettings.m_Log.Debug(Header + " : Checking customerUniqueId for existance ");
                        _isExist = checkExistance();

                        if (!_isExist)
                        {
                            BaseAppSettings.m_Log.Debug(Header + " : customerUniqueId - Not Exist ");

                            // if (_isFilledColumn)
                            // {
                        
                            lstTabFieldValues.Clear();

                            _tabExist.Clear();

                            tabOtherInfo.Items.Clear();



                            //  if (txtRFTag.Visibility == Visibility.Visible)
                            //  {
                            lstCustomColumn_item.Clear();
                           
                            createTab(lstCustomColumn_item);
                            //  }

                            //    else
                            //    {
                            //        createTab(lstCustomColumn_item);
                            //    }
                            //}


                            lstTabFieldValues.Clear();
                            _itemPrintType = ItemPrintType.NEW_ITEM;
                            _isNew = true;

                            txtRFTag.Text = string.Empty;
                            txtRFTag.Visibility = Visibility.Hidden;
                            lblRFID.Visibility = Visibility.Hidden;

                            txtCompany.Text = "";
                            txtSKUName.Text = "";
                            txtProduct.Text = "";
                            txtDescription.Text = "";


                            lblMsg.Content = "Serial Number not exists.";
                            lblMsg.Foreground = Brushes.Blue;
                            lblMsg.FontWeight = FontWeights.Bold;
                            btnChangeSku.Visibility = Visibility.Visible;

                            if (txtCompany.Text != "")
                            {
                                btnPrint.IsEnabled = true;
                            }

                            BaseAppSettings.m_Log.Debug(Header + " : customerUniqueId - Not Exist leaving ");

                        }

                        else
                        {
                            BaseAppSettings.m_Log.Debug(Header + " : customerUniqueId - Exist ");

                            txtRFTag.Visibility = Visibility.Visible;
                            lblRFID.Visibility = Visibility.Visible;
                            btnChangeSku.Visibility = Visibility.Hidden;

                            txtRFTag.Text = string.Empty;


                            tabOtherInfo.Items.Clear();
                            _tabExist.Clear();
                            fillItemDetails();
                            btnPrint.IsEnabled = true;

                            BaseAppSettings.m_Log.Debug(Header + " : customerUniqueId - Exist Leaving ");
                        }

                        lblSerMsg.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                       

                    }
                }
                else
                {
                    _isExist = false;
                    tabOtherInfo.Items.Clear();
                    txtRFTag.Text = string.Empty;
                    txtRFTag.Visibility = Visibility.Hidden;
                    lblRFID.Visibility = Visibility.Hidden;
                    txtCompany.Text = "";
                    txtSKUName.Text = "";
                    txtProduct.Text = "";
                    txtDescription.Text = "";

                    _tabExist.Clear();

                    

                    //CustomMessageBox.Show(parentWin, "Error: Enter valid serial number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }


            }
            catch (Exception ex)
            {
                CustomMessageBox.Show(parentWin, "Error:" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                BaseAppSettings.m_Log.ErrorException(String.Format("{0}Error occured.{1}", Header, ex.Message), ex);
                BaseAppSettings.m_Log.Error(Header + ":: " + Environment.NewLine + "Error:: " + ex.StackTrace);
            }
            finally
            {
                BaseAppSettings.m_Log.Debug(Header + "Leaving..");
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
            Window parentWin = Window.GetWindow(this);
            string Header = "ctrlPrintItem::UserControl_Loaded: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");
            try
            {
                

            }
            catch (Exception ex)
            {
                CustomMessageBox.Show(parentWin, "Error:" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                BaseAppSettings.m_Log.ErrorException(String.Format("{0}Error occured.{1}", Header, ex.Message), ex);
                BaseAppSettings.m_Log.Error(Header + ":: " + Environment.NewLine + "Error:: " + ex.StackTrace);
            }
            finally
            {
                Mouse.OverrideCursor = Cursors.Arrow;
                parentWin.Cursor = Cursors.Arrow;
                BaseAppSettings.m_Log.Debug(Header + "Leaving..");
            }

        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            Window parentWin = Window.GetWindow(this);
            string Header = "ctrlPrintItem::btnPrint_Click: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");
            string err = string.Empty;

            bool result = false;

            try
            {
                int timeInterval = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("PrintInterval"));

                Cursor = Cursors.Wait;

                if (string.IsNullOrEmpty(txtSerialNo.Text.Trim()))
                {
                    CustomMessageBox.Show(parentWin, "Serial Number field should not be left blank.", "Validation", MessageBoxButton.OK, MessageBoxImage.Information);
                    txtCopies.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtCopies.Text.Trim()))
                {
                    CustomMessageBox.Show(parentWin, "No. of copies should not be left blank.", "Validation", MessageBoxButton.OK, MessageBoxImage.Information);
                    txtCopies.Focus();
                    return;
                }
                else if (Convert.ToInt32(txtCopies.Text.Trim()) == 0)
                {
                    CustomMessageBox.Show(parentWin, "No. of copies should be greater than 0.", "Validation", MessageBoxButton.OK, MessageBoxImage.Information);
                    txtCopies.Text = "1";
                    txtCopies.Focus();
                    return;
                }

                int locationId = AppConfigSettings.ItemLocationID;

                if (!getTabData())
                {

                    lblMsg.Content = "Mandatory fileds should not be left blank";
                    lblMsg.Foreground = Brushes.Red;
                    return;
                }

                int noOfCopies = Convert.ToInt32(txtCopies.Text.Trim());


                if (!result)
                {
                    BaseAppSettings.m_Log.Error(Header + ": Error : " + err);
                    BaseAppSettings.m_Log.Debug(Header + " :: Item failed to print \n CustomerUniqueId : " + this.customerUniqueId + Environment.NewLine + " ItemDetails : " + ItemDetails.ToString());

                    lblMsg.Content = "Item Printing Failed."; //err.ToString().Trim();
                    lblMsg.Foreground = Brushes.Red;
                    //CustomMessageBox.Show("Item Successfully Printed", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                }
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Arrow;

                BaseAppSettings.m_Log.Error(Header + " :: Failed to print Item for \n CustomerUniqueId : " + this.customerUniqueId + Environment.NewLine + " SKU Name : " + txtSKUName.Text + Environment.NewLine + " Product Name : " + txtProduct.Text + Environment.NewLine + " Company Name : " + txtCompany.Text);
                if (ItemDetails != null)
                {
                    BaseAppSettings.m_Log.Debug(Header + Environment.NewLine + " ItemDetails :" + ItemDetails.ToString());
                }
                // BaseAppSettings.m_Log.Error(String.Format("Failed to print Item for :: CustomerUniqueId : {0} ", ItemDetails.CustomerUniqueID));
                BaseAppSettings.m_Log.ErrorException(String.Format("{0}Error occured.{1}", Header, ex.Message), ex);
                BaseAppSettings.m_Log.Error(Header + ":: " + Environment.NewLine + "Error:: " + ex.StackTrace);

                CustomMessageBox.Show(parentWin, "Error:" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                lblMsg.Content = "Item Printing Failed";
                lblMsg.Foreground = Brushes.Red;
            }
            finally
            {
                Cursor = Cursors.Arrow;
                BaseAppSettings.m_Log.Debug(Header + "Leaving..");
            }
        }

        void txtColumnValue_KeyDown(object sender, KeyEventArgs e)
        {

            if (!((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ||
                ((e.Key >= Key.D0 && e.Key <= Key.D9) && (!(e.Key == Key.LeftShift) || !(e.Key == Key.RightShift))) ||
                e.Key == Key.Back || e.Key == Key.Delete || e.Key ==
                Key.LeftAlt || e.Key == Key.Left || e.Key == Key.Right ||
                e.Key == Key.LeftShift || e.Key == Key.RightShift || e.Key == Key.Home || e.Key ==
                Key.End || e.Key == Key.Tab))
                e.Handled = true;// .SuppressKeyPress = true;

        }

        void txtColumnValue1_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox t = (TextBox)sender;
            string text = t.Text;
            if (e.Key == Key.Decimal || e.Key == Key.OemPeriod)
            {

                if (!text.Contains('.'))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }

            }
            else if (!((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ||
            ((e.Key >= Key.D0 && e.Key <= Key.D9) && (!(e.Key == Key.LeftShift) || !(e.Key == Key.RightShift))) ||
            e.Key == Key.Back || e.Key == Key.Delete || e.Key ==
            Key.LeftAlt || e.Key == Key.Left || e.Key == Key.Right ||
            e.Key == Key.LeftShift || e.Key == Key.RightShift || e.Key == Key.Home || e.Key ==
            Key.End || e.Key == Key.Tab))
                e.Handled = true;
        }

        private void btnChangeSku_Click(object sender, RoutedEventArgs e)
        {
            Window parentWin = Window.GetWindow(this);

            string Header = "ctrlPrintItem::btnGo_Click: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");

            try
            {
                
            }
            catch (Exception ex)
            {

                CustomMessageBox.Show(parentWin, "Error:" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                BaseAppSettings.m_Log.ErrorException(String.Format("{0}Error occured.{1}", Header, ex.Message), ex);
                BaseAppSettings.m_Log.Error(Header + ":: " + Environment.NewLine + "Error:: " + ex.StackTrace);
            }
            finally
            {
                BaseAppSettings.m_Log.Debug(Header + "Leaving..");
            }

        }

        #endregion

        #region [Public Method]

        public void setFormValues()
        {
            

        }

        public void fillItemDetails()
        {
            Window parentWin = Window.GetWindow(this);

            string Header = "ctrlPrintItem::fillItemDetails: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");

            try
            {
                
            }
            catch (Exception ex)
            {

                CustomMessageBox.Show(parentWin, "Error:" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                BaseAppSettings.m_Log.ErrorException(String.Format("{0}Error occured.{1}", Header, ex.Message), ex);
                BaseAppSettings.m_Log.Error(Header + ":: " + Environment.NewLine + "Error:: " + ex.StackTrace);
            }
            finally
            {
                BaseAppSettings.m_Log.Debug(Header + "Leaving..");
            }
        }

        public bool checkExistance()
        {
            string Header = "ctrlPrintItem::checkExistance: ";
            try
            {
                
            }
            catch (Exception ex)
            {
                BaseAppSettings.m_Log.ErrorException(String.Format("Error occured : {0} , CustomerUniqueID : {1}" + Environment.NewLine + "Error : {2}", Header, customerUniqueId, ex.Message), ex);
                BaseAppSettings.m_Log.Error(Header + ":: " + Environment.NewLine + "Error:: " + ex.StackTrace);
                throw ex;
            }
            finally
            {

            }

            return true;
        }

        public bool getTabData()
        {

            Window parentWin = Window.GetWindow(this);

            string colField = string.Empty;
            string colValue = string.Empty;
            string datatype = string.Empty;
            bool isMandatory = true;

            ItemDetails = new KTItemDetails(dataOwnerID, DateTime.Now, _locationId, "Printed");//  (2, 2, DateTime.Now, 0);


            if (SelectedItem != null)
            {
                ItemDetails = SelectedItem;
            }

            //ItemDetails value setting

            ItemDetails.ItemStatus = "Printed";
            ItemDetails.CustomerUniqueID = this.customerUniqueId;
            ItemDetails.LastSeenTime = DateTime.Now;
            ItemDetails.Status = "Printed";
            ItemDetails.Comments = "";
            ItemDetails.CustomColumnDetails.Clear();



            #region [customColumnDetails]
            foreach (string TabName in _tabExist)
            {
                for (int i = 0; i < tabOtherInfo.Items.Count; i++)
                {
                    if (((HeaderedContentControl)(tabOtherInfo.Items[i])).Name.Equals("tab" + TabName))
                    {
                        object obj = ((HeaderedContentControl)(tabOtherInfo.Items[i])).Content;
                        if (((System.Windows.FrameworkElement)(obj)).Name.Equals("scr" + TabName))
                        {
                            ScrollViewer scrollViewer = (ScrollViewer)((System.Windows.FrameworkElement)(obj));

                            #region [New Code]

                            if (((System.Windows.Controls.Panel)(((System.Windows.Controls.ContentControl)(scrollViewer)).Content)).Name.Equals("grd" + TabName))
                            {
                                Grid grd = (Grid)((System.Windows.Controls.Panel)(((System.Windows.Controls.ContentControl)(scrollViewer)).Content));

                                int k = 0;

                                for (int j = 0; j < grd.RowDefinitions.Count; j++)
                                {
                                    int col = (j + 1) * 4;
                                    for (k = k; k < col; k++)
                                    {
                                        if (grd.Children.Count > k)
                                        {
                                            if (k % 4 == 0 || k % 4 == 2)
                                            {
                                                TextBlock txtBlock = (TextBlock)((System.Windows.FrameworkElement)(grd.Children[k]));
                                                colField = txtBlock.Name.Trim();
                                                colField = colField.Substring(colField.IndexOf('_') + 1);

                                                if (_columnDataType.ContainsKey(colField))
                                                {
                                                    datatype = _columnDataType[colField];
                                                }

                                                if (_columnMandatoy.ContainsKey(colField))
                                                {
                                                    isMandatory = _columnMandatoy[colField];
                                                }
                                            }

                                            else if (k % 4 == 1 || k % 4 == 3)
                                            {
                                                string typeOfControl = ((FrameworkElement)(grd.Children[k])).GetType().FullName;

                                                #region [Duplicate-Existing]

                                                if (typeOfControl.Equals("System.Windows.Controls.TextBlock"))
                                                {
                                                    TextBlock txtBox = (TextBlock)((System.Windows.FrameworkElement)(grd.Children[k]));
                                                    // colValue = txtBox.Text.Trim();

                                                    if (datatype == "datetime")
                                                    {
                                                        if (string.IsNullOrEmpty(txtBox.Text.Trim()))
                                                            colValue = "";
                                                        else
                                                            colValue = Convert.ToString(Convert.ToDateTime(txtBox.Text.Trim()));
                                                    }

                                                    else if (datatype == "decimal")
                                                    {
                                                        if (isMandatory)
                                                        {
                                                            if (string.IsNullOrEmpty(txtBox.Text.Trim()))
                                                            {
                                                                CustomMessageBox.Show("Please enter value in mandatory field '" + txtBox.Name + "'");
                                                                // return;
                                                            }
                                                            else
                                                                colValue = Convert.ToString((txtBox.Text.Trim()));

                                                            if (colValue.EndsWith(".00") || colValue.EndsWith(".0"))
                                                            {
                                                                colValue = colValue.Remove(colValue.IndexOf('.'));
                                                            }
                                                            else
                                                            {
                                                                colValue = Convert.ToString(Convert.ToDecimal(txtBox.Text.Trim()));
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (string.IsNullOrEmpty(txtBox.Text.Trim()))
                                                                colValue = "";
                                                            else
                                                                colValue = Convert.ToString((txtBox.Text.Trim()));
                                                            if (!string.IsNullOrEmpty(txtBox.Text.Trim()))
                                                            {
                                                                if (colValue.EndsWith(".00") || colValue.EndsWith(".0"))
                                                                {
                                                                    colValue = colValue.Remove(colValue.IndexOf('.'));
                                                                }
                                                                else
                                                                {
                                                                    colValue = Convert.ToString(Convert.ToDecimal(txtBox.Text.Trim()));
                                                                }
                                                            }
                                                        }


                                                    }

                                                    else if (datatype == "int")
                                                    {
                                                        if (isMandatory)
                                                        {
                                                            if (string.IsNullOrEmpty(txtBox.Text.Trim()))
                                                            {
                                                                CustomMessageBox.Show(parentWin, "Please enter value in mandatory field '" + txtBox.Name + "'");
                                                                //return;
                                                            }
                                                            else
                                                                colValue = Convert.ToString(Convert.ToInt32(txtBox.Text.Trim()));
                                                        }
                                                        else
                                                        {
                                                            if (!string.IsNullOrEmpty(txtBox.Text.Trim()))
                                                            {
                                                                colValue = Convert.ToString(Convert.ToInt32(txtBox.Text.Trim()));
                                                            }
                                                            else
                                                            {
                                                                colValue = Convert.ToString(txtBox.Text.Trim());
                                                            }
                                                        }

                                                    }
                                                    else if (datatype == "bigint")
                                                    {
                                                        if (isMandatory)
                                                        {
                                                            if (string.IsNullOrEmpty(txtBox.Text.Trim()))
                                                            {
                                                                CustomMessageBox.Show(parentWin, "Please enter value in mandatory field '" + txtBox.Name + "'");
                                                                //return;
                                                            }
                                                            else
                                                                colValue = Convert.ToString(Convert.ToInt64(txtBox.Text.Trim()));
                                                        }
                                                        else
                                                        {
                                                            if (!string.IsNullOrEmpty(txtBox.Text.Trim()))
                                                            {
                                                                colValue = Convert.ToString(Convert.ToInt64(txtBox.Text.Trim()));
                                                            }
                                                            else
                                                            {
                                                                colValue = Convert.ToString(txtBox.Text.Trim());
                                                            }
                                                        }
                                                    }

                                                    else if (datatype == "varchar")
                                                    {
                                                        if (isMandatory)
                                                        {
                                                            if (string.IsNullOrEmpty(txtBox.Text.Trim()))
                                                            {
                                                                CustomMessageBox.Show(parentWin, "Please enter value in mandatory field '" + txtBox.Name + "'");
                                                                //return;
                                                            }
                                                            else
                                                                colValue = Convert.ToString(txtBox.Text.Trim());
                                                        }
                                                        else
                                                        {
                                                            colValue = Convert.ToString(txtBox.Text.Trim());
                                                        }
                                                    }
                                                }

                                                #endregion

                                                #region [NewPrint]
                                                else
                                                {
                                                    if (typeOfControl.Equals("System.Windows.Controls.ComboBox"))
                                                    {
                                                        ComboBox cmbBox = (ComboBox)((FrameworkElement)(grd.Children[k]));
                                                        colValue = Convert.ToString(cmbBox.SelectedValue);
                                                    }
                                                    else
                                                    {
                                                        if (datatype == "datetime")
                                                        {
                                                            DatePicker dt = (DatePicker)((FrameworkElement)(grd.Children[k]));
                                                            colValue = Convert.ToString(Convert.ToDateTime(dt.Text.Trim()));
                                                        }
                                                        else
                                                        {

                                                            TextBox txtBox = (TextBox)((FrameworkElement)(grd.Children[k]));

                                                            colValue = Convert.ToString(txtBox.Text.Trim());
                                                            if (datatype == "decimal")
                                                            {
                                                                if (isMandatory)
                                                                {
                                                                    if (string.IsNullOrEmpty(txtBox.Text.Trim()))
                                                                    {
                                                                        CustomMessageBox.Show(parentWin, "Please enter value in mandatory field '" + txtBox.Name + "'");
                                                                        txtBox.Focus();
                                                                        return false;
                                                                    }
                                                                    else
                                                                        colValue = Convert.ToString((txtBox.Text.Trim()));

                                                                    if (colValue.EndsWith(".00") || colValue.EndsWith(".0"))
                                                                    {
                                                                        colValue = colValue.Remove(colValue.IndexOf('.'));
                                                                    }
                                                                    else
                                                                    {
                                                                        colValue = Convert.ToString(Convert.ToDecimal(txtBox.Text.Trim()));
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (string.IsNullOrEmpty(txtBox.Text.Trim()))
                                                                        colValue = "";
                                                                    else
                                                                        colValue = Convert.ToString((txtBox.Text.Trim()));
                                                                    if (!string.IsNullOrEmpty(txtBox.Text.Trim()))
                                                                    {
                                                                        if (colValue.EndsWith(".00") || colValue.EndsWith(".0"))
                                                                        {
                                                                            colValue = colValue.Remove(colValue.IndexOf('.'));
                                                                        }
                                                                        else
                                                                        {
                                                                            colValue = Convert.ToString(Convert.ToDecimal(txtBox.Text.Trim()));
                                                                        }
                                                                    }
                                                                }


                                                            }

                                                            else if (datatype == "int")
                                                            {
                                                                if (isMandatory)
                                                                {
                                                                    if (string.IsNullOrEmpty(txtBox.Text.Trim()))
                                                                    {
                                                                        CustomMessageBox.Show(parentWin, "Please enter value in mandatory field '" + txtBox.Name + "'");
                                                                        txtBox.Focus();
                                                                        return false;
                                                                    }
                                                                    else
                                                                        colValue = Convert.ToString(Convert.ToInt32(txtBox.Text.Trim()));
                                                                }
                                                                else
                                                                {
                                                                    if (!string.IsNullOrEmpty(txtBox.Text.Trim()))
                                                                    {
                                                                        colValue = Convert.ToString(Convert.ToInt32(txtBox.Text.Trim()));
                                                                    }
                                                                    else
                                                                    {
                                                                        colValue = Convert.ToString(txtBox.Text.Trim());
                                                                    }
                                                                }

                                                            }
                                                            else if (datatype == "bigint")
                                                            {
                                                                if (isMandatory)
                                                                {
                                                                    if (string.IsNullOrEmpty(txtBox.Text.Trim()))
                                                                    {
                                                                        CustomMessageBox.Show(parentWin, "Please enter value in mandatory field '" + txtBox.Name + "'");

                                                                        txtBox.Focus();
                                                                        return false;
                                                                    }
                                                                    else
                                                                        colValue = Convert.ToString(Convert.ToInt64(txtBox.Text.Trim()));
                                                                }
                                                                else
                                                                {
                                                                    if (!string.IsNullOrEmpty(txtBox.Text.Trim()))
                                                                    {
                                                                        colValue = Convert.ToString(Convert.ToInt64(txtBox.Text.Trim()));
                                                                    }
                                                                    else
                                                                    {
                                                                        colValue = Convert.ToString(txtBox.Text.Trim());
                                                                    }
                                                                }
                                                            }

                                                            else if (datatype == "varchar")
                                                            {
                                                                if (isMandatory)
                                                                {
                                                                    if (string.IsNullOrEmpty(txtBox.Text.Trim()))
                                                                    {
                                                                        CustomMessageBox.Show(parentWin, "Please enter value in mandatory field '" + txtBox.Name + "'");
                                                                        txtBox.Focus();
                                                                        return false;
                                                                    }
                                                                    else
                                                                        colValue = Convert.ToString(txtBox.Text.Trim());
                                                                }
                                                                else
                                                                {
                                                                    colValue = Convert.ToString(txtBox.Text.Trim());
                                                                }
                                                            }
                                                        }

                                                    }
                                                }
                                                #endregion

                                                if (!ItemDetails.CustomColumnDetails.ContainsKey(colField))
                                                {
                                                    ItemDetails.CustomColumnDetails.Add(colField, colValue);
                                                }
                                            }
                                        }

                                    }
                                }
                            }

                            #endregion

                        }
                    }
                }
            }
            #endregion

            if (stkRFID.Visibility == Visibility.Visible)
            {
                foreach (SDCTagData tagData in ItemDetails.TagDetails)
                {
                    tagData.TagID = txtRFTag.Text.Trim();
                }
            }
            return true;
        }

        public void createTab(Dictionary<string, string> lstCustomColumn)
        {
            string Header = "ctrlPrintItem::createTab: ";

            string TabName = string.Empty;

            try
            {
                _tabExist.Clear();

                string columnValue = string.Empty;
                string columnName = string.Empty;

                _columnMandatoy.Clear();

                _columnDataType.Clear();

                string[] groupName = new string[_lstUniqueCustomFieldInfo.Count];
                int j = 0;

                Thickness marginThickness = new Thickness();


                foreach (var groupDetails in _lstGroupFields)
                {

                    string grpName = groupDetails.Key;// .Remove(' ');
                    char ch = ' ';
                    grpName = grpName.Replace(ch.ToString(), "");

                    List<CustomFeildInfo> lstColNames = new List<CustomFeildInfo>();
                    lstColNames = groupDetails.Value;

                    #region creatingColumn

                    for (int i = 0; i < lstColNames.Count; i++)
                    {
                        if (!groupName.Contains(Convert.ToString(groupDetails.Key)))
                        {
                            groupName[j] = groupDetails.Key;                            //groupName[j] = _lstUniqueCustomFieldInfo[i].GroupName;

                            string TabHeader = groupName[j].ToString();

                            TabName = TabHeader.Replace(' ', '_');
                            _tabExist.Add(TabName);

                            TabItem tab = new TabItem
                            {
                                Name = "tab" + TabName,
                                Header = TabHeader,
                                Height = Double.NaN,
                                Width = Double.NaN,
                                HorizontalAlignment = HorizontalAlignment.Stretch,
                                VerticalAlignment = VerticalAlignment.Stretch,
                                IsSelected = false
                            };

                            Grid grdTab = new Grid();
                            grdTab.Name = "grd" + TabName;
                            grdTab.HorizontalAlignment = HorizontalAlignment.Stretch;
                            grdTab.VerticalAlignment = VerticalAlignment.Stretch;

                            ColumnDefinition col1 = new ColumnDefinition();
                            col1.Width = new GridLength(210);
                            grdTab.ColumnDefinitions.Add(col1);

                            ColumnDefinition col2 = new ColumnDefinition();
                            col2.Width = new GridLength(250);
                            grdTab.ColumnDefinitions.Add(col2);

                            ColumnDefinition col3 = new ColumnDefinition();
                            col3.Width = new GridLength(210);
                            grdTab.ColumnDefinitions.Add(col3);

                            ColumnDefinition col4 = new ColumnDefinition();
                            col4.Width = new GridLength(250);
                            grdTab.ColumnDefinitions.Add(col4);

                            ScrollViewer scrollViewer = new ScrollViewer();
                            scrollViewer.Name = "scr" + TabName;
                            scrollViewer.HorizontalAlignment = HorizontalAlignment.Stretch;
                            scrollViewer.VerticalAlignment = VerticalAlignment.Stretch;

                            StackPanel stkPanel = new StackPanel();
                            stkPanel.Orientation = Orientation.Vertical;
                            stkPanel.HorizontalAlignment = HorizontalAlignment.Stretch;
                            stkPanel.VerticalAlignment = VerticalAlignment.Stretch;
                            stkPanel.Name = "stk" + TabName;

                            tab.Content = scrollViewer;


                            scrollViewer.Content = grdTab;



                            int k = 1;
                            var groupItems = from grItems in _lstUniqueCustomFieldInfo
                                             where (grItems.GroupName == groupName[j])
                                             select grItems;

                            int count = lstColNames.Count;// groupItems.Count();

                            decimal d = Math.Ceiling(Convert.ToDecimal((count * 2) / 4.0));

                            int x = 0;
                            for (x = 0; x < d; x++)
                            {
                                RowDefinition row = new RowDefinition();
                                row.Height = new GridLength(35);
                                grdTab.RowDefinitions.Add(row);
                            }

                            k = 0;
                            x = 0;


                            foreach (var v in groupDetails.Value)
                            {

                                if (!_columnDataType.ContainsKey(v.CustColName))
                                {
                                    _columnDataType.Add(v.CustColName, v.DataType);
                                    _columnMandatoy.Add(v.CustColName, v.IsMandatory);

                                }
                                if (k == 4)
                                {
                                    k = 0;
                                    x++;
                                }

                                marginThickness.Left = 5;
                                marginThickness.Right = 5;
                                marginThickness.Top = 5;
                                marginThickness.Bottom = 5;


                                // lstAttributes.Add(v.DataType, v.IsMandatory);

                                //colAttributes.Add(v.CustColName, lstAttributes);



                                foreach (var fieldValue in lstCustomColumn)
                                {
                                    columnName = fieldValue.Key;

                                    if (v.CustColName.Equals(columnName))
                                    {
                                        columnValue = fieldValue.Value;
                                        break;
                                    }

                                }
                                char ch1 = '_';
                                string CustColName = v.CustColName;
                                CustColName = CustColName.Replace(ch1.ToString(), "");

                                TextBlock txtColumnName = new TextBlock() { Name = grpName + "_" + v.CustColName, Text = v.AliasName + " :", Margin = marginThickness, FontWeight = FontWeights.Bold, Width = 200 };

                                Grid.SetRow(txtColumnName, x);
                                Grid.SetColumn(txtColumnName, k);

                                grdTab.Children.Add(txtColumnName);


                                k = k + 1;

                                if (_isExist)
                                {

                                    TextBlock txtColumnValue = new TextBlock() { Name = grpName + "_" + "txt" + CustColName, Margin = marginThickness, Text = columnValue, Width = 200 };

                                    Grid.SetRow(txtColumnValue, x);
                                    Grid.SetColumn(txtColumnValue, k);
                                    grdTab.Children.Add(txtColumnValue);

                                }

                                else
                                {
                                    if (v.DataType == "datetime")
                                    {
                                        DatePicker dtPicker = new DatePicker()
                                        {
                                            Name = grpName + "_" + "dt" + CustColName,
                                            Margin = marginThickness,
                                            Width = 200,
                                            Height = 30
                                        };
                                        dtPicker.SelectedDate = DateTime.Today;
                                        dtPicker.VerticalAlignment = VerticalAlignment = VerticalAlignment.Center;
                                        txtColumnName.Height = 30;

                                        this.RegisterName(dtPicker.Name, dtPicker);
                                        regControl.Add(dtPicker.Name);

                                        dtPicker.SelectedDateChanged += new EventHandler<SelectionChangedEventArgs>(dtPicker_SelectedDateChanged);

                                        dtPicker.SelectedDate = DateTime.Today;

                                        Grid.SetRow(dtPicker, x);
                                        Grid.SetColumn(dtPicker, k);
                                        grdTab.Children.Add(dtPicker);

                                    }
                                    else
                                    {
                                        if (v.ListColValues.Count > 0)
                                        {
                                            ComboBox cmbColVal = new ComboBox()
                                            {
                                                Name = Name = grpName + "_" + "cmb" + CustColName,
                                                Margin = marginThickness,
                                                Width = 200
                                            };

                                            for (int l = 0; l < v.ListColValues.Count; l++)
                                            {
                                                cmbColVal.Items.Add(v.ListColValues[l]);
                                            }

                                            this.RegisterName(cmbColVal.Name, cmbColVal);
                                            regControl.Add(cmbColVal.Name);
                                            cmbColVal.SelectionChanged += new SelectionChangedEventHandler(cmbColVal_SelectionChanged);

                                            cmbColVal.SelectedIndex = 0;


                                            Grid.SetRow(cmbColVal, x);
                                            Grid.SetColumn(cmbColVal, k);
                                            grdTab.Children.Add(cmbColVal);

                                        }
                                        else
                                        {
                                            TextBox txtColumnValue = new TextBox() { Name = grpName + "_" + "txt" + CustColName, Margin = marginThickness, Width = 200 };

                                            txtColumnValue.Text = columnValue.Trim();

                                            if (v.DataType == "int" || v.DataType == "bigint")
                                            {

                                                txtColumnValue.KeyDown += new KeyEventHandler(txtColumnValue_KeyDown);

                                            }
                                            else if (v.DataType == "decimal")
                                            {
                                                txtColumnValue.KeyDown += new KeyEventHandler(txtColumnValue1_KeyDown);

                                            }

                                            this.RegisterName(txtColumnValue.Name, txtColumnValue);
                                            regControl.Add(txtColumnValue.Name);

                                            txtColumnValue.TextChanged += new TextChangedEventHandler(txtColumnValue_TextChanged);

                                            Grid.SetRow(txtColumnValue, x);
                                            Grid.SetColumn(txtColumnValue, k);
                                            grdTab.Children.Add(txtColumnValue);


                                        }
                                    }
                                }
                                k = k + 1;
                            }

                            //}

                            if (grdTab.Children.Count > 0)
                            {
                                tabOtherInfo.Items.Add(tab);
                            }

                            j++;
                        }
                    }
                    #endregion
                }
                tabOtherInfo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                BaseAppSettings.m_Log.Error(Header + ":: " + string.Format("Error occured : {0} ," + Environment.NewLine + "Error::{12}", Header, ex.StackTrace));
                throw ex;
            }

        }


        public void changeControlValue(string controlName)
        {
            Window parentWin = Window.GetWindow(this);

            string changedControl = string.Empty;

            //ItemDetails value setting


            string _CnttabName = controlName.Substring(0, controlName.IndexOf('_'));
            string _controlName = string.Empty;

            object control = this.FindName(controlName);

            #region [customColumnDetails]
            foreach (string TabName in _tabExist)
            {
                for (int i = 0; i < tabOtherInfo.Items.Count; i++)
                {
                    if (_CnttabName == TabName)
                    {
                        break;
                    }

                    if (((HeaderedContentControl)(tabOtherInfo.Items[i])).Name.Equals("tab" + TabName))
                    {

                        changedControl = controlName.Substring(controlName.IndexOf('_') + 1);
                        changedControl = TabName + "_" + changedControl;
                        object obj = ((HeaderedContentControl)(tabOtherInfo.Items[i])).Content;
                        if (((System.Windows.FrameworkElement)(obj)).Name.Equals("scr" + TabName))
                        {
                            ScrollViewer scrollViewer = (ScrollViewer)((System.Windows.FrameworkElement)(obj));

                            #region [New Code]

                            if (((System.Windows.Controls.Panel)(((System.Windows.Controls.ContentControl)(scrollViewer)).Content)).Name.Equals("grd" + TabName))
                            {
                                Grid grd = (Grid)((System.Windows.Controls.Panel)(((System.Windows.Controls.ContentControl)(scrollViewer)).Content));

                                string typeOfControl = ((FrameworkElement)(control)).GetType().FullName;

                                object objControl = grd.FindName(changedControl);

                                if (objControl != null)
                                {
                                    if (typeOfControl.Equals("Microsoft.Windows.Controls.DatePicker"))
                                    {
                                        ((Microsoft.Windows.Controls.DatePicker)(objControl)).Text = ((Microsoft.Windows.Controls.DatePicker)(control)).Text;
                                    }
                                    else if (typeOfControl.Equals("System.Windows.Controls.TextBox"))
                                    {
                                        ((System.Windows.Controls.TextBox)(objControl)).Text = ((System.Windows.Controls.TextBox)(control)).Text;
                                    }
                                    else if (typeOfControl.Equals("System.Windows.Controls.ComboBox"))
                                    {
                                        ((System.Windows.Controls.ComboBox)(objControl)).SelectedIndex = ((System.Windows.Controls.ComboBox)(control)).SelectedIndex;
                                    }

                                    break;
                                }
                                else
                                {
                                    break;
                                }

                            }
                            #endregion

                        }
                    }
                }

            }

            _cntNameChanged = string.Empty;

            #endregion

        }

        void dtPicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker dtPicker = (DatePicker)sender;
            string name = dtPicker.Name;
            if (string.IsNullOrEmpty(_cntNameChanged) || _cntNameChanged == name)
            {
                //tabOtherInfo.
                _cntNameChanged = dtPicker.Name;
                changeControlValue(name);
            }


        }

        void cmbColVal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox dtPicker = (ComboBox)sender;
            string name = dtPicker.Name;
            if (string.IsNullOrEmpty(_cntNameChanged) || _cntNameChanged == name)
            {
                //tabOtherInfo.
                _cntNameChanged = dtPicker.Name;
                changeControlValue(name);
            }
        }

        void txtColumnValue_TextChanged(object sender, TextChangedEventArgs e)
        {

            TextBox dtPicker = (TextBox)sender;
            string name = dtPicker.Name;
            if (string.IsNullOrEmpty(_cntNameChanged) || _cntNameChanged == name)
            {
                //tabOtherInfo.
                _cntNameChanged = dtPicker.Name;
                changeControlValue(name);
            }

        }





        #endregion

        private void txtCopies_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }

        }


    }

    public enum ItemPrintType
    {
        NEW_ITEM,
        DUPLICATE_ITEM,
        EXISTING_ITEM
    }

    public class colAtt
    {
        public string DataType;
        public bool IsMandatory;
    }

}

