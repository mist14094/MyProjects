namespace ROOT.CIMV2.Win32 {
    using System;
    using System.ComponentModel;
    using System.Management;
    using System.Collections;
    using System.Globalization;
    
    
    // Functions ShouldSerialize<PropertyName> are functions used by VS property browser to check if a particular property has to be serialized. These functions are added for all ValueType properties ( properties of type Int32, BOOL etc.. which cannot be set to null). These functions use Is<PropertyName>Null function. These functions are also used in the TypeConverter implementation for the properties to check for NULL value of property so that an empty value can be shown in Property browser in case of Drag and Drop in Visual studio.
    // Functions Is<PropertyName>Null() are used to check if a property is NULL.
    // Functions Reset<PropertyName> are added for Nullable Read/Write properties. These functions are used by VS designer in property browser to set a property to NULL.
    // Every property added to the class for WMI property has attributes set to define its behavior in Visual Studio designer and also to define a TypeConverter to be used.
    // Datetime conversion functions ToDateTime and ToDmtfDateTime are added to the class to convert DMTF datetime to System.DateTime and vice-versa.
    // An Early Bound class generated for the WMI class.Win32_CacheMemory
    public class CacheMemory : System.ComponentModel.Component {
        
        // Private property to hold the WMI namespace in which the class resides.
        private static string CreatedWmiNamespace = "root\\cimv2";
        
        // Private property to hold the name of WMI class which created this class.
        private static string CreatedClassName = "Win32_CacheMemory";
        
        // Private member variable to hold the ManagementScope which is used by the various methods.
        private static System.Management.ManagementScope statMgmtScope = null;
        
        private ManagementSystemProperties PrivateSystemProperties;
        
        // Underlying lateBound WMI object.
        private System.Management.ManagementObject PrivateLateBoundObject;
        
        // Member variable to store the 'automatic commit' behavior for the class.
        private bool AutoCommitProp;
        
        // Private variable to hold the embedded property representing the instance.
        private System.Management.ManagementBaseObject embeddedObj;
        
        // The current WMI object used
        private System.Management.ManagementBaseObject curObj;
        
        // Flag to indicate if the instance is an embedded object.
        private bool isEmbedded;
        
        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public CacheMemory() {
            this.InitializeObject(null, null, null);
        }
        
        public CacheMemory(string keyDeviceID) {
            this.InitializeObject(null, new System.Management.ManagementPath(CacheMemory.ConstructPath(keyDeviceID)), null);
        }
        
        public CacheMemory(System.Management.ManagementScope mgmtScope, string keyDeviceID) {
            this.InitializeObject(((System.Management.ManagementScope)(mgmtScope)), new System.Management.ManagementPath(CacheMemory.ConstructPath(keyDeviceID)), null);
        }
        
        public CacheMemory(System.Management.ManagementPath path, System.Management.ObjectGetOptions getOptions) {
            this.InitializeObject(null, path, getOptions);
        }
        
        public CacheMemory(System.Management.ManagementScope mgmtScope, System.Management.ManagementPath path) {
            this.InitializeObject(mgmtScope, path, null);
        }
        
        public CacheMemory(System.Management.ManagementPath path) {
            this.InitializeObject(null, path, null);
        }
        
        public CacheMemory(System.Management.ManagementScope mgmtScope, System.Management.ManagementPath path, System.Management.ObjectGetOptions getOptions) {
            this.InitializeObject(mgmtScope, path, getOptions);
        }
        
        public CacheMemory(System.Management.ManagementObject theObject) {
            Initialize();
            if ((CheckIfProperClass(theObject) == true)) {
                PrivateLateBoundObject = theObject;
                PrivateSystemProperties = new ManagementSystemProperties(PrivateLateBoundObject);
                curObj = PrivateLateBoundObject;
            }
            else {
                throw new System.ArgumentException("Class name does not match.");
            }
        }
        
        public CacheMemory(System.Management.ManagementBaseObject theObject) {
            Initialize();
            if ((CheckIfProperClass(theObject) == true)) {
                embeddedObj = theObject;
                PrivateSystemProperties = new ManagementSystemProperties(theObject);
                curObj = embeddedObj;
                isEmbedded = true;
            }
            else {
                throw new System.ArgumentException("Class name does not match.");
            }
        }
        
        // Property returns the namespace of the WMI class.
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string OriginatingNamespace {
            get {
                return "root\\cimv2";
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ManagementClassName {
            get {
                string strRet = CreatedClassName;
                if ((curObj != null)) {
                    if ((curObj.ClassPath != null)) {
                        strRet = ((string)(curObj["__CLASS"]));
                        if (((strRet == null) 
                                    || (strRet == string.Empty))) {
                            strRet = CreatedClassName;
                        }
                    }
                }
                return strRet;
            }
        }
        
        // Property pointing to an embedded object to get System properties of the WMI object.
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ManagementSystemProperties SystemProperties {
            get {
                return PrivateSystemProperties;
            }
        }
        
        // Property returning the underlying lateBound object.
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Management.ManagementBaseObject LateBoundObject {
            get {
                return curObj;
            }
        }
        
        // ManagementScope of the object.
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Management.ManagementScope Scope {
            get {
                if ((isEmbedded == false)) {
                    return PrivateLateBoundObject.Scope;
                }
                else {
                    return null;
                }
            }
            set {
                if ((isEmbedded == false)) {
                    PrivateLateBoundObject.Scope = value;
                }
            }
        }
        
        // Property to show the commit behavior for the WMI object. If true, WMI object will be automatically saved after each property modification.(ie. Put() is called after modification of a property).
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AutoCommit {
            get {
                return AutoCommitProp;
            }
            set {
                AutoCommitProp = value;
            }
        }
        
        // The ManagementPath of the underlying WMI object.
        [Browsable(true)]
        public System.Management.ManagementPath Path {
            get {
                if ((isEmbedded == false)) {
                    return PrivateLateBoundObject.Path;
                }
                else {
                    return null;
                }
            }
            set {
                if ((isEmbedded == false)) {
                    if ((CheckIfProperClass(null, value, null) != true)) {
                        throw new System.ArgumentException("Class name does not match.");
                    }
                    PrivateLateBoundObject.Path = value;
                }
            }
        }
        
        // Public static scope property which is used by the various methods.
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static System.Management.ManagementScope StaticScope {
            get {
                return statMgmtScope;
            }
            set {
                statMgmtScope = value;
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsAccessNull {
            get {
                if ((curObj["Access"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Access describes whether the media is readable (value=1), writeable (value=2), or" +
            " both (value=3). \"Unknown\" (0) and \"Write Once\" (4) can also be defined.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public AccessValues Access {
            get {
                if ((curObj["Access"] == null)) {
                    return ((AccessValues)(System.Convert.ToInt32(5)));
                }
                return ((AccessValues)(System.Convert.ToInt32(curObj["Access"])));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"An array of octets holding additional error information. An example is ECC Syndrome or the return of the check bits if a CRC-based error methodology is used. In the latter case, if a single bit error is recognized and the CRC algorithm is known, it is possible to determine the exact bit that failed.  This type of data (ECC Syndrome, Check Bit or Parity Bit data, or other vendor supplied information) is included in this field. If the ErrorInfo property is equal to 3, ""OK"", then the AdditionalErrorData property has no meaning.")]
        public byte[] AdditionalErrorData {
            get {
                return ((byte[])(curObj["AdditionalErrorData"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsAssociativityNull {
            get {
                if ((curObj["Associativity"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("An integer enumeration defining the system cache associativity. For example, 6 in" +
            "dicates a fully associative cache.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public AssociativityValues Associativity {
            get {
                if ((curObj["Associativity"] == null)) {
                    return ((AssociativityValues)(System.Convert.ToInt32(0)));
                }
                return ((AssociativityValues)(System.Convert.ToInt32(curObj["Associativity"])));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsAvailabilityNull {
            get {
                if ((curObj["Availability"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"The availability and status of the device.  For example, the Availability property indicates that the device is running and has full power (value=3), or is in a warning (4), test (5), degraded (10) or power save state (values 13-15 and 17). Regarding the power saving states, these are defined as follows: Value 13 (""Power Save - Unknown"") indicates that the device is known to be in a power save mode, but its exact status in this mode is unknown; 14 (""Power Save - Low Power Mode"") indicates that the device is in a power save state but still functioning, and may exhibit degraded performance; 15 (""Power Save - Standby"") describes that the device is not functioning but could be brought to full power 'quickly'; and value 17 (""Power Save - Warning"") indicates that the device is in a warning state, though also in a power save mode.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public AvailabilityValues Availability {
            get {
                if ((curObj["Availability"] == null)) {
                    return ((AvailabilityValues)(System.Convert.ToInt32(0)));
                }
                return ((AvailabilityValues)(System.Convert.ToInt32(curObj["Availability"])));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsBlockSizeNull {
            get {
                if ((curObj["BlockSize"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Size in bytes of the blocks which form this StorageExtent. If variable block size, then the maximum block size in bytes should be specified. If the block size is unknown or if a block concept is not valid (for example, for Aggregate Extents, Memory or LogicalDisks), enter a 1.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ulong BlockSize {
            get {
                if ((curObj["BlockSize"] == null)) {
                    return System.Convert.ToUInt64(0);
                }
                return ((ulong)(curObj["BlockSize"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsCacheSpeedNull {
            get {
                if ((curObj["CacheSpeed"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The CacheSpeed property specifies the speed of the cache in nanoseconds.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint CacheSpeed {
            get {
                if ((curObj["CacheSpeed"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((uint)(curObj["CacheSpeed"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsCacheTypeNull {
            get {
                if ((curObj["CacheType"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Defines whether this is for instruction caching (value=3), data caching (value=4)" +
            " or both (value=5, \"Unified\"). Also, \"Other\" (1) and \"Unknown\" (2) can be define" +
            "d.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public CacheTypeValues CacheType {
            get {
                if ((curObj["CacheType"] == null)) {
                    return ((CacheTypeValues)(System.Convert.ToInt32(0)));
                }
                return ((CacheTypeValues)(System.Convert.ToInt32(curObj["CacheType"])));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The Caption property is a short textual description (one-line string) of the obje" +
            "ct.")]
        public string Caption {
            get {
                return ((string)(curObj["Caption"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsConfigManagerErrorCodeNull {
            get {
                if ((curObj["ConfigManagerErrorCode"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Indicates the Win32 Configuration Manager error code.  The following values may b" +
            "e returned: \n0\tThis device is working properly. \n1\tThis device is not configured" +
            " correctly. \n2\tWindows cannot load the driver for this device. \n3\tThe driver for" +
            " this device might be corrupted, or your system may be running low on memory or " +
            "other resources. \n4\tThis device is not working properly. One of its drivers or y" +
            "our registry might be corrupted. \n5\tThe driver for this device needs a resource " +
            "that Windows cannot manage. \n6\tThe boot configuration for this device conflicts " +
            "with other devices. \n7\tCannot filter. \n8\tThe driver loader for the device is mis" +
            "sing. \n9\tThis device is not working properly because the controlling firmware is" +
            " reporting the resources for the device incorrectly. \n10\tThis device cannot star" +
            "t. \n11\tThis device failed. \n12\tThis device cannot find enough free resources tha" +
            "t it can use. \n13\tWindows cannot verify this device\'s resources. \n14\tThis device" +
            " cannot work properly until you restart your computer. \n15\tThis device is not wo" +
            "rking properly because there is probably a re-enumeration problem. \n16\tWindows c" +
            "annot identify all the resources this device uses. \n17\tThis device is asking for" +
            " an unknown resource type. \n18\tReinstall the drivers for this device. \n19\tYour r" +
            "egistry might be corrupted. \n20\tFailure using the VxD loader. \n21\tSystem failure" +
            ": Try changing the driver for this device. If that does not work, see your hardw" +
            "are documentation. Windows is removing this device. \n22\tThis device is disabled." +
            " \n23\tSystem failure: Try changing the driver for this device. If that doesn\'t wo" +
            "rk, see your hardware documentation. \n24\tThis device is not present, is not work" +
            "ing properly, or does not have all its drivers installed. \n25\tWindows is still s" +
            "etting up this device. \n26\tWindows is still setting up this device. \n27\tThis dev" +
            "ice does not have valid log configuration. \n28\tThe drivers for this device are n" +
            "ot installed. \n29\tThis device is disabled because the firmware of the device did" +
            " not give it the required resources. \n30\tThis device is using an Interrupt Reque" +
            "st (IRQ) resource that another device is using. \n31\tThis device is not working p" +
            "roperly because Windows cannot load the drivers required for this device.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ConfigManagerErrorCodeValues ConfigManagerErrorCode {
            get {
                if ((curObj["ConfigManagerErrorCode"] == null)) {
                    return ((ConfigManagerErrorCodeValues)(System.Convert.ToInt32(32)));
                }
                return ((ConfigManagerErrorCodeValues)(System.Convert.ToInt32(curObj["ConfigManagerErrorCode"])));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsConfigManagerUserConfigNull {
            get {
                if ((curObj["ConfigManagerUserConfig"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Indicates whether the device is using a user-defined configuration.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool ConfigManagerUserConfig {
            get {
                if ((curObj["ConfigManagerUserConfig"] == null)) {
                    return System.Convert.ToBoolean(0);
                }
                return ((bool)(curObj["ConfigManagerUserConfig"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsCorrectableErrorNull {
            get {
                if ((curObj["CorrectableError"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Boolean indicating that the most recent error was correctable. If the ErrorInfo p" +
            "roperty is equal to 3, \"OK\", then this property has no meaning.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool CorrectableError {
            get {
                if ((curObj["CorrectableError"] == null)) {
                    return System.Convert.ToBoolean(0);
                }
                return ((bool)(curObj["CorrectableError"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("CreationClassName indicates the name of the class or the subclass used in the cre" +
            "ation of an instance. When used with the other key properties of this class, thi" +
            "s property allows all instances of this class and its subclasses to be uniquely " +
            "identified.")]
        public string CreationClassName {
            get {
                return ((string)(curObj["CreationClassName"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The CurrentSRAM property indicates the types of Static Random Access Memory (SRAM" +
            ") that being used for the cache memory.")]
        public CurrentSRAMValues[] CurrentSRAM {
            get {
                System.Array arrEnumVals = ((System.Array)(curObj["CurrentSRAM"]));
                CurrentSRAMValues[] enumToRet = new CurrentSRAMValues[arrEnumVals.Length];
                int counter = 0;
                for (counter = 0; (counter < arrEnumVals.Length); counter = (counter + 1)) {
                    enumToRet[counter] = ((CurrentSRAMValues)(System.Convert.ToInt32(arrEnumVals.GetValue(counter))));
                }
                return enumToRet;
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The Description property provides a textual description of the object. ")]
        public string Description {
            get {
                return ((string)(curObj["Description"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The DeviceID property contains a string that uniquely identifies the cache repres" +
            "ented by an instance of Win32_CacheMemory.\nExample: Cache Memory 1")]
        public string DeviceID {
            get {
                return ((string)(curObj["DeviceID"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsEndingAddressNull {
            get {
                if ((curObj["EndingAddress"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The ending address, referenced by an application or operating system and mapped b" +
            "y a memory controller, for this memory object. The ending address is specified i" +
            "n KBytes.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ulong EndingAddress {
            get {
                if ((curObj["EndingAddress"] == null)) {
                    return System.Convert.ToUInt64(0);
                }
                return ((ulong)(curObj["EndingAddress"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsErrorAccessNull {
            get {
                if ((curObj["ErrorAccess"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("An integer enumeration indicating the memory access operation that caused the las" +
            "t error. The type of error is described by the ErrorInfo property. If the ErrorI" +
            "nfo property is equal to 3, \"OK\", then this property has no meaning.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ErrorAccessValues ErrorAccess {
            get {
                if ((curObj["ErrorAccess"] == null)) {
                    return ((ErrorAccessValues)(System.Convert.ToInt32(0)));
                }
                return ((ErrorAccessValues)(System.Convert.ToInt32(curObj["ErrorAccess"])));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsErrorAddressNull {
            get {
                if ((curObj["ErrorAddress"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Specifies the address of the last memory error. The type of error is described by" +
            " the ErrorInfo property. If the ErrorInfo property is equal to 3, \"OK\", then thi" +
            "s property has no meaning.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ulong ErrorAddress {
            get {
                if ((curObj["ErrorAddress"] == null)) {
                    return System.Convert.ToUInt64(0);
                }
                return ((ulong)(curObj["ErrorAddress"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsErrorClearedNull {
            get {
                if ((curObj["ErrorCleared"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("ErrorCleared is a boolean property indicating that the error reported in LastErro" +
            "rCode property is now cleared.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool ErrorCleared {
            get {
                if ((curObj["ErrorCleared"] == null)) {
                    return System.Convert.ToBoolean(0);
                }
                return ((bool)(curObj["ErrorCleared"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsErrorCorrectTypeNull {
            get {
                if ((curObj["ErrorCorrectType"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The ErrorCorrectType property indicates the error correction method used by the c" +
            "ache memory.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ErrorCorrectTypeValues ErrorCorrectType {
            get {
                if ((curObj["ErrorCorrectType"] == null)) {
                    return ((ErrorCorrectTypeValues)(System.Convert.ToInt32(7)));
                }
                return ((ErrorCorrectTypeValues)(System.Convert.ToInt32(curObj["ErrorCorrectType"])));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Data captured during the last erroneous memory access. The data occupies the firs" +
            "t n octets of the array necessary to hold the number of bits specified by the Er" +
            "rorTransferSize property. If ErrorTransferSize is 0, then this property has no m" +
            "eaning.")]
        public byte[] ErrorData {
            get {
                return ((byte[])(curObj["ErrorData"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsErrorDataOrderNull {
            get {
                if ((curObj["ErrorDataOrder"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The ordering for data stored in the ErrorData property. \"Least Significant Byte F" +
            "irst\" (value=1) or \"Most Significant Byte First\" (2) can be specified. If ErrorT" +
            "ransferSize is 0, then this property has no meaning.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ErrorDataOrderValues ErrorDataOrder {
            get {
                if ((curObj["ErrorDataOrder"] == null)) {
                    return ((ErrorDataOrderValues)(System.Convert.ToInt32(3)));
                }
                return ((ErrorDataOrderValues)(System.Convert.ToInt32(curObj["ErrorDataOrder"])));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("ErrorDescription is a free-form string supplying more information about the error" +
            " recorded in LastErrorCode property, and information on any corrective actions t" +
            "hat may be taken.")]
        public string ErrorDescription {
            get {
                return ((string)(curObj["ErrorDescription"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsErrorInfoNull {
            get {
                if ((curObj["ErrorInfo"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"An integer enumeration describing the type of error that occurred most recently. For example, single (value=6) or double bit errors (7) can be specified using this property. The values, 12-14, are undefined in the CIM Schema since in DMI, they mix the semantics of the type of error and whether it was correctable or not.  The latter is indicated in the property, CorrectableError.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ErrorInfoValues ErrorInfo {
            get {
                if ((curObj["ErrorInfo"] == null)) {
                    return ((ErrorInfoValues)(System.Convert.ToInt32(0)));
                }
                return ((ErrorInfoValues)(System.Convert.ToInt32(curObj["ErrorInfo"])));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("ErrorMethodology for CIM_Memory is a string property that indicates whether parit" +
            "y or CRC algorithms, ECC or other mechanisms are used. Details on the algorithm " +
            "can also be supplied.")]
        public string ErrorMethodology {
            get {
                return ((string)(curObj["ErrorMethodology"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsErrorResolutionNull {
            get {
                if ((curObj["ErrorResolution"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Specifies the range, in bytes, to which the last error can be resolved. For example, if error addresses are resolved to bit 11 (i.e., on a typical page basis), then errors can be resolved to 4K boundaries and this property is set to 4000. If the ErrorInfo property is equal to 3, ""OK"", then this property has no meaning.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ulong ErrorResolution {
            get {
                if ((curObj["ErrorResolution"] == null)) {
                    return System.Convert.ToUInt64(0);
                }
                return ((ulong)(curObj["ErrorResolution"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsErrorTimeNull {
            get {
                if ((curObj["ErrorTime"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The time that the last memory error occurred. The type of error is described by t" +
            "he ErrorInfo property. If the ErrorInfo property is equal to 3, \"OK\", then this " +
            "property has no meaning.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public System.DateTime ErrorTime {
            get {
                if ((curObj["ErrorTime"] != null)) {
                    return ToDateTime(((string)(curObj["ErrorTime"])));
                }
                else {
                    return System.DateTime.MinValue;
                }
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsErrorTransferSizeNull {
            get {
                if ((curObj["ErrorTransferSize"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The size of the data transfer in bits that caused the last error. 0 indicates no " +
            "error. If the ErrorInfo property is equal to 3, \"OK\", then this property should " +
            "be set to 0.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint ErrorTransferSize {
            get {
                if ((curObj["ErrorTransferSize"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((uint)(curObj["ErrorTransferSize"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsFlushTimerNull {
            get {
                if ((curObj["FlushTimer"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Maximum amount of time, in seconds, dirty lines or buckets may remain in the cach" +
            "e before they are flushed. A value of zero indicated that a cache flush is not c" +
            "ontrolled by a flushing timer.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint FlushTimer {
            get {
                if ((curObj["FlushTimer"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((uint)(curObj["FlushTimer"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsInstallDateNull {
            get {
                if ((curObj["InstallDate"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The InstallDate property is datetime value indicating when the object was install" +
            "ed. A lack of a value does not indicate that the object is not installed.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public System.DateTime InstallDate {
            get {
                if ((curObj["InstallDate"] != null)) {
                    return ToDateTime(((string)(curObj["InstallDate"])));
                }
                else {
                    return System.DateTime.MinValue;
                }
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsInstalledSizeNull {
            get {
                if ((curObj["InstalledSize"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The InstalledSize property indicates the current size of the installed cache memo" +
            "ry.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint InstalledSize {
            get {
                if ((curObj["InstalledSize"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((uint)(curObj["InstalledSize"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsLastErrorCodeNull {
            get {
                if ((curObj["LastErrorCode"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("LastErrorCode captures the last error code reported by the logical device.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint LastErrorCode {
            get {
                if ((curObj["LastErrorCode"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((uint)(curObj["LastErrorCode"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsLevelNull {
            get {
                if ((curObj["Level"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Defines whether this is the primary (value=3), secondary (value=4) or tertiary (v" +
            "alue=5) cache. Also, \"Other\" (1), \"Unknown\" (2) and \"Not Applicable\" (6) can be " +
            "defined.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public LevelValues Level {
            get {
                if ((curObj["Level"] == null)) {
                    return ((LevelValues)(System.Convert.ToInt32(0)));
                }
                return ((LevelValues)(System.Convert.ToInt32(curObj["Level"])));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsLineSizeNull {
            get {
                if ((curObj["LineSize"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Size, in bytes, of a single cache bucket or line.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint LineSize {
            get {
                if ((curObj["LineSize"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((uint)(curObj["LineSize"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsLocationNull {
            get {
                if ((curObj["Location"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The Location property indicates the physical location of the cache memory.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public LocationValues Location {
            get {
                if ((curObj["Location"] == null)) {
                    return ((LocationValues)(System.Convert.ToInt32(4)));
                }
                return ((LocationValues)(System.Convert.ToInt32(curObj["Location"])));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsMaxCacheSizeNull {
            get {
                if ((curObj["MaxCacheSize"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The MaxCacheSize property indicates the maximum cache size installable to this pa" +
            "rticular cache memory.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint MaxCacheSize {
            get {
                if ((curObj["MaxCacheSize"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((uint)(curObj["MaxCacheSize"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The Name property defines the label by which the object is known. When subclassed" +
            ", the Name property can be overridden to be a Key property.")]
        public string Name {
            get {
                return ((string)(curObj["Name"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsNumberOfBlocksNull {
            get {
                if ((curObj["NumberOfBlocks"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Total number of consecutive blocks, each block the size of the value contained in the BlockSize property, which form this storage extent. Total size of the storage extent can be calculated by multiplying the value of the BlockSize property by the value of this property. If the value of BlockSize is 1, this property is the total size of the storage extent.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ulong NumberOfBlocks {
            get {
                if ((curObj["NumberOfBlocks"] == null)) {
                    return System.Convert.ToUInt64(0);
                }
                return ((ulong)(curObj["NumberOfBlocks"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Free form string providing more information if the ErrorType property is set to 1" +
            ", \"Other\". If not set to 1, this string has no meaning.")]
        public string OtherErrorDescription {
            get {
                return ((string)(curObj["OtherErrorDescription"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Indicates the Win32 Plug and Play device ID of the logical device.  Example: *PNP" +
            "030b")]
        public string PNPDeviceID {
            get {
                return ((string)(curObj["PNPDeviceID"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Indicates the specific power-related capabilities of the logical device. The array values, 0=""Unknown"", 1=""Not Supported"" and 2=""Disabled"" are self-explanatory. The value, 3=""Enabled"" indicates that the power management features are currently enabled but the exact feature set is unknown or the information is unavailable. ""Power Saving Modes Entered Automatically"" (4) describes that a device can change its power state based on usage or other criteria. ""Power State Settable"" (5) indicates that the SetPowerState method is supported. ""Power Cycling Supported"" (6) indicates that the SetPowerState method can be invoked with the PowerState input variable set to 5 (""Power Cycle""). ""Timed Power On Supported"" (7) indicates that the SetPowerState method can be invoked with the PowerState input variable set to 5 (""Power Cycle"") and the Time parameter set to a specific date and time, or interval, for power-on.")]
        public PowerManagementCapabilitiesValues[] PowerManagementCapabilities {
            get {
                System.Array arrEnumVals = ((System.Array)(curObj["PowerManagementCapabilities"]));
                PowerManagementCapabilitiesValues[] enumToRet = new PowerManagementCapabilitiesValues[arrEnumVals.Length];
                int counter = 0;
                for (counter = 0; (counter < arrEnumVals.Length); counter = (counter + 1)) {
                    enumToRet[counter] = ((PowerManagementCapabilitiesValues)(System.Convert.ToInt32(arrEnumVals.GetValue(counter))));
                }
                return enumToRet;
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPowerManagementSupportedNull {
            get {
                if ((curObj["PowerManagementSupported"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Boolean indicating that the Device can be power managed - ie, put into a power save state. This boolean does not indicate that power management features are currently enabled, or if enabled, what features are supported. Refer to the PowerManagementCapabilities array for this information. If this boolean is false, the integer value 1, for the string, ""Not Supported"", should be the only entry in the PowerManagementCapabilities array.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool PowerManagementSupported {
            get {
                if ((curObj["PowerManagementSupported"] == null)) {
                    return System.Convert.ToBoolean(0);
                }
                return ((bool)(curObj["PowerManagementSupported"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("A free form string describing the media and/or its use.")]
        public string Purpose {
            get {
                return ((string)(curObj["Purpose"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsReadPolicyNull {
            get {
                if ((curObj["ReadPolicy"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Policy that shall be employed by the cache for handling read requests. For example, ""Read"", ""Read-Ahead"" or both can be specified using the values, 3, 4 or 5, respectively. If the read policy is determined individually (ie, for each request), then the value 6 (""Determination per I/O"") should be specified. ""Other"" (1) and ""Unknown"" (2) are also valid values.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ReadPolicyValues ReadPolicy {
            get {
                if ((curObj["ReadPolicy"] == null)) {
                    return ((ReadPolicyValues)(System.Convert.ToInt32(0)));
                }
                return ((ReadPolicyValues)(System.Convert.ToInt32(curObj["ReadPolicy"])));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsReplacementPolicyNull {
            get {
                if ((curObj["ReplacementPolicy"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("An integer enumeration describing the algorithm to determine which cache lines or" +
            " buckets should be re-used.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ReplacementPolicyValues ReplacementPolicy {
            get {
                if ((curObj["ReplacementPolicy"] == null)) {
                    return ((ReplacementPolicyValues)(System.Convert.ToInt32(0)));
                }
                return ((ReplacementPolicyValues)(System.Convert.ToInt32(curObj["ReplacementPolicy"])));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsStartingAddressNull {
            get {
                if ((curObj["StartingAddress"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The beginning address, referenced by an application or operating system and mappe" +
            "d by a memory controller, for this memory object. The starting address is specif" +
            "ied in KBytes.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ulong StartingAddress {
            get {
                if ((curObj["StartingAddress"] == null)) {
                    return System.Convert.ToUInt64(0);
                }
                return ((ulong)(curObj["StartingAddress"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"The Status property is a string indicating the current status of the object. Various operational and non-operational statuses can be defined. Operational statuses are ""OK"", ""Degraded"" and ""Pred Fail"". ""Pred Fail"" indicates that an element may be functioning properly but predicting a failure in the near future. An example is a SMART-enabled hard drive. Non-operational statuses can also be specified. These are ""Error"", ""Starting"", ""Stopping"" and ""Service"". The latter, ""Service"", could apply during mirror-resilvering of a disk, reload of a user permissions list, or other administrative work. Not all such work is on-line, yet the managed element is neither ""OK"" nor in one of the other states.")]
        public string Status {
            get {
                return ((string)(curObj["Status"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsStatusInfoNull {
            get {
                if ((curObj["StatusInfo"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("StatusInfo is a string indicating whether the logical device is in an enabled (va" +
            "lue = 3), disabled (value = 4) or some other (1) or unknown (2) state. If this p" +
            "roperty does not apply to the logical device, the value, 5 (\"Not Applicable\"), s" +
            "hould be used.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public StatusInfoValues StatusInfo {
            get {
                if ((curObj["StatusInfo"] == null)) {
                    return ((StatusInfoValues)(System.Convert.ToInt32(0)));
                }
                return ((StatusInfoValues)(System.Convert.ToInt32(curObj["StatusInfo"])));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The SupportedSRAM property indicates the supported types of Static Random Access " +
            "Memory (SRAM) that can be used for the cache memory.")]
        public SupportedSRAMValues[] SupportedSRAM {
            get {
                System.Array arrEnumVals = ((System.Array)(curObj["SupportedSRAM"]));
                SupportedSRAMValues[] enumToRet = new SupportedSRAMValues[arrEnumVals.Length];
                int counter = 0;
                for (counter = 0; (counter < arrEnumVals.Length); counter = (counter + 1)) {
                    enumToRet[counter] = ((SupportedSRAMValues)(System.Convert.ToInt32(arrEnumVals.GetValue(counter))));
                }
                return enumToRet;
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The scoping System\'s CreationClassName.")]
        public string SystemCreationClassName {
            get {
                return ((string)(curObj["SystemCreationClassName"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsSystemLevelAddressNull {
            get {
                if ((curObj["SystemLevelAddress"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Boolean indicating whether the address information in the property, ErrorAddress," +
            " is a system-level address (TRUE) or a physical address (FALSE). If the ErrorInf" +
            "o property is equal to 3, \"OK\", then this property has no meaning.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool SystemLevelAddress {
            get {
                if ((curObj["SystemLevelAddress"] == null)) {
                    return System.Convert.ToBoolean(0);
                }
                return ((bool)(curObj["SystemLevelAddress"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The scoping System\'s Name.")]
        public string SystemName {
            get {
                return ((string)(curObj["SystemName"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsWritePolicyNull {
            get {
                if ((curObj["WritePolicy"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Defines whether this is write-back (value=3) or write-through (value=4) Cache, or" +
            " whether this information \"Varies with Address\" (5) or is defined individually f" +
            "or each I/O (6). Also, \"Other\" (1) and \"Unknown\" (2) can be specified.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public WritePolicyValues WritePolicy {
            get {
                if ((curObj["WritePolicy"] == null)) {
                    return ((WritePolicyValues)(System.Convert.ToInt32(0)));
                }
                return ((WritePolicyValues)(System.Convert.ToInt32(curObj["WritePolicy"])));
            }
        }
        
        private bool CheckIfProperClass(System.Management.ManagementScope mgmtScope, System.Management.ManagementPath path, System.Management.ObjectGetOptions OptionsParam) {
            if (((path != null) 
                        && (string.Compare(path.ClassName, this.ManagementClassName, true, System.Globalization.CultureInfo.InvariantCulture) == 0))) {
                return true;
            }
            else {
                return CheckIfProperClass(new System.Management.ManagementObject(mgmtScope, path, OptionsParam));
            }
        }
        
        private bool CheckIfProperClass(System.Management.ManagementBaseObject theObj) {
            if (((theObj != null) 
                        && (string.Compare(((string)(theObj["__CLASS"])), this.ManagementClassName, true, System.Globalization.CultureInfo.InvariantCulture) == 0))) {
                return true;
            }
            else {
                System.Array parentClasses = ((System.Array)(theObj["__DERIVATION"]));
                if ((parentClasses != null)) {
                    int count = 0;
                    for (count = 0; (count < parentClasses.Length); count = (count + 1)) {
                        if ((string.Compare(((string)(parentClasses.GetValue(count))), this.ManagementClassName, true, System.Globalization.CultureInfo.InvariantCulture) == 0)) {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        
        private bool ShouldSerializeAccess() {
            if ((this.IsAccessNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeAssociativity() {
            if ((this.IsAssociativityNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeAvailability() {
            if ((this.IsAvailabilityNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeBlockSize() {
            if ((this.IsBlockSizeNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeCacheSpeed() {
            if ((this.IsCacheSpeedNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeCacheType() {
            if ((this.IsCacheTypeNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeConfigManagerErrorCode() {
            if ((this.IsConfigManagerErrorCodeNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeConfigManagerUserConfig() {
            if ((this.IsConfigManagerUserConfigNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeCorrectableError() {
            if ((this.IsCorrectableErrorNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeEndingAddress() {
            if ((this.IsEndingAddressNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeErrorAccess() {
            if ((this.IsErrorAccessNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeErrorAddress() {
            if ((this.IsErrorAddressNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeErrorCleared() {
            if ((this.IsErrorClearedNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeErrorCorrectType() {
            if ((this.IsErrorCorrectTypeNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeErrorDataOrder() {
            if ((this.IsErrorDataOrderNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeErrorInfo() {
            if ((this.IsErrorInfoNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeErrorResolution() {
            if ((this.IsErrorResolutionNull == false)) {
                return true;
            }
            return false;
        }
        
        // Converts a given datetime in DMTF format to System.DateTime object.
        static System.DateTime ToDateTime(string dmtfDate) {
            System.DateTime initializer = System.DateTime.MinValue;
            int year = initializer.Year;
            int month = initializer.Month;
            int day = initializer.Day;
            int hour = initializer.Hour;
            int minute = initializer.Minute;
            int second = initializer.Second;
            long ticks = 0;
            string dmtf = dmtfDate;
            System.DateTime datetime = System.DateTime.MinValue;
            string tempString = string.Empty;
            if ((dmtf == null)) {
                throw new System.ArgumentOutOfRangeException();
            }
            if ((dmtf.Length == 0)) {
                throw new System.ArgumentOutOfRangeException();
            }
            if ((dmtf.Length != 25)) {
                throw new System.ArgumentOutOfRangeException();
            }
            try {
                tempString = dmtf.Substring(0, 4);
                if (("****" != tempString)) {
                    year = int.Parse(tempString);
                }
                tempString = dmtf.Substring(4, 2);
                if (("**" != tempString)) {
                    month = int.Parse(tempString);
                }
                tempString = dmtf.Substring(6, 2);
                if (("**" != tempString)) {
                    day = int.Parse(tempString);
                }
                tempString = dmtf.Substring(8, 2);
                if (("**" != tempString)) {
                    hour = int.Parse(tempString);
                }
                tempString = dmtf.Substring(10, 2);
                if (("**" != tempString)) {
                    minute = int.Parse(tempString);
                }
                tempString = dmtf.Substring(12, 2);
                if (("**" != tempString)) {
                    second = int.Parse(tempString);
                }
                tempString = dmtf.Substring(15, 6);
                if (("******" != tempString)) {
                    ticks = (long.Parse(tempString) * ((long)((System.TimeSpan.TicksPerMillisecond / 1000))));
                }
                if (((((((((year < 0) 
                            || (month < 0)) 
                            || (day < 0)) 
                            || (hour < 0)) 
                            || (minute < 0)) 
                            || (minute < 0)) 
                            || (second < 0)) 
                            || (ticks < 0))) {
                    throw new System.ArgumentOutOfRangeException();
                }
            }
            catch (System.Exception e) {
                throw new System.ArgumentOutOfRangeException(null, e.Message);
            }
            datetime = new System.DateTime(year, month, day, hour, minute, second, 0);
            datetime = datetime.AddTicks(ticks);
            System.TimeSpan tickOffset = System.TimeZone.CurrentTimeZone.GetUtcOffset(datetime);
            int UTCOffset = 0;
            int OffsetToBeAdjusted = 0;
            long OffsetMins = ((long)((tickOffset.Ticks / System.TimeSpan.TicksPerMinute)));
            tempString = dmtf.Substring(22, 3);
            if ((tempString != "******")) {
                tempString = dmtf.Substring(21, 4);
                try {
                    UTCOffset = int.Parse(tempString);
                }
                catch (System.Exception e) {
                    throw new System.ArgumentOutOfRangeException(null, e.Message);
                }
                OffsetToBeAdjusted = ((int)((OffsetMins - UTCOffset)));
                datetime = datetime.AddMinutes(((double)(OffsetToBeAdjusted)));
            }
            return datetime;
        }
        
        // Converts a given System.DateTime object to DMTF datetime format.
        static string ToDmtfDateTime(System.DateTime date) {
            string utcString = string.Empty;
            System.TimeSpan tickOffset = System.TimeZone.CurrentTimeZone.GetUtcOffset(date);
            long OffsetMins = ((long)((tickOffset.Ticks / System.TimeSpan.TicksPerMinute)));
            if ((System.Math.Abs(OffsetMins) > 999)) {
                date = date.ToUniversalTime();
                utcString = "+000";
            }
            else {
                if ((tickOffset.Ticks >= 0)) {
                    utcString = string.Concat("+", ((System.Int64 )((tickOffset.Ticks / System.TimeSpan.TicksPerMinute))).ToString().PadLeft(3, '0'));
                }
                else {
                    string strTemp = ((System.Int64 )(OffsetMins)).ToString();
                    utcString = string.Concat("-", strTemp.Substring(1, (strTemp.Length - 1)).PadLeft(3, '0'));
                }
            }
            string dmtfDateTime = ((System.Int32 )(date.Year)).ToString().PadLeft(4, '0');
            dmtfDateTime = string.Concat(dmtfDateTime, ((System.Int32 )(date.Month)).ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, ((System.Int32 )(date.Day)).ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, ((System.Int32 )(date.Hour)).ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, ((System.Int32 )(date.Minute)).ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, ((System.Int32 )(date.Second)).ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, ".");
            System.DateTime dtTemp = new System.DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, 0);
            long microsec = ((long)((((date.Ticks - dtTemp.Ticks) 
                        * 1000) 
                        / System.TimeSpan.TicksPerMillisecond)));
            string strMicrosec = ((System.Int64 )(microsec)).ToString();
            if ((strMicrosec.Length > 6)) {
                strMicrosec = strMicrosec.Substring(0, 6);
            }
            dmtfDateTime = string.Concat(dmtfDateTime, strMicrosec.PadLeft(6, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, utcString);
            return dmtfDateTime;
        }
        
        private bool ShouldSerializeErrorTime() {
            if ((this.IsErrorTimeNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeErrorTransferSize() {
            if ((this.IsErrorTransferSizeNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeFlushTimer() {
            if ((this.IsFlushTimerNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeInstallDate() {
            if ((this.IsInstallDateNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeInstalledSize() {
            if ((this.IsInstalledSizeNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeLastErrorCode() {
            if ((this.IsLastErrorCodeNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeLevel() {
            if ((this.IsLevelNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeLineSize() {
            if ((this.IsLineSizeNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeLocation() {
            if ((this.IsLocationNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeMaxCacheSize() {
            if ((this.IsMaxCacheSizeNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeNumberOfBlocks() {
            if ((this.IsNumberOfBlocksNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializePowerManagementSupported() {
            if ((this.IsPowerManagementSupportedNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeReadPolicy() {
            if ((this.IsReadPolicyNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeReplacementPolicy() {
            if ((this.IsReplacementPolicyNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeStartingAddress() {
            if ((this.IsStartingAddressNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeStatusInfo() {
            if ((this.IsStatusInfoNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeSystemLevelAddress() {
            if ((this.IsSystemLevelAddressNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeWritePolicy() {
            if ((this.IsWritePolicyNull == false)) {
                return true;
            }
            return false;
        }
        
        [Browsable(true)]
        public void CommitObject() {
            if ((isEmbedded == false)) {
                PrivateLateBoundObject.Put();
            }
        }
        
        [Browsable(true)]
        public void CommitObject(System.Management.PutOptions putOptions) {
            if ((isEmbedded == false)) {
                PrivateLateBoundObject.Put(putOptions);
            }
        }
        
        private void Initialize() {
            AutoCommitProp = true;
            isEmbedded = false;
        }
        
        private static string ConstructPath(string keyDeviceID) {
            string strPath = "root\\cimv2:Win32_CacheMemory";
            strPath = string.Concat(strPath, string.Concat(".DeviceID=", string.Concat("\"", string.Concat(keyDeviceID, "\""))));
            return strPath;
        }
        
        private void InitializeObject(System.Management.ManagementScope mgmtScope, System.Management.ManagementPath path, System.Management.ObjectGetOptions getOptions) {
            Initialize();
            if ((path != null)) {
                if ((CheckIfProperClass(mgmtScope, path, getOptions) != true)) {
                    throw new System.ArgumentException("Class name does not match.");
                }
            }
            PrivateLateBoundObject = new System.Management.ManagementObject(mgmtScope, path, getOptions);
            PrivateSystemProperties = new ManagementSystemProperties(PrivateLateBoundObject);
            curObj = PrivateLateBoundObject;
        }
        
        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static CacheMemoryCollection GetInstances() {
            return GetInstances(null, null, null);
        }
        
        public static CacheMemoryCollection GetInstances(string condition) {
            return GetInstances(null, condition, null);
        }
        
        public static CacheMemoryCollection GetInstances(System.String [] selectedProperties) {
            return GetInstances(null, null, selectedProperties);
        }
        
        public static CacheMemoryCollection GetInstances(string condition, System.String [] selectedProperties) {
            return GetInstances(null, condition, selectedProperties);
        }
        
        public static CacheMemoryCollection GetInstances(System.Management.ManagementScope mgmtScope, System.Management.EnumerationOptions enumOptions) {
            if ((mgmtScope == null)) {
                if ((statMgmtScope == null)) {
                    mgmtScope = new System.Management.ManagementScope();
                    mgmtScope.Path.NamespacePath = "root\\cimv2";
                }
                else {
                    mgmtScope = statMgmtScope;
                }
            }
            System.Management.ManagementPath pathObj = new System.Management.ManagementPath();
            pathObj.ClassName = "Win32_CacheMemory";
            pathObj.NamespacePath = "root\\cimv2";
            System.Management.ManagementClass clsObject = new System.Management.ManagementClass(mgmtScope, pathObj, null);
            if ((enumOptions == null)) {
                enumOptions = new System.Management.EnumerationOptions();
                enumOptions.EnsureLocatable = true;
            }
            return new CacheMemoryCollection(clsObject.GetInstances(enumOptions));
        }
        
        public static CacheMemoryCollection GetInstances(System.Management.ManagementScope mgmtScope, string condition) {
            return GetInstances(mgmtScope, condition, null);
        }
        
        public static CacheMemoryCollection GetInstances(System.Management.ManagementScope mgmtScope, System.String [] selectedProperties) {
            return GetInstances(mgmtScope, null, selectedProperties);
        }
        
        public static CacheMemoryCollection GetInstances(System.Management.ManagementScope mgmtScope, string condition, System.String [] selectedProperties) {
            if ((mgmtScope == null)) {
                if ((statMgmtScope == null)) {
                    mgmtScope = new System.Management.ManagementScope();
                    mgmtScope.Path.NamespacePath = "root\\cimv2";
                }
                else {
                    mgmtScope = statMgmtScope;
                }
            }
            System.Management.ManagementObjectSearcher ObjectSearcher = new System.Management.ManagementObjectSearcher(mgmtScope, new SelectQuery("Win32_CacheMemory", condition, selectedProperties));
            System.Management.EnumerationOptions enumOptions = new System.Management.EnumerationOptions();
            enumOptions.EnsureLocatable = true;
            ObjectSearcher.Options = enumOptions;
            return new CacheMemoryCollection(ObjectSearcher.Get());
        }
        
        [Browsable(true)]
        public static CacheMemory CreateInstance() {
            System.Management.ManagementScope mgmtScope = null;
            if ((statMgmtScope == null)) {
                mgmtScope = new System.Management.ManagementScope();
                mgmtScope.Path.NamespacePath = CreatedWmiNamespace;
            }
            else {
                mgmtScope = statMgmtScope;
            }
            System.Management.ManagementPath mgmtPath = new System.Management.ManagementPath(CreatedClassName);
            System.Management.ManagementClass tmpMgmtClass = new System.Management.ManagementClass(mgmtScope, mgmtPath, null);
            return new CacheMemory(tmpMgmtClass.CreateInstance());
        }
        
        [Browsable(true)]
        public void Delete() {
            PrivateLateBoundObject.Delete();
        }
        
        public uint Reset() {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("Reset", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public uint SetPowerState(ushort PowerState, System.DateTime Time) {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                inParams = PrivateLateBoundObject.GetMethodParameters("SetPowerState");
                inParams["PowerState"] = ((System.UInt16 )(PowerState));
                inParams["Time"] = ToDmtfDateTime(((System.DateTime)(Time)));
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("SetPowerState", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public enum AccessValues {
            
            Unknown0 = 0,
            
            Readable = 1,
            
            Writeable = 2,
            
            Read_Write_Supported = 3,
            
            Write_Once = 4,
            
            NULL_ENUM_VALUE = 5,
        }
        
        public enum AssociativityValues {
            
            Other0 = 1,
            
            Unknown0 = 2,
            
            Direct_Mapped = 3,
            
            Val_2_way_Set_Associative = 4,
            
            Val_4_way_Set_Associative = 5,
            
            Fully_Associative = 6,
            
            Val_8_way_Set_Associative = 7,
            
            Val_16_way_Set_Associative = 8,
            
            NULL_ENUM_VALUE = 0,
        }
        
        public enum AvailabilityValues {
            
            Other0 = 1,
            
            Unknown0 = 2,
            
            Running_Full_Power = 3,
            
            Warning = 4,
            
            In_Test = 5,
            
            Not_Applicable = 6,
            
            Power_Off = 7,
            
            Off_Line = 8,
            
            Off_Duty = 9,
            
            Degraded = 10,
            
            Not_Installed = 11,
            
            Install_Error = 12,
            
            Power_Save_Unknown = 13,
            
            Power_Save_Low_Power_Mode = 14,
            
            Power_Save_Standby = 15,
            
            Power_Cycle = 16,
            
            Power_Save_Warning = 17,
            
            Paused = 18,
            
            Not_Ready = 19,
            
            Not_Configured = 20,
            
            Quiesced = 21,
            
            NULL_ENUM_VALUE = 0,
        }
        
        public enum CacheTypeValues {
            
            Other0 = 1,
            
            Unknown0 = 2,
            
            Instruction = 3,
            
            Data = 4,
            
            Unified = 5,
            
            NULL_ENUM_VALUE = 0,
        }
        
        public enum ConfigManagerErrorCodeValues {
            
            This_device_is_working_properly_ = 0,
            
            This_device_is_not_configured_correctly_ = 1,
            
            Windows_cannot_load_the_driver_for_this_device_ = 2,
            
            The_driver_for_this_device_might_be_corrupted_or_your_system_may_be_running_low_on_memory_or_other_resources_ = 3,
            
            This_device_is_not_working_properly_One_of_its_drivers_or_your_registry_might_be_corrupted_ = 4,
            
            The_driver_for_this_device_needs_a_resource_that_Windows_cannot_manage_ = 5,
            
            The_boot_configuration_for_this_device_conflicts_with_other_devices_ = 6,
            
            Cannot_filter_ = 7,
            
            The_driver_loader_for_the_device_is_missing_ = 8,
            
            This_device_is_not_working_properly_because_the_controlling_firmware_is_reporting_the_resources_for_the_device_incorrectly_ = 9,
            
            This_device_cannot_start_ = 10,
            
            This_device_failed_ = 11,
            
            This_device_cannot_find_enough_free_resources_that_it_can_use_ = 12,
            
            Windows_cannot_verify_this_device_s_resources_ = 13,
            
            This_device_cannot_work_properly_until_you_restart_your_computer_ = 14,
            
            This_device_is_not_working_properly_because_there_is_probably_a_re_enumeration_problem_ = 15,
            
            Windows_cannot_identify_all_the_resources_this_device_uses_ = 16,
            
            This_device_is_asking_for_an_unknown_resource_type_ = 17,
            
            Reinstall_the_drivers_for_this_device_ = 18,
            
            Failure_using_the_VxD_loader_ = 19,
            
            Your_registry_might_be_corrupted_ = 20,
            
            System_failure_Try_changing_the_driver_for_this_device_If_that_does_not_work_see_your_hardware_documentation_Windows_is_removing_this_device_ = 21,
            
            This_device_is_disabled_ = 22,
            
            System_failure_Try_changing_the_driver_for_this_device_If_that_doesn_t_work_see_your_hardware_documentation_ = 23,
            
            This_device_is_not_present_is_not_working_properly_or_does_not_have_all_its_drivers_installed_ = 24,
            
            Windows_is_still_setting_up_this_device_ = 25,
            
            Windows_is_still_setting_up_this_device_0 = 26,
            
            This_device_does_not_have_valid_log_configuration_ = 27,
            
            The_drivers_for_this_device_are_not_installed_ = 28,
            
            This_device_is_disabled_because_the_firmware_of_the_device_did_not_give_it_the_required_resources_ = 29,
            
            This_device_is_using_an_Interrupt_Request_IRQ_resource_that_another_device_is_using_ = 30,
            
            This_device_is_not_working_properly_because_Windows_cannot_load_the_drivers_required_for_this_device_ = 31,
            
            NULL_ENUM_VALUE = 32,
        }
        
        public enum CurrentSRAMValues {
            
            Other0 = 0,
            
            Unknown0 = 1,
            
            Non_Burst = 2,
            
            Burst = 3,
            
            Pipeline_Burst = 4,
            
            Synchronous = 5,
            
            Asynchronous = 6,
            
            NULL_ENUM_VALUE = 7,
        }
        
        public enum ErrorAccessValues {
            
            Other0 = 1,
            
            Unknown0 = 2,
            
            Read = 3,
            
            Write = 4,
            
            Partial_Write = 5,
            
            NULL_ENUM_VALUE = 0,
        }
        
        public enum ErrorCorrectTypeValues {
            
            Reserved = 0,
            
            Other0 = 1,
            
            Unknown0 = 2,
            
            None = 3,
            
            Parity = 4,
            
            Single_bit_ECC = 5,
            
            Multi_bit_ECC = 6,
            
            NULL_ENUM_VALUE = 7,
        }
        
        public enum ErrorDataOrderValues {
            
            Unknown0 = 0,
            
            Least_Significant_Byte_First = 1,
            
            Most_Significant_Byte_First = 2,
            
            NULL_ENUM_VALUE = 3,
        }
        
        public enum ErrorInfoValues {
            
            Other0 = 1,
            
            Unknown0 = 2,
            
            OK = 3,
            
            Bad_Read = 4,
            
            Parity_Error = 5,
            
            Single_Bit_Error = 6,
            
            Double_Bit_Error = 7,
            
            Multi_Bit_Error = 8,
            
            Nibble_Error = 9,
            
            Checksum_Error = 10,
            
            CRC_Error = 11,
            
            Undefined = 12,
            
            Undefined0 = 13,
            
            Undefined1 = 14,
            
            NULL_ENUM_VALUE = 0,
        }
        
        public enum LevelValues {
            
            Other0 = 1,
            
            Unknown0 = 2,
            
            Primary = 3,
            
            Secondary = 4,
            
            Tertiary = 5,
            
            Not_Applicable = 6,
            
            NULL_ENUM_VALUE = 0,
        }
        
        public enum LocationValues {
            
            Internal = 0,
            
            External = 1,
            
            Reserved = 2,
            
            Unknown0 = 3,
            
            NULL_ENUM_VALUE = 4,
        }
        
        public enum PowerManagementCapabilitiesValues {
            
            Unknown0 = 0,
            
            Not_Supported = 1,
            
            Disabled = 2,
            
            Enabled = 3,
            
            Power_Saving_Modes_Entered_Automatically = 4,
            
            Power_State_Settable = 5,
            
            Power_Cycling_Supported = 6,
            
            Timed_Power_On_Supported = 7,
            
            NULL_ENUM_VALUE = 8,
        }
        
        public enum ReadPolicyValues {
            
            Other0 = 1,
            
            Unknown0 = 2,
            
            Read = 3,
            
            Read_Ahead = 4,
            
            Read_and_Read_Ahead = 5,
            
            Determination_Per_I_O = 6,
            
            NULL_ENUM_VALUE = 0,
        }
        
        public enum ReplacementPolicyValues {
            
            Other0 = 1,
            
            Unknown0 = 2,
            
            Least_Recently_Used_LRU_ = 3,
            
            First_In_First_Out_FIFO_ = 4,
            
            Last_In_First_Out_LIFO_ = 5,
            
            Least_Frequently_Used_LFU_ = 6,
            
            Most_Frequently_Used_MFU_ = 7,
            
            Data_Dependent_Multiple_Algorithms = 8,
            
            NULL_ENUM_VALUE = 0,
        }
        
        public enum StatusInfoValues {
            
            Other0 = 1,
            
            Unknown0 = 2,
            
            Enabled = 3,
            
            Disabled = 4,
            
            Not_Applicable = 5,
            
            NULL_ENUM_VALUE = 0,
        }
        
        public enum SupportedSRAMValues {
            
            Other0 = 0,
            
            Unknown0 = 1,
            
            Non_Burst = 2,
            
            Burst = 3,
            
            Pipeline_Burst = 4,
            
            Synchronous = 5,
            
            Asynchronous = 6,
            
            NULL_ENUM_VALUE = 7,
        }
        
        public enum WritePolicyValues {
            
            Other0 = 1,
            
            Unknown0 = 2,
            
            Write_Back = 3,
            
            Write_Through = 4,
            
            Varies_with_Address = 5,
            
            Determination_Per_I_O = 6,
            
            NULL_ENUM_VALUE = 0,
        }
        
        // Enumerator implementation for enumerating instances of the class.
        public class CacheMemoryCollection : object, ICollection {
            
            private ManagementObjectCollection privColObj;
            
            public CacheMemoryCollection(ManagementObjectCollection objCollection) {
                privColObj = objCollection;
            }
            
            public virtual int Count {
                get {
                    return privColObj.Count;
                }
            }
            
            public virtual bool IsSynchronized {
                get {
                    return privColObj.IsSynchronized;
                }
            }
            
            public virtual object SyncRoot {
                get {
                    return this;
                }
            }
            
            public virtual void CopyTo(System.Array array, int index) {
                privColObj.CopyTo(array, index);
                int nCtr;
                for (nCtr = 0; (nCtr < array.Length); nCtr = (nCtr + 1)) {
                    array.SetValue(new CacheMemory(((System.Management.ManagementObject)(array.GetValue(nCtr)))), nCtr);
                }
            }
            
            public virtual System.Collections.IEnumerator GetEnumerator() {
                return new CacheMemoryEnumerator(privColObj.GetEnumerator());
            }
            
            public class CacheMemoryEnumerator : object, System.Collections.IEnumerator {
                
                private ManagementObjectCollection.ManagementObjectEnumerator privObjEnum;
                
                public CacheMemoryEnumerator(ManagementObjectCollection.ManagementObjectEnumerator objEnum) {
                    privObjEnum = objEnum;
                }
                
                public virtual object Current {
                    get {
                        return new CacheMemory(((System.Management.ManagementObject)(privObjEnum.Current)));
                    }
                }
                
                public virtual bool MoveNext() {
                    return privObjEnum.MoveNext();
                }
                
                public virtual void Reset() {
                    privObjEnum.Reset();
                }
            }
        }
        
        // TypeConverter to handle null values for ValueType properties
        public class WMIValueTypeConverter : TypeConverter {
            
            private TypeConverter baseConverter;
            
            private System.Type baseType;
            
            public WMIValueTypeConverter(System.Type inBaseType) {
                baseConverter = TypeDescriptor.GetConverter(inBaseType);
                baseType = inBaseType;
            }
            
            public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Type srcType) {
                return baseConverter.CanConvertFrom(context, srcType);
            }
            
            public override bool CanConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Type destinationType) {
                return baseConverter.CanConvertTo(context, destinationType);
            }
            
            public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value) {
                return baseConverter.ConvertFrom(context, culture, value);
            }
            
            public override object CreateInstance(System.ComponentModel.ITypeDescriptorContext context, System.Collections.IDictionary dictionary) {
                return baseConverter.CreateInstance(context, dictionary);
            }
            
            public override bool GetCreateInstanceSupported(System.ComponentModel.ITypeDescriptorContext context) {
                return baseConverter.GetCreateInstanceSupported(context);
            }
            
            public override PropertyDescriptorCollection GetProperties(System.ComponentModel.ITypeDescriptorContext context, object value, System.Attribute[] attributeVar) {
                return baseConverter.GetProperties(context, value, attributeVar);
            }
            
            public override bool GetPropertiesSupported(System.ComponentModel.ITypeDescriptorContext context) {
                return baseConverter.GetPropertiesSupported(context);
            }
            
            public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(System.ComponentModel.ITypeDescriptorContext context) {
                return baseConverter.GetStandardValues(context);
            }
            
            public override bool GetStandardValuesExclusive(System.ComponentModel.ITypeDescriptorContext context) {
                return baseConverter.GetStandardValuesExclusive(context);
            }
            
            public override bool GetStandardValuesSupported(System.ComponentModel.ITypeDescriptorContext context) {
                return baseConverter.GetStandardValuesSupported(context);
            }
            
            public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, System.Type destinationType) {
                if ((baseType.BaseType == typeof(System.Enum))) {
                    if ((value.GetType() == destinationType)) {
                        return value;
                    }
                    if ((((value == null) 
                                && (context != null)) 
                                && (context.PropertyDescriptor.ShouldSerializeValue(context.Instance) == false))) {
                        return  "NULL_ENUM_VALUE" ;
                    }
                    return baseConverter.ConvertTo(context, culture, value, destinationType);
                }
                if (((baseType == typeof(bool)) 
                            && (baseType.BaseType == typeof(System.ValueType)))) {
                    if ((((value == null) 
                                && (context != null)) 
                                && (context.PropertyDescriptor.ShouldSerializeValue(context.Instance) == false))) {
                        return "";
                    }
                    return baseConverter.ConvertTo(context, culture, value, destinationType);
                }
                if (((context != null) 
                            && (context.PropertyDescriptor.ShouldSerializeValue(context.Instance) == false))) {
                    return "";
                }
                return baseConverter.ConvertTo(context, culture, value, destinationType);
            }
        }
        
        // Embedded class to represent WMI system Properties.
        [TypeConverter(typeof(System.ComponentModel.ExpandableObjectConverter))]
        public class ManagementSystemProperties {
            
            private System.Management.ManagementBaseObject PrivateLateBoundObject;
            
            public ManagementSystemProperties(System.Management.ManagementBaseObject ManagedObject) {
                PrivateLateBoundObject = ManagedObject;
            }
            
            [Browsable(true)]
            public int GENUS {
                get {
                    return ((int)(PrivateLateBoundObject["__GENUS"]));
                }
            }
            
            [Browsable(true)]
            public string CLASS {
                get {
                    return ((string)(PrivateLateBoundObject["__CLASS"]));
                }
            }
            
            [Browsable(true)]
            public string SUPERCLASS {
                get {
                    return ((string)(PrivateLateBoundObject["__SUPERCLASS"]));
                }
            }
            
            [Browsable(true)]
            public string DYNASTY {
                get {
                    return ((string)(PrivateLateBoundObject["__DYNASTY"]));
                }
            }
            
            [Browsable(true)]
            public string RELPATH {
                get {
                    return ((string)(PrivateLateBoundObject["__RELPATH"]));
                }
            }
            
            [Browsable(true)]
            public int PROPERTY_COUNT {
                get {
                    return ((int)(PrivateLateBoundObject["__PROPERTY_COUNT"]));
                }
            }
            
            [Browsable(true)]
            public string[] DERIVATION {
                get {
                    return ((string[])(PrivateLateBoundObject["__DERIVATION"]));
                }
            }
            
            [Browsable(true)]
            public string SERVER {
                get {
                    return ((string)(PrivateLateBoundObject["__SERVER"]));
                }
            }
            
            [Browsable(true)]
            public string NAMESPACE {
                get {
                    return ((string)(PrivateLateBoundObject["__NAMESPACE"]));
                }
            }
            
            [Browsable(true)]
            public string PATH {
                get {
                    return ((string)(PrivateLateBoundObject["__PATH"]));
                }
            }
        }
    }
}
