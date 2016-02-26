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
    public class KTAuditInstance
    {
        #region Attributes
        
        private long instanceID;      
        private string auditName = string.Empty;
        
        #endregion Attributes
        
        #region Constructor
        public KTAuditInstance()
        { 
        
        }
        public KTAuditInstance(int instanceID, string auditName)
        {
            this.instanceID = instanceID;
            this.auditName = auditName;
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
        
        #endregion Properties
    }
}
