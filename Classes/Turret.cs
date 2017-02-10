using System;
using GXPEngine;

public class Turret : Sprite
{
    public Turret() : base("assets\\spaceship\\turret.png")
    {
        SetOrigin(width / 2, height / 2);
    }

    public void TurretRotation()
    {
        if (parent != null)
        {
            Vec2 _distance = new Vec2();
            _distance.x = Input.mouseX - parent.x;
            _distance.y = Input.mouseY - parent.y;
            rotation = _distance.GetAngleDegrees() - parent.rotation;
        }
    }
}