using MailCrypter.Model.Entitties;
using System.Collections.Generic;

namespace MailCrypter.Model.Logics
{
    public interface IMailTransporter
    {
        bool SendMessage(Message message);

        ICollection<Message> ReceiveMessages(int returCount);
    }
}