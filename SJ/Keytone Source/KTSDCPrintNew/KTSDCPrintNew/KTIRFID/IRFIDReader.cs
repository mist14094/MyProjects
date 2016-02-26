using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;


namespace KTone.Core.KTIRFID
{
    /// <summary>
    /// Holds informations of Input/Output pin status. 
    /// </summary>
    [Serializable]
    public struct IOPinStatus
    {
        /// <summary>
        /// indicates pin name/number. 
        /// </summary>
        public int pinNo;
        /// <summary>
        /// indicates the status:On/Off of the pin(true corresponds to On while false corresponds to Off).
        /// </summary>
        public bool pinStatus;
        /// <summary>
        /// Constructor intializes the member variables.
        /// </summary>
        /// <param name="pinNo">IOPins</param>
        /// <param name="pinStatus">bool representing pin status</param>
        public IOPinStatus(int pinNo, bool pinStatus)
        {
            this.pinNo = pinNo;
            this.pinStatus = pinStatus;
        }
    }

    /// <summary>
    /// <exclude />
    /// </summary>
    public interface IRFIDReader
    {
        #region RFID Commands
        /// <summary>
        /// Read the Id of a tag.
        /// </summary>
        /// <returns>IRFID tag array</returns>
        IRFIDTag[] ReadId();

        /// <summary>
        /// Read data from a tag
        /// </summary>
        /// <param name="tagID">tagID of tag to be read data.</param>
        /// <param name="startAddress">start byte number</param>
        /// <param name="noBytes">number of bytes to be read</param>
        /// <param name="retryCount">retry count</param>
        /// <returns>IRFID tag </returns>
        IRFIDTag ReadData(string tagID, int startAddress, int noBytes, int retryCount);

        /// <summary>
        /// Read data from a tag using the tag template associated with the reader.
        /// </summary>
        /// <param name="tagID">tagID of tag to be read data.</param>
        /// <param name="startAddress">start byte number</param>
        /// <param name="noBytes">number of bytes to be read</param>
        /// <param name="retryCount">retry count</param>
        /// <returns>Dictionary that contains name - value pair of tag data fields transformed using the tag template</returns>
        Dictionary<string, string> ReadDataSegments(string tagID, int startAddress, int noBytes, int retryCount);

        /// <summary>
        /// Read data from a tag using the tag template associated with the reader.
        /// </summary>
        /// <param name="tagID">tagID of tag to be read data.</param>
        /// <param name="segmentNames">list of segment names</param>
        /// <returns>Dictionary that contains name - value pair of tag data fields transformed using the tag template</returns>
        Dictionary<string, object> ReadDataSegments(string tagID, string[] segmentNames, int retryCount);


        /// <summary>
        /// Write ID on a blank tag.
        /// </summary>
        /// <param name="tagID"></param>
        void WriteID(string tagID);

        /// <summary>
        ///  Write ID on a blank tag 
        ///  This method accept tag type (protocol) and antenna name
        /// </summary>
        /// <param name="tagID"></param>
        /// <param name="protocol"></param>
        /// <param name="antennaId"></param>
        void WriteID(string tagID, TagProtocol protocol, string antennaId);

        /// <summary>
        /// Write ID with filter
        /// </summary>
        /// <param name="oldTagID"></param>
        /// <param name="newTagID"></param>
        /// <param name="retryCount"></param>
        /// <returns></returns>
        bool WriteID(string oldTagID, string newTagID, int retryCount);
        /// <summary>
        /// Write data on a tag.
        /// </summary>
        /// <param name="tagID">tagID of tag to be read data.</param>
        /// <param name="startAddress">start byte number</param>
        /// <param name="dataBytes">byte array containing data to be written on the tag</param>
        void WriteData(string tagID, int startAddress, byte[] dataBytes, int retryCount);


        /// <summary>
        /// Write data on a tag using the tag template associated with the reader.
        /// </summary>
        /// <param name="tagID">tagID of tag to be read data.</param>
        /// <param name="tagDataDictionary">Dictionary that contains name - value pair of tag data fields transformed using the tag template</param>
        void WriteDataSegments(string tagID, Dictionary<string, string> tagDataDictionary, int retryCount);

        /// <summary>
        //This command  fills the tag with the Fill Byte, 
        //for the specified number of consecutive bytes,beginning at the Start Address.
        //When the Fill Length is set to zero (0x0000), 
        //the controller will write fill data from the Start Address to the end of the tag’s memory.
        /// </summary>
        /// <param name="tagID">tagID of tag to be read data.</param>
        /// <param name="startAddress">start byte number</param>
        /// <param name="fillLength">number of bytes to be filled by the dataByte</param>
        /// <param name="dataByte">data to be written on the tag</param>
        void FillData(string tagID, int startAddress, int fillLength, byte dataByte);

        /// <summary>
        /// starts continuous reading of tags.
        /// </summary>
        void StartContinuousMode();

        /// <summary>
        /// stops continuous read of tags.
        /// </summary>
        void StopContinuousMode();

        /// <summary>
        /// Retrieves the Input and output pins status
        /// </summary>
        /// <param name="inputPinsStatus"></param>
        /// <param name="outputPinsStatus"></param>
        void GetIOStatus(out IOPinStatus[] inputPinsStatus, out IOPinStatus[] outputPinsStatus);

        /// <summary>
        /// Set output lines status
        /// </summary>
        /// <param name="outputPinStatus"></param>
        void SetOutputStatus(IOPinStatus[] outputPinStatus);

        /// <summary>
        /// Reboots the Reader through HTTP
        /// </summary>
        void HttpReboot();

        /// <summary>
        /// Reboots the Reader through its established communication channel. 
        /// </summary>
        void Reboot();

        /// <summary>
        /// Ping the Reader. Call a command like GetVersion on the reader to make sure 
        /// that the reader communication with the device is established properly.
        /// </summary>
        void Ping();

        /// <summary>
        /// Kills the tag
        /// </summary>
        /// <param name="hexTagId">Tag id in hex format e.g. 31142BBB9C2FAF0804000000</param>
        /// ///<param name="hexKillPassword">Kill password in hex format e.g. 11223344 </param>
        void KillTag(string hexTagId, string hexKillPassword);

        /// <summary>
        /// Writes kill password on a tag with. 
        /// </summary>
        /// <param name="hexKillPassword">Kill password in hex format e.g. 11223344</param>
        void WriteKillPassword(string hexKillPassword);

        /// <summary>
        /// Writes kill password on a tag where the tag is protected with a non-zero access password.
        /// </summary>
        /// <param name="hexKillPassword">Kill password in hex format e.g. 11223344</param>
        /// <param name="hexAccessPassword">Access password in hex format e.g. 11223344</param>
        void WriteKillPassword(string hexKillPassword, string hexAccessPassword);
        #endregion RFID Commands

        #region Other methods

        /// <summary>
        /// Disconnect and again connect with the reader.
        /// </summary>
        void Reconnect();

        /// <summary>
        /// Set the Profile(configuration) of the reader 
        /// </summary>        
        void SetProfile(Dictionary<ProfileKey, object> profile);

        /// <summary>
        /// Get the Profile(configuration) of the reader 
        /// </summary>        
        object GetProfile(ProfileKey profileKey);

        /// <summary>
        /// Get the Capability of the reader 
        /// </summary>        
        Dictionary<ProfileKey, object> GetReaderCapability();
        /// <summary>
        /// sets the EpcMask on Reader Level
        /// </summary>
        /// <param name="isEnableEpcMask"></param>
        /// <param name="epcMask"></param>
        void SetReaderEpcMask(bool isEnableEpcMask, string epcMask);
        /// <summary>
        /// Sets the Antenna level epc mask
        /// </summary>
        /// <param name="antennaName"></param>
        /// <param name="isEnableEpcMask"></param>
        /// <param name="epcMask"></param>
        void SetAntennaEpcMask(string antennaName, bool isEnableEpcMask, string epcMask);
        /// <summary>
        /// Gets all Reader level and Antenna level masks
        /// </summary>
        /// <returns></returns>
        Dictionary<string, string> GetAllEpcMask();
        #endregion Other methods

        #region Properties

        /// <summary>
        /// It will return a string that will include communication details such as communication mode,
        /// IP address,port,COM port.
        /// e.g when the communication mode is ethernet, it will return Ethernet:192.168.0.1:5000  
        /// When the communication mode is serial, it will return Serial:COM1
        /// </summary>
        string CommunicationDetails
        {
            get;
        }



        /// <summary>
        /// Returns the communication mode of the underlying drivers(ethernet,serial,etc.).
        /// </summary>
        CommunicationMode CommMode
        {
            get;
        }

        /// <summary>
        /// Returns the string representation of the ip address if the device is connected on ethernet.
        /// </summary>
        string HostIP
        {
            get;
        }

        /// <summary>
        /// Returns the port if the device is connected on ethernet.
        /// </summary>
        string HostPort
        {
            get;
        }

        /// <summary>
        /// Returns the COM port of the reader(for serial reader)
        /// </summary>
        string COMPort
        {
            get;
        }


        /// <summary>
        /// Returns the number of bytes which can be read at a time depending upon the selected tag type. 
        /// </summary>
        int ReadPageSize
        {
            get;
        }

        /// <summary>
        /// Returns the number of bytes which can be written at a time depending upon the selected tag type. 
        /// </summary>
        int WritePageSize
        {
            get;
        }

        /// <summary>
        /// Returns maximum number of bytes which can be read/written depending upon the selected tag type. 
        /// </summary>
        int MaxTagDataBytes
        {
            get;
        }

        /// <summary>
        /// Gets/sets time out in miliseconds which is used while executing a reader command. 
        /// </summary>
        int CommandTimeout
        {
            get;
            set;
        }

        /// <summary>
        /// Returns the vendor of the reader.
        /// </summary>
        string Vendor
        {
            get;
        }

        /// <summary>
        /// Returns the model of the reader.
        /// </summary>
        string Model
        {
            get;
        }


        /// <summary>
        /// Returns name of the reader.
        /// </summary>
        string ReaderName
        {
            get;
        }


        /// <summary>
        /// Returns number of input lines supported by the reader
        /// </summary>
        byte NumberOfInputLines
        {
            get;
        }

        /// <summary>
        /// Returns number of output lines supported by the reader
        /// </summary>
        byte NumberOfOutputLines
        {
            get;
        }
        /// <summary>
        /// Returns the Mode of RFID Reader Ondemand or Auto Mode.
        /// </summary>
        RFIDReaderMode RFIDReaderMode
        {
            get;
        }
        /// <summary>
        /// get the antennanames
        /// </summary>
        List<string> AntennaNames
        {
            get;
        }

        string AssociatedTemplateName
        {
            get;
        }

        string AssociatedTemplateId
        {
            get;
            set;
        }

        string GroupId
        {
            get;
            set;
        }

        string GroupName
        {
            get;
        }
        #endregion Properties

        #region Events
        /// <summary>
        /// Event raised when the command is sent to the reader
        /// </summary>
        event EventHandler<RFIDReaderEventArgs> CommandSent;
        /// <summary>
        /// Event raised when the response is received from the reader
        /// </summary>
        event EventHandler<RFIDReaderEventArgs> ResponseReceived;

        /// <summary>
        /// Event raised when the response for continuous read is received 
        /// from the reader.
        /// </summary>
        event EventHandler<RFIDReaderEventArgs> ResponseReceivedNext;


        /// <summary>
        /// Event raised when there is an error during command execution
        /// </summary>
        event EventHandler<RFIDReaderEventArgs> ErrorOccurred;

        /// <summary>
        /// Event raised when the status of input / output pins changes.
        /// </summary>
        event EventHandler<IOPinEventArgs> IOPinStatusChanged;
        /// <summary>
        /// Event raised when the readermode changes.
        /// </summary>
        event EventHandler<ReaderModeChangedArgs> ReaderModeChanged;

        /// <summary>
        /// Event raised as soon as a tag is read by the reader.
        /// </summary>
        event EventHandler<TagReadEventArgs> OnTagRead;

        /// <summary>
        /// Event fired when raw bytes are sent by Ethernet driver 
        /// </summary>
        event EventHandler<CommDriverEventArgs> OnBytesSent;

        /// <summary>
        /// Event fired when raw bytes are received by Ethernet driver 
        /// </summary>
        event EventHandler<CommDriverEventArgs> OnBytesReceived;

        /// <summary>
        /// Event fired when raw bytes are written to tag
        /// </summary>
        event EventHandler<TagDataArgs> OnWriteTagData;

        /// <summary>
        /// Event fired when raw bytes are read from tag
        /// </summary>
        event EventHandler<TagDataArgs> OnReadTagData;

        #endregion Events
    }
}
