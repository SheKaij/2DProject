using System;
using GXPEngine;


public class Bullet : Sprite
{
    private Vec2 _position;
    private Vec2 _velocity;

    private Sound _sfxDestroyed;

    public Bullet(Vec2 pPosition = null, Vec2 pVelocity = null) : base("assets\\bullet.png")
    {
        SetOrigin(width / 2, height / 2);
        position = pPosition;
        velocity = pVelocity;

        _sfxDestroyed = new Sound("assets\\sfx\\placeholder_hit2.wav", false, true);

        x = position.x;
        y = position.y;
    }

    public Sound sfxDestroyed
    {
        set
        {
            _sfxDestroyed = value;
        }
        get
        {
            return _sfxDestroyed;
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
}

