using Microsoft.Extensions.DependencyInjection;

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