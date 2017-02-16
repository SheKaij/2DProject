using System;
using System.Drawing;
using System.Collections.Specialized;
using System.Drawing.Text;
using GXPEngine;

public class HUD : Sprite
{
    private Level _level;

    private Canvas _currentPlayer;
    private Canvas _shotsleft;
    private Canvas _timeLeft;

    private AnimationSprite _currentBullet;

    private PrivateFontCollection _pfc;
    private Font _font;

    public HUD(Level pLevel) : base("assets/menu/hud_overlay3.png")
    {
        _level = pLevel;

        _currentPlayer = new Canvas(width, height);
        SetChildIndex(_currentPlayer, 1);

        _shotsleft = new Canvas(width, height);
        SetChildIndex(_shotsleft, 1);

        _timeLeft = new Canvas(width, height);
        SetChildIndex(_timeLeft, 1);

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
    }

    private void HandleCurrentBullet()
    {
        if (Input.GetKeyDown(Key.ONE))
        {
            _currentBullet.currentFrame = 0;
        }

        else if (Input.GetKeyDown(Key.TWO))
        {
            _currentBullet.currentFrame = 1;
        }

        else if (Input.GetKeyDown(Key.THREE))
        {
            _currentBullet.currentFrame = 2;
        }

        else if (Input.GetKeyDown(Key.FOUR))
        {
            _currentBullet.currentFrame = 3;
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
    }

    private void Update()
    {
        HandleInfo();
        HandleBulletCount();
        TextOpacity();
        HandleCurrentBullet();
    }
}
