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

using KTone.Core.KTIRFID;

namespace KTone.Core.KTIRFID
{
    /// <summary>
    /// Implements IRFIDIFXTag ,holds tag related data 
    /// </summary>
    [Serializable]
    public class RFIDIFXTag : IRFIDIFXTag
    {
        #region [ member vars ]
        private byte[] m_tagSN = null;       
        private uint m_WritePageSize = 0;
        private uint m_ReadPageSize = 0 ;
        private string m_Readername = string.Empty;
        private string m_AntennaName = string.Empty;
        private uint m_RSSI = 0;
        private IFXTagType m_TagType = IFXTagType.Class3;
        private string m_Epc = string.Empty;
        private int m_ReadCount = 1;
        #endregion [ member vars ]

        #region [ Constructors ]

        /// <summary>
        /// Creates Instance of EPC Class3 
        /// </summary>
        /// <param name="tagSN">12 byte Array.</param>
        /// <param name="rssi">RSSI value</param>
        /// <param name="tagtype">Type of tag</param>
        public RFIDIFXTag(byte[] tagSN,uint rssi,IFXTagType tagtype)            
        {
            if (tagSN == null 
                || tagSN.Length!=12)
                throw new IFXException("TagSN is not 12 byte array.");
            m_tagSN = tagSN;
            m_Epc = GetEPCID(tagSN);
            Init(tagtype, rssi);
        }

        /// <summary>
        /// Creates Instance of EPC Class1 Gen2 Tag.
        /// </summary>
        /// <param name="epcID">24 character EPC Code</param>
        /// <param name="tagtype">Tag type Class 1 GEN 2</param>
        public RFIDIFXTag(string epcID, IFXTagType tagtype)           
        {
            
            if(epcID.Trim().Length!=24)
                throw new IFXException("EPCID is not 24 in Length.");
            m_tagSN = GetTagSN(epcID);
            m_Epc = epcID;
            Init(tagtype, 0);
        }

        /// <summary>
        /// Creates Instance of EPC Class3 
        /// </summary>
        /// <param name="rssi">RSSI value</param>
        /// <param name="epcID">24 character EPC Code</param>
        /// <param name="tagtype">Tag Type</param>
        public RFIDIFXTag(string epcID, uint rssi, IFXTagType tagtype) 
        {
           
            if (epcID.Trim().Length != 24)
                throw new IFXException("EPCID is not 24 in Length.");
            m_tagSN = GetTagSN(epcID);
            m_Epc = epcID;
            Init(tagtype, rssi);            
        }

        /// <summary>
        /// Creates Instance of EPC Class1 Gen2 Tag.
        /// </summary>
        /// <param name="tagSN">12 byte Array.</param>
        /// <param name="tagtype">Tag Type</param>
        public RFIDIFXTag(byte[] tagSN, IFXTagType tagtype) 
        {
            if (tagSN == null || tagSN.Length != 12)
                throw new IFXException("TagSN is not 12 byte array.");
            m_tagSN = tagSN;
            m_Epc = GetEPCID(tagSN);
            Init(tagtype, 0);
        }

        /// <summary>
        /// Returns string containing following information
        /// EPCID, Tag Type, Reader Name, Antenna Name, RSSI, Read Count.
        /// </summary>
        /// <returns>EPCID, Tag Type, Reader Name, Antenna Name, RSSI, Read Count in a single string</returns>
        override public  string  ToString()
        {
            StringBuilder strbuilder = new StringBuilder();
            strbuilder.AppendLine("EPC Code              : " + EPCID);
            strbuilder.AppendLine("Tag Type              : " + m_TagType.ToString());
            strbuilder.AppendLine("Reader Name           : " + ReaderName);
            strbuilder.AppendLine("Antenna Name          : " + AntennaName);
            strbuilder.AppendLine("RSSI                  : " + RSSI);
            strbuilder.AppendLine("READ Count            : " + ReadCount);
            return strbuilder.ToString();
        }

        private void Init(IFXTagType tagType,uint rssi)
        {
            if (tagType == IFXTagType.Class3)
            {
                m_ReadPageSize = 2;
                m_WritePageSize = 2;
                m_RSSI = rssi;
            }
            else
            {
                m_RSSI = 0;
            }            
            m_TagType = tagType;
        }
        private string GetEPCID(byte[] tagSN)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < m_tagSN.Length; i++)
                sb.Append(m_tagSN[i].ToString("X2"));
            return sb.ToString();
        }
        private byte[] GetTagSN(string epcCode)
        {
            string byteStr = string.Empty;
            List<byte> bytLst = new List<byte>();
            byte byteTemp = 0;
            for (int cnt = 0; cnt < epcCode.Length; cnt = cnt + 2)
            {
                byteStr = epcCode.Substring(cnt, 2);
                byteTemp = Convert.ToByte(byteStr, 16);
                bytLst.Add(byteTemp);
            }
            return bytLst.ToArray();
        }
        #endregion

        #region [ IRFIDIFXTag members ]
        /// <summary>
        /// Gets Tag SN12 bytes based on the EPC format. 
        /// </summary>
        public byte[] TagSN
        {
            get
            {
                return m_tagSN;
            }
        }
      
        /// <summary>
        /// Gets hex representation of tag id in string format.
        /// </summary>
        public int ReadCount
        {
            get 
            {
                return m_ReadCount;
            }
            set
            {
                m_ReadCount = value;
            }
        }
        /// <summary>
        /// Gets EPC Code for tag. 
        /// </summary>
        public string EPCID
        {
            get
            {
                return m_Epc;
            }
            set
            {
                m_Epc = value;
            }
        }        
        /// <summary>
        /// Gets Read Page size in bytes ( 2 for GEN2 tags ) 
        /// </summary>
        public uint ReadPageSize
        {
            get
            {
                return m_ReadPageSize;
            }
        }
        /// <summary>
        /// Gets Write Page Size ( 2 for GEN2 Tags) 
        /// </summary>
        public uint WritePageSize
        {
            get
            {
                return m_WritePageSize;
            }
        }
        /// <summary>
        /// Gets Reader name that returned the tag.
        /// </summary>
        /// <returns></returns>
        public string ReaderName
        {
            get
            {
                return m_Readername;
            }
            set
            {
                m_Readername = value;
            }
        }
        /// <summary>
        /// Gets Antenna name where tag was seen. 
        /// </summary>
        /// <returns></returns>
        public string AntennaName
        {
            get
            {
                return m_AntennaName;
            }
            set
            {
                m_AntennaName = value;
            }
        }
        /// <summary>
        /// Gets TAG type of the TAG read. 
        /// </summary>
        /// <returns></returns>
        public IFXTagType TagType
        {
            get
            {
                return m_TagType;
            }
        }
        /// <summary>
        /// gets RSSI value for the Tag when it was read.
        /// </summary>
        public uint RSSI
        {
            get
            {
                return m_RSSI;
            }
        }
        #endregion [ IRFIDIFXTag members ]
    }
}
