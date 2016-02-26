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
using KTone.Core.KTIRFID;
using KTone.Win.KTSDCWS_DAL;
using DevExpress.Wpf.Grid;
using KTWPFAppBase.Controls;

namespace KTone.Win.KTSDCPrint
{
    /// <summary>
    /// Interaction logic for ctrlItemGrid.xaml
    /// </summary>
    public partial class ctrlItemGrid : UserControl
    {

        
        private bool isSingleCheck = true;

        public Dictionary<string, string> dctPrintStatus = new Dictionary<string, string>();
        public Dictionary<string, KTItemDetails> dctItemList = new Dictionary<string, KTItemDetails>();
        public List<string> lstItemList = new List<string>();
        private List<KTItemDetails> lstItemDetails = null;
        private string userId = KTone.Win.KTSDCWS_DAL.UserAunthetication.UserID;
        private string password = KTone.Win.KTSDCWS_DAL.UserAunthetication.Password;
        public ctrlItemGrid()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //createItemList();
            
        }

        public void createItemList()
        {
            //ListViewItem lvItem = new ListViewItem();
            //ListViewItemHelper item;
            //ItemPrint itemPrint = new ItemPrint();
            //lstItemDetails = itemPrint.GetItemsForBatchPrinting(userId, password, 1, 100, ItemType.All);

            //if (lstItemDetails != null && lstItemDetails.Count > 0)
            //{
            //    //lvItemDetails.ItemsSource = lstItemDetails;

            //    List<ListViewItemHelper> lstItem = new List<ListViewItemHelper>();
            //    foreach (var a in lstItemDetails)
            //    {

            //        item = new ListViewItemHelper();
            //        //a.ID, a.SKU_ID, a.Status, a.CustomerUniqueID, a.DataOwnerID, a.CreatedDate, a.UpdatedDate, a.LastSeenTime, a.TimeStamp, a.LastSeenLocation, a.MovementID, a.MovementDetailsID, a.CreatedBy, a.UpdatedBy, a.TagDetails, a.IsActive, a.Comments, a.CustomColumnDetails, a.ItemStatus, false , "");
            //        lvItem. = item;
            //        lvItem.Tag = a.ID;



            //        lstItem.Add(item);

            //    }
            //    gdItemList.DataSource = lstItem;

            //    // lvItemDetails.ItemsSource = lstItemDetails;
            //}
        }

        private void CheckEdit_Checked(object sender, RoutedEventArgs e)
        {
            SelectAllItems(true);
        }

        private void CheckEdit_Unchecked(object sender, RoutedEventArgs e)
        {
            SelectAllItems(false);
        }

       
        public List<Int64> GetSelectedIDs(string id)
        {
            List<Int64> itemId = new List<Int64>();
            //get the selected asset IDs form the grid
            int RowCont = gdItemList.VisibleRowCount;

            for (int i = 0; i < RowCont; i++)
            {
                bool isRowChecked = false;
                isRowChecked = (bool)gdItemList.GetCellValue(i, "IsCompleted");
                if (isRowChecked)
                {
                    Int64 ID = 0;
                    if (id == "AssetID")
                        ID = Convert.ToInt64(gdItemList.GetCellValue(i, "AssetID").ToString());
                    else if (id == "AssetTripID")
                        ID = Convert.ToInt64(gdItemList.GetCellValue(i, "AssetTripID").ToString());
                    itemId.Add(ID);
                }
            }
            return itemId;
        }

        private void SelectAllItems(bool selectAll)
        {
            int RowCont = gdItemList.VisibleRowCount;
            for (int i = 0; i < RowCont; i++)
            {
                gdItemList.SetCellValue(i, "IsSelectedItem", selectAll);
            }
            gdItemList.RefreshData();
        }
        
         //private void gdItemList_Loaded(object sender, RoutedEventArgs e)
        //{
        //    string headr = "CtlSelectAssets::gdItemList_Loaded:";
        //    App.m_Log.Trace(headr + "   Entering ....   ");
        //    try
        //    {
        //        Dispatcher.BeginInvoke(
        //                  new System.Action(((TableView)gdItemList.View).BestFitColumns),
        //                  System.Windows.Threading.DispatcherPriority.Normal
        //              );
        //    }
        //    catch (Exception exc)
        //    {
        //        App.m_Log.ErrorException(headr + exc.Message, exc);
        //        CustomMessageBox.Show("Error:" + exc.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //    finally
        //    {
        //        App.m_Log.Trace(headr + "   Leaving ....    ");
        //    }
        //}

    }

    
}
