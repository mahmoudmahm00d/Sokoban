using SokoFarm.Core.Interfaces;
using SokoFarm.Presentation;

namespace SokoFarm.Core.Logic;

public class ConsoleController : IController
{
    private readonly IRenderer renderer;
    private readonly IPlayerInput playerInput;
    public IRenderer Renderer => renderer;

    public IPlayerInput PlayerInput => playerInput;

    public ConsoleController()
    {
        renderer = new ConsoleRenderer();
        playerInput = new ConsolePlayerInput();
    }
}
