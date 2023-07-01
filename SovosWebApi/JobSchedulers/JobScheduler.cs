using Quartz.Impl;
using Quartz;

namespace SovosWebApi.JobSchedulers
{
    public class JobScheduler : IJobScheduler
    {
        public async void SchedulerSetup<T>(JobDataMap data) where T : IJob
        {
            IScheduler scheduler = JobSchedulerFactory.scheduler;

            IJobDetail job = JobBuilder.Create<T>().SetJobData(data).Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithSimpleSchedule
                  (s => s
                    .WithIntervalInSeconds(5)
                  )
                .Build();

            scheduler?.ScheduleJob(job, trigger);
        }
    }

    public class JobSchedulerFactory
    {
        private static IScheduler _scheduler;
        public static IScheduler scheduler
        {
            get
            {
                if (_scheduler == null)
                {
                    _scheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
                    scheduler?.Start();
                }

                return _scheduler;
            }
            private set { }
        }
        private JobSchedulerFactory() { }
    }
}
