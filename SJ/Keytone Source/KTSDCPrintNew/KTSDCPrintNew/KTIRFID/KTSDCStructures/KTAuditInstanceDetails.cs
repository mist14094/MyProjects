using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace KTone.Core.KTIRFID
{    
   
    /// <summary>
    ///  AuditName , InstanceID
    /// </summary>
    [Serializable]
    [DataContract]
    public class KTAuditInstanceDetails
    {
        #region Attributes
        
        private long instanceID, totalItem , foundItem;      
        private string auditName = string.Empty;
        
        #endregion Attributes
        
        #region Constructor
        public KTAuditInstanceDetails()
        { 
        
        }
        public KTAuditInstanceDetails(long instanceID, string auditName,long totalItem , long foundItem)
        {
            this.instanceID = instanceID;
            this.auditName = auditName;
            this.totalItem = totalItem;
            this.foundItem = foundItem;
        }
        #endregion Constructor

        #region Properties

        /// <summary>
        /// Returns InstanceID
        /// </summary>
        /// 
        [DataMember]
        public long InstanceID
        {
            get
            {
                return this.instanceID;
            }
            set { instanceID = value; }
        }      

        /// <summary>
        /// Returns AuditName
        /// </summary>
        /// 
        [DataMember]
        public string AuditName
        {
            get
            {
                return this.auditName;
            }
            set { auditName = value; }
        }


        /// <summary>
        /// Returns TotalItem
        /// </summary>
        /// 
        [DataMember]
        public long TotalItem
        {
            get
            {
                return this.totalItem;
            }
            set { totalItem = value; }
        }


        /// <summary>
        /// Returns FoundItem
        /// </summary>
        /// 
        [DataMember]
        public long FoundItem
        {
            get
            {
                return this.foundItem;
            }
            set { foundItem = value; }
        }   

        #endregion Properties
    }
}
