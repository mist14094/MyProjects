using System;
using System.Collections;
using System.Collections.Generic;

namespace KTone.Core.KTIRFID
{
	#region Enums
	
	/// <summary>
	/// Defines tag filter status of the reader
	/// </summary>
	public enum  TagFilterStatus
	{
		/// <summary>
		/// Report all tags
		/// </summary>
		All ,
		/// <summary>
		/// Report only tags with an Alarm condition
		/// </summary>
		WithAlarmCond ,
		/// <summary>
		/// Report only tags without any Alarm condition
		/// </summary>
		WithoutAlarmCond 

	}

	/// <summary>
	/// Defines tag input/output line status of the reader
	/// </summary>
	public enum IOStatus
	{
		/// <summary>
		/// Open
		/// </summary>
		OPEN = 0,

		/// <summary>
		/// Close
		/// </summary>
		CLOSE = 1
	}

	/// <summary>
	/// Defines input/output pin numbers of the reader
	/// </summary>
	public enum PinNumber
	{
		/// <summary>
		/// First Pin : Pin0
		/// </summary>
		FIRST = 0,

		/// <summary>
		/// Second Pin : Pin1
		/// </summary>
		SECOND = 1
	}


	/// <summary>
	/// Defines the gain mode of the reader
	/// </summary>
	public enum GainMode
	{
		/// <summary>
		/// Low gain
		/// </summary>
		Lowgain  =0x00,

		/// <summary>
		/// High gain
		/// </summary>
		Highgain =0x01
	}

	/// <summary>
	/// Defines the pattern of tags to be collected in the Auto polling mode
	/// </summary>
	public enum TagListType
	{
		/// <summary>
		/// Returns tags present in current cycle
		/// </summary>
		Current,
		
		/// <summary>
		/// Returns tags present in current cycle and previous cyle. 
		/// </summary>
		CurrentAndRemoved
	}

	/// <summary>
	/// Defines Reader Autopolling mode
	/// </summary>
	public enum AutoPollMode
	{
		/// <summary>
		/// Autopolling mode off
		/// </summary>
		Disabled = 0x00,

		/// <summary>
		/// Autopolling mode on
		/// </summary>
		Enabled = 0x01
	}
    /// <summary>
    /// Baud Rate which Serial Comm based readers supports
    /// </summary>
    public enum BaudRate
    {
        /// <summary>
        /// 115200
        /// </summary>
        BAUD115200 = 0x00,
        /// <summary>
        /// 57600
        /// </summary>
        BAUD57600,
        /// <summary>
        /// 38400
        /// </summary>
        BAUD38400,
        /// <summary>
        /// 19200
        /// </summary>
        BAUD19200,
        /// <summary>
        /// 9600
        /// </summary>
        BAUD9600
    }

    /// <summary>
    /// Reader Models 
    /// </summary>
    public enum WTReaderModel
    {
        /// <summary>
        /// L series RX900 reader
        /// </summary>
        LRX900 = 0x00,
        /// <summary>
        /// L series RX300 reader
        /// </summary>
        LRX300 = 0x01,
        /// <summary>
        /// RX201 readers network
        /// </summary>
        RX201 = 0x02,
        /// <summary>
        /// W series RX300 reader
        /// </summary>
        
        WRX300 = 0x03,
        /// <summary>
        /// W series RX900 reader
        /// </summary>
        WRX900 = 0x04,
        /// <summary>
        /// W series RX1000 reader
        /// </summary>
        WRX1000 = 0x05,
        /// <summary>
        /// L series RX1000 reader
        /// </summary>
        LRX1000 = 0x06,
        /// <summary>
        /// RX201 Readers Network over Ethernet.
        /// </summary>
        RX201OverEthernet = 0x07,
        /// <summary>
        /// W-PS300 Tracking Power Supply Unit
        /// </summary>
        WPS300 = 0x08,

        /// <summary>
        /// W series LRX201 reader
        /// </summary>
        LRX201 = 0x09,
        /// <summary>
        /// W series WRX201 reader
        /// </summary>
        WRX201 = 0x10,

        /// <summary>
        /// W series LRX201 reader over GPRS
        /// </summary>
        LRX201_GPRS = 0x11,
        /// <summary>
        /// W series WRX201 reader over GPRS
        /// </summary>
        WRX201_GPRS = 0x12,

        /// <summary>
        /// W series GPRS Listener
        /// </summary>
        WTGPRSListenerWSeries,

        /// <summary>
        /// W series 201 reader
        /// </summary>
        WRX201GPRS,

        /// <summary>
        /// L series GPRS Listener
        /// </summary>
        WTGPRSListenerLSeries,

        /// <summary>
        ///  L series 201 reader
        /// </summary>
        LRX201GPRS,

        /// <summary>
        /// L series LRX2100 reader
        /// </summary>
        LRM2100,

        /// <summary>
        /// W series LRX2100 reader
        /// </summary>
        WRM2100,
        
    }
	#endregion Enums

	#region Interface
#if RX900 || RX1000 || COMPLETE
    /// <summary>
	/// Exposes methods and events to communicate with Wavetrend reader.
	/// Exposes properties to control Wavetrend reader.
	/// </summary>
#else
    /// <exclude/>
    #endif   
    public interface IWTRFReader : IDisposable
    {
        #region Properties
        /// <summary>
        /// Gets Reader Version Information
        /// </summary>
        ReaderVersionInfo ReaderVersion
        {
            get;
        }
        #endregion Properties


        #region Events
        

        /// <summary>
        /// Event raised when the response packet 
        /// indicating reader status is received from the reader 
        /// Data is passed using WTReaderStatusMonitorEventArgs
        /// </summary>
        event EventHandler<WTReaderEventArgs> OnWTReaderStatusMonitor;

        /// <summary>
        /// Event raised when the Heart Beat message is received from the reader.
        /// Data is passed using WTReaderHeartBeatEventArgs
        /// </summary>
        event EventHandler<WTReaderEventArgs> OnWTReaderHeartBeat;
        #endregion Events

        #region Methods
        /// <summary>
        /// Connects with the ehternet reader 
        /// </summary>
        /// <example>
        /// <code>
        /// 	void ExecuteConnect(IWTRFReader paramReader)
        ///		{
        ///			paramReader.Connect();
        ///		}
        /// </code>
        /// </example>
        void WTConnect();

        /// <summary>
        /// Disconnects the ehternet reader 
        /// </summary>
        /// <example>
        /// <code>
        /// 	void ExecuteDisconnect(IWTRFReader paramReader)
        ///		{
        ///			paramReader.Disconnect();
        ///		}
        /// </code>
        /// </example>
        void Disconnect();

        /// <summary>
        /// The function of this command is to set the reader into an Automatic Polling mode. It sets the Auto Polling
        ///	flag in the Data EEPROM of the reader to enable Auto Polling after power up.
        /// This command is used to restart the Auto Polling if it has been stopped by a Disable Auto Polling command.
        /// In this mode the reader continuously sends out data packets if received.
        /// </summary>
        /// <param name="timeInterval">Polling Cycle</param>
        /// <param name="taglistType">Tag List Type, it can be Current or Current and Removed</param>
        /// <example>
        /// <code>
        /// 	void ExecuteEnableAutoPolling(IWTRFReader paramReader)
        ///		{
        ///			int timeInterval=2000;
        ///			TagListType taglistType = TagListType.CurrentAndRemoved;
        ///			paramReader.EnableAutoPolling(timeInterval,taglistType);
        ///		}
        /// </code>
        ///  </example>
        void EnableAutoPolling(int timeInterval/*, TagListType taglistType*/);
        /// <summary>
        /// The function of this command is to set the reader into an Automatic Polling mode. It sets the Auto Polling
        ///	flag in the Data EEPROM of the reader to enable Auto Polling after power up.
        /// This command is used to restart the Auto Polling if it has been stopped by a Disable Auto Polling command.
        /// In this mode the reader continuously sends out data packets if received.
        /// </summary>
        /// <param name="timeInterval">Polling Cycle</param>
        /// <param name="taglistType">Tag List Type, it can be Current or Current and Removed</param>
        ///  <param name="timeout">Time Out</param>
        /// <example>
        /// <code>
        /// 	void ExecuteEnableAutoPolling(IWTRFReader paramReader)
        ///		{
        ///			int timeout =5000;
        ///			int timeInterval=2000;
        ///			TagListType taglistType = TagListType.CurrentAndRemoved;
        ///			paramReader.EnableAutoPolling(timeInterval,taglistType,timeout);
        ///		}
        /// </code>
        ///  </example>
        void EnableAutoPolling(int timeInterval/*, TagListType taglistType*/, int timeout);
        /// <summary>
        /// The function of this command is to set the reader into an Automatic Polling mode. It sets the Auto Polling
        ///	flag in the Data EEPROM of the reader to enable Auto Polling after power up.
        /// This command is used to restart the Auto Polling if it has been stopped by a Disable Auto Polling command.
        /// In this mode the reader continuously sends out data packets if received.
        /// </summary>
        /// <param name="timeInterval">Polling Cycle</param>
        /// <param name="taglistType">Tag List Type, it can be Current or Current and Removed</param>
        /// <param name="timeout">Time Out</param>
        /// <param name="cmdByteArray">Command Byte Array</param>
        ///  <param name="rspByteArray">Response from Reader</param>
        /// <example>
        /// <code>
        /// 	void ExecuteEnableAutoPolling(IWTRFReader paramReader)
        ///		{
        ///			int timeout =5000;
        ///			int timeInterval=2000;
        ///			TagListType taglistType = TagListType.CurrentAndRemoved;
        ///			byte[] cmdByteArray = null;
        ///			byte[] rspByteArray = null;
        ///			paramReader.EnableAutoPolling(timeInterval,taglistType,timeout,
        ///											out cmdByteArray,out rspByteArray);
        ///		}
        /// </code>
        ///  </example>
        void EnableAutoPolling(int timeInterval,/* TagListType taglistType,*/int timeout,
                                out byte[] cmdByteArray, out byte[] rspByteArray);

        /// <summary>
        /// The function of this command is to disable the Auto Polling feature after power up by resetting the Auto
        /// Polling flag in the Data EEPROM of the reader. This command can be addressed directly to reader 1, or
        /// on a broadcast basis.
        /// </summary>
        /// <param name="timeout">Time Out</param>
        /// <example>
        /// <code>
        /// 	void ExecuteDisableAutoPolling(IWTRFReader paramReader)
        ///		{
        ///			int timeout =5000;
        ///			paramReader.DisableAutoPolling(timeout);
        ///		}
        /// </code>
        ///  </example>
        void DisableAutoPolling(int timeout);
        /// <summary>
        /// The function of this command is to disable the Auto Polling feature after power up by resetting the Auto
        /// Polling flag in the Data EEPROM of the reader. This command can be addressed directly to reader 1, or
        /// on a broadcast basis.
        /// </summary>
        /// <param name="timeout">Time Out</param>
        /// <param name="cmdByteArray">Command Byte Array</param>
        /// <param name="rspByteArray">Response from Reader</param>
        /// <example>
        /// <code>
        /// 	void ExecuteDisableAutoPolling(IWTRFReader paramReader)
        ///		{
        ///			int timeout =5000;
        ///			byte[] cmdByteArray = null;
        ///			byte[] rspByteArray = null;
        ///			paramReader.DisableAutoPolling(timeout,out cmdByteArray,out rspByteArray);
        ///		}
        /// </code>
        ///  </example>
        void DisableAutoPolling(int timeout, out byte[] cmdByteArray, out byte[] rspByteArray);

        /// <summary>
        /// The Ping Command is simply used to check if a reader is online and responding correctly. It can be
        /// used to read back Network ID's, Reader ID's and Node ID's. Inserted into the response from a Ping
        /// Command is an Error Number. This number refers to the last error the respective reader has experienced.
        /// Once read, this number is cleared.
        /// </summary>
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
        void PingReader(out byte networkID, out byte receiverID,
                                out byte nodeID, out WTReaderErrors errorCode);
        /// <summary>
        /// The Ping Command is simply used to check if a reader is online and responding correctly. It can be
        /// used to read back Network ID's, Reader ID's and Node ID's. Inserted into the response from a Ping
        /// Command is an Error Number. This number refers to the last error the respective reader has experienced.
        /// Once read, this number is cleared.
        /// </summary>
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
        void PingReader(out byte networkID, out byte receiverID,
                        out byte nodeID, out WTReaderErrors errorCode, int timeoutMS);
        /// <summary>
        /// The Ping Command is simply used to check if a reader is online and responding correctly. It can be
        /// used to read back Network ID's, Reader ID's and Node ID's. Inserted into the response from a Ping
        /// Command is an Error Number. This number refers to the last error the respective reader has experienced.
        /// Once read, this number is cleared.
        /// </summary>
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
        void PingReader(out byte networkID, out byte receiverID,
                    out byte nodeID, out WTReaderErrors errorCode,
                    int timeoutMS, out byte[] cmdByteArray, out byte[] respByteArray);

        /// <summary>
        /// The function of this command is to assign the Reader ID as well as commit it to the Data EEPROM of
        /// the reader.
        /// </summary>
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
        void SetReaderID(byte newReaderID);
        /// <summary>
        /// The function of this command is to assign the Reader ID as well as commit it to the Data EEPROM of
        /// the reader.
        /// </summary>
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

        void SetReaderID(byte newReaderID, int timeout);
        /// <summary>
        /// The function of this command is to assign the Reader ID as well as commit it to the Data EEPROM of
        /// the reader.
        /// </summary>
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

        void SetReaderID(byte newReaderID, int timeout, out byte[] cmdByteArray, out byte[] rspByteArray);

        /// <summary>
        /// This will be the most commonly used command on any system. Its function is to request a data packet
        /// from the reader which contains the tag data packet if there is one ready for sending. A tag packet is removed
        /// from the readers’ buffer, and returned with this command, making room for a new tag packet.
        /// New tags received by the RF Module are stored in the Reader Buffer and the existing tags are deleted in
        /// a FIFO method in order to keep the data current. If no tag is ready for sending to the PC, an empty
        /// packet is sent back. That is, no data in the Data field.
        /// The reader has a 3 stage buffer which allows for 3 tag data packets to be stored.
        /// </summary>
        /// <returns>WTTag Object</returns>
        /// <example>
        /// <code>
        /// 	void ExecuteGetTagPacket(IWTRFReader paramReader)
        ///		{
        ///			WTTag tag = paramReader.GetTagPacket();
        ///		}
        /// </code>
        /// </example>
        WTTag GetTagPacket();
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
        /// <returns>WTTag Object</returns>
        /// <example>
        /// <code>
        /// 	void ExecuteGetTagPacket(IWTRFReader paramReader)
        ///		{
        ///			byte newReaderID =1;
        ///			int timeout =5000;
        ///			WTTag tag = paramReader.GetTagPacket(timeout);
        ///		}
        /// </code>
        /// </example>
        WTTag GetTagPacket(int timeout);
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
        ///			WTTag tag = paramReader.GetTagPacket(timeout,out cmdByteArray, out rspByteArray);
        ///		}
        /// </code>
        /// </example>
        WTTag GetTagPacket(int timeout, out byte[] cmdByteArray, out byte[] rspByteArray);

        /// <summary>
        /// This command will set the RSSI value and commit it to the Data EEPROM of the reader. It also initiates
        /// an RF Module reset and writes the new value to the RF Module. Broadcasts here are useful to set all the
        /// readers to their most sensitive etc. The RSSI value ranges from 0 to 255 where 0 value being the most
        /// sensitive.
        /// </summary>
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
        void SetRSSIValue(byte RSSIvalue);
        /// <summary>
        /// This command will set the RSSI value and commit it to the Data EEPROM of the reader. It also initiates
        /// an RF Module reset and writes the new value to the RF Module. Broadcasts here are useful to set all the
        /// readers to their most sensitive etc. The RSSI value ranges from 0 to 255 where 0 value being the most
        /// sensitive.
        /// </summary>
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
        void SetRSSIValue(byte RSSIvalue, int timeout);
        /// <summary>
        /// This command will set the RSSI value and commit it to the Data EEPROM of the reader. It also initiates
        /// an RF Module reset and writes the new value to the RF Module. Broadcasts here are useful to set all the
        /// readers to their most sensitive etc. The RSSI value ranges from 0 to 255 where 0 value being the most
        /// sensitive.
        /// </summary>
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
        void SetRSSIValue(byte RSSIvalue, int timeout,
                            out byte[] cmdByteArray, out byte[] rspByteArray);

        /// <summary>
        /// This command will return the RSSI value currently stored in the Data EEPROM section of the reader.
        /// </summary>
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
        byte GetRSSIValue();
        /// <summary>
        /// This command will return the RSSI value currently stored in the Data EEPROM section of the reader.
        /// </summary>
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
        byte GetRSSIValue(int timeout);
        /// <summary>
        /// This command will return the RSSI value currently stored in the Data EEPROM section of the reader.
        /// </summary>
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
        byte GetRSSIValue(int timeout, out byte[] cmdByteArray, out byte[] rspByteArray);

        /// <summary>
        /// The site code is a 3 byte value assigned to Wavetrend Tags. It is a property of the tag that describes a
        /// customer site installation. The Set Site Code function is to store a site code value to the Data
        /// EEPROM of the reader. Once a value is stored in the reader, it will enable the reader to filter out any
        /// tags that it receives that do not correspond to the stored site code value.
        /// When the site code is set to 0 value, the reader will allow ALL tags to be read, hence the reader is said
        /// to be open. Setting a specific site code will result in only tags that have that site code to be read and
        /// reported.
        /// </summary>
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
        void SetSiteCode(uint sitecode);
        /// <summary>
        /// The site code is a 3 byte value assigned to Wavetrend Tags. It is a property of the tag that describes a
        /// customer site installation. The Set Site Code function is to store a site code value to the Data
        /// EEPROM of the reader. Once a value is stored in the reader, it will enable the reader to filter out any
        /// tags that it receives that do not correspond to the stored site code value.
        /// When the site code is set to 0 value, the reader will allow ALL tags to be read, hence the reader is said
        /// to be open. Setting a specific site code will result in only tags that have that site code to be read and
        /// reported.
        /// </summary>
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
        void SetSiteCode(uint sitecode, int timeout);
        /// <summary>
        /// The site code is a 3 byte value assigned to Wavetrend Tags. It is a property of the tag that describes a
        /// customer site installation. The Set Site Code function is to store a site code value to the Data
        /// EEPROM of the reader. Once a value is stored in the reader, it will enable the reader to filter out any
        /// tags that it receives that do not correspond to the stored site code value.
        /// When the site code is set to 0 value, the reader will allow ALL tags to be read, hence the reader is said
        /// to be open. Setting a specific site code will result in only tags that have that site code to be read and
        /// reported.
        /// </summary>
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

        void SetSiteCode(uint sitecode, int timeout, out byte[] cmdByteArray, out byte[] rspByteArray);

        /// <summary>
        /// This command will return the active Site Code stored in the specific reader.
        /// </summary>
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

        uint GetSiteCode();
        /// <summary>
        /// This command will return the active Site Code stored in the specific reader.
        /// </summary>
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
        uint GetSiteCode(int timeout);

        /// <summary>
        /// This command will return the active Site Code stored in the specific reader.
        /// </summary>
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
        uint GetSiteCode(int timeout, out byte[] cmdByteArray, out byte[] rspByteArray);
        
        /// <summary>
        /// This command will set the RF Module into its 2 different gain levels.
        /// Gain = 0 (Low Gain Mode – Short range reader)
        /// Gain = 1 (High Gain Mode – Long range reader)
        /// </summary>
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
        void SetReceiverGain(GainMode gainMode);
        /// <summary>
        /// This command will set the RF Module into its 2 different gain levels.
        /// Gain = 0 (Low Gain Mode – Short range reader)
        /// Gain = 1 (High Gain Mode – Long range reader)
        /// </summary>
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
        void SetReceiverGain(GainMode gainMode, int timeout);
        /// <summary>
        /// This command will set the RF Module into its 2 different gain levels.
        /// Gain = 0 (Low Gain Mode – Short range reader)
        /// Gain = 1 (High Gain Mode – Long range reader)
        /// </summary>
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
        void SetReceiverGain(GainMode gainMode, int timeout,
                                out byte[] cmdByteArray, out byte[] rspByteArray);

        /// <summary>
        /// This command will return the Receiver Gain Mode value stored in the reader.
        /// Gain = 0 (Low Gain Mode – Short range reader)
        /// Gain = 1 (High Gain Mode – Long range reader)
        /// </summary>
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
        GainMode GetReceiverGain();
        /// <summary>
        /// This command will return the Receiver Gain Mode value stored in the reader.
        /// Gain = 0 (Low Gain Mode – Short range reader)
        /// Gain = 1 (High Gain Mode – Long range reader)
        /// </summary>
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
        GainMode GetReceiverGain(int timeout);
        /// <summary>
        /// This command will return the Receiver Gain Mode value stored in the reader.
        /// Gain = 0 (Low Gain Mode – Short range reader)
        /// Gain = 1 (High Gain Mode – Long range reader)
        /// </summary>
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
        GainMode GetReceiverGain(int timeout, out byte[] cmdByteArray, out byte[] rspByteArray);

        /// <summary>
        /// This command will filter out tags with a specific Alarm condition. The function sets a status value in the
        /// Data EEPROM section of the reader.
        /// 
        /// Status = 0 - Report all tags
        /// Status = 1 - Report only tags with an Alarm condition
        /// Status = 2 - Report only tags without any Alarm condition
        /// </summary>
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
        void SetAlarmFilter(TagFilterStatus tagfilterstatus);
        /// <summary>
        /// This command will filter out tags with a specific Alarm condition. The function sets a status value in the
        /// Data EEPROM section of the reader.
        /// 
        /// Status = 0 - Report all tags
        /// Status = 1 - Report only tags with an Alarm condition
        /// Status = 2 - Report only tags without any Alarm condition
        /// </summary>
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
        void SetAlarmFilter(TagFilterStatus tagfilterstatus, int timeout);
        /// <summary>
        /// This command will filter out tags with a specific Alarm condition. The function sets a status value in the
        /// Data EEPROM section of the reader.
        /// 
        /// Status = 0 - Report all tags
        /// Status = 1 - Report only tags with an Alarm condition
        /// Status = 2 - Report only tags without any Alarm condition
        /// </summary>
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
        void SetAlarmFilter(TagFilterStatus tagfilterstatus, int timeout,
                            out byte[] cmdByteArray, out byte[] rspByteArray);

        /// <summary>
        /// This command will return the current Alarm tag filter status value stored in the reader.
        /// Status = 0 - Report all tags
        /// Status = 1 - Report only tags with an Alarm condition
        /// Status = 2 - Report only tags without any Alarm condition
        /// </summary>
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
        TagFilterStatus GetAlarmFilter();
        /// <summary>
        /// This command will return the current Alarm tag filter status value stored in the reader.
        /// Status = 0 - Report all tags
        /// Status = 1 - Report only tags with an Alarm condition
        /// Status = 2 - Report only tags without any Alarm condition
        /// </summary>
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
        TagFilterStatus GetAlarmFilter(int timeout);
        /// <summary>
        /// This command will return the current Alarm tag filter status value stored in the reader.
        /// Status = 0 - Report all tags
        /// Status = 1 - Report only tags with an Alarm condition
        /// Status = 2 - Report only tags without any Alarm condition
        /// </summary>
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
        TagFilterStatus GetAlarmFilter(int timeout, out byte[] cmdByteArray, out byte[] rspByteArray);

        /// <summary>
        /// This command will return the number of Invalid Tags received by the RF module since the last read. This
        /// data is calculated by the RF Module and is a direct interpretation of tag collisions or read failures. This
        /// is a 2-byte value for L series and 1-byte value for W series reader.
        /// </summary>
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
        int GetInvalidTagCount();
        /// <summary>
        /// This command will return the number of Invalid Tags received by the RF module since the last read. This
        /// data is calculated by the RF Module and is a direct interpretation of tag collisions or read failures. This
        /// is a 2-byte value for L series and 1-byte value for W series reader.
        /// </summary>
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
        int GetInvalidTagCount(int timeout);
        /// <summary>
        /// This command will return the number of Invalid Tags received by the RF module since the last read. This
        /// data is calculated by the RF Module and is a direct interpretation of tag collisions or read failures. This
        /// is a 2-byte value for L series and 1-byte value for W series reader.
        /// </summary>
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
        int GetInvalidTagCount(int timeout, out byte[] cmdByteArray, out byte[] rspByteArray);

        /// <summary>
        /// This command will return the voltage of the power supply at this reader. It is a single byte and represents
        /// the power in 0.1 voltage increments. Eg. Value 131 = 13.1 Volts
        /// </summary>
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
        float GetSupplyVoltageReply();
        /// <summary>
        /// This command will return the voltage of the power supply at this reader. It is a single byte and represents
        /// the power in 0.1 voltage increments. Eg. Value 131 = 13.1 Volts
        /// </summary>
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
        float GetSupplyVoltageReply(int timeout);
        /// <summary>
        /// This command will return the voltage of the power supply at this reader. It is a single byte and represents
        /// the power in 0.1 voltage increments. Eg. Value 131 = 13.1 Volts
        /// </summary>
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
        float GetSupplyVoltageReply(int timeout, out byte[] cmdByteArray, out byte[] rspByteArray);

        /// <summary>
        /// This command will set the reader into an evaluation mode in order to calculate the environmental white
        /// noise level at 433.92 MHz. The unit will remain in evaluation mode for a time period of 40 seconds. During
        /// this period no tag transmissions will be decoded. Once the calculation has been completed, the
        /// reader will resume normal operation.
        /// </summary>
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
        void StartRFWhiteNoiseCalculation();
        /// <summary>
        /// This command will set the reader into an evaluation mode in order to calculate the environmental white
        /// noise level at 433.92 MHz. The unit will remain in evaluation mode for a time period of 40 seconds. During
        /// this period no tag transmissions will be decoded. Once the calculation has been completed, the
        /// reader will resume normal operation.
        /// </summary>
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
        void StartRFWhiteNoiseCalculation(int timeout);
        /// <summary>
        /// This command will set the reader into an evaluation mode in order to calculate the environmental white
        /// noise level at 433.92 MHz. The unit will remain in evaluation mode for a time period of 40 seconds. During
        /// this period no tag transmissions will be decoded. Once the calculation has been completed, the
        /// reader will resume normal operation.
        /// </summary>
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
        void StartRFWhiteNoiseCalculation(int timeout, out byte[] cmdByteArray, out byte[] rspByteArray);

        /// <summary>
        /// This command will retrieve the calculated value (between 0 and 255) of the environmental white noise
        /// level. Take note that this command can only follow after the Start Environmental Noise Level Value Calculation.
        /// If a command is send down to the unit, while still in evaluation mode, the reader will cancel the
        /// calculation process, reset and continue normal operation.
        /// </summary>
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
        byte GetRFWhiteNoise();
        /// <summary>
        /// This command will retrieve the calculated value (between 0 and 255) of the environmental white noise
        /// level. Take note that this command can only follow after the Start Environmental Noise Level Value Calculation.
        /// If a command is send down to the unit, while still in evaluation mode, the reader will cancel the
        /// calculation process, reset and continue normal operation.
        /// </summary>
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
        byte GetRFWhiteNoise(int timeout);
        /// <summary>
        /// This command will retrieve the calculated value (between 0 and 255) of the environmental white noise
        /// level. Take note that this command can only follow after the Start Environmental Noise Level Value Calculation.
        /// If a command is send down to the unit, while still in evaluation mode, the reader will cancel the
        /// calculation process, reset and continue normal operation.
        /// </summary>
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
        byte GetRFWhiteNoise(int timeout, out byte[] cmdByteArray, out byte[] rspByteArray);

        /// <summary>
        /// This command will return the Receiver Version Information. These include
        ///	RFV - RF Module Firmware Version
        ///	RHV - RF Module Hardware Version
        /// </summary>
        /// <param name="RFV">RF Module Firmware Version</param>
        /// <param name="RHV">RF Module Hardware Version</param>
        /// <example>
        /// <code>
        /// 	void ExecuteGetVersionInformation(IWTRFReader paramReader)
        ///		{
        ///			float RFV;
        ///			float RHV;
        ///			paramReader.GetVersionInformation(out RFV,out RHV);
        ///		}
        /// </code>
        /// </example>
        void GetVersionInformation(out float RFV, out float RHV);
        /// <summary>
        /// This command will return the Receiver Version Information. These include
        ///	RFV - RF Module Firmware Version
        ///	RHV - RF Module Hardware Version
        /// </summary>
        /// <param name="RFV">RF Module Firmware Version</param>
        /// <param name="RHV">RF Module Hardware Version</param>
        /// <param name="timeout">Time Out</param>
        /// <example>
        /// <code>
        /// 	void ExecuteGetVersionInformation(IWTRFReader paramReader)
        ///		{
        ///			float RFV;
        ///			float RHV;
        ///			int timeout =5000;	
        ///			paramReader.GetVersionInformation(out RFV,out RHV,timeout);
        ///		}
        /// </code>
        /// </example>
        void GetVersionInformation(out float RFV, out float RHV, int timeout);
        /// <summary>
        /// This command will return the Receiver Version Information. These include
        ///	RFV - RF Module Firmware Version
        ///	RHV - RF Module Hardware Version
        /// </summary>
        /// <param name="RFV">RF Module Firmware Version</param>
        /// <param name="RHV">RF Module Hardware Version</param>
        /// <param name="timeout">Time Out</param>
        /// <param name="cmdByteArray">Command Byte Array</param>
        /// <param name="rspByteArray">Response from Reader</param>
        /// <example>
        /// <code>
        /// 	void ExecuteGetVersionInformation(IWTRFReader paramReader)
        ///		{
        ///			float RFV;
        ///			float RHV;
        ///			int timeout =5000;	
        ///			byte[] cmdByteArray = null;
        ///			byte[] rspByteArray = null;
        ///			paramReader.GetVersionInformation(out RFV,out RHV,timeout,cmdByteArray,rspByteArray);
        ///		}
        /// </code>
        /// </example>
        void GetVersionInformation(out float RFV, out float RHV, int timeout,
            out byte[] cmdByteArray, out byte[] rspByteArray);

        #endregion Methods

    }
	#endregion Interface

    #if !RX300 || COMPLETE
    /// <summary>
    ///  Reader Version Information
    /// </summary>
    #else
    /// <exclude/>
    #endif
    public class ReaderVersionInfo
    {
        private float m_CFV;
        private float m_CHV;
        private float m_RFV;
        private float m_RHV;

#if !RX300 || COMPLETE
        /// <summary>
        ///  Reader Version Information
        /// </summary>
#else
    /// <exclude/>
#endif
        public ReaderVersionInfo(float CFV, float CHV, float RFV, float RHV)
        {
            this.m_CFV = CFV;
            this.m_CHV = CHV;
            this.m_RFV = RFV;
            this.m_RHV = RHV;
        }
#if !RX300 || COMPLETE
        /// <summary>
        /// Controller Firmware Version
        /// </summary>
        /// 
        #else
    /// <exclude/>
#endif
        public float CFV
        {
            get
            {
                return m_CFV;
            }
        }
#if !RX300|| COMPLETE
        /// <summary>
        /// Controller Hardware Version
        /// </summary>
        /// 
        #else
    /// <exclude/>
#endif
        public float CHV
        {
            get
            {
                return m_CHV;
            }

        }
#if !RX300|| COMPLETE
        /// <summary>
        /// RF Module Firmware Version
        /// </summary>
        /// 
        #else
    /// <exclude/>
#endif
        public float RFV
        {
            get
            {
                return m_RFV;
            }
        }
#if !RX300|| COMPLETE
        /// <summary>
        /// RF Module Hardware Version
        /// </summary>
        /// 
#else
    /// <exclude/>
#endif
        public float RHV
        {
            get
            {
                return m_RHV;
            }
        }
        /// <exclude/>
        public float ReaderCFV
        {
            get
            {
                return m_CFV;
            }
            set
            {
                m_CFV = value;
            }
        }
        /// <exclude/>
        public float ReaderCHV
        {
            get
            {
                return m_CHV;
            }
            set
            {
                m_CHV = value;
            }
        }
        /// <exclude/>
        public float ReaderRFV
        {
            get
            {
                return m_RFV;
            }
            set
            {
                m_RFV = value;
            }
        }
        /// <exclude/>
        public float ReaderRHV
        {
            get
            {
                return m_RHV;
            }
            set
            {
                m_RHV = value;
            }
        }
        

    }
}

