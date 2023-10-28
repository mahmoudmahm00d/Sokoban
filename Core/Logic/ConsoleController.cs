using Sokofarm.Core.Interfaces;
using Sokofarm.Presentation;

namespace Sokofarm.Core.Logic;

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
