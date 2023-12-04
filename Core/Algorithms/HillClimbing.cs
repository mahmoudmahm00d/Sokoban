using SokoFarm.Core.Actions;
using SokoFarm.Core.Interfaces;
using SokoFarm.Core.Logic;
using SokoFarm.Core.Models;
using static SokoFarm.Core.Actions.Actions;

namespace SokoFarm.Core.Algorithms;

public class HillClimbing : SokobanSearchAlgorithm
{
    public override Tuple<State, HashSet<State>> Start(State state, IRenderer renderer = null)
    {
        var current = state;
        int currentHeuristic = Heuristic.Custom(current);

        while (true)
        {
            var neighbors = GetPossibleStates(current);
            var smallestLocal = neighbors.FirstOrDefault();
            int smallestHeuristic = Heuristic.Custom(smallestLocal);

            foreach (var item in neighbors)
            {
                int itemHeuristic = Heuristic.Custom(item);

                if (itemHeuristic <= smallestHeuristic)
                {
                    smallestHeuristic = itemHeuristic;
                    smallestLocal = item;
                }
            }

            if (currentHeuristic <= smallestHeuristic)
            {
                renderer?.ClearPreviousState();
                renderer?.Display(current);
                return new Tuple<State, HashSet<State>>(current, null);
            }

            current = smallestLocal;
            currentHeuristic = smallestHeuristic;
            renderer?.ClearPreviousState();
            renderer?.Display(current);
        }
    }
}
