using System;
using System.Collections.Generic;
using System.Linq;
using Diez.Extensions;
using Diez.Keyed;
using Diez.Tests.TestServices;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Diez.Tests.Integration
{
    public class KeyedServiceExtensionsTests
    { 
        private static IServiceProvider GetTestProviderWithKeyedServices()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddKeyedServices<PizzaClient, IPizzaClient>(registry => 
            {
                registry.AddTransient<PapaJohnsClient>(PizzaClient.PapaJohns);
                registry.AddTransient<PizzaHutClient>(PizzaClient.PizzaHut);
                registry.AddTransient<LittleCaesarsClient>(PizzaClient.LittleCaesars);
            });
            return serviceCollection.BuildServiceProvider();
        }

        [Fact]
        public void ServiceProviderWillReturnKeyedServicesDictionary()
        {
            var provider = GetTestProviderWithKeyedServices();
            var dictionary = provider.GetRequiredService<IKeyedServiceDictionary<PizzaClient, IPizzaClient>>();

            dictionary.Values.Select(value => value.GetType())
                .Should().BeEquivalentTo(new[]
                {
                    typeof(PapaJohnsClient),
                    typeof(LittleCaesarsClient),
                    typeof(PizzaHutClient)
                });
        }

        [Theory]
        [InlineData(PizzaClient.LittleCaesars, typeof(LittleCaesarsClient))]
        [InlineData(PizzaClient.PapaJohns, typeof(PapaJohnsClient))]
        [InlineData(PizzaClient.PizzaHut, typeof(PizzaHutClient))]
        public void KeyedDictionaryWillReturnExpectedType(PizzaClient pizzaClient, Type expectedType)
        {
            var provider = GetTestProviderWithKeyedServices();
            var dictionary = provider.GetRequiredService<IKeyedServiceDictionary<PizzaClient, IPizzaClient>>();

            dictionary[pizzaClient].Should().BeOfType(expectedType);
        }
    }
}