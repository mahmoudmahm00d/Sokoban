using SokoFarm.Core.Interfaces;
using SokoFarm.Core.Logic;
using SokoFarm.Core.Models;
using static SokoFarm.Core.Actions.Actions;

namespace SokoFarm.Core.Algorithms;

/// <summary>
/// Breadth-First Search class
/// </summary>
public class BFS : SokobanSearchAlgorithm
{
    public override Tuple<State, HashSet<State>> Start(State state, IRenderer renderer = null, CancellationTokenSource token = null)
    {
        var visited = new HashSet<State>();
        var queue = new Queue<State>();

        // If this state is solved, reconstruct and return the path
        if (state.Solved())
        {
            return new Tuple<State, HashSet<State>>(state, visited);
        }

        queue.Enqueue(state);

        while (queue.Count > 0)
        {
            var currentState = queue.Dequeue();
            visited.Add(currentState);

            renderer?.ClearPreviousState();
            renderer?.Display(currentState);
            
            if (token?.IsCancellationRequested ?? false)
            {
                return new Tuple<State, HashSet<State>>(currentState, visited);
            }

            // Otherwise, add all possible next states to the queue
            foreach (var nextState in GetPossibleStates(currentState))
            {
                if (visited.Contains(nextState))
                {
                    continue;
                }

                // If this state is solved, reconstruct and return the path
                if (nextState.Solved())
                {
                    renderer?.ClearPreviousState();
                    renderer?.Display(nextState);
                    return new Tuple<State, HashSet<State>>(currentState, visited);
                }

                queue.Enqueue(nextState);
            }
        }

        // If we've exhausted all possible states without finding a solution, return null
        return null;
    }
}
