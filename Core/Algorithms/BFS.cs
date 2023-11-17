using SokoFarm.Core.Interfaces;
using SokoFarm.Core.Logic;
using SokoFarm.Core.Models;
using static SokoFarm.Core.Actions.Actions;

namespace SokoFarm.Core.Algorithms;

/// <summary>
/// Breadth-First Search class
/// </summary>
public class BFS
{
    public static State Start(State state, IRenderer renderer = null)
    {
        var visited = new HashSet<State>();
        var queue = new Queue<State>();

        queue.Enqueue(state);

        while (queue.Count > 0)
        {
            var currentState = queue.Dequeue();
            // If we've already visited this state, skip it
            if (visited.Contains(currentState))
            {
                continue;
            }

            visited.Add(currentState);

            renderer?.ClearPreviousState();
            renderer?.Display(currentState);

            // If this state is solved, reconstruct and return the path
            if (currentState.Solved())
            {
                return currentState;
            }

            if (currentState.Trapped())
            {
                continue;
            }

            // Otherwise, add all possible next states to the queue
            foreach (var nextState in GetPossibleStates(currentState))
            {
                queue.Enqueue(nextState);
            }
        }

        // If we've exhausted all possible states without finding a solution, return null
        return null;
    }
}
