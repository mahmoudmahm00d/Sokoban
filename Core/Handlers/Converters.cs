using Sokofarm.Core.Enums;

namespace Sokofarm.Core.Handlers;

public static class Converters
{
    public static Direction? ToDirection(this PlayerActions action)
    {
        return action switch
        {
            PlayerActions.MoveUp => Direction.Up,
            PlayerActions.MoveDown => Direction.Down,
            PlayerActions.MoveLeft => Direction.Left,
            PlayerActions.MoveRight => Direction.Right,
            _ => null
        };
    }
}
