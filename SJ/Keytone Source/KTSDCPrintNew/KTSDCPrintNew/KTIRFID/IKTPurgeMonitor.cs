using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace KTone.Core.KTIRFID
{
    /// <summary>
    /// Defines methods for purging the Violation tables 
    /// </summary>
    public interface IKTViolationPurgeMonitor
    {
        /// <summary>
        /// Fired when there record will purge from Violation table.
        /// </summary>
        event EventHandler<KTViolationPurgeMonitorArgs> ViolationPurgeMonitor;
        #region Methods 
        bool PurgeTimerStart();
        #endregion Methods 
    }
    public interface IKTDBPurgeMonitor
    {
        /// <summary>
        /// Fired when there record will purge from ItemMaster and related table.
        /// </summary>
        event EventHandler<KTDBPurgeMonitorArgs> DBPurgeMonitor;
        #region Methods
        bool PurgeTimerStart();
        #endregion Methods 
    }
}
