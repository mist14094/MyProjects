using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using JarvisDataAccess;
using NLog;

namespace JarvisBusinessAccess
{
    public class DefaultBusinessAccess
    {
        private readonly Logger _nlog = LogManager.GetCurrentClassLogger();
        JarvisDataAccess.DefaultDataAccess _access;
        public DefaultBusinessAccess()
        {
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            _access = new DefaultDataAccess();
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Exit");
        }

        public DataTable GetAllDataBaseConnection()
        {
            return _access.GetAllDataBaseConnection();
        }

        public DataTable GetDataElements(string ReferenceDataBaseName, bool Enabled,string ElementType)
        {
            return _access.GetDataElements(ReferenceDataBaseName, Enabled, ElementType);
        }


        public int GetDataBaseConnectionStringRef(int sno)
        {
            return _access.GetDataBaseConnectionStringRef(sno);
        }

        public DataTable GetSampleData(string DataBaseElementSno)
        {
            DynamicDataAccess dl = new DynamicDataAccess(GetDataBaseConnectionStringRef(int.Parse(DataBaseElementSno)));
            return dl.GetSampleData(GetExecutionScript(DataBaseElementSno));
        }

        public DataTable GetRealData(string DataBaseElementSno)
        {
            DynamicDataAccess dl = new DynamicDataAccess(GetDataBaseConnectionStringRef(int.Parse(DataBaseElementSno)));
            return dl.GetRealData(GetExecutionScript(DataBaseElementSno));
        }


        public string GetExecutionScript(string sno)
        {
            return _access.GetExecutionScript(sno);
        }

        public DataTable GetCharttypes()
        {
            return _access.GetCharttypes();
        }

        public string InsertMainChartConfiguration(string ChartPrimaryHeader, string ChartSecondaryHeader,
            Boolean? AllowMultipleSelection, Boolean? ExportOptionsExporttoImage,
            Boolean? ExportOptionsAllowPrint, Int32? Height, String HeightMode, Boolean? IsInverted, Int32? Width,
            String WidthMode, Int32? ZoomMode, Boolean? AxisMarkersEnabled,
            Int32? AxisMarkersMode, Int32? AxisMarkersWidth, Boolean? TooltipSettingsChartBound, DateTime? ModifiedDate)
        {
            return _access.InsertMainChartConfiguration(ChartPrimaryHeader, ChartSecondaryHeader,
                AllowMultipleSelection, ExportOptionsExporttoImage,
                ExportOptionsAllowPrint, Height, HeightMode, IsInverted, Width,
                WidthMode, ZoomMode, AxisMarkersEnabled,
                AxisMarkersMode, AxisMarkersWidth, TooltipSettingsChartBound, ModifiedDate);
        }

        public DataTable GetChartValues(string sno)
        {
            return _access.GetChartValues(sno);
        }

        public string UpdateMainChartConfiguration(string ChartPrimaryHeader, string ChartSecondaryHeader,
           Boolean? AllowMultipleSelection, Boolean? ExportOptionsExporttoImage,
           Boolean? ExportOptionsAllowPrint, Int32? Height, String HeightMode, Boolean? IsInverted, Int32? Width,
           String WidthMode, Int32? ZoomMode, Boolean? AxisMarkersEnabled,
           Int32? AxisMarkersMode, Int32? AxisMarkersWidth, Boolean? TooltipSettingsChartBound, DateTime? ModifiedDate, string Sno)
        {
            return _access.UpdateMainChartConfiguration(ChartPrimaryHeader, ChartSecondaryHeader,
                AllowMultipleSelection, ExportOptionsExporttoImage,
                ExportOptionsAllowPrint, Height, HeightMode, IsInverted, Width,
                WidthMode, ZoomMode, AxisMarkersEnabled,
                AxisMarkersMode, AxisMarkersWidth, TooltipSettingsChartBound, ModifiedDate,Sno);
        }

        public DataTable GetChartList()
        {
            return _access.GetChartList();
        }

        public DataTable SelectChartTypes()
        {
            return _access.SelectChartTypes().AsEnumerable().Where(row => row["ChartType"].ToString() == "Line").CopyToDataTable();
        }

        public DataTable SelectChartNameDesc()
        {
            return _access.SelectChartNameDesc();
        }

        public string GetDatabaseSnofromElementReference(string sno)
        {
            return _access.GetDatabaseSnofromElementReference(sno);
        }

        public string InsertMainConfigurationComp(string ChartName, string ChartDesc, int ChartTypeRefNo,
            int DataBaseElementSno, bool IsValid, DateTime ModifiedDate,
            string pagelink, string BigIconPath, string SmallIconPath, int SortOrder)
        {
            return _access.InsertMainConfigurationComp(ChartName, ChartDesc, ChartTypeRefNo, DataBaseElementSno, IsValid,
                ModifiedDate, pagelink, BigIconPath, SmallIconPath, SortOrder);
        }
        public string UpdateMainConfigurationComp(string ChartName, string ChartDesc, int ChartTypeRefNo,
            int DataBaseElementSno, bool IsValid, DateTime ModifiedDate,
            string pagelink, string BigIconPath, string SmallIconPath, int SortOrder, string sno)
        {
            return _access.UpdateMainConfigurationComp(ChartName, ChartDesc, ChartTypeRefNo, DataBaseElementSno, IsValid,
                ModifiedDate, pagelink, BigIconPath, SmallIconPath, SortOrder, sno);
        }

        public DataTable GetYAxisLine(string MainChartReferenceChartNo)
        {
            return _access.GetYAxisLine(MainChartReferenceChartNo);
        }

        public DataTable GetXAxisLine(string MainChartReferenceChartNo)
        {
            return _access.GetXAxisLine(MainChartReferenceChartNo);
        }

        public DataTable GetYAxisValueFromCLSSno(string ChartLineSeriesSno)
        {
            return _access.GetYAxisValueFromCLSSno(ChartLineSeriesSno);
        }

        public DataTable GetXAxisValueFromMCABSno(string MainChartAxisBaseSno)
        {
            return _access.GetXAxisValueFromMCABSno(MainChartAxisBaseSno);
        }

        public string InsertMainChartAxisBase(string MainChartReferenceChartNo, int AxisType, int SortOrder,
            decimal TicksRepeat,
            Boolean SwapLocation, string CatagoricalValuesColumnName, int AxisTextAngle, int AxisTextAngleX,
            int AxisTextAngleY,
            int AxisTextAngleStep, string TitleText, Boolean IsValid, DateTime ModifiedDate)

        {

            return _access.InsertMainChartAxisBase(MainChartReferenceChartNo,   AxisType,  SortOrder,
             TicksRepeat, SwapLocation,  CatagoricalValuesColumnName,  AxisTextAngle,  AxisTextAngleX,
             AxisTextAngleY, AxisTextAngleStep,  TitleText,  IsValid,  ModifiedDate);
        }

        public string UpdateMainChartAxisBase( int AxisType, int SortOrder,
           decimal TicksRepeat,
           Boolean SwapLocation, string CatagoricalValuesColumnName, int AxisTextAngle, int AxisTextAngleX,
           int AxisTextAngleY,
           int AxisTextAngleStep, string TitleText, Boolean IsValid, DateTime ModifiedDate, string Sno)
        {

            return _access.UpdateMainChartAxisBase( AxisType, SortOrder,
             TicksRepeat, SwapLocation, CatagoricalValuesColumnName, AxisTextAngle, AxisTextAngleX,
             AxisTextAngleY, AxisTextAngleStep, TitleText, IsValid, ModifiedDate,Sno);
        }

        public string InsertChartLineSeries(int MainChartAxisBaseSno, string DataFieldY, string CollectionAlias,
            Boolean EnablePointSelection, int DrawWidth, int DrawRadius, int StackMode, Boolean IsValid,
            DateTime ModifiedDate)
        {
            return _access.InsertChartLineSeries( MainChartAxisBaseSno,  DataFieldY,  CollectionAlias,
              EnablePointSelection,  DrawWidth,  DrawRadius,  StackMode,  IsValid, ModifiedDate);
        }

        public string UpdateChartLineSeries(string DataFieldY, string CollectionAlias,
            Boolean EnablePointSelection, int DrawWidth, int DrawRadius, int StackMode, Boolean IsValid,
            DateTime ModifiedDate, string Sno)
        {
            return _access.UpdateChartLineSeries(   DataFieldY,  CollectionAlias,
              EnablePointSelection,  DrawWidth,  DrawRadius,  StackMode,  IsValid, ModifiedDate,  Sno);
        }

        public string GetMainChartAxisBaseSnofromSno(string sno)
        {
            return _access.GetMainChartAxisBaseSnofromSno(sno);
        }


    }
}
