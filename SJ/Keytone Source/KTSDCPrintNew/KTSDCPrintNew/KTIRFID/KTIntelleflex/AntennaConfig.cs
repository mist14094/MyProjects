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
    /// Forward link (interrogator to tag) data rate in kbps.
    /// Enumeration containing valid values for Forward link speed.
    /// </summary>
    public enum ForwardLink : int
    {
        /// <summary>
        /// 8 Kbps
        /// </summary>
        Kbps8 = 1,
        /// <summary>
        /// 16 KBps
        /// </summary>
        Kbps16,
        /// <summary>
        /// 32 KBps
        /// </summary>
        Kbps32,
        //OKK 19June2007 Begin
        //Removed 64Kbps 
        /// <summary>
        /// 48 KBps
        /// </summary>       
        Kbps48
        //OKK 19June2007 End
    }
    /// <summary>
    /// Reverse link (tag to interrogator) data rate in kbps
    /// Enumeration containing valid values for Reverse link speed.
    /// </summary>
    public enum ReverseLink : int
    {
        /// <summary>
        /// 2 KBps
        /// </summary>
        Kbps2 = 1,
        /// <summary>
        /// 8 KBps
        /// </summary>
        Kbps8,
        /// <summary>
        /// 32 KBps
        /// </summary>
        Kbps32
    }
    /// <summary>
    /// Modes of scanning Tags
    /// This can take 2 values 
    /// 1. Portal - which is used for reading stationary tags
    /// 2. Scour -  which is used for reading moving tags.
    /// </summary>
    public enum Inventory :int
    {
        /// <summary>
        /// It is used for reading stationary tags
        /// </summary>
        Portal =1,
        /// <summary>
        /// It is used for reading moving tags
        /// </summary>
        Scour 
    }
    /// <summary>
    /// Antenna Configuration details        
    /// </summary>
    
    [Serializable]
    public struct AntennaConfig
    {
        private ushort id;
        private string antennaName;
        private IFXTagType[] tagTypesInUse;
        private uint power;
        private bool isEnabled;
        private Inventory scanType;
        private uint txAntenna ;
        private uint rxAntenna ;
        private ForwardLink forwardLinkRate;
        private ReverseLink reverseLinkRate;
        private ushort qValue;
       
        /// <summary>
        /// Initializes a new instance of the AntennaConfig.
        /// </summary>
        /// <param name="antName">Name of the Antenna</param>
        /// <param name="tagTypesInUse">Tag types supported. This can be either C1G2 or C3.</param>
        /// <param name="power">Power Level</param>
        /// <param name="scanType">Tag Scan Type</param>
        /// <param name="isEnabled">Connection state - connected or disconnected</param>
        /// <param name="txAnt">Transmit Sub Antenna</param>
        /// <param name="rxAnt">Recieve Sub Antenna</param>      
        /// <param name="forwardLink">Forward Link Rate</param>
        /// <param name="reverseLink">Reverse Link Rate</param>
        /// <param name="qVal">Q Value</param>        
        public AntennaConfig(string antName, IFXTagType[] tagTypesInUse, Inventory scanType,
            uint power,bool isEnabled,uint txAnt,uint rxAnt,ForwardLink forwardLink,
            ReverseLink reverseLink,ushort qVal)
        {
            if (antName == null || antName.Trim() == string.Empty)
                throw new IFXException("Antenna Name cannot be empty");
            if (antName.Length>50)
                throw new IFXException("Antenna Name cannot be more than 50 characters");
            if (tagTypesInUse == null || tagTypesInUse.Length == 0)
                throw new IFXException("Select atleast one tag type.");
            if (power >30)
                throw new IFXException("Power cannot be more than 30.");
            if (txAnt == 0 || txAnt > IFXConstants.NoOfTxAntennas)
                throw new IFXException("Tx Antenna should be between 1 and 4.");
            if (rxAnt == 0 || rxAnt > IFXConstants.NoOfRxAntennas)
                throw new IFXException("Rx Antenna should be between 1 and 4.");
            if (qVal > IFXConstants.MaxQval)
                throw new IFXException("Q -Value should be between 0 and 15.");
            this.antennaName = antName;
            this.power = power;
            this.isEnabled = isEnabled;            
            this.tagTypesInUse = tagTypesInUse;
            this.scanType = scanType;
            this.txAntenna = txAnt;
            this.rxAntenna = rxAnt;
            this.forwardLinkRate = forwardLink;
            this.reverseLinkRate = reverseLink;
            this.qValue = qVal;
            this.id = 0;
        }
        /// <summary>
        /// Initializes a new instance of the AntennaConfig.
        /// </summary>
        /// <param name="antName">Name of the Antenna</param>
        /// <param name="tagTypesInUse">Tag types supported. This can be either C1G2 or C3.</param>
        /// <param name="power">Power Level</param>
        /// <param name="scanType">Tag Scan Type</param>
        /// <param name="isEnabled">Connection state whther connected or disconnected</param>
        /// <param name="txAnt">Transmit Sub Antenna</param>      
        /// <param name="rxAnt">Recieve Sub Antenna</param>      
        public AntennaConfig(string antName, IFXTagType[] tagTypesInUse, Inventory scanType,
            uint power, bool isEnabled, uint txAnt, uint rxAnt)
        {
            if (tagTypesInUse == null || tagTypesInUse.Length == 0)
                throw new IFXException("Select atleast one tag type.");
            if (antName == null || antName.Trim() == string.Empty)
                throw new IFXException("Antenna Name cannot be empty");
            if (antName.Length > 50)
                throw new IFXException("Antenna Name cannot be more than 50 characters");
            if (power > 30)
                throw new IFXException("Power cannot be more than 30.");

            this.antennaName = antName;
            this.power = power;
            this.isEnabled = isEnabled;
            this.tagTypesInUse = tagTypesInUse;
            this.scanType = scanType;

            this.txAntenna = txAnt;
            this.rxAntenna = rxAnt;
            this.forwardLinkRate = ForwardLink.Kbps32;
            this.reverseLinkRate = ReverseLink.Kbps32;
            this.qValue = 2;
            this.id = 0;
        }
        /// <summary>
        /// Gets string represenation of AntennaConfig structure.
        /// </summary>
        /// <returns>string represenation of AntennaConfig structure.</returns>
        public string GetString()
        {
            StringBuilder strbuilder = new StringBuilder();
            strbuilder.AppendLine("Antenna Name          : " + antennaName);
            strbuilder.AppendLine("ID                    : " + id);
            strbuilder.AppendLine("Power                 : " + power);
            for (int cnt = 0; cnt < tagTypesInUse.Length; cnt++)
            {
                if (cnt == 0)
                    strbuilder.AppendLine("Tag Types Supported   : " + tagTypesInUse[0]);
                else
                    strbuilder.AppendLine("                        " + tagTypesInUse[cnt]);
            }            
            strbuilder.AppendLine("Enabled               : " + isEnabled.ToString());
            strbuilder.AppendLine("Q- Value              : " + QVal);
            strbuilder.AppendLine("Tx Port               : " + txAntenna);
            strbuilder.AppendLine("Rx Port               : " + rxAntenna);
            strbuilder.AppendLine("Forward Link Rate     : " + forwardLinkRate.ToString());
            strbuilder.AppendLine("Reverse Link Rate     : " + reverseLinkRate.ToString());
            strbuilder.AppendLine("Scan Type             : " + scanType.ToString());
            return strbuilder.ToString();
        }                

        #region ReadOnly Properties

        /// <summary>
        /// Gets the Antenna Name
        /// </summary>
        public string AntennaName
        {
            get { return antennaName; }
        }
        /// <summary>
        /// Gets the array of tag types in use of the antenna
        /// </summary>
        public IFXTagType[] TagTypesInUse
        {
            get { return tagTypesInUse; }
            internal set
            {
                tagTypesInUse = value;
            }
        }

        /// <summary>
        /// Gets the power for the antenna
        /// </summary>
        public uint Power
        {
            get { return power; }
            set { this.power = value; }
        }

        /// <summary>
        /// Gets Tag Scan Type of Antenna.
        /// </summary>
        public Inventory InventoryType
        {
            get { return this.scanType; }
        }
        /// <summary>
        /// Gets a bool value indicating whether the antenna is Enabled.
        /// </summary>
        public bool IsEnabled
        {
            get { return isEnabled; }
            internal set
            {
                isEnabled = value;
            }
        }
        /// <summary>
        /// Gets the recieve Antenna
        /// </summary>
        public uint RxAntenna
        {
            get
            {
                return rxAntenna;
            }
        }
        /// <summary>
        /// Gets the Transmit Antenna
        /// </summary>
        public uint TxAntenna
        {
            get
            {
                return txAntenna;
            }
        }
        /// <summary>
        /// Gets ID of this AntennaConfig
        /// </summary>
        public ushort ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        /// <summary>
        /// Gets Qval of this AntennaConfig
        /// </summary>
        public ushort QVal
        {
            get
            {
                return qValue;
            }
            internal set
            {
                qValue = value;
            }
        }
        /// <summary>
        /// Gets Forward Link of AntennaConfig
        /// </summary>
        public ForwardLink ForwardLink
        {
            get
            {
                return forwardLinkRate;
            }
            internal set
            {
                forwardLinkRate = value;
            }
        }
        /// <summary>
        /// Gets Reverse Link of AntennaConfig
        /// </summary>
        public ReverseLink ReverseLink
        {
            get
            {
                return reverseLinkRate;
            }
            internal set
            {
                reverseLinkRate = value;
            }
        }
        #endregion ReadOnly Properties       
        
    }
}
