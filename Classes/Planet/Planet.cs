using System;
using System.Drawing;
using GXPEngine;

public class Planet : AnimationSprite
{
    public enum PlanetType
    {
        SMALL,
        MEDIUM,
        BIG,
        LARGE
    }

    public int radius { get; set; }
	private int _timer = 100;

    public Vec2 position { get; set; }
    public float mass { get; set; }
    public float health { get; set; }
	public float maxHealth { get; set; }

	public bool planetstage0, planetStage1, planetStage2, planetStage3, planetStage4;

	private Sound planetExplode;
	private SoundChannel planetChannel;

    public Planet(Vec2 position, float mass, float maxHealth, float scale, string asset) : base(asset, 3, 2)
    {
        radius = (int)(400/2*scale);
        this.position = position;
        this.mass = mass;
		this.maxHealth = maxHealth;
        health = maxHealth;

        SetOrigin(width / 2, height / 2);

        SetScaleXY(scale);
        x = position.x;
        y = position.y;

		planetExplode = new Sound("assets\\sfx\\planetexplosion.wav");
		planetChannel = planetExplode.Play();
		planetChannel.IsPaused = true;
    }

    public bool Contains(Vec2 vector)
    {
        return position.Clone().Substract(vector).Length() <= radius;
    }

    private void Update()
    {
		if (health <= maxHealth * 0.8 && !planetStage1)
		{
			planetStage1 = true;
			SetFrame(1);
		}

		if (health <= maxHealth * 0.6 && !planetStage2)
		{
			planetStage2 = true;
			SetFrame(2);
		}

		if (health <= maxHealth * 0.4 && !planetStage3)
		{
			planetStage3 = true;
			SetFrame(3);
		}

		if (health <= maxHealth * 0.2 && !planetStage4)
		{
			planetStage4 = true;
			SetFrame(4);
		}

        if (health <= 0)
        {
			if (planetChannel.IsPaused)
			{
				planetChannel.IsPaused = false;
			}
			_timer--;
			alpha = _timer / 100f;
			SetFrame(5);
            radius = 0;
            mass = 0;

			if (_timer <= 0)
			{
				this.Destroy();
			}
        }
    }
}