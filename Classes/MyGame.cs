using System;
using GXPEngine;
using System.Drawing;

public class MyGame : Game
{
    private StartScreen _start;
    private ControlScreen _controls;
    private Level _level;
    private ResultScreen _result;
    private StoreScreen _store;
    private GameState _gameState;

    public Spaceship currentSpaceship { get; set; }
    public bool playerOneWon { get; set; }
    public bool playerTwoWon { get; set; }
    public bool startMusic { get; set; }
    public bool levelMusic { get; set; }

    public enum GameState
    {
        START,
        CONTROLS,
        LEVEL,
        RESULT,
        STORE
    }

    public MyGame() : base(1920, 1080, true)
    {
        //SetScaleXY(0.7f);
        ShowMouse(true);
        SetState(GameState.START);
    }

    public GameState GetGameState()
    {
        return _gameState;
    }

    public void SetState(GameState pGameState)
    {
        StopState(_gameState);
        _gameState = pGameState;
        StartState(_gameState);
    }

    public void StartState(GameState pGameState)
    {
        switch (pGameState)
        {
            case GameState.START:
                _start = new StartScreen(this);
                AddChild(_start);
                break;
            case GameState.CONTROLS:
                _controls = new ControlScreen(this);
                AddChild(_controls);
                break;
            case GameState.LEVEL:
                _level = new Level(this);
                AddChild(_level);
                break;
            case GameState.RESULT:
                _result = new ResultScreen(this);
                AddChild(_result);
                break;
            case GameState.STORE:
                _store = new StoreScreen(this);
                AddChild(_store);
                break;

            default:
                break;
        }
    }


    public void StopState(GameState pGameState)
    {
        switch (pGameState)
        {
            case GameState.START:
                if (_start != null)
                {
                    _start.playMusic.Stop();
                    _start.Destroy();
                    _start = null;
                }
                break;
            case GameState.CONTROLS:
                if (_controls != null)
                {
                    _controls.playMusic.Stop();
                    _controls.Destroy();
                    _controls = null;
                }
                break;
            case GameState.LEVEL:
                if (_level != null)
                {
                    _level.playMusic.Stop();
                    _level.Destroy();
                    _level = null;
                }
                break;
            case GameState.RESULT:
                if (_result != null)
                {
                    _result.Destroy();
                    _result = null;
                }
                break;
            case GameState.STORE:
                if (_store != null)
                {
                    _store.playMusic.Stop();
                    _store.Destroy();
                    _store = null;
                }
                break;

            default:
                break;
        }
    }

    public static void Main()
    {
        new MyGame().Start();
    }
}
