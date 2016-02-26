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
    /// Enlists possible tag types supported by the reader models. 
    /// </summary>
    [Serializable]
    public enum IFXTagType:int 
    {
        
        /// <summary>
        /// EPC Class1 Gen2 tag
        /// </summary>
        Class1_GEN2=4,

        //OKK 31May2007 Begin
        //Added for New Fimware (1.22) It is giving command timeout if we pass 1 as protocol.
        //Class3NewVersion --> 1 and Class3 -->32
        /// <summary>
        ///  Class 3 battery assisted tag 
        ///  (For backwards compatibility)
        /// </summary>
        Class3=32,

        
        /// <summary>
        ///  Class 3 battery assisted tag
        /// </summary>
        Class3_NewVersion = 1,
        //OKK 31May2007 End
    }

   
    /// <summary>
    /// Memory banks in Tags
    /// </summary>
    public enum IFXMemoryBank:uint
    {
        /// <summary>
        /// EPC memory bank
        /// </summary>
        EPC = 1,
        /// <summary>
        /// Tag Id memory bank
        /// </summary>
        TID,
        /// <summary>
        /// User memory bank
        /// </summary>
        USER,
        
    }

    //OKK 31May2007 Begin
    /// <summary>
    /// Lock types for memory lock bits.
    /// </summary>
    public enum IFXLockType : uint
    {
        /// <summary>
        /// Write lock
        /// </summary>
        WRITE=1,      
        /// <summary>
        /// Read lock
        /// </summary>
        READ,
        /// <summary>
        /// Parma lock
        /// </summary>
        PERMA
    }
    //OKK 31May2007 End

    /// <summary>
    /// This structure allows user to select the tag data. For the tags that support Tag user data, user 
    /// will be able to select the desired memory banks based on this structure. 
    /// </summary>
    [Serializable]
    public struct TagSelector
    {
        private IFXMemoryBank memBank;
        private TagLocFilter selectFilter;
        private TagLocFilter activateFilter;
        //OKK 31Oct2007
        //Added bool variable for Activatex xml tag
        private bool m_useActivate;


        static TagSelector()
        {

        }
        /// <summary>
        /// Tag filtering Criteria.
        /// </summary>
        /// <param name="sFilter">Tag Selection and Activation Criteria</param>
        public TagSelector(TagLocFilter sFilter, bool sActivate)
        {
            memBank = IFXMemoryBank.EPC;
            selectFilter = sFilter;
            activateFilter = sFilter;
            m_useActivate = sActivate;
        }
        /// <summary>
        /// Tag filtering Criteria.
        /// </summary>
        /// <param name="sSelectFilter">Tag Selection Criteria</param>
        /// <param name="sActivateFilter">Tag Activation Criteria</param>
        public TagSelector(TagLocFilter sSelectFilter, TagLocFilter sActivateFilter,bool sActivate)
        {
            memBank = IFXMemoryBank.EPC;
            selectFilter = sSelectFilter;
            activateFilter = sActivateFilter;
            m_useActivate = sActivate;
        }
        /// <summary>
        /// Tag filtering Criteria.
        /// </summary>
        /// <param name="sSelectFilter">Tag Selection Criteria</param>
        /// <param name="sActivateFilter">Tag Activation Criteria</param>
        /// <param name="aMemBank">Read/Write operation to be performed on Memmory bank.</param>
        public TagSelector(TagLocFilter sSelectFilter, TagLocFilter sActivateFilter
            , IFXMemoryBank aMemBank, bool sActivate)
        {
            memBank = aMemBank;
            selectFilter = sSelectFilter;
            activateFilter = sActivateFilter;
            m_useActivate = sActivate;
        }
        /// <summary>
        /// Gets Memory Bank
        /// </summary>
        public IFXMemoryBank MemBank
        {
            get
            {
                return this.memBank;
            }
        }
        /// <summary>
        /// Gets Selection filter.
        /// </summary>
        public TagLocFilter SelectFilter
        {
            get
            {
                return this.selectFilter;
            }
        }
        /// <summary>
        /// Gets Activation filter.
        /// </summary>
        public TagLocFilter ActivateFilter
        {
            get
            {
                return this.activateFilter;
            }
        }

        /// <summary>
        /// Added for backword compatibility.Activate tag will get added to command if it is true else will not get added to command.
        /// </summary>
        public bool UseActivate
        {
            get
            {
                return m_useActivate;
            }
        }
    }
    /// <summary>
    ///This structure allows user to select Tag data Filter. 
    /// </summary>
    [Serializable]
    public struct TagLocFilter
    {

        private ushort offset;  // Pointer to  ( PageSize in bytes of the word)  word on tag
        private ushort length;   // Count of the Words
        private byte[] criteria;
      
        /// <summary>
        /// Select Tag data Filter. 
        /// </summary>
        /// <param name="aOffset">Start index (decimal)</param>
        /// <param name="aLength">No of byte (decimal)</param>
        /// <param name="aCriteria">Filtering Data</param>
        public TagLocFilter(ushort aOffset, ushort aLength, byte[] aCriteria)
        {
            if (aCriteria == null)
                throw new IFXException(" Criteria cannot be null.");

            if (aCriteria.Length == 0)
                throw new IFXException(" Invalid criteria bytes length.");

            this.offset = aOffset;
            this.length = aLength;
            this.criteria = aCriteria;
        }
        /// <summary>
        /// Select Tag data Filter. 
        /// </summary>
        /// <param name="aOffset">Start index (decimal)</param>        
        /// <param name="aCriteria">Filtering Data</param>
        public TagLocFilter(ushort aOffset, byte[] aCriteria)
        {
            if (aCriteria == null)
                throw new IFXException(" Criteria cannot be null.");

            if (aCriteria.Length == 0)
                throw new IFXException(" Invalid criteria bytes length.");

            this.offset = aOffset;
            this.length = Convert.ToUInt16(aCriteria.Length*8);
            this.criteria = aCriteria;
        }
        /// <summary>
        /// Gets Start offset w.r.t block 0 (decimal)
        /// </summary>
        public int Offset
        {
            get
            {
                return this.offset;
            }

        }
        /// <summary>
        /// Gets No of byte (decimal)
        /// </summary>
        public int Length
        {
            get
            {
                return this.length;
            }
        }
        /// <summary>
        /// Gets Filtering Data
        /// </summary>
        public byte[] Criteria
        {
            get
            {
                return this.criteria;
            }
        }
    }
    /// <summary>
    /// This Structure stores 12 byte EPC Code.
    /// </summary>
    [Serializable]
    public struct EpcId
    {
        private byte[] idAry;
        private string idStr;
        /// <summary>
        /// This structure stores 12 byte of EPC ID.
        /// </summary>
        /// <param name="id">12 byte EPC Code</param>
        public EpcId(byte[] id)
        {
            if (id == null || id.Length != 12)
                throw new Exception("Invalid epc id");
            this.idAry = id;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < id.Length; i++)
                sb.Append(id[i].ToString("X2"));

            this.idStr = sb.ToString();
        }

        /// <summary>
        /// This structure takes in 24 char string and converts it into 12 byte array of EPC ID
        /// </summary>
        /// <param name="epcid"></param>
        public EpcId(string epcid)
        {
            if (epcid == string.Empty || epcid.Length != 24)
                throw new Exception("Invalid epc id");           
            this.idStr=epcid;

            List<byte> bytLst = new List<byte>();
            string byteStr = string.Empty;
            byte byteTemp = 0;
            for (int cnt = 0; cnt < epcid.Length; cnt = cnt + 2)
            {
                byteStr = epcid.Substring(cnt, 2);
                byteTemp = Convert.ToByte(byteStr, 16);
                bytLst.Add(byteTemp);
            }
            this.idAry = bytLst.ToArray();
        }
        /// <summary>
        /// 12 byte EPC Code.
        /// </summary>
        public byte[] Id
        {
            get
            {
                return this.idAry;
            }
        }
        /// <summary>
        /// Gets string represnation of ID.
        /// </summary>
        public string EPCID
        {
            get
            {
                return this.idStr;
            }
        }       
    }
    /// <summary>
    /// This structure stores information required to access tag Memory area.
    /// </summary>
    [Serializable]
    public struct TagDataSpec
    {
        private ushort length;
        private ushort flatMemOffset;
        private ushort block;
        private ushort offset;
        private IFXMemoryBank bank;
        private string blockPassword;
        private string exchangePassword;
        private string accessPassword;
        /// <summary>
        /// Information required to access tag Memory area.
        /// This a linear memory address space from 0 to 60Kb
        /// </summary>        
        /// <param name="sLength">No of bytes to be Read. (decimal bytes)</param>
        /// <param name="sOffset">Start location in bank. (decimal bytes)</param>
        /// <param name="sBlock">Block number</param>
        public TagDataSpec(ushort sLength,ushort sBlock , ushort sOffset)
        {
            try
            {

                if (sLength > 128 || sLength < 1) 
                    throw new IFXException("Invalid Length. Length should be between 1 and 128") ;

                if (sLength % 2 != 0)
                    throw new IFXException("Length or Data Length should be even number.");

                if (sBlock >= 60)
                    throw new IFXException("Invalid Block Number. Block number should be between 0 to 59");

                if (sOffset >= 128)
                {
                    throw new IFXException("Invalid Offset. Offset should be between 0 to 127");
                }

                if ((sLength + sOffset) > 128)
                    throw new IFXException("Length and offset addition must be less than or equal to 128");
                

                bank = IFXMemoryBank.USER;
                length = sLength;
                blockPassword = string.Empty;
                exchangePassword = string.Empty;
                accessPassword = string.Empty;
                offset = sOffset;
                block = sBlock;
                flatMemOffset = (ushort)((64 * block) + offset);
            }
            catch (Exception ex)
            {
                throw new IFXException(ex.Message); 
            }
        }
        /// <summary>
        /// Information required to access tag Memory area.
        /// </summary>        
        /// <param name="sLength">No of bytes to be Read. (decimal)</param>
        /// <param name="sBlock">Block number</param>
        /// <param name="sOffset">Start location in bank. (decimal)</param>
        /// <param name="sBlockPassword">Block Password.</param>
        /// <param name="sAccessPassword">Access Password.</param>
        /// <param name="sExchangePassword">Exchange Password.</param>
        public TagDataSpec(ushort sLength, ushort sBlock,ushort sOffset,
            string sBlockPassword, string sAccessPassword, string sExchangePassword)
        {
            try
            {

                if (sLength > 128 || sLength < 1)
                    throw new IFXException("Invalid Length. Length should be between 1 and 128");

                if (sLength % 2 != 0)
                    throw new IFXException("Length or Data Length should be even number.");

                if (sBlock >= 60)
                    throw new IFXException("Invalid Block Number. Block number should be between 0 to 59");

                if (sOffset >= 128)
                {
                    throw new IFXException("Invalid Offset. Offset should be between 0 to 127");
                }

                if ((sLength + sOffset) > 128)
                    throw new IFXException("Length and offset addition must be less than or equal to 128");


                bank = IFXMemoryBank.USER;
                length = sLength;
                blockPassword = sBlockPassword;
                exchangePassword = sExchangePassword;
                accessPassword = sAccessPassword;
                offset = sOffset;
                block = sBlock;
                flatMemOffset = (ushort)((64 * block) + offset);
            }
            catch(Exception exp)
            {
                throw new IFXException(exp.Message); 
            }
        }
        /// <summary>
        /// Information required to access tag Memory area.
        /// </summary>        
        /// <param name="sLength">No of bytes to be Read. (decimal)</param>
        /// <param name="sOffset">Start location in bank. (decimal)</param>
        ///  <param name="sBank">Bank on operation has to be performed.</param>
        ///  <param name="sBlock">Block number</param>
        public TagDataSpec(IFXMemoryBank sBank, ushort sLength, ushort sBlock,ushort sOffset)
        {
            try
            {


                if (sLength > 128 || sLength < 1)
                    throw new IFXException("Invalid Length. Length should be between 1 and 128");

                if (sLength % 2 != 0)
                    throw new IFXException("Length or Data Length should be even number.");

                if (sBlock >= 60)
                    throw new IFXException("Invalid Block Number. Block number should be between 0 to 59");

                if (sOffset >= 128)
                {
                    throw new IFXException("Invalid Offset. Offset should be between 0 to 127");
                }
                if ((sLength + sOffset) > 128)
                    throw new IFXException("Length and offset addition must be less than or equal to 128");


                bank = sBank;
                length = sLength;
                blockPassword = string.Empty;
                exchangePassword = string.Empty;
                accessPassword = string.Empty;
                offset = sOffset;
                block = sBlock;
                flatMemOffset = (ushort)((64 * block) + offset);
            }
            catch(Exception exp)
            {

                throw new IFXException(exp.Message);
            }

        }
        /// <summary>
        /// Information required to access tag Memory area.
        /// Flat Memory Offset is calculated w.r.t block 0.
        /// </summary>        
        ///  <param name="sBank">Bank on operation has to be performed.</param>
        /// <param name="sLength">No of bytes to be Read. (decimal)</param>
        /// <param name="sOffset">Start location in bank. (decimal)</param>
        /// <param name="sBlockPassword">Block Password.</param>
        /// <param name="sAccessPassword">Access Password.</param>
        /// <param name="sExchangePassword">Exchange Password.</param>
        public TagDataSpec(IFXMemoryBank sBank, ushort sLength,ushort sOffset,
            string sBlockPassword, string sAccessPassword, string sExchangePassword)
        {
            try
            {
                if (sLength > 128 || sLength < 1)
                    throw new IFXException("Invalid Length. Length should be between 1 and 128");

                if (sLength % 2 != 0)
                    throw new IFXException("Length or Data Length should be even number.");

                if (sOffset >= 128)
                {
                    throw new IFXException("Invalid Offset. Offset should be between 0 to 127");
                }
                if ((sLength + sOffset) > 128)
                    throw new IFXException("Length and offset addition must be less than or equal to 128");

                bank = sBank;
                length = sLength;
                blockPassword = sBlockPassword;
                exchangePassword = sExchangePassword;
                accessPassword = sAccessPassword;
                offset = sOffset;
                block = 0;
                flatMemOffset = (ushort)((64 * block) + offset);
            }
            catch (Exception exp)
            {
                throw new IFXException(exp.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sBank">Bank on operation has to be performed.</param>
        /// <param name="sLength">No of bytes to be Read. (decimal)</param>
        /// <param name="sBlock">Block number</param>
        /// <param name="sOffset">Start location in bank. (decimal)</param>
        /// <param name="sBlockPassword">Block Password.</param>
        /// <param name="sAccessPassword">Access Password.</param>
        /// <param name="sExchangePassword">Exchange Password.</param>
        public TagDataSpec(IFXMemoryBank sBank, ushort sLength, ushort sBlock, ushort sOffset,
           string sBlockPassword, string sAccessPassword, string sExchangePassword)
        {
            try
            {

                if (sLength > 128 || sLength < 1)
                    throw new IFXException("Invalid Length. Length should be between 1 and 128");

                if (sLength % 2 != 0)
                    throw new IFXException("Length or Data Length should be even number.");

                if (sBlock >= 60)
                    throw new IFXException("Invalid Block Number. Block number should be between 0 to 59");

                if (sOffset >= 128)
                {
                    throw new IFXException("Invalid Offset. Offset should be between 0 to 127");
                }

                if ((sLength + sOffset) > 128)
                    throw new IFXException("Length and offset addition must be less than or equal to 128");

                block = sBlock;
                bank = sBank;
                length = sLength;
                blockPassword = sBlockPassword;
                exchangePassword = sExchangePassword;
                accessPassword = sAccessPassword;
                offset = sOffset;
                flatMemOffset = (ushort)((64 * block) + offset);
            }
            catch (Exception exp)
            {
                throw new IFXException(exp.Message);
            }
        }       


        /// <summary>
        /// Gets number of bytes to be Read.
        /// </summary>
        public ushort Length
        {
            get
            {
                return this.length;
            }
        }
        /// <summary>
        /// Gets location in bank.
        /// </summary>
        public ushort Offset
        {
            get
            {
                return offset;
            }
        }
        /// <summary>
        /// Gets Access Password.
        /// </summary>
        public string AccessPassword
        {
            get
            {
                return this.accessPassword;
            }
        }
        /// <summary>
        /// Gets Block Password.
        /// </summary>
        public string BlockPassword
        {
            get
            {
                return this.blockPassword;
            }
        }
        /// <summary>
        /// Gets Exchange Password.
        /// </summary>
        public string ExchangePassword
        {
            get
            {
                return this.exchangePassword;
            }
        }
        /// <summary>
        /// Gets Memory Bank to be Accessed.
        /// </summary>
        public IFXMemoryBank Bank
        {
            get
            {
                return this.bank;
            }            
        }

        /// <summary>
        /// Gets Block Number
        /// </summary>
        public ushort Block
        {
            get
            {
                return this.block;
            }
        }

        /// <summary>
        /// Gets Offset w.r.t. block 0 
        /// </summary>
        public ushort FlatMemoryOffset
        {
            get
            {
                return this.flatMemOffset;
            }
        }

        /// <summary>
        /// Get Structure values in string.
        /// </summary>
        /// <returns>Structure values in string.</returns>
        public string GetString()
        {
            StringBuilder strbuilder = new StringBuilder();
            strbuilder.AppendLine("Memory Bank           : " + Bank.ToString());
            strbuilder.AppendLine("Block                 : " + Block);
            strbuilder.AppendLine("Length                : " + Length);
            strbuilder.AppendLine("Offset                : " + Offset);
            strbuilder.AppendLine("Flat Memory Offset    : " + FlatMemoryOffset);    
            strbuilder.AppendLine("Access Password       : " + AccessPassword);
            strbuilder.AppendLine("Block Password        : " + BlockPassword);
            strbuilder.AppendLine("Exchange Password     : " + ExchangePassword);
            return strbuilder.ToString();

        }
    }
    /// <summary>
    /// This structure is used for Tag Data related operation.
    /// </summary>
    [Serializable]    
    public struct TagDataInfo
    {
        private TagDataSpec tagDataSpec;
        private byte[] data;
        
      
        /// <summary>
        /// Used for Tag Data related operation.
        /// </summary>             
        /// <param name="sData">Tag Data.</param>
        /// <param name="sblock">Block number</param>
        /// <param name="sOffset">Start location in bank.</param>
        public TagDataInfo(byte[] sData, ushort sblock ,ushort sOffset)
        {
            try
            {
                if (sData == null)
                    throw new IFXException("Data cannot be null.");

                tagDataSpec = new TagDataSpec(Convert.ToUInt16(sData.Length), sblock, sOffset);
                data = sData;
            }
            catch(Exception exp)
            {
                throw exp;
            }
        }
        /// <summary>
        /// Used for Tag Data related operation.
        /// </summary>               
        /// <param name="sData">Tag Data.</param>
        /// <param name="sBlock">Block name</param>
        /// <param name="sOffset">Start location in bank.</param>
        /// <param name="sBlockPassword">Block Password.</param>
        /// <param name="sAccessPassword">ccess Password.</param>
        /// <param name="sExchangePassword">Exchange Password.</param>
        public TagDataInfo(byte[] sData,ushort sBlock , ushort sOffset,            
            string sBlockPassword, string sAccessPassword, string sExchangePassword)
        {
            if (sData == null)
                throw new IFXException("Data cannot be null.");

            tagDataSpec = new TagDataSpec(Convert.ToUInt16(sData.Length), sBlock, sOffset,            
            sBlockPassword, sAccessPassword, sExchangePassword);
            data = sData;
        }
       
        /// <summary>
        ///  Used for Tag Data related operation.
        /// </summary>
        /// <param name="sData">Tag Data.</param>
        /// <param name="sTagDataSpec">TagDataSpec required to access tag Memory area.</param>
        public TagDataInfo(byte[] sData,TagDataSpec sTagDataSpec)
        {
            if (sData == null)
                throw new IFXException("Data cannot be null.");
            tagDataSpec = sTagDataSpec;            
            data = sData;
        }
        /// <summary>
        /// Gets byte array associated with TagDataInfo.
        /// </summary>
        public byte[] Data
        {
            get
            {
                return this.data;
            }
        }
        /// <summary>
        /// Gets string representation of TagDatainfo structure
        /// </summary>
        /// <returns>Structure values in string.</returns>
        public string GetString()
        {
            StringBuilder strbuilder = new StringBuilder();

            strbuilder.AppendLine("TagDataSpec           : ");
            strbuilder.AppendLine(this.TagDataSpec.GetString());                        
            strbuilder.AppendLine();            
            strbuilder.AppendLine("Data                  : ");
            if (this.data != null)
            {
                for (int cnt = 0; cnt < this.data.Length; cnt++)
                {
                    if (cnt + 1 % 10 == 0)
                        strbuilder.AppendLine("                     :0x" + this.data[cnt].ToString("X2"));
                    else
                    {
                        strbuilder.Append("0x");            
                        strbuilder.Append(this.data[cnt].ToString("X2"));
                    }
                    strbuilder.Append(" ");            
                }
            }
            return strbuilder.ToString();
        }
        /// <summary>
        /// Gets TagDataSpec associated with TagDataInfo
        /// </summary>
        public TagDataSpec TagDataSpec
        {
            get 
            {
                return this.tagDataSpec;
            }
        }
    }

    /// <summary>
    /// This structure stores set and reset information of Lock Bits.
    /// </summary>
    [Serializable]
    public struct MBLocks
    {
        private bool write;
        private bool writeMask;
        private bool read;
        private bool readMask;
        private bool perma;
        private bool permaMask;

        /// <summary>
        /// Memory Locks
        /// </summary>
        /// <param name="sWrite">Write lock</param>
        /// <param name="sWriteMask">Write lock mask</param>
        /// <param name="sRead">Read lock</param>
        /// <param name="sReadMask">Read lock mask</param>
        /// <param name="sPerma">Parma lock</param>
        /// <param name="sPermaMask">Parma lock end</param>
        public MBLocks(bool sWrite, bool sWriteMask, bool sRead, bool sReadMask, bool sPerma, bool sPermaMask)
        {
            write = sWrite;
            writeMask = sWriteMask;
            read = sRead;
            readMask = sReadMask;
            perma = sPerma;
            permaMask = sPermaMask;
        }

        #region Properties

        /// <summary>
        /// Get write lock bit.
        /// False - Lock bit not set
        /// True - Write lock bit is set
        /// </summary>
        public bool Write
        {
            get
            {
                return write;
            }
        }
        /// <summary>
        /// Get write mask lock bit.
        /// False - Write Mask not set
        /// True - Write Mask set
        /// </summary>
        public bool WriteMask
        {
            get
            {
                return writeMask;
            }
        }

        /// <summary>
        /// Get Read lock bit.
        /// False - Read lock bit not set
        /// True - Read lock bit is set
        /// </summary>
        public bool Read
        {
            get
            {
                return read;
            }
        }

        /// <summary>
        /// Get read mask lock bit.
        /// False - Read Mask bit not set
        /// True - Read Mask bit set
        /// </summary>
        public bool ReadMask
        {
            get
            {
                return readMask;
            }
        }

        /// <summary>
        /// Get Perma lock bit.
        /// False - Perma lock bit not set
        /// True - Perma lock bit is set
        /// </summary>
        public bool Perma
        {
            get
            {
                return perma;
            }
        }

        /// <summary>
        /// Get perma mask lock bit.
        /// False - Parma Mask bit not set
        /// True - Parma Mask bit is set
        /// </summary>
        public bool PermaMask
        {
            get
            {
                return permaMask;
            }
        }
        #endregion


    }


    /// <summary>
    /// GPIO connector mounted on the reader panel
    /// </summary>
    [Serializable]
    public struct GPIOPins
    {
        private bool pin1;
        private bool pin2;
        private bool pin3;
        private bool pin4;

        /// <summary>
        /// GPIO pins status
        /// </summary>
        /// <param name="sPin1"></param>
        /// <param name="sPin2"></param>
        /// <param name="sPin3"></param>
        /// <param name="sPin4"></param>
        public GPIOPins(bool sPin1, bool sPin2, bool sPin3, bool sPin4)
        {
            pin1 = sPin1;
            pin2 = sPin2;
            pin3 = sPin3;
            pin4 = sPin4;
        }

        #region Properties

        /// <summary>
        /// Pin 1
        /// </summary>
        public bool PIN1
        {
            get
            {
                return this.pin1;
            }
        }

        /// <summary>
        /// Pin 2
        /// </summary>
        public bool PIN2
        {
            get
            {
                return this.pin2;
            }
        }

        /// <summary>
        /// Pin 3
        /// </summary>
        public bool PIN3
        {
            get
            {
                return this.pin3;
            }
        }

        /// <summary>
        /// Pin 4
        /// </summary>
        public bool PIN4
        {
            get
            {
                return this.pin4;
            }
        }

        #endregion Properties

    }

    /// <summary>
    /// Exposes properties of the tag read by the Intelleflex reader
    /// </summary>
    
    public interface IRFIDIFXTag
    {
        /// <summary>
        /// Gets Tag ID 12 bytes based on the EPC format. 
        /// </summary>
        byte[] TagSN 
        {
            get;

        }
        /// <summary>
        /// Gets hex representation of tag id in string format.
        /// </summary>
        string EPCID
        {
            get;
        }
        /// <summary>
        /// Gets the read  count for a tag.
        /// </summary>
        int ReadCount
        {
            get;
        }
        /// <summary>
        /// Gets Read Page size in bytes ( 2 for GEN2 tags ) 
        /// </summary>
        uint ReadPageSize
        {
            get;
        }
        /// <summary>
        /// Gets Write Page Size ( 2 for GEN2 Tags) 
        /// </summary>
        uint WritePageSize
        {
            get;
        }       
        /// <summary>
        /// Gets Reader Name that returned the tag.
        /// </summary>
        /// <returns></returns>
        string ReaderName
        {
            get;
        }
        /// <summary>
        /// Gets Antenna Name where tag was seen. 
        /// </summary>
        /// <returns></returns>
        string AntennaName
        {
            get;
        }        
        /// <summary>
        /// Gets TAG type of the TAG read. 
        /// </summary>
        /// <returns></returns>
        IFXTagType TagType
        {
            get;
            
        }
        /// <summary>
        /// Gets RSSI value for the Tag when it was read.
        /// </summary>
        uint RSSI
        {
            get;
        }

        /// <summary>
        /// Gets Tag Data sting 
        /// </summary>
        /// <returns>Tag data</returns>
        string ToString();
    }


    /// <summary>
    /// Trigger Types
    /// </summary>
    public enum IfxTriggerTypes : uint
    {
        /// <summary>
        /// GPIO Trigger
        /// </summary>
        GPIOTRIGGER = 0,
        /// <summary>
        /// Time Delayed Trigger 
        /// </summary>
        TIMEDELAYEDTRIGGER
    }

    /// <summary>
    /// Input pins
    /// </summary>
    public enum GPInputPins
    {
        /// <summary>
        /// GPIO input pin 1
        /// </summary>
        InputPin1 = 1,
        /// <summary>
        /// GPIO input pin 2
        /// </summary>
        InputPin2,
        /// <summary>
        /// GPIO input pin 3
        /// </summary>
        InputPin3,
        /// <summary>
        /// GPIO input pin 4
        /// </summary>
        InputPin4
    }

    /// <summary>
    /// Trigger Inforation
    /// </summary>
    [Serializable]
    public class IfxTrigger
    {
        private IfxTriggerTypes m_TriggerType;
        private object m_param1;
        private object m_param2;
        private bool m_Repeating = false;

        /// <summary>
        /// Static parameter for the command with no trigger parameter
        /// </summary>
        public static IfxTrigger NoTrigger = null;
        /// <summary>
        /// Is Repeating ON
        /// </summary>
        public bool Repeating
        {
            get
            {
                return m_Repeating;
            }
        }

        /// <summary>
        /// Trigger Type
        /// </summary>
        public IfxTriggerTypes TriggerType
        {

            get
            {
                return m_TriggerType;
            }
        }

        /// <summary>
        /// Param 1 for trigger 
        /// if trigger type is GPIOTRIGGER value is input pin (GPInputPins)
        /// if trigger type is TIMEDELAYEDTRIGGER value is startTime
        /// </summary>
        public object Param1
        {
            get
            {
                return m_param1;
            }
        }

        /// <summary>
        /// Param 2 for trigger 
        /// if trigger type is GPIOTRIGGER value is gpio initial state in bool
        /// if trigger type is TIMEDELAYEDTRIGGER value is stopTime
        /// </summary>
        public object Param2
        {
            get
            {
                return m_param2;
            }
        }

        /// <summary>
        /// Trigger parameters
        /// </summary>
        /// <param name="triggerType">Type of trigger 
        /// 1. GPIO trigger
        /// 2. Time delayed trigger</param>
        /// <param name="repeating">if it is true repeating is ON else OFF</param>
        /// <param name="param1">
        /// Param 1 for trigger 
        /// if trigger type is GPIOTRIGGER pass value as GPInputPins for input pin
        /// if trigger type is TIMEDELAYEDTRIGGER pass value as uint for start time
        /// </param>
        /// <param name="param2"> 
        /// Param 2 for trigger 
        /// if trigger type is GPIOTRIGGER pass value as bool for gpio initial state
        /// if trigger type is TIMEDELAYEDTRIGGER pass value as uint for stop time</param>
        public IfxTrigger(IfxTriggerTypes triggerType, bool repeating, object param1, object param2)
        {
            m_Repeating = repeating;
            if (triggerType == IfxTriggerTypes.TIMEDELAYEDTRIGGER)
            {

                if (param1 is uint && param2 is uint)
                {
                    m_TriggerType = triggerType;
                    m_param1 = param1;
                    m_param2 = param2;
                }
                else
                {
                    throw new IFXException("Parameter type mismatched");
                }
            }
            else
            {
                if (param1 is GPInputPins && param2 is bool)
                {
                    m_TriggerType = triggerType;
                    m_param1 = param1;
                    m_param2 = param2;
                }
                else
                {
                    throw new IFXException("Parameter type mismatched");
                }
            }
        }
    }
}