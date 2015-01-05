using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BreakEvenDAL;
using NLog;

namespace BreakEvenBAL
{
    public class BreakEvenSnapShot
    {

        NLog.Logger nlog = LogManager.GetCurrentClassLogger();

        public Dictionary<long, BEP_Detail> GetBreakEvenSnapShot_ALL()
        {
            nlog.Trace("BreakEvenBAL:BreakEvenSnapShot:GetBreakEvenSnapShot_ALL::Entering");
            try
            {
                BESanpshot snapShot_AllResult = new BESanpshot();
                DataSet ds = snapShot_AllResult.GetBreakEvenSnapShot();

                Dictionary<long, BEP_Detail> lst = new Dictionary<long, BEP_Detail>();

                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {

                        BEP_Detail bep = new BEP_Detail();
                        if (dr["PC_ID"] != null   && dr["PC_ID"].ToString().Trim() != "")
                            bep.PC_ID = long.Parse(dr["PC_ID"].ToString());
                        if (dr["UPC"] != null && dr["UPC"].ToString().Trim() != "") bep.UPC = dr["UPC"].ToString();
                        if (dr["SKU"] != null && dr["SKU"].ToString().Trim() != "") bep.SKU = dr["SKU"].ToString();
                        if (dr["StockCode"] != null && dr["StockCode"].ToString().Trim() != "") bep.StockCode = dr["StockCode"].ToString();
                        if (dr["ItemDesc"] != null && dr["ItemDesc"].ToString().Trim() != "") bep.Description = dr["ItemDesc"].ToString();
                        if (dr["Vendor"] != null && dr["Vendor"].ToString().Trim() != "") bep.Vendor = dr["Vendor"].ToString();
                        if (dr["UOM"] != null && dr["UOM"].ToString().Trim() != "") bep.UOM = dr["UOM"].ToString();
                        if (dr["AvgCOGS"] != null && dr["AvgCOGS"].ToString().Trim() != "") bep.AvgCOGS = float.Parse(dr["AvgCOGS"].ToString());
                        if (dr["AvgSalePrice"] != null && dr["AvgSalePrice"].ToString().Trim() != "") bep.AvgSalePrice = float.Parse(dr["AvgSalePrice"].ToString());
                        if (dr["LastSaleDate"] != null && dr["LastSaleDate"].ToString().Trim() != "") bep.LastSaleDate = DateTime.Parse(dr["LastSaleDate"].ToString());
                        if (dr["StoreName"] != null && dr["StoreName"].ToString().Trim() != "") bep.LastSaleLocation = dr["StoreName"].ToString();
                        if (dr["SuggRetail"] != null && dr["SuggRetail"].ToString().Trim() != "") bep.SuggRetail = float.Parse(dr["SuggRetail"].ToString());
                        if (dr["TotalCOGS"] != null && dr["TotalCOGS"].ToString().Trim() != "") bep.TotalCOGS = float.Parse(dr["TotalCOGS"].ToString());
                        if (dr["TotalRCVD"] != null && dr["TotalRCVD"].ToString().Trim() != "") bep.TotalRCVD = long.Parse(dr["TotalRCVD"].ToString());
                        if (dr["TotalSales"] != null && dr["TotalSales"].ToString().Trim() != "") bep.TotalSales = float.Parse(dr["TotalSales"].ToString());
                        if (dr["TotalSold"] != null && dr["TotalSold"].ToString().Trim() != "") bep.TotalSold = long.Parse(dr["TotalSold"].ToString());
                        if (dr["AvgCOGS"] != null && dr["AvgCOGS"].ToString().Trim() != "") bep.AvgCOGS = float.Parse(dr["AvgCOGS"].ToString());
                        if (dr["AvgCOGS"] != null && dr["AvgCOGS"].ToString().Trim() != "") bep.AvgCOGS = float.Parse(dr["AvgCOGS"].ToString());
                        if (dr["Profit_Margin"] != null && dr["Profit_Margin"].ToString().Trim() != "") bep.ProfitMargin = float.Parse(dr["Profit_Margin"].ToString());
                        if (dr["Profit_Percentage"] != null && dr["Profit_Percentage"].ToString().Trim() != "") bep.ProfitPercentage = float.Parse(dr["Profit_Percentage"].ToString());
                        lst[bep.PC_ID] = bep;

                    }

                }

                return lst;
            }
            catch (Exception ex)
            {
                nlog.ErrorException("BreakEvenBAL:BreakEvenSnapShot:GetBreakEvenSnapShot_ALL::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace("BreakEvenBAL:BreakEvenSnapShot:GetBreakEvenSnapShot_ALL::Leaving");

            }
        }
    }


}
