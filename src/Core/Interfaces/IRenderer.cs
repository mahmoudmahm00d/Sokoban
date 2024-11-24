using SokoFarm.Core.Models;
using SokoFarm.Presentation;

namespace SokoFarm.Core.Interfaces;

public interface IRenderer
{
    void ClearPreviousState();
    void Display(State grid);
    void DisplayMessage(string message);
    void DisplayAllPath(State grid, Delay delay, CancellationTokenSource cancellationToken = null);
    void DisplayAlgorithmExecutionStatistics(State grid, HashSet<State> visited, long elapsedTime);
}
