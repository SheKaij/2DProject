using System;
using System.Drawing;
using System.Collections.Specialized;
using System.Drawing.Text;
using GXPEngine;

public class ExitWindow : Sprite
{
    private MyGame _myGame;
    private Level _level;

    private Button _backButton, _confirmButton;

    private Canvas _warningMessage;

    private PrivateFontCollection _pfc;
    private Font _font;

    public ExitWindow(MyGame pMyGame, Level pLevel) : base("assets/menu/window.png")
    {
        alpha = 0;

        _myGame = pMyGame;
        _level = pLevel;

        _warningMessage = new Canvas(width, height);
        _warningMessage.alpha = 0;
        SetChildIndex(_warningMessage, 1);

        _backButton = new Button("assets/menu/back_button.png");
        AddChild(_backButton);
        _backButton.x = width * 0.25f;
        _backButton.y = height - _backButton.height * 1.25f;

        _confirmButton = new Button("assets/menu/ok_button.png");
        AddChild(_confirmButton);
        _confirmButton.x = width * 0.75f;
        _confirmButton.y = height - _backButton.height * 1.25f;

        _pfc = new PrivateFontCollection();
        _pfc.AddFontFile("assets\\font\\earthorbiter.ttf");
        _font = new Font(_pfc.Families[0], 48);
    }

    private void DrawText()
    {
        _warningMessage.graphics.Clear(Color.Transparent);
        _warningMessage.graphics.DrawString("                   Warning!" + "\n"
                                          + "\n"
                                          + "Do you want to exit the game?",
                                            _font, Brushes.AliceBlue, width * 0.19f, height * 0.15f);
    }

    private void WindowAppear()
    {
        if (alpha <= 1)
        {
            alpha += 0.05f;
        }

        if (_warningMessage.alpha <= 1)
        {
            _warningMessage.alpha += 0.05f;
        }
    }

    private void HandleButtons()
    {
        if (Input.GetMouseButtonUp(0) && _backButton.MouseHover())
        {
            _level.SetWindowActive(false);
            Destroy();
        }

        else if (Input.GetMouseButtonUp(0) && _confirmButton.MouseHover())
        {
            //_level.GetMusic().Stop();
            _myGame.SetState(MyGame.GameState.START);
        }
    }
    

    private void Update()
    {
        DrawText();
        HandleButtons();
        WindowAppear();
    }
}
