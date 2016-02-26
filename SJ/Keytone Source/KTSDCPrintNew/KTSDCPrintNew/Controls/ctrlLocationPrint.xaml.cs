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

namespace KTone.Win.KTSDCPrint
{
    /// <summary>
    /// Interaction logic for ctrlLocationPrint.xaml
    /// </summary>
    public partial class ctrlLocationPrint : UserControl
    {

        private string userId = KTone.Win.KTSDCWS_DAL.UserAunthetication.UserID;
        private string password = KTone.Win.KTSDCWS_DAL.UserAunthetication.Password;
        private int dataOwnerID = App.DataOwnerId;


        private int locationID = 0;
        private int categoryID = 0;
        private string location = string.Empty;


        public ctrlLocationPrint()
        {
            InitializeComponent();
        }

        private void txtLocation_LostFocus(object sender, RoutedEventArgs e)
        {
            SearchLocation searchLocation = new SearchLocation();

            Nullable<bool> result = searchLocation.ShowDialog();
            
            if (result == true)
            {
              

            }

        }
    }
}
