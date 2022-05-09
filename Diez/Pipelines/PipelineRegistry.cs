using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Diez.Pipelines
{
    internal class PipelineRegistry<TPipelineStep> : IPipelineRegistry<TPipelineStep>
        where TPipelineStep : IPipelineStep
    {   
        private readonly IServiceCollection _services;
        
        private readonly IDictionary<Type, Func<IServiceProvider, TPipelineStep>> _pipelineSteps 
            = new Dictionary<Type, Func<IServiceProvider, TPipelineStep>>();

        public PipelineRegistry(IServiceCollection services)
        {
            _services = services;
        }

        private void Add<TImplementationStep>(
            ServiceLifetime lifetime, 
            Func<IServiceProvider, TImplementationStep>? factory = null
        ) 
            where TImplementationStep : notnull, TPipelineStep
        {
            var implementationType = typeof(TImplementationStep);
            _pipelineSteps.Add(implementationType, provider => (TPipelineStep)provider.GetRequiredService(implementationType));

            _services.TryAdd(
                factory is null
                    ? new ServiceDescriptor(implementationType, implementationType, lifetime)
                    : new(implementationType, (serviceProvider) => factory(serviceProvider), lifetime)
            );
        }

        private void TryAdd<TImplementationStep>(
            ServiceLifetime lifetime, 
            Func<IServiceProvider, TImplementationStep>? factory = null
        ) 
            where TImplementationStep : notnull, TPipelineStep
        {
            if(_pipelineSteps.ContainsKey(typeof(TImplementationStep)))
            {
                return;
            }
            Add(lifetime, factory);
        }

        public void Add<TImplementationStep>(
            TImplementationStep instance
        ) where TImplementationStep : notnull, TPipelineStep
        {
            var implementationType = typeof(TImplementationStep);  
            _pipelineSteps.Add(implementationType, provider => (TPipelineStep)provider.GetRequiredService(implementationType));

            _services.TryAdd(new ServiceDescriptor(implementationType, instance));
        }

        public void TryAdd<TImplementationStep>(
            TImplementationStep instance
        ) where TImplementationStep : notnull, TPipelineStep
        {
            if(_pipelineSteps.ContainsKey(typeof(TImplementationStep)))
            {
                return;
            }
            Add(instance);
        }

        public void AddTransient<TImplementationStep>() where TImplementationStep : notnull, TPipelineStep
            => Add<TImplementationStep>(ServiceLifetime.Transient);

        public void AddTransient<TImplementationStep>(Func<IServiceProvider, TImplementationStep> factory) 
            where TImplementationStep : notnull, TPipelineStep
            => Add(ServiceLifetime.Transient, factory);

        public void TryAddTransient<TImplementationStep>() where TImplementationStep : notnull, TPipelineStep
            => TryAdd<TImplementationStep>(ServiceLifetime.Transient);

        public void TryAddTransient<TImplementationStep>(Func<IServiceProvider, TImplementationStep> factory) 
            where TImplementationStep : notnull, TPipelineStep
            => TryAdd(ServiceLifetime.Transient, factory);

        public void AddScoped<TImplementationStep>() where TImplementationStep : notnull, TPipelineStep
            => Add<TImplementationStep>(ServiceLifetime.Scoped);

        public void AddScoped<TImplementationStep>(Func<IServiceProvider, TImplementationStep> factory) 
            where TImplementationStep : notnull, TPipelineStep
            => Add(ServiceLifetime.Scoped, factory);

        public void TryAddScoped<TImplementationStep>() where TImplementationStep : notnull, TPipelineStep
            => TryAdd<TImplementationStep>(ServiceLifetime.Scoped);

        public void TryAddScoped<TImplementationStep>(Func<IServiceProvider, TImplementationStep> factory) 
            where TImplementationStep : notnull, TPipelineStep
            => TryAdd(ServiceLifetime.Scoped, factory);

        public void AddSingleton<TImplementationStep>() where TImplementationStep : notnull, TPipelineStep
            => Add<TImplementationStep>(ServiceLifetime.Singleton);

        public void AddSingleton<TImplementationStep>(Func<IServiceProvider, TImplementationStep> factory) 
            where TImplementationStep : notnull, TPipelineStep
            => Add(ServiceLifetime.Singleton, factory);

        public void TryAddSingleton<TImplementationStep>() where TImplementationStep : notnull, TPipelineStep
            => TryAdd<TImplementationStep>(ServiceLifetime.Singleton);

        public void TryAddSingleton<TImplementationStep>(Func<IServiceProvider, TImplementationStep> factory) 
            where TImplementationStep : notnull, TPipelineStep
            => TryAdd(ServiceLifetime.Singleton, factory);

        public IEnumerable<Func<IServiceProvider, TPipelineStep>> GetList() => _pipelineSteps.Values;
    }
}