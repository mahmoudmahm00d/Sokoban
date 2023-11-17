using Sokofarm.Core.Interfaces;

namespace Sokofarm.Core.Models;

public class Position : IPrototype<Position>
{
    public int X { get; set; }
    public int Y { get; set; }

    public Position() { }

    public Position(int X, int Y)
    {
        this.X = X;
        this.Y = Y;
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
