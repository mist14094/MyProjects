using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace JarvisReportsConstant
{
    public class GlobalConstants
    {
       public static string EPMString = ConfigurationManager.ConnectionStrings["EPMString"].ConnectionString;
       public static string RFIDString = ConfigurationManager.ConnectionStrings["RFIDString"].ConnectionString;
       public static string SysproString = ConfigurationManager.ConnectionStrings["SysproString"].ConnectionString;
       public static string DefaultString = ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString;

        public string EPMConnection
        {
            get
            {
                return EPMString;
            }
            set
            {
                EPMConnection = value;
            }
        }
        public string RFIDConnection
        {
            get
            {
                return RFIDString;
            }
            set
            {
                RFIDConnection = value;
            }
        }
        public string SysproConnection
        {
            get
            {
                return SysproString;
            }
            set
            {
                SysproConnection = value;
            }
        }
        public string DefaultConnectionString
        {
            get
            {
                return DefaultString;
            }
            set
            {
                DefaultString = value;
            }
        }
        //Stored Procedure
        public string tblReceivingDetails = "sp_GetReceivingDetailsCustomFields";
        //Queries
        public string QRGetViewandProcedureName = "SELECT name + ' - [V]' AS name FROM sys.views UNION SELECT name + ' - [P]' AS name FROM sys.procedures";
        public void GetAllCoonectionString()
        {
            string EPMString1 = ConfigurationManager.ConnectionStrings["EPMString"].ConnectionString;

        }

        ///////////////////////////////
        public string GetAllDataBaseConnection =
            "SELECT [Sno] ,[DataBaseName] ,[ConnectionString] ,[EquivalentString] ,[Enabled] ,[DateCreated]  FROM [Jarvis].[dbo].[DataBaseInstanceNames] where Enabled = 1 ";

        public string GetConnectionString =
            "SELECT [ConnectionString]  FROM [Jarvis].[dbo].[DataBaseInstanceNames] WHERE Sno={0}";

        public string GetDataElements =
            "SELECT  [sno],[ElementName],[ElementAliasName] + ' - [' +ElementType +']' AS [ElementAliasName],[ElementDesc],[ElementType],[Enabled],[CreatedTime],[ExecutionScript],[ReferenceDatabaseName]  FROM [Jarvis].[dbo].[DataBaseElements] WHERE ReferenceDatabaseName={0} AND [Enabled]= '{1}'  AND ElementType IN ({2}) order by ElementAliasName asc";

        public string GetSampleData =
            "SET ROWCOUNT 4 {0}   SET ROWCOUNT 0";

        public string GetExecutionScript =
            "SELECT [ExecutionScript]  FROM [Jarvis].[dbo].[DataBaseElements] WHERE sno={0}";

        public string GetDataBaseConnectionStringRef =
            "SELECT ReferenceDatabaseName FROM [Jarvis].[dbo].[DataBaseElements] WHERE sno={0}";

        public string GetCharttypes =
            "SELECT [Sno],[ChartType],[ChartDesc],[IsEnabled],[CreatedDate] FROM [Jarvis].[dbo].[ChartCategories]";

        public string GetChartValues =
            "SELECT [ChartPrimaryHeader] ,[ChartSecondaryHeader] ,[AllowMultipleSelection] ,[ExportOptionsExporttoImage] ,[ExportOptionsAllowPrint] ,[Height] ,[HeightMode] ,[IsInverted] ,[Width] ,[WidthMode] ,[ZoomMode] ,[AxisMarkersEnabled] ,[AxisMarkersMode] ,[AxisMarkersWidth] ,[TooltipSettingsChartBound] ,[ModifiedDate]  FROM [Jarvis].[dbo].[jrvs_MainChartConfiguration] WHERE sno={0}";

        public string GetAxisConfiguration =
            "SELECT  [sno] ,[MainChartReferenceChartNo] ,[AxisType] ,[SortOrder] ,[TicksRepeat] ,[SwapLocation] ,[CatagoricalValuesColumnName] ,[AxisTextAngle] ,[AxisTextAngleX] ,[AxisTextAngleY] ,[AxisTextAngleStep] ,[TitleText] ,[IsValid] ,[ModifiedDate]  FROM [Jarvis].[dbo].[jrvs_MainChartAxisBase] WHERE axistype={0} AND isvalid = 1 and [MainChartReferenceChartNo] = {1} ORDER BY sortorder asc";

        public string GetRealData=
            "SET ROWCOUNT 1000 {0}   SET ROWCOUNT 0";

        public string GetChartLineSeries =
            "SELECT [sno] ,[MainChartAxisBaseSno] ,[DataFieldY] ,[CollectionAlias] ,[EnablePointSelection] ,[DrawWidth] ,[DrawRadius] ,[StackMode] ,[IsValid] ,[ModifiedDate]  FROM [Jarvis].[dbo].[jrvs_ChartLineSeries] WHERE isvalid=1 AND mainchartaxisbasesno={0}";

        public string InsertMainChartConfiguration =
            "jrvs__InsertMainChartConfiguration";

        public string UpdateMainChartConfiguration =
           "jrvs__UpdateMainChartConfiguration";

        public string GetChartList =
            "SELECT ISNULL(ChartName,'') + ' - ' + ChartType + ' - ' + ElementName AS ChartDesc,  Jarvis.dbo.jrvs_MainConfigurationComp.sno  FROM [Jarvis].[dbo].[jrvs_MainConfigurationComp] INNER JOIN Jarvis.dbo.DataBaseElements ON Jarvis.dbo.jrvs_MainConfigurationComp.DataBaseElementSno = Jarvis.dbo.DataBaseElements.sno INNER JOIN Jarvis.dbo.ChartCategories ON Jarvis.dbo.jrvs_MainConfigurationComp.ChartTypeRefNo = Jarvis.dbo.ChartCategories.Sno";

        public string SelectChartTypes =
            "SELECT  [Sno],[ChartType],[ChartDesc],[LinkPage],[IsEnabled],[CreatedDate]  FROM [Jarvis].[dbo].[ChartCategories] WHERE isEnabled=1";

        public string SelectChartNameDesc =
            "SELECT  [sno],[ChartName],[ChartDesc],[ChartTypeRefNo],[MainChartConfigurationRefNo],[DataBaseElementSno],[IsValid],[ModifiedDate],[pagelink],[BigIconPath],[SmallIconPath],[SortOrder]  FROM [Jarvis].[dbo].[jrvs_MainConfigurationComp]";

        public string GetDatabaseSnofromElementReference =
            "SELECT [ReferenceDatabaseName]  FROM [Jarvis].[dbo].[DataBaseElements] WHERE sno={0}";

        public string jrvs_InsertMainConfigurationComp =
            "jrvs_InsertMainConfigurationComp";

        public string jrvs_UpdateMainConfigurationComp =
            "jrvs_UpdateMainConfigurationComp";

        public string GetXAxisLine =
            "SELECT *   FROM [Jarvis].[dbo].[jrvs_MainChartAxisBase] WHERE AxisType=0 AND MainChartReferenceChartNo={0} order by sortOrder";

        public string GetYAxisLine =
            "SELECT * FROM [Jarvis].[dbo].[jrvs_MainChartAxisBase] INNER JOIN Jarvis.dbo.jrvs_ChartLineSeries  ON Jarvis.dbo.jrvs_MainChartAxisBase.sno = Jarvis.dbo.jrvs_ChartLineSeries.MainChartAxisBaseSno WHERE AxisType=1 AND  MainChartReferenceChartNo={0}   ORDER BY SortOrder ";

        public string GetXAxisValueFromMCABSno =
            "SELECT jrvs_MainChartAxisBase.*,DataBaseElementSno FROM dbo.jrvs_MainChartAxisBase LEFT OUTER JOIN dbo.jrvs_MainChartConfiguration  ON jrvs_MainChartAxisBase.MainChartReferenceChartNo = jrvs_MainChartConfiguration.Sno LEFT OUTER JOIN dbo.jrvs_MainConfigurationComp ON dbo.jrvs_MainConfigurationComp.MainChartConfigurationRefNo =  dbo.jrvs_MainChartConfiguration.sno WHERE jrvs_MainChartAxisBase.sno={0} AND AxisType=0 ";

        public string GetYAxisValueFromCLSSno =
            "SELECT jrvs_MainChartAxisBase.*,jrvs_ChartLineSeries.*,DataBaseElementSno FROM dbo.jrvs_MainChartAxisBase LEFT OUTER JOIN dbo.jrvs_ChartLineSeries ON dbo.jrvs_MainChartAxisBase.sno = dbo.jrvs_ChartLineSeries.MainChartAxisBaseSno LEFT OUTER JOIN dbo.jrvs_MainChartConfiguration  ON jrvs_MainChartAxisBase.MainChartReferenceChartNo = jrvs_MainChartConfiguration.Sno LEFT OUTER JOIN dbo.jrvs_MainConfigurationComp ON dbo.jrvs_MainConfigurationComp.MainChartConfigurationRefNo =  dbo.jrvs_MainChartConfiguration.sno WHERE dbo.jrvs_ChartLineSeries.sno={0} AND AxisType=1";

        public string InsertMainChartAxisBase =
            "jrvs__InsertMainChartAxisBase";

        public string UpdateMainChartAxisBase =
           "jrvs__UpdateMainChartAxisBase";

        public string InsertChartLineSeries =
            "jrvs__InsertChartLineSeries";

        public string UpdateChartLineSeries =
            "jrvs__UpdateChartLineSeries";

        public string GetMainChartConfigurationRefNo =
            "SELECT [MainChartConfigurationRefNo]  FROM [Jarvis].[dbo].[jrvs_MainConfigurationComp] WHERE sno={0}";

        public string GetMainChartAxisBaseSnofromSno =
            "SELECT [MainChartAxisBaseSno]  FROM [Jarvis].[dbo].[jrvs_ChartLineSeries] WHERE sno={0}";
    }


}
