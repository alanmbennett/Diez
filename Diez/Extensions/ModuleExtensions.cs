using System.Reflection;
using Diez.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace Diez.Extensions
{
    public static class ModuleExtensions
    { 
        public static void RegisterModule(this IServiceCollection services, IModule module)
            => module.Register(services);

        public static void RegisterModule<TModule>(this IServiceCollection services)
            where TModule : IModule, new()
            => RegisterModule(services, new TModule());

        public static void RegisterModules(
            this IServiceCollection services, 
            params Assembly[] assemblies
        )
        {
            var moduleType = typeof(IModule);
            var modules = assemblies.SelectMany(assembly => assembly.GetTypes())
                .Where(type => !type.IsInterface 
                    && !type.IsAbstract
                    && moduleType.IsAssignableFrom(type)
                )
                .Select(type => Activator.CreateInstance(type))
                .Cast<IModule>();
            
            foreach(var module in modules)
            {
                RegisterModule(services, module);
            }
        }
    }
}