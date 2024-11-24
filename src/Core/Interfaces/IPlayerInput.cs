using Sokoban.Core.Enums;

namespace Sokoban.Core.Interfaces;

public interface IPlayerInput
{
    PlayerActions? ReadPlayerInput();
}
