using SokoFarm.Core.Logic;
using SokoFarm.Core.Handlers;
using SokoFarm.Core.Interfaces;
using SokoFarm.Core.Algorithms;
using System.Diagnostics;

namespace SokoFarm.Core.Models;

public class GameModel
{
    private State _currentState;

    private readonly IController _controller;
    private SokobanSearchAlgorithm _algorithm;
    private CancellationTokenSource _token;

    public GameModel()
    {
        _currentState = new State { Grid = new() };
        _currentState = LevelSelector.SelectLevel(_currentState.Grid, 1);

        if (_currentState.SeedsCount != _currentState.StoragesCount)
        {
            throw new ArgumentException();
        }

        _token = new();
        _controller = new ConsoleController();
    }

    public bool IsGameOver()
    {
        return Actions.Actions.IsGameOver;
    }

    public void Render()
    {
        _controller.Renderer.Display(_currentState);
    }

    public void Play()
    {
        var action = _controller.PlayerInput.ReadPlayerInput();
        if (action is null)
        {
            return;
        }

        if (action.Value == Enums.PlayerActions.Cancel)
        {
            _token.Cancel();
            _token = new();
        }

        if (_algorithm is not null)
        {
            return;
        }

        var direction = action?.ToDirection();
        if (direction is null)
        {
            if (action.Value == Enums.PlayerActions.ResetLevel)
            {
                _currentState = LevelSelector.SelectLevel(
                    _currentState.Grid,
                    _currentState.CurrentLevel
                );

                _controller.Renderer.ClearPreviousState();
                Render();
                return;
            }
            else if (action.Value == Enums.PlayerActions.PreviousLevel)
            {
                if (_currentState.CurrentLevel - 1 <= 0)
                {
                    return;
                }

                try
                {
                    _currentState = LevelSelector.SelectLevel(
                        _currentState.Grid,
                        _currentState.CurrentLevel - 1
                    );

                    _controller.Renderer.ClearPreviousState();
                    Render();
                }
                catch (FileNotFoundException) { }
                return;
            }
            else if (action.Value == Enums.PlayerActions.NextLevel)
            {
                try
                {
                    _currentState = LevelSelector.SelectLevel(
                        _currentState.Grid,
                        _currentState.CurrentLevel + 1
                    );

                    _controller.Renderer.ClearPreviousState();
                    Render();
                }
                catch (FileNotFoundException) { }
                return;
            }
            else if (action.Value == Enums.PlayerActions.ResetLevel)
            {
                try
                {
                    _currentState = LevelSelector.SelectLevel(
                        _currentState.Grid,
                        _currentState.CurrentLevel
                    );

                    _controller.Renderer.ClearPreviousState();
                    Render();
                }
                catch (FileNotFoundException) { }
                return;
            }
            else if (action.Value == Enums.PlayerActions.PlayDFS)
            {
                _algorithm = new DFS();
                AlgorithmExecutionStatistics();
            }
            else if (action.Value == Enums.PlayerActions.PlayBFS)
            {
                _algorithm = new BFS();
                AlgorithmExecutionStatistics();
            }
            else if (action.Value == Enums.PlayerActions.PlayUniformCostSearch)
            {
                _algorithm = new UCS();
                AlgorithmExecutionStatistics();
            }
            else if (action.Value == Enums.PlayerActions.PlayAStar)
            {
                _algorithm = new AStar();
                AlgorithmExecutionStatistics();
            }
            else if (action.Value == Enums.PlayerActions.HillClimbing)
            {
                _algorithm = new HillClimbing();
                AlgorithmExecutionStatistics();
            }
            else if (action.Value == Enums.PlayerActions.DisplayPath)
            {
                _controller.Renderer.DisplayAllPath(_currentState);
            }
            else if (action.Value == Enums.PlayerActions.Quit)
            {
                Actions.Actions.QuitGame();
            }

            return;
        }

        var canMove = Actions.Actions.CanMove(_currentState, direction.Value);
        if (canMove)
        {
            _currentState.IsHumanPlayer = true;
            _currentState = Actions.Actions.Move(_currentState, direction.Value);
            _controller.Renderer.ClearPreviousState();
            Render();
        }
    }

    private void AlgorithmExecutionStatistics()
    {
        
        var task = new Task(() =>
        {
            _currentState.IsHumanPlayer = false;
            Stopwatch stopwatch = new();
            stopwatch.Start();
            Tuple<State, HashSet<State>> result = _algorithm.Start(_currentState, _controller.Renderer, _token);
            _currentState = result?.Item1 ?? _currentState;
            stopwatch.Stop();
            _controller.Renderer.DisplayAlgorithmExecutionStatistics(
                result.Item1,
                result.Item2,
                stopwatch.ElapsedMilliseconds
            );

            _algorithm = null;
        });

        task.Start();
    }
}
