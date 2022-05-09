namespace Diez.Pipelines
{
    public interface IKeyedPipelineRegistry<TKey, TPipelineStep>
    {
        void AddPipeline(
            TKey keyValue,
            Action<IPipelineRegistry<TPipelineStep>> registryAction    
        );
    }
}