namespace Diez.Pipelines
{
    public interface IPipeline<TModel>
    {
        TModel Start(TModel model);
    }

    public interface IAsyncPipeline<TModel>
    {
        Task<TModel> Start(TModel model);
    }
}