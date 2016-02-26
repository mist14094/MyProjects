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
using System.Windows.Forms;
using KTWPFAppBase.Classes;

namespace KTWPFAppBase.Controls
{
    /// <summary>
    /// Interaction logic for ctlImageCapture.xaml
    /// </summary>
    public partial class ctlImageCapture : System.Windows.Controls.UserControl
    {
        #region Variables Declaration
        private string m_WebCamName = string.Empty;
        //Code edited by shekhar start
        //private System.Drawing.Image imgCapture = null;
        private BitmapSource imgCapture = null;
        //Code edited by shekhar end
        private bool _mShowOk = true;
        private Device showVideo = null;

        //Code added by shekhar start
        //Declare WebCam
        WebCam webcam;
        //Code added by shekhar end

        public delegate void btnOkDelegate(object sender, RoutedEventArgs args);
        public event btnOkDelegate btnOkEvent = null;
        #endregion

        #region Constructor
        public ctlImageCapture()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            string Header = "ctlImageCapture::UserControl_Loaded: ";
            BaseAppSettings.m_Log.Trace(Header + "Entering.. ");
            try
            {
                
                if (_mShowOk)
                {
                    btnOk.Visibility = Visibility.Visible;
                }
                else
                {
                    btnOk.Visibility = Visibility.Collapsed;
                }

                //Code edited by shekhar start
                //int devIndex = Devicemanager.showIndex(m_WebCamName);

                //showVideo = Devicemanager.GetDevice(devIndex);
                //showVideo.ShowWindow(this.picShowVideo);

                webcam = new WebCam();
                webcam.InitializeWebCam(ref picShowVideo);
                webcam.Start();
                //Code edited by shekhar end
            }
            catch (Exception ex)
            {
                BaseAppSettings.m_Log.ErrorException(Header + "Error occured." + ex.Message, ex);
            }
            finally
            {
                BaseAppSettings.m_Log.Trace(Header + "Leaving..");
            }
        }

        private void btnCapture_Click(object sender, RoutedEventArgs e)
        {
            string Header = "ctlImageCapture::btnCapture_Click: ";
            BaseAppSettings.m_Log.Trace(Header + "Entering.. ");
            try
            {
                this.Cursor = System.Windows.Input.Cursors.Wait;
                System.Windows.Forms.Clipboard.Clear();
                picScanedImage.Source = picShowVideo.Source;

                if (picScanedImage.Source != null)
                {
                    picScanedImage.Stretch = Stretch.Fill;
                    imgCapture = (BitmapSource)picScanedImage.Source;
                }
                else
                {
                    CustomMessageBox.Show("Camera is not responding, Please check the connection, Retry!", "Capture Image", MessageBoxButton.OK, MessageBoxImage.Information);
                    if (btnOkEvent != null)
                        btnOkEvent(sender, e);
                }
                this.Cursor = System.Windows.Input.Cursors.Arrow;
                imgCapture = (BitmapSource)picScanedImage.Source;
            }
            catch (Exception ex)
            {
                BaseAppSettings.m_Log.ErrorException(Header + "Error occured." + ex.Message, ex);
            }
            finally
            {
                BaseAppSettings.m_Log.Trace(Header + "Leaving..");
            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            string Header = "ctlImageCapture::btnOk_Click: ";
            BaseAppSettings.m_Log.Trace(Header + "Entering.. ");
            try
            {
                this.Cursor = System.Windows.Input.Cursors.Wait;
                if (imgCapture == null)
                {
                    CustomMessageBox.Show("  Please capture the image first","Image Capture",MessageBoxButton.OK,MessageBoxImage.Information);
                }
                else
                {
                    if (btnOkEvent != null)
                        btnOkEvent(sender, e);
                }
                this.Cursor = System.Windows.Input.Cursors.Arrow;
            }
            catch (Exception ex)
            {
                BaseAppSettings.m_Log.ErrorException(Header + "Error occured." + ex.Message, ex);
            }
            finally
            {
                BaseAppSettings.m_Log.Trace(Header + "Leaving..");
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            string Header = "ctlImageCapture::UserControl_Unloaded: ";
            BaseAppSettings.m_Log.Trace(Header + "Entering.. ");
            try
            {
                webcam.Stop();
                showVideo.Stop();
            }
            catch (Exception ex)
            {
                BaseAppSettings.m_Log.ErrorException(Header + "Error occured." + ex.Message, ex);
            }
            finally
            {
                BaseAppSettings.m_Log.Trace(Header + "Leaving..");
            }
        }
        #endregion

        #region Properties
        public bool ShowOk
        {
            set
            {
                _mShowOk = value;
            }
        }

        //Code edited by shekhar start 

        public BitmapSource CapturedImage
        {
            get
            {
                return imgCapture;
            }
        }
        //Code edited by shekhar end 
        public string WebcamName
        {
            set
            {
                m_WebCamName = value;
            }
            get 
            {
                return m_WebCamName;
            }
        }

        #endregion
    }
}