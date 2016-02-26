using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace KTone.Core.KTIRFID
{
    /// <summary>
    /// Interface for Tag attributes..
    /// <exclude />
    /// </summary>
    public interface IRFIDTag
    {

        #region Methods
        /// <summary>
		/// Returns the tag data from the given start address for the given read length 
		/// </summary>
		/// <param name="startAddress"></param>
		/// <param name="readLength"></param>
		/// <returns></returns>
        byte[] GetTagData(int startAddress, int readLength);

        /// <summary>
		/// Sets the tag data from the given start address for the given write length 
		/// </summary>
		/// <param name="startAddress"></param>
		/// <param name="writeLength"></param>
		/// <param name="writeData"></param>
        void SetTagData(int startAddress, int writeLength, byte[] writeData);

        /// <summary>
		/// Parses the tag serial no.
		/// </summary>
		/// <returns></returns>
        Hashtable ParseTagSNXml();

        /// <summary>
		/// Parses the tag data stored in tag data xml.
		/// </summary>
		/// <returns></returns>
        Hashtable ParseTagDataXml();

        /// <summary>
		/// Compares the given tag serial number with the current serial no.
		/// of current tag.
		/// </summary>
		/// <param name="requestedTagSN"></param>
		/// <returns></returns>
        bool CompareTagSN(byte[] requestedTagSN);

        /// <summary>
        /// Modify attributes of current object(this) using input parameter tag
        /// </summary>
        /// <param name="tag"></param>
        void UpdateTag(IRFIDTag tag);
       
        #endregion Methods

        #region Properties
       
        /// <summary>
        /// When overridden in a derived class, gets/sets the tag data.
        /// </summary>
        byte[] TagData
        {
            get;
            set;

        }
        /// <summary>
        /// Gets/sets the tag serial number. In the EPC classes it 
        /// assigned the EPCTagSN_URN also
        /// </summary>
          byte[] TagSN
        {
            get;
            set;
            
        }

          int RSSI
          {

              get;
              set;

          }
        /// <summary>
        /// Gets the string representation of tag serial number(hex).
        /// </summary>
        string TagSNBytes
        {
            get;

        }
       
        /// <summary>
        /// Returns the number of bytes used by tag serial number.
        /// </summary>
        int SNLengthInBytes
        {
            get;

        }
        /// <summary>
        /// Returns the number of bits used by tag serial number.
        /// </summary>
        int SNLengthInBits
        {
            get;
            
        }

        /// <summary>
        /// Returns true for EPC tags else returns false 
        /// </summary>
        bool IsEPCTag
        {
            get;

        }

        /// <summary>
        /// When overridden in a derived class, gets/sets a flag indicating whether 
        /// this tag is lockable.
        /// </summary>
        bool Lockable
        {
            get;
            
        }
        /// <summary>
        /// When overridden in a derived class, returns the maximum number of bytes 
        /// which can be accessed .
        /// </summary>
         int MaxDataLength
        {
            get;
            
        }
        /// <summary>
        /// When overridden in a derived class,returns the minimum number of bytes 
        /// which can be accessed.
        /// </summary>
          int MinDataLength
        {
            get;
            
        }
        /// <summary>
        /// When overridden in a derived class, gets a value indicating whether the 
        /// current tag supports reading.
        /// </summary>
          bool CanRead
        {
            get;
            
        }
        /// <summary>
        /// When overridden in a derived class, gets a value indicating whether the 
        /// current tag supports writing.
        /// </summary>
          bool CanWrite
        {
            get;
            
        }
        /// <summary>
        /// When overridden in a derived class, gets a value indicating whether the 
        /// current tag is proprietary.
        /// </summary>
        bool IsProprietary
        {
            get;
            
        }
        ///// <summary>
        ///// When overridden in a derived class, gets a value indicating whether the 
        ///// current tag is ISO15693-2 Compliant.
        ///// </summary>
        //bool IsISO15693_2Compliant
        //{
        //    get;
            
        //}
        ///// <summary>
        ///// When overridden in a derived class, gets a value indicating whether the 
        ///// current tag is ISO15693-3 Compliant.
        ///// </summary>
        //bool IsISO15693_3Compliant
        //{
        //    get;
            
        //}
        /// <summary>
        /// When overridden in a derived class,gets a value indicating whether the 
        /// current tag supports anticollision.
        /// </summary>
        bool SupportsAnticollision
        {
            get;
        }
        /// <summary>
        /// When overridden in a derived class, gets/sets the read page size for 
        /// the tag.
        /// </summary>
        int ReadPageSize
        {
            get;
            set;
        }
        /// <summary>
        /// When overridden in a derived class, gets/sets the write page size for 
        /// the tag.
        /// </summary>
        int WritePageSize
        {
            get;
            set;
        }
        /// <summary>
        /// When overridden in a derived class, returns the vendor name.
        /// </summary>
        string TagVendorName
        {
            get;

        }
        /// <summary>
        /// When overridden in a derived class, returns the tag description.
        /// </summary>
        string Description
        {
            get;
            set;
        }

        /// <summary>
        /// When overridden in a derived class, returns the tag type.
        /// </summary>
        string TagType
        {
            get;

        }

        /// <summary>
        /// When overridden in a derived class, gets a value indicating whether the 
        /// current tag supports writing the tag id.
        /// </summary>
        bool CanWriteTagId
        {
            //Only for EPC0 and EPC1,this property is overridden.
            //Otherwise it returns the same value as that of CanWrite property.
            get;

        }

        /// <summary>
        /// Gets a value indicating whether the tag id of the current tag 
        /// is initialized.
        /// </summary>
        bool TagIdInitialized
        {
            get;

        }

        /// <summary>
        /// Gets/sets the tagFamily Index of the current tag.
        /// </summary>
        int TagFamily
        {
            get;
            set;
        }
        /// <summary>
        /// Gets/sets the parsed tag serial number in xml format.
        /// </summary>
        string TagSNXml
        {
            get;
            set;
        }

        /// <summary>
        /// Gets/sets the parsed tag data in xml format.
        /// </summary>
        string TagDataXml
        {
            get;
            set;
        }


        /// <summary>
        /// Returns a list of available tag classes.
        /// </summary>
        string[] TagClassNames
        {
            get;

        }

        /// <summary>
        /// Returns the concrete derived class types of RFIDTag
        /// </summary>
         Type[] TagClassTypes
        {
            get;
            
        }

        /// <summary>
        /// Gets/sets Id of the RFIDReader that read this tag
        /// </summary>
         string ReaderId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets/sets name of the RFIDReader that read this tag
        /// </summary>
         string ReaderName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets/sets Id of the antenna where this tag was detected.
        /// </summary>
         string AntennaId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets/sets name of the antenna where this tag was detected.
        /// </summary>
        string AntennaName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets/sets the tag read count.
        /// </summary>
        int TagReadCount
        {
            get;
            set;
        }
        /// <summary>
        /// Gets/sets the last seen time.
        /// </summary>
        string LastSeenTime
        {
            get;
            set;
        }


        #endregion Properties
    }
}
