using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace KTone.Core.KTIRFID
{

    [DataContract]
    public class UserData
    {
        private Boolean _active;
        // private DateTime _modifiedDate, _createdDate;
        private Int32 _roleID, _userID;
        private String _name, _userName, _password, _emailID, _roleName;

        #region [Class Property Declarations]
        [DataMember(IsRequired = true)]
        public Int32 UserID
        {
            get
            {
                return _userID;
            }
            set
            {
                _userID = value;
            }
        }
        [DataMember(IsRequired = true)]
        public String Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        [DataMember(IsRequired = true)]
        public String UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                String userNameTmp = (String)value;
                if (userNameTmp == null)
                {
                    throw new ArgumentOutOfRangeException("UserName", "UserName can't be NULL");
                }
                _userName = value;
            }
        }
        [DataMember(IsRequired = true)]
        public String Password
        {
            get
            {
                return _password;
            }
            set
            {
                String passwordTmp = (String)value;
                if (passwordTmp == null)
                {
                    throw new ArgumentOutOfRangeException("Password", "Password can't be NULL");
                }
                _password = value;
            }
        }
        [DataMember(IsRequired = true)]
        public Int32 RoleID
        {
            get
            {
                return _roleID;
            }
            set
            {
                _roleID = value;
            }
        }
        [DataMember]
        public String RoleName
        {
            get
            {
                return _roleName;
            }
            set
            {
                _roleName = value;
            }
        }
        [DataMember]
        public Boolean Active
        {
            get
            {
                return _active;
            }
            set
            {
                _active = value;
            }
        }
        [DataMember]
        public String EmailID
        {
            get
            {
                return _emailID;
            }
            set
            {
                _emailID = value;
            }
        }
        #endregion
    }





    [DataContract]
    public class MenuAccessData
    {
        private Int32 _roleID;
        private String _name;

        [DataMember(IsRequired = true)]
        public String MenuName
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        [DataMember(IsRequired = true)]
        public Int32 RoleID
        {
            get
            {
                return _roleID;
            }
            set
            {
                _roleID = value;
            }
        }

    }

    [DataContract]

    public class LookUpSettings
    {

        private String _lookUpName, _lookUpValue;

        [DataMember(IsRequired = true)]
        public String SettingParameter
        {
            get
            {
                return _lookUpName;
            }
            set
            {
                _lookUpName = value;
            }
        }

        [DataMember(IsRequired = true)]
        public String SettingValue
        {
            get
            {
                return _lookUpValue;
            }
            set
            {
                _lookUpValue = value;
            }
        }

    }

}


