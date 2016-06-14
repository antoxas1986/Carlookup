using Autofac;
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
            builder.RegisterType<RoleService>().As<IRoleService>();
            builder.RegisterType<LoginService>().As<ILoginService>();
        }
    }
}
