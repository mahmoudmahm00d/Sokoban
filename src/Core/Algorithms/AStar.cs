using Sokoban.Core.Actions;
using Sokoban.Core.Interfaces;
using Sokoban.Core.Logic;
using Sokoban.Core.Models;
using static Sokoban.Core.Actions.Actions;

namespace Sokoban.Core.Algorithms;

public class AStar : SokobanSearchAlgorithm
{
    public override Tuple<State, HashSet<State>> Start(
        State start,
        IRenderer renderer = null,
        CancellationTokenSource token = null
    )
    {
        PriorityQueue<State, int> queue = new();
        HashSet<State> visited = [];
        if (start.Solved())
        {
            return new Tuple<State, HashSet<State>>(start, visited);
        }

        queue.Enqueue(start, start.Cost + Heuristic.Custom(start));
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
                if (visited.Contains(neighbor))
                {
                    continue;
                }

                if (neighbor.Solved())
                {
                    renderer?.ClearPreviousState();
                    renderer?.Display(neighbor);
                    return new Tuple<State, HashSet<State>>(neighbor, visited);
                }

                var newCost = Heuristic.Custom(currentState);
                if (newCost != int.MaxValue)
                {
                    newCost += currentState.Cost;
                }

                neighbor.Cost = newCost;
                queue.Enqueue(neighbor, newCost);
            }
        }

        return null;
    }
}
