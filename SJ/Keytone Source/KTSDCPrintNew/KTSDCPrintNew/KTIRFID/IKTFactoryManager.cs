using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace KTone.Core.KTIRFID
{
    public interface IKTFactoryManager
    {
        #region Methods
        /// <summary>
        /// Stop the factory
        /// </summary>
        void Stop();

        /// <summary>
        /// Start the factory
        /// </summary>
        void Start();

        /// <summary>
        /// Returns the server status
        /// </summary>
        /// <returns></returns>
        KTServerStatus ServerStatus
        {
            get;
        }
        #endregion Methods

        #region Events
        /// <summary>
        /// Event fired when the server is going to shut down
        /// </summary>
        event EventHandler<KTFactoryManagerEventArgs> ServerShuttingDown;

        /// <summary>
        /// Event fired when the server has shut down
        /// </summary>
        event EventHandler<KTFactoryManagerEventArgs> ServerDown;

        /// <summary>
        /// Event fired when the server is going to start
        /// </summary>
        event EventHandler<KTFactoryManagerEventArgs> ServerComingUp;

        /// <summary>
        /// Event fired when the server is up.
        /// </summary>
        event EventHandler<KTFactoryManagerEventArgs> ServerUp;
        #endregion Events
    }
}
