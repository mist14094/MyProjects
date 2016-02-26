/********************************************************************************************************
Copyright (c) 2010 KeyTone Technologies.All Right Reserved

KeyTone's source code and documentation is copyrighted and contains proprietary information.
Licensee shall not modify, create any derivative works, make modifications, improvements, 
distribute or reveal the source code ("Improvements") to anyone other than the software 
developers of licensee's organization unless the licensee has entered into a written agreement
("Agreement") to do so with KeyTone Technologies Inc. Licensee hereby assigns to KeyTone all right,
title and interest in and to such Improvements unless otherwise stated in the Agreement. Licensee 
may not resell, rent, lease, or distribute the source code alone, it shall only be distributed in 
compiled component of an application within the licensee'organization. 
The licensee shall not resell, rent, lease, or distribute the products created from the source code
in any way that would compete with KeyTone Technologies Inc. KeyTone' copyright notice may not be 
removed from the source code.
   
Licensee may be held legally responsible for any infringement of intellectual property rights that
is caused or encouraged by licensee's failure to abide by the terms of this Agreement. Licensee may 
make copies of the source code provided the copyright and trademark notices are reproduced in their 
entirety on the copy. KeyTone reserves all rights not specifically granted to Licensee. 
 
Use of this source code constitutes an agreement not to criticize, in any way, the code-writing style
of the author, including any statements regarding the extent of documentation and comments present.

THE SOFTWARE IS PROVIDED "AS IS" BASIS, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING 
BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER  LIABILITY, 
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE
OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. KEYTONE TECHNOLOGIES SHALL NOT BE LIABLE FOR ANY DAMAGES 
SUFFERED BY LICENSEE AS A RESULT OF USING, MODIFYING OR DISTRIBUTING THIS SOFTWARE OR ITS DERIVATIVES.
********************************************************************************************************/
 
using System;

using System.Collections.Generic;
using System.Text;

namespace KTone.Core.KTIRFID
{
    #region [ ALL CLASSES ]

    //+++++++++++++++++++++++++++++++++++++++++++++++

    #region [ Company details ]
    /// <summary>
    /// Contains Company Details
    /// </summary>
    [Serializable]
    public class CompanyDetails
    {
        #region [ EnterpriseID ]
        private string mEnterpriseID;
        /// <summary>
        /// Contains Company Id
        /// </summary>
        public string EnterpriseID
        {
            get { return mEnterpriseID; }
            set { mEnterpriseID = value; }
        }

        #endregion [ EnterpriseID ]

        #region [ CompName ]
        private string mCompanyName;
        /// <summary>
        /// Contains Company Name
        /// </summary>
        public string CompanyName
        {
            get { return mCompanyName; }
            set { mCompanyName = value; }
        }
        #endregion [ CompName ]

        #region [ Address1 ]
        private string mAddress1 = string.Empty;
        public string Address1
        {
            get { return mAddress1; }
            set { mAddress1 = value; }
        }
        #endregion [ Address1 ]

        #region [ Address2 ]
        private string mAddress2 = string.Empty;
        public string Address2
        {
            get { return mAddress2; }
            set { mAddress2 = value; }
        }
        #endregion [ Address2 ]

        #region [ Country ]
        private string mCountry;
        public string Country
        {
            get { return mCountry; }
            set { mCountry = value; }
        }
        #endregion [ Country ]

        #region [ STATE ]
        private string mState = string.Empty;
        public string State
        {
            get { return mState; }
            set { mState = value; }
        }
        #endregion [ STATE ]

        #region [ CITY ]
        private string mCity;
        public string City
        {
            get { return mCity; }
            set { mCity = value; }
        }
        #endregion [ CITY ]

        #region [ ZIP ]
        private string mZip = string.Empty;
        public string Zip
        {
            get { return mZip; }
            set { mZip = value; }
        }
        #endregion [ ZIP ]

        #region [ PHONE ]
        private string mPhoneNo;
        /// <summary>
        /// 
        /// </summary>
        public string PhoneNo
        {
            get { return mPhoneNo; }
            set { mPhoneNo = value; }
        }
        #endregion [ PHONE ]

        #region [ SEC PHONE ]
        private string mSecPhoneNo;
        /// <summary>
        /// 
        /// </summary>
        public string PhoneNoSecondary
        {
            get { return mSecPhoneNo; }
            set { mSecPhoneNo = value; }
        }
        #endregion [ SEC PHONE ]

        #region [ CompEMAIL ]
        private string mEmail;
        /// <summary>
        /// Contains Company Email Id
        /// </summary>
        public string Email
        {
            get { return mEmail; }
            set { mEmail = value; }
        }
        #endregion [ CompEMAIL ]
    }
    #endregion [ Company details ]

    #region [ Product Details ]

    /// <summary>
    /// Information about the KTone's products like its Name/Ver, where it is available (URL)
    /// version release date.
    /// </summary>
    [Serializable]
    internal class ProductDetails
    {
        private string mProductId = string.Empty;
        private string mProductSerialId = string.Empty;
        private string mProductDesc = string.Empty;
        private string mProductName = string.Empty;
        private string mProductVer = string.Empty;
        private string mPatch = string.Empty;
        private string mProductLocation = string.Empty;
        private string mProductReleaseDate = string.Empty;
        private string mCheckSumMD5 = string.Empty;

        #region Properties

        public string ProductId
        {
            get { return mProductId; }
            set { mProductId = value; }
        }

        public string ProductSerialId
        {
            get { return mProductSerialId; }
            set { mProductSerialId = value; }
        }

        public string ProductName
        {
            get { return mProductName; }
            set { mProductName = value; }
        }

        public string ProductDescription
        {
            get { return mProductDesc; }
            set { mProductDesc = value; }
        }

        public string ProductVer
        {
            get { return mProductVer; }
            set { mProductVer = value; }
        }

        public string Patch
        {
            get { return mPatch; }
            set { mPatch = value; }
        }

        public string ProductLocation
        {
            get { return mProductLocation; }
            set { mProductLocation = value; }
        }

        public string ProductReleaseDate
        {
            get { return mProductReleaseDate; }
            set { mProductReleaseDate = value; }
        }

        public string CheckSumMD5
        {
            get { return mCheckSumMD5; }
            set { mCheckSumMD5 = value; }
        }
        #endregion Properties
    }

    #endregion [ Product Details ]

    #region [ Reader details ]
    /// <summary>
    /// Conatains Reader Related deatils like Vendor and Model Information
    /// </summary>
    [Serializable]
    public class ModelLIC
    {
        private string mModelName;
        /// <summary>
        /// 
        /// </summary>
        public string ModelName
        {
            get { return mModelName; }
            set { mModelName = value; }
        }

        private string mVendorName;
        /// <summary>
        /// 
        /// </summary>
        public string VendorName
        {
            get { return mVendorName; }
            set { mVendorName = value; }
        }

        private int mNumOfModel;
        /// <summary>
        /// 
        /// </summary>
        public int NumOfModel
        {
            get { return mNumOfModel; }
            set { mNumOfModel = value; }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ReaderLIC
    {
        private string mVendorName;
        /// <summary>
        /// 
        /// </summary>
        public string VendorName
        {
            get { return mVendorName; }
            set { mVendorName = value; }
        }

        private bool mIsAllModelsAllowed;
        /// <summary>
        /// 
        /// </summary>
        public bool IsAllModelsAllowed
        {
            get { return mIsAllModelsAllowed; }
            set { mIsAllModelsAllowed = value; }
        }

        private ModelLIC[] arrModels;
        /// <summary>
        /// 
        /// </summary>
        public ModelLIC[] Models
        {
            get { return arrModels; }
            set { arrModels = value; }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ReaderDetails
    {
        private int mTotalReaderCount;
        /// <summary>
        /// 
        /// </summary>
        public int TotalReaderCount
        {
            get { return mTotalReaderCount; }
            set { mTotalReaderCount = value; }
        }

        private bool mIsUnlimitedReaders;
        /// <summary>
        /// 
        /// </summary>
        public bool IsUnlimitedReaders
        {
            get { return mIsUnlimitedReaders; }
            set { mIsUnlimitedReaders = value; }
        }

        private bool mIsAllVendorAllowed;
        /// <summary>
        /// 
        /// </summary>
        public bool IsAllVendorAllowed
        {
            get { return mIsAllVendorAllowed; }
            set { mIsAllVendorAllowed = value; }
        }

        private ReaderLIC[] mReaders;
        /// <summary>
        /// 
        /// </summary>
        public ReaderLIC[] Readers
        {
            get { return mReaders; }
            set { mReaders = value; }
        }
    }
    #endregion [ Reader details ]

    #region [ Printer details ]
    /// <summary>
    /// Contains Printer Related details like total count of Printers Information
    /// </summary> 
    
    [Serializable]
    public class PrinterDetails
    {
        private int mTotalPrinterCount;
        /// <summary>
        /// 
        /// </summary>
        public int TotalPrinterCount
        {
            get { return mTotalPrinterCount; }
            set { mTotalPrinterCount = value; }
        } 
    }
    #endregion [ Printer details ]

    #region [ Handheld details ]
    /// <summary>
    /// Conatains Handheld Related details.
    /// </summary>
    
    [Serializable]
    public class HandheldDetails
    {
        private int mTotalHandheldCount;
        /// <summary>
        /// 
        /// </summary>
        public int TotalHandheldCount
        {
            get { return mTotalHandheldCount; }
            set { mTotalHandheldCount = value; }
        }
 
    }
    #endregion [ Handheld details ]

    #region [ Registration details ]
    [Serializable]
    public class RegistrationDetails
    {
        public RegistrationDetails(string LicDetailId)
        {
            mLicDetailId = LicDetailId;
        }

        public RegistrationDetails()
        {
           // mLicDetailId = LicDetailId;
        }

        #region [ LIC-DETAIL ID ]
        private string mLicDetailId = string.Empty;
        public string LicDetailId
        {
            get { return mLicDetailId; }
            /// NOTE: - LicDetailId will be read only.
        }
        #endregion [ LIC-DETAIL ID ]

        #region [ MACHINE ID ]
        private string mMachineId = string.Empty;
        public string MachineId
        {
            get { return mMachineId; }
            set { mMachineId = value; }
        }
        #endregion [ MACHINE ID ]

        #region [ LIC KEY ]
        private string mLicenseKey = string.Empty;
        public string LicenseKey
        {
            get { return mLicenseKey; }
            set { mLicenseKey = value; }
        }
        #endregion [ LIC KEY ]

        #region [ LIC-Registration DATE ]
        private DateTime mLicRegistrationDate;
        /// <summary>
        /// 
        /// </summary>
        public DateTime LicRegistrationDate
        {
            get { return mLicRegistrationDate; }
            set { mLicRegistrationDate = value; }
        }
        #endregion [ LIC-Registration DATE ]

        #region [ LIC-UnRegister DATE ]
        private DateTime mLicUnRegisterDate;
        /// <summary>
        /// 
        /// </summary>
        public DateTime LicUnRegisterDate
        {
            get { return mLicUnRegisterDate; }
            set { mLicUnRegisterDate = value; }
        }
        #endregion [ LIC-UnRegister DATE ]

        #region [ LIC REGISTER BY ]
        private string mLicRegisterBy = string.Empty;
        public string LicRegisterBy
        {
            get { return mLicRegisterBy; }
            set { mLicRegisterBy = value; }
        }
        #endregion [ LIC REGISTER BY ]

    }
    #endregion [ Registration details ]

    #region [ Purchase Details ]
    [Serializable]
    public class PurchaseDetails
    {
        public PurchaseDetails(string LicId)
        {
            mLicId = LicId;
        }

        #region [ License ID ]
        private string mLicId = string.Empty;
        public string LicId
        {
            get { return mLicId; }
            /// NOTE: - Reader ONLY Field
        }
        #endregion [ License ID ]

        #region [ Comp-ID ]
        private int mCompId = -1;
        public int CompanyId
        {
            get { return mCompId; }
            set { mCompId = value; }
        }
        #endregion [ Comp-ID ]

        #region [ PURCHASE BY NAME ]
        private string mPurchaseByName;
        /// <summary>
        /// 
        /// </summary>
        public string PurchaseByName
        {
            get { return mPurchaseByName; }
            set { mPurchaseByName = value; }
        }
        #endregion [ PURCHASE BY NAME ]

        #region [ EMAIL ]
        private string mEmail = string.Empty;
        public string Email
        {
            get { return mEmail; }
            set { mEmail = value; }
        }
        #endregion [ EMAIL ]

        #region [ PHONE 1 ]
        private string mPhone1 = string.Empty;
        public string Phone1
        {
            get { return mPhone1; }
            set { mPhone1 = value; }
        }
        #endregion [ PHONE 1 ]

        #region [ PHONE 2 ]
        private string mPhone2 = string.Empty;
        public string Phone2
        {
            get { return mPhone2; }
            set { mPhone2 = value; }
        }
        #endregion [ PHONE 2 ]

        #region [ PURCHASE DATE ]
        private DateTime mPurchaseDate;
        public DateTime PurchaseDate
        {
            get { return mPurchaseDate; }
            set { mPurchaseDate = value; }
        }
        #endregion [ PURCHASE DATE ]

        #region [ Purchase USER-ID]
        private string mPurchaseUserID;
        /// <summary>
        /// Contains User ID
        /// </summary>
        public string PurchaseUserID
        {
            get { return mPurchaseUserID; }
            set { mPurchaseUserID = value; }
        }
        #endregion [ USER-ID]

        #region [ USER NAME ]
        /*
        private string mUserName = string.Empty;
        public string UserName
        {
            get { return mUserName; }
            set { mUserName = value; }
        }*/
        #endregion [ USER NAME ]

        #region [ Product SERIAL - ID ]
        private string mProductSerialId = string.Empty;
        public string ProductSerialId
        {
            get { return mProductSerialId; }
            set { mProductSerialId = value; }
        }
        #endregion [ Product SERIAL - ID ]

        #region [ Num Of Retry Allowed ]
        private int mNumOfRetryAllowed = 1;
        public int NumOfRetryAllowed
        {
            get { return mNumOfRetryAllowed; }
            set { mNumOfRetryAllowed = value; }
        }
        #endregion [ Num Of Retry Allowed ]

        #region [ Retry Count ]
        private int mRetryCount = -1;
        public int RetryCount
        {
            get { return mRetryCount; }
            set { mRetryCount = value; }
        }
        #endregion [ Retry Count ]

        #region [ LIC XML ]
        private string mLicXml = string.Empty;
        public string LicXml
        {
            get { return mLicXml; }
            set { mLicXml = value; }
        }
        #endregion [ LIC XML ]

        #region [ LIC CREATED BY ]
        private string mCreatedById = string.Empty;
        public string CreatedById
        {
            get { return mCreatedById; }
            set { mCreatedById = value; }
        }
        #endregion [ LIC CREATED BY ]

        #region [ LIC-CreatedOnDate ]
        private DateTime mLicCreatedDate;
        /// <summary>
        /// 
        /// </summary>
        public DateTime LicenseCreatedDate
        {
            get { return mLicCreatedDate; }
            set { mLicCreatedDate = value; }
        }
        #endregion [ LIC-CreatedOnDate ]

        #region [ LIC-Support Type ]
        private string mLicSupportType;
        /// <summary>
        /// 
        /// </summary>
        public string LicSupportType
        {
            get { return mLicSupportType; }
            set { mLicSupportType = value; }
        }
        #endregion [ LIC-Support Type ]

        #region [ LIC-Support Expiry DATE ]
        private DateTime mLicSupportExpiryDate;
        /// <summary>
        /// 
        /// </summary>
        public DateTime LicSupportExpiryDate
        {
            get { return mLicSupportExpiryDate; }
            set { mLicSupportExpiryDate = value; }
        }
        #endregion [ LIC-Support Expiry DATE ]

        #region [ IS EVAL LIC ]
        private bool mIsLicForEvaluation;
        /// <summary>
        /// 
        /// </summary>
        public bool IsLicForEvaluation
        {
            get { return mIsLicForEvaluation; }
            set { mIsLicForEvaluation = value; }
        }

        #endregion [ IS EVAL LIC ]

        #region [ LIC TYPE ]
        private string mLicenseType;
        /// <summary>
        /// 
        /// </summary>
        public string LicenseType
        {
            get { return mLicenseType; }
            set { mLicenseType = value; }
        }
        #endregion [ LIC TYPE ]

        #region [ LIC-COMMENTS ]
        private string mLicComments;
        /// <summary>
        /// 
        /// </summary>
        public string LicComments
        {
            get { return mLicComments; }
            set { mLicComments = value; }
        }
        #endregion [ LIC-COMMENTS ]
    }

    #endregion [ Purchase Details ]

    #region [ Expiry details ]

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ExpiryDetailsData
    {
        private bool mIsPerpetual;
        /// <summary>
        /// 
        /// </summary>
        public bool IsPerpetual
        {
            get { return mIsPerpetual; }
            set { mIsPerpetual = value; }
        }

        private DateTime mExpiryDate;
        /// <summary>
        /// 
        /// </summary>
        public DateTime ExpiryDate
        {
            get { return mExpiryDate; }
            set { mExpiryDate = value; }
        }

        private string m_NoOfDaysRemaining = string.Empty;

        /// <summary>
        /// For evaluation license
        /// </summary>
        public string NoOfDaysRemaining
        {
            get { return m_NoOfDaysRemaining; }
            set { m_NoOfDaysRemaining = value; }
        }

        private string mLicenseType;

        public string LicenseType
        {
            get { return mLicenseType; }
            set { mLicenseType = value; }
        }

        private string mLicenseKey;

        public string LicenseKey
        {
            get { return mLicenseKey; }
            set { mLicenseKey = value; }
        }

        private bool mIsEvalLicense;

        public bool IsEvalLicense
        {
            get { return mIsEvalLicense; }
            set { mIsEvalLicense = value; }
        }
    }

    #endregion [ Expiry details ]

    //+++++++++++++++++++++++++++++++++++++++++++++++

    #endregion [ ALL CLASSES ]
    /// <summary>
    /// Structure containing the License information.
    /// </summary>
    [Serializable]
    public class KTLicenseData
    {
        //private string mLicMsg = @"License is locked."; 
        private bool mIsMachineLockOpen = false;
        private const string SIGNATURE = @"kEytONetEcH";
        #region [ Constructor ]

        public KTLicenseData() { }
    
        /// <summary>
        /// Contains License Data as per license structure.
        /// </summary>
    /*    public KTLicenseData(string password, bool isLicenseUnLocked, string reason)
        {
            if (password == null )
            {
                throw new ApplicationException(@"Authentication failed to create License data instance.");
            }

            if ( password != SIGNATURE)
            {
                throw new ApplicationException(@"Authentication failed to create License data instance.");
            }

            if (isLicenseUnLocked == false)
            {
                this.mCompanyDetails = new CompanyDetails();
                this.mReaderDetails = new ReaderDetails();
                this.mRegistrationDetails = new RegistrationDetails("-1");
                this.mExpiryDetails = new ExpiryDetailsData();
            }

            if ( reason != null && reason.Length > 0 )
                this.mLicMsg = reason;
        }*/
        
        #endregion [ Constructor ]
        /// <summary>
        /// 
        /// </summary>
        public bool IsMachineLockOpen
        {
            get { return this.mIsMachineLockOpen; }
            set
            {
                //string assemblyName = System.Reflection.Assembly.GetEntryAssembly().FullName;

                //if (assemblyName.IndexOf(@"RFIDReaderWinService") < 0 )
                //  throw new ApplicationException(@"Access denied to set value of this property ");                    

                this.mIsMachineLockOpen = value;
            }
        }

        private CompanyDetails mCompanyDetails;
        /// <summary>
        /// 
        /// </summary>
        public CompanyDetails CompanyDetailsInfo
        {
            get { return mCompanyDetails; }
            set { mCompanyDetails = value; }
        }

        private ReaderDetails mReaderDetails;
        /// <summary>
        /// 
        /// </summary>
        public ReaderDetails ReaderDetailsInfo
        {
            get { return mReaderDetails; }
            set { mReaderDetails = value; }
        }

        private RegistrationDetails mRegistrationDetails;
        /// <summary>
        /// 
        /// </summary>
        public RegistrationDetails RegistrationDetailsData
        {
            get { return mRegistrationDetails; }
            set { mRegistrationDetails = value; }
        }

        private Dictionary<string, string> productSpecificDetails = null;
        public Dictionary<string, string> ProductSpecificDetails
        {
            get { return this.productSpecificDetails; }
            set { this.productSpecificDetails = value; }
        }
        private PurchaseDetails mPurchaseDetails;
        public PurchaseDetails PurchaseDetailsData
        {
            get { return mPurchaseDetails; }
            set { mPurchaseDetails = value; }
        }

        private ExpiryDetailsData mExpiryDetails;
        /// <summary>
        /// 
        /// </summary>
        public ExpiryDetailsData ExpiryDetails
        {
            get { return mExpiryDetails; }
            set { mExpiryDetails = value; }
        }

        //private PrinterDetails mprinterDetails;
        ///// <summary>
        ///// 
        ///// </summary>
        //public PrinterDetails PrinterDetailsData
        //{
        //    get { return mprinterDetails; }
        //    set { mprinterDetails = value; }
        //}

        //private HandheldDetails mhandheldDetails;
        ///// <summary>
        ///// 
        ///// </summary>
        //public HandheldDetails HandheldDetailsData
        //{
        //    get { return mhandheldDetails; }
        //    set { mhandheldDetails = value; }
        //}

    }
}
