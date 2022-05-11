using Diez.Modules;
using Diez.Tests.TestServices.Decorators;
using Microsoft.Extensions.DependencyInjection;

namespace Diez.Tests.TestServices.Modules
{
    public class RepositoryModule : IModule
    { 
        public void Register(IServiceCollection services)
        {
            services.AddTransient<IRepository, DatabaseRepository>();
        }
    }
}