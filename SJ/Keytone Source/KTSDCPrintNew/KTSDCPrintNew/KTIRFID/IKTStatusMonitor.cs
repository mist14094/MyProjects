/********************************************************************************************************
Copyright (c) 2010 KeyTone Technologies.All Right Reserved

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
using System.Text;
using System.Collections.Generic;

namespace KTone.Core.KTIRFID
{
	/// <summary>
	/// Stores  EmailSettings for a periticular component
	/// </summary>
	[Serializable]
	public struct MonitorSetting
	{
		/// <summary>
		/// MonitorSettingId
		/// </summary>
		private string m_MonitorSettingId;

		/// <summary>
		/// ComponentCategory Agent/Filter/Reader
		/// </summary>
        private KTComponentCategory m_ComponentCategory;

        /// <summary>
        /// Enables Online state Monitoring
        /// </summary>
        private bool m_EnableOnlineMonitoring;

        /// <summary>
        /// Enables Offline state Monitoring
        /// </summary>
        private bool m_EnableOfflineMonitoring;

		/// <summary>
		/// Enables Idle state Monitoring
		/// </summary>
		private bool m_EnableIdleMonitoring;

		/// <summary>
		/// Enables Active state Monitoring
		/// </summary>
		private bool m_EnableActiveMonitoring;
	
		/// <summary>
		/// Email settings
		/// </summary>
		private EmailSetting  m_EmailSetting;

		/// <summary>
		/// Time stamp indicating when the last Email was sent
		/// </summary>
		private System.DateTime m_TimeLastEmailSent;

        

		/// <summary>
		/// Initializes a new instance of MonitorSetting
		/// </summary>
		/// <param name="monitorSettingId"></param>
		/// <param name="componentCategory"></param>
		/// <param name="enableActiveMonitoring"></param>
		/// <param name="enableIdleMonitoring"></param>
        /// <param name="enableOfflineMonitoring"></param>
        /// <param name="enableOnlineMonitoring"></param>
		/// <param name="emailSetting"></param>
        public MonitorSetting(string monitorSettingId, KTComponentCategory componentCategory,
            bool enableActiveMonitoring, bool enableIdleMonitoring, bool enableOnlineMonitoring,
            bool enableOfflineMonitoring, EmailSetting emailSetting)
		{
			m_MonitorSettingId = monitorSettingId;
			m_ComponentCategory = componentCategory;
			m_EnableActiveMonitoring = enableActiveMonitoring;
			m_EnableIdleMonitoring = enableIdleMonitoring;
            m_EnableOnlineMonitoring = enableOnlineMonitoring;
            m_EnableOfflineMonitoring = enableOfflineMonitoring;
			m_EmailSetting = emailSetting;
			m_TimeLastEmailSent = DateTime.MinValue;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="monitorSettingId"></param>
		public MonitorSetting(string monitorSettingId)
		{
			m_MonitorSettingId = monitorSettingId;
            m_ComponentCategory = KTComponentCategory.Agent;
			m_EnableActiveMonitoring = false;
			m_EnableIdleMonitoring = false;
            m_EnableOnlineMonitoring = false;
            m_EnableOfflineMonitoring = false;
			m_EmailSetting = new EmailSetting();
			m_TimeLastEmailSent = DateTime.MinValue;
		}

		#region Properties

		/// <summary>
		/// MonitorSettingId
		/// </summary>
		public string MonitorSettingId
		{
			get
			{
				return m_MonitorSettingId ;
			}
		}

		/// <summary>
		/// ComponentCategory
		/// </summary>
		public KTComponentCategory ComponentCategory
		{
			get
			{
				return m_ComponentCategory;
			}
		}

		/// <summary>
		/// EnableActiveMonitoring
		/// </summary>
		public bool EnableActiveMonitoring
		{
			get
			{
				return m_EnableActiveMonitoring;
			}
		}


		/// <summary>
		/// EnableIdleMonitoring
		/// </summary>
		public bool EnableIdleMonitoring
		{
			get
			{
				return m_EnableIdleMonitoring;
			}
		}

        /// <summary>
        /// Returns true if Online Monitoring is enabled
        /// </summary>
        public bool EnableOnlineMonitoring
        {
            get 
            {
                return m_EnableOnlineMonitoring;
            }
        }

        /// <summary>
        /// Returns true if Offline Monitoring is enabled
        /// </summary>
        public bool EnableOfflineMonitoring
        {
            get
            {
                return m_EnableOfflineMonitoring;
            }
        }
		/// <summary>
		/// EmailSetting
		/// </summary>
		public EmailSetting EmailSetting 
		{
			get
			{
				return m_EmailSetting ;
			}
			set
			{
				 m_EmailSetting  = value;
			}
		}

		/// <summary>
		/// TimeLastEmailSent 
		/// </summary>
		public DateTime TimeLastEmailSent
		{
			get
			{
				return m_TimeLastEmailSent;
			}
			set
			{
				m_TimeLastEmailSent = value;
			}

		}
		#endregion Properties

        /// <summary>
        /// Returns string representation of MonitorSetting object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("MonitorSettingId : " + m_MonitorSettingId);
                sb.Append("\r\n");

                sb.Append("ComponentCategory  : " + m_ComponentCategory.ToString());
                sb.Append("\r\n");

                sb.Append("Enable Idle state Monitoring : " + m_EnableIdleMonitoring);
                sb.Append("\r\n");

                sb.Append("Enable Active state Monitoring : " + m_EnableActiveMonitoring);
                sb.Append("\r\n");

                sb.Append("Enable Online state change Monitoring : " + m_EnableOnlineMonitoring);
                sb.Append("\r\n");

                sb.Append("Enable Offline state change Monitoring : " + m_EnableOfflineMonitoring);
                sb.Append("\r\n");

                sb.Append("Email settings : " + m_EmailSetting);
                sb.Append("\r\n");

                sb.Append(" Timestamp Last Email Sent : " + m_TimeLastEmailSent);
                sb.Append("\r\n");

                return sb.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
    }
	

	/// <summary>
	/// Stores  EmailSettings for a periticular component
	/// </summary>
	[Serializable]
	public struct EmailSetting
	{
		/// <summary>
		/// EMail Subject
		/// </summary>
		private string m_MailSubject;

		/// <summary>
		/// Email Sender 
		/// </summary>
		private string m_MailFrom;

		/// <summary>
		/// Email To
		/// </summary>
		private string m_MailTo;

		/// <summary>
		/// Email CC ,comma separated string of EmailIds
		/// </summary>
		private string m_MailCC;
		
		/// <summary>
		/// Email BCC ,comma separated string of EmailIds
		/// </summary>
		private string m_MailBCC;

		/// <summary>
		/// Emails Sent in a day
		/// </summary>
		private int m_EmailsSent;

		/// <summary>
		/// Maximum Emails that can be sent in a day
		/// </summary>
		private int m_EmailsPerDay;

        /// <summary>
        /// Mail header
        /// </summary>
        private string m_MailHeader;

        /// <summary>
        /// Mail Footer
        /// </summary>
        private string m_MailFooter;

		
		/// <summary>
		/// Initializes a new instance of EmailSetting
		/// </summary>
		/// <param name="mailSubject"></param>
		/// <param name="mailFrom"></param>
		/// <param name="mailTo"></param>
		/// <param name="mailCC"></param>
		/// <param name="mailBCC"></param>
		/// <param name="emailsPerDay"></param>
        /// <param name="mailHeader"></param>
        /// <param name="mailFooter"></param>
		public EmailSetting(string mailSubject,string mailFrom,string mailTo,string mailCC,string mailBCC,
            int emailsPerDay,string mailHeader, string mailFooter)
		{
			m_MailSubject = mailSubject;
			m_MailFrom = mailFrom;
			m_MailTo = mailTo;
			m_MailCC = mailCC;
			m_MailBCC = mailBCC;
			m_EmailsPerDay = emailsPerDay;
			m_EmailsSent = 0;
            m_MailHeader = mailHeader;
            m_MailFooter = mailFooter;
		}
		
		#region Properties
		/// <summary>
		/// EmailsSent
		/// </summary>
		public int EmailsSent
		{
			set
			{
				m_EmailsSent = value;

			}
			get 
			{
				return m_EmailsSent;
			}
			

		}

		/// <summary>
		/// EmailsPerDay 
		/// </summary>
		public int EmailsPerDay 
		{
			get 
			{
				return m_EmailsPerDay ;
			}
		}



		/// <summary>
		/// MailSubject
		/// </summary>
		public string MailSubject
		{
			get 
			{
				return m_MailSubject;
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
		/// MailCC
		/// </summary>
		public string MailCC
		{
			get
			{
				return m_MailCC;
			}
		}


		/// <summary>
		/// MailBCC
		/// </summary>
		public string MailBCC
		{
			get
			{
				return m_MailBCC;
			}
		}

        /// <summary>
        /// Mail header
        /// </summary>
        public string MailHeader
        {
            get 
            {
                return m_MailHeader;
            }
        }

        /// <summary>
        /// Mail Footer
        /// </summary>
        public string MailFooter
        {
            get
            {
                return m_MailFooter;
            }
        }

		#endregion Properties

        /// <summary>
        /// Returns string representation of EmailSetting object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("EMail Subject :" + m_MailSubject);
                sb.Append("\r\n");

                sb.Append("Email Sender :" + m_MailFrom);
                sb.Append("\r\n");

                sb.Append("Email To :" + m_MailTo);
                sb.Append("\r\n");

                sb.Append("Email CC :" + m_MailCC);
                sb.Append("\r\n");

                sb.Append("Email BCC :" + m_MailBCC);
                sb.Append("\r\n");

                sb.Append("Emails Sent in a day:" + m_EmailsSent);
                sb.Append("\r\n");

                sb.Append("Maximum Emails that can be sent in a day:" + m_EmailsPerDay);
                sb.Append("\r\n");

                sb.Append("Mail header:" + m_MailHeader);
                sb.Append("\r\n");

                sb.Append("Mail footer:" + m_MailFooter);
                sb.Append("\r\n");

                return sb.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
	}
    [Serializable]
    public struct AlertMonitorSettings
    {
        private string ruleID;
        private string violationRuleTypeName;
        private string violationRuleName;
        private string violationRuleDescription;
        EmailSetting emailSetting;
        DateTime timeLastEmailSent;

        public AlertMonitorSettings(string ruleID, string violationRuleTypeName, string violationRuleName,
            string violationRuleDescription, EmailSetting emailSetting)
        {
            this.ruleID = ruleID;
            this.violationRuleTypeName = violationRuleTypeName;
            this.violationRuleName = violationRuleName;
            this.violationRuleDescription = violationRuleDescription;
            this.emailSetting = emailSetting;
            this.timeLastEmailSent = DateTime.Now;
        }

        public override string ToString()
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("RuleID :" + ruleID);
                sb.Append("\r\n");

                sb.Append("ViolationRuleTypeName :" + violationRuleTypeName);
                sb.Append("\r\n");

                sb.Append("ViolationRuleName :" + violationRuleName);
                sb.Append("\r\n");

                sb.Append("ViolationRuleDescription :" + violationRuleDescription);
                sb.Append("\r\n");

                sb.Append(this.emailSetting.ToString());

                return sb.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        
        #region Properties

        public string RuleID
        {
            get 
            {
                return this.ruleID;
            }
        }

        public string ViolationRuleTypeName
        {
            get
            {
                return this.violationRuleTypeName;
            }
        }
        public string ViolationRuleName
        {
            get
            {
                return this.violationRuleName;
            }
        }

        public string ViolationRuleDescription
        {
            get 
            {
                return this.violationRuleDescription;
            }
        }

		/// <summary>
		/// TimeLastEmailSent 
		/// </summary>
		public DateTime TimeLastEmailSent
		{
			get
			{
				return timeLastEmailSent;
			}
			set
			{
				timeLastEmailSent = value;
			}

		}

        /// <summary>
        /// EmailSetting
        /// </summary>
        public EmailSetting EmailSetting
        {
            get
            {
                return this.emailSetting;
            }
            set
            {
                this.emailSetting = value;
            }
        }

		#endregion Properties
    }
	
	/// <summary>
	/// Summary description for IStatusMonitor.
	/// </summary>
	public interface IKTStatusMonitor
	{
        /// <summary>
        /// This event is fired when a component is added to / removed from KTRFService.
        /// </summary>
        event EventHandler<KTFactoryActionEventArgs> ComponentActionEvent;
        /// <summary>
        /// Event fired when a mail is sent
        /// </summary>
        event EventHandler<OnMonitorMailSentEventArgs> OnMailSent;
        /// <summary>
		/// Returns factory connection status 
		/// </summary>
		/// <returns></returns>
		bool IsConnectedtoFactory();

		/// <summary>
		/// Synchronizes the Factory components with Status monitor
		/// Newly added components in the factory will have the Generic settings.
		/// </summary>
		void Synchronize();

		/// <summary>
		/// Returns array of all MonitorSetting objects of given  ComponentCategory
		/// </summary>
		/// <param name="compCategory"></param>
        MonitorSetting[] GetAllMonitorSettings(KTComponentCategory compCategory);

		/// <summary>
		/// Returns array of all ComponentIds in  StatusMonitor.
		/// </summary>
		/// <returns></returns>
		string[] GetAllComponentIds();

        /// <summary>
        /// Returns array of all ComponentIds in  StatusMonitor depending on ComponentCategory.
        /// </summary>
        /// <returns></returns>
        List<string> GetAllSpecificComponentIds(KTComponentCategory componentCategory);

		/// <summary>
		/// Returns array of ComponentIds with generic Monitor Settings.
		/// </summary>
		/// <returns></returns>
		string[] GetCompIdsWithGenericSettings();

		
		/// <summary>
		///  Update ComponentSettings for a given component
		///  Generic monitorSettings are updated,
		///  Component specific monitorSettings if already exist modified else new specific setting is added
		/// </summary>
		/// <param name="monitorSetting"></param>
		void UpdateSettings(MonitorSetting monitorSetting);

        /// <summary>
        /// Resets the ComponentSettings.
        /// </summary>
        /// <param name="monitorSetting"></param>
        void ResetSettings(MonitorSetting monitorSetting);

		/// <summary>
		/// Apply GenericSettings to the given component
		/// </summary>
		/// <param name="compCategory"></param>
		/// <param name="componentID"></param>
        void ApplyGenericSettings(KTComponentCategory compCategory, string componentID);

		/// <summary>
		/// Returns Start time and end time in which Emails are to be delivered
		/// </summary>
		/// <param name="startTime"></param>
		/// <param name="endTime"></param>
		void GetEmailScheduleOfDay(out DateTime startTime,out DateTime endTime);
        /// <summary>
        /// Test Mail.
        /// </summary>
        /// <returns>Boolean value indicating Test Mail succeeded or not</returns>
        // To Do..bool TestMail();
		/// <summary>
		/// Returns WeeklyOff Days set in StatusMonitor
		/// </summary>
		/// <returns></returns>
		string[] GetWeeklyOffDays();		
		/// <summary>
		/// Returns list of Holidays set in StatusMonitor
		/// </summary>
		/// <returns></returns>
		DateTime[] GetHolidays();			
		/// <summary>
		/// SMTP Server Name
		/// </summary>
		string SMTPServerName {get;}
        /// <summary>
        /// SMTP Server Port
        /// </summary>
        int SMTPServerPort { get;}
		/// <summary>
		/// Enables Email for Filter 
		/// </summary>
		bool EmailForFilter{get;}
		/// <summary>
		/// Enables Email for Device 
		/// </summary>
		bool EmailForDevice{get;}
		/// <summary>
		/// Enables Email for Application 
		/// </summary>
		bool EmailForApplication{get;}
        /// <summary>
        /// Start Time for sending Email.
        /// </summary>
        DateTime EmailStartTime { get;}
        /// <summary>
        /// End Time for sending Email.
        /// </summary>
        DateTime EmailEndTime { get;}
		/// <summary>
        /// Mail footer address
        /// </summary>
        string MailFooterAddress { get;} 
		/// Sets General Settings for Email schedule.
		/// </summary>
		/// <param name="smtpName">SMTP SERVER NAME</param>		
        /// <param name="monitoringIntervalMS"></param>
		/// <param name="emailStartTime">EMAIL START TIME</param>
		/// <param name="emailEndTime">EMAIL END TIME</param>
		/// <param name="sendEmailForDevice">Enable\Diable Emails for Device</param>
		/// <param name="sendEmailForFilter">Enable\Diable Emails for Filter</param>
		/// <param name="sendEmailForApplication">Enable\Diable Emails for Application</param>
		/// <param name="weeklyOFFs">List of weelky offs</param>
        /// <param name="configurableEMailFooterAddress">Allow configurable EMail address</param>
		void SetGeneralSettings(string smtpName,int monitoringIntervalMS,DateTime emailStartTime,DateTime emailEndTime,
			bool sendEmailForDevice,bool sendEmailForFilter,bool sendEmailForApplication,
            string[] weeklyOFFs, DateTime[] holidays, bool startRFService, int smtpPort,string mailFooterAddress,
            int offlineTimeLimitMinutes,string userName,string password);

        /// <summary>
        /// Fills the arrays with Components.
        /// </summary>
        void FillComponentList();
        /// <summary>
        /// TestMail
        /// </summary>
        /// <returns>Returns boolean value true,if Test Mail suceeded,else returns false</returns>
        bool TestEmail(EmailSetting testMailSettings,string testSubject,string testMailMsg);

        /// <summary>
        /// Time interval in Milliseconds to get component state notification from factory
        /// </summary>
        int MonitoringIntervalMS
        {
            get;
        }
     
        /// <summary>
        /// When StatusMonitor receives component offline message, it will not send the mail immediately.It will wait for some
        /// time for the online event.If it receives offline and then online, both the mails will not be sent, assuming that 
        /// it is a temporary state change.
        /// </summary>
        int OfflineStateDelayMinutes
        {
            get;
        }
        /// <summary>
        /// Get the UserName for SMTP server for client authentication
        /// </summary>
        string SMTPServerUserName
        {
            get;
        }
        /// <summary>
        /// Get the Password for SMTP server for client authentication
        /// </summary>
        string SMTPServerPassword
        {
            get;
        }

        /// <summary>
        /// Database connection string
        /// </summary>
        string DbConnectionString
        {
            get;
        }
    }

}

