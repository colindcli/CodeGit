using Quartz;
using System;
using System.Threading.Tasks;
using FileService.Biz;
using FileService.Common;

namespace FileService.WebApi.Jobs
{
    public class DeleteAttachmentJob : IJob
    {
        private static readonly AttachmentBiz Biz = new AttachmentBiz();

        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Run(() =>
            {
                LogHelper.Info($"执行删除附件任务：{DateTime.Now}");
                Biz.DeleteAttachment();
            });
        }
    }
}