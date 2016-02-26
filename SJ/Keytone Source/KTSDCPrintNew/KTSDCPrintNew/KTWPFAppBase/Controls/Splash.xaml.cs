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
using System.Threading;

namespace KTWPFAppBase.Controls
{
    /// <summary>
    /// Interaction logic for Splash.xaml
    /// </summary>
    public partial class Splash : Window
    {
        Thread mSplashThread = null;
        public delegate void DelegateClear();
        System.Diagnostics.Process prcCurrentProcess = null;
        private string appDisplayName = string.Empty;

        public Splash(string imgPath)
        {
            string Header = "Splash::Splash: ";
            BaseAppSettings.m_Log.Trace(Header + "Entering.. ");
            try
            {
                InitializeComponent();
                try
                {
                    prcCurrentProcess = System.Diagnostics.Process.GetCurrentProcess();
                    this.Title = prcCurrentProcess.ProcessName;

                    string basePath = AppDomain.CurrentDomain.BaseDirectory;
                    if (!basePath.EndsWith(@"\"))
                        basePath += @"\";

                    string imagePath = basePath + imgPath;

                    if (System.IO.File.Exists(imagePath))
                    {
                        this.img.Source = GetBitmapImage(imagePath);
                    }
                }
                catch { }
                this.Loaded += new RoutedEventHandler(SplashScreen_Loaded);
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

        private void SplashScreen_Loaded(object sender, RoutedEventArgs e)
        {
            string Header = "Splash::SplashScreen_Loaded: ";
            BaseAppSettings.m_Log.Trace(Header + "Entering.. ");
            try
            {
                if (appDisplayName != "")
                {
                    Label lbl = new Label();
                    lbl.Content = appDisplayName;
                    Thickness thickness = new Thickness();
                    thickness.Top = 120;
                    thickness.Left = 114;
                    lbl.Height = 175;
                    lbl.Width = 498;
                    lbl.FontSize = 40;
                    lbl.Margin = thickness;
                    lbl.VerticalAlignment = VerticalAlignment.Center;
                    lbl.FontWeight = FontWeights.Bold;
                    lbl.FontFamily = new System.Windows.Media.FontFamily("Microsoft Sans Serif");
                    LayoutRoot.Children.Add(lbl);
                }

                mSplashThread = new Thread(new ThreadStart(ShowSplash));
                mSplashThread.Start();
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

        private void ShowSplash()
        {
            string Header = "Splash::ShowSplash: ";
            BaseAppSettings.m_Log.Trace(Header + "Entering.. ");
            try
            {
                Thread.Sleep(3000);
                this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new DelegateClear(CloseScreen));
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

        private void CloseScreen()
        {
            string Header = "Splash::CloseScreen: ";
            BaseAppSettings.m_Log.Trace(Header + "Entering.. ");
            try
            {
                this.Close();
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

        public static BitmapImage GetBitmapImage(string path)
        {
            string Header = "Splash::GetBitmapImage: ";
            BaseAppSettings.m_Log.Trace(Header + "Entering.. ");
            try
            {
                BitmapImage myBitImage = new BitmapImage();
                myBitImage.BeginInit();
                myBitImage.UriSource = new Uri(path);
                myBitImage.EndInit();

                return myBitImage;
            }
            catch (Exception ex)
            {
                BaseAppSettings.m_Log.ErrorException(Header + "Error occured." + ex.Message, ex);
                throw ex;
            }
            finally
            {
                BaseAppSettings.m_Log.Trace(Header + "Leaving..");
            }
        }

        public string AppDisplayName
        {
            set
            {
                appDisplayName = value;
            }
        }
    }
}
