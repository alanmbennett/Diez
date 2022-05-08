namespace Diez.Tests.TestServices
{
    public interface IPizzaClient { }

    public class PapaJohnsClient : IPizzaClient { }

    public class LittleCaesarsClient : IPizzaClient { }

    public class PizzaHutClient : IPizzaClient { }

    public enum PizzaClient
    {
        PapaJohns,
        LittleCaesars,
        PizzaHut
    }
}