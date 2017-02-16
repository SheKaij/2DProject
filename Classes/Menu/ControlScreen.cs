using System;
using System.Drawing;
using System.Collections.Specialized;
using System.Drawing.Text;
using GXPEngine;

public class ControlScreen : GameObject
{
    private MyGame _myGame;

    private Sprite _bg, _icon1, _icon2, _icon3, _icon4;
    private Canvas _info1, _info2, _info3, _info4;

    private Button _backButton;
    private PrivateFontCollection _pfc;
    private Font _font;

    public ControlScreen(MyGame pMyGame) : base()
    {
        _myGame = pMyGame;

        _bg = new Sprite("assets/background.png");
        AddChild(_bg);

        _info1 = new Canvas(game.width, game.height);
        SetChildIndex(_info1, 10);
        _info2 = new Canvas(game.width, game.height);
        SetChildIndex(_info2, 10);
        _info3 = new Canvas(game.width, game.height);
        SetChildIndex(_info3, 10);
        _info4 = new Canvas(game.width, game.height);
        SetChildIndex(_info4, 10);

        _icon1 = new Sprite("assets/icons/controls_icon1.png");
        _icon1.SetXY(game.width * 0.1f, game.height * 0.08f);
        AddChild(_icon1);

        _icon2 = new Sprite("assets/icons/controls_icon2.png");
        _icon2.SetXY(game.width * 0.1f, game.height * 0.25f);
        AddChild(_icon2);

        _icon3 = new Sprite("assets/icons/controls_icon3.png");
        _icon3.SetXY(game.width * 0.15f, game.height * 0.44f);
        AddChild(_icon3);

        _icon4 = new Sprite("assets/icons/controls_icon4.png");
        _icon4.SetXY(game.width * 0.1f, game.height * 0.6f);
        AddChild(_icon4);

        

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
        //_info.graphics.DrawImage(Image.FromFile("assets/icons/controls_icon1.png"), game.width * 0.1f, game.height * 0.1f);
        _info1.graphics.DrawString("Shoot your bullet in the" + "\n"
                                 + "direction of your mouse",
                                 _font, Brushes.AliceBlue, game.width * 0.40f, game.height * 0.1f);

        //_info.graphics.DrawImage(Image.FromFile("assets/icons/controls_icon2.png"), game.width * 0.1f, game.height * 0.27f);
        _info2.graphics.DrawString("Your bullets are affected" + "\n"
                                 + "by the gravity of planets",
                                 _font, Brushes.AliceBlue, game.width * 0.40f, game.height * 0.27f);

        //_info.graphics.DrawImage(Image.FromFile("assets/icons/controls_icon3.png"), game.width * 0.1f, game.height * 0.44f);
        _info3.graphics.DrawString("You can switch bullet upgrades" + "\n"
                                 + "by pressing these number keys",
                                 _font, Brushes.AliceBlue, game.width * 0.40f, game.height * 0.44f);

        //_info.graphics.DrawImage(Image.FromFile("assets/icons/controls_icon4.png"), game.width * 0.1f, game.height * 0.61f);
        _info4.graphics.DrawString("You buy upgrades with Starbux" + "\n"
                                 + "Starbux is earned by destroying" + "\n"
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