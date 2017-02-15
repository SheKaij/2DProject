using System;
using System.Drawing;
using GXPEngine;

public class StoreScreen : GameObject
{
    private MyGame _myGame;

    private Sprite _bg, _window;

    private Button _backButton, _nextButton;

    public StoreScreen(MyGame pMyGame) : base()
    {
        _myGame = pMyGame;

        _bg = new Sprite("assets/background.png");
        AddChild(_bg);

        _window = new Sprite("assets/menu/window.png");
        AddChild(_window);
        _window.SetOrigin(_window.width / 2, _window.height / 2);
        _window.x = game.width / 2;
        _window.y = game.height / 2;

        _backButton = new Button("assets/menu/back_button.png");
        AddChild(_backButton);
        _backButton.x = _window.width * 0.292f; ;
        _backButton.y = _window.height - _backButton.height * 1.33f;

        _nextButton = new Button("assets/menu/next_button.png");
        AddChild(_nextButton);
        _nextButton.x = _window.width * 0.72f; ;
        _nextButton.y = _window.height - _nextButton.height * 1.33f;
    }

    private void HandleButtons()
    {
        if (Input.GetMouseButtonUp(0) && _backButton.MouseHover())
        {
            _myGame.SetState(MyGame.GameState.RESULT);
        }

        else if (Input.GetMouseButtonUp(0) && _nextButton.MouseHover())
        {
            _myGame.SetState(MyGame.GameState.LEVEL);
        }
    }

    private void Update()
    {
        HandleButtons();
    }
}