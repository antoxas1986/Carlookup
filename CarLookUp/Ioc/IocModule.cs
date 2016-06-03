using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Postal;

namespace CarLookUp.Web.Ioc
{
    public class IocModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new Services.Ioc.IocModule());
            builder.RegisterType<EmailService>().As<IEmailService>();

            builder.RegisterControllers(typeof(WebApiApplication).Assembly);
            builder.RegisterApiControllers(typeof(WebApiApplication).Assembly);
            builder.RegisterFilterProvider();
            builder.RegisterSource(new ViewRegistrationSource());
        }
    }
}
