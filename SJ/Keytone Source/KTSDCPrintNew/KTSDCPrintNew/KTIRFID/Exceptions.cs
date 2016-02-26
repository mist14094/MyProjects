using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace KTone.Core.KTIRFID
{
    /// <summary>
    /// <exclude />
    /// The exception that is thrown when a RFID application error occurs.
    /// </summary>
    [SerializableAttribute]
    public class KTAppException : ApplicationException, ISerializable
    {
        #region Attributes

        /// <summary>
        /// The name of the server where this exception is generated.
        /// </summary>
        private string serverName = "";

        /// <summary>
        /// Message of the inner exception
        /// </summary>
        private string ktAppExceptionMessage = string.Empty;

        /// <summary>
        /// Stack trace  of the inner exception
        /// </summary>
        private string ktAppExceptionStackTrace = string.Empty;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the KTAppException 
        /// <see cref="KTone.Core.KTIRFID.KTAppException"/> class.
        /// </summary>
        public KTAppException()
            : base()
        {
            Init();
        }


        /// <summary>
        /// Initializes a new instance of the KTAppException <see cref="KTone.Core.KTIRFID.KTAppException"/> 
        /// class with a specified error message.
        /// </summary>
        /// <param name="message">A message that describes the error. </param>
        public KTAppException(string message)
            : base(message)
        {
            Init();
        }


        /// <summary>
        /// Initializes a new instance of the KTAppException <see cref="KTone.Core.KTIRFID.KTAppException"/> 
        /// class with a specified error message and message and stack trace of the inner exception that 
        /// is the cause of this exception.
        /// </summary>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public KTAppException(string message, Exception innerException)
            : base(message)
        {
            Init();
            InitInnerExceptionInfo(innerException);
        }


        /// <summary>
        /// Initializes a new instance of the KTAppException <see cref="KTone.Core.KTIRFID.KTAppException"/> 
        /// class with a specified error message and the name of the server where this exception is generated.
        /// </summary>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="serverName">The name of the server where this exception is generated.</param>
        public KTAppException(string message, string serverName)
            : base(message)
        {
            this.serverName = serverName;
        }


        /// <summary>
        /// Initializes a new instance of the KTAppException <see cref="KTone.Core.KTIRFID.KTAppException"/> 
        /// class with a specified error message, message and stack trace of the inner exception that is the cause 
        /// of this exception and the name of the server where this exception is generated.
        /// </summary>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        /// <param name="serverName">The name of the server where this exception is generated.</param>
        public KTAppException(string message, Exception innerException, string serverName)
            : base(message)
        {
            this.serverName = serverName;

            InitInnerExceptionInfo(innerException);
        }


        /// <summary>
        /// Initializes a new instance of the KTAppException <see cref="KTone.Core.KTIRFID.KTAppException"/> 
        /// class with  message and stack trace of the inner exception that is the cause 
        /// of this exception and the name of the server where this exception is generated.
        /// </summary>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public KTAppException(Exception innerException)
            : base(string.Empty)
        {
            Init();
            InitInnerExceptionInfo(innerException);
        }

        /// <summary>
        /// Initializes a new instance of the KTAppException <see cref="KTone.Core.KTIRFID.KTAppException"/> 
        /// class with  message and stack trace of the inner exception that is the cause 
        /// of this exception and the name of the server where this exception is generated.
        /// </summary>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        /// <param name="serverName">The name of the server where this exception is generated.</param>
        public KTAppException(Exception innerException, string serverName)
            : base(string.Empty)
        {
            this.serverName = serverName;
            InitInnerExceptionInfo(innerException);
        }


        /// <summary>
        /// Initializes a new instance of the KTAppException <see cref="KTone.Core.KTIRFID.KTAppException"/> 
        /// class with serialized data.
        /// </summary>
        /// <param name="exceptionInfo">The object that holds the serialized object data.</param>
        /// <param name="exceptionContext">The contextual information about the source or destination. 
        ///</param>
        public KTAppException(SerializationInfo exceptionInfo,StreamingContext exceptionContext)
            : base(exceptionInfo, exceptionContext)
        {
            this.serverName = exceptionInfo.GetString("ServerName");
            this.ktAppExceptionMessage = exceptionInfo.GetString("ktAppExceptionMessage");
            this.ktAppExceptionStackTrace = exceptionInfo.GetString("ktAppExceptionStackTrace");
        }
        #endregion

        #region Serialization

        /// <summary>
        /// Sets the SerializationInfo with information about the exception.
        /// </summary>
        /// <param name="exceptionInfo">The SerializationInfo that holds the 
        /// serialized object data about the exception being thrown. </param>
        /// <param name="exceptionContext">The StreamingContext that contains 
        /// contextual information about the source or destination. </param>
        public override void GetObjectData(SerializationInfo exceptionInfo,
            StreamingContext exceptionContext)
        {
            base.GetObjectData(exceptionInfo, exceptionContext);
            exceptionInfo.AddValue("ServerName", this.ServerName);
            exceptionInfo.AddValue("ktAppExceptionMessage", this.ktAppExceptionMessage);
            exceptionInfo.AddValue("ktAppExceptionStackTrace", this.ktAppExceptionStackTrace);
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Initializes the server name to host name.
        /// </summary>
        private void Init()
        {
            try
            {
                serverName = System.Net.Dns.GetHostName();
            }
            catch
            {
                serverName = "";
            }
        }


        /// <summary>
        /// Initializes the innner exception information recursively if the innerException is not null.
        /// </summary>
        /// <param name="innerException"></param>
        private void InitInnerExceptionInfo(Exception innerException)
        {
            ktAppExceptionMessage = string.Empty;
            ktAppExceptionStackTrace = string.Empty;
            string indent = "\t";
            while (innerException != null)
            {
                ktAppExceptionMessage += innerException.Message + Environment.NewLine;
                ktAppExceptionStackTrace += indent + "Inner exception type thrown by server component: "
                    + innerException.GetType().Name.ToString() + Environment.NewLine;
                ktAppExceptionStackTrace += indent + "Message: " + innerException.Message
                    + Environment.NewLine;
                ktAppExceptionStackTrace += indent + "StackTrace: " + innerException.StackTrace
                    + Environment.NewLine;
                indent += "\t";

                innerException = innerException.InnerException;
            }
        }


        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the server where this exception is generated.
        /// </summary>
        public string ServerName
        {
            get { return (serverName.Trim()); }
        }

        /// <summary>
        /// Gets a message that describes the current exception.
        /// </summary>
        public override string Message
        {
            get
            {
                if (this.ServerName.Length == 0)
                    return (base.Message + " [SERVER NAME - NOT FOUND]");
                else
                {
                    string errorString = base.Message;

                    if (!ktAppExceptionMessage.Equals(string.Empty))
                    {
                        if (!errorString.Equals(string.Empty))
                            errorString += Environment.NewLine;
                        errorString += ktAppExceptionMessage;
                    }

                    //return (" [SERVER NAME " + this.ServerName + "] " + errorString);
                    //Do not add header if it already exists.
                    string msgHeader = "[SERVER NAME " + this.ServerName + "] ";
                    if ((errorString != string.Empty) && (!errorString.StartsWith(msgHeader)))
                        errorString = msgHeader + errorString;
                    return errorString;
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates and returns a string representation of the current exception.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string errorString = "APPLICATION EXCEPTION has occurred on SERVER Component [SERVER NAME "
                + this.ServerName + "] ";

            errorString += Environment.NewLine + "StackTrace of the client component: " +
                this.StackTrace;

            if (!ktAppExceptionStackTrace.Equals(string.Empty))
            {
                errorString += Environment.NewLine + "StackTrace of the inner exception: ";
                errorString += Environment.NewLine + ktAppExceptionStackTrace;
            }
            return (errorString);
        }


        /// <summary>
        /// Calls ToString() method of the base class.
        /// </summary>
        /// <returns></returns>
        public string ToBaseString()
        {
            return (base.ToString());
        }

        /// <summary>
        /// Serves as a hash function for KTAppException, 
        /// suitable for use in hashing algorithms and data structures like a hash table.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return (ServerName.GetHashCode());
        }

        /// <summary>
        /// Determines whether two KTAppException instances are equal.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            bool isEqual = false;

            if (obj == null || (this.GetType() != obj.GetType()))
            {
                isEqual = false;
            }
            else
            {
                KTAppException se = (KTAppException)obj;
                if ((this.ServerName.Length == 0) && (se.ServerName.Length == 0))
                    isEqual = false;
                else
                    isEqual = (this.ServerName == se.ServerName);
            }

            return (isEqual);
        }


        /// <summary>
        /// Determines whether two KTAppException instances are equal.
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static bool operator ==(KTAppException v1, KTAppException v2)
        {
            return (v1.Equals(v2));
        }


        /// <summary>
        /// Determines whether two KTAppException instances arenot equal.
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static bool operator !=(KTAppException v1, KTAppException v2)
        {
            return (!(v1 == v2));
        }

        #endregion
    }


    /// <summary>
    /// <exclude />
    /// The exception that is thrown when an error occurs in the execution of a reader 
    /// factory object that supports IKTComponentFactory interface.
    /// </summary>
    [SerializableAttribute]
    public class KTComponentFactoryException : KTAppException
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the KTComponentFactoryException 
        /// <see cref="KTone.Core.KTIRFID.KTComponentFactoryException"/> class.
        /// </summary>
        public KTComponentFactoryException()
            : base()
        {
        }


        /// <summary>
        /// Initializes a new instance of the KTComponentFactoryException 
        /// <see cref="KTone.Core.KTIRFID.KTComponentFactoryException"/> 
        /// class with a specified error message.
        /// </summary>
        /// <param name="message">A message that describes the error. </param>
        public KTComponentFactoryException(string message)
            : base(message)
        {
        }


        /// <summary>
        /// Initializes a new instance of the KTComponentFactoryException 
        /// <see cref="KTone.Core.KTIRFID.KTComponentFactoryException"/> 
        /// class with a specified error message and message and stack trace of the inner exception that 
        /// is the cause of this exception.
        /// </summary>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public KTComponentFactoryException(string message, Exception innerException)
            : base(message, innerException)
        {
        }


        /// <summary>
        /// Initializes a new instance of the KTComponentFactoryException 
        /// <see cref="KTone.Core.KTIRFID.KTComponentFactoryException"/> 
        /// class with a specified error message and the name of the server where this exception is generated.
        /// </summary>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="serverName">The name of the server where this exception is generated.</param>
        public KTComponentFactoryException(string message, string serverName)
            : base(message, serverName)
        {
        }


        /// <summary>
        /// Initializes a new instance of the KTComponentFactoryException 
        /// <see cref="KTone.Core.KTIRFID.KTComponentFactoryException"/> 
        /// class with a specified error message, message and stack trace of the inner exception that is the cause 
        /// of this exception and the name of the server where this exception is generated.
        /// </summary>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        /// <param name="serverName">The name of the server where this exception is generated.</param>
        public KTComponentFactoryException(string message, Exception innerException, string serverName)
            : base(message, innerException, serverName)
        {
        }


        /// <summary>
        /// Initializes a new instance of the KTComponentFactoryException 
        /// <see cref="KTone.Core.KTIRFID.KTComponentFactoryException"/> 
        /// class with  message and stack trace of the inner exception that is the cause 
        /// of this exception and the name of the server where this exception is generated.
        /// </summary>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public KTComponentFactoryException(Exception innerException)
            : base(innerException)
        {
        }


        /// <summary>
        /// Initializes a new instance of the KTComponentFactoryException 
        /// <see cref="KTone.Core.KTIRFID.KTComponentFactoryException"/> 
        /// class with  message and stack trace of the inner exception that is the cause 
        /// of this exception and the name of the server where this exception is generated.
        /// </summary>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        /// <param name="serverName">The name of the server where this exception is generated.</param>
        public KTComponentFactoryException(Exception innerException, string serverName)
            : base(innerException, serverName)
        {
        }


        /// <summary>
        /// Initializes a new instance of the KTComponentFactoryException 
        /// <see cref="KTone.Core.KTIRFID.KTComponentFactoryException"/> class with serialized data.
        /// </summary>
        /// <param name="exceptionInfo">The object that holds the serialized object data.</param>
        /// <param name="exceptionContext">The contextual information about the source or destination. 
        ///</param>
        public KTComponentFactoryException(SerializationInfo exceptionInfo, StreamingContext exceptionContext)
            : base(exceptionInfo, exceptionContext)
        {
        }


        #endregion
    }

    /// <summary>
    /// <exclude />
    /// The exception that is thrown when an error occurs in the execution of a component object 
    /// that supports IKTComponent.cs interface.
    /// </summary>
    [SerializableAttribute]
    public class KTComponentException : KTAppException
    {
        #region Attributes
        string componentId;
        #endregion Attributes

        #region Constructors


        /// <summary>
        /// Initializes a new instance of the KTComponentException 
        /// <see cref="KTone.Core.KTIRFID.KTComponentException"/> 
        /// class with a specified error message.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        public KTComponentException(string componentId,string message)
            : base(message)
        {
            this.componentId = componentId;
        }


        /// <summary>
        /// Initializes a new instance of the KTComponentException 
        /// <see cref="KTone.Core.KTIRFID.KTComponentException"/> 
        /// class with a specified error message and message and stack trace of the inner exception that 
        /// is the cause of this exception.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public KTComponentException(string componentId,string message, Exception innerException)
            : base(message, innerException)
        {
            this.componentId = componentId;
        }


        /// <summary>
        /// Initializes a new instance of the KTComponentException 
        /// <see cref="KTone.Core.KTIRFID.KTComponentException"/> 
        /// class with a specified error message and the name of the server where this exception is generated.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="serverName">The name of the server where this exception is generated.</param>
        public KTComponentException(string componentId, string message, string serverName)
            : base(message, serverName)
        {
            this.componentId = componentId;
        }


        /// <summary>
        /// Initializes a new instance of the KTComponentException 
        /// <see cref="KTone.Core.KTIRFID.KTComponentException"/> 
        /// class with a specified error message, message and stack trace of the inner exception that is the cause 
        /// of this exception and the name of the server where this exception is generated.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        /// <param name="serverName">The name of the server where this exception is generated.</param>
        public KTComponentException(string componentId, string message, Exception innerException, string serverName)
            : base(message, innerException, serverName)
        {
            this.componentId = componentId;
        }

        /// <summary>
        /// Initializes a new instance of the KTComponentException 
        /// <see cref="KTone.Core.KTIRFID.KTComponentException"/> class with serialized data.
        /// </summary>
        /// <param name="exceptionInfo">The object that holds the serialized object data.</param>
        /// <param name="exceptionContext">The contextual information about the source or destination. 
        ///</param>
        public KTComponentException(SerializationInfo exceptionInfo, StreamingContext exceptionContext)
            : base(exceptionInfo, exceptionContext)
        {
            this.componentId = exceptionInfo.GetString("ComponentId");
        }


        #endregion

        #region Serialization

        /// <summary>
        /// Sets the SerializationInfo with information about the exception.
        /// </summary>
        /// <param name="exceptionInfo">The SerializationInfo that holds the 
        /// serialized object data about the exception being thrown. </param>
        /// <param name="exceptionContext">The StreamingContext that contains 
        /// contextual information about the source or destination. </param>
        public override void GetObjectData(SerializationInfo exceptionInfo,
            StreamingContext exceptionContext)
        {
            base.GetObjectData(exceptionInfo, exceptionContext);
            exceptionInfo.AddValue("ComponentId", this.ComponentId);
        }
        #endregion

        #region Properties
        public string ComponentId
        {
            get
            {
                return componentId;
            }
        }
        #endregion Properties
    }

    /// <summary>
    /// <exclude />
    /// The exception that is thrown when an error occurs in the execution of a reader object 
    /// that supports IRFIDReader interface.
    /// </summary>
    [SerializableAttribute]
    public class RFIDReaderException : KTComponentException
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the RFIDReaderException 
        /// <see cref="KTone.Core.KTIRFID.RFIDReaderException"/> 
        /// class with a specified error message.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        public RFIDReaderException(string componentId, string message)
            : base(componentId,message)
        {
        }


        /// <summary>
        /// Initializes a new instance of the RFIDReaderException 
        /// <see cref="KTone.Core.KTIRFID.RFIDReaderException"/> 
        /// class with a specified error message and message and stack trace of the inner exception that 
        /// is the cause of this exception.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public RFIDReaderException(string componentId, string message, Exception innerException)
            : base(componentId,message, innerException)
        {
        }


        /// <summary>
        /// Initializes a new instance of the RFIDReaderException 
        /// <see cref="KTone.Core.KTIRFID.RFIDReaderException"/> 
        /// class with a specified error message and the name of the server where this exception is generated.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="serverName">The name of the server where this exception is generated.</param>
        public RFIDReaderException(string componentId, string message, string serverName)
            : base(componentId,message, serverName)
        {
        }


        /// <summary>
        /// Initializes a new instance of the RFIDReaderException 
        /// <see cref="KTone.Core.KTIRFID.RFIDReaderException"/> 
        /// class with a specified error message, message and stack trace of the inner exception that is the cause 
        /// of this exception and the name of the server where this exception is generated.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        /// <param name="serverName">The name of the server where this exception is generated.</param>
        public RFIDReaderException(string componentId, string message, Exception innerException, string serverName)
            : base(componentId,message, innerException, serverName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the RFIDReaderException 
        /// <see cref="KTone.Core.KTIRFID.RFIDReaderException"/> class with serialized data.
        /// </summary>
        /// <param name="exceptionInfo">The object that holds the serialized object data.</param>
        /// <param name="exceptionContext">The contextual information about the source or destination. 
        ///</param>
        public RFIDReaderException(SerializationInfo exceptionInfo, StreamingContext exceptionContext)
            : base(exceptionInfo, exceptionContext)
        {
        }


        #endregion
    }

    /// <summary>
    /// <exclude />
    /// The exception that is thrown when an error occurs in the execution of a reader object 
    /// that supports a custom reader interface.
    /// </summary>
    [SerializableAttribute]
    public class RFIDCustomReaderException : KTComponentException
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the RFIDCustomReaderException 
        /// <see cref="KTone.Core.KTIRFID.RFIDCustomReaderException"/> 
        /// class with a specified error message.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        public RFIDCustomReaderException(string componentId, string message)
            : base(componentId, message)
        {
        }


        /// <summary>
        /// Initializes a new instance of the RFIDCustomReaderException 
        /// <see cref="KTone.Core.KTIRFID.RFIDCustomReaderException"/> 
        /// class with a specified error message and message and stack trace of the inner exception that 
        /// is the cause of this exception.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public RFIDCustomReaderException(string componentId, string message, Exception innerException)
            : base(componentId, message, innerException)
        {
        }


        /// <summary>
        /// Initializes a new instance of the RFIDCustomReaderException 
        /// <see cref="KTone.Core.KTIRFID.RFIDCustomReaderException"/> 
        /// class with a specified error message and the name of the server where this exception is generated.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="serverName">The name of the server where this exception is generated.</param>
        public RFIDCustomReaderException(string componentId, string message, string serverName)
            : base(componentId, message, serverName)
        {
        }


        /// <summary>
        /// Initializes a new instance of the RFIDCustomReaderException 
        /// <see cref="KTone.Core.KTIRFID.RFIDCustomReaderException"/> 
        /// class with a specified error message, message and stack trace of the inner exception that is the cause 
        /// of this exception and the name of the server where this exception is generated.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        /// <param name="serverName">The name of the server where this exception is generated.</param>
        public RFIDCustomReaderException(string componentId, string message, Exception innerException, string serverName)
            : base(componentId, message, innerException, serverName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the RFIDCustomReaderException 
        /// <see cref="KTone.Core.KTIRFID.RFIDCustomReaderException"/> class with serialized data.
        /// </summary>
        /// <param name="exceptionInfo">The object that holds the serialized object data.</param>
        /// <param name="exceptionContext">The contextual information about the source or destination. 
        ///</param>
        public RFIDCustomReaderException(SerializationInfo exceptionInfo, StreamingContext exceptionContext)
            : base(exceptionInfo, exceptionContext)
        {
        }


        #endregion
    }
    
    /// <summary>
    /// <exclude />
    /// The exception that is thrown when a parameter to a method is invalid. 
    /// </summary>
    [SerializableAttribute]
    public class InvalidParamException : KTComponentException
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the InvalidParamException 
        /// <see cref="KTone.Core.KTIRFID.InvalidParamException"/> 
        /// class with a specified error message.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        public InvalidParamException(string componentId, string message)
            : base(componentId, message)
        {
        }


        /// <summary>
        /// Initializes a new instance of the InvalidParamException 
        /// <see cref="KTone.Core.KTIRFID.InvalidParamException"/> 
        /// class with a specified error message and message and stack trace of the inner exception that 
        /// is the cause of this exception.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public InvalidParamException(string componentId, string message, Exception innerException)
            : base(componentId, message, innerException)
        {
        }


        /// <summary>
        /// Initializes a new instance of the InvalidParamException 
        /// <see cref="KTone.Core.KTIRFID.InvalidParamException"/> 
        /// class with a specified error message and the name of the server where this exception is generated.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="serverName">The name of the server where this exception is generated.</param>
        public InvalidParamException(string componentId, string message, string serverName)
            : base(componentId, message, serverName)
        {
        }


        /// <summary>
        /// Initializes a new instance of the InvalidParamException 
        /// <see cref="KTone.Core.KTIRFID.InvalidParamException"/> 
        /// class with a specified error message, message and stack trace of the inner exception that is the cause 
        /// of this exception and the name of the server where this exception is generated.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        /// <param name="serverName">The name of the server where this exception is generated.</param>
        public InvalidParamException(string componentId, string message, Exception innerException, string serverName)
            : base(componentId, message, innerException, serverName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the InvalidParamException 
        /// <see cref="KTone.Core.KTIRFID.InvalidParamException"/> class with serialized data.
        /// </summary>
        /// <param name="exceptionInfo">The object that holds the serialized object data.</param>
        /// <param name="exceptionContext">The contextual information about the source or destination. 
        ///</param>
        public InvalidParamException(SerializationInfo exceptionInfo, StreamingContext exceptionContext)
            : base(exceptionInfo, exceptionContext)
        {
        }


        #endregion
    }
    
    /// <summary>
    /// <exclude />
    /// The exception that is thrown when an error occurs while formatting a command 
    /// from the given input parameters.
    /// </summary>
    [SerializableAttribute]
    public class RFIDCommandException : KTComponentException
    {
        #region Constructors


        /// <summary>
        /// Initializes a new instance of the RFIDCommandException 
        /// <see cref="KTone.Core.KTIRFID.RFIDCommandException"/> 
        /// class with a specified error message.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        public RFIDCommandException(string componentId,string message)
            : base(componentId,message)
        {
        }


        /// <summary>
        /// Initializes a new instance of the RFIDCommandException 
        /// <see cref="KTone.Core.KTIRFID.RFIDCommandException"/> 
        /// class with a specified error message and message and stack trace of the inner exception that 
        /// is the cause of this exception.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public RFIDCommandException(string componentId,string message, Exception innerException)
            : base(componentId,message, innerException)
        {
        }


        /// <summary>
        /// Initializes a new instance of the RFIDCommandException 
        /// <see cref="KTone.Core.KTIRFID.RFIDCommandException"/> 
        /// class with a specified error message and the name of the server where this exception is generated.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="serverName">The name of the server where this exception is generated.</param>
        public RFIDCommandException(string componentId,string message, string serverName)
            : base(message, serverName)
        {
        }


        /// <summary>
        /// Initializes a new instance of the RFIDCommandException 
        /// <see cref="KTone.Core.KTIRFID.RFIDCommandException"/> 
        /// class with a specified error message, message and stack trace of the inner exception that is the cause 
        /// of this exception and the name of the server where this exception is generated.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        /// <param name="serverName">The name of the server where this exception is generated.</param>
        public RFIDCommandException(string componentId,string message, Exception innerException, string serverName)
            : base(componentId,message, innerException, serverName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the RFIDCommandException 
        /// <see cref="KTone.Core.KTIRFID.RFIDCommandException"/> class with serialized data.
        /// </summary>
        /// <param name="exceptionInfo">The object that holds the serialized object data.</param>
        /// <param name="exceptionContext">The contextual information about the source or destination. 
        ///</param>
        public RFIDCommandException(SerializationInfo exceptionInfo, StreamingContext exceptionContext)
            : base(exceptionInfo, exceptionContext)
        {
        }


        #endregion
    }

    /// <summary>
    /// <exclude />
    /// The exception that is thrown when an error occurs while parsing the response received.
    /// </summary>
    [SerializableAttribute]
    public class RFIDResponseException : KTComponentException
    {
        #region Constructors


        /// <summary>
        /// Initializes a new instance of the RFIDResponseException 
        /// <see cref="KTone.Core.KTIRFID.RFIDResponseException"/> 
        /// class with a specified error message and message and stack trace of the inner exception that 
        /// is the cause of this exception.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        public RFIDResponseException(string componentId, string message)
            : base(componentId, message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the RFIDResponseException 
        /// <see cref="KTone.Core.KTIRFID.RFIDResponseException"/> 
        /// class with a specified error message and message and stack trace of the inner exception that 
        /// is the cause of this exception.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public RFIDResponseException(string componentId,string message, Exception innerException)
            : base(componentId,message, innerException)
        {
        }


        /// <summary>
        /// Initializes a new instance of the RFIDResponseException 
        /// <see cref="KTone.Core.KTIRFID.RFIDResponseException"/> 
        /// class with a specified error message and the name of the server where this exception is generated.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="serverName">The name of the server where this exception is generated.</param>
        public RFIDResponseException(string componentId,string message, string serverName)
            : base(componentId,message, serverName)
        {
        }


        /// <summary>
        /// Initializes a new instance of the RFIDResponseException 
        /// <see cref="KTone.Core.KTIRFID.RFIDResponseException"/> 
        /// class with a specified error message, message and stack trace of the inner exception that is the cause 
        /// of this exception and the name of the server where this exception is generated.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        /// <param name="serverName">The name of the server where this exception is generated.</param>
        public RFIDResponseException(string componentId,string message, Exception innerException, string serverName)
            : base(componentId,message, innerException, serverName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the RFIDResponseException 
        /// <see cref="KTone.Core.KTIRFID.RFIDResponseException"/> class with serialized data.
        /// </summary>
        /// <param name="exceptionInfo">The object that holds the serialized object data.</param>
        /// <param name="exceptionContext">The contextual information about the source or destination. 
        ///</param>
        public RFIDResponseException(SerializationInfo exceptionInfo, StreamingContext exceptionContext)
            : base(exceptionInfo, exceptionContext)
        {
        }


        #endregion
    }

    /// <summary>
    /// <exclude />
    /// The exception that is thrown when a communication(serial/ethernet) error occurs.
    /// </summary>
    [SerializableAttribute]
    public class RFIDCommunicationException : KTComponentException
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the RFIDCommunicationException 
        /// <see cref="KTone.Core.KTIRFID.RFIDCommunicationException"/> 
        /// class with a specified error message.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        public RFIDCommunicationException(string componentId,string message)
            : base(componentId,message)
        {
        }


        /// <summary>
        /// Initializes a new instance of the RFIDCommunicationException 
        /// <see cref="KTone.Core.KTIRFID.RFIDCommunicationException"/> 
        /// class with a specified error message and message and stack trace of the inner exception that 
        /// is the cause of this exception.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public RFIDCommunicationException(string componentId,string message, Exception innerException)
            : base(componentId,message, innerException)
        {
        }


        /// <summary>
        /// Initializes a new instance of the RFIDCommunicationException 
        /// <see cref="KTone.Core.KTIRFID.RFIDCommunicationException"/> 
        /// class with a specified error message and the name of the server where this exception is generated.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="serverName">The name of the server where this exception is generated.</param>
        public RFIDCommunicationException(string componentId,string message, string serverName)
            : base(componentId,message, serverName)
        {
        }


        /// <summary>
        /// Initializes a new instance of the RFIDCommunicationException 
        /// <see cref="KTone.Core.KTIRFID.RFIDCommunicationException"/> 
        /// class with a specified error message, message and stack trace of the inner exception that is the cause 
        /// of this exception and the name of the server where this exception is generated.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        /// <param name="serverName">The name of the server where this exception is generated.</param>
        public RFIDCommunicationException(string componentId,string message, Exception innerException, string serverName)
            : base(componentId,message, innerException, serverName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the RFIDCommunicationException 
        /// <see cref="KTone.Core.KTIRFID.RFIDCommunicationException"/> class with serialized data.
        /// </summary>
        /// <param name="exceptionInfo">The object that holds the serialized object data.</param>
        /// <param name="exceptionContext">The contextual information about the source or destination. 
        ///</param>
        public RFIDCommunicationException(SerializationInfo exceptionInfo, StreamingContext exceptionContext)
            : base(exceptionInfo, exceptionContext)
        {
        }

        #endregion
    }

    /// <summary>
    /// <exclude />
    /// The exception that is thrown when timeout occurs while executing a reader command.
    /// </summary>
    [SerializableAttribute]
    public class RFIDTimeoutException : KTComponentException
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the RFIDTimeoutException 
        /// <see cref="KTone.Core.KTIRFID.RFIDTimeoutException"/> 
        /// class with a specified error message.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        public RFIDTimeoutException(string componentId,string message)
            : base(componentId,message)
        {
        }


        /// <summary>
        /// Initializes a new instance of the RFIDTimeoutException 
        /// <see cref="KTone.Core.KTIRFID.RFIDTimeoutException"/> 
        /// class with a specified error message and message and stack trace of the inner exception that 
        /// is the cause of this exception.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public RFIDTimeoutException(string componentId,string message, Exception innerException)
            : base(componentId,message, innerException)
        {
        }


        /// <summary>
        /// Initializes a new instance of the RFIDTimeoutException 
        /// <see cref="KTone.Core.KTIRFID.RFIDTimeoutException"/> 
        /// class with a specified error message and the name of the server where this exception is generated.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="serverName">The name of the server where this exception is generated.</param>
        public RFIDTimeoutException(string componentId,string message, string serverName)
            : base(componentId,message, serverName)
        {
        }


        /// <summary>
        /// Initializes a new instance of the RFIDTimeoutException 
        /// <see cref="KTone.Core.KTIRFID.RFIDTimeoutException"/> 
        /// class with a specified error message, message and stack trace of the inner exception that is the cause 
        /// of this exception and the name of the server where this exception is generated.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        /// <param name="serverName">The name of the server where this exception is generated.</param>
        public RFIDTimeoutException(string componentId,string message, Exception innerException, string serverName)
            : base(componentId,message, innerException, serverName)
        {
        }


        /// <summary>
        /// Initializes a new instance of the RFIDTimeoutException 
        /// <see cref="KTone.Core.KTIRFID.RFIDTimeoutException"/> class with serialized data.
        /// </summary>
        /// <param name="exceptionInfo">The object that holds the serialized object data.</param>
        /// <param name="exceptionContext">The contextual information about the source or destination. 
        ///</param>
        public RFIDTimeoutException(SerializationInfo exceptionInfo, StreamingContext exceptionContext)
            : base(exceptionInfo, exceptionContext)
        {
        }


        #endregion
    }
    
    #region Tibco Exceptions
    /// <summary>
    /// <exclude />
    /// The exception that is thrown when an error occurs in creating instance
    /// of a MessageReceivedArgs object with a null Message Object.
    /// </summary>
    [Serializable]
    public class InvalidMessageException : KTComponentException
    {
        /// <summary>
        /// Initializes a new instance of the InvalidMessageException
        /// class with a specific erroe message
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="info">A message that describes the error. </param>
        public InvalidMessageException(string componentId, string info) : base(componentId, info) { }

        /// <summary>
        /// Initializes a new instance of the InvalidMessageException
        /// class with a specific erroe message and message and stack trace of the inner exception that 
        /// is the cause of this exception.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="info">A message that describes the error. </param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public InvalidMessageException(string componentId, string info, Exception innerException) : base(componentId, info, innerException) { }
    }


    /// <summary>
    /// <exclude />
    /// The exception that is thrown when an error occurs in initialising
    /// the wrapper class to open TIBCO environment.
    /// </summary>
    [Serializable]
    public class WrapperInitializationException : KTComponentException
    {
        /// <summary>
        /// Initializes a new instance of the WrapperInitializationException
        /// class with a specific erroe message
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="info">A message that describes the error. </param>
        public WrapperInitializationException(string componentId, string info) : base(componentId, info) { }

        /// <summary>
        /// Initializes a new instance of the WrapperInitializationException
        /// class with a specific erroe message and message and stack trace of the inner exception that 
        /// is the cause of this exception.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="info">A message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public WrapperInitializationException(string componentId, string info, Exception innerException) : base(componentId, info, innerException) { }
    }


    /// <summary>
    /// <exclude />
    /// The exception that is thrown when an error occurs in starting
    /// the wrapper object to create initiate the publisher and listener.
    /// </summary>
    [Serializable]
    public class WrapperStartException : KTComponentException
    {
        /// <summary>
        /// Initializes a new instance of the WrapperStartException
        /// class with a specific erroe message
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="info">A message that describes the error. </param>
        public WrapperStartException(string componentId, string info) : base(componentId, info) { }

        /// <summary>
        /// Initializes a new instance of the WrapperStartException
        /// class with a specific erroe message and message and stack trace of the inner exception that 
        /// is the cause of this exception.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="info">A message that describes the error. </param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public WrapperStartException(string componentId, string info, Exception innerException) : base(componentId, info, innerException) { }
    }


    /// <summary>
    /// <exclude />
    /// The exception that is thrown when an error occurs in creating
    /// a publisher object for publishing subjects with given delivery mode.
    /// </summary>
    [Serializable]
    public class PublisherCreationException : KTComponentException
    {
        /// <summary>
        /// Initializes a new instance of the PublisherCreationException
        /// class with a specific erroe message
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="info">A message that describes the error. </param>
        public PublisherCreationException(string componentId, string info) : base(componentId, info) { }

        /// <summary>
        /// Initializes a new instance of the PublisherCreationException
        /// class with a specific erroe message and message and stack trace of the inner exception that 
        /// is the cause of this exception.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="info">A message that describes the error. </param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public PublisherCreationException(string componentId, string info, Exception innerException) : base(componentId, info, innerException) { }
    }


    /// <summary>
    /// <exclude />
    /// The exception that is thrown when an error occurs in setting
    /// the subject for a message Object.
    /// </summary>
    [Serializable]
    public class MessageSubjectException : KTComponentException
    {
        /// <summary>
        /// Initializes a new instance of the MessageSubjectException
        /// class with a specific erroe message
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="info">A message that describes the error. </param>
        public MessageSubjectException(string componentId, string info) : base(componentId, info) { }

        /// <summary>
        /// Initializes a new instance of the MessageSubjectException
        /// class with a specific erroe message and message and stack trace of the inner exception that 
        /// is the cause of this exception.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="info">A message that describes the error. </param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public MessageSubjectException(string componentId, string info, Exception innerException) : base(componentId, info, innerException) { }
    }


    /// <summary>
    /// <exclude />
    /// The exception that is thrown when an error occurs in sending
    /// a message object with a publisher of defined delivery mode.
    /// </summary>
    [Serializable]
    public class MessageSentException : KTComponentException
    {
        /// <summary>
        /// Initializes a new instance of the MessageSentException
        /// class with a specific erroe message
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="info">A message that describes the error. </param>
        public MessageSentException(string componentId, string info) : base(componentId, info) { }

        /// <summary>
        /// Initializes a new instance of the MessageSentException
        /// class with a specific erroe message and message and stack trace of the inner exception that 
        /// is the cause of this exception.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="info">A message that describes the error. </param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public MessageSentException(string componentId, string info, Exception innerException) : base(componentId, info, innerException) { }
    }


    /// <summary>
    /// <exclude />
    /// The exception that is thrown when an error occurs in creating
    /// a message queue object for listening to the messages for a particular subject.
    /// </summary>
    [Serializable]
    public class QueueCreationException : KTComponentException
    {
        /// <summary>
        /// Initializes a new instance of the QueueCreationException
        /// class with a specific erroe message
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="info">A message that describes the error. </param>
        public QueueCreationException(string componentId, string info) : base(componentId, info) { }

        /// <summary>
        /// Initializes a new instance of the QueueCreationException
        /// class with a specific erroe message and message and stack trace of the inner exception that 
        /// is the cause of this exception.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="info">A message that describes the error. </param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public QueueCreationException(string componentId, string info, Exception innerException) : base(componentId, info, innerException) { }
    }


    /// <summary>
    /// <exclude />
    /// The exception that is thrown when an error occurs in creating
    /// a dispatcher object for a particular message queue.
    /// </summary>
    [Serializable]
    public class DispatcherException : KTComponentException
    {
        /// <summary>
        /// Initializes a new instance of the DispatcherException
        /// class with a specific erroe message
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="info">A message that describes the error. </param>
        public DispatcherException(string componentId, string info) : base(componentId, info) { }

        /// <summary>
        /// Initializes a new instance of the DispatcherException
        /// class with a specific erroe message and message and stack trace of the inner exception that 
        /// is the cause of this exception.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="info">A message that describes the error. </param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public DispatcherException(string componentId, string info, Exception innerException) : base(componentId, info, innerException) { }
    }


    /// <summary>
    /// <exclude />
    /// The exception that is thrown when an error occurs in creating
    /// a listener object for a particular subject.
    /// </summary>
    [Serializable]
    public class ListenerCreationException : KTComponentException
    {
        /// <summary>
        /// Initializes a new instance of the ListenerCreationException
        /// class with a specific erroe message
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="info">A message that describes the error. </param>
        public ListenerCreationException(string componentId, string info) : base(componentId, info) { }

        /// <summary>
        /// Initializes a new instance of the ListenerCreationException
        /// class with a specific erroe message and message and stack trace of the inner exception that 
        /// is the cause of this exception.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="info">A message that describes the error. </param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public ListenerCreationException(string componentId, string info, Exception innerException) : base(componentId, info, innerException) { }
    }


    /// <summary>
    /// <exclude />
    /// The exception that is thrown when an error occurs in registering
    /// a subject to the subject pool for publishers and listeners.
    /// </summary>
    [Serializable]
    public class SubjectRegistrationException : KTComponentException
    {
        /// <summary>
        /// Initializes a new instance of the SubjectRegistrationException
        /// class with a specific erroe message
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="info">A message that describes the error. </param>
        public SubjectRegistrationException(string componentId, string info) : base(componentId, info) { }

        /// <summary>
        /// Initializes a new instance of the SubjectRegistrationException
        /// class with a specific erroe message and message and stack trace of the inner exception that 
        /// is the cause of this exception.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="info">A message that describes the error. </param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public SubjectRegistrationException(string componentId, string info, Exception innerException) : base(componentId, info, innerException) { }
    }


    /// <summary>
    /// <exclude />
    /// The exception that is thrown when an error occurs in unregistering
    /// a subject from the subject pool for the publishers and listeners.
    /// </summary>
    [Serializable]
    public class SubjectUnregistrationException : KTComponentException
    {
        /// <summary>
        /// Initializes a new instance of the SubjectUnregistrationException
        /// class with a specific erroe message
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="info">A message that describes the error. </param>
        public SubjectUnregistrationException(string componentId, string info) : base(componentId, info) { }

        /// <summary>
        /// Initializes a new instance of the SubjectUnregistrationException
        /// class with a specific erroe message and message and stack trace of the inner exception that 
        /// is the cause of this exception.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="info">A message that describes the error. </param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public SubjectUnregistrationException(string componentId, string info, Exception innerException) : base(componentId, info, innerException) { }
    }

    #endregion Tibco Exceptions


    /// <summary>
    /// The exception that is thrown when an error occurs in the execution of a device object 
    /// that supports IRFIDDevice interface.
    /// </summary>
    [SerializableAttribute]
    public class RFIDDeviceException : KTComponentException
    {
       #region Constructors

        /// <summary>
        /// Initializes a new instance of the RFIDDeviceException 
        /// <see cref="KTone.Core.KTIRFID.RFIDDeviceException"/> 
        /// class with a specified error message.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        public RFIDDeviceException(string componentId, string message)
            : base(componentId,message)
        {
        }


        /// <summary>
        /// Initializes a new instance of the RFIDDeviceException 
        /// <see cref="KTone.Core.KTIRFID.RFIDDeviceException"/> 
        /// class with a specified error message and message and stack trace of the inner exception that 
        /// is the cause of this exception.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public RFIDDeviceException(string componentId, string message, Exception innerException)
            : base(componentId,message, innerException)
        {
        }


        /// <summary>
        /// Initializes a new instance of the RFIDDeviceException 
        /// <see cref="KTone.Core.KTIRFID.RFIDDeviceException"/> 
        /// class with a specified error message and the name of the server where this exception is generated.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="serverName">The name of the server where this exception is generated.</param>
        public RFIDDeviceException(string componentId, string message, string serverName)
            : base(componentId,message, serverName)
        {
        }


        /// <summary>
        /// Initializes a new instance of the RFIDDeviceException 
        /// <see cref="KTone.Core.KTIRFID.RFIDDeviceException"/> 
        /// class with a specified error message, message and stack trace of the inner exception that is the cause 
        /// of this exception and the name of the server where this exception is generated.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        /// <param name="serverName">The name of the server where this exception is generated.</param>
        public RFIDDeviceException(string componentId, string message, Exception innerException, string serverName)
            : base(componentId,message, innerException, serverName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the RFIDDeviceException 
        /// <see cref="KTone.Core.KTIRFID.RFIDDeviceException"/> class with serialized data.
        /// </summary>
        /// <param name="exceptionInfo">The object that holds the serialized object data.</param>
        /// <param name="exceptionContext">The contextual information about the source or destination. 
        ///</param>
        public RFIDDeviceException(SerializationInfo exceptionInfo, StreamingContext exceptionContext)
            : base(exceptionInfo, exceptionContext)
        {
        }


        #endregion    
    }


    /// <summary>
    /// <exclude />
    /// The exception that is thrown when an error occurs in the execution of a KTAgent object 
    /// that supports IKTAgent interface.
    /// </summary>
    [SerializableAttribute]
    public class KTAgentException : KTComponentException 
    {
        #region Attributes
        string mAgentId;
        #endregion Attributes

        #region Constructors


        /// <summary>
        /// Initializes a new instance of the KTAgentException         
        /// class with a specified error message.
        /// </summary>
        /// <param name="AgentId">Agent Id. </param>
        /// <param name="message">A message that describes the error. </param>
        public KTAgentException(string AgentId, string message)
            : base(AgentId, message)
        {
            this.mAgentId = AgentId ;
        }


        /// <summary>
        /// Initializes a new instance of the KTAgentException
        /// class with a specified error message and message and stack trace of the inner exception that 
        /// is the cause of this exception.
        /// </summary>
        /// <param name="AgentId">Agent Id. </param>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public KTAgentException(string AgentId, string message, Exception innerException)
            : base(AgentId,message, innerException)
        {
            this.mAgentId = AgentId;
        }


        /// <summary>
        /// Initializes a new instance of the KTAgentException 
        /// class with a specified error message and the name of the server where this exception is generated.
        /// </summary>
        /// <param name="AgentId">Agent Id. </param>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="serverName">The name of the server where this exception is generated.</param>
        public KTAgentException(string AgentId, string message, string serverName)
            : base(message, serverName)
        {
            this.mAgentId = AgentId;
        }


        /// <summary>
        /// Initializes a new instance of the KTAgentException 
        /// class with a specified error message, message and stack trace of the inner exception that is the cause 
        /// of this exception and the name of the server where this exception is generated.
        /// </summary>
        /// <param name="AgentId">Agent Id. </param>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        /// <param name="serverName">The name of the server where this exception is generated.</param>
        public KTAgentException(string AgentId, string message, Exception innerException, string serverName)
            : base(AgentId, message, innerException, serverName)
        {
            this.mAgentId = AgentId;
        }

        /// <summary>
        /// Initializes a new instance of the KTAgentException 
        /// </summary>
        /// <param name="exceptionInfo">The object that holds the serialized object data.</param>
        /// <param name="exceptionContext">The contextual information about the source or destination. 
        ///</param>
        public KTAgentException(SerializationInfo exceptionInfo, StreamingContext exceptionContext)
            : base(exceptionInfo, exceptionContext)
        {
            this.mAgentId = exceptionInfo.GetString("AgentId");
        }


        #endregion

        #region Serialization

        /// <summary>
        /// Sets the SerializationInfo with information about the exception.
        /// </summary>
        /// <param name="exceptionInfo">The SerializationInfo that holds the 
        /// serialized object data about the exception being thrown. </param>
        /// <param name="exceptionContext">The StreamingContext that contains 
        /// contextual information about the source or destination. </param>
        public override void GetObjectData(SerializationInfo exceptionInfo,
            StreamingContext exceptionContext)
        {
            base.GetObjectData(exceptionInfo, exceptionContext);
            exceptionInfo.AddValue("AgentId", this.mAgentId);
        }
        #endregion

        #region Properties
        public string AgentId
        {
            get
            {
                return this.mAgentId;
            }
        }
        #endregion Properties
    }


    /// <summary>
    /// <exclude />
    /// The exception that is thrown when an error occurs in the command execution of a printer object.
    /// </summary>
    [SerializableAttribute]
    public class KTPrinterException : KTComponentException
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the KTPrinterException 
        /// <see cref="KTone.Core.KTIRFID.KTPrinterException"/> 
        /// class with a specified error message.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        public KTPrinterException(string componentId, string message)
            : base(componentId, message)
        {
        }


        /// <summary>
        /// Initializes a new instance of the KTPrinterException 
        /// <see cref="KTone.Core.KTIRFID.KTPrinterException"/> 
        /// class with a specified error message and message and stack trace of the inner exception that 
        /// is the cause of this exception.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public KTPrinterException(string componentId, string message, Exception innerException)
            : base(componentId, message, innerException)
        {
        }


        /// <summary>
        /// Initializes a new instance of the KTPrinterException 
        /// <see cref="KTone.Core.KTIRFID.KTPrinterException"/> 
        /// class with a specified error message and the name of the server where this exception is generated.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="serverName">The name of the server where this exception is generated.</param>
        public KTPrinterException(string componentId, string message, string serverName)
            : base(componentId, message, serverName)
        {
        }


        /// <summary>
        /// Initializes a new instance of the KTPrinterException 
        /// <see cref="KTone.Core.KTIRFID.KTPrinterException"/> 
        /// class with a specified error message, message and stack trace of the inner exception that is the cause 
        /// of this exception and the name of the server where this exception is generated.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        /// <param name="serverName">The name of the server where this exception is generated.</param>
        public KTPrinterException(string componentId, string message, Exception innerException, string serverName)
            : base(componentId, message, innerException, serverName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the KTPrinterException 
        /// <see cref="KTone.Core.KTIRFID.KTPrinterException"/> class with serialized data.
        /// </summary>
        /// <param name="exceptionInfo">The object that holds the serialized object data.</param>
        /// <param name="exceptionContext">The contextual information about the source or destination. 
        ///</param>
        public KTPrinterException(SerializationInfo exceptionInfo, StreamingContext exceptionContext)
            : base(exceptionInfo, exceptionContext)
        {
        }


        #endregion
    }

    /// <summary>
    /// <exclude />
    /// The exception that is thrown when an error occurs in the Licensing methods
    /// </summary>
    [SerializableAttribute]
    public class KTLicenseException : KTAppException
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the KTLicenseException 
        /// class with a specified error message.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        public KTLicenseException(string componentId, string message)
            : base(componentId, message)
        {
        }



        /// <summary>
        /// Initializes a new instance of the KTLicenseException 
        ///  class with serialized data.
        /// </summary>
        /// <param name="exceptionInfo">The object that holds the serialized object data.</param>
        /// <param name="exceptionContext">The contextual information about the source or destination. 
        ///</param>
        public KTLicenseException(SerializationInfo exceptionInfo, StreamingContext exceptionContext)
            : base(exceptionInfo, exceptionContext)
        {
        }


        #endregion
    }


    /// <summary>
    /// <exclude />
    /// The exception that is thrown when an error occurs in the execution of a KTAgent object 
    /// that supports IKTAlert interface.
    /// </summary>
    [SerializableAttribute]
    public class KTAlertException : KTComponentException
    {        
        #region Constructors


        /// <summary>
        /// Initializes a new instance of the KTAlertException         
        /// class with a specified error message.
        /// </summary>
        /// <param name="AlertId">Alert Id. </param>
        /// <param name="message">A message that describes the error. </param>
        public KTAlertException(string alertId, string message)
            : base(alertId, message)
        {
            
        }


        /// <summary>
        /// Initializes a new instance of the KTAlertException
        /// class with a specified error message and message and stack trace of the inner exception that 
        /// is the cause of this exception.
        /// </summary>
        /// <param name="AlertId">alert Id. </param>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public KTAlertException(string alertId, string message, Exception innerException)
            : base(alertId, message, innerException)
        {
            
        }


        /// <summary>
        /// Initializes a new instance of the KTAlertException 
        /// class with a specified error message and the name of the server where this exception is generated.
        /// </summary>
        /// <param name="AlertId">Alert Id. </param>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="serverName">The name of the server where this exception is generated.</param>
        public KTAlertException(string alertId, string message, string serverName)
            : base(message, serverName)
        {
           
        }


        /// <summary>
        /// Initializes a new instance of the KTAlertException 
        /// class with a specified error message, message and stack trace of the inner exception that is the cause 
        /// of this exception and the name of the server where this exception is generated.
        /// </summary>
        /// <param name="AlertId">alert Id. </param>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        /// <param name="serverName">The name of the server where this exception is generated.</param>
        public KTAlertException(string alertId, string message, Exception innerException, string serverName)
            : base(alertId, message, innerException, serverName)
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the KTAlertException 
        /// </summary>
        /// <param name="exceptionInfo">The object that holds the serialized object data.</param>
        /// <param name="exceptionContext">The contextual information about the source or destination. 
        ///</param>
        public KTAlertException(SerializationInfo exceptionInfo, StreamingContext exceptionContext)
            : base(exceptionInfo, exceptionContext)
        {
            
        }


        #endregion

      
    }

    /// <summary>
    /// The exception that is thrown when an error occurs in the execution of transformation 
    /// of data according to template.
    /// </summary>
    [SerializableAttribute]
    public class KTTransformException : KTComponentException
    {
        #region Constructors
       
        /// <summary>
        /// Initializes a new instance of the KTTransformException 
        /// class with a specified error message.
        /// </summary>
        /// <param name="componentId"></param>
        /// <param name="message"></param>
        public KTTransformException(string componentId, string message)
            : base(componentId, message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the KTTransformException 
        /// class with a specified error message and innerException.
        /// </summary>
        /// <param name="componentId"></param>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public KTTransformException(string componentId, string message, Exception innerException)
            : base(componentId, message, innerException)
        {

        }

        /// <summary>
        /// Initializes a new instance of the KTTransformException 
        /// class with a specified error message and server name.
        /// </summary>
        /// <param name="componentId"></param>
        /// <param name="message"></param>
        /// <param name="serverName"></param>
        public KTTransformException(string componentId, string message, string serverName)
            : base(message, serverName)
        {

        }

        /// <summary>
        /// Initializes a new instance of the KTTransformException 
        /// </summary>
        /// <param name="componentId"></param>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        /// <param name="serverName"></param>
        public KTTransformException(string componentId, string message, Exception innerException, string serverName)
            : base(componentId, message, innerException, serverName)
        {

        }
       
        /// <summary>
        /// Initializes a new instance of the KTTransformException 
        /// </summary>
        /// <param name="exceptionInfo"></param>
        /// <param name="exceptionContext"></param>
        public KTTransformException(SerializationInfo exceptionInfo, StreamingContext exceptionContext)
            : base(exceptionInfo, exceptionContext)
        {

        }

        /// <summary>
        /// Initializes a new instance of the KTTransformException
        /// </summary>
        /// <param name="componentId"></param>
        /// <param name="msgFormat"></param>
        /// <param name="lstParam"></param>
        public KTTransformException(string componentId, string msgFormat, params object[] lstParam)
            : base(componentId,String.Format(msgFormat, lstParam))
		{			
			
		}



        #endregion


    }

    #region [CognexAPIsException]
    /// <summary>
    /// <exclude />
    /// The exception that is thrown when an error occurs in the command execution of a KTImageProcessor object.
    /// </summary>
    [SerializableAttribute]
    public class KTCognexAPIsException : KTComponentException
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the KTCognexAPIsException 
        /// class with a specified error message.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        public KTCognexAPIsException(string componentId, string message)
            : base(componentId, message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the KTCognexAPIsException 
        /// class with a specified error message and message and stack trace of the inner exception that 
        /// is the cause of this exception.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public KTCognexAPIsException(string componentId, string message, Exception innerException)
            : base(componentId, message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the KTCognexAPIsException 
        /// <see cref="KTone.Core.KTIRFID.KTCognexAPIsException"/> class with serialized data.
        /// </summary>
        /// <param name="exceptionInfo">The object that holds the serialized object data.</param>
        /// <param name="exceptionContext">The contextual information about the source or destination. 
        ///</param>
        public KTCognexAPIsException(SerializationInfo exceptionInfo, StreamingContext exceptionContext)
            : base(exceptionInfo, exceptionContext)
        {
        }

        #endregion
    }

    [SerializableAttribute]
    public class KTCognexVProRootException : KTCognexAPIsException
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the KTCognexVProRootException 
        /// class with a specified error message.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        public KTCognexVProRootException(string componentId, string message)
            : base(componentId, message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the KTCognexVProRootException 

        /// class with a specified error message and message and stack trace of the inner exception that 
        /// is the cause of this exception.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public KTCognexVProRootException(string componentId, string message, Exception innerException)
            : base(componentId, message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the KTCognexVProRootException 
        /// <see cref="KTone.Core.KTIRFID.KTCognexVProRootException"/> class with serialized data.
        /// </summary>
        /// <param name="exceptionInfo">The object that holds the serialized object data.</param>
        /// <param name="exceptionContext">The contextual information about the source or destination. 
        ///</param>
        public KTCognexVProRootException(SerializationInfo exceptionInfo, StreamingContext exceptionContext)
            : base(exceptionInfo, exceptionContext)
        {
        }
        #endregion
    }

    [SerializableAttribute]
    public class KTCognexImageFileToolException : KTCognexAPIsException
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the KTCognexImageFileToolException 
        /// class with a specified error message.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        public KTCognexImageFileToolException(string componentId, string message)
            : base(componentId, message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the KTCognexImageFileToolException 
        /// <see cref="KTone.Core.KTIRFID.KTCognexImageFileToolException"/> 
        /// class with a specified error message and message and stack trace of the inner exception that 
        /// is the cause of this exception.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public KTCognexImageFileToolException(string componentId, string message, Exception innerException)
            : base(componentId, message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the KTCognexImageFileToolException 
        /// <see cref="KTone.Core.KTIRFID.KTCognexImageFileToolException"/> class with serialized data.
        /// </summary>
        /// <param name="exceptionInfo">The object that holds the serialized object data.</param>
        /// <param name="exceptionContext">The contextual information about the source or destination. 
        ///</param>
        public KTCognexImageFileToolException(SerializationInfo exceptionInfo, StreamingContext exceptionContext)
            : base(exceptionInfo, exceptionContext)
        {
        }
        #endregion
    }

    [SerializableAttribute]
    public class KTCognexSFPatternException : KTCognexAPIsException
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the KTCognexSFPatternException 
        /// class with a specified error message.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        public KTCognexSFPatternException(string componentId, string message)
            : base(componentId, message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the KTCognexSFPatternException 
        /// <see cref="KTone.Core.KTIRFID.KTCognexSFPatternException"/> 
        /// class with a specified error message and message and stack trace of the inner exception that 
        /// is the cause of this exception.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public KTCognexSFPatternException(string componentId, string message, Exception innerException)
            : base(componentId, message, innerException)
        {
        }


        /// <summary>
        /// Initializes a new instance of the KTCognexSFPatternException 
        /// <see cref="KTone.Core.KTIRFID.KTCognexSFPatternException"/> class with serialized data.
        /// </summary>
        /// <param name="exceptionInfo">The object that holds the serialized object data.</param>
        /// <param name="exceptionContext">The contextual information about the source or destination. 
        ///</param>
        public KTCognexSFPatternException(SerializationInfo exceptionInfo, StreamingContext exceptionContext)
            : base(exceptionInfo, exceptionContext)
        {
        }
        #endregion
    }

    [SerializableAttribute]
    public class KTCognexRotatePatternException : KTCognexAPIsException
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the KTCognexRotatePatternException 
        /// class with a specified error message.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        public KTCognexRotatePatternException(string componentId, string message)
            : base(componentId, message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the KTCognexRotatePatternException 
        /// <see cref="KTone.Core.KTIRFID.KTCognexRotatePatternException"/> 
        /// class with a specified error message and message and stack trace of the inner exception that 
        /// is the cause of this exception.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public KTCognexRotatePatternException(string componentId, string message, Exception innerException)
            : base(componentId, message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the KTCognexRotatePatternException 
        /// <see cref="KTone.Core.KTIRFID.KTCognexRotatePatternException"/> class with serialized data.
        /// </summary>
        /// <param name="exceptionInfo">The object that holds the serialized object data.</param>
        /// <param name="exceptionContext">The contextual information about the source or destination. 
        ///</param>
        public KTCognexRotatePatternException(SerializationInfo exceptionInfo, StreamingContext exceptionContext)
            : base(exceptionInfo, exceptionContext)
        {
        }
        #endregion
    }

    [SerializableAttribute]
    public class KTCognexProcessImageException : KTCognexAPIsException
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the KTCognexProcessImageException 
        /// class with a specified error message.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        public KTCognexProcessImageException(string componentId, string message)
            : base(componentId, message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the KTCognexProcessImageException 
        /// <see cref="KTone.Core.KTIRFID.KTCognexProcessImageException"/> 
        /// class with a specified error message and message and stack trace of the inner exception that 
        /// is the cause of this exception.
        /// </summary>
        /// <param name="componentId">Component Id. </param>
        /// <param name="message">A message that describes the error. </param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public KTCognexProcessImageException(string componentId, string message, Exception innerException)
            : base(componentId, message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the KTCognexProcessImageException 
        /// <see cref="KTone.Core.KTIRFID.KTCognexProcessImageException"/> class with serialized data.
        /// </summary>
        /// <param name="exceptionInfo">The object that holds the serialized object data.</param>
        /// <param name="exceptionContext">The contextual information about the source or destination. 
        ///</param>
        public KTCognexProcessImageException(SerializationInfo exceptionInfo, StreamingContext exceptionContext)
            : base(exceptionInfo, exceptionContext)
        {
        }
        #endregion
    }

    #region [toolwise exceptions]

    #region [VPROROOT]
    [SerializableAttribute]
    public class KTCogVPROROOTException : KTCognexAPIsException
    {
        #region Constructors
        public KTCogVPROROOTException(string componentId, string message)
            : base(componentId, message)
        {
        }
        public KTCogVPROROOTException(SerializationInfo exceptionInfo, StreamingContext exceptionContext)
            : base(exceptionInfo, exceptionContext)
        {
        }
        #endregion
    }
    #endregion [VPROROOT]

    #region [CogIPOneImageTool]
    [SerializableAttribute]
    public class KTCogIPOneImageToolException : KTCognexAPIsException
    {
        #region Constructors
        public KTCogIPOneImageToolException(string componentId, string message)
            : base(componentId, message)
        {
        }
        public KTCogIPOneImageToolException(SerializationInfo exceptionInfo, StreamingContext exceptionContext)
            : base(exceptionInfo, exceptionContext)
        {
        }
        #endregion
    }
    #endregion [CogIPOneImageTool]

    #region [CogPMAlignTool]
    [SerializableAttribute]
    public class KTCogPMAlignToolException : KTCognexAPIsException
    {
        #region Constructors
        public KTCogPMAlignToolException(string componentId, string message)
            : base(componentId, message)
        {
        }
        public KTCogPMAlignToolException(SerializationInfo exceptionInfo, StreamingContext exceptionContext)
            : base(exceptionInfo, exceptionContext)
        {
        }
        #endregion
    }
    #endregion [CogPMAlignTool]

    #region [CogPMAlignPattern]
    [SerializableAttribute]
    public class KTCogPMAlignPatternException : KTCognexAPIsException
    {
        #region Constructors
        public KTCogPMAlignPatternException(string componentId, string message)
            : base(componentId, message)
        {
        }
        public KTCogPMAlignPatternException(SerializationInfo exceptionInfo, StreamingContext exceptionContext)
            : base(exceptionInfo, exceptionContext)
        {
        }
        #endregion
    }
    #endregion [CogPMAlignPattern]

    #region [CogFixtureTool]
    [SerializableAttribute]
    public class KTCogFixtureToolException : KTCognexAPIsException
    {
        #region Constructors
        public KTCogFixtureToolException(string componentId, string message)
            : base(componentId, message)
        {
        }
        public KTCogFixtureToolException(SerializationInfo exceptionInfo, StreamingContext exceptionContext)
            : base(exceptionInfo, exceptionContext)
        {
        }
        #endregion
    }
    #endregion [CogFixtureTool]

    #region [CogOCVTool]
    [SerializableAttribute]
    public class KTCogOCVToolException : KTCognexAPIsException
    {
        #region Constructors
        public KTCogOCVToolException(string componentId, string message)
            : base(componentId, message)
        {
        }
        public KTCogOCVToolException(SerializationInfo exceptionInfo, StreamingContext exceptionContext)
            : base(exceptionInfo, exceptionContext)
        {
        }
        #endregion
    }
    #endregion [CogOCVTool]

    #region [CogOCFont]
    [SerializableAttribute]
    public class KTCogOCFontException : KTCognexAPIsException
    {
        #region Constructors
        public KTCogOCFontException(string componentId, string message)
            : base(componentId, message)
        {
        }
        public KTCogOCFontException(SerializationInfo exceptionInfo, StreamingContext exceptionContext)
            : base(exceptionInfo, exceptionContext)
        {
        }
        #endregion
    }
    #endregion [CogOCFont]

    #region [CogImageConvertTool]
    [SerializableAttribute]
    public class KTCogImageConvertToolException : KTCognexAPIsException
    {
        #region Constructors
        public KTCogImageConvertToolException(string componentId, string message)
            : base(componentId, message)
        {
        }
        public KTCogImageConvertToolException(SerializationInfo exceptionInfo, StreamingContext exceptionContext)
            : base(exceptionInfo, exceptionContext)
        {
        }
        #endregion
    }
    #endregion [CogImageConvertTool]


    #endregion [toolwise exceptions]

    #endregion [CognexAPIsException]

}
