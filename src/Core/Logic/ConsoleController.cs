using SokoFarm.Core.Interfaces;
using SokoFarm.Presentation;

namespace SokoFarm.Core.Logic;

public class ConsoleController : IController
{
    public IRenderer Renderer { get; } = new ConsoleRenderer();

    public IPlayerInput PlayerInput { get; } = new ConsolePlayerInput();
}
