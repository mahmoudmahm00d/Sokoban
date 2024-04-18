using SokoFarm.Core.Enums;

namespace SokoFarm.Core.Interfaces;

public interface IPlayerInput
{
    PlayerActions? ReadPlayerInput();
}
