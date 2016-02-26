using System;
using System.Collections.Generic;
using System.Text;

namespace KTone.RFIDGlobal.ConfigParams
{
    /// <summary>
    /// Used to lookup/fetch the values corresponding to the key supplied from the Data\ConfigParams\GlobalConfigs\KTone.Global.config file
    /// </summary>
    public class GlobalConfigParams
    {
        //private static IConfigParams objGlobalConfig = ConfigParamsImpl.GetGlobalParamInstace("KTone.Global.config");
        private static IConfigParams objGlobalConfig = null;
        //private GlobalConfigParams()
        //{

        //}

         static GlobalConfigParams()
        {
            objGlobalConfig = ConfigParamsImpl.GetGlobalParamInstace("KTone.Global.config");
        }

        /// <summary>
        /// TO get the string output
        /// </summary>
        /// <param name="name"></param>
        /// <param name="paramVal"></param>
        public static void Lookup(string name, out string paramVal)
        {
            paramVal = "";
            objGlobalConfig.Lookup(name, out paramVal);
        }
        public static void LookupDecoded(string name,out string paramVal)
        {
            paramVal = "";
            Lookup(name,out paramVal);
            paramVal = RFUtils.Decode(paramVal);
        }

        public static string BaseDirPath
        {
            get
            {
                return ((ConfigParamsImpl)objGlobalConfig).BaseDirPath;
            }
        }
        /// <summary>
        /// TO get the boolean output
        /// </summary>
        /// <param name="name"></param>
        /// <param name="paramVal"></param>
        public static void Lookup(string name, out bool paramVal)
        {
            paramVal = false;
            objGlobalConfig.Lookup(name, out paramVal);
        }

        /// <summary>
        /// TO get the In16 output
        /// </summary>
        /// <param name="name"></param>
        /// <param name="paramVal"></param>
        public static void Lookup(string name, out Int16 paramVal)
        {
            paramVal = 0;
            objGlobalConfig.Lookup(name, out paramVal);
        }

        /// <summary>
        /// TO get the In32 output
        /// </summary>
        /// <param name="name"></param>
        /// <param name="paramVal"></param>
        public static void Lookup(string name, out Int32 paramVal)
        {
            paramVal = 0;
            objGlobalConfig.Lookup(name, out paramVal);
        }

        /// <summary>
        /// TO get the Int64 output
        /// </summary>
        /// <param name="name"></param>
        /// <param name="paramVal"></param>
        public static void Lookup(string name, out Int64 paramVal)
        {
            paramVal = 0;
            objGlobalConfig.Lookup(name, out paramVal);
        }


        /// <summary>
        /// TO get the float output
        /// </summary>
        /// <param name="name"></param>
        /// <param name="paramVal"></param>
        public static void Lookup(string name, out float paramVal)
        {
            paramVal = 0;
            objGlobalConfig.Lookup(name, out paramVal);
        }

        /// <summary>
        /// TO get the double output
        /// </summary>
        /// <param name="name"></param>
        /// <param name="paramVal"></param>
        public static void Lookup(string name, out double paramVal)
        {
            paramVal = 0;
            objGlobalConfig.Lookup(name, out paramVal);
        }

        /// <summary>
        /// Creats new attribute with key-value pair as name and value supplied
        /// </summary>
        /// <param name="name"></param>
        /// <param name="paramVal"></param>
        public static void Save(string name, string paramVal)
        {
            objGlobalConfig.Save(name, paramVal);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="defVal"></param>
        /// <param name="minVal"></param>
        /// <param name="maxVal"></param>
        void GetLimits(out string defVal, out string minVal, out string maxVal)
        {
            objGlobalConfig.GetLimits(out defVal, out minVal, out maxVal);
        }
    }
}
