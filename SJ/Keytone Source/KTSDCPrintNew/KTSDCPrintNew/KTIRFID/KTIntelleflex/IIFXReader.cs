/********************************************************************************************************
Copyright (c) 2010 - 2011 KeyTone Technologies.All Right Reserved

KeyTone's source code and documentation is copyrighted and contains proprietary information.
Licensee shall not modify, create any derivative works, make modifications, improvements, 
distribute or reveal the source code ("Improvements") to anyone other than the software 
developers of licensee's organization unless the licensee has entered into a written agreement
("Agreement") to do so with KeyTone Technologies Inc. Licensee hereby assigns to KeyTone all right,
title and interest in and to such Improvements unless otherwise stated in the Agreement. Licensee 
may not resell, rent, lease, or distribute the source code alone, it shall only be distributed in 
compiled component of an application within the licensee'organization. 
The licensee shall not resell, rent, lease, or distribute the products created from the source code
in any way that would compete with KeyTone Technologies Inc. KeyTone' copyright notice may not be 
removed from the source code.
   
Licensee may be held legally responsible for any infringement of intellectual property rights that
is caused or encouraged by licensee's failure to abide by the terms of this Agreement. Licensee may 
make copies of the source code provided the copyright and trademark notices are reproduced in their 
entirety on the copy. KeyTone reserves all rights not specifically granted to Licensee. 
 
Use of this source code constitutes an agreement not to criticize, in any way, the code-writing style
of the author, including any statements regarding the extent of documentation and comments present.

THE SOFTWARE IS PROVIDED "AS IS" BASIS, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING 
BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER  LIABILITY, 
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE
OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. KEYTONE TECHNOLOGIES SHALL NOT BE LIABLE FOR ANY DAMAGES 
SUFFERED BY LICENSEE AS A RESULT OF USING, MODIFYING OR DISTRIBUTING THIS SOFTWARE OR ITS DERIVATIVES.
********************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace KTone.Core.KTIRFID
{
    /// <summary>
    /// Intelliflex Reader models supported by the SDK
    /// </summary>
    public enum ReaderModel
    {
        /// <summary>
        /// Model IBEAM Reader 500
        /// </summary>
        I_BEAM500,
    }
 
    /// <summary>
    /// Enlists connection Status of the Reader.
    /// </summary>
    public enum IFXReaderStatus
    {
        /// <summary>
        /// Indicates that reader is connected.
        /// </summary>
        ONLINE,

        /// <summary>
        /// Indicates that reader is not connected.
        /// </summary>
        OFFLINE,

        /// <summary>
        /// Indicates that the user is offline.
        /// </summary>
        USER_OFFLINE,

        /// <summary>
        /// Indicates some error has occurred while connecting to the reader. 
        /// </summary>
        ERROR
    }
    
    /// <summary>
    /// Debug information 
    /// </summary>
    [Serializable]
    public struct IFXDebug
    {
        private string m_load;
        private uint m_maxRAM;
        private uint m_maxFSSize;
        private uint m_numProcesses;
        private uint m_currentRAMUsed;
        private uint m_currentFSUsed;
        private uint m_currentCPUUsed;

        /// <summary>
        /// Constructor for Debug information 
        /// </summary>
        /// <param name="sLoad"></param>
        /// <param name="sMaxRAM"></param>
        /// <param name="sMaxFSSize"></param>
        /// <param name="sNumProcesses"></param>
        /// <param name="sCurrentRAMUsed"></param>
        /// <param name="sCurrentFSUsed"></param>
        /// <param name="sCurrentCPUUsed"></param>
        public IFXDebug(string sLoad, uint sMaxRAM, uint
            sMaxFSSize, uint sNumProcesses,
            uint sCurrentRAMUsed, uint sCurrentFSUsed, uint sCurrentCPUUsed)
        {
            m_load = sLoad;
            m_maxRAM=sMaxRAM;
            m_maxFSSize=sMaxFSSize;

            m_numProcesses=sNumProcesses;

            m_currentRAMUsed=sCurrentRAMUsed;
            m_currentFSUsed=sCurrentFSUsed;
            m_currentCPUUsed=sCurrentCPUUsed;
        }

        /// <summary>
        /// Load
        /// </summary>
        public string Load
        {
            get
            {
                return this.m_load;
            }
        }

        /// <summary>
        /// Maximum Ram
        /// </summary>
        public uint MaxRAM
        {
            get
            {
                return this.m_maxRAM;
            }
        }

        /// <summary>
        /// MaxFS Size
        /// </summary>
        public uint MaxFSSIze
        {
            get
            {
                return this.m_maxFSSize;
            }
        }

        /// <summary>
        /// NumberOfProcesses
        /// </summary>
        public uint NumberOfProcesses
        {
            get
            {
                return this.m_numProcesses;
            }
        }

        /// <summary>
        /// CurrentRAMUsed
        /// </summary>
        public uint CurrentRAMUsed
        {
            get
            {
                return this.m_currentRAMUsed;
            }
        }

        /// <summary>
        /// CurrentFSUsed
        /// </summary>
        public uint CurrentFSUsed
        {
            get
            {
                return this.m_currentFSUsed;
            }
        }

        /// <summary>
        /// CurrentCPUUsed
        /// </summary>
        public uint CurrentCPUUsed
        {
            get
            {
                return this.m_currentCPUUsed;
            }
        }
    }

    /// <summary>
    ///Exposes the parameters giving the information about the reader in detail.
    /// </summary>
    [Serializable]
    public struct IfxReaderInfo
    {
        #region Attributes
        private string firmwareVersion;
        private string deviceSerialNumber;

        private string product;
        private string version;
        private string dspVersion;
        private string hardwareVersion;
        private string name;
        private string ntpserver;
        private string ipAddress;
        private string macAdd;
        private uint[] rxAntennas;
        private uint[] txAntennas;
        private string upTime;


        private string netMask;
        private string gateWay;
        private string sysLogServer;
        private uint sysLogPort;
        private uint llrpPort;
        private uint slrrpPort;
        private uint xmlPort;
        private string logLevel;
        private string remoteLogging;
        private string dhcp;
        private Dictionary<IFXGroup, uint> groups;
        private IFXDebug debugInfo;

        private uint numberOfException;

        #endregion Attributes


        /// <summary>
        /// This structure is used store reader information
        /// </summary>
        /// <param name="firmwareVer">version of the ARM7 code</param>
        /// <param name="deviceSerialNum">reader's DeviceSerialNumber</param>
        /// <param name="productVer">product of the hardware - "I-Beam 3-2-1 Reader"</param>
        /// <param name="ver"> version of the VR code</param>
        /// <param name="dspVer">version of the DSP code</param>        
        /// <param name="hardwareVer">reader's Hardware Version</param>
        /// <param name="readerName">reader's name</param>
        /// <param name="ntpSer">reader's NTPServer </param>
        /// <param name="ipAdd">Ip Address of Reader</param>
        /// <param name="macAddress">reader's MAC Address </param>
        /// <param name="txAntennas">List of active Transmit Antennas. </param>
        /// <param name="rxAntennas">List of active Recieve Antennas</param>
        /// <param name="aUpTime">Up time</param>
        /// <param name="sNetMask"></param>
        /// <param name="sGateWay"></param>
        /// <param name="sSsLogServer"></param>
        /// <param name="sSysLogPort"></param>
        /// <param name="sLlrpPort"></param>
        /// <param name="sSlrrpPort"></param>
        /// <param name="sXmlPort"></param>
        /// <param name="sLogLevel"></param>
        /// <param name="sRemoteLogging"></param>
        /// <param name="sDhcp"></param>
        /// <param name="sGroup"></param>
        /// <param name="sNoException"></param>
        /// <param name="sDebugInfo"></param>
        public IfxReaderInfo(string firmwareVer, string deviceSerialNum,
                            string productVer, string ver,
                            string dspVer, string hardwareVer,
                            string readerName,string ntpSer,string ipAdd,        
            string macAddress,uint[] txAntennas,uint[] rxAntennas,string aUpTime,             
            string sNetMask,
            string sGateWay,
            string sSsLogServer,
            uint sSysLogPort,
            uint sLlrpPort,
            uint sSlrrpPort,
            uint sXmlPort,
            string sLogLevel,
            string sRemoteLogging,
            string sDhcp, Dictionary<IFXGroup, uint> sGroup,uint sNoException ,IFXDebug sDebugInfo)
        {
            this.firmwareVersion = firmwareVer;
            this.deviceSerialNumber = deviceSerialNum;           
            this.hardwareVersion = hardwareVer;            
            this.ntpserver = ntpSer;
            this.ipAddress = ipAdd;
            this.macAdd = macAddress;
            this.product = productVer;
            this.version = ver;
            this.dspVersion = dspVer;
            this.hardwareVersion = hardwareVer;
            this.ipAddress = ipAdd;
            this.ntpserver = ntpSer;
            this.name = readerName;
            this.txAntennas = txAntennas;
            this.rxAntennas = rxAntennas;
            this.upTime = aUpTime;

            this.netMask = sNetMask;
            this.gateWay = sGateWay;
            this.sysLogServer = sSsLogServer;
            this.sysLogPort = sSysLogPort;
            this.llrpPort = sLlrpPort;
            this.slrrpPort = sSlrrpPort;
            this.xmlPort = sXmlPort;
            this.logLevel = sLogLevel;
            this.remoteLogging = sRemoteLogging;
            this.dhcp = sDhcp;
            this.groups = sGroup;

            this.numberOfException = sNoException;

            this.debugInfo = sDebugInfo;

        }
        /// <summary>
        /// Gets string representation of structure.
        /// </summary>
        /// <returns>string representaion of structure</returns>
        public string GetString()
        {
            StringBuilder strbuilder = new StringBuilder();
            strbuilder.AppendLine("Firmware Version      : " + firmwareVersion);
            strbuilder.AppendLine("Serial No             : " + deviceSerialNumber);
            strbuilder.AppendLine("DSP Version           : " + dspVersion);
            strbuilder.AppendLine("Hardware Version      : " + hardwareVersion);
            strbuilder.AppendLine("Reader Name           : " + name);
            strbuilder.AppendLine("NTP Server            : " + ntpserver);
            strbuilder.AppendLine("IP address            : " + ipAddress);
            strbuilder.AppendLine("MAC address           : " + macAdd);
            strbuilder.Append("TX Antennas           : ");
            if (txAntennas != null)
            {
                for (int cnt = 0; cnt < txAntennas.Length; cnt++)
                {
                    strbuilder.Append(txAntennas[cnt]);
                    if (cnt + 1 < txAntennas.Length)
                        strbuilder.Append(",");
                    else
                        strbuilder.Append(".");
                }
            }
            strbuilder.AppendLine();
            strbuilder.Append("RX Antennas           : ");
            if (rxAntennas != null)
            {
                for (int cnt = 0; cnt < rxAntennas.Length; cnt++)
                {
                    strbuilder.Append(rxAntennas[cnt]);
                    if (cnt + 1 < rxAntennas.Length)
                        strbuilder.Append(",");
                    else
                        strbuilder.Append(".");
                }
            }
            strbuilder.AppendLine();
            strbuilder.Append("Up Time               : "+upTime);            
            strbuilder.AppendLine();
            return strbuilder.ToString();
        }

        #region Public ReadOnly Properties

        /// <summary>
        /// Gets the details about version of Firmware
        /// </summary>
        public string FirmwareVersion
        {
            get { return firmwareVersion; }
        }


        /// <summary>
        /// Gets the details about reader's DeviceSerialNumber
        /// </summary>
        public string DeviceSerialNumber
        {
            get { return deviceSerialNumber; }
        }

        /// <summary>
        /// Gets the details about product of the hardware - "I-Beam 500 Reader"
        /// </summary>
        public string Product
        {
            get { return product; }
        }

        /// <summary>
        /// Gets the details about version of the VR code
        /// </summary>
        public string Version
        {
            get { return version; }
        }

        /// <summary>
        /// Gets the details about version of the DSP code
        /// </summary>
        public string DSPVersion
        {
            get { return dspVersion; }
        }

        /// <summary>
        /// Gets the details about reader's Hardware Version
        /// </summary>
        public string HardwareVersion
        {
            get { return hardwareVersion; }
        }

        /// <summary>
        /// Gets the details about reader's name .
        /// </summary>
        public string ReaderName
        {
            get { return name; }
        }

        /// <summary>
        /// Gets the details about reader's NTPServer .
        /// </summary>
        public string NTPServer
        {
            get { return ntpserver; }
        }

        /// <summary>
        /// Gets the details about reader's NTPServer .
        /// </summary>
        public string IPAddress
        {
            get { return ipAddress; }
        }

        /// <summary>
        /// Gets the details about reader's MAC Address .
        /// </summary>
        public string MACAddress
        {
            get { return macAdd; }
        }

        /// <summary>
        /// Gets the List of Active Recieve Antennas Ports.
        /// </summary>
        public uint [] RXAntennas
        {
            get { return rxAntennas; }
        }
        /// <summary>
        /// Gets the List of Active Transmit Antennas Ports.
        /// </summary>
        public uint[] TXAntennas
        {
            get { return txAntennas; }
        }
        /// <summary>
        /// Gets the elapsed time since last turn ON of power.
        /// </summary>
        public string UpTime
        {
            get { return upTime; }
        }
        /// <summary>
        /// Gets the netmask.
        /// </summary>
        public string NetMask
        {
            get { return netMask; }
        }

        /// <summary>
        /// Gets the gateway
        /// </summary>
        public string Gateway
        {
            get { return gateWay; }
        }

        /// <summary>
        /// Gets the System Log Server
        /// </summary>
        public string SysLogServer
        {
            get { return sysLogServer; }
        }

        /// <summary>
        /// Gets the System Log Port
        /// </summary>
        public uint SysLogPort
        {
            get { return sysLogPort; }
        }

        /// <summary>
        /// Gets the LLRP Port number
        /// </summary>
        public uint LLRPPort
        {
            get { return llrpPort; }
        }

        /// <summary>
        /// Gets the SLRRP Port number
        /// </summary>
        public uint SLRRPPort
        {
            get { return slrrpPort; }
        }

        /// <summary>
        /// Gets the XML Port number
        /// </summary>
        public uint XMLPort
        {
            get { return xmlPort; }
        }

        /// <summary>
        /// Gets the log Level
        /// </summary>
        public string LogLevel
        {
            get { return logLevel; }
        }

        /// <summary>
        /// Gets the Remote Logging 
        /// </summary>
        public string RemoteLogging
        {
            get { return remoteLogging; }
        }

        /// <summary>
        /// Gets the dhcp address.
        /// </summary>
        public string DHCP
        {
            get { return dhcp; }
        }

        /// <summary>
        /// Gets the Group number assigned to antenna connected
        /// </summary>
        public Dictionary<IFXGroup,uint> Groups
        {
            get { return groups; }
        }


        /// <summary>
        /// Gets the number of exceptions
        /// </summary>
        public uint MumberOfExceptions
        {
            get { return numberOfException; }
        }

        /// <summary>
        /// Gets the debug Information
        /// </summary>
        public IFXDebug DebugInfo
        {
            get { return debugInfo; }
        }
        #endregion Public ReadOnly Properties



    }

    /// <summary>
    /// Exposes the parameters giving the information about the reader.
    /// </summary>
    [Serializable]
    public struct IFXReaderDiscoverResponse
    {

        private string m_ReaderName;
        private string m_ProductName;
        private string m_SerialNuber;
        private string m_IPAddress;
        private string m_Version;
        private string m_FirmWare;

        /// <summary>
        /// Reader discovery response
        /// </summary>
        /// <param name="sReaderName">Name of reader</param>
        /// <param name="sIpAddress">IP address of reader</param>
        /// <param name="sProductName">Product name</param>
        /// <param name="sSerialNuber">Serial number</param>
        /// <param name="sVersion">Version number</param>
        /// <param name="sFirmWare">firmware version</param>
        public IFXReaderDiscoverResponse(string sReaderName,string sIpAddress , string sProductName, string sSerialNuber, string sVersion, string sFirmWare)
        {
            this.m_ReaderName = sReaderName;
            this.m_ProductName = sProductName;
            this.m_SerialNuber = sSerialNuber;
            this.m_Version = sVersion;
            this.m_FirmWare = sFirmWare;
            this.m_IPAddress = sIpAddress;
        }

        /// <summary>
        /// Reader discovery response
        /// </summary>
        /// <param name="sReaderName">Name of reader</param>
        /// <param name="sProductName">Product name</param>
        /// <param name="sSerialNuber">Serial number</param>
        /// <param name="sVersion">Version number</param>
        /// <param name="sFirmWare">firmware version</param>
        public IFXReaderDiscoverResponse(string sReaderName, string sProductName, string sSerialNuber, string sVersion, string sFirmWare)
        {
            this.m_ReaderName = sReaderName;
            this.m_ProductName = sProductName;
            this.m_SerialNuber = sSerialNuber;
            this.m_Version = sVersion;
            this.m_FirmWare = sFirmWare;
            this.m_IPAddress = "";
        }

        /// <summary>
        /// Gets reader name
        /// </summary>
        public string ReaderName
        {
            get
            {
                return m_ReaderName;
            }             
        }

        /// <summary>
        /// Gets IP address of reader
        /// </summary>
        public string IPAddress
        {
            get
            {
                return m_IPAddress;
            }
        }

        /// <summary>
        /// Gets version number
        /// </summary>
        public string Version
        {
            get
            {
                return m_Version;
            }
        }

        /// <summary>
        /// Gets serial number
        /// </summary>
        public string SerialNumber
        {
            get
            {
                return m_SerialNuber;
            }
        }

        /// <summary>
        /// Gets firmware version
        /// </summary>
        public string Firmware
        {
            get
            {
                return m_FirmWare;
            }
        }


        /// <summary>
        /// Gets product name
        /// </summary>
        public string ProductName
        {
            get
            {
                return m_ProductName;
            }
        }


    }


    /// <summary>
    /// Group numbers assigned to antenna connected.
    /// </summary>
    public enum IFXGroup
    {
        /// <summary>
        /// Group 1
        /// </summary>
        group1,
        /// <summary>
        /// Group 2
        /// </summary>
        group2,
        /// <summary>
        /// Group 3
        /// </summary>
        group3,
        /// <summary>
        /// Group 4
        /// </summary>
        group4,
        /// <summary>
        /// Group 5
        /// </summary>       
        group5,
        /// <summary>
        /// Group 6
        /// </summary>
        group6,
        /// <summary>
        /// Group 7
        /// </summary>
        group7,
        /// <summary>
        /// Group 8
        /// </summary>
        group8
    }

    /// <summary>
    /// Command Types 
    /// </summary>
    public enum IfxCommandTypes
    {
        /// <summary>
        /// Syncronous commands having immediate response.
        /// </summary>
        SYNCHRONOUS = 1,
        /// <summary>
        /// Asyncronous commands 
        /// </summary>
        ASYNCHRONOUS,

        /// <summary>
        /// Commands which don't have response 
        /// </summary>
        NO_RESPONSE
    }

    /// <summary>
    /// Exposes methods and events to communicate with Intelleflex reader.
    /// Exposes properties to control Intelleflex reader.
    /// </summary>
    
    public interface IIFXReader:IDisposable
    {

        #region Events
        /// <summary>
        /// Event raised after a tag is read
        /// </summary>
        event EventHandler<IFXReaderEventArgs> TagReadEvent;
        /// <summary>
        /// Event raised after Active Transmit/Recieve Antenna Ports changed .
        /// </summary>
        event EventHandler<IFXReaderEventArgs> AntennaStatusEvent;

        /// <summary>
        /// Event raised after reader status is changed.
        /// </summary>
        event EventHandler<IFXReaderEventArgs> StatusMonitorEvent;

        /// <summary>
        /// Event raised after inventory ends.
        /// </summary>
        event EventHandler<IFXReaderEventArgs> EndInventoryEvent;
        #endregion Events

        #region Properties

        #region Reader

        /// <summary>
        /// Gets reader model
        /// </summary>
        ReaderModel ReaderModel
        {
            get;
        }

        /// <summary>
        /// Gets or Sets reader name
        /// </summary>
        string IfxReaderName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets reader description
        /// </summary> 
        string ReaderDescription
        {
            get;
        }

        /// <summary>
        /// Gets IP address of the reader 
        /// </summary> 
        string IpAddress
        {
            get;
        }

        /// <summary>
        /// Get the current Status of the reader 
        /// Intelliflex Reader Status can be : ONLINE,OFFLINE,ERROR
        /// </summary>
        IFXReaderStatus ReaderStatus
        {
            get;
        }
        /// <summary>
        /// Gets whether reader is connected.
        /// </summary>
        bool IsConnected
        {
            get;
        }

        //OKK 2June2007 begin
        //Added to get inventory status.
        /// <summary>
        /// Gets wheather Inventory of tags is started.
        /// </summary>
        bool IsInventoryStarted
        {
            get;
        }
        //OKK 2June2007 End

        #endregion Reader

        #region Antenna
        /// <summary>
        /// Get the count of Antenna (Sources) that can be addressed
        /// </summary>
        int NoOfAntenna
        {
            get;
        }
        /// <summary>
        /// Get or Sets Antenna Configurations for all the antennas. 
        /// AntennaConfig includes  antennaName,tagTypeSupported,
        /// txPower,rxPower,isConnected;
        /// </summary>
        AntennaConfig[] AntennaConfiguration
        {
            get;
            set;
        }
        /// <summary>
        /// Gets the antenna names.
        /// </summary>
        string[] AntennaNames
        {
            get;
        }


        /// <summary>
        /// Gets Active Tx Antenna
        /// </summary>
        List<uint> ActiveTxAntenna
        {
            get;
        }

        /// <summary>
        /// Gets Active Rx Antenna
        /// </summary>
        List<uint> ActiveRxAntenna
        {
            get;
        }

        /// <summary>
        /// Gets Firmware Version of Reader
        /// </summary>
        string FirmwareVersion
        {
            get;
        }

        #endregion Antenna

        #endregion Properties

        #region Transport
        /// <summary>
        /// This method with connect to reader
        /// </summary>
        void Connect();
        /// <summary>
        /// This method with disconnect the reader
        /// </summary>
        void Disconnect();
        #endregion Transport

        #region Antenna Commands

        /// <summary>
        /// Returns active Recieve/Transmit antenna ports.
        /// </summary>
        /// <param name="txAntennaPorts">Transmit antenna ports.</param>
        /// <param name="rxAntennaPorts">Recieve antenna ports.</param>
        void GetActiveAntennaPorts(out uint[] txAntennaPorts, out uint[] rxAntennaPorts);

        /// <summary>
        /// Returns active Recieve/Transmit antenna ports.
        /// </summary>
        /// <param name="txAntennaPorts">Transmit antenna ports.</param>
        /// <param name="rxAntennaPorts">Recieve antenna ports.</param>
        /// <param name="timeoutMS">TimeOut will occur after specified milliseconds.</param>
        /// <param name="cmdStr">Command sent to reader.</param>
        /// <param name="rspStr">Response of the reader.</param>
        /// <param name="errorCode">Error occured during command execution.</param>
        void GetActiveAntennaPorts(out uint[] txAntennaPorts, out uint[] rxAntennaPorts,
            int timeoutMS, out string cmdStr, out string rspStr, out IFXReaderErrors errorCode);

        /// <summary>
        /// Sets active Recieve/Transmit antenna ports.
        /// </summary>
        /// <param name="txAntennaPorts">Transmit antenna ports.</param>
        /// <param name="rxAntennaPorts">Recieve antenna ports.</param>
        void SetActiveAntennaPorts(uint[] txAntennaPorts, uint[] rxAntennaPorts);

        /// <summary>
        /// Sets active Recieve/Transmit antenna ports.
        /// </summary>
        /// <param name="txAntennaPorts">Transmit antenna ports.</param>
        /// <param name="rxAntennaPorts">Recieve antenna ports.</param>
        /// <param name="timeoutMS">TimeOut will occur after specified milliseconds.</param>
        /// <param name="cmdStr">Command sent to reader.</param>
        /// <param name="rspStr">Response of the reader.</param>
        /// <param name="error">Error occured during command execution.</param>
        void SetActiveAntennaPorts(uint[] txAntennaPorts, uint[] rxAntennaPorts,
            int timeoutMS, out string cmdStr, out string rspStr, out Dictionary<IFXReaderErrors, string> error);
        #endregion Antenna Commands

        #region Tag Commands
        /// <summary>
        /// Gets Tag Ids from ALL Configured Antennas .
        /// </summary>        
        /// <returns>tag list</returns>
        IRFIDIFXTag[] FindTags();
        /// <summary>
        ///  Gets Tag Ids from ALL Configured Antennas.
        /// </summary>
        /// <param name="timeoutMS">TimeOut will occur after specified milliseconds.</param>
        /// <param name="trigger">Trigger Parameter</param>
        /// <param name="cmdStr">Command sent to reader.</param>
        /// <param name="rspStr">Response of the reader.</param>
        /// <param name="error">Error occured during command execution.</param>
        /// <returns>Tag list</returns>
        IRFIDIFXTag[] FindTags(int timeoutMS, IfxTrigger trigger, out string cmdStr, out string rspStr,
            out Dictionary<IFXReaderErrors, string> error);

        /// <summary>
        /// Gets Tag IDs from all configured antennas
        /// </summary>
        /// <param name="timeoutMS"></param>
        /// <param name="opCode"></param>
        /// <param name="cmdStr"></param>
        /// <param name="rspStr"></param>
        /// <param name="error"></param>
        /// <returns>Tag list</returns>
        IRFIDIFXTag[] FindTags(int timeoutMS, IFXCommandOpCode opCode, out string cmdStr, out string rspStr,
            out Dictionary<IFXReaderErrors, string> error);


        /// <summary>
        /// Gets  Tag Ids from Specifed Antennas.
        /// </summary>        
        /// <param name="antennaNames">list of antennas on which tags will be searched</param>
        /// <returns>Tag list</returns>
        IRFIDIFXTag[] FindTags(string[] antennaNames);
        /// <summary>
        /// Gets  Tag Ids from Specifed Antennas.
        /// </summary>
        /// <param name="antennaNames">Names of Antenna to be searched.</param>
        /// <param name="timeoutMS">TimeOut will occur after specified milliseconds.</param>
        /// <param name="cmdStr">Command sent to reader.</param>
        /// <param name="rspStr">Response of the reader.</param>
        /// <param name="error">Error occured during command execution.</param>
        /// <returns>Tag list</returns>
        IRFIDIFXTag[] FindTags(string[] antennaNames,
            int timeoutMS, out string cmdStr, out string rspStr, out Dictionary<IFXReaderErrors, string> error);
        /// <summary>
        /// Gets Tag Ids from ALL Antennas with specified TagSelector.
        /// </summary>       
        /// <param name="selector">Tag Selection Criteria</param>
        /// <returns>Tag list</returns>
        IRFIDIFXTag[] FindTags(TagSelector selector);
        /// <summary>
        ///  Gets Tag Ids from ALL Antennas with specified TagSelector.
        /// </summary>
        /// <param name="selector">Tag Selection Criteria</param>
        /// <param name="timeoutMS">TimeOut will occur after specified milliseconds.</param>
        /// <param name="cmdStr">Command sent to reader.</param>
        /// <param name="rspStr">Response of the reader.</param>
        /// <param name="error">Error occured during command execution.</param>
        /// <returns>Tag list</returns>
        IRFIDIFXTag[] FindTags(TagSelector selector,
            int timeoutMS, out string cmdStr, out string rspStr, out Dictionary<IFXReaderErrors, string> error);
        /// <summary>
        /// Gets Tag Ids for specified TagSelector and Antennas
        /// </summary>        
        /// <param name="selector">Tag Selection Criteria</param>
        /// <param name="antennaNames">list of antennas on which tags will be searched</param>
        /// <returns>Tag list</returns>
        IRFIDIFXTag[] FindTags(TagSelector selector, string[] antennaNames);
        /// <summary>
        /// Gets Tag Ids for  specified TagSelector and Antennas.
        /// </summary>
        /// <param name="selector">Tag Selection Criteria</param>
        /// <param name="antennaNames">>Names of Antenna to be searched.</param>
        /// <param name="timeoutMS">TimeOut will occur after specified milliseconds.</param>
        /// <param name="cmdStr">Command sent to reader.</param>
        /// <param name="rspStr">Response of the reader.</param>
        /// <param name="error">Error occured during command execution.</param>
        /// <returns>Tag list</returns>
        IRFIDIFXTag[] FindTags(TagSelector selector, string[] antennaNames,
            int timeoutMS, out string cmdStr, out string rspStr, out Dictionary<IFXReaderErrors, string> error);

        /// <summary>
        /// Writes EPC Tag ID on Tag.
        /// </summary>
        /// <param name="epcTagId">Tag ID to be written</param>  
        /// <param name="antennaName">Antenna name where write operation is to be performed</param>
        void WriteTagId(EpcId epcTagId, string antennaName);

        /// <summary>
        /// Writes EPC Tag ID on Tag.
        /// </summary>
        /// <param name="epcTagId">Tag ID to be written</param>  
        /// <param name="antennaName">Antenna name where write operation is to be performed</param>
        /// <param name="timeoutMS">TimeOut will occur after specified milliseconds.</param>
        /// <param name="cmdStr">Command sent to reader.</param>
        /// <param name="rspStr">Response of the reader.</param>
        /// <param name="error">Error occured during command execution</param>
        void WriteTagId(EpcId epcTagId, string antennaName,
            int timeoutMS, out string cmdStr, out string rspStr, out Dictionary<IFXReaderErrors, string> error);

        /// <summary>
        /// Starts Inventory of Tags.
        /// </summary>
        ///// <param name="cmdStr">Command sent to reader.</param>
        ///// <param name="rspStr">Response of the reader.</param>
        void StartInventory();//OKKout string cmdStr,out string rspStr)

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trigger"></param>
        void StartInventory(IfxTrigger trigger);
        /// <summary>
        /// Stops Inventory of Tags.
        /// </summary>
        IRFIDIFXTag[] StopInventory(/*out string cmdStr, out string rspStr*/);

        #endregion Tag Commands

        #region Tag Data Commands
        /// <summary>
        /// Reads Tag Data
        /// </summary>
        /// <param name="epcId">EPC ID</param>
        /// <param name="dataSpec">Array of Tag Memory Area to be Read.</param>
        /// <returns>TagDataInfo</returns>
        TagDataInfo[] ReadTagData(EpcId epcId, TagDataSpec[] dataSpec);


        /// <summary>
        /// Reads Tag Data 
        /// </summary>
        /// <param name="epcId">EPC ID</param>
        /// <param name="dataSpec">Array of Tag Memory Area to be Read.</param>
        /// <param name="timeoutMS">TimeOut will occur after specified milliseconds.</param>
        /// <param name="cmdStr">Command sent to reader.</param>
        /// <param name="rspStr">Response of the reader.</param>
        /// <param name="error">Error occured during command execution.</param>
        /// <returns>TagDataInfo</returns>
        TagDataInfo[] ReadTagData(EpcId epcId, TagDataSpec[] dataSpec,
            int timeoutMS, out string cmdStr, out string rspStr, out Dictionary<IFXReaderErrors, string> error);

        /// <summary>
        /// Reads Tag Data
        /// </summary>
        /// <param name="epcId">EPC ID</param>
        /// <param name="dataSpec">Array of Tag Memory Area to be Read.</param>
        /// <param name="antennaName">Antenna</param>
        /// <returns>TagDataInfo</returns>
        TagDataInfo[] ReadTagData(EpcId epcId, TagDataSpec[] dataSpec, string antennaName);

        /// <summary>
        /// Reads Tag Data
        /// </summary>
        /// <param name="epcId">EPC ID</param>
        /// <param name="dataSpec">Array of Tag Memory Area to be Read.</param>
        /// <param name="antennaName">Antenna</param>
        /// <param name="timeoutMS">TimeOut will occur after specified milliseconds.</param>
        /// <param name="cmdStr">Command sent to reader.</param>
        /// <param name="rspStr">Response of the reader.</param>
        /// <param name="error">Error occured during command execution.</param>
        /// <returns>TagDataInfo</returns>
        TagDataInfo[] ReadTagData(EpcId epcId, TagDataSpec[] dataSpec, string antennaName,
            int timeoutMS, out string cmdStr, out string rspStr, out Dictionary<IFXReaderErrors, string> error);

        /// <summary>
        /// Reads Tag Data
        /// </summary>
        /// <param name="epcId">EPC ID</param>
        /// <param name="dataSpec">Array of Tag Memory Area to be Read.</param>
        /// <param name="selector">Tag Selection Criteria</param>
        /// <param name="antennaName">Antenna</param>
        /// <param name="timeoutMS">TimeOut will occur after specified milliseconds.</param>
        /// <param name="cmdStr">Command sent to reader.</param>
        /// <param name="rspStr">Response of the reader.</param>
        /// <param name="error">Error occured during command execution.</param>
        /// <returns>TagDataInfo</returns>
        TagDataInfo[] ReadTagData(EpcId epcId, TagDataSpec[] dataSpec, TagSelector selector, string antennaName,
                   int timeoutMS, out string cmdStr, out string rspStr, out Dictionary<IFXReaderErrors, string> error);
        /// <summary>
        /// Writes Data on Specifed Tag Memory Area and Antenna.
        /// </summary>
        /// <param name="dataInfo">Tag Memory Area and Data to be written</param>
        /// <param name="antennaName">Antenna name where write operation is to be performed</param>
        void WriteTagData(TagDataInfo[] dataInfo, string antennaName);

        /// <summary>
        /// Writes Data on Specifed Tag Memory Area and Antenna.
        /// </summary>
        /// <param name="dataInfo">Tag Memory Area and Data to be written</param>
        /// <param name="antennaName">Antenna name where write operation is to be performed</param>
        /// <param name="timeoutMS">TimeOut will occur after specified milliseconds.</param>
        /// <param name="cmdStr">Command sent to reader.</param>
        /// <param name="rspStr">Response of the reader.</param>
        /// <param name="error">Error occured during command execution.</param>
        void WriteTagData(TagDataInfo[] dataInfo, string antennaName,
            int timeoutMS, out string cmdStr, out string rspStr, out Dictionary<IFXReaderErrors, string> error);

        /// <summary>
        /// Writes Data on Specifed Tag Memory Area ,Antenna and TagSelectior.
        /// </summary>
        /// <param name="dataInfo">Tag Memory Area and Data to be written</param>
        /// <param name="selector">Tag Selection Criteria</param>
        /// <param name="antennaName">Antenna name where write operation is to be performed</param>
        void WriteTagData(TagDataInfo[] dataInfo, TagSelector selector, string antennaName);

        /// <summary>
        /// Writes Data on Specifed Tag Memory Area ,Antenna and TagSelectior.
        /// </summary>
        /// <param name="dataInfo">Tag Memory Area and Data to be written</param>
        /// <param name="selector">Tag Selection Criteria</param>
        /// <param name="timeoutMS">TimeOut will occur after specified milliseconds.</param>
        /// <param name="cmdStr">Command sent to reader.</param>
        /// <param name="rspStr">Response of the reader.</param>
        /// <param name="error">Error occured during command execution.</param>
        /// <param name="antennaName">Antenna name where write operation is to be performed</param>
        void WriteTagData(TagDataInfo[] dataInfo, TagSelector selector, string antennaName,
            int timeoutMS, out string cmdStr, out string rspStr, out Dictionary<IFXReaderErrors, string> error);

        /// <summary>
        /// Writes Data on Specifed Tag Memory Area ,Antenna and EPC.
        /// </summary>
        /// <param name="dataInfo">Tag Memory Area and Data to be written</param>
        /// <param name="epcId">EPC ID</param>              
        void WriteTagData(TagDataInfo[] dataInfo, EpcId epcId);


        /// <summary>
        /// Writes Data on Specifed Tag Memory Area ,EPC and  default Antenna.
        /// </summary>
        /// <param name="dataInfo">Tag Memory Area and Data to be written</param>
        /// <param name="epcId">EPC ID</param>
        /// <param name="timeoutMS">TimeOut will occur after specified milliseconds.</param>
        /// <param name="cmdStr">Command sent to reader.</param>
        /// <param name="rspStr">Response of the reader.</param>
        /// <param name="error">Error occured during command execution.</param>        
        void WriteTagData(TagDataInfo[] dataInfo, EpcId epcId,
            int timeoutMS, out string cmdStr, out string rspStr, out Dictionary<IFXReaderErrors, string> error);


        /// <summary>
        /// Writes Data on Specifed Tag Memory Area ,Antenna and EPC.
        /// </summary>
        /// <param name="dataInfo">Tag Memory Area and Data to be written</param>
        /// <param name="epcId">EPC ID</param>
        /// <param name="timeoutMS">TimeOut will occur after specified milliseconds.</param>
        /// <param name="cmdStr">Command sent to reader.</param>
        /// <param name="rspStr">Response of the reader.</param>
        /// <param name="error">Error occured during command execution.</param>
        /// <param name="antennaName">Antenna name where write operation is to be performed</param>
        void WriteTagData(TagDataInfo[] dataInfo, EpcId epcId, string antennaName,
            int timeoutMS, out string cmdStr, out string rspStr, out Dictionary<IFXReaderErrors, string> error);

        #endregion Tag Data Commands


        #region Memory Commands

        /// <summary>
        /// Gets memory locks for a block
        /// </summary>
        /// <param name="epcId">Tag Id</param>
        /// <param name="dataSpec">Tag Data Specification</param>
        /// <param name="antennaName">Antenna name where write operation is to be performed</param>
        /// <param name="timeoutMS">Time Out</param>
        /// <param name="cmdStr">Command String</param>
        /// <param name="rspStr">Response String</param>
        /// <param name="error">Error code if error orrurs during command execution</param>
        /// <returns>Memory Locks</returns>
        MBLocks GetMemoryLocks(EpcId epcId, TagDataSpec[] dataSpec, string antennaName,
            int timeoutMS, out string cmdStr, out string rspStr, out Dictionary<IFXReaderErrors, string> error);

        /// <summary>
        /// Gets memory locks for a block
        /// </summary>
        /// <param name="epcId">Tag Id</param>
        /// <param name="dataSpec">Tag Data Specification</param>
        /// <param name="selector">Tag selector</param>
        /// <param name="antennaName">Antenna name where write operation is to be performed</param>
        /// <param name="timeoutMS">Time Out</param>
        /// <param name="cmdStr">Command String</param>
        /// <param name="rspStr">Response String</param>
        /// <param name="error">Error code if error orrurs during command execution</param>
        /// <returns>Memory Locks</returns>
        MBLocks GetMemoryLocks(EpcId epcId, TagDataSpec[] dataSpec, TagSelector selector, string antennaName,
            int timeoutMS, out string cmdStr, out string rspStr, out Dictionary<IFXReaderErrors, string> error);

        /// <summary>
        /// Set Memory Locks for a block
        /// </summary>
        /// <param name="epcId">Tag Id</param>
        /// <param name="dataSpec">Tag Data Specification</param>
        /// <param name="mbLocks">Locks to be set</param>
        /// <param name="antennaName">Antenna name where write operation is to be performed</param>
        /// <param name="timeoutMS">Time Out</param>
        /// <param name="cmdStr">Command String</param>
        /// <param name="rspStr">Reesponse String</param>
        /// <param name="error">Error code if error orrurs during command execution</param>
        void SetMemoryLocks(EpcId epcId, TagDataSpec[] dataSpec, MBLocks mbLocks, string antennaName,
          int timeoutMS, out string cmdStr, out string rspStr, out Dictionary<IFXReaderErrors, string> error);

        /// <summary>
        /// Set Memory Locks for a block
        /// </summary>
        /// <param name="epcId">Tag Id</param>
        /// <param name="dataSpec">Tag Data Specification</param>
        /// <param name="selector">Tag selector</param>
        /// <param name="mbLocks">Locks to be set</param>
        /// <param name="antennaName">Antenna name where write operation is to be performed</param>
        /// <param name="timeoutMS">Time Out</param>
        /// <param name="cmdStr">Command String</param>
        /// <param name="rspStr">Reesponse String</param>
        /// <param name="error">Error code if error orrurs during command execution</param>
        void SetMemoryLocks(EpcId epcId, TagDataSpec[] dataSpec, TagSelector selector, MBLocks mbLocks, string antennaName,
            int timeoutMS, out string cmdStr, out string rspStr, out Dictionary<IFXReaderErrors, string> error);

        /// <summary>
        /// Set Memory Lock for a block
        /// </summary>
        /// <param name="epcId">Tag Id</param>
        /// <param name="dataSpec">Tag Data Specification</param>        
        /// <param name="antennaName">Antenna name where write operation is to be performed</param>
        /// <param name="timeoutMS">Time Out</param>
        /// <param name="lockType">Type of lock to be set - Read/Write/Parma</param>
        void SetMemoryLock(EpcId epcId, TagDataSpec[] dataSpec, string antennaName,
           int timeoutMS, IFXLockType lockType);

        /// <summary>
        /// Set Memory Lock for a block
        /// </summary>
        /// <param name="epcId">Tag Id</param>
        /// <param name="dataSpec">Tag Data Specification</param>
        /// <param name="selector">Tag selector</param>
        /// <param name="antennaName">Antenna name where write operation is to be performed</param>
        /// <param name="timeoutMS">Time Out</param>
        /// <param name="lockType">Type of lock to be set - Read/Write/Parma</param>
        void SetMemoryLock(EpcId epcId, TagDataSpec[] dataSpec, TagSelector selector, string antennaName,
           int timeoutMS, IFXLockType lockType);

        /// <summary>
        /// Reset Memory Lock for a block
        /// </summary>
        /// <param name="epcId">Tag Id</param>
        /// <param name="dataSpec">Tag Data Specification</param>        
        /// <param name="antennaName">Antenna name where write operation is to be performed</param>
        /// <param name="timeoutMS">Time Out</param>
        /// <param name="lockType">Type of lock to be set - Read/Write/Parma</param>
        void ResetMemoryLock(EpcId epcId, TagDataSpec[] dataSpec, string antennaName,
           int timeoutMS, IFXLockType lockType);

        /// <summary>
        /// Reset Memory Lock for a block
        /// </summary>
        /// <param name="epcId">Tag Id</param>
        /// <param name="dataSpec">Tag Data Specification</param>
        /// <param name="selector">Tag selector</param>
        /// <param name="antennaName">Antenna name where write operation is to be performed</param>
        /// <param name="timeoutMS">Time Out</param>
        /// <param name="lockType">Type of lock to be set - Read/Write/Parma</param>
        void ResetMemoryLock(EpcId epcId, TagDataSpec[] dataSpec, TagSelector selector, string antennaName,
           int timeoutMS, IFXLockType lockType);

        /// <summary>
        /// Change password for a block
        /// </summary>
        /// <param name="epcId">Tag Id</param>
        /// <param name="dataSpec">Tag Data Specification</param>        
        /// <param name="antennaName">Antenna name where write operation is to be performed</param>
        /// <param name="timeoutMS">Time Out</param>
        /// <param name="lockType">Type of lock to be set - Read/Write/Parma</param>
        /// <param name="newPassword">New password that needs to be set</param>
        void ChangeMemoryLockPassword(EpcId epcId, TagDataSpec[] dataSpec, string antennaName,
           int timeoutMS, IFXLockType lockType, string newPassword);

        /// <summary>
        /// Change password for a block
        /// </summary>
        /// <param name="epcId">Tag Id</param>
        /// <param name="dataSpec">Tag Data Specification</param>
        /// <param name="selector">Tag selector</param>
        /// <param name="antennaName">Antenna name where write operation is to be performed</param>
        /// <param name="timeoutMS">Time Out</param>
        /// <param name="lockType">Type of lock to be set - Read/Write/Parma</param>
        /// <param name="newPassword">New password that needs to be set</param>
        void ChangeMemoryLockPassword(EpcId epcId, TagDataSpec[] dataSpec, TagSelector selector, string antennaName,
           int timeoutMS, IFXLockType lockType, string newPassword);

        /// <summary>
        /// Get memory lock for a block
        /// </summary>
        /// <param name="epcId">Tag Id</param>
        /// <param name="dataSpec">Tag Data Specification</param>        
        /// <param name="antennaName">Antenna name where write operation is to be performed</param>
        /// <param name="timeoutMS">Time Out</param>
        /// <param name="lockType">Type of lock to be set - Read/Write/Parma</param>
        /// <returns>Memory Locks</returns>
        bool GetMemoryLock(EpcId epcId, TagDataSpec[] dataSpec, string antennaName,
           int timeoutMS, IFXLockType lockType);

        /// <summary>
        /// Get memory lock for a block
        /// </summary>
        /// <param name="epcId">Tag Id</param>
        /// <param name="dataSpec">Tag Data Specification</param>
        /// <param name="selector">Tag selector</param>
        /// <param name="antennaName">Antenna name where write operation is to be performed</param>
        /// <param name="timeoutMS">Time Out</param>
        /// <param name="lockType">Type of lock to be set - Read/Write/Parma</param>
        /// <returns>Memory Locks</returns>
        bool GetMemoryLock(EpcId epcId, TagDataSpec[] dataSpec, TagSelector selector, string antennaName,
           int timeoutMS, IFXLockType lockType);
        #endregion

        #region Reader Information
        /// <summary>
        /// Gets Reader information
        /// </summary>
        /// <returns>Structure containing Reader Information</returns>
        IfxReaderInfo GetReaderInfo();
        /// <summary>
        /// Gets Reader information
        /// </summary>
        /// <param name="timeoutMS">TimeOut will occur after specified milliseconds.</param>
        /// <param name="cmdStr">Command sent to reader.</param>
        /// <param name="rspStr">Response of the reader.</param>
        /// <param name="errorCode">Error occured during command execution.</param>
        /// <returns>Structure containing Reader Information</returns>
        IfxReaderInfo GetReaderInfo(int timeoutMS, out string cmdStr, out string rspStr, out IFXReaderErrors errorCode);

        #endregion Reader Information

        #region Discover

        /// <summary>
        /// Discovers reader
        /// </summary>
        /// <param name="timeoutMS">TimeOut will occur after specified milliseconds.</param>
        /// <param name="cmdStr">Command sent to reader.</param>
        /// <param name="rspStr">Response of the reader.</param>
        /// <param name="error">Error occured during command execution.</param>
        /// <returns></returns>
        List<IFXReaderDiscoverResponse> DiscoverReaders(int timeoutMS, out string cmdStr, out string rspStr, out Dictionary<IFXReaderErrors, string> error);

        #endregion

        #region GPIO

        /// <summary>
        /// Set GPIO pins status
        /// </summary>
        /// <param name="gpioPins">Structure of Pins to set or reset</param>
        /// <param name="timeoutMS">Time Out</param>
        /// <param name="cmdStr">Command sent to reader.</param>
        /// <param name="rspStr">Response from the reader.</param>
        /// <param name="error">Error code if error occured during command execution.</param>
        void SetGPIO(GPIOPins gpioPins, int timeoutMS, out string cmdStr, out string rspStr, out Dictionary<IFXReaderErrors, string> error);


        #endregion

        #region Power

        void SetPower(uint power);

        uint GetPower();        

        #endregion power 

    }    
}
