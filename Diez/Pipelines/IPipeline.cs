namespace Diez.Pipelines
{
    public interface IPipeline<TPipelineStep, TModel>
        where TPipelineStep : IPipelineStep<TModel>
    {
        TModel Start(TModel model);
    }

    public interface IAsyncPipeline<TPipelineStep, TModel>
        where TPipelineStep : IAsyncPipelineStep<TModel>
    {
        Task<TModel> Start(TModel model);
    }
}