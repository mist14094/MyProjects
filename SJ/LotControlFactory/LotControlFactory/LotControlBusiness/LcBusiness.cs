using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using LotControlConstants;
using LotControlDataAccess;
using NLog;

namespace LotControlBusiness
{
    public class LcBusiness
    {
        private readonly Logger _nlog = LogManager.GetCurrentClassLogger();
        private LotControlDataAccess.LcDataAccess _access = new LcDataAccess();
        private LotControlConstants.LcConstants _lcConstants = new LcConstants();
        public LcBusiness()
        {
            _nlog.Trace(message:
                this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            _access = new LcDataAccess();
            _nlog.Trace(message:
                this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "::Exit");
        }
        public DataTable GetGrnValues(string grnValues)
        {
            _nlog.Trace(message:
                this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return _access.GetGrnValues(grnValues);

        }

        public DataTable GetLotDetails(string poDetails)
        {
            _nlog.Trace(message:
                this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return _access.GetLotDetails(poDetails);

        }
        public DataTable ImportItemsinPO(string poNumber)
        {
            _nlog.Trace(message:
                this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return _access.ImportItemsinPO(poNumber);

        }

        public DataTable GetBarcodeDetails(string barcode)
        {
            _nlog.Trace(message:
              this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
              System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return _access.GetBarcodeDetails(barcode);
        }

        public object InsertLabelPrintLog(int numberoftags, bool chkPrint, int barcode, string stockCode, string description, string quantity, string warehouse, string lotnumber, string grnNumber, string supplier, string poNumber, string counts)
        {
            _nlog.Trace(message:
                this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return _access.InsertLabelPrintLog(numberoftags, chkPrint, barcode,
                stockCode, description, quantity, warehouse,
                lotnumber, grnNumber, supplier, poNumber, counts);
        }


        public DataTable UpdateAltUOM(int barcode, float quantity)
        {
            _nlog.Trace(message:
              this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
              System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return _access.UpdateAltUOM(barcode,quantity);
        }

        public DataTable updateFinalized(int barcode)
        {
            _nlog.Trace(message:
              this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
              System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return _access.updateFinalized(barcode);
        }

        public  DataTable GetLabelForPO(string poNumber)
        {
            _nlog.Trace(message:
                this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return _access.GetLabelForPO(poNumber);

        }

    }
}
