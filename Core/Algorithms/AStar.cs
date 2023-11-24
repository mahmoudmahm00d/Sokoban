using SokoFarm.Core.Actions;
using SokoFarm.Core.Interfaces;
using SokoFarm.Core.Logic;
using SokoFarm.Core.Models;
using static SokoFarm.Core.Actions.Actions;

namespace SokoFarm.Core.Algorithms;

public class AStar
{
    public static State Start(State start, IRenderer renderer = null)
    {
        PriorityQueue<State, int> queue = new();
        queue.Enqueue(start, 0);
        Dictionary<State, int> visited = new();

        while (queue.Count > 0)
        {
            State currentState = queue.Dequeue();

            renderer?.ClearPreviousState();
            renderer?.Display(currentState);

            if (currentState.Solved())
            {
                return currentState;
            }

            visited[currentState] = currentState.Cost + Heuristic.Custom(currentState);

            foreach (State neighbor in GetPossibleStates(currentState))
            {
                if (visited.ContainsKey(neighbor))
                {
                    continue;
                }

                int newCost = currentState.Cost + Heuristic.Custom(currentState);
                neighbor.Cost = newCost;
                queue.Enqueue(neighbor, newCost);
            }
        }

        return null;
    }
}