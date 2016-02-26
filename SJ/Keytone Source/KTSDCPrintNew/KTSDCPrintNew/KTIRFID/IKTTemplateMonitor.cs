using System;
using System.Collections.Generic;
using System.Text;
using KTone.RFIDGlobal.TagDataXForm;
using KTone.RFIDGlobal.MetaDataEditor;

namespace KTone.Core.KTIRFID
{
    public interface IKTTemplateMonitor
    {
        void AddTemplate(string templateName, int maxNoOfBytes, string byteEndian,
            string templateBody, string description);

        bool RemoveTemplate(string templateName,out string statusResult);

        /// <summary>
        /// Returns a dictionary containing pairs of template id and template name.
        /// </summary>
        /// <returns></returns>
        Dictionary<string, string> GetAllTemplateNames();

        List<TemplateInfo> GetAllTemplates();

        TemplateInfo GetTemplate(string templateName);

        string GetTemplateNameForId(string templateId);

        TemplateElement[] GetTemplateElements(string templateName);

        bool Validate(string templateName);
        
        string Transform(string templateName, byte[] inArr);
        
        byte[] Transform(string templateName, string xmlDataTemplate);
        
        string Transform(string templateName, byte[] inArr, int indexInTag);

        ParsedData[] Transform(string templateName, Dictionary<string, string> tagDataHash,bool getParsedDataBytes);

        Dictionary<string, object> Transform(string templateName, byte[] inArr, int idxInTag,int dataLength, out int nextStartAddress);

        Dictionary<string, object> Transform(string templateName, int totalDataRead, int idxInTag, int dataLength, out int nextStartAddress);

    }
}
