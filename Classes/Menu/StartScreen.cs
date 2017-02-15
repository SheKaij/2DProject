﻿using System;
using System.Drawing;
using System.Drawing.Text;
using GXPEngine;
using System.IO;

public class StartScreen : GameObject
{
    private MyGame _myGame;
    private Button _playButton, _creditsButton, _exitButton;
    private Sprite _bg, _fg;

    public StartScreen(MyGame pMyGame) : base()
    {
        _myGame = pMyGame;

        _bg = new Sprite("assets/menu/start_screen.png");
        AddChild(_bg);
        _bg.SetScaleXY(0.7f);

        _playButton = new Button("assets/menu/start_button.png");
        AddChild(_playButton);
        _playButton.x = game.width * 0.20f;
        _playButton.y = game.height - _playButton.height * 0.66f;

        _creditsButton = new Button("assets/menu/credits_button.png");
        AddChild(_creditsButton);
        _creditsButton.x = game.width * 0.50f;
        _creditsButton.y = game.height - _creditsButton.height * 0.66f;
        _exitButton = new Button("assets/menu/exit_button.png");
        AddChild(_exitButton);
        _exitButton.x = game.width * 0.80f;
        _exitButton.y = game.height - _exitButton.height * 0.66f;

        _fg = new FadeOut();
        AddChild(_fg);
    }

    private void HandleButtons()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (_playButton.MouseHover())
            {
                _myGame.SetState(MyGame.GameState.LEVEL);
            }

            else if (_creditsButton.MouseHover())
            {
                _myGame.SetState(MyGame.GameState.OPTIONS);
            }

            else if (_exitButton.MouseHover())
            {
                _myGame.Destroy();
                Environment.Exit(0);
            }
        }
    }

    private void Update()
    {
        HandleButtons();
    }
}