using Autofac;
using System.Reflection;

public class AutoFacHelper
{
    private static IContainer Container { get; set; }
    public static IContainer Init()
    {
        if (Container != null)
        {
            return Container;
        }

        var builder = new ContainerBuilder();
        var assembly = Assembly.Load(Config.DataAccess);
        builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().InstancePerLifetimeScope();
        Container = builder.Build();
        return Container;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T Resolve<T>()
    {
        return Container.Resolve<T>();
    }
}
