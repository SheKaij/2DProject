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
    private bool _windowActive;

    public ExitWindow(MyGame pMyGame) : base("assets/menu/window.png")
    {
        alpha = 0;
        //SetScaleXY(0.75f);

        _myGame = pMyGame;

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
        _font = new Font(_pfc.Families[0], 24);
    }

    public bool windowActive
    {
        get
        {
            return _windowActive;
        }
        set
        {
            _windowActive = value;
        }
    }

    private void DrawText()
    { 
        
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
        if (_windowActive == true)
        {
            if (Input.GetMouseButtonUp(0) && _backButton.MouseHover())
            {
                _windowActive = false;
                this.Destroy();
            }

            else if (Input.GetMouseButtonUp(0) && _confirmButton.MouseHover())
            {
                _windowActive = false;
                _myGame.SetState(MyGame.GameState.START);
            }
        }
    }

    private void Update()
    {
        DrawText();
        HandleButtons();
        WindowAppear();
    }
}
