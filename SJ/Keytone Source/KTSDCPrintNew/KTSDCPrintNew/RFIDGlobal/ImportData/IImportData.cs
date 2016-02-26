/********************************************************************************************************
Copyright (c) 2005 KeyTone Technologies.All Right Reserved

KeyTone's source code and documentation is copyrighted and contains proprietary information.
Licensee shall not modify, create any derivative works, make modifications, improvements, 
distribute or reveal the source code ("Improvements") to anyone other than the software 
developers of licensee's organization unless the licensee has entered into a written agreement
("Agreement") to do so with KeyTone Technologies Inc. Licensee hereby assigns to KeyTone all right,
title and interest in and to such Improvements unless otherwise stated in the Agreement. Licensee 
may not resell, rent, lease, or distribute the source code alone, it shall only be distributed in 
compiled component of an application within the licensee'organization. 
The licensee shall not resell, rent, lease, or distribute the products created from the source code
in any way that would compete with KeyTone Technologies Inc. KeyTone' copyright notice may not be 
removed from the source code.
   
Licensee may be held legally responsible for any infringement of intellectual property rights that
is caused or encouraged by licensee's failure to abide by the terms of this Agreement. Licensee may 
make copies of the source code provided the copyright and trademark notices are reproduced in their 
entirety on the copy. KeyTone reserves all rights not specifically granted to Licensee. 
 
Use of this source code constitutes an agreement not to criticize, in any way, the code-writing style
of the author, including any statements regarding the extent of documentation and comments present.

THE SOFTWARE IS PROVIDED "AS IS" BASIS, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING 
BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER  LIABILITY, 
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE
OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. KEYTONE TECHNOLOGIES SHALL NOT BE LIABLE FOR ANY DAMAGES 
SUFFERED BY LICENSEE AS A RESULT OF USING, MODIFYING OR DISTRIBUTING THIS SOFTWARE OR ITS DERIVATIVES.
********************************************************************************************************/
 using System;
using System.Data;
using System.IO;

namespace KTone.RFIDGlobal.ImportData
{	
	/// <summary>
	/// Stores a record information of a record in flat file.
	/// </summary>
    //[Serializable] 
    //public struct FlatFileRecInfo
    //{
    //    /// <summary>
    //    /// Record string as it is stored in a flat file.
    //    /// </summary>
    //    public string recordLine;
    //    /// <summary>
    //    /// Stores fields and corr value
    //    /// </summary>
    //    public System.Collections.Hashtable fieldValueHash;
    //    /// <summary>
    //    /// It identifies whether a record is valid/not 
    //    /// </summary>
    //    public bool recStatus;
    //    /// <summary>
    //    /// Line Number
    //    /// </summary>
    //    public int recNo;
    //}



    /// <summary>
    /// Defines the Importers
    /// </summary>
    public enum Importer
    {
        /// <summary>
        /// OrderDetails
        /// </summary>
        OrderDetails,

        /// <summary>
        /// ContainerDetails
        /// </summary>
        ContainerDetails,
        /// <summary>
        /// Retailink SSCC data import using comma seperated flat file
        /// </summary>
        RetailLinkSSCC,

        /// <summary>
        /// Retailink SGTIN data import using comma seperated flat file
        /// </summary>
        RetailLinkSGTIN,

        /// <summary>
        /// RetailLink data import using EDI 816 
        /// </summary>
        RetailLinkEDI861,

        /// <summary>
        /// ProductCatalogue,
        /// </summary>
        ProductCatalogue,

        /// <summary>
        /// ItemDetails,
        /// </summary>
        ItemDetails,

        /// <summary>
        /// Fedex
        /// </summary>
        Fedex,

        /// <summary>
        /// UPS
        /// </summary>
        UPS,

        /// <summary>
        /// Asset
        /// </summary>
        AssetDetails,

        ///<summary>
        /// Asset Association
        /// </summary>
        AssetAssociation,
        /// <summary>
        /// Asset Zone Association
        /// </summary>
        AssetZoneAssociation

    }
	

	/// <summary>
	/// Stores the configuration settings for importing the files
	/// </summary>
	[Serializable]
	public struct ImportSettings
	{
		/// <summary>
		/// Import StartTime
		/// </summary>
		private DateTime m_StartTime;
		
		/// <summary>
		/// Import EndTime
		/// </summary>
		private DateTime m_EndTime;

		/// <summary>
		/// ImportInterval in Minutes
		/// </summary>
		private double m_ImportIntervalMin;
 
		/// <summary>
		/// FileCleanupInterval in Minutes
		/// </summary>
		private int m_FileCleanupIntervalMin;

		/// <summary>
		/// Directiory where the files to be imporyted are stored 
		/// </summary>
		private string  m_FlatFileDirectiory;

		/// <summary>
		/// DBConnectionString
		/// </summary>
		private string m_DBConnectionString;

		/// <summary>
		/// Time at which notification mail is to be generated.
		/// </summary>
		private DateTime m_NotifyTime;

		/// <summary>
		/// SMTPServerName
		/// </summary>
		private string m_SMTPServerName;

		/// <summary>
		/// Mail sender's Id
		/// </summary>
		private string m_MailFrom;

		/// <summary>
		/// Mail receiver's Id
		/// </summary>
		private string m_MailTo;

		/// <summary>
		/// Bcc
		/// </summary>
		private string m_Bcc;

		/// <summary>
		/// Initializes the ImportSettings 
		/// </summary>
		/// <param name="startTime"></param>
		/// <param name="endTime"></param>
		/// <param name="importIntervalMin"></param>
		/// <param name="fileCleanupIntervalMin"></param>
		/// <param name="flatFileDirectiory"></param>
		/// <param name="dbConnectionString"></param>
		/// <param name="notifyTime"></param>
		/// <param name="smtpServerName"></param>
		/// <param name="mailFrom"></param>
		/// <param name="mailTo"></param>
		/// <param name="bcc"></param>
		public ImportSettings(DateTime startTime,DateTime endTime,
			double importIntervalMin,int fileCleanupIntervalMin,
			string flatFileDirectiory,string dbConnectionString,
			DateTime notifyTime,string smtpServerName,string mailFrom,string mailTo,string bcc)
		{
			m_StartTime  = startTime;
			m_EndTime = endTime;
			m_ImportIntervalMin = importIntervalMin;
			m_FileCleanupIntervalMin = fileCleanupIntervalMin;
			m_FlatFileDirectiory = flatFileDirectiory;
			m_DBConnectionString = dbConnectionString;
			m_NotifyTime = notifyTime;
			m_SMTPServerName = smtpServerName;
			m_MailFrom = mailFrom;
			m_MailTo = mailTo;
			m_Bcc = bcc;

		}

		/// <summary>
		/// Import StartTime
		/// </summary>
		public DateTime StartTime
		{
			get
			{
				return m_StartTime;
			}

		}
		/// <summary>
		/// Import EndTime
		/// </summary>
		public DateTime EndTime
		{
			get
			{
				return m_EndTime;
			}
		}

		/// <summary>
		/// ImportInterval in Minutes
		/// </summary>
		public double ImportIntervalMin
		{
			get
			{
				return m_ImportIntervalMin;
			}
		}

		/// <summary>
		/// FileCleanupInterval
		/// </summary>
		public int FileCleanupIntervalMin
		{
			get
			{
				return m_FileCleanupIntervalMin ;
			}
		}

		/// <summary>
		/// FlatFileDirectiory
		/// </summary>
		public string FlatFileDirectiory
		{
			get
			{
				return m_FlatFileDirectiory;;
			}
		}

		/// <summary>
		/// DBConnectionString
		/// </summary>
		public string DBConnectionString
		{
			get
			{
				return m_DBConnectionString;
			}
		}


		/// <summary>
		/// NotifyTime
		/// </summary>
		public DateTime NotifyTime
		{
			get
			{
				return m_NotifyTime;
			}
		}


		/// <summary>
		/// SMTPServerName 
		/// </summary>
		public string SMTPServerName 
		{
			get
			{
				return m_SMTPServerName ;
			}
		}


		/// <summary>
		/// MailFrom
		/// </summary>
		public string MailFrom
		{
			get
			{
				return m_MailFrom; 
			}
		}

		/// <summary>
		/// MailTo
		/// </summary>
		public string MailTo
		{
			get
			{
				return m_MailTo;
			}
		}

		/// <summary>
		/// BCC
		/// </summary>
		public string Bcc 
		{
			get
			{
				return m_Bcc ;
			}
		}
		
	}
	

	/// <summary>
	/// delegate to handle OnImportEvent 
	/// </summary>
	//public delegate void ImportDelHandler(FlatFileRecInfo[] recInfoArr, string fileName);

	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public interface IImportData
	{
		/// <summary>
		/// Enables the  Importer
		/// </summary>
		void Enable();

		/// <summary>
		/// Disables the importer
		/// </summary>
		void Disable();
 
		/// <summary>
		/// Retrieves settings for the importer.
		/// </summary>
		/// <returns></returns>
		ImportSettings GetSettings();
		
		/// <summary>
		/// Updates the import settings to config file
		/// </summary>
		/// <param name="importSettings"></param>
		void UpdateSettings(ImportSettings importSettings);

		
		/// <summary>
		/// Registers the client returning unique clientId
		/// </summary>
		/// <returns>ClientId</returns>
		string RegisterLogClient();

		/// <summary>
		/// UnRegisters LogClient
		/// </summary>
		/// <param name="clientId"></param>
		void UnRegisterLogClient(string clientId);

		/// <summary>
		/// Returns next log file contents  
		/// </summary>
		/// <param name="clientId"></param>
		/// <returns></returns>
		string GetNextLogMessage(string clientId);
		
		/// <summary>
		/// Returns previous log file contents  
		/// </summary>
		/// <param name="clientId"></param>
		/// <returns></returns>
		string GetPreviousLogMessage(string clientId);

		/// <summary>
		/// Returns first log file contents  
		/// </summary>
		/// <param name="clientId"></param>
		/// <returns></returns>
		string GetFirstLogMessage(string clientId);

		/// <summary>
		/// Returns last log file contents  
		/// </summary>
		/// <param name="clientId"></param>
		/// <returns></returns>
		string GetLastLogMessage(string clientId);

		/// <summary>
		/// Starts uploading the file to be processed
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns>ClientId</returns>
		string StartUploading(string fileName);

		/// <summary>
		/// Uploads  a block of byteArr read from the file
		/// </summary>
		/// <param name="sessionID"></param>
		/// <param name="fileBlock"></param>
		void UpLoadBlock(string sessionID,byte[] fileBlock);
		/// <summary>
		/// Stops uploading the file and Saves it at server.
		/// </summary>
		/// <param name="clientId"></param>
		void EndUploading(string clientId);

		/// <summary>
		/// Processes the given file 
		/// </summary>
		/// <param name="fileName">name of the file to be processed</param>
		/// <param name="importedRecCnt">returns number of imported records</param>
		/// <param name="errorRecCnt">returns number of error records</param>
		/// <param name="warningRecCnt">returns number of warning records</param>
		void ProcessImmediately(string fileName,out int importedRecCnt,
								out int errorRecCnt,out int warningRecCnt);

        /// <summary>
        /// Creates file from memory stream and processes file .
        /// </summary>
        /// <param name="fileName">file name without path</param>
        /// <param name="fileText"></param>
        /// <param name="importedRecCnt">returns number of imported records</param>
        /// <param name="errorRecCnt">returns number of error records</param>
        /// <param name="warningRecCnt">returns number of warning records</param>
        void ProcessImmediately(string fileName, string fileText, out int importedRecCnt,
                                out int errorRecCnt, out int warningRecCnt);
		
		/// <summary>
		/// Event raised when a the data is imported 
		/// </summary>
		event ImportDelHandler OnImportEvent;

		/// <summary>
		/// working ststus of the component
		/// </summary>
		/// <returns></returns>
		string IsWorking();

        /// <summary>
        /// Used to create a record from an application for say Order/product 
        /// </summary>
        /// <param name="dsRecordInfo"></param>
        void ManualCreate(DataSet dsRecordInfo);
	}

}


