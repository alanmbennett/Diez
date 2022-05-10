using Diez.Decorators;
using Microsoft.Extensions.DependencyInjection;

namespace Diez.Extensions
{
    public static class DecoratorExtensions
    {
        public static void Decorate<TService>(
            this IServiceCollection services,
            Action<IDecoratorRegistry<TService>> registryAction
        )
        {
            var registry = new DecoratorRegistry<TService>(services);
            registryAction(registry);
            registry.CommitRegistration();
        }

        public static void Decorate<TService, TImplementation>(
            this IServiceCollection services,
            Action<IDecoratorRegistry<TService>> registryAction
        )
            where TImplementation : notnull, TService
        {
            var registry = new DecoratorRegistry<TService>(services, typeof(TImplementation));
            registryAction(registry);
            registry.CommitRegistration();
        }
    }
}