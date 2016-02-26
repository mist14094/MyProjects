using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.IO;
using System.Data;
using System.Management;
using KTone.Win.KTSDCPrint;

namespace KTone.Core.KTPrinterApp
{
    public class Program
    {
        static bool isWithinTime = false;
        static Timer time = new Timer();
        static PrintWrapper wrapper = null;

        //static void Main(string[] args)
        //{
        //    try
        //    {
        //        AppSettings.m_Log.Trace("Entering Program:Main()");

        //        string strPrinterMessage;             

        //        if (vaildateArgumens(args))
        //        {
        //            string s = args[0];
        //            if (s == "-p" || s == "-P")
        //            {
        //                Printer printerObj = new Printer();

        //                printerObj.InputFile = args[1];
        //                printerObj.LabelFile = args[2];
        //                printerObj.PrinterName = args[3];
        //                printerObj.PrintEngine = args[4].ToUpper();
        //                printerObj.TimeOut = Convert.ToInt32(args[5]);


        //                try
        //                {
        //                    Type typePrinter = Type.GetType("KTone.Core.KTPrinterApp." + printerObj.PrintEngine + "Printer");
        //                    wrapper = (PrintWrapper)Activator.CreateInstance(typePrinter);
        //                }
        //                catch (ArgumentNullException ex)
        //                {
        //                    AppSettings.m_Log.ErrorException("Error at Program:Main() : " + ex.Message, ex);
        //                    Console.WriteLine("Error Occured: Please enter valid print engine");
        //                    return;
        //                }
        //                //time.Interval = printerObj.TimeOut;
        //                //time.Elapsed += new ElapsedEventHandler(time_Elapsed);
        //                //time.Start();


        //                if (wrapper.Print(printerObj, printerObj.TimeOut, out strPrinterMessage))
        //                {
        //                    //time.Stop();
        //                    Console.WriteLine("SUCCESS");
        //                }
        //                else
        //                {
        //                   // time.Stop();
        //                    Console.WriteLine("FAILED : " + strPrinterMessage);
        //                }
        //            }
        //            else if (s == "-v" || s == "-V")
        //            {

        //                string printEngine = args[1].ToUpper();
        //                try
        //                {
        //                    string errMsg = "";
        //                    Type typePrinter = Type.GetType("KTone.Core.KTPrinterApp." + printEngine + "Printer");
        //                    wrapper = (PrintWrapper)Activator.CreateInstance(typePrinter);

        //                    if (wrapper.Verify(out errMsg))
        //                    {
        //                        if (errMsg.Trim().Length > 0)
        //                            throw new Exception(errMsg);
        //                        else
        //                            Console.Write("SUCCESS");
        //                    }
        //                    else
        //                    {
        //                        Console.WriteLine("FAILED");
        //                    }

        //                }
        //                catch (ArgumentNullException ex)
        //                {
        //                    AppSettings.m_Log.ErrorException("Error at Program:Main() : " + ex.Message, ex);
        //                    Console.WriteLine("Error Occured: Please enter valid print engine");
        //                    return;
        //                }

        //            }
        //            else if (s == "-k" || s == "-K")
        //            {

        //                string printEngine = args[1].ToUpper();
        //                try
        //                {
        //                    Type typePrinter = Type.GetType("KTone.Core.KTPrinterApp." + printEngine + "Printer");
        //                    wrapper = (PrintWrapper)Activator.CreateInstance(typePrinter);

        //                    wrapper.KillPrintProcess();

        //                }
        //                catch (ArgumentNullException ex)
        //                {
        //                    AppSettings.m_Log.ErrorException("Error at Program:Main() : " + ex.Message, ex);
        //                    Console.WriteLine("Error Occured: Please enter valid print engine");
        //                    return;
        //                }

        //            }
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        AppSettings.m_Log.ErrorException("Error at Program:Main() : " + ex.Message, ex);
        //        Console.WriteLine("Error Occured: " + ex.Message);
        //    }
        //    finally
        //    {
        //        AppSettings.m_Log.Trace("Leaving Program:Main()");
        //    }

        //    Console.ReadLine();
        //}

        static void time_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                AppSettings.m_Log.Trace("Entering Program:time_Elapsed()");

                if (!isWithinTime)
                {
                    Console.WriteLine("Failed... Time out occurs.");
                    AppSettings.m_Log.Trace("Time out occurs. Exiting application");
                    Environment.Exit(0);
                }

                time.Stop();
            }
            catch (Exception ex)
            {
                AppSettings.m_Log.ErrorException("Error at Program:time_Elapsed():" + ex.Message, ex);
                throw ex;
            }
            finally
            {
                AppSettings.m_Log.Trace("Leaving Program:time_Elapsed()");
            }
        }

        private static bool vaildateArgumens1(string[] args)
        {
            AppSettings.m_Log.Trace("Entering Program:vaildateArgumens");
            try
            {
                if (args.Length > 0)
                {

                    string s = args[0];
                    AppSettings.m_Log.Trace("Input Paramters");
                    for (int i = 0; i < args.Length; i++)
                    {
                        AppSettings.m_Log.Trace("Args [{0}] : {1}", i.ToString(), args[i]);
                    }


                    if (s == "-p" || s == "-P")
                    {
                        if (args.Length < 6)
                        {
                            AppSettings.m_Log.Trace("Failed: One of the parameter is not provided.");
                            Console.WriteLine("Failed: One of the parameter is not provided.");
                            return false;
                        }

                        if (args[1].Contains(".txt"))
                        {
                            if (!File.Exists(args[1]))
                            {
                                AppSettings.m_Log.Trace("Failed: Input file does not exist");
                                Console.WriteLine("Failed: Input file does not exist");
                                return false;
                            }
                        }
                        else
                        {
                            AppSettings.m_Log.Trace("Failed: Please enter the correct input file.");
                            Console.WriteLine("Failed: Please enter the correct input file.");
                            return false;
                        }

                        if (args[2].Contains(".btw"))
                        {
                            if (!File.Exists(args[2]))
                            {
                                AppSettings.m_Log.Trace("Failed: Input label file does not exist");
                                Console.WriteLine("Failed: Input label file does not exist");
                                return false;
                            }
                        }
                        else
                        {
                            AppSettings.m_Log.Trace("Failed: Please enter the correct input label file.");
                            Console.WriteLine("Failed: Please enter the correct input label file.");
                            return false;
                        }

                        int time;
                        if (int.TryParse(args[5], out time) == false)
                        {
                            AppSettings.m_Log.Trace("Failed: Please enter numerical value of timeout.");
                            Console.WriteLine("Failed: Please enter numerical value of timeout.");
                            return false;
                        }
                        return true;
                    }
                    else if (s == "-v" || s == "-V" || s == "-k" || s == "-K")
                    {
                        if (args.Length != 2)
                        {
                            AppSettings.m_Log.Trace("Failed: 2 input paramters expected.");
                            Console.WriteLine("Failed: 2 input paramters expected.");
                            return false;
                        }
                        return true;
                    }
                }
                else
                {

                    Console.WriteLine("Failed: No parameters passed.");

                }
            }
            catch (Exception ex)
            {
                AppSettings.m_Log.ErrorException("Error at Program:vaildateArgumens : " + ex.Message, ex);
                throw ex;
            }
            finally
            {
                AppSettings.m_Log.Trace("Leaving Program:vaildateArgumens");
            }
            return false;
        }

        private static string vaildateArgumens(string[] args)
        {
            AppSettings.m_Log.Trace("Entering Program:vaildateArgumens");
            string ErrMsg = string.Empty;
            try
            {

                if (args.Length > 0)
                {

                    string s = args[0];
                    AppSettings.m_Log.Trace("Input Paramters");
                    for (int i = 0; i < args.Length; i++)
                    {
                        AppSettings.m_Log.Trace("Args [{0}] : {1}", i.ToString(), args[i]);
                    }


                    if (s == "-p" || s == "-P")
                    {
                        if (args.Length < 6)
                        {
                            AppSettings.m_Log.Trace("Failed: One of the parameter is not provided.");
                            Console.WriteLine("Failed to print:One of the parameter is not provided.");
                            //return false;
                            ErrMsg = "";
                        }

                        if (args[1].Contains(".txt"))
                        {
                            if (!File.Exists(args[1]))
                            {
                                AppSettings.m_Log.Trace("Failed: Input file does not exist");
                                Console.WriteLine("Failed: Input file does not exist");
                                ErrMsg = "Failed to print:Input file does not exist";
                                return ErrMsg;
                            }
                        }
                        else
                        {
                            AppSettings.m_Log.Trace("Failed: Please enter the correct input file.");
                            Console.WriteLine("Failed to print: Please enter the correct input file.");
                            //return false;
                            ErrMsg = "Failed to print:Input file is corrupted,please provide valid input file.";
                            return ErrMsg;
                        }

                        if (args[2].Contains(".btw"))
                        {
                            if (!File.Exists(args[2]))
                            {
                                AppSettings.m_Log.Trace("Failed: Input label file does not exist");
                                Console.WriteLine("Failed: Input label file does not exist");
                                ErrMsg = "Failed to print:Input label file does not exist";
                                return ErrMsg;
                            }
                        }
                        else
                        {
                            AppSettings.m_Log.Trace("Failed: Please enter the correct input label file.");
                            Console.WriteLine("Failed: Please enter the correct input label file.");
                            ErrMsg = "Failed to print:Label file not supported,please provide valid label file.";
                            return ErrMsg;
                        }

                        int time;
                        if (int.TryParse(args[5], out time) == false)
                        {
                            AppSettings.m_Log.Trace("Failed: Please enter numerical value of timeout.");
                            Console.WriteLine("Failed: Please enter numerical value of timeout.");
                            //return false;
                        }
                        return ErrMsg;
                    }
                    else if (s == "-v" || s == "-V" || s == "-k" || s == "-K")
                    {
                        if (args.Length != 2)
                        {
                            AppSettings.m_Log.Trace("Failed: 2 input paramters expected.");
                            Console.WriteLine("Failed: 2 input paramters expected.");
                            //return false;
                        }
                        //return true;
                    }
                }
                else
                {
                    Console.WriteLine("Failed: No parameters passed.");
                    ErrMsg = "Failed to print:No input parameters passed to printer.";
                    return ErrMsg;

                }
            }
            catch (Exception ex)
            {
                AppSettings.m_Log.ErrorException("Error at Program:vaildateArgumens : " + ex.Message, ex);
                throw ex;
            }
            finally
            {
                AppSettings.m_Log.Trace("Leaving Program:vaildateArgumens");
            }
            return ErrMsg;
        }

        public bool Print(string[] args, DataTable dtForPrint, string hexVale, int NoOfCopies,
            out string ToUpdateSerial, out string ErrMsg, out List<string> lstRFIDs)
        {

            StreamReader f = null;
            FileStream fileStream = null;
            lstRFIDs = new List<string>();

            try
            {
                ToUpdateSerial = string.Empty;

                AppSettings.m_Log.Trace("Entering Program:Main()");

                string strPrinterMessage;

                StringBuilder sbPrintData = new StringBuilder("");
                string strPrintData = string.Empty;

                string fileName = args[1];
                if (!File.Exists(fileName))
                {
                    File.Create(fileName).Close();
                }

                f = new StreamReader(fileName);
                string HeaderValue = f.ReadLine();
                f.Close();

                fileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);

                //Byte[] fileInfo = new UTF8Encoding(true).GetBytes(HeaderValue);

                int k = 0;

                string serialNumber = string.Empty;

                int hexLength = hexVale.Length;

                string chars = "0123456789ABCDEF";


                StringBuilder Header = new StringBuilder();

                ConfigPrinterSection cs = ConfigPrinterSection.GetSection();

                if (cs.MySettings.Count > 0)
                {

                    for (int i = 0; i < cs.MySettings.Count; i++)
                    {

                        Header = Header.Append(cs.MySettings[i].NAME);
                        if (i < cs.MySettings.Count - 1)
                            Header = Header.Append(",");
                    }

                }


                List<string> col = new List<string>();
                col.AddRange(Header.ToString().Split(new char[] { ',' }));

                foreach (DataRow drPrint in dtForPrint.Rows)
                {

                    long Lines = Convert.ToInt32(drPrint["Qty"]);
                    StringBuilder ArrlineData = new StringBuilder();
                    foreach (string dc in col)
                    {
                        if (dc != "SERIAL_NO")
                        {
                            if (drPrint.Table.Columns.Contains(dc))
                                ArrlineData.Append(drPrint[dc].ToString().Trim() + ",");
                            else
                                ArrlineData.Append(",");
                        }
                    }

                    for (long j = 0; j < Lines; j++)
                    {

                        if (hexVale.Substring(hexLength - 1).ToString().ToUpper() == "F")
                        {

                            hexVale = hexVale.Remove(hexLength - 1);
                            hexVale = hexVale + "0";

                            char[] hexNumber = hexVale.ToCharArray();

                            for (int charIndex = hexNumber.Length - 2; charIndex >= 0; charIndex--)
                            {
                                if (hexNumber[charIndex] == 'F')
                                {
                                    hexNumber[charIndex] = '0';
                                }
                                else
                                {
                                    char c = hexNumber[charIndex];
                                    int index = chars.IndexOf(c);
                                    c = chars[index + 1];
                                    hexNumber[charIndex] = c;
                                    // hexVale = hexVale.Remove(hexLength - 1);
                                    //hexVale = hexVale + c.ToString();

                                    hexVale = hexNumber.ToString();
                                    hexVale = new string(hexNumber);
                                    break;
                                }
                            }

                            for (int i = hexLength - 1; i >= 0; i--)
                            {
                                if (i == hexLength - 1)
                                {
                                    if (hexVale[i] == 'F')
                                    {

                                    }
                                    else
                                    {
                                        char c = hexVale[i];
                                        int charIndex = chars.IndexOf(c);
                                        c = chars[charIndex + 1];
                                    }
                                }
                            }
                        }
                        else
                        {
                            char c = hexVale[hexLength - 1];
                            int charIndex = chars.IndexOf(c);
                            c = chars[charIndex + 1];
                            hexVale = hexVale.Remove(hexLength - 1);
                            hexVale = hexVale + c.ToString();
                        }

                        for (int copies = 0; copies < NoOfCopies; copies++)
                        {
                            sbPrintData.Append(ArrlineData);

                            sbPrintData.Append(hexVale + "" + Environment.NewLine);
                            lstRFIDs.Add(hexVale);
                        }


                    }
                }


                string fileData = sbPrintData.ToString().TrimEnd();

                Byte[] fileInfo = new UTF8Encoding(true).GetBytes(Header + Environment.NewLine + fileData);

                //System.IO.File.WriteAllText(@fileName, string.Empty);

                fileStream.Flush();
                fileStream.SetLength(0);
                fileStream.Write(fileInfo, 0, fileInfo.Length);

                fileStream.Close();

                ErrMsg = string.Empty;

                ErrMsg = vaildateArgumens(args);

                if (string.IsNullOrEmpty(ErrMsg))
                {
                    string s = args[0];
                    if (s == "-p" || s == "-P")
                    {
                        Printer printerObj = new Printer();

                        printerObj.InputFile = args[1];
                        printerObj.LabelFile = args[2];
                        printerObj.PrinterName = args[3];
                        printerObj.PrintEngine = args[4].ToUpper();
                        printerObj.TimeOut = Convert.ToInt32(args[5]);


                        try
                        {
                            Type typePrinter = Type.GetType("KTone.Core.KTPrinterApp." + printerObj.PrintEngine + "Printer");
                            AppSettings.m_Log.Error("Printing Success:" + typePrinter.FullName + " ," + typePrinter.Name);
                            wrapper = (PrintWrapper)Activator.CreateInstance(typePrinter);
                        }
                        catch (ArgumentNullException ex)
                        {
                            AppSettings.m_Log.ErrorException("Error at Program:Main() : " + ex.Message, ex);
                            return false;
                        }
                        //time.Interval = printerObj.TimeOut;
                        //time.Elapsed += new ElapsedEventHandler(time_Elapsed);
                        //time.Start();

                        strPrinterMessage = "";
                        if (wrapper.Print(printerObj, printerObj.TimeOut, out strPrinterMessage))
                        {
                            //time.Stop();                           
                            AppSettings.m_Log.Error("Printing Success:" + strPrinterMessage);
                            return true;

                        }
                        else
                        {
                            // time.Stop();   
                            if (!strPrinterMessage.Trim().Equals(string.Empty))
                                ErrMsg = strPrinterMessage;
                            else
                                ErrMsg = "Failed to print:Either BarTender License expired or BarTender is not installed.";
                            AppSettings.m_Log.Error("Failed Printing :" + strPrinterMessage);
                            return false;
                        }
                    }
                    else if (s == "-v" || s == "-V")
                    {

                        string printEngine = args[1].ToUpper();
                        try
                        {
                            string errMsg = "";
                            Type typePrinter = Type.GetType("KTone.Core.KTPrinterApp." + printEngine + "Printer");
                            wrapper = (PrintWrapper)Activator.CreateInstance(typePrinter);

                            if (wrapper.Verify(out errMsg))
                            {
                                if (errMsg.Trim().Length > 0)
                                    throw new Exception(errMsg);
                                else
                                    Console.Write("SUCCESS");
                            }
                            else
                            {
                                Console.WriteLine("FAILED");
                            }

                        }
                        catch (ArgumentNullException ex)
                        {
                            AppSettings.m_Log.ErrorException("Error at Program:Main() : " + ex.Message, ex);
                            Console.WriteLine("Error Occured: Please enter valid print engine");
                            return false;
                        }

                    }
                    else if (s == "-k" || s == "-K")
                    {

                        string printEngine = args[1].ToUpper();
                        try
                        {
                            Type typePrinter = Type.GetType("KTone.Core.KTPrinterApp." + printEngine + "Printer");
                            wrapper = (PrintWrapper)Activator.CreateInstance(typePrinter);

                            wrapper.KillPrintProcess();

                        }
                        catch (ArgumentNullException ex)
                        {
                            AppSettings.m_Log.ErrorException("Error at Program:Main() : " + ex.Message, ex);
                            Console.WriteLine("Error Occured: Please enter valid print engine");
                            return false;
                        }

                    }
                }
                else
                {
                    return false;
                }

            }

            catch (Exception ex)
            {
                f.Close();
                fileStream.Close();
                ErrMsg = "BarTender License expired.";
                AppSettings.m_Log.ErrorException("Error at Program:Main() : " + ex.Message, ex);
                Console.WriteLine("Error Occured: " + ex.Message);
            }
            finally
            {
                ToUpdateSerial = hexVale;
                AppSettings.m_Log.Trace("Leaving Program:Main()");
            }

            //Console.ReadLine();


            return true;
        }
    }
}
