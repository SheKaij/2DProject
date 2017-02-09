using System;
using GXPEngine;
using System.Drawing;

public class MyGame : Game
{

    static void Main()
    {
        new MyGame().Start();
    }

    private Tank _currentTank = null;
    private Tank _tank1 = null;
    private Tank _tank2 = null;

    private Background _background1 = null;
    private Background _background2 = null;
    private Background _background3 = null;
    private Background _background4 = null;

    private Bullet _bullet = null;
    private Target _target = null;
    private TextField tf = null;
    private UnitTest _unitTest = null;

    private string _currentPlayer;
    private int _totalScore = 0;
    private const float BULLETSPEED = 24;
    private float _gravForce;
    private const float _gravForceConstant = 6.67408f * 10f - 11f;

    public MyGame() : base(1920, 600, false)
    {
        _unitTest = new UnitTest();
        
        // REAL CHEAP BACKGROUND TRICK, SHOULD PROBABLY USE A FOR LOOP
        _background1 = new Background();
        AddChild(_background1);

        _background2 = new Background();
        AddChild(_background2);
        _background2.x = game.width / 2;

        _background3 = new Background();
        AddChild(_background3);
        _background3.y = game.height / 2;

        _background4 = new Background();
        AddChild(_background4);
        _background4.x = game.width / 2;
        _background4.y = game.height / 2;

        _tank1 = new Tank(30, new Vec2(width * 0.25f, height * 0.33f));
        AddChild(_tank1);
        _currentTank = _tank1;      // I want player 1 to start the game
        _tank1.hasControl = true;
        
        _tank2 = new Tank(30, new Vec2(width * 0.25f, height * 0.66f));
        AddChild(_tank2);
        //_tank2.rotation = 180;
        //_tank2.hasControl = false;

        _target = new Target(30, new Vec2(width * 0.5f, height / 2));
        AddChild(_target);

        tf = TextField.CreateTextField("Control: Player 0" + "\n"
                                     + "Speed: 000 mph" + "\n" 
                                     + "Score: 00");
        tf.backgroundColor = Color.White;
        AddChild(tf);
    }

    public void HandleBoundaries()
    {
        if (_currentTank.position.x + _currentTank.width / 2 < 0)       // if tank goes left, continue to the right
        {
            _currentTank.position.x = width + _currentTank.width / 2;
        }
        else if (_currentTank.position.x - _currentTank.width / 2 > width)
        {
            _currentTank.position.x = 0 - _currentTank.width / 2;
        }


        if (_currentTank.position.y + _currentTank.height < 0)
        {
            _currentTank.position.y = height + _currentTank.height;
        }
        else if (_currentTank.position.y - _currentTank.height > height)
        {
            _currentTank.position.y = 0 - _currentTank.height;
        }
    }

    public void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // I WOULD LIKE TO DO THIS ALL WITH A 'CURRENT-TANK' INSTANCE
            _bullet = new Bullet(20, new Vec2(_currentTank.position.x, _currentTank.position.y));
            AddChild(_bullet);

            //Rotate the bullet the way the barrel was rotated towards
            _bullet.rotation = _currentTank.barrel.rotation + _currentTank.rotation;
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

    public void ScoreDetection()
    {
       OnDestroy();
    }

    private void HandleHUD()
    {
        if (_currentTank == _tank1) { _currentPlayer = "1"; }
        else if (_currentTank == _tank2) { _currentPlayer = "2"; }

        tf.text = "Control: Player " + _currentPlayer + "\n"
                + "Speed: " + Math.Abs(Math.Round(_currentTank.travelSpeed)) + "  Mph" + "\n"
                + "Score: " + _totalScore;
    }

    private void OrbitGravity()
    {
        if (_bullet != null)
        {
            if (_bullet.x > _target.x - 350 && _bullet.x < _target.x + 350 && _bullet.y > _target.y - 100 && _bullet.y < _target.y + 100)
            {
                _bullet.position = _bullet.position.RotateAroundDegrees(_target.position.x, _target.position.y, 0.05f);

                //This would be the Newton formula for calculating the gravitational force
                

                // Calculate the distance between the bullet and the center of the target
                Vec2 _distance = new Vec2();
                _distance.x = _target.position.x - _bullet.position.x;
                _distance.y = _target.position.y - _bullet.position.y;
                _bullet.rotation = _distance.GetAngleDegrees();

                // This would be gravity
                //_tank.velocity.x += 1;

                //DONE: SET THE CENTER ORBIT POINT AT A TARGET
                //DONE: LET BULLETS ROTATE AROUND A TARGET
                //DONE: DESTROY A BULLET 

                //NEW PROBLEMS: BULLETS DON'T HONE TOWARDS TARGET
                //NEW PROBLEMS: BULLETS SHOULD BE MADE CANNONS, A ROUND OBJECT, SO THAT THE ROTATION DOESN'T LOOK OFF
            }
        }
    }

    void Update()
    {
        HandleAttack();
        HandleBoundaries();
        HandleHUD();

        ScoreDetection();
        //OrbitGravity();
        
        if (Input.GetKeyDown(Key.R))
        {
           _tank1.Respawn();
        }

        if (Input.GetKey(Key.TAB))
        {
            if (_currentTank.travelSpeed != 0)
            {
                _currentTank.position = _currentTank.position.RotateAroundDegrees(_target.position.x, _target.position.y, 0.05f);

                // THIS IS THE ORBIT
                Vec2 _distance = new Vec2();
                _distance.x = _target.position.x - _currentTank.position.x;
                _distance.y = _target.position.y - _currentTank.position.y;
                _currentTank.rotation = _currentTank.velocity.GetAngleDegrees();
            }
        }

        Console.WriteLine(_currentTank.velocity);
        // CURRENT PLAYER CONTROL SWITCHING PROTOTYPE
        if (Input.GetKeyDown(Key.ONE))
        {
            _currentTank.travelSpeed = 0;
            _currentTank = _tank1;
            _tank1.hasControl = true;
            _tank2.hasControl = false;
        }

        if (Input.GetKeyDown(Key.TWO))
        {
            _currentTank.travelSpeed = 0;
            _currentTank = _tank2;
            _tank1.hasControl = false;
            _tank2.hasControl = true;
        }
        //_tank.position = _tank.position.RotateAroundDegrees(width / 2, height / 2, 0.01f);
        //NOTE: Ask Martins to check sourcetree for the previous project, 'Flexman', as the code for the score system/health bars might be needed
    }
}
