using SokoFarm.Core.Actions;
using SokoFarm.Core.Interfaces;
using SokoFarm.Core.Logic;
using SokoFarm.Core.Models;
using static SokoFarm.Core.Actions.Actions;

namespace SokoFarm.Core.Algorithms;

public class HillClimbing : SokobanSearchAlgorithm
{
    public override Tuple<State, HashSet<State>> Start(
        State state,
        IRenderer renderer = null,
        CancellationTokenSource token = null
    )
    {
        var current = state;
        var currentHeuristic = Heuristic.Custom(current);

        while (true)
        {
            var neighbors = GetPossibleStates(current);
            var smallestLocal = neighbors.FirstOrDefault();
            var smallestHeuristic = Heuristic.Custom(smallestLocal);

            foreach (var item in neighbors)
            {
                var itemHeuristic = Heuristic.Custom(item);

                if (itemHeuristic > smallestHeuristic)
                    continue;
                smallestHeuristic = itemHeuristic;
                smallestLocal = item;
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

            if (token?.IsCancellationRequested ?? false)
            {
                return new Tuple<State, HashSet<State>>(current, []);
            }
        }
    }
}
