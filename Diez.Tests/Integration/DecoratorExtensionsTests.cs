using System.Collections.Generic;
using System.Linq;
using Diez.Extensions;
using Diez.Tests.TestServices;
using Diez.Tests.TestServices.Decorators;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Diez.Tests.Integration
{
    public class DecoratorExtensionsTests
    {
        [Fact]
        public void ItWillDecorateServices()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddTransient<IRepository, DatabaseRepository>();
            serviceCollection.Decorate<IRepository>(registry =>
            {
                registry.Add<CachingDecorator>();
                registry.Add<LoggingDecorator>();
            });

            var provider = serviceCollection.BuildServiceProvider();

            var service = provider.GetRequiredService<IRepository>();
            var model = service.Get();

            model.Should().BeEquivalentTo(new TestModel
            {
                Text = "DB call -> Caching call -> Logging call -> "
            });
        }

        [Fact]
        public void ItWillDecorateMultipleServices()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddTransient<IRepository, DatabaseRepository>();
            serviceCollection.AddTransient<IRepository, AnotherRepository>();
            serviceCollection.Decorate<IRepository>(registry =>
            {
                registry.Add<CachingDecorator>();
                registry.Add<LoggingDecorator>();
            });

            var provider = serviceCollection.BuildServiceProvider();

            var services = provider.GetRequiredService<IEnumerable<IRepository>>();
            var model = services.Select(service => service.Get());

            model.Should().BeEquivalentTo(new TestModel[]
            {
                new TestModel
                {
                    Text = "DB call -> Caching call -> Logging call -> "
                },
                new TestModel
                {
                    Text = "Another DB call -> Caching call -> Logging call -> "
                }
            });
        }

        [Fact]
        public void ItWillOnlyDecorateImplementation()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddTransient<IRepository, DatabaseRepository>();
            serviceCollection.AddTransient<IRepository, AnotherRepository>();
            serviceCollection.Decorate<IRepository, DatabaseRepository>(registry =>
            {
                registry.Add<CachingDecorator>();
                registry.Add<LoggingDecorator>();
            });

            var provider = serviceCollection.BuildServiceProvider();

            var services = provider.GetRequiredService<IEnumerable<IRepository>>();
            var model = services.Select(service => service.Get());

            model.Should().BeEquivalentTo(new TestModel[]
            {
                new TestModel
                {
                    Text = "DB call -> Caching call -> Logging call -> "
                },
                new TestModel
                {
                    Text = "Another DB call -> "
                }
            });
        }
    }
}