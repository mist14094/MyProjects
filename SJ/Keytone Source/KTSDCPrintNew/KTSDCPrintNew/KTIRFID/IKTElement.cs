using System;
using System.Collections.Generic;
using System.Text;

namespace KTone.Core.KTIRFID
{
    /// <summary>
    /// <exclude />
    /// </summary>
    public interface IKTElement
    {
        #region Methods
        /// <summary>
        /// Creates underlying elements
        /// </summary>
        void Create();
        /// <summary>
        /// Initializes the element
        /// </summary>
        void Init();
        /// <summary>
        /// Publishes the element
        /// </summary>
        void Publish();
        /// <summary>
        /// Discovers other elements
        /// </summary>
        void Discover();
        /// <summary>
        /// Connects with hardware  
        /// </summary>
        void Connect();
        /// <summary>
        /// Starts the element
        /// </summary>
        void Start();
        /// <summary>
        /// Stops the element
        /// </summary>
        void Stop();
        /// <summary>
        /// Disconnects from hardware 
        /// </summary>
        void Disconnect();
        /// <summary>
        /// Saves the configuration data
        /// </summary>
        void Save();
        /// <summary>
        /// Disposes the element
        /// </summary>
        void Close();
        #endregion

        #region Properties
        /// <summary>
        /// Gets the element's state
        /// </summary>
        KTElementState KTElementState { get;}
        #endregion

        #region Events
        /// <summary>
        /// Event raised when the state of the element changes.
        /// </summary>
        event EventHandler<KTElementStateChangeEventArgs> KTElementStateChanged;

        #endregion
    }

    /// <summary>
    /// <exclude />
    /// Provides data for the KTElementStateChanged event of IKTElement
    /// </summary>
    [Serializable]
    public class KTElementStateChangeEventArgs : KTComponentEventArgs
    {
        #region Attributes

        private KTElementState elementState;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance KTElementStateChangeEventArgs class.
        /// </summary>
        /// <param name="eventId">event Id</param>
        /// <param name="elementState">element state. </param>
        public KTElementStateChangeEventArgs(IKTComponent component, string eventId,
            KTElementState elementState)
            : base(component, eventId)
        {
            this.elementState = elementState;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns element state (<see cref="KTone.Core.KTIRFID.KTElementState"/>)
        /// </summary>
        public KTElementState KTElementState
        {
            get
            {
                return this.elementState;
            }
        }


        #endregion
    }
}
