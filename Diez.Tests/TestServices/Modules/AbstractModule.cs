using Diez.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace Diez.Tests.TestServices.Modules
{
    public abstract class AbstractModule : IModule
    {
        public void Register(IServiceCollection services) { }
    }
}