using Diez.Keyed;
using Microsoft.Extensions.DependencyInjection;

namespace Diez.Extensions
{
    public static class KeyedServiceExtensions
    {
        public static void AddKeyedServices<TKey, TService>(
            this IServiceCollection services, 
            Action<IKeyedServiceRegistry<TKey, TService>> registryAction
        ) 
            where TKey : notnull
        {
            var registry = new KeyedServiceRegistry<TKey, TService>(services);
            registryAction(registry);
            
            services.AddSingleton<IKeyedServiceDictionary<TKey, TService>>(
                provider => new KeyedServiceDictionary<TKey, TService>(provider, registry.GetRegisteredServices())
            );
        }
    }
}