using System;
using System.Collections.Generic;
using System.Text;

namespace KTone.Core.KTIRFID
{
    [Serializable]
    public class TamperWrapper
    {
        #region Attributes

        string tamperType = string.Empty;
        int tagType = 0;
        int delayCount = 0;
        int delayResetTimeSec = 0;

        #endregion Attributes

        #region Constructor

        public TamperWrapper(string tamperType, int tagType,
            int delayCount, int relayResetTimeSec)
        {
            this.tamperType = tamperType;
            this.tagType = tagType;
            this.delayCount = delayCount;
            this.delayResetTimeSec = relayResetTimeSec;
        }

        public TamperWrapper()
        {

        }

        #endregion Constructor

        #region Properties

        public int DelayResetTimeSec
        {
            get { return delayResetTimeSec; }
            set { delayResetTimeSec = value; }
        }

        public int DelayCount
        {
            get { return delayCount; }
            set { delayCount = value; }
        }

        public int TagType
        {
            get { return tagType; }
            set { tagType = value; }
        }

        public string TamperType
        {
            get { return tamperType; }
            set { tamperType = value; }
        }

        #endregion Properties
    }
}
