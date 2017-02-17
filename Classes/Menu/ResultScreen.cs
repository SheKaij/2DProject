using System;
using System.Drawing;
using System.Collections.Specialized;
using System.Drawing.Text;
using GXPEngine;

public class ResultScreen : GameObject
{
    private MyGame _myGame;

    private Sprite _bg, _window;
    private Canvas _playerWon;

    private Button _storeButton, _nextButton;
    private PrivateFontCollection _pfc;
    private Font _font, _fontMega;

    private string _currentPlayer;

	private Sound _victoryMusic;
	private SoundChannel _victoryChannel;

    public ResultScreen(MyGame pMyGame) : base()
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

        _playerWon = new Canvas(_window.width, _window.height);
        SetChildIndex(_playerWon, 10);

        _storeButton = new Button("assets/menu/store_button.png");
        AddChild(_storeButton);
        _storeButton.x = _window.width * 0.292f; ;
        _storeButton.y = _window.height - _storeButton.height * 1.33f;

        _nextButton = new Button("assets/menu/next_button.png");
        AddChild(_nextButton);
        _nextButton.x = _window.width * 0.72f; ;
        _nextButton.y = _window.height - _nextButton.height * 1.33f;

        _pfc = new PrivateFontCollection();
        _pfc.AddFontFile("assets\\font\\earthorbiter.ttf");
        _font = new Font(_pfc.Families[0], 36);
        _fontMega = new Font(_pfc.Families[0], 60);

		_victoryMusic = new Sound("assets\\sfx\\victorymusic.mp3", false, true);
		_victoryChannel = _victoryMusic.Play();
    }

    public string GetCurrentPlayer()
    {
        return _currentPlayer;
    }

    public void SetCurrentPlayer(string pCurrentPlayer)
    {
        _currentPlayer = pCurrentPlayer;
    }
    private void DrawText()
    {
        _playerWon.graphics.DrawString("Player " + _currentPlayer +/*_level.GetCurrentPlayer() +*/ " won!", _fontMega, Brushes.AliceBlue, _window.width * 0.36f /*(_storeButton.x / _nextButton.x)*/, _window.height * 0.1f);
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
        if (Input.GetMouseButtonUp(0) && _storeButton.MouseHover())
        {
            _myGame.SetState(MyGame.GameState.STORE);
			_victoryChannel.Stop();
        }

        else if (Input.GetMouseButtonUp(0) && _nextButton.MouseHover())
        {
            _myGame.SetState(MyGame.GameState.LEVEL);
			_victoryChannel.Stop();
        }
    }

    private void Update()
    {
        HandleButtons();
        WindowAppear();
        DrawText();
    }
}