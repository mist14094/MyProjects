using System;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using KTone.RFIDGlobal.MetaDataEditor;
using System.Xml.Schema;
using System.IO;
using NLog;
namespace KTone.RFIDGlobal.TagDataXForm
{
    public struct ParsedData
    {
        public int startAddress;
        public byte[] dataBytes;

        #region Constructors
        public ParsedData(int startAddress, byte[] dataBytes)
        {
            this.startAddress = startAddress;
            this.dataBytes = dataBytes;

        }
        #endregion

    }
    /// <summary>
    /// TransForm TagData.
    /// </summary>
    public 
        class TagDataXFormImpl : ITagDataXForm
    {
        private string _encodingForXml;
        private MetaDataEditor.MetaData _metaDataObj;
        private System.Text.Encoding _encoding;
        private string _xsdXFormData = AppDomain.CurrentDomain.BaseDirectory.TrimEnd(new char[] { '\\' }) + @"\XSD\TagConfig\RFIDTagDataXFormSpec.xsd";

        //Statics
        //private static string TAGDATATEMPLATE = "/RFIDXFormData/TagDataTemplate";
        //private const int MAX_BYTE_ARRAY = 48 ; //Tag bytes size.
        private int MAX_BYTE_ARRAY = 48; //Tag bytes size.
        private static string DATAELEMENT = "/RFIDXFormData/TagDataTemplate/DataElements/DataElement";
        private Logger m_Log = KTone.RFIDGlobal.KTLogger.KTLogManager.GetLogger();

        private ByteEndian m_endianNess;
        #region [ Constructors ]

        /// <summary>
        /// Instantiate XForm object. 
        /// </summary>
        /// <param name="templateDir">Directory of templates</param>
        /// <param name="xsdTagTemplate">Name of xsd file for template with path</param>
        public TagDataXFormImpl(Dictionary<string, TemplateInfo> templates, string xsdTagTemplate)
        {
            try
            {

                m_Log.Trace("Entering");
                //param chk 
                if (templates == null)
                    throw new XFormException("Failed : Parameter templates is null.");

                if (templates.Count == 0)
                    throw new XFormException("Failed : Parameter templates is empty.");

                if (xsdTagTemplate == null || xsdTagTemplate.Equals(string.Empty) == true)
                    throw new XFormException("[TagDataXFormImpl::TagDataXFormImpl] failed : Parameter 'xsdTagTemplate' is null/empty.");

                if (System.IO.File.Exists(xsdTagTemplate) == false)
                    throw new XFormException("[TagDataXFormImpl::TagDataXFormImpl] failed : XsdFile not exist. Parameter 'xsdTagTemplate' = {0}.", xsdTagTemplate);

                _encodingForXml = "utf-8";
                _metaDataObj = MetaData.GetInstance(templates, xsdTagTemplate);
            }
            catch (XFormException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new XFormException("[TagDataXFormImpl::TagDataXFormImpl] failed : Exception = {0}", ex.Message);
            }
            finally
            {
                m_Log.Trace("Finally Leaving");
            }
        }


        #endregion [ Constructors ]

        #region [ Properties ]

        /// <summary>
        /// Get count of templates cached in the Module 
        /// </summary>
        public int Count
        {
            get
            {
                m_Log.Trace("[TagDataXFormImpl::Count]");
                return _metaDataObj.GetAllTemplateIDs().Length;
            }
        }


        /// <summary>
        /// Get template path
        /// </summary>
        public string BaseDirectory
        {
            get
            { return AppDomain.CurrentDomain.BaseDirectory; }
        }


        public string EncodingForXml
        {
            get
            {
                m_Log.Trace("[TagDataXFormImpl::EncodingForXml]");
                return this._encodingForXml;
            }
            set
            {
                this._encodingForXml = value;
            }
        }


        /// <summary>
        /// Xsd file name for templates with path.
        /// </summary>
        public string XsdXFormData
        {
            get
            {
                m_Log.Trace("[TagDataXFormImpl::XsdXFormData] : get");
                return this._xsdXFormData;
            }
            set
            {
                m_Log.Trace("[TagDataXFormImpl::XsdXFormData] : set");
                if (System.IO.File.Exists(value) == true)
                    this._xsdXFormData = value;
                else
                    throw new XFormException("[TagDataXFormImpl::XsdXFormData (set)] failed : XSD for XFormData not found. Xsd file Name with full path expected. Given value is: - '{0}'", value);

            }
        }

        public MetaData _MetaData
        {
            get
            {
                return _metaDataObj;
            }
        }




        #endregion [ Properties ]

        #region [ Utils ]

        /// <summary>
        /// Set encoding scheme.
        /// </summary>
        /// <param name="xDocDataElements"></param>
        private void SetEncodingObj(ref XmlDocument xDocDataElements)
        {
            m_Log.Trace("[TagDataXFormImpl::SetEncodingObj]");
            string ndVal = xDocDataElements.FirstChild.Value;
            //string ndValEncoding = ndVal.Substring(ndVal.IndexOf("encoding"), .......
            string strEncoding = "utf-8";
            this._encoding = System.Text.Encoding.GetEncoding(strEncoding);
        }

        /// <summary>
        /// For Byte DataType
        /// </summary>
        /// <param name="inArr"></param>
        /// <param name="startIdx"></param>
        /// <param name="length"></param>
        /// <returns>return a single byte</returns>
        private byte ReadByte(ref byte[] inArr, int startIdx, int length)
        {
            m_Log.Trace(string.Format("[TagDataXFormImpl::ReadByte] : inArr.Length {0}, startIdx {1}, length {2}", inArr.Length.ToString(), startIdx.ToString(), length.ToString()));
            byte xFormVal;

            if (startIdx + length > inArr.Length)
            {
                m_Log.Trace("[TagDataXFormImpl::ReadByte] : Input array is insufficient in length for this XForm.");
                return 0;
            }

            if (length != 1)
            {
                m_Log.Trace("[TagDataXFormImpl::ReadByte] : length should be 1 for this XForm.");
                return 0;
            }

            xFormVal = inArr[startIdx];
            m_Log.Trace(string.Format("[TagDataXFormImpl::ReadByte] : xFormVal {0}", xFormVal.ToString()));
            return xFormVal;
        }

        /// <summary>
        /// convert bytes to short.
        /// </summary>
        /// <param name="inArr"></param>
        /// <param name="startIdx"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private UInt16 ReadShort(ref byte[] inArr, int startIdx, int length)
        {
            m_Log.Trace(string.Format("[TagDataXFormImpl::ReadShort] : inArr.Length {0}, startIdx {1}, length {2}", inArr.Length.ToString(), startIdx.ToString(), length.ToString()));

            UInt16 xFormVal;
            int lenForShort = 2;
            byte[] tmpBytArr = new byte[lenForShort];

            if (startIdx + length > inArr.Length)
            {
                m_Log.Trace("[TagDataXFormImpl::ReadShort] : Input array is insufficient in length for this XForm.");
                return 0;
            }

            int idx = 0;
            //			if(length < lenForShort)
            //			{
            //				idx = lenForShort - length;
            //				lenForShort = length;
            //			}			

            Array.Copy(inArr, startIdx, tmpBytArr, idx, lenForShort);

            if ((m_endianNess == ByteEndian.LittleEndian && !BitConverter.IsLittleEndian) ||
                (m_endianNess == ByteEndian.BigEndian && BitConverter.IsLittleEndian))
            {
                Array.Reverse(tmpBytArr);
            }

            xFormVal = BitConverter.ToUInt16(tmpBytArr, 0);

            m_Log.Trace(string.Format("[TagDataXFormImpl::ReadShort] : xFormVal {0}", xFormVal.ToString()));
            return xFormVal;
        }


        /// <summary>
        /// convert bytes to integer.
        /// </summary>
        /// <param name="inArr"></param>
        /// <param name="startIdx"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private UInt32 ReadInteger32(ref byte[] inArr, int startIdx, int length)
        {
            m_Log.Trace(string.Format("[TagDataXFormImpl::ReadInteger32] : inArr.Length {0}, startIdx {1}, length {2}", inArr.Length.ToString(), startIdx.ToString(), length.ToString()));
            UInt32 xFormVal;
            int lenForInt32 = 4;
            byte[] tmpBytArr = new byte[lenForInt32];

            if (startIdx + length > inArr.Length)
            {
                //If actual no.of bytes is less than the field length,
                //return without converting the value.As converting partial byte 
                //array into an int value will be a wrong conversion.
                m_Log.Trace("[TagDataXFormImpl::ReadInteger32] : Input array is insufficient in length for this XForm.");
                return 0;
            }

            int index = 0;
            //			//If the field length in no.of bytes is less than 4,try to extract only those many bytes.
            //			if(length < lenForInt32)
            //			{
            //				index = lenForInt32 - length;
            //				lenForInt32 = length;
            //			}

            Array.Copy(inArr, startIdx, tmpBytArr, index, lenForInt32);

            //Big endian input
            if ((m_endianNess == ByteEndian.LittleEndian && !BitConverter.IsLittleEndian) ||
                (m_endianNess == ByteEndian.BigEndian && BitConverter.IsLittleEndian))
            {
                Array.Reverse(tmpBytArr);
            }
            //			if(BitConverter.IsLittleEndian)
            //				Array.Reverse(tmpBytArr);

            xFormVal = BitConverter.ToUInt32(tmpBytArr, 0);

            m_Log.Trace(string.Format("[TagDataXFormImpl::ReadInteger32] : xFormVal {0}", xFormVal.ToString()));
            return xFormVal;
        }


        /// <summary>
        /// convert bytes to long.
        /// </summary>
        /// <param name="inArr"></param>
        /// <param name="startIdx"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private UInt64 ReadInteger64(ref byte[] inArr, int startIdx, int length)
        {
            m_Log.Trace(string.Format("[TagDataXFormImpl::ReadInteger64] : inArr.Length {0}, startIdx {1}, length {2}", inArr.Length.ToString(), startIdx.ToString(), length.ToString()));
            UInt64 xFormVal = 0;
            int lenForlong = 8;
            byte[] tmpBytArr = new byte[lenForlong];

            if (startIdx + length > inArr.Length)
            {
                //If actual no.of bytes is less than the field length,
                //return without converting the value.As converting partial byte 
                //array into an int value will be a wrong conversion.
                m_Log.Trace("[TagDataXFormImpl::ReadInteger64] : Input array is insufficient in length for this XForm.");
                return 0;
            }

            int idx = 0;
            //			if(length < lenForlong)
            //			{
            //				idx = lenForlong - length;
            //				lenForlong = length;
            //			}

            Array.Copy(inArr, startIdx, tmpBytArr, idx, lenForlong);

            //Big endian input
            if ((m_endianNess == ByteEndian.LittleEndian && !BitConverter.IsLittleEndian) ||
                (m_endianNess == ByteEndian.BigEndian && BitConverter.IsLittleEndian))
            {
                Array.Reverse(tmpBytArr);
            }
            //			if(BitConverter.IsLittleEndian)
            //				Array.Reverse(tmpBytArr);

            xFormVal = BitConverter.ToUInt64(tmpBytArr, 0);

            m_Log.Trace(string.Format("[TagDataXFormImpl::ReadInteger64] : xFormVal {0}", xFormVal.ToString()));
            return xFormVal;

        }//Readlong


        /// <summary>
        /// convert bytes to string.
        /// </summary>
        /// <param name="inArr"></param>
        /// <param name="startIdx"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private string ReadStringVal(ref byte[] inArr, int startIdx, int length)
        {
            m_Log.Trace(string.Format("[TagDataXFormImpl::ReadStringVal] : inArr.Length {0}, startIdx {1}, length {2}", inArr.Length.ToString(), startIdx.ToString(), length.ToString()));
            string xFormVal;
            int lenForStr = length;
            byte[] tmpBytArr = new byte[lenForStr];

            if (startIdx + length > inArr.Length)
            {
                m_Log.Trace("[TagDataXFormImpl::ReadStringVal] : Input array is insufficient in length. It will try to XFormed available string bytes.");
                lenForStr = inArr.Length - startIdx;
                if (lenForStr < 0)
                {
                    m_Log.Trace("[TagDataXFormImpl::ReadStringVal] : XFormed available string bytes not found in given array. start index must be within length of given array.");
                    throw new XFormException("[TagDataXFormImpl::ReadStringVal] failed : XFormed available string bytes not found in given array. start index must be within length of given array.");
                }
            }

            Array.Copy(inArr, startIdx, tmpBytArr, 0, lenForStr);

            xFormVal = System.Text.Encoding.ASCII.GetString(tmpBytArr);
            int idxTrminatr = xFormVal.IndexOf("\0");
            if (idxTrminatr > -1)
                xFormVal = xFormVal.Substring(0, idxTrminatr);

            m_Log.Trace(string.Format("[TagDataXFormImpl::ReadStringVal] : xFormVal {0}", xFormVal.ToString()));
            return xFormVal;
        }


        /// <summary>
        /// convert bytes to bool
        /// </summary>
        /// <param name="inArr"></param>
        /// <param name="startIdx"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private bool ReadboolVal(ref byte[] inArr, int startIdx, int length)
        {
            m_Log.Trace(string.Format("[TagDataXFormImpl::ReadboolVal] : inArr.Length {0}, startIdx {1}, length {2}", inArr.Length.ToString(), startIdx.ToString(), length.ToString()));
            bool xFormVal;

            if (startIdx + length > inArr.Length)
            {
                m_Log.Trace("[TagDataXFormImpl::ReadboolVal] : Input array is insufficient in length for this XForm.");
                return false;
            }

            if (length != 1)
            {
                m_Log.Trace("[TagDataXFormImpl::ReadboolVal] : length should be 1 for this XForm.");
                return false;
            }

            xFormVal = System.BitConverter.ToBoolean(inArr, startIdx);
            m_Log.Trace(string.Format("[TagDataXFormImpl::ReadboolVal] : xFormVal {0}", xFormVal.ToString()));
            return xFormVal;
        }


        /// <summary>
        /// Parse data (bytes to xml) according to its datatype defined in template and set the xmlElement
        /// </summary>
        /// <param name="inArr"></param>
        /// <param name="elmForRead"></param>
        /// <param name="elmTmpltElement"></param>
        private void ParseData(ref byte[] inArr, ref TemplateElement elmForRead, ref XmlElement elmTmpltElement)
        {
            m_Log.Trace(string.Format("[TagDataXFormImpl::ParseData] : inArr.Length={0}, FieldName={1}, StartByteIndex={2}", inArr.Length.ToString(), elmForRead.Name, elmForRead.StartByteIndex.ToString()));
            try
            {
                elmTmpltElement.SetAttribute("FieldName", elmForRead.Name);
                elmTmpltElement.SetAttribute("FieldDataType", elmForRead.DataType);

                switch (elmForRead.DataType)
                {
                    case "byte":
                        {
                            elmTmpltElement.SetAttribute("FieldValue", ReadByte(ref inArr, elmForRead.StartByteIndex, elmForRead.Length).ToString());
                            break;
                        }

                    case "short":
                        {
                            elmTmpltElement.SetAttribute("FieldValue", ReadShort(ref inArr, elmForRead.StartByteIndex, elmForRead.Length).ToString());
                            break;
                        }

                    case "int":
                        {
                            elmTmpltElement.SetAttribute("FieldValue", ReadInteger32(ref inArr, elmForRead.StartByteIndex, elmForRead.Length).ToString());
                            break;
                        }

                    case "long":
                        {
                            elmTmpltElement.SetAttribute("FieldValue", ReadInteger64(ref inArr, elmForRead.StartByteIndex, elmForRead.Length).ToString());
                            break;
                        }

                    case "string":
                        {
                            elmTmpltElement.SetAttribute("FieldValue", ReadStringVal(ref inArr, elmForRead.StartByteIndex, elmForRead.Length));
                            break;
                        }
                    case "bool":
                        {
                            elmTmpltElement.SetAttribute("FieldValue", ReadboolVal(ref inArr, elmForRead.StartByteIndex, elmForRead.Length).ToString());
                            break;
                        }

                }//switch(elmForRead.DataType)
            }
            catch (XFormException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new XFormException("[TagDataXFormImpl::ParseData] failed :" +
                    "Template element vs. input Byte array mismatched for field='{0}' " +
                    "and DataType='{1}'. Exception={2}",
                    elmForRead.Name, elmForRead.DataType, ex.Message);

            }

        }//private void ParseData

        private object ParseData(ref byte[] inArr, ref TemplateElement elmForRead)
        {
            m_Log.Trace(string.Format("[TagDataXFormImpl::ParseData] : inArr.Length={0}, FieldName={1}, StartByteIndex={2}", inArr.Length.ToString(), elmForRead.Name, elmForRead.StartByteIndex.ToString()));
            try
            {
                switch (elmForRead.DataType)
                {
                    case "byte":
                        {
                            return ReadByte(ref inArr, elmForRead.StartByteIndex, elmForRead.Length);
                        }

                    case "short":
                        {
                            return ReadShort(ref inArr, elmForRead.StartByteIndex, elmForRead.Length);
                        }

                    case "int":
                        {
                            return ReadInteger32(ref inArr, elmForRead.StartByteIndex, elmForRead.Length);
                        }

                    case "long":
                        {
                            return ReadInteger64(ref inArr, elmForRead.StartByteIndex, elmForRead.Length);
                        }

                    case "string":
                        {
                            return ReadStringVal(ref inArr, elmForRead.StartByteIndex, elmForRead.Length);
                        }
                    case "bool":
                        {
                            return ReadboolVal(ref inArr, elmForRead.StartByteIndex, elmForRead.Length);
                        }
                    default:
                        {
                            return string.Empty;
                        }

                }//switch(elmForRead.DataType)
            }
            catch (XFormException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new XFormException("[TagDataXFormImpl::ParseData] failed :" +
                    "Template element vs. input Byte array mismatched for field='{0}' " +
                    "and DataType='{1}'. Exception={2}",
                    elmForRead.Name, elmForRead.DataType, ex.Message);

            }

        }//private void ParseData


        /// <summary>
        /// Parse data from given index (bytes to xml) according to its datatype defined in template and set the xmlElement
        /// </summary>
        /// <param name="inArr"></param>
        /// <param name="elmForRead"></param>
        /// <param name="elmTmpltElement"></param>
        private void ParseDataFrmIndex(ref byte[] inArr, int idxInArr, ref TemplateElement elmForRead, ref XmlElement elmTmpltElement)
        {
            m_Log.Trace(string.Format("[TagDataXFormImpl::ParseDataFrmIndex] : inArr.Length={0}, FieldName={1}, StartByteIndex={2}", inArr.Length.ToString(), elmForRead.Name, elmForRead.StartByteIndex.ToString()));
            try
            {
                elmTmpltElement.SetAttribute("FieldName", elmForRead.Name);
                elmTmpltElement.SetAttribute("FieldDataType", elmForRead.DataType);

                switch (elmForRead.DataType)
                {
                    case "byte":
                        {
                            elmTmpltElement.SetAttribute("FieldValue", ReadByte(ref inArr, idxInArr, elmForRead.Length).ToString());
                            break;
                        }

                    case "short":
                        {
                            elmTmpltElement.SetAttribute("FieldValue", ReadShort(ref inArr, idxInArr, elmForRead.Length).ToString());
                            break;
                        }

                    case "int":
                        {
                            elmTmpltElement.SetAttribute("FieldValue", ReadInteger32(ref inArr, idxInArr, elmForRead.Length).ToString());
                            break;
                        }

                    case "long":
                        {
                            elmTmpltElement.SetAttribute("FieldValue", ReadInteger64(ref inArr, idxInArr, elmForRead.Length).ToString());
                            break;
                        }

                    case "string":
                        {
                            elmTmpltElement.SetAttribute("FieldValue", ReadStringVal(ref inArr, idxInArr, elmForRead.Length));
                            break;
                        }
                    case "bool":
                        {
                            elmTmpltElement.SetAttribute("FieldValue", ReadboolVal(ref inArr, idxInArr, elmForRead.Length).ToString());
                            break;
                        }

                }//switch(elmForRead.DataType)
            }
            catch (XFormException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new XFormException("[TagDataXFormImpl::ParseData] failed : Template element vs. input Byte array mismatched for field='{0}' and DataType='{1}'. Exception={2}", elmForRead.Name, elmForRead.DataType, ex.Message);
            }

        }//private void ParseData



        private void GetShort(TemplateElement elmForRead, string fieldValue, ref ArrayList arlst)
        {
            m_Log.Trace("[TagDataXFormImpl::GetShort]");
            byte[] bShort;
            if (fieldValue == null || fieldValue.Equals(String.Empty) == true)
            {
                m_Log.Debug("[TagDataXFormImpl::ParseData] failed : " +
                    elmForRead.Name + "is empty");
                throw new ApplicationException("Failed to parse " + elmForRead.Name + "is empty");
                //bShort = new byte[elmForRead.Length];
            }
            else
            {
                UInt16 srtVal = System.Convert.ToUInt16(fieldValue);
                bShort = System.BitConverter.GetBytes(srtVal);
            }

            if ((m_endianNess == ByteEndian.LittleEndian && !BitConverter.IsLittleEndian) ||
                (m_endianNess == ByteEndian.BigEndian && BitConverter.IsLittleEndian))
            {
                Array.Reverse(bShort);
            }
            //			if(BitConverter.IsLittleEndian)
            //				Array.Reverse(bShort);

            for (int idx = 0; idx < bShort.Length; idx++)
                arlst.Add(bShort[idx]);
        }

        private void GetInteger32(TemplateElement elmForRead, string fieldValue, ref ArrayList arlst)
        {
            m_Log.Trace("[TagDataXFormImpl::GetInteger32]");
            byte[] bInt32;
            if (fieldValue == null || fieldValue.Equals(String.Empty) == true)
            {
                m_Log.Debug("[TagDataXFormImpl::ParseData] failed : " + elmForRead.Name + "is empty");
                //bInt32 = new byte[elmForRead.Length];
                throw new ApplicationException("Failed to parse " + elmForRead.Name + "is empty");
            }
            else
            {
                UInt32 intVal = System.Convert.ToUInt32(fieldValue);
                bInt32 = System.BitConverter.GetBytes(intVal);
                if (bInt32.Length == 0)
                {
                    m_Log.Debug("[TagDataXFormImpl::ParseData] failed : for " + elmForRead.Name);
                    throw new ApplicationException("Failed to parse " + elmForRead.Name);
                }
            }

            if ((m_endianNess == ByteEndian.LittleEndian && !BitConverter.IsLittleEndian) ||
                (m_endianNess == ByteEndian.BigEndian && BitConverter.IsLittleEndian))
            {
                Array.Reverse(bInt32);
            }

            //			if(BitConverter.IsLittleEndian)
            //				Array.Reverse(bInt32);

            for (int idx = 0; idx < bInt32.Length; idx++)
                arlst.Add(bInt32[idx]);
        }

        private void GetInteger64(TemplateElement elmForRead, string fieldValue, ref ArrayList arlst)
        {
            m_Log.Trace("[TagDataXFormImpl::GetInteger64]");
            byte[] bInt64;
            if (fieldValue == null || fieldValue.Equals(String.Empty) == true)
            {
                m_Log.Debug("[TagDataXFormImpl::ParseData] failed : " + elmForRead.Name + "is empty");
                throw new ApplicationException("Failed to parse " + elmForRead.Name + "is empty");
                //bInt64 = new byte[elmForRead.Length];
            }
            else
            {
                UInt64 intVal = System.Convert.ToUInt64(fieldValue);
                bInt64 = System.BitConverter.GetBytes(intVal);
                if (bInt64.Length == 0)
                {
                    m_Log.Debug("[TagDataXFormImpl::ParseData] failed : for " + elmForRead.Name);
                    throw new ApplicationException("Failed to parse " + elmForRead.Name);
                }
            }
            if ((m_endianNess == ByteEndian.LittleEndian && !BitConverter.IsLittleEndian) ||
                (m_endianNess == ByteEndian.BigEndian && BitConverter.IsLittleEndian))
            {
                Array.Reverse(bInt64);
            }

            //			if(BitConverter.IsLittleEndian)
            //				Array.Reverse(bInt64);

            for (int idx = 0; idx < bInt64.Length; idx++)
                arlst.Add(bInt64[idx]);
        }

        private void GetString(TemplateElement elmForRead, string fieldValue, ref ArrayList arlst)
        {
            m_Log.Trace("[TagDataXFormImpl::GetString]");
            byte[] bStr = null;

            //if(fieldValue == null || fieldValue.Equals(String.Empty) == true)
            if (fieldValue == null)
            {
                m_Log.Debug("[TagDataXFormImpl::ParseData] failed : " + elmForRead.Name + "is empty");
                throw new ApplicationException("Failed to parse " + elmForRead.Name + "is empty");
                //bStr = new byte[elmForRead.Length];
            }
            else
            {
                if (fieldValue.Length > elmForRead.Length)
                {
                    m_Log.Debug("[TagDataXFormImpl::ParseData] failed : for " + elmForRead.Name);
                    throw new ApplicationException("Failed to parse " + elmForRead.Name);
                }

                //By default byte array is initialized to 0
                bStr = new byte[elmForRead.Length];
                //for empty string ,length of byteArr will be 0
                byte[] byteArr = System.Text.Encoding.ASCII.GetBytes(fieldValue);
                for (int idx = 0; idx < byteArr.Length; idx++)
                    bStr[idx] = byteArr[idx];
            }

            for (int idx = 0; idx < bStr.Length; idx++)
                arlst.Add(bStr[idx]);
        }

        private void GetBool(TemplateElement elmForRead, string fieldValue, ref ArrayList arlst)
        {
            m_Log.Trace("[TagDataXFormImpl::GetBool]");
            byte[] bBool;
            if (fieldValue == null || fieldValue.Equals(String.Empty) == true)
            {
                //bBool = new byte[elmForRead.Length];
                m_Log.Trace("[TagDataXFormImpl::ParseData] failed : " + elmForRead.Name + "is empty");
                throw new ApplicationException("Failed to parse " + elmForRead.Name + "is empty");
            }
            else
            {
                bool boolVal = System.Convert.ToBoolean(fieldValue);
                bBool = System.BitConverter.GetBytes(boolVal);
                if (bBool.Length == 0)
                {
                    m_Log.Debug("[TagDataXFormImpl::ParseData] failed : for " + elmForRead.Name);
                    throw new ApplicationException("Failed to parse " + elmForRead.Name);
                }

            }
            for (int idx = 0; idx < bBool.Length; idx++)
                arlst.Add(bBool[idx]);
        }

        private void GetByte(TemplateElement elmForRead, string fieldValue, ref ArrayList arlst)
        {
            m_Log.Trace("[TagDataXFormImpl::GetByte]");
            byte byteval;
            if (fieldValue == null || fieldValue.Equals(String.Empty) == true)
            {
                m_Log.Debug("[TagDataXFormImpl::ParseData] failed : " + elmForRead.Name + "is empty");
                throw new ApplicationException("Failed to parse " + elmForRead.Name + "is empty");
                //byteval = 0;
            }
            else
            {
                byteval = System.Convert.ToByte(fieldValue);
            }

            arlst.Add(byteval);
        }


        /// <summary>
        /// start converting FieldValues to ByteArray using DataType and TemplateElement
        /// </summary>
        /// <param name="elmForRead"></param>
        /// <param name="FieldDataType"></param>
        /// <param name="FieldValue"></param>
        /// <param name="arlst"></param>
        private void ParseData(TemplateElement elmForRead, string sFieldName,
            string sFieldDataType, string fieldValue, ref ArrayList arlst)
        {
            m_Log.Trace(string.Format("[TagDataXFormImpl::ParseData] : FieldName={0}, FieldDataType={1}, fieldValue={2}", sFieldName, sFieldDataType, fieldValue));
            string minVal = string.Empty, maxVal = string.Empty;

            try
            {
                switch (sFieldDataType)
                {
                    case "short":
                        {
                            minVal = System.UInt16.MinValue.ToString();
                            maxVal = System.UInt16.MaxValue.ToString();
                            GetShort(elmForRead, fieldValue, ref arlst);

                            break;
                        }
                    case "int":
                        {
                            minVal = System.UInt32.MinValue.ToString();
                            maxVal = System.UInt32.MaxValue.ToString();
                            GetInteger32(elmForRead, fieldValue, ref arlst);

                            break;
                        }
                    case "long":
                        {

                            minVal = System.UInt64.MinValue.ToString();
                            maxVal = System.UInt64.MaxValue.ToString();
                            GetInteger64(elmForRead, fieldValue, ref arlst);

                            break;
                        }
                    case "string":
                        {
                            minVal = "0";
                            maxVal = elmForRead.Length.ToString();
                            GetString(elmForRead, fieldValue, ref arlst);

                            break;
                        }
                    case "bool":
                        {

                            minVal = System.Boolean.TrueString;
                            maxVal = System.Boolean.FalseString;
                            GetBool(elmForRead, fieldValue, ref arlst);

                            break;
                        }
                    case "byte":
                        {

                            minVal = System.Byte.MinValue.ToString();
                            maxVal = System.Byte.MaxValue.ToString();
                            GetByte(elmForRead, fieldValue, ref arlst);


                            break;
                        }
                }
            }
            catch (XFormException ex)
            {
                throw ex;
            }
            catch (Exception)
            {
                //				throw new XFormException("[TagDataXFormImpl::ParseData] failed :"
                //					+"Input FieldValue not proper to transform data. field='{0}' "
                //					+"and DataType='{1}' Value='{2}'.  Exception={3}", 
                //					sFieldName, sFieldDataType, fieldValue, ex.Message);
                string msg = "Field : " + elmForRead.Name + ",Data type : " + elmForRead.DataType +
                    ",Range : " + minVal + "-" + maxVal;
                throw new ApplicationException(msg);

            }
        }


        private void GetTargetElement(ref TemplateElement[] tmpltElms, int idxInTag, out int tmpltElmsIdx, out int posInArr)
        {
            m_Log.Trace("[TagDataXForm::GetTargetElement] :");
            int minDiff1 = MAX_BYTE_ARRAY, minIdx = 0;


            for (int idx = 0; idx < tmpltElms.Length; idx++)
            {
                if (tmpltElms[idx].StartByteIndex >= idxInTag)
                {
                    minIdx = tmpltElms[idx].StartByteIndex - idxInTag < minDiff1 ? idx : minIdx;
                    minDiff1 = tmpltElms[idx].StartByteIndex - idxInTag < minDiff1 ? tmpltElms[idx].StartByteIndex - idxInTag : minDiff1;
                }

            }

            if (minDiff1 < MAX_BYTE_ARRAY)
            {
                if (idxInTag == 0 && minDiff1 == 0)
                {
                    posInArr = 0;
                    tmpltElmsIdx = minIdx;
                    return;
                }

                tmpltElmsIdx = minIdx;
                if (minIdx > 0)
                    posInArr = (tmpltElms[minIdx - 1].StartByteIndex + tmpltElms[minIdx - 1].Length) - idxInTag;
                else
                    posInArr = (tmpltElms[minIdx].StartByteIndex + tmpltElms[minIdx].Length) - idxInTag;
            }
            else
                throw new XFormException("[TagDataXForm::GetTargetElement] failed : Given index is mismatched with the TemplateElements.");

            m_Log.Trace(string.Format("[TagDataXForm::GetTargetElement] : Targete element index={0}, StartByteIndex={1}, FiledName={2}",
                minIdx.ToString(), tmpltElms[minIdx].StartByteIndex.ToString(), tmpltElms[minIdx].Name.ToString()));

            return;
        }//GetTargetElement()

        #endregion [ Utils ]

        #region [ ---Methods--- ]

        #region --Refresh fxns--
        /// <summary>
        /// Refreshes all the TAG Templates definition from Meta-File-definitions stored in the files.
        /// </summary>		
        public void RefreshAll()
        {
            m_Log.Trace("[TagDataXFormImpl::RefreshAll]");
            _metaDataObj.Refresh();
        }


        /// <summary>
        /// Refresh only one TAG Template identified by templateID. Throws exception for Invalid , non-existing 
        /// or Malformed XML This method performs Add, Delete or Update based on if 
        /// Meta-definition file is added, deleted or modified. 
        /// </summary>
        /// <param name="templateID"></param>
        public void Refresh(string templateID)
        {
            try
            {
                m_Log.Trace(string.Format("[TagDataXFormImpl::Refresh] : templateID = {0}", templateID));
                if (templateID == null || templateID.Equals(String.Empty) == true)
                    throw new XFormException("[TagDataXFormImpl::Refresh] failed : Parameter templateID should not be NULL/Empty for Refresh.");

                _metaDataObj.Refresh(templateID);

            }
            catch (XFormException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new XFormException("[TagDataXFormImpl::Refresh] failed : Exception={0}", ex.Message);
            }

        }


        /// <summary>
        /// Refresh only one TAG Template identified by templateID. Throws exception for Invalid , non-existing 
        /// or Malformed XML This method performs Add, Delete or Update based on if 
        /// Meta-definition file is added, deleted or modified. Optionally, returns the operation as ADDED/MODIFIED/DELETED 
        /// </summary>
        /// <param name="templateID"></param>
        /// <param name="status"></param>
        public void Refresh(string templateID, out OperationStatus operation)
        {
            try
            {
                m_Log.Trace(string.Format("[TagDataXFormImpl::Refresh] : templateID = {0}, with out param OperationStatus ", templateID));
                if (templateID == null || templateID.Equals(String.Empty) == true)
                    throw new XFormException("[TagDataXFormImpl::Refresh] failed : Parameter templateID should not be NULL/Empty for Refresh.");

                TemplateOperation op;
                _metaDataObj.Refresh(templateID, out op);
                operation = (OperationStatus)Enum.Parse(typeof(OperationStatus), op.ToString(), true);

            }
            catch (XFormException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new XFormException("[TagDataXFormImpl::Refresh] failed : Exception={0}", ex.Message);
            }
        }


        #endregion Refresh fxns

        #region --Transforms--

        /// <summary>
        /// Main method that takes InArr bytes of array and transforms it into XML string 
        /// as per the Tag Template definition 
        /// </summary>
        /// <param name="templateID"></param>
        /// <param name="TagType"></param>
        /// <param name="inArray"></param>
        /// return transformed xml string
        public string Transform(string templateID, TagType tagForXForm, byte[] inArr,
            int maxTagDataSize)
        {
            m_Log.Trace(string.Format("[TagDataXFormImpl::Transform] : templateID={0}, tagForXForm={1}, inArr.Length={2}", templateID, tagForXForm.ToString(), inArr.Length.ToString()));
            MAX_BYTE_ARRAY = maxTagDataSize;
            try
            {
                //Parameter validation
                if (templateID == null || templateID.Equals(String.Empty) == true)
                    throw new XFormException("[TagDataXFormImpl::Transform] failed : Parameter templateID found null/empty.");
                //2nd param is of TagType cant null
                if (inArr == null)
                    throw new XFormException("[TagDataXFormImpl::Transform] failed : Byte Array to transform is null");


                XmlDocument xDocXform = new XmlDocument();
                xDocXform.AppendChild(xDocXform.CreateXmlDeclaration("1.0", this._encodingForXml, "no"));
                XmlElement RootNode = xDocXform.CreateElement("RFIDXFormData");
                xDocXform.AppendChild(RootNode);

                _metaDataObj.Open(templateID);

                m_endianNess = _metaDataObj.GetByteEndian();

                //TODO - Omkar tag type check is removed but TemplateElements must be count > 0
                //_metaDataObj.GotoTagType(tagForXForm);
                _metaDataObj.GotoTopTemplateElement();

                //Making TagDataTmplt Node
                XmlElement elmTagDataTmplt = xDocXform.CreateElement("TagDataTemplate");
                elmTagDataTmplt.SetAttribute("TagType", tagForXForm.ToString());
                RootNode.AppendChild(elmTagDataTmplt);

                //Making TemplateElements Node
                XmlElement elmTmpltElements = xDocXform.CreateElement("DataElements");
                elmTagDataTmplt.AppendChild(elmTmpltElements);

                long elmCount = _metaDataObj.GetCountOfCurrentTagElements();

                _metaDataObj.GotoTopTemplateElement();
                TemplateElement elmForRead;
                for (long idx = 0; idx < elmCount; idx++)
                {
                    elmForRead = _metaDataObj.GetCurrentTemplateElement();

                    XmlElement elmTmpltElement = xDocXform.CreateElement("DataElement");

                    //if( elmForRead.StartByteIndex < inArr.Length)
                    if (elmForRead.StartByteIndex + elmForRead.Length <= inArr.Length)
                    {
                        ParseData(ref inArr, ref elmForRead, ref elmTmpltElement);
                        elmTmpltElements.AppendChild(elmTmpltElement);
                    }
                    //else
                    //log.Trace("[TagDataXFormImpl::Transform] : 'CurrentTemplateElement' is either invalid or Input array length mismatch.");

                    _metaDataObj.GotoNextTemplateElement();
                }

                m_Log.Trace(string.Format("[TagDataXFormImpl::Transform] : Validate XFormedStr = {0}", xDocXform.OuterXml));
                if (this.Validate(xDocXform.OuterXml) == true)
                    return xDocXform.OuterXml;
                else
                    throw new XFormException(string.Format("[TagDataXFormImpl::Transform] : Validation failed for XFormed XmlStr. xsdfile = {0}", this.XsdXFormData));
            }
            catch (XmlException ex)
            {
                throw new XFormException("[TagDataXFormImpl::Transform] : XmlException = {0}", ex.Message);
            }
            catch (XFormException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new XFormException("[TagDataXFormImpl::Transform] : Exception = {0}", ex.Message);
            }
        }//public string Transform 



        public string Transform(string templateID, TagType tagForXForm, byte[] inArr, int idxInTag,
            int maxTagDataSize)
        {
            m_Log.Trace(string.Format("[TagDataXFormImpl::Transform] : templateID={0}, tagForXForm={1}, inArr.Length={2}, idxOfInArr={3}", templateID, tagForXForm.ToString(), inArr.Length.ToString(), idxInTag.ToString()));
            MAX_BYTE_ARRAY = maxTagDataSize;
            try
            {
                //Parameter validation
                if (templateID == null || templateID.Equals(String.Empty) == true)
                    throw new XFormException("[TagDataXFormImpl::Transform] failed : Parameter templateID found null/empty.");

                if (inArr.Length <= 0)
                    throw new XFormException("[TagDataXFormImpl::Transform] failed : Parameter inArr length is 0 or less then 0.");

                if (idxInTag < 0 || idxInTag >= MAX_BYTE_ARRAY)
                    throw new XFormException("[TagDataXFormImpl::Transform] failed : Parameter idxOfInArr is out of bound for byte array.");

                //Parameter validation complete

                //Start Parsing
                XmlDocument xDocXform = new XmlDocument();
                xDocXform.AppendChild(xDocXform.CreateXmlDeclaration("1.0", this._encodingForXml, "no"));
                XmlElement RootNode = xDocXform.CreateElement("RFIDXFormData");
                xDocXform.AppendChild(RootNode);

                //Open Template and move to given Tag.
                m_Log.Trace(string.Format("[TagDataXFormImpl::Transform] : Opening template - {0}", templateID));
                _metaDataObj.Open(templateID);

                m_endianNess = _metaDataObj.GetByteEndian();

                _metaDataObj.GotoTopTemplateElement();

                //Making TagDataTmplt Node
                XmlElement elmTagDataTmplt = xDocXform.CreateElement("TagDataTemplate");
                elmTagDataTmplt.SetAttribute("TagType", tagForXForm.ToString());
                RootNode.AppendChild(elmTagDataTmplt);

                //Making TemplateElements Node
                XmlElement elmTmpltElements = xDocXform.CreateElement("DataElements");
                elmTagDataTmplt.AppendChild(elmTmpltElements);

                m_Log.Trace("[TagDataXFormImpl::Transform] : Getting template elments");
                TemplateElement[] tmpltElms = _metaDataObj.GetAllTemplateElements();

                int tmpltElmsIdx, posInArr;
                GetTargetElement(ref tmpltElms, idxInTag, out tmpltElmsIdx, out posInArr);

                for (int idx = tmpltElmsIdx; idx < tmpltElms.Length; idx++)
                {
                    if (tmpltElms[idx].StartByteIndex >= idxInTag)
                    {
                        posInArr = tmpltElms[idx].StartByteIndex - idxInTag;
                        if (posInArr + tmpltElms[idx].Length <= inArr.Length)
                        {
                            XmlElement elmTmpltElement = xDocXform.CreateElement("DataElement");
                            ParseDataFrmIndex(ref inArr, posInArr, ref tmpltElms[idx], ref elmTmpltElement);
                            elmTmpltElements.AppendChild(elmTmpltElement);
                        }
                    }
                    //posInArr += tmpltElms[idx].Length;
                }

                m_Log.Trace(string.Format("[TagDataXFormImpl::Transform] : Validate XFormedStr = {0}", xDocXform.OuterXml));
                if (this.Validate(xDocXform.OuterXml) == true)
                    return xDocXform.OuterXml;
                else
                    throw new XFormException(string.Format("[TagDataXFormImpl::Transform] : Validation failed for XFormed XmlStr. xsdfile = {0}", this.XsdXFormData));

            }
            catch (XmlException ex)
            {
                throw new XFormException("[TagDataXFormImpl::Transform] failed : Exception={0}", ex.Message);
            }
            catch (XFormException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new XFormException("[TagDataXFormImpl::Transform] failed : Exception={0}", ex.Message);
            }
        }


        /// <summary>
        /// Main method that takes xmlDataTemplate string in xml and transforms it into byte array 
        /// as per the Tag Template definition
        /// </summary>
        /// <param name="templateID"></param>
        /// <param name="TagForXForm"></param>
        /// <param name="xmlDataTemplate"></param>
        /// <returns>byte array</returns>
        public byte[] Transform(string templateID, TagType tagForXForm, string xmlDataElements,
            int maxTagDataSize)
        {
            m_Log.Trace("[TagDataXFormImpl::Transform]");
            //Parameter chk
            if (templateID == null || templateID.Equals(String.Empty) == true)
                throw new XFormException("[TagDataXFormImpl::Transform] failed : Parameter templateID should not be NULL/Empty for Tranform.");
            if (xmlDataElements == null || xmlDataElements.Equals(String.Empty) == true)
                throw new XFormException("[TagDataXFormImpl::Transform] failed : Parameter xmlDataElements should not be NULL/Empty for Tranform.");
            MAX_BYTE_ARRAY = maxTagDataSize;
            try
            {
                //chk Validation of input DataElem Str
                if (Validate(xmlDataElements) == false)
                    throw new XFormException("[TagDataXFormImpl::Transform] failed : Validation failed for Xml DataElement.");

                ArrayList arlst = new ArrayList();

                XmlDocument xDocDataElements = new XmlDocument();
                xDocDataElements.LoadXml(xmlDataElements);

                _metaDataObj.Open(templateID);

                m_endianNess = _metaDataObj.GetByteEndian();

                //TODO -omkar removed taf type check
                //_metaDataObj.GotoTagType(tagForXForm);

                TemplateElement elmForRead;
                _metaDataObj.GotoTopTemplateElement();

                string FieldName = "";
                string FieldDataType = "";
                string FieldValue = "";

                int countDataElem = xDocDataElements.SelectNodes(DATAELEMENT).Count;
                int countTmpltElem = _metaDataObj.GetCountOfCurrentTagElements();

                if (countDataElem > countTmpltElem)
                    throw new XFormException("[TagDataXFormImpl::Transform] failed : No. of DataElement should be equal to TagData's TemplateElement or greater. No. of DataElement = '{0}', No. of TemplateElement = '{1}'", countDataElem, countTmpltElem);

                for (int idx = 0; idx < countDataElem; idx++)
                {
                    elmForRead = _metaDataObj.GetCurrentTemplateElement();
                    //DATAELEMENT = "/RFIDXFormData/TagDataTemplate/DataElements/DataElement";
                    FieldName = xDocDataElements.SelectSingleNode(DATAELEMENT + "[" + (idx + 1).ToString() + "]/@FieldName").Value;
                    FieldDataType = xDocDataElements.SelectSingleNode(DATAELEMENT + "[" + (idx + 1).ToString() + "]/@FieldDataType").Value;
                    FieldValue = xDocDataElements.SelectSingleNode(DATAELEMENT + "[" + (idx + 1).ToString() + "]/@FieldValue").Value;
                    if (FieldDataType == elmForRead.DataType)
                        ParseData(elmForRead, FieldName, FieldDataType, FieldValue, ref arlst);

                    _metaDataObj.GotoNextTemplateElement();
                }

                byte[] XformBytes = new byte[arlst.Count];
                for (int idx = 0; idx < arlst.Count; idx++)
                    XformBytes[idx] = (byte)arlst[idx];

                return XformBytes;
            }
            catch (XmlException ex)
            {
                throw new XFormException("[TagDataXFormImpl::Transform] failed : XMLException ={0}", ex.Message);
            }
            catch (XFormException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new XFormException("[TagDataXFormImpl::Transform] failed : Exception = {0}", ex.Message);
            }
        }


        /// <summary>
        /// Main method that takes tagDataHash and if getParsedDataBytes is true ,
        /// transforms it into array  of ParsedData objects as per the Tag Template definition.
        /// If getParsedDataBytes is false  transforms it into array  of ParsedData objects which have 
        /// no actual tagData inside but data size is same as per the  Tag Template definition.
        /// </summary>
        /// <param name="templateID"></param>
        /// <param name="TagForXForm"></param>
        /// <param name="tagDataHash"></param>
        /// <param name="getParsedDataBytes"></param>
        /// <returns>byte array</returns>
        public ParsedData[] Transform(string templateID, TagType tagForXForm, Dictionary<string, string> tagDataHash,
            int maxTagDataSize, bool getParsedDataBytes)
        {
            m_Log.Trace("[TagDataXFormImpl::Transform]");
            //Parameter chk
            if (templateID == null || templateID.Equals(String.Empty) == true)
                throw new XFormException("[TagDataXFormImpl::Transform] failed : Parameter templateID should not be NULL/Empty for Tranform.");
            if (tagDataHash == null || tagDataHash.Count == 0)
                throw new XFormException("[TagDataXFormImpl::Transform] failed : Parameter tagDataHash should not be NULL/Empty for Tranform.");
            MAX_BYTE_ARRAY = maxTagDataSize;
            try
            {
                _metaDataObj.Open(templateID);

                m_endianNess = _metaDataObj.GetByteEndian();

                //Removed _metaDataObj.GotoTagType(tagForXForm);

                _metaDataObj.GotoTopTemplateElement();

                int countDataElem = tagDataHash.Count;
                int countTmpltElem = _metaDataObj.GetCountOfCurrentTagElements();

                if (countDataElem > countTmpltElem)
                    throw new XFormException("[TagDataXFormImpl::Transform] failed : No."
                        + "of DataElement should be equal to or greater than TagData's TemplateElement. "
                        + "No. of DataElement = '{0}', No. of TemplateElement = '{1}'",
                        countDataElem, countTmpltElem);

                int dataSegStartAdd = -1;
                string fieldName = string.Empty;
                string fieldValue = string.Empty;
                ArrayList dataByteLst = new ArrayList();
                ArrayList parsedDataLst = new ArrayList();
                TemplateElement elmForRead;

                //Create a copy of tagDataHash.
                //Hashtable dataHash = (Hashtable)tagDataHash.Clone();
                Dictionary<string, string> dataHash = new Dictionary<string, string>(tagDataHash);
                string errorStr = string.Empty;

                for (int i = 0; i < countTmpltElem; i++)
                {
                    elmForRead = _metaDataObj.GetCurrentTemplateElement();
                    int elmStartAdd = elmForRead.StartByteIndex;
                    fieldName = elmForRead.Name;
                    if (dataHash.ContainsKey(fieldName))
                    {
                        //Keep contigious bytes in a buffer - dataByteLst.
                        //Compare current element's start address with (dataSegStartAdd + buffer size).
                        //If current element isnot contigious with the previous element,
                        //create a new instance of ParsedData.
                        //ParsedData contains buffer of contigious bytes and its start address.
                        if (dataByteLst.Count != 0)
                        {
                            if (elmStartAdd != (dataSegStartAdd + dataByteLst.Count))
                            {
                                byte[] dataSeg = (byte[])dataByteLst.ToArray(typeof(System.Byte));
                                ParsedData parsedData = new ParsedData(dataSegStartAdd, dataSeg);
                                parsedDataLst.Add(parsedData);
                                dataByteLst.Clear();
                                dataSegStartAdd = -1;
                            }
                        }

                        //parse the data only in case getParsedDataBytes is true
                        if (getParsedDataBytes)
                        {
                            fieldValue = Convert.ToString(dataHash[fieldName]);
                            try
                            {
                                ParseData(elmForRead, fieldName, elmForRead.DataType, fieldValue,
                                    ref dataByteLst);
                            }
                            catch (ApplicationException ex)
                            {
                                errorStr += "\r\n" + ex.Message;
                            }
                        }
                        else//just initialize the array with correct field length
                        {
                            dataByteLst.AddRange(new byte[elmForRead.Length]);

                        }
                        //Set new buffer start address - dataSegStartAdd 
                        if (dataSegStartAdd == -1)
                            dataSegStartAdd = elmStartAdd;

                        dataHash.Remove(fieldName);
                        if (dataHash.Count == 0)
                            break;
                    }
                    _metaDataObj.GotoNextTemplateElement();


                }

                #region dataHash has some elements which are not parsed So writedata will not be complete
                if (dataHash.Count > 0)
                {
                    string keysStr = string.Empty;
                    foreach (KeyValuePair<string, string> entry in dataHash)
                    {
                        keysStr += entry.Key + ", ";

                    }
                    keysStr = keysStr.Trim(new char[] { ',', ' ' });
                    if (dataHash.Count == 1)
                        keysStr += "\r\n Field " + keysStr + "\r\n is not present in Tag template associated with the reader.";
                    else
                        keysStr += "\r\n Fields " + keysStr + "\r\n are not present in Tag template associated with the reader.";

                    throw new XFormException(keysStr);
                }
                #endregion dataHash has some elements which are not parsed So writedata will not be complete

                if (errorStr != string.Empty)
                    throw new ApplicationException("Values for the following fields are out of bounds:" + errorStr);

                //Add the last data segment 
                if (dataByteLst.Count > 0)
                {
                    byte[] dataSeg = (byte[])dataByteLst.ToArray(typeof(System.Byte));
                    ParsedData parsedData = new ParsedData(dataSegStartAdd, dataSeg);
                    parsedDataLst.Add(parsedData);
                }

                ParsedData[] parsedDataArr = (ParsedData[])parsedDataLst.ToArray(typeof(ParsedData));

                return parsedDataArr;

            }
            catch (XmlException ex)
            {
                throw new XFormException("[TagDataXFormImpl::Transform] failed : XMLException ={0}", ex.Message);
            }
            catch (XFormException ex)
            {
                throw ex;
            }
            catch (ApplicationException ex)
            {
                throw ex;

            }
            catch (Exception ex)
            {
                throw new XFormException("[TagDataXFormImpl::Transform] failed : Exception = {0}", ex.Message);
            }


        }

        public Dictionary<string, object> Transform(string templateID, byte[] inArr, int maxTagDataSize)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();

            m_Log.Trace("Entering");
            MAX_BYTE_ARRAY = maxTagDataSize;
            try
            {
                //Parameter validation
                if (templateID == null || templateID.Equals(String.Empty) == true)
                    throw new XFormException("[TagDataXFormImpl::Transform] failed : Parameter templateID found null/empty.");
                //2nd param is of TagType cant null
                if (inArr == null)
                    throw new XFormException("[TagDataXFormImpl::Transform] failed : Byte Array to transform is null");

                _metaDataObj.Open(templateID);

                m_endianNess = _metaDataObj.GetByteEndian();

                //TODO - Omkar tag type check is removed but TemplateElements must be count > 0
                //_metaDataObj.GotoTagType(tagForXForm);
                _metaDataObj.GotoTopTemplateElement();

                long elmCount = _metaDataObj.GetCountOfCurrentTagElements();

                _metaDataObj.GotoTopTemplateElement();
                TemplateElement elmForRead;
                for (long idx = 0; idx < elmCount; idx++)
                {
                    elmForRead = _metaDataObj.GetCurrentTemplateElement();

                    if (elmForRead.StartByteIndex + elmForRead.Length <= inArr.Length)
                    {

                        data[elmForRead.Name] = ParseData(ref inArr, ref elmForRead);
                    }

                    _metaDataObj.GotoNextTemplateElement();
                }

                if (0 == data.Count)
                    throw new XFormException(string.Format("[TagDataXFormImpl::Transform] : Validation failed for XFormed XmlStr. xsdfile = {0}", this.XsdXFormData));
                else
                {
                    if (m_Log.IsTraceEnabled)
                    {
                        foreach (string key in data.Keys)
                        {
                            m_Log.Trace(string.Format("Field Name :{0} Field Value:{1}", key, data[key]));
                        }
                    }
                    return data;
                }
            }
            catch (XmlException ex)
            {
                throw new XFormException("[TagDataXFormImpl::Transform] : XmlException = {0}", ex.Message);
            }
            catch (XFormException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new XFormException("[TagDataXFormImpl::Transform] : Exception = {0}", ex.Message);
            }
        }


        /// <summary>
        /// Main method that takes data value dictionary in xml and transforms it into byte array 
        /// as per the Tag Template definition
        /// </summary>
        public byte[] Transform(string templateID, TagType tagForXForm, Dictionary<string, string> fields,
            int maxTagDataSize)
        {
            m_Log.Trace("[TagDataXFormImpl::Transform]");

            //Parameter chk
            if (templateID == null || templateID.Equals(String.Empty) == true)
                throw new XFormException("[TagDataXFormImpl::Transform] failed : Parameter templateID should not be NULL/Empty for Tranform.");
            MAX_BYTE_ARRAY = maxTagDataSize;
            try
            {

                ArrayList arlst = new ArrayList();


                _metaDataObj.Open(templateID);

                m_endianNess = _metaDataObj.GetByteEndian();

                //TODO -omkar removed taf type check
                //_metaDataObj.GotoTagType(tagForXForm);

                TemplateElement elmForRead;
                _metaDataObj.GotoTopTemplateElement();


                int countDataElem = fields.Count;
                int countTmpltElem = _metaDataObj.GetCountOfCurrentTagElements();

                if (countDataElem > countTmpltElem)
                    throw new XFormException("[TagDataXFormImpl::Transform] failed : No. of DataElement should be equal to TagData's TemplateElement or greater. No. of DataElement = '{0}', No. of TemplateElement = '{1}'", countDataElem, countTmpltElem);


                for (int idx = 0; idx < countDataElem; idx++)
                {
                    elmForRead = _metaDataObj.GetCurrentTemplateElement();

                    if (fields.ContainsKey(elmForRead.Name))
                    {
                        ParseData(elmForRead, elmForRead.Name, elmForRead.DataType,
                            fields[elmForRead.Name], ref arlst);
                    }
                }

                byte[] XformBytes = new byte[arlst.Count];
                for (int idx = 0; idx < arlst.Count; idx++)
                    XformBytes[idx] = (byte)arlst[idx];

                return XformBytes;
            }
            catch (XmlException ex)
            {
                throw new XFormException("[TagDataXFormImpl::Transform] failed : XMLException ={0}", ex.Message);
            }
            catch (XFormException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new XFormException("[TagDataXFormImpl::Transform] failed : Exception = {0}", ex.Message);
            }
        }


        #endregion Transforms

        #region --others-- GetAllTemplateIDs/IsValid/Validate
        /// <summary>
        /// Returns all the template Ids of that are cached in the module. This list might vary from 
        /// templates stored on the disk. 
        /// </summary>
        /// <returns></returns>
        public string[] GetAllTemplateIDs()
        {
            m_Log.Trace("[TagDataXFormImpl::GetAllTemplateIDs]");
            try
            {
                string[] sTmpltIDs = _metaDataObj.GetAllTemplateIDs();
                return sTmpltIDs;
            }
            catch (XFormException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new XFormException("[TagDataXFormImpl::GetAllTemplateIDs]", ex.Message);
            }
        }//GetAllTemplateIDs() 


        /// <summary>
        /// Checks if a given Template is a Valid Template ID 
        /// </summary>
        /// <param name="templateID"></param>
        /// <returns></returns>
        public bool IsValid(string templateID)
        {
            m_Log.Trace("[TagDataXFormImpl::IsValid]");
            try
            {
                return _metaDataObj.IsValid(templateID);
            }
            catch (XFormException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new XFormException("[TagDataXFormImpl::IsValid]", ex.Message);
            }

        }//IsValid( string templateID)


        /// <summary>
        /// Validate a Template identified by templateID again a XSD file. 
        /// Throws Exception for a Invalid template ID 
        /// </summary>
        /// <param name="templateID"></param>
        /// <returns></returns>
        public bool Validate(string xmlStr4Validate)
        {
            try
            {
                m_Log.Trace("[TagDataXFormImpl::Validate]");
                MemoryStream configStream = new MemoryStream(System.Text.Encoding.ASCII.GetBytes(xmlStr4Validate));
                KTone.RFIDGlobal.RFUtils.ValidateXML(this.XsdXFormData, new StreamReader(configStream));
                return true;
            }
            catch (XFormException ex)
            {
                m_Log.Error(string.Format("[TagDataXFormImpl::Validate] failed : Validation failed. Xsd = {0}. {1}", this.XsdXFormData, ex.Message));
                //throw ex;
                return false;
            }
            catch (Exception ex)
            {
                //TempCode
                m_Log.Error(string.Format("[TagDataXFormImpl::Validate] failed : Validation failed. Xsd = {0}. {1}", this.XsdXFormData, ex.Message));
                return false;
            }
        }//Validate(string xmlStr4Validate)


        public TemplateElement[] GetAllTemplateElementsAt(TagType tag)
        {
            try
            {
                return _metaDataObj.GetAllTemplateElementsAt(tag);
            }
            catch (Exception ex)
            {
                throw new XFormException("[TagDataXFormImpl::GetAllTemplateElementsAt] failed : Exception = {0}", ex.Message);
            }
        }

        public TemplateElement[] GetAllTemplateElementsAt(string templateID, TagType tag)
        {
            try
            {
                _metaDataObj.Open(templateID);

                return _metaDataObj.GetAllTemplateElementsAt(tag);
            }
            catch (Exception ex)
            {
                throw new XFormException("[TagDataXFormImpl::GetAllTemplateElementsAt] failed : Exception = {0}", ex.Message);
            }
        }

        public int CheckSerialData(string templateId, out SortedDictionary<int, int> dataBreaks)
        {
            return _metaDataObj.CheckSerialData(templateId, out dataBreaks);
        }


        #endregion others

        #endregion [ ---Methods--- ]
    }
}

