using System;
using System.Drawing;
using System.Collections.Specialized;
using System.Drawing.Text;
using GXPEngine;

public class ExitWindow : Sprite
{
    private MyGame _myGame;

    private Button _backButton, _confirmButton;

    private PrivateFontCollection _pfc;
    private Font _font;

    public ExitWindow(MyGame pMyGame) : base("assets/menu/window.png")
    {
        alpha = 0;

        _myGame = pMyGame;

        _backButton = new Button("assets/menu/back_button.png");
        AddChild(_backButton);
        _backButton.x = 0;
        _backButton.y = 0;

        _confirmButton = new Button("assets/menu/next_button.png");
        AddChild(_confirmButton);
        _confirmButton.x = width;
        _confirmButton.y = height;

        _pfc = new PrivateFontCollection();
        _pfc.AddFontFile("assets\\font\\earthorbiter.ttf");
        _font = new Font(_pfc.Families[0], 24);
    }

    private void DrawText()
    { 
        //drawstring stuffs
    }


    private void WindowAppear()
    {
        if (alpha <= 1)
        {
            alpha += 0.05f;
        }
    }

    private void HandleButtons()
    {
        if (Input.GetMouseButtonUp(0) && _backButton.MouseHover())
        {
            this.Destroy();
        }

        else if (Input.GetMouseButtonUp(0) && _confirmButton.MouseHover())
        {
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
