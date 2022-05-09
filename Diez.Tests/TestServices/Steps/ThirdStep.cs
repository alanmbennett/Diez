using Diez.Pipelines;

namespace Diez.Tests.TestServices.Steps
{
    public class ThirdStep : IPipelineStep<TestModel>
    {
        public TestModel Execute(TestModel model)
        {
            model.Text += "Third step executed ";
            return model;
        }
    }
}