using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SovosWebApi.JobSchedulers
{
    public interface IJobScheduler
    {
        void SchedulerSetup<T>(JobDataMap data) where T : IJob;
    }
}
