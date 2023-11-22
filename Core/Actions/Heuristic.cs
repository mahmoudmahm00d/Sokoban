using SokoFarm.Core.Algorithms;
using SokoFarm.Core.Models;
using static SokoFarm.Core.Actions.Actions;

namespace SokoFarm.Core.Actions;

public class Heuristic
{
    public static int ManhattanDistance(State currentState)
    {
        int distance = 0;
        var storages = GetStoragesPositions(currentState);
        var seeds = GetSeedsPositions(currentState);

        for (int seedIndex = 0; seedIndex < seeds.Count; seedIndex++)
        {
            var seed = seeds[seedIndex];
            var storage = storages[seedIndex];

            int xDistance = Math.Abs(storage.X - seed.X);
            int yDistance = Math.Abs(storage.Y - seed.Y);

            distance += xDistance + yDistance;
        }

        return distance;
    }

    public static int Custom(State currentState)
    {
        if (!currentState.IsCurrentLevelSolvable)
        {
            return int.MaxValue;
        }

        int distanceCost = 0;
        var storages = GetStoragesPositions(currentState);
        var seeds = GetSeedsPositions(currentState);

        for (int seedIndex = 0; seedIndex < seeds.Count; seedIndex++)
        {
            var box = storages[seedIndex];
            var goal = seeds[seedIndex];

            int xDistance = Math.Abs(box.X - goal.X);
            int yDistance = Math.Abs(box.Y - goal.Y);

            distanceCost += xDistance + yDistance;
        }

        for (int seedIndex = 0; seedIndex < seeds.Count; seedIndex++)
        {
            var goal = seeds[seedIndex];

            int xDistance = Math.Abs(currentState.Farmer.X - goal.X);
            int yDistance = Math.Abs(currentState.Farmer.Y - goal.Y);

            distanceCost += xDistance + yDistance;
        }

        return distanceCost;
    }
}
