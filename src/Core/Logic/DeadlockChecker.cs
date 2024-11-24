using Sokoban.Core.Enums;
using Sokoban.Core.Models;
using static Sokoban.Core.Actions.Actions;

namespace Sokoban.Core.Logic;

public static class DeadlockChecker
{
    public static bool HasDeadlock(this State state)
    {
        ArgumentNullException.ThrowIfNull(state);

        if (state?.Grid?.Cells is null)
        {
            throw new ArgumentException(nameof(state.Grid.Cells));
        }

        return SquareDeadlock(state) || TowOnWallDeadlock(state);
    }

    private static bool SquareDeadlock(State state)
    {
        for (var y = 0; y < state.Grid.Cells.GetLength(0); y++)
        {
            for (var x = 0; x < state.Grid.Cells.GetLength(1); x++)
            {
                var cell = state.Grid.Cells[y, x];
                if (cell.Type != CellType.Box)
                {
                    continue;
                }

                var rightPosition = NextPosition(
                    new Position { X = cell.X + 1, Y = cell.Y },
                    Direction.Right
                );
                var downPosition = NextPosition(
                    new Position { X = cell.X, Y = cell.Y + 1 },
                    Direction.Right
                );
                var rightDownPosition = NextPosition(
                    new Position { X = cell.X + 1, Y = cell.Y + 1 },
                    Direction.Right
                );

                var withinTheGrid =
                    WithinTheGrid(state, rightPosition)
                    && WithinTheGrid(state, downPosition)
                    && WithinTheGrid(state, rightDownPosition);

                if (!withinTheGrid)
                {
                    continue;
                }

                var downCell = state.Grid.Cells[y + 1, x];
                var rightCell = state.Grid.Cells[y, x + 1];
                var rightDownCell = state.Grid.Cells[y + 1, x + 1];

                // $$
                // $$
                if (
                    cell.Type == downCell.Type
                    && cell.Type == rightCell.Type
                    && cell.Type == rightDownCell.Type
                )
                {
                    return true;
                }
            }
        }

        return false;
    }

    private static bool TowOnWallDeadlock(State state)
    {
        for (var y = 0; y < state.Grid.Cells.GetLength(0); y++)
        {
            for (var x = 0; x < state.Grid.Cells.GetLength(1); x++)
            {
                var cell = state.Grid.Cells[y, x];
                if (cell.Type != CellType.Box && cell.Type != CellType.Rock)
                {
                    continue;
                }

                var rightPosition = NextPosition(
                    new Position { X = cell.X + 1, Y = cell.Y },
                    Direction.Right
                );
                var downPosition = NextPosition(
                    new Position { X = cell.X, Y = cell.Y + 1 },
                    Direction.Right
                );
                var rightDownPosition = NextPosition(
                    new Position { X = cell.X + 1, Y = cell.Y + 1 },
                    Direction.Right
                );

                var withinTheGrid =
                    WithinTheGrid(state, rightPosition)
                    && WithinTheGrid(state, downPosition)
                    && WithinTheGrid(state, rightDownPosition);

                if (!withinTheGrid)
                {
                    continue;
                }

                var downCell = state.Grid.Cells[y + 1, x];
                var rightCell = state.Grid.Cells[y, x + 1];
                var rightDownCell = state.Grid.Cells[y + 1, x + 1];

                // #$
                // #$
                if (
                    cell.Type == downCell.Type
                    && cell.Type == CellType.Rock
                    && rightDownCell.Type == rightCell.Type
                    && rightDownCell.Type == CellType.Box
                )
                {
                    return true;
                }

                // $#
                // $#
                if (
                    cell.Type == downCell.Type
                    && cell.Type == CellType.Box
                    && rightDownCell.Type == rightCell.Type
                    && rightDownCell.Type == CellType.Rock
                )
                {
                    return true;
                }

                // $$
                // ##
                if (
                    cell.Type == rightCell.Type
                    && cell.Type == CellType.Box
                    && rightDownCell.Type == downCell.Type
                    && rightDownCell.Type == CellType.Rock
                )
                {
                    return true;
                }

                // ##
                // $$
                if (
                    cell.Type == rightCell.Type
                    && cell.Type == CellType.Rock
                    && rightDownCell.Type == downCell.Type
                    && rightDownCell.Type == CellType.Box
                )
                {
                    return true;
                }
            }
        }

        return false;
    }
}
