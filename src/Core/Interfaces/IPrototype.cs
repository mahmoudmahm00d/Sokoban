namespace Sokoban.Core.Interfaces;

public interface IPrototype<out T>
{
    T Clone();
}
