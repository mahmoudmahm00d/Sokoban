using Sokoban.Core.Models;

namespace Sokoban.Core.Logic;

public static class LevelSelector
{
    public static State SelectLevel(Grid grid, int level = 1)
    {
        var fileExists = File.Exists($"./Levels/Level{level}.txt");

        if (!fileExists)
        {
            throw new FileNotFoundException();
        }

        var seedsCount = 0;
        var storagesCount = 0;

        Position playerPosition = null;
        var textContent = File.ReadAllLines($"./Levels/Level{level}.txt");

        grid.Cells = new Cell[textContent.GetLength(0), textContent[0].Length];
        for (var y = 0; y < textContent.GetLength(0); y++)
        {
            for (var x = 0; x < textContent[0].Length; x++)
            {
                var text = textContent[y];
                if (text[x] == '@')
                {
                    playerPosition = new Position(x, y);
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
            SeedsCount = seedsCount,
            StoragesCount = storagesCount,
            Player = playerPosition,
            CurrentLevel = level,
            Cost = 0,
        };
    }

    private static CellType ToCellType(this char character)
    {
        return character switch
        {
            'x' => CellType.Empty,
            '#' => CellType.Rock,
            '.' => CellType.Storage,
            '@' => CellType.Player,
            '$' => CellType.Box,
            _ => CellType.Empty,
        };
    }
}
