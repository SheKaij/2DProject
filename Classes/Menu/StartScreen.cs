using System;
using System.Drawing;
using GXPEngine;

public class StartScreen : GameObject
{
    private MyGame _myGame;
    private Button _playButton, _optionsButton, _exitButton;
    private TextField _title;

    private float _distancePlayX, _distanceOptionsX, _distanceExitX;
    private float _distancePlayY, _distanceOptionsY, _distanceExitY;
    private bool _hoverPlayButton, _hoverOptionsButton, _hoverExitButton;


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
        _distancePlayX = Mathf.Abs(Input.mouseX - _playButton.x);
        _distancePlayY = Mathf.Abs(Input.mouseY - _playButton.y);

        _distanceOptionsX = Mathf.Abs(Input.mouseX - _optionsButton.x);
        _distanceOptionsY = Mathf.Abs(Input.mouseY - _optionsButton.y);

        _distanceExitX = Mathf.Abs(Input.mouseX - _exitButton.x);
        _distanceExitY = Mathf.Abs(Input.mouseY - _exitButton.y);

        if (_distancePlayX < _playButton.width / 2 && _distancePlayY < _playButton.height / 2)
        {
            _playButton.alpha = 0.5f;
            _hoverPlayButton = true;
            Clickable();
        }

        else
        {
            _playButton.alpha = 1f;
            _playButton.color = Color.DarkBlue;
            _hoverPlayButton = false;
        }

        if (_distanceOptionsX < _optionsButton.width / 2 && _distanceOptionsY < _optionsButton.height / 2)
        {
            _optionsButton.alpha = 0.5f;
            _hoverOptionsButton = true;
            Clickable();
        }

        else
        {
            _optionsButton.alpha = 1f;
            _optionsButton.color = Color.DarkBlue;
            _hoverOptionsButton = false;
        }

        if (_distanceExitX < _exitButton.width / 2 && _distanceExitY < _exitButton.height / 2)
        {
            _exitButton.alpha = 0.5f;
            _hoverExitButton = true;
            Clickable();
        }

        else
        {
            _exitButton.alpha = 1f;
            _exitButton.color = Color.DarkBlue;
            _hoverExitButton = false;
        }
    }

    private void Clickable()
    {
        if (Input.GetMouseButton(0))
        {
            if (_hoverPlayButton == true)
            {
                _playButton.color = Color.DarkRed;
            }

            else if (_hoverOptionsButton == true)
            {
                _optionsButton.color = Color.DarkRed;
            }

            else if (_hoverExitButton == true)
            {
                _exitButton.color = Color.DarkRed;
                if (Input.GetMouseButtonUp(0))
                {
                    _exitButton.color = Color.DarkBlue;
                    _myGame.Destroy();
                    Environment.Exit(0);
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_hoverPlayButton == true)
            {
                _playButton.color = Color.DarkBlue;
                _myGame.SetState(MyGame.GameState.LEVEL);
            }

            else if (_hoverOptionsButton == true)
            {
                _optionsButton.color = Color.DarkBlue;
                _myGame.SetState(MyGame.GameState.OPTIONS);
            }

            else if (_hoverOptionsButton == true)
            {
                
            }
        }
    }

    private void Update()
    {
        MouseHover();
    }
}