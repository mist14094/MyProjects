using System;
using System.Collections.Generic;
using System.Text;

namespace KTone.Core.KTIRFID
{
    /// <summary>
    /// 
    /// </summary>
    public interface IKTMail
    {
        #region Mail

        /// <summary>
        /// Sends mail from sender to recipient.
        /// </summary>
        /// <param name="sender">email address from whom mail is sent.</param>
        /// <param name="senderPassword">password for sender's account.</param>
        /// <param name="recipient">To who the mail has to be sent.</param>
        /// <param name="subject">Subject of mail</param>
        /// <param name="message">Text body of the mail</param>
        void SendMail(string sender, string senderPassword, string recipient, string subject, string message);

        /// <summary>
        /// Using the sender user account details specified in mail setting, 
        /// sends mail to the first recipient in mail setting,and waits for acknowledgement from the recipient.
        /// If no acknowledgement is received with time interval 'waitTimeInMinutes',resend mail to the next member in recipient list.
        /// This will continue till any acknowledgement is reaceived.
        /// If no acknowledgment is received even after all recipients have received the mail,
        /// then this pending mail will be stored as unacknowledged mail,and no further mails will be sent for that mailsetting.
        /// </summary>
        /// <param name="mailSettingID">Id of the mail setting to use</param>
        /// <param name="subject">Subject of mail</param>
        /// <param name="message">Message/Text body of the mail</param>
        /// <param name="waitTimeInMinutes">Time interval to wait for acknowledgement 
        /// before sending message to the next recipient</param>
        string SendMailForMailSetting(int mailSettingID, string subject, string message, int waitTimeInMinutes);


        /// <summary>
        /// Gets all new received mails from the server(SMTP Server specified),
        /// for the user account specified by senderUserName.
        /// </summary>
        /// <param name="senderUserName"></param>
        /// <param name="senderPassword"></param>
        /// <returns></returns>
        KTMailMessage[] CheckMails(string senderUserName, string senderPassword);

        ///// <summary>
        ///// Deletes all mails on the server, for the user account 'address'
        ///// </summary>
        ///// <param name="address"></param>
        ///// <param name="password"></param>
        //void ClearMailBox(string address, string password);

        ///// <summary>
        ///// Starts receive thread for a particular mail setting id.
        ///// Receive thread will check for new mail on the server  after specific time interval,
        ///// and if a mail is  received, notifies to the system that a new mail has arrived.
        ///// </summary>
        ///// <param name="mailSettingID"></param>
        //void StartReceivingMails(int mailSettingID);

        ///// <summary>
        ///// Stops receive thread.
        ///// </summary>
        ///// <param name="mailSettingID"></param>
        //void StopReceivingMails(int mailSettingID);

        #endregion Mail

        #region MailSettings

        /// <summary>
        /// Adds a new mail setting.
        /// </summary>
        /// <param name="senderUserName"></param>
        /// <param name="senderPassword"></param>
        /// <param name="recipientList"></param>
        /// <param name="CheckMailIntrvlInMin"></param>
        /// <returns></returns>
        int AddMailSetting(string senderUserName, string senderPassword, SortedList<int, Recipient> recipientList, int CheckMailIntrvlInMin);

        /// <summary>
        /// Removes mail setting from the list of mail settings.
        /// </summary>
        /// <param name="MailSettingID"></param>
        void RemoveMailSetting(int MailSettingID);

        /// <summary>
        /// Get list of all mail settings that have been defined.
        /// </summary>
        /// <returns></returns>
        Dictionary<int, KTMailSettingsDetails> GetMailSettingsList();

        #endregion MailSettings

        #region KTPendingMessageDetails
        /// <summary>
        /// Gets a list of all messages that have been sent, but no acknowledgement received.
        /// </summary>
        /// <returns></returns>
        Dictionary<string, KTPendingMessageDetails> GetPendingMessagesList();

        /// <summary>
        /// Get list of all messages for which mails have been sent to all recipients in the list,
        /// and the wait timer has expired, but no acknowledgement was received from any recipient.
        /// </summary>
        /// <returns></returns>
        KTPendingMessageDetails[] GetUnansweredMessagesList();

        /// <summary>
        /// Stop waiting for acknowledgment for the sent mail
        /// that has messageID as specified.
        /// The message will be removed from pending messages list.
        /// </summary>
        /// <param name="mainMessageID"></param>
        void AbortPendingMessage(string mainMessageID, bool logAsUnanswered);
        #endregion KTPendingMessageDetails

        #region Properties
        /// <summary>
        /// Name of SMTP server to use to send/receive mails
        /// </summary>
        string SMTPServerName
        {

            get;
            set;
        }
        #endregion Properties
    }

    /// <summary>
    /// Specifies the mode by which 
    /// message will be sent.i.e, EMail or Pager
    /// </summary>
    public enum SendMode
    {
        /// <summary>
        /// Send message as EMail
        /// </summary>
        Email,

        /// <summary>
        /// Send message as pager message
        /// </summary>
        Pager
    }

    /// <summary>
    /// Details about the recipient to whom mail has been sent
    /// </summary>

    [Serializable]
    public class RecipientDetails
    {
        private int recipientID;
        private string messageID;
        private string timeStamp;

        #region Constructor

        /// <summary>
        /// Initializes SentList details 
        /// </summary>
        /// <param name="sentToID"></param>
        /// <param name="messageID"></param>
        /// <param name="timeStamp"></param>
        public RecipientDetails(int recipientID, string messageID, string timeStamp)
        {
            this.recipientID = recipientID;
            this.messageID = messageID;
            this.timeStamp = timeStamp;
        }

        public override string ToString()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("RecipientID".PadRight(40) + ":" + recipientID);
                sb.AppendLine("MessageID".PadRight(40) + ":" + messageID);
                sb.AppendLine("TimeStamp".PadRight(40) + ":" + timeStamp);
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new KTComponentException("RecipientDetails",
                    "Error in reading RecipientDetails:" + ex.Message);
            }
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Id of the recipient as per MailSettings
        /// </summary>
        public int RecipientID
        {
            get { return this.recipientID; }
        }

        /// <summary>
        ///Unique id associated with each mail that has been sent.
        /// </summary>
        public string MessageID
        {
            get { return this.messageID; }
        }

        /// <summary>
        /// Time at which the mail was sent
        /// </summary>
        public string TimeStamp
        {
            get { return this.timeStamp; }
        }
        #endregion Properties
    }

    /// <summary>
    /// Details about the recipients to send mails 
    /// A list of recipients is associated with a MailSetting 
    /// </summary>
    [Serializable]
    public class Recipient
    {
        private int recipientID;
        private string address;
        private SendMode sendMode;

        #region Constructor
        /// <summary>
        /// Initializes new recipient
        /// </summary>
        /// <param name="recipientIndex"></param>
        /// <param name="address"></param>
        /// <param name="sendMode"></param>
        public Recipient(int recipientIndex, string address, SendMode sendMode)
        {
            this.recipientID = recipientIndex;
            this.address = address;
            this.sendMode = sendMode;
        }
        #endregion Constructor

        public override string ToString()
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("RecipientID".PadRight(40) + ":" + recipientID.ToString());

                sb.AppendLine("Address".PadRight(40) + ":" + address.ToString());
                sb.AppendLine("SendMode".PadRight(40) + ":" + sendMode.ToString());

                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new KTComponentException("Recipient",
                    "Error in ToString():" + ex.Message);
            }
        }

        #region Properties

        /// <summary>
        /// Unique id for a recipient in  MailSetting
        /// </summary>
        public int RecipientID
        {
            get { return recipientID; }
        }

        /// <summary>
        /// Address to which mail has to be sent
        /// </summary>
        public string Address
        {
            get { return address; }
        }

        /// <summary>
        /// Mode by which mail should be sent
        /// </summary>
        public SendMode SendMode
        {
            get { return sendMode; }
        }

        #endregion Properties
    }

    /// <summary>
    /// MailSettings contain details about sender and 
    /// recipients to whom mail should be sent.
    /// </summary>
    [Serializable]
    public class KTMailSettingsDetails
    {
        #region Attributes

        private int mailSettingID;
        private string senderUserName;
        private string senderPassword;
        private SortedList<int, Recipient> recipientList;
        private int checkMailIntrvlInMin = 0;

        #endregion Attributes

        /// <summary>
        /// Initializes mail settings
        /// </summary>
        /// <param name="mailSettingID"></param>
        /// <param name="senderUserName"></param>
        /// <param name="senderPassword"></param>
        /// <param name="recipientList"></param>
        /// <param name="checkMailIntrvlInMin"></param>
        public KTMailSettingsDetails(int mailSettingID, string senderUserName, string senderPassword,
                                SortedList<int, Recipient> recipientList, int checkMailIntrvlInMin)
        {
            this.mailSettingID = mailSettingID;
            this.senderUserName = senderUserName;
            this.senderPassword = senderPassword;
            this.recipientList = recipientList;
            this.checkMailIntrvlInMin = checkMailIntrvlInMin;
        }



        public override string ToString()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("MailSettingID".PadRight(40) + ":" + this.mailSettingID);
                sb.AppendLine("SenderUserName".PadRight(40) + ":" + this.senderUserName);
                sb.AppendLine("RecipientList".PadRight(40));

                foreach (Recipient rec in recipientList.Values)
                {
                    sb.Append(rec.ToString());
                    sb.AppendLine();
                }
                sb.AppendLine("CheckMailInterval(Minutes)".PadRight(40) + ":" + this.checkMailIntrvlInMin);
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new KTComponentException("KTMailSettingsDetails",
                    "Error in reading KTMailSettingsDetails:" + ex.Message);
            }
        }

        #region Properties

        /// <summary>
        /// Mail Component contains multiple mailSettings.
        /// MailSettingId is unique,and is used to identify a particular mail setting.
        /// </summary>
        public int MailSettingID
        {
            get { return mailSettingID; }
            //set { this.mailSettingID = value; }
        }

        /// <summary>
        /// SenderUserName is user account from whom mail will be sent.
        /// </summary>
        public string SenderUserName
        {
            get { return senderUserName; }
        }

        /// <summary>
        /// Password for the user account
        /// </summary>
        public string SenderPassword
        {
            get { return senderPassword; }
        }

        /// <summary>
        /// List of recipients to whom mail should be sent 
        /// when the particular mail setting is selected.
        /// </summary>
        public SortedList<int, Recipient> RecipientList
        {
            get { return recipientList; }
        }

        /// <summary>
        /// Time interval (in minutes) to wait for before checking for new mails on server.
        /// </summary>
        public int CheckMailIntrvlInMin
        {
            get { return checkMailIntrvlInMin; }
        }

        #endregion Properties
    }


    /// <summary>
    /// When mail is sent using mailsetting, the component waits for an acknowledgement from recipient.
    /// Details about the mail for which acknowledgement is not received is KTPendingMessageDetails.
    /// </summary>
    [Serializable]
    public class KTPendingMessageDetails
    {
        #region Attributes

        private string mainMsgId;
        private string subject;
        private string message;
        private int waitTimeInMinutes;
        private int mailSettingID;
        private SortedList<int, RecipientDetails> recipientDetailsList;

        #endregion Attributes

        /// <summary>
        /// Initializes KTPendingMessageDetails
        /// </summary>
        /// <param name="mainMsgId"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <param name="recipients"></param>
        /// <param name="waitTimeInMinutes"></param>
        /// <param name="mailSettingID"></param>
        public KTPendingMessageDetails(string mainMsgId, string subject, string message,
                        SortedList<int, RecipientDetails> recipientDetailsList, int waitTimeInMinutes, int mailSettingID)
        {
            try
            {
                this.mainMsgId = mainMsgId;
                this.subject = subject;
                this.message = message;
                this.recipientDetailsList = recipientDetailsList;
                this.waitTimeInMinutes = waitTimeInMinutes;
                this.mailSettingID = mailSettingID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Properties
        /// <summary>
        /// Message id of the first mail that was sent for the pending message
        /// </summary>
        public string MainMessageID
        {
            get { return this.mainMsgId; }
        }

        /// <summary>
        /// Subject of the mail
        /// </summary>
        public string Subject
        {
            get { return this.subject; }
        }

        /// <summary>
        /// Text Body of the mail
        /// </summary>
        public string Message
        {
            get { return this.message; }
        }

        /// <summary>
        /// Time interval in minutes to wait for before resending mail
        /// </summary>
        public int WaitTimeInMinutes
        {
            get { return this.waitTimeInMinutes; }
        }

        /// <summary>
        /// List of recipients to whom mail has been sent.
        /// </summary>
        public SortedList<int, RecipientDetails> RecipientDetailsList
        {
            get { return this.recipientDetailsList; }
        }

        /// <summary>
        /// Id of the mail setting associated with the pending message.
        /// Mail setting specifies sender and recipients list for the pending message
        /// </summary>
        public int MailSettingID
        {
            get { return this.mailSettingID; }
        }

        #endregion Properties

        public override string ToString()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("MainMessageID".PadRight(40) + ":" + this.mainMsgId);
                sb.AppendLine("Subject".PadRight(40) + ":" + this.subject);
                sb.AppendLine("Message".PadRight(40) + ":" + this.message);
                sb.AppendLine("WaitTimeinMinutes:".PadRight(40) + ":" + this.waitTimeInMinutes);
                sb.AppendLine("MailSettingID".PadRight(40) + ":" + this.mailSettingID);

                sb.AppendLine("RecipientList".PadRight(40));

                foreach (RecipientDetails rec in recipientDetailsList.Values)
                {
                    sb.Append(rec.ToString());
                    sb.AppendLine();
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new KTComponentException("KTPendingMessageDetails",
                    "Error in reading KTPendingMessageDetails:" + ex.Message);
            }

        }
    }

    /// <summary>
    /// Contains description of the  mail message that was sent.
    /// </summary>
    [Serializable]
    public class KTMailMessage
    {
        private string from = string.Empty;
        private string to = string.Empty;
        private string subject = string.Empty;
        private string textBody = string.Empty;
        private string replyTo = string.Empty;

        private List<string> cc = new List<string>();

        private Dictionary<string, string> headers = new Dictionary<string, string>();

        #region Constructor
        /// <summary>
        /// Initializes mail message.
        /// </summary>
        /// <param name="sentFrom"></param>
        /// <param name="sendTo"></param>
        /// <param name="subject"></param>
        /// <param name="textBody"></param>
        public KTMailMessage(string sentFrom, string sendTo, string subject, string textBody)
        {
            this.subject = subject;
            this.textBody = textBody;
            this.to = sendTo;
            this.from = sentFrom;
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// List of all headers associated with the mail message.
        /// </summary>
        public Dictionary<string, string> Headers
        {
            get { return headers; }
        }

        /// <summary>
        /// Subject of the mail
        /// </summary>
        public string Subject
        {
            get { return subject; }
        }

        /// <summary>
        /// Text body or message contained in the mail
        /// </summary>
        public string TextBody
        {
            get { return textBody; }
        }

        /// <summary>
        /// Recipient address to whom mail has to be sent
        /// </summary>
        public string SendTo
        {
            get { return to; }
        }

        /// <summary>
        /// Sender's address 
        /// </summary>
        public string SentFrom
        {
            get { return from; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ReplyTo
        {
            get { return replyTo; }
            set { replyTo = value; }
        }

        /// <summary>
        /// List of recipient address to whom mail should be sent as Cc.
        /// </summary>
        public List<string> CC
        {
            get { return cc; }
        }
        #endregion Properties

        public override string ToString()
        {
            try
            {

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("To".PadRight(20) + ":" + to);
                sb.AppendLine("From".PadRight(20) + ":" + from);
                sb.AppendLine("Subject".PadRight(20) + ":" + subject);
                sb.AppendLine("TextBody".PadRight(20) + ":" + textBody);
                sb.AppendLine("Headers:");
                foreach (KeyValuePair<string, string> de in headers)
                {
                    sb.AppendLine(de.Key.ToString().PadRight(20) + ":" + de.Value.ToString());
                }

                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new KTComponentException("KTMailMessage",
               "Error in reading KTMailMessage details:" + ex.Message);
            }
        }
    }


}
