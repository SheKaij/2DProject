using System;
using GXPEngine;


public class Bullet : Sprite
{
    private Vec2 _position;
    private Vec2 _velocity;

    public Bullet(Vec2 pPosition = null, Vec2 pVelocity = null) : base("tank_assets\\bullet.png")
    {
        position = pPosition;
        velocity = pVelocity;

        x = position.x;
        y = position.y;
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

    public Vec2 velocity
    {
        set
        {
            _velocity = value ?? Vec2.zero;
        }
        get
        {
            return _velocity;
        }
    }

    public void Update()
    {
        // Add the angle to the position
        _position.Add(_velocity);

        x = _position.x;
        y = _position.y;
    }

    private void OnCollision(GameObject other)
    {
        if (other is Target)
        {
            Destroy();
            //Increase score
        }

        if (other is TextField)
        {
            Destroy();
        }
    }
}

