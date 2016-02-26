using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using KTone.DAL.KTDBBaseLib;
using KTone.Core.Utils.XCrypt;
using KTone.RFIDGlobal;
using KTone.Core.KTIRFID;

namespace KTone.DAL.SmartDCDataAccess
{
    public class SmartDCLD
    {

        private static Dictionary<string, SmartDCLD> _objLicDetailList = new Dictionary<string, SmartDCLD>();

        NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();
        Int64 _maxSKUCount = 0;
        Int64 _maxPrintersCount = 0;
        Int64 _maxHandHeldCount = 0;
        Int64 _maxReadersCount = 0;
        bool _isEval = true;
        string _licenseType = "EVALUATION";
        Int32 _noOfDaysRemaining = 0;
        private string productBranch = "SMARTDC";

        DateTime _firstAccessDate = DateTime.Now;

        private SmartDCLD(string productBranch)
        {
            this.productBranch = productBranch;
            GetLicenseDetail();
        }
        private SmartDCLD()
        {

        }
        public static void Reset()
        {
            if (_objLicDetailList != null)
                _objLicDetailList.Clear();
        }
        public static SmartDCLD GetInstance()
        {
            return new SmartDCLD();
        }
        public static SmartDCLD GetInstance(string productBranch, string conString)
        {
            
            if (!_objLicDetailList.ContainsKey(productBranch))
            {
                DBInteractionBase.DBConnString = conString;
                SmartDCLD objLicDetail = new SmartDCLD(productBranch);
                _objLicDetailList[productBranch] = objLicDetail;
            }

            return _objLicDetailList[productBranch];
        }

        public static void RemoveInstance(string productBranch)
        {
            if (_objLicDetailList.ContainsKey(productBranch))
            {
                SmartDCLD obj = _objLicDetailList[productBranch];
                obj = null;

                _objLicDetailList.Remove(productBranch);
            }
        }

        public bool UpdateLicense(string licContent)
        {
            SqlConnection conn = null;
            try
            {
                log.Trace("Entering SmartDCLD:UpdateLicense");
                conn = new SqlConnection(DBInteractionBase.DBConnString);
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Update SmartDCLD SET [LDContent]='" + licContent + "' , [InitOn] = NULL,[CreateDate] = '" + DateTime.Now + "',[lastAccessOn] = NULL";
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                log.ErrorException("UpdateLicense failed.", ex);
                throw ex;
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
            GetLicenseDetail();
            return true;
        }

        private void GetLicenseDetail()
        {
            string errorMessage = string.Empty;
            string msg = string.Empty;
            SqlConnection conn = null;
            string liContent = string.Empty;
            string initDate = string.Empty;
            string lastAccessOn = string.Empty;

            try
            {
                log.Trace("Entering SmartDCLD:GetLicenseDetail");
                string strSQL = string.Empty;
                strSQL = "Select TOP 1 ldcontent, initon, lastAccessOn From SmartDCLD ";
                conn = new SqlConnection(DBInteractionBase.DBConnString);
                SqlDataAdapter da = new SqlDataAdapter(strSQL, conn);
                DataTable dtLicDetail = new DataTable();
                da.Fill(dtLicDetail);
                if (dtLicDetail.Rows.Count <= 0)
                {
                    errorMessage = "License does not exists.";
                    log.Trace(errorMessage);
                    throw new ApplicationException(errorMessage);
                }
                liContent = dtLicDetail.Rows[0]["ldcontent"].ToString().Trim();
                initDate = dtLicDetail.Rows[0]["initon"].ToString().Trim();
                lastAccessOn = dtLicDetail.Rows[0]["lastAccessOn"].ToString().Trim();

                if (initDate == null || initDate.Equals(string.Empty))
                {
                    _firstAccessDate = DateTime.Now;
                    //update first access date
                    initDate = Crypting.EncryptText(_firstAccessDate.ToLongDateString());
                    if (conn.State != ConnectionState.Open)
                        conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "Update SmartDCLD SET initon='" + initDate + "', lastAccessOn = '" + initDate + "' ";
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    initDate = Crypting.DecryptText(initDate);

                    // _firstAccessDate = Convert.ToDateTime(initDate);
                }
                log.Trace("SmartDCLD:dtLastAccessed:" + lastAccessOn);
                if (lastAccessOn != null && !lastAccessOn.Equals(string.Empty))
                {
                    DateTime dtLastAccessed = DateTime.MinValue;
                    try
                    {
                        lastAccessOn = Crypting.DecryptText(lastAccessOn);
                        dtLastAccessed = Convert.ToDateTime(lastAccessOn);
                        log.Trace("SmartDCLD:dtLastAccessed:" + dtLastAccessed);
                    }
                    catch (Exception ex)
                    {
                        log.ErrorException("Unable to parse lastAccessed Time", ex);
                        SShield.RestartFactory = false;
                        SShield.SystemEvents_TimeChanged(null, null);
                        SShield.RestartFactory = true;
                    }
                    log.Trace("SmartDCLD:dtLastAccessed:" + dtLastAccessed + " Curr date:" + DateTime.Now);
                    if (dtLastAccessed > DateTime.Now)
                    {
                        SShield.RestartFactory = false;
                        SShield.SystemEvents_TimeChanged(null, null);
                        SShield.RestartFactory = true;
                    }
                }


                log.Trace("Entering LicenseManager.GetInstance() method");

                LicenseManager _licManager = null;
                _licManager = LicenseManager.GetInstance(liContent, productBranch);

                log.Trace("Leaving LicenseManager.GetInstance() method");
                //licMgr.IsLicValidOnSystem;
                KTLicenseData licData = _licManager.KTLicenseData;
                _isEval = licData.ExpiryDetails.IsEvalLicense;
                log.Trace("Licence Type : " + licData.ExpiryDetails.LicenseType);

                if (!_isEval)
                {
                    log.Trace("Readers Count : " + licData.ReaderDetailsInfo.TotalReaderCount);
                    _maxReadersCount = licData.ReaderDetailsInfo.TotalReaderCount;

                    Dictionary<string, string> productSpecificDetails = licData.ProductSpecificDetails;
                    log.Trace("ProductSpecificDetails : " + licData.ProductSpecificDetails);
                    if (productSpecificDetails == null)// || !productSpecificDetails.ContainsKey("NoOfAssets"))
                    {
                        errorMessage = "License does not contain product Specific details.";
                        log.Trace(errorMessage);
                        throw new ApplicationException(errorMessage);
                    }

                    if (productSpecificDetails.ContainsKey("NoOfSKUs"))
                    {
                        try
                        {
                            if (productSpecificDetails["NoOfSKUs"] != "")
                            {
                                _maxSKUCount = Convert.ToInt64(productSpecificDetails["NoOfSKUs"]);
                            }
                            log.Trace("Number of SKUs " + _maxSKUCount);

                        }
                        catch (Exception ex)
                        {
                            errorMessage = "License check failed." + ex.Message;
                            log.ErrorException("License check failed.", ex);
                            throw;
                        }
                    }
                    else
                        throw new ApplicationException("Invalid license.");

                    if (productSpecificDetails.ContainsKey("NoOfHandhelds"))
                    {
                        try
                        {
                            if (productSpecificDetails["NoOfHandhelds"] != "")
                            {
                                _maxHandHeldCount = Convert.ToInt64(productSpecificDetails["NoOfHandhelds"]);
                            }
                            log.Trace("Number of handhelds " + _maxHandHeldCount);
                        }
                        catch (Exception ex)
                        {
                            errorMessage = "License check failed." + ex.Message;
                            log.ErrorException("License check failed.", ex);
                            throw;
                        }
                    }
                    if (productSpecificDetails.ContainsKey("NoOfPrinters"))
                    {
                        try
                        {
                            if (productSpecificDetails["NoOfPrinters"] != "")
                            {
                                _maxPrintersCount = Convert.ToInt64(productSpecificDetails["NoOfPrinters"]);
                            }
                            log.Trace("Number of Printers " + _maxPrintersCount);
                        }
                        catch (Exception ex)
                        {
                            errorMessage = "License check failed." + ex.Message;
                            log.ErrorException("License check failed.", ex);
                            throw;
                        }
                    }
                    else
                        throw new ApplicationException("Invalid license.");
                }
                try
                {
                    _isEval = licData.ExpiryDetails.IsEvalLicense;
                    _licenseType = licData.ExpiryDetails.LicenseType;
                    if (_isEval)
                    {
                        _noOfDaysRemaining = Convert.ToInt32(licData.ExpiryDetails.NoOfDaysRemaining);
                    }
                }
                catch { log.Error("RefreshLicenseDetail:: error while reading expiry details"); }


                try
                {
                    _firstAccessDate = DateTime.Now;
                    //update first access date
                    string lastAccesDate = Crypting.EncryptText(_firstAccessDate.ToLongDateString());
                    if (conn.State != ConnectionState.Open)
                        conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "Update SmartDCLD SET  LastAccessOn = '" + lastAccesDate + "' ";
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                }
                catch { }

            }
            catch (Exception ex)
            {
                log.ErrorException("License check failed.", ex);
                throw ex;
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }

        /// <summary>
        /// If wvalk then validates if its expired
        /// </summary>
        public bool CheckLicenseExpiry(out string errorMessage)
        {
            log.Debug("Entering");
            errorMessage = string.Empty;
            string msgStr = string.Empty;
            if (_isEval)
            {
                int noOfDaysRemaining = _noOfDaysRemaining;
                TimeSpan timeElapsed = DateTime.Now - _firstAccessDate;
                noOfDaysRemaining = noOfDaysRemaining - timeElapsed.Days;
                if (noOfDaysRemaining <= 0)
                {
                    noOfDaysRemaining = 0;
                    msgStr = "Evaluation license is expired, please contact your administrator.";
                    errorMessage = msgStr;
                    log.Error(msgStr);

                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Validates if any more handheld is allowed 
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool MoreHandHeldAllowed(out string errMsg)
        {
            //-------------Licence Check--------------------------
            log.Trace("Entering License Check for HandHeld.");
            bool addHHAllowed = false;
            errMsg = string.Empty;
            if (!_isEval)
            {
                if (_maxHandHeldCount <= 0)
                {
                    errMsg = "Invalid license.";
                    return addHHAllowed;
                }

                Int32 dbLiveHHCount = 0;
                Int32 dbSystemHHCount = 0;
                try
                {
                    KTone.DAL.SmartDCDataAccess.HandheldDevice handheld = new KTone.DAL.SmartDCDataAccess.HandheldDevice();
                    handheld.HandHeldSummary(out dbLiveHHCount, out dbSystemHHCount);

                    if (dbLiveHHCount >= _maxHandHeldCount)
                    {
                        errMsg = "Maximum live handhelds allowed limit: " + _maxHandHeldCount.ToString() + " is reached, Please contact administrator for more details.";
                        return addHHAllowed;
                    }
                    if (dbSystemHHCount >= _maxHandHeldCount)//OLD 2 * (_maxHandHeldCount)
                    {
                        errMsg = "Maximum handheld in system allowed limit: " + dbSystemHHCount.ToString() + " is reached, Please contact administrator for more details.";
                        return addHHAllowed;
                    }
                }
                catch (Exception ex)
                {
                    log.Error("SmartDCLD:MoreHandHeldAllowed:: " + ex.Message);
                    throw ex;
                }
            }

            addHHAllowed = true;
            return addHHAllowed;
        }

        #region license details
        public Int64 MaxSKUsAllowed
        {
            get
            {
                return _maxSKUCount;
            }
        }

        public Int64 MaxHandHeldAllowed
        {
            get
            {
                return _maxHandHeldCount;
            }
        }
        public Int64 MaxPrintersAllowed
        {
            get
            {
                return _maxPrintersCount;
            }
        }
        public Int64 MaxReadersAllowed
        {
            get
            {
                return _maxReadersCount;
            }
        }

        public string LicenseType
        {
            get
            {
                return _licenseType;
            }
        }

        public bool Evaluation
        {
            get
            {
                return _isEval;
            }
        }

        public Int32 NoOfDaysRemaining
        {
            get
            {
                try
                {
                    int noOfDaysRemaining = _noOfDaysRemaining;
                    TimeSpan timeElapsed = DateTime.Now - _firstAccessDate;
                    noOfDaysRemaining = noOfDaysRemaining - timeElapsed.Days;
                    return noOfDaysRemaining;
                }
                catch { return 0; }
            }
        }

        public DateTime ExpiryDate
        {
            get
            {
                try
                {

                    return _firstAccessDate.AddDays(_noOfDaysRemaining);
                }
                catch { return DateTime.Now; }
            }
        }

        #endregion license details
    }
}
