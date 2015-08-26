using System.Reflection;
using Autofac;

namespace NancyDemo.Areas.Bootstrap
{
    public class IoC
    {
        public static IContainer Container { get; private set; }

        static IoC()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.Load("NancyDemo.Lib")).AsSelf().AsImplementedInterfaces();
            Container = builder.Build();
        }
    }
}