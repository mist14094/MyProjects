using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KTone.Core.KTPrinterApp
{
    /// <summary>
    /// This class contains all information of printer.
    /// </summary>

    public class Printer
    {

        private string inputFile = string.Empty;
        private string labelFile = string.Empty;
        private string printerName = string.Empty;
        private string printEngine = string.Empty;
        private int timeOut = 0;
        private int noOfCopies = 0;

        
        public string InputFile
        {
            get { return inputFile; }
            set { inputFile = value; }
        }    

        public string LabelFile
        {
            get { return labelFile; }
            set { labelFile = value; }
        }       

        public string PrinterName
        {
            get { return printerName; }
            set { printerName = value; }
        }       

        public string PrintEngine
        {
            get { return printEngine; }
            set { printEngine = value; }
        }     

        public int TimeOut
        {
            get { return timeOut; }
            set { timeOut = value; }
        }

        public int NoOfCopies
        {
            get { return noOfCopies; }
            set { noOfCopies = value; }
        }


        public Printer()
        {
            this.inputFile = string.Empty;
            this.labelFile = string.Empty;
            this.printerName = string.Empty;
            this.printEngine = string.Empty;
            this.timeOut = 0;
            this.noOfCopies = 0;
        }

        public Printer(string inputFile, string labelFile, string printerName, string printEngine, int timeOut,int noOfCopies)
        {
            this.inputFile = inputFile;
            this.labelFile = labelFile;
            this.printerName = printerName;
            this.printEngine = printEngine.ToUpper();
            this.timeOut = timeOut;
            this.noOfCopies = noOfCopies;
        }
    }

    public enum OperationType
    {
        PRINT,
        VERIFY_PRINTER,
        KILL_PROCESS,
    }
}
