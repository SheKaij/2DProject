using System;
using System.Drawing;
using GXPEngine;

public class CreditScreen : GameObject
{
    private MyGame _myGame;

    private Sprite _bg;

    private Button _backButton;

    public CreditScreen(MyGame pMyGame) : base()
    {
        _myGame = pMyGame;

        _bg = new Sprite("assets/background.png");
        AddChild(_bg);
        _bg.SetScaleXY(0.7f);

        _backButton = new Button("assets/menu/back_button.png");
        AddChild(_backButton);
        _backButton.x = game.width * 0.80f;
        _backButton.y = game.height - _backButton.height * 0.66f;
    }

    private void HandleButtons()
    {
        if (Input.GetMouseButtonUp(0) && _backButton.MouseHover())
        {
            _myGame.SetState(MyGame.GameState.START);
        }
    }

    private void Update()
    {
        HandleButtons();
    }
}