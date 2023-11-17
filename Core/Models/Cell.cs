using Sokofarm.Core.Interfaces;

namespace Sokofarm.Core.Models;

public class Cell : IPrototype<Cell>
{
	public int X { get; set; }
	public int Y { get; set; }
	public CellType Type { get; set; }

	public Cell() { }

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
        Cell otherCell = obj as Cell;

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
			Type = Type
		};
	}
}

public enum CellType
{
	Empty,
	Rock,
	Seed,
	SeedOnStorage,
	Storage,
	Farmer,
	FarmerOnStorage,
}
