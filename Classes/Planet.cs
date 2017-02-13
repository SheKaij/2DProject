using System;
using System.Drawing;
using GXPEngine;

public class Planet : Sprite
{
    public readonly int radius;
    private Vec2 _position;
    private int _health;

    public Planet(int pRadius, int pHealth, Vec2 pPosition = null, Color? pColor = null) : base("assets\\prototype_planet.png")
    {
        radius = pRadius;
        position = pPosition;
        health = pHealth;
        SetOrigin(width / 2, height / 2);

        SetScaleXY(0.25f);
        x = game.width / 2;
        y = game.height / 2;

        

    }

    public int health
    {
        set
        {
            _health = value;
        }
        get
        {
            return _health;
        }
    }

    public Vec2 position
    {
        set
        {
            _position = value ?? Vec2.zero;
        }
        get
        {
            return _position;
        }
    }

    private void Update()
    {
        if (health <= 0)
        {
            this.Destroy();
        }
    }
}