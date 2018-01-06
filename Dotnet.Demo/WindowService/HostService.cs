//创建 Windows服务 文件HostService.cs
//在设计模式下，右键 添加安装程序 ProjectInstaller.cs：serviceProcessInstaller1 属性Account设为LocalSystem；serviceInstaller1设置属性Discription、DisplayName、ServiceName，StartType设为Automatic

using System.ServiceProcess;

namespace ConsoleApp1
{
    partial class HostService : ServiceBase
    {
        public HostService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            ServiceBoot.StartAsync().Wait();
        }

        protected override void OnStop()
        {
            ServiceBoot.StopAsync().Wait();
        }
    }
}
