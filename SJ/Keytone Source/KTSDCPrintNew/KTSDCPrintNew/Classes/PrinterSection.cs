using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace KTone.Win.KTSDCPrint
{
    public class PrinterFormat : ConfigurationElement
    {
        private const string ATTRIBUTE_Name = "Name";
       

        [ConfigurationProperty(ATTRIBUTE_Name, IsRequired = true, IsKey = true)]
        public string NAME
        {
            get { return (string)this[ATTRIBUTE_Name]; }
            set { this[ATTRIBUTE_Name] = value; }
        }

     

    }

    public class PrinterCollection : ConfigurationElementCollection
    {
        public PrinterCollection()
        {
            this.AddElementName = "Column";
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as PrinterFormat).NAME;
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new PrinterFormat();
        }

        public new PrinterFormat this[string key]
        {
            get { return base.BaseGet(key) as PrinterFormat; }
        }

        public PrinterFormat this[int ind]
        {
            get { return base.BaseGet(ind) as PrinterFormat; }
        }
    }

    public class ConfigPrinterSection : ConfigurationSection
    {
        public const string sectionName = "PrinterSettings";

        [ConfigurationProperty("", IsDefaultCollection = true)]
        public PrinterCollection MySettings
        {
            get
            {
                return this[""] as PrinterCollection;
            }
        }

        public static ConfigPrinterSection GetSection()
        {
            return (ConfigPrinterSection)ConfigurationManager.GetSection(sectionName);
        }
    }


}
