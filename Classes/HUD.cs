using System;
using System.Drawing;
using System.Collections.Specialized;
using System.Drawing.Text;
using GXPEngine;

public class HUD : Sprite
{
    private Level _level;

    private Canvas _currentPlayer;
    private PrivateFontCollection _pfc;
    private Font _font;

    public HUD(Level pLevel) : base("assets/menu/hud_overlay.png")
    {
        _level = pLevel;

        _currentPlayer = new Canvas(game.width, 128);
        SetChildIndex(_currentPlayer, 1);
        _currentPlayer.x = 0;
        _currentPlayer.y = 0;

        _pfc = new PrivateFontCollection();
        _pfc.AddFontFile("assets\\font\\earthorbiter.ttf");
        _font = new Font(_pfc.Families[0], 24);
    }

    private void HandleInfo()
    {
        if (_level.GetCurrentPlayer() == "1")
        {
            _currentPlayer.graphics.Clear(Color.Transparent);
            _currentPlayer.graphics.DrawString("Current Player: " + _level.GetCurrentPlayer(), _font, Brushes.AliceBlue, width * 0.15f , 0);
        }

        else if (_level.GetCurrentPlayer() == "2")
        {
            _currentPlayer.graphics.Clear(Color.Transparent);
            _currentPlayer.graphics.DrawString("Current Player: " + _level.GetCurrentPlayer(), _font, Brushes.AliceBlue, width * 0.15f, 0);
        }
    }

    private void Update()
    {
        HandleInfo();
    }
}
