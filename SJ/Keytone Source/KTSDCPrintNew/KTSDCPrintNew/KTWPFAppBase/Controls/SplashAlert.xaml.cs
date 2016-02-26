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
using System.Windows.Threading;
using System.Timers;
using KTWPFAppBase;

namespace KTWPFAppBase.Controls
{
    /// <summary>
    /// It will splash the provided message for configured time (in seconds) duration.
    /// </summary>
    public partial class SplashAlert : Window
    {
        #region Attibutes

        private int disposeFormTimer = 0;
        private  string _splashMessage ;
        private Int32 _splashInterval;

        #endregion

        #region Properties

        public string SplashMessage
        {
            set
            {
                _splashMessage = value;
            }
        }

        public Int32 SplashInterval
        {
            set
            {
                if ((Int32)value <= 0)
                    _splashInterval = 2;
                else
                    _splashInterval = value;
            }
        }

        #endregion

        public SplashAlert()
        {
            InitializeComponent();
        }

        private void SplashAlert_Load(object sender, EventArgs e)
        {
            BaseAppSettings.m_Log.Trace("Entering....");
            try
            {
                disposeFormTimer = _splashInterval + 1;
                lblMessage.Content = _splashMessage;

                System.Windows.Forms.Timer msgTimer = new System.Windows.Forms.Timer();
                msgTimer.Interval = 1000;
                msgTimer.Enabled = true;
                msgTimer.Start();
                msgTimer.Tick += new System.EventHandler(this.timer_tick);
            }
            catch (Exception ex)
            {
                BaseAppSettings.m_Log.Trace("Error Occured:" + ex.Message);
            }
            finally
            {
                BaseAppSettings.m_Log.Trace("Leaving....");
            }
        }

        private void timer_tick(object sender, EventArgs e)
        {
            BaseAppSettings.m_Log.Trace("Entering....");
            try
            {
                disposeFormTimer--;

                if (disposeFormTimer <= 0)
                    this.Close();
            }
            catch (Exception ex)
            {
                BaseAppSettings.m_Log.Trace("Error Occured:" + ex.Message);
            }
            finally
            {
                BaseAppSettings.m_Log.Trace("Leaving....");
            }
        }
    }
}
