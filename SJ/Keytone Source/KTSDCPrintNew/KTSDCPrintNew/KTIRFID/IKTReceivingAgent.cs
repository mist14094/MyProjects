using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace KTone.Core.KTIRFID
{
    public interface IKTReceivingAgent
    {
        #region Events

        /// <summary>
        /// Event raised when tag seen
        /// </summary>
        //event EventHandler<KTTagSeenAtLocationAgentTagArgs> SeenTags;

        //event EventHandler<KTLocationAgentTimeOut> TimerTimeOut;

        event EventHandler<KTReceivingAgentItemsUpdated> ItemsUpdated;

        //event EventHandler<KTReceivingAgentExceptionsAndAlertsArgs> ExceptionsAndAlertsSeen;

        #endregion Events

        #region Methods
        List<string> GetErrors();
        List<string> GetSuccessLogs();
        List<string> GetAgentStatics();

        #endregion Methods

        #region Attributes
        /// <summary>
        /// Location to which this location station belongs to.
        /// </summary>
        long LocationID { get; }
        /// <summary>
        /// Location agent Cycle Time out preiod.
        /// </summary>
        //int TimeOut { get; set; }
     //   Dictionary<string, bool> GetAlertSettings { get; }
     //   Dictionary<string, bool> SetAlertSettings { set; }
        int GetRefreshLocationTime { get; }
        int SetRefreshLocationTime { set; }
        //int ItemTimeOut { set; }

        #endregion Attributes
    }
}

