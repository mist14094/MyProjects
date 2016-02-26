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
using KTWPFAppBase.Controls;
using KTone.Core.KTIRFID;
using KTWPFAppBase;
using System.Threading;


using DevExpress.Wpf.Grid;
using System.Configuration;
using System.ComponentModel;
using System.Windows.Threading;
using DevExpress.Wpf.Editors;
using DevExpress.Wpf.Editors.Settings;
using TrackerRetailDataAccess;
using System.Data;
using System.IO;
using System.Xml;
using System.Reflection;
using System.Management;
using KTone.Core.KTPrinterApp;



namespace KTone.Win.KTSDCPrint
{
    /// <summary>
    /// Interaction logic for ctrlItemBatchPrinting.xaml
    /// </summary>
    public partial class ctrlItemBatchPrinting : UserControl
    {

        BackgroundWorker bcLoad = new BackgroundWorker();

        Window parentWin;
        public event EventHandler<UpdateEventArgs> UpdateUserInterface;

        #region [Private Section]

        private List<KTItemDetails> _lstItemDetails = null;
        private List<CustomFeildInfo> _lstCustomFieldInfo = new List<CustomFeildInfo>();
        private List<CustomFeildInfo> _lstUnioqeCustomFields = new List<CustomFeildInfo>();
        private Dictionary<string, List<CustomFeildInfo>> _lstGroupFields = new Dictionary<string, List<CustomFeildInfo>>();
        private int dataOwnerID = App.DataOwnerId;
        private int locationID = AppConfigSettings.ItemLocationID;
        private int _noOfItems = 0;
        private int _skuId = 0;
        private List<bool> _unboundData;
        private string selectedType = string.Empty;
        private int noOfCopies = 0;// Convert.ToInt32(txtCopies.Text);
        int IssueCommand = 0;
        private string errMsg = string.Empty;
        //private int noOfCopies = 0;// Convert.ToInt32(txtCopies.Text);

        delegate void ProgressClose();
        public ItemType _itemType = ItemType.All;
        private List<string> _tabExist = new List<string>();
        private Dictionary<string, string> _lstTabFieldValues = new Dictionary<string, string>();

        private bool _isExist = false;
        private bool _isFirstTime = true;
        private bool _isFirstTimeGd = true;
        private bool _isSelectAll = false;
        private int _previousSelection = -1;
        private bool _isAfterPrint = false;


        private bool IsChildCheckbox = false;
        private bool isChkMain = true;
        private bool chkListSelected = false;
        private bool onLoad = true;

        private bool printResult = false;

        private string configPath = string.Empty;
        private Configuration appConfig;



        #endregion
        List<ListViewItemHelper> lstItemHlpr = new List<ListViewItemHelper>();

        DataTable dtPrintDetails = new DataTable();

        string labelFile = string.Empty;

        bool IsVendorTagging = false;
        bool IsManualPrinting = false;

        public ctrlItemBatchPrinting()
        {
            //Mouse.OverrideCursor = Cursors.Wait;
            //this.Cursor = Cursors.Wait;
            InitializeComponent();

        }


        #region [Private Method]


        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            string Header = "ctrlItemBatchPrinting::btnPrint_Click: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");




            Window parentWin = Window.GetWindow(this);
            string err = string.Empty;

            btnReset.IsEnabled = false;

            parentWin.IsEnabled = false;
            parentWin.Cursor = Cursors.None;
            //Mouse.OverrideCursor = Cursors.Wait;
            //Mouse.OverrideCursor = Cursors.None;
            lblMsg.Visibility = Visibility.Hidden;

            Cursor = Cursors.None;

            noOfCopies = Convert.ToInt32(txtCopies.Text);

            try
            {
                string erMsg = string.Empty;

                if (!checkPrinter(AppConfigSettings.PrinterName, out erMsg))
                {
                    lblMsg.Visibility = Visibility.Visible;
                    lblMsg.Text = "Printer is not available.";
                    lblMsg.Foreground = Brushes.Red;
                    btnClose.IsEnabled = true;
                    btnPrint.IsEnabled = true;
                    parentWin.IsEnabled = true;
                    return;
                }


                string currnePath = Environment.CurrentDirectory;

                //   dgUPCList.ItemsSource

                if (string.IsNullOrEmpty(txtCopies.Text))
                {
                    lblMsg.Visibility = Visibility.Visible;
                    lblMsg.Text = "No.of Copies should not be null";
                    txtCopies.Focus();
                    txtCopies.Text = "1";
                    lblMsg.Foreground = Brushes.Red;
                    parentWin.IsEnabled = true;
                    return;
                }
                if (txtCopies.Text == "0")
                {
                    lblMsg.Visibility = Visibility.Visible;
                    lblMsg.Text = "No.of Copies should be greater than 0";
                    txtCopies.Focus();
                    txtCopies.Text = "1";
                    lblMsg.Foreground = Brushes.Red;
                    parentWin.IsEnabled = true;
                    return;
                }
                if (dtPrintDetails != null && dtPrintDetails.Rows.Count > 0 && rbtnManual.IsChecked == true)
                    StartTask();
                else if (rbtnPO.IsChecked == true)
                {
                    StartTask();
                }
                else
                {
                }

                //if (printResult)
                //{
                //    btnPrint.IsEnabled = false;

                //    dtPrintDetails.Rows.Clear();
                //    lvPrintDetails.DataContext = null;

                //    btnClose.IsEnabled = true;
                //    parentWin.IsEnabled = true;
                //    //kt_tagClass.NextSerialNumber = toUpdateNextSerial;
                //    //result = kt_tagClass.UpdateNextSerialNumber();
                //}




            }
            catch (Exception ex)
            {
                btnClose.IsEnabled = true;
                btnPrint.IsEnabled = true;
                parentWin.IsEnabled = true;
                CustomMessageBox.Show(parentWin, "Error:" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                BaseAppSettings.m_Log.ErrorException(String.Format("{0}Error occured.{1}", Header, ex.Message), ex);
                BaseAppSettings.m_Log.Error(Header + ":: " + Environment.NewLine + "Error:: " + ex.StackTrace);

            }
            finally
            {
                //btnClose.IsEnabled = true;
                //btnPrint.IsEnabled = true;
                //parentWin.IsEnabled = true;
                BaseAppSettings.m_Log.Debug(Header + "Leaving..");
            }

        }

        #region Threading


        public void StartTask()
        {

            KTProgressBar progressBar = KTProgressBar.GetInstance(parentWin);

            ThreadPool.QueueUserWorkItem(new WaitCallback(Task), null);

        }
        private void Task(object o)
        {
            string Header = "ctrlItemBatchPrinting::Task(): ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");



            bool result = false;



            KTProgressBar progressBar = KTProgressBar.GetInstance(parentWin);

            try
            {
                progressBar.CurrentStatus = "Item Printing is in process....";

                string currnePath = Environment.CurrentDirectory;
                string InputFileName = string.Empty;
                string InputLabelName = string.Empty;


                KT_TagSerialization kt_tagClass = new KT_TagSerialization();

                string hexVale = string.Empty;//= "3100000000000000000000ff";
                string toUpdateNextSerial = string.Empty;

                int totalCount = 0;

                BaseAppSettings.m_Log.Error("Got Serial Number");



                BaseAppSettings.m_Log.Error("Got Table");


                long totalTagsToPrint = 0;

                KTone.Core.KTPrinterApp.Program clsProgram = new KTone.Core.KTPrinterApp.Program();

                string InputFilePath = string.Empty;
                string InputLabelPath = string.Empty;
                string PrinterName = string.Empty;
                string LabelFile = string.Empty;

                BaseAppSettings.m_Log.Error("Initialized Printer");


                try
                {
                    currnePath = Convert.ToString(ConfigurationManager.AppSettings["LabelName"].ToString()).Trim();
                    InputFilePath = currnePath + "\\" + Convert.ToString(ConfigurationManager.AppSettings["InputFileName"].ToString()).Trim();//.ToLower();
                    InputLabelPath = currnePath + "\\" + labelFile.Trim();//.ToLower();
                    PrinterName = AppConfigSettings.PrinterName;// Convert.ToString(ConfigurationManager.AppSettings["Printer"].ToString()).Trim();//.ToLower();
                    LabelFile = labelFile;//.ToLower();

                }
                catch { }

                string[] args = { "-p", InputFilePath, InputLabelPath, PrinterName, "BarTender", "60" };

                BaseAppSettings.m_Log.Error("Going through each records");

                int count = 0;
                int mainCount = 0;

                long sum = Convert.ToInt64(dtPrintDetails.AsEnumerable().Sum(x => x.Field<int>("Qty")));

                //DataTable dtToPrint = dtPrintDetails.Clone();
                long sumCount = 0;

                if (dgUPCList.ItemsSource != null && dgUPCList.Items.Count > 0)
                {
                    DataView dv = (DataView)dgUPCList.ItemsSource;
                    DataTable dtToPrint = dv.Table.Clone();

                    foreach (DataRow dr in dv.Table.Rows)
                    {

                        if (dr["Print"].ToString().ToLower() == "true")
                        {
                            int qty = Convert.ToInt32(dr["Qty"]);

                            for (int i = 0; i < qty; i++)
                            {
                                dtToPrint.ImportRow(dr);
                                dtToPrint.Rows[i]["Qty"] = 1;

                                mainCount++;
                                sumCount++;
                            }
                            UpdateSKUChanges(dr);
                            hexVale = getHexValue(mainCount);
                            printResult = false;
                            List<string> lstRFIDs = new List<string>();
                            printResult = clsProgram.Print(args, dtToPrint, hexVale.ToUpper(), noOfCopies, out toUpdateNextSerial, out errMsg, out lstRFIDs);


                            if (printResult)
                            {

                                UpdatePODetails(dr, lstRFIDs);

                                if(dr.Table.Columns.Contains("PrintStatus"))
                                    dr["PrintStatus"] = "Success";
                                if (dr.Table.Columns.Contains("IsPrinted"))
                                    dr["IsPrinted"] = true;
                                kt_tagClass.NextSerialNumber = toUpdateNextSerial;

                                dtToPrint.Rows.Clear();
                                mainCount = 0;
                                progressBar.CurrentStatus = "Item Printing is in process.\nNumber of RFID Tag printed = " + sumCount;
                            }
                            else
                            {
                                if (dr.Table.Columns.Contains("PrintStatus"))
                                    dr["PrintStatus"] = "Failed";
                                if (dr.Table.Columns.Contains("IsPrinted"))
                                    dr["IsPrinted"] = false;
                                //errMsg = "Failed to print : Please Call administrator for further details.";
                                // throw new Exception("Failed to print : Please Call administrator for further details.");
                                this.Dispatcher.BeginInvoke(new ThreadStart(() => FireUpdateUseInterface(1, result)), null);
                                BaseAppSettings.m_Log.Error(errMsg);
                                return;

                            }

                            //mainCount++;
                        }



                    }


                    this.Dispatcher.BeginInvoke(new ThreadStart(() => FireUpdateUseInterface(1, result)), null);


                }


                #region OLD Printing Code
                /*
                if (dgUPCList != null && dgUPCList.Items.Count > 0)
                {
                    var sum = 0;//dgUPCList.Items.Sum(x => x.Field<int>("Qty"));

                    foreach (object item in dgUPCList.Items)
                    {
                        // if(item
                    }

                    if (sum != null)
                    {
                        totalCount = Convert.ToInt32(sum);
                    }
                }

                BaseAppSettings.m_Log.Error("Got Table");


                long totalTagsToPrint = 0;

                KTone.Core.KTPrinterApp.Program clsProgram = new KTone.Core.KTPrinterApp.Program();

                string InputFilePath = string.Empty;
                string InputLabelPath = string.Empty;
                string PrinterName = string.Empty;
                string LabelFile = string.Empty;

                BaseAppSettings.m_Log.Error("Initialized Printer");


                try
                {
                    currnePath = Convert.ToString(ConfigurationManager.AppSettings["LabelName"].ToString()).Trim();
                    InputFilePath = currnePath + "\\" + Convert.ToString(ConfigurationManager.AppSettings["InputFileName"].ToString()).Trim();//.ToLower();
                    InputLabelPath = currnePath + "\\" + labelFile.Trim();//.ToLower();
                    PrinterName = AppConfigSettings.PrinterName;// Convert.ToString(ConfigurationManager.AppSettings["Printer"].ToString()).Trim();//.ToLower();
                    LabelFile = labelFile;//.ToLower();

                }
                catch { }

                string[] args = { "-p", InputFilePath, InputLabelPath, PrinterName, "BarTender", "60" };

                BaseAppSettings.m_Log.Error("Going through each records");

                if (dtPrintDetails != null && dtPrintDetails.Rows.Count > 0)
                {

                    int rowCount = dtPrintDetails.Rows.Count;
                    int count = 0;
                    int mainCount = 0;

                    long sum = Convert.ToInt64(dtPrintDetails.AsEnumerable().Sum(x => x.Field<int>("Qty")));

                    DataTable dtToPrint = dtPrintDetails.Clone();
                    long sumCount = 0;

                    for (; count < rowCount; count++)
                    {
                        int qty = Convert.ToInt32(dtPrintDetails.Rows[count]["Qty"]);

                        for (int i = 0; i < qty; i++)
                        {
                            if (mainCount == IssueCommand || sumCount == sum - 1)
                            {
                                if (sumCount == sum - 1)
                                {
                                    dtToPrint.ImportRow(dtPrintDetails.Rows[count]);
                                    dtToPrint.Rows[mainCount]["Qty"] = 1;
                                    hexVale = getHexValue(mainCount + 1);
                                }
                                else
                                {
                                    hexVale = getHexValue(mainCount);
                                }

                                printResult = false;
                                printResult = clsProgram.Print(args, dtToPrint, hexVale.ToUpper(), noOfCopies, out toUpdateNextSerial, out errMsg);
                                if (printResult)
                                {
                                    kt_tagClass.NextSerialNumber = toUpdateNextSerial;
                                    dtToPrint.Rows.Clear();
                                    mainCount = 0;
                                    progressBar.CurrentStatus = "Item Printing is in process.\nNumber of RFID Tag printed = " + sumCount;
                                }
                                else
                                {
                                    //errMsg = "Failed to print : Please Call administrator for further details.";
                                    // throw new Exception("Failed to print : Please Call administrator for further details.");
                                    this.Dispatcher.BeginInvoke(new ThreadStart(() => FireUpdateUseInterface(1, result)), null);
                                    BaseAppSettings.m_Log.Error(errMsg);
                                    return;

                                }
                            }

                            dtToPrint.ImportRow(dtPrintDetails.Rows[count]);
                            dtToPrint.Rows[mainCount]["Qty"] = 1;
                            mainCount++;

                            sumCount++;

                        
                        }
                 }
                    this.Dispatcher.BeginInvoke(new ThreadStart(() => FireUpdateUseInterface(1, result)), null);

                    }
                else
                {

                }
                 * */
                #endregion




            }
            catch (Exception ex)
            {
                // CustomMessageBox.Show(parentWin, "Error:" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                // throw ex;
                this.Dispatcher.BeginInvoke(new ThreadStart(() => FireUpdateUseInterface(1, result)), null);
                BaseAppSettings.m_Log.ErrorException(String.Format("{0}Error occured.{1}", Header, ex.Message), ex);

            }
            finally
            {

                this.Dispatcher.Invoke(new ProgressClose(CloseProgress));
                BaseAppSettings.m_Log.Debug(Header + "Leaving..");
            }


        }

        private void UpdatePODetails(DataRow dr, List<string> lstRFIDs)
        {
            try
            {

                string UPC = ""; string SKU = ""; string Description = ""; float Cost = 0; float Price = 0;
                string VendorName = ""; string stockCode = string.Empty; string PONumber = ""; DateTime dtPODate = new DateTime(); string lineNo = "";
                int OrderQty = 0; string OrderUOM = "";


                if (IsManualPrinting == false)
                {

                    foreach (DataColumn dc in dr.Table.Columns)
                    {
                        switch (dc.ColumnName.ToUpper())
                        {
                            case "UPC":
                                UPC = dr["UPC"].ToString();
                                break;
                            case "SKU":
                                SKU = dr["SKU"].ToString();
                                break;
                            case "DESC":
                                Description = dr["DESC"].ToString();
                                break;
                            case "CUSTOM1":
                                Cost = float.Parse(dr["Custom1"].ToString());
                                break;
                            case "PRICE":
                                Price = float.Parse(dr["Price"].ToString());
                                break;
                            case "VENDORNAME":
                                VendorName = dr["VendorName"].ToString();
                                break;
                            case "PONUMBER":
                                PONumber = dr["PONumber"].ToString();
                                break;
                            case "PODATE":
                                dtPODate = DateTime.Parse(dr["PODate"].ToString());
                                break;
                            case "STOCKCODE":
                                stockCode = dr["StockCode"].ToString();
                                break;
                            case "LINENUMBER":
                                lineNo = dr["LineNumber"].ToString();
                                break;
                            case "QTY":
                                OrderQty = int.Parse(dr["Qty"].ToString());
                                break;
                            case "ORDERUOM":
                                OrderUOM = dr["ORDERUOM"].ToString();
                                break;
                        }
                    }

                    StringBuilder strRFIDs = new StringBuilder();
                    for (int i = 0; i < lstRFIDs.Count; i++)
                    {
                        strRFIDs.Append(lstRFIDs[i]);
                        if (i < lstRFIDs.Count - 1)
                            strRFIDs.Append(",");
                    }

                    string taggingLoc = string.Empty;

                    if (IsVendorTagging == true)
                        taggingLoc = "Vendor";
                    else
                        taggingLoc = "Smokin Joe";


                    KT_POIntegeration dbProd = new KT_POIntegeration();
                    bool flag = dbProd.InsertPODetail(PONumber, dtPODate, taggingLoc, lineNo, stockCode, UPC, SKU, Description, OrderQty, Cost, Price,
                        VendorName, OrderUOM, "PRINTED", lstRFIDs.Count, strRFIDs.ToString(), 1);
                    if (flag == false)
                        throw new Exception("Unable to Add or update PO Details");
                }
                
                /* Code added by Sameer for Manual Print data capturing. */
                else
                {
                    foreach (DataColumn dc in dr.Table.Columns)
                    {
                        switch (dc.ColumnName.ToUpper())
                        {
                            case "UPC":
                                UPC = dr["UPC"].ToString();
                                break;
                            case "SKU":
                                SKU = dr["SKU"].ToString();
                                break;
                            case "DESC":
                                Description = dr["DESC"].ToString();
                                break;
                            case "CUSTOM1":
                                Cost = float.Parse(dr["Custom1"].ToString());
                                break;
                            case "PRICE":
                                Price = float.Parse(dr["Price"].ToString());
                                break;                            
                        }
                    }

                    StringBuilder strRFIDs = new StringBuilder();
                    for (int i = 0; i < lstRFIDs.Count; i++)
                    {
                        strRFIDs.Append(lstRFIDs[i]);
                        if (i < lstRFIDs.Count - 1)
                            strRFIDs.Append(",");
                    }


                    KT_ManualPrint dbProd = new KT_ManualPrint();
                    bool flag = dbProd.InsertManualPrintDetail(UPC, SKU, Description, Cost, Price, strRFIDs.ToString(), AppConfigSettings.UserID);
                    if (flag == false)
                        throw new Exception("Unable to Add Manual Print details.");

                }

                /* Ends here */
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        private bool checkPrinter(string printerName, out string message)
        {
            message = string.Empty;

            try
            {
                // AppSettings.m_Log.Trace("Entering BarTenderPrinter:CheckPrinter()");

                ManagementObjectSearcher moSearch = new ManagementObjectSearcher("Select * from Win32_Printer");
                ManagementObjectCollection moReturn = moSearch.Get();


                foreach (ManagementObject item in moReturn)
                {
                    string Pname = item["Name"].ToString().Trim();
                    string[] Pnames = Pname.Split('\\');
                    Pname = Pnames[Pnames.Length - 1];
                    if (Pname.Equals(printerName))
                    {
                        #region Commanet
                        //ArrayList alParam = new ArrayList();
                        //foreach (PropertyData item1 in item.Properties)
                        //{
                        //    alParam.Add(item1.Name.ToString());
                        //}
                        #endregion

                        int state = Int32.Parse(item["ExtendedPrinterStatus"].ToString());

                        UInt32 stat = UInt32.Parse(item["PrinterState"].ToString());

                        string sta = item["Status"].ToString();

                        if (item["WorkOffline"].ToString().Equals("True"))
                        {
                            message = "Printer is Offline.";
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                AppSettings.m_Log.ErrorException("Error in BarTenderPrinter:CheckPrinter : " + ex.Message, ex);
                throw ex;
            }
            finally
            {
                AppSettings.m_Log.Trace("Leaving BarTenderPrinter:CheckPrinter()");
            }
            return false;
        }

        private bool UpdateSKUChanges(DataRow dr)
        {
            try
            {
                string UPC = ""; string SKU = ""; int storeId = 0; string Description = ""; string Cost = ""; float Price = 0;
                int BatchUOM = 0; string VendorName = "";

                storeId = 101;
                try { storeId = int.Parse(ConfigurationManager.AppSettings.Get("StoreID")); }
                catch { storeId = 101; }

                foreach (DataColumn dc in dr.Table.Columns)
                {
                    switch (dc.ColumnName.ToUpper())
                    {
                        case "UPC":
                            UPC = dr["UPC"].ToString();
                            break;
                        case "SKU":
                            SKU = dr["SKU"].ToString();
                            break;
                        case "DESC":
                            Description = dr["DESC"].ToString();
                            break;
                        case "CUSTOM1":
                            Cost = dr["Custom1"].ToString();
                            break;
                        case "PRICE":
                            Price = float.Parse(dr["PRICE"].ToString());
                            break;
                        case "BATCHUOM":
                            BatchUOM = Convert.ToInt32(dr["BatchUOM"].ToString());
                            break;
                        case "VENDORNAME":
                            VendorName = dr["VendorName"].ToString();
                            break;
                    }
                }

                Products dbProd = new Products();
                bool flag = dbProd.InsertProduct(UPC.Trim(), SKU, storeId, Description, Cost, Price, BatchUOM, VendorName);
                if (flag == false)
                    throw new Exception("Unable to Add or update UPC");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public string getHexValue(int valueToUpdatate)
        {

            KT_TagSerialization kt_tagClass = new KT_TagSerialization();


            string hexVale = string.Empty;//= "3100000000000000000000ff";
            string toUpdateNextSerial = string.Empty;
            int cnt = 0;
            if (valueToUpdatate <= AppConfigSettings.IssueCommand)
                cnt = valueToUpdatate;
            else
                cnt = AppConfigSettings.IssueCommand;
            bool result = kt_tagClass.GetNextSerialNumber(out hexVale, cnt);

            string HexNumber = hexVale.ToString();

            return HexNumber;
        }


        private void CloseProgress()
        {
            string Header = "ctrlItemBatchPrinting::Task(): ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");

            try
            {
                KTProgressBar.RemoveInstance();
            }
            catch (Exception ex)
            {
                BaseAppSettings.m_Log.Error(Header + ":: " + ex.Message);
            }
        }

        void threadedTask_UpdateUI(object sender, UpdateEventArgs e)
        {

        }

        private void FireUpdateUseInterface(int counter, bool result)
        {

            //if (UpdateUserInterface != null)
            //{
            //    UpdateUserInterface(this, new UpdateEventArgs(counter));
            //}

            if (printResult)
            {
                Cursor = Cursors.Arrow;
                btnClose.IsEnabled = true;
                btnPrint.IsEnabled = false;
                btnReset.IsEnabled = true;
                parentWin.IsEnabled = true;
                btnLoad.IsEnabled = true;
                dtPrintDetails.Rows.Clear();
                dgUPCList.DataContext = null;
                dgUPCList.ItemsSource = null;
                dgUPCList.Items.Clear();
                txtPO.Text = "";
                txtUPC.Text = "";
                //lblMsg.Content = "Printing process completed.";
                lblMsg.Foreground = Brushes.Green;
                lblMsg.Text = "Label printing completed successfully";
                lblMsg.Visibility = Visibility.Visible;
                KTProgressBar.RemoveInstance();
            }
            else
            {
                Cursor = Cursors.Arrow;
                parentWin.Cursor = Cursors.Arrow;
                //btnClose.IsEnabled = true;
                btnReset.IsEnabled = true;
                btnPrint.IsEnabled = true;
                parentWin.IsEnabled = true;
                //lblMsg.Content = "Printing process completed.";
                lblMsg.Foreground = Brushes.Red;
                if (!string.IsNullOrEmpty(errMsg))
                {
                    lblMsg.Text = errMsg;
                }
                else
                {
                    lblMsg.Text = "Either BarTender doesn't have license or Printer is not available.";
                }
                // lblMsg.Content = "Label printing failed, Please call administrator for details.";
                lblMsg.Visibility = Visibility.Visible;
                KTProgressBar.RemoveInstance();

            }
        }


        #endregion


        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Window parentWin = Window.GetWindow(this);
            string Header = "ctrlItemBatchPrinting::btnClose_Click: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");
            try
            {
                dckItemPanel.Children.Clear();
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show(parentWin, "Error:" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                BaseAppSettings.m_Log.ErrorException(Header + ":: " + String.Format("{0}Error occured.{1}", Header, ex.Message), ex);
                BaseAppSettings.m_Log.Error(Header + ":: " + Environment.NewLine + "Error:: " + ex.StackTrace);
            }
            finally
            {
                BaseAppSettings.m_Log.Debug(Header + "Leaving..");
            }

        }

        // public static DependencyProperty headerStyle = DependencyProperty.Register("",typeof(HeaderContainerStyle),



        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            this.Cursor = Cursors.Wait;
            string Header = "ctrlItemBatchPrinting::btnClose_Click: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");

            //UpdateUserInterface += new EventHandler<UpdateEventArgs>(threadedTask_UpdateUI);

            UpdateUserInterface += new EventHandler<UpdateEventArgs>(threadedTask_UpdateUI);

            parentWin = Window.GetWindow(this);

            try
            {

                DataTable dtColumns = new DataTable();


                DataColumn dtCol1 = new DataColumn("NAME");
                dtCol1.DataType = System.Type.GetType("System.String");
                dtColumns.Columns.Add(dtCol1);
                DataColumn dtCol2 = new DataColumn("VISIBLENAME");
                dtCol2.DataType = System.Type.GetType("System.String");
                dtColumns.Columns.Add(dtCol2);
                DataColumn dtCol3 = new DataColumn("ISENABLE");
                dtCol3.DataType = System.Type.GetType("System.String");
                dtColumns.Columns.Add(dtCol3);
                DataColumn dtCol4 = new DataColumn("COLUMNORDER");
                dtCol4.DataType = System.Type.GetType("System.Int32");
                dtColumns.Columns.Add(dtCol4);
                DataColumn dtCol5 = new DataColumn("ISEDITABLE");
                dtCol5.DataType = System.Type.GetType("System.Boolean");
                dtColumns.Columns.Add(dtCol5);
                DataColumn dtCol6 = new DataColumn("ISDELETABLE");
                dtCol6.DataType = System.Type.GetType("System.Boolean");
                dtColumns.Columns.Add(dtCol6);



                // Environment.

                //currnePath = Assembly.GetExecutingAssembly().CodeBase.ToString();
                string currnePath = Environment.CurrentDirectory;

                configPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                appConfig = ConfigurationManager.OpenExeConfiguration(configPath);


                string PrinterName = string.Empty;

                string isFirst_NoOfLabel = "false";

                try
                {
                    if (!string.IsNullOrEmpty(AppConfigSettings.PrinterName))
                    {
                        PrinterName = AppConfigSettings.PrinterName;
                    }
                    else
                    {
                        AppConfigSettings.PrinterName = (ConfigurationManager.AppSettings["Printer"]);
                        PrinterName = Convert.ToString(ConfigurationManager.AppSettings["Printer"]);
                    }
                    isFirst_NoOfLabel = Convert.ToString(ConfigurationManager.AppSettings["IsFirst_NoOfLabel"]);

                }
                catch
                {
                }

                if (!string.IsNullOrEmpty(PrinterName))
                {
                    txtPrinter.Text = PrinterName;

                    ManagementScope scope = new ManagementScope(@"\root\cimv2");
                    scope.Connect();

                    // Select Printers from WMI Object Collections
                    ManagementObjectSearcher searcher = new
                     ManagementObjectSearcher("SELECT * FROM Win32_Printer");

                    string printerName = "";
                    foreach (ManagementObject printer in searcher.Get())
                    {
                        printerName = printer["Name"].ToString().ToLower();
                        if (printerName.Equals(PrinterName.ToLower()))
                        {
                            Console.WriteLine("Printer = " + printer["Name"]);
                            if (printer["WorkOffline"].ToString().ToLower().Equals("true"))
                            {
                                // printer is offline by user
                                lblOnline.Content = "OFFLINE";
                                lblOnline.Background = Brushes.Red;
                                btnAdd.IsEnabled = false;
                            }
                            else
                            {
                                // printer is not offline
                                lblOnline.Content = "ONLINE";
                                lblOnline.Background = Brushes.Green;
                            }
                        }
                    }
                }
                else
                {
                    lblOnline.Content = "OFFLINE";
                    lblOnline.Background = Brushes.Red;
                    btnAdd.IsEnabled = false;
                }

                string LabelFormat = string.Empty;
                if (string.IsNullOrEmpty(AppConfigSettings.LabelName))
                {
                    try
                    {
                        AppConfigSettings.LabelName = (ConfigurationManager.AppSettings["LabelFormat"]);
                        LabelFormat = AppConfigSettings.LabelName;
                        labelFile = LabelFormat;
                        AppConfigSettings.LabelDecs = (ConfigurationManager.AppSettings["LabelDescription"]);
                        txtDesc.Text = AppConfigSettings.LabelDecs;
                        txtDesc.ToolTip = AppConfigSettings.LabelDecs;
                    }
                    catch
                    {
                    }
                }
                else
                {
                    LabelFormat = AppConfigSettings.LabelName;
                    txtDesc.Text = AppConfigSettings.LabelDecs;
                }

                if (AppConfigSettings.IssueCommand == 0)
                {
                    try
                    {
                        AppConfigSettings.IssueCommand = Convert.ToInt32((ConfigurationManager.AppSettings["IssueCommand"]));
                        IssueCommand = AppConfigSettings.IssueCommand;
                    }
                    catch
                    { }
                }
                else
                {
                    IssueCommand = AppConfigSettings.IssueCommand;
                }

                ConfigLabelSection lablesection = ConfigLabelSection.GetSections();
                if (lablesection.LabelSetting.Count > 0)
                {
                    for (int i = 0; i < lablesection.LabelSetting.Count; i++)
                    {
                        string labelName = Convert.ToString(lablesection.LabelSetting[i].LABELNAME);// string.Empty;
                        string lableFile = Convert.ToString(lablesection.LabelSetting[i].FILENAME);
                        string Description = Convert.ToString(lablesection.LabelSetting[i].DESCRIPTION);

                        ComboBoxItem cbi = new ComboBoxItem();
                        cbi.Content = labelName;
                        cbi.Tag = lableFile + "," + Description;
                        cmbLabelType.Items.Add(cbi);

                        string[] labDesc = Convert.ToString(cbi.Tag).Split(',');

                        if (!string.IsNullOrEmpty(LabelFormat) && labDesc[0] == LabelFormat)
                        {
                            cbi.IsSelected = true;
                        }
                    }

                    if (string.IsNullOrEmpty(LabelFormat))
                    {
                        cmbLabelType.SelectedIndex = 0;
                    }


                }

                ConfigColumnSection cs = ConfigColumnSection.GetSection();



                if (cs.MySettings.Count > 0)
                {

                    for (int i = 0; i < cs.MySettings.Count; i++)
                    {
                        DataRow dt = dtColumns.NewRow();

                        dt["NAME"] = Convert.ToString(cs.MySettings[i].NAME);
                        dt["VISIBLENAME"] = Convert.ToString(cs.MySettings[i].VISIBLENAME);
                        dt["ISENABLE"] = Convert.ToString(cs.MySettings[i].ISENABLE);
                        if (string.IsNullOrEmpty(cs.MySettings[i].COLUMNORDER))
                        {
                            dt["COLUMNORDER"] = 100;
                        }
                        else
                            dt["COLUMNORDER"] = Convert.ToInt32(cs.MySettings[i].COLUMNORDER);
                        if (string.IsNullOrEmpty(cs.MySettings[i].ISEDITABLE))
                        {
                            dt["ISEDITABLE"] = false;
                        }
                        else
                            dt["ISEDITABLE"] = Convert.ToBoolean(cs.MySettings[i].ISEDITABLE);
                        if (string.IsNullOrEmpty(cs.MySettings[i].ISDELETABLE))
                        {
                            dt["ISDELETABLE"] = false;
                        }
                        else
                            dt["ISDELETABLE"] = Convert.ToBoolean(cs.MySettings[i].ISDELETABLE);
                        dtColumns.Rows.Add(dt);
                        dtColumns.AcceptChanges();
                    }

                    dtColumns = dtColumns.Select("", "COLUMNORDER").CopyToDataTable();



                    if (isFirst_NoOfLabel.ToUpper().Equals("TRUE"))
                    {
                        DataGridTextColumn dgTextColumn1 = new DataGridTextColumn() { Header = "No.of RFID Labels" };

                        dgTextColumn1.Width = DataGridLength.Auto;
                        dgTextColumn1.IsReadOnly = false;
                        // dgTextColumn.DisplayMemberBinding = new Binding("Qty");
                        // dgTextColumn1.CellStyle = new Style(Resources["SingleClickEditing"]);
                        dgTextColumn1.CellStyle = ((Style)this.FindResource("SingleClickEditing"));
                        dgTextColumn1.Binding = new Binding("Qty");

                        dgTextColumn1.DisplayIndex = 0;
                        //column.HeaderContainerStyle = new Style(Style.Resources.FindName());
                        dgUPCList.Columns.Add(dgTextColumn1);

                        DataGridCheckBoxColumn dgTextColumn2 = new DataGridCheckBoxColumn() { Header = "Print" };

                        dgTextColumn2.Width = DataGridLength.Auto;
                        dgTextColumn2.IsReadOnly = false;
                        dgTextColumn2.CellStyle = ((Style)this.FindResource("SingleClickEditing"));
                        dgTextColumn2.Binding = new Binding("Print");

                        dgTextColumn2.DisplayIndex = 0;
                        dgTextColumn2.Width = 105;
                        dgUPCList.Columns.Add(dgTextColumn2);


                        foreach (DataRow dr in dtColumns.Rows)
                        {
                            string ColumnName = Convert.ToString(dr["NAME"]);
                            string VisibleName = Convert.ToString(dr["VISIBLENAME"]);

                            if (Convert.ToString(dr["ISENABLE"]).ToLower() == "true")
                            {
                                DataGridTextColumn dgTextColumn = new DataGridTextColumn() { Header = ColumnName };

                                if (!string.IsNullOrEmpty(VisibleName))
                                    dgTextColumn.Header = VisibleName;
                                else
                                {
                                    dgTextColumn.Header = ColumnName;
                                }
                                dgTextColumn.Width = DataGridLength.Auto;
                                if (Convert.ToString(dr["ISEDITABLE"]).ToLower() == "true")
                                    dgTextColumn.IsReadOnly = false;
                                else
                                    dgTextColumn.IsReadOnly = true;
                                //if (Convert.ToString(dr["ISDELETABLE"]).ToLower() == "true")
                                //    dgTextColumn.IsD = false;
                                //else
                                //    dgTextColumn.IsReadOnly = true;
                                dgTextColumn.Binding = new Binding(ColumnName);
                                //column.HeaderContainerStyle = new Style(Style.Resources.FindName());
                                dgUPCList.Columns.Add(dgTextColumn);

                                //column.HeaderContainerStyle = // "{StaticResource hcs}";


                            }
                        }



                    }
                    else
                    {

                        foreach (DataRow dr in dtColumns.Rows)
                        {
                            string ColumnName = Convert.ToString(dr["NAME"]);
                            string VisibleName = Convert.ToString(dr["VISIBLENAME"]);

                            if (Convert.ToString(dr["ISENABLE"]).ToLower() == "true")
                            {
                                DataGridTextColumn dgTextColumn = new DataGridTextColumn() { Header = ColumnName };

                                if (!string.IsNullOrEmpty(VisibleName))
                                    dgTextColumn.Header = VisibleName;
                                else
                                {
                                    dgTextColumn.Header = ColumnName;
                                }
                                dgTextColumn.Width = DataGridLength.Auto;
                                if (Convert.ToString(dr["ISEDITABLE"]).ToLower() == "true")
                                    dgTextColumn.IsReadOnly = false;
                                else
                                    dgTextColumn.IsReadOnly = true;
                                dgTextColumn.Binding = new Binding(ColumnName);
                                //column.HeaderContainerStyle = new Style(Style.Resources.FindName());
                                dgUPCList.Columns.Add(dgTextColumn);

                                //column.HeaderContainerStyle = // "{StaticResource hcs}";


                            }
                        }


                        DataGridTextColumn dgTextColumn1 = new DataGridTextColumn() { Header = "No.of RFID Labels" };

                        dgTextColumn1.Width = DataGridLength.Auto;
                        dgTextColumn1.IsReadOnly = false;
                        dgTextColumn1.CellStyle = ((Style)this.FindResource("SingleClickEditing"));
                        // dgTextColumn.DisplayMemberBinding = new Binding("Qty");
                        dgTextColumn1.DisplayIndex = 0;
                        dgTextColumn1.Binding = new Binding("Qty");
                        //column.HeaderContainerStyle = new Style(Style.Resources.FindName());
                        dgUPCList.Columns.Add(dgTextColumn1);


                        DataGridCheckBoxColumn dgTextColumn2 = new DataGridCheckBoxColumn() { Header = "Print" };

                        dgTextColumn2.Width = DataGridLength.Auto;
                        dgTextColumn2.IsReadOnly = false;
                        dgTextColumn2.CellStyle = ((Style)this.FindResource("SingleClickEditing"));
                        dgTextColumn2.Binding = new Binding("Print");


                        dgTextColumn2.DisplayIndex = 0;
                        dgTextColumn2.Width = 105;
                        dgUPCList.Columns.Add(dgTextColumn2);



                    }

                    //GridView grdView = this.dgUPCList.View as GridView;


                    //if (grdView != null)
                    //{
                    //    grdView.ColumnHeaderContainerStyle = (Style)this.FindResource("hcs");
                    //}
                }




            }
            catch (Exception ex)
            {
                CustomMessageBox.Show(parentWin, "Error:" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                BaseAppSettings.m_Log.ErrorException(Header + ":: " + String.Format("{0}Error occured.{1}", Header, ex.Message), ex);
                BaseAppSettings.m_Log.Error(Header + ":: " + Environment.NewLine + "Error:: " + ex.StackTrace);
            }
            finally
            {
                Mouse.OverrideCursor = Cursors.Arrow;
                this.Cursor = Cursors.Arrow;
                parentWin.Cursor = Cursors.Arrow;
                BaseAppSettings.m_Log.Debug(Header + "Leaving..");
            }


        }


        private void DataGridCell_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DataGridCell cell = sender as DataGridCell;


                if (cell != null && !cell.IsEditing && !cell.IsReadOnly)
                {
                    if (!cell.IsFocused)
                    {
                        cell.Focus();
                    }
                    DataGrid dataGrid = FindVisualParent<DataGrid>(cell);
                    if (dataGrid != null)
                    {
                        if (dataGrid.SelectionUnit != DataGridSelectionUnit.FullRow)
                        {
                            if (!cell.IsSelected)
                                cell.IsSelected = true;
                        }
                        else
                        {
                            DataGridRow row = FindVisualParent<DataGridRow>(cell);
                            if (row != null && !row.IsSelected)
                            {
                                row.IsSelected = true;
                            }
                        }
                    }
                }


                cell.LostFocus += new RoutedEventHandler(cell_LostFocus);
            }
            catch { }
        }

        void cell_LostFocus(object sender, RoutedEventArgs e)
        {
            //e..LostFocus -= new RoutedEventHandler(cell_LostFocus);



        }

        static T FindVisualParent<T>(UIElement element) where T : UIElement
        {
            UIElement parent = element;
            while (parent != null)
            {
                T correctlyTyped = parent as T;
                if (correctlyTyped != null)
                {
                    return correctlyTyped;
                }

                parent = VisualTreeHelper.GetParent(parent) as UIElement;
            }
            return null;
        }

        void txtCopies_KeyDown(object sender, KeyEventArgs e)
        {


            if (Keyboard.Modifiers == ModifierKeys.Shift && (e.Key >= Key.D0 && e.Key <= Key.D9))
                e.Handled = true;

            if (!((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ||
                ((e.Key >= Key.D0 && e.Key <= Key.D9) && (!(e.Key == Key.LeftShift) || !(e.Key == Key.RightShift) || !(e.Key == Key.System))) ||
                e.Key == Key.Back || e.Key == Key.Delete || e.Key ==
                Key.LeftAlt || e.Key == Key.Left || e.Key == Key.Right ||
                e.Key == Key.LeftShift || e.Key == Key.RightShift || e.Key == Key.System || e.Key == Key.Home || e.Key ==
                Key.End || e.Key == Key.Tab))
                e.Handled = true;// .SuppressKeyPress = true;

        }

        #endregion

        public void PrintDetailsGenerate()
        {
            dtPrintDetails = new DataTable();

            DataColumn dtCol1 = new DataColumn("UPC");
            dtCol1.DataType = System.Type.GetType("System.String");
            dtPrintDetails.Columns.Add(dtCol1);
            DataColumn dtCol2 = new DataColumn("SKU");
            dtCol2.DataType = System.Type.GetType("System.String");
            dtPrintDetails.Columns.Add(dtCol2);
            DataColumn dtCol3 = new DataColumn("Desc");
            dtCol3.DataType = System.Type.GetType("System.String");
            dtPrintDetails.Columns.Add(dtCol3);
            DataColumn dtCol4 = new DataColumn("Qty");
            dtCol4.DataType = System.Type.GetType("System.Int32");
            dtPrintDetails.Columns.Add(dtCol4);
            DataColumn dtCol5 = new DataColumn("Price");
            dtCol5.DataType = System.Type.GetType("System.Double");
            dtPrintDetails.Columns.Add(dtCol5);
            DataColumn dtCol6 = new DataColumn("Cost");
            dtCol6.DataType = System.Type.GetType("System.Double");
            dtPrintDetails.Columns.Add(dtCol6);


        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string Header = "ctrlItemBatchPrinting::btnAdd_Click: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");

            Window parentWin = Window.GetWindow(this);
            try
            {
                AddUPC(txtUPC.Text, txtQuantity.Text);

                txtQuantity.Text = "";
                txtUPC.Text = "";


            }
            catch (Exception ex)
            {

                //CustomMessageBox.Show(parentWin, "Error:" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                BaseAppSettings.m_Log.ErrorException(Header + ":: " + String.Format("{0}Error occured.{1}", Header, ex.Message), ex);
                BaseAppSettings.m_Log.Error(Header + ":: " + Environment.NewLine + "Error:: " + ex.StackTrace);
            }
            finally
            {
                BaseAppSettings.m_Log.Debug(Header + "Leaving..");
            }
        }

        private void AddUPC(string txtUPC, string txtQuantity)
        {

            AddUPC(txtUPC, txtQuantity, "", "", "", "", "", "", true);
        }

        private void txtQuantity_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }

        private void cmbLabelType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            string LabelDesc = Convert.ToString(((System.Windows.FrameworkElement)(((System.Windows.Controls.Primitives.Selector)(sender)).SelectedItem)).Tag);

            if (!string.IsNullOrEmpty(LabelDesc))
            {
                string[] labDesc = LabelDesc.Split(',');

                AppConfigSettings.LabelName = Convert.ToString(labDesc[0]);
                AppConfigSettings.LabelDecs = Convert.ToString(labDesc[1]);

                txtDesc.Text = AppConfigSettings.LabelDecs;
                txtDesc.ToolTip = AppConfigSettings.LabelDecs;


                appConfig.AppSettings.Settings["LabelFormat"].Value = Convert.ToString(labDesc[0]); ;
                appConfig.AppSettings.Settings["LabelDescription"].Value = Convert.ToString(labDesc[1]);
                labelFile = labDesc[0];
                appConfig.Save();

            }

        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            string Header = "ctrlItemBatchPrinting::btnImport_Click: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");

            Window parentWin = Window.GetWindow(this);
            try
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.DefaultExt = ".UPC";
                dlg.Filter = "UPC (*.UPC)|*.UPC|Text (*.txt)|*.txt";

                Nullable<bool> result = dlg.ShowDialog();


                // Get the selected file name and display in a TextBox 
                if (result == true)
                {
                    // Open document 
                    string filename = dlg.FileName;
                    lblMsg.Text = "";
                    lblMsg.Visibility = Visibility.Hidden;

                    StreamReader fStream = File.OpenText(filename);
                    string strReadFile = fStream.ReadToEnd();

                    string[] strLines = strReadFile.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                    bool isHeader = false;
                    try { isHeader = bool.Parse(ConfigurationManager.AppSettings.Get("WithHeader")); }
                    catch { isHeader = false; }


                    for (int i = 0; i <= strLines.Length; i++)
                    {
                        string field = strLines[i];
                        if (i == 0 && isHeader)
                            continue;

                        if (field.Trim().Equals(string.Empty))
                            continue;

                        string[] strfields = strLines[i].Split(new char[] { '|' });
                        string UPC = strfields[4].Trim();
                        string SKU = "";//strfields[2].Trim();
                        string Desc = strfields[1].Trim();
                        string Cost = "0.0"; // strfields[3].Trim();
                        string Price = strfields[5].Trim();
                        string Quantity = strfields[0].Trim();
                        string batchUOM = "1";
                        string VendorName = "";

                        AddUPC(UPC, Quantity, SKU, Desc, Cost, Price, batchUOM, VendorName, false);
                    }


                }


            }
            catch (Exception ex)
            {

                //CustomMessageBox.Show(parentWin, "Error:" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                BaseAppSettings.m_Log.ErrorException(Header + ":: " + String.Format("{0}Error occured.{1}", Header, ex.Message), ex);
                BaseAppSettings.m_Log.Error(Header + ":: " + Environment.NewLine + "Error:: " + ex.StackTrace);
            }
            finally
            {
                BaseAppSettings.m_Log.Debug(Header + "Leaving..");
            }
        }

        private void AddUPC(string txtUPC, string txtQuantity, string txtSKU, string txtDesc, string Cost, string Price, string batchUOM, string VendorName, bool isManual)
        {
            lblMsg.Visibility = Visibility.Collapsed;

            float fCost = 0, fPrice = 0;
            int iBatchUOM = 0;

            #region Begin Validation
            if (string.IsNullOrEmpty(txtUPC))
            {
                if (isManual)
                {
                    lblMsg.Text = "Enter UPC to continue";
                    lblMsg.Visibility = Visibility.Visible;
                    lblMsg.Foreground = Brushes.Red;
                }
                return;
            }


            if (string.IsNullOrEmpty(txtQuantity))
            {
                if (isManual)
                {
                    lblMsg.Text = "Enter number of RFID labels to continue";
                    lblMsg.Visibility = Visibility.Visible;
                    lblMsg.Foreground = Brushes.Red;
                }
                return;
            }

            if (txtQuantity.Trim().Equals("0"))
            {
                if (isManual)
                {
                    lblMsg.Text = "Entered number of RFID labels should be greater than 0.";
                    lblMsg.Visibility = Visibility.Visible;
                    lblMsg.Foreground = Brushes.Red;
                }
                return;
            }

            if (!isManual)
            {

                if (string.IsNullOrEmpty(txtSKU))
                {

                    // return;
                }


                if (string.IsNullOrEmpty(txtDesc))
                {
                    return;
                }

                if (string.IsNullOrEmpty(Cost))
                {
                    // return;
                }

                if (string.IsNullOrEmpty(Price))
                {
                    return;
                }

                if (!float.TryParse(Cost, out fCost))
                {
                    // return;
                }
                if (!float.TryParse(Price, out fPrice))
                {
                    return;
                }
                if (!int.TryParse(batchUOM, out iBatchUOM))
                {
                    batchUOM = "1";
                    iBatchUOM = 1;

                    // return;
                }
                if (string.IsNullOrEmpty(VendorName))
                {
                    // return;
                }
            }
            #endregion

            Products clsProducts = new Products();
            clsProducts.StoreID = 0;
            clsProducts.UPC = txtUPC.Trim();

            DataTable dtUPC = clsProducts.GetProductdetailsForUPC1();

            #region IF UPC Exist
            if (dtUPC != null && dtUPC.Rows.Count > 0)
            {

                if (dtPrintDetails == null || dtPrintDetails.Columns.Count <= 0)
                {
                    dtPrintDetails = dtUPC.Clone();

                    foreach (DataColumn dt in dtPrintDetails.Columns)
                    {
                        dt.DataType = Type.GetType("System.String");
                    }

                    dtPrintDetails.Columns.Add("Qty", Type.GetType("System.Int32"));
                    dtPrintDetails.Columns.Add("Print", Type.GetType("System.Boolean"));

                }



                DataRow dr = dtPrintDetails.NewRow();
                for (int i = 0; i < dtPrintDetails.Columns.Count; i++)
                {
                    if (dtPrintDetails.Columns.Count - 2 != i)
                    {

                        if (string.IsNullOrEmpty(Convert.ToString(dtUPC.Rows[0][i])))
                        {
                            dr[i] = "";
                        }
                        else

                            dr[i] = Convert.ToString(dtUPC.Rows[0][i]);



                        btnPrint.IsEnabled = true;



                    }
                    else
                    {
                        dr[i] = Convert.ToInt32(txtQuantity);
                        dr[i + 1] = true;
                        break;
                    }
                }

                dtPrintDetails.Rows.Add(dr);
                dtPrintDetails.AcceptChanges();

                //lvPrintDetails.DataContext = null;
                //lvPrintDetails.DataContext = dtPrintDetails;

                dgUPCList.ItemsSource = null;
                DataView tempView = dtPrintDetails.AsDataView();
                tempView.AllowEdit = true;
                tempView.AllowDelete = true;




                dgUPCList.ItemsSource = tempView;



                // dgUPCList.DisplayMemberPath = "Product";
            }
            #endregion
            #region IF NOT Exist
            else
            {
                if (isManual)
                {
                    lblMsg.Visibility = Visibility.Visible;
                    lblMsg.Text = "Entered UPC " + txtUPC + " does not exists.";
                    lblMsg.Foreground = Brushes.Red;
                }
                else
                {
                    bool isAddNewUPC = false;
                    try { isAddNewUPC = bool.Parse(ConfigurationManager.AppSettings.Get("AddUPC")); }
                    catch { isAddNewUPC = false; }

                    int storeId = 101;
                    try { storeId = int.Parse(ConfigurationManager.AppSettings.Get("StoreID")); }
                    catch { storeId = 101; }

                    if (isAddNewUPC)
                    {
                        if (clsProducts.InsertProduct(txtUPC, txtSKU, storeId, txtDesc, Cost, fPrice, iBatchUOM, VendorName))
                        {
                            AddUPC(txtUPC, txtQuantity);
                        }
                    }

                    lblMsg.Visibility = Visibility.Visible;
                    lblMsg.Text += "Entered UPC " + txtUPC + " does not exists.";
                    lblMsg.Foreground = Brushes.Red;
                }
            }
            #endregion



        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            string Header = "ctrlItemBatchPrinting::btnLoad_Click: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");

            Window parentWin = Window.GetWindow(this);
            try
            {
                lblMsg.Text = "";
                lblMsg.Visibility = System.Windows.Visibility.Collapsed;
                if (string.IsNullOrEmpty(txtPO.Text))
                {
                    lblMsg.Visibility = Visibility.Visible;
                    lblMsg.Text = "Entered proper PO#.";
                    lblMsg.Foreground = Brushes.Red;
                    return;
                }

                dgUPCList.ItemsSource = null;
                Products prdDB = new Products();
                DataTable dtPODetails = prdDB.FetchPONumber(txtPO.Text);
                dtPODetails.Columns.Add(new DataColumn("Print", typeof(Boolean)));
                foreach (DataRow dr in dtPODetails.Rows)
                {
                    dr["Print"] = true;
                }
                dgUPCList.ItemsSource = dtPODetails.AsDataView();

                btnLoad.IsEnabled = false;
                btnPrint.IsEnabled = true;
            }
            catch (Exception ex)
            {

                //CustomMessageBox.Show(parentWin, "Error:" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                BaseAppSettings.m_Log.ErrorException(Header + ":: " + String.Format("{0}Error occured.{1}", Header, ex.Message), ex);
                BaseAppSettings.m_Log.Error(Header + ":: " + Environment.NewLine + "Error:: " + ex.StackTrace);
            }
            finally
            {
                BaseAppSettings.m_Log.Debug(Header + "Leaving..");
            }
        }

        private void rbtnManual_Checked(object sender, RoutedEventArgs e)
        {
            string Header = "ctrlItemBatchPrinting::rbtnManual_Checked: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");

            Window parentWin = Window.GetWindow(this);
            try
            {
                IsManualPrinting = true;
                rbtnManual.IsChecked = true;
                rbtnPO.IsChecked = false;
                grdInputParaManual.Visibility = System.Windows.Visibility.Visible;
                grdPOENtry.Visibility = System.Windows.Visibility.Collapsed;
            }
            catch (Exception ex)
            {

                //CustomMessageBox.Show(parentWin, "Error:" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                BaseAppSettings.m_Log.ErrorException(Header + ":: " + String.Format("{0}Error occured.{1}", Header, ex.Message), ex);
                BaseAppSettings.m_Log.Error(Header + ":: " + Environment.NewLine + "Error:: " + ex.StackTrace);
            }
            finally
            {
                BaseAppSettings.m_Log.Debug(Header + "Leaving..");
            }
        }

        private void rbtnPO_Checked(object sender, RoutedEventArgs e)
        {
            string Header = "ctrlItemBatchPrinting::rbtnPO_Checked: ";
            BaseAppSettings.m_Log.Debug(Header + "Entering.. ");

            Window parentWin = Window.GetWindow(this);
            try
            {
                IsManualPrinting = false;
                rbtnManual.IsChecked = false;
                rbtnPO.IsChecked = true;
                grdInputParaManual.Visibility = System.Windows.Visibility.Collapsed;
                grdPOENtry.Visibility = System.Windows.Visibility.Visible;
            }
            catch (Exception ex)
            {

                //CustomMessageBox.Show(parentWin, "Error:" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                BaseAppSettings.m_Log.ErrorException(Header + ":: " + String.Format("{0}Error occured.{1}", Header, ex.Message), ex);
                BaseAppSettings.m_Log.Error(Header + ":: " + Environment.NewLine + "Error:: " + ex.StackTrace);
            }
            finally
            {
                BaseAppSettings.m_Log.Debug(Header + "Leaving..");
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cursor = Cursors.Arrow;
                btnClose.IsEnabled = true;
                btnPrint.IsEnabled = false;
                parentWin.IsEnabled = true;
                btnLoad.IsEnabled = true;
                dtPrintDetails.Rows.Clear();
                dgUPCList.DataContext = null;
                dgUPCList.ItemsSource = null;
                dgUPCList.Items.Clear();
                txtUPC.Text = "";
                txtPO.Text = "";

                lblMsg.Visibility = Visibility.Collapsed;
            }
            catch
            {
            }
        }

        private void rBtnSJ_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                IsVendorTagging = false;
                rBtnSJ.IsChecked = true;
                rBtnVendor.IsChecked = false;

            }
            catch { }
        }

        private void rBtnVendor_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                IsVendorTagging = true;
                rBtnSJ.IsChecked = false;
                rBtnVendor.IsChecked = true;

            }
            catch { }
        }



    }


    public class ListViewItemHelper
    {
        public ListViewItemHelper()
        {

        }

        public KTItemDetails ItemDetails { get; set; }
        public bool IsSelectedItem { get; set; }
        public string PrintStatus { get; set; }
    }

    public class UpdateEventArgs : EventArgs
    {
        public UpdateEventArgs()
            : base()
        {
        }

        public UpdateEventArgs(int nCounter)
            : this()
        {
            _counter = nCounter;
        }

        private int _counter;
        public int Counter
        {
            get { return _counter; }
            set { _counter = value; }
        }
    }



}
