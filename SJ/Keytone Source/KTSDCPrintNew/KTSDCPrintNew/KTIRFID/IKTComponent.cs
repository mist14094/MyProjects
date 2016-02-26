using System;
using System.Collections.Generic;
using System.Text;

namespace KTone.Core.KTIRFID
{
    /// <summary>
    /// <exclude/>
    /// </summary>
    public interface IKTComponent
    {
        #region Event
        /// <summary>
        /// Event raised when the state of the element changes.
        /// </summary>
        event EventHandler<ComponentStateChangeEventArgs> ComponentStateChanged;
        /// <summary>
        /// Event raised when the communication state of the element changes.
        /// </summary>
        event EventHandler<CommunicationStateChangeEventArgs> CommunicationStateChanged;
        #endregion Event

        #region Methods
        /// <summary>
        /// ValidateComponentConfig
        /// </summary>
        /// <param name="configXml"></param>
        void ValidateComponentConfig(string configXml);

        #endregion Methods

        #region Properties
        /// <summary>
        /// Returns the component id.
        /// </summary>
        string ComponentId { get;}

        /// <summary>
        /// Returns the component name.
        /// </summary>
        string ComponentName { get;}
        /// <summary>
        /// Returns the component Desription
        /// </summary>
        string ComponentDescription { get;}


        /// <summary>
        /// Gets the component's state
        /// </summary>
        KTComponentState ComponentState { get;}

        /// <summary>
        /// Gets the component type
        /// </summary>
        KTComponentCategory ComponentCategory { get;}


        /// <summary>
        /// Get Details of component
        /// </summary>
        string ComponentDetails
        {
            get;
        }


        /// <summary>
        /// Gets the uri of the component.e.g for Reader it will retun Reader\READER00001
        /// </summary>
        string ComponentUri { get;}

        /// <summary>
        /// Returns true if the component is disabled.
        /// </summary>
        bool IsDisabled { get;set;}
        /// <summary>
        /// Returns the communication status of the component if it is connected to hardware.
        /// </summary>
        bool IsOnline { get;}


        int? DataOwnerID { get; }


        #endregion Properties
    }
}
