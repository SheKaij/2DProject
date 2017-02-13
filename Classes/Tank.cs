using System;
using System.Collections.Generic;
using System.Drawing;
using static Bullet;

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
        public BulletType bulletType { get; set; }
        public int bulletCount { get; set; }

        public int score { get; set; }


        private Sound _sfxEngine;


        public Tank(Vec2 pPosition, int pRotation, bool pIsActive) : base("assets\\spaceship\\ship.png")
        {
            turret = new Turret();
            bullets = new List<Bullet>();
            position = pPosition;
            velocity = Vec2.zero;
            angular_velocity = 0;
            isActive = pIsActive;
            bulletType = BulletType.STANDARD;
            bulletCount = MAX_BULLET;

            _sfxEngine = new Sound("assets\\sfx\\placeholder_engine1.wav", false, false);

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

            //if (Input.GetMouseButtonDown(0) && _bullet == null)
            //{
            //    Bullet bullet = BulletFactory.Create(BulletType.STANDARD, new Vec2(position), new Vec2(Input.mouseX - x, Input.mouseY - y)));
            //    AddChild(_bullet);
            //    _currentTank.bulletCount -= 1;
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

        private void Move()
        {
            rotation += angular_velocity;
            position.Add(velocity);
            x = position.x;
            y = position.y;
        }

        public void Shoot()
        {

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
                //FireParticles();
            }
            HandleFriction();
            Move();
        }
        ////public but still readonly, can only be assigned once and cannot be overwritten after this
        //public readonly int radius;

        //private Sound _sfxEngine;

        //private Vec2 _position;
        //private Vec2 _velocity;
        //private Vec2 _acceleration;
        //private Vec2 _targetPos;
        //private Vec2 _targetDelta;

        //private int _shotsLeft = 5;

        //private float _flightAcceleration = 0.1f;
        //private const float FLIGHTFRICTION = 0.98f;

        //public float _rotationalAcceleration;
        //public float _rotationalVelocity;

        //private const float ROTATIONACCELERATION = 0.001f;
        //private const float ROTATIONFRICTION = 0.95f;

        //private bool _hasControl = false;
        //private int _score;

        //private Turret _barrel;

        //public Tank(int pRadius, Vec2 pPosition = null, Vec2 pVelocity = null, bool pHasControl = false, Color? pColor = null) : base("assets\\spaceship\\ship.png")
        //{
        //    SetOrigin(width * 0.40f, height / 2);

        //    radius = pRadius;
        //    position = pPosition;
        //    velocity = pVelocity;
        //    hasControl = pHasControl;

        //    _acceleration = Vec2.zero;

        //    _sfxEngine = new Sound("assets\\sfx\\placeholder_engine1.wav", false, false);

        //    _targetPos = new Vec2(Input.mouseX, Input.mouseY);

        //    x = position.x;
        //    y = position.y;

        //    _barrel = new Turret();
        //    AddChild(_barrel);
        //}

        //public int shotsLeft
        //{
        //    get
        //    {
        //        return _shotsLeft;
        //    }
        //    set
        //    {
        //        _shotsLeft = value;
        //    }
        //}

        //public int score
        //{
        //    get
        //    {
        //        return _score;
        //    }
        //    set
        //    {
        //        _score = value;
        //    }
        //}

        //public bool hasControl
        //{
        //    set
        //    {
        //        _hasControl = value;
        //    }
        //    get
        //    {
        //        return _hasControl;
        //    }
        //}

        //public float travelSpeed
        //{
        //    set
        //    {
        //        _flightAcceleration = value;
        //    }
        //    get
        //    {
        //        return _flightAcceleration;
        //    }
        //}

        //public int barrelWidth
        //{
        //    set
        //    {
        //        _barrel.width = value;
        //    }
        //    get
        //    {
        //        return _barrel.width;
        //    }
        //}

        //public Turret barrel
        //{
        //    set
        //    {
        //        _barrel = value;
        //    }
        //    get
        //    {
        //        return _barrel;
        //    }
        //}

        //public Vec2 position
        //{
        //    set
        //    {
        //        _position = value ?? Vec2.zero;
        //    }
        //    get
        //    {
        //        return _position;
        //    }
        //}

        //public Vec2 velocity
        //{
        //    set
        //    {
        //        _velocity = value ?? Vec2.zero;
        //    }
        //    get
        //    {
        //        return _velocity;
        //    }
        //}

        //public Vec2 targetPos
        //{
        //    set
        //    {
        //        _targetPos = value ?? Vec2.zero;
        //    }
        //    get
        //    {
        //        return _targetPos;
        //    }
        //}

        //public Vec2 targetDelta
        //{
        //    set
        //    {
        //        _targetDelta = value ?? Vec2.zero;
        //    }
        //    get
        //    {
        //        return _targetDelta;
        //    }
        //}

        //public void HandleMovement()
        //{

        //    if (Input.GetKey(Key.W))
        //    {
        //        float radians = rotation * Mathf.PI / 180;
        //        _acceleration.x = Mathf.Cos(radians) * _flightAcceleration;
        //        _acceleration.y = Mathf.Sin(radians) * _flightAcceleration;
        //        RotationSpeed();
        //    }

        //    else
        //    {
        //        _acceleration.SetXY(0, 0);
        //    }

        //    if (_acceleration.x < 1 && _acceleration.x < -1 && _acceleration.y < 1 && _acceleration.y < -1)
        //    {
        //        _velocity.SetXY(0, 0);
        //    }
        //    _velocity.Scale(FLIGHTFRICTION);
        //    Step();
        //}

        //public void RotationSpeed()
        //{
        //    if (Input.GetKey(Key.D))
        //    {
        //        _rotationalAcceleration += ROTATIONACCELERATION;
        //    }

        //    else if (Input.GetKey(Key.A))
        //    {
        //        _rotationalAcceleration -= ROTATIONACCELERATION;
        //    }

        //    else
        //    {
        //        _rotationalAcceleration = 0;
        //    }

        //    if (Input.GetKeyUp(Key.D) || Input.GetKeyUp(Key.A))
        //    {
        //        _rotationalAcceleration = 0;
        //    }

        //    _rotationalVelocity *= ROTATIONFRICTION;
        //    _rotationalVelocity += _rotationalAcceleration;

        //    if (_rotationalVelocity > 1 && _flightAcceleration != 0)
        //    {
        //        _rotationalVelocity = 1;
        //    }

        //    else if (_rotationalVelocity > 2)
        //    {
        //        _rotationalVelocity = 2;
        //    }


        //    if (_rotationalVelocity < -1 && _flightAcceleration != 0)
        //    {
        //        _rotationalVelocity = -1;
        //    }

        //    else if (_rotationalVelocity < -2)
        //    {
        //        _rotationalVelocity = -2;

        //    }

        //    rotation += _rotationalVelocity;
        //}

        //public void Respawn()
        //{
        //    position.SetXY(game.width / 2, game.height / 2);
        //    rotation = 0;
        //    _flightAcceleration = 0;
        //}

        //public void Step()
        //{
        //    _velocity.Add(_acceleration);
        //    _position.Add(_velocity);

        //    x = _position.x;
        //    y = _position.y;
        //}


        //public void Update()
        //{
        //    if (hasControl == true)
        //    {
        //        RotationSpeed();
        //        HandleMovement();
        //        _barrel.TurretRotation();
        //    }
        //}
    }
}

