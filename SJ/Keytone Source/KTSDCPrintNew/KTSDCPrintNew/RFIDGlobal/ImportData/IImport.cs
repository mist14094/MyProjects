using System;

namespace KTone.RFIDGlobal.ImportData
{
	/// <summary>
	/// Stores information of a particular field in a flat file.
	/// </summary>
    //[Serializable]
    //public struct FieldInfo
    //{
    //    /// <summary>
    //    /// Name of a field as defined in config file.
    //    /// </summary>
    //    public string fieldName;
    //    /// <summary>
    //    ///  Value of a field as in a flat file
    //    /// </summary>
    //    //public string fieldValue;
    //    /// <summary>
    //    /// Indicates whether the current field is a key column or not.
    //    /// </summary>
    //    public bool isKeyCol;
    //}


	/// <summary>
	/// Stores information of a particular table & its fields.
	/// </summary>
    //[Serializable]
    //public struct TableInfo
    //{
    //    public string tableName;
    //    public FieldInfo[] fieldDetails;
    //}
	

	/// <summary>
	/// Stores a record information of a record in flat file.
	/// </summary>
	[Serializable] 
	public struct FlatFileRecInfo
	{
		/// <summary>
		/// Record string as it is stored in a flat file.
		/// </summary>
		public string recordLine;
		/// <summary>
		/// Stores fields & corr value
		/// </summary>
		public System.Collections.Hashtable fieldValueHash;
		/// <summary>
		/// It identifies whether a record is valid/not 
		/// </summary>
		public bool recStatus;
		/// <summary>
		/// Line Number
		/// </summary>
		public int recNo;
		/// <summary>
		/// Array of TableInfo having fields details for a record.
		/// </summary>
		//public TableInfo[] tableDetails;
	}
	

//    [Serializable]
//    public struct ImportSettings
//    {
//        private DateTime m_StartTime;
		
//        private DateTime m_EndTime;

//        private int m_ImportIntervalMin;
 
//        private int m_FileCleanupIntervalMin;

//        private string  m_BaseDirectiory;

//        private string m_DBConnectionString;

//        private DateTime m_NotifyTime;

//        private string m_SMTPServerName;

//        private string m_MailFrom;

//        private string m_MailTo;

//        private string m_Bcc;

////		public ImportSettings(
////			
////			DateTime startTime,DateTime endTime,
////			int importIntervalMin,int fileCleanupIntervalMin,
////			string baseDirectiory,string dbConnectionString,
////			DateTime notifyTime,string sMTPServerName,
////			string mailFrom,string mailTo,
////			string bcc
////
////			)
////		{
////
////
////			DateTime m_StartTime  = startTime;
////			DateTime m_EndTime = endTime;
////			m_ImportIntervalMin;
////			m_FileCleanupIntervalMin;
////			m_BaseDirectiory;
////			m_DBConnectionString;
////			m_NotifyTime;
////			m_SMTPServerName;
////			m_MailFrom;
////			m_MailTo;
////			m_Bcc;
////
////




////		}

//    }
	

	public delegate void ImportDelHandler(FlatFileRecInfo[] recInfoArr, string fileName);

	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public interface IImport
	{
		event ImportDelHandler OnImportEvent;
		string IsWorking();
	}

}


