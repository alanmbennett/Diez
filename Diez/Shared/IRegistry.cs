namespace Diez.Shared
{
    public interface IRegistry<TService>
    {
        void TryAdd<TImplementation>(
            TImplementation instance
        ) where TImplementation : notnull, TService;

        void AddTransient<TImplementation>() where TImplementation : notnull, TService;

        void AddTransient<TImplementation>(Func<IServiceProvider, TImplementation> factory) 
            where TImplementation : notnull, TService;

        void TryAddTransient<TImplementation>() where TImplementation : notnull, TService;

        void TryAddTransient<TImplementation>(Func<IServiceProvider, TImplementation> factory) 
            where TImplementation : notnull, TService;

        void AddScoped<TImplementation>() where TImplementation : notnull, TService;

        void AddScoped<TImplementation>(Func<IServiceProvider, TImplementation> factory) 
            where TImplementation : notnull, TService;

        public void TryAddScoped<TImplementation>() where TImplementation : notnull, TService;

        void TryAddScoped<TImplementation>(Func<IServiceProvider, TImplementation> factory) 
            where TImplementation : notnull, TService;

        public void AddSingleton<TImplementation>() where TImplementation : notnull, TService;

        public void AddSingleton<TImplementation>(Func<IServiceProvider, TImplementation> factory) 
            where TImplementation : notnull, TService;

        public void TryAddSingleton<TImplementation>() where TImplementation : notnull, TService;

        public void TryAddSingleton<TImplementation>(Func<IServiceProvider, TImplementation> factory) 
            where TImplementation : notnull, TService;
    }
}