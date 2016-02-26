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

namespace KTWPFAppBase.Controls
{
    /// <summary>
    /// Interaction logic for CustomMessageBox.xaml
    /// </summary>

    public partial class CustomMessageBox : Window
    {
        private string _caption = "";
        private string _messageBoxText = "";
        private MessageBoxResult _result = MessageBoxResult.None;
        private MessageBoxButton _btnEnum = MessageBoxButton.OK;
        private bool _showScroll = false;
    
        #region Constructors
        public CustomMessageBox()
        {
            InitializeComponent();

        }
        #endregion

        #region Properties
        public string Caption
        {
            get
            {
                return _caption;
            }
            set
            {
                _caption = value;
            }
        }

        public string MessageBoxText
        {
            get
            {
                return _messageBoxText;
            }
            set
            {
                _messageBoxText = value;
            }
        }

        public MessageBoxResult MessageBoxResult
        {
            get
            {
                return this._result;
            }
            private set
            {
                this._result = value;
                if (MessageBoxResult.Cancel == this._result)
                {
                    this.DialogResult = false;
                }
                else
                {
                    this.DialogResult = true;
                }
            }
        }

        public bool ShowScroll
        {
            get
            {
                return _showScroll;
            }
            set
            {
                _showScroll = value;
            }
        }
        
        #endregion


        #region Methods

        public static MessageBoxResult Show(string messageBoxText)
        {
            return Show(null, messageBoxText, "", MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.None, MessageBoxOptions.None);
        }
        public static MessageBoxResult Show(string messageBoxText, string caption)
        {
            return Show(null, messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.None, MessageBoxOptions.None);
        }
        public static MessageBoxResult Show(Window owner, string messageBoxText)
        {
            return Show(owner, messageBoxText, "", MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.None, MessageBoxOptions.None);
        }
        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button)
        {
            return Show(null, messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.None, MessageBoxOptions.None);
        }
        public static MessageBoxResult Show(Window owner, string messageBoxText, string caption)
        {
            return Show(owner, messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.None, MessageBoxOptions.None);
        }
        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            return Show(null, messageBoxText, caption, button, icon, MessageBoxResult.None, MessageBoxOptions.None);
        }
        public static MessageBoxResult Show(string messageBoxText, string caption, int boxHeight, bool isScrollable, MessageBoxButton button, MessageBoxImage icon)
        {
            return Show(null, messageBoxText, caption, button, icon, MessageBoxResult.None, MessageBoxOptions.None, boxHeight, 0, 0, isScrollable);
        }
        public static MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button)
        {
            return Show(owner, messageBoxText, caption, button, MessageBoxImage.None, MessageBoxResult.None, MessageBoxOptions.None);
        }
        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult)
        {
            return Show(null, messageBoxText, caption, button, icon, defaultResult, MessageBoxOptions.None);
        }
        public static MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            return Show(owner, messageBoxText, caption, button, icon, MessageBoxResult.None, MessageBoxOptions.None);
        }
        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult, MessageBoxOptions options)
        {
            return Show(null, messageBoxText, caption, button, icon, defaultResult, options);
        }
        public static MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult)
        {
            return Show(owner, messageBoxText, caption, button, icon, defaultResult, MessageBoxOptions.None);
        }
        public static MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult, MessageBoxOptions options)
        {
            return Show(owner, messageBoxText, caption, button, icon, defaultResult, options,0,0,0,false);
        }

        public static MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult, 
            MessageBoxOptions options, double boxHeight, double boxWidth, double boxFontSize,bool ShowScroll)
        {
            CustomMessageBox messageBox = new CustomMessageBox();
            messageBox.Title = caption;
            messageBox.Owner = owner;
            messageBox._btnEnum = button;
            messageBox._result = defaultResult;
            messageBox.ShowScroll = ShowScroll;
            // messageBox.img.Source = System.Windows.Media.Imaging.;
            messageBox.txtMessage.Text = messageBoxText;

            //display Msg Scroll bar.

            if (ShowScroll)
               messageBox.scrlViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            else
                messageBox.scrlViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            
            switch (button)
            {
                case MessageBoxButton.OK:
                    messageBox.btnOK.Visibility = Visibility.Visible;
                    messageBox.btnOK.Content = "OK";
                    break;
                case MessageBoxButton.OKCancel:
                    messageBox.btnOK.Visibility = Visibility.Visible;
                    messageBox.btnOK.Content = "OK";
                    messageBox.btnCancel.Visibility = Visibility.Visible;
                    messageBox.btnCancel.Content = "Cancel";
                    break;
                case MessageBoxButton.YesNo:
                    messageBox.btnOK.Visibility = Visibility.Visible;
                    messageBox.btnOK.Content = "Yes";
                    messageBox.btnCancel.Visibility = Visibility.Visible;
                    messageBox.btnCancel.Content = "No";
                    break;
                case MessageBoxButton.YesNoCancel:
                    messageBox.btnOK.Visibility = Visibility.Visible;
                    messageBox.btnOK.Content = "Yes";
                    messageBox.btnCancel.Visibility = Visibility.Visible;
                    messageBox.btnCancel.Content = "No";
                    messageBox.btnExtra.Visibility = Visibility.Visible;
                    messageBox.btnExtra.Content = "Cancel";
                    break;

            }

            messageBox.btnOK.Focus();

            IconBitmapDecoder decoder = null;
            System.IO.MemoryStream mem = new System.IO.MemoryStream();
            switch (icon.ToString())
            {
                case "Asterisk":
                    System.Drawing.SystemIcons.Asterisk.Save(mem);
                    break;
                case "Error":
                    System.Drawing.SystemIcons.Error.Save(mem);
                    break;
                case "Exclamation":
                    System.Drawing.SystemIcons.Exclamation.Save(mem);
                    break;
                case "Hand":
                    System.Drawing.SystemIcons.Hand.Save(mem);
                    break;
                case "Information":
                    System.Drawing.SystemIcons.Information.Save(mem);
                    break;
                case "None":
                    messageBox.img.Source = null;
                    break;
                case "Question":
                    System.Drawing.SystemIcons.Question.Save(mem);
                    break;
                case "Stop":
                    System.Drawing.SystemIcons.Shield.Save(mem);
                    break;
                case "Warning":
                    System.Drawing.SystemIcons.Warning.Save(mem);
                    break;
            }
            if (mem.Length > 0)
            {
                decoder = new IconBitmapDecoder(mem, BitmapCreateOptions.IgnoreImageCache, BitmapCacheOption.Default);
                messageBox.img.Source = decoder.Frames[0];
            }
            if (boxHeight > 150)
                messageBox.Height = boxHeight;
            if (boxWidth > 300)
                messageBox.Width = boxWidth;
            if (boxFontSize > 10)
            {
                messageBox.FontSize = boxFontSize;
                messageBox.btnOK.FontSize = boxFontSize;
                messageBox.btnCancel.FontSize = boxFontSize;
                messageBox.btnExtra.FontSize = boxFontSize;
                messageBox.txtMessage.FontSize = boxFontSize;
            }
            messageBox.ShowDialog();

            return messageBox._result;
        }

        #endregion

        #region Event Handlers

        private void ok_Click(object sender, RoutedEventArgs e)
        {
            string Header = "CustomMessageBox::ok_Click: ";
            BaseAppSettings.m_Log.Trace(Header + "Entering.. ");
            try
            {
                if (btnOK.Content.ToString().ToUpper() == "YES")
                    _result = MessageBoxResult.Yes;
                else
                    _result = MessageBoxResult.OK;
                this.Close();
            }
            catch (Exception exp)
            {
                BaseAppSettings.m_Log.ErrorException(Header + "Error occured." + exp.Message, exp);
            }
            finally
            {
                BaseAppSettings.m_Log.Trace(Header + "Leaving..");
            }
        }

        private void this_Loaded(object sender, RoutedEventArgs e)
        {
            // this._close = (Button)this.Template.FindName("btnClose", this);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs args)
        {
            string Header = "CustomMessageBox::btnCancel_Click: ";
            BaseAppSettings.m_Log.Trace(Header + "Entering.. ");
            try
            {
                if (btnCancel.Content.ToString().ToUpper() == "NO")
                    _result = MessageBoxResult.No;
                else
                    _result = MessageBoxResult.Cancel;
                this.Close();
            }
            catch (Exception exp)
            {
                BaseAppSettings.m_Log.ErrorException(Header + "Error occured." + exp.Message, exp);
            }
            finally
            {
                BaseAppSettings.m_Log.Trace(Header + "Leaving..");
            }
        }

        private void btnExtra_Click(object sender, RoutedEventArgs args)
        {
            string Header = "CustomMessageBox::btnExtra_Click: ";
            BaseAppSettings.m_Log.Trace(Header + "Entering.. ");
            try
            {
                _result = MessageBoxResult.Cancel;
                this.Close();
            }
            catch (Exception exp)
            {
                BaseAppSettings.m_Log.ErrorException(Header + "Error occured." + exp.Message, exp);
            }
            finally
            {
                BaseAppSettings.m_Log.Trace(Header + "Leaving..");
            }
        }

        #endregion
    }
}
