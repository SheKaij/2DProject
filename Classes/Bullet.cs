using System;
using GXPEngine;


public abstract class Bullet : Sprite
{
    public enum BulletType
    {
        STANDARD,
        CLUSTER
    }

    protected Sound _shotSound;
    protected Sound _hitSound;
    protected float _speed;

    public Vec2 position { get; set; }
    public Vec2 velocity { get; set; }

    public Bullet(Vec2 position, Vec2 velocity, string pAsset, String shotSound,  String hitSound,  float speed) : base(pAsset)
    {
        this.position = position;
        this.velocity = velocity.Scale(speed);

        _shotSound = new Sound (shotSound);
        _hitSound = new Sound (hitSound);
        _speed = speed;

        SetOrigin(width / 2, height / 2);

        x = position.x;
        y = position.y;
        rotation = velocity.GetAngleDegrees();

        _shotSound.Play();
    }

    protected abstract void Move();

    public void Update()
    {
        Move();
    }

    public override void Destroy()
    {
        _hitSound.Play();
        base.Destroy();
    }
}