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
        queue.Enqueue(start, start.Cost + Heuristic.Custom(start));
        HashSet<State> visited = new();

        while (queue.Count > 0)
        {
            State currentState = queue.Dequeue();

            renderer?.ClearPreviousState();
            renderer?.Display(currentState);

            if (currentState.Solved())
            {
                return new Tuple<State, HashSet<State>>(currentState, visited);
            }

            visited.Add(currentState);

            foreach (State neighbor in GetPossibleStates(currentState))
            {
                if (visited.Contains(neighbor))
                {
                    continue;
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
