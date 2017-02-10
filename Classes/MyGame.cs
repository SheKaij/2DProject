using System;
using GXPEngine;
using System.Drawing;

public class MyGame : Game
{

    static void Main()
    {
        new MyGame().Start();
    }

    private Sound _sfxShooting;
    private Sound _bgMusicSound;
    private SoundChannel _playMusic;

    private Tank _currentTank = null;
    private Tank _tank1 = null;
    private Tank _tank2 = null;

    private Background _background1 = null;

    private Bullet _bullet;
    private Target _target = null;
    private TextField tf = null;
    private UnitTest _unitTest = null;

    private string _currentPlayer;
    private int _totalScore = 0;
    private const float BULLETSPEED = 8;
    private float _gravForce;
    private const float _gravForceConstant = 6.67408f * 10 - 11;

    public MyGame() : base(1920, 600, false)
    {
        _unitTest = new UnitTest();

        _sfxShooting = new Sound("assets\\sfx\\placeholder_shoot2.wav");
        _bgMusicSound = new Sound("assets\\sfx\\placeholder_music2.mp3", true, true);
        _playMusic = _bgMusicSound.Play();
        
        _background1 = new Background();
        AddChild(_background1);

        _tank1 = new Tank(30, new Vec2(width * 0.25f, height * 0.33f));
        AddChild(_tank1);
        _currentTank = _tank1;      // I want player 1 to start the game
        _tank1.hasControl = true;

        _tank2 = new Tank(30, new Vec2(width * 0.25f, height * 0.66f));
        AddChild(_tank2);
        _tank2.hasControl = false;

        _target = new Target(30, new Vec2(width * 0.5f, height / 2));
        AddChild(_target);

        tf = TextField.CreateTextField("Control: Player 0" + "\n"
                                     + "Speed: 00 mph" + "\n"
                                     + "Score: 0000" + "\n"
                                     + "Shots left: 0");
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
            _sfxShooting.Play();
            _currentTank.shotsLeft -= 1;

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

    private void CheckHitCollision()
    {
        if (_bullet != null)
        {
            foreach (GameObject item in _bullet.GetCollisions())
            {
                if (item is Target)
                {
                    _bullet.sfxDestroyed.Play();
                    _currentTank.score += 10;
                    _bullet.Destroy();
                }

                if (item is Tank && item != _currentTank)
                {
                    _bullet.sfxDestroyed.Play();
                    _currentTank.score += 50;
                    _bullet.Destroy();

                }
            }
        }
    }

    public void ScoreIncrease()
    {
        _currentTank.score += 100;
    }

    private void HandleHUD()
    {
        if (_currentTank == _tank1) { _currentPlayer = "1"; }
        else if (_currentTank == _tank2) { _currentPlayer = "2"; }

        tf.text = "Control: Player " + _currentPlayer + "\n"
                + "Speed: " + Math.Abs(Math.Round(_currentTank.velocity.x)) + "  Mph" + "\n"
                + "Score: " + _currentTank.score + "\n"
                + "Shots left: " + _currentTank.shotsLeft;
    }

    private void TurnCheck()
    {
        if (_currentTank == _tank2 && _currentTank.shotsLeft <= 0)
        {
            _currentTank.velocity.SetXY(0, 0);
            _currentTank = _tank1;
            _tank2.shotsLeft = 5;
            _tank1.hasControl = true;
            _tank2.hasControl = false;
        }

        if (_currentTank == _tank1 && _currentTank.shotsLeft <= 0)
        {
            _currentTank.velocity.SetXY(0, 0);
            _currentTank = _tank2;
            _tank1.shotsLeft = 5;
            _tank1.hasControl = false;
            _tank2.hasControl = true;
        }
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
                //_bullet.rotation = _distance.GetAngleDegrees();

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

    private void Update()
    {
        HandleAttack();
        HandleBoundaries();
        HandleHUD();
        CheckHitCollision();
        TurnCheck();
        //OrbitGravity();

        Console.WriteLine(_currentTank.score);
        
        if (Input.GetKeyDown(Key.R))
        {
           _tank1.Respawn();
        }

        // CURRENT PLAYER CONTROL SWITCHING PROTOTYPE
        if (Input.GetKeyDown(Key.ONE))
        {
            _currentTank.velocity.SetXY(0, 0);
            _currentTank = _tank1;
            _tank1.hasControl = true;
            _tank2.hasControl = false;
        }

        if (Input.GetKeyDown(Key.TWO))
        {
            _currentTank.velocity.SetXY(0, 0);
            _currentTank = _tank2;
            _tank1.hasControl = false;
            _tank2.hasControl = true;
        }
        //_tank.position = _tank.position.RotateAroundDegrees(width / 2, height / 2, 0.01f);
        //NOTE: Ask Martins to check sourcetree for the previous project, 'Flexman', as the code for the score system/health bars might be needed
    }
}
