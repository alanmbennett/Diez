using Diez.Pipelines;
using Microsoft.Extensions.DependencyInjection;

namespace Diez.Extensions
{
    public static class PipelineExtensions
    { 
        public static void AddPipeline<TPipelineStep, TModel>(
            this IServiceCollection services, 
            Action<IPipelineRegistry<TPipelineStep>> registryAction    
        )
            where TPipelineStep : IPipelineStep<TModel>
        {
            var registry = new PipelineRegistry<TPipelineStep>(services);
            registryAction(registry);

            services.AddSingleton<IPipeline<TPipelineStep, TModel>>(
                provider => new Pipeline<TPipelineStep, TModel>(
                    provider, 
                    registry.GetList()
                )
            );
        }

        public static void AddKeyedPipelines<TKey, TPipelineStep, TModel>(
            this IServiceCollection services, 
            Action<IKeyedPipelineRegistry<TKey, TPipelineStep>> registryAction    
        )
            where TPipelineStep : IPipelineStep<TModel>
            where TKey : notnull
        {
            var registry = new KeyedPipelineRegistry<TKey, TPipelineStep, TModel>(services);
            registryAction(registry);
            services.RegisterKeyedServiceDictionary(registry);
        }
        
        public static void AddAsyncPipeline<TPipelineStep, TModel>(
            this IServiceCollection services, 
            Action<IPipelineRegistry<TPipelineStep>> registryAction    
        )
            where TPipelineStep : IAsyncPipelineStep<TModel>
        {
            var registry = new PipelineRegistry<TPipelineStep>(services);
            registryAction(registry);

            services.AddSingleton<IAsyncPipeline<TPipelineStep, TModel>>(
                provider => new AsyncPipeline<TPipelineStep, TModel>(
                    provider, 
                    registry.GetList()
                )
            );
        }

        public static void AddKeyedAsyncPipelines<TKey, TPipelineStep, TModel>(
            this IServiceCollection services, 
            Action<IKeyedPipelineRegistry<TKey, TPipelineStep>> registryAction    
        )
            where TPipelineStep : IAsyncPipelineStep<TModel>
            where TKey : notnull
        {
            var registry = new KeyedAsyncPipelineRegistry<TKey, TPipelineStep, TModel>(services);
            registryAction(registry);
            services.RegisterKeyedServiceDictionary(registry);
        }
    }
}