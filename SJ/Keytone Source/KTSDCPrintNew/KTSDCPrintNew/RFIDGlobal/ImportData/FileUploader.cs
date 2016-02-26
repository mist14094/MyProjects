using System;
using System.Text;
using System.IO;


using NLog;


using KTone.RFIDGlobal;

namespace KTone.RFIDGlobal.ImportData
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class FileUploader
	{
		#region [ Attributes ]
		private static FileUploader m_FileUploader = null;
		private string  m_SessionId = string.Empty;
		private int  m_SessionIdCntr = 0;
		private int m_BlockLength = 10000;
		private string m_RelativePath = string.Empty;
		private MemoryStream m_MemStream = null;
		private StreamWriter m_StreamWriter = null;
		private string m_FileName = string.Empty;
		#endregion [ Attributes ]

		#region [Logger]
        private static readonly Logger m_Log
              = KTone.RFIDGlobal.KTLogger.KTLogManager.GetLogger();
        #endregion [Logger]

		private FileUploader(string flatFileDirpath)
		{

		}

		public static FileUploader GetInstance(string flatFileDirpath)
		{
			
			m_Log.Trace("FileUploader::GetInstance() Entering");
			try
			{
				if(m_FileUploader == null)
				{
					m_FileUploader = new FileUploader(flatFileDirpath);				
					m_Log.Info("FileUploader::GetInstance() FileUploader Created");
				}
			}
			catch(Exception exp)
			{
				m_Log.Error("FileUploader::GetInstance()",exp);
				throw exp;
			}
			m_Log.Trace("FileUploader::GetInstance() Leaving");
			
			return m_FileUploader;
		}


		#region [ Public Methods ]
		/// <summary>
		/// It returns an Session ID to start uploading of file.
		/// </summary>
		/// <returns>Session ID </returns>
		public string StartUpLoad(string fileName)
		{
			string lgHeader = "FileUploader::StartUpLoad()";
			try
			{
				m_Log.Trace(lgHeader+" Entering");
				m_FileName = m_RelativePath + fileName;
				//m_SessionId = DateTime.UtcNow.Ticks;
				m_SessionIdCntr++;
				m_SessionId = String.Format("FILEUPLOADCLIENT" + "{0:d4}", m_SessionIdCntr);
				InitStream();
				m_Log.Debug(lgHeader+ " New Session Started with ID:"+ m_SessionId);
				m_Log.Trace(lgHeader+" Leaving");
			}
			catch(Exception exp)
			{
				m_Log.Error(lgHeader,exp);
				throw exp;
			}
			return m_SessionId;
		}

		/// <summary>
		/// Uploads Block of a file 
		/// </summary>
		/// <param name="sessionID">Session ID Provided with StartUpLoad</param>
		/// <param name="fileBlock">Block</param>
		public void UpLoadBlock(string sessionID,byte[] fileBlock)
		{
			string lgHeader = "FileUploader::UploadBlock()";
			try
			{
				m_Log.Trace(lgHeader+" Entering");
				CheckForUpload(sessionID,fileBlock);
				if(m_StreamWriter==null)
					throw new NoStartUploadException("Start Upload first!");
				WriteBytesInStream(fileBlock);     				
				m_Log.Trace(lgHeader+" Leaving");
			}
			catch(NoStartUploadException exp)
			{
				m_Log.Error("FileUploader::UploadBlock()",exp);
				throw exp;
			}
			catch(FileUploadSessionExpireException exp)
			{
				m_Log.Error("FileUploader::UploadBlock()",exp);
				throw exp;
			}
			catch(BlockSizeTooLargeException exp)
			{
				m_Log.Error("FileUploader::CheckSessionID()",exp);
				throw exp;
			}
			catch(Exception exp)
			{
				m_Log.Error("FileUploader::UploadBlock()",exp);
				throw new FileUploadSessionExpireException("Session ended got terminated");
			}
		}

		/// <summary>
		/// Stops Uploading of file and saves it
		/// </summary>
		/// <param name="sessionID"></param>
		public void EndUpLoad(string sessionID)
		{
			string lgHeader = "FileUploader::EndUpoad()";
			try
			{
				m_Log.Trace(lgHeader+" Entering");
				CheckForEndUpload(sessionID);
											
				if(Directory.Exists(m_RelativePath))
					SaveFileFromMemoryStream();					
				else
					throw new DirectoryDoesNotExistException("Directory doesnot exist");
				m_Log.Debug(lgHeader+" EndUploadCompleted for session:"+m_SessionId);
				m_Log.Trace(lgHeader+" Leaving");
			}
			catch(NoStartUploadException exp)
			{
				m_Log.Error("FileUploader::EndUpoad()",exp);
				throw exp;
			}
			catch(FileUploadSessionExpireException exp)
			{
				m_Log.Error("FileUploader::EndUpoad()",exp);
				throw exp;
			}
			catch(BlockSizeTooLargeException exp)
			{
				m_Log.Error("FileUploader::EndUpoad()",exp);
				throw exp;
			}
			catch(Exception exp)
			{
				m_Log.Error("FileUploader::EndUpoad()",exp);
				throw exp;
			}
			finally
			{
				m_SessionId = string.Empty;
				if(m_StreamWriter!=null)
				{	
					try
					{
						m_StreamWriter.Flush();
					}
					catch(Exception exp)
					{
						m_Log.Error("FileUploader::EndUpoad()",exp);
					}
					try
					{
						m_StreamWriter.Close();
					}
					catch(Exception exp)
					{
						m_Log.Error("FileUploader::EndUpoad()",exp);
					}
				}
				if(m_MemStream!=null)
				{
					try
					{
						m_MemStream.Flush();
						m_MemStream.Close();
					}
					catch(Exception exp)
					{
						m_Log.Error("FileUploader::EndUpoad()",exp);
					}
				}
			}
		}

		#endregion [ Public Methods ]

		#region [ Private Methods ] 
		private void CheckForEndUpload(string sessionID)
		{
			CheckUploadBlockParameters(sessionID,null,false);
		}
		private void CheckForUpload(string sessionID,byte [] fileBlock)
		{
			CheckUploadBlockParameters(sessionID,fileBlock,true);
		}
		private void CheckUploadBlockParameters(string sessionID,byte [] fileBlock
			,bool checkFileBlock)
		{
			try
			{
				if(sessionID == string.Empty)
					throw new NoStartUploadException();
				else if(m_SessionId !=sessionID)
					throw new FileUploadSessionExpireException("Session got terminated");
				if(checkFileBlock)
				{
					if(fileBlock==null)
						throw new ArgumentNullException("fileBlock is null");
					if(fileBlock.Length>m_BlockLength)
						throw new BlockSizeTooLargeException("File block size is too Large");
				}
				if((!m_RelativePath.EndsWith(@"\"))
					&&(!m_RelativePath.EndsWith(@"/")))
				{
					m_RelativePath += @"\";
				}
				
			}
			catch(BlockSizeTooLargeException exp)
			{
				m_Log.Error("FileUploader::CheckSessionID()",exp);
				throw exp;
			}
			catch(NoStartUploadException exp)
			{
				m_Log.Error("FileUploader::CheckSessionID()",exp);
				throw exp;
			}
			catch(FileUploadSessionExpireException exp)
			{
				m_Log.Error("FileUploader::CheckSessionID()",exp);
				throw exp;
			}
			catch(Exception exp)
			{
				m_Log.Error("FileUploader::CheckSessionID()",exp);
				throw exp;
			}
		}

		private void SaveFileFromMemoryStream()
		{
			try
			{
				if(m_StreamWriter!=null)
				{	
					m_StreamWriter.Flush(); 
				}
			}
			catch(Exception exp)
			{
				m_Log.Error("FileUploader::SaveFileFromMemoryStream()",exp);
			}

			try
			{			
				if (m_MemStream == null)
					throw new NoStartUploadException("Start UpLoad First");
				if (m_FileName == string.Empty)
					throw new ApplicationException("Provide valid File Name");							
				FileStream fileStream = File.Create(m_FileName);				
				m_MemStream.Seek(0,SeekOrigin.Begin);
				m_MemStream.WriteTo(fileStream);				
				fileStream.Flush();
				fileStream.Close();
			}
			catch(Exception exp)
			{
				m_Log.Error("FileUploader::SaveFileFromMemoryStream()");
				throw exp;
			}
		}

		private void InitStream()
		{
			string lgHeader = "FileUploader::InitStream()";
			try
			{
				try
				{
					if(m_StreamWriter!=null)
					{	
						m_StreamWriter.Close();
					}
				}
				catch(Exception exp)
				{
					m_Log.Error("FileUploader::EndUpoad()",exp);
				}
				try
				{
					if(m_MemStream!=null)
					{
						m_MemStream.Flush();
						m_MemStream.Close();
					}
				}
				catch(Exception exp)
				{
					m_Log.Error("FileUploader::EndUpoad()",exp);
				}
				m_MemStream = new MemoryStream();
				m_StreamWriter = new StreamWriter(m_MemStream); 				
				m_Log.Debug(lgHeader+" New Memory Created");
				m_Log.Info(lgHeader+" New Memory Created");
			}
			catch(Exception exp)
			{
				m_Log.Error(lgHeader,exp);
				throw exp;
			}
		}
		
		private void WriteBytesInStream( byte[] fileBlock)
		{
			string lgheader = "FileUploader::WriteBytesInStream";
			try
			{
				char[] blockinChar = Encoding.UTF8.GetChars(fileBlock) ; 
				m_StreamWriter.Write(blockinChar) ; 
				
				m_StreamWriter.Flush() ; 
			}
			catch(Exception exp)
			{
				m_Log.Error(lgheader,exp);
				throw exp;
			}
		}
		
		#endregion [ Private Methods ] 

		#region [ Properties ]
		/// <summary>
		/// Maximum allowed block of a file.
		/// </summary>
		public int MaxBlockLength
		{
			get
			{
				return m_BlockLength ;
			}
		}
		public string RelativePath
		{
			set
			{
				m_RelativePath = value;
				if(!m_RelativePath.EndsWith(@"\"))
					m_RelativePath += @"\";


			}
			get
			{
				return m_RelativePath;
			}

		}
		#endregion [ Properties ]
	}
}
