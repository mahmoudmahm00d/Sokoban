using Sokoban.Core.Interfaces;

namespace Sokoban.Core.Models;

public class Position : IPrototype<Position>
{
    public int X { get; init; }
    public int Y { get; init; }

    public Position() { }

    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Position Clone()
    {
        return new Position { X = X, Y = Y };
    }

    public override bool Equals(object obj)
    {
        var other = obj as Position;
        return other is not null && X == other.X && Y == other.Y;
    }

    public override int GetHashCode()
    {
        return X.GetHashCode() * 17 + Y.GetHashCode() * 17;
    }
}
