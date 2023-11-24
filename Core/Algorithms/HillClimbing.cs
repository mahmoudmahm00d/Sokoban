using SokoFarm.Core.Actions;
using SokoFarm.Core.Interfaces;
using SokoFarm.Core.Logic;
using SokoFarm.Core.Models;
using static SokoFarm.Core.Actions.Actions;

namespace SokoFarm.Core.Algorithms;

public class HillClimbing
{
    public static State Start(State state, IRenderer renderer = null)
    {
        var current = state;
        int currentHeuristic = int.MaxValue;

        while (!current.Solved())
        {
            var neighbors = GetPossibleStates(current);

            foreach (var item in neighbors)
            {
                int itemHeuristic = Heuristic.Custom(item);

                if (itemHeuristic <= currentHeuristic)
                {
                    current = item;
                    currentHeuristic = itemHeuristic;
                }
            }

            renderer?.ClearPreviousState();
            renderer?.Display(current);
        }

        return current;
    }
}
