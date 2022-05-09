using Diez.Pipelines;

namespace Diez.Tests.TestServices.Steps
{
    public class FirstStep : IPipelineStep<TestModel>
    {
        public TestModel Execute(TestModel model)
        {
            model.Text += "First step executed ";
            return model;
        }
    }
}