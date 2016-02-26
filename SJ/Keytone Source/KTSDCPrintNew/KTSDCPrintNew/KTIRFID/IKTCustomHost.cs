using System;
using System.Collections.Generic;
using System.Text;

namespace KTone.Core.KTIRFID
{
    /// <summary>
    /// Defines interface for custom host component
    /// </summary>
    public interface IKTCustomHost
    {
        /// <summary>
        /// Monitoring event with message from the custom hosted component.
        /// </summary>
        event EventHandler<ComponentMonitorEventArgs> MonitorEvent;

        /// <summary>
        /// Returns custom component if it is derived from MarshalByRefObject, else returns null
        /// </summary>
        IKTCustomComponent CustomComponent
        { get; }
    }
}
