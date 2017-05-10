using MailCrypter.Model.Entitties;

namespace MailCrypter.Model.Logics
{
    public interface IMailTransporter
    {
        bool SendMessage(Message message);

    }
}