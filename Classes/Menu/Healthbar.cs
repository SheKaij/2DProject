using System;
using System.Drawing;
using GXPEngine;

public class Healthbar : GameObject
{
    private Spaceship _spaceship;

    private const int HEALTH_BAR_WIDTH = 250;
    private const int HEALTH_BAR_OUTLINE_WIDTH = HEALTH_BAR_WIDTH + 1;
    private int _healthBarWidth;
    private Brush _healthBrushColor;

    private Canvas _shipCanvas;

    public Healthbar(Spaceship pSpaceship) : base()
    {
        _spaceship = pSpaceship;
        _shipCanvas = new Canvas(game.width, game.height);
        _shipCanvas.alpha = 0.85f;
        SetChildIndex(_shipCanvas, 1);
    }

    private Brush GetHealthBrushColor()
    {
        // more than 75% and lower or equal than 100%;
        if (_spaceship.health > (Mathf.Ceiling(Convert.ToSingle(_spaceship.maxHealth) / 100 * 75)) && _spaceship.health <= _spaceship.maxHealth)
        {
            _healthBrushColor = Brushes.ForestGreen;
            // more or equal than 25% and less than 75%
        }
        else if (_spaceship.health > (Mathf.Ceiling(Convert.ToSingle(_spaceship.maxHealth) / 100 * 25)) && _spaceship.health <= (Mathf.Floor(Convert.ToSingle(_spaceship.maxHealth) / 100 * 75)))
        {
            _healthBrushColor = Brushes.OrangeRed;

            // more or equal than 0% and less than 25%
        }
        else if (_spaceship.health >= (Convert.ToSingle(_spaceship.maxHealth) - _spaceship.maxHealth) && _spaceship.health <= (Mathf.Floor(Convert.ToSingle(_spaceship.maxHealth) / 100 * 25)))
        {
            _healthBrushColor = Brushes.Red;

        }
        return _healthBrushColor;
    }

    private int GetHealthBarWidth()
    {
        if (_spaceship.maxHealth - _spaceship.health == 0)
        { // full hp
            _healthBarWidth = HEALTH_BAR_WIDTH; // full size of the bar
        }
        else if (_spaceship.maxHealth - _spaceship.health == _spaceship.maxHealth)
        { // dead
            _healthBarWidth = 0;
        }
        else
        {
            // what is the percentage of your current hp, compared to your max hp
            float _singlePoint = Mathf.Round((float)_spaceship.health / _spaceship.maxHealth * 100);
            // get new width
            _healthBarWidth = Mathf.Round((float)HEALTH_BAR_WIDTH / 100 * _singlePoint);
        }
        return _healthBarWidth;
    }

    private void Update()
    {
        _shipCanvas.graphics.Clear(Color.Transparent);
        _shipCanvas.graphics.FillRectangle(GetHealthBrushColor(), 0, 0, GetHealthBarWidth(), 20); // hp bar itself
    }
}
