using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace SovosWebApi.Core.MailServer
{
    public class Mailer : IMailer
    {
        public string senderMail { get; private set; }
        public string senderPassword { get; private set; }
        public string smtpAddress { get; private set; }
        public int smtpPort { get; private set; }
        public string receiverEmail { get; private set; }
        private SmtpClient smtpClient;
        public Mailer(IConfiguration configuration)
        {
            senderMail = configuration.GetSection("MailInfo").GetSection("senderMail").Value;
            senderPassword = configuration.GetSection("MailInfo").GetSection("senderPassword").Value;
            smtpAddress = configuration.GetSection("MailInfo").GetSection("smtpAddress").Value;
            int.TryParse(configuration.GetSection("MailInfo").GetSection("smtpPort").Value, out int smtpPort);
            this.smtpPort = smtpPort;
            receiverEmail = configuration.GetSection("MailInfo").GetSection("receiverEmail").Value;

            smtpClient = new SmtpClient(smtpAddress, smtpPort);
            smtpClient.EnableSsl = true; // SSL kullanarak güvenli bir bağlantı kurun
            
            // Kimlik doğrulama için kullanıcı bilgilerini belirleyin
            smtpClient.Credentials = new NetworkCredential(senderMail, senderPassword);
        }

        public void Send(string message,string subject = "")
        {
            // E-posta oluşturun
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(senderMail);
            mail.To.Add(receiverEmail);
            mail.CC.Add("gurkan.kir@bil.omu.edu.tr");
            mail.Subject = subject;
            mail.Body = message;

            // E-postayı gönderin
            smtpClient.Send(mail);
        }
    }
}
