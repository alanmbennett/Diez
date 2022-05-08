namespace Diez.Keyed
{
    public interface IKeyedServiceDictionary<TKey, TService> 
        : IReadOnlyDictionary<TKey, TService>
        where TKey : notnull
    { }
}