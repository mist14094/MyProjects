using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace KTone.Core.KTIRFID
{
    /// <summary>
    /// Convergence reader tag (C1G2 Tags)
    /// </summary>
    [Serializable]
    public class CSTagC1G2_96BitTag : EPCClass1Gen2_96BitTag, ICloneable
    {
        #region Attributes
        private short rssiValue;
        private short phaseI;
        private short phaseQ;
        private UInt32 count = 0;
        private DateTime utcTimeStamp = DateTime.UtcNow;

        private UInt32 timestampSecond;
        private UInt32 timestampUS;

        #endregion Attributes

        #region Constructor

        /// <summary>
        /// Constructor to create CSTag object
        /// </summary>
        /// <param name="tagSN"></param>
        /// <param name="epcTagURN"></param>
        /// <param name="rssi"></param>
        /// <param name="phaseI"></param>
        /// <param name="phaseQ"></param>
        /// <param name="timestampSecond"></param>
        /// <param name="timestampUS"></param>
        public CSTagC1G2_96BitTag(byte[] tagSN, string epcTagURN, short rssi, short phaseI,
            short phaseQ, UInt32 timestampSecond, UInt32 timestampUS)
            : base(tagSN, epcTagURN)
        {
            this.rssiValue = rssi;
            this.phaseI = phaseI;
            this.phaseQ = phaseQ;

            this.timestampSecond = timestampSecond;
            this.timestampUS = timestampUS;
        }

        public CSTagC1G2_96BitTag(byte[] tagSN, string epcTagURN, short rssi, UInt32 count)
            : base(tagSN, epcTagURN)
        {
            this.rssiValue = rssi;
            this.count = count;

            this.UTCTimestamp = DateTime.UtcNow;
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// RSSI
        /// </summary>
        public short RSSI
        {
            get { return this.rssiValue; }
            set { this.rssiValue = value; }
        }

        /// <summary>
        /// PhaseI
        /// </summary>
        public short PhaseI
        {
            get { return this.phaseI; }
        }

        /// <summary>
        /// PhaseQ
        /// </summary>
        public short PhaseQ
        {
            get { return this.phaseQ; }
        }

        public UInt32 TimestampSecond
        {
            get { return this.timestampSecond; }
        }

        public UInt32 TimestampUS
        {
            get { return this.timestampUS; }
        }

        public DateTime UTCTimestamp
        {
            get { return this.utcTimeStamp; }
            set { this.utcTimeStamp = value; }
        }

        public UInt32 Count
        {
            get { return this.count; }
            set { this.count = value; }
        }


        #endregion Properties

        #region Public Methods


        #endregion Public Methods

        #region Private Methods


        #endregion Private Methods

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
        public CSTagC1G2_96BitTag CloneTag()
        {
            return (CSTagC1G2_96BitTag)Clone();
        }

        #endregion
    }
}
