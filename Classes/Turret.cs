using System;
using GXPEngine;

public class Turret : Sprite
{
    public Turret() : base("assets\\spaceship\\turret.png")
    {
        SetOrigin(width / 2, height / 2);
    }

    public void Move()
    {
        rotation = new Vec2(Input.mouseX - parent.x, Input.mouseY - parent.y).GetAngleDegrees() - parent.rotation;
    }
}