using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace KTone.Core.KTIRFID
{

    public enum ItemCategory
    {
        Company,
        Product,
        SKU,
    }   
   
    /// <summary>
    ///  Company,Product,SKU details
    /// </summary>
    [Serializable]
    [DataContract]
    public class KTLocationMonitor
    {
        #region Attributes
        private int companyID;
        private long  productID,skuID;
        private string companyName = string.Empty, productName = string.Empty, skuName = string.Empty,
            locationaName = string.Empty, stencilData = string.Empty, locationzone = string.Empty;
        byte[] locationimage;
        private int count;
        private int locationID;
        #endregion Attributes
        
        #region Constructor
        public KTLocationMonitor()
        { 
        
        }
        public KTLocationMonitor(int companyID, long productID, long skuID, string companyName, string productName,string skuName,int count, string locationaName, int locationID,string stencilData,string locationzone,byte[] locationimage)
        {
            this.companyID = companyID;
            this.productID = productID;
            this.skuID = skuID;
            this.companyName = companyName;
            this.productName = productName;
            this.skuName = skuName;
            this.count = count;
            this.locationaName = locationaName;
            this.locationID = locationID;
            this.stencilData = stencilData;
            this.locationzone = locationzone;
            this.locationimage = locationimage;
        }
        #endregion Constructor

        #region Properties

        /// <summary>
        /// Returns CompanyID
        /// </summary>
        /// 
        [DataMember]
        public int CompanyID
        {
            get
            {
                return this.companyID;
            }
            set { companyID = value; }
        }

        /// <summary>
        /// Returns ProductID
        /// </summary>
        /// 
        [DataMember]
        public long ProductID
        {
            get
            {
                return this.productID;
            }
            set { productID = value; }
        }

        /// <summary>
        /// Returns SKUID
        /// </summary>
        /// 
        [DataMember]
        public long SKUID
        {
            get
            {
                return this.skuID;
            }
            set { skuID = value; }
        }

        /// <summary>
        /// Returns CompanyNmae
        /// </summary>
        /// 
        [DataMember]
        public string  CompanyName
        {
            get
            {
                return this.companyName;
            }
            set { companyName = value; }
        }

        /// <summary>
        /// Returns ProductNmae
        /// </summary>
        /// 
        [DataMember]
        public string ProductName
        {
            get
            {
                return this.productName;
            }
            set { productName = value; }
        }

        /// <summary>
        /// Returns SKUName
        /// </summary>
        /// 
        [DataMember]
        public string SKUName
        {
            get
            {
                return this.skuName;
            }
            set { skuName = value; }
        }
        /// <summary>
        /// Returns Count
        /// </summary>
        /// 
        [DataMember]
        public int Count
        {
            get
            {
                return this.count;
            }
            set { count = value; }
        }
        /// <summary>
        /// Returns LocationID
        /// </summary>
        /// 
        [DataMember]
        public int LocationID
        {
            get
            {
                return this.locationID;
            }
            set { locationID = value; }
        }

        /// <summary>
        /// Returns LocationName
        /// </summary>
        /// 
        [DataMember]
        public string LocationName
        {
            get
            {
                return this.locationaName;
            }
            set { locationaName = value; }
        }

        /// <summary>
        /// Returns LocationZone
        /// </summary>
        /// 
        [DataMember]
        public string LocationZone
        {
            get
            {
                return this.locationzone;
            }
            set { locationzone = value; }
        }


        /// <summary>
        /// Returns StencilData
        /// </summary>
        /// 
        [DataMember]
        public string StencilData
        {
            get
            {
                return this.stencilData;
            }
            set { stencilData = value; }
        }


        /// <summary>
        /// Returns LocationImage
        /// </summary>
        /// 
        [DataMember]
        public byte[] LocationImage
        {
            get
            {
                return this.locationimage;
            }
            set { locationimage = value; }
        }
        #endregion Properties
    }
}
