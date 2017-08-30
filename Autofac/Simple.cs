using Autofac;
using System;

namespace ConsoleApp
{
    class Program
    {
        public static IContainer Container { get; set; }

        private static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<MyComponent>();
            builder.RegisterType<MyComponent>().As<IService>();
            //对每一个依赖或每一次调用创建一个新的唯一的实例。这也是默认的创建实例的方式。
            //.InstancePerDependency()
            //在一个生命周期域中，每一个依赖或调用创建一个单一的共享的实例，且每一个不同的生命周期域，实例是唯一的，不共享的。
            //.InstancePerLifetimeScope()
            //在一个做标识的生命周期域中，每一个依赖或调用创建一个单一的共享的实例。打了标识了的生命周期域中的子标识域中可以共享父级域中的实例。若在整个继承层次中没有找到打标识的生命周期域，则会抛出异常：DependencyResolutionException。
            //.InstancePerMatchingLifetimeScope()
            //在一个生命周期域中所拥有的实例创建的生命周期中，每一个依赖组件或调用Resolve()方法创建一个单一的共享的实例，并且子生命周期域共享父生命周期域中的实例。若在继承层级中没有发现合适的拥有子实例的生命周期域，则抛出异常：DependencyResolutionException。
            //.InstancePerOwned<MyComponent>()
            //每一次依赖组件或调用Resolve()方法都会得到一个相同的共享的实例。其实就是单例模式。
            //.SingleInstance()
            //在一次Http请求上下文中,共享一个组件实例。仅适用于asp.net mvc开发。
            //.InstancePerHttpRequest()

            Container = builder.Build();

            var service = Container.Resolve<IService>();
            service.Add();

            Console.WriteLine("Press any key to close the application");
            Console.ReadKey();
        }
    }

    public class MyComponent : IService
    {
        public void Add()
        {
            Console.WriteLine(DateTime.Now.ToLongTimeString());
        }
    }

    public interface IService
    {
        void Add();
    }
}
