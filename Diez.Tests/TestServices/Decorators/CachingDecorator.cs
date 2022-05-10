namespace Diez.Tests.TestServices.Decorators
{
    public class CachingDecorator : IRepository
    {  
        private readonly IRepository _decorated;
        
        public CachingDecorator(IRepository decorated)
        {
            _decorated = decorated;
        }

        public TestModel Get()
        {
            var model = _decorated.Get();
            model.Text += "Caching call -> ";
            return model;
        }
    }
}