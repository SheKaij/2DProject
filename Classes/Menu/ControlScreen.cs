using System;
using System.Drawing;
using System.Collections.Specialized;
using System.Drawing.Text;
using GXPEngine;

public class ControlScreen : GameObject
{
    private MyGame _myGame;

    private Sprite _bg;
    private Canvas _info;

    private Button _backButton;
    private PrivateFontCollection _pfc;
    private Font _font;

    public ControlScreen(MyGame pMyGame) : base()
    {
        _myGame = pMyGame;

        _bg = new Sprite("assets/background.png");
        AddChild(_bg);

        _info = new Canvas(game.width, game.height);
        SetChildIndex(_info, 1);

        _backButton = new Button("assets/menu/back_button.png");
        AddChild(_backButton);
        _backButton.x = game.width * 0.80f;
        _backButton.y = game.height - _backButton.height * 0.66f;

        _pfc = new PrivateFontCollection();
        _pfc.AddFontFile("assets\\font\\earthorbiter.ttf");
        _font = new Font(_pfc.Families[0], 40);

        Sprite _fg = new FadeOut();
        AddChild(_fg);
    }

    private void DrawText()
    {
        _info.graphics.DrawImage(Image.FromFile("assets/icons/controls_icon1.png"), game.width * 0.1f, game.height * 0.1f);
        _info.graphics.DrawString("Shoot your bullet in the" + "\n"
                                 + "direction of your mouse",
                                 _font, Brushes.AliceBlue, game.width * 0.40f, game.height * 0.1f);

        _info.graphics.DrawImage(Image.FromFile("assets/icons/controls_icon2.png"), game.width * 0.1f, game.height * 0.27f);
        _info.graphics.DrawString("Your bullets are affected" + "\n"
                                 + "by the gravity of planets",
                                 _font, Brushes.AliceBlue, game.width * 0.40f, game.height * 0.27f);

        _info.graphics.DrawImage(Image.FromFile("assets/icons/controls_icon3.png"), game.width * 0.1f, game.height * 0.44f);
        _info.graphics.DrawString("You can switch bullet upgrades" + "\n"
                                 + "by pressing these number keys",
                                 _font, Brushes.AliceBlue, game.width * 0.40f, game.height * 0.44f);

        _info.graphics.DrawImage(Image.FromFile("assets/icons/controls_icon3.png"), game.width * 0.1f, game.height * 0.61f);
        _info.graphics.DrawString("You buy upgrades with currency" + "\n"
                                 + "Currency is earned by destroying" + "\n"
                                 + "planets and damaging spaceships",
                                 _font, Brushes.AliceBlue, game.width * 0.40f, game.height * 0.61f);
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
        DrawText();
        HandleButtons();
    }
}