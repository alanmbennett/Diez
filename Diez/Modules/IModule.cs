using Microsoft.Extensions.DependencyInjection;

namespace Diez.Modules
{
    public interface IModule
    {
        void Register(IServiceCollection services);
    }
}