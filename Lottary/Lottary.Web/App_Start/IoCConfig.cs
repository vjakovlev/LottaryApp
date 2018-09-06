using Autofac;
using Autofac.Integration.WebApi;
using Lottary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace Lottary.Web
{
    public class IoCConfig
    {
        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterDependecies(new ContainerBuilder()));
        }

        private static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterDependecies(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<LottaryService>()
                .As<IlottaryService>()
                .InstancePerRequest();

            builder.RegisterModule(new ServiceModule());

            return builder.Build();
        }
    }
}