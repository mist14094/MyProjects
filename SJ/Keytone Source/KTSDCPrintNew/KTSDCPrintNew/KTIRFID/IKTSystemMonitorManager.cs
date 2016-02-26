using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace KTone.Core.KTIRFID
{
    /// <summary>
    /// <exclude />
    /// Defines a set of methods to monitor system state of selected components.It will keep track of the 
    /// idle and active time periods for each of the added component. It will periodically fire a event 
    /// with state information of the components.
    /// </summary>
    public interface IKTSystemMonitorManager
    {
        #region Methods
        /// <summary>
        /// Starts monitoring system state for a given component
        /// </summary>
        /// <param name="componentId">Id of the component whose system state is to be monitored</param>
        /// <param name="category">Category of the component whose system state is to be monitored</param>
        void AddComponent(string componentId,KTComponentCategory category);

        /// <summary>
        /// Stops moitoring system state for a given component
        /// </summary>
        /// <param name="componentId">Id of the component for which system state monitoring should be stopped</param>
        void RemoveComponent(string componentId);

        /// <summary>
        /// Returns the state of all the added components
        /// </summary>
        /// <returns></returns>
        SystemStateInfo[] GetComponentState();

        /// <summary>
        /// Returns the state of a given component
        /// </summary>
        /// <param name="componentId">Id of the component</param>
        /// <returns></returns>
        SystemStateInfo GetComponentState(string componentId);

        /// <summary>
        /// Returns the state change history of a given component
        /// </summary>
        /// <param name="componentId">Id of the component</param>
        /// <returns></returns>
        List<SystemStateInfo> GetComponentStateChangeHistory(string componentId);

        /// <summary>
        /// Returns a list of ids of all the added components
        /// </summary>
        /// <returns></returns>
        string[] GetRegisteredComponents();

        /// <summary>
        /// Returns true if a component id is already added
        /// </summary>
        /// <returns></returns>
        bool IsComponentRegistered(string componentId);
        #endregion Methods
        
        #region Event
        /// <summary>
        /// This event will be fired after a time interval set through TimeIntervalMS.
        /// It will contain details about the syatem state of all the added components.
        /// </summary>
        event EventHandler<SystemMonitorManagerEventArgs> SystemMonitorManagerEvent;

        #endregion Event

        #region Properties
        /// <summary>
        /// Interval used by the manager to check the system state of the components.
        /// After this time interval, SystemMonitorManagerEvent will be fired by the manager.
        /// </summary>
        int TimeIntervalMS
        {
            get;
            set;
        }
        #endregion Properties
    }

    /// <summary>
    /// <exclude />
    /// Defines a property and an event to monitor system state of a given component.
    /// It will periodically check the state of activity at the component level and fire a event.
    /// </summary>
    public interface IKTSystemMonitor
    {
        #region Properties
        /// <summary>
        /// Time interval in MS for defining Idle/Active state
        /// </summary>
        int SystemMonitorIntervalMS { get;set;}

        #endregion

        #region Events
        /// <summary>
        /// Event raised periodically with active/idle state information.
        /// </summary>
        event EventHandler<SystemMonitorEventArgs> SystemMonitorEvent;
        #endregion
    }
}
