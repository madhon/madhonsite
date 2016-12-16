namespace Romulus.Web
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Autofac;
    using Autofac.Features.Variance;
    using JetBrains.Annotations;
    using MediatR;
    using Nancy;
    using Nancy.Bootstrapper;
    using Nancy.Bootstrappers.Autofac;
    using Nancy.Conventions;
    using Nancy.Gzip;
    using Nancy.Security;
    using Romulus.Web.Handlers;
    using Romulus.Web.Helpers;

    [UsedImplicitly]
    public class RomulusBootstrapper : AutofacNancyBootstrapper
    {
        protected override byte[] FavIcon => null;

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);
            nancyConventions.EnableMobileViewLocationConventions();
        }

        protected override void ApplicationStartup(ILifetimeScope container, IPipelines pipelines)
        {
            RomulsStatusCodeHandler.AddCode(404);
            //CustomStatusCode.AddCode(ConfigurationManager.AppSettings["HttpErrorCodes"].Split(',').Select(x => int.Parse(x)));
            Csrf.Enable(pipelines);

            pipelines.EnableGzipCompression();

            base.ApplicationStartup(container, pipelines);
        }

        protected override void ConfigureRequestContainer(ILifetimeScope container, NancyContext context)
        {
            var builder = new ContainerBuilder();
            builder.RegisterSource(new ContravariantRegistrationSource());
            builder.RegisterAssemblyTypes(typeof (IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof (ContactMessageHandler).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();
            builder.RegisterType<GmailTransport>().As<ITransport>();

            builder.Register<SingleInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
            builder.Register<MultiInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => (IEnumerable<object>) c.Resolve(typeof (IEnumerable<>).MakeGenericType(t));
            });

            builder.Update(container.ComponentRegistry);

            base.ConfigureRequestContainer(container, context);
        }
    }
}