using System;
using GXPEngine;
using System.Drawing;

public class MyGame : Game
{

    static void Main()
    {
        new MyGame().Start();
    }

    private Tank _tank = null;
    private Target _target = null;
    private Bullet _bullet = null;
    private Background _background = null;
    private TextField tf = null;
    private UnitTest _unitTest = null;

    private int _totalScore = 0;
    private const float BULLETSPEED = 24;

    public MyGame() : base(800, 600, false)
    {
        _unitTest = new UnitTest();

        //SetScaleXY(0.5f);

        _background = new Background();
        AddChild(_background);

        _tank = new Tank(30, new Vec2(width / 2, height / 2));
        AddChild(_tank);

        _target = new Target(30, new Vec2(width / 2, height - 30));
        AddChild(_target);

        tf = TextField.CreateTextField("Speed: 000 mph" + "\n" 
                                     + "Score = 00");
        tf.backgroundColor = Color.White;
        AddChild(tf);
    }

    public void HandleBoundaries()
    {
        if (_tank.position.x + _tank.width / 2 < 0)       // if tank goes left, continue to the right
        {
            _tank.position.x = width + _tank.width / 2;
        }
        else if (_tank.position.x - _tank.width / 2 > width)
        {
            _tank.position.x = 0 - _tank.width / 2;
        }


        if (_tank.position.y + _tank.height < 0)
        {
            _tank.position.y = height + _tank.height;
        }
        else if (_tank.position.y - _tank.height > height)
        {
            _tank.position.y = 0 - _tank.height;
        }
    }

    public void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Console.WriteLine("Shot the bullet!");
            _bullet = new Bullet(new Vec2(_tank.position.x, _tank.position.y));
            AddChild(_bullet);

            //Rotate the bullet the way the barrel is rotated towards
            _bullet.rotation = _tank.barrel.rotation + _tank.rotation;
        }

        if (_bullet != null)
        {
            float angleInRadians = _bullet.rotation * Mathf.PI / 180;

            _bullet.velocity.x = BULLETSPEED * Mathf.Cos(angleInRadians);
            _bullet.velocity.y = BULLETSPEED * Mathf.Sin(angleInRadians);

            if (_bullet.x > game.width || _bullet.x < 0 || _bullet.y > game.height || _bullet.y < 0)
            {
                _bullet.Destroy();
            }
        }
    }

    void Update()
    {
        HandleBoundaries();
        HandleAttack();

        tf.text = "Speed: " + Math.Abs(Math.Round(_tank.travelSpeed)) + "  Mph" + "\n"
                + "Score: " + _totalScore;

        if (Input.GetKeyDown(Key.R))
        {
           _tank.Respawn();
        }

        _tank.position = _tank.position.RotateAroundDegrees(width / 2, height / 2, 0.01f);
    }
}
