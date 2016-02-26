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
using System.Collections;
using System.IO;
using System.Configuration;

using KTone.RFIDGlobal;


using NLog;


namespace KTone.RFIDGlobal.ImportData
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class LogFileList
	{		
		private string m_RelativeDirectoryPath = string.Empty;
		private int m_CurrentLogCtr = -1;		
		private string[] m_logFilePathArray = null;
		private string m_FilePattern = "*.*";
		private string m_CurrentLogPath = string.Empty;

		#region [Logger]
        private static readonly Logger m_Log
            = KTone.RFIDGlobal.KTLogger.KTLogManager.GetLogger();
		#endregion [Logger]
		
		#region [Constructor]
		public LogFileList(string relativeDirectoryPath)
		{
			string lgHeader = "LogFileList::GetLog()";
			try
			{
                if (!Directory.Exists(relativeDirectoryPath))
                {
                    Directory.CreateDirectory(relativeDirectoryPath);
                    //throw new DirectoryDoesNotExistException("Log Directory Path:" + relativeDirectoryPath
                    //  + " not found.");
                }
				
				m_RelativeDirectoryPath = relativeDirectoryPath;
				m_logFilePathArray = GetLogFilePaths();
			}
			catch(Exception exp)
			{
                m_Log.Error(lgHeader, exp);
				throw exp;
			}
		}


		#endregion [Constructor]

		#region [ Public Methods ] 
		public string MoveNext()
		{
			string lgHeader = "LogFileList::MoveNext()";
			string logData  = string.Empty;

            m_Log.Trace(lgHeader + " Entering");

			try
			{
				m_CurrentLogCtr++;
				if(m_logFilePathArray==null||
                    m_logFilePathArray.Length <= m_CurrentLogCtr ||
                    m_CurrentLogCtr<0)
				{
					m_CurrentLogCtr =m_logFilePathArray.Length;
					logData = "No Next Logs are Available.";
					return logData;
				}
				else
				{
					m_CurrentLogPath = m_logFilePathArray[m_CurrentLogCtr];
					logData = GetLog(m_CurrentLogPath);			
				}
			}
			catch(Exception exp)
			{
                m_Log.Error(lgHeader, exp);
				throw exp;
			}
			
			m_Log.Trace(lgHeader + " Leaving");
			return logData ;			
		}
		public string MovePrevious()
		{
			string lgHeader = "LogFileList::MovePrevious()";
			string logData  = string.Empty;
			m_Log.Trace(lgHeader + " Entering");
			try
			{	
				m_CurrentLogCtr--;
				if(m_logFilePathArray==null||
					m_CurrentLogCtr<0)
				{
					m_CurrentLogCtr = -1;
					return logData = "No Previous Logs are available";
				}
				else
				{
					m_CurrentLogPath = m_logFilePathArray[m_CurrentLogCtr];
					logData = GetLog(m_CurrentLogPath);
				}
				
			}
			catch(Exception exp)
			{
				m_Log.Error(lgHeader,exp);
				throw exp;
			}			
			m_Log.Trace(lgHeader + " Leaving");
			return logData ;				
		}
		public string MoveFirst()
		{
			string lgHeader = "LogFileList::MoveFirst()";
			string logData  = string.Empty;
			m_Log.Trace(lgHeader + " Entering");
			try
			{
				m_CurrentLogCtr = 0;
				if(m_logFilePathArray==null||
					m_logFilePathArray.Length<=m_CurrentLogCtr)
				{
					logData ="No Logs are available";
					return logData;
				}			
				m_CurrentLogPath = m_logFilePathArray[m_CurrentLogCtr];
				logData = GetLog(m_CurrentLogPath);
			}
			catch(Exception exp)
			{
				m_Log.Error(lgHeader,exp);
				throw exp;
			}			
			m_Log.Trace(lgHeader + " Leaving");
			return logData ;				
		}
		public string MoveLast()
		{
			string lgHeader = "LogFileList::MoveLast()";
			string logData  = string.Empty;
			m_Log.Trace(lgHeader + " Entering");
			try
			{
				if(m_logFilePathArray==null||
					m_logFilePathArray.Length==0)					
				{
					logData = "No Logs are available";	
					m_CurrentLogCtr-=1;
					return logData;			
				}
				else
				{
					m_CurrentLogCtr = m_logFilePathArray.Length -1;
					m_CurrentLogPath = m_logFilePathArray[m_CurrentLogCtr];
					logData = GetLog(m_CurrentLogPath);
				}				
				
			}
			catch(Exception exp)
			{
				m_Log.Error(lgHeader,exp);
				throw exp;
			}
			
			m_Log.Trace(lgHeader + " Leaving");
			return logData ;				
		}		

		#endregion [ Public Methods ] 

		#region [ Helper Function ]
		private string [] GetLogFilePaths()
		{
			string lgHeader = "LogFileList::GetLog()";
			string [] filePathAry = null;
            DirectoryInfo objFileDir = null;
            FileInfo[] fileInfoArray = null;
            FileInfo tempFileInfo = null;
			try
			{
                objFileDir = new DirectoryInfo(m_RelativeDirectoryPath);
                fileInfoArray = objFileDir.GetFiles(m_FilePattern);
                //bubble sort based on last accessed file
                for (int otCtr = fileInfoArray.Length - 1; otCtr >= 0; otCtr--)
                {
                    for (int inCtr = 1; inCtr <= otCtr; inCtr++)
                    {
                        if (fileInfoArray[inCtr - 1].LastWriteTime >
                            fileInfoArray[inCtr].LastWriteTime)
                        {
                            tempFileInfo = fileInfoArray[inCtr - 1];
                            fileInfoArray[inCtr - 1] = fileInfoArray[inCtr];
                            fileInfoArray[inCtr] = tempFileInfo;
                        }

                    }
                }

                filePathAry = new string[fileInfoArray.Length];
                for(int fileCtr = 0;fileCtr< fileInfoArray.Length;fileCtr++)
                {
                    filePathAry[fileCtr] = fileInfoArray[fileCtr].FullName;
                }
                //if(!m_RelativeDirectoryPath.EndsWith(@"\"))
                //{
                //    m_RelativeDirectoryPath +=@"\";
                //}
	
                //filePathAry =
                //    Directory.GetFiles(m_RelativeDirectoryPath,m_FilePattern);
                //if(filePathAry==null)
                //    throw new LogReadException("Logs could not be read at : "+Environment.NewLine
                //        +m_RelativeDirectoryPath);				
                //Array.Reverse(filePathAry);

			}
			catch(Exception exp)
			{
				m_Log.Error(lgHeader,exp);
				throw exp;
			}			
			return filePathAry;
		}	
		private string GetLog(string logFilePath)
		{
			string logFileData = string.Empty;
			string lgHeader = "LogFileList::GetLog()";
			try
			{				
				using (FileStream strm = File.OpenRead(logFilePath))
				{
					StreamReader reader = new StreamReader(strm);
					logFileData = reader.ReadToEnd();
				}
			}
			catch(Exception exp)
			{
				m_Log.Error(lgHeader,exp);
				throw exp;
			}
			return logFileData ;				
		}
		private void PopulateConfigSettings()

		{
			Hashtable connInfHash = 
				(Hashtable)ConfigurationManager.GetSection("LogSettings");
			if(connInfHash==null)
				throw new NullReferenceException("Config file not vaild");
			IDictionaryEnumerator configEnum = connInfHash.GetEnumerator();
			while(configEnum.MoveNext())
			{
				switch(Convert.ToString(configEnum.Key).ToUpper())
				{
					case "LOGDIRECTORY":
						m_RelativeDirectoryPath = Convert.ToString(configEnum.Value);						
						break;
					case "LOGFILEPATTERN":
						m_FilePattern = Convert.ToString(configEnum.Value);						
						break;
				}
			}

		}

		#endregion [ Helper Function ]
		
	}		
}
