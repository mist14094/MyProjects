using System;
using System.Collections.Generic;
using System.Text;

namespace KTone.Core.KTIRFID
{
    public delegate void CustomComponentMonitorDelegate(string message,MessageLevel level);

    /// <summary>
    /// Interface for the custom components to be hosted by KTRFFramework
    /// </summary>
    public interface IKTCustomComponent
    {
        /// <summary>
        /// Perform all the initialization of the component and start threads/timers in this method.
        /// </summary>
        /// <param name="componentFactory">Reference to factory object which can be used to get other components</param>
        /// <param name="startupParams">Dictionary of initialization parameters</param>
        /// <param name="monitorDelegate">Delegate which can be called to fire MonitorEvent from host component 
        /// that implements IKTCustomHost </param>
        /// <returns></returns>
        bool Startup(IKTComponentFactory componentFactory, Dictionary<string, object> startupParams,
            CustomComponentMonitorDelegate monitorDelegate);

        /// <summary>
        /// Stop any thread/timer started through startup.
        /// </summary>
        void Shutdown();
    }


    /// <summary>
    /// Enlists datatype of component Attribute.
    /// </summary>

    public enum ComponentAttributeDataType
    {
        Int,
        String,
        Long,
        Byte,
        Bool,
        Float,
        Double
    }
    /// <summary>
    /// Class used to store the metadata of a custom component attribute.
    /// </summary>
    public class CustomComponentAttributeMetadata
    {
        /// <summary>
        /// Name of the attribute
        /// </summary>
        string name;
        /// <summary>
        /// Data type of the attribute. Valid data types are int, long, float, double, string
        /// </summary>
        ComponentAttributeDataType dataType; 
        /// <summary>
        /// Default value of the attribute
        /// </summary>
        string defaultValue;
        /// <summary>
        /// Attribute is of type IKTComponent or not
        /// </summary>
        bool isComponentId;
        /// <summary>
        /// Category of the Attribute
        /// </summary>
        KTComponentCategory componentCategory;
        /// <summary>
        /// Attribute is File Path or not
        /// </summary>
        bool isFilePath;
        /// <summary>
        /// Description of the Attribute
        /// </summary>
        string description;
        /// <summary>
        /// range of the Attribute
        /// </summary>
        List<string> range;
        /// <summary>
        /// filter for File to browse
        /// </summary>
        string fileFilter;
        /// <summary>
        /// Returns name of the Attribute
        /// </summary>        
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Returns data type of the attribute
        /// </summary>
        public ComponentAttributeDataType DataType
        {
            get
            {
                return this.dataType;
            }
        }

        /// <summary>
        /// Returns default value of the attribute
        /// </summary>
        public string DefaultValue
        {
            get 
            {
                return this.defaultValue;
            }
        }

        /// <summary>
        /// return whether Attribute is of type IKTComponent or not
        /// </summary>
        public bool IsComponentId
        {
            get
            {
                return this.isComponentId;
            }
        }

        /// <summary>
        /// returns Category of component
        /// </summary>
        public KTComponentCategory ComponentCategory
        {
            get
            {
                return this.componentCategory;
            }
        }

        /// <summary>
        /// Returns whether Attribute is File Path or not
        /// </summary>
        public bool IsFilePath
        {
            get
            {
                return this.isFilePath;
            }

        }

        /// <summary>
        /// Returns Description of the Attribute
        /// </summary>
        public string Description
        {
            get
            {
                return this.description;
            }
        }

        /// <summary>
        /// Returns Range for attribute.
        /// </summary>
        public List<string> Range
        {
            get
            {
                return this.range;
            }
        }

        /// <summary>
        /// returns filter for File to browse
        /// </summary>
        public string FileFilter
        {
            get
            {
                return this.fileFilter;
            }
        }

        public CustomComponentAttributeMetadata(string name, ComponentAttributeDataType dataType, string defaultValue)
        {
            this.name = name;
            this.dataType = dataType;
            this.defaultValue = defaultValue;            
        }

        public CustomComponentAttributeMetadata(string name, ComponentAttributeDataType dataType, string defaultValue,string description)
        {
            this.name = name;
            this.dataType = dataType;
            this.defaultValue = defaultValue;
            this.description = description;
        }

        public CustomComponentAttributeMetadata(string name, ComponentAttributeDataType dataType, string defaultValue, string description, bool isComponentId, KTComponentCategory componentCategory)
        {
            this.name = name;
            this.dataType = dataType;
            this.defaultValue = defaultValue;
            this.description = description;
            this.isComponentId = isComponentId;
            this.componentCategory = componentCategory;
        }

        public CustomComponentAttributeMetadata(string name, ComponentAttributeDataType dataType, string defaultValue, string description,bool isFilePath,string fileFilter)
        {
            this.name = name;
            this.dataType = dataType;
            this.defaultValue = defaultValue;
            this.description = description;
            this.isFilePath = isFilePath;
            this.fileFilter = fileFilter;
        }

        public CustomComponentAttributeMetadata(string name, ComponentAttributeDataType dataType, string defaultValue, string description,List<string> range)
        {
            this.name = name;
            this.dataType = dataType;
            this.defaultValue = defaultValue;
            this.description = description;
            this.range = range;
        }
    }

}
