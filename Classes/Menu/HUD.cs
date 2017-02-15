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

    private PrivateFontCollection _pfc;
    private Font _font;

    public HUD(Level pLevel) : base("assets/menu/hud_overlay.png")
    {
        _level = pLevel;

        _currentPlayer = new Canvas(width, height);
        SetChildIndex(_currentPlayer, 1);

        _shotsleft = new Canvas(width, height);
        SetChildIndex(_shotsleft, 1);

        _timeLeft = new Canvas(width, height);
        SetChildIndex(_timeLeft, 1);

        _pfc = new PrivateFontCollection();
        _pfc.AddFontFile("assets\\font\\earthorbiter.ttf");
        _font = new Font(_pfc.Families[0], 24);
    }

    private void HandleBulletCount()
    {
        for (int i = 0; i < _level.GetBulletCount(); i++)
        {
            //_shotsleft.graphics.DrawImage(Image.FromFile("assets/bullet.png"), (width * 0.15f) * i, height / 2, 128, 128);

            _shotsleft.graphics.Clear(Color.Transparent);
            _shotsleft.graphics.DrawString("Bullets Left: " + _level.GetBulletCount(), _font, Brushes.AliceBlue, width * 0.17f, height * 0.33f);
        }
    }

    private void HandleInfo()
    {
        if (_level.GetCurrentPlayer() == "1")
        {
            _currentPlayer.graphics.Clear(Color.Transparent);
            _currentPlayer.graphics.DrawString("Current Player: " + _level.GetCurrentPlayer(), _font, Brushes.AliceBlue, width * 0.10f , height * 0.15f);
        }

        else if (_level.GetCurrentPlayer() == "2")
        {
            _currentPlayer.graphics.Clear(Color.Transparent);
            _currentPlayer.graphics.DrawString("Current Player: " + _level.GetCurrentPlayer(), _font, Brushes.AliceBlue, width * 0.10f, height * 0.15f);
        }

        _timeLeft.graphics.Clear(Color.Transparent);
        _timeLeft.graphics.DrawString(_level.GetTurnTimer(), _font, Brushes.AliceBlue, width * 0.44f, height * 0.70f);
    }

    private void Update()
    {
        HandleInfo();
        HandleBulletCount();
    }
}
