using Microsoft.Extensions.DependencyInjection;

namespace Diez.Pipelines
{
    public interface IKeyedPipelineRegistry<TKey, TPipelineStep>
    {
        void AddPipeline(
            TKey keyValue,
            ServiceLifetime pipelineLiftime,
            Action<IPipelineRegistry<TPipelineStep>> registryAction    
        );
    }
}