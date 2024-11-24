using System.Text;
using SokoFarm.Core.Handlers;
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

        for (var i = 0; i < grid.Cells.GetLength(0); i++)
        {
            for (var j = 0; j < grid.Cells.GetLength(1); j++)
            {
                builder.Append(grid.Cells[i, j]);
            }

            builder.AppendLine();
        }

        return builder.ToString();
    }

    private static void Display(Cell cell)
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
        ArgumentNullException.ThrowIfNull(state);
        PrintLevelInfo(state);
        PrintGrid(state);
        PrintControls();
    }

    private static void PrintLevelInfo(State state)
    {
        AnsiConsole.MarkupLine("[bold]Welcome to SokoFarm[/]");
        AnsiConsole.MarkupLine($"[bold]Level: {state.CurrentLevel}[/]");
        if (state.IsCurrentLevelSolved)
        {
            AnsiConsole.MarkupLine($"[bold white on green4]Level Cleared[/]");
        }

        if (!state.IsCurrentLevelSolvable)
        {
            AnsiConsole.MarkupLine($"[bold white on darkred]Level Is Unsolvable[/]");
        }
    }

    private static void PrintControls()
    {
        AnsiConsole.MarkupLine(
            "Move with arrow keys [bold blue](Up, Right, Down, Left)[/], UnDo: [bold blue]U[/]"
        );
        AnsiConsole.MarkupLine("Next Level: [bold blue]N[/], Previous Level: [bold blue]B[/]");
        AnsiConsole.MarkupLine(
            "DFS: [bold blue]1[/], BFS: [bold blue]2[/], UCS: [bold blue]3[/], A*: [bold blue]4[/], Hill Climbing: [bold blue]5[/]"
        );
        AnsiConsole.MarkupLine(
            "Print path [bold blue]P[/]([bold blue]K[/] [gray]-50ms playback delay[/],[bold blue]L[/] [gray]+50ms playback delay[/])"
        );
        AnsiConsole.MarkupLine(
            "Reset with [bold blue]R[/], Cancel: [bold blue]C[/], Quit: [bold blue]Q[/]"
        );
    }

    private static void PrintGrid(State state)
    {
        for (var i = 0; i < state.Grid.Cells.GetLength(0); i++)
        {
            for (var j = 0; j < state.Grid.Cells.GetLength(1); j++)
            {
                Display(state.Grid.Cells[i, j]);
            }

            Console.WriteLine();
        }
    }

    public void ClearPreviousState()
    {
        Console.Clear();
    }

    public void DisplayAllPath(
        State state,
        Delay delay,
        CancellationTokenSource cancellationToken = null
    )
    {
        if (state is null)
        {
            throw new ArgumentNullException(nameof(state));
        }

        var currentState = state;
        Stack<State> moves = [];
        moves.Push(currentState);
        while (currentState is not null)
        {
            moves.Push(currentState);
            currentState = currentState.PreviousState;
        }

        currentState = moves.Pop();
        var movesCount = -1; // Do not consider initial state
        do
        {
            if (cancellationToken?.IsCancellationRequested ?? false)
            {
                return;
            }

            Console.Clear();
            if (moves.Count == 0)
            {
                PrintLevelInfo(currentState);
            }
            DisplayMessage($"Current delay: {delay.Duration}");
            PrintGrid(currentState);
            if (moves.Count == 0)
            {
                PrintControls();
            }

            if (0 < delay.Duration)
            {
                Thread.Sleep(delay.Duration);
            }

            if (0 < moves.Count)
            {
                currentState = moves.Pop();
                movesCount++;
            }
            else
            {
                currentState = null;
            }

            AnsiConsole.MarkupLine($"[blue]{new string('=', 50)}[/]");
        } while (currentState is not null);
        movesCount = 0 < movesCount ? movesCount : 0;
        AnsiConsole.MarkupLine($"Moves Count: [blue]{movesCount}[/]");
    }

    public void DisplayMessage(string message)
    {
        AnsiConsole.MarkupLine($"[blue]{message}[/]");
    }

    public void DisplayAlgorithmExecutionStatistics(
        State state,
        HashSet<State> visited,
        long elapsedTime
    )
    {
        if (state is not null)
        {
            DisplayMessage($"Elapsed time: {elapsedTime}ms");
            DisplayMessage($"Moves count: {state.MovesCount()}");
            DisplayMessage($"Visited Set items count: {visited?.Count ?? 0}");
            return;
        }

        DisplayMessage("Could not solve this board");
    }
}

public class Delay
{
    public Delay()
    {
        Duration = 0;
    }

    public Delay(int duration)
    {
        Duration = duration;
    }

    private int duration;

    public int Duration
    {
        get => duration;
        set
        {
            duration = value;
            OnDurationChanged?.Invoke(this, new DurationChangedEventArgs(duration));
        }
    }

    public event EventHandler<DurationChangedEventArgs> OnDurationChanged;

    public class DurationChangedEventArgs(int duration)
    {
        public int Duration { get; } = duration;
    }
}
