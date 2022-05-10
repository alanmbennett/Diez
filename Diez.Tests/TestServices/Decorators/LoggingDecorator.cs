namespace Diez.Tests.TestServices.Decorators
{
    public class LoggingDecorator : IRepository
    {  
        private readonly IRepository _decorated;
        
        public LoggingDecorator(IRepository decorated)
        {
            _decorated = decorated;
        }

        public TestModel Get()
        {
            var model = _decorated.Get();
            model.Text += "Logging call -> ";
            return model;
        }
    }
}