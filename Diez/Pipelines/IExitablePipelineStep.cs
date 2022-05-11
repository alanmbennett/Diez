namespace Diez.Pipelines
{
    public interface IExitablePipelineStep
    { 
        public bool ExitPipeline { get; set; }
    }
}