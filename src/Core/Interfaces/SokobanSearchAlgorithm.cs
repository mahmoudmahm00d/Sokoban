using SokoFarm.Core.Models;

namespace SokoFarm.Core.Interfaces;

public abstract class SokobanSearchAlgorithm
{
    public abstract Tuple<State, HashSet<State>> Start(
        State start,
        IRenderer renderer = null,
        CancellationTokenSource token = null
    );
}
