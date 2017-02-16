using System;
using GXPEngine;


public abstract class Bullet : Sprite
{
    public enum BulletType
    {
        STANDARD,
        RICOCHET
    }

    protected Sound _shotSound;
    protected Sound _hitSound;
    protected float _speed;

    public Vec2 position { get; set; }
    public Vec2 velocity { get; set; }
	public float damage { get; set; }

    public Bullet(Vec2 position, Vec2 velocity, float damage, string pAsset, string shotSound, string hitSound,  float speed) : base(pAsset)
    {
        this.position = position;
        this.velocity = velocity.Scale(speed);
		this.damage = damage;
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

    private void BulletTrail()
    {
        Particle _particle = new Particle("assets/bullet.png", 1, 1);
        game.AddChild(_particle);
        _particle.SetXY(x, y);
        _particle.rotation = rotation;
    }

    public void Update()
    {
        Move();
        BulletTrail();
    }

    public override void Destroy()
    {
        _hitSound.Play();
        base.Destroy();
    }
}