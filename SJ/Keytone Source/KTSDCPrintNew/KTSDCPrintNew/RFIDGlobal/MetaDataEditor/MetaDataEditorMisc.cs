
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
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
namespace KTone.RFIDGlobal.MetaDataEditor
{
    //Enum names should be same as RFIDTag class names.
    public enum TagType
    {
        UnknownTag,
        TagItHFITag,
        ICodeSLISL2Tag,
        MyDSRF55VO2PTag,
        MyDSRF55V10PTag,
        LR512Tag,
        TagItHFTag,
        ICode1SL1Tag,
        PicoTag2KTag,
        MatricsTag,
        LegacyMatricsTag,
        EMSLRPTag,
        EMSHMSTag,
        EMSHMS_4kTag,
        EMSLRP_STag,
        EMSLRP_I_10kBitsTag,
        EMSLRP_I_2kBitsTag,
        Mifare4kTag,
        MifareUltraLiteTag,
        Mifare1kTag,
        EPC_64BitTag,
        EPC_96BitTag,
        ISO_18000_6BTag,
    };

    public enum UCCNumberingStandard
    {
        Unknown,
        SGTIN,
        SSCC,
        SGLN,
        GRAI,
        GIAI,
        GID,
        USDOD,
        LCTN,
        ASET
    };

    public enum TemplateOperation
    {
        // Template is added in the cache
        TEMPLATE_ADDED,
        // template is modified 
        TEMPLATE_MODIFIED,
        // template is deleted from the cache
        TEMPLATE_DELETED
    }

    /// <summary>
    /// description for TemplateElement.
    /// </summary>
    public struct TemplateElement
    {
        public string Name;
        public string DataType;
        public int StartByteIndex;
        public int Length;
        public int StartBitIndex;
    }

    [Serializable]
    /// <summary>
    /// Information of Template
    /// </summary>
    public class TemplateInfo
    {
        #region Attributes

        private string templateId = string.Empty;
        private string templateName = string.Empty;
        private uint maxNoOfBytes;
        private bool isLittleEndian;
        private string template = string.Empty;
        private string description = string.Empty;
        private SortedDictionary<int, int> blockInfo = new SortedDictionary<int, int>();

        #endregion Attributes

        #region Constructor

        public TemplateInfo(string templateId, string templateName,
            uint maxNoOfBytes, bool isLittleEndian, string template, string description)
        {
            this.templateId = templateId;
            this.templateName = templateName;
            this.maxNoOfBytes = maxNoOfBytes;
            this.isLittleEndian = isLittleEndian;
            this.template = template;
            this.description = description;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Template Id
        /// </summary>
        public string TemplateId
        {
            get { return templateId; }
        }

        /// <summary>
        /// Template name to identify template.
        /// </summary>
        public string TemplateName
        {
            get { return templateName; }            
        }

        /// <summary>
        /// Maximum number of bytes in template.
        /// </summary>
        public uint MaxNoOfBytes
        {
            get { return maxNoOfBytes; }            
        }

        /// <summary>
        /// Return id isLittleEndian.
        /// </summary>
        public bool IsLittleEndian
        {
            get { return isLittleEndian; }            
        }

        /// <summary>
        /// Xml representation of template in string.
        /// </summary>
        public string Template
        {
            get { return template; }            
        }

        /// <summary>
        /// Gives addtional information about template.
        /// </summary>
        public string Description
        {
            get { return description; }            
        }

        /// <summary>
        /// Data block breakup information for template.
        /// Gives sorted dictionary of startindex and length.
        /// <para>Key is startindex </para>
        /// <para>Value is length</para>
        /// </summary>
        public SortedDictionary<int, int> DataBlockInfo
        {
            get { return blockInfo; }
            set { blockInfo = value; }
        }
        #endregion Properties

        #region Methods

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(1000);
            
            try
            {
                sb.Append("Template Id : " + this.templateId);
                sb.Append(Environment.NewLine);
                sb.Append("Template Name : " + this.templateName);
                sb.Append(Environment.NewLine);
                sb.Append("IsLittleEndian : " + this.isLittleEndian);
                sb.Append(Environment.NewLine);
                sb.Append("TemplateXml : ");
                sb.Append(Environment.NewLine);
                XmlTextWriter textWriter = new XmlTextWriter(new StringWriter(sb));
                textWriter.Formatting = Formatting.Indented;
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(this.template);
                xDoc.WriteTo(textWriter);
                textWriter.Close();
            }
            catch { }

            return sb.ToString();
        }

        #endregion
    }


    /// <summary>
    /// MetaDataException.
    /// </summary>
    public class MetaDataException : System.Exception
    {
        private static NLog.Logger m_Log = KTone.RFIDGlobal.KTLogger.KTLogManager.GetLogger();

        #region Constructors

        public MetaDataException(string msg)
            : base(msg)
        {
            m_Log.Error(msg);
        }

        public MetaDataException(string msg, Exception ex)
            : base(msg, ex)
        {
            m_Log.Error(msg, ex);
        }

        public MetaDataException(string msgFormat, params object[] lstParam)
            : base(String.Format(msgFormat, lstParam))
        {
            m_Log.Error(String.Format(msgFormat, lstParam));
        }

        #endregion Constructors

    }



}