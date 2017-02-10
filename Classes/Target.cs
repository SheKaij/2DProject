using System;
using System.Drawing;
using GXPEngine;

public class Target : Sprite
{
    public readonly int radius;
    private Vec2 _position;

    public Target(int pRadius, Vec2 pPosition = null, Color? pColor = null) : base("assets\\prototype_planet.png")
    {
        radius = pRadius;
        position = pPosition;
        SetOrigin(width / 2, height / 2);

        SetScaleXY(0.5f);
        x = game.width / 2;
        y = game.height / 2;

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
}