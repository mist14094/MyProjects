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
using KTWPFAppBase.Classes;
using System.Configuration;

namespace KTWPFAppBase.Controls
{
    /// <summary>
    /// Interaction logic for ctlWebCamList.xaml
    /// </summary>
    public partial class ctlWebCamList : UserControl
    {
        public ctlWebCamList()
        {
            InitializeComponent();

        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Device[] devices = Devicemanager.GetAllDevices();

            string devName = System.Configuration.ConfigurationManager.AppSettings["WebCameraName"];

            foreach (Device d in devices)
            {
                char[] myChar = { ' ', '\0' };
                cmbWebCamDiv.Items.Add(d.Name.TrimEnd(myChar));

            }
            if (cmbWebCamDiv.Items.Contains(devName))
            {
                cmbWebCamDiv.SelectedItem = devName;
            }
            else
            {
                cmbWebCamDiv.SelectedIndex = 0;
            }

        }

        public string Webcamname
        {
            set
            {
                if (cmbWebCamDiv.Items.Contains(value))
                {
                    cmbWebCamDiv.SelectedItem = value;
                }
                else
                {
                    cmbWebCamDiv.SelectedIndex = 0;
                }
            }
            get
            {
                return cmbWebCamDiv.Text;
            }
        }
    }
}
