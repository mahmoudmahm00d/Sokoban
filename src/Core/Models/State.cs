using Sokoban.Core.Interfaces;

namespace Sokoban.Core.Models;

public class State : IPrototype<State>
{
    public State PreviousState { get; set; }
    public Grid Grid { get; init; }
    public Position Player { get; set; }
    public int CurrentLevel { get; init; }
    public bool IsHumanPlayer { get; set; } = true;
    public bool IsCurrentLevelSolved { get; set; }
    public bool IsCurrentLevelSolvable { get; set; } = true;
    public int SeedsCount { get; init; }
    public int StoragesCount { get; init; }
    private int SeedsOnStorageCount { get; init; }
    public int Cost { get; set; } = 1;

    public State Clone()
    {
        return new State
        {
            PreviousState = PreviousState?.Clone(),
            Grid = Grid.Clone(),
            Player = Player.Clone(),
            CurrentLevel = CurrentLevel,
            IsHumanPlayer = IsHumanPlayer,
            IsCurrentLevelSolved = IsCurrentLevelSolved,
            IsCurrentLevelSolvable = IsCurrentLevelSolvable,
            SeedsCount = SeedsCount,
            StoragesCount = StoragesCount,
            SeedsOnStorageCount = SeedsOnStorageCount,
        };
    }

    public override bool Equals(object obj)
    {
        var otherState = obj as State;

        return otherState is not null && Grid.Equals(otherState.Grid);
    }

    public override int GetHashCode()
    {
        return 17 + Grid.GetHashCode() + Player.GetHashCode();
    }
}
