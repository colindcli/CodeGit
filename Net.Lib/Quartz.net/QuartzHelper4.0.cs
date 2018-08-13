//安装版本.net4.0：Install-Package Quartz -Version 2.6.2

using Quartz;
using Quartz.Impl;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace CaiPao.Common
{
    public class QuartzHelper
    {
        public void Run(List<JobList> jobLists)
        {
            //LogProvider.SetCurrentLogProvider(new QuartzLogProvider(LogHelper.Quartz));
            try
            {
                var props = new NameValueCollection
                {
                    { "quartz.serializer", "binary" }
                };
                var factory = new StdSchedulerFactory(props);
                var scheduler = factory.GetScheduler();
                scheduler.Start();

                foreach (var jobList in jobLists)
                {
                    var job = JobBuilder.Create(jobList.JobType.GetType()).WithIdentity(jobList.Name, jobList.Group).Build();
                    var trigger = new Quartz.Impl.Triggers.CronTriggerImpl(jobList.Name, jobList.Group, jobList.CronExpression);
                    scheduler.ScheduleJob(job, trigger);
                }
            }
            catch (SchedulerException se)
            {
                LogHelper.Quartz(se.Message, se);
            }
        }

        public class JobList
        {
            public string Name { get; set; }
            public string Group { get; set; }
            /// <summary>
            /// Cron表达式
            /// </summary>
            public string CronExpression { get; set; }
            /// <summary>
            /// 作业类(继承IJob)
            /// </summary>
            public IJob JobType { get; set; }
        }

    }
}
