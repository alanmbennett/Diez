using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace Diez.Keyed
{
    internal class KeyedServiceDictionary<TKey, TService> 
        : IKeyedServiceDictionary<TKey, TService>
        where TKey : notnull
    {      
        public readonly IServiceProvider _serviceProvider;
        
        public readonly IReadOnlyDictionary<TKey, Func<TService>> _dictionary;
        
        public KeyedServiceDictionary(IServiceProvider serviceProvider, IDictionary<TKey, Type> dictionary) 
        { 
            _serviceProvider = serviceProvider;

            _dictionary = new ReadOnlyDictionary<TKey, Func<TService>>(
                dictionary.ToDictionary(
                    pair => pair.Key, 
                    pair => GetFactory(pair.Value)
                )
            );
        }

        private Func<TService> GetFactory(Type serviceType)
            => () => (TService)_serviceProvider.GetRequiredService(serviceType);

        public TService this[TKey key] => _dictionary[key]();

        public IEnumerable<TKey> Keys => _dictionary.Keys;

        public IEnumerable<TService> Values => _dictionary.Values.Select(value => value());

        public int Count => _dictionary.Count;

        public bool ContainsKey(TKey key) => _dictionary.ContainsKey(key);

        public IEnumerator<KeyValuePair<TKey, TService>> GetEnumerator()
        {
            foreach(var pair in _dictionary)
            {
                yield return new(pair.Key, pair.Value());
            }
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TService value)
        {
            var exists = _dictionary.TryGetValue(key, out var innerValue);
            value = exists
                ? innerValue!()
                : default;

            return exists;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}