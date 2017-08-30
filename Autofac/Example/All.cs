using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MvcApplication5
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AutofacConfig.RegisterService();
        }
    }

    public class AutofacConfig
    {
        public static void RegisterService()
        {
            var builder = new ContainerBuilder();

            var baseType = typeof(IDependency);
            var assemblys = AppDomain.CurrentDomain.GetAssemblies().ToList();
            //var allServices = assemblys.SelectMany(s => s.GetTypes()).Where(p => baseType.IsAssignableFrom(p) && p != baseType);

            builder.RegisterControllers(assemblys.ToArray());

            builder.RegisterAssemblyTypes(assemblys.ToArray())
                .Where(t => baseType.IsAssignableFrom(t) && t != baseType)
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            //builder.RegisterAssemblyTypes(assemblys.ToArray())//查找程序集中以Dal结尾的类型  
            //    .Where(t => t.Name.EndsWith("Da"))
            //    .AsImplementedInterfaces();//表示注册的类型，以接口的方式注册  

            // builder.RegisterType<GmsWorkContext>();  
            // builder.RegisterType<GmsWorkContext>().PropertiesAutowired(); 

            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));
        }
    }

    /// <summary>
    /// 注入接口基类
    /// </summary>
    public interface IDependency
    {
    }

    public interface IHelloModel : IDependency
    {
        string GetName();
    }

    public class HelloModel : IHelloModel
    {
        public string GetName()
        {
            return "Hello world!";
        }
    }

    public class HomeController : Controller
    {
        private readonly IHelloModel _helloModel;
        public HomeController(IHelloModel helloModel)
        {
            _helloModel = helloModel;
        }

        public ActionResult Index()
        {
            var helloworld = _helloModel.GetName();
            return View();
        }
    }
}
