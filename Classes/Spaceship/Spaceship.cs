﻿using System;
using System.Collections.Generic;
using System.Drawing;
using static Bullet;

namespace GXPEngine
{
    public class Spaceship : AnimationSprite
    {
        private readonly float ACCELERATION = 0.5f;
        private readonly float ANGULAR_ACCELERATION = 0.2f;
        private readonly float FRICTION = 0.95f;

        public readonly int MAX_BULLET = 3;
        public readonly int HEALTH = 30;
        public readonly int MAX_HEALTH = 30;
        public readonly int MAX_FUEL = 50;

        public Turret turret { get; set; }
        public List<Bullet> bullets { get; set; }

        public Vec2 position { get; set; }
        public Vec2 velocity { get; set; }
        public float angular_velocity { get; set; }
        public bool isActive { get; set; }
        public BulletType bulletType { get; set; }
        public int bulletCount { get; set; }
        
        public int currency { get; set; }
        public float health { get; set; }
        public float maxHealth { get; set; }
        public bool shopping { get; set; }
        
		public int fuel { get; set; }
        private Sound _sfxEngine;
        private AnimationSprite _thrusterFlame;
        private int _thrusterTimer = 10;
        
		public Spaceship(string pFilename, Vec2 pPosition, int pRotation, bool pIsActive) : base(pFilename, 2, 1)
        {
            turret = new Turret();
            bullets = new List<Bullet>();
            position = pPosition;
            velocity = Vec2.zero;
            angular_velocity = 0;
            isActive = pIsActive;
            bulletType = BulletType.STANDARD;

            bulletCount = MAX_BULLET;
			health = HEALTH;
            maxHealth = MAX_HEALTH;
			fuel = MAX_FUEL;

            //_healthbar = new Healthbar(this);
            //AddChild(_healthbar);
            //_healthbar.x -= this.width / 2;
            //_healthbar.y -= this.height * 0.66f;
           

            _sfxEngine = new Sound("assets\\sfx\\enginesound.wav", true);
			//_sfxEngine.Play();
            SetOrigin(width * 0.40f, height / 2);
            x = position.x;
            y = position.y;
            rotation = pRotation;

            _thrusterFlame = new AnimationSprite("assets/spaceship/flames.png", 4, 1);
            _thrusterFlame.SetOrigin(_thrusterFlame.width / 2, _thrusterFlame.height / 2);
            _thrusterFlame.rotation = 90;
            _thrusterFlame.alpha = 0;
            AddChild(_thrusterFlame);
            _thrusterFlame.x -= 125;
            _thrusterFlame.y = 0;

            AddChild(turret);
        }

        private void HandleFlames()
        {
            _thrusterTimer--;
            _thrusterFlame.alpha -= 0.03f;
            if (_thrusterFlame.alpha <= 0)
            {
                _thrusterFlame.alpha = 0;
            }

            if (_thrusterTimer <= 0)
            {
                //_thrusterFlame.NextFrame();
                _thrusterTimer = 10;
            }
        }

        private void HandleControls()
        {
			if (fuel != 0)
			{
				if (Input.GetKey(Key.W))
				{
					velocity.Add(Vec2.GetUnitVectorDegrees(rotation).Scale(ACCELERATION));
					fuel--;

                    if (_thrusterFlame.alpha <= 0.8f)
                    {
                        _thrusterFlame.alpha += 0.07f;
                    }
                }

				if (Input.GetKey(Key.D))
				{
					angular_velocity += ANGULAR_ACCELERATION;
				}

				if (Input.GetKey(Key.A))
				{
					angular_velocity -= ANGULAR_ACCELERATION;
				}
            }

            if (Input.GetKey(Key.ONE))
            {
                bulletType = BulletType.STANDARD;
            }

            if (Input.GetKey(Key.TWO))
            {
                bulletType = BulletType.CONTROLLED;
            }

            if (Input.GetKey(Key.THREE))
            {
                bulletType = BulletType.CLUSTER;
            }

            if (Input.GetKey(Key.FOUR))
            {
                bulletType = BulletType.RICOCHET;
            }
        }

        private void HandleFriction()
        {
            if (velocity.Length() > 0.01)
            {
                velocity.Scale(FRICTION);
            }
            else
            {
                velocity = Vec2.zero;
            }

            if (Mathf.Abs(angular_velocity) > 0.01)
            {
                angular_velocity *= FRICTION;
            }
            else
            {
                angular_velocity = 0;
            }
        }

        private void Move()
        {
            rotation += angular_velocity;
            position.Add(velocity);
            x = position.x;
            y = position.y;
        }

        public void Respawn(Vec2 pPostion)
        {
            position = (pPostion);
        }

        private void FireParticles()
        {
            Particle _particle = new Particle("assets/spaceship/flames.png", 4 , 1);
            game.AddChild(_particle);
            _particle.currentFrame = 0;
            _particle.SetXY(x, y);
            _particle.rotation = rotation + 90;
        }

        public void Update()
        {
            if (isActive)
            {
                HandleControls();
                turret.Move();
            }
            HandleFriction();
            Move();

			if (health <= 0)
			{
				this.Destroy();
			}

            HandleFlames();
        }
    }
}

