using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace Fourth.SqlParser.Infrastructure.Infrastructure
{
    public class EmailSender
    {
        private readonly string _mailFrom;
        private readonly string _mailSmtpHost;
        private readonly string _mailSmtpPassword;
        private readonly int _mailSmtpPort;
        private readonly string _mailSmtpUsername;

        public EmailSender(string mailSmtpHost, int mailSmtpPort, string mailSmtpUsername, string mailSmtpPassword,
            string mailFrom)
        {
            _mailSmtpHost = mailSmtpHost;
            _mailSmtpPort = mailSmtpPort;
            _mailSmtpUsername = mailSmtpUsername;
            _mailSmtpPassword = mailSmtpPassword;
            _mailFrom = mailFrom;
        }

        public bool SendEmail(string to, string subject, string body)
        {
            var mail = new MailMessage(_mailFrom, to, subject, body);
            AlternateView alternameView = AlternateView.CreateAlternateViewFromString(body, new ContentType("text/html"));
            mail.AlternateViews.Add(alternameView);

            var smtpClient = new SmtpClient(_mailSmtpHost, _mailSmtpPort)
            {
                Credentials = new NetworkCredential(_mailSmtpUsername, _mailSmtpPassword),
                EnableSsl = true
            };

            smtpClient.Send(mail);
            return true;
        }


        public bool SendEmail(string to, string subject, string body, string attachmentLocation)
        {
            var mail = new MailMessage(_mailFrom, to, subject, body);
            var attachment = new Attachment(attachmentLocation);
            mail.Attachments.Add(attachment);
            AlternateView alternameView = AlternateView.CreateAlternateViewFromString(body, new ContentType("text/html"));
            mail.AlternateViews.Add(alternameView);

            var smtpClient = new SmtpClient(_mailSmtpHost, _mailSmtpPort)
            {
                Credentials = new NetworkCredential(_mailSmtpUsername, _mailSmtpPassword),
                EnableSsl = true
            };

            smtpClient.Send(mail);
            return true;
        }
    }
}