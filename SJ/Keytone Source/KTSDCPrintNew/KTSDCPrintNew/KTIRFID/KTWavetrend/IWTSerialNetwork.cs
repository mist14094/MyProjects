using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;


namespace KTone.Core.KTIRFID
{
#if RX201 || RX201OE ||COMPLETE
    /// <summary>
    /// Interface to support RX201 Readers Network
    /// </summary>
#else
    /// <exclude/>
#endif
    public interface IWTSerialNetwork : IDisposable
    {

        #region Properties
        /// <summary>
        /// Returns Reader model
        /// </summary>
        WTReaderModel WTModel
        {
            get;
        }
        /// <summary>
        /// Returns Reader name assigned to the reader object
        /// </summary>
        string ReaderName
        {
            get;
        }

        /// <summary>
        /// Returns Reader description  assigned to the reader object
        /// </summary>
        string ReaderDescription
        {
            get;
        }
        /// <summary>
        /// Gets Com Port Name
        /// </summary>
        string ComPortName
        {
            get;
        }
        /// <summary>
        /// Gets  IP Address
        /// </summary>
        string IPAddress
        {
            get;

        }
        /// <summary>
        ///  Gets  Port Baud Rate
        /// </summary>
        int PortBaudRate
        {
            get;
            set;
        }
        /// <summary>
        /// Gets Port Number
        /// </summary>
        int PortNumber
        {
            get;

        }
        /// <summary>
        /// Gets and sets Network Name
        /// </summary>
        string NetworkName
        { get; }
        /// <summary>
        /// Get bool value indicating whether reader is in autopolling mode or not.
        /// </summary>
        bool IsAutoPollingOn
        { get; }
        /// <summary>
        ///  Get bool value indicating whether reader is connected or not.
        /// </summary>
        bool IsConnected
        { get; }
        /// <summary>
        /// Provide network id of RS232 network
        /// </summary>
        byte NetworkId
        { get;}
        /// <summary>
        /// Command Timeout
        /// </summary>
        int TimeOut
        {
            set;
            get;
        }
        /// <summary>
        /// Gives No of Nodes to be added in the network 
        /// </summary>
        int NoOfNodes
        {
            get;
        }
        /// <summary>
        /// Handshake Protocol
        /// </summary>
        //CommBase.Handshake PortHandshake
        //{
        //    get;

        //}
        #endregion Properties

        #region Events
        /// <summary>
        /// Event raised after each tag read when 
        /// the reader is in auto polling mode.
        /// Data is passed using WTReaderTagReadEventArgs
        /// </summary>
        event EventHandler<WTReaderEventArgs> OnWTReaderTagRead;

        /// <summary>
        /// Event raised when the response packet 
        /// indicating reader status is received from the reader 
        /// Data is passed using WTReaderStatusMonitorEventArgs
        /// </summary>
        event EventHandler<WTReaderEventArgs> OnWTReaderStatusMonitor;

        /// <summary>
        /// Event raised when the RX201 Reader list is modified.
        /// </summary>
        // event ModifiedReaderListEventHandler OnModifiedReaderList;
         

        #endregion Events

        #region Methods

        #region Network Level Command
        /*

        /// <summary>
        /// Connects with the serial port 
        /// </summary>
        /// <example>
        /// <code>
        /// 	void ExecuteConnect(IWTSerialNetwork paramNetwork)
        ///		{
        ///			paramNetwork.Connect();
        ///		}
        /// </code>
        /// </example>
        void ConnectComport();

        /// <summary>
        /// Disconnects the Serial Network 
        /// </summary>
        /// <example>
        /// <code>
        /// 	void ExecuteDisconnect(IWTSerialNetwork paramNetwork)
        ///		{
        ///			paramNetwork.Disconnect();
        ///		}
        /// </code>
        /// </example>
        void DisconnectComport();
        */

        /// <summary>
        /// The function of this command is to set the reader at position 1 into an Automatic Polling se-quence. It sets the Auto 
        /// Polling flag in the Data EEPROM of the reader to enable Auto Polling after power up.
        /// It will establish the size of the network by sending out tag requests until such time that it gets no response.
        /// This will determine the number of readers on the network.
        /// Once this has been established, it will sequentially poll each reader indefinitely.
        /// Data responses from the readers pass through the first reader 1 and onto the PC.
        /// Readers without a valid tag will respond with an empty packet of data. 
        /// This will enable the monitoring software to determine if any readers are no longer responding.
        /// This command can be addressed directly to reader 1, or on a broadcast basis.
        /// When broadcasting, any reader that is not reader at position 1 on the net-work, will disable its Auto Polling flag in its Data EEPROM section on the reader to avoid any problems in the future because of incorrect parameters.
        /// This command is used to restart the Auto Polling if it has been stopped by a break character.
        /// </summary>
        /// <param name="nodeId">Node Id</param>
        /// <param name="timeInterval">Polling Cycle</param>
        /// <param name="taglistType">Tag List Type, it can be Current or Current and Removed</param>
        /// <example>
        /// <code>
        /// 	void ExecuteEnableAutoPolling(IWTSerialNetwork paramNetwork)
        ///		{
        ///			int timeInterval=2000;
        ///			TagListType taglistType = TagListType.CurrentAndRemoved;
        ///			paramNetwork.EnableAutoPolling(0,timeInterval,taglistType);
        ///		}
        /// </code>
        ///  </example>
        void EnableAutoPolling(int nodeId, int timeInterval/*, TagListType taglistType*/);
        /// <summary>
        /// The function of this command is to set the reader at position 1 into an Automatic Polling se-quence. It sets the Auto 
        /// Polling flag in the Data EEPROM of the reader to enable Auto Polling after power up.
        /// It will establish the size of the network by sending out tag requests until such time that it gets no response.
        /// This will determine the number of readers on the network.
        /// Once this has been established, it will sequentially poll each reader indefinitely.
        /// Data responses from the readers pass through the first reader 1 and onto the PC.
        /// Readers without a valid tag will respond with an empty packet of data. 
        /// This will enable the monitoring software to determine if any readers are no longer responding.
        /// This command can be addressed directly to reader 1, or on a broadcast basis.
        /// When broadcasting, any reader that is not reader at position 1 on the net-work, will disable its Auto Polling flag in its Data EEPROM section on the reader to avoid any problems in the future because of incorrect parameters.
        /// This command is used to restart the Auto Polling if it has been stopped by a break character.
        /// </summary>
        ///  <param name="nodeId">Node Id</param>
        /// <param name="timeInterval">Polling Cycle</param>
        /// <param name="taglistType">Tag List Type, it can be Current or Current and Removed</param>
        ///  <param name="timeout">Time Out</param>
        /// <example>
        /// <code>
        /// 	void ExecuteEnableAutoPolling(IWTSerialNetwork paramNetwork)
        ///		{
        ///			int timeout =5000;
        ///			int timeInterval=2000;
        ///			TagListType taglistType = TagListType.CurrentAndRemoved;
        ///			paramNetwork.EnableAutoPolling(timeInterval,taglistType,timeout);
        ///		}
        /// </code>
        ///  </example>
        void EnableAutoPolling(int nodeId, int timeInterval, /*TagListType taglistType,*/ int timeout);
        /// <summary>
        /// The function of this command is to set the reader at position 1 into an Automatic Polling se-quence. It sets the Auto 
        /// Polling flag in the Data EEPROM of the reader to enable Auto Polling after power up.
        /// It will establish the size of the network by sending out tag requests until such time that it gets no response.
        /// This will determine the number of readers on the network.
        /// Once this has been established, it will sequentially poll each reader indefinitely.
        /// Data responses from the readers pass through the first reader 1 and onto the PC.
        /// Readers without a valid tag will respond with an empty packet of data. 
        /// This will enable the monitoring software to determine if any readers are no longer responding.
        /// This command can be addressed directly to reader 1, or on a broadcast basis.
        /// When broadcasting, any reader that is not reader at position 1 on the net-work, will disable its Auto Polling flag in its Data EEPROM section on the reader to avoid any problems in the future because of incorrect parameters.
        /// This command is used to restart the Auto Polling if it has been stopped by a break character.
        /// </summary>
        ///  <param name="nodeId">Node Id</param>
        /// <param name="timeInterval">Polling Cycle</param>
        /// <param name="taglistType">Tag List Type, it can be Current or Current and Removed</param>
        /// <param name="timeout">Time Out</param>
        /// <param name="cmdByteArray">Command Byte Array</param>
        ///  <param name="rspByteArray">Response from Reader</param>
        /// <example>
        /// <code>
        /// 	void ExecuteEnableAutoPolling(IWTSerialNetwork paramNetwork)
        ///		{
        ///			int timeout =5000;
        ///			int timeInterval=2000;
        ///			TagListType taglistType = TagListType.CurrentAndRemoved;
        ///			byte[] cmdByteArray = null;
        ///			byte[] rspByteArray = null;
        ///			paramNetwork.EnableAutoPolling(timeInterval,taglistType,timeout,
        ///											out cmdByteArray,out rspByteArray);
        ///		}
        /// </code>
        ///  </example>
        void EnableAutoPolling(int nodeId, int timeInterval, /*TagListType taglistType,*/ int timeout,
                                out byte[] cmdByteArray, out byte[] rspByteArray);



        /// <summary>
        /// The function of this command is to disable the Auto Polling feature after power up by resetting the Auto
        /// Polling flag in the Data EEPROM of the reader. This command can be addressed directly to reader 1, or
        /// on a broadcast basis.
        /// </summary>
        ///  <param name="nodeId">Node Id</param>
        /// <param name="timeout">Time Out</param>
        /// <example>
        /// <code>
        /// 	void ExecuteDisableAutoPolling(IWTSerialNetwork paramNetwork)
        ///		{
        ///			int timeout =5000;
        ///			paramNetwork.DisableAutoPolling(timeout);
        ///		}
        /// </code>
        ///  </example>
        void DisableAutoPolling(int nodeId, int timeout);
        /// <summary>
        /// The function of this command is to disable the Auto Polling feature after power up by resetting the Auto
        /// Polling flag in the Data EEPROM of the reader. This command can be addressed directly to reader 1, or
        /// on a broadcast basis.
        /// </summary>
        ///  <param name="nodeId">Node Id</param>
        /// <param name="timeout">Time Out</param>
        /// <param name="cmdByteArray">Command Byte Array</param>
        /// <param name="rspByteArray">Response from Reader</param>
        /// <example>
        /// <code>
        /// 	void ExecuteDisableAutoPolling(IWTSerialNetwork paramNetwork)
        ///		{
        ///			int timeout =5000;
        ///			byte[] cmdByteArray = null;
        ///			byte[] rspByteArray = null;
        ///			paramNetwork.DisableAutoPolling(timeout,out cmdByteArray,out rspByteArray);
        ///		}
        /// </code>
        ///  </example>
        void DisableAutoPolling(int nodeId, int timeout, out byte[] cmdByteArray, out byte[] rspByteArray);


        /// <summary>
        /// The function of this command is to reset the entire network and re-establish the NODE ID ad-dresses. 
        /// The NODE ID address in the command packet should hold a 255 (broadcast value) to ensure that the entire
        /// network enters into the reset sequence. Only reader at position 1 will respond with the reply packet. 
        /// This is the only condition under which a response is sent from a broadcast command.
        /// Note: receiving a reset network reply packet at any point where no reset command was sent, 
        /// will imply that a spontaneous reset has occurred. This would probably be as the result of a power problem.
        /// </summary>
        /// <param name="timeout">Time Out</param>
        /// <example>
        /// <code>
        /// 	void ExecuteResetNetwork(IWTSerialNetwork paramNetwork)
        ///		{
        ///			int timeout =5000;
        ///			paramNetwork.ResetNetwork(timeout);
        ///		}
        /// </code>
        ///  </example>
        void ResetNetwork(int timeout);
        /// <summary>
        /// The function of this command is to reset the entire network and re-establish the NODE ID ad-dresses. 
        /// The NODE ID address in the command packet should hold a 255 (broadcast value) to ensure that the entire
        /// network enters into the reset sequence. Only reader at position 1 will respond with the reply packet. 
        /// This is the only condition under which a response is sent from a broadcast command.
        /// Note: receiving a reset network reply packet at any point where no reset command was sent, 
        /// will imply that a spontaneous reset has occurred. This would probably be as the result of a power problem.
        /// </summary>
        /// <param name="timeout">Time Out</param>
        /// <param name="cmdByteArray">Command Byte Array</param>
        /// <param name="rspByteArray">Response from Reader</param>
        /// <example>
        /// <code>
        /// 	void ExecuteResetNetwork(IWTSerialNetwork paramNetwork)
        ///		{
        ///			int timeout =5000;
        ///			byte[] cmdByteArray = null;
        ///			byte[] rspByteArray = null;
        ///			paramNetwork.ResetNetwork(timeout,out cmdByteArray,out rspByteArray);
        ///		}
        /// </code>
        ///  </example>
        void ResetNetwork(int timeout, out byte[] cmdByteArray, out byte[] rspByteArray);



        /// <summary>
        /// This command will reset the network Baud Rate. It will only accept a 
        /// broadcast command and there is no response sent. Changes are immediate 
        /// and will result in a communication loss if the PC does not change its 
        /// baud rate accordingly.
        /// </summary>
        /// <param name="baudRate">Baud Rate Type </param>
        /// <param name="timeout">Time Out</param>
        /// <example>
        /// <code>
        /// 	void ExecuteResetBaudRate(IWTSerialNetwork paramNetwork)
        ///		{
        ///			int timeout =5000;
        ///			BaudRate buadRate = BaudRate.Baud115600
        ///			paramNetwork.SetBaudRate(timeout);
        ///		}
        /// </code>
        ///  </example>
        void ResetBaudRate(BaudRate baudRate, int timeout);
        /// <summary>
        /// This command will reset the network Baud Rate. It will only accept a 
        /// broadcast command and there is no response sent. Changes are immediate 
        /// and will result in a communication loss if the PC does not change its 
        /// baud rate accordingly.
        /// </summary>
        /// <param name="baudRate">Baud Rate Type </param>
        /// <param name="timeout">Time Out</param>
        /// <param name="cmdByteArray">Command Byte Array</param>
        /// <param name="rspByteArray">Response from Reader</param>
        /// <example>
        /// <code>
        /// 	void ExecuteResetBaudRate(IWTSerialNetwork paramNetwork)
        ///		{
        ///			int timeout =5000;
        ///			byte[] cmdByteArray = null;
        ///			byte[] rspByteArray = null;
        ///         BaudRate buadRate = BaudRate.Baud115600
        ///			paramNetwork.SetBaudRate(baudRate, timeout,out cmdByteArray,out rspByteArray);
        ///		}
        /// </code>
        ///  </example>
        void ResetBaudRate(BaudRate baudRate, int timeout, out byte[] cmdByteArray, out byte[] rspByteArray);



        /// <summary>
        /// The function of this command is to assign the Network ID as well as commit it to the Data EEPROM of
        /// the reader.
        /// </summary>
        ///  <param name="nodeId">Node Id</param>
        /// <param name="newNetworkId">New Network Id</param>
        /// <example>
        /// <code>
        /// 	void ExecuteSetNetworkID(IWTSerialNetwork paramReader)
        ///		{
        ///			byte newNetworkID =1;
        ///			paramReader.SetNetworkID(newNetworkID);
        ///		}
        /// </code>
        ///  </example>
        void SetNetworkID(int nodeId, byte newNetworkId);
        /// <summary>
        /// The function of this command is to assign the Network ID as well as commit it to the Data EEPROM of
        /// the reader.
        /// </summary>
        ///  <param name="nodeId">Node Id</param>
        /// <param name="newNetworkId">New Network Id</param>
        /// <param name="timeout">Time Out</param>
        /// <example>
        /// <code>
        /// 	void ExecuteSetNetworkID(IWTSerialNetwork paramReader)
        ///		{
        ///			byte newNetworkID =1;
        ///			int timeout =5000;
        ///			paramReader.SetNetworkID(newNetworkID,timeout);
        ///		}
        /// </code>
        ///  </example>
        void SetNetworkID(int nodeId, byte newNetworkId, int timeout);
        /// <summary>
        /// The function of this command is to assign the Network ID as well as commit it to the Data EEPROM of
        /// the reader.
        /// </summary>
        ///  <param name="nodeId">Node Id</param>
        /// <param name="newNetworkId">New Network Id</param>
        /// <param name="timeout">Time Out</param>
        /// <param name="cmdByteArray">Command Byte Array</param>
        /// <param name="rspByteArray">Response from Reader</param>
        /// <example>
        /// <code>
        /// 	void ExecuteSetNetworkID(IWTSerialNetwork paramReader)
        ///		{
        ///			byte newNetworkID =1;
        ///			int timeout =5000;
        ///			byte[] cmdByteArray = null;
        ///			byte[] rspByteArray = null;
        ///			paramReader.SetNetworkID(newNetworkID,timeout,out cmdByteArray, out rspByteArray);
        ///		}
        /// </code>
        /// </example>
        void SetNetworkID(int nodeId, byte newNetworkId, int timeout,
                            out byte[] cmdByteArray, out byte[] rspByteArray);


        /// <summary>
        /// Start Autopolling while send continous enable autopolling command to 1st Node in the RS485 network.
        /// </summary>
        /// <param name="timeInterval">Time Interval of PollingCycle in mill seconds</param>
        /// <param name="taglistType">Type of List you want after each polling cycle</param>
        ///  <example>
        /// <code>
        /// 	void ExecuteSetNetworkID(IWTSerialNetwork paramReader)
        ///		{
        ///			
        ///			int timeout =5000;
        ///      	int timeInterval=2000;
        ///			TagListType taglistType = TagListType.CurrentAndRemoved;
        ///			paramReader.StartAutoPolling(timeInterval,taglistType);
        ///		}
        /// </code>
        /// </example>
        void StartAutoPolling(TagListType taglistType);
        /// <summary>
        /// Start Autopolling while send continous enable autopolling command to 1st Node in the RS485 network.
        /// </summary>
        /// <param name="timeInterval">Time Interval of PollingCycle in mill seconds</param>
        /// <param name="taglistType">Type of List you want after each polling cycle</param>
        /// <param name="timeoutMs" >Time Out in milli seconds</param>
        /// <example>
        /// <code>
        /// 	void ExecuteSetNetworkID(IWTSerialNetwork paramReader)
        ///		{
        ///			
        ///			int timeout =5000;
        ///      	int timeInterval=2000;
        ///			TagListType taglistType = TagListType.CurrentAndRemoved;
        ///			paramReader.SetNetworkID(timeInterval,taglistType,timeout);
        ///		}
        /// </code>
        /// </example>
        void StartAutoPolling(TagListType taglistType, int timeoutMs);
        /// <summary>
        /// Start Autopolling while send continous enable autopolling command to 1st Node in the RS485 network.
        /// </summary>
        /// <param name="timeInterval">Time Interval of PollingCycle in mill seconds</param>
        /// <param name="taglistType">Type of List you want after each polling cycle</param>
        /// <param name="timeoutMS"> Time Out in milli seconds </param>
        /// <param name="cmdByteArray">Command Byte Array</param>
        /// <param name="respByteArray">Response from Reader</param>
        /// <example>
        /// <code>
        /// 	void ExecuteSetNetworkID(IWTSerialNetwork paramReader)
        ///		{
        ///			
        ///			int timeout =5000;
        ///      	int timeInterval=2000;
        ///			TagListType taglistType = TagListType.CurrentAndRemoved;
        ///			byte[] cmdByteArray = null;
        ///			byte[] rspByteArray = null;
        ///			paramReader.SetNetworkID(timeInterval,taglistType,timeout, out cmdByteArray, out rspByteArray);
        ///		}
        /// </code>
        /// </example>
        void StartAutoPolling(TagListType taglistType, int timeoutMS, out byte[] cmdByteArray, out byte[] respByteArray);



        /// <summary>
        /// This method will stop autopolling mode by sending countinous break character to network.
        /// </summary>
        /// <example>
        /// <code>
        /// 	void ExecuteSetNetworkID(IWTSerialNetwork paramReader)
        ///		{
        ///			
        ///			paramReader.StopAutoPolling();
        ///		}
        /// </code>
        /// </example>
        void StopAutoPolling();
        /// <summary>
        /// This method will stop autopolling mode by sending countinous break character to network.
        /// </summary>
        /// <param name="timeoutMs">Time out</param>
        /// <example>
        /// <code>
        /// 	void ExecuteSetNetworkID(IWTSerialNetwork paramReader)
        ///		{
        ///			int timeout =5000;
        ///			paramReader.StopAutoPolling(timeout);
        ///		}
        /// </code>
        /// </example>
        void StopAutoPolling(int timeoutMs);
        /// <summary>
        /// This method will stop autopolling mode by sending countinous break character to network.
        /// </summary>
        /// <param name="timeoutMS">Time out</param>
        /// <param name="cmdByteArray">Command Bytes</param>
        /// <param name="respByteArray">Response Bytes</param>
        /// <example>
        /// <code>
        /// 	void ExecuteSetNetworkID(IWTSerialNetwork paramReader)
        ///		{
        ///			int timeout =5000;
        ///         byte[] cmdByteArray = null;
        ///			byte[] rspByteArray = null;
        ///			paramReader.StopAutoPolling(timeout, out cmdByteArray, out respByteArray));
        ///		}
        /// </code>
        /// </example>
        void StopAutoPolling(int timeoutMS, out byte[] cmdByteArray, out byte[] respByteArray);

        /// <summary>
        /// Get the WTSerialNetworkReader object for node attached in network
        /// </summary>
        /// <param name="nodeId">position of reader in network</param>
        /// <returns>Reader Object</returns>
        WTSerialNetworkReader GetReader(int nodeId);
        /// <summary>
        /// Get the WTSerialNetworkReader object for node attached in network
        /// </summary>
        /// <returns>Reader Object</returns>
        WTSerialNetworkReader[] GetReaders();
        /// <summary>
        /// Update Reader List
        /// </summary>
        int RefreshNetwork();
        /// <summary>
        /// Try to ping the nodes of the serial Network and return the No of Nodes that can be 
        /// reachable.Again To ping the reader this command first stop the automode.
        /// </summary>
        /// <returns></returns>
        int ProbeNetWork();

        #endregion Network Level Command

        #region Reader Level Command

        /// <summary>
        /// Connects the reader in serial network
        /// </summary>
        ///  <param name="NodeId">Node Id</param>
        /// <example>
        /// <code>
        /// 	void ExecuteConnect(IWTSerialNetwork paramNetwork)
        ///		{
        ///			paramNetwork.Online();
        ///		}
        /// </code>
        /// </example>
        void Online(int NodeId);

        /// <summary>
        /// Disconnects the reader in serial network
        /// </summary>
        ///  <param name="NodeId">Node Id</param>
        /// <example>
        /// 
        /// <code>
        /// 	void ExecuteDisconnect(IWTSerialNetwork paramNetwork)
        ///		{
        ///			paramNetwork.Offline();
        ///		}
        /// </code>
        /// </example>
        void Offline(int NodeId);

        /// <summary>
        /// The Ping Command is simply used to check if a reader is online and responding correctly. It can be
        /// used to read back Network ID's, Reader ID's and Node ID's. Inserted into the response from a Ping
        /// Command is an Error Number. This number refers to the last error the respective reader has experienced.
        /// Once read, this number is cleared.
        /// </summary>
        ///  <param name="nodeId">Node Id</param>
        /// <param name="networkID">Network Id</param>
        /// <param name="receiverID">Receiver Id</param>
        /// <param name="nodeID">Node Id</param>
        /// <param name="errorCode">Error Code</param>
        /// <example>
        /// <code>
        /// 	void ExecutePingReader(IWTRFReader paramReader)
        ///		{
        ///			byte networkID;
        ///			byte receiverID;
        ///			byte nodeID;
        ///			WTReaderErrors errorCode;
        ///			paramReader.PingReader(out networkID,out receiverID,
        ///			out nodeID,out WTReaderErrors errorCode);
        ///		}
        /// </code>
        ///  </example>
        void PingReader(int nodeId, out byte networkID, out byte receiverID,
                                out byte nodeID, out WTReaderErrors errorCode);
        /// <summary>
        /// The Ping Command is simply used to check if a reader is online and responding correctly. It can be
        /// used to read back Network ID's, Reader ID's and Node ID's. Inserted into the response from a Ping
        /// Command is an Error Number. This number refers to the last error the respective reader has experienced.
        /// Once read, this number is cleared.
        /// </summary>
        ///  <param name="nodeId">Node Id</param>
        /// <param name="networkID">Network Id</param>
        /// <param name="receiverID">Receiver Id</param>
        /// <param name="nodeID">Node Id</param>
        /// <param name="errorCode">Error Code</param>
        /// <param name="timeoutMS">Time Out</param>
        /// <example>
        /// <code>
        /// 	void ExecutePingReader(IWTRFReader paramReader)
        ///		{
        ///			byte networkID;
        ///			byte receiverID;
        ///			byte nodeID;
        ///			WTReaderErrors errorCode;
        ///			int timeout =5000;
        ///			paramReader.PingReader(out networkID,out receiverID,
        ///			out nodeID,out WTReaderErrors errorCode,
        ///			int timeoutMS);
        ///		}
        /// </code>
        ///  </example>
        void PingReader(int nodeId, out byte networkID, out byte receiverID,
                        out byte nodeID, out WTReaderErrors errorCode, int timeoutMS, bool retry);
        /// <summary>
        /// The Ping Command is simply used to check if a reader is online and responding correctly. It can be
        /// used to read back Network ID's, Reader ID's and Node ID's. Inserted into the response from a Ping
        /// Command is an Error Number. This number refers to the last error the respective reader has experienced.
        /// Once read, this number is cleared.
        /// </summary>
        ///  <param name="nodeId">Node Id</param>
        /// <param name="networkID">Network Id</param>
        /// <param name="receiverID">Receiver Id</param>
        /// <param name="nodeID">Node Id</param>
        /// <param name="errorCode">Error Code</param>
        /// <param name="timeoutMS">Time Out</param>
        /// <param name="cmdByteArray">Command Byte Array</param>
        /// <param name="respByteArray">Response from Reader</param>
        /// <example>
        /// <code>
        /// 	void ExecutePingReader(IWTRFReader paramReader)
        ///		{
        ///			byte networkID;
        ///			byte receiverID;
        ///			byte nodeID;
        ///			WTReaderErrors errorCode;
        ///			int timeout =5000;
        ///			byte[] cmdByteArray = null;
        ///			byte[] rspByteArray = null;
        ///			paramReader.PingReader(out networkID,out receiverID,
        ///			out nodeID,out WTReaderErrors errorCode,
        ///			int timeoutMS,out cmdByteArray,out respByteArray);
        ///		}
        /// </code>
        ///  </example>
        void PingReader(int nodeId, out byte networkID, out byte receiverID,
                    out byte nodeID, out WTReaderErrors errorCode,
                    int timeoutMS, out byte[] cmdByteArray, out byte[] respByteArray);

        /// <summary>
        /// The function of this command is to assign the Reader ID as well as commit it to the Data EEPROM of
        /// the reader.
        /// </summary>
        ///  <param name="nodeId">Node Id</param>
        /// <param name="newReaderID">New Reader Id</param>
        /// <example>
        /// <code>
        /// 	void ExecuteSetReaderId(IWTRFReader paramReader)
        ///		{
        ///			byte newReaderID =1;
        ///			paramReader.SetReaderID(newReaderID);
        ///		}
        /// </code>
        ///  </example>
        void SetReaderID(int nodeId, byte newReaderID);
        /// <summary>
        /// The function of this command is to assign the Reader ID as well as commit it to the Data EEPROM of
        /// the reader.
        /// </summary>
        ///  <param name="nodeId">Node Id</param>
        /// <param name="newReaderID">New Reader Id</param>
        /// <param name="timeout">Time Out</param>
        /// <example>
        /// <code>
        /// 	void ExecuteSetReaderId(IWTRFReader paramReader)
        ///		{
        ///			byte newReaderID =1;
        ///			int timeout =5000;
        ///			paramReader.SetReaderID(newReaderID,timeout);
        ///		}
        /// </code>
        ///  </example>
        void SetReaderID(int nodeId, byte newReaderID, int timeout);
        /// <summary>
        /// The function of this command is to assign the Reader ID as well as commit it to the Data EEPROM of
        /// the reader.
        /// </summary>
        ///  <param name="nodeId">Node Id</param>
        /// <param name="newReaderID">New Reader Id</param>
        /// <param name="timeout">Time Out</param>
        /// <param name="cmdByteArray">Command Byte Array</param>
        /// <param name="rspByteArray">Response from Reader</param>
        /// <example>
        /// <code>
        /// 	void ExecuteSetReaderId(IWTRFReader paramReader)
        ///		{
        ///			byte newReaderID =1;
        ///			int timeout =5000;
        ///			byte[] cmdByteArray = null;
        ///			byte[] rspByteArray = null;
        ///			paramReader.SetReaderID(newReaderID,timeout,out cmdByteArray, out rspByteArray);
        ///		}
        /// </code>
        /// </example>
        void SetReaderID(int nodeId, byte newReaderID, int timeout, out byte[] cmdByteArray, out byte[] rspByteArray);

        /// <summary>
        /// This will be the most commonly used command on any system. Its function is to request a data packet
        /// from the reader which contains the tag data packet if there is one ready for sending. A tag packet is removed
        /// from the readers’ buffer, and returned with this command, making room for a new tag packet.
        /// New tags received by the RF Module are stored in the Reader Buffer and the existing tags are deleted in
        /// a FIFO method in order to keep the data current. If no tag is ready for sending to the PC, an empty
        /// packet is sent back. That is, no data in the Data field.
        /// The reader has a 3 stage buffer which allows for 3 tag data packets to be stored.
        /// </summary>
        ///  <param name="nodeId">Node Id</param>
        /// <returns>WTTag Object</returns>
        /// <example>
        /// <code>
        /// 	void ExecuteGetTagPacket(IWTRFReader paramReader)
        ///		{
        ///			paramReader.GetTagPacket();
        ///		}
        /// </code>
        /// </example>
        WTTag GetTagPacket(int nodeId);
        /// <summary>
        /// This will be the most commonly used command on any system. Its function is to request a data packet
        /// from the reader which contains the tag data packet if there is one ready for sending. A tag packet is removed
        /// from the readers’ buffer, and returned with this command, making room for a new tag packet.
        /// New tags received by the RF Module are stored in the Reader Buffer and the existing tags are deleted in
        /// a FIFO method in order to keep the data current. If no tag is ready for sending to the PC, an empty
        /// packet is sent back. That is, no data in the Data field.
        /// The reader has a 3 stage buffer which allows for 3 tag data packets to be stored.
        /// </summary>
        /// <param name="timeout">Time Out</param>
        ///  <param name="nodeId">Node Id</param>
        /// <returns>WTTag Object</returns>
        /// <example>
        /// <code>
        /// 	void ExecuteGetTagPacket(IWTRFReader paramReader)
        ///		{
        ///			byte newReaderID =1;
        ///			int timeout =5000;
        ///			paramReader.GetTagPacket(timeout);
        ///		}
        /// </code>
        /// </example>
        WTTag GetTagPacket(int nodeId, int timeout);
        /// <summary>
        /// This will be the most commonly used command on any system. Its function is to request a data packet
        /// from the reader which contains the tag data packet if there is one ready for sending. A tag packet is removed
        /// from the readers’ buffer, and returned with this command, making room for a new tag packet.
        /// New tags received by the RF Module are stored in the Reader Buffer and the existing tags are deleted in
        /// a FIFO method in order to keep the data current. If no tag is ready for sending to the PC, an empty
        /// packet is sent back. That is, no data in the Data field.
        /// The reader has a 3 stage buffer which allows for 3 tag data packets to be stored.
        /// </summary>
        ///  <param name="nodeId">Node Id</param>
        /// <param name="timeout">Time Out</param>
        /// <param name="cmdByteArray">Command Byte Array</param>
        /// <param name="rspByteArray">Response from Reader</param>
        /// <returns>WTTag Object</returns>
        /// <example>
        /// <code>
        /// 	void ExecuteGetTagPacket(IWTRFReader paramReader)
        ///		{
        ///			int timeout =5000;
        ///			byte[] cmdByteArray = null;
        ///			byte[] rspByteArray = null;
        ///			paramReader.GetTagPacket(timeout,out cmdByteArray, out rspByteArray);
        ///		}
        /// </code>
        /// </example>
        WTTag GetTagPacket(int nodeId, int timeout, out byte[] cmdByteArray, out byte[] rspByteArray);

        /// <summary>
        /// This command will set the RSSI value and commit it to the Data EEPROM of the reader. It also initiates
        /// an RF Module reset and writes the new value to the RF Module. Broadcasts here are useful to set all the
        /// readers to their most sensitive etc. The RSSI value ranges from 0 to 255 where 0 value being the most
        /// sensitive.
        /// </summary>
        ///  <param name="nodeId">Node Id</param>
        /// <param name="RSSIvalue">RSSI value</param>
        /// <example>
        /// <code>
        /// 	void ExecuteSetRSSIValue(IWTRFReader paramReader)
        ///		{
        ///			byte RSSIvalue =1;
        ///			paramReader.SetRSSIValue(RSSIvalue);
        ///		}
        /// </code>
        /// </example>
        void SetRSSIValue(int nodeId, byte RSSIvalue);
        /// <summary>
        /// This command will set the RSSI value and commit it to the Data EEPROM of the reader. It also initiates
        /// an RF Module reset and writes the new value to the RF Module. Broadcasts here are useful to set all the
        /// readers to their most sensitive etc. The RSSI value ranges from 0 to 255 where 0 value being the most
        /// sensitive.
        /// </summary>
        ///  <param name="nodeId">Node Id</param>
        /// <param name="RSSIvalue">RSSI value</param>
        /// <param name="timeout">Time Out</param>
        /// <example>
        /// <code>
        /// 	void ExecuteSetRSSIValue(IWTRFReader paramReader)
        ///		{
        ///			byte RSSIvalue =1;
        ///			int timeout =5000;
        ///			paramReader.SetRSSIValue(RSSIvalue,timeout);
        ///		}
        /// </code>
        /// </example>
        void SetRSSIValue(int nodeId, byte RSSIvalue, int timeout);
        /// <summary>
        /// This command will set the RSSI value and commit it to the Data EEPROM of the reader. It also initiates
        /// an RF Module reset and writes the new value to the RF Module. Broadcasts here are useful to set all the
        /// readers to their most sensitive etc. The RSSI value ranges from 0 to 255 where 0 value being the most
        /// sensitive.
        /// </summary>
        ///  <param name="nodeId">Node Id</param>
        /// <param name="RSSIvalue">RSSI value</param>
        /// <param name="timeout">Time Out</param>
        /// <param name="cmdByteArray">Command Byte Array</param>
        /// <param name="rspByteArray">Response from Reader</param>
        /// <example>
        /// <code>
        /// 	void ExecuteSetRSSIValue(IWTRFReader paramReader)
        ///		{
        ///			byte RSSIvalue =1;
        ///			int timeout =5000;
        ///			byte[] cmdByteArray = null;
        ///			byte[] rspByteArray = null;
        ///			paramReader.SetRSSIValue(RSSIvalue,timeout,out cmdByteArray, out rspByteArray);
        ///		}
        /// </code>
        /// </example>
        void SetRSSIValue(int nodeId, byte RSSIvalue, int timeout,
                            out byte[] cmdByteArray, out byte[] rspByteArray);

        /// <summary>
        /// This command will return the RSSI value currently stored in the Data EEPROM section of the reader.
        /// </summary>
        ///  <param name="nodeId">Node Id</param>
        /// <returns>RSSI value</returns>
        /// <example>
        /// <code>
        /// 	void ExecuteGetRSSIValue(IWTRFReader paramReader)
        ///		{
        ///			byte RSSIvalue;
        ///			RSSIvalue = paramReader.GetRSSIValue();
        ///		}
        /// </code>
        /// </example>
        byte GetRSSIValue(int nodeId);
        /// <summary>
        /// This command will return the RSSI value currently stored in the Data EEPROM section of the reader.
        /// </summary>
        ///  <param name="nodeId">Node Id</param>
        /// <param name="timeout">Time Out</param>
        /// <returns>RSSI value</returns>
        /// <example>
        /// <code>
        /// 	void ExecuteGetRSSIValue(IWTRFReader paramReader)
        ///		{
        ///			byte RSSIvalue;
        ///			int timeout =5000;
        ///			RSSIvalue = paramReader.GetRSSIValue(RSSIvalue,timeout);
        ///		}
        /// </code>
        /// </example>
        byte GetRSSIValue(int nodeId, int timeout);
        /// <summary>
        /// This command will return the RSSI value currently stored in the Data EEPROM section of the reader.
        /// </summary>
        ///  <param name="nodeId">Node Id</param>
        /// <param name="timeout">Time Out</param>
        /// <param name="cmdByteArray">Command Byte Array</param>
        /// <param name="rspByteArray">Response from Reader</param>
        /// <returns>RSSI value</returns>
        /// <example>
        /// <code>
        /// 	void ExecuteGetRSSIValue(IWTRFReader paramReader)
        ///		{
        ///			byte RSSIvalue;
        ///			int timeout =5000;	
        ///			byte[] cmdByteArray = null;
        ///			byte[] rspByteArray = null;
        ///			RSSIvalue = paramReader.GetRSSIValue(RSSIvalue,timeout,cmdByteArray,rspByteArray);
        ///		}
        /// </code>
        /// </example>
        byte GetRSSIValue(int nodeId, int timeout, out byte[] cmdByteArray, out byte[] rspByteArray);

        /// <summary>
        /// The site code is a 3 byte value assigned to Wavetrend Tags. It is a property of the tag that describes a
        /// customer site installation. The Set Site Code function is to store a site code value to the Data
        /// EEPROM of the reader. Once a value is stored in the reader, it will enable the reader to filter out any
        /// tags that it receives that do not correspond to the stored site code value.
        /// When the site code is set to 0 value, the reader will allow ALL tags to be read, hence the reader is said
        /// to be open. Setting a specific site code will result in only tags that have that site code to be read and
        /// reported.
        /// </summary>
        ///  <param name="nodeId">Node Id</param>
        /// <param name="sitecode">Site Code</param>
        /// <example>
        /// <code>
        /// 	void ExecuteSetSiteCode(IWTRFReader paramReader)
        ///		{
        ///			int sitecode;
        ///			paramReader.SetSiteCode(sitecode);
        ///		}
        /// </code>
        /// </example>
        void SetSiteCode(int nodeId, uint sitecode);
        /// <summary>
        /// The site code is a 3 byte value assigned to Wavetrend Tags. It is a property of the tag that describes a
        /// customer site installation. The Set Site Code function is to store a site code value to the Data
        /// EEPROM of the reader. Once a value is stored in the reader, it will enable the reader to filter out any
        /// tags that it receives that do not correspond to the stored site code value.
        /// When the site code is set to 0 value, the reader will allow ALL tags to be read, hence the reader is said
        /// to be open. Setting a specific site code will result in only tags that have that site code to be read and
        /// reported.
        /// </summary>
        ///  <param name="nodeId">Node Id</param>
        /// <param name="sitecode">Site Code</param>
        /// <param name="timeout">Time Out</param>
        /// <example>
        /// <code>
        /// 	void ExecuteSetSiteCode(IWTRFReader paramReader)
        ///		{
        ///			int sitecode;
        ///			int timeout =5000;	
        ///			paramReader.SetSiteCode(sitecode,timeout);
        ///		}
        /// </code>
        /// </example>
        void SetSiteCode(int nodeId, uint sitecode, int timeout);
        /// <summary>
        /// The site code is a 3 byte value assigned to Wavetrend Tags. It is a property of the tag that describes a
        /// customer site installation. The Set Site Code function is to store a site code value to the Data
        /// EEPROM of the reader. Once a value is stored in the reader, it will enable the reader to filter out any
        /// tags that it receives that do not correspond to the stored site code value.
        /// When the site code is set to 0 value, the reader will allow ALL tags to be read, hence the reader is said
        /// to be open. Setting a specific site code will result in only tags that have that site code to be read and
        /// reported.
        /// </summary>
        ///  <param name="nodeId">Node Id</param>
        /// <param name="sitecode">Site Code</param>
        /// <param name="timeout">Time Out</param>
        /// <param name="cmdByteArray">Command Byte Array</param>
        /// <param name="rspByteArray">Response from Reader</param>
        /// <example>
        /// <code>
        /// 	void ExecuteSetSiteCode(IWTRFReader paramReader)
        ///		{
        ///			int sitecode;
        ///			int timeout =5000;	
        ///			byte[] cmdByteArray = null;
        ///			byte[] rspByteArray = null;
        ///			paramReader.SetSiteCode(sitecode,timeout,cmdByteArray,rspByteArray);
        ///		}
        /// </code>
        /// </example>
        void SetSiteCode(int nodeId, uint sitecode, int timeout, out byte[] cmdByteArray, out byte[] rspByteArray);

        /// <summary>
        /// This command will return the active Site Code stored in the specific reader.
        /// </summary>
        ///  <param name="nodeId">Node Id</param>
        /// <returns>SiteCode</returns>
        /// <example>
        /// <code>
        /// 	void ExecuteGetSiteCode(IWTRFReader paramReader)
        ///		{
        ///			int sitecode;
        ///			int timeout =5000;	
        ///			byte[] cmdByteArray = null;
        ///			byte[] rspByteArray = null;
        ///			sitecode = paramReader.GetSiteCode();
        ///		}
        /// </code>
        /// </example>
        uint GetSiteCode(int nodeId);
        /// <summary>
        /// This command will return the active Site Code stored in the specific reader.
        /// </summary>
        ///  <param name="nodeId">Node Id</param>
        /// <param name="timeout">Time Out</param>
        /// <returns>SiteCode, Byte array of length 3</returns>
        /// <example>
        /// <code>
        /// 	void ExecuteGetSiteCode(IWTRFReader paramReader)
        ///		{
        ///			int sitecode;
        ///			int timeout =5000;	
        ///			sitecode = paramReader.GetSiteCode(timeout);
        ///		}
        /// </code>
        /// </example>
        uint GetSiteCode(int nodeId, int timeout);
        /// <summary>
        /// This command will return the active Site Code stored in the specific reader.
        /// </summary>
        ///  <param name="nodeId">Node Id</param>
        /// <param name="timeout">Time Out</param>
        /// <param name="cmdByteArray">Command Byte Array</param>
        /// <param name="rspByteArray">Response from Reader</param>
        /// <returns>SiteCode, Byte array of length 3</returns>
        /// <example>
        /// <code>
        /// 	void ExecuteGetSiteCode(IWTRFReader paramReader)
        ///		{
        ///			int sitecode;
        ///			int timeout =5000;	
        ///			byte[] cmdByteArray = null;
        ///			byte[] rspByteArray = null;
        ///			sitecode = paramReader.GetSiteCode(timeout,cmdByteArray,rspByteArray);
        ///		}
        /// </code>
        /// </example>
        uint GetSiteCode(int nodeId, int timeout, out byte[] cmdByteArray, out byte[] rspByteArray);

        /// <summary>
        /// This command will set the RF Module into its 2 different gain levels.
        /// Gain = 0 (Low Gain Mode – Short range reader)
        /// Gain = 1 (High Gain Mode – Long range reader)
        /// </summary>
        ///  <param name="nodeId">Node Id</param>
        /// <param name="gainMode">Gain Mode enum</param>
        /// <example>
        /// <code>
        /// 	void ExecuteSetReceiverGain(IWTRFReader paramReader)
        ///		{
        ///			GainMode gainMode  = GainMode.LOWGAIN;
        ///			int timeout =5000;	
        ///			byte[] cmdByteArray = null;
        ///			byte[] rspByteArray = null;
        ///			paramReader.SetReceiverGain(gainMode,timeout,cmdByteArray,rspByteArray);
        ///		}
        /// </code>
        /// </example>
        void SetReceiverGain(int nodeId, GainMode gainMode);
        /// <summary>
        /// This command will set the RF Module into its 2 different gain levels.
        /// Gain = 0 (Low Gain Mode – Short range reader)
        /// Gain = 1 (High Gain Mode – Long range reader)
        /// </summary>
        ///  <param name="nodeId">Node Id</param>
        /// <param name="gainMode">Gain Mode enum</param>
        /// <param name="timeout">Time Out</param>
        /// <example>
        /// <code>
        /// 	void ExecuteSetReceiverGain(IWTRFReader paramReader)
        ///		{
        ///			GainMode gainMode  = GainMode.LOWGAIN;
        ///			int timeout =5000;	
        ///			paramReader.SetReceiverGain(gainMode,timeout);
        ///		}
        /// </code>
        /// </example>
        void SetReceiverGain(int nodeId, GainMode gainMode, int timeout);
        /// <summary>
        /// This command will set the RF Module into its 2 different gain levels.
        /// Gain = 0 (Low Gain Mode – Short range reader)
        /// Gain = 1 (High Gain Mode – Long range reader)
        /// </summary>
        ///  <param name="nodeId">Node Id</param>
        /// <param name="gainMode">Gain Mode enum</param>
        /// <param name="timeout">Time Out</param>
        /// <param name="cmdByteArray">Command Byte Array</param>
        /// <param name="rspByteArray">Response from Reader</param>
        /// <example>
        /// <code>
        /// 	void ExecuteSetReceiverGain(IWTRFReader paramReader)
        ///		{
        ///			GainMode gainMode  = GainMode.LOWGAIN;
        ///			int timeout =5000;	
        ///			byte[] cmdByteArray = null;
        ///			byte[] rspByteArray = null;
        ///			paramReader.SetReceiverGain(gainMode,timeout,cmdByteArray,rspByteArray);
        ///		}
        /// </code>
        /// </example>
        void SetReceiverGain(int nodeId, GainMode gainMode, int timeout,
                                out byte[] cmdByteArray, out byte[] rspByteArray);

        /// <summary>
        /// This command will return the Receiver Gain Mode value stored in the reader.
        /// Gain = 0 (Low Gain Mode – Short range reader)
        /// Gain = 1 (High Gain Mode – Long range reader)
        /// </summary>
        ///  <param name="nodeId">Node Id</param>
        /// <returns>Gain Mode</returns>
        /// <example>
        /// <code>
        /// 	void ExecuteGetReceiverGain(IWTRFReader paramReader)
        ///		{
        ///			GainMode gainMode;
        ///			gainMode = paramReader.GetReceiverGain();
        ///		}
        /// </code>
        /// </example>
        GainMode GetReceiverGain(int nodeId);
        /// <summary>
        /// This command will return the Receiver Gain Mode value stored in the reader.
        /// Gain = 0 (Low Gain Mode – Short range reader)
        /// Gain = 1 (High Gain Mode – Long range reader)
        /// </summary>
        ///  <param name="nodeId">Node Id</param>
        /// <param name="timeout">Time Out</param>
        /// <returns>Gain Mode</returns>
        /// <example>
        /// <code>
        /// 	void ExecuteGetReceiverGain(IWTRFReader paramReader)
        ///		{
        ///			GainMode gainMode;
        ///			int timeout =5000;	
        ///			gainMode = paramReader.GetReceiverGain(timeout);
        ///		}
        /// </code>
        /// </example>
        GainMode GetReceiverGain(int nodeId, int timeout);
        /// <summary>
        /// This command will return the Receiver Gain Mode value stored in the reader.
        /// Gain = 0 (Low Gain Mode – Short range reader)
        /// Gain = 1 (High Gain Mode – Long range reader)
        /// </summary>
        ///  <param name="nodeId">Node Id</param>
        /// <param name="timeout">Time Out</param>
        /// <param name="cmdByteArray">Command Byte Array</param>
        /// <param name="rspByteArray">Response from Reader</param>
        /// <returns>Gain Mode</returns>
        /// <example>
        /// <code>
        /// 	void ExecuteGetReceiverGain(IWTRFReader paramReader)
        ///		{
        ///			GainMode gainMode;
        ///			int timeout =5000;	
        ///			byte[] cmdByteArray = null;
        ///			byte[] rspByteArray = null;
        ///			gainMode = paramReader.GetReceiverGain(timeout,cmdByteArray,rspByteArray);
        ///		}
        /// </code>
        /// </example>
        GainMode GetReceiverGain(int nodeId, int timeout, out byte[] cmdByteArray, out byte[] rspByteArray);

        /// <summary>
        /// This command will filter out tags with a specific Alarm condition. The function sets a status value in the
        /// Data EEPROM section of the reader.
        /// 
        /// Status = 0 - Report all tags
        /// Status = 1 - Report only tags with an Alarm condition
        /// Status = 2 - Report only tags without any Alarm condition
        /// </summary>
        ///  <param name="nodeId">Node Id</param>
        /// <param name="tagfilterstatus">Tag Filter Status</param>
        /// <example>
        /// <code>
        /// 	void ExecuteSetAlarmFilter(IWTRFReader paramReader)
        ///		{
        ///			TagFilterStatus tagfilterstatus = TagFilterStatus.All;
        ///			paramReader.SetAlarmFilter(tagfilterstatus);
        ///		}
        /// </code>
        /// </example>
        void SetAlarmFilter(int nodeId, TagFilterStatus tagfilterstatus);
        /// <summary>
        /// This command will filter out tags with a specific Alarm condition. The function sets a status value in the
        /// Data EEPROM section of the reader.
        /// 
        /// Status = 0 - Report all tags
        /// Status = 1 - Report only tags with an Alarm condition
        /// Status = 2 - Report only tags without any Alarm condition
        /// </summary>
        ///  <param name="nodeId">Node Id</param>
        /// <param name="tagfilterstatus">Tag Filter Status</param>
        /// <param name="timeout">Time Out</param>
        /// <example>
        /// <code>
        /// 	void ExecuteSetAlarmFilter(IWTRFReader paramReader)
        ///		{
        ///			TagFilterStatus tagfilterstatus = TagFilterStatus.All;
        ///			int timeout =5000;	
        ///			paramReader.SetAlarmFilter(tagfilterstatus,timeout);
        ///		}
        /// </code>
        /// </example>
        void SetAlarmFilter(int nodeId, TagFilterStatus tagfilterstatus, int timeout);
        /// <summary>
        /// This command will filter out tags with a specific Alarm condition. The function sets a status value in the
        /// Data EEPROM section of the reader.
        /// 
        /// Status = 0 - Report all tags
        /// Status = 1 - Report only tags with an Alarm condition
        /// Status = 2 - Report only tags without any Alarm condition
        /// </summary>
        ///  <param name="nodeId">Node Id</param>
        /// <param name="tagfilterstatus">Tag Filter Status</param>
        /// <param name="timeout">Time Out</param>
        /// <param name="cmdByteArray">Command Byte Array</param>
        /// <param name="rspByteArray">Response from Reader</param>
        /// <example>
        /// <code>
        /// 	void ExecuteSetAlarmFilter(IWTRFReader paramReader)
        ///		{
        ///			TagFilterStatus tagfilterstatus = TagFilterStatus.All;
        ///			int timeout =5000;	
        ///			byte[] cmdByteArray = null;
        ///			byte[] rspByteArray = null;
        ///			paramReader.SetAlarmFilter(tagfilterstatus,timeout,cmdByteArray,rspByteArray);
        ///		}
        /// </code>
        /// </example>
        void SetAlarmFilter(int nodeId, TagFilterStatus tagfilterstatus, int timeout,
                            out byte[] cmdByteArray, out byte[] rspByteArray);

        /// <summary>
        /// This command will return the current Alarm tag filter status value stored in the reader.
        /// Status = 0 - Report all tags
        /// Status = 1 - Report only tags with an Alarm condition
        /// Status = 2 - Report only tags without any Alarm condition
        /// </summary>
        /// <param name="nodeId">Node Id</param>
        /// <returns>Tag Filter Status</returns>
        /// <example>
        /// <code>
        /// 	void ExecuteGetAlarmFilter(IWTRFReader paramReader)
        ///		{
        ///			TagFilterStatus tagfilterstatus ;
        ///			tagfilterstatus = paramReader.GetAlarmFilter();
        ///		}
        /// </code>
        /// </example>
        TagFilterStatus GetAlarmFilter(int nodeId);
        /// <summary>
        /// This command will return the current Alarm tag filter status value stored in the reader.
        /// Status = 0 - Report all tags
        /// Status = 1 - Report only tags with an Alarm condition
        /// Status = 2 - Report only tags without any Alarm condition
        /// </summary>.
        /// <param name="nodeId">Node Id</param>
        /// <param name="timeout">Time Out</param>
        /// <returns>Tag Filter Status</returns>
        /// <example>
        /// <code>
        /// 	void ExecuteGetAlarmFilter(IWTRFReader paramReader)
        ///		{
        ///			TagFilterStatus tagfilterstatus ;
        ///			int timeout =5000;	
        ///			tagfilterstatus = paramReader.GetAlarmFilter(timeout);
        ///		}
        /// </code>
        /// </example>
        TagFilterStatus GetAlarmFilter(int nodeId, int timeout);
        /// <summary>
        /// This command will return the current Alarm tag filter status value stored in the reader.
        /// Status = 0 - Report all tags
        /// Status = 1 - Report only tags with an Alarm condition
        /// Status = 2 - Report only tags without any Alarm condition
        /// </summary>
        /// <param name="nodeId">Node Id</param>
        /// <param name="timeout">Time Out</param>
        /// <param name="cmdByteArray">Command Byte Array</param>
        /// <param name="rspByteArray">Response from Reader</param>
        /// <returns>Tag Filter Status</returns>
        /// <example>
        /// <code>
        /// 	void ExecuteGetAlarmFilter(IWTRFReader paramReader)
        ///		{
        ///			TagFilterStatus tagfilterstatus ;
        ///			int timeout =5000;	
        ///			byte[] cmdByteArray = null;
        ///			byte[] rspByteArray = null;
        ///			tagfilterstatus = paramReader.GetAlarmFilter(timeout,cmdByteArray,rspByteArray);
        ///		}
        /// </code>
        /// </example>
        TagFilterStatus GetAlarmFilter(int nodeId, int timeout, out byte[] cmdByteArray, out byte[] rspByteArray);

        /// <summary>
        /// This command will return the number of Invalid Tags received by the RF module since the last read. This
        /// data is calculated by the RF Module and is a direct interpretation of tag collisions or read failures. This
        /// is a 2-byte value.
        /// </summary>
        /// <param name="nodeId">Node Id</param>
        /// <returns>Number invalid tags</returns>
        /// <example>
        /// <code>
        /// 	void ExecuteGetInvalidTagCount(IWTRFReader paramReader)
        ///		{
        ///			int invalidTags ;
        ///			invalidTags = paramReader.GetInvalidTagCount();
        ///		}
        /// </code>
        /// </example>
        int GetInvalidTagCount(int nodeId);
        /// <summary>
        /// This command will return the number of Invalid Tags received by the RF module since the last read. This
        /// data is calculated by the RF Module and is a direct interpretation of tag collisions or read failures. This
        /// is a 2-byte value.
        /// </summary>
        /// <param name="nodeId">Node Id</param>
        /// <param name="timeout">Time Out</param>
        /// <returns>Number invalid tags</returns>
        /// <example>
        /// <code>
        /// 	void ExecuteGetInvalidTagCount(IWTRFReader paramReader)
        ///		{
        ///			int invalidTags ;
        ///			int timeout =5000;	
        ///			invalidTags = paramReader.GetInvalidTagCount(timeout);
        ///		}
        /// </code>
        /// </example>
        int GetInvalidTagCount(int nodeId, int timeout);
        /// <summary>
        /// This command will return the number of Invalid Tags received by the RF module since the last read. This
        /// data is calculated by the RF Module and is a direct interpretation of tag collisions or read failures. This
        /// is a 2-byte value.
        /// </summary>
        /// <param name="nodeId">Node Id</param>
        /// <param name="timeout">Time Out</param>
        /// <param name="cmdByteArray">Command Byte Array</param>
        /// <param name="rspByteArray">Response from Reader</param>
        /// <returns>Number invalid tags</returns>
        /// <example>
        /// <code>
        /// 	void ExecuteGetInvalidTagCount(IWTRFReader paramReader)
        ///		{
        ///			int invalidTags ;
        ///			int timeout =5000;	
        ///			byte[] cmdByteArray = null;
        ///			byte[] rspByteArray = null;
        ///			invalidTags = paramReader.GetInvalidTagCount(timeout,cmdByteArray,rspByteArray);
        ///		}
        /// </code>
        /// </example>
        int GetInvalidTagCount(int nodeId, int timeout, out byte[] cmdByteArray, out byte[] rspByteArray);

        /// <summary>
        /// This command will return the voltage of the power supply at this reader. It is a single byte and represents
        /// the power in 0.1 voltage increments. Eg. Value 131 = 13.1 Volts
        /// </summary>
        /// <param name="nodeId">Node Id</param>
        /// <returns>Voltage</returns>
        /// <example>
        /// <code>
        /// 	void ExecuteGetSupplyVoltageReply(IWTRFReader paramReader)
        ///		{
        ///			float voltage ;
        ///			voltage = paramReader.GetSupplyVoltageReply();
        ///		}
        /// </code>
        /// </example>
        float GetSupplyVoltageReply(int nodeId);
        /// <summary>
        /// This command will return the voltage of the power supply at this reader. It is a single byte and represents
        /// the power in 0.1 voltage increments. Eg. Value 131 = 13.1 Volts
        /// </summary>
        /// <param name="nodeId">Node Id</param>
        /// <param name="timeout">Time Out</param>
        /// <returns>Voltage</returns>
        /// <example>
        /// <code>
        /// 	void ExecuteGetSupplyVoltageReply(IWTRFReader paramReader)
        ///		{
        ///			float voltage ;
        ///			int timeout =5000;	
        ///			voltage = paramReader.GetSupplyVoltageReply(timeout);
        ///		}
        /// </code>
        /// </example>
        float GetSupplyVoltageReply(int nodeId, int timeout);
        /// <summary>
        /// This command will return the voltage of the power supply at this reader. It is a single byte and represents
        /// the power in 0.1 voltage increments. Eg. Value 131 = 13.1 Volts
        /// </summary>
        /// <param name="nodeId">Node Id</param>
        /// <param name="timeout">Time Out</param>
        /// <param name="cmdByteArray">Command Byte Array</param>
        /// <param name="rspByteArray">Response from Reader</param>
        /// <returns>Voltage</returns>
        /// <example>
        /// <code>
        /// 	void ExecuteGetSupplyVoltageReply(IWTRFReader paramReader)
        ///		{
        ///			float voltage ;
        ///			int timeout =5000;	
        ///			byte[] cmdByteArray = null;
        ///			byte[] rspByteArray = null;
        ///			voltage = paramReader.GetSupplyVoltageReply(timeout,cmdByteArray,rspByteArray);
        ///		}
        /// </code>
        /// </example>
        float GetSupplyVoltageReply(int nodeId, int timeout, out byte[] cmdByteArray, out byte[] rspByteArray);

        /// <summary>
        /// This command will set the reader into an evaluation mode in order to calculate the environmental white
        /// noise level at 433.92 MHz. The unit will remain in evaluation mode for a time period of 40 seconds. During
        /// this period no tag transmissions will be decoded. Once the calculation has been completed, the
        /// reader will resume normal operation.
        /// </summary>
        /// <param name="nodeId">Node Id</param>
        /// <example>
        /// <code>
        /// 	void ExecuteStartRFWhiteNoiseCalculation(IWTRFReader paramReader)
        ///		{
        ///			
        ///			int timeout =5000;	
        ///			byte[] cmdByteArray = null;
        ///			byte[] rspByteArray = null;
        ///			paramReader.StartRFWhiteNoiseCalculation(timeout,cmdByteArray,rspByteArray);
        ///		}
        /// </code>
        /// </example>
        void StartRFWhiteNoiseCalculation(int nodeId);
        /// <summary>
        /// This command will set the reader into an evaluation mode in order to calculate the environmental white
        /// noise level at 433.92 MHz. The unit will remain in evaluation mode for a time period of 40 seconds. During
        /// this period no tag transmissions will be decoded. Once the calculation has been completed, the
        /// reader will resume normal operation.
        /// </summary>
        /// <param name="nodeId">Node Id</param>
        /// <param name="timeout">Time Out</param>
        /// <example>
        /// <code>
        /// 	void ExecuteStartRFWhiteNoiseCalculation(IWTRFReader paramReader)
        ///		{
        ///			
        ///			int timeout =5000;	
        ///			paramReader.StartRFWhiteNoiseCalculation(timeout);
        ///		}
        /// </code>
        /// </example>
        void StartRFWhiteNoiseCalculation(int nodeId, int timeout);
        /// <summary>
        /// This command will set the reader into an evaluation mode in order to calculate the environmental white
        /// noise level at 433.92 MHz. The unit will remain in evaluation mode for a time period of 40 seconds. During
        /// this period no tag transmissions will be decoded. Once the calculation has been completed, the
        /// reader will resume normal operation.
        /// </summary>
        /// <param name="nodeId">Node Id</param>
        /// <param name="timeout">Time Out</param>
        /// <param name="cmdByteArray">Command Byte Array</param>
        /// <param name="rspByteArray">Response from Reader</param>
        /// <example>
        /// <code>
        /// 	void ExecuteStartRFWhiteNoiseCalculation(IWTRFReader paramReader)
        ///		{
        ///			
        ///			int timeout =5000;	
        ///			byte[] cmdByteArray = null;
        ///			byte[] rspByteArray = null;
        ///			paramReader.StartRFWhiteNoiseCalculation(timeout,cmdByteArray,rspByteArray);
        ///		}
        /// </code>
        /// </example>
        void StartRFWhiteNoiseCalculation(int nodeId, int timeout, out byte[] cmdByteArray, out byte[] rspByteArray);

        /// <summary>
        /// This command will retrieve the calculated value (between 0 and 255) of the environmental white noise
        /// level. Take note that this command can only follow after the Start Environmental Noise Level Value Calculation.
        /// If a command is send down to the unit, while still in evaluation mode, the reader will cancel the
        /// calculation process, reset and continue normal operation.
        /// </summary>
        /// <param name="nodeId">Node Id</param>
        /// <returns>Calculated value(between 0 to 255) of environmental white noise</returns>
        /// <example>
        /// <code>
        /// 	void ExecuteGetRFWhiteNoise(IWTRFReader paramReader)
        ///		{
        ///			byte whiteNoise ;
        ///			whiteNoise = paramReader.GetRFWhiteNoise();
        ///		}
        /// </code>
        /// </example>
        byte GetRFWhiteNoise(int nodeId);
        /// <summary>
        /// This command will retrieve the calculated value (between 0 and 255) of the environmental white noise
        /// level. Take note that this command can only follow after the Start Environmental Noise Level Value Calculation.
        /// If a command is send down to the unit, while still in evaluation mode, the reader will cancel the
        /// calculation process, reset and continue normal operation.
        /// </summary>
        /// <param name="nodeId">Node Id</param>
        /// <param name="timeout">Time Out</param>
        /// <returns>Calculated value(between 0 to 255) of environmental white noise</returns>
        /// <example>
        /// <code>
        /// 	void ExecuteGetRFWhiteNoise(IWTRFReader paramReader)
        ///		{
        ///			byte whiteNoise ;
        ///			int timeout =5000;	
        ///			whiteNoise = paramReader.GetRFWhiteNoise(timeout);
        ///		}
        /// </code>
        /// </example>
        byte GetRFWhiteNoise(int nodeId, int timeout);
        /// <summary>
        /// This command will retrieve the calculated value (between 0 and 255) of the environmental white noise
        /// level. Take note that this command can only follow after the Start Environmental Noise Level Value Calculation.
        /// If a command is send down to the unit, while still in evaluation mode, the reader will cancel the
        /// calculation process, reset and continue normal operation.
        /// </summary>
        /// <param name="nodeId">Node Id</param>
        /// <param name="timeout">Time Out</param>
        /// <param name="cmdByteArray">Command Byte Array</param>
        /// <param name="rspByteArray">Response from Reader</param>
        /// <returns>Calculated value(between 0 to 255) of environmental white noise</returns>
        /// <example>
        /// <code>
        /// 	void ExecuteGetRFWhiteNoise(IWTRFReader paramReader)
        ///		{
        ///			byte whiteNoise ;
        ///			int timeout =5000;	
        ///			byte[] cmdByteArray = null;
        ///			byte[] rspByteArray = null;
        ///			whiteNoise = paramReader.GetRFWhiteNoise(timeout,cmdByteArray,rspByteArray);
        ///		}
        /// </code>
        /// </example>
        byte GetRFWhiteNoise(int nodeId, int timeout, out byte[] cmdByteArray, out byte[] rspByteArray);


        /// <summary>
        /// This command will return the Receiver Version Information. These include
        /// CFV - Controller Firmware Version
        ///	RFV - RF Module Firmware Version
        ///	CHV - Controller Hardware Version
        ///	RHV - RF Module Hardware Version
        /// </summary>
        /// <param name="nodeId">Node Id</param>
        /// <param name="CFV">Controller Firmware Version</param>
        /// <param name="RFV">RF Module Firmware Version</param>
        /// <param name="CHV">Controller Hardware Version</param>
        /// <param name="RHV">RF Module Hardware Version</param>
        /// <example>
        /// <code>
        /// 	void ExecuteGetVersionInformation(IWTRFReader paramReader)
        ///		{
        ///         float CFV;
        ///			float RFV;
        ///			float CHV;
        ///			float RHV;
        ///			paramReader.GetVersionInformation(out CFV,out RFV,out CHV,out RHV);
        ///		}
        /// </code>
        /// </example>
        void GetVersionInformation(int nodeId, out float CFV, out float RFV, out float CHV, out float RHV);
        /// <summary>
        /// This command will return the Receiver Version Information. These include
        /// CFV - Controller Firmware Version
        ///	RFV - RF Module Firmware Version
        ///	CHV - Controller Hardware Version
        ///	RHV - RF Module Hardware Version
        /// </summary>
        /// <param name="nodeId">Node Id</param>
        /// <param name="CFV">Controller Firmware Version</param>
        /// <param name="RFV">RF Module Firmware Version</param>
        /// <param name="CHV">Controller Hardware Version</param>
        /// <param name="RHV">RF Module Hardware Version</param>
        /// <param name="timeout">Time Out</param>
        /// <example>
        /// <code>
        /// 	void ExecuteGetVersionInformation(IWTRFReader paramReader)
        ///		{
        ///         float CFV;
        ///			float RFV;
        ///			float CHV;
        ///			float RHV;
        ///			int timeout =5000;	
        ///			paramReader.GetVersionInformation(out CFV,out RFV,out CHV,out RHV,timeout);
        ///		}
        /// </code>
        /// </example>
        void GetVersionInformation(int nodeId, out float CFV, out float RFV, out float CHV, out float RHV, int timeout);
        /// <summary>
        /// This command will return the Receiver Version Information. These include
        /// CFV - Controller Firmware Version
        ///	RFV - RF Module Firmware Version
        ///	CHV - Controller Hardware Version
        ///	RHV - RF Module Hardware Version
        /// </summary>
        /// <param name="nodeId">Node Id</param>
        /// <param name="CFV">Controller Firmware Version</param>
        /// <param name="RFV">RF Module Firmware Version</param>
        /// <param name="CHV">Controller Hardware Version</param>
        /// <param name="RHV">RF Module Hardware Version</param>
        /// <param name="timeout">Time Out</param>
        /// <param name="cmdByteArray">Command Byte Array</param>
        /// <param name="rspByteArray">Response from Reader</param>
        /// <example>
        /// <code>
        /// 	void ExecuteGetVersionInformation(IWTRFReader paramReader)
        ///		{
        ///         float CFV;
        ///			float RFV;
        ///			float CHV;
        ///			float RHV;
        ///			int timeout =5000;	
        ///			byte[] cmdByteArray = null;
        ///			byte[] rspByteArray = null;
        ///			paramReader.GetVersionInformation(out CFV,out RFV,out CHV,out RHV,timeout,cmdByteArray,rspByteArray);
        ///		}
        /// </code>
        /// </example>
        void GetVersionInformation(int nodeId, out float CFV, out float RFV, out float CHV, out float RHV, int timeout,
            out byte[] cmdByteArray, out byte[] rspByteArray);

        #endregion Reader Level Command

        #endregion Methods
    }

#if RX201 ||RX201OE || COMPLETE
    /// <summary>
    /// Reader Class for reader attached in RX201 network
    /// WTSerialNetworkReader object will created and maintained by WTRFSerial Object
    /// </summary>
#else
    /// <exclude/>
#endif
    [Serializable]
    public class WTSerialNetworkReader
    {
        int m_NetworkId;
        int m_ReaderId;
        int m_NodeId;
        bool m_Status = true;
        WTReaderModel m_ReaderModel = WTReaderModel.RX201;
        string m_LastCmdExecuted;
        DateTime m_LastCmdSentAt;
        DateTime m_LastResponseRecdAt;

        #region Properties
        /// <summary>
        /// Gets Network Id of WTSerialNetworkReader
        /// </summary>
        public int NetworkId
        {
            get
            {
                return m_NetworkId;
            }

        }
        public int SetNetworkId
        {
            set
            {
                m_NetworkId = value;
            }
        }
        /// <summary>
        /// Gets Reader Id of WTSerialNetworkReader
        /// </summary>
        public int ReaderId
        {
            get
            {
                return m_ReaderId;
            }

        }
        public int SetReaderId
        {
            set
            {
                m_ReaderId = value;
            }
        }
        /// <summary>
        /// Gets Node Id of WTSerialNetworkReader
        /// </summary>
        public int NodeId
        {
            get
            {
                return m_NodeId;
            }

        }
        public int SetNodeId
        {
            set
            {
                m_NodeId = value;
            }
        }
        /// <summary>
        /// Gets Status if reader on network
        /// </summary>
        public bool Online
        {
            get
            {
                return m_Status;
            }

        }
        public bool SetOnline
        {
            set
            {
                m_Status = value;
            }
        }
        /// <summary>
        /// Gets Last command executed name
        /// </summary>
        public string LastCommandExecuted
        {
            get
            {
                return m_LastCmdExecuted;
            }

        }
        public string SetLastCommandExecuted
        {
            set
            {
                m_LastCmdExecuted = value;
            }
        }

        /// <summary>
        /// Gets time stamp of last command executed.
        /// </summary>
        public WTReaderModel ReaderModel
        {
            get
            {
                return m_ReaderModel;
            }
            set
            {
                m_ReaderModel = value;
            }

        }


        /// <summary>
        /// Gets time stamp of last command executed.
        /// </summary>
        public DateTime LastCommandExecutedTimeStamp
        {
            get
            {
                return m_LastCmdSentAt;
            }

        }
        public DateTime SetLastCommandExecutedTimeStamp
        {
            set
            {
                m_LastCmdSentAt = value;
            }
        }
        /// <summary>
        /// Gets time stamp of last response received.
        /// </summary>
        public DateTime LastTimeResponseReceived
        {
            get
            {
                return m_LastResponseRecdAt;
            }

        }
        public DateTime SetLastTimeResponseReceived
        {
            set
            {
                m_LastResponseRecdAt = value;
            }
        }

        #endregion Properties

        /// <summary>
        /// Constructor for WTSerialNetworkReader Class to instantiate new object of a reader attached in RX201 network
        /// </summary>
        /// <param name="NetworkId">Network Id </param>
        /// <param name="ReaderId">Reader Id</param>
        /// <param name="NodeId">Node Id</param>
        public WTSerialNetworkReader(int networkId, int readerId, int nodeId)
        {
            m_ReaderId = readerId;
            m_NodeId = nodeId;
            m_NetworkId = networkId;
        }

    }
}
