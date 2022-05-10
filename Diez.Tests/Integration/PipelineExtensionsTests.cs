using Diez.Extensions;
using Diez.Keyed;
using Diez.Pipelines;
using Diez.Tests.TestServices;
using Diez.Tests.TestServices.Steps;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Diez.Tests.Integration
{
    public class PipelineExtensionsTests
    {  
        [Fact]
        public void ItWillExecuteEntirePipeline()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddPipeline<TestModel>(
                ServiceLifetime.Transient,
                registry => 
                {
                    registry.AddTransient<FirstStep>();
                    registry.AddTransient<SecondStep>();
                    registry.AddTransient<ThirdStep>();
                }
            );
            var provider = serviceCollection.BuildServiceProvider();

            var pipeline = provider.GetRequiredService<IPipeline<TestModel>>();
            var model = pipeline.Start(new());

            model.Should().BeEquivalentTo(new TestModel
            {
                Text = "First step executed Second step executed Third step executed "
            });
        }

        [Fact]
        public void ItWillExecuteKeyedPipeline()
        {
            var serviceCollection = new ServiceCollection();

            const string firstPipelineKey = "FirstTwo";
            serviceCollection.AddKeyedPipelines<string, TestModel>(registry => 
            {
                registry.AddPipeline(
                    firstPipelineKey,
                    ServiceLifetime.Transient,
                    pipelineRegistry => 
                    {
                        pipelineRegistry.AddTransient<FirstStep>();
                        pipelineRegistry.AddTransient<SecondStep>();
                    }
                );

                registry.AddPipeline(
                    "LastOne",
                    ServiceLifetime.Transient,
                    pipelineRegistry => 
                    {
                        pipelineRegistry.AddTransient<ThirdStep>();
                    }
                );
            });

            var provider = serviceCollection.BuildServiceProvider();

            var dictionary = provider.GetRequiredService<IKeyedServiceDictionary<string, IPipeline<TestModel>>>();
            var model = dictionary[firstPipelineKey].Start(new());

            model.Should().BeEquivalentTo(new TestModel
            {
                Text = "First step executed Second step executed "
            });
        }
    }
}