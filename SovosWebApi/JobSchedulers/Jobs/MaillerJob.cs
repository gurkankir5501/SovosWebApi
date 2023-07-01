using Quartz;
using SovosWebApi.Core.MailServer;

namespace SovosWebApi.JobSchedulers.Jobs
{
    public class MailerJob : IJob
    {
        public MailerJob()
        {

        }

        public Task Execute(IJobExecutionContext context)
        {
            IMailer _mailer = context.MergedJobDataMap.Get(typeof(IMailer).Name) as IMailer;

            _mailer?.Send("New record created !", "New registration notification !");
            return Task.CompletedTask;
        }
    }
}
