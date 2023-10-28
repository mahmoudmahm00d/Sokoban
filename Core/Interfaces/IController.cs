using Sokofarm.Core.Logic;
using Sokofarm.Presentation;

namespace Sokofarm.Core.Interfaces;

public interface IController
{
    IRenderer Renderer { get; }
    IPlayerInput PlayerInput { get; }
}
