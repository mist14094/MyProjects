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
using System.Runtime.Serialization;

namespace KTone.Core.KTIRFID
{
    /// <summary>
    /// Base class for all the EventArgs classes provided by the SDK
    /// </summary>
    [Serializable]
    public class IFXReaderEventArgs : EventArgs
    {
        #region Attributes
        private string m_ReaderName = string.Empty;
        private DateTime m_TimeStamp;
        #endregion Attributes

        /// <summary>
        /// Initializes the object of IFXReaderEventArgs
        /// </summary>
        /// <param name="readerName">string</param>
        public IFXReaderEventArgs(string readerName)
        {
            m_ReaderName = readerName;
            m_TimeStamp = DateTime.Now;
        }
     

        #region Properties
        /// <summary>
        /// Gets reader name
        /// </summary>
        public string ReaderName
        {
            get { return m_ReaderName; }
        }


        /// <summary>
        /// Time Stamp
        /// </summary>
        public DateTime TimeStamp
        {
            get
            {
                return m_TimeStamp;
            }
        }
        #endregion Properties
    }
    /// <summary>
    /// Provides data for the tag read events
    /// </summary>
    [Serializable]
    public class IFXReaderTagReadEventArgs : IFXReaderEventArgs
    {
        #region Attributes
        private IRFIDIFXTag m_Tag = null;
        #endregion Attributes

        /// <summary>
        /// Initializes the object of IFXReaderTagReadEventArgs
        /// </summary>
        /// <param name="readerName">string</param>
        /// <param name="tag">IRFIDIFXTag</param>
        public IFXReaderTagReadEventArgs(string readerName, IRFIDIFXTag tag)
            : base(readerName)
        {

            m_Tag = tag;
        }


        #region Properties
        /// <summary>
        /// Gets IRFIDIFX tag 
        /// </summary>
        public IRFIDIFXTag Tag
        {
            get
            {
                return m_Tag;
            }
        }
        #endregion Properties
    }


    /// <summary>
    /// Provides data for the Reader status Monitor events
    /// </summary>
    [Serializable]
    public class IFXReaderStatusMonitorEventArgs : IFXReaderEventArgs
    {
        #region Attributes
        private bool m_IsConnected = false;

        #endregion Attributes
        /// <summary>
        /// Initializes the object of IFXReaderStatusMonitorEventArgs
        /// </summary>
        /// <param name="readerName">string</param>
        /// <param name="isConnected">bool</param>
        public IFXReaderStatusMonitorEventArgs(string readerName, bool isConnected)
            : base(readerName)
        {
            m_IsConnected = isConnected;
        }

        #region Properties
        /// <summary>
        /// Gets if reader is connected        
        /// </summary>
        public bool IsConnected
        {
            get
            {
                return m_IsConnected;
            }
        }

        #endregion Properties
    }

    /// <summary>
    /// Provides data for the Antenna Ports status events
    /// </summary>
    [Serializable]
    public class IFXReaderAntennaStatusEventArgs : IFXReaderEventArgs
    {
        #region Attributes
        private uint[] m_TxAntennaPorts = null;
        private uint[] m_RxAntennaPorts = null;

        #endregion Attributes
        /// <summary>
        /// Initializes the object of IFXReaderAntennaStatusEventArgs
        /// </summary>
        /// <param name="readerName">string</param>
        /// <param name="txAntennaPorts">uint[] Active Tx ports</param>
        /// <param name="rxAntennaPorts">uint[] Active Rx ports</param>
        public IFXReaderAntennaStatusEventArgs(string readerName,
            uint[] txAntennaPorts, uint[] rxAntennaPorts)
            : base(readerName)
        {
            m_TxAntennaPorts = txAntennaPorts;
            m_RxAntennaPorts = rxAntennaPorts;
        }

        #region Properties
        /// <summary>
        /// Gets list of Transmit Antenna Ports
        /// </summary>
        public uint[] TxAntennaPorts
        {
            get
            {
                return m_TxAntennaPorts;
            }
        }

        /// <summary>
        /// Gets list of Recieve Antenna Ports
        /// </summary>
        public uint[] RxAntennaPorts
        {
            get
            {
                return m_RxAntennaPorts;
            }
        }
      
        #endregion Properties
    }

    /// <summary>
    /// Reader end inventory
    /// </summary>
    [Serializable]
    public class IFXReaderEndInventoryArgs : IFXReaderEventArgs
    {
        private bool m_IsExcepation = false;
        private string response = "";
        private IRFIDIFXTag[] tags = null; 
 
        /// <summary>
        /// Reader End Inventory
        /// </summary>
        /// <param name="readerName">Reader name where we want to end inventory</param>
        /// <param name="aTags">Tags</param>
        /// <param name="isException">Was there any exception</param>
        /// <param name="resp">Resonse string</param>
        public IFXReaderEndInventoryArgs(string readerName,IRFIDIFXTag[] aTags,  bool isException, string resp)
            : base(readerName)

        {
            m_IsExcepation = isException;
            response = resp;
            tags = aTags; 
        }

        /// <summary>
        /// Gets response string
        /// </summary>
        public string Response
        {
            get
            {
                return response;
            }
        }

        /// <summary>
        /// Gets boolean specifing if there was any error
        /// </summary>
        public bool IsException
        {
            get
            {
                return m_IsExcepation;
            }
        }

        /// <summary>
        /// Gets array of tags
        /// </summary>
        public IRFIDIFXTag[] Tags
        {
            get
            {
                return tags;
            }
        }
    }



}
