using Autofac;
using CarLookUp.Services.CarServices;
using CarLookUp.Services.Interfaces;

namespace CarLookUp.Services.Ioc
{
    public class IocModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterModule(new Data.Ioc.IocModule());
            builder.RegisterType<CarsService>().As<ICarsService>();
        }
    }
}
