using System;
using GXPEngine;
using System.Drawing;
using System.Collections;

public class MyGame : Game
{

    static void Main()
    {
        new MyGame().Start();
    }

    private const float BULLETSPEED = 0.05f;
    private readonly float GRAVITATIONAL_FORCE = 30000f;
    
    private Sound _bgMusicSound;
    private SoundChannel _playMusic;

    private Background _background1 = null;

    private Tank _currentTank;
    private Tank _tank1;
    private Tank _tank2;
    
    private Bullet _bullet;
    private Planet _planet = null;
    private TextField tf = null;
    private UnitTest _unitTest = null;

    private string _currentPlayer;
    private int _totalScore = 0;
    

    public MyGame() : base(1920, 600, false)
    {
        _unitTest = new UnitTest();
        _bgMusicSound = new Sound("assets\\sfx\\placeholder_music2.mp3", true, true);
        _playMusic = _bgMusicSound.Play();
        
        _background1 = new Background();
        AddChild(_background1);

        _tank1 = new Tank(new Vec2(width * 0.25f, height * 0.33f), true);
        AddChild(_tank1);
        _currentTank = _tank1;      // I want player 1 to start the game

        _tank2 = new Tank(new Vec2(width * 0.25f, height * 0.66f), false);
        AddChild(_tank2);

        _planet = new Planet(30, new Vec2(width * 0.5f, height / 2));
        AddChild(_planet);

        tf = TextField.CreateTextField("Control: Player 0" + "\n"
                                     + "Speed: 00 mph" + "\n"
                                     + "Score: 0000" + "\n"
                                     + "Shots left: 0");
        tf.backgroundColor = Color.White;
        AddChild(tf);
    }

    public void HandleBoundaries()
    {
        if (_currentTank.position.x < 0)  
        {
            _currentTank.position.x = 0;
            _currentTank.velocity.x = 0;
        }
        else if (_currentTank.position.x > width)
        {
            _currentTank.position.x = width;
            _currentTank.velocity.x = 0;
        }


        if (_currentTank.position.y < 0)
        {
            _currentTank.position.y = 0;
            _currentTank.velocity.y = 0;
        }
        else if (_currentTank.position.y > height)
        {
            _currentTank.position.y = height;
            _currentTank.velocity.y = 0;
        }
    }

    public void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0) && _bullet == null)
        {
            _bullet = new Bullet(new Vec2(_currentTank.position.x, _currentTank.position.y));
            AddChild(_bullet);
            _currentTank.bulletCount -= 1;

            _bullet.velocity = new Vec2(Input.mouseX - _currentTank.x, Input.mouseY - _currentTank.y);
            _bullet.velocity.Scale(BULLETSPEED);
            _bullet.rotation = _bullet.velocity.GetAngleDegrees();

        }

        if (_bullet != null)
        {
            if (_bullet.x > game.width || _bullet.x < 0 || _bullet.y > game.height || _bullet.y < 0)
            {
                _bullet.Destroy();
                _bullet = null;
            }
        }
    }

    private void CheckHitCollision()
    {
        if (_bullet != null)
        {
            foreach (GameObject item in _bullet.GetCollisions())
            {
                if (item is Planet)
                {
                    _currentTank.score += 10;
                    _bullet.Destroy();
                    _bullet = null;
                }

                if (item is Tank && item != _currentTank)
                {
                    _currentTank.score += 50;
                    _bullet.Destroy();
                    _bullet = null;

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
                + "Shots left: " + _currentTank.bulletCount;
    }

    private void TurnCheck()
    {
        if (_currentTank == _tank2 && _currentTank.bulletCount <= 0)
        {
            _currentTank = _tank1;
            _tank2.bulletCount = 5;
            _tank1.isActive = true;
            _tank2.isActive = false;
        }

        if (_currentTank == _tank1 && _currentTank.bulletCount <= 0)
        {
            _currentTank = _tank2;
            _tank1.bulletCount = 5;
            _tank1.isActive = false;
            _tank2.isActive = true;
        }
    }

    private void CheckControls()
    {
        if (Input.GetKeyDown(Key.R))
        {
            _tank1.Respawn(Vec2.zero);
        }

        // CURRENT PLAYER CONTROL SWITCHING PROTOTYPE
        if (Input.GetKeyDown(Key.ONE))
        {
            _currentTank = _tank1;
            _tank1.bulletCount = 5;
            _tank1.isActive = true;
            _tank2.isActive = false;
        }

        if (Input.GetKeyDown(Key.TWO))
        {
            _currentTank = _tank2;
            _tank2.bulletCount = 5;
            _tank1.isActive = false;
            _tank2.isActive = true;
        }
    }

    private void HandleGravity()
    {
        if (_bullet != null)
        {
            Vec2 gravityVector = new Vec2(_planet.x - _bullet.x, _planet.y - _bullet.y);
            gravityVector.Normalize();
            gravityVector.Scale(GRAVITATIONAL_FORCE / Mathf.Pow(_bullet.DistanceTo(_planet), 2));
            _bullet.velocity.Add(gravityVector);
        }
    }
    
    private void Update()
    {
        HandleAttack();
        HandleBoundaries();
        HandleHUD();
        CheckHitCollision();
        TurnCheck();
        HandleGravity();
        CheckControls();

        Console.WriteLine(_currentTank.score);
        
        
        //_tank.position = _tank.position.RotateAroundDegrees(width / 2, height / 2, 0.01f);
        //NOTE: Ask Martins to check sourcetree for the previous project, 'Flexman', as the code for the score system/health bars might be needed
    }

    
}
