using System;
using System.Collections.Generic;
using System.Text;

namespace KTone.Core.KTIRFID
{
    public interface IKTRFIDFilter
    {
        #region Methods
        /// <summary>
        /// Starts collecting the tags from the reader.If one of the readers is offline, this method will continue
        /// with other readers.It will return false, if it fails to start auto mode on reader.
        /// </summary>
        bool StartApplicationCycle(out string errorMsg);

        /// <summary>
        /// Gets filter category
        /// </summary>
        FilterCategory FilterCategory
        {
            get;
        }
        /// <summary>
        /// Stops collecting the tags from the reader and fire Aggregate event.
        /// </summary>
        bool StopApplicationCycle(out string errorMsg);
        #endregion Methods

        #region Events
        /// <summary>
        /// Event raised when the filter gets the tags from a reader/another filter
        /// </summary>
        event EventHandler<KTRFIDFilterEventArgs> PassThroughEvent;

        /// <summary>
        /// Event raised at the end of the application cycle.
        /// </summary>
        event EventHandler<KTRFIDFilterEventArgs> AggregateEvent;

        #endregion Events

        #region properties
        string[] ReaderIds
        {
            get;
        }

        #endregion properties
    }

    /// <summary>
    /// Category of the filter
    /// </summary>
    public enum FilterCategory
    {
        /// <summary>
        /// Application cycle of the filer is timer based
        /// </summary>
        TIMER,

        /// <summary>
        /// Application cycle of the filer is controlled by the dio.
        /// </summary>
        DIO,

        /// <summary>
        /// Application cycle of the filer is controlled manually.
        /// </summary>
        MANUAL,

        /// <summary>
        /// Application cycle of the filer is controlled by added events from the reader and a maximum timeout.
        /// </summary>
        ADAPTIVE
    }

   
}
