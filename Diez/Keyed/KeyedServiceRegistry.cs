using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Diez.Keyed
{
    internal class KeyedServiceRegistry<TKey, TService> 
        : IKeyedServiceRegistry<TKey, TService>
        where TKey: notnull
    {  
        private readonly IServiceCollection _services;

        public KeyedServiceRegistry(IServiceCollection services)
        {
            _services = services;
        }

        private readonly IDictionary<TKey, Type> _registeredServices = new Dictionary<TKey, Type>();

        private void Add<TImplementation>(
            TKey keyValue, 
            ServiceLifetime lifetime, 
            Func<IServiceProvider, TImplementation>? factory = null
        ) where TImplementation : notnull, TService
        {
            var implementationType = typeof(TImplementation);
            _registeredServices.Add(keyValue, implementationType);

            _services.TryAdd(
                factory is null
                    ? new ServiceDescriptor(implementationType, implementationType, lifetime)
                    : new(implementationType, (serviceProvider) => factory(serviceProvider), lifetime)
            );
        }

        private void TryAdd<TImplementation>(
            TKey keyValue, 
            ServiceLifetime lifetime, 
            Func<IServiceProvider, TImplementation>? factory = null
        ) where TImplementation : notnull, TService
        {
            if(_registeredServices.ContainsKey(keyValue))
            {
                return;
            }

            var implementationType = typeof(TImplementation);
            _registeredServices.Add(keyValue, implementationType);

            _services.TryAdd(
                factory is null
                    ? new ServiceDescriptor(implementationType, implementationType, lifetime)
                    : new(implementationType, (serviceProvider) => factory(serviceProvider), lifetime)
            );
        }

        public void Add<TImplementation>(
            TKey keyValue,
            TImplementation instance
        ) where TImplementation : notnull, TService
        {
            var implementationType = typeof(TImplementation);  
            _registeredServices.Add(keyValue, implementationType);

            _services.TryAdd(new ServiceDescriptor(implementationType, instance));
        }

        public void TryAdd<TImplementation>(
            TKey keyValue,
            TImplementation instance
        ) where TImplementation : notnull, TService
        {
            if(_registeredServices.ContainsKey(keyValue))
            {
                return;
            }

            var implementationType = typeof(TImplementation);
            _registeredServices.Add(keyValue, implementationType);
            _services.TryAdd(new ServiceDescriptor(implementationType, instance));
        }

        public void AddTransient<TImplementation>(TKey keyValue) where TImplementation : notnull, TService
            => Add<TImplementation>(keyValue, ServiceLifetime.Transient);

        public void AddTransient<TImplementation>(TKey keyValue, Func<IServiceProvider, TImplementation> factory) 
            where TImplementation : notnull, TService
            => Add(keyValue, ServiceLifetime.Transient, factory);

        public void TryAddTransient<TImplementation>(TKey keyValue) where TImplementation : notnull, TService
            => TryAdd<TImplementation>(keyValue, ServiceLifetime.Transient);

        public void TryAddTransient<TImplementation>(TKey keyValue, Func<IServiceProvider, TImplementation> factory) 
            where TImplementation : notnull, TService
            => TryAdd(keyValue, ServiceLifetime.Transient, factory);

        public void AddScoped<TImplementation>(TKey keyValue) where TImplementation : notnull, TService
            => Add<TImplementation>(keyValue, ServiceLifetime.Scoped);

        public void AddScoped<TImplementation>(TKey keyValue, Func<IServiceProvider, TImplementation> factory) 
            where TImplementation : notnull, TService
            => Add(keyValue, ServiceLifetime.Scoped, factory);

        public void TryAddScoped<TImplementation>(TKey keyValue) where TImplementation : notnull, TService
            => TryAdd<TImplementation>(keyValue, ServiceLifetime.Scoped);

        public void TryAddScoped<TImplementation>(TKey keyValue, Func<IServiceProvider, TImplementation> factory) 
            where TImplementation : notnull, TService
            => TryAdd(keyValue, ServiceLifetime.Scoped, factory);

        public void AddSingleton<TImplementation>(TKey keyValue) where TImplementation : notnull, TService
            => Add<TImplementation>(keyValue, ServiceLifetime.Singleton);

        public void AddSingleton<TImplementation>(TKey keyValue, Func<IServiceProvider, TImplementation> factory) 
            where TImplementation : notnull, TService
            => Add(keyValue, ServiceLifetime.Singleton, factory);

        public void TryAddSingleton<TImplementation>(TKey keyValue) where TImplementation : notnull, TService
            => TryAdd<TImplementation>(keyValue, ServiceLifetime.Singleton);

        public void TryAddSingleton<TImplementation>(TKey keyValue, Func<IServiceProvider, TImplementation> factory) 
            where TImplementation : notnull, TService
            => TryAdd(keyValue, ServiceLifetime.Singleton, factory);

        public IDictionary<TKey, Type> GetRegisteredServices() => _registeredServices;
    }
}