using System;
using System.Collections.Generic;

using System.Text;
using System.Xml;
using System.IO;

namespace KTone.RFIDGlobal
{
    public class XmlUtils
    {
        public static void WriteAttribute(XmlWriter writer, string attributeName, string value)
        {           
            writer.WriteAttributeString(attributeName,value);
        }

        public static  void WriteWholeElement(XmlWriter writer, string element, byte[] value)
        {
            if (value == null)
                throw new Exception("byte array is not valid.");

            string criteriaStr = BitConverter.ToString(value);
            criteriaStr = criteriaStr.Replace("-", "");
            WriteWholeElement(writer, element, criteriaStr);
        }

        public static void WriteWholeElement(XmlWriter writer, string element, ushort value)
        {
            WriteWholeElement(writer, element, Convert.ToString(value));
        }

        public static void WriteWholeElement(XmlWriter writer, string element, short value)
        {
            WriteWholeElement(writer, element, Convert.ToString(value));
        }

        public static void WriteWholeElement(XmlWriter writer, string element, uint value)
        {
            WriteWholeElement(writer, element, Convert.ToString(value));
        }

        public static void WriteWholeElement(XmlWriter writer, string element, int value)
        {
            WriteWholeElement(writer, element, Convert.ToString(value));
        }

        public static void WriteWholeElement(XmlWriter writer, string element, string value)
        {
            writer.WriteStartElement(element);
            writer.WriteString(value);
            writer.WriteEndElement();
        }
        public static void WriteWholeElementDefaultPass(XmlWriter writer, string element, string value)
        {
            if (value == null
                || value.Trim() == string.Empty)
                value = "0000";
            writer.WriteStartElement(element);
            writer.WriteString(value);
            writer.WriteEndElement();

        }
        public static void WriteWholeElememtWithAttributes(XmlWriter writer, Dictionary<string, string> attributes,string element, string value)
        {
            writer.WriteStartElement(element);
            foreach (KeyValuePair<string, string> kvp in attributes)
            {
                writer.WriteAttributeString(kvp.Key, kvp.Value);
            }
            //writer.WriteString(value);
            writer.WriteEndElement();
        }
        public static void WriteWholeElementWithAttribute(XmlWriter writer, string attribute, string attributeValue, string element, string value)
        {
            writer.WriteStartElement(element);
            writer.WriteAttributeString(attribute, attributeValue);
            writer.WriteString(value);
            writer.WriteEndElement();
        }
        public static XmlWriter StartXmlWriting(string opcode, MemoryStream mStream)
        {
            XmlWriter Writer = null;
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;

            Writer = XmlTextWriter.Create(mStream, settings);

            Writer.WriteStartDocument();
            Writer.WriteStartElement(opcode.ToString());

            return Writer;
        }

        public static XmlWriter StartXmlWriting(string opcode, MemoryStream mStream,bool omitXmlDeclaration)
        {
            XmlWriter Writer = null;
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = omitXmlDeclaration;

            Writer = XmlTextWriter.Create(mStream, settings);

            Writer.WriteStartDocument();
            Writer.WriteStartElement(opcode.ToString());

            return Writer;
        }

        public static XmlWriter StartXmlWriting(string opcode, MemoryStream mStream, bool omitXmlDeclaration,
            string dockTypeName, string pubid, string sysid, string subset)
        {
            XmlWriter Writer = null;
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = omitXmlDeclaration;

            Writer = XmlTextWriter.Create(mStream, settings);
            Writer.WriteDocType(dockTypeName, pubid, sysid, subset);
            Writer.WriteStartDocument();
            Writer.WriteStartElement(opcode.ToString());

            return Writer;
        }

        public static string EndXmlWriting(XmlWriter xWriter, MemoryStream mStream)
        {
            string cmdstr = string.Empty;
            try
            {
                xWriter.WriteEndDocument();
                xWriter.Flush();
                StreamReader strmReader = new StreamReader(mStream);
                mStream.Seek(0, SeekOrigin.Begin);
                cmdstr += strmReader.ReadToEnd();
                if (xWriter != null)
                    xWriter.Close();
                if (mStream != null)
                    mStream.Close();
                if (strmReader != null)
                    strmReader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return cmdstr;
        }


        public static void GetNodeValue(string path, XmlNode xNode, out uint value, bool throwException)
        {
            value = 0;
            string valueStr = string.Empty;
            GetNodeValue(path, xNode, out valueStr, true);
            if (valueStr != null & valueStr.Trim() != string.Empty)
                value = Convert.ToUInt32(valueStr);
        }
        public static void GetNodeValue(string path, XmlDocument xDoc, out string value, bool throwException)
        {
            value = string.Empty;
            XmlNode node = xDoc.SelectSingleNode(path);
            if (node != null)
                value = node.InnerText.Trim();
            else if (throwException)
                new ApplicationException("Could not find Node at Path : " + path);
        }
        public static void GetNodeValue(string path, XmlDocument xDoc, out uint value, bool throwException)
        {

            value = 0;
            string valueStr = string.Empty;

            try
            {
                GetNodeValue(path, xDoc, out valueStr, true);
                value = Convert.ToUInt32(valueStr);
            }
            catch
            {
                if (throwException)
                    throw new ApplicationException("Value not correct or node may not be present");
                else
                    value = 0;
            }            
        }

        public static void GetNodeValue(string path, XmlNode xNode, out int value, bool throwException)
        {
            value = 0;
            string valueStr = string.Empty;
            GetNodeValue(path, xNode, out valueStr, true);
            if (valueStr != null & valueStr.Trim() != string.Empty)
                value = Convert.ToInt32(valueStr);
        }

        public static void GetNodeValue(string path, XmlNode xNode, out string value, bool throwException)
        {
            if (xNode == null)
                throw new ApplicationException("Could not find Node at Path : " + path);
            XmlNode childnode = null;
            childnode = xNode.SelectSingleNode(path);
            value = string.Empty;
            if (childnode != null)
                value = childnode.InnerText.Trim();
            else if (throwException)
                new ApplicationException("Could not find Node at Path : " + path);
        }

        public static void GetNodeValue(string path, XmlNode xNode, out bool value, bool throwException)
        {
            if (xNode == null)
                throw new ApplicationException("Could not find Node at Path : " + path);
            XmlNode childnode = null;
            childnode = xNode.SelectSingleNode(path);
            value = false;
            if (childnode != null)
            {
                int aVal = Convert.ToInt32(childnode.InnerText.Trim());
                if (aVal == 0)
                {
                    value = false;
                }
                else if (aVal == 1)
                {
                    value = true;
                }
                else
                {
                    throw new ApplicationException("Invalid Value : " + path);
                }
            }
            else if (throwException)
                new ApplicationException("Could not find Node at Path : " + path);
        }
        
        public List<string> GetSeparateResponses(string completeReponse, string splitingString,
            string cmdTag)
        {

            List<string> responses = new List<string>();
            int firstIndex = -1;
            int terminatingIndex = -1;
            string response = string.Empty;
            string temstr = completeReponse;
            int nextRspStartIndex = -1;
            string terminatingStr = "</" + cmdTag + ">";
            while (true)
            {
                firstIndex =
                temstr.IndexOf(splitingString);
                if (firstIndex == -1)
                    break;
                terminatingIndex =
                temstr.IndexOf(terminatingStr, firstIndex);
                if (terminatingIndex == -1)
                    break;
                nextRspStartIndex = terminatingIndex + terminatingStr.Length;
                response = temstr.Substring(firstIndex,
                    nextRspStartIndex - firstIndex);
                temstr = temstr.Substring(nextRspStartIndex);
                responses.Add(response);
            }
            if (responses.Count == 0)
                throw new ApplicationException("Response is invalid or empty ");
            return responses;
        }

        public static void EndXmlWritingToSave(XmlWriter xWriter, MemoryStream mStream)
        {
            try
            {
                xWriter.WriteEndDocument();
                xWriter.Flush();

                mStream.Seek(0, SeekOrigin.Begin);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
