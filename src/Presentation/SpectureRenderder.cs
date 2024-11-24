// using Sokoban.Core.Handlers;
// using Sokoban.Core.Interfaces;
// using Sokoban.Core.Models;
// using Spectre.Console;
//
// namespace Sokoban.Presentation;
//
// public class SpectureRenderder : IRenderer
// {
//     private static string ToString(Cell cell)
//     {
//         // return cell.Type switch
//         // {
//         //     CellType.Empty => " ",
//         //     CellType.Rock => "#",
//         //     CellType.Seed => "$",
//         //     CellType.SeedOnStorage => "*",
//         //     CellType.Storage => ".",
//         //     CellType.Farmer => "@",
//         //     CellType.FarmerOnStorage => "+",
//         //     _ => " ",
//         // };
//
//         return cell.Type switch
//         {
//             CellType.Empty => "[black] [/]",
//             CellType.Rock => "[silver]#[/]",
//             CellType.Seed => "[grey23 on green4]$[/]",
//             CellType.SeedOnStorage => "[grey23 on green]*[/]",
//             CellType.Storage => "[grey23 on darkgreen].[/]",
//             CellType.Farmer => "[dodgerblue2 on silver]@[/]",
//             CellType.FarmerOnStorage => "[dodgerblue2 on silver]+[/]",
//             _ => "[black on green4] [/]"
//         };
//     }
//
//     private static void Display(Cell cell)
//     {
//         switch (cell.Type)
//         {
//             case CellType.Empty:
//                 AnsiConsole.Write(new Markup("[black] [/]"));
//                 break;
//             case CellType.Rock:
//                 AnsiConsole.Write(new Markup("[silver]#[/]"));
//                 break;
//             case CellType.Seed:
//                 AnsiConsole.Write(new Markup("[grey23 on green4]$[/]"));
//                 break;
//             case CellType.SeedOnStorage:
//                 AnsiConsole.Write(new Markup("[grey23 on green]*[/]"));
//                 break;
//             case CellType.Storage:
//                 AnsiConsole.Write(new Markup("[grey23 on darkgreen].[/]"));
//                 break;
//             case CellType.Farmer:
//                 AnsiConsole.Write(new Markup("[dodgerblue2 on silver]@[/]"));
//                 break;
//             case CellType.FarmerOnStorage:
//                 AnsiConsole.Write(new Markup("[dodgerblue2 on silver]+[/]"));
//                 break;
//             default:
//                 AnsiConsole.Write(new Markup("[black on green4] [/]"));
//                 break;
//         }
//     }
//
//     public void ClearPreviousState(LiveDisplayContext? context = null)
//     {
//         context?.Refresh();
//     }
//
//     public void Display(State state, Table? table = null, LiveDisplayContext? context = null)
//     {
//         ArgumentNullException.ThrowIfNull(state);
//
//         // AnsiConsole.MarkupLine("[bold]Welcome to Sokoban[/]");
//         // AnsiConsole.MarkupLine($"[bold]Level: {state.CurrentLevel}[/]");
//         // if (state.IsCurrentLevelSolved)
//         // {
//         //     AnsiConsole.MarkupLine($"[bold white on green4]Level Cleared[/]");
//         // }
//         //
//         // if (!state.IsCurrentLevelSolvable)
//         // {
//         //     AnsiConsole.MarkupLine($"[bold white on darkred]Level Is Unsolvable[/]");
//         // }
//
//         var rowsCount = table.Rows.Count;
//         for (int i = rowsCount - 1; i > 0; i--)
//         {
//             table.RemoveRow(i);
//         }
//
//         table.Border = TableBorder.None;
//         table.ShowHeaders = false;
//         for (var i = 0; i < state.Grid.Cells.GetLength(0); i++)
//         {
//             List<string> items = [];
//             for (var j = 0; j < state.Grid.Cells.GetLength(1); j++)
//             {
//                 items.Add(ToString(state.Grid.Cells[i, j]));
//             }
//
//             if (i == 0 && table.Columns.Count == 0)
//             {
//                 table.AddColumns(items.Select(item => new TableColumn(item).Padding(0,0,0,0)).ToArray());
//             }
//
//             table.AddRow(items.ToArray());
//         }
//
//         if (context is null)
//             AnsiConsole.Write(table);
//         else
//         {
//             context.Refresh();
//         }
//
//         // AnsiConsole.MarkupLine("Move with arrow keys [bold blue](Up, Right, Down, Left)[/]");
//         // AnsiConsole.MarkupLine("Next Level: [bold blue]n[/], Previous Level: [bold blue]b[/]");
//         // AnsiConsole.MarkupLine(
//         //     "DFS: [bold blue]1[/], BFS: [bold blue]2[/], UCS: [bold blue]3[/], A*: [bold blue]4[/], Hill Climbing: [bold blue]5[/]"
//         // );
//         // AnsiConsole.MarkupLine(
//         //     "Print path [bold blue]p[/], Reset with [bold blue]r[/], Cancel: [bold blue]c[/], Quit: [bold blue]q[/]");
//     }
//
//     public void DisplayMessage(string message)
//     {
//         AnsiConsole.MarkupLine($"[blue]{message}[/]");
//     }
//
//     public void DisplayAllPath(State state)
//     {
//         if (state is null)
//         {
//             throw new ArgumentNullException(nameof(state));
//         }
//
//         var currentState = state;
//         var movesCount = 0;
//         do
//         {
//             Display(currentState);
//             currentState = currentState.PreviousState;
//             AnsiConsole.MarkupLine($"[blue]{new string('=', 50)}[/]");
//             movesCount++;
//         } while (currentState != null);
//
//         AnsiConsole.MarkupLine($"Moves Count: [blue]{movesCount}[/]");
//     }
//
//     public void DisplayAlgorithmExecutionStatistics(State state, HashSet<State> visited, long elapsedTime)
//     {
//         if (state is not null)
//         {
//             DisplayMessage($"Elapsed time: {elapsedTime}ms");
//             DisplayMessage($"Moves count: {state.MovesCount()}");
//             DisplayMessage($"Visited Set items count: {visited?.Count ?? 0}");
//             return;
//         }
//
//         DisplayMessage("Could not solve this board");
//     }
// }
