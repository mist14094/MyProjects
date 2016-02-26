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
using KTWPFAppBase.Controls;

namespace KTone.Win.KTSDCProvision
{
    /// <summary>
    /// Interaction logic for LocationPrint.xaml
    /// </summary>
    public partial class LocationPrint : Window
    {
        public bool isDuplicate = false;


        public LocationPrint()
        {
            InitializeComponent();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTag.Text.Trim()))
            {
                CustomMessageBox.Show("Plese select Tag to print", "Tag", MessageBoxButton.OKCancel);
                return;
            }
            chkPrinted.IsEnabled = true;
            if (chkPrinted.IsEnabled)
            {
                isDuplicate = true;
            }

            if (isDuplicate && (chkPrinted.IsChecked == false))
            {

                CustomMessageBox.Show("Select checkbox for duplicate printing", "Duplicate Printing", MessageBoxButton.OK);
                return;
            }

            //MessageBox.Show("Select checkbox for duplicate printing", "Duplicate Printing", MessageBoxButton.OKCancel);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            //cmbLocation.SelectedIndex = 0;
            //cmbResource.SelectedIndex = 0;
            txtTag.Text = string.Empty;
            txtValue.Text = string.Empty;
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            //SearchLocation 

            SearchLocation selectLocation = new SearchLocation();
            Nullable<bool> result = selectLocation.ShowDialog();
            selectLocation.Activate();
            //selectLocation.Owner = this;
            //selectLocation.Show();
            

            //txtTag.Text = selectLocation.selectedValue;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            AddTag addTag = new AddTag();
            Nullable<bool> result = addTag.ShowDialog();
            addTag.Activate();
            txtTag.Text = addTag.tagValue;
        }
    }
}
