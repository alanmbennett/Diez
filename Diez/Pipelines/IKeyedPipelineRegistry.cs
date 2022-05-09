namespace Diez.Pipelines
{
    public interface IKeyedPipelineRegistry<TKey, TPipelineStep>
        where TPipelineStep : IPipelineStep
    {
        void AddPipeline(
            TKey keyValue,
            Action<IPipelineRegistry<TPipelineStep>> registryAction    
        );
    }
}