using Sokoban.Core.Models;
using static Sokoban.Core.Actions.Actions;

namespace Sokoban.Core.Logic;

public static class TrappedChecker
{
    public static bool Trapped(this State state)
    {
        if (state?.Grid?.Cells is null)
        {
            return false;
        }

        for (var y = 0; y < state.Grid.Cells.GetLength(0); y++)
        {
            for (var x = 0; x < state.Grid.Cells.GetLength(1); x++)
            {
                var cell = state.Grid.Cells[y, x];
                if (cell.Type != CellType.Box)
                {
                    continue;
                }

                List<Cell> neighbors = [];
                foreach (var direction in GetDirections())
                {
                    var position = NextPosition(new Position { X = cell.X, Y = cell.Y }, direction);
                    var withinTheGrid = WithinTheGrid(state, position);

                    if (!withinTheGrid)
                    {
                        continue;
                    }

                    neighbors.Add(GetCell(state, position));
                }

                if (neighbors.Count < 2)
                {
                    continue;
                }

                if (neighbors[0].Type == neighbors[3].Type && neighbors[0].Type == CellType.Rock)
                {
                    return true;
                }

                if (neighbors[0].Type == neighbors[1].Type && neighbors[0].Type == CellType.Rock)
                {
                    return true;
                }

                if (neighbors[1].Type == neighbors[2].Type && neighbors[1].Type == CellType.Rock)
                {
                    return true;
                }

                if (neighbors[2].Type == neighbors[3].Type && neighbors[2].Type == CellType.Rock)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
