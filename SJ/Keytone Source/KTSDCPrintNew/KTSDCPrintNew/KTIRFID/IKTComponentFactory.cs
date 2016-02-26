using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;

namespace KTone.Core.KTIRFID
{
    /// <summary>
    /// <exclude />
    /// </summary>
    public interface IKTComponentFactory
    {
        event EventHandler<KTFactoryActionEventArgs> OnComponentAction;

        /// <summary>
        /// Returns the version of component factory
        /// </summary>
        /// <returns></returns>
        string GetVersion();

        /// <summary>
        /// Returns the component for given id
        /// </summary>
        /// <param name="componentId">Component Id</param>
        /// <returns></returns>
        IKTComponent GetComponent(string componentId);

        /// <summary>
        /// Returns the component for given id and category
        /// </summary>
        /// <param name="componentCategory">Component Category</param>
        /// <param name="componentId">Component Id</param>
        /// <returns></returns>
        IKTComponent GetComponent(KTComponentCategory componentCategory, string componentId);

        /// <summary>
        /// Returns all the components
        /// </summary>
        /// <returns></returns>
        IKTComponent[] GetAllComponents(int dataOwnerId);

        /// <summary>
        /// Returns all the components for given category 
        /// </summary>
        /// <param name="componentCategory"></param>
        /// <returns></returns>
        IKTComponent[] GetAllComponents(KTComponentCategory componentCategory, int dataOwnerId);

        /// <summary>
        /// Returns all component ids 
        /// </summary>
        /// <returns></returns>
        string[] GetAllComponentIds(int dataOwnerId);

        /// <summary>
        /// Returns all component ids for given category 
        /// </summary>
        /// <param name="componentCategory"></param>
        /// <returns></returns>
        string[] GetAllComponentIds(KTComponentCategory componentCategory, int dataOwnerId);

        /// <summary>
        /// Returns all component ids for given categories
        /// </summary>
        /// <param name="componentCategories">List of component categories</param>
        /// <returns></returns>
        string[] GetAllComponentIds(List<KTComponentCategory> componentCategories, int dataOwnerId);

        /// <summary>
        /// Returns all the components
        /// </summary>
        /// <returns></returns>
        IKTComponent[] GetAllComponents();

        /// <summary>
        /// Returns all the components for given category 
        /// </summary>
        /// <param name="componentCategory"></param>
        /// <returns></returns>
        IKTComponent[] GetAllComponents(KTComponentCategory componentCategory);

        /// <summary>
        /// Returns all component ids 
        /// </summary>
        /// <returns></returns>
        string[] GetAllComponentIds();

        /// <summary>
        /// Returns all component ids for given category 
        /// </summary>
        /// <param name="componentCategory"></param>
        /// <returns></returns>
        string[] GetAllComponentIds(KTComponentCategory componentCategory);

        /// <summary>
        /// Returns all component ids for given categories
        /// </summary>
        /// <param name="componentCategories">List of component categories</param>
        /// <returns></returns>
        string[] GetAllComponentIds(List<KTComponentCategory> componentCategories);




        /// <summary>
        /// Returns a map containing component id and component name pairs.
        /// </summary>
        /// <param name="componentId"></param>
        /// <returns></returns>
        Dictionary<string, string> GetComponentNamesForIds(string[] componentIds);

        /// <summary>
        /// Returns a reader ids for given IPAddresses
        /// </summary>
        /// <param name="ipAddressList">List of IP adress strings</param>
        /// <returns>a dictionary with IP address string as key and Reader Id as value</returns>
        Dictionary<string, string> GetComponentIdsForIPAddresses(List<string> ipAddressList);

        /// <summary>
        /// Returns a map containing component id and component state pairs.
        /// </summary>
        /// <returns></returns>
        Dictionary<string, KTComponentState> GetComponentStateOfAllComponents();

        /// <summary>
        /// Returns a map containing pairs of component id and a boolean value indicating the communication state.
        /// </summary>
        /// <returns></returns>
        Dictionary<string, bool> GetCommunicationStateOfAllComponents();

        /// <summary>
        /// Adds a component/reader in factory and database.
        /// </summary>
        /// <param name="componentCategory"></param>
        /// <param name="masterId"></param>
        /// <param name="componentName"></param>
        /// <param name="componentDescription"></param>
        /// <param name="componentConfig"></param>
        /// <param name="createOffline"></param>
        /// <param name="attributeDictionary">Dictionary containing name-value pairs of attributes</param>
        /// <returns></returns>
        IKTComponent AddComponent(KTComponentCategory componentCategory, int masterId, string componentName, 
            string componentDescription, string componentConfig, bool createOffline,
            Dictionary<string, string> attributeDictionary, int? dataOwnerId, int? userId);

        /// <summary>
        /// This method will add a component and it will try to preserve the component id.
        /// </summary>
        /// <param name="componentCategory"></param>
        /// <param name="masterId"></param>
        /// <param name="componentName"></param>
        /// <param name="componentDescription"></param>
        /// <param name="componentConfig"></param>
        /// <param name="createOffline"></param>
        /// <param name="componentId"></param>
        /// <param name="attributeDictionary"></param>
        /// <returns></returns>
        IKTComponent AddComponent(KTComponentCategory componentCategory, int masterId, string componentName,
            string componentDescription, string componentConfig, bool createOffline,string componentId,
            Dictionary<string, string> attributeDictionary, int? dataOwnerId, int? userId);

        bool AddComponent(string componentDetailsXml, bool createOffline, out string componentId, out string errorMsg, int? dataOwnerId, int? userId);
      /*  /// <summary>
        /// add the Template which can then be configured to the Reader
        /// </summary>
        /// <param name="TemplateName"></param>
        /// <param name="maxNoOfBytes"></param>
        /// <param name="byteEndian"></param>
        /// <param name="TemplateBody"></param>
        /// <param name="description"></param>
        void AddTemplate(string templateName, int maxNoOfBytes, string byteEndian,
            string templateBody, string description);
        /// <summary>
        /// Remove the Reader
        /// </summary>
        /// <param name="TemplateName"></param>
        /// <param name="statusResult"></param>
        /// <returns></returns>
        bool RemoveTemplate(string templateName, out string statusResult);*/

        void RemoveComponent(KTComponentCategory componentCategory, string componentId);

        void RemoveComponent(string componentId);

        void ConnectComponent(string componentId);

        void DisconnectComponent(string componentId);

        void EnableComponent(string componentId);

        void DisableComponent(string componentId);

        void MarkComponentAsDeleted(string componentId);

        string GetComponentIdForName(string componentName);

        /// <summary>
        /// Register state change event for all components
        /// </summary>
        /// <param name="handler"></param>
        void RegisterForAllCommunicationStateChangedEvent(EventHandler<CommunicationStateChangeEventArgs> handler);

        void RegisterForCommunicationStateChangedEvent(string componentId, EventHandler<CommunicationStateChangeEventArgs> handler);

        /// <summary>
        /// Unregister state change event for all components
        /// </summary>
        /// <param name="handler"></param>
        void UnregisterForAllCommunicationStateChangedEvent(EventHandler<CommunicationStateChangeEventArgs> handler);

        void UnregisterForCommunicationStateChangedEvent(string componentId, EventHandler<CommunicationStateChangeEventArgs> handler);

        /// <summary>
        /// Register state change event for all components
        /// </summary>
        /// <param name="handler"></param>
        void RegisterForAllComponentStateChangedEvent(EventHandler<ComponentStateChangeEventArgs> handler);

        void RegisterForComponentStateChangedEvent(string componentId, EventHandler<ComponentStateChangeEventArgs> handler);

        /// <summary>
        /// Unregister state change event for all components
        /// </summary>
        /// <param name="handler"></param>
        void UnregisterForAllComponentStateChangedEvent(EventHandler<ComponentStateChangeEventArgs> handler);

        void UnregisterForComponentStateChangedEvent(string componentId, EventHandler<ComponentStateChangeEventArgs> handler);

        #region Register/Unregister KTElementStateChangedEvent
        void RegisterForAllKTElementStateChangedEvent(EventHandler<KTElementStateChangeEventArgs> handler);

        void RegisterForKTElementStateChangedEvent(string componentId, EventHandler<KTElementStateChangeEventArgs> handler);

        void UnregisterForAllKTElementStateChangedEvent(EventHandler<KTElementStateChangeEventArgs> handler);

        void UnregisterForKTElementStateChangedEvent(string componentId, EventHandler<KTElementStateChangeEventArgs> handler);
        
        #endregion Register/Unregister KTElementStateChangedEvent

        void RegisterCustomComponent(KTComponentCategory category, MemoryStream memStream, string fileName, string mainAssemblyFileName);

        void RegisterCustomUIComponent(MemoryStream memStream, string fileName);

        MemoryStream GetCustomComponent(string fileName);

        MemoryStream GetCustomUIComponent(string fileName);

        void UnregisterCustomComponent(KTComponentCategory category, string fileName);

        void UnregisterCustomUIComponent(string fileName);

        /// <summary>
        /// Returns default component object for the given category.
        /// If default object is not present, null will be returned. 
        /// </summary>
        /// <param name="componentCategory"></param>
        /// <returns></returns>
        IKTComponent GetDefaultComponent(KTComponentCategory componentCategory);

        /// <summary>
        /// Returns default component id for the given category.
        /// If default object is not present, empty string will be returned. 
        /// </summary>
        /// <param name="componentCategory"></param>
        /// <returns></returns>
        string GetDefaultComponentId(KTComponentCategory componentCategory);
        
        /// <summary>
        /// Apply License 
        /// </summary>
        /// <param name="licenseInfo"></param>
        /// <returns></returns>
        string SaveLicense(string licenseInfo);

        /// <summary>
        /// Get License details
        /// </summary>
        /// <returns></returns>
        KTLicenseData GetLicenseData();

        /// <summary>
        /// Gets the value corresponding to the key from the Sequence table and 
        /// also increments the value in the database.
        /// </summary>
        /// <param name="key">It can be Reader,ComponentInstance,ReaderInstanceAttribute,
        /// ComponentInstanceAttributes</param>
        /// <returns>Integer value for the id</returns>
        int GetNextValSequence(string key);

        /// <summary>
        /// Copies label file on to the server side.
        /// </summary>
        /// <param name="fileName">ame of the label file</param>
        /// <param name="printerType">BarTender otr PrintEasy</param>
        /// <param name="labelStream">MemoryStream containing label file</param>
        void CopyPrinterLabelFile(string fileName, string printerType, MemoryStream labelStream);

        /// <summary>
        /// Validates given configuration string for given component master. 
        /// Throws exception if the string is not valid.
        /// </summary>
        /// <param name="componentMasterId"></param>
        /// <param name="configXml"></param>
        void ValidateComponentConfig(int componentMasterId, string configXml);

        /// <summary>
        /// Validates given configuration string for given master reader. 
        /// Throws exception if the string is not valid.
        /// </summary>
        /// <param name="masterReaderId"></param>
        /// <param name="configXml"></param>
        void ValidateReaderConfig(int masterReaderId, string configXml);

        void SaveFile(MemoryStream stream, bool isPathRelative, string filePath, bool overWrite);

        /// <summary>
        /// Returns main NLOG configuration in xml format
        /// </summary>
        /// <returns></returns>
        string GetNLogSettings();

        /// <summary>
        /// Updates main NLOG configuration
        /// </summary>
        /// <param name="nLogXml">modified NLOG configuration in xml format</param>
        void UpdateNLogSettings(string nLogXml);

        /// <summary>
        /// Returns NLOG configuration for transaction log in xml format
        /// </summary>
        /// <returns></returns>
        string GetTransactionNLogSettings();

        /// <summary>
        /// Updates NLOG configuration for transaction log
        /// </summary>
        /// <param name="nLogXml">modified NLOG configuration in xml format for transaction log</param>
        void UpdateTransactionNLogSettings(string nLogXml);

        #region Reader Group
        /// <summary>
        /// Adds given group name in system.
        /// </summary>
        /// <param name="groupName">name of group to add</param>
        int AddReaderGroup(string groupName, int? dataOwnerId, int? userId);

        /// <summary>
        /// Removes given group name from system.
        /// </summary>
        /// <param name="groupName">name of group to remove</param>
        void RemoveReaderGroup(string groupName);

        /// <summary>
        /// Changes current group name to new group name from system.
        /// </summary>
        /// <param name="groupName">group name to modify</param>
        /// <param name="newGroupName"></param>
        void ModifyReaderGroup(string groupName, string newGroupName);
        /// <summary>
        /// Get all available reader groups
        /// </summary>
        /// <returns>A dictionary containing groupId-Group name pairs</returns>
        Dictionary<int, string> GetAllReaderGroups(int? dataOwnerId);
        
        /// <summary>
        /// Sets the reader group of a single reader
        /// </summary>
        /// <param name="readerId"></param>
        /// <param name="groupId"></param>
        void SetReaderGroup(string readerId, int groupId);

        /// <summary>
        /// Sets the reader group of multiple readers
        /// </summary>
        /// <param name="readerId"></param>
        /// <param name="groupId"></param>
        void SetReaderGroup(List<string> readerIds, int groupId);

        /// <summary>
        /// Returns a list of ReaderGroups which are not associated with any reader.
        /// </summary>
        /// <returns></returns>
        Dictionary<int, string> GetAllUnassignedReaderGroups();

        /// <summary>
        /// Removes the ReaderGroups which are not associated with any reader.
        /// </summary>
        void RemoveAllUnassignedReaderGroups();
        #endregion Reader Group

        /// <summary>
        /// Database connection string
        /// </summary>
        string DbConnectionString
        {
            get;
        }

        /// <summary>
        /// Product Database connection string
        /// </summary>
        string ProductDbConnectionString
        {
            get;
        }
        
        /// <summary>
        /// Base path
        /// </summary>
        string BasePath
        {
            get;
        }

        /// <summary>
        /// Returns a list of COM ports available on the server. 
        /// </summary>
        string[] COMPorts
        {
            get;
        }

        /// <summary>
        /// Returns details of all the components in the factory.
        /// </summary>
        ComponentInfo[] ComponentInfo
        {
            get;
        }

        /// <summary>
        /// Returns details of the server.
        /// </summary>
        ServerInfo ServerInfo
        {
            get;
        }

        string ProductBranch
        {
            get;
            set;
        }

        /// <summary>
        /// Returns Component creation order
        /// </summary>
        List<KTComponentCategory> ComponentCreationOrder
        {
            get;
        }

        DateTime ServerStartedTimeStamp
        { get; }
    }
}
