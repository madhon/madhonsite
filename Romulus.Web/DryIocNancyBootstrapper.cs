namespace Romulus.Web
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using DryIoc;
  using global::Nancy;
  using global::Nancy.Bootstrapper;
  using global::Nancy.Diagnostics;

  public abstract class DryIocNancyBootstrapper : NancyBootstrapperWithRequestContainerBase<IContainer>
  {
    protected override IDiagnostics GetDiagnostics()
    {
      return ApplicationContainer.Resolve<IDiagnostics>();
    }

    protected override IEnumerable<IApplicationStartup> GetApplicationStartupTasks()
    {
      return ApplicationContainer.ResolveMany<IApplicationStartup>();
    }

    protected override IEnumerable<IRequestStartup> RegisterAndGetRequestStartupTasks(IContainer container,
      Type[] requestStartupTypes)
    {
      foreach (var requestStartupType in requestStartupTypes)
      {
        container.Register(typeof(IRequestStartup), requestStartupType, Reuse.Singleton);
      }

      return container.ResolveMany<IRequestStartup>();
    }

    protected override IEnumerable<IRegistrations> GetRegistrationTasks()
    {
      return ApplicationContainer.ResolveMany<IRegistrations>();
    }

    protected override INancyEngine GetEngineInternal()
    {
      return ApplicationContainer.Resolve<INancyEngine>();
    }

    protected override IContainer GetApplicationContainer()
    {
      return new Container(rules => rules.With(FactoryMethod.ConstructorWithResolvableArguments));
    }

    protected override void RegisterBootstrapperTypes(IContainer applicationContainer)
    {
      applicationContainer.RegisterInstance<INancyModuleCatalog>(this);
    }

    protected override void RegisterTypes(IContainer container, IEnumerable<TypeRegistration> typeRegistrations)
    {
      foreach (var typeRegistration in typeRegistrations)
      {
        switch (typeRegistration.Lifetime)
        {
          case Lifetime.Transient:
            container.Register(typeRegistration.RegistrationType, typeRegistration.ImplementationType, Reuse.Transient,
              FactoryMethod.ConstructorWithResolvableArguments);
            break;
          case Lifetime.Singleton:
            container.Register(typeRegistration.RegistrationType, typeRegistration.ImplementationType, Reuse.Singleton,
              FactoryMethod.ConstructorWithResolvableArguments);
            break;
          case Lifetime.PerRequest:
            throw new InvalidOperationException("Unable to directly register a per request lifetime.");
          default:
            throw new ArgumentOutOfRangeException();
        }
      }
    }

    protected override void RegisterCollectionTypes(IContainer container,
      IEnumerable<CollectionTypeRegistration> collectionTypeRegistrations)
    {
      foreach (var collectionTypeRegistration in collectionTypeRegistrations)
      {
        foreach (var implementationType in collectionTypeRegistration.ImplementationTypes)
        {
          switch (collectionTypeRegistration.Lifetime)
          {
            case Lifetime.Transient:
              container.Register(collectionTypeRegistration.RegistrationType, implementationType, Reuse.Transient);
              break;
            case Lifetime.Singleton:
              container.Register(collectionTypeRegistration.RegistrationType, implementationType, Reuse.Singleton);
              break;
            case Lifetime.PerRequest:
              throw new InvalidOperationException("Unable to directly register a per request lifetime.");
            default:
              throw new ArgumentOutOfRangeException();
          }
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

    protected override IContainer CreateRequestContainer(NancyContext context)
    {
      return ApplicationContainer.OpenScope();
    }

    private List<Type> _moduleTypes;

    protected override void RegisterRequestContainerModules(IContainer container,
      IEnumerable<ModuleRegistration> moduleRegistrationTypes)
    {
      _moduleTypes = new List<Type>();

      foreach (var moduleRegistrationType in moduleRegistrationTypes)
      {
        _moduleTypes.Add(moduleRegistrationType.ModuleType);
        container.Register(typeof(INancyModule), moduleRegistrationType.ModuleType,
          serviceKey: moduleRegistrationType.ModuleType.FullName, ifAlreadyRegistered: IfAlreadyRegistered.Keep);
      }
    }

    protected override IEnumerable<INancyModule> GetAllModules(IContainer container)
    {
      // Hack - need to create an instance using New() of each type otherwise they won't be resolved.
      foreach (var type in _moduleTypes)
      {
        container.New(type);
      }

      return container.ResolveMany<INancyModule>().ToArray();
    }

    protected override INancyModule GetModule(IContainer container, Type moduleType)
    {
      var typeFullName = moduleType.FullName;

      if (container.IsRegistered<INancyModule>(typeFullName))
      {
        return container.Resolve<INancyModule>(typeFullName);
      }

      var instance = (INancyModule)container.New(moduleType);
      container.RegisterInstance(instance, serviceKey: typeFullName);

      return instance;
    }
  }
}