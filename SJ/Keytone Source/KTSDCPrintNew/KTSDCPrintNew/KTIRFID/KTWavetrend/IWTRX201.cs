using System;
using System.Collections.Generic;
using System.Text;
using KTone.Core.KTIRFID;

namespace KTone.Core.KTIRFID
{
    public interface IWTRX201:IDisposable
    {

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
        void Online();

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
        void Offline();
                   




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
        void GetVersionInformation(out float CFV, out float RFV, out float CHV, out float RHV);
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
        void GetVersionInformation(out float CFV, out float RFV, out float CHV, out float RHV, int timeout);
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
        void GetVersionInformation(out float CFV, out float RFV, out float CHV, out float RHV, int timeout,
            out byte[] cmdByteArray, out byte[] rspByteArray);

        #endregion Reader Level Command

        #region IDisposable Members

       

        #endregion
    }
}
