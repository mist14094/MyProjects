using System;
using System.Collections.Generic;
using System.Text;

namespace KTone.Core.KTIRFID
{
    [Serializable]
    public enum ProfileGroup 
    {
        GENERAL,
        ANTENNA,
        RF,
        TAGSETTING,
        CUSTOM,
    }

    #region [ Groups ]
    [Serializable]
    public enum AntennaProfile
    {
        NAME,
        POWER,
        TYPE        
    }
    
    [Serializable]
    public enum RFProfile
    {
        RF_POWER,
        TRANSMISSION_TYPE
    }
   
    [Serializable]
    public enum CustomProfile
    {
    }
   
    [Serializable]
    public enum GeneralProfile
    {
        VENDOR,
        MODEL,
        COMMUNICATION_TYPE,
        NO_OF_ANTENNA,
    }


   
    #endregion [ Groups ]

    [Serializable]
    public /*sealed*/ struct ProfileKey //: IEquatable<ProfileKey>
    {
        private ProfileGroup mGroupName;
        private string mKey;

        //private ProfileKey()
        //{
        //}

        public ProfileKey(ProfileGroup GroupName, string Key)
        {
            this.mGroupName = GroupName;
            this.mKey = Key;
        }

        public ProfileGroup GroupName
        {
            get { return this.mGroupName; }
        }

        public string Key
        {
            get {return this.mKey; }
        }

        //#region IEquatable<ProfileKey> Members

        //public bool Equals(ProfileKey other)
        //{
        //    bool result = false;

        //    if (this.GroupName.Equals(other.GroupName) && this.Key.Equals(other.Key) )
        //    {
        //        result = true;
        //    }

        //    return result;
        //}

        //#endregion
    }
  
    [Serializable]
    public sealed class ProfileEntity
    {
        private ProfileKey mKey;
        private object mValue;

        private ProfileEntity()
        {
        }

        public ProfileEntity(ProfileKey Key, object Value)
        {
            this.mValue = Value;
            this.mKey = Key;
        }

        public string Value
        {
            get { return this.mValue.ToString(); }
        }

        public string Key
        {
            get {return this.mKey.Key; }
        }
    }
   
    [Serializable]
    public sealed class KTMetaData
    {
        private Type mDataType;

        private object mMinVal;
        private object mMaxVal;
        private object mDefaultVal;
        private object[] mPossibleVals;
        
        private string mDescription;

        #region [ CONSTRUTOR ]
        private KTMetaData()
        {
        }

        public KTMetaData(Type DataType,object MinVal, object MaxVal, object DefaultVal, object[] PossibleVals, string Description )
        {
            this.mDataType = DataType;

            mMinVal = MinVal ;
            mMaxVal = MaxVal ;
            mDefaultVal = DefaultVal;
            mPossibleVals = PossibleVals;

            this.mDescription = Description;
        }

        public KTMetaData(Type DataType, string Description)
        {
            this.mDataType = DataType;
            this.mDescription = Description;
        }
        #endregion [ CONSTRUTOR ]

        #region [ Pros ]
        public object MinVal
        {
            get { return mMinVal; }
        }

        public object MaxVal
        {
            get { return mMaxVal; }
        }

        public object DefaultVal
        {
            get { return mDefaultVal; }
        }

        public Type DataType
        {
            get {return this.mDataType ; }
        }

        public string Description
        {
            get {return this.mDescription; }
        }

        public object[] PossibleVals
        {
            get { return this.mPossibleVals; }
        }
        #endregion [ Pros ]
    }
   
    [Serializable]
    public static class ProfileAttributes
    {
        public static ProfileKey AntennaPower = new ProfileKey(ProfileGroup.ANTENNA, AntennaProfile.POWER.ToString());
        public static ProfileKey AntennaType = new ProfileKey(ProfileGroup.ANTENNA, AntennaProfile.TYPE.ToString());

        public static ProfileKey RFPower = new ProfileKey(ProfileGroup.RF, RFProfile.RF_POWER.ToString());
        public static ProfileKey RFType = new ProfileKey(ProfileGroup.RF, RFProfile.TRANSMISSION_TYPE.ToString());

        public static ProfileKey GeneralVendorName = new ProfileKey(ProfileGroup.GENERAL, GeneralProfile.VENDOR.ToString());
        public static ProfileKey GeneralModelName = new ProfileKey(ProfileGroup.GENERAL, GeneralProfile.MODEL.ToString());
        public static ProfileKey GeneralNo_Of_Antenna = new ProfileKey(ProfileGroup.GENERAL, GeneralProfile.NO_OF_ANTENNA.ToString());
        public static ProfileKey GeneralComm_Type = new ProfileKey(ProfileGroup.GENERAL, GeneralProfile.COMMUNICATION_TYPE.ToString());
    }
}
