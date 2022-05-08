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
        [Fact]
        public void ServiceProviderWillReturnKeyedServicesDictionary()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddKeyedServices<PizzaClient, IPizzaClient>(registry => 
            {
                registry.AddTransient<PapaJohnsClient>(PizzaClient.PapaJohns);
                registry.AddTransient<PizzaHutClient>(PizzaClient.PizzaHut);
                registry.AddTransient<LittleCaesarsClient>(PizzaClient.LittleCaesars);
            });

            var provider = serviceCollection.BuildServiceProvider();
            var dictionary = provider.GetRequiredService<IKeyedServiceDictionary<PizzaClient, IPizzaClient>>();

            dictionary.Values.Select(value => value.GetType())
                .Should().BeEquivalentTo(new[]
                {
                    typeof(PapaJohnsClient),
                    typeof(LittleCaesarsClient),
                    typeof(PizzaHutClient)
                });
        }
    }
}