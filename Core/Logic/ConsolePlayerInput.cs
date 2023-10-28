using Sokofarm.Core.Enums;
using Sokofarm.Core.Interfaces;

namespace Sokofarm.Core.Logic;

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
            ConsoleKey.C => PlayerActions.Quit,
            ConsoleKey.R => PlayerActions.ResetLevel,
            ConsoleKey.B => PlayerActions.PreviousLevel,
            ConsoleKey.N => PlayerActions.NextLevel,
            _ => null,
        };
    }
}
