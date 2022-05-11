using Diez.Pipelines;

namespace Diez.Tests.TestServices.Steps
{
    public class ExitStep : IExitablePipelineStep, IPipelineStep<TestModel>
    {  
        public bool ExitPipeline { get; set; }

        public TestModel Execute(TestModel model)
        {
            ExitPipeline = true;
            model.Text += "EXIT";
            return model;
        }
    }
}