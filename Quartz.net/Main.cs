using System;
using System.Collections.Generic;

class Program
{
    private static void Main(string[] args)
    {
        #region Job
        Action<object, Exception> log = (message, ex) =>
        {
            Console.WriteLine(message);
        };

        var jobLists = new List<QuartzHelper.JobList>()
        {
            new QuartzHelper.JobList()
            {
                Name = "job",
                Group = "group",
                CronExpression = "0/5 * * * * ?",
                JobType = typeof(HelloJob)
            }
        };

        var quartz = new QuartzHelper(log);
        quartz.Run(jobLists);
        #endregion

        Console.WriteLine("Press any key to close the application");
        Console.ReadKey();
    }
}
