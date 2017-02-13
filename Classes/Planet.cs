using System;
using System.Drawing;
using GXPEngine;

public class Planet : Sprite
{
    public enum PlanetType
    {
        SMALL,
        MEDIUM,
        BIG,
        LARGE
    }

    private int _radius;

    public Vec2 position { get; set; }
    public float mass { get; set; }
    public int health { get; set; }

    public Planet(Vec2 position, float mass, int health, String asset) : base(asset)
    {
        _radius = height / 8;
        this.position = position;
        this.mass = mass;
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