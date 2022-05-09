namespace Diez.Pipelines
{
    public interface IPipelineStep {}

    public interface IPipelineStep<TModel> : IPipelineStep
    {
        TModel Execute(TModel model);
    }

    public interface IAsyncPipelineStep<TModel> : IPipelineStep
    {
        Task<TModel> Execute(TModel model);
    }
}