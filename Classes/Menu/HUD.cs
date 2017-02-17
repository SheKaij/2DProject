using System;
using System.Drawing;
using System.Collections.Specialized;
using System.Drawing.Text;
using GXPEngine;
using static Bullet;

public class HUD : Sprite
{
    private Level _level;
    private Spaceship _player1;
    private Spaceship _player2;

    private Canvas _currentPlayer, _currency, _shotsleft, _timeLeft, _fuelLeft;

    private AnimationSprite _currentBullet;
    private Sprite _fuelMeter;

    private PrivateFontCollection _pfc;
    private Font _font;

    public HUD(Level pLevel) : base("assets/menu/hud_overlay3.png")
    {
        _level = pLevel;

        _fuelMeter = new Sprite("assets/menu/small_button2.png");
        AddChild(_fuelMeter);
        _fuelMeter.SetScaleXY(0.5f, 0.45f);
        _fuelMeter.x -= width * 0.119f;
        _fuelMeter.y = height * 0.06f;

        _currentPlayer = new Canvas(width, height);
        SetChildIndex(_currentPlayer, 10);

        _currency = new Canvas(width, height);
        SetChildIndex(_currency, 10);

        _shotsleft = new Canvas(width, height);
        SetChildIndex(_shotsleft, 10);

        _timeLeft = new Canvas(width, height);
        SetChildIndex(_timeLeft, 10);

        _fuelLeft = new Canvas(game.width, game.height);
        SetChildIndex(_fuelLeft, 10);
        _fuelLeft.x -= 40;

        _currentBullet = new AnimationSprite("assets/menu/bullet_sheet.png", 4, 1);
        AddChild(_currentBullet);
        _currentBullet.SetScaleXY(0.5f);
        _currentBullet.currentFrame = 0;
        _currentBullet.x = width * 0.95f;
        _currentBullet.y = height * 0.06f;

        

        _pfc = new PrivateFontCollection();
        _pfc.AddFontFile("assets\\font\\earthorbiter.ttf");
        _font = new Font(_pfc.Families[0], 24);
    }

    private void TextOpacity()
    {
        _currentPlayer.alpha = alpha;
        _shotsleft.alpha = alpha;
        _timeLeft.alpha = alpha;
        _currentBullet.alpha = alpha;
        _fuelMeter.alpha = alpha;
    }

    
    
    private void HandleCurrentBullet()
    {
        switch (_level.GetCurrentShip().bulletType)
        {
            case BulletType.STANDARD:
                _currentBullet.currentFrame = 0;
                break;
            case BulletType.CONTROLLED:
                _currentBullet.currentFrame = 1;
                break;
            case BulletType.CLUSTER:
                _currentBullet.currentFrame = 2;
                break;
            case BulletType.RICOCHET:
                _currentBullet.currentFrame = 3;
                break;
        }
     }

    private void HandleBulletCount()
    {
        for (int i = 0; i < _level.GetBulletCount(); i++)
        {
            //_shotsleft.graphics.DrawImage(Image.FromFile("assets/bullet.png"), (width * 0.15f) * i, height / 2, 128, 128);

            _shotsleft.graphics.Clear(Color.Transparent);
            _shotsleft.graphics.DrawString("Bullets Left: " + _level.GetBulletCount(), _font, Brushes.AliceBlue, width * 0.2f, height * 0.33f);
        }
    }

    private void HandleInfo()
    {
        if (_level.GetCurrentPlayer() == "1")
        {
            _currentPlayer.graphics.Clear(Color.Transparent);
            _currentPlayer.graphics.DrawString("Current Player: " + _level.GetCurrentPlayer(), _font, Brushes.AliceBlue, width * 0.15f , height * 0.1f);
        }

        else if (_level.GetCurrentPlayer() == "2")
        {
            _currentPlayer.graphics.Clear(Color.Transparent);
            _currentPlayer.graphics.DrawString("Current Player: " + _level.GetCurrentPlayer(), _font, Brushes.AliceBlue, width * 0.13f, height * 0.1f);
        }

        _timeLeft.graphics.Clear(Color.Transparent);
        _timeLeft.graphics.DrawString(_level.GetTurnTimer(), _font, Brushes.AliceBlue, width * 0.45f, height * 0.70f);

        _fuelLeft.graphics.Clear(Color.Transparent);
        _fuelLeft.graphics.DrawString(_level.GetCurrentShip().fuel.ToString(), _font, Brushes.AliceBlue, 0, _fuelMeter.height / 2);
    }

    private void Update()
    {
        HandleInfo();
        HandleBulletCount();
        TextOpacity();
        HandleCurrentBullet();
    }
}
