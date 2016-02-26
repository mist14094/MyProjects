using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KTone.Core.KTIRFID
{
    public interface IKTAssetInventoryAgent
    {

        #region Methods

        /// <summary>
        /// Returns requested no. of error records.
        /// </summary>
        /// <param name="recCount">no. of error records</param>
        /// <returns></returns>
        string[] QueryErrorRecords(int recCount);

        /// <summary>
        /// Returns requested no. of log records.
        /// </summary>
        /// <param name="recCount">no. of log records</param>
        /// <returns></returns>
        string[] QueryLogRecords(int recCount);

        /// <summary>
        /// CustomColumn's value for DBAGENT type.
        /// </summary>
        /// <param name="CustomColValues">List of values for custom col of ACTIVETAG_DATA(</param>
        void SetCustomColumns(List<string> CustomColValues);
        #endregion


        #region Events
        /// <summary>
        /// Event raised when the state of the element changes.
        /// </summary>
        //event StateChangeEventHandler OnStateChanged;

        /// <summary>
        /// Event raised when the data received by the agent and queued for processing. 
        /// </summary>
        event EventHandler<KTAssetInventoryAgentEventArgs> KTAssetInventoryAgentDataReceived;

        /// <summary>
        /// Event raised when the queued data is processed successfully. 
        /// </summary>
        event EventHandler<KTAssetInventoryAgentEventArgs> KTAssetInventoryAgentDataProcessed;

        /// <summary>
        /// Event raised when the queued data is processed successfully. 
        /// </summary>
        event EventHandler<KTAssetInventoryAgentTagSeenEventArgs> TagSeenEvent;

        /// <summary>
        /// Event raised when the queued data is processed successfully. 
        /// </summary>
        event EventHandler<KTAssetInventoryAgentTagMovementEventArgs> TagMovementEvent;

        #endregion

    }

    [Serializable]
    public enum RPGAssetStatus
    {
        NONE,
        IN,
        OUT,
        SEEN,
    }

}



