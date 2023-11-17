using SokoFarm.Core.Interfaces;
using SokoFarm.Core.Logic;
using SokoFarm.Core.Models;
using static SokoFarm.Core.Actions.Actions;

namespace SokoFarm.Core.Algorithms;

/// <summary>
/// Depth-First Search class
/// </summary>
public class DFS
{
    public static State Start(State state, IRenderer renderer = null)
    {
		var visited = new HashSet<State>();

		var stack = new Stack<State>();

		stack.Push(state);

		while (stack.Count > 0)
		{
			var currentState = stack.Pop();

			// If we've already visited this state, skip it
			if (visited.Contains(currentState))
			{
				continue;
			}

			visited.Add(currentState);

			renderer?.ClearPreviousState();
			renderer?.Display(currentState);

			// If this state is solved, return it
			if (currentState.Solved())
			{
				return currentState;
			}

			if (currentState.Trapped())
			{
				continue;
			}

			// Otherwise, add all possible next states to the stack
			foreach (var nextState in GetPossibleStates(currentState))
			{
				stack.Push(nextState);
			}
		}

		// If we've exhausted all possible states without finding a solution, return null
		return null;
	}
}
