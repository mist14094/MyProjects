using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace KTone.Core.KTIRFID
{
    /// <summary>
    /// <exclude/>
    /// Details of TemplateSegment.
    /// </summary>
    [Serializable]
    public struct TemplateSegment
    {
        private string name;
        private string dataType;
        private int startByteIndex;
        private int length;
        private int startBitIndex;

        /// <summary>
        /// Initializes a new instance of TemplateSegment
        /// </summary>
        /// <param name="name">name of the template segment</param>
        /// <param name="dataType">data type of the template segment</param>
        /// <param name="startByteIndex">the start byte of template segment</param>
        /// <param name="length">length of template segment in bytes</param>
        /// <param name="startBitIndex">the start bit of template segment,if the segment does not start at byte boundary</param>
        public TemplateSegment(string name, string dataType, int startByteIndex, int length,
            int startBitIndex)
        {
            this.name = name;
            this.dataType = dataType;
            this.startByteIndex = startByteIndex;
            this.length = length;
            this.startBitIndex = startBitIndex;
        }

        /// <summary>
        /// Returns name of the template segment.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }


        /// <summary>
        /// Returns data type of the template segment.Data type can be int,string,bool,byte,short or long.
        /// </summary>
        public string DataType
        {
            get
            {
                return dataType;
            }
        }

        /// <summary>
        /// Returns the start byte of template segment.
        /// </summary>
        public int StartByteIndex
        {
            get
            {
                return startByteIndex;
            }
        }

        /// <summary>
        /// Returns length of template segment in bytes
        /// </summary>
        public int Length
        {
            get
            {
                return length;
            }
        }


        /// <summary>
        /// Returns the start bit of template segment,if the segment does not start at byte boundary. 
        /// </summary>
        public int StartBitIndex
        {
            get
            {
                return startBitIndex;
            }
        }
    }


    /// <summary>
    /// <exclude/>
    /// Represents the system state of a component which include idle and active time period details.
    /// </summary>
    [Serializable]
    public struct SystemStateInfo
    {
        #region [ Attributes ]
        string componentId;
        string componentName;
        KTComponentCategory category;
        SystemState systemState;
        bool isOnline;
        DateTime startTime;
        DateTime endTime;
        #endregion [ Attributes ]

        #region [ Constructor ]

        /// <summary>
        /// Gets the Instance of SystemStateInfo for specifed component.
        /// </summary>
        /// <param name="componentID">Unique Id across all category of components</param>
        /// <param name="componentName">Component name</param>
        /// <param name="startTime">Start time</param>
        /// <param name="endTime">End time</param>
        /// <param name="systemState">Current State of System</param>
        /// <param name="isOnLine">Flag that indicates whether the component is Online/ offline</param>
        /// <param name="category">KTComponentCategory of Component</param>
        public SystemStateInfo(string componentID, string componentName, DateTime startTime, DateTime endTime, 
            SystemState systemState, bool isOnline, KTComponentCategory category)
        {
            this.componentId = componentID;
            this.componentName = componentName;
            this.startTime = startTime;
            this.endTime = endTime;
            this.systemState = systemState;
            this.isOnline = isOnline;
            this.category = category;
        }
        #endregion [ Constructor ]

        #region [ Properties ]
        /// <summary>
        /// Gets/Sets State of System.
        /// </summary>     
        public SystemState State
        {
            get
            {
                return systemState;
            }
            set
            {
                systemState = value;
            }

        }
    
        /// <summary>
        /// Gets/Sets the Component ID.
        /// </summary>        
        public string ComponentID
        {
            get
            {
                return componentId;
            }
            set
            {
                componentId = value;
            }
        }


        /// <summary>
        /// Gets/Sets the Component name.
        /// </summary>        
        public string ComponentName
        {
            get
            {
                return componentName;
            }
            set
            {
                componentName = value;
            }
        }

        /// <summary>
        /// Gets/Sets the KTComponentCategory of Component
        /// </summary>
        public KTComponentCategory Category
        {
            get
            {
                return category;
            }
            set
            {
                category = value;
            }
        }

        public DateTime StartTime
        {
            get 
            {
                return this.startTime;
            }
            set
            {
                this.startTime = value;
            }
        }

        public DateTime EndTime
        {
            get
            {
                return this.endTime;
            }
            set 
            {
                this.endTime = value;
            }
        }

        public bool IsOnline
        {
            get
            {
                return this.isOnline;
            }
            set
            {
                this.isOnline = value;
            }
        }

        #endregion [ Properties ]

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(componentId);
            sb.Append(" ");
            sb.Append(componentName);
            sb.Append(" ");
            sb.Append(category.ToString());
            sb.Append(" ");
            sb.Append(isOnline.ToString());
            sb.Append(" ");
            sb.Append(systemState.ToString());
            sb.Append(" ");
            sb.Append(startTime.ToString());
            sb.Append(" ");
            sb.Append(endTime.ToString());
            sb.Append(" ");
            return sb.ToString();
        }
    }

    /// <summary>
    /// Stores the server details
    /// </summary>
    [Serializable]
    public struct ServerInfo
    {
        private string serverId;
        private string serverName;
        private string serverDescription;

        /// <summary>
        /// Initializes an instance of ServerInfo
        /// </summary>
        /// <param name="serverId">Server id</param>
        /// <param name="serverName">Server name</param>
        /// <param name="serverDescription">Server description</param>
        public ServerInfo(string serverId, string serverName, string serverDescription)
        { 
            this.serverId = serverId;
            this.serverName = serverName;
            this.serverDescription = serverDescription;
        }

        #region Properties
        /// <summary>
        /// Returns Server id
        /// </summary>
        public string ServerId
        {
            get 
            {
                return this.serverId;
            }
        }

        /// <summary>
        /// Returns Server name
        /// </summary>
        public string ServerName
        {
            get 
            {
                return this.serverName;
            }
        }

        /// <summary>
        /// Returns Server description
        /// </summary>
        public string ServerDescription
        {
            get 
            {
                return this.serverDescription;
            }
        }

        #endregion Properties
    }

    /// <summary>
    /// Stores the component details
    /// </summary>
    [Serializable]
    public struct ComponentInfo
    {
        private string componentId;
        private string componentName;
        private string componentDescription;
        private string serverId;

        /// <summary>
        /// Initializes an instance of ComponentInfo
        /// </summary>
        /// <param name="componentId">Id of the component</param>
        /// <param name="componentName">Name of the component</param>
        /// <param name="componentDescription">Description of the component</param>
        /// <param name="serverId">Server Id</param>
        public ComponentInfo(string componentId, string componentName, string componentDescription,
            string serverId)
        {
            this.componentId = componentId;
            this.componentName = componentName;
            this.componentDescription = componentDescription;
            this.serverId = serverId;
        }

        #region Properties
        /// <summary>
        /// Returns Id of the component
        /// </summary>
        public string ComponentId
        {
            get
            {
                return this.componentId;
            }
        }

        /// <summary>
        /// Returns Name of the component
        /// </summary>
        public string ComponentName
        {
            get
            {
                return this.componentName;
            }
        }

        /// <summary>
        /// Returns Description of the component
        /// </summary>
        public string ComponentDescription
        {
            get
            {
                return this.componentDescription;
            }
        }

        /// <summary>
        /// Returns Server Id
        /// </summary>
        public string ServerId
        {
            get
            {
                return this.serverId;
            }
        }
        #endregion Properties
  
    }

    [Serializable]
    public struct ImageProcessingCognexToolDetails
    {
        string mCognexToolName;
        bool mCognexToolIsEnabled;
        Dictionary<string, string> mCognexToolParams;


        public ImageProcessingCognexToolDetails(string cognexToolName, bool cognexToolIsEnabled, Dictionary<string, string> cognexToolParams)
        {
            this.mCognexToolName = cognexToolName;
            this.mCognexToolIsEnabled = cognexToolIsEnabled;
            this.mCognexToolParams = cognexToolParams;

        }

        #region Properties

        public string CognexToolName
        {
            get
            {
                return this.mCognexToolName;
            }
        }


        public bool CognexToolIsEnabled
        {
            get
            {
                return this.mCognexToolIsEnabled;
            }
        }


        public Dictionary<string, string> CognexToolParams
        {
            get
            {
                return this.mCognexToolParams;
            }
        }


        #endregion Properties
    }
}