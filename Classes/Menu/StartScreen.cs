using System;
using System.Drawing;
using GXPEngine;

public class StartScreen : GameObject
{
    private MyGame _myGame;
    private Button _playButton, _optionsButton, _exitButton;
    private TextField _title;

    private float _distanceX;
    private float _distanceY;


    public StartScreen(MyGame pMyGame) : base()
    {
        _myGame = pMyGame;

        _playButton = new Button(300, 100);
        AddChild(_playButton);
        _playButton.x = game.width - _playButton.width * 0.66f;
        _playButton.y = game.height * 0.25f;

        _optionsButton = new Button(300, 100);
        AddChild(_optionsButton);
        _optionsButton.x = game.width - _optionsButton.width * 0.66f;
        _optionsButton.y = game.height * 0.5f;

        _exitButton = new Button(300, 100);
        AddChild(_exitButton);
        _exitButton.x = game.width - _exitButton.width * 0.66f;
        _exitButton.y = game.height * 0.75f;

        _title = TextField.CreateTextField("Orbital Warfare");
        AddChild(_title);
    }

    private void MouseHover()
    {
        _distanceX = Mathf.Abs(Input.mouseX - _playButton.x);
        _distanceY = Mathf.Abs(Input.mouseY - _playButton.y);

        if (_distanceX < _playButton.width / 2 && _distanceY < _playButton.height / 2)
        {
            _playButton.alpha = 0.5f;
            Clickable();
        }

        else
        {
            _playButton.alpha = 1f;
            _playButton.color = Color.DarkBlue;
        }

    }

    private void Clickable()
    {
        if (Input.GetMouseButton(0))
        {
            _playButton.color = Color.DarkRed;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _playButton.color = Color.DarkBlue;
            _myGame.SetState(MyGame.GameState.LEVEL);
        }
    }

    private void Update()
    {
        MouseHover();
    }
}