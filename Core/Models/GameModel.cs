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

    public GameModel()
    {
        _currentState = new State { Grid = new() };
        _currentState = LevelSelector.SelectLevel(_currentState.Grid, 1);

        if (_currentState.SeedsCount != _currentState.StoragesCount)
        {
            throw new ArgumentException();
        }

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
                Stopwatch stopwatch = new();
                stopwatch.Start();
                _currentState = DFS.Start(_currentState, _controller.Renderer);
                stopwatch.Stop();
                _controller.Renderer.DisplayMessage(
                    $"Elapsed time: {stopwatch.ElapsedMilliseconds}ms"
                );
            }
            else if (action.Value == Enums.PlayerActions.PlayBFS)
            {
                Stopwatch stopwatch = new();
                stopwatch.Start();
                _currentState = BFS.Start(_currentState, _controller.Renderer);
                stopwatch.Stop();
                _controller.Renderer.DisplayMessage(
                    $"Elapsed time: {stopwatch.ElapsedMilliseconds}ms"
                );
            }
            else if (action.Value == Enums.PlayerActions.PlayUniformCostSearch)
            {
                Stopwatch stopwatch = new();
                stopwatch.Start();
                _currentState = UCS.Start(_currentState, _controller.Renderer);
                stopwatch.Stop();
                _controller.Renderer.DisplayMessage(
                    $"Elapsed time: {stopwatch.ElapsedMilliseconds}ms"
                );
            }
            else if (action.Value == Enums.PlayerActions.PlayAStar)
            {
                Stopwatch stopwatch = new();
                stopwatch.Start();
                _currentState = AStar.Start(_currentState, _controller.Renderer);
                stopwatch.Stop();
                _controller.Renderer.DisplayMessage(
                    $"Elapsed time: {stopwatch.ElapsedMilliseconds}ms"
                );
            }
            else if (action.Value == Enums.PlayerActions.HillClimbing)
            {
                Stopwatch stopwatch = new();
                stopwatch.Start();
                _currentState = HillClimbing.Start(_currentState, _controller.Renderer);
                stopwatch.Stop();
                _controller.Renderer.DisplayMessage(
                    $"Elapsed time: {stopwatch.ElapsedMilliseconds}ms"
                );
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
            _currentState = Actions.Actions.Move(_currentState, direction.Value);
            _controller.Renderer.ClearPreviousState();
            Render();
        }
    }
}
