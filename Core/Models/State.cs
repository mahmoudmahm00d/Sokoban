using Sokofarm.Core.Interfaces;

namespace Sokofarm.Core.Models;

public class State : IPrototype<State>
{
	public State PreviousState { get; set; }
	public Grid Grid { get; set; }
	public Position Farmer { get; set; }
	public int CurrentLevel { get; set; }
	public bool IsCurrentLevelSolved { get; set; }
	public bool IsCurrentLevelSolvable { get; set; } = true;
	public int SeedsCount { get; set; }
	public int StoragesCount { get; set; }
	public int SeedsOnStorageCount { get; set; }
    public int Cost { get; set; } = 1;

	public State Clone()
	{
		return new State
		{
			PreviousState = PreviousState?.Clone(),
			Grid = Grid.Clone(),
			Farmer = Farmer.Clone(),
			CurrentLevel = CurrentLevel,
			IsCurrentLevelSolved = IsCurrentLevelSolved,
			IsCurrentLevelSolvable = IsCurrentLevelSolvable,
			SeedsCount = SeedsCount,
			StoragesCount = StoragesCount,
			SeedsOnStorageCount = SeedsOnStorageCount
		};
	}

    public override bool Equals(object obj)
    {
        State otherState = obj as State;

        return otherState is not null && Grid.Equals(otherState.Grid);
    }

    public override int GetHashCode()
    {
        return 17 + Grid.GetHashCode() + Farmer.GetHashCode();
    }
}
