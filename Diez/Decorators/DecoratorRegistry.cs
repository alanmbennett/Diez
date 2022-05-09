using Diez.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace Diez.Decorators
{
    internal class DecoratorRegistry<TService> : BaseRegistry<TService>
    { 
        private readonly IList<Type> _decorators;
        
        public DecoratorRegistry(IServiceCollection services) : base(services)
        {
            _decorators = new List<Type>();
        }
    }
}