using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BreakEvenDAL;

namespace BreakEvenBAL
{
    public class BEBAL
    {
        BEDAL ObjBEDAL = new BEDAL();
        #region BEPProductSelect #Page 1#
        public DataSet getAllUPC()
        {
           return  ObjBEDAL.getAllUPC();
     
        }

        public DataSet getProductSearch(string UPC, string SKU, string Desc)
        {
            return ObjBEDAL.getProductSearch(UPC, SKU, Desc);

        }

        public DataSet getBreakEvenProductSearch(string UPC, string SKU, string Desc, string Vendorname)
        {
            return ObjBEDAL.getBreakEvenProductSearch(UPC, SKU, Desc, Vendorname);

        }
        public string getPIDByUPC(string UPC)
        {
            return ObjBEDAL.getPIDByUPC(UPC);

        }
        public string getDESCByPID(string PID)
        {

            return ObjBEDAL.getDESCByPID(PID);
        }
        public string getSKUByPID(string PID)
        {
            return ObjBEDAL.getSKUByPID(PID);
        }


        #endregion
        #region BEPProduct #Page 2#

        public double getFinalAVGRetailPriceByPID(string PID)
        {
            return ObjBEDAL.getFinalAVGRetailPriceByPID(PID);
        }

        public string getUPCByPID(string PID)
        {
            return ObjBEDAL.getUPCByPID(PID);
        }
        public double getFinalAVGCostByPID(string PID)
        {
            return ObjBEDAL.getFinalAVGCostByPID(PID);
        }
        public double GetLastFinalPurchaseCost(string PID)
        {
            return ObjBEDAL.GetLastFinalPurchaseCost(PID);
        }
        public double getAVGRetailPriceByPID(string PID)
        {
            return ObjBEDAL.getAVGRetailPriceByPID(PID);
        }
        public double getAVGRetailCostByPID(string PID)
        {
            return ObjBEDAL.getAVGRetailPriceByPID(PID);
        }
        public int getTotalReceivedByPID(string PID)
        {
            return ObjBEDAL.getTotalReceivedByPID(PID);
        }
        public double getTotalCostByPID(string PID)
        {

            return ObjBEDAL.getTotalCostByPID(PID);
        }
        public int getTotalSoldByPID(string PID)
        {

            return ObjBEDAL.getTotalSoldByPID(PID);
        }
        public double getTotalMoneyRealisedByPID(string PID)
        {
            return ObjBEDAL.getTotalMoneyRealisedByPID(PID);
        }

        public string GetDistinctVendorName()
        {
            return ObjBEDAL.GetDistinctVendorName();
        }

        public string GetDistinctSKU()
        {
            return ObjBEDAL.GetDistinctSKU();
        }


        public string GetDistinctDescription()
        {
            return ObjBEDAL.GetDistinctDescription();
        }

        public string GetConnKTBEValue()
        {
            return ObjBEDAL.GetConnKTBEValue();
        }

        public DataSet GetUPCSKUbyDesc(string Desc)
        {
            return ObjBEDAL.GetUPCSKUbyDesc(Desc);
        }

        public DataSet GetBreakEvenValues(string PC_ID)
        {
            return ObjBEDAL.GetBreakEvenValues(PC_ID);
        }

        #endregion
    }
}
