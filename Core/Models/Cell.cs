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
