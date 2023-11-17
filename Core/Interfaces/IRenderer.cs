using SokoFarm.Core.Models;

namespace SokoFarm.Core.Interfaces;

public interface IRenderer
{
    void ClearPreviousState();
    void Display(State grid);
    void DisplayAllPath(State grid);
}
