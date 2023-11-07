using Sokofarm.Core.Enums;
using Sokofarm.Core.Interfaces;
using Sokofarm.Core.Logic;
using Sokofarm.Core.Models;

namespace Sokofarm.Core.Actions;

public static class Actions
{
    private static bool isGameOver = false;

    public static bool IsGameOver => isGameOver;

    public static bool CanMove(State state, Direction direction)
    {
        var nextPosition = NextPosition(state.Farmer, direction);
        var nextCell = GetCell(state, nextPosition);

        var nextBeyondPosition = NextBeyondPosition(state.Farmer, direction);
        var nextBeyondCell = GetCell(state, nextBeyondPosition);

        if (nextCell.Type == CellType.Empty)
        {
            return true;
        }

        if (nextCell.Type == CellType.Storage)
        {
            return true;
        }

        if (
            nextCell.Type == CellType.SeedOnStorage
            && nextBeyondCell is not null
            && nextBeyondCell.Type == CellType.Storage
        )
        {
            return true;
        }

        if (
            nextCell.Type == CellType.Seed
            && nextBeyondCell is not null
            && (nextBeyondCell.Type == CellType.Empty || nextBeyondCell.Type == CellType.Storage)
        )
        {
            return true;
        }

        return false;
    }

    public static State Move(State state, Direction direction)
    {
        if (!CanMove(state, direction))
        {
            return state;
        }

        var newState = state.Clone();

        var currentCell = GetCell(newState, newState.Farmer);
        var nextPosition = NextPosition(newState.Farmer, direction);
        var nextCell = GetCell(newState, nextPosition);
        var beyondPosition = NextBeyondPosition(newState.Farmer, direction);
        var beyondCell = GetCell(newState, beyondPosition);

        if (nextCell.Type == CellType.Empty)
        {
            if (currentCell.Type == CellType.FarmerOnStorage)
            {
                nextCell.Type = CellType.Farmer;
                currentCell.Type = CellType.Storage;
            }
            else
            {
                Swap(nextCell, currentCell);
            }
        }
        else if (nextCell.Type == CellType.Storage)
        {
            if (currentCell.Type == CellType.Farmer)
            {
                nextCell.Type = CellType.FarmerOnStorage;
                currentCell.Type = CellType.Empty;
            }
            else if (currentCell.Type == CellType.FarmerOnStorage)
            {
                nextCell.Type = CellType.FarmerOnStorage;
                currentCell.Type = CellType.Storage;
            }
            else
            {
                nextCell.Type = CellType.FarmerOnStorage;
                currentCell.Type = CellType.Empty;
            }
        }
        else if (nextCell.Type == CellType.Seed)
        {
            if (beyondCell is null)
            {
                return state;
            }

            if (beyondCell.Type == CellType.Empty)
            {
                Swap(nextCell, beyondCell);
                if (currentCell.Type == CellType.FarmerOnStorage)
                {
                    currentCell.Type = CellType.Storage;
                    nextCell.Type = CellType.Farmer;
                }
                else
                {
                    Swap(currentCell, nextCell);
                }
            }
            else if (beyondCell.Type == CellType.Storage)
            {
                beyondCell.Type = CellType.SeedOnStorage;
                nextCell.Type = CellType.Empty;
                Swap(currentCell, nextCell);
            }
            else
            {
                return state;
            }
        }
        else if (
            nextCell.Type == CellType.SeedOnStorage
            && beyondCell is not null
            && beyondCell.Type == CellType.Storage
        )
        {
            Swap(beyondCell, nextCell);
            nextCell.Type = CellType.FarmerOnStorage;
            if (currentCell.Type == CellType.FarmerOnStorage)
            {
                currentCell.Type = CellType.Storage;
            }
            else
            {
                currentCell.Type = CellType.Empty;
            }
        }
        else if (nextCell.Type == CellType.Rock)
        {
            Console.WriteLine();
        }
        else
        {
            return state;
        }

        newState.Farmer = nextPosition;
        newState.IsCurrentLevelSolved = newState.Solved();
        return newState;
    }

    private static void Swap(Cell cellA, Cell cellB)
    {
        (cellB.Type, cellA.Type) = (cellA.Type, cellB.Type);
    }

    private static Position NextPosition(Position position, Direction direction)
    {
        int dx = position.X;
        int dy = position.Y;

        switch (direction)
        {
            case Direction.Up:
                dy--;
                break;
            case Direction.Down:
                dy++;
                break;
            case Direction.Left:
                dx--;
                break;
            case Direction.Right:
                dx++;
                break;
            default:
                break;
        }

        return new Position(dx, dy);
    }

    private static Position NextBeyondPosition(Position position, Direction direction)
    {
        int dx = position.X;
        int dy = position.Y;

        switch (direction)
        {
            case Direction.Up:
                dy -= 2;
                break;
            case Direction.Down:
                dy += 2;
                break;
            case Direction.Left:
                dx -= 2;
                break;
            case Direction.Right:
                dx += 2;
                break;
            default:
                break;
        }

        return new Position(dx, dy);
    }

    public static void QuitGame()
    {
        isGameOver = true;
    }

    private static bool WithinTheGrid(State state, Position position)
    {
        var width = state.Grid.Cells.GetLength(1);
        var height = state.Grid.Cells.GetLength(0);

        if (position.X < 0 || width <= position.X)
        {
            return false;
        }

        if (position.Y < 0 || height <= position.Y)
        {
            return false;
        }

        return true;
    }

    private static Cell GetCell(State state, Position position)
    {
        return WithinTheGrid(state, position) ? state.Grid.Cells[position.Y, position.X] : null;
    }
}
