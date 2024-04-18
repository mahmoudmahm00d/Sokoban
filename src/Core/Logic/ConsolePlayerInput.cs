using SokoFarm.Core.Enums;
using SokoFarm.Core.Interfaces;

namespace SokoFarm.Core.Logic;

public class ConsolePlayerInput : IPlayerInput
{
    public PlayerActions? ReadPlayerInput()
    {
        // TODO: Check if the key available
        // if (!Console.KeyAvailable)
        // {
        //     throw new NotSupportedException("Console keys is not available");
        // }

        ConsoleKeyInfo key = Console.ReadKey(intercept: true);
        return key.Key switch
        {
            ConsoleKey.UpArrow => PlayerActions.MoveUp,
            ConsoleKey.DownArrow => PlayerActions.MoveDown,
            ConsoleKey.LeftArrow => PlayerActions.MoveLeft,
            ConsoleKey.RightArrow => PlayerActions.MoveRight,
            ConsoleKey.Q => PlayerActions.Quit,
            ConsoleKey.C => PlayerActions.Cancel,
            ConsoleKey.R => PlayerActions.ResetLevel,
            ConsoleKey.B => PlayerActions.PreviousLevel,
            ConsoleKey.N => PlayerActions.NextLevel,
            ConsoleKey.P => PlayerActions.DisplayPath,
            ConsoleKey.D1 => PlayerActions.PlayDFS,
            ConsoleKey.D2 => PlayerActions.PlayBFS,
            ConsoleKey.D3 => PlayerActions.PlayUniformCostSearch,
            ConsoleKey.D4 => PlayerActions.PlayAStar,
            ConsoleKey.D5 => PlayerActions.HillClimbing,
            _ => null,
        };
    }
}
