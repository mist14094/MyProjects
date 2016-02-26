using System;
using System.Collections.Generic;
using System.Text;

namespace KTone.Core.KTIRFID
{
    /// <summary>
    /// <exclude />
    /// Includes a set of methods to monitor the Wavetrend tag reads.
    /// </summary>
    public interface IWTTagMonitor
    {
        /// <summary>
        /// Event is fired when new tags are added as compared to the tags in previous cycle
        /// </summary>
        event EventHandler<WTTagEventArgs> TagsAdded;
        event EventHandler<WTTagEventArgs> TagsRemoved;
        event EventHandler<WTTagEventArgs> TagsCurrent;
    }
}
