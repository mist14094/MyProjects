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

namespace KTone.Win.KTSDCProvision
{
    /// <summary>
    /// Interaction logic for LocationSelect.xaml
    /// </summary>
    public partial class LocationSelect : Window
    {
        public string selectedValue = string.Empty;
        public LocationSelect()
        {
            InitializeComponent();            

            Grid grid = new Grid();
            grid.Height = 300;
            grid.Width = 250;

            dckPanel.Children.Clear();
            dckPanel.Children.Add(grid);

            selectedValue = "KeyTone Technologies";
        }
    }
}
