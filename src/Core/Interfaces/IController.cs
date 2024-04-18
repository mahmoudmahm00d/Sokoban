namespace SokoFarm.Core.Interfaces;

public interface IController
{
    IRenderer Renderer { get; }
    IPlayerInput PlayerInput { get; }
}
