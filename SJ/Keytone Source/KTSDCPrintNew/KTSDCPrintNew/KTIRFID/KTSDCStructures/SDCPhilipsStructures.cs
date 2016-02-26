using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace KTone.Core.KTIRFID
{

    public enum ItemState
    {
        MFG_OUT,
        RDC_IN,
        RDC_OUT,
        DISSOCIATION_BIN
    }


    /// <summary>
    /// Philips Specific structures : KTLocationItemCount
    /// </summary>
    [Serializable]
    [DataContract]

    public class KTLocationItemCount
    {
        #region Attributes

            private bool _isAssociated;
            private int _itemCount, _dataOwnerID;
            private string _locationName = string.Empty;                     
        #endregion Attributes

        #region Constructor
            public KTLocationItemCount(string locationName, int itemCount , bool isAssociated)
            {
                this._locationName = locationName;
                this._itemCount = itemCount;
                this._isAssociated = isAssociated;                
            }
        #endregion Constructor

        #region Properties
            
            /// <summary>
            /// Returns IsAssociated
            /// </summary>
            /// 
            [DataMember]
            public bool IsAssociated
            {
                get
                {
                    return this._isAssociated;
                }
                set
                {
                    this._isAssociated = value;
                }
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
                    return this._locationName;
                }
                set
                {
                    this._locationName = value;
                }
            }

            /// <summary>
            /// Returns ItemCount
            /// </summary>
            /// 
            [DataMember]
            public int ItemCount
            {
                get
                {
                    return this._itemCount;
                }
                set
                {
                    this._itemCount = value;
                }
            }

            /// <summary>
            /// Returns DataOwnerID
            /// </summary>
            /// 
            [DataMember]
            public int DataOwnerID
            {
                get
                {
                    return this._dataOwnerID;
                }
                set
                {
                    this._dataOwnerID = value;
                }
            }



        #endregion Properties
    }

    
}
