using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace KTone.Win.KTSDCPrint
{
    public class ColumnSection : ConfigurationElement
    {
        private const string ATTRIBUTE_Name = "Name";
        private const string ATTRIBUTE_VisibleName = "VisibleName";
        private const string ATTRIBUTE_IsEnable = "IsEnable";
        private const string ATTRIBUTE_ColumnOrder = "Order";
        private const string ATTRIBUTE_IsEditable = "IsEditable";
        private const string ATTRIBUTE_IsDeletable = "IsDeletable";


        [ConfigurationProperty(ATTRIBUTE_Name, IsRequired = true, IsKey = true)]
        public string NAME
        {
            get { return (string)this[ATTRIBUTE_Name]; }
            set { this[ATTRIBUTE_Name] = value; }
        }

        [ConfigurationProperty(ATTRIBUTE_VisibleName, IsRequired = true, IsKey = false)]
        public string VISIBLENAME
        {
            get { return (string)this[ATTRIBUTE_VisibleName]; }
            set { this[ATTRIBUTE_VisibleName] = value; }
        }

        [ConfigurationProperty(ATTRIBUTE_IsEnable, IsRequired = true, IsKey = false)]
        public string ISENABLE
        {
            get { return (string)this[ATTRIBUTE_IsEnable]; }
            set { this[ATTRIBUTE_IsEnable] = value; }
        }

        [ConfigurationProperty(ATTRIBUTE_ColumnOrder, IsRequired = true, IsKey = false)]
        public string COLUMNORDER
        {
            get { return (string)this[ATTRIBUTE_ColumnOrder]; }
            set { this[ATTRIBUTE_ColumnOrder] = value; }
        }

        [ConfigurationProperty(ATTRIBUTE_IsEditable, IsRequired = false, IsKey = false)]
        public string ISEDITABLE
        {
            get { return (string)this[ATTRIBUTE_IsEditable]; }
            set { this[ATTRIBUTE_IsEditable] = value; }
        }

        [ConfigurationProperty(ATTRIBUTE_IsDeletable, IsRequired = true, IsKey = false)]
        public string ISDELETABLE
        {
            get { return (string)this[ATTRIBUTE_IsDeletable]; }
            set { this[ATTRIBUTE_IsDeletable] = value; }
        }

    }

    public class ColumnCollection : ConfigurationElementCollection
    {
        public ColumnCollection()
        {
            this.AddElementName = "Column";
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as ColumnSection).NAME;
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ColumnSection();
        }

        public new ColumnSection this[string key]
        {
            get { return base.BaseGet(key) as ColumnSection; }
        }

        public ColumnSection this[int ind]
        {
            get { return base.BaseGet(ind) as ColumnSection; }
        }
    }

    public class ConfigColumnSection : ConfigurationSection
    {
        public const string sectionName = "userSettings";

        [ConfigurationProperty("", IsDefaultCollection = true)]
        public ColumnCollection MySettings
        {
            get
            {
                return this[""] as ColumnCollection;
            }
        }

        public static ConfigColumnSection GetSection()
        {
            return (ConfigColumnSection)ConfigurationManager.GetSection(sectionName);
        }
    }

   
}
