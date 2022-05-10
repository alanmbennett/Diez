using Diez.Pipelines;
using Microsoft.Extensions.DependencyInjection;

namespace Diez.Extensions
{
    public static class PipelineExtensions
    { 
        public static void AddPipeline<TModel>(
            this IServiceCollection services, 
            ServiceLifetime pipelineLifetime,
            Action<IPipelineRegistry<IPipelineStep<TModel>>> registryAction    
        )
        {
            var registry = new PipelineRegistry<IPipelineStep<TModel>>(services);
            registryAction(registry);

            var list = registry.GetList().ToList();
            services.Add(new ServiceDescriptor(
                typeof(IPipeline<TModel>),
                provider => new Pipeline<TModel>(
                    provider, 
                    list
                ),
                pipelineLifetime
            ));
        }

        public static void AddKeyedPipelines<TKey, TModel>(
            this IServiceCollection services, 
            Action<IKeyedPipelineRegistry<TKey, IPipelineStep<TModel>>> registryAction    
        )
            where TKey : notnull
        {
            var registry = new KeyedPipelineRegistry<TKey, TModel>(services);
            registryAction(registry);
            services.RegisterKeyedServiceDictionary(registry);
        }
        
        public static void AddAsyncPipeline<TModel>(
            this IServiceCollection services, 
            ServiceLifetime pipelineLifetime,
            Action<IPipelineRegistry<IAsyncPipelineStep<TModel>>> registryAction    
        )
        {
            var registry = new PipelineRegistry<IAsyncPipelineStep<TModel>>(services);
            registryAction(registry);

            var list = registry.GetList().ToList();
            services.Add(new ServiceDescriptor(
                typeof(IAsyncPipeline<TModel>),
                provider => new AsyncPipeline<TModel>(
                    provider, 
                    list
                ),
                pipelineLifetime
            ));
        }

        public static void AddKeyedAsyncPipelines<TKey, TModel>(
            this IServiceCollection services, 
            Action<IKeyedPipelineRegistry<TKey, IAsyncPipelineStep<TModel>>> registryAction    
        )
            where TKey : notnull
        {
            var registry = new KeyedAsyncPipelineRegistry<TKey, TModel>(services);
            registryAction(registry);
            services.RegisterKeyedServiceDictionary(registry);
        }
    }
}