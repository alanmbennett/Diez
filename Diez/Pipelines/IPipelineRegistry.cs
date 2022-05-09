namespace Diez.Pipelines
{
    public interface IPipelineRegistry<TPipelineStep>
    {
        void Add<TImplementationStep>(
            TImplementationStep instance
        ) where TImplementationStep : notnull, TPipelineStep;

        void TryAdd<TImplementationStep>(
            TImplementationStep instance
        ) where TImplementationStep : notnull, TPipelineStep;

        void AddTransient<TImplementationStep>() where TImplementationStep : notnull, TPipelineStep;

        void AddTransient<TImplementationStep>(Func<IServiceProvider, TImplementationStep> factory) 
            where TImplementationStep : notnull, TPipelineStep;

        void TryAddTransient<TImplementationStep>() where TImplementationStep : notnull, TPipelineStep;

        void TryAddTransient<TImplementationStep>(Func<IServiceProvider, TImplementationStep> factory) 
            where TImplementationStep : notnull, TPipelineStep;

        void AddScoped<TImplementationStep>() where TImplementationStep : notnull, TPipelineStep;

        void AddScoped<TImplementationStep>(Func<IServiceProvider, TImplementationStep> factory) 
            where TImplementationStep : notnull, TPipelineStep;

        void TryAddScoped<TImplementationStep>() where TImplementationStep : notnull, TPipelineStep;

        void TryAddScoped<TImplementationStep>(Func<IServiceProvider, TImplementationStep> factory) 
            where TImplementationStep : notnull, TPipelineStep;

        void AddSingleton<TImplementationStep>() where TImplementationStep : notnull, TPipelineStep;

        void AddSingleton<TImplementationStep>(Func<IServiceProvider, TImplementationStep> factory) 
            where TImplementationStep : notnull, TPipelineStep;

        void TryAddSingleton<TImplementationStep>() where TImplementationStep : notnull, TPipelineStep;

        void TryAddSingleton<TImplementationStep>(Func<IServiceProvider, TImplementationStep> factory) 
            where TImplementationStep : notnull, TPipelineStep;
    }
}