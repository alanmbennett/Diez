using Diez.Pipelines;
using Microsoft.Extensions.DependencyInjection;

namespace Diez.Extensions
{
    public static class PipelineExtensions
    { 
        public static void AddPipeline<TPipelineSetup, TModel>(
            this IServiceCollection services, 
            Action<IPipelineRegistry<TPipelineSetup>> registryAction    
        )
            where TPipelineSetup : IPipelineStep<TModel>
        {
            var registry = new PipelineRegistry<TPipelineSetup>(services);
            registryAction(registry);

            services.AddSingleton<IPipeline<TPipelineSetup, TModel>>(
                provider => new Pipeline<TPipelineSetup, TModel>(
                    provider, 
                    registry.GetList()
                )
            );
        }
        
        public static void AddAsyncPipeline<TPipelineSetup, TModel>(
            this IServiceCollection services, 
            Action<IPipelineRegistry<TPipelineSetup>> registryAction    
        )
            where TPipelineSetup : IAsyncPipelineStep<TModel>
        {
            var registry = new PipelineRegistry<TPipelineSetup>(services);
            registryAction(registry);

            services.AddSingleton<IAsyncPipeline<TPipelineSetup, TModel>>(
                provider => new AsyncPipeline<TPipelineSetup, TModel>(
                    provider, 
                    registry.GetList()
                )
            );
        }
    }
}