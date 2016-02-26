using System;
using System.Collections.Generic;
using System.Text;

namespace KTone.Core.KTIRFID
{
    /// <summary>
    /// <exclude />
    /// Includes a set of methods to monitor the tag reads.
    /// </summary>
    public interface IRFIDTagMonitor
    {
        #region Properties
        /// <summary>
        /// Gets the maximum no. of history records stored by a component
        /// </summary>
        int MaxHistoryRecords { get;set; }
        /// <summary>
        /// Gets the tag monitor time interval in seconds. This interval has to be in multiples of 2 or 5.
        /// TagReadEvent will be fired after this time interval has elapsed.The tags will be then stored 
        /// in the history. 
        /// </summary>
        int TagMonitorIntervalSec { get;set; }
        #endregion Properties

        #region Methods
        /// <summary>
        /// Starts the tag monitor.
        /// </summary>
        void StartTagMonitor();

        /// <summary>
        /// Stops the tag monitor.
        /// </summary>
        void StopTagMonitor();

        /// <summary>
        /// User can poll for the current tags stored in the history using this method.
        /// </summary>
        /// <returns>It will return a list of IRFIDTag objects for latest time stamp. 
        /// It will return null array if no tags are available for the given time stamp</returns>
        IRFIDTag[] GetCurrentTags();
        /// <summary>
        /// User can poll for the current tags stored in the history using this method.
        /// </summary>
        /// <param name="timeStamp">timestamp in the format HH:mm:ss</param>
        /// <returns>It will return a list of IRFIDTag objects for given time stamp. 
        /// It will return null array if no tags are available for the given time stamp</returns>
        IRFIDTag[] GetCurrentTags(string timeStamp);

        /// <summary>
        /// User can poll for the current tags stored in the history using this method.
        /// </summary>
        /// <param name="timeStamp">timestamp in the format HH:mm:ss</param>
        /// <param name="antennaNames">antenna names</param>
        /// <returns>It will return a list of IRFIDTag objects for given time stamp. 
        /// It will return null array if no tags are available for the given time stamp</returns>
        IRFIDTag[] GetCurrentTags(string timeStamp, string[] antennaNames );

        /// <summary>
        /// User can poll for the current tags stored in the history using this method.
        /// </summary>
        /// <param name="timeStamp">timestamp in the format HH:mm:ss</param>
        /// <param name="noOfPrevHistoryRecords">This is the number of history records that will be checked to get the tags.</param>
        /// <exception cref="KTone.Core.KTIRFID.InvalidParamException">Throws exception if noOfPrevHistoryRecords 
        /// is greater than MaxHistoryRecords</exception>
        /// <returns>It will return a list of IRFIDTag objects for given time stamp. 
        /// It will also include tags from previous history records(count = noOfPrevHistoryRecords).
        /// It will return null array if no tags are available for the given time stamp</returns>
        IRFIDTag[] GetCurrentTags(string timeStamp, int noOfPrevHistoryRecords);

        /// <summary>
        /// User can poll for the current tags stored in the history using this method.
        /// </summary>
        /// <param name="timeStamp">timestamp in the format HH:mm:ss</param>
        /// <param name="antennaNames">antenna names</param>
        /// <param name="noOfPrevHistoryRecords">This is the number of history records that will be checked to get the tags.</param>
        /// <exception cref="KTone.Core.KTIRFID.InvalidParamException">Throws exception if noOfPrevHistoryRecords 
        /// is greater than MaxHistoryRecords</exception>
        /// <returns>It will return a list of IRFIDTag objects for given time stamp. 
        /// It will also include tags from previous history records(count = noOfPrevHistoryRecords).
        /// It will return null array if no tags are available for the given time stamp</returns>
        IRFIDTag[] GetCurrentTags(string timeStamp, string[] antennaNames, int noOfPrevHistoryRecords);

        /// <summary>
        /// User can poll for the added tags stored in the history using this method.
        /// </summary>
        /// <param name="timeStamp">timestamp in the format HH:mm:ss</param>
        /// <returns>It will return a list of IRFIDTag objects for given time stamp. 
        /// It will return null array if no tags are available for the given time stamp</returns>
        IRFIDTag[] GetAddedTags(string timeStamp);

        /// <summary>
        /// User can poll for the added tags stored in the history using this method.
        /// </summary>
        /// <param name="timeStamp">timestamp in the format HH:mm:ss</param>
        /// <param name="antennaNames">antenna names</param>
        /// <returns>It will return a list of IRFIDTag objects for given time stamp. 
        /// It will return null array if no tags are available for the given time stamp</returns>
        IRFIDTag[] GetAddedTags(string timeStamp, string[] antennaNames);

        /// <summary>
        /// User can poll for the added tags stored in the history using this method.
        /// </summary>
        /// <param name="timeStamp">timestamp in the format HH:mm:ss</param>
        /// <param name="noOfPrevHistoryRecords">This is the number of history records that will be checked to get the tags.</param>
        /// <exception cref="KTone.Core.KTIRFID.InvalidParamException">Throws exception if noOfPrevHistoryRecords 
        /// is greater than MaxHistoryRecords</exception>
        /// <returns>It will return a list of IRFIDTag objects for given time stamp. 
        /// It will also include tags from previous history records(count = noOfPrevHistoryRecords).
        /// It will return null array if no tags are available for the given time stamp</returns>
        IRFIDTag[] GetAddedTags(string timeStamp, int noOfPrevHistoryRecords);

        /// <summary>
        /// User can poll for the added tags stored in the history using this method.
        /// </summary>
        /// <param name="timeStamp">timestamp in the format HH:mm:ss</param>
        /// <param name="antennaNames">antenna names</param>
        /// <param name="noOfPrevHistoryRecords">This is the number of history records that will be checked to get the tags.</param>
        /// <exception cref="KTone.Core.KTIRFID.InvalidParamException">Throws exception if noOfPrevHistoryRecords 
        /// is greater than MaxHistoryRecords</exception>
        /// <returns>It will return a list of IRFIDTag objects for given time stamp. 
        /// It will also include tags from previous history records(count = noOfPrevHistoryRecords).
        /// It will return null array if no tags are available for the given time stamp</returns>
        IRFIDTag[] GetAddedTags(string timeStamp, string[] antennaNames, int noOfPrevHistoryRecords);

        /// <summary>
        /// User can poll for the removed tags stored in the history using this method.
        /// </summary>
        /// <param name="timeStamp">timestamp in the format HH:mm:ss</param>
        /// <returns>It will return a list of IRFIDTag objects for given time stamp. 
        /// It will return null array if no tags are available for the given time stamp</returns>
        IRFIDTag[] GetRemovedTags(string timeStamp);

        /// <summary>
        /// User can poll for the removed tags stored in the history using this method.
        /// </summary>
        /// <param name="timeStamp">timestamp in the format HH:mm:ss</param>
        /// <param name="antennaNames">antenna names</param>
        /// <returns>It will return a list of IRFIDTag objects for given time stamp. 
        /// It will return null array if no tags are available for the given time stamp</returns>
        IRFIDTag[] GetRemovedTags(string timeStamp, string[] antennaNames);
   
        /// <summary>
        /// User can poll for the removed tags stored in the history using this method.
        /// </summary>
        /// <param name="timeStamp">timestamp in the format HH:mm:ss</param>
        /// <param name="noOfPrevHistoryRecords">This is the number of history records that will be checked to get the tags.</param>
        /// <exception cref="KTone.Core.KTIRFID.InvalidParamException">Throws exception if noOfPrevHistoryRecords 
        /// is greater than MaxHistoryRecords</exception>
        /// <returns>It will return a list of IRFIDTag objects for given time stamp. 
        /// It will also include tags from previous history records(count = noOfPrevHistoryRecords).
        /// It will return null array if no tags are available for the given time stamp</returns>
        IRFIDTag[] GetRemovedTags(string timeStamp, int noOfPrevHistoryRecords);

        /// <summary>
        /// User can poll for the removed tags stored in the history using this method.
        /// </summary>
        /// <param name="timeStamp">timestamp in the format HH:mm:ss</param>
        /// <param name="antennaNames">antenna names</param>
        /// <param name="noOfPrevHistoryRecords">This is the number of history records that will be checked to get the tags.</param>
        /// <exception cref="KTone.Core.KTIRFID.InvalidParamException">Throws exception if noOfPrevHistoryRecords 
        /// is greater than MaxHistoryRecords</exception>
        /// <returns>It will return a list of IRFIDTag objects for given time stamp. 
        /// It will also include tags from previous history records(count = noOfPrevHistoryRecords).
        /// It will return null array if no tags are available for the given time stamp</returns>
        IRFIDTag[] GetRemovedTags(string timeStamp, string[] antennaNames, int noOfPrevHistoryRecords);

        #endregion Methods

        #region Events

        /// <summary>
        /// Registers the client event handler for TagReadEvent which is fired after TagMonitorIntervalSec. 
        /// Returns current tags.
        /// </summary>
        /// <param name="handler">event handler method</param>
        void RegisterTagReadEvent(EventHandler<TagReadEventArgs> handler);

        /// <summary>
        /// Registers the client event handler for TagReadEvent which is fired after TagMonitorIntervalSec. 
        /// Returns current tags from selected antenna.
        /// </summary>
        /// <param name="antennaName">Registers for tag reads from selected antenna</param>
        /// <param name="handler">event handler method</param>
        void RegisterTagReadEvent(EventHandler<TagReadEventArgs> handler, string antennaName);


        /// <summary>
        /// Registers the client event handler for TagReadEvent which is fired after TagMonitorIntervalSec. 
        /// Returns current tags from selected antenna.
        /// </summary>
        /// <param name="antennaNames">Registers for tag reads from selected antennae</param>
        /// <param name="handler">event handler method</param>
        void RegisterTagReadEvent(EventHandler<TagReadEventArgs> handler, List<string> antennaNames);

        /// <summary>
        /// Unregisters the client event handler from TagReadEvent.
        /// </summary>
        /// <param name="handler">event handler method</param>
        void UnregisterTagReadEvent(EventHandler<TagReadEventArgs> handler);

        /// <summary>
        /// Unregisters the client event handler from TagReadEvent.
        /// </summary>
        /// <param name="antennaName">Unregisters from tag reads from selected antenna</param>
        /// <param name="handler">event handler method</param>
        void UnregisterTagReadEvent(EventHandler<TagReadEventArgs> handler, string antennaName);

        /// <summary>
        /// Unregisters the client event handler from TagReadEvent.
        /// </summary>
        /// <param name="antennaNames">Unregisters from tag reads from selected antennae</param>
        /// <param name="handler">event handler method</param>
        void UnregisterTagReadEvent(EventHandler<TagReadEventArgs> handler, List<string> antennaNames);

        /// <summary>
        /// Registers the client event handler for TagsAddedEvent which is fired after TagMonitorIntervalSec. 
        /// Returns added tags.
        /// </summary>
        /// <param name="handler">event handler method</param>
        void RegisterTagsAddedEvent(EventHandler<TagReadEventArgs> handler);

        /// <summary>
        /// Registers the client event handler for TagsAddedEvent which is fired after TagMonitorIntervalSec. 
        /// Returns added tags.
        /// </summary>
        /// <param name="antennaName">Registers for tag reads from selected antenna</param>
        /// <param name="handler">event handler method</param>
        void RegisterTagsAddedEvent(EventHandler<TagReadEventArgs> handler, string antennaName);

        /// <summary>
        /// Registers the client event handler for TagsAddedEvent which is fired after TagMonitorIntervalSec. 
        /// Returns added tags.
        /// </summary>
        /// <param name="antennaNames">Registers for tag reads from selected antennae</param>
        /// <param name="handler">event handler method</param>
        void RegisterTagsAddedEvent(EventHandler<TagReadEventArgs> handler, List<string> antennaNames);

        /// <summary>
        /// Unregisters the client event handler from TagsAddedEvent.
        /// </summary>
        /// <param name="handler">event handler method</param>
        void UnregisterTagsAddedEvent(EventHandler<TagReadEventArgs> handler);

        /// <summary>
        /// Unregisters the client event handler from TagsAddedEvent.
        /// </summary>
        /// <param name="antennaName">Unregisters from tag reads from selected antenna</param>
        /// <param name="handler">event handler method</param>
        void UnregisterTagsAddedEvent(EventHandler<TagReadEventArgs> handler, string antennaName);

        /// <summary>
        /// Unregisters the client event handler from TagsAddedEvent.
        /// </summary>
        /// <param name="antennaNames">Unregisters from tag reads from selected antennae</param>
        /// <param name="handler">event handler method</param>
        void UnregisterTagsAddedEvent(EventHandler<TagReadEventArgs> handler, List<string> antennaNames);

        /// <summary>
        /// Registers the client event handler for TagsRemovedEvent which is fired after TagMonitorIntervalSec. 
        /// Returns removed tags.
        /// </summary>
        /// <param name="handler">event handler method</param>
        void RegisterTagsRemovedEvent(EventHandler<TagReadEventArgs> handler);

        /// <summary>
        /// Registers the client event handler for TagsRemovedEvent which is fired after TagMonitorIntervalSec. 
        /// Returns removed tags.
        /// </summary>
        /// <param name="handler">event handler method</param>
        /// <param name="antennaName">Registers for tag reads from selected antenna</param>
        void RegisterTagsRemovedEvent(EventHandler<TagReadEventArgs> handler, string antennaName);

        /// <summary>
        /// Registers the client event handler for TagsRemovedEvent which is fired after TagMonitorIntervalSec. 
        /// Returns removed tags.
        /// </summary>
        /// <param name="handler">event handler method</param>
        /// <param name="antennaNames">Registers for tag reads from selected antennae</param>
        void RegisterTagsRemovedEvent(EventHandler<TagReadEventArgs> handler, List<string> antennaNames);

        /// <summary>
        /// Unregisters the client event handler from TagsRemovedEvent.
        /// </summary>
        /// <param name="handler">event handler method</param>
        void UnregisterTagsRemovedEvent(EventHandler<TagReadEventArgs> handler);

        /// <summary>
        /// Unregisters the client event handler from TagsRemovedEvent.
        /// </summary>
        /// <param name="antennaName">Unregisters from tag reads from selected antenna</param>
        /// <param name="handler">event handler method</param>
        void UnregisterTagsRemovedEvent(EventHandler<TagReadEventArgs> handler, string antennaName);

        /// <summary>
        /// Unregisters the client event handler from TagsRemovedEvent.
        /// </summary>
        /// <param name="handler">event handler method</param>
        /// <param name="antennaNames">Unregisters from tag reads from selected antennae</param>
        void UnregisterTagsRemovedEvent(EventHandler<TagReadEventArgs> handler, List<string> antennaNames);

        #endregion Events
    }
}
