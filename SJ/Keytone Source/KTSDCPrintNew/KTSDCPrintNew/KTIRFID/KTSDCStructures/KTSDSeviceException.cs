using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.Runtime.Serialization;


namespace KTone.WebServer
{
    public class KTErrorCode
    {
        public const int DATA_NOT_SUPPLIED = 100;
        public const int DATA_NOT_FOUND = 101;
        public const int DATA_ALREADY_EXISTS = 102;
        public const int UNABLE_TO_SAVE = 103;
        public const int UNABLE_TO_UPDATE = 104;
        public const int UNABLE_TO_DELETE = 105;
        public const int INVALID_SUPPLIED_DATA = 106;
        public const int EXCEPTION = 107;
    }

    [DataContract]
    public class KTSDSeviceException
    {
        string _message;
        int _errorCode = 0;

        /// <summary>
        /// Returns error message.
        /// </summary>
        [DataMember]
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
            }
        }

        /// <summary>
        /// Returns error code.
        /// </summary>
        [DataMember]
        public Int32 ErrorCode
        {
            get
            {
                return _errorCode;
            }
            set
            {
                _errorCode = value;
            }
        }

    }
}
