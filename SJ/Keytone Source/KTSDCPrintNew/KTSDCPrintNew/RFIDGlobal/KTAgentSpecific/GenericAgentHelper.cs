using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace KTone.RFIDGlobal
{
    /// <summary>
    /// 
    /// </summary>
    public class GenericAgentHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlParams"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public static string CreateGenericXMLForAgent(Dictionary<string, string> xmlParams, List<string> tags)
        {

            if (null == xmlParams || xmlParams.Count == 0)
                throw new Exception("Xml Params not present");

            try
            {
                MemoryStream mStream = new MemoryStream();

                string componentid = xmlParams["COMPONENT_ID"];
                string tagMovedTime = xmlParams["TAG_MOVED_TIME"];
                string agentType = xmlParams["AGENT_TYPE"];
                string fromComponentId = xmlParams["FROM_COMPONENT_ID"];
                string toComponentId = xmlParams["TO_COMPONENT_ID"];
                string fromComponentName = xmlParams["FROM_COMPONENT_NAME"];
                string toComponentName = xmlParams["TO_COMPONENT_NAME"];
                string movementType = xmlParams["MOVEMENT_TYPE"];
                string fromSubComponentId = string.Empty;
                string toSubComponentId = string.Empty;

                if (agentType == "ACTIVEWAVE_AGENT")
                {
                    fromSubComponentId = xmlParams["FROM_SUB_COMPONENT_ID"];
                    toSubComponentId = xmlParams["TO_SUB_COMPONENT_ID"];
                }


                XmlWriter writer = StartXmlWriting("Inventory", mStream);
                if (null != xmlParams && tags != null)
                {
                    writer.WriteAttributeString("AgentId", componentid);
                    writer.WriteAttributeString("AgentType", agentType);
                    writer.WriteAttributeString("Time", tagMovedTime);

                    writer.WriteStartElement("MovementDetail");
                    writer.WriteAttributeString("FromComponentId", fromComponentId);
                    writer.WriteAttributeString("FromComponentName", fromComponentName);
                    writer.WriteAttributeString("ToComponentId", toComponentId);
                    writer.WriteAttributeString("ToComponentName", toComponentName);
                    writer.WriteAttributeString("ToSubComponent", toSubComponentId);
                    writer.WriteAttributeString("FromSubComponent", fromSubComponentId);
                    writer.WriteAttributeString("MovementType", movementType);

                    foreach (string tag in tags)
                    {
                        writer.WriteStartElement("RFTag");
                        writer.WriteAttributeString("Id", tag);
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                }
                else
                {
                    writer.WriteFullEndElement();
                }
                return EndXmlWriting(writer, mStream);

            }
            catch (Exception)
            {
                throw new Exception("Unbale to get the params");
            }
        }


        /// <summary>
        /// Common method to start XML writting 
        /// </summary>
        /// <param name="opCode"></param>
        /// <param name="mStream"></param>
        /// <returns></returns>
        private static XmlWriter StartXmlWriting(string opCode, MemoryStream mStream)
        {
            XmlWriter Writer = null;
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;

            Writer = XmlTextWriter.Create(mStream, settings);

            Writer.WriteStartDocument();
            Writer.WriteStartElement(opCode);

            return Writer;
        }

        /// <summary>
        /// Common method to end XML writting
        /// </summary>
        /// <param name="xWriter"></param>
        /// <param name="mStream"></param>
        /// <returns></returns>
        private static string EndXmlWriting(XmlWriter xWriter, MemoryStream mStream)
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
            catch (Exception exp)
            {
                throw exp;
            }
            return cmdstr;
        }
    }
}
