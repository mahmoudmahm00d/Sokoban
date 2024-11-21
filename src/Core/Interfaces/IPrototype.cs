namespace SokoFarm.Core.Interfaces;

public interface IPrototype<out T>
{
    T Clone();
}
