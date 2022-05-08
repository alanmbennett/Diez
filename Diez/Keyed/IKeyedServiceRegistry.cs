namespace Diez.Keyed
{
    public interface IKeyedServiceRegistry<TKey, TService> where TKey : notnull
    {
        void Add<TImplementation>(
            TKey keyValue,
            TImplementation instance
        ) where TImplementation : notnull, TService;

        void TryAdd<TImplementation>(
            TKey keyValue,
            TImplementation instance
        ) where TImplementation : notnull, TService;

        void AddTransient<TImplementation>(TKey keyValue) where TImplementation : notnull, TService;

        void AddTransient<TImplementation>(TKey keyValue, Func<IServiceProvider, TImplementation> factory) 
            where TImplementation : notnull, TService;

        void TryAddTransient<TImplementation>(TKey keyValue) where TImplementation : notnull, TService;

        void TryAddTransient<TImplementation>(TKey keyValue, Func<IServiceProvider, TImplementation> factory) 
            where TImplementation : notnull, TService;

        void AddScoped<TImplementation>(TKey keyValue) where TImplementation : notnull, TService;

        void AddScoped<TImplementation>(TKey keyValue, Func<IServiceProvider, TImplementation> factory) 
            where TImplementation : notnull, TService;

        void TryAddScoped<TImplementation>(TKey keyValue) where TImplementation : notnull, TService;

        void TryAddScoped<TImplementation>(TKey keyValue, Func<IServiceProvider, TImplementation> factory) 
            where TImplementation : notnull, TService;

        void AddSingleton<TImplementation>(TKey keyValue) where TImplementation : notnull, TService;

        void AddSingleton<TImplementation>(TKey keyValue, Func<IServiceProvider, TImplementation> factory) 
            where TImplementation : notnull, TService;

        void TryAddSingleton<TImplementation>(TKey keyValue) where TImplementation : notnull, TService;

        void TryAddSingleton<TImplementation>(TKey keyValue, Func<IServiceProvider, TImplementation> factory) 
            where TImplementation : notnull, TService;
    }
}