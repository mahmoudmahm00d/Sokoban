using Sokofarm.Core.Models;

namespace Sokofarm.Core.Interfaces;

public interface IRenderer
{
    void ClearPreviousState();
    void Display(State grid);
}
