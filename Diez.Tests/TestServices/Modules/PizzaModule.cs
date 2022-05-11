using Diez.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace Diez.Tests.TestServices.Modules
{
    public class PizzaModule : IModule
    { 
        public void Register(IServiceCollection services)
        {
            services.AddTransient<IPizzaClient, PizzaHutClient>();
        }
    }
}