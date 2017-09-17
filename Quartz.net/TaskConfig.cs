using System.Collections.Generic;

public class TaskConfig
{
    public static void Run()
    {

        var jobLists = new List<QuartzHelper.JobList>()
        {
            new QuartzHelper.JobList()
            {
                Name = "TopHits",
                Group = "group",
                CronExpression = "0 0 0/1 * * ? ",//每小时
                JobType = new HourJob()
            }
        };

        var quartz = new QuartzHelper(LogHelper.Debug);
        quartz.Run(jobLists);
    }
}
