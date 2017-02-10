using System;
using System.Drawing;

namespace GXPEngine
{
    public class Tank : Sprite
    {
        //public but still readonly, can only be assigned once and cannot be overwritten after this
        public readonly int radius;

        private Sound _sfxEngine;

        private Vec2 _position;
        private Vec2 _velocity;
        private Vec2 _acceleration;
        private Vec2 _targetPos;
        private Vec2 _targetDelta;

        private float _flightAcceleration = 0.1f;
        private const float FLIGHTFRICTION = 0.98f;

        public float _rotationalAcceleration;
        public float _rotationalVelocity;

        private const float ROTATIONACCELERATION = 0.001f;
        private const float ROTATIONFRICTION = 0.95f;

        private bool _hasControl = false;

        private Turret _barrel;

        public Tank(int pRadius, Vec2 pPosition = null, Vec2 pVelocity = null, bool pHasControl = false, Color? pColor = null) : base("assets\\spaceship\\ship.png")
        {
            SetOrigin(width * 0.40f, height / 2);

            radius = pRadius;
            position = pPosition;
            velocity = pVelocity;
            hasControl = pHasControl;
            
            _acceleration = Vec2.zero;

            _sfxEngine = new Sound("assets\\sfx\\placeholder_engine1.wav", false, false);

            _targetPos = new Vec2(Input.mouseX, Input.mouseY);

            x = position.x;
            y = position.y;

            _barrel = new Turret();
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
                _flightAcceleration = value;
            }
            get
            {
                return _flightAcceleration;
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

        public Turret barrel
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
                float radians = rotation * Mathf.PI / 180;
                _acceleration.x = Mathf.Cos(radians) * _flightAcceleration;
                _acceleration.y = Mathf.Sin(radians) * _flightAcceleration;
                RotationSpeed();
            }

            else
            {
                _acceleration.SetXY(0, 0);
            }

            if (_acceleration.x < 1 && _acceleration.x < -1 && _acceleration.y < 1 && _acceleration.y < -1)
            {
                _velocity.SetXY(0, 0);
            }
            _velocity.Scale(FLIGHTFRICTION);
            Step();
        }

        public void RotationSpeed()
        {
            if (Input.GetKey(Key.D))
            {
                _rotationalAcceleration += ROTATIONACCELERATION;
            }

            else if (Input.GetKey(Key.A))
            {
                _rotationalAcceleration -= ROTATIONACCELERATION;
            }

            else
            {
                _rotationalAcceleration = 0;
            }

            if (Input.GetKeyUp(Key.D) || Input.GetKeyUp(Key.A))
            {
                _rotationalAcceleration = 0;
            }
            
            _rotationalVelocity *= ROTATIONFRICTION;
            _rotationalVelocity += _rotationalAcceleration;

            if (_rotationalVelocity > 1 && _flightAcceleration != 0)
            {
                _rotationalVelocity = 1;
            }

            else if (_rotationalVelocity > 2)
            {
                _rotationalVelocity = 2;
            }


            if (_rotationalVelocity < -1 && _flightAcceleration != 0)
            {
                _rotationalVelocity = -1;
            }

            else if (_rotationalVelocity < -2)
            {
                _rotationalVelocity = -2;
                
            }

            rotation += _rotationalVelocity;
        }

        public void Respawn()
        {
            position.SetXY(game.width / 2, game.height / 2);
            rotation = 0;
            _flightAcceleration = 0;
        }

        public void Step()
        {
            _velocity.Add(_acceleration);
            _position.Add(_velocity);

            x = _position.x;
            y = _position.y;
        }


        public void Update()
        {
            if (hasControl == true)
            {
                Console.WriteLine(_acceleration);
                RotationSpeed();
                HandleMovement();
                _barrel.TurretRotation();
            }
        }
    }
}

