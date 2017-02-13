using System;
using GXPEngine;


public abstract class Bullet : Sprite
{
    protected Sound _shotSound;
    protected Sound _hitSound;
    protected float _speed;
    
    public Vec2 position { get; set; }
    public Vec2 velocity { get; set; }

    

    public Bullet(string pAsset) : base(pAsset)
    {
        SetOrigin(width / 2, height / 2);

        x = position.x;
        y = position.y;
        rotation = velocity.GetAngleDegrees();
    }

    protected abstract void Move();

    private void HandleBoarders()
    {
        
        if (x > game.width || x < 0 || y > game.height || y < 0)
        {
            Destroy();
        }
    }

    public void Update()
    {
        Move();
        HandleBoarders();
    }

    public override void Destroy()
    {
        _hitSound.Play();
        base.Destroy();
    }
}

