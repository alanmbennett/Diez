namespace Diez.Decorators
{
    public interface IDecoratorRegistry<TService>
    {
        void Add<TDecorator>() where TDecorator : notnull, TService;
    }
}