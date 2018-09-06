using Autofac;
using Lottary.Data;
using Lottary.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottary.Services
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LottaryContext>()
                .As<DbContext>()
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepository<>));
        }
    }
}
