using System;
using System.Collections.Generic;
using System.Text;

namespace KTone.Core.KTIRFID
{
    public interface IKTOrizinReader
    {
        #region GET METHODS
        /// <summary>
        /// Gets the Model of the Reader
        /// </summary>
        /// <returns></returns>
        string GetModel();
        /// <summary>
        /// Get the H/w information
        /// </summary>
        /// <returns></returns>
        string GetHWInfo();
        /// <summary>
        /// Get the Serial No of the Reader
        /// </summary>
        /// <returns></returns>
        string GetSerialNo();
        /// <summary>
        /// Get the Firmware related info
        /// </summary>
        /// <returns></returns>
        string GetFirmwareInfo();
        /// <summary>
        /// Get H/w address from the Reader
        /// </summary>
        /// <returns></returns>
        string GetHWAddress();
        /// <summary>
        /// Get thet Name of the Reader
        /// </summary>
        /// <returns></returns>
        string GetReaderName();
        /// <summary>
        /// Get the Role Of the Reeader assign to it
        /// </summary>
        /// <returns></returns>
        string GetRoleOfReader();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetReaderHandled();
        /// <summary>
        /// Get the Default Trigger to the reader
        /// </summary>
        /// <returns></returns>
        string GetDefaultReadTrigger();
        /// <summary>
        /// Get if the Buzzer is ON /Off
        /// </summary>
        /// <returns></returns>
        int GetBuzzerMode();
        /// <summary>
        /// Get All Antenna assign to the reader
        /// </summary>
        /// <returns></returns>
        string GetAllSources();
        /// <summary>
        /// Get all NotiFication channels from host will get the Tag Reads.
        /// </summary>
        /// <returns></returns>
        string GetAllNotificationChannels();
        /// <summary>
        /// Get the address of the given Notification Channel
        /// </summary>
        /// <param name="notificationChannel"></param>
        /// <returns></returns>
        string GetNotificationAddress(string notificationChannel);
        /// <summary>
        /// Get All Trigger associate with the given notificationChannel
        /// </summary>
        /// <param name="notificationChannel"></param>
        /// <returns></returns>
        string GetAllNotificationTriggers(string notificationChannel);
        /// <summary>
        /// get Value of the given trigger
        /// </summary>
        /// <param name="trigger"></param>
        /// <returns></returns>
        string GetNotificationTriggerValue(string trigger);
        /// <summary>
        /// get the TRigger type of the REader
        /// </summary>
        /// <param name="trigger"></param>
        /// <returns></returns>
        TriggerType GetNotificationTriggerType(string trigger);
        /// <summary>
        /// get all Date Selector
        /// </summary>
        /// <returns></returns>
        string GetAllDataSelector();
        /// <summary>
        /// geives all data supported
        /// </summary>
        /// <returns></returns>
        string[] GetAllSupportedDataName();
        /// <summary>
        /// gives all selected data field
        /// </summary>
        /// <param name="dataSelecter"></param>
        /// <returns></returns>
        string[] GetAllSelectedFieldNames(string dataSelector);

        #endregion GET METHODS

        #region SET COMMANDS
        /// <summary>
        /// Enable The Readers predefine Trigger which starts giving Tag Reads.
        /// </summary>
        void EnableReaderTrigger();
        /// <summary>
        /// Set notify Address to the Reader
        /// </summary>
        /// <param name="notifyChannel"></param>
        void SetNotifyAddress();
        /// <summary>
        /// Disable The Readers predefine Trigger which stops giving Tag Reads.
        /// </summary>
        void DisableReaderTrigger();
        /// <summary>
        /// Set the Trigger Type to the Reader
        ///   1) Continuous
        ///   2) Timer
        /// </summary>
        /// <param name="triggerType"></param>
        /// <param name="triggerValue"></param>
        void SetNotificationTriggerType(TriggerType triggerType,string triggerValue);
        /// <summary>
        /// Remove all the selected field from the Reader.
        /// </summary>
        void RemoveAllDataFieldNames();

        #endregion SET COMMANDS
    }
}
