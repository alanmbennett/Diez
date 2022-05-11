using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Diez.Extensions;
using Diez.Tests.TestServices;
using Diez.Tests.TestServices.Decorators;
using Diez.Tests.TestServices.Modules;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Diez.Tests.Integration
{
    public class ModuleExtensionsTests
    { 
        [Fact]
        public void ItWillRegisterModuleByInstance()
        {
            var serviceCollection = new ServiceCollection(); 
            serviceCollection.RegisterModule(new PizzaModule());
            var provider = serviceCollection.BuildServiceProvider();
            
            var service = provider.GetRequiredService<IPizzaClient>();
            service.Should().BeOfType<PizzaHutClient>();
        }

        [Fact]
        public void ItWillRegisterModule()
        {
            var serviceCollection = new ServiceCollection(); 
            serviceCollection.RegisterModule<PizzaModule>();
            var provider = serviceCollection.BuildServiceProvider();
            
            var service = provider.GetRequiredService<IPizzaClient>();
            service.Should().BeOfType<PizzaHutClient>();
        }

        [Fact]
        public void ItWillRegisterAssemblyModules()
        {
            var serviceCollection = new ServiceCollection(); 
            serviceCollection.RegisterModules(Assembly.GetExecutingAssembly());
            var provider = serviceCollection.BuildServiceProvider();
            
            var service1 = provider.GetRequiredService<IEnumerable<IPizzaClient>>();
            var service2 = provider.GetRequiredService<IRepository>();

            service1.Select(service => service.GetType())
                .Should().BeEquivalentTo(new[]
                {
                    typeof(PapaJohnsClient),
                    typeof(LittleCaesarsClient),
                    typeof(PizzaHutClient)
                });

            service2.Should().BeOfType<DatabaseRepository>();
        }
    }
}