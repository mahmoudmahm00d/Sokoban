using Sokoban.Core.Interfaces;
using Sokoban.Core.Logic;
using Sokoban.Core.Models;
using static Sokoban.Core.Actions.Actions;

namespace Sokoban.Core.Algorithms;

/// <summary>
/// Depth-First Search class
/// </summary>
public class DFS : SokobanSearchAlgorithm
{
    public override Tuple<State, HashSet<State>> Start(
        State state,
        IRenderer renderer = null,
        CancellationTokenSource token = null
    )
    {
        var visited = new HashSet<State>();

        var stack = new Stack<State>();

        // If this state is solved, reconstruct and return the path
        if (state.Solved())
        {
            return new Tuple<State, HashSet<State>>(state, visited);
        }

        stack.Push(state);

        while (stack.Count > 0)
        {
            var currentState = stack.Pop();

            visited.Add(currentState);

            renderer?.ClearPreviousState();
            renderer?.Display(currentState);

            if (token?.IsCancellationRequested ?? false)
            {
                return new Tuple<State, HashSet<State>>(currentState, visited);
            }

            // Otherwise, add all possible next states to the stack
            foreach (var nextState in GetPossibleStates(currentState))
            {
                // If we've already visited this state, skip it
                if (visited.Contains(nextState))
                {
                    continue;
                }

                // If this state is solved, return it
                if (nextState.Solved())
                {
                    renderer?.ClearPreviousState();
                    renderer?.Display(nextState);
                    return new Tuple<State, HashSet<State>>(nextState, visited);
                }

                stack.Push(nextState);
            }
        }

        // If we've exhausted all possible states without finding a solution, return null
        return null;
    }
}
