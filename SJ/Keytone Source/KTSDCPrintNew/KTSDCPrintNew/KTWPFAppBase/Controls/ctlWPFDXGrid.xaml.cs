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
using DevExpress.Wpf.Grid;
using DevExpress.Utils;
using System.Windows.Markup;

namespace KTWPFAppBase.Controls
{
    /// <summary>
    /// Interaction logic for ctlWPFDXGrid.xaml
    /// </summary>
    public partial class ctlWPFDXGrid : UserControl
    {
        public ctlWPFDXGrid()
        {
            InitializeComponent();
            LoadGridTheme();
        }

        private void LoadGridTheme()
        {
            string Header = "ctlDXGrid::LoadGridTheme: ";
            BaseAppSettings.m_Log.Trace(Header + "Entering.. ");
            try
            {
                view.BestFitColumns();
                view.BestFitMode = DevExpress.Wpf.Core.BestFitMode.Default;
                view.AllowGrouping = true;
                view.AllowHorizontalScrollingVirtualization = true;
                view.AllowMoving = true;
                view.AllowResizing = true;
                view.AllowSorting = true;
                view.AutoWidth = true;
                view.ScrollingMode = ScrollingMode.Smart;
                view.ShowFilterPanelMode = ShowFilterPanelMode.ShowAlways;
                view.ShowGroupPanel = true;
                view.ShowHorizontalLines = true;
                view.ShowIndicator = true;
                view.ShowVerticalLines = true;
                view.ShowAutoFilterRow = true;
            }
            catch (Exception ex)
            {
                BaseAppSettings.m_Log.ErrorException(Header + "Error occured. " + ex.Message, ex);
            }
            finally
            {
                BaseAppSettings.m_Log.Trace(Header + "Leaving..");
            }
        }

        public void AddColumn(string caption, string FieldName, bool visible, int visibleIndex, int colWidth, bool allowEditing)
        {
            
            GridColumn grdColumn = new GridColumn();
            grdColumn.Header = caption;
            grdColumn.FieldName = FieldName;
            grdColumn.Visible = visible;
            
            if (allowEditing)
                grdColumn.AllowEditing = DefaultBoolean.True;
            else
                grdColumn.AllowEditing = DefaultBoolean.False;
            grdColumn.VisibleIndex = visibleIndex;
            if (colWidth != 0)
                grdColumn.Width = colWidth;
            dxGrid.Columns.Add(grdColumn);
        }
        
        #region Property Declaration

        public object DataSource
        {
            set
            {
                dxGrid.DataSource = value;
            }
            get
            {
                return dxGrid.DataSource;
            }
        }

        public bool ShowHorizontalLines
        {
            set
            {
                view.ShowHorizontalLines = value;
            }
        }

        public bool ShowVerticalLines
        {
            set
            {
                view.ShowVerticalLines = value;
            }
        }

        public bool ShowIndicator
        {
            set
            {
                view.ShowIndicator = value;
            }
        }

        public bool ShowGroupPanel
        {
            set
            {
                view.ShowGroupPanel = value;
            }
        }

        public bool AllowGrouping
        {
            set
            {
                view.AllowGrouping = value;
            }
        }

        public bool ShowFilterPanel
        {
            set
            {
                if (value)
                    view.ShowFilterPanelMode = ShowFilterPanelMode.ShowAlways;
                else
                    view.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            }
        }

        public bool ShowAutoFilterRow
        {
            set
            {
                view.ShowAutoFilterRow = value;
            }
        }

        #endregion

    }
}
