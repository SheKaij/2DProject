using System;
using System.Drawing;
using GXPEngine;

public class Planet : Sprite
{
    private int _radius;

    public Vec2 position { get; set; }
    public int health { get; set; }

    public Planet(Vec2 position, int health) : base("assets\\prototype_planet.png")
    {
        _radius = height / 8;
        this.position = position;
        this.health = health;

        SetOrigin(width / 2, height / 2);

        SetScaleXY(0.25f);
        x = position.x;
        y = position.y;
    }

    public bool Contains(Vec2 vector)
    {
        return position.Clone().Substract(vector).Length() <= _radius;
    }

    private void Update()
    {
        if (health <= 0)
        {
            this.Destroy();
        }
    }
}