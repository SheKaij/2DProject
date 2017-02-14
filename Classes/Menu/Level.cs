using System;
using System.Drawing;
using GXPEngine;
using assignment_2.Classes;
using System.Collections.Generic;
using static Planet;

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

	private Spaceship _currentSpaceship;
	private Spaceship _spaceship1;
	private Spaceship _spaceship2;
    private List<Planet> _planets;
    private List<Bullet> _bullets;

    private Bullet _bullet = null;
    private TextField tf = null;

    private string _currentPlayer;

    private const float _gravForceConstant = 6.67408f * 10 - 11;

    private Level() : base()
    {
        _bgMusicSound = new Sound("assets\\sfx\\placeholder_music2.mp3", true, true);
        //_playMusic = _bgMusicSound.Play();

        _background1 = new Background();
        AddChild(_background1);

        _spaceship1 = new Spaceship(new Vec2(game.width * 0.1f, game.height * 0.4f), 0, true);
        AddChild(_spaceship1);
        _currentSpaceship = _spaceship1;

        _spaceship2 = new Spaceship(new Vec2(game.width * 0.9f, game.height * 0.6f), 180, false);
        AddChild(_spaceship2);

        _planets = new List<Planet>();
        _bullets = new List<Bullet>();

        Planet planet = PlanetFactory.Create(PlanetType.SMALL, new Vec2(game.width * 0.3f, game.height * 0.2f));
        _planets.Add(planet);
        AddChild(planet);

        planet = PlanetFactory.Create(PlanetType.MEDIUM, new Vec2(game.width * 0.2f, game.height * 0.8f));
        _planets.Add(planet);
        AddChild(planet);

        planet = PlanetFactory.Create(PlanetType.BIG, new Vec2(game.width * 0.5f, game.height * 0.6f));
        _planets.Add(planet);
        AddChild(planet);

        planet = PlanetFactory.Create(PlanetType.LARGE, new Vec2(game.width * 0.7f, game.height * 0.4f));
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
        if (_currentSpaceship.position.x < 0)
        {
            _currentSpaceship.position.x = 0;
            _currentSpaceship.velocity.x = 0;
        }
        else if (_currentSpaceship.position.x > game.width)
        {
            _currentSpaceship.position.x = game.width;
            _currentSpaceship.velocity.x = 0;
        }

        if (_currentSpaceship.position.y < 0)
        {
            _currentSpaceship.position.y = 0;
            _currentSpaceship.velocity.y = 0;
        }
        else if (_currentSpaceship.position.y > game.height)
        {
            _currentSpaceship.position.y = game.height;
            _currentSpaceship.velocity.y = 0;
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

        //_distanceX = _currentSpaceship.x - _test.x;
        //_distanceY = _currentSpaceship.y - _test.y;
        //_distanceTotal = Mathf.Sqrt(_distanceX * _distanceX + _distanceY * _distanceY);

        //if (_distanceTotal < _test.radius)
        //{
        //    _currentSpaceship.velocity.SetXY(0, 0);
        //    _currentSpaceship.isActive = false;

        //    if (_currentSpaceship.alpha > 0.05f && _currentSpaceship.turret.alpha > 0.05f)
        //    {
        //        _currentSpaceship.alpha *= 0.95f;
        //        _currentSpaceship.turret.alpha *= 0.95f;
        //    }

        //    else
        //    {
        //        _currentSpaceship.Destroy();
        //        _currentSpaceship.turret.Destroy();

        //        if (_currentSpaceship == _spaceship1)
        //        {
        //            _currentSpaceship = _spaceship2;
        //            _spaceship2.isActive = true;
        //        }

        //        else if (_currentSpaceship == _spaceship2)
        //        {
        //            _currentSpaceship = _spaceship1;
        //            _spaceship1.isActive = true;
        //        }
        //    }
        //}
    }

    public void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0) && _bullet == null)
        {
            Bullet bullet = BulletFactory.Create(_currentSpaceship.bulletType, _currentSpaceship.position.Clone(), new Vec2(Input.mouseX - _currentSpaceship.x, Input.mouseY - _currentSpaceship.y));
            _bullets.Add(bullet);
            AddChild(bullet);
            _currentSpaceship.bulletCount -= 1;
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
					planet.health -= _bullets[i].damage;
					_currentSpaceship.score += 10;
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
        //            _currentSpaceship.score += 10;
        //            _planet.health--;
        //            Console.WriteLine(_planet.health);
        //            (item as Bullet).Destroy();
        //        }

        //        if (item is Spaceship)
        //        {
        //            (item as Spaceship).Destroy();
        //        }
        //    }
    }


    //if (item is Spaceship && item != _currentSpaceship)
    //{
    //    (item as Bullet).sfxDestroyed.Play();
    //    _currentSpaceship.score += 50;
    //    (item as Bullet).Destroy();
    //}

    public void ScoreIncrease()
    {
        _currentSpaceship.score += 100;
    }

    private void HandleHUD()
    {
        if (_currentSpaceship == _spaceship1) { _currentPlayer = "1"; }
        else if (_currentSpaceship == _spaceship2) { _currentPlayer = "2"; }

        tf.text = "Control: Player " + _currentPlayer + "\n"
                + "Speed: " + Math.Abs(Math.Round(_currentSpaceship.velocity.x)) + "  Mph" + "\n"
                + "Score: " + _currentSpaceship.score + "\n"
                + "Shots left: " + _currentSpaceship.bulletCount;
    }

    private void TurnCheck()
    {
        if (_currentSpaceship == _spaceship2 && _currentSpaceship.bulletCount <= 0)
        {
            _currentSpaceship.velocity.SetXY(0, 0);
            _currentSpaceship = _spaceship1;
            _spaceship2.bulletCount = 5;
            _spaceship1.isActive = true;
            _spaceship2.isActive = false;
        }

        if (_currentSpaceship == _spaceship1 && _currentSpaceship.bulletCount <= 0)
        {
            _currentSpaceship.velocity.SetXY(0, 0);
            _currentSpaceship = _spaceship2;
            _spaceship1.bulletCount = 5;
            _spaceship1.isActive = false;
            _spaceship2.isActive = true;
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
                gravityVector.Scale(GRAVITATIONAL_FORCE * planet.mass / Mathf.Pow(bullet.DistanceTo(planet), 2));
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