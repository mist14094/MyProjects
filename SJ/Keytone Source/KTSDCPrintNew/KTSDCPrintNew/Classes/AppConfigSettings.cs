using System;
using System.Configuration;
using System.Data;

namespace KTone.Win.KTSDCPrint
{
    internal static class AppConfigSettings
    {
        private static int _dataOwnerID;
        private static string _dataOwnerName;
        private static string _applicationName;
        private static string _defaultAssetType;
        private static string _locLocationName;
        private static int _locLocationID;
        private static string _locInstanceName;
        private static string _itemLocationName;
        private static int _itemLocationID;
        private static string _itemInstanceName;
        private static string _xmlString;
        private static string _labelName;
        private static string _labelDesc;
        private static string _printerName;
        private static int _issuecommand;
        private static string userName;
        private static int userID;
        private static DataTable _dtColumnDetails;


        static AppConfigSettings()
        {
            try
            {
                DataTable dtColumns = new DataTable();


                DataColumn dtCol1 = new DataColumn("NAME");
                dtCol1.DataType = System.Type.GetType("System.String");
                dtColumns.Columns.Add(dtCol1);
                DataColumn dtCol2 = new DataColumn("VISIBLENAME");
                dtCol2.DataType = System.Type.GetType("System.String");
                dtColumns.Columns.Add(dtCol2);
                DataColumn dtCol3 = new DataColumn("ISENABLE");
                dtCol3.DataType = System.Type.GetType("System.String");
                dtColumns.Columns.Add(dtCol3);
                DataColumn dtCol4 = new DataColumn("COLUMNORDER");
                dtCol4.DataType = System.Type.GetType("System.Int32");
                dtColumns.Columns.Add(dtCol4);
                DataColumn dtCol5 = new DataColumn("ISEDITABLE");
                dtCol5.DataType = System.Type.GetType("System.Boolean");
                dtColumns.Columns.Add(dtCol5);
                DataColumn dtCol6 = new DataColumn("ISDELETABLE");
                dtCol6.DataType = System.Type.GetType("System.Boolean");
                dtColumns.Columns.Add(dtCol6);

                ConfigColumnSection cs = ConfigColumnSection.GetSection();


                if (cs.MySettings.Count > 0)
                {

                    for (int i = 0; i < cs.MySettings.Count; i++)
                    {
                        DataRow dt = dtColumns.NewRow();

                        dt["NAME"] = Convert.ToString(cs.MySettings[i].NAME);
                        dt["VISIBLENAME"] = Convert.ToString(cs.MySettings[i].VISIBLENAME);
                        dt["ISENABLE"] = Convert.ToString(cs.MySettings[i].ISENABLE);
                        if (string.IsNullOrEmpty(cs.MySettings[i].COLUMNORDER))
                        {
                            dt["COLUMNORDER"] = 100;
                        }
                        else
                            dt["COLUMNORDER"] = Convert.ToInt32(cs.MySettings[i].COLUMNORDER);
                        if (string.IsNullOrEmpty(cs.MySettings[i].ISEDITABLE))
                        {
                            dt["ISEDITABLE"] = false;
                        }
                        else
                            dt["ISEDITABLE"] = Convert.ToBoolean(cs.MySettings[i].ISEDITABLE);
                        if (string.IsNullOrEmpty(cs.MySettings[i].ISDELETABLE))
                        {
                            dt["ISDELETABLE"] = false;
                        }
                        else
                            dt["ISDELETABLE"] = Convert.ToBoolean(cs.MySettings[i].ISDELETABLE);
                        dtColumns.Rows.Add(dt);
                        dtColumns.AcceptChanges();
                    }

                    dtColumns = dtColumns.Select("", "COLUMNORDER").CopyToDataTable();

                }

                _dtColumnDetails = dtColumns;
            }
            catch { }
        }

        public static string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public static int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        public static string DefaultAssetType
        {
            get
            {
                return _defaultAssetType;
            }
            set
            {
                _defaultAssetType = value;
            }
        }

        public static string LabelName
        {
            get
            {
                return _labelName;
            }
            set
            {
                _labelName = value;
            }
        }
        public static int IssueCommand
        {
            get
            {
                return _issuecommand;
            }
            set
            {
                _issuecommand = value;
            }
        }

        public static string PrinterName
        {
            get
            {
                return _printerName;
            }
            set
            {
                _printerName = value;
            }
        }

        public static string LabelDecs
        {
            get
            {
                return _labelDesc;
            }
            set
            {
                _labelDesc = value;
            }
        }

        public static string xmlString
        {
            get
            {
                return _xmlString;
            }
            set
            {
                _xmlString = value;
            }
        }

        public static string ItemLocationName
        {
            get
            {
                return _itemLocationName;
            }
            set
            {
                _itemLocationName = value;
            }
        }

        public static string LocLocationName
        {
            get
            {
                return _locLocationName;
            }
            set
            {
                _locLocationName = value;
            }
        }


        public static int LocLocationID
        {
            get
            {
                return _locLocationID;
            }
            set
            {
                _locLocationID = value;
            }
        }

        public static int ItemLocationID
        {
            get
            {
                return _itemLocationID;
            }
            set
            {
                _itemLocationID = value;
            }
        }

        public static string LocInstanceName
        {
            get
            {
                return _locInstanceName;
            }
            set
            {
                _locInstanceName = value;
            }
        }


        public static string ItemInstanceName
        {
            get
            {
                return _itemInstanceName;
            }
            set
            {
                _itemInstanceName = value;
            }
        }

        public static string ApplicationName
        {
            get
            {
                return _applicationName;
            }
            set
            {
                _applicationName = value;
            }
        }

        public static int DataOwnerID
        {
            get
            {
                return _dataOwnerID;
            }
            set
            {
                _dataOwnerID = value;
            }
        }

        public static DataTable TableStructure
        {
            get
            {
                return _dtColumnDetails;
            }
            set
            {
                _dtColumnDetails = value;
            }
        }
        public static string DataOwnerName
        {
            get
            {
                return _dataOwnerName;
            }
            set
            {
                _dataOwnerName = value;
            }
        }

        internal static string LogLevel
        {
            get
            {
                return ConfigurationManager.AppSettings["LogLevel"];
            }
        }

        internal static string ApplicationHostPort
        {
            get
            {
                return ConfigurationManager.AppSettings["HostPort"];
            }
        }
    }

    public enum Attributes
    {
        AAPLICATION_NAME,
        LOC_LOCATION_NAME,
        LOC_LOCATION_ID,
        LOC_INSTANCE_NAME,
        ITEM_LOCATION_NAME,
        ITEM_LOCATION_ID,
        ITEM_INSTANCE_NAME
    }
}
