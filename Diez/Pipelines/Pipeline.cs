namespace Diez.Pipelines
{
    internal class Pipeline<TPipelineStep, TModel> : IPipeline<TPipelineStep, TModel>
        where TPipelineStep : IPipelineStep<TModel>
    {
        private readonly IServiceProvider _provider;

        private readonly IEnumerable<Func<IServiceProvider, TPipelineStep>> _steps;
        
        public Pipeline(IServiceProvider provider, IEnumerable<Func<IServiceProvider, TPipelineStep>> steps)
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

    internal class AsyncPipeline<TPipelineStep, TModel> : IAsyncPipeline<TPipelineStep, TModel>
        where TPipelineStep : IAsyncPipelineStep<TModel>
    {
        private readonly IServiceProvider _provider;

        private readonly IEnumerable<Func<IServiceProvider, TPipelineStep>> _steps;
        
        public AsyncPipeline(IServiceProvider provider, IEnumerable<Func<IServiceProvider, TPipelineStep>> steps)
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