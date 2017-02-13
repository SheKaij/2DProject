using System;
using System.Drawing;
using GXPEngine;
using assignment_2.Classes;
using System.Collections.Generic;

public class Level : GameObject
{
    private static Level _instance;
    public static Level Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Level();
            }
            return _instance;
        }
    }
    private readonly float GRAVITATIONAL_FORCE = 30000f;
    private const float BULLETSPEED = 0.05f;

    private Background _background1;
    private Sound _bgMusicSound;
    private SoundChannel _playMusic;

    private Tank _currentTank;
    private Tank _tank1;
    private Tank _tank2;
    private List<Planet> _planets;
    private List<Bullet> _bullets;

    private Bullet _bullet = null;
    //private PlanetManager _pm = null;
    //private Planet _planet = null;
    private TextField tf = null;

    private float _distanceX;
    private float _distanceY;
    private float _distanceTotal;

    private string _currentPlayer;

    private const float _gravForceConstant = 6.67408f * 10 - 11;

    private Level() : base()
    {
        _bgMusicSound = new Sound("assets\\sfx\\placeholder_music2.mp3", true, true);
        //_playMusic = _bgMusicSound.Play();

        _background1 = new Background();
        AddChild(_background1);

        _tank1 = new Tank(new Vec2(game.width * 0.1f, game.height * 0.4f), 0, true);
        AddChild(_tank1);
        _currentTank = _tank1;

        _tank2 = new Tank(new Vec2(game.width * 0.9f, game.height * 0.6f), 180, false);
        AddChild(_tank2);

        _planets = new List<Planet>();
        _bullets = new List<Bullet>();

        Planet planet = new Planet(new Vec2(game.width * 0.3f, game.height * 0.2f), 10);
        _planets.Add(planet);
        AddChild(planet);

        planet = new Planet(new Vec2(game.width * 0.2f, game.height * 0.8f), 10);
        _planets.Add(planet);
        AddChild(planet);

        planet = new Planet(new Vec2(game.width * 0.5f, game.height * 0.6f), 10);
        _planets.Add(planet);
        AddChild(planet);

        planet = new Planet(new Vec2(game.width * 0.7f, game.height * 0.4f), 10);
        _planets.Add(planet);
        AddChild(planet);

        tf = TextField.CreateTextField("Control: Player 0" + "\n"
                                     + "Speed: 00 mph" + "\n"
                                     + "Score: 0000" + "\n"
                                     + "Shots left: 0");
        tf.backgroundColor = Color.White;
        AddChild(tf);
    }

    public void HandleBoarders()
    {
        if (_currentTank.position.x < 0)
        {
            _currentTank.position.x = 0;
            _currentTank.velocity.x = 0;
        }
        else if (_currentTank.position.x > game.width)
        {
            _currentTank.position.x = game.width;
            _currentTank.velocity.x = 0;
        }

        if (_currentTank.position.y < 0)
        {
            _currentTank.position.y = 0;
            _currentTank.velocity.y = 0;
        }
        else if (_currentTank.position.y > game.height)
        {
            _currentTank.position.y = game.height;
            _currentTank.velocity.y = 0;
        }

        for (int i = _bullets.Count - 1; i >= 0; i--)
        {
            if (_bullets[i].x > game.width + 100 || _bullets[i].x < - 100 || _bullets[i].y > game.height + 100 || _bullets[i].y < - 100)
            {
                _bullets[i].Destroy();
                _bullets.RemoveAt(i);
            }
        }
    }

    private void TestCollision()
    {
        //if (_bullet != null)
        //{
        //    _distanceX = _bullet.x - _test.x;
        //    _distanceY = _bullet.y - _test.y;
        //    _distanceTotal = Mathf.Sqrt(_distanceX * _distanceX + _distanceY * _distanceY);

        //    if (_distanceTotal < _test.radius)
        //    {
        //        _bullet.velocity.SetXY(0, 0);
        //    }
        //}

        //_distanceX = _currentTank.x - _test.x;
        //_distanceY = _currentTank.y - _test.y;
        //_distanceTotal = Mathf.Sqrt(_distanceX * _distanceX + _distanceY * _distanceY);

        //if (_distanceTotal < _test.radius)
        //{
        //    _currentTank.velocity.SetXY(0, 0);
        //    _currentTank.isActive = false;

        //    if (_currentTank.alpha > 0.05f && _currentTank.turret.alpha > 0.05f)
        //    {
        //        _currentTank.alpha *= 0.95f;
        //        _currentTank.turret.alpha *= 0.95f;
        //    }

        //    else
        //    {
        //        _currentTank.Destroy();
        //        _currentTank.turret.Destroy();

        //        if (_currentTank == _tank1)
        //        {
        //            _currentTank = _tank2;
        //            _tank2.isActive = true;
        //        }

        //        else if (_currentTank == _tank2)
        //        {
        //            _currentTank = _tank1;
        //            _tank1.isActive = true;
        //        }
        //    }
        //}
    }

    public void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0) && _bullet == null)
        {
            Bullet bullet = BulletFactory.Create(_currentTank.bulletType, _currentTank.position.Clone(), new Vec2(Input.mouseX - _currentTank.x, Input.mouseY - _currentTank.y));
            _bullets.Add(bullet);
            AddChild(bullet);
            _currentTank.bulletCount -= 1;
        }
    }

    private void CheckHitCollision()
    {
        foreach (Planet planet in _planets)
        {
            for (int i = _bullets.Count - 1; i >= 0; i--)
            {
                if (planet.Contains(_bullets[i].position))
                {
                    _bullets[i].Destroy();
                    _bullets.RemoveAt(i);
                }
            }
        }

        //foreach (Planet planet in _pm.GetAllPlanets())
        //{
        //    foreach (GameObject item in planet.GetCollisions())
        //    {
        //        if (item is Bullet)
        //        {
        //            _currentTank.score += 10;
        //            _planet.health--;
        //            Console.WriteLine(_planet.health);
        //            (item as Bullet).Destroy();
        //        }

        //        if (item is Tank)
        //        {
        //            (item as Tank).Destroy();
        //        }
        //    }
    }


    //if (item is Tank && item != _currentTank)
    //{
    //    (item as Bullet).sfxDestroyed.Play();
    //    _currentTank.score += 50;
    //    (item as Bullet).Destroy();
    //}

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
            _currentTank.velocity.SetXY(0, 0);
            _currentTank = _tank1;
            _tank2.bulletCount = 5;
            _tank1.isActive = true;
            _tank2.isActive = false;
        }

        if (_currentTank == _tank1 && _currentTank.bulletCount <= 0)
        {
            _currentTank.velocity.SetXY(0, 0);
            _currentTank = _tank2;
            _tank1.bulletCount = 5;
            _tank1.isActive = false;
            _tank2.isActive = true;
        }
    }

    private void HandleGravity()
    {
        foreach(Planet planet in _planets)
        {
            foreach (Bullet bullet in _bullets)
            {
                Vec2 gravityVector = new Vec2(planet.x - bullet.x, planet.y - bullet.y);
                gravityVector.Normalize();
                gravityVector.Scale(GRAVITATIONAL_FORCE / Mathf.Pow(bullet.DistanceTo(planet), 2));
                bullet.velocity.Add(gravityVector);
            }
        }
    }

    private void Update()
    {
        HandleAttack();
        HandleBoarders();
        HandleHUD();
        CheckHitCollision();
        TurnCheck();
        TestCollision();
        HandleGravity();
    }
}