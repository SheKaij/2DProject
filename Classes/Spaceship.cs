using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment_2.Classes
{
    public class Spaceship : Sprite
    {
        private readonly float AVARAGE_ACCELRATION = 0.5f;
        private readonly float KINETIC_FRICTION_COEFFICIENT = 0.9f;
        private readonly float STATIC_FRICTION_COEFFICIENT = 0.2f;
        private readonly float ROTATION_SPEED = 0.5f;

        public Vec2 Position { get; set; }
        public Vec2 Velocity { get; set; }
        public Vec2 Acceleration { get; set; }
        public Vec2 Target { get; set; }
        public float Direction { get; set; }
        public bool HasControl { get; set; }

        public Spaceship(string image, Vec2 position, float direction, bool hasControl) : base(image)
        {
            Position = position;
            Velocity = new Vec2();
            Acceleration = new Vec2();
            Target = new Vec2(Input.mouseX, Input.mouseY);
            Direction = direction;
            HasControl = hasControl;
            
            SetOrigin(width / 2, height / 2);
            x = Position.x;
            y = Position.y;
            rotation = Direction;
        }

        public void HandleControls()
        {
            if (Input.GetKey(Key.W))
            {
                Acceleration = Vec2.GetUnitVectorDegrees(Direction);
            } else
            {
                Acceleration.SetXY(0, 0);
            }

            if (Input.GetKey(Key.S))
            {
                Acceleration = Vec2.GetUnitVectorDegrees((Direction + 180) % 360);
            }
            else
            {
                Acceleration.SetXY(0, 0);
            }

            if (Input.GetKey(Key.D))
            {
                Direction += ROTATION_SPEED;
            }

            if (Input.GetKey(Key.A))
            {
                Direction -= ROTATION_SPEED;
            }
        }

        public void HandleFriction()
        {
            //if (Speed != 0)
            //{
            //    if (Math.Abs(Speed) < STOPPED_AT)
            //    {
            //        Speed = 0;
            //    }
            //    else
            //    {
            //        Speed *= FRICTION;
            //    }
            //}
        }

        public void Rotate()
        {
            rotation = Direction;
        }

        public void Step()
        {
            Position.Add(Velocity);

            x = Position.x;
            y = Position.y;
        }

        public void Update()
        {
            if (HasControl == true)
            {
                HandleControls();
            }
            HandleFriction();
            Rotate();
            Step();
        }
    }
}
