using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailCrypter.Model.Entitties;
using System.Net.Mail;
using System.Net;

namespace MailCrypter.Model.Logics
{
    public class MailSender : IMailSender, IDisposable
    {
        protected SmtpClient smtpClient;

        public MailSender(string credentialEmail,
                            string password,
                            string host,
                            int port,
                            bool enableSsl,
                            int timeout)
        {
            smtpClient = new SmtpClient()
            {
                Port = port,
                Host = host,
                EnableSsl = enableSsl,
                Timeout = timeout,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(credentialEmail, password)
            };
        }

        public bool SendMessage(Message message)
        {
            using (var mailMessage = message.ToMailMessage())
            {
                mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                smtpClient.Send(mailMessage);
                
                return true;
            }
        }

        public void Dispose()
        {
            smtpClient.Dispose();
        }
    }
}
