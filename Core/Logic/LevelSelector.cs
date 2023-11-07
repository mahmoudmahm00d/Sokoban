using Sokofarm.Core.Models;

namespace Sokofarm.Core.Logic;

public static class LevelSelector
{
    public static State SelectLevel(Grid grid, int level = 1)
    {
        bool fileExists = File.Exists($"./Levels/Level{level}.txt");

        if (!fileExists)
        {
            throw new FileNotFoundException();
        }

        int seedsCount = 0;
        int storagesCount = 0;

        Position farmerPosition = null;
        string[] textContent = File.ReadAllLines($"./Levels/Level{level}.txt");

        grid.Cells = new Cell[textContent.GetLength(0), textContent[0].Length];
        for (int y = 0; y < textContent.GetLength(0); y++)
        {
            for (int x = 0; x < textContent[0].Length; x++)
            {
                var text = textContent[y];
                if (text[x] == '@')
                {
                    farmerPosition = new(x, y);
                }
                if (text[x] == '$')
                {
                    seedsCount++;
                }
                if (text[x] == '.')
                {
                    storagesCount++;
                }

                grid.Cells[y, x] = new Cell(x, y, text[x].ToCellType());
            }
        }

        return new State
        {
            Grid = grid,
            Farmer = farmerPosition,
            CurrentLevel = level
        };
    }

    private static CellType ToCellType(this char character)
    {
        return character switch
        {
            'x' => CellType.Empty,
            '#' => CellType.Rock,
            '.' => CellType.Storage,
            '@' => CellType.Farmer,
            '$' => CellType.Seed,
            _ => CellType.Empty,
        };
    }
}
