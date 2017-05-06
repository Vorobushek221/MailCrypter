using MailCrypter.Model.Entitties;

namespace MailCrypter.Model.Logics
{
    public interface IMailSender
    {
        bool SendMessage(Message message);

    }
}