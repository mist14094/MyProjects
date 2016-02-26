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
using System.Net.Mail;

namespace KTone.RFIDGlobal
{
	/// <summary>
	/// Summary description for SendMail.
	/// </summary>
	public class SendMail
	{
		private string m_Server = "127.0.0.1";
		private string m_User = string.Empty;
		private string m_PWD = string.Empty;


		private MailMessage m_message = null;

		public SendMail()
		{
			//
			// TODO: Add constructor logic here
			//
			m_message = new MailMessage();
		
		}

		#region Methods

		public void Send(string msgFrom, string msgTo, string msgBcc, string msgSubj, string msgBody)
		{
			try
			{
                m_message.From = new MailAddress(msgFrom);
				m_message.To.Add(msgTo);
                if(msgBcc!="")
				m_message.Bcc.Add(msgBcc);
				m_message.Subject = msgSubj;
				m_message.Body = msgBody;
                SmtpClient client = new SmtpClient(m_Server);

                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = true;

				client.Send(m_message);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
        public void Send(string msgFrom, string msgTo, string msgBcc, string msgCC
            , string msgSubj, string msgBody)
        {
            try
            {
                m_message.From = new MailAddress(msgFrom);
                m_message.To.Add(msgTo);
                if (msgCC != "")
                    m_message.CC.Add(msgCC);
                if (msgBcc != "")
                    m_message.Bcc.Add(msgBcc);
                m_message.Subject = msgSubj;
                m_message.Body = msgBody;

                SmtpClient client = new SmtpClient(m_Server);

                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = true;

                client.Send(m_message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
		public void AddAttachment(string attachmentPath)
		{
			try
			{
                if (attachmentPath == "")
                    return;
				Attachment attachment = new Attachment(attachmentPath);
				m_message.Attachments.Add(attachment);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		#endregion


		#region "Properties"
		public string SMTPServer
		{
			set
			{m_Server = value;}
		}

		public string SMTPUser
		{
			set{m_User = value;}
		}

		public string SMTPPassword
		{
			set{m_PWD=value;}
		}
	}

	#endregion
}
