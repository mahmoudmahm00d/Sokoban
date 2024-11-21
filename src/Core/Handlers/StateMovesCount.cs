using SokoFarm.Core.Models;

namespace SokoFarm.Core.Handlers;

public static class StateMovesCount
{
    public static int MovesCount(this State state)
    {
        var count = 0;
        while (state != null)
        {
            state = state.PreviousState;
            count++;
        }

        return count;
    }
}
