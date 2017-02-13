using System;
using GXPEngine;
using System.Drawing;

public class MyGame : Game
{
    private StartScreen _start;
    private OptionScreen _options;
    private Level _level;
    private ResultScreen _result;
    private ShopScreen _shop;
    private GameState _gameState;

    public enum GameState
    {
        START,
        OPTIONS,
        LEVEL,
        RESULT,
        SHOP
    }

    public MyGame() : base(1920, 1080, false)
    {
        SetState(GameState.LEVEL);
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
            //case GameState.OPTIONS:
            //    _options = new OptionScreen(this);
            //    AddChild(_options);
            //    break;
            case GameState.LEVEL:
                _level = Level.Instance;
                AddChild(_level);
                break;
            //case GameState.RESULT:
            //    _result = new ResultScreen(this);
            //    AddChild(_result);
            //    break;
            //case GameState.SHOP:
            //    _shop = new ShopScreen(this);
            //    AddChild(_shop);
            //    break;

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
                if (_options != null)
                {
                    _options.Destroy();
                    _options = null;
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
            case GameState.SHOP:
                if (_shop != null)
                {
                    _shop.Destroy();
                    _shop = null;
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
