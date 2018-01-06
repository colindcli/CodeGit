using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace FileService.Common
{
    public class QuartzHelper
    {
        public Action<object, Exception> Log { get; set; }

        public QuartzHelper(Action<object, Exception> log)
        {
            Log = log;
        }

        public async void Run(List<JobList> jobLists)
        {
            if (Log != null)
            {
                LogProvider.SetCurrentLogProvider(new QuartzLogProvider(Log));
            }
            try
            {
                var props = new NameValueCollection
                {
                    { "quartz.serializer.type", "binary" }
                };
                var factory = new StdSchedulerFactory(props);
                var scheduler = await factory.GetScheduler();
                await scheduler.Start();

                foreach (var jobList in jobLists)
                {
                    var job = JobBuilder.Create(jobList.JobType.GetType()).WithIdentity(jobList.Name, jobList.Group).Build();
                    var trigger = new Quartz.Impl.Triggers.CronTriggerImpl(jobList.Name, jobList.Group, jobList.CronExpression);
                    await scheduler.ScheduleJob(job, trigger);
                }
            }
            catch (SchedulerException se)
            {
                Log?.Invoke(se.Message, se);
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

        private class QuartzLogProvider : ILogProvider
        {
            private Action<object, Exception> AddLog { get; set; }

            public QuartzLogProvider(Action<object, Exception> log = null)
            {
                AddLog = log;
            }

            public Logger GetLogger(string name)
            {
                return (level, func, exception, parameters) =>
                {
                    if (AddLog == null || func == null || level == LogLevel.Debug) return true;

                    var message = $"[{DateTime.Now.ToLongTimeString()}] [{level}] {func()} {string.Join(";", parameters)}";
                    AddLog(message, exception);
                    return true;
                };
            }

            public IDisposable OpenNestedContext(string message)
            {
                AddLog(message, null);
                return null;
            }

            public IDisposable OpenMappedContext(string key, string value)
            {
                AddLog($"{key}:{value}", null);
                return null;
            }
        }
    }
}
