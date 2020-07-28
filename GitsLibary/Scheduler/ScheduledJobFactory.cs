using Quartz;
using Quartz.Spi;
using System;

namespace GitsLibary.Scheduler
{
    public class ScheduledJobFactory : IJobFactory
    {
        private readonly IServiceProvider serviceProvider;

        public ScheduledJobFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var jobDetail = bundle.JobDetail;

            var job = (IJob)serviceProvider.GetService(jobDetail.JobType);
            return job;
        }

        public void ReturnJob(IJob job) { }
    }
}
