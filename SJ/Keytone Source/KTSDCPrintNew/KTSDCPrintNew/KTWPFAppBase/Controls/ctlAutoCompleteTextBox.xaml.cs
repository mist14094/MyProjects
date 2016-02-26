using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace KTWPFAppBase.Controls
{
    public delegate void TextValueSet();
    /// <summary>
    /// Interaction logic for AutoCompleteTextBox.xaml
    /// </summary>    
    public partial class AutoCompleteTextBox : Canvas
    {
        #region Members

        private VisualCollection controls;
        private TextBox textBox;
        private ComboBox comboBox;
        private System.Timers.Timer keypressTimer;
        private delegate void TextChangedCallback();

        private bool insertText;
        private int delayTime;
        private int searchIndex;
        private string searchKey;
        private Dictionary<string, List<string>> searchDataSource;
        public event TextValueSet OnTextValueSet;
        #endregion

        #region Constructor

        public AutoCompleteTextBox()
        {
            controls = new VisualCollection(this);
            InitializeComponent();

            searchIndex = 2;        // default threshold to 2 char

            // set up the key press timer
            keypressTimer = new System.Timers.Timer();
            keypressTimer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);

            // set up the text box and the combo box
            comboBox = new ComboBox();
            comboBox.IsSynchronizedWithCurrentItem = true;
            comboBox.SelectionChanged += new SelectionChangedEventHandler(comboBox_SelectionChanged);
            
            textBox = new TextBox();
            textBox.TextChanged += new TextChangedEventHandler(textBox_TextChanged);
            textBox.VerticalContentAlignment = VerticalAlignment.Center;

            textBox.TabIndex = 100;
            comboBox.TabIndex = 101;
            
            controls.Add(comboBox);
            controls.Add(textBox);
        }

        #endregion

        #region Methods

        public string Text
        {
            get
            {
                return textBox.Text;
            }
            set
            {
                insertText = true;
                textBox.Text = value;
            }
        }

        public int DelayTime
        {
            get
            {
                return delayTime;
            }
            set
            {
                delayTime = value;
            }
        }

        public int SearchIndex
        {
            get
            {
                return searchIndex;
            }
            set
            {
                searchIndex = value;
            }
        }

        public string SearchKey
        {
            get
            {
                return searchKey;
            }
            set
            {
                searchKey = value;
            }
        }

        public Dictionary<string, List<string>> DataSource
        {
            get
            {
                return searchDataSource;
            }
            set
            {
                searchDataSource = value;
            }
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string Header = "AutoCompleteTextBox::comboBox_SelectionChanged: ";
            BaseAppSettings.m_Log.Trace(Header + "Entering.. ");
            try
            {
                if (null != comboBox.SelectedItem)
                {
                    insertText = true;
                    ComboBoxItem cbItem = (ComboBoxItem)comboBox.SelectedItem;
                    textBox.Text = cbItem.Content.ToString();
                    if (OnTextValueSet != null)
                        OnTextValueSet();
                }
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

        private void TextChanged()
        {
            string Header = "AutoCompleteTextBox::TextChanged: ";
            BaseAppSettings.m_Log.Trace(Header + "Entering.. ");
            try
            {
                comboBox.Items.Clear();

                if (textBox.Text.Length >= searchIndex)
                {
                    if (searchKey != "")
                    {
                        if (searchDataSource != null && searchDataSource.Count > 0)
                        {
                            if (searchDataSource.ContainsKey(searchKey))
                            {
                                List<string> dataSourceList = searchDataSource[searchKey];
                                foreach (string data in dataSourceList)
                                {
                                    if (data.StartsWith(textBox.Text, StringComparison.CurrentCultureIgnoreCase))
                                    {
                                        ComboBoxItem cbItem = new ComboBoxItem();
                                        cbItem.Content = data;
                                        comboBox.Items.Add(cbItem);
                                    }
                                }

                                comboBox.IsDropDownOpen = comboBox.HasItems;
                            }
                            else
                                comboBox.IsDropDownOpen = false;
                        }
                        else
                            comboBox.IsDropDownOpen = false;
                    }
                    else
                        comboBox.IsDropDownOpen = false;
                }
                else
                {
                    comboBox.IsDropDownOpen = false;
                }
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

        private void OnTimedEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            keypressTimer.Stop();
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                new TextChangedCallback(this.TextChanged));
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string Header = "AutoCompleteTextBox::textBox_TextChanged: ";
            BaseAppSettings.m_Log.Trace(Header + "Entering.. ");
            try
            {
                // text was not typed, do nothing and consume the flag
                if (insertText == true) insertText = false;

                // if the delay time is set, delay handling of text changed
                else
                {
                    if (delayTime > 0)
                    {
                        keypressTimer.Interval = delayTime;
                        keypressTimer.Start();
                    }
                    else TextChanged();
                }
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

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            textBox.Arrange(new Rect(arrangeSize));
            comboBox.Arrange(new Rect(arrangeSize));
            return base.ArrangeOverride(arrangeSize);
        }

        protected override Visual GetVisualChild(int index)
        {
            return controls[index];
        }

        protected override int VisualChildrenCount
        {
            get { return controls.Count; }
        }

        #endregion
    }
}