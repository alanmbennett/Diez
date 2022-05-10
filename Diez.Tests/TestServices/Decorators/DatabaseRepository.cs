namespace Diez.Tests.TestServices.Decorators
{
    public class DatabaseRepository : IRepository
    {
        public TestModel Get()
        {
            return new TestModel
            {
                Text = "DB call -> "
            };
        }
    }
}