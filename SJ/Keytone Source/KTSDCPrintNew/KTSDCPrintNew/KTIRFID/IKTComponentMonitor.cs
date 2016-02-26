using System;
using System.Collections.Generic;
using System.Text;

namespace KTone.Core.KTIRFID
{
    /// <summary>
    /// Interface for KTComponentMonitor component.
    /// </summary>
    public interface IKTComponentMonitor
    {
        #region Event
        /// <summary>
        /// Event raised when the state of the element changes.
        /// </summary>
        event EventHandler<ComponentMonitorEventArgs> ComponentMonitorEvent;
        #endregion Event

        #region Methods
        /// <summary>
        /// Publishes message sent by the monitored component.
        /// </summary>
        /// <param name="monitoredComponentId">Id of the monitored component.</param>
        /// <param name="message">message sent by the monitored component.</param>
        /// <param name="messageLevel">message level</param>
        /// <returns></returns>
        string PublishMonitorMessage(string monitoredComponentId, string message, MessageLevel messageLevel);
        /// <summary>
        /// Gives the All pending Messages to the component Monitor.
        /// </summary>
        /// <returns></returns>
        List<string> GetAllPendingMessages(); 
        #endregion Methods
    }

    public interface IKTRebootReader
    {
        /// <summary>
        ///Reboot the added Readers.
        /// </summary>
        void Reboot();

        /// <summary>
        ///Reconnects the added Readers.
        /// </summary>
        void Reconnect();

        /// <summary>
        ///Add reader in the Reboot Reader List.
        /// </summary>
        /// <param name="readerId">Id of the reader to reboot/reconnect</param>
        /// <param name="reboot">true if you want to reboot the reader</param>
        /// <param name="reconnect">true if you want to reconnect the reader</param>
        void AddReader(string readerId, bool reboot, bool reconnect,int nextActionTimeMin);

        /// <summary>
        ///Remove reader in the Reboot Reader List.
        /// </summary>
        void RemoveReader(string readerId);
        /// <summary>
        /// Set the Time Frame for  Reboot /Reconnect
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        void SetTimeFrame(string startTime, string endTime);
        /// <summary>
        /// Get the Time frame set by the User for Reboot/Reconnect
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        void GetTimeFrame(out string startTime, out string endTime);
        
        Dictionary<string, string> SelectedReaders
        {
            get;
        }

        Dictionary<string, string> AllReaderIds
        { 
            get;
        }
    }
}
