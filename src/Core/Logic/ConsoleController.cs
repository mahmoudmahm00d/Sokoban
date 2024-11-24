using Sokoban.Core.Interfaces;
using Sokoban.Presentation;

namespace Sokoban.Core.Logic;

public class ConsoleController : IController
{
    public IRenderer Renderer { get; } = new ConsoleRenderer();

    public IPlayerInput PlayerInput { get; } = new ConsolePlayerInput();
}
