using Sokoban.Core.Models;

namespace Sokoban.Core.Handlers;

public static class StateMovesCount
{
    public static int MovesCount(this State state)
    {
        var count = 0;
        while (state?.PreviousState != null)
        {
            state = state.PreviousState;
            count++;
        }

        return count;
    }
}
