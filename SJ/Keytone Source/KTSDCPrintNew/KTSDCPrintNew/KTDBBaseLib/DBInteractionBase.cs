////////////////////////////////////////////////////////////////////////////////
// Description: Base class for Database Interaction.                       
// Because this class implements IDisposable, derived classes shouldn't do so.
///////////////////////////////////////////////////////////////////////////////

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Threading;

namespace KTone.DAL.KTDBBaseLib
{
    /// <summary>
    /// Purpose: Error Enums used by this LLBL library.
    /// </summary>
    public enum LLBLError
    {
        AllOk
        // Add more here (check the comma's!)
    }


    /// <summary>
    /// Purpose: General interface of the API generated. Contains only common methods of all classes.
    /// </summary>
    public interface ICommonDBAccess
    {
        bool Insert();
        bool Update();
        bool Delete();
        DataTable SelectOne();
        DataTable SelectAll();
    }


    /// <summary>
    /// Purpose: Abstract base class for Database Interaction classes.
    /// </summary>
    public abstract class DBInteractionBase : IDisposable, ICommonDBAccess
    {
        #region Class Member Declarations
        private static string _strDBConnection = string.Empty;
        private static string _strTrackerDBConnection = string.Empty;
        protected SqlConnection _mainConnection;
        protected SqlConnection _trackerRetailConnection;
        protected SqlInt32 _errorCode;

        protected bool _trackerRetailConnectionIsCreatedLocal;
        protected ConnectionProvider _trackerRetailConnectionProvider;

        protected bool _mainConnectionIsCreatedLocal;
        protected ConnectionProvider _mainConnectionProvider;

        private bool _isDisposed;
        protected NLog.Logger _log = NLog.LogManager.GetCurrentClassLogger();
        private static bool _createConnection = true;
        #endregion


        /// <summary>
        /// Purpose: Class constructor.
        /// </summary>
        public DBInteractionBase()
        {
            if (!_createConnection)
                return;
            // Initialize the class' members.
            if (_strDBConnection == string.Empty)
                throw new ApplicationException("Set the database connection string before using the class.");
            InitClass();
        }

        /// <summary>
        /// It is useful in HH host application for working in Offline mode
        /// </summary>
        public static bool CreateDBConnection
        {
            set
            {
                _createConnection = value;
            }
            get
            {
                return _createConnection;
            }
        }
        /// <summary>
        /// Connection string on which the class will work.
        /// Set it before using the class
        /// </summary>
        public static string DBConnString
        {
            set
            {
                _strDBConnection = value;
            }
            get
            {
                return _strDBConnection;
            }
        }

        public static string TrackerDBConnString
        {
            set
            {
                _strTrackerDBConnection = value;
            }
            get
            {
                return _strTrackerDBConnection;
            }
        }

        /// <summary>
        /// Purpose: Initializes class members.
        /// </summary>
        private void InitClass()
        {
            // create all the objects and initialize other members.
            _mainConnection = new SqlConnection();
            _mainConnectionIsCreatedLocal = true;
            _mainConnectionProvider = null;

            // Set connection string of the sqlconnection object
            _mainConnection.ConnectionString = _strDBConnection;

            _trackerRetailConnection = new SqlConnection();
            _trackerRetailConnectionIsCreatedLocal = true;
            _trackerRetailConnectionProvider = null;

            _trackerRetailConnection.ConnectionString = _strTrackerDBConnection;
            _errorCode = 0;
           
            _isDisposed = false;
        }


        /// <summary>
        /// Purpose: Implements the IDispose' method Dispose.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        /// <summary>
        /// Purpose: Implements the Dispose functionality.
        /// </summary>
        protected virtual void Dispose(bool isDisposing)
        {
            // Check to see if Dispose has already been called.
            if (!_isDisposed)
            {
                if (isDisposing)
                {
                    // Dispose managed resources.
                    if (_mainConnectionIsCreatedLocal)
                    {
                        // Object is created in this class, so destroy it here.
                        _mainConnection.Close();
                        _mainConnection.Dispose();
                        _mainConnectionIsCreatedLocal = false;
                    }
                    _mainConnectionProvider = null;
                    _mainConnection = null;
                }
            }
            _isDisposed = true;
        }


        /// <summary>
        /// Purpose: Implements the ICommonDBAccess.Insert() method.
        /// </summary>
        public virtual bool Insert()
        {
            // No implementation, throw exception
            throw new NotImplementedException();
        }


        /// <summary>
        /// Purpose: Implements the ICommonDBAccess.Delete() method.
        /// </summary>
        public virtual bool Delete()
        {
            // No implementation, throw exception
            throw new NotImplementedException();
        }


        /// <summary>
        /// Purpose: Implements the ICommonDBAccess.Update() method.
        /// </summary>
        public virtual bool Update()
        {
            // No implementation, throw exception
            throw new NotImplementedException();
        }


        /// <summary>
        /// Purpose: Implements the ICommonDBAccess.SelectOne() method.
        /// </summary>
        public virtual DataTable SelectOne()
        {
            // No implementation, throw exception
            throw new NotImplementedException();
        }


        /// <summary>
        /// Purpose: Implements the ICommonDBAccess.SelectAll() method.
        /// </summary>
        public virtual DataTable SelectAll()
        {
            // No implementation, throw exception
            throw new NotImplementedException();
        }

        protected void UpdateNotifyCacheUpdateTable()
        {
            try
            {
                NotifyCacheUpdate notifyTbl = new NotifyCacheUpdate(_mainConnection);
                notifyTbl.daTimeStamp = DateTime.Now;
                notifyTbl.iID = 1;
                notifyTbl.Update();
            }
            catch (Exception ex)
            {
                _log.Error("DBInteractionBase::UpdateNotifyCacheUpdateTable : {0}", ex.Message);
                throw new Exception("DBInteractionBase::UpdateNotifyCacheUpdateTable::Error occured." + ex.Message, ex);
            }
        }

        protected void UpdateNotifyCacheInTransaction(ConnectionProvider objConnectionProvider)
        {
            try
            {
                NotifyCacheUpdate.CreateDBConnection = false;
                NotifyCacheUpdate notifyTbl = new NotifyCacheUpdate(_mainConnection);
                notifyTbl.MainConnectionProvider = objConnectionProvider;

                notifyTbl.daTimeStamp = DateTime.Now;
                notifyTbl.iID = 1;
                notifyTbl.Update();
            }
            catch (Exception ex)
            {
                _log.Error("DBInteractionBase::UpdateNotifyCacheInTransaction : {0}", ex.Message);
                throw new Exception("DBInteractionBase::UpdateNotifyCacheInTransaction::Error occured." + ex.Message, ex);
            }
            finally
            {
                NotifyCacheUpdate.CreateDBConnection = true;
            }
        }

        #region Class Property Declarations
        public ConnectionProvider MainConnectionProvider
        {
            set
            {
                if (value == null)
                {
                    // Invalid value
                    throw new ArgumentNullException("MainConnectionProvider", "Null passed as value to this property which is not allowed.");
                }

                // A connection provider object is passed to this class.
                // Retrieve the SqlConnection object, if present and create a
                // reference to it. If there is already a MainConnection object
                // referenced by the membervar, destroy that one or simply 
                // remove the reference, based on the flag.
                if (_mainConnection != null)
                {
                    // First get rid of current connection object. Caller is responsible
                    if (_mainConnectionIsCreatedLocal)
                    {
                        // Is local created object, close it and dispose it.
                        _mainConnection.Close();
                        _mainConnection.Dispose();
                    }
                    // Remove reference.
                    _mainConnection = null;
                }
                _mainConnectionProvider = (ConnectionProvider)value;
                _mainConnection = _mainConnectionProvider.DBConnection;
                _mainConnectionIsCreatedLocal = false;
            }

            get
            {
               return _mainConnectionProvider;
            
            }

        }


        public SqlInt32 ErrorCode
        {
            get
            {
                return _errorCode;
            }
        }

      
        #endregion
    }
}
