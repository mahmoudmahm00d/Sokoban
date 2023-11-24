using System.Text;
using SokoFarm.Core.Interfaces;
using SokoFarm.Core.Models;
using Spectre.Console;

namespace SokoFarm.Presentation;

public class ConsoleRenderer : IRenderer
{
    public static string ToString(Cell cell)
    {
        return cell.Type switch
        {
            CellType.Empty => " ",
            CellType.Rock => "#",
            CellType.Seed => "$",
            CellType.SeedOnStorage => "*",
            CellType.Storage => ".",
            CellType.Farmer => "@",
            CellType.FarmerOnStorage => "+",
            _ => " ",
        };
    }

    public static string ToString(Core.Models.Grid grid)
    {
        StringBuilder builder = new();

        for (int i = 0; i < grid.Cells.GetLength(0); i++)
        {
            for (int j = 0; j < grid.Cells.GetLength(1); j++)
            {
                builder.Append(grid.Cells[i, j]);
            }
            builder.AppendLine();
        }

        return builder.ToString();
    }

    public void Display(Cell cell)
    {
        switch (cell.Type)
        {
            case CellType.Empty:
                AnsiConsole.Write(new Markup("[black] [/]"));
                break;
            case CellType.Rock:
                AnsiConsole.Write(new Markup("[silver]#[/]"));
                break;
            case CellType.Seed:
                AnsiConsole.Write(new Markup("[grey23 on green4]$[/]"));
                break;
            case CellType.SeedOnStorage:
                AnsiConsole.Write(new Markup("[grey23 on green]*[/]"));
                break;
            case CellType.Storage:
                AnsiConsole.Write(new Markup("[grey23 on darkgreen].[/]"));
                break;
            case CellType.Farmer:
                AnsiConsole.Write(new Markup("[dodgerblue2 on silver]@[/]"));
                break;
            case CellType.FarmerOnStorage:
                AnsiConsole.Write(new Markup("[dodgerblue2 on silver]+[/]"));
                break;
            default:
                AnsiConsole.Write(new Markup("[black on green4] [/]"));
                break;
        }
    }

    public void Display(State state)
    {
        if (state is null)
        {
            throw new ArgumentNullException(nameof(state));
        }

        AnsiConsole.MarkupLine("[bold]Welcome to SokoFarm[/]");
        AnsiConsole.MarkupLine($"[bold]Level: {state.CurrentLevel}[/]");
        if (state.IsCurrentLevelSolved)
        {
            AnsiConsole.MarkupLine($"[bold white on green4]Level Cleared[/]");
        }

		if (!state.IsCurrentLevelSolvable)
		{
			AnsiConsole.MarkupLine($"[bold white on red]Level Is Unsolvable[/]");
		}

		for (int i = 0; i < state.Grid.Cells.GetLength(0); i++)
        {
            for (int j = 0; j < state.Grid.Cells.GetLength(1); j++)
            {
                Display(state.Grid.Cells[i, j]);
            }

            Console.WriteLine();
        }

        AnsiConsole.MarkupLine("Move with arrow keys [bold blue](Up, Right, Down, Left)[/]");
        AnsiConsole.MarkupLine("Next Level: [bold blue]n[/], Previous Level: [bold blue]b[/]");
        AnsiConsole.MarkupLine("DFS: [bold blue]1[/], BFS: [bold blue]2[/], UCS: [bold blue]3[/], A*: [bold blue]4[/], HillClimbing: [bold blue]5[/]");
        AnsiConsole.MarkupLine("Reset with [bold blue]r[/], Quit: [bold blue]c[/]");
    }

    public void ClearPreviousState()
    {
        Console.Clear();
    }

    public void DisplayAllPath(State state)
    {
        if (state is null)
        {
            throw new ArgumentNullException(nameof(state));
        }

        var currentState = state;
        int movesCount = 0;
        do
        {
            Display(currentState);
            currentState = currentState.PreviousState;
            AnsiConsole.MarkupLine($"[blue]{new String('=', 50)}[/]");
            movesCount++;
        } while (currentState != null);
        AnsiConsole.MarkupLine($"Moves Count: [blue]{movesCount}[/]");
    }

    public void DisplayMessage(string message)
    {
        AnsiConsole.MarkupLine($"[blue]{message}[/]");
    }
}
