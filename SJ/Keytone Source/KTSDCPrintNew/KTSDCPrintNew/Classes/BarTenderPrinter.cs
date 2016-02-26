using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seagull.BarTender.Print;
using Seagull.BarTender.Print.Database;
using System.Management;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
//using KTone.RFIDGlobal.KTLogger;

using NLog;


namespace KTone.Core.KTPrinterApp
{
    public class BARTENDERPrinter : PrintWrapper
    {
        #region Attributes

        private readonly Logger log = KTone.RFIDGlobal.KTLogger.KTLogManager.GetLogger();
        private int btProcessId = -1;
        private int timeOut = 1000;
        #endregion Attributes

        [DllImport("msi.dll", CharSet = CharSet.Unicode)]
        static extern Int32 MsiGetProductInfo(string product, string property,
            [Out] StringBuilder valueBuf, ref Int32 len);

        [DllImport("msi.dll", SetLastError = true)]
        static extern int MsiEnumProducts(int iProductIndex,
            StringBuilder lpProductBuf);

        public override bool Print(Printer printer, int timeOut, out string PrinterMessage)
        {
            PrinterMessage = string.Empty;
            
            try
            {
                // AppSettings.m_Log.Trace("Entering BarTenderPrinter:Print()");

                bool isEnginePresent = false;

                StringBuilder sbProductCode = new StringBuilder(39);
                int iIdx = 0;
                while (
                    0 == MsiEnumProducts(iIdx++, sbProductCode))
                {
                    Int32 productNameLen = 512;
                    StringBuilder sbProductName = new StringBuilder(productNameLen);

                    MsiGetProductInfo(sbProductCode.ToString(),
                        "ProductName", sbProductName, ref productNameLen);
                    if (sbProductName.ToString().Contains("BarTender"))
                    {
                        isEnginePresent = true;
                    }                   
                }
                

                if (isEnginePresent)
                {

                    this.timeOut = timeOut;
                    bool isPrinterAvail = checkPrinter(printer.PrinterName, out PrinterMessage);
                    if (isPrinterAvail)
                    {

                        Engine btEngine = new Engine();

                        btEngine.Start();
                        btEngine.Window.Visible = false;

                        LabelFormatDocument format = btEngine.Documents.Open(printer.LabelFile);

                        ((TextFile)format.DatabaseConnections[0]).FileName = printer.InputFile;

                        
                        format.PrintSetup.PrinterName = printer.PrinterName;
                        format.PrintSetup.UseDatabase = true;
                        format.PrintSetup.IdenticalCopiesOfLabel = 1;
                        format.PrintSetup.RecordRange = "1...";

                        Seagull.BarTender.Print.Messages strMsg;
                       
                        Result result = format.Print(Process.GetCurrentProcess().ProcessName,60000, out strMsg);
                        //Result result = format.Print();
                        format.Close(SaveOptions.SaveChanges);
                        btEngine.Stop(SaveOptions.SaveChanges);

                        if (result == Result.Success)
                            return true;
                        else
                        {
                            
                            string strErr = "";
                            for (int i = 0; i < strMsg.Count; i++)
                            {
                                strErr += strMsg[i].Text + " ";
                            }
                            if(strErr.Contains("Maestro Service"))
                                return true;
                            AppSettings.m_Log.Error("Error at BarTenderPrinter:Print(): " + strErr);
                            PrinterMessage = strErr;
                            return false;
                
                        }
                    }
                    else
                    {
                        if (PrinterMessage.Equals(string.Empty))
                        {
                            PrinterMessage = "Printer not available";
                        }
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                AppSettings.m_Log.ErrorException("Error at BarTenderPrinter:Print(): " + ex.Message, ex);               
                return false;
                //throw ex;
            }
            finally
            {
                AppSettings.m_Log.Trace("Leaving BarTenderPrinter:Print()");
            }
        }

        public override bool Verify(out string ErrMsg)
        {
            try
            {
                ErrMsg = "";

                Engine btEngine = new Engine(true);
                // BarTender.ApplicationClass btApp = null;

                List<int> existingBTProcessIdsBefore = GetExistingBTProcessIds();

                //btApp = new BarTender.ApplicationClass();
                string expectedVersion = "9.40";
                log.Trace("Current version : " + btEngine.Version);
                log.Trace("Expected version : " + expectedVersion);
                if (btEngine.Version != expectedVersion)
                {
                    bool versionValid = false;
                    try
                    {
                        float expectedVersionVal = Convert.ToSingle(expectedVersion);
                        float currentVersionVal = Convert.ToSingle(btEngine.Version);
                        if (expectedVersionVal <= currentVersionVal)
                            versionValid = true;
                    }
                    catch { }
                    if (!versionValid)
                        throw new Exception("Please check the version of BarTender software "
                            + "installed on your machine.\r\n"
                            + "Current version\t: " + btEngine.Version + "\r\n"
                            + "Expected version\t: " + expectedVersion);
                }

                //Setting the BarTender Application Visible 
                btEngine.Window.Visible = false;

                List<int> existingBTProcessIdsAfter = GetExistingBTProcessIds();

                SetBTProcessId(existingBTProcessIdsBefore, existingBTProcessIdsAfter);
                btEngine.Stop(SaveOptions.SaveChanges);
                return true;

            }
            catch (Exception ex)
            {
                string errMsg = "Please make sure that BarTender software(Version 9.40 or more) is installed on your machine.";
                log.Error(errMsg, ex);
                ErrMsg = errMsg;
                throw new Exception(errMsg);
            }
            finally
            {
                log.Trace("Leaving ");
            }
        }

        private List<int> GetExistingBTProcessIds()
        {
            List<int> existingBTProcessIds = new List<int>();
            #region Get ids of existing BT processes
            try
            {
                Process[] existingBTProcesses = Process.GetProcessesByName("bartend");
                if (existingBTProcesses != null)
                {
                    foreach (Process pr in existingBTProcesses)
                        existingBTProcessIds.Add(pr.Id);

                }
            }
            catch (Exception ex)
            {
                log.ErrorException("Failed to get BarTender process details.", ex);
            }
            #endregion Get ids of existing BT processes
            return existingBTProcessIds;
        }

        private void SetBTProcessId(List<int> existingBTProcessIdsBefore, List<int> existingBTProcessIdsAfter)
        {
            try
            {
                foreach (int processId in existingBTProcessIdsAfter)
                {
                    if (!existingBTProcessIdsBefore.Contains(processId))
                    {
                        this.btProcessId = processId;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                log.ErrorException("Failed to get BarTender process details.", ex);
            }
        }

        public override void KillPrintProcess()
        {
            try
            {
                if (btProcessId != -1)
                {
                    Process[] existingBTProcesses = Process.GetProcessesByName("bartend");
                    if (existingBTProcesses != null)
                    {
                        foreach (Process pr in existingBTProcesses)
                            pr.Kill();

                    }
                }
            }
            catch (Exception ex)
            {
                log.ErrorException("Failed to kill BarTender process.", ex);
            }
        }

        private bool checkPrinter(string printerName, out string message)
        {
            message = string.Empty;

            try
            {
                // AppSettings.m_Log.Trace("Entering BarTenderPrinter:CheckPrinter()");

                ManagementObjectSearcher moSearch = new ManagementObjectSearcher("Select * from Win32_Printer");
                ManagementObjectCollection moReturn = moSearch.Get();


                foreach (ManagementObject item in moReturn)
                {
                    string Pname = item["Name"].ToString().Trim();
                    string[] Pnames = Pname.Split('\\');
                    Pname = Pnames[Pnames.Length - 1];
                    if (Pname.Equals(printerName))
                    {
                        #region Commanet
                        //ArrayList alParam = new ArrayList();
                        //foreach (PropertyData item1 in item.Properties)
                        //{
                        //    alParam.Add(item1.Name.ToString());
                        //}
                        #endregion

                        if (item["WorkOffline"].ToString().Equals("True"))
                        {
                            message = "Printer is Offline.";
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                AppSettings.m_Log.ErrorException("Error in BarTenderPrinter:CheckPrinter : " + ex.Message, ex);
                throw ex;
            }
            finally
            {
                AppSettings.m_Log.Trace("Leaving BarTenderPrinter:CheckPrinter()");
            }
            return false;
        }
    }
}
