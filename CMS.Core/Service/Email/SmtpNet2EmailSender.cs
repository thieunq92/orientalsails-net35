using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace CMS.Core.Service.Email
{
    /// <summary>
    /// Implements IEmailSender using the System.Net.Email classes from the .NET 2.0 framework.
    /// </summary>
    public class SmtpNet2EmailSender : IEmailSender
    {
        private readonly string _host;
        private Encoding _encoding;
        private int _port;
        private string _smtpPassword;
        private string _smtpUsername;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="host">SMTP hostname is required for this service to work.</param>
        public SmtpNet2EmailSender(string host)
        {
            _host = host;
            _port = 25;
            _encoding = Encoding.Default;
        }

        /// <summary>
        /// SMTP port (default 25).
        /// </summary>
        public int Port
        {
            set { _port = value; }
        }

        /// <summary>
        /// SMTP Username
        /// </summary>
        public string SmtpUsername
        {
            set { _smtpUsername = value; }
        }

        /// <summary>
        /// SMTP Password
        /// </summary>
        public string SmtpPassword
        {
            set { _smtpPassword = value; }
        }

        /// <summary>
        /// Email body encoding
        /// </summary>
        public string EmailEncoding
        {
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _encoding = Encoding.GetEncoding(value);
                }
            }
        }

        #region IEmailSender Members

        public void Send(string from, string to, string subject, string body)
        {
            Send(from, to, subject, body, null, null);
        }

        public void Send(string from, string to, string subject, string body, string[] cc, string[] bcc)
        {
            // Create mail message
            MailMessage message = new MailMessage(from, to, subject, body);
            message.BodyEncoding = _encoding;

            if (cc != null && cc.Length > 0)
            {
                foreach (string ccAddress in cc)
                {
                    message.CC.Add(new MailAddress(ccAddress));
                }
            }
            if (bcc != null && bcc.Length > 0)
            {
                foreach (string bccAddress in bcc)
                {
                    message.Bcc.Add(new MailAddress(bccAddress));
                }
            }

            // Send email
            SmtpClient client = new SmtpClient(_host, _port);
            if (!String.IsNullOrEmpty(_smtpUsername) && !String.IsNullOrEmpty(_smtpPassword))
            {
                client.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);
            }
            client.Send(message);
        }

        public void Send(MailMessage message)
        {
            // Send email
            SmtpClient client = new SmtpClient(_host, _port);
            if (!String.IsNullOrEmpty(_smtpUsername) && !String.IsNullOrEmpty(_smtpPassword))
            {
                client.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);
            }
            client.Send(message);
        }

        #endregion
    }
}