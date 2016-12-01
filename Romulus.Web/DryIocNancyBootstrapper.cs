namespace Romulus.Web
{
    using System;
    using System.Collections.Generic;
    using DryIoc;
    using Nancy;
    using Nancy.Bootstrapper;
    using Nancy.Diagnostics;
    using Nancy.ViewEngines;

    public abstract class DryIocNancyBootstrapper : NancyBootstrapperWithRequestContainerBase<IContainer>
    {
        //private List<Type> _moduleTypes;

        protected override IContainer CreateRequestContainer(NancyContext context) => ApplicationContainer.OpenScope(Reuse.WebRequestScopeName);

        protected override IEnumerable<INancyModule> GetAllModules(IContainer container) => container.Resolve<IEnumerable<INancyModule>>();

        protected override IContainer GetApplicationContainer() => new Container(rules => rules.With(FactoryMethod.ConstructorWithResolvableArguments));

        protected override IEnumerable<IApplicationStartup> GetApplicationStartupTasks() => ApplicationContainer.Resolve<IEnumerable<IApplicationStartup>>();

        protected override IDiagnostics GetDiagnostics() => ApplicationContainer.Resolve<IDiagnostics>();

        protected override INancyEngine GetEngineInternal() => ApplicationContainer.Resolve<INancyEngine>();

        protected override INancyModule GetModule(IContainer container, Type moduleType)
        {
            var moduleKey = moduleType.FullName;

            if (!container.IsRegistered<INancyModule>(moduleKey))
            {
                RegisterRequestContainerModules(container, new[] { new ModuleRegistration(moduleType) });
            }

            return container.Resolve<INancyModule>(moduleKey);
        }

        protected override IEnumerable<IRegistrations> GetRegistrationTasks() => ApplicationContainer.Resolve<IEnumerable<IRegistrations>>();

        protected override IEnumerable<IRequestStartup> RegisterAndGetRequestStartupTasks(IContainer container,
            Type[] requestStartupTypes)
        {
            container.RegisterMany(requestStartupTypes, Reuse.Singleton,
                serviceTypeCondition: t => t == typeof (IRequestStartup));
            return container.Resolve<IEnumerable<IRequestStartup>>();
        }

        protected override void RegisterBootstrapperTypes(IContainer applicationContainer)
        {
            applicationContainer.RegisterInstance<INancyModuleCatalog>(this);
            applicationContainer.Register<IFileSystemReader, DefaultFileSystemReader>(Reuse.Singleton);
        }

        protected override void RegisterCollectionTypes(IContainer container,
            IEnumerable<CollectionTypeRegistration> collectionTypeRegistrations)
        {
            var isScopedContainer = IsScoped(container);
            foreach (var registration in collectionTypeRegistrations)
            {
                foreach (var implementationType in registration.ImplementationTypes)
                {
                    Register(container, registration.RegistrationType, implementationType, registration.Lifetime,
                        isScopedContainer);
                }
            }
        }

        protected override void RegisterInstances(IContainer container,
            IEnumerable<InstanceRegistration> instanceRegistrations)
        {
            foreach (var instanceRegistration in instanceRegistrations)
            {
                container.RegisterInstance(instanceRegistration.RegistrationType, instanceRegistration.Implementation);
            }
        }

        protected override void RegisterRequestContainerModules(IContainer container,
            IEnumerable<ModuleRegistration> moduleRegistrationTypes)
        {
            foreach (var moduleRegistrationType in moduleRegistrationTypes)
            {
                container.Register(
                    typeof (INancyModule),
                    moduleRegistrationType.ModuleType,
                    serviceKey: moduleRegistrationType.ModuleType.FullName,
                    ifAlreadyRegistered: IfAlreadyRegistered.Keep
                    );
            }
        }

        protected override void RegisterTypes(IContainer container, IEnumerable<TypeRegistration> typeRegistrations)
        {
            var isScopedContainer = IsScoped(container);
            foreach (var registration in typeRegistrations)
            {
                Register(container, registration.RegistrationType, registration.ImplementationType,
                    registration.Lifetime, isScopedContainer);
            }
        }

        private bool IsScoped(IContainer container) => container.ContainerWeakRef.Scopes.GetCurrentScope() != null;

        private static void Register(IRegistrator registrator, Type registrationType, Type implementationType,
            Lifetime lifetime,
            bool isScopedContainer)
        {
            var reuse = MapLifetimeToReuse(isScopedContainer ? Lifetime.PerRequest : lifetime);
            registrator.Register(registrationType, implementationType, reuse);
        }

        private static IReuse MapLifetimeToReuse(Lifetime lifetime)
        {
            switch (lifetime)
            {
                case Lifetime.Transient:
                    return Reuse.Transient;
                case Lifetime.Singleton:
                    return Reuse.Singleton;
                case Lifetime.PerRequest:
                    return Reuse.InWebRequest;
                default:
                    throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, "Not supported lifetime: " + lifetime);
            }
        }
    }
}