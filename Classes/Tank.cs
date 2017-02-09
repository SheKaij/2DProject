using System;
using System.Drawing;

namespace GXPEngine
{
    public class Tank : Sprite
    {
        //public but still readonly, can only be assigned once and cannot be overwritten after this
        public readonly int radius;

        private Vec2 _position;
        private Vec2 _velocity;
        private Vec2 _targetPos;
        private Vec2 _targetDelta;

        private float _travelSpeed;
        private float _rotationSpeed;

        private bool _hasControl = false;

        private Barrel _barrel;

        public Tank(int pRadius, Vec2 pPosition = null, Vec2 pVelocity = null, bool pHasControl = false, Color? pColor = null) : base("assets\\tanks\\bodies\\pz_kpfw_iv.png")
        {
            SetOrigin(width / 2, height / 2);

            radius = pRadius;
            position = pPosition;
            velocity = pVelocity;
            hasControl = pHasControl;
            

            _targetPos = new Vec2(Input.mouseX, Input.mouseY);

            x = position.x;
            y = position.y;

            _travelSpeed = 0;
            _rotationSpeed = 0;

            _barrel = new Barrel();
            AddChild(_barrel);
        }

        public bool hasControl
        {
            set
            {
                _hasControl = value;
            }
            get
            {
                return _hasControl;
            }
        }

        public float travelSpeed
        {
            set
            {
                _travelSpeed = value;
            }
            get
            {
                return _travelSpeed;
            }
        }

        public int barrelWidth
        {
            set
            {
                _barrel.width = value;
            }
            get
            {
                return _barrel.width;
            }
        }

        public Barrel barrel
        {
            set
            {
                _barrel = value;
            }
            get
            {
                return _barrel;
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

        public Vec2 targetPos
        {
            set
            {
                _targetPos = value ?? Vec2.zero;
            }
            get
            {
                return _targetPos;
            }
        }

        public Vec2 targetDelta
        {
            set
            {
                _targetDelta = value ?? Vec2.zero;
            }
            get
            {
                return _targetDelta;
            }
        }

        public void HandleMovement()
        {

            if (Input.GetKey(Key.W))
            {
                _travelSpeed += 0.5f;
                float radians = rotation * Mathf.PI / 180;
                _velocity.x = Mathf.Cos(radians) * _travelSpeed;
                _velocity.y = Mathf.Sin(radians) * _travelSpeed;
                RotationSpeed();
            }

            else
            {
                _velocity.x *= 0.95f;
                _velocity.y *= 0.9555f;
                _travelSpeed *= 0.955f;
            }

            if (Input.GetKey(Key.S))
            {
                _travelSpeed -= 0.5f;
                float radians = rotation * Mathf.PI / 180;
                _velocity.x = Mathf.Cos(radians) * _travelSpeed;
                _velocity.y = Mathf.Sin(radians) * _travelSpeed;
                RotationSpeed();
            }
            
            _velocity.x *= 0.9555f;
            _velocity.y *= 0.9555f;
            _travelSpeed *= 0.955f;
            Step();

            if (_travelSpeed < 0.3 && _travelSpeed > -0.3)
            {
                _travelSpeed = 0;
                _velocity.SetXY(0, 0);
            }

            //if (_travelSpeed == 0)
            //{
            //    RotationSpeed();
            //}
        }

        public void RotationSpeed()
        {
            if (Input.GetKey(Key.D))
            {
                _rotationSpeed += 0.66f;
            }

            if (Input.GetKey(Key.A))
            {
                _rotationSpeed -= 0.66f;
            }

            rotation = _rotationSpeed;
            //_rotationSpeed *= 0.99f;
        }

        public void Respawn()
        {
            position.SetXY(game.width / 2, game.height / 2);
            rotation = 0;
            _travelSpeed = 0;
            _rotationSpeed = 0;
        }

        public void Step()
        {
            _position.Add(_velocity);

            x = _position.x;
            y = _position.y;
        }


        public void Update()
        {
            if (hasControl == true)
            {
                RotationSpeed();
                HandleMovement();
                _barrel.BarrelRotation();
            }
        }
    }
}

