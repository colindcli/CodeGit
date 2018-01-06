using FileService.Common;
using System.Collections.Generic;

namespace FileService.WebApi
{
    public class TaskConfig
    {
        public static void RegisterQuartz()
        {

            var jobLists = new List<QuartzHelper.JobList>()
            {
                //每天6小时执行一次
                new QuartzHelper.JobList()
                {
                    Name = "DeleteAttachment",
                    Group = "UnUsedAttachment",
                    CronExpression = "0 0 0/6 * * ? ",
                    JobType = new Jobs.DeleteAttachmentJob()
                }
            };

            var quartz = new QuartzHelper(LogHelper.Debug);
            quartz.Run(jobLists);
        }
    }
}