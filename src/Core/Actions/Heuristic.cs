using SokoFarm.Core.Algorithms;
using SokoFarm.Core.Logic;
using SokoFarm.Core.Models;
using static SokoFarm.Core.Actions.Actions;

namespace SokoFarm.Core.Actions;

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
            var xDistance = Math.Abs(currentState.Farmer.X - goal.X);
            var yDistance = Math.Abs(currentState.Farmer.Y - goal.Y);

            distanceCost += xDistance + yDistance;
        }

        return distanceCost;
    }
}
