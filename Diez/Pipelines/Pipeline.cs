namespace Diez.Pipelines
{
    internal class Pipeline<TModel> : IPipeline<TModel>
    {
        private readonly IServiceProvider _provider;

        private readonly IEnumerable<Func<IServiceProvider, IPipelineStep<TModel>>> _steps;
        
        public Pipeline(IServiceProvider provider, IEnumerable<Func<IServiceProvider, IPipelineStep<TModel>>> steps)
        {
            _provider = provider;
            _steps = steps;
        }
        
        public TModel Start(TModel model)
        {
            var currentModel = model;
            foreach(var step in _steps)
            {
                currentModel = step(_provider).Execute(currentModel);
            }
            return currentModel;
        }
    }

    internal class AsyncPipeline<TModel> : IAsyncPipeline<TModel>
    {
        private readonly IServiceProvider _provider;

        private readonly IEnumerable<Func<IServiceProvider, IAsyncPipelineStep<TModel>>> _steps;
        
        public AsyncPipeline(IServiceProvider provider, IEnumerable<Func<IServiceProvider, IAsyncPipelineStep<TModel>>> steps)
        {
            _provider = provider;
            _steps = steps;
        }
        
        public async Task<TModel> Start(TModel model)
        {
            var currentModel = model;
            foreach(var step in _steps)
            {
                currentModel = await step(_provider).Execute(currentModel);
            }
            return currentModel;
        }
    }
}