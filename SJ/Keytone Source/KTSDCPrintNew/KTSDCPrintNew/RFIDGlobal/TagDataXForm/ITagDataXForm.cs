using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using KTone.RFIDGlobal.MetaDataEditor;

namespace KTone.RFIDGlobal.TagDataXForm
{
    /// <summary>
    /// The ItagDataXForm provides transformation service for binary data into 
    /// XML stream based on Meta Data Definition of binary data
    /// Each meta data definition is also stored in XML file and it is refered by
    /// a unique template ID ( templateID as refered in the API documentation ) 
    /// </summary>

    #region Enumerations
    /// <summary>
    /// Defines the operation performed by the refresh method
    /// </summary>
    public enum OperationStatus
    {
        // Template is added in the cache
        TEMPLATE_ADDED,
        // template is modified 
        TEMPLATE_MODIFIED,
        // template is deleted from the cache
        TEMPLATE_DELETED
    }
    #endregion enumeration


    #region Interface
    public interface ITagDataXForm
    {
        #region Properties

        /// <summary>
        /// Returns the count of templates cached in the Module 
        /// </summary>
        int Count
        {
            get;
        }

        string BaseDirectory
        {
            get;
        }

        #endregion Properties


        #region Methods
        /// <summary>
        /// Refreshes all the TAG Templates definition from Meta-File-definitions stored in the files.
        /// </summary>
        void RefreshAll();
        /// <summary>
        /// Refresh only one TAG Template identified by templateID. Throws exception for Invalid , non-existing 
        /// or Malformed XML This method performs Add, Delete or Update based on if 
        /// Meta-definition file is added, deleted or modified. 
        /// </summary>
        /// <param name="templateID"></param>
        void Refresh(string templateID);
        /// <summary>
        /// 
        /// Refresh only one TAG Template identified by templateID. Throws exception for Invalid , non-existing 
        /// or Malformed XML This method performs Add, Delete or Update based on if 
        /// Meta-definition file is added, deleted or modified. Optionally, returns the operation as ADDED/MODIFIED/DELETED 
        /// </summary>
        /// <param name="templateID"></param>
        /// <param name="status"></param>
        void Refresh(string templateID, out OperationStatus operation);


        /// <summary>
        /// Main method that takes InArr bytes of array and transforms it into XML string 
        /// as per the Tag Template definition 
        /// </summary>
        /// <param name="templateID"></param>
        /// <param name="TagType"></param>
        /// <param name="inArray"></param>
        /// <param name="maxTagDataSize"></param>
        /// return transformed string
        string Transform(string templateID, TagType Tag, byte[] inArr, int maxTagDataSize);


        /// <summary>
        /// Main method that takes InArr bytes of array and transforms it into XML string 
        /// as per the Tag Template definition ,from the given index.
        /// </summary>
        /// <param name="templateID"></param>
        /// <param name="TagType"></param>
        /// <param name="inArray"></param>
        /// <param name="maxTagDataSize"></param>
        /// return transformed string
        string Transform(string templateID, TagType Tag, byte[] inArr, int idxInTag, int maxTagDataSize);

        ParsedData[] Transform(string templateID, TagType tagForXForm, Dictionary<string, string> tagDataHash,
            int maxTagDataSize, bool getParsedDataBytes);
        /// <summary>
        /// Main method that takes InArr bytes of array and transforms it into dictionary
        /// as per the Tag Template definition 
        /// </summary>
        /// <param name="templateID"></param>
        /// <param name="TagType"></param>
        /// <param name="inArray"></param>
        /// <param name="maxTagDataSize"></param>
        /// return transformed string
        Dictionary<string, object> Transform(string templateID, byte[] inArr, int maxTagDataSize);


        /// <summary>
        /// Main method that takes xmlDataTemplate string in xml and transforms it into byte array 
        /// as per the Tag Template definition 
        /// </summary>
        /// <param name="templateID"></param>
        /// <param name="TagForXForm"></param>
        /// <param name="xmlDataTemplate"></param>
        /// <param name="maxTagDataSize"></param>
        /// <returns>byte array</returns>
        byte[] Transform(string templateID, TagType TagForXForm, string xmlDataTemplate,
            int maxTagDataSize);



        /// <summary>
        /// Returns all the template Ids of that are cached in the module. This list might vary from 
        /// templates stored on the disk. 
        /// </summary>
        /// <returns></returns>
        string[] GetAllTemplateIDs();

        /// <summary>
        /// Checks if a given Template is a Valid Template ID 
        /// </summary>
        /// <param name="templateID"></param>
        /// <returns></returns>
        bool IsValid(string templateID);

        /// <summary>
        /// Validate a Template identified by templateID again a XSD file. 
        /// Throws Exception for a Invalid template ID 
        /// </summary>
        /// <param name="templateID"></param>
        /// <returns></returns>
        bool Validate(string templateID);

        #endregion  Methods

    }
    #endregion // interface
}
