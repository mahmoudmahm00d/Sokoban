using Sokoban.Core.Interfaces;

namespace Sokoban.Core.Models;

public class Cell : IPrototype<Cell>
{
    public int X { get; private init; }
    public int Y { get; private init; }
    public CellType Type { get; set; }

    private Cell() { }

    public Cell(CellType type)
    {
        Type = type;
    }

    public Cell(int x, int y, CellType type)
    {
        X = x;
        Y = y;
        Type = type;
    }

    public override bool Equals(object obj)
    {
        var otherCell = obj as Cell;

        return otherCell is not null && Type.Equals(otherCell.Type);
    }

    public override int GetHashCode()
    {
        return Type.GetHashCode() * 17 + X.GetHashCode() * 17 + Y.GetHashCode() * 17;
    }

    public Cell Clone()
    {
        return new Cell
        {
            X = X,
            Y = Y,
            Type = Type,
        };
    }
}

public enum CellType
{
    Empty,
    Rock,
    Box,
    BoxOnStorage,
    Storage,
    Player,
    PlayerOnStorage,
}
