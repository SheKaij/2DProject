using System;
using System.Drawing;
using System.Collections.Specialized;
using System.Drawing.Text;
using GXPEngine;

public class ExitWindow : Sprite
{
    private Level _level;

    private Canvas _currentPlayer;
    private Canvas _shotsleft;
    private Canvas _timeLeft;

    private Button _backButton, _confirmButton;

    private PrivateFontCollection _pfc;
    private Font _font;

    public ExitWindow(Level pLevel) : base("assets/menu/window.png")
    {
        SetOrigin(game.width / 2, game.height / 2);
        _level = pLevel;

        _backButton = new Button("assets/menu/back_button.png");
        AddChild(_backButton);
        _backButton.x = width * 0.292f; ;
        _backButton.y = height - _backButton.height * 1.33f;

        _confirmButton = new Button("assets/menu/next_button.png");
        AddChild(_confirmButton);
        _confirmButton.x = width * 0.72f; ;
        _confirmButton.y = height - height * 1.33f;

        _pfc = new PrivateFontCollection();
        _pfc.AddFontFile("assets\\font\\earthorbiter.ttf");
        _font = new Font(_pfc.Families[0], 24);
    }

    private void DrawText()
    { 
        //drawstring stuffs
    }

    private void HandleButtons()
    {
        if (Input.GetMouseButtonUp(0) && _backButton.MouseHover())
        {
            this.Destroy();
        }
    }

    private void Update()
    {
        DrawText();
    }
}
