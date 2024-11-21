using SokoFarm.Core.Models;
using Spectre.Console;

namespace SokoFarm.Core.Interfaces;

public interface IRenderer
{
    void ClearPreviousState();
    void Display(State grid);
    void DisplayMessage(string message);
    void DisplayAllPath(State grid);
    void DisplayAlgorithmExecutionStatistics(State grid, HashSet<State> visited, long elapsedTime);
}
