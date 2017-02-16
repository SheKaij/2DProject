using System;
using System.Drawing.Text;
using System.Drawing;
using GXPEngine;

public class StoreScreen : GameObject
{
    private MyGame _myGame;

    private Sprite _bg, _window, _storeTitle;
    private Canvas _upgradeText;
    

    private Button _backButton, _arrowButton, _upgradeCluster, _upgradeThruster, _upgradeRicochet;
    private PrivateFontCollection _pfc;
    private Font _font, _fontMega;

    public StoreScreen(MyGame pMyGame) : base()
    {
        _myGame = pMyGame;

        _bg = new Sprite("assets/background.png");
        AddChild(_bg);

        _window = new Sprite("assets/menu/window.png");
        AddChild(_window);
        _window.alpha = 0;
        _window.SetOrigin(_window.width / 2, _window.height / 2);
        _window.x = game.width / 2;
        _window.y = game.height / 2;

        _upgradeCluster = new Button("assets/menu/bullet_sheet.png", 3, 1);
        AddChild(_upgradeCluster);
        _upgradeCluster.currentFrame = 0;
        _upgradeCluster.x = game.width / 2 - _upgradeCluster.width / 2;
        // 0.18f difference
        _upgradeCluster.y = game.height * 0.32f;

        _upgradeThruster = new Button("assets/menu/bullet_sheet.png", 3, 1);
        AddChild(_upgradeThruster);
        _upgradeThruster.currentFrame = 1;
        _upgradeThruster.x = game.width / 2 - _upgradeThruster.width / 2;
        _upgradeThruster.y = game.height * 0.50f;

        _upgradeRicochet = new Button("assets/menu/bullet_sheet.png", 3, 1);
        AddChild(_upgradeRicochet);
        _upgradeRicochet.currentFrame = 2;
        _upgradeRicochet.x = game.width / 2 - _upgradeRicochet.width / 2;
        _upgradeRicochet.y = game.height * 0.68f;

        _storeTitle = new Sprite("assets/menu/store_title.png");
        AddChild(_storeTitle);


        _backButton = new Button("assets/menu/back_button.png");
        AddChild(_backButton);
        _backButton.x = _window.width  / 2;
        _backButton.y = _window.height - _backButton.height * 1.2f;

        _arrowButton = new Button("assets/menu/arrow_button.png");
        //AddChild(_arrowButton);
        //_arrowButton.x = _upgradeIcon.x + _upgradeIcon.width * 1.5f;
        //_arrowButton.y = _upgradeIcon.y + _upgradeIcon.height / 2;

        _pfc = new PrivateFontCollection();
        _pfc.AddFontFile("assets\\font\\earthorbiter.ttf");
        _font = new Font(_pfc.Families[0], 36);
        _fontMega = new Font(_pfc.Families[0], 80);
    }

    //ð = currency

    private void DrawText()
    {
        _upgradeText.graphics.DrawString("cluster", _font, Brushes.AliceBlue, 0, 0);
        _upgradeText.graphics.DrawString("thruster", _font, Brushes.AliceBlue, 0, 0);
        _upgradeText.graphics.DrawString("ricochet", _font, Brushes.AliceBlue, 0, 0);
    }

    private void WindowAppear()
    {
        if (_window.alpha <= 1)
        {
            _window.alpha += 0.05f;
        }
    }

    private void HandleButtons()
    {
        if (Input.GetMouseButtonUp(0) && _backButton.MouseHover())
        {
            _myGame.SwitchState(MyGame.GameState.RESULT);
            _myGame.StopState(MyGame.GameState.STORE);
        }
    }

    private void Update()
    {
        HandleButtons();
        WindowAppear();
        //DrawText();
    }
}