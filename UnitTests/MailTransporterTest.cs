using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MailCrypter.Model.Logics;
using MailCrypter.Model.Entitties;

namespace UnitTests
{
    [TestClass]
    public class MailTransporterTest
    {
        [TestMethod]
        public void SendEmailTest()
        {
            //send from yandex.ru
            var mailSender = new MailTransporter(
                "dima-anisko@yandex.ru",
                "abasupamy221",
                "smtp.yandex.ru",
                25,
                true,
                10000,
                "imap.yandex.ru",
                993,
                true,
                10000);

            var success = mailSender.SendMessage(new Message()
            {
                DestinationEmail = @"dmitryanisko221@gmail.com",
                SenderEmail = @"dima-anisko@yandex.ru",
                Subject = "Test Message",
                Text = "Hello"
            });

            Assert.AreEqual(success, true);
        }

        [TestMethod]
        public void ReceiveEmailTest()
        {
            var mailSender = new MailTransporter(
                    "dima-anisko@yandex.ru",
                    "abasupamy221",
                    "smtp.yandex.ru",
                    25,
                    true,
                    10000,
                    "imap.yandex.ru",
                    993,
                    true,
                    10000);

            var receivedMessages = mailSender.ReceiveMessages(5);
            Assert.AreEqual(receivedMessages.Count, 5);
        }
    }
}
