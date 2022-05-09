using Diez.Keyed;
using Microsoft.Extensions.DependencyInjection;

namespace Diez.Pipelines
{
    internal class KeyedPipelineRegistry<TKey, TPipelineStep, TModel> 
        : KeyedServiceRegistry<TKey, IPipeline<TPipelineStep, TModel>>, 
        IKeyedPipelineRegistry<TKey, TPipelineStep>
        where TPipelineStep : IPipelineStep<TModel>
        where TKey : notnull
    {   
        public KeyedPipelineRegistry(IServiceCollection services) : base(services) { }

        public void AddPipeline(
            TKey keyValue,
            Action<IPipelineRegistry<TPipelineStep>> registryAction    
        )
        {
            var registry = new PipelineRegistry<TPipelineStep>(_services);
            registryAction(registry);

            AddSingleton(
                keyValue,
                provider => new Pipeline<TPipelineStep, TModel>(
                    provider, 
                    registry.GetList()
                )
            );
        }
    }

    internal class KeyedAsyncPipelineRegistry<TKey, TPipelineStep, TModel> 
        : KeyedServiceRegistry<TKey, IAsyncPipeline<TPipelineStep, TModel>>, 
        IKeyedPipelineRegistry<TKey, TPipelineStep>
        where TPipelineStep : IAsyncPipelineStep<TModel>
        where TKey : notnull
    {   
        public KeyedAsyncPipelineRegistry(IServiceCollection services) : base(services) { }

        public void AddPipeline(
            TKey keyValue,
            Action<IPipelineRegistry<TPipelineStep>> registryAction    
        )
        {
            var registry = new PipelineRegistry<TPipelineStep>(_services);
            registryAction(registry);

            AddSingleton(
                keyValue,
                provider => new AsyncPipeline<TPipelineStep, TModel>(
                    provider, 
                    registry.GetList()
                )
            );
        }
    }
}