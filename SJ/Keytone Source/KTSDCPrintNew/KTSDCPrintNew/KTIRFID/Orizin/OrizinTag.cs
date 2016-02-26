using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace KTone.Core.KTIRFID
{
    [Serializable]
    public class  OrizinTag:RFIDTag, ICloneable
    {
        #region Attributes
        private string uniqueId;
        private short rssiValue;
        #endregion
        public OrizinTag(string uniqueID,short rssi)
        {
            this.uniqueId = uniqueID;
            this.rssiValue = rssi;
        }

        #region Properties
        public string UniqueId
        {
            get
            {
                return this.uniqueId;
            }
        }
        public short RSSI
        {
            get 
            {
                return this.rssiValue;
            }
        }

        public override byte[] TagSN
        {
            get
            {
                return tagSNArray;
            }
            set
            {
                
                byte[] tagSN = value;
                tagSNArray = new byte[tagSN.Length];
                Array.Copy(tagSN, tagSNArray, tagSN.Length);
                tagIdInitialized = true;
            }
        }
        #endregion Properties


        #region ICloneable Members

        public object Clone()
        {
            MemoryStream ms = new MemoryStream();

            BinaryFormatter bf = new BinaryFormatter();

            bf.Serialize(ms, this);

            ms.Position = 0;

            object obj = bf.Deserialize(ms);

            ms.Close();

            return obj;
        }

        /// <summary>
        ///  Make a deep copy
        /// </summary>
        /// <returns></returns>
        public ActiveWaveTag CloneTag()
        {
            return (ActiveWaveTag)Clone();
        }

        #endregion
    }
}
