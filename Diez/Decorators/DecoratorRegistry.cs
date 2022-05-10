using Microsoft.Extensions.DependencyInjection;

namespace Diez.Decorators
{
    internal class DecoratorRegistry<TService> : IDecoratorRegistry<TService>
    {         
        private readonly IServiceCollection _services;

        private readonly Type? _implementationType;
        
        private readonly HashSet<Type> _registeredDecorators = new();

        public DecoratorRegistry(
            IServiceCollection services, 
            Type? implementationType = null
        )
        { 
            _services = services;
            _implementationType = implementationType;
        }

        public void Add<TDecorator>() where TDecorator : notnull, TService
            => _registeredDecorators.Add(typeof(TDecorator));

        public void CommitRegistration()
        {
            var serviceType = typeof(TService);
            var services = GetServicesToDecorate(serviceType);
            foreach(var service in services)
            {
                var currentDescriptor = service.Descriptor;
                foreach(var decoratorType in _registeredDecorators)
                {
                    var factory = ActivatorUtilities.CreateFactory(
                        decoratorType,
                        new[] { serviceType }
                    );

                    var previousDescriptor = currentDescriptor;
                    currentDescriptor = new ServiceDescriptor(
                        serviceType, 
                        provider => (TService)factory(
                            provider, 
                            new[] { CreateInstance(provider, previousDescriptor) }
                        ),
                        previousDescriptor.Lifetime
                    );
                }

                _services[service.Index] = currentDescriptor;
            }

            // Taken from https://greatrexpectations.com/2018/10/25/decorators-in-net-core-with-dependency-injection
            static object CreateInstance(
                IServiceProvider provider, 
                ServiceDescriptor descriptor
            )
            {
                if (descriptor.ImplementationInstance != null)
                {
                    return descriptor.ImplementationInstance;
                }

                if (descriptor.ImplementationFactory != null)
                {
                    return descriptor.ImplementationFactory(provider);
                }

                return ActivatorUtilities.GetServiceOrCreateInstance(provider, descriptor.ImplementationType!);
            }
        }

        private IEnumerable<(int Index, ServiceDescriptor Descriptor)> GetServicesToDecorate(Type serviceType)
        {
            for (var index = 0; index < _services.Count; index++)
            {
                var descriptor = _services[index];
                if(descriptor.ServiceType == serviceType 
                    && (_implementationType == null 
                        || descriptor.ImplementationType == _implementationType))
                {
                    yield return (index, descriptor);
                }
            }
        }
    }
}