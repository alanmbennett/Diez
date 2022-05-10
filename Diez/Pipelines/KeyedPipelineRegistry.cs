using Diez.Keyed;
using Microsoft.Extensions.DependencyInjection;

namespace Diez.Pipelines
{
    internal class KeyedPipelineRegistry<TKey, TModel> 
        : KeyedServiceRegistry<TKey, IPipeline<TModel>>, 
        IKeyedPipelineRegistry<TKey, IPipelineStep<TModel>>
        where TKey : notnull
    {   
        public KeyedPipelineRegistry(IServiceCollection services) : base(services) { }

        public void AddPipeline(
            TKey keyValue,
            ServiceLifetime pipelineLiftime,
            Action<IPipelineRegistry<IPipelineStep<TModel>>> registryAction    
        )
        {
            var registry = new PipelineRegistry<IPipelineStep<TModel>>(_services);
            registryAction(registry);

            var list = registry.GetList().ToList();
            Add(
                keyValue,
                pipelineLiftime,
                provider => new Pipeline<TModel>(
                    provider, 
                    list
                )
            );
        }
    }

    internal class KeyedAsyncPipelineRegistry<TKey, TModel> 
        : KeyedServiceRegistry<TKey, IAsyncPipeline<TModel>>, 
        IKeyedPipelineRegistry<TKey, IAsyncPipelineStep<TModel>>
        where TKey : notnull
    {   
        public KeyedAsyncPipelineRegistry(IServiceCollection services) : base(services) { }

        public void AddPipeline(
            TKey keyValue,
            ServiceLifetime pipelineLiftime,
            Action<IPipelineRegistry<IAsyncPipelineStep<TModel>>> registryAction    
        )
        {
            var registry = new PipelineRegistry<IAsyncPipelineStep<TModel>>(_services);
            registryAction(registry);

            var list = registry.GetList().ToList();
            Add(
                keyValue,
                pipelineLiftime,
                provider => new AsyncPipeline<TModel>(
                    provider, 
                    list
                )
            );
        }
    }
}