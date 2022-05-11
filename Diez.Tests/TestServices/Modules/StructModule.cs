using Diez.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace Diez.Tests.TestServices.Modules
{
    public struct StructModule : IModule
    {
        public void Register(IServiceCollection services)
        {
            services.AddTransient<IPizzaClient, PapaJohnsClient>();
        }
    }
}