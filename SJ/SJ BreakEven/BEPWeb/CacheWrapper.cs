using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BreakEvenBAL;
using System.Web.Caching;


namespace BEPWeb
{
    public class CacheWrapper
    {
        public static CacheWrapper objCacheWrapper = null;
        private static System.Web.Caching.Cache objCache = new Cache();
        private static Dictionary<long, BEP_Detail> lstDetails = new Dictionary<long, BEP_Detail>();
        private static string UPC, Description, Vendor;

        public NLog.Logger nlog = NLog.LogManager.GetCurrentClassLogger();

        public static CacheWrapper GetInstance()
        {
            if (objCacheWrapper == null)
                objCacheWrapper = new CacheWrapper();
            return objCacheWrapper;
        }

        CacheWrapper()
        {
            try
            {
                nlog.Trace("CacheWrapper:CacheWrapper::Entering");
                if (lstDetails.Count == 0)
                    GetBEPSnapShot();
            }
            catch (Exception ex)
            {
                nlog.ErrorException("CacheWrapper:CacheWrapper::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace("CacheWrapper:CacheWrapper::Leaving");
            }

        }


        void GetBEPSnapShot()
        {
            try
            {
                nlog.Trace("CacheWrapper:GetBEPSnapShot::Entering");
                BreakEvenSnapShot objBEP = new BreakEvenSnapShot();
                //Dictionary<long, BEP_Detail> 
                lstDetails = objBEP.GetBreakEvenSnapShot_ALL();

                // objCache.Add("BEPGrid", lstDetails, DateTime.Now.Add(new TimeSpan(1, 0, 0)), new TimeSpan(1, 0, 0), CacheItemPriority.Normal, null);
            }
            catch (Exception ex)
            {
                nlog.ErrorException("CacheWrapper:GetBEPSnapShot::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace("CacheWrapper:GetBEPSnapShot::Leaving");
            }

        }


        public List<BEP_Detail> GetAllBEPItems()
        {
            return GetAllBEPItems("", "", "", "", 0);
        }
        public List<BEP_Detail> GetAllBEPItems(string upc, string vendor, string desc, string precentageCriteria, int minQtyOnHand)
        {
            try
            {
                nlog.Trace("CacheWrapper:GetAllBEPItems::Entering");

                List<BEP_Detail> lstItems = new List<BEP_Detail>();
                try
                {
                    if (lstDetails.Count == 0)
                    {
                        GetBEPSnapShot();
                    }
                }
                catch { GetBEPSnapShot(); }

                if (upc.Trim() == "" && vendor.Trim() == "" && desc.Trim() == "" && precentageCriteria.Trim() == "NONE" && minQtyOnHand == 0)
                    return lstDetails.Values.ToList<BEP_Detail>();
                else
                {
                    vendor = vendor.ToUpper();
                    desc = desc.ToUpper();
                    float minRange, maxRange;
                    minRange = -1;
                    maxRange = -1;

                    switch (precentageCriteria)
                    {
                        case "MINUS50":
                            maxRange = -50;
                            break;
                        case "MINUS2550":
                            minRange = -50;
                            maxRange = -25;
                            break;
                        case "MINUS025":
                            minRange = -25;
                            maxRange = 0;
                            break;
                        case "POSITIVE010":
                            minRange = 0;
                            maxRange = 10;
                            break;
                        case "POSITIVE1025":
                            minRange = 10;
                            maxRange = 25;
                            break;
                        case "POSITIVE2550":
                            minRange = 25;
                            maxRange = 50;
                            break;
                        case "POSITIVE50":
                            minRange = 50;
                            break;
                        default: minRange = -1; maxRange = -1; break;

                    }

                    if (upc.Trim() != "")
                        lstItems.AddRange(lstDetails.Values.Where(b => b.UPC.Contains(upc)).ToList<BEP_Detail>());
                    if (vendor.Trim() != "")
                        lstItems.AddRange(lstDetails.Values.Where(b => b.Vendor.ToUpper().Contains(vendor)).ToList<BEP_Detail>());
                    if (desc.Trim() != "")
                        lstItems.AddRange(lstDetails.Values.Where(b => b.Description.Contains(desc)).ToList<BEP_Detail>());
                   
                    if(minRange == -1 && maxRange != -1)
                        lstItems.AddRange(lstDetails.Values.Where(b => b.ProfitPercentage < maxRange).ToList<BEP_Detail>());
                    else if (minRange != -1 && maxRange == -1)
                        lstItems.AddRange(lstDetails.Values.Where(b => b.ProfitPercentage >= minRange).ToList<BEP_Detail>());
                    else if (minRange != -1 && maxRange != -1)
                        lstItems.AddRange(lstDetails.Values.Where(b => b.ProfitPercentage >= minRange && b.ProfitPercentage < maxRange).ToList<BEP_Detail>());
                  
                    if(minQtyOnHand > 0)
                        lstItems.AddRange(lstDetails.Values.Where(b => (b.TotalRCVD - b.TotalSold) >= minQtyOnHand).ToList<BEP_Detail>());
                  
                    return lstItems;
                }
            }
            catch (Exception ex)
            {
                nlog.ErrorException("CacheWrapper:GetAllBEPItems::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace("CacheWrapper:GetAllBEPItems::Leaving");
            }

        }

        public BEP_Detail GetBEPItem(long id)
        {
            try
            {
                nlog.Trace("CacheWrapper:GetBEPItem::Entering");

                Dictionary<long, BEP_Detail> lstDetailTemps = null;
                try
                {
                    if (lstDetails.Count > 0)
                        lstDetailTemps = lstDetails;
                    else
                    {
                        GetBEPSnapShot(); lstDetailTemps = lstDetails;
                    }
                }
                catch { GetBEPSnapShot(); lstDetailTemps = lstDetails; }
                return lstDetailTemps[id];

            }
            catch (Exception ex)
            {
                nlog.ErrorException("CacheWrapper:GetBEPItem::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace("CacheWrapper:GetBEPItem::Leaving");
            }

        }

        public List<BEP_Detail> GetAllProfitBEPItems()
        {
            return GetAllProfitBEPItems("", "", "", "", 0);
        }
        public List<BEP_Detail> GetAllProfitBEPItems(string upc, string vendor, string desc, string precentageCriteria, int minQtyOnHand)
        {
            try
            {
                nlog.Trace("CacheWrapper:GetAllBEPItems::Entering");

                List<BEP_Detail> lstItems = new List<BEP_Detail>();
                List<BEP_Detail> lstItems1 = new List<BEP_Detail>();

                try
                {
                    if (lstDetails.Count == 0)
                    {
                        GetBEPSnapShot();
                    }
                }
                catch { GetBEPSnapShot(); }

                lstItems1 = lstDetails.Values.Where(b => b.ProfitMargin >= 0).ToList<BEP_Detail>();

                if (upc.Trim() == "" && vendor.Trim() == "" && desc.Trim() == "" && precentageCriteria.Trim() == "NONE" && minQtyOnHand == 0)
                    return lstItems1;
                else
                {
                    vendor = vendor.ToUpper();
                    desc = desc.ToUpper();
                    if (upc.Trim() != "")
                        lstItems.AddRange(lstItems1.Where(b => b.UPC.Contains(upc)).ToList<BEP_Detail>());
                    if (vendor.Trim() != "")
                        lstItems.AddRange(lstItems1.Where(b => b.Vendor.ToUpper().Contains(vendor)).ToList<BEP_Detail>());
                    if (desc.Trim() != "")
                        lstItems.AddRange(lstItems1.Where(b => b.Description.Contains(desc)).ToList<BEP_Detail>());

                    float minRange, maxRange;
                    minRange = -1;
                    maxRange = -1;

                    switch (precentageCriteria)
                    {
                        case "MINUS50":
                            maxRange = -50;
                            break;
                        case "MINUS2550":
                            minRange = -50;
                            maxRange = -25;
                            break;
                        case "MINUS025":
                            minRange = -25;
                            maxRange = 0;
                            break;
                        case "POSITIVE010":
                            minRange = 0;
                            maxRange = 10;
                            break;
                        case "POSITIVE1025":
                            minRange = 10;
                            maxRange = 25;
                            break;
                        case "POSITIVE2550":
                            minRange = 25;
                            maxRange = 50;
                            break;
                        case "POSITIVE50":
                            minRange = 50;
                            break;
                        default: minRange = -1; maxRange = -1; break;

                    }

                    if (minRange == -1 && maxRange != -1)
                        lstItems.AddRange(lstItems1.Where(b => b.ProfitPercentage < maxRange).ToList<BEP_Detail>());
                    else if (minRange != -1 && maxRange == -1)
                        lstItems.AddRange(lstItems1.Where(b => b.ProfitPercentage >= minRange).ToList<BEP_Detail>());
                    else if (minRange != -1 && maxRange != -1)
                        lstItems.AddRange(lstItems1.Where(b => b.ProfitPercentage >= minRange && b.ProfitPercentage < maxRange).ToList<BEP_Detail>());

                    if (minQtyOnHand > 0)
                        lstItems.AddRange(lstItems1.Where(b => (b.TotalRCVD - b.TotalSold) >= minQtyOnHand).ToList<BEP_Detail>());
                  

                    return lstItems;
                }


            }
            catch (Exception ex)
            {
                nlog.ErrorException("CacheWrapper:GetAllBEPItems::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace("CacheWrapper:GetAllBEPItems::Leaving");
            }

        }

        public List<BEP_Detail> GetAllLossBEPItems()
        {
            return GetAllLossBEPItems("", "", "", "", 0);
        }
        public List<BEP_Detail> GetAllLossBEPItems(string upc, string vendor, string desc, string precentageCriteria, int minQtyOnHand)
        {
            try
            {
                nlog.Trace("CacheWrapper:GetAllBEPItems::Entering");

                List<BEP_Detail> lstItems = new List<BEP_Detail>();
                List<BEP_Detail> lstItems1 = new List<BEP_Detail>();

                try
                {
                    if (lstDetails.Count == 0)
                    {
                        GetBEPSnapShot();
                    }
                }
                catch { GetBEPSnapShot(); }

                lstItems1 = lstDetails.Values.Where(b => b.ProfitMargin < 0).ToList<BEP_Detail>();

                if (upc.Trim() == "" && vendor.Trim() == "" && desc.Trim() == "" && precentageCriteria.Trim() == "NONE" && minQtyOnHand == 0)
                    return lstItems1;
                else
                {
                    vendor = vendor.ToUpper();
                    desc = desc.ToUpper();
                    if (upc.Trim() != "")
                        lstItems.AddRange(lstItems1.Where(b => b.UPC.Contains(upc)).ToList<BEP_Detail>());
                    if (vendor.Trim() != "")
                        lstItems.AddRange(lstItems1.Where(b => b.Vendor.ToUpper().Contains(vendor)).ToList<BEP_Detail>());
                    if (desc.Trim() != "")
                        lstItems.AddRange(lstItems1.Where(b => b.Description.Contains(desc)).ToList<BEP_Detail>());
                    float minRange, maxRange;
                    minRange = -1;
                    maxRange = -1;

                    switch (precentageCriteria)
                    {
                        case "MINUS50":
                            maxRange = -50;
                            break;
                        case "MINUS2550":
                            minRange = -50;
                            maxRange = -25;
                            break;
                        case "MINUS025":
                            minRange = -25;
                            maxRange = 0;
                            break;
                        case "POSITIVE010":
                            minRange = 0;
                            maxRange = 10;
                            break;
                        case "POSITIVE1025":
                            minRange = 10;
                            maxRange = 25;
                            break;
                        case "POSITIVE2550":
                            minRange = 25;
                            maxRange = 50;
                            break;
                        case "POSITIVE50":
                            minRange = 50;
                            break;
                        default: minRange = -1; maxRange = -1; break;

                    }

                    if (minRange == -1 && maxRange != -1)
                        lstItems.AddRange(lstItems1.Where(b => b.ProfitPercentage < maxRange).ToList<BEP_Detail>());
                    else if (minRange != -1 && maxRange == -1)
                        lstItems.AddRange(lstItems1.Where(b => b.ProfitPercentage >= minRange).ToList<BEP_Detail>());
                    else if (minRange != -1 && maxRange != -1)
                        lstItems.AddRange(lstItems1.Where(b => b.ProfitPercentage >= minRange && b.ProfitPercentage < maxRange).ToList<BEP_Detail>());

                    if (minQtyOnHand > 0)
                        lstItems.AddRange(lstItems1.Where(b => (b.TotalRCVD - b.TotalSold) >= minQtyOnHand).ToList<BEP_Detail>());
                  

                    return lstItems;
                }


            }
            catch (Exception ex)
            {
                nlog.ErrorException("CacheWrapper:GetAllBEPItems::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace("CacheWrapper:GetAllBEPItems::Leaving");
            }

        }


        public List<BEP_Detail> GetAllEvenBEPItems()
        {
            return GetAllEvenBEPItems("", "", "", "", 0);
        }
        public List<BEP_Detail> GetAllEvenBEPItems(string upc, string vendor, string desc, string precentageCriteria, int minQtyOnHand)
        {
            try
            {
                nlog.Trace("CacheWrapper:GetAllEvenBEPItems::Entering");

                List<BEP_Detail> lstItems = new List<BEP_Detail>();
                List<BEP_Detail> lstItems1 = new List<BEP_Detail>();

                try
                {
                    if (lstDetails.Count == 0)
                    {
                        GetBEPSnapShot();
                    }
                }
                catch { GetBEPSnapShot(); }

                lstItems1 = lstDetails.Values.Where(b => b.TotalSold == b.TotalRCVD).ToList<BEP_Detail>();

                if (upc.Trim() == "" && vendor.Trim() == "" && desc.Trim() == "" && precentageCriteria.Trim() == "NONE" && minQtyOnHand == 0)
                    return lstItems1;
                else
                {
                    vendor = vendor.ToUpper();
                    desc = desc.ToUpper();
                    if (upc.Trim() != "")
                        lstItems.AddRange(lstItems1.Where(b => b.UPC.Contains(upc)).ToList<BEP_Detail>());
                    if (vendor.Trim() != "")
                        lstItems.AddRange(lstItems1.Where(b => b.Vendor.ToUpper().Contains(vendor)).ToList<BEP_Detail>());
                    if (desc.Trim() != "")
                        lstItems.AddRange(lstItems1.Where(b => b.Description.Contains(desc)).ToList<BEP_Detail>());

                    float minRange, maxRange;
                    minRange = -1;
                    maxRange = -1;

                    switch (precentageCriteria)
                    {
                        case "MINUS50":
                            maxRange = -50;
                            break;
                        case "MINUS2550":
                            minRange = -50;
                            maxRange = -25;
                            break;
                        case "MINUS025":
                            minRange = -25;
                            maxRange = 0;
                            break;
                        case "POSITIVE010":
                            minRange = 0;
                            maxRange = 10;
                            break;
                        case "POSITIVE1025":
                            minRange = 10;
                            maxRange = 25;
                            break;
                        case "POSITIVE2550":
                            minRange = 25;
                            maxRange = 50;
                            break;
                        case "POSITIVE50":
                            minRange = 50;
                            break;
                        default: minRange = -1; maxRange = -1; break;

                    }

                    if (minRange == -1 && maxRange != -1)
                        lstItems.AddRange(lstItems1.Where(b => b.ProfitPercentage < maxRange).ToList<BEP_Detail>());
                    else if (minRange != -1 && maxRange == -1)
                        lstItems.AddRange(lstItems1.Where(b => b.ProfitPercentage >= minRange).ToList<BEP_Detail>());
                    else if (minRange != -1 && maxRange != -1)
                        lstItems.AddRange(lstItems1.Where(b => b.ProfitPercentage >= minRange && b.ProfitPercentage < maxRange).ToList<BEP_Detail>());

                    if (minQtyOnHand > 0)
                        lstItems.AddRange(lstItems1.Where(b => (b.TotalRCVD - b.TotalSold) >= minQtyOnHand).ToList<BEP_Detail>());
                  

                    return lstItems;
                }


            }
            catch (Exception ex)
            {
                nlog.ErrorException("CacheWrapper:GetAllEvenBEPItems::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace("CacheWrapper:GetAllEvenBEPItems::Leaving");
            }

        }

        public List<BEP_Detail> GetAllQOHmorethnZeroItems()
        {
            return GetAllQOHmorethnZeroItems("", "", "", "", 0);
        }
        public List<BEP_Detail> GetAllQOHmorethnZeroItems(string upc, string vendor, string desc, string precentageCriteria, int minQtyOnHand)
        {
            try
            {
                nlog.Trace("CacheWrapper:GetAllQOHmorethnZeroItems::Entering");

                List<BEP_Detail> lstItems = new List<BEP_Detail>();
                List<BEP_Detail> lstItems1 = new List<BEP_Detail>();

                try
                {
                    if (lstDetails.Count == 0)
                    {
                        GetBEPSnapShot();
                    }
                }
                catch { GetBEPSnapShot(); }

                lstItems1 = lstDetails.Values.Where(b => (b.TotalRCVD - b.TotalSold) > 0).ToList<BEP_Detail>();

                if (upc.Trim() == "" && vendor.Trim() == "" && desc.Trim() == "" && precentageCriteria.Trim() == "NONE" && minQtyOnHand == 0)
                    return lstItems1;
                else
                {
                    vendor = vendor.ToUpper();
                    desc = desc.ToUpper();
                    if (upc.Trim() != "")
                        lstItems.AddRange(lstItems1.Where(b => b.UPC.Contains(upc)).ToList<BEP_Detail>());
                    if (vendor.Trim() != "")
                        lstItems.AddRange(lstItems1.Where(b => b.Vendor.ToUpper().Contains(vendor)).ToList<BEP_Detail>());
                    if (desc.Trim() != "")
                        lstItems.AddRange(lstItems1.Where(b => b.Description.Contains(desc)).ToList<BEP_Detail>());

                    float minRange, maxRange;
                    minRange = -1;
                    maxRange = -1;

                    switch (precentageCriteria)
                    {
                        case "MINUS50":
                            maxRange = -50;
                            break;
                        case "MINUS2550":
                            minRange = -50;
                            maxRange = -25;
                            break;
                        case "MINUS025":
                            minRange = -25;
                            maxRange = 0;
                            break;
                        case "POSITIVE010":
                            minRange = 0;
                            maxRange = 10;
                            break;
                        case "POSITIVE1025":
                            minRange = 10;
                            maxRange = 25;
                            break;
                        case "POSITIVE2550":
                            minRange = 25;
                            maxRange = 50;
                            break;
                        case "POSITIVE50":
                            minRange = 50;
                            break;
                        default: minRange = -1; maxRange = -1; break;

                    }

                    if (minRange == -1 && maxRange != -1)
                        lstItems.AddRange(lstItems1.Where(b => b.ProfitPercentage < maxRange).ToList<BEP_Detail>());
                    else if (minRange != -1 && maxRange == -1)
                        lstItems.AddRange(lstItems1.Where(b => b.ProfitPercentage >= minRange).ToList<BEP_Detail>());
                    else if (minRange != -1 && maxRange != -1)
                        lstItems.AddRange(lstItems1.Where(b => b.ProfitPercentage >= minRange && b.ProfitPercentage < maxRange).ToList<BEP_Detail>());

                    if (minQtyOnHand > 0)
                        lstItems.AddRange(lstItems1.Where(b => (b.TotalRCVD - b.TotalSold) >= minQtyOnHand).ToList<BEP_Detail>());


                    return lstItems;
                }


            }
            catch (Exception ex)
            {
                nlog.ErrorException("CacheWrapper:GetAllQOHmorethnZeroItems::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace("CacheWrapper:GetAllQOHmorethnZeroItems::Leaving");
            }

        }




    }
}