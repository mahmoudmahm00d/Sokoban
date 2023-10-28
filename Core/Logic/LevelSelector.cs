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
        for (int i = 0; i < textContent.GetLength(0); i++)
        {
            for (int j = 0; j < textContent[0].Length; j++)
            {
                var text = textContent[i];
                if (text[j] == '@')
                {
                    farmerPosition = new(j, i);
                }
                if (text[j] == '$')
                {
                    seedsCount++;
                }
                if (text[j] == '.')
                {
                    storagesCount++;
                }

                grid.Cells[i, j] = new Cell(text[j].ToCellType());
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
