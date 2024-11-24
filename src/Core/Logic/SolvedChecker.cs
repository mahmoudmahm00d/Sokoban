using Sokoban.Core.Models;

namespace Sokoban.Core.Logic;

public static class SolvedChecker
{
    public static bool Solved(this State state)
    {
        if (state?.Grid?.Cells is null)
        {
            return false;
        }

        foreach (var cell in state.Grid.Cells)
        {
            if (cell.Type == CellType.Box)
            {
                return false;
            }
        }

        return true;
    }
}
