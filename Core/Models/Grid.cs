using Sokofarm.Core.Interfaces;

namespace Sokofarm.Core.Models;

public class Grid : IPrototype<Grid>
{
    private Cell[,] cells;
    public int InitialStoragesCount { get; set; }
    public int InitialSeedsCount { get; set; }

    public Cell[,] Cells
    {
        get => cells;
        set => cells = value;
    }

    public Grid() { }

    public Grid(int rows, int columns) { }

    public override bool Equals(object obj)
    {
        Grid otherGrid = obj as Grid;

        if (otherGrid is null)
        {
            return false;
        }

        if (
            Cells.GetLength(0) != otherGrid.Cells.GetLength(0)
            || Cells.GetLength(1) != otherGrid.Cells.GetLength(1)
        )
        {
            return false;
        }

        for (int i = 0; i < Cells.GetLength(0); i++)
        {
            for (int j = 0; j < Cells.GetLength(1); j++)
            {
                if (!Cells[i, j].Equals(otherGrid.Cells[i, j]))
                {
                    return false;
                }
            }
        }

        return true;
    }

    public override int GetHashCode()
    {
        int hash = 17;
        foreach (var cell in Cells)
        {
            hash += cell.GetHashCode();
        }

        return hash;
    }

	public Grid Clone()
	{
        var clonedCells = new Cell[Cells.GetLength(0), Cells.GetLength(1)];
		for (int i = 0; i < Cells.GetLength(0); i++)
		{
			for (int j = 0; j < Cells.GetLength(1); j++)
			{
                clonedCells[i, j] = Cells[i, j].Clone();
			}
		}

        return new Grid
        {
            InitialSeedsCount = InitialSeedsCount,
            InitialStoragesCount = InitialStoragesCount,
            Cells = clonedCells
        };
	}
}
