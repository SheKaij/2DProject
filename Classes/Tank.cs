using System;
using System.Collections.Generic;
using System.Drawing;

namespace GXPEngine
{
    public class Tank : Sprite
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
        public int bulletCount { get; set; }

        public int score { get; set; }


        private Sound _sfxEngine;
        

        public Tank(Vec2 pPosition = null, bool pIsActive = false, Color? pColor = null) : base("assets\\spaceship\\ship.png")
        {
            turret = new Turret();
            bullets = new List<Bullet>();
            position = pPosition;
            velocity = Vec2.zero;
            angular_velocity = 0;
            isActive = pIsActive;
            bulletCount = MAX_BULLET;

            _sfxEngine = new Sound("assets\\sfx\\placeholder_engine1.wav", false, false);

            SetOrigin(width * 0.40f, height / 2);
            x = position.x;
            y = position.y;
            
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

            //if (Input.GetMouseButtonDown(0))
            //{
            //    Bullet bullet = new Bullet(new Vec2(position.x, position.y));
            //    bullet.velocity = new Vec2(Input.mouseX - x, Input.mouseY - y).Scale(bullet.speed);
            //    bullet.rotation = bullet.velocity.GetAngleDegrees();

            //    bullets.Add(bullet);
            //    bulletCount -= 1;

            //    AddChild(bullet);
            //}
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

        public void Respawn(Vec2 pPostion)
        {
            position = (pPostion);
        }

        public void Step()
        {
            rotation += angular_velocity;
            position.Add(velocity);
            x = position.x;
            y = position.y;
        }


        public void Update()
        {
            if (isActive)
            {
                HandleControls();
                turret.Move();
            }
            HandleFriction();
            Step();
        }
    }
}

