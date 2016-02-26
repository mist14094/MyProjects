using System;
using System.Collections.Generic;
using System.Text;

namespace KTone.Core.KTIRFID
{
    /// <summary>
    /// Interface for RX201 GPRS reader
    /// </summary>
    public interface IWTReaderRX201GPRS
    {
        /// <summary>
        /// Enable health check for detecting hardware problems.PingReader command will be sent to the reader periodically.
        /// </summary>
        bool EnableHealthCheck { get;set;}

        /// <summary>
        /// If health check is enabled, PingReader command will be sent to the reader periodically.
        /// </summary>
        int HealthCheckIntervalSec { get;set;}

        /// <summary>
        /// Ping reader command
        /// </summary>
        /// <param name="timeOut">timeout for execution of reader</param>
        /// <param name="errorCode">gives status of command execution</param>
        /// <param name="cmdByteArray">raw command bytes</param>
        /// <param name="rspByteArray">raw response bytes</param>
        void PingReader(int timeOut, out WTReaderErrors errorCode, out byte[] cmdByteArray, out byte[] rspByteArray);

        /// <summary>
        /// Reset reader command
        /// </summary>
        /// <param name="timeOut">timeout for execution of reader</param>
        /// <param name="errorCode">gives status of command execution</param>
        /// <param name="cmdByteArray">raw command bytes</param>
        /// <param name="rspByteArray">raw response bytes</param>
        void Reset(int timeOut, out WTReaderErrors errorCode, out byte[] cmdByteArray, out byte[] rspByteArray);
    }
}
