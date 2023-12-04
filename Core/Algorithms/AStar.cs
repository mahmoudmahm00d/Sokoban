using SokoFarm.Core.Actions;
using SokoFarm.Core.Interfaces;
using SokoFarm.Core.Logic;
using SokoFarm.Core.Models;
using static SokoFarm.Core.Actions.Actions;

namespace SokoFarm.Core.Algorithms;

public class AStar : SokobanSearchAlgorithm
{
    public override Tuple<State, HashSet<State>> Start(State start, IRenderer renderer = null)
    {
        PriorityQueue<State, int> queue = new();
        HashSet<State> visited = new();
        if (start.Solved())
        {
            return new Tuple<State, HashSet<State>>(start, visited);
        }
        
        queue.Enqueue(start, start.Cost + Heuristic.Custom(start));
        while (queue.Count > 0)
        {
            State currentState = queue.Dequeue();
            visited.Add(currentState);

            renderer?.ClearPreviousState();
            renderer?.Display(currentState);

            foreach (State neighbor in GetPossibleStates(currentState))
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
