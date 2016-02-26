using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

namespace KTone.Core.KTPrinterApp
{
    public class PrintWrapper
    {
        public virtual bool Print(Printer printer, out string printerMessage)
        {
            printerMessage = string.Empty;
            return false;
        }

        public virtual bool Print(Printer printer,int timeOut, out string printerMessage)
        {
            printerMessage = string.Empty;
            return false;
        }

        public virtual bool Verify(out string printerMessage)
        {
            printerMessage = string.Empty;
            return false;
        }

        public virtual void KillPrintProcess()
        {

        }
    }
}
