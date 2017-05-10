using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MailCrypter.Model.Entitties
{
    public class Message
    {
        public Message(AE.Net.Mail.MailMessage mailMessage)
        {
            DestinationEmail = mailMessage.To.FirstOrDefault().Address;
            SenderEmail = mailMessage.From.Address;
            Subject = mailMessage.Subject;
            Text = mailMessage.Body;
            ReceiveTime = mailMessage.Date;
            SendTime = null;
        }

        public Message()
        {}

        public string DestinationEmail { get; set; }

        public string SenderEmail { get; set; }

        public string Subject { get; set; }

        public DateTime? SendTime { get; set; }

        public DateTime? ReceiveTime { get; set; }

        public string Text { get; set; }

        /// <summary>
        /// Method converts MailCrypter.Model.Entities.Message to System.Net.Mail.MailMessage.
        /// </summary>
        /// <returns>System.Net.Mail.MailMessage entity</returns>
        public MailMessage ToMailMessage()
        {
            return new MailMessage(this.SenderEmail,
                                this.DestinationEmail,
                                this.Subject,
                                this.Text);
        }
    }
}
