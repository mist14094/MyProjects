using System;
using System.Collections.Generic;
using System.Text;

namespace KTone.Core.KTIRFID
{
    /// <summary>
    /// Interface for Convergence reader (CS461)
    /// </summary>
    public interface IKTCSReader
    {
        bool EnableHealthCheck
        {
            get;
            set;
        }
        int HealthCheckIntervalSec
        {
            get;
            set;
        }
    }
}
