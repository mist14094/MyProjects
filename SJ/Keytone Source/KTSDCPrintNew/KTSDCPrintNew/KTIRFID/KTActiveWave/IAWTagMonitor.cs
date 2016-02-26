using System;
using System.Collections.Generic;
using System.Text;

namespace KTone.Core.KTIRFID
{

    /// <summary>
    /// <exclude />
    /// Includes a set of methods to monitor the ActiveWave tag reads.
    /// </summary>
    public interface IAWTagMonitor
    {
        #region Events
        /// <summary>
        /// Event fired when new tags are added as compared to the tags in previous cycle
        /// </summary>
        event EventHandler<AWTagEventArgs> TagsAdded;
        event EventHandler<AWTagEventArgs> TagsRemoved;
        event EventHandler<AWTagEventArgs> TagsCurrent;
        event EventHandler<AWTagEventArgs> TagRead;
        /// <summary>
        /// Event fired when tag is tampered
        /// </summary>
        event EventHandler<TagTamperedEventArg> TagTampered;
        /// <summary>
        /// This event will be fired when a tag which is already in the field of the reader, moves 
        /// and the FGen detects the movement.
        /// </summary>
        event EventHandler<AWTagEventArgs> TagSensed;

        /// <summary>
        /// Event fired when OnDemandInventory is started/complete
        /// </summary>
        event EventHandler<AWInventoryStatusEventArgs> OnDemandInventoryStatusChanged;

        /// <summary>
        /// Event fired when low battery detected for tags
        /// </summary>
        event EventHandler<AWTagEventArgs> TagBatteryLowWarning;
        #endregion Events

        #region Methods

        /// <summary>
        /// Returns the status of ActiveWave OnDemandInventory
        /// </summary>
        AWInventoryStatus OnDemandInventoryStatus
        {
            get;
        }

        /// <summary>
        /// Returns the time when the last OnDemandInventory was done.
        /// It will be DateTime.MinValue when the service is restarted/ component is reconnected.
        /// </summary>
        DateTime LastOnDemandInventoryTime
        {
            get;
        }

        /// <summary>
        /// Returns tags that are added in the ondemand inventory
        /// </summary>
        List<ActiveWaveTag> OnDemandInventoryAddedTags
        {
            get;
        }

        /// <summary>
        /// Method to start OnDemand inventory on ActiveWave reader,filter
        /// If inventory is alredy started, it will throw an exception.
        /// </summary>
        /// <returns></returns>
        AWInventoryStatus StartOnDemandInventory(out string errorMsg);
        #endregion Methods
    }
}
