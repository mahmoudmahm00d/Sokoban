using Sokofarm.Core.Models;

namespace Sokofarm.Core.Logic;

public static class SolvedChecker
{
    public static bool Solved(this Cell[,] cells)
    {
        foreach (var cell in cells)
        {
            if (cell.Type == CellType.Seed)
            {
                return false;
            }
        }

        return true;
    }
}
