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
        public string DestinationEmail { get; set; }

        public string SenderEmail { get; set; }

        public string Subject { get; set; }

        public DateTime? SendTime { get; set; }

        public DateTime? ReceiveTime { get; set; }

        public string Text { get; set; }

        public MailMessage ToMailMessage()
        {
            return new MailMessage(this.SenderEmail,
                                this.DestinationEmail,
                                this.Subject,
                                this.Text);
        }
        
    }
}
