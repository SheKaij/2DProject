using System;
using System.Drawing;
using GXPEngine;

public class StoreScreen : GameObject
{
    private MyGame _myGame;

    private Sprite _bg, _window;
    private AnimationSprite _upgradeIcon;
    private Canvas _upgradeText;

    private Button _backButton, _arrowButton;

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

        _upgradeIcon = new AnimationSprite("assets/menu/bullet_sheet.png", 3, 1);
        AddChild(_upgradeIcon);
        _upgradeIcon.x = game.width / 2;
        _upgradeIcon.y = game.height / 2;

        _backButton = new Button("assets/menu/back_button.png");
        AddChild(_backButton);
        _backButton.x = _window.width  / 2;
        _backButton.y = _window.height - _backButton.height * 1.33f;

        _arrowButton = new Button("assets/menu/arrow_button.png");
        AddChild(_arrowButton);
        _arrowButton.x = _upgradeIcon.x + _upgradeIcon.width * 1.5f;
        _arrowButton.y = _upgradeIcon.y + _upgradeIcon.height / 2;
    }

    private void DrawText()
    {
        if (_upgradeIcon.currentFrame == 3)
        {
            // default bullet text, cant buy
        }

        else if (_upgradeIcon.currentFrame == 1)
        {
            // cluster bullet text, add in price and total bought
        }
        else if (_upgradeIcon.currentFrame == 2)
        {
            // thruster bullet text, add in price and total bought
        }

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

        if (Input.GetMouseButtonDown(0) && _arrowButton.MouseHover())
        {
            _upgradeIcon.NextFrame();
        }
    }

    private void Update()
    {
        HandleButtons();
        WindowAppear();
    }
}