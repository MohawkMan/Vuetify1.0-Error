using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hangfire;
using VBL.Data;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;

namespace VBL.Core
{
    public partial class MessageSender : IEmailSender
    {
        private readonly SmtpOptions _smtpOptions;
        private readonly SmtpSecrets _smtpSecrets;
        private readonly VBLDbContext _db;
        private readonly ILogger _logger;
        
        public MessageSender(IOptions<SmtpOptions> smtpOptions, IOptions<SmtpSecrets> smtpSecrets, VBLDbContext db, ILogger<MessageSender> logger)
        {
            _smtpOptions = smtpOptions.Value;
            _smtpSecrets = smtpSecrets.Value;
            _db = db;
            _logger = logger;
        }

        public void SendEmail(int emailMessageId)
        {
            var email = _db.EmailMessages.Find(emailMessageId);
            var m = CreateEmail(email.To, email.From, email.CC, email.BCC, email.Subject, email.PlainTextMessage, email.HtmlMessage, email.ReplyTo);
            Send(m);
            email.From = m.From.ToString();
            email.DTSent = DateTime.UtcNow;
            _db.SaveChanges();
        }

        private void Send(MimeMessage message)
        {
            using (var client = new SmtpClient())
            {
                client.Connect(_smtpOptions.Host, _smtpOptions.Port, _smtpOptions.UseSsl);

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                
                // Note: only needed if the SMTP server requires authentication
                if (_smtpOptions.RequiresAuthentication)
                {
                    client.Authenticate(_smtpOptions.Username, _smtpSecrets.SmtpPassword);
                }
                client.Send(message);
                client.Disconnect(true);
            }
        }
        private MimeMessage CreateEmail(string to, string from, string cc, string bcc, string subject, string plainTextMessage, string htmlMessage, string replyTo = null)
        {
            if (string.IsNullOrWhiteSpace(to))
            {
                throw new ArgumentException("no to address provided");
            }

            if (string.IsNullOrWhiteSpace(subject))
            {
                throw new ArgumentException("no subject provided");
            }

            var hasPlainText = !string.IsNullOrWhiteSpace(plainTextMessage);
            var hasHtml = !string.IsNullOrWhiteSpace(htmlMessage);
            if (!hasPlainText && !hasHtml)
            {
                throw new ArgumentException("no message provided");
            }

            var m = new MimeMessage();

            #region From
            var fromAddress = new MailboxAddress(_smtpOptions.DefaultFromName, _smtpOptions.DefaultFromAddress);
            if (!string.IsNullOrEmpty(from))
            {
                if (from.Contains(","))
                {
                    var fSplit = from.Split(',');
                    fromAddress.Name = fSplit[0];
                    fromAddress.Address = fSplit[1];
                }
                else
                {
                    fromAddress.Name = "";
                    fromAddress.Address = from;
                }
            }
            m.From.Add(fromAddress);
            #endregion

            #region Reply To
            if (!string.IsNullOrWhiteSpace(replyTo))
            {
                m.ReplyTo.Add(new MailboxAddress("", replyTo));
            }
            #endregion

            #region To
            var toAddresses = to.Split(';');
            foreach (var t in toAddresses)
            {
                if (t.Contains(","))
                {
                    var tSplit = t.Split(',');
                    m.To.Add(new MailboxAddress(tSplit[0], tSplit[1]));
                }
                else
                {
                    m.To.Add(new MailboxAddress("", t));
                }
            }

            if (!string.IsNullOrEmpty(to))
            {
            }
            #endregion

            #region Cc
            if (!string.IsNullOrWhiteSpace(cc))
            {
                var ccAddresses = cc.Split(';');
                foreach (var c in ccAddresses)
                {
                    if (c.Contains(","))
                    {
                        var cSplit = c.Split(',');
                        m.Cc.Add(new MailboxAddress(cSplit[0], cSplit[1]));
                    }
                    else
                    {
                        m.Cc.Add(new MailboxAddress("", c));
                    }
                }
            }
            #endregion

            #region Bcc
            if (!string.IsNullOrWhiteSpace(bcc))
            {
                var bccAddresses = bcc.Split(';');
                foreach (var b in bccAddresses)
                {
                    if (b.Contains(","))
                    {
                        var bSplit = b.Split(',');
                        m.Bcc.Add(new MailboxAddress(bSplit[0], bSplit[1]));
                    }
                    else
                    {
                        m.Bcc.Add(new MailboxAddress("", b));
                    }
                }
            }
            #endregion

            m.Subject = subject;

            BodyBuilder bodyBuilder = new BodyBuilder();
            if (hasPlainText)
            {
                bodyBuilder.TextBody = plainTextMessage;
            }

            if (hasHtml)
            {
                bodyBuilder.HtmlBody = htmlMessage;
            }

            m.Body = bodyBuilder.ToMessageBody();
            return m;
        }

    }
}
