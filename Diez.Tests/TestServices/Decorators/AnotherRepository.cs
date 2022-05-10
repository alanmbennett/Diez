namespace Diez.Tests.TestServices.Decorators
{
    public class AnotherRepository : IRepository
    {
        public TestModel Get()
        {
            return new TestModel
            {
                Text = "Another DB call -> "
            };
        }
    }
}