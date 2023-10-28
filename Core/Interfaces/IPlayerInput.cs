using Sokofarm.Core.Enums;

namespace Sokofarm.Core.Interfaces;

public interface IPlayerInput
{
    PlayerActions? ReadPlayerInput();
}
