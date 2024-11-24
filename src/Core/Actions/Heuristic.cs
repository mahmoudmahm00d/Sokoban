using Sokoban.Core.Algorithms;
using Sokoban.Core.Logic;
using Sokoban.Core.Models;
using static Sokoban.Core.Actions.Actions;

namespace Sokoban.Core.Actions;

public static class Heuristic
{
    public static int ManhattanDistance(State currentState)
    {
        var distance = 0;
        var storages = GetStoragesPositions(currentState);
        var seeds = GetSeedsPositions(currentState);

        for (var seedIndex = 0; seedIndex < seeds.Count; seedIndex++)
        {
            var seed = seeds[seedIndex];
            var storage = storages[seedIndex];

            var xDistance = Math.Abs(storage.X - seed.X);
            var yDistance = Math.Abs(storage.Y - seed.Y);

            distance += xDistance + yDistance;
        }

        return distance;
    }

    public static int Custom(State currentState)
    {
        if (currentState.Trapped())
        {
            return int.MaxValue;
        }

        if (currentState.HasDeadlock())
        {
            return int.MaxValue;
        }

        var distanceCost = 0;
        var storages = GetStoragesPositions(currentState);
        var seeds = GetSeedsPositions(currentState);

        for (var seedIndex = 0; seedIndex < seeds.Count; seedIndex++)
        {
            var box = storages[seedIndex];
            var goal = seeds[seedIndex];

            var xDistance = Math.Abs(box.X - goal.X);
            var yDistance = Math.Abs(box.Y - goal.Y);

            distanceCost += xDistance + yDistance;
        }

        foreach (var goal in seeds)
        {
            var xDistance = Math.Abs(currentState.Player.X - goal.X);
            var yDistance = Math.Abs(currentState.Player.Y - goal.Y);

            distanceCost += xDistance + yDistance;
        }

        return distanceCost;
    }
}
