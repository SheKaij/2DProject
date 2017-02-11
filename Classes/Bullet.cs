using System;
using GXPEngine;


public class Bullet : Sprite
{
    private Sound _sfxDestroyed = new Sound("assets\\sfx\\placeholder_hit2.wav", false, true);
    private Sound _sfxShot = new Sound("assets\\sfx\\placeholder_shoot2.wav");

    public Vec2 position { get; set; }
    public Vec2 velocity { get; set; }
    public float speed { get; set; }

    

    public Bullet(Vec2 pPosition = null, float pSpeed = 0) : base("assets\\bullet.png")
    {
        position = pPosition;
        velocity = new Vec2(0,0);
        speed = pSpeed;
        

        SetOrigin(width / 2, height / 2);

        x = position.x;
        y = position.y;

        _sfxShot.Play();
    }




    public void Update()
    {
        rotation = velocity.GetAngleDegrees();

        position.Add(velocity);
        x = position.x;
        y = position.y;
    }

    public override void Destroy()
    {
        _sfxDestroyed.Play();
        base.Destroy();
    }
}

