using System.Diagnostics;
using Sokoban.Core.Algorithms;
using Sokoban.Core.Handlers;
using Sokoban.Core.Interfaces;
using Sokoban.Core.Logic;
using Sokoban.Presentation;
using static Sokoban.Presentation.Delay;

namespace Sokoban.Core.Models;

public class GameModel
{
    private bool _isRenderLastPath = false;
    private State _currentState;
    private readonly Delay _delay;

    private readonly IController _controller;
    private SokobanSearchAlgorithm _algorithm;
    private CancellationTokenSource _token;

    public GameModel()
    {
        _currentState = new State { Grid = new Grid() };
        _currentState = LevelSelector.SelectLevel(_currentState.Grid, 1);

        if (_currentState.SeedsCount != _currentState.StoragesCount)
        {
            throw new ArgumentException();
        }

        _token = new CancellationTokenSource();
        _controller = new ConsoleController();
        _delay = new Delay(1000);
        _delay.OnDurationChanged += (object sender, DurationChangedEventArgs args) =>
            _controller.Renderer.DisplayMessage($"Current delay: {args.Duration}");
    }

    public static bool IsGameOver()
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
            _token = new CancellationTokenSource();
        }

        if (_algorithm is not null)
        {
            return;
        }

        if (action == Enums.PlayerActions.IncreaseSpeed)
        {
            _delay.Duration += 50;
        }

        if (action == Enums.PlayerActions.DecreaseSpeed)
        {
            _delay.Duration -= 50;
        }

        if (action == Enums.PlayerActions.Quit)
        {
            Actions.Actions.QuitGame();
        }

        if (_isRenderLastPath)
        {
            return;
        }

        var direction = action?.ToDirection();
        if (direction is not null)
        {
            var canMove = Actions.Actions.CanMove(_currentState, direction.Value);
            if (!canMove)
                return;
            _currentState.IsHumanPlayer = true;
            _currentState = Actions.Actions.Move(_currentState, direction.Value);
            _controller.Renderer.ClearPreviousState();
            Render();
            return;
        }

        if (action == Enums.PlayerActions.UnDo && _currentState.PreviousState is not null)
        {
            _currentState = _currentState.PreviousState;
            _controller.Renderer.ClearPreviousState();
            Render();
            return;
        }

        if (action == Enums.PlayerActions.ResetLevel)
        {
            _currentState = LevelSelector.SelectLevel(
                _currentState.Grid,
                _currentState.CurrentLevel
            );

            _controller.Renderer.ClearPreviousState();
            Render();
            return;
        }

        if (action.Value == Enums.PlayerActions.PreviousLevel)
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

        if (action == Enums.PlayerActions.NextLevel)
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

        if (action == Enums.PlayerActions.ResetLevel)
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

        if (action == Enums.PlayerActions.PlayDFS)
        {
            _algorithm = new DFS();
            AlgorithmExecutionStatistics();
        }

        if (action == Enums.PlayerActions.PlayBFS)
        {
            _algorithm = new BFS();
            AlgorithmExecutionStatistics();
        }

        if (action == Enums.PlayerActions.PlayUniformCostSearch)
        {
            _algorithm = new UCS();
            AlgorithmExecutionStatistics();
        }

        if (action == Enums.PlayerActions.PlayAStar)
        {
            _algorithm = new AStar();
            AlgorithmExecutionStatistics();
        }

        if (action == Enums.PlayerActions.HillClimbing)
        {
            _algorithm = new HillClimbing();
            AlgorithmExecutionStatistics();
        }

        if (action == Enums.PlayerActions.DisplayPath)
        {
            Task.Run(() =>
            {
                _isRenderLastPath = true;
                _controller.Renderer.DisplayAllPath(_currentState, _delay, _token);
                _isRenderLastPath = false;
            });
        }

        return;
    }

    private void AlgorithmExecutionStatistics()
    {
        var task = new Task(() =>
        {
            _currentState.IsHumanPlayer = false;
            Stopwatch stopwatch = new();
            stopwatch.Start();
            Tuple<State, HashSet<State>> result = _algorithm.Start(
                _currentState,
                _controller.Renderer,
                _token
            );
            stopwatch.Stop();
            _currentState = result?.Item1 ?? _currentState;
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
