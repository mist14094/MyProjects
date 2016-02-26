using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace KTone.Win.KTSDCPrint
{
    public class LabelFormat : ConfigurationElement
    {
        private const string Label_Name = "Name";
        private const string FileName = "FileName";
        private const string Description = "Description";

        [ConfigurationProperty(Label_Name, IsRequired = true, IsKey = true)]
        public string LABELNAME
        {
            get { return (string)this[Label_Name]; }
            set { this[Label_Name] = value; }
        }

        [ConfigurationProperty(FileName, IsRequired = true, IsKey = false)]
        public string FILENAME
        {
            get { return (string)this[FileName]; }
            set { this[FileName] = value; }
        }

        [ConfigurationProperty(Description, IsRequired = true, IsKey = false)]
        public string DESCRIPTION
        {
            get { return (string)this[Description]; }
            set { this[Description] = value; }
        }

    }

    public class LabelCollection : ConfigurationElementCollection
    {
        public LabelCollection()
        {
            this.AddElementName = "Labels";
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as LabelFormat).LABELNAME;
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new LabelFormat();
        }

        public new LabelFormat this[string key]
        {
            get { return base.BaseGet(key) as LabelFormat; }
        }

        public LabelFormat this[int ind]
        {
            get { return base.BaseGet(ind) as LabelFormat; }
        }
    }

    public class ConfigLabelSection : ConfigurationSection
    {
        public const string secName = "labelSettings";

        [ConfigurationProperty("", IsDefaultCollection = true)]
        public LabelCollection LabelSetting
        {
            get
            {
                return this[""] as LabelCollection;
            }
        }

        public static ConfigLabelSection GetSections()
        {
            return (ConfigLabelSection)ConfigurationManager.GetSection(secName);
        }
    }
}
