using System;
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
        private readonly int MAX_BULLET = 5;

        public Turret turret { get; set; }
        public List<Bullet> bullets { get; set; }

        public Vec2 position { get; set; }
        public Vec2 velocity { get; set; }
        public float angular_velocity { get; set; }
        public bool isActive { get; set; }
        public BulletType bulletType { get; set; }
        public int bulletCount { get; set; }
		public float health { get; set; }
        public int score { get; set; }


        private Sound _sfxEngine;

		public Spaceship(string pFilename, Vec2 pPosition, int pRotation, bool pIsActive, float health) : base(pFilename, 2, 1)
        {
            turret = new Turret();
            bullets = new List<Bullet>();
            position = pPosition;
            velocity = Vec2.zero;
            angular_velocity = 0;
            isActive = pIsActive;
            bulletType = BulletType.STANDARD;
            bulletCount = MAX_BULLET;
			this.health = health;

            _sfxEngine = new Sound("assets\\sfx\\enginesound.wav", true);
			//_sfxEngine.Play();
            SetOrigin(width * 0.40f, height / 2);
            x = position.x;
            y = position.y;
            rotation = pRotation;

            AddChild(turret);
        }

        private void HandleControls()
        {

            if (Input.GetKey(Key.W))
            {
                velocity.Add(Vec2.GetUnitVectorDegrees(rotation).Scale(ACCELERATION));
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
        }
    }
}

