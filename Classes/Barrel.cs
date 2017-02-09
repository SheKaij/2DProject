using System;
using GXPEngine;

public class Barrel : Sprite
{
    public Barrel() : base("assets\\tanks\\barrels\\pz_kpfw_iv.png")
    {
        SetOrigin(width / 2, height / 2);
    }

    public void BarrelRotation()
    {
        Vec2 _distance = new Vec2();
        _distance.x = Input.mouseX - parent.x;
        _distance.y = Input.mouseY - parent.y;
        rotation = _distance.GetAngleDegrees() - parent.rotation;
    }
}