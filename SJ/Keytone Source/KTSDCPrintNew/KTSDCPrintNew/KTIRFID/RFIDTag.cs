
/********************************************************************************************************
Copyright (c) 2010 KeyTone Technologies.All Right Reserved

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
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;


namespace KTone.Core.KTIRFID
{
    /// <summary>
    /// Holds the data read from a RFIDTag and exposes the tag attributes.
    /// </summary>
    [Serializable]
    public abstract class RFIDTag : IRFIDTag
    {
        #region Attributes
        /// <summary>
        /// Byte array representating the tag data 
        /// </summary>
        protected byte[] tagDataArray = null;
        /// <summary>
        /// Byte array representating the tag serial number 
        /// </summary>
        protected byte[] tagSNArray = null;
        /// <summary>
        /// Address which is used as a starting address for a read/write operation.
        /// </summary>
        protected int startAddress;
        /// <summary>
        /// Parsed tag serial number in xml format.
        /// </summary>
        protected string tagSNXml = string.Empty;

        /// <summary>
        /// Parsed tag data in xml format.
        /// </summary>
        protected string tagDataXml = string.Empty;

        /// <summary>
        /// Indicates the length of serial number in Bytes.
        /// </summary>
        protected int snLengthInBytes = 8;
        /// <summary>
        /// Index which is used to set tagFamily of the current Tag.
        /// </summary>
        protected int tagFamily = 0;

        /// <summary>
        /// Flag that indicates whether the tag id of the current tag 
        /// is initialized.
        /// </summary>
        protected bool tagIdInitialized = false;

        /// <summary>
        /// A flag indicating whether this tag is lockable.
        /// </summary>
        protected bool lockable = false;
        /// <summary>
        /// Maximum number of bytes which can be accessed .
        /// </summary>
        protected int maxDataLength = 0;
        /// <summary>
        /// Minimum number of bytes which can be accessed.
        /// </summary>
        protected int minDataLength = 0;
        /// <summary>
        /// Read page size for the tag.
        /// </summary>
        protected int readPageSize = 0;
        /// <summary>
        /// Write page size for the tag.
        /// </summary>
        protected int writePageSize = 0;

        /// <summary>
        /// A flag indicating whether the current tag supports reading.
        /// </summary>
        protected bool canRead = false;
        /// <summary>
        /// A flag indicating whether the current tag supports writing.
        /// </summary>
        protected bool canWrite = false;
        /// <summary>
        /// A flag indicating whether the current tag is proprietary.
        /// </summary>
        protected bool isProprietary = false;
        /// <summary>
        /// A flag indicating whether the current tag is ISO15693-2 Compliant.
        /// </summary>
        protected bool isISO15693_2Compliant = false;
        /// <summary>
        /// A flag indicating whether the current tag is ISO15693-3 Compliant.
        /// </summary>
        protected bool isISO15693_3Compliant = false;
        /// <summary>
        /// A flag indicating whether the current tag is ISO14443-TypeA Compliant.
        /// </summary>
        protected bool isISO14443_TypeACompliant = false;
        /// <summary>
        /// A flag indicating whether the current tag supports anticollision.
        /// </summary>
        protected bool supportsAnticollision = false;
        /// <summary>
        /// Tag vendor name.
        /// </summary>
        protected string vendorName = string.Empty;
        /// <summary>
        /// Tag description.
        /// </summary>
        protected string description = string.Empty;

        /// <summary>
        /// String representing the tag type.
        /// </summary>
        protected string tagType = string.Empty;

        /// <summary>
        /// True in case of EPC tags ex. EPC64, EPC96 and gen2 tags else flase
        /// </summary>
        protected bool isEPCtag = false;

        /// <summary>
        /// List of available tag classes.
        /// </summary>
        private static string[] tagClassNames = null;
        /// <summary>
        /// Store the Concrete TagTypes for all the derived RFID tags 
        /// </summary>
        private static Type[] tagClassTypes = null;

        /// <summary>
        /// Id of the antenna where this tag was detected.
        /// </summary>
        private string antennaId = string.Empty;

        /// <summary>
        /// Name of the antenna where this tag was detected.
        /// </summary>
        private string antennaName = string.Empty;

        /// <summary>
        /// Id of the RFIDReader that read this tag.
        /// </summary>
        private string readerId = string.Empty;

        /// <summary>
        /// Name of the RFIDReader that read this tag.
        /// </summary>
        private string readerName = string.Empty;

        /// <summary>
        /// Tag read count.
        /// </summary>
        private int tagReadCount = 0;
        /// <summary>
        /// Takes the last seen Timestamp.
        /// </summary>
        private string lastSeenTime = string.Empty;


        #endregion

        #region Properties
        /// <summary>
        /// When overridden in a derived class, gets/sets the tag data.
        /// </summary>
        public byte[] TagData
        {
            get
            {
                return tagDataArray;
            }
            set
            {
                tagDataArray = value;
            }
        }
        /// <summary>
        /// Gets/sets the tag serial number. In the EPC classes it 
        /// assigned the EPCTagSN_URN also
        /// </summary>
        public virtual byte[] TagSN
        {
            get
            {
                return tagSNArray;
            }
            set
            {
                if (CanWriteTagId)
                {
                    if (value != null)
                    {
                        //tagSNArray = value;
                        byte[] tagSN = value;
                        tagSNArray = new byte[tagSN.Length];
                        Array.Copy(tagSN, tagSNArray, tagSN.Length);
                        tagIdInitialized = true;

                    }
                }
            }
        }
        /// <summary>
        /// Gets the string representation of tag serial number(hex).
        /// </summary>
        public string TagSNBytes
        {
            get
            {
                if (tagSNArray == null)
                    return "";
                StringBuilder strBlder = new StringBuilder(50);
                foreach (byte b in tagSNArray)
                    strBlder.Append("0x" + b.ToString("X2") + " ");
                return strBlder.ToString();
            }
        }
        //		/// <summary>
        //		/// Gets the string representation of tag serial number(ASCII).
        //		/// </summary>
        //		public string TagSNASCII
        //		{
        //			get
        //			{
        //				if(tagSNArray == null)
        //					return "";
        //				return System.Text.ASCIIEncoding.ASCII.GetString(tagSNArray);
        //			}
        //		}
        /// <summary>
        /// Returns the number of bytes used by tag serial number.
        /// </summary>
        public int SNLengthInBytes
        {
            get
            {
                return snLengthInBytes;
            }
        }
        /// <summary>
        /// Returns the number of bits used by tag serial number.
        /// </summary>
        public int SNLengthInBits
        {
            get
            {
                return snLengthInBytes * 8;
            }
        }

        /// <summary>
        /// Returns true for EPC tags else returns false 
        /// </summary>
        public bool IsEPCTag
        {
            get
            {
                return isEPCtag;
            }
        }

        /// <summary>
        /// When overridden in a derived class, gets/sets a flag indicating whether 
        /// this tag is lockable.
        /// </summary>
        public bool Lockable
        {
            get
            {
                return lockable;
            }
        }
        /// <summary>
        /// When overridden in a derived class, returns the maximum number of bytes 
        /// which can be accessed .
        /// </summary>
        public int MaxDataLength
        {
            get
            {
                return maxDataLength;
            }
        }
        /// <summary>
        /// When overridden in a derived class,returns the minimum number of bytes 
        /// which can be accessed.
        /// </summary>
        public int MinDataLength
        {
            get
            {
                return minDataLength;
            }
        }
        /// <summary>
        /// When overridden in a derived class, gets a value indicating whether the 
        /// current tag supports reading.
        /// </summary>
        public bool CanRead
        {
            get
            {
                return canRead;
            }
        }
        /// <summary>
        /// When overridden in a derived class, gets a value indicating whether the 
        /// current tag supports writing.
        /// </summary>
        public bool CanWrite
        {
            get
            {
                return canWrite;
            }
        }
        /// <summary>
        /// When overridden in a derived class, gets a value indicating whether the 
        /// current tag is proprietary.
        /// </summary>
        public bool IsProprietary
        {
            get
            {
                return isProprietary;
            }
        }
        /// <summary>
        /// When overridden in a derived class, gets a value indicating whether the 
        /// current tag is ISO15693-2 Compliant.
        /// </summary>
        public bool IsISO15693_2Compliant
        {
            get
            {
                return isISO15693_2Compliant;
            }
        }
        /// <summary>
        /// When overridden in a derived class, gets a value indicating whether the 
        /// current tag is ISO15693-3 Compliant.
        /// </summary>
        public bool IsISO15693_3Compliant
        {
            get
            {
                return isISO15693_3Compliant;
            }
        }
        /// <summary>
        /// When overridden in a derived class,gets a value indicating whether the 
        /// current tag supports anticollision.
        /// </summary>
        public bool SupportsAnticollision
        {
            get
            {
                return supportsAnticollision;
            }
        }
        /// <summary>
        /// When overridden in a derived class, gets/sets the read page size for 
        /// the tag.
        /// </summary>
        public int ReadPageSize
        {
            get
            {
                return readPageSize;
            }
            set
            {
                readPageSize = value;
            }
        }
        /// <summary>
        /// When overridden in a derived class, gets/sets the write page size for 
        /// the tag.
        /// </summary>
        public int WritePageSize
        {
            get
            {
                return writePageSize;
            }
            set
            {
                writePageSize = value;
            }
        }
        /// <summary>
        /// When overridden in a derived class, returns the vendor name.
        /// </summary>
        public string TagVendorName
        {
            get
            {
                return vendorName;
            }
        }
        /// <summary>
        /// When overridden in a derived class, returns the tag description.
        /// </summary>
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

        /// <summary>
        /// When overridden in a derived class, returns the tag type.
        /// </summary>
        public string TagType
        {
            get
            {
                return tagType;
            }
        }

        /// <summary>
        /// When overridden in a derived class, gets a value indicating whether the 
        /// current tag supports writing the tag id.
        /// </summary>
        public virtual bool CanWriteTagId
        {
            //Only for EPC0 and EPC1,this property is overridden.
            //Otherwise it returns the same value as that of CanWrite property.
            get
            {
                return CanWrite;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the tag id of the current tag 
        /// is initialized.
        /// </summary>
        public bool TagIdInitialized
        {
            get
            {
                return tagIdInitialized;
            }
        }

        /// <summary>
        /// Gets/sets the tagFamily Index of the current tag.
        /// </summary>
        public int TagFamily
        {
            get
            {
                return tagFamily;
            }
            set
            {
                tagFamily = value;
            }
        }
        /// <summary>
        /// Gets/sets the parsed tag serial number in xml format.
        /// </summary>
        public string TagSNXml
        {
            get
            {
                return tagSNXml;
            }
            set
            {
                tagSNXml = value;
            }
        }

        /// <summary>
        /// Gets/sets the parsed tag data in xml format.
        /// </summary>
        public string TagDataXml
        {
            get
            {
                return tagDataXml;
            }
            set
            {
                tagDataXml = value;
            }
        }


        /// <summary>
        /// Returns a list of available tag classes.
        /// </summary>
        public string[] TagClassNames
        {
            get
            {
                return tagClassNames;
            }
        }

        /// <summary>
        /// Returns the concrete derived class types of RFIDTag
        /// </summary>
        public Type[] TagClassTypes
        {
            get
            {
                return tagClassTypes;
            }
        }

        /// <summary>
        /// Gets/sets Id of the RFIDReader that read this tag
        /// </summary>
        public string ReaderId
        {
            get
            {
                return readerId;
            }
            set
            {
                readerId = value;
            }
        }

        /// <summary>
        /// Gets/sets name of the RFIDReader that read this tag
        /// </summary>
        public string ReaderName
        {
            get
            {
                return readerName;
            }
            set
            {
                readerName = value;
            }
        }

        /// <summary>
        /// Gets/sets Id of the antenna where this tag was detected.
        /// </summary>
        public string AntennaId
        {
            get
            {
                return antennaId;
            }
            set
            {
                antennaId = value;
            }
        }
        /// <summary>
        /// Gets/sets the LastseenTimestamp.
        /// </summary>
        public string LastSeenTime
        {
            get
            {
                return lastSeenTime;
            }
            set
            {
                lastSeenTime = value;
            }
        }

        /// <summary>
        /// Gets/sets name of the antenna where this tag was detected.
        /// </summary>
        public string AntennaName
        {
            get
            {
                return antennaName;
            }
            set
            {
                antennaName = value;
            }
        }

        /// <summary>
        /// Gets/sets the tag read count.
        /// </summary>
        public int TagReadCount
        {
            get
            {
                return tagReadCount;
            }
            set
            {
                tagReadCount = value;
            }
        }

        public int RSSI
        {
            get;
            set;
        }
        #endregion

        #region Constructor
        static RFIDTag()
        {
            InitTagClassNames();
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Returns information about the tag.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.Append("Tag type: " + this.GetType().ToString());
            str.Append(Environment.NewLine);


            if (tagSNArray != null)
            {
                str.Append("Tag Serial No.: ");
                str.Append(Environment.NewLine);
                str.Append(TagSNBytes);
            }
            else if (TagIdInitialized == false)
            {
                str.Append("Tag Serial No.:   NOT ASSIGNED YET ");
            }
            str.Append(Environment.NewLine);
            str.Append("Vendor: " + TagVendorName);
            str.Append(Environment.NewLine);
            str.Append("Min. data length (bytes): " + MinDataLength);
            str.Append(Environment.NewLine);
            str.Append("Max. data length (bytes): " + MaxDataLength);
            str.Append(Environment.NewLine);
            str.Append("Read page size (bytes): " + ReadPageSize);
            str.Append(Environment.NewLine);
            str.Append("Write page size (bytes): " + WritePageSize);
            str.Append(Environment.NewLine);
            str.Append("Serial no. length (bytes): " + SNLengthInBytes);
            str.Append(Environment.NewLine);
            str.Append("Serial no. length (bits): " + SNLengthInBits);
            str.Append(Environment.NewLine);

            str.Append("Supports Anticollision: " + SupportsAnticollision);
            str.Append(Environment.NewLine);
            str.Append("ISO 15693-2 Compliant: " + IsISO15693_2Compliant);
            str.Append(Environment.NewLine);
            str.Append("ISO 15693-3 Compliant: " + IsISO15693_3Compliant);
            str.Append(Environment.NewLine);
            str.Append("Proprietary: " + IsProprietary);
            str.Append(Environment.NewLine);
            str.Append("Lockable: " + Lockable);
            str.Append(Environment.NewLine);
            str.Append("Can read: " + CanRead);
            str.Append(Environment.NewLine);
            str.Append("Can write: " + CanWrite);
            str.Append(Environment.NewLine);
            str.Append("Is Initialized: " + TagIdInitialized);
            str.Append(Environment.NewLine);
            str.Append("Description: " + Description);
            str.Append(Environment.NewLine);
            str.Append("Is EPC Tag : " + IsEPCTag);
            str.Append(Environment.NewLine);
            return str.ToString();
        }

        /// <summary>
        /// Converts the given data into string format.
        /// </summary>
        /// <param name="details">Details is boolean parameter.</param>
        /// <returns>It returns the string.</returns>
        public string ToString(bool details)
        {
            if (details)
                return this.ToString();
            StringBuilder str = new StringBuilder();
            str.Append("Tag type                    : " + this.GetType().ToString());
            str.Append(Environment.NewLine);
            str.Append("Tag Serial No.              : ");

            if (tagSNArray != null)
            {
                str.Append(Environment.NewLine);
                str.Append(TagSNBytes);
                //str.Append(Environment.NewLine);
                //str.Append(TagSNASCII);
            }

            return str.ToString();
        }


        #endregion

        #region Public Methods
        /// <summary>
        /// Prints all the data on the Tag.
        /// </summary>
        /// <returns>Tag data in string format.</returns>
        public string PrintTagData()
        {
            int noBytes = 0;
            if (tagDataArray != null)
                noBytes = tagDataArray.Length;
            return PrintTagData(noBytes);
        }

        /// <summary>
        /// Prints data on the Tag.
        /// </summary>
        /// <param name="noBytes">Number of bytes to be printed</param>
        /// <returns>Tag data in string format.</returns>
        public string PrintTagData(int noBytes)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Tag Data : ");
            sb.Append(Environment.NewLine);
            sb.Append("--------------------------------------------------------------");
            sb.Append(Environment.NewLine);
            if (tagDataArray != null && noBytes > 0)
            {
                for (int cnt = 0; cnt < tagDataArray.Length; cnt++)
                {
                    if ((cnt != 0) && ((cnt % 16) == 0)) sb.Append(Environment.NewLine);
                    sb.Append(tagDataArray[cnt].ToString("X2") + "  ");
                    if (cnt == noBytes)
                    {
                        if (tagDataArray.Length > noBytes)
                        {
                            sb.Append(Environment.NewLine);
                            sb.Append("...[more]");
                        }
                        break;
                    }
                }
            }
            else
                sb.Append("--");

            sb.Append(Environment.NewLine);
            sb.Append("--------------------------------------------------------------"); sb.Append(Environment.NewLine);
            return sb.ToString();
        }

        /// <summary>
        /// Prints Tag SN.
        /// </summary>
        /// <returns>Tag SN in string format.</returns>
        public string PrintTagSN()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Tag SN : ");
            if (tagSNArray != null)
            {
                if (isEPCtag)
                {
                    sb.Append(((EPCTag)this).EPCTagSN_URN);
                    sb.Append(" [");
                }

                for (int cnt = 0; cnt < tagSNArray.Length; cnt++)
                {
                    if ((cnt != 0) && ((cnt % 16) == 0)) sb.Append(Environment.NewLine);
                    sb.Append(tagSNArray[cnt].ToString("X2") + " ");
                }

                if (isEPCtag)
                    sb.Append("]");
            }
            else
                sb.Append("--");

            sb.Append(Environment.NewLine);

            return sb.ToString();
        }

        /// <summary>
        /// Returns the tag data from the given start address for the given read length 
        /// </summary>
        /// <param name="startAddress"></param>
        /// <param name="readLength"></param>
        /// <returns></returns>
        public byte[] GetTagData(int startAddress, int readLength)
        {
            if (readLength <= 0)
                return null;
            if (tagDataArray == null)
                return null;

            byte[] requestedTagData = new byte[readLength];

            if (readLength > tagDataArray.Length)
                readLength = tagDataArray.Length;

            Array.Copy(tagDataArray, startAddress, requestedTagData, 0, readLength);

            return requestedTagData;
        }

        /// <summary>
        /// Sets the tag data from the given start address for the given write length 
        /// </summary>
        /// <param name="startAddress"></param>
        /// <param name="writeLength"></param>
        /// <param name="writeData"></param>
        public void SetTagData(int startAddress, int writeLength, byte[] writeData)
        {
            if (writeLength <= 0)
                return;
            if (writeData == null)
                return;

            if (startAddress + writeLength > MaxDataLength)
                writeLength = MaxDataLength - startAddress;

            byte[] newTagData = new byte[MaxDataLength];
            Array.Copy(tagDataArray, newTagData, tagDataArray.Length);
            Array.Copy(writeData, 0, newTagData, startAddress, writeData.Length);
            tagDataArray = newTagData;
        }


        /// <summary>
        /// Parses the tag serial no.
        /// </summary>
        /// <returns></returns>
        public Hashtable ParseTagSNXml()
        {
            Hashtable parsedTagSNHash = new Hashtable();
            if (tagSNXml == string.Empty)
                return parsedTagSNHash;
            MemoryStream memStream = new MemoryStream(
                System.Text.ASCIIEncoding.ASCII.GetBytes(tagSNXml));
            StreamReader streamReader = new StreamReader(memStream);
            if (streamReader.BaseStream.CanSeek)
                streamReader.BaseStream.Seek(0, SeekOrigin.Begin);
            XmlTextReader textReader = new XmlTextReader(streamReader);
            XPathDocument xPathDoc = new XPathDocument(textReader, XmlSpace.Preserve);
            XPathNavigator xpathNav = xPathDoc.CreateNavigator();


            string EPCTAGCONFIG = "/RFIDTagConfig/EPCTagConfig";

            XPathNodeIterator xpathDataElementIter = xpathNav.Select(EPCTAGCONFIG);
            while (xpathDataElementIter.MoveNext())
            {
                string serialNo = string.Empty;
                string productId = string.Empty;
                string companyId = string.Empty;
                XPathNodeIterator xpathIter =
                    xpathDataElementIter.Current.Select("SerialNo");
                if (xpathIter.MoveNext())
                    serialNo = (string)xpathIter.Current.Value.ToUpper();

                xpathIter =
                    xpathDataElementIter.Current.Select("ProductId");
                if (xpathIter.MoveNext())
                    productId = (string)xpathIter.Current.Value.ToUpper();

                xpathIter =
                    xpathDataElementIter.Current.Select("CompanyId");
                if (xpathIter.MoveNext())
                    companyId = (string)xpathIter.Current.Value.ToUpper();

                parsedTagSNHash["SERIALNO"] = serialNo;
                parsedTagSNHash["PRODUCTID"] = productId;
                parsedTagSNHash["COMPANYID"] = companyId;

                if (tagType == "EPCClass1" && snLengthInBytes == 64)
                    parsedTagSNHash["TagType"] = "EPCClass1_64BitTag";
                else if (tagType == "EPCClass0" && snLengthInBytes == 64)
                    parsedTagSNHash["TagType"] = "EPCClass0_64BitTag";

            }

            return parsedTagSNHash;
        }

        /// <summary>
        /// Parses the tag data stored in tag data xml.
        /// </summary>
        /// <returns></returns>
        public Hashtable ParseTagDataXml()
        {
            if (tagDataXml == null || tagDataXml.Equals(string.Empty))
                return null;
            Hashtable tagDataHash = new Hashtable();

            MemoryStream memStream = new MemoryStream(
                System.Text.ASCIIEncoding.ASCII.GetBytes(tagDataXml));

            StreamReader streamReader = new StreamReader(memStream);
            if (streamReader.BaseStream.CanSeek)
                streamReader.BaseStream.Seek(0, SeekOrigin.Begin);

            XmlTextReader textReader = new XmlTextReader(streamReader);
            XPathDocument xPathDoc = new XPathDocument(textReader, XmlSpace.Preserve);
            XPathNavigator xpathNav = xPathDoc.CreateNavigator();

            string DATAELEMENT = "/RFIDXFormData/TagDataTemplate/DataElements/DataElement";

            XPathNodeIterator xpathDataElementIter = xpathNav.Select(DATAELEMENT);
            while (xpathDataElementIter.MoveNext())
            {
                string fieldName = xpathDataElementIter.Current.GetAttribute("FieldName", "");
                string fieldVal = xpathDataElementIter.Current.GetAttribute("FieldValue", "");
                string dataType = xpathDataElementIter.Current.GetAttribute("FieldDataType", "");
                tagDataHash[fieldName] = fieldVal;
            }

            return tagDataHash;
        }


        /// <summary>
        /// Compares the given tag serial number with the current serial no.
        /// of current tag.
        /// </summary>
        /// <param name="requestedTagSN"></param>
        /// <returns></returns>
        public bool CompareTagSN(byte[] requestedTagSN)
        {
            if (tagSNArray == null)
                return false;
            if (requestedTagSN == null)
                return false;
            if (tagSNArray.Length != requestedTagSN.Length)
                return false;

            for (int i = 0; i < requestedTagSN.Length; i++)
            {
                if (requestedTagSN[i] != tagSNArray[i])
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Modify attributes of current object(this) using input parameter tag
        /// </summary>
        /// <param name="tag"></param>
        public virtual void UpdateTag(IRFIDTag tag)
        {
            // to be implemented in derived class
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Initializes the list of available tag classes.
        /// Also Initialize their Types also
        /// </summary>
        private static void InitTagClassNames()
        {
            try
            {
                ArrayList list = new ArrayList();
                ArrayList listTypes = new ArrayList();
                Type rfidTagType = typeof(KTone.Core.KTIRFID.RFIDTag);

                Assembly assembly = Assembly.GetAssembly(rfidTagType);
                if (assembly == null)
                    return;

                Type[] types = assembly.GetTypes();
                foreach (Type t in types)
                {
                    if (!t.IsClass)
                        continue;
                    if (t.IsAbstract)
                        continue;
                    if (!t.IsSubclassOf(rfidTagType))
                        continue;
                    string typeName = t.ToString();
                    int index = typeName.LastIndexOf('.');
                    if (index > -1)
                        typeName = typeName.Substring(index + 1);
                    list.Add(typeName);
                    listTypes.Add(t);
                }

                tagClassNames = (string[])list.ToArray(typeof(System.String));
                Array.Sort(tagClassNames);
                tagClassTypes = (Type[])listTypes.ToArray(typeof(System.Type));

            }
            catch
            {
                //Catch any exception 
                tagClassNames = null;
                tagClassTypes = null;
            }
        }
        #endregion Private Methods
    }

    /// <summary>
    /// Holds the data read from an EPC Tag and exposes the tag attributes.
    /// </summary>
    [Serializable]
    public abstract class EPCTag : RFIDTag
    {
        /// <summary>
        /// In case of EPC tag (isEPCtag is true) the URN representation of the EPC tag Serial Number
        /// See EPC Specs for more details about EPC URN.
        /// </summary>
        protected String epcTagSNURN = string.Empty;

        #region Property
        /// <summary>
        /// Will return the URN representation of EPC tag
        /// </summary>
        public string EPCTagSN_URN
        {
            get
            {
                return epcTagSNURN;
            }
        }
        #endregion Property

        #region Constructors
        /// <summary>
        /// Intializes an instance of EPCTag class.
        /// </summary>
        public EPCTag()
        {
            snLengthInBytes = 8;
            Init();
        }

        /// <summary>
        /// Intializes an instance of EPCTag class with a given tag serial number.
        /// </summary>
        public EPCTag(byte[] tagSN, string epcTagSNURN)
        {
            if (tagSN != null)
            {
                snLengthInBytes = tagSN.Length;
                tagSNArray = new byte[tagSN.Length];
                Array.Copy(tagSN, tagSNArray, tagSN.Length);
                this.epcTagSNURN = epcTagSNURN;
                tagIdInitialized = true;
            }
            Init();
        }

        /// <summary>
        /// Intializes an instance of EPCTag class.
        /// </summary>
        /// <param name="snLengthInBytes">Size of tag serial number in bytes</param>
        public EPCTag(int snLengthInBytes)
        {
            this.snLengthInBytes = snLengthInBytes;
            Init();
        }

        private void Init()
        {
            maxDataLength = 0;
            minDataLength = 0;
            readPageSize = 0;
            writePageSize = 0;

            lockable = false;
            canRead = false;
            canWrite = false;
            isProprietary = false;
            isEPCtag = true;
            isISO15693_2Compliant = false;
            isISO15693_3Compliant = false;
            isISO14443_TypeACompliant = false;
            supportsAnticollision = false;

            vendorName = "";
            description = "";
            tagType = "EPCTag";
            //			if ( tagIdInitialized ) 
            //			{
            //				epcTagSNURN = ParseEPCSerialNumber( tagSNArray) ; 
            //			}
        }
        #endregion


        #region Overrides
        /// <summary>
        /// Gets/sets the tag serial number.
        /// </summary>
        public override byte[] TagSN
        {
            get
            {
                return tagSNArray;
            }
            set
            {
                if (CanWriteTagId)
                {
                    if (value != null)
                    {
                        throw new Exception("Cannot assign only TagSN without epcTagSNURN.");//rfidappexception to be done

                        //tagSNArray = value;
                        //						byte[] tagSN = value;
                        //						tagSNArray = new byte[tagSN.Length];
                        //						Array.Copy(tagSN,tagSNArray,tagSN.Length);
                        //						tagIdInitialized = true;
                        //						if ( isEPCtag ) 
                        //						{
                        //							epcTagSNURN = ParseEPCSerialNumber( tagSNArray) ; 
                        //						}
                    }
                }

            }
        }


        /// <summary>
        /// Convert the given data to string format.
        /// </summary>
        /// <returns>Returns information about the tag in string format.</returns>
        public override string ToString()
        {
            return (base.ToString() + "EPC URN : " + epcTagSNURN + Environment.NewLine);
        }
        #endregion overrides


        /// <summary>
        /// Sets tag serial no. and urn.
        /// </summary>
        /// <param name="tagSN"></param>
        /// <param name="epcTagSNURN"></param>
        public void AssignTagSN(byte[] tagSN, string epcTagSNURN)
        {
            if (CanWriteTagId)
            {
                if (tagSN != null)
                {
                    if (tagSN.Length != snLengthInBytes)
                        throw new Exception("Supports only " + snLengthInBytes + " byte tag serial number." 
                            + BitConverter.ToString(tagSN));
                    tagSNArray = new byte[tagSN.Length];
                    Array.Copy(tagSN, tagSNArray, tagSN.Length);
                    this.epcTagSNURN = epcTagSNURN;
                    tagIdInitialized = true;
                }
            }
        }


        //		/// <summary>
        //		/// Parse the EPC SerialNumber(96 bits/64 bits) into standard EPC formats like 
        //		/// SGTIN,SSCC,SGLN,GID etc.
        //		/// </summary>
        //		/// <param name="epcTagSerialNum">It stores the epc TagSerial Number.</param>
        //		/// <returns></returns>
        //		public static  string  ParseEPCSerialNumber ( byte[] epcTagSerialNum)
        //		{
        //			if(epcTagSerialNum == null)
        //				return string.Empty;
        //			string parsedSN = string.Empty;
        //			try 
        //			{			
        //				if(epcTagSerialNum.Length == 8)
        //					parsedSN = ParseEPC64SerialNumber (epcTagSerialNum);
        //				else if(epcTagSerialNum.Length == 12)
        //					parsedSN = EPCBytes.Decode(epcTagSerialNum);
        //			}
        //			catch ( Exception ) {}
        //			return parsedSN;
        //		}

        /*
        /// <summary>
        /// Parse the EPC SerialNumber(96 bits/64 bits).
        /// </summary>
        /// <param name="epcTagSerialNum">It stores the epc TagSerial Number.</param>
        /// <param name="manufId">Manufacturer Id</param>
        /// <param name="productId">Product Id</param>
        /// <param name="serialNum">Serial No</param>
        public static void ParseEPCSerialNumber ( byte[] epcTagSerialNum,
            out string manufId,out string productId,out string serialNum)
        {
            manufId = string.Empty ; 
            productId = string.Empty ; 
            serialNum = string.Empty ; 

            if(epcTagSerialNum == null)
                return ;
			
            if(epcTagSerialNum.Length == 8)
                ParseEPC64SerialNumber(epcTagSerialNum,out manufId,out productId,out serialNum);
            else if(epcTagSerialNum.Length == 12)
                ParseEPC96SerialNumber (epcTagSerialNum,out manufId,out productId,out serialNum);
        }

		
        /// <summary>
        /// Parse the EPC 96 bit SerialNumber.
        /// </summary>
        /// <param name="epcTagSerialNum">It stores the epc TagSerial Number.</param>
        /// <param name="manufId">Manufacturer Id</param>
        /// <param name="productId">Product Id</param>
        /// <param name="serialNum">Serial No</param>
        private static void ParseEPC96SerialNumber ( byte[] epcTagSerialNum,
            out string manufId,out string productId,out string serialNum)
        {
            //Header : 8 bits
            //Company Id : 28 bits
            //Product Id : 24 bits
            //Serial No : 36 bits

            manufId = string.Empty ; 
            productId = string.Empty ; 
            serialNum = string.Empty ; 

            try 
            {
                UInt32 manufCode = epcTagSerialNum[1] ; 

                manufCode = manufCode << 20 ; 
                UInt32 tempByte = (UInt32) epcTagSerialNum[2] ; 
                manufCode |= tempByte << 12 ;
                tempByte = (UInt32) epcTagSerialNum[3] ; 
                manufCode |= tempByte << 4 ; 
                manufCode |= (UInt32) ( epcTagSerialNum[4] >> 4 ) ; 

                manufId = "0x" + manufCode.ToString("X7") ; 

                UInt32  productCode = (UInt32) (epcTagSerialNum[4] & 0xF) ; 
                productCode = productCode << 20 ; 

                tempByte = epcTagSerialNum[5] ; 
                productCode |= tempByte << 12 ; 

                tempByte = epcTagSerialNum[6] ; 
                productCode |= tempByte << 4 ; 

                productCode |= (UInt32) ( epcTagSerialNum[7] >> 4 ) ; 

                productId = "0x" + productCode.ToString("X6") ; 

                byte bt  =  (byte) (epcTagSerialNum[7] & 0xF) ; 
                StringBuilder sb = new StringBuilder( "0x" + bt.ToString("X1") ) ; 
                for ( int cnt = 8 ; cnt < 12 ;  cnt ++  )
                {
                    sb.Append(epcTagSerialNum[cnt].ToString("X2"))  ; 
                }

                serialNum = sb.ToString() ; 
            }
            catch ( Exception ) 
            {
            }
        }
	
        /// <summary>
        /// Parse the EPC96SerialNumber.
        /// </summary>
        /// <param name="epcTagSerialNum">It stores the epc TagSerial Number.</param>
        /// <returns>string value of epc tag serial number.</returns>
        private static  string  ParseEPC96SerialNumber ( byte[] epcTagSerialNum)
        {
            //Header : 8 bits
            //Company Id : 28 bits
            //Product Id : 24 bits
            //Serial No : 36 bits

            try 
            {
                string manufId = string.Empty ; 
                string productId = string.Empty ; 
                string serialNum = string.Empty ; 

                ParseEPC96SerialNumber(epcTagSerialNum,out manufId,out productId,out serialNum);

                StringBuilder toStringStr  = new StringBuilder( ) ; 
                toStringStr.Append("Tag Id is  -- ") ; 
                toStringStr.Append(Environment.NewLine);
                toStringStr.Append("-----------------------------------------");
                toStringStr.Append( Environment.NewLine);

                toStringStr.Append(" Company Identifier: " + manufId + Environment.NewLine) ; 

                toStringStr.Append(" Product Identifier: "  + productId + Environment.NewLine) ; 

                toStringStr.Append(" Serial Number: " +  serialNum + Environment.NewLine) ; 
                toStringStr.Append("-----------------------------------------");
                toStringStr.Append( Environment.NewLine);
								
                return toStringStr.ToString() ; 
            }
            catch ( Exception ) 
            {
                return string.Empty ; 
            }
        }
	

        /// <summary>
        /// Parse the EPC 64 bit SerialNumber.
        /// </summary>
        /// <param name="epcTagSerialNum">It stores the epc TagSerial Number.</param>
        /// <param name="manufId">Manufacturer Id</param>
        /// <param name="productId">Product Id</param>
        /// <param name="serialNum">Serial No</param>
        private static void ParseEPC64SerialNumber ( byte[] epcTagSerialNum,
            out string manufId,out string productId,out string serialNum)
        {
            //Header : 2 bits
            //Company Id : 21 bits
            //Product Id : 17 bits
            //Serial No : 24 bits
            manufId = string.Empty ; 
            productId = string.Empty ; 
            serialNum = string.Empty ; 

            try 
            {
                //Get 6 bits from 0th byte
                UInt32 manufCode = (UInt32) (epcTagSerialNum[0] & 0x3F) ; 
                manufCode = manufCode << 15 ; 

                UInt32 tempByte = (UInt32) epcTagSerialNum[1] ; 
                manufCode |= tempByte << 7 ;

                //Get 7 bits from 2nd byte
                manufCode |= (UInt32) ( epcTagSerialNum[2] >> 1 ) ; 

                manufId = "0x" + manufCode.ToString("X6") ; 

                //Get 1 bit from 2nd byte
                UInt32  productCode = (UInt32) (epcTagSerialNum[2] & 0x1) ; 
                productCode = productCode << 16 ; 

                tempByte = epcTagSerialNum[3] ; 
                productCode |= tempByte << 8 ; 

                tempByte = epcTagSerialNum[4] ; 
                productCode |= tempByte; 

                productId = "0x" + productCode.ToString("X5") ; 

                //Get last 3 bytes
                UInt32  serialNo = epcTagSerialNum[5] ; 
                serialNo = serialNo << 16 ; 

                tempByte = epcTagSerialNum[6] ; 
                serialNo |= tempByte << 8 ; 

                tempByte = epcTagSerialNum[7] ; 
                serialNo |= tempByte; 

                serialNum = "0x" + serialNo.ToString("X6") ; 
            }
            catch ( Exception ) 
            {
            }
        }
	

        /// <summary>
        /// Parse the EPC64SerialNumber.
        /// </summary>
        /// <param name="epcTagSerialNum">It stores the epc TagSerial Number.</param>
        /// <returns>string value of epc tag serial number.</returns>
        private static  string  ParseEPC64SerialNumber ( byte[] epcTagSerialNum)
        {
            //Header : 2 bits
            //Company Id : 21 bits
            //Product Id : 17 bits
            //Serial No : 24 bits
            try 
            {
                string manufId = string.Empty ; 
                string productId = string.Empty ; 
                string serialNum = string.Empty ; 
				
                ParseEPC64SerialNumber(epcTagSerialNum,out manufId,out productId,out serialNum);
				
                StringBuilder toStringStr  = new StringBuilder( ) ; 
				
                toStringStr.Append("Tag Id is  -- ") ; 
                toStringStr.Append(Environment.NewLine);
                toStringStr.Append("-----------------------------------------");
                toStringStr.Append( Environment.NewLine);

                toStringStr.Append(" Company Identifier: " + manufId + Environment.NewLine) ; 

                toStringStr.Append(" Product Identifier: "  + productId + Environment.NewLine) ; 

                toStringStr.Append(" Serial Number: " +  serialNum + Environment.NewLine) ; 
                toStringStr.Append("-----------------------------------------");
                toStringStr.Append( Environment.NewLine);
								
                return toStringStr.ToString() ; 
            }
            catch ( Exception ) 
            {
                return string.Empty ; 
            }
        }
	
*/
        /// <summary>
        /// Creates xml representation of the parsedDataHash
        /// </summary>
        /// <param name="parsedDataHash"></param>
        /// <returns></returns>
        public static string CreateEPCTagXml(Hashtable parsedDataHash)
        {
            MemoryStream xmlStream = new MemoryStream();
            UTF8Encoding encoding = new UTF8Encoding();

            XmlTextWriter writer = new XmlTextWriter(xmlStream, encoding);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartDocument();

            writer.WriteStartElement("RFIDTagConfig");
            writer.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
            writer.Flush();

            writer.WriteStartElement("EPCTagConfig");

            if (parsedDataHash.ContainsKey("SerialNo"))
            {
                writer.WriteStartElement("SerialNo");
                writer.WriteString(Convert.ToString(parsedDataHash["SerialNo"]));
                writer.WriteEndElement();
            }

            if (parsedDataHash.ContainsKey("ProductId"))
            {
                writer.WriteStartElement("ProductId");
                writer.WriteString(Convert.ToString(parsedDataHash["ProductId"]));
                writer.WriteEndElement();
            }

            if (parsedDataHash.ContainsKey("CompanyId"))
            {
                writer.WriteStartElement("CompanyId");
                writer.WriteString(Convert.ToString(parsedDataHash["CompanyId"]));
                writer.WriteEndElement();
            }

            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();

            xmlStream.Seek(0, SeekOrigin.Begin);
            StreamReader sr = new StreamReader(xmlStream);
            string xmlString = sr.ReadToEnd();
            sr.Close();

            return xmlString;
        }
    }



    /// <summary>
    /// Holds the data read from an Unknown RFIDTag and exposes the tag attributes.
    /// </summary>
    [Serializable]
    public class UnknownTag : RFIDTag
    {
        #region Constructors
        /// <summary>
        /// Intializes an instance of UnknownTag class.
        /// </summary>
        public UnknownTag()
        {
            snLengthInBytes = 8;
            Init();
        }
        private void Init()
        {
            maxDataLength = 0;
            minDataLength = 0;
            readPageSize = 0;
            writePageSize = 0;

            lockable = true;
            canRead = true;
            canWrite = true;
            isProprietary = false;
            isISO15693_2Compliant = true;
            isISO15693_3Compliant = true;
            isISO14443_TypeACompliant = false;
            supportsAnticollision = true;

            vendorName = "Unknown";
            description = "Unknown Read/Write Tag";
            tagType = "Unknown";
        }
        #endregion
        /// <summary>
        /// Convert the given data to string format.
        /// </summary>
        /// <returns>Returns information about the tag in string format.</returns>
        public override string ToString()
        {
            return base.ToString() + PrintTagData();
        }

    }

    #region Documentation
#if RD_DOC_EPCClass
	/// <summary>
	/// Holds the data read from an EPC Class0 RFIDTag and exposes 
	/// the tag attributes.
	/// </summary>
#else
    ///<exclude/>
#endif
    #endregion Documentation

    [Serializable]
    public abstract class EPCClass0Tag : EPCTag
    {
        #region Properties
        /// <summary>
        /// Gets a value indicating whether the current tag supports writing the tag id.
        /// </summary>
        public override bool CanWriteTagId
        {
            get
            {
                return false;
            }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Intializes an instance of EPCClass0Tag class with a given tag serial number.
        /// </summary>
        public EPCClass0Tag(byte[] tagSN, string epcTagURN)
            : base(tagSN, epcTagURN)
        {
            Init();
        }

        /// <summary>
        /// Intializes an instance of EPCClass0Tag class.
        /// </summary>
        /// <param name="snLengthInBytes">Size of tag serial number in bytes</param>
        public EPCClass0Tag(int snLengthInBytes)
            : base(snLengthInBytes)
        {
            Init();
        }
        private void Init()
        {
            maxDataLength = 0;
            minDataLength = 0;
            readPageSize = 0;
            writePageSize = 0;

            lockable = false;
            canRead = true;
            canWrite = false;
            isProprietary = false;
            isISO15693_2Compliant = false;
            isISO15693_3Compliant = false;
            isISO14443_TypeACompliant = false;
            supportsAnticollision = true;

            vendorName = string.Empty;
            description = "EPC Class 0 Tag";
            tagType = "EPCClass0";
        }
        #endregion
    }
    #region Documentation
#if RD_DOC_EPCClass
	/// <summary>
	/// Holds the data read from an EPC Class0 RFIDTag with 64 bit EPC code and exposes 
	/// the tag attributes.
	/// </summary>
#else
    ///<exclude/>
#endif
    #endregion Documentation

    [Serializable]
    public class EPCClass0_64BitTag : EPCClass0Tag
    {
        #region Constructors
        /// <summary>
        /// Intializes an instance of EPCClass0_64BitTag class.
        /// </summary>
        public EPCClass0_64BitTag(byte[] tagSN, string epcTagURN)
            : base(tagSN, epcTagURN)
        {
            if (tagSN == null || tagSN.Length != 8)
            {
                string message = string.Empty;
                if (tagSN == null)
                    message = "tagSN is null ";
                else
                    message = "tag SN should be of 8 bytes " + BitConverter.ToString(tagSN);
                throw new Exception("Supports only 64 bit tag serial number." + message);
            }
            Init();
        }
        private void Init()
        {
            maxDataLength = 0;
            minDataLength = 0;
            readPageSize = 0;
            writePageSize = 0;

            lockable = false;
            canRead = false;
            canWrite = false;
            isProprietary = false;
            isISO15693_2Compliant = false;
            isISO15693_3Compliant = false;
            isISO14443_TypeACompliant = false;
            supportsAnticollision = false;

            vendorName = "";
            description = "EPC Class 0 Tag - 64 bits";

        }
        #endregion
    }

    #region Documentation
#if RD_DOC_EPCClass
	/// <summary>
	/// Holds the data read from an EPC Class0 RFIDTag with 96 bit EPC code and exposes 
	/// the tag attributes.
	/// </summary>
#else
    ///<exclude/>
#endif
    #endregion Documentation

    [Serializable]
    public class EPCClass0_96BitTag : EPCClass0Tag
    {
        #region Constructors
        /// <summary>
        /// Intializes an instance of EPCClass0_96BitTag class.
        /// </summary>
        public EPCClass0_96BitTag(byte[] tagSN, string epcTagURN)
            : base(tagSN, epcTagURN)
        {
            if (tagSN == null || tagSN.Length != 12)
            {
                string message = string.Empty;
                if (tagSN == null)
                    message = "tagSN is null ";
                else
                    message = "tag SN should be of 12 bytes " + BitConverter.ToString(tagSN);
                throw new Exception("Supports only 96 bit tag serial number." + message);
            }
            Init();
        }

        private void Init()
        {
            maxDataLength = 0;
            minDataLength = 0;
            readPageSize = 0;
            writePageSize = 0;

            lockable = false;
            canRead = false;
            canWrite = false;
            isProprietary = false;
            isISO15693_2Compliant = false;
            isISO15693_3Compliant = false;
            isISO14443_TypeACompliant = false;
            supportsAnticollision = false;

            vendorName = "";
            description = "EPC Class 0 Tag - 96 bits";

        }
        #endregion
    }


    #region Documentation
#if RD_DOC_EPCClass
	/// <summary>
	/// Holds the data read from an EPC Class0+ RFIDTag with 64 bit EPC code and exposes 
	/// the tag attributes.
	/// </summary>
#else
    ///<exclude/>
#endif
    #endregion Documentation
    [Serializable]
    public class EPCClass0Plus_64BitTag : EPCClass0_64BitTag
    {
        #region Constructors
        /// <summary>
        /// Intializes an instance of EPCClass0Plus_64BitTag class.
        /// </summary>
        public EPCClass0Plus_64BitTag(byte[] tagSN, string epcTagURN)
            : base(tagSN, epcTagURN)
        {
            Init();
        }

        private void Init()
        {
            maxDataLength = 13;
            minDataLength = 0;
            readPageSize = 13;
            writePageSize = 13;

            lockable = true;
            canRead = true;
            canWrite = true;

            description = "EPC Class 0+ Tag - 64 bits";

        }
        #endregion
    }

    #region Documentation
#if RD_DOC_EPCClass
	/// <summary>
	/// Holds the data read from an EPC Class0+ RFIDTag with 96 bit EPC code and exposes 
	/// the tag attributes.
	/// </summary>
#else
    ///<exclude/>
#endif
    #endregion Documentation
    [Serializable]
    public class EPCClass0Plus_96BitTag : EPCClass0_96BitTag
    {
        #region Constructors
        /// <summary>
        /// Intializes an instance of EPCClass0_96BitTag class.
        /// </summary>
        public EPCClass0Plus_96BitTag(byte[] tagSN, string epcTagURN)
            : base(tagSN, epcTagURN)
        {
            Init();
        }

        private void Init()
        {
            maxDataLength = 13;
            minDataLength = 0;
            readPageSize = 13;
            writePageSize = 13;

            lockable = true;
            canRead = true;
            canWrite = true;

            description = "EPC Class 0+ Tag - 96 bits";

        }
        #endregion
    }


    #region Documentation
#if RD_DOC_EPCClass
	/// <summary>
	/// Holds the data read from an EPC Class1 RFIDTag and exposes 
	/// the tag attributes.
	/// </summary>
#else
    ///<exclude/>
#endif
    #endregion Documentation

    [Serializable]
    public abstract class EPCClass1Tag : EPCTag
    {
        #region Properties

        /// <summary>
        /// Gets a value indicating whether the current tag supports writing the tag id.
        /// </summary>
        public override bool CanWriteTagId
        {
            get
            {
                //Write the tag id only once.
                if (tagIdInitialized)
                    return false;
                return true;
            }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Intializes an instance of EPCClass1Tag class.
        /// </summary>
        public EPCClass1Tag()
            : base()
        {
            Init();
        }

        /// <summary>
        /// Intializes an instance of EPCClass1Tag class with a given tag serial number.
        /// </summary>
        /// <param name="tagSN">Byte array containing the tag serial number.</param>
        /// <param name="epcTagURN">EPC URN.</param>
        public EPCClass1Tag(byte[] tagSN, string epcTagURN)
            : base(tagSN, epcTagURN)
        {
            Init();
        }

        /// <summary>
        /// Intializes an instance of EPCClass1Tag class.
        /// </summary>
        /// <param name="snLengthInBytes">Size of tag serial number in bytes</param>
        public EPCClass1Tag(int snLengthInBytes)
            : base(snLengthInBytes)
        {
            Init();
        }

        private void Init()
        {
            maxDataLength = 0;
            minDataLength = 0;
            readPageSize = 0;
            writePageSize = 0;

            lockable = false;
            canRead = true;
            canWrite = false;
            isProprietary = false;
            isISO15693_2Compliant = false;
            isISO15693_3Compliant = false;
            isISO14443_TypeACompliant = false;
            supportsAnticollision = true;

            vendorName = string.Empty;
            description = "EPC Class 1 Tag";
            tagType = "EPCClass1";
        }
        #endregion
    }

    [Serializable]
    public abstract class EPCClass3Tag : EPCTag
    {
        #region Properties

        /// <summary>
        /// Gets a value indicating whether the current tag supports writing the tag id.
        /// </summary>
        public override bool CanWriteTagId
        {
            get
            {
                //Write the tag id only once.
                if (tagIdInitialized)
                    return false;
                return true;
            }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Intializes an instance of EPCClass3Tag class.
        /// </summary>
        public EPCClass3Tag()
            : base()
        {
            Init();
        }

        /// <summary>
        /// Intializes an instance of EPCClass3Tag class with a given tag serial number.
        /// </summary>
        /// <param name="tagSN">Byte array containing the tag serial number.</param>
        /// <param name="epcTagURN">EPC URN.</param>
        public EPCClass3Tag(byte[] tagSN, string epcTagURN)
            : base(tagSN, epcTagURN)
        {
            Init();
        }

        /// <summary>
        /// Intializes an instance of EPCClass3Tag class.
        /// </summary>
        /// <param name="snLengthInBytes">Size of tag serial number in bytes</param>
        public EPCClass3Tag(int snLengthInBytes)
            : base(snLengthInBytes)
        {
            Init();
        }

        private void Init()
        {
            maxDataLength = 0;
            minDataLength = 0;
            readPageSize = 0;
            writePageSize = 0;

            lockable = false;
            canRead = true;
            canWrite = false;
            isProprietary = false;
            isISO15693_2Compliant = false;
            isISO15693_3Compliant = false;
            isISO14443_TypeACompliant = false;
            supportsAnticollision = true;

            vendorName = string.Empty;
            description = "EPC Class 3 Tag";
            tagType = "EPCClass3";
        }
        #endregion
    }
    #region Documentation
#if RD_DOC_EPCClass
	/// <summary>
	/// Holds the data read from an EPC Class1 RFIDTag with 64 bit EPC code and exposes 
	/// the tag attributes.
	/// </summary>
#else
    ///<exclude/>
#endif
    #endregion Documentation

    [Serializable]
    public class EPCClass1_64BitTag : EPCClass1Tag
    {
        #region Constructors

        /// <summary>
        /// Intializes an instance of EPCClass1_64BitTag class.
        /// </summary>
        public EPCClass1_64BitTag()
            : base(8)
        {
            Init();
        }

        /// <summary>
        /// Intializes an instance of EPCClass1_64BitTag class.
        /// </summary>
        public EPCClass1_64BitTag(byte[] tagSN, string epcTagURN)
            : base(tagSN, epcTagURN)
        {
            if (tagSN == null || tagSN.Length != 8)
            {
                string message = string.Empty;
                if (tagSN == null)
                    message = "tagSN is null ";
                else
                    message = "tag SN should be of 8 bytes " + BitConverter.ToString(tagSN);
                throw new Exception("Supports only 64 bit tag serial number." + message);
            }
            Init();
        }
        private void Init()
        {
            maxDataLength = 0;
            minDataLength = 0;
            readPageSize = 0;
            writePageSize = 0;

            lockable = false;
            canRead = false;
            canWrite = false;
            isProprietary = false;
            isISO15693_2Compliant = false;
            isISO15693_3Compliant = false;
            isISO14443_TypeACompliant = false;
            supportsAnticollision = false;

            vendorName = "";
            description = "EPC Class 1 Tag - 64 bits";

        }
        #endregion
    }

    #region Documentation
#if RD_DOC_EPCClass
	/// <summary>
	/// Holds the data read from an EPC Class1 RFIDTag with 96 bit EPC code and exposes 
	/// the tag attributes.
	/// </summary>
#else
    ///<exclude/>
#endif
    #endregion Documentation

    [Serializable]
    public class EPCClass1_96BitTag : EPCClass1Tag
    {
        #region Constructors
        /// <summary>
        /// Intializes an instance of EPCClass1_96BitTag class.
        /// </summary>
        public EPCClass1_96BitTag()
            : base(12)
        {
            Init();
        }

        /// <summary>
        /// Intializes an instance of EPCClass1_96BitTag class.
        /// </summary>
        public EPCClass1_96BitTag(byte[] tagSN, string epcTagURN)
            : base(tagSN, epcTagURN)
        {
            if (tagSN == null || tagSN.Length != 12)
            {
                string message = string.Empty;
                if (tagSN == null)
                    message = "tagSN is null ";
                else
                    message = "tag SN should be of 12 bytes " + BitConverter.ToString(tagSN);
                throw new Exception("Supports only 96 bit tag serial number." + message);
            }
            Init();
        }

        private void Init()
        {
            maxDataLength = 0;
            minDataLength = 0;
            readPageSize = 0;
            writePageSize = 0;

            lockable = false;
            canRead = false;
            canWrite = false;
            isProprietary = false;
            isISO15693_2Compliant = false;
            isISO15693_3Compliant = false;
            isISO14443_TypeACompliant = false;
            supportsAnticollision = false;

            vendorName = "";
            description = "EPC Class 1 Tag - 96 bits";

        }
        #endregion
    }

    [Serializable]
    public class EPCClass3_96BitTag : EPCClass3Tag
    {
        #region Constructors
        /// <summary>
        /// Intializes an instance of EPCClass3_96BitTag class.
        /// </summary>
        public EPCClass3_96BitTag()
            : base(12)
        {
            Init();
        }

        /// <summary>
        /// Intializes an instance of EPCClass3_96BitTag class.
        /// </summary>
        public EPCClass3_96BitTag(byte[] tagSN, string epcTagURN)
            : base(tagSN, epcTagURN)
        {
            if (tagSN == null || tagSN.Length != 12)
            {
                string message = string.Empty;
                if (tagSN == null)
                    message = "tagSN is null ";
                else
                    message = "tag SN should be of 12 bytes " + BitConverter.ToString(tagSN);
                throw new Exception("Supports only 96 bit tag serial number." + message);
            }
            Init();
        }

        private void Init()
        {
            maxDataLength = 0;
            minDataLength = 0;
            readPageSize = 0;
            writePageSize = 0;

            lockable = false;
            canRead = false;
            canWrite = false;
            isProprietary = false;
            isISO15693_2Compliant = false;
            isISO15693_3Compliant = false;
            isISO14443_TypeACompliant = false;
            supportsAnticollision = false;

            vendorName = "";
            description = "EPC Class 3 Tag - 96 bits";

        }
        #endregion
    }



    #region Documentation
#if RD_DOC_EPCClass
	/// <summary>
	/// Holds the data read from an EPC Class1 Gen2 RFIDTag with 64 bit EPC code and exposes 
	/// the tag attributes.
	/// </summary>
#else
    ///<exclude/>
#endif
    #endregion Documentation
    [Serializable]
    public class EPCClass1Gen2_64BitTag : EPCClass1Tag
    {
        #region Constructors

        /// <summary>
        /// Intializes an instance of EPCClass1Gen2_64BitTag class.
        /// </summary>
        public EPCClass1Gen2_64BitTag()
            : base(8)
        {
            Init();
        }

        /// <summary>
        /// Intializes an instance of EPCClass1GEn2_64BitTag class.
        /// </summary>
        public EPCClass1Gen2_64BitTag(byte[] tagSN, string epcTagURN)
            : base(tagSN, epcTagURN)
        {
            if (tagSN == null || tagSN.Length != 8)
            {
                string message = string.Empty;
                if (tagSN == null)
                    message = "tagSN is null ";
                else
                    message = "tag SN should be of 8 bytes " + BitConverter.ToString(tagSN);
                throw new Exception("Supports only 64 bit tag serial number." + message);
            }
            Init();
        }
        private void Init()
        {
            maxDataLength = 0;
            minDataLength = 0;
            readPageSize = 0;
            writePageSize = 0;

            lockable = false;
            canRead = false;
            canWrite = false;
            isProprietary = false;
            isISO15693_2Compliant = false;
            isISO15693_3Compliant = false;
            isISO14443_TypeACompliant = false;
            supportsAnticollision = false;
            tagType = "EPCClass1Gen2";
            vendorName = "";
            description = "EPC Class 1 Gen 2 Tag - 64 bits";

        }
        #endregion
    }

    #region Documentation
#if RD_DOC_EPCClass
	/// <summary>
	/// Holds the data read from an EPC Class1 Gen2 RFIDTag with 96 bit EPC code and exposes 
	/// the tag attributes.
	/// </summary>
#else
    ///<exclude/>
#endif
    #endregion Documentation

    [Serializable]
    public class EPCClass1Gen2_96BitTag : EPCClass1Tag
    {
        #region Constructors
        /// <summary>
        /// Intializes an instance of EPCClass1Gen2_96BitTag class.
        /// </summary>
        public EPCClass1Gen2_96BitTag()
            : base(12)
        {
            Init();
        }

        /// <summary>
        /// Intializes an instance of EPCClass1Gen2_96BitTag class.
        /// </summary>
        public EPCClass1Gen2_96BitTag(byte[] tagSN, string epcTagURN)
            : base(tagSN, epcTagURN)
        {
            if (tagSN == null || tagSN.Length != 12)
            {
                string message = string.Empty;
                if (tagSN == null)
                    message = "tagSN is null ";
                else
                    message = "tag SN should be of 12 bytes " + BitConverter.ToString(tagSN);
                throw new Exception("Supports only 96 bit tag serial number." + message);
            }
            Init();
        }

        private void Init()
        {
            maxDataLength = 0;
            minDataLength = 0;
            readPageSize = 0;
            writePageSize = 0;

            lockable = false;
            canRead = false;
            canWrite = false;
            isProprietary = false;
            isISO15693_2Compliant = false;
            isISO15693_3Compliant = false;
            isISO14443_TypeACompliant = false;
            supportsAnticollision = false;
            tagType = "EPCClass1Gen2";
            vendorName = "";
            description = "EPC Class 1 Gen 2 Tag - 96 bits";

        }
        #endregion
    }



    #region Documentation
#if RD_DOC_OMRONV720
		/// <summary>
		/// Holds the data read from an Omron V720 Series Tag and exposes the tag attributes.
		/// </summary>
#else
    ///<exclude/>
#endif
    #endregion Documentation
    [Serializable]
    public class OmronV720SeriesTag : RFIDTag
    {
        #region Attributes

        /// <summary>
        /// Application Id of the current Tag.
        /// </summary>
        protected int applicationId = 0;

        #endregion

        #region Properties

        /// <summary>
        /// Gets/sets the applicationId of the current tag.
        /// </summary>
        public int ApplicationId
        {
            get
            {
                return applicationId;
            }
            set
            {
                applicationId = value;
            }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Intializes an instance of OmronV720SeriesTag class.
        /// </summary>
        public OmronV720SeriesTag()
        {
            snLengthInBytes = 8;
            Init();
        }

        private void Init()
        {
            maxDataLength = 44;
            minDataLength = 1;
            readPageSize = 4;
            writePageSize = 4;

            lockable = true;
            canRead = true;
            canWrite = true;
            isProprietary = true;
            isISO15693_2Compliant = false;
            isISO15693_3Compliant = false;
            isISO14443_TypeACompliant = false;
            supportsAnticollision = true;

            vendorName = "Omron";
            description = "Omron V720 Series Tag";
            tagType = "Omron";
        }

        #endregion
        /// <summary>
        /// Convert the given data to string format.
        /// </summary>
        /// <returns>Returns information about the tag in string format.</returns>
        public override string ToString()
        {
            return base.ToString() + PrintTagData();
        }
    }


    /// <summary>
    /// Holds the data read from a LR512 specific RFIDTag and exposes 
    /// the tag attributes.
    /// </summary>
    [Serializable]
    public class ISO_18000_6BTag : RFIDTag
    {
        #region Constructors
        /// <summary>
        /// Intializes an instance of LR512Tag class.
        /// </summary>
        public ISO_18000_6BTag()
        {
            snLengthInBytes = 8;
            Init();
        }

        private void Init()
        {
            maxDataLength = 256;
            minDataLength = 0;
            readPageSize = 24;
            writePageSize = 1;

            lockable = true;
            canRead = true;
            canWrite = true;
            isProprietary = false;
            isISO15693_2Compliant = false;
            isISO15693_3Compliant = false;
            isISO14443_TypeACompliant = false;
            supportsAnticollision = false;

            vendorName = "";
            description = "ISO 18000-6 Type B tag";
            tagType = "ISO_18000_6B";
        }

        /// <summary>
        /// Convert the given data to string format.
        /// </summary>
        /// <returns>Returns information about the tag in string format.</returns>
        public override string ToString()
        {
            return base.ToString() + PrintTagData();
        }
        #endregion
    }
    /// <summary>
    /// Base class of Intelleflex Tags
    /// </summary>
    [Serializable]
    public class Class3Base : EPCClass3_96BitTag
    {
        private uint m_ReadCount = 1;
        private string m_epcId = string.Empty;

        public Class3Base(byte[] tagSN, string epcTagURN)
            : base(tagSN, epcTagURN)
        {
            Init();
        }

        private void Init()
        {
            tagType = "Class3";
            vendorName = "";
        }

        /// <summary>
        /// 
        /// </summary>
        public uint ReadCount
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
        /// 
        /// </summary>
        public string EPCID
        {

            get
            {
                return m_epcId;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="epcCode"></param>
        /// <returns></returns>
        protected byte[] GetTagSN(string epcCode)
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

    }

    /// <summary>
    /// Derived class for Intlleflex Tags
    /// </summary>
    [Serializable]
    public class Class3IFXTag : Class3Base
    {
        #region [ member vars ]
        //private uint m_WritePageSize = 0;
        //private uint m_ReadPageSize = 0;        

        private uint m_RSSI = 0;

        //private string m_Epc = string.Empty;

        #endregion [ member vars ]

        /// <summary>
        /// Constructor for Class3 Tag
        /// </summary>
        /// <param name="epcID"></param>
        /// <param name="readerName"></param>
        /// <param name="antennaName"></param>
        /// <param name="rssi"></param>
        public Class3IFXTag(byte[] tagSN, string epcTagURN)
            : base(tagSN, epcTagURN)
        {
            if (tagSN == null || tagSN.Length != 12)
            {
                string message = string.Empty;
                if (tagSN == null)
                    message = "tagSN is null ";
                else
                    message = "tag SN should be of 12 bytes " + BitConverter.ToString(tagSN);
                throw new Exception("Supports only 96 bit tag serial number." + message);
            }
            description = "Class3 Semi Passive Tags";
            vendorName = "Intelleflex Corporation";
        }



        /// <summary>
        /// Receive Signal Strength Indication 
        /// </summary>
        public uint RSSI
        {
            get
            {
                return m_RSSI;
            }
            set
            {
                m_RSSI = value;
            }
        }

        /// <summary>
        /// Gives string representation of Tag
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder strbuilder = new StringBuilder();
            strbuilder.AppendLine("EPC Code              : " + EPCID);
            strbuilder.AppendLine("Tag Type              : " + TagType);
            strbuilder.AppendLine("Reader Name           : " + ReaderName);
            strbuilder.AppendLine("Antenna Name          : " + AntennaName);
            strbuilder.AppendLine("RSSI                  : " + RSSI);
            strbuilder.AppendLine("READ Count            : " + ReadCount);
            return strbuilder.ToString();
        }
    }

}
