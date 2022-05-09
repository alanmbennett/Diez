using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Diez.Shared
{
    internal abstract class BaseRegistry<TService> : IRegistry<TService>
    {     
        protected readonly IServiceCollection _services;

        public BaseRegistry(IServiceCollection services)
        {
            _services = services;
        }

        protected virtual void BeforeAdd(Type implementationType) { } 

        protected virtual bool Exists(Type implementationType) => false;

        private void Add<TImplementation>(
            ServiceLifetime lifetime, 
            Func<IServiceProvider, TImplementation>? factory = null
        ) 
            where TImplementation : notnull, TService
        {
            var implementationType = typeof(TImplementation);
            BeforeAdd(implementationType);

            _services.TryAdd(
                factory is null
                    ? new ServiceDescriptor(implementationType, implementationType, lifetime)
                    : new(implementationType, (serviceProvider) => factory(serviceProvider), lifetime)
            );
        }

        private void TryAdd<TImplementation>(
            ServiceLifetime lifetime, 
            Func<IServiceProvider, TImplementation>? factory = null
        ) 
            where TImplementation : notnull, TService
        {
            if(Exists(typeof(TImplementation)))
            {
                return;
            }
            Add(lifetime, factory);
        }

        public void Add<TImplementation>(
            TImplementation instance
        ) where TImplementation : notnull, TService
        {
            var implementationType = typeof(TImplementation);  
            BeforeAdd(implementationType);

            _services.TryAdd(new ServiceDescriptor(implementationType, instance));
        }

        public void TryAdd<TImplementation>(
            TImplementation instance
        ) where TImplementation : notnull, TService
        {
            if(Exists(typeof(TImplementation)))
            {
                return;
            }
            Add(instance);
        }

        public void AddTransient<TImplementation>() where TImplementation : notnull, TService
            => Add<TImplementation>(ServiceLifetime.Transient);

        public void AddTransient<TImplementation>(Func<IServiceProvider, TImplementation> factory) 
            where TImplementation : notnull, TService
            => Add(ServiceLifetime.Transient, factory);

        public void TryAddTransient<TImplementation>() where TImplementation : notnull, TService
            => TryAdd<TImplementation>(ServiceLifetime.Transient);

        public void TryAddTransient<TImplementation>(Func<IServiceProvider, TImplementation> factory) 
            where TImplementation : notnull, TService
            => TryAdd(ServiceLifetime.Transient, factory);

        public void AddScoped<TImplementation>() where TImplementation : notnull, TService
            => Add<TImplementation>(ServiceLifetime.Scoped);

        public void AddScoped<TImplementation>(Func<IServiceProvider, TImplementation> factory) 
            where TImplementation : notnull, TService
            => Add(ServiceLifetime.Scoped, factory);

        public void TryAddScoped<TImplementation>() where TImplementation : notnull, TService
            => TryAdd<TImplementation>(ServiceLifetime.Scoped);

        public void TryAddScoped<TImplementation>(Func<IServiceProvider, TImplementation> factory) 
            where TImplementation : notnull, TService
            => TryAdd(ServiceLifetime.Scoped, factory);

        public void AddSingleton<TImplementation>() where TImplementation : notnull, TService
            => Add<TImplementation>(ServiceLifetime.Singleton);

        public void AddSingleton<TImplementation>(Func<IServiceProvider, TImplementation> factory) 
            where TImplementation : notnull, TService
            => Add(ServiceLifetime.Singleton, factory);

        public void TryAddSingleton<TImplementation>() where TImplementation : notnull, TService
            => TryAdd<TImplementation>(ServiceLifetime.Singleton);

        public void TryAddSingleton<TImplementation>(Func<IServiceProvider, TImplementation> factory) 
            where TImplementation : notnull, TService
            => TryAdd(ServiceLifetime.Singleton, factory);
    }
}