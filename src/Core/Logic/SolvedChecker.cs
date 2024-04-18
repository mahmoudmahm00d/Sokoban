using SokoFarm.Core.Models;

namespace SokoFarm.Core.Logic;

public static class SolvedChecker
{
    public static bool Solved(this State State)
    {
        if (State is null || State.Grid is null || State.Grid.Cells is null)
        {
            return false;
        }

        foreach (var cell in State.Grid.Cells)
        {
            if (cell.Type == CellType.Seed)
            {
                return false;
            }
        }

        return true;
    }
}
