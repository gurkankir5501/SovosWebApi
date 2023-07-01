using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SovosWebApi.Core.MailServer
{
    public interface IMailer
    {

        void Send(string message, string subject = "");
    }
}
