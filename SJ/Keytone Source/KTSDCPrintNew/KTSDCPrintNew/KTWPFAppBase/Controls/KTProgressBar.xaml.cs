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
using System.Windows.Media.Animation;

namespace KTWPFAppBase.Controls
{
    /// <summary>
    /// Interaction logic for ProgressStatus.xaml
    /// </summary>
    public partial class KTProgressBar : Window
    {
        #region Attibutes
        private static string _statusMessage;
        delegate void SetStatusMessage();
        private static KTProgressBar _progress = null;

        #endregion

        #region Properties

        public string CurrentStatus
        {
            set
            {
                _statusMessage = value;
                this.Dispatcher.Invoke(new SetStatusMessage(SetMessage));
            }
        }

        #endregion

        private KTProgressBar()
        {
            InitializeComponent();
        }

        public static KTProgressBar GetInstance()
        {
            BaseAppSettings.m_Log.Trace("Entering KTProgressBar::SetMessage ....");
            try
            {
                if (_progress == null)
                {
                    _progress = new KTProgressBar();
                    
                }
                _progress.Show();
            }
            catch (Exception ex)
            {
                BaseAppSettings.m_Log.Trace("Error Occured : KTProgressBar::SetMessage :" + ex.Message);
            }

            return _progress;
        }

        public static KTProgressBar GetInstance(Window win)
        {
            BaseAppSettings.m_Log.Trace("Entering KTProgressBar::SetMessage ....");
            try
            {
                if (_progress == null)
                {
                    _progress = new KTProgressBar();
                  
                }
                _progress.Owner = win;
                _progress.Show();
            }
            catch (Exception ex)
            {
                BaseAppSettings.m_Log.Trace("Error Occured : KTProgressBar::SetMessage :" + ex.Message);
            }

            return _progress;
        }

        private void SetMessage()
        {
            try
            {
                txtMessage.Text = _statusMessage;
            }
            catch (Exception ex)
            { }
        }
        public static void HideInstance()
        {
            if (_progress != null)
                _progress.Close();
           // _progress = null;
        }

        public static void RemoveInstance()
        {
            if (_progress != null)
                _progress.Close();

            _progress = null;
        }

       
    }
}
