using System;
using System.Drawing.Text;
using System.Drawing;
using GXPEngine;

public class StoreScreen : GameObject
{
    private MyGame _myGame;

    private Sprite _bg, _window, _storeTitle;
    private Canvas _textDefault, _textCluster, _textThruster, _textRicochet, _textPlayer;
    private string _currentPlayer = "Current player: 1";

    private Button _backButton, _arrowBackButton, _arrowNextButton, _defaultBullet, _upgradeCluster, _upgradeThruster, _upgradeRicochet;
    private PrivateFontCollection _pfc;
    private Font _font, _fontMega;

	private Sound _storeMusic;
	public SoundChannel playMusic { get; set; }

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




        // 0.18f difference
        _defaultBullet = new Button("assets/menu/bullet_sheet.png", 4, 1);
        AddChild(_defaultBullet);
        _defaultBullet.currentFrame = 0;
        _defaultBullet.x = game.width * 0.35f;
        _defaultBullet.y = game.height * 0.45f;

        _upgradeCluster = new Button("assets/menu/bullet_sheet.png", 4, 1);
        AddChild(_upgradeCluster);
        _upgradeCluster.currentFrame = 1;
        _upgradeCluster.x = game.width * 0.35f;
        _upgradeCluster.y = game.height * 0.65f;

        _upgradeThruster = new Button("assets/menu/bullet_sheet.png", 4, 1);
        AddChild(_upgradeThruster);
        _upgradeThruster.currentFrame = 2;
        _upgradeThruster.x = game.width * 0.85f;
        _upgradeThruster.y = game.height * 0.45f;

        _upgradeRicochet = new Button("assets/menu/bullet_sheet.png", 4, 1);
        AddChild(_upgradeRicochet);
        _upgradeRicochet.currentFrame = 3;
        _upgradeRicochet.x = game.width * 0.85f;
        _upgradeRicochet.y = game.height * 0.65f;




        _storeTitle = new Sprite("assets/menu/store_title.png");
        AddChild(_storeTitle);
        _storeTitle.alpha = 0;
        



        _backButton = new Button("assets/menu/next_button.png");
        AddChild(_backButton);
        _backButton.x = _window.width  / 2;
        _backButton.y = _window.height - _backButton.height * 1.2f;

        _arrowBackButton = new Button("assets/menu/arrow_button.png");
        AddChild(_arrowBackButton);
        _arrowBackButton.Mirror(true, false);
        _arrowBackButton.x = game.width * 0.35f;
        _arrowBackButton.y = game.height * 0.275f;

        _arrowNextButton = new Button("assets/menu/arrow_button.png");
        AddChild(_arrowNextButton);
        _arrowNextButton.x = game.width * 0.7f;
        _arrowNextButton.y = game.height * 0.275f;



        _pfc = new PrivateFontCollection();
        _pfc.AddFontFile("assets\\font\\earthorbiter.ttf");
        _font = new Font(_pfc.Families[0], 34);
        _fontMega = new Font(_pfc.Families[0], 80);

        _textPlayer = new Canvas(game.width, game.height);
        SetChildIndex(_textPlayer, 10);
        _textPlayer.alpha = 0;

        _textDefault = new Canvas(game.width, game.height);
        SetChildIndex(_textDefault, 10);
        _textDefault.alpha = 0;

        _textCluster = new Canvas(game.width, game.height);
        SetChildIndex(_textCluster, 10);
        _textCluster.alpha = 0;

        _textThruster = new Canvas(game.width, game.height);
        SetChildIndex(_textThruster, 10);
        _textThruster.alpha = 0;

        _textRicochet = new Canvas(game.width, game.height);
        SetChildIndex(_textRicochet, 10);
        _textRicochet.alpha = 0;

        _storeMusic = new Sound("assets/sfx/levelmusic.mp3", true, true);
        playMusic = _storeMusic.Play();

        DrawText();
    }

    private void DrawCurrentPlayer()
    {
        _textPlayer.graphics.Clear(Color.Transparent);
        _textPlayer.graphics.DrawString(_currentPlayer, _font, Brushes.AliceBlue, game.width * 0.4f, game.height * 0.25f);
    }

    //ð = currency

    private void DrawText()
    {
        _textDefault.graphics.DrawString("Default bullet:" + "\n"
                                       + "no added effect" + "\n"
                                       + "damage: 1", _font, Brushes.AliceBlue, game.width * 0.07f, _defaultBullet.y - _defaultBullet.height / 2);
        _textCluster.graphics.DrawString("Cluster bullet:" + "\n"
                                       + "split bullet in 3" + "\n"
                                       + "damage: 0.5", _font, Brushes.AliceBlue, game.width * 0.07f, _upgradeCluster.y - _upgradeCluster.height / 2);
        _textThruster.graphics.DrawString("Thruster bullet:" + "\n"
                                       + "thrust to the mouse" + "\n"
                                       + "damage: 1", _font, Brushes.AliceBlue, game.width * 0.5f, _upgradeThruster.y - _upgradeThruster.height / 2);
        _textRicochet.graphics.DrawString("Ricochet bullet:" + "\n"
                                       + "bullets bounce off" + "\n"
                                       + "damage: 0.8", _font, Brushes.AliceBlue, game.width * 0.5f, _upgradeRicochet.y - _upgradeRicochet.height / 2);
    }

    private void WindowAppear()
    {
        if (_window.alpha <= 1)
        {
            _window.alpha += 0.05f;
        }

        _storeTitle.alpha = _window.alpha;
        _textDefault.alpha = _window.alpha;
        _textCluster.alpha = _window.alpha;
        _textThruster.alpha = _window.alpha;
        _textRicochet.alpha = _window.alpha;
        _textPlayer.alpha = _window.alpha;
    }

    private void HandleButtons()
    {
        if (Input.GetMouseButtonUp(0) && _backButton.MouseHover())
        {
            _myGame.SetState(MyGame.GameState.LEVEL);
			playMusic.Stop();
        }

        if (Input.GetMouseButtonUp(0) && _arrowBackButton.MouseHover() && _currentPlayer != "Current player: 1")
        {
            _currentPlayer = "Current player: 1";
        }

        else if (Input.GetMouseButtonUp(0) && _arrowNextButton.MouseHover() && _currentPlayer != "Current player: 2")
        {
            _currentPlayer = "Current Player: 2";
        }
    }

    private void Update()
    {
        DrawCurrentPlayer();
        HandleButtons();
        WindowAppear();
    }
}