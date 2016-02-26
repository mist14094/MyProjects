using System;
using System.Collections.Generic;
using System.Text;

namespace KTone.Core.KTIRFID
{
    public interface IWTGPIO
    {
        /// <summary>
        /// The function of this command is to set a specific relay.
        /// There are 2 relays available on the device.
        /// The parameter RelayNo of the command specifies the relay Number ( 0 or 1 )
        /// The Status parameter of the command specifies the state of the relay. (0–open or 1- close)
        /// </summary>
        /// <param name="pinNumber">Relay Number</param>
        /// <param name="ioStatus">Its status</param>
        /// <example>
        /// <code>
        /// 	void ExecuteSetRelayStatus(IWTRFReader paramReader)
        ///		{
        ///			PinNumber pinNumber = PinNumber.First;
        ///			IOStatus ioStatus = IOStatus.Open;
        ///			paramReader.SetRelayStatus(pinNumber,ioStatus);
        ///		}
        /// </code>
        /// </example>
        void SetRelayStatus(PinNumber pinNumber, IOStatus ioStatus);
        /// <summary>
        /// The function of this command is to set a specific relay.
        /// There are 2 relays available on the device.
        /// The parameter RelayNo of the command specifies the relay Number ( 0 or 1 )
        /// The Status parameter of the command specifies the state of the relay. (0–open or 1- close)
        /// </summary>
        /// <param name="pinNumber">Relay Number</param>
        /// <param name="ioStatus">Its status</param>
        /// <param name="timeout">Time Out</param>
        /// <example>
        /// <code>
        /// 	void ExecuteSetRelayStatus(IWTRFReader paramReader)
        ///		{
        ///			PinNumber pinNumber = PinNumber.First;
        ///			IOStatus ioStatus = IOStatus.Open;
        ///			int timeout =5000;	
        ///			paramReader.SetRelayStatus(pinNumber,ioStatus,timeout);
        ///		}
        /// </code>
        /// </example>
        void SetRelayStatus(PinNumber pinNumber, IOStatus ioStatus, int timeout);
        /// <summary>
        /// The function of this command is to set a specific relay.
        /// There are 2 relays available on the device.
        /// The parameter RelayNo of the command specifies the relay Number ( 0 or 1 )
        /// The Status parameter of the command specifies the state of the relay. (0–open or 1- close)
        /// </summary>
        /// <param name="pinNumber">Relay Number</param>
        /// <param name="ioStatus">Its status</param>
        /// <param name="timeout">Time Out</param>
        /// <param name="cmdByteArray">Command Byte Array</param>
        /// <param name="rspByteArray">Response from Reader</param>
        /// <example>
        /// <code>
        /// 	void ExecuteSetRelayStatus(IWTRFReader paramReader)
        ///		{
        ///			PinNumber pinNumber = PinNumber.First;
        ///			IOStatus ioStatus = IOStatus.Open;
        ///			int timeout =5000;	
        ///			byte[] cmdByteArray = null;
        ///			byte[] rspByteArray = null;
        ///			paramReader.SetRelayStatus(pinNumber,ioStatus,timeout,cmdByteArray,rspByteArray);
        ///		}
        /// </code>
        /// </example>
        void SetRelayStatus(PinNumber pinNumber, IOStatus ioStatus, int timeout,
                            out byte[] cmdByteArray, out byte[] rspByteArray);

        /// <summary>
        /// The function of this command is to get the current status of a specific relay.
        /// There are 2 relays available on the device.
        /// The parameter RelayNo of the command specifies the relay Number ( 0 / 1 )
        /// </summary>
        /// <param name="pinNumber">Relay Number</param>
        /// <returns>Its Status</returns>
        /// <example>
        /// <code>
        /// 	void ExecuteGetRelayStatus(IWTRFReader paramReader)
        ///		{
        ///			PinNumber pinNumber = PinNumber.First;
        ///			IOStatus ioStatus ;
        ///			ioStatus = paramReader.GetRelayStatus(pinNumber);
        ///		}
        /// </code>
        /// </example>
        IOStatus GetRelayStatus(PinNumber pinNumber);
        /// <summary>
        /// The function of this command is to get the current status of a specific relay.
        /// There are 2 relays available on the device.
        /// The parameter RelayNo of the command specifies the relay Number ( 0 / 1 )
        /// </summary>
        /// <param name="pinNumber">Relay Number</param>
        /// <param name="timeout">Time Out</param>
        /// <returns>Its Status</returns>
        /// <example>
        /// <code>
        /// 	void ExecuteGetRelayStatus(IWTRFReader paramReader)
        ///		{
        ///			PinNumber pinNumber = PinNumber.First;
        ///			IOStatus ioStatus ;
        ///			int timeout =5000;	
        ///			ioStatus = paramReader.GetRelayStatus(pinNumber,timeout);
        ///		}
        /// </code>
        /// </example>
        IOStatus GetRelayStatus(PinNumber pinNumber, int timeout);
        /// <summary>
        /// The function of this command is to get the current status of a specific relay.
        /// There are 2 relays available on the device.
        /// The parameter RelayNo of the command specifies the relay Number ( 0 / 1 )
        /// </summary>
        /// <param name="pinNumber">Relay Number</param>
        /// <param name="timeout">Time Out</param>
        /// <param name="cmdByteArray">Command Byte Array</param>
        /// <param name="rspByteArray">Response from Reader</param>
        /// <returns>Its Status</returns>
        /// <example>
        /// <code>
        /// 	void ExecuteGetRelayStatus(IWTRFReader paramReader)
        ///		{
        ///			PinNumber pinNumber = PinNumber.First;
        ///			IOStatus ioStatus ;
        ///			int timeout =5000;	
        ///			byte[] cmdByteArray = null;
        ///			byte[] rspByteArray = null;
        ///			ioStatus = paramReader.GetRelayStatus(pinNumber,timeout,cmdByteArray,rspByteArray);
        ///		}
        /// </code>
        /// </example>
        IOStatus GetRelayStatus(PinNumber pinNumber, int timeout,
                                out byte[] cmdByteArray, out byte[] rspByteArray);

        /// <summary>
        /// The function of this command is to get the status of the specific input. There are 2 inputs available on
        /// the device. ( Input - 0 / 1)
        /// </summary>
        /// <param name="pinNumber">Input Numbar</param>
        /// <returns>Its Status</returns>
        /// <example>
        /// <code>
        /// 	void ExecuteGetInputStatus(IWTRFReader paramReader)
        ///		{
        ///			PinNumber pinNumber = PinNumber.First;
        ///			IOStatus ioStatus ;
        ///			ioStatus = paramReader.GetInputStatus(pinNumber);
        ///		}
        /// </code>
        /// </example>
        IOStatus GetInputStatus(PinNumber pinNumber);
        /// <summary>
        /// The function of this command is to get the status of the specific input. There are 2 inputs available on
        /// the device. ( Input - 0 / 1)
        /// </summary>
        /// <param name="pinNumber">Input Numbar</param>
        /// <param name="timeout">Time Out</param>
        /// <returns>Its Status</returns>
        /// <example>
        /// <code>
        /// 	void ExecuteGetInputStatus(IWTRFReader paramReader)
        ///		{
        ///			PinNumber pinNumber = PinNumber.First;
        ///			IOStatus ioStatus ;
        ///			int timeout =5000;	
        ///			ioStatus = paramReader.GetInputStatus(pinNumber,timeout);
        ///		}
        /// </code>
        /// </example>
        IOStatus GetInputStatus(PinNumber pinNumber, int timeout);
        /// <summary>
        /// The function of this command is to get the status of the specific input. There are 2 inputs available on
        /// the device. ( Input - 0 / 1)
        /// </summary>
        /// <param name="pinNumber">Input Numbar</param>
        /// <param name="timeout">Time Out</param>
        /// <param name="cmdByteArray">Command Byte Array</param>
        /// <param name="rspByteArray">Response from Reader</param>
        /// <returns>Its Status</returns>
        /// <example>
        /// <code>
        /// 	void ExecuteGetInputStatus(IWTRFReader paramReader)
        ///		{
        ///			PinNumber pinNumber = PinNumber.First;
        ///			IOStatus ioStatus ;
        ///			int timeout =5000;	
        ///			byte[] cmdByteArray = null;
        ///			byte[] rspByteArray = null;
        ///			ioStatus = paramReader.GetInputStatus(pinNumber,timeout,cmdByteArray,rspByteArray);
        ///		}
        /// </code>
        /// </example>
        IOStatus GetInputStatus(PinNumber pinNumber, int timeout,
                                    out byte[] cmdByteArray, out byte[] rspByteArray);
    }
}
