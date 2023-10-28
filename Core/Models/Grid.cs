using System.Text;
using Sokofarm.Core.Logic;

namespace Sokofarm.Core.Models;

public class Grid
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
}
