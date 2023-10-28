namespace Sokofarm.Core.Models;

public class Cell
{
    public CellType Type { get; set; }

    public Cell() { }

    public Cell(CellType type)
    {
        Type = type;
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
