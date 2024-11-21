using SokoFarm.Core.Models;

namespace SokoFarm.Core.Logic;

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
            if (cell.Type == CellType.Seed)
            {
                return false;
            }
        }

        return true;
    }
}
