using Diez.Pipelines;

namespace Diez.Tests.TestServices.Steps
{
    public class SecondStep : IPipelineStep<TestModel>
    {
        public TestModel Execute(TestModel model)
        {
            model.Text += "Second step executed ";
            return model;
        }
    }
}