using System;
using System.Collections.Generic;
using System.Text;

namespace KTone.Core.KTIRFID
{
    public interface IWTGPRSListener
    {
        /// <summary>
        ///  Enable health check for detecting hardware problems.PingReader command will be sent to the readers periodically.
        /// </summary>
        bool EnableHealthCheck { get;set;}

        /// <summary>
        /// If health check is enabled, PingReader command will be sent to the readers periodically.
        /// </summary>
        int HealthCheckIntervalSec { get;set;}
    }
}
