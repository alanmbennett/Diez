using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Diez.Decorators
{
    public class DecoratorRegistry<TService>
    { 
        private readonly IServiceCollection _services;
        
        private readonly IList<Type> _decorators;
        
        public DecoratorRegistry(IServiceCollection services)
        {
            _services = services;
            _decorators = new List<Type>();
        }
    }
}