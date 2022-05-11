using Diez.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace Diez.Tests.TestServices.Modules
{
    public record RecordModule : IModule
    {     
        public void Register(IServiceCollection services)
        {
            services.AddTransient<IPizzaClient, LittleCaesarsClient>();
        }
    }
}