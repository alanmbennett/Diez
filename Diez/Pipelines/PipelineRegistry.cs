using Diez.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace Diez.Pipelines
{
    internal class PipelineRegistry<TPipelineStep> 
        : BaseRegistry<TPipelineStep>,
        IPipelineRegistry<TPipelineStep>
        where TPipelineStep : IPipelineStep
    {   
        private readonly IDictionary<Type, Func<IServiceProvider, TPipelineStep>> _pipelineSteps 
            = new Dictionary<Type, Func<IServiceProvider, TPipelineStep>>();

        public PipelineRegistry(IServiceCollection services) : base(services) { }

        protected override void BeforeAdd(Type implementationType) 
            => _pipelineSteps.Add(implementationType, provider => (TPipelineStep)provider.GetRequiredService(implementationType));

        protected override bool Exists(Type implementationType) => _pipelineSteps.ContainsKey(implementationType);

        public IEnumerable<Func<IServiceProvider, TPipelineStep>> GetList() => _pipelineSteps.Values;
    }
}