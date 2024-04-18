using SokoFarm.Core.Enums;
using SokoFarm.Core.Models;
using static SokoFarm.Core.Actions.Actions;

namespace SokoFarm.Core.Logic;

public static class DeadlockChecker
{
    public static bool HasDeadlock(this State state)
    {
        if (state is null)
        {
            throw new ArgumentNullException(nameof(state));
        }

        if (state?.Grid?.Cells is null)
        {
            throw new ArgumentException(nameof(state.Grid.Cells));
        }

        return SquareDeadlock(state) || TowOnWallDeadlock(state);
    }

    public static bool SquareDeadlock(State state)
    {
        for (int y = 0; y < state.Grid.Cells.GetLength(0); y++)
        {
            for (int x = 0; x < state.Grid.Cells.GetLength(1); x++)
            {
                var cell = state.Grid.Cells[y, x];
                if (cell.Type != CellType.Seed)
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

    public static bool TowOnWallDeadlock(State state)
    {
        for (int y = 0; y < state.Grid.Cells.GetLength(0); y++)
        {
            for (int x = 0; x < state.Grid.Cells.GetLength(1); x++)
            {
                var cell = state.Grid.Cells[y, x];
                if (cell.Type != CellType.Seed && cell.Type != CellType.Rock)
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
                    && rightDownCell.Type == CellType.Seed
                )
                {
                    return true;
                }

                // $#
                // $#
                if (
                    cell.Type == downCell.Type
                    && cell.Type == CellType.Seed
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
                    && cell.Type == CellType.Seed
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
                    && rightDownCell.Type == CellType.Seed
                )
                {
                    return true;
                }
            }
        }

        return false;
    }
}
