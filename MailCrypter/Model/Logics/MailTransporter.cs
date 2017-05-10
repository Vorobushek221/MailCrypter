using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailCrypter.Model.Entitties;
using System.Net.Mail;
using System.Net;
using AE.Net.Mail;

namespace MailCrypter.Model.Logics
{
    public class MailTransporter : IMailTransporter, IDisposable
    {
        protected SmtpClient smtpClient;

        protected ImapClient imapClient;

        public MailTransporter(string credentialEmail,
                          string password,
                            string smtpHost,
                            int smtpPort,
                            bool smtpEnableSsl,
                            int smtpTimeout,
                                string imapHost,
                                int imapPort,
                                bool imapEnableSsl,
                                int imapTimeout)
        {
            smtpClient = new SmtpClient()
            {
                Port = smtpPort,
                Host = smtpHost,
                EnableSsl = smtpEnableSsl,
                Timeout = smtpTimeout,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(credentialEmail, password)
            };

            imapClient = new ImapClient(imapHost,
                                        credentialEmail,
                                        password,
                                        AuthMethods.Login,
                                        imapPort,
                                        imapEnableSsl);
        }

        public bool SendMessage(Message message) //TODO needs refectoring try-catch
        {
            using (var mailMessage = message.ToMailMessage())
            {
                mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                try
                {
                    smtpClient.Send(mailMessage);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public ICollection<Message> ReceiveMessages(int returnCount) //TODO needs refectoring try-catch
        {
            imapClient.SelectMailbox("INBOX");
            var messageTotalAmount = imapClient.GetMessageCount();
            if(messageTotalAmount < returnCount)
            {
                returnCount = messageTotalAmount;
            }
            var mailMessageList = new List<AE.Net.Mail.MailMessage>();
            for(int i = messageTotalAmount - 1; i >= messageTotalAmount - returnCount; i--)
            {
                AE.Net.Mail.MailMessage message = imapClient.GetMessage(i);
                mailMessageList.Add(message);
            }
            var resultMessageList = new List<Message>();
            mailMessageList.ForEach(mail => resultMessageList.Add(new Message(mail)));

            return resultMessageList;
        }

        public void Dispose()
        {
            smtpClient.Dispose();
            imapClient.Dispose();
        }
    }
}
