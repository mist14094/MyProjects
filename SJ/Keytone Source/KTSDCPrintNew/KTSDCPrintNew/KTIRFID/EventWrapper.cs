using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace KTone.Core.KTIRFID
{
    public class EventWrapper<TEventArgs> : MarshalByRefObject, IDisposable where TEventArgs : EventArgs
    {
        #region [ Attributes ]
        private string assemblyName = string.Empty;
        
        /// <summary>
        /// Target method for delegate with argument of type TEventArgs
        /// </summary>
        private EventHandler<TEventArgs> target = null;
        private static Dictionary<EventHandler<TEventArgs>, EventWrapper<TEventArgs>> eventWrapperObjectDict = 
            new Dictionary<EventHandler<TEventArgs>, EventWrapper<TEventArgs>>();
 
        #endregion [ Attributes ]

        #region [ TEventArgs ]
        /// <summary>
        /// Initializes a new instance of the EventWrapper class.
        /// </summary>
        /// <param name="ktComponentTarget">T which will be called when the server fires an event</param>
        public EventWrapper(EventHandler<TEventArgs> target)
        {
            this.target = target;
            Init();
        }
        /// <summary>
        /// Wrapper event handler with a signature similar to that of the 
        /// actual target T.Forwards events received from 
        /// the server to the client
        /// </summary>
        /// <param name="sender">object who generated this event</param>
        /// <param name="args">Data sent by the event</param>
        public void ForwardEvent(object sender, TEventArgs args)
        {
            //call the target function of the client.
            if (target != null)
                target(sender, args);
        }
        /// <summary>
        /// Creates an object of the wrapper class for event handling.
        /// Adds a wrapper event handler which will the actual event handler 
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static EventHandler<TEventArgs> Create(EventHandler<TEventArgs> target)
        {
            EventWrapper<TEventArgs> wrapperObj = new EventWrapper<TEventArgs>(target);
            EventHandler<TEventArgs> newTarget = new EventHandler<TEventArgs>(wrapperObj.ForwardEvent);
            eventWrapperObjectDict[newTarget] = wrapperObj;
            return newTarget;
        }

        public static void Delete(EventHandler<TEventArgs> newTarget)
        {
            try
            {
                if (eventWrapperObjectDict.ContainsKey(newTarget))
                {
                    EventWrapper<TEventArgs> eventWrapper = eventWrapperObjectDict[newTarget];
                    eventWrapper.Dispose();
                    eventWrapperObjectDict.Remove(newTarget);
                }
            }
            catch { }
        }
        #endregion [ TEventArgs ]

        #region [ Public Methods ]
        /// <summary>
        /// Keep the object life for right now, We may have to worry about it on Server 
        /// to generate a resonable Lease in future
        /// </summary>
        /// <returns></returns>
        public override object InitializeLifetimeService()
        {
            return null;
        }
        /// <summary>
        /// Returns a string representation of underlying event handler 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            try
            {

                //This method will return string of following format
                //"Assembly : RFIDAdminConsole , Class : ClsRFReader"
                string targetClassName = string.Empty;
                int index = targetClassName.LastIndexOf('.');
                if (index > 0)
                    targetClassName = targetClassName.Substring(index + 1);
                string desc = "Assembly : " + assemblyName + " , Class : " + targetClassName;

                return desc;
            }
            catch
            {
                return base.ToString();
            }
        }
        #endregion [ Public Methods ]

        #region [ Private Methods ]
        private void Init()
        {
            try
            {
                Assembly entryAssembly = Assembly.GetEntryAssembly();
                this.assemblyName = entryAssembly.GetName().Name;
            }
            catch
            { }
        }
        #endregion [ Private Methods ]

        #region IDisposable Members

        public void Dispose()
        {
            this.target = null;            
        }

        #endregion
    }
}
