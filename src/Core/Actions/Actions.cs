using Sokoban.Core.Algorithms;
using Sokoban.Core.Enums;
using Sokoban.Core.Logic;
using Sokoban.Core.Models;

namespace Sokoban.Core.Actions;

public static class Actions
{
    public static bool IsGameOver { get; private set; } = false;

    public static bool CanMove(State state, Direction direction)
    {
        var nextPosition = NextPosition(state.Player, direction);
        var nextCell = GetCell(state, nextPosition);

        var nextBeyondPosition = NextBeyondPosition(state.Player, direction);
        var nextBeyondCell = GetCell(state, nextBeyondPosition);

        return nextCell.Type switch
        {
            CellType.Empty or CellType.Storage => true,
            CellType.BoxOnStorage
                when nextBeyondCell?.Type is CellType.Storage or CellType.Empty => true,
            CellType.Box when nextBeyondCell?.Type is CellType.Empty or CellType.Storage => true,
            _ => false,
        };
    }

    public static State Move(State state, Direction direction)
    {
        if (!CanMove(state, direction))
        {
            return state;
        }

        var newState = state.Clone();
        newState.PreviousState = state;

        var currentCell = GetCell(newState, newState.Player);
        var nextPosition = NextPosition(newState.Player, direction);
        var nextCell = GetCell(newState, nextPosition);
        var beyondPosition = NextBeyondPosition(newState.Player, direction);
        var beyondCell = GetCell(newState, beyondPosition);

        if (nextCell.Type == CellType.Empty && currentCell.Type == CellType.PlayerOnStorage)
        {
            nextCell.Type = CellType.Player;
            currentCell.Type = CellType.Storage;
        }
        else if (nextCell.Type == CellType.Empty)
        {
            Swap(nextCell, currentCell);
        }
        else if (nextCell.Type == CellType.Storage && currentCell.Type == CellType.Player)
        {
            nextCell.Type = CellType.PlayerOnStorage;
            currentCell.Type = CellType.Empty;
        }
        else if (nextCell.Type == CellType.Storage && currentCell.Type == CellType.PlayerOnStorage)
        {
            nextCell.Type = CellType.PlayerOnStorage;
            currentCell.Type = CellType.Storage;
        }
        else if (nextCell.Type == CellType.Storage)
        {
            nextCell.Type = CellType.PlayerOnStorage;
            currentCell.Type = CellType.Empty;
        }
        else if (nextCell.Type == CellType.Box && beyondCell is null)
        {
            return state;
        }
        else if (nextCell.Type == CellType.Box && beyondCell.Type == CellType.Empty)
        {
            Swap(nextCell, beyondCell);
            if (currentCell.Type == CellType.PlayerOnStorage)
            {
                currentCell.Type = CellType.Storage;
                nextCell.Type = CellType.Player;
            }
            else
            {
                Swap(currentCell, nextCell);
            }
        }
        else if (nextCell.Type == CellType.Box && beyondCell.Type == CellType.Storage)
        {
            beyondCell.Type = CellType.BoxOnStorage;
            nextCell.Type = CellType.Empty;
            if (currentCell.Type == CellType.PlayerOnStorage)
            {
                currentCell.Type = CellType.Storage;
                nextCell.Type = CellType.Player;
            }
            else
            {
                Swap(currentCell, nextCell);
            }
        }
        else if (nextCell.Type == CellType.Box)
        {
            return state;
        }
        else if (
            nextCell.Type == CellType.BoxOnStorage
            && beyondCell?.Type is CellType.Storage or CellType.Empty
        )
        {
            if (beyondCell.Type == CellType.Empty)
            {
                beyondCell.Type = CellType.Box;
            }
            else
            {
                Swap(beyondCell, nextCell);
            }

            nextCell.Type = CellType.PlayerOnStorage;
            if (currentCell.Type == CellType.PlayerOnStorage)
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

        newState.Player = nextPosition;
        newState.IsCurrentLevelSolved = newState.Solved();
        newState.IsCurrentLevelSolvable =
            !newState.IsHumanPlayer || (!newState.Trapped() && !newState.HasDeadlock());
        return newState;
    }

    public static List<Direction> GetDirections()
    {
        return new() { Direction.Up, Direction.Right, Direction.Down, Direction.Left };
    }

    public static ICollection<State> GetPossibleStates(State currentState)
    {
        List<State> possibleStates = [];

        var directions = GetDirections();

        foreach (var direction in directions)
        {
            if (CanMove(currentState, direction))
            {
                possibleStates.Add(Move(currentState, direction));
            }
        }

        return possibleStates;
    }

    public static IList<Position> GetStoragesPositions(State currentState)
    {
        List<Position> storagesPositions = [];

        foreach (var cell in currentState.Grid.Cells)
        {
            if (cell.Type is CellType.Storage or CellType.PlayerOnStorage)
            {
                storagesPositions.Add(new Position { X = cell.X, Y = cell.Y });
            }
        }

        return storagesPositions;
    }

    public static IList<Position> GetSeedsPositions(State currentState)
    {
        List<Position> storagesPositions = [];

        foreach (var cell in currentState.Grid.Cells)
        {
            if (cell.Type == CellType.Box)
            {
                storagesPositions.Add(new Position { X = cell.X, Y = cell.Y });
            }
        }

        return storagesPositions;
    }

    private static void Swap(Cell cellA, Cell cellB)
    {
        (cellB.Type, cellA.Type) = (cellA.Type, cellB.Type);
    }

    public static Position NextPosition(Position position, Direction direction)
    {
        var dx = position.X;
        var dy = position.Y;

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
        var dx = position.X;
        var dy = position.Y;

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
        IsGameOver = true;
    }

    public static bool WithinTheGrid(State state, Position position)
    {
        var width = state.Grid.Cells.GetLength(1);
        var height = state.Grid.Cells.GetLength(0);

        if (position.X < 0 || width <= position.X)
        {
            return false;
        }

        return position.Y >= 0 && height > position.Y;
    }

    public static Cell GetCell(State state, Position position)
    {
        return WithinTheGrid(state, position) ? state.Grid.Cells[position.Y, position.X] : null;
    }
}
