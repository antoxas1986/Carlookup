using Autofac;
using CarLookUp.Data.Context;
using CarLookUp.Data.Context.Interfaces;
using CarLookUp.Data.Repository;
using CarLookUp.Data.Repository.Interfaces;

namespace CarLookUp.Data.Ioc
{
    public class IocModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterModule(new Core.Ioc.IocModule());
            builder.RegisterType<CarRepository>().As<ICarRepository>();
            builder.RegisterType<CarContext>().As<ICarContext>().InstancePerRequest();
            builder.RegisterType<BodyTypeRepository>().As<IBodyTypeRepository>();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<RoleRepo>().As<IRoleRepo>();
        }
    }
}
