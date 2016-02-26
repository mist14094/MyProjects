using System;
using System.Collections.Generic;
using System.Text;

namespace KTone.Core.KTIRFID
{
    public interface IWTReaderEthernet
    {
        #region Properties
        /// <summary>
        /// Returns the string representation of the ip address on which the reader is connectd
        /// </summary>
        string HostIP
        {
            get;
        }

        /// <summary>
        /// Returns the string representation of the port on which the reader is connectd
        /// </summary>
        string HostPort
        {
            get;
        }
        #endregion Properties

        /// <summary>
        /// The function of this command is to assign the Network ID as well as commit it to the Data EEPROM of
        /// the reader.
        /// </summary>
        /// <param name="newNetworkId">New Network Id</param>
        /// <example>
        /// <code>
        /// 	void ExecuteSetNetworkID(IWTRFReader paramReader)
        ///		{
        ///			byte newNetworkID =1;
        ///			paramReader.SetNetworkID(newNetworkID);
        ///		}
        /// </code>
        ///  </example>
        void SetNetworkID(byte newNetworkId);
        /// <summary>
        /// The function of this command is to assign the Network ID as well as commit it to the Data EEPROM of
        /// the reader.
        /// </summary>
        /// <param name="newNetworkId">New Network Id</param>
        /// <param name="timeout">Time Out</param>
        /// <example>
        /// <code>
        /// 	void ExecuteSetNetworkID(IWTRFReader paramReader)
        ///		{
        ///			byte newNetworkID =1;
        ///			int timeout =5000;
        ///			paramReader.SetNetworkID(newNetworkID,timeout);
        ///		}
        /// </code>
        ///  </example>
        void SetNetworkID(byte newNetworkId, int timeout);
        /// <summary>
        /// The function of this command is to assign the Network ID as well as commit it to the Data EEPROM of
        /// the reader.
        /// </summary>
        /// <param name="newNetworkId">New Network Id</param>
        /// <param name="timeout">Time Out</param>
        /// <param name="cmdByteArray">Command Byte Array</param>
        /// <param name="rspByteArray">Response from Reader</param>
        /// <example>
        /// <code>
        /// 	void ExecuteSetNetworkID(IWTRFReader paramReader)
        ///		{
        ///			byte newNetworkID =1;
        ///			int timeout =5000;
        ///			byte[] cmdByteArray = null;
        ///			byte[] rspByteArray = null;
        ///			paramReader.SetNetworkID(newNetworkID,timeout,out cmdByteArray, out rspByteArray);
        ///		}
        /// </code>
        /// </example>
        void SetNetworkID(byte newNetworkId, int timeout,
                            out byte[] cmdByteArray, out byte[] rspByteArray);



        /// <summary>
        /// The function of this command is to clear the current buffers within the reader. The reader has a 3 level
        /// FIFO buffer which can store 3 separate tag data packets.
        /// </summary>
        /// <example>
        /// <code>
        /// 	void ExecuteClearbuffers(IWTRFReader paramReader)
        ///		{
        ///			paramReader.Clearbuffers();
        ///		}
        /// </code>
        /// </example>
        void Clearbuffers();
        /// <summary>
        /// The function of this command is to clear the current buffers within the reader. The reader has a 3 level
        /// FIFO buffer which can store 3 separate tag data packets.
        /// </summary>
        /// <param name="timeout">Time Out</param>
        /// <example>
        /// <code>
        /// 	void ExecuteClearbuffers(IWTRFReader paramReader)
        ///		{
        ///			int timeout =5000;	
        ///			paramReader.Clearbuffers(timeout);
        ///		}
        /// </code>
        /// </example>
        void Clearbuffers(int timeout);
        /// <summary>
        /// The function of this command is to clear the current buffers within the reader. The reader has a 3 level
        /// FIFO buffer which can store 3 separate tag data packets.
        /// </summary>
        /// <param name="timeout">Time Out</param>
        /// <param name="cmdByteArray">Command Byte Array</param>
        /// <param name="rspByteArray">Response from Reader</param>
        /// <example>
        /// <code>
        /// 	void ExecuteClearbuffers(IWTRFReader paramReader)
        ///		{
        ///			int timeout =5000;	
        ///			byte[] cmdByteArray = null;
        ///			byte[] rspByteArray = null;
        ///			paramReader.Clearbuffers(timeout,cmdByteArray,rspByteArray);
        ///		}
        /// </code>
        /// </example>
        void Clearbuffers(int timeout, out byte[] cmdByteArray, out byte[] rspByteArray);

        /// <summary>
        /// The function of this command is to reset the reader to all factory default settings.
        /// These settings are stored on the reader level, it is remembered once a power reset
        /// has been done. Settings include Polling ,Sitecode , RSSI, Filter etc.
        /// </summary>
        /// <example>
        /// <code>
        /// 	void ExecuteReInitialise(IWTRFReader paramReader)
        ///		{
        ///			int timeout =5000;	
        ///			byte[] cmdByteArray = null;
        ///			byte[] rspByteArray = null;
        ///			paramReader.ReInitialise(timeout,cmdByteArray,rspByteArray);
        ///		}
        /// </code>
        /// </example>
        void ReInitialise();
        /// <summary>
        /// The function of this command is to reset the reader to all factory default settings.
        /// These settings are stored on the reader level, it is remembered once a power reset
        /// has been done. Settings include Polling ,Sitecode , RSSI, Filter etc.
        /// </summary>
        /// <param name="timeout">Time Out</param>
        /// <example>
        /// <code>
        /// 	void ExecuteReInitialise(IWTRFReader paramReader)
        ///		{
        ///			int timeout =5000;	
        ///			paramReader.ReInitialise(timeout);
        ///		}
        /// </code>
        /// </example>
        void ReInitialise(int timeout);
        /// <summary>
        /// The function of this command is to reset the reader to all factory default settings.
        /// These settings are stored on the reader level, it is remembered once a power reset
        /// has been done. Settings include Polling ,Sitecode , RSSI, Filter etc.
        /// </summary>
        /// <param name="timeout">Time Out</param>
        /// <param name="cmdByteArray">Command Byte Array</param>
        /// <param name="rspByteArray">Response from Reader</param>
        /// <example>
        /// <code>
        /// 	void ExecuteReInitialise(IWTRFReader paramReader)
        ///		{
        ///			int timeout =5000;	
        ///			byte[] cmdByteArray = null;
        ///			byte[] rspByteArray = null;
        ///			paramReader.ReInitialise(timeout,cmdByteArray,rspByteArray);
        ///		}
        /// </code>
        /// </example>
        void ReInitialise(int timeout, out byte[] cmdByteArray, out byte[] rspByteArray);
        /// <summary>
        /// The function of this command is to enable or disable the heartbeat message. The heart beat message
        /// can be used to detect if the reader is still online when there are no tags being received by that reader.
        /// To disable the heart beat message set the interval to 0, else specify the interval in seconds that you
        /// would like the message to transmit. When enabled the interval can be in the following range [1 – 255]
        /// By enabling the heart beat message you will be able to retrieve the following status for the reader:
        /// Gain Mode, Polling Mode, Relays and Inputs.
        /// Eg : Interval = 0 implies disabled
        /// Interval = 20 implies the heart beat will be transmitted every 20 seconds.
        /// </summary>
        /// <param name="interval">Interval - denotes how often the heart beat message should be transmitted.</param>
        void SetHeartBeat(byte interval);
        /// <summary>
        /// The function of this command is to enable or disable the heartbeat message. The heart beat message
        /// can be used to detect if the reader is still online when there are no tags being received by that reader.
        /// To disable the heart beat message set the interval to 0, else specify the interval in seconds that you
        /// would like the message to transmit. When enabled the interval can be in the following range [1 – 255]
        /// By enabling the heart beat message you will be able to retrieve the following status for the reader:
        /// Gain Mode, Polling Mode, Relays and Inputs.
        /// Eg : Interval = 0 implies disabled
        /// Interval = 20 implies the heart beat will be transmitted every 20 seconds.
        /// </summary>
        /// <param name="interval">Interval - denotes how often the heart beat message should be transmitted.</param>
        /// <param name="timeoutMS">Time Out</param>
        void SetHeartBeat(byte interval, int timeoutMS);
        /// <summary>
        /// The function of this command is to enable or disable the heartbeat message. The heart beat message
        /// can be used to detect if the reader is still online when there are no tags being received by that reader.
        /// To disable the heart beat message set the interval to 0, else specify the interval in seconds that you
        /// would like the message to transmit. When enabled the interval can be in the following range [1 – 255]
        /// By enabling the heart beat message you will be able to retrieve the following status for the reader:
        /// Gain Mode, Polling Mode, Relays and Inputs.
        /// Eg : Interval = 0 implies disabled
        /// Interval = 20 implies the heart beat will be transmitted every 20 seconds.
        /// </summary>
        /// <param name="interval">Interval - denotes how often the heart beat message should be transmitted.</param>
        /// <param name="timeoutMS">Time Out</param>
        /// <param name="cmdByteArray">Command Byte Array</param>
        /// <param name="respByteArray">Response from Reader</param>
        void SetHeartBeat(byte interval,
            int timeoutMS, out byte[] cmdByteArray, out byte[] respByteArray);

        /// <summary>
        /// The function of this command is to get the status of the parameters for the heart beat message structure.
        ///	The heart beat message can be used to detect if the reader is still online when there are no tags
        /// being received by that reader.
        /// The function returns 2 parameters:
        /// 1. Interval - denotes how often the heart beat message should be transmitted.
        /// Eg : Interval = 0 - implies disabled
        /// Interval = 20 - implies the heart beat will be transmitted every 20 seconds.
        /// 2. Status ( 1 byte value)
        /// bit 0 : Autpoll - 0 : Disabled	/ 1 : Enabled
        /// bit 1 : Gain	- 0	: Low		/ 1 : High
        /// bit 2 : Relay 0 - 0 : Open		/ 1 : Closed
        /// bit 3 : Relay 1 - 0 : Open		/ 1 : Closed
        /// bit 4 : Input 0 - 0 : Open		/ 1 : Closed
        /// bit 5 : Input 1 - 0 : Open		/ 1 : Closed
        /// bit 6 : Reserved
        /// bit 7 : Reserved
        /// </summary>
        /// <param name="interval">Interval - denotes how often the heart beat message should be transmitted.</param>
        /// <param name="autoPoll">AutoPollMode - 0 : Disabled/ 1 : Enabled</param>
        /// <param name="gain">Gain	- 0	: Low/ 1 : High</param>
        /// <param name="relay0"> Relay 0 - 0 : Open/ 1 : Closed</param>
        /// <param name="relay1">Relay 1 - 0 : Open/ 1 : Closed</param>
        /// <param name="input0">Input 0 - 0 : Low/ 1 : High</param>
        /// <param name="input1">Input 1 - 0 : Low/ 1 : High</param>
        void GetHeartBeat(out byte interval, out AutoPollMode autoPoll, out GainMode gain,
            out IOStatus relay0, out IOStatus relay1, out IOStatus input0, out IOStatus input1);
        /// <summary>
        /// The function of this command is to get the status of the parameters for the heart beat message structure.
        ///	The heart beat message can be used to detect if the reader is still online when there are no tags
        /// being received by that reader.
        /// The function returns 2 parameters:
        /// 1. Interval - denotes how often the heart beat message should be transmitted.
        /// Eg : Interval = 0 - implies disabled
        /// Interval = 20 - implies the heart beat will be transmitted every 20 seconds.
        /// 2. Status ( 1 byte value)
        /// bit 0 : Autpoll - 0 : Disabled	/ 1 : Enabled
        /// bit 1 : Gain	- 0	: Low		/ 1 : High
        /// bit 2 : Relay 0 - 0 : Open		/ 1 : Closed
        /// bit 3 : Relay 1 - 0 : Open		/ 1 : Closed
        /// bit 4 : Input 0 - 0 : Open		/ 1 : Closed
        /// bit 5 : Input 1 - 0 : Open		/ 1 : Closed
        /// bit 6 : Reserved
        /// bit 7 : Reserved
        /// </summary>
        /// <param name="interval">Interval - denotes how often the heart beat message should be transmitted.</param>
        /// <param name="autoPoll">AutoPollMode - 0 : Disabled/ 1 : Enabled</param>
        /// <param name="gain">Gain	- 0	: Low/ 1 : High</param>
        /// <param name="relay0"> Relay 0 - 0 : Open/ 1 : Closed</param>
        /// <param name="relay1">Relay 1 - 0 : Open/ 1 : Closed</param>
        /// <param name="input0">Input 0 - 0 : Open/ 1 : Closed</param>
        /// <param name="input1">Input 1 - 0 : Open/ 1 : Closed</param>
        /// <param name="timeoutMS">Time Out</param>
        void GetHeartBeat(out byte interval, out AutoPollMode autoPoll, out GainMode gain,
            out IOStatus relay0, out IOStatus relay1, out IOStatus input0, out IOStatus input1, int timeoutMS);
        /// <summary>
        /// The function of this command is to get the status of the parameters for the heart beat message structure.
        ///	The heart beat message can be used to detect if the reader is still online when there are no tags
        /// being received by that reader.
        /// The function returns 2 parameters:
        /// 1. Interval - denotes how often the heart beat message should be transmitted.
        /// Eg : Interval = 0 - implies disabled
        /// Interval = 20 - implies the heart beat will be transmitted every 20 seconds.
        /// 2. Status ( 1 byte value)
        /// bit 0 : AutoPollMode - 0 : Disabled	/ 1 : Enabled
        /// bit 1 : Gain	- 0	: Low		/ 1 : High
        /// bit 2 : Relay 0 - 0 : Open		/ 1 : Closed
        /// bit 3 : Relay 1 - 0 : Open		/ 1 : Closed
        /// bit 4 : Input 0 - 0 : Open		/ 1 : Closed
        /// bit 5 : Input 1 - 0 : Open		/ 1 : Closed
        /// bit 6 : Reserved
        /// bit 7 : Reserved
        /// </summary>
        /// <param name="interval">Interval - denotes how often the heart beat message should be transmitted.</param>
        /// <param name="autoPoll">Autpoll - 0 : Disabled/ 1 : Enabled</param>
        /// <param name="gain">Gain	- 0	: Low/ 1 : High</param>
        /// <param name="relay0"> Relay 0 - 0 : Open/ 1 : Closed</param>
        /// <param name="relay1">Relay 1 - 0 : Open/ 1 : Closed</param>
        /// <param name="input0">Input 0 - 0 : Open/ 1 : Closed</param>
        /// <param name="input1">Input 1 - 0 : Open/ 1 : Closed</param>
        /// <param name="timeoutMS">Time Out</param>
        /// <param name="cmdByteArray">Command Byte Array</param>
        /// <param name="respByteArray">Response from Reader</param>
        void GetHeartBeat(out byte interval, out AutoPollMode autoPoll, out GainMode gain,
            out IOStatus relay0, out IOStatus relay1, out IOStatus input0, out IOStatus input1,
            int timeoutMS, out byte[] cmdByteArray, out byte[] respByteArray);

        /// <summary>
        /// Change the IP address of reader
        /// </summary>
        /// <param name="IPAddress">New IP Address</param>
        void ChangeIPAddress(string IPAddress);
        /// <summary>
        /// Change the IP address of reader
        /// </summary>
        /// <param name="IPAddress">New IP Address</param>
        /// <param name="subnetMask">Subnet Mask</param>
        void ChangeIPAddress(string IPAddress, string subnetMask);
        /// <summary>
        /// Change the IP address of reader
        /// </summary>
        /// <param name="IPAddress">New IP Address</param>
        /// <param name="subnetMask">Subnet Mask</param>
        /// <param name="defaultGateway">Default Getway of Network</param>
        void ChangeIPAddress(string IPAddress, string subnetMask, string defaultGateway);
        /// <summary>
        /// Change the IP address of reader
        /// </summary>
        /// <param name="IPAddress">New IP Address</param>
        /// <param name="subnetMask">Subnet Mask</param>
        /// <param name="defaultGateway">Default Getway of Network</param>
        /// <param name="primaryDNS">Primary DNS</param>
        void ChangeIPAddress(string IPAddress, string subnetMask, string defaultGateway, string primaryDNS);
        /// <summary>
        /// Change the IP address of reader
        /// </summary>
        /// <param name="IPAddress">New IP Address</param>
        /// <param name="subnetMask">Subnet Mask</param>
        /// <param name="defaultGateway">Default Getway of Network</param>
        /// <param name="primaryDNS">Primary DNS</param>
        /// <param name="secondaryDNS">Secondary DNS</param>
        void ChangeIPAddress(string IPAddress, string subnetMask, string defaultGateway, string primaryDNS, string secondaryDNS);
        /// <summary>
        /// Change the IP address of reader
        /// </summary>
        /// <param name="IPAddress">New IP Address</param>
        /// <param name="subnetMask">Subnet Mask</param>
        /// <param name="defaultGateway">Default Getway of Network</param>
        /// <param name="primaryDNS">Primary DNS</param>
        /// <param name="secondaryDNS">Secondary DNS</param>
        /// <param name="Domain">Domain Name</param>
        void ChangeIPAddress(string IPAddress, string subnetMask, string defaultGateway, string primaryDNS, string secondaryDNS, string Domain);
        /// <summary>
        /// Change the IP address of reader
        /// </summary>
        /// <param name="IPAddress">New IP Address</param>
        /// <param name="subnetMask">Subnet Mask</param>
        /// <param name="defaultGateway">Default Getway of Network</param>
        /// <param name="primaryDNS">Primary DNS</param>
        /// <param name="secondaryDNS">Secondary DNS</param>
        /// <param name="Domain">Domain Name</param>
        /// <param name="HostName">Host Name</param>
        void ChangeIPAddress(string IPAddress, string subnetMask, string defaultGateway, string primaryDNS, string secondaryDNS, string Domain, string HostName);
    }
}
