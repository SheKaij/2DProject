using System;
using System.Drawing;
using GXPEngine;

public class StoreScreen : GameObject
{
    private MyGame _myGame;

    private Sprite _bg, _window;

    private Button _backButton;

    public StoreScreen(MyGame pMyGame) : base()
    {
        _myGame = pMyGame;

        _bg = new Sprite("assets/background.png");
        AddChild(_bg);

        _window = new Sprite("assets/menu/window.png");
        AddChild(_window);
        _window.alpha = 0;
        _window.SetOrigin(_window.width / 2, _window.height / 2);
        _window.x = game.width / 2;
        _window.y = game.height / 2;

        _backButton = new Button("assets/menu/back_button.png");
        AddChild(_backButton);
        _backButton.x = _window.width  / 2;
        _backButton.y = _window.height - _backButton.height * 1.33f;
    }

    private void WindowAppear()
    {
        if (_window.alpha <= 1)
        {
            _window.alpha += 0.05f;
        }
    }

    private void HandleButtons()
    {
        if (Input.GetMouseButtonUp(0) && _backButton.MouseHover())
        {
            _myGame.SwitchState(MyGame.GameState.RESULT);
            _myGame.StopState(MyGame.GameState.STORE);
        }
    }

    private void Update()
    {
        HandleButtons();
        WindowAppear();
    }
}