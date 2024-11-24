using Sokoban.Core.Interfaces;
using Sokoban.Core.Logic;
using Sokoban.Core.Models;
using static Sokoban.Core.Actions.Actions;

namespace Sokoban.Core.Algorithms;

/// <summary>
/// Uniform Cost Search class
/// </summary>
class UCS : SokobanSearchAlgorithm
{
    public override Tuple<State, HashSet<State>> Start(
        State start,
        IRenderer renderer = null,
        CancellationTokenSource token = null
    )
    {
        PriorityQueue<State, int> queue = new();
        queue.Enqueue(start, 0);
        HashSet<State> visited = [];

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

            foreach (var neighbor in GetPossibleStates(currentState))
            {
                var previouslyVisited = visited.FirstOrDefault(item => item.Equals(neighbor));

                if (previouslyVisited is not null && neighbor.Cost <= previouslyVisited.Cost)
                {
                    continue;
                }

                if (neighbor.Solved())
                {
                    renderer?.ClearPreviousState();
                    renderer?.Display(neighbor);
                    return new Tuple<State, HashSet<State>>(neighbor, visited);
                }

                int newCost = currentState.Cost + neighbor.Cost;
                neighbor.Cost = newCost;
                queue.Enqueue(neighbor, newCost);
            }
        }

        return null;
    }
}
