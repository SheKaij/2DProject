using System;
using GXPEngine;
using System.Drawing;

public class MyGame : Game
{
    private StartScreen _start;
    private CreditScreen _credits;
    private Level _level;
    private ResultScreen _result;
    private StoreScreen _store;
    private GameState _gameState;

    public enum GameState
    {
        START,
        OPTIONS,
        LEVEL,
        RESULT,
        STORE
    }

    public MyGame() : base(1920, 1080, false)
    {
        //SetScaleXY(0.7f);
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
            case GameState.OPTIONS:
                _credits = new CreditScreen(this);
                AddChild(_credits);
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
                    _start.Destroy();
                    _start = null;
                }
                break;
            case GameState.OPTIONS:
                if (_credits != null)
                {
                    _credits.Destroy();
                    _credits = null;
                }
                break;
            case GameState.LEVEL:
                if (_level != null)
                {
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
