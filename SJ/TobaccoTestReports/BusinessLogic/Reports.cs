using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Configuration;
using DataLogic;
using Microsoft.Office.Interop.Excel;
using NLog;
using DataTable = System.Data.DataTable;


namespace BusinessLogic
{
    public class Reports
    {
        readonly DataLogic.DataLogic _dl = new DataLogic.DataLogic();


        static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        private static extern int GetWindowThreadProcessId(IntPtr hwnd, out int pid);

        /*
         
         1) Check for the specified Folder, If doesnt Exist, Create the Folder
         2) COnver the xls to Xlsx
         */

        public void FolderProcessor(string sourceFolderPath, string processedFolderPath, string failedFolderPath, string importFailedPath, string fileConversionPath)
        {

            Logger.Trace(message:
                string.Format("Entering FolderProcessor with paramerter {0},{1},{2},{3}", sourceFolderPath,
                    processedFolderPath, importFailedPath, fileConversionPath));
            if (sourceFolderPath == null) throw new ArgumentNullException("sourceFolderPath");
            if (processedFolderPath == null) throw new ArgumentNullException("processedFolderPath");
            if (failedFolderPath == null) throw new ArgumentNullException("failedFolderPath");
            if (importFailedPath == null) throw new ArgumentNullException("importFailedPath");
            if (fileConversionPath == null) throw new ArgumentNullException("fileConversionPath");

            if (!Directory.Exists(sourceFolderPath))
            {
                Directory.CreateDirectory(sourceFolderPath);
            }

            if (!Directory.Exists(processedFolderPath))
            {
                Directory.CreateDirectory(processedFolderPath);
            }

            if (!Directory.Exists(failedFolderPath))
            {
                Directory.CreateDirectory(failedFolderPath);
            }

            if (!Directory.Exists(importFailedPath))
            {
                Directory.CreateDirectory(importFailedPath);
            }

            if (!Directory.Exists(fileConversionPath))
            {
                Directory.CreateDirectory(fileConversionPath);
            }


            Logger.Trace("Folder Checked Successful and starting Excel Converstion");
            var fileEntries = Directory.GetFiles(sourceFolderPath, "*.xls");
            foreach (var fileName in fileEntries)
            {
                string extension = Path.GetExtension(fileName);
                if (extension == ".xls")
                {
                    string convertToDataTable = ConvertToDataTable(fileName);
                    if (convertToDataTable == "")
                    {
                        Logger.Trace("Excel Convertion Failed");
                        MoveFileWithReplace(failedFolderPath, fileName);
                    }
                    else
                    {
                        Logger.Trace("Excel Convertion Successful");
                        MoveFileWithReplace(fileConversionPath, fileName);
                    }
                }
            }

            fileEntries = Directory.GetFiles(sourceFolderPath, "*.xls");
            foreach (var fileName in fileEntries)
            {
                Logger.Trace(fileName + " - Started");
                var importToGrid = Import_To_Grid(fileName, Path.GetExtension(fileName), "Yes", Path.GetFileName(fileName));

                if (importToGrid > 0)
                {
                    MoveFileWithReplace(processedFolderPath, fileName);
                    Logger.Trace("Processing Sucessful");
                }
                if (importToGrid == 0)
                {
                    MoveFileWithReplace(failedFolderPath, fileName);
                    Logger.Trace("Processing Failed");
                }

                if (importToGrid == -1)
                {
                    MoveFileWithReplace(importFailedPath, fileName);
                    Logger.Trace("Import Failed, unmatch source and destination");
                }

            }
        }

        private static void MoveFileWithReplace(string folderPath, string fileName)
        {
            try
            {
                if (!File.Exists(folderPath + "\\" + Path.GetFileName(fileName)))
                {
                    Logger.Trace("File moved");
                    File.Move(fileName, folderPath + "\\" + Path.GetFileName(fileName));
                }
                else
                {
                    Logger.Trace("Filename already exist - Moving with different Name");
                    File.Move(fileName, folderPath + "\\" + DateTime.Now.ToString("yy_MM_dd_hh_mm_ss_fff") + "_" + Path.GetFileName(fileName));
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                File.Delete(folderPath + "\\" + Path.GetFileName(fileName));
               
            }
         
        }

        private int Import_To_Grid(string filePath, string extension, string isHdr, string fileName)
        {
            Logger.Trace("Import_To_Grid " + fileName);
            int returnValue = 0;
            string conStr = "";
            conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]
                           .ConnectionString;
            conStr = String.Format(conStr, filePath, isHdr);
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();

            Logger.Trace("Connection Opened " + fileName);
            DataTable dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            
            Debug.Assert(dtExcelSchema != null, "dtExcelSchema != null");
            var name =
                dtExcelSchema.AsEnumerable()
                    .Where(d1 => (d1.Field<string>("TABLE_NAME").Contains("$")))
                    .Where(d1 => (!d1.Field<string>("TABLE_NAME").Contains("#")));
            DataTable dtn = name.AsDataView().ToTable();
            
            string sheetName = dtn.Rows[0]["TABLE_NAME"].ToString();
            Logger.Trace(string.Format("Got SheetNames - Processing [{0}]", sheetName));
            connExcel.Close();

            Logger.Trace("Closing Connection");
            //Read Data from First Sheet
            GetValue(connExcel, cmdExcel, sheetName, oda, dt);
            returnValue = ProcessData(dt, fileName, sheetName);
            //Bind Data to GridView

            bool checkCredible = CheckCredibility(dt, returnValue.ToString(CultureInfo.InvariantCulture));
            if (!checkCredible)
            {
                Logger.Trace("Deleting Un Match Value");
                _dl.DeleteDirtyData(returnValue.ToString(CultureInfo.InvariantCulture));
                returnValue = -1;
            }
            return returnValue;
        }

        private static void GetValue(OleDbConnection connExcel, OleDbCommand cmdExcel, string sheetName, OleDbDataAdapter oda,
            DataTable dt)
        {
            Logger.Trace("GetValue " + sheetName);
            try
            {
                if (connExcel != null)
                {
                    connExcel.Open();
                    cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                    oda.SelectCommand = cmdExcel;
                    oda.Fill(dt);
                    connExcel.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
            }
            finally
            {
                if (connExcel != null) connExcel.Close();
            }

        }

        private static string ConvertToDataTable(string path)
        {
            Logger.Trace("Starting ConvertToDataTable");
            System.Data.DataTable dt = null;
            int id;
            Application app = new Application();
            try
            {
                string appStartTime = DateTime.Now.ToString();
               
                app.DisplayAlerts = false;
               
                Workbook workBook = app.Workbooks.Open(path, Type.Missing,
                   Type.Missing, Type.Missing, Type.Missing,
                   Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                   Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                   Type.Missing, Type.Missing);
                workBook.SaveAs(path + "x", XlFileFormat.xlOpenXMLWorkbook, Missing.Value,
                Missing.Value, false, false, XlSaveAsAccessMode.xlNoChange,
                XlSaveConflictResolution.xlLocalSessionChanges, true,
                Missing.Value, Missing.Value, Missing.Value);
                workBook.Close(0);
                app.Workbooks.Close();
                app.Quit();
              
                return path + "x";
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                return "";
            }
            finally
                {  int hWnd = app.Application.Hwnd;
                uint processID; GetWindowThreadProcessId((IntPtr)hWnd, out id);
                Process process = Process.GetProcessById(id);
                process.Kill();}

        }

        private static void Nar(object o)
        {
            try
            {
                while (System.Runtime.InteropServices.Marshal.ReleaseComObject(o) > 0) ;
            }
            catch(Exception ex) { Logger.ErrorException(ex.Message, ex); }
            finally
            {
                o = null;
            }
        }

        private int ProcessData(DataTable dt, string fileName, string sheetName)
        {

            Logger.Trace("In Process Data");
            if (dt == null) throw new ArgumentNullException("dt");
            if (fileName == null) throw new ArgumentNullException("fileName");
            if (sheetName == null) throw new ArgumentNullException("sheetName");

            int masterSno = _dl.AddMasterData(dt, fileName, sheetName);
            bool flag = false;
            int monitorCount = 0;
            if (masterSno == 0) return masterSno;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    if (i > 3) //if cross column number 4
                    {

                        if (dt.Rows[i]["F2"].ToString() == "MONITOR")
                        {
                            monitorCount++;
                            flag = false;
                        }
                        else
                        {
                            if (monitorCount > 0)
                            {
                                flag = true;

                            }
                        }
                        
                        


                        if (!flag)
                        {
                            _dl.InsertSheetData(dt.Rows[i], masterSno, i + 1, "0", "");
                        }
                        else
                        {
                            string toBeProcessed = "";
                            string productClass;
                            if (dt.Rows[i]["F2"].ToString().IndexOf(" ", System.StringComparison.Ordinal) != -1)
                            {

                                //productClass = dt.Rows[i]["F2"].ToString().Substring(0, dt.Rows[i][columnName: "F2"].ToString().IndexOf(" ", System.StringComparison.Ordinal));
                                productClass = dt.Rows[i]["F2"].ToString().Substring(0, dt.Rows[i][columnName: "F2"].ToString().LastIndexOf(" ", System.StringComparison.Ordinal));
                                toBeProcessed = "1";
                            }
                            else
                            {
                                productClass = "";
                                toBeProcessed = "0";
                            }

                            _dl.InsertSheetData(dt.Rows[i], masterSno, i + 1, toBeProcessed, productClass);
                        }


                    }
                }
                catch (Exception ex)
                {
                    Logger.ErrorException(ex.Message, ex);
                }
            }
            return masterSno;
        }
  
        private bool CheckCredibility(DataTable dt, string masterSno)
        {
            Logger.Trace(string.Format("Checking credibility for SNO# {0}", masterSno));
            bool checkerFlag = true;
            try
            {
                DataTable fromDatabase = _dl.GetSheetDetails(masterSno);
                if (fromDatabase == null) throw new ArgumentNullException("fromDatabase");
                dt.Rows.RemoveAt(0);
                dt.Rows.RemoveAt(0);
                dt.Rows.RemoveAt(0);
                dt.Rows.RemoveAt(0);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        if (dt.Rows[i][j].ToString() == fromDatabase.Rows[i][j].ToString())
                        {
                        }
                        else
                        {
                            if (!(dt.Rows[i][j].ToString() == "" && fromDatabase.Rows[i][j].ToString() == "0"))
                            {
                                checkerFlag = false;
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                checkerFlag = false;
            }
            return checkerFlag;
        }

        public DataTable GetGetTobbaccoClass()
        {
            return _dl.GetTobbaccoClass();
        }

        public DataTable GetTobbaccoClassByLatest()
        {
            return _dl.GetTobbaccoClassByLatest();
        }

        private DataTable GetTobbaccoClassDetails(string className)
        {
           return _dl.GetTobbaccoClassDetails(tobbaaccoClass: className);
        }

        public List<TobbaccoValues> TobbaccoDetails(string className)
        {
            var dataTable = GetTobbaccoClassDetails(className);
            var tobbaccoValues = new List<TobbaccoValues>();
            if (dataTable != null)
            {
                tobbaccoValues = dataTable.AsEnumerable().Select(
                    datarow => new TobbaccoValues
                    {
                        AvgNicotinePercentage =(float)datarow.Field<double>("AVGNicotinePercentage"),
                        AvgRSugarPercentage =  (float)datarow.Field<double>("AVGRSugarPercentage"),
                        AvgTSugarPercentage = (float)datarow.Field<double>("AVGTSugarPercentage"),
                        ProductClass = datarow.Field<string>("ProductClass"),
                        Sno = datarow.Field<int>("Sno"),
                        Filename = datarow.Field<string>("FileName"),
                        Date = datarow.Field<DateTime>("Date"),
                        ImportedTime = datarow.Field<DateTime>("ImportedTime"),

                    }).ToList();
            }

            return tobbaccoValues;

        }

        public TbcAnalytics TbcAnalytics(List<TobbaccoValues> tobbaccoValueses)
        {
            var tbcAnalytics = new TbcAnalytics();

            if (tobbaccoValueses.Count>0)
            {
                try
                {

                    tbcAnalytics.TotalAverageNicotine =
                        tobbaccoValueses.AsEnumerable().Average(values => values.AvgNicotinePercentage);
                    tbcAnalytics.TotalAverageTSugar =
                        tobbaccoValueses.AsEnumerable().Average(values => values.AvgTSugarPercentage);
                    tbcAnalytics.TotalAverageRSugar =
                        tobbaccoValueses.AsEnumerable().Average(values => values.AvgRSugarPercentage);


                    var avgNic = tobbaccoValueses.AsEnumerable()
                        .OrderByDescending(time => time.Date)
                        .FirstOrDefault();
                    if (avgNic != null)

                    {
                        tbcAnalytics.LatestNicotine = avgNic.AvgNicotinePercentage;
                        tbcAnalytics.DateLatestNicotine = avgNic.Date;
                    }

                    var avgRsugar = tobbaccoValueses.AsEnumerable()
                        .OrderByDescending(time => time.Date)
                        .FirstOrDefault();
                    if (avgRsugar != null)
                    {
                        tbcAnalytics.LatestRSugar = avgRsugar.AvgRSugarPercentage;
                        tbcAnalytics.DateLatestRSugar = avgRsugar.Date;
                    }


                    var avgTsugar = tobbaccoValueses.AsEnumerable()
                        .OrderByDescending(time => time.Date)
                        .FirstOrDefault();
                    if (avgTsugar != null)
                    {
                        tbcAnalytics.LatestTSugar = avgTsugar.AvgTSugarPercentage;
                        tbcAnalytics.DateLatestTSugar = avgTsugar.Date;
                    }


                    ///////////////////////////////////

                    var avgNicH = tobbaccoValueses.AsEnumerable()
                        .OrderByDescending(values => values.AvgNicotinePercentage)
                        .FirstOrDefault();
                    if (avgNicH != null)
                    {
                        tbcAnalytics.HighestNicotine = avgNicH.AvgNicotinePercentage;
                        tbcAnalytics.DateHighestNicotine = avgNicH.Date;
                    }


                    var avgTSugH = tobbaccoValueses.AsEnumerable()
                        .OrderByDescending(values => values.AvgTSugarPercentage)
                        .FirstOrDefault();
                    if (avgTSugH != null)
                    {
                        tbcAnalytics.HighestTSugar = avgTSugH.AvgTSugarPercentage;
                        tbcAnalytics.DateHighestTSugar = avgTSugH.Date;
                    }


                    var avgRSugH = tobbaccoValueses.AsEnumerable()
                        .OrderByDescending(values => values.AvgRSugarPercentage)
                        .FirstOrDefault();
                    if (avgRSugH != null)
                    {
                        tbcAnalytics.HighestRSugar = avgRSugH.AvgRSugarPercentage;
                        tbcAnalytics.DateHighestRSugar = avgRSugH.Date;
                    }

                    //////////////////////////////////////////////////////////
                    var avgNicL = tobbaccoValueses.AsEnumerable()
                        .OrderBy(values => values.AvgNicotinePercentage)
                        .FirstOrDefault();
                    if (avgNicL != null)
                    {
                        tbcAnalytics.LowestNicotine = avgNicL.AvgNicotinePercentage;
                        tbcAnalytics.DateLowestNicotine = avgNicL.Date;
                    }


                    var avgTSugL = tobbaccoValueses.AsEnumerable()
                        .OrderBy(values => values.AvgTSugarPercentage)
                        .FirstOrDefault();
                    if (avgTSugL != null)
                    {
                        tbcAnalytics.LowestTSugar = avgTSugL.AvgTSugarPercentage;
                        tbcAnalytics.DateLowestTSugar = avgTSugL.Date;
                    }


                    var avgRSugL = tobbaccoValueses.AsEnumerable()
                        .OrderBy(values => values.AvgRSugarPercentage)
                        .FirstOrDefault();
                    if (avgRSugL != null)
                    {
                        tbcAnalytics.LowestRSugar = avgRSugL.AvgRSugarPercentage;
                        tbcAnalytics.DateLowestRSugar = avgRSugL.Date;
                    }




                }
                catch (Exception exception)
                {

                    throw;
                }
            }
            return tbcAnalytics;
        }


      
    }
}
