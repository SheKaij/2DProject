using System;
using System.Drawing;
using GXPEngine;
using assignment_2.Classes;
using System.Collections.Generic;
using static Planet;

public class Level : GameObject
{
    private MyGame _myGame;

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
	private List<Spaceship> _spaceships;

    private Bullet _bullet = null;
    private FadeOut _fg;

    private int _timer;
    private int _turnTimer = 30;
	private int _destroyTimer = 100;

    private Button _exitButton;
    private HUD _hud;
    private ExitWindow _exitWindow;

    public Level(MyGame pMyGame) : base()
    {
        _myGame = pMyGame;

        _bgMusicSound = new Sound("assets\\sfx\\placeholder_music2.mp3", true, true);
        //_playMusic = _bgMusicSound.Play();

        _background1 = new Background();
        AddChild(_background1);

		_spaceships = new List<Spaceship>();

        _spaceship1 = new Spaceship(new Vec2(game.width * 0.1f, game.height * 0.4f), 0, true);
		_spaceships.Add(_spaceship1);
        AddChild(_spaceship1);
        _currentSpaceship = _spaceship1;

        _spaceship2 = new Spaceship(new Vec2(game.width * 0.9f, game.height * 0.6f), 180, false);
		_spaceships.Add(_spaceship2);
        AddChild(_spaceship2);

        _planets = new List<Planet>();
        _bullets = new List<Bullet>();

        Planet planet = PlanetFactory.Create(PlanetType.SMALL, new Vec2(game.width * 0.3f, game.height * 0.2f));
        _planets.Add(planet);
        AddChild(planet);

        planet = PlanetFactory.Create(PlanetType.MEDIUM, new Vec2(game.width * 0.2f, game.height * 0.8f));
        _planets.Add(planet);
        AddChild(planet);

        planet = PlanetFactory.Create(PlanetType.BIG, new Vec2(game.width * 0.5f, game.height * 0.7f));
        _planets.Add(planet);
        AddChild(planet);

        planet = PlanetFactory.Create(PlanetType.LARGE, new Vec2(game.width * 0.8f, game.height * 0.3f));
        _planets.Add(planet);
        AddChild(planet);

        _exitButton = new Button("assets/menu/exit_button2.png");
        AddChild(_exitButton);
        _exitButton.x = game.width - (_exitButton.radius * 2);
        _exitButton.y += _exitButton.radius * 2;

        _hud = new HUD(this);
        AddChild(_hud);
        _hud.x = game.width / 2 - _hud.width / 2;

        _exitWindow = new ExitWindow(_myGame);

        _fg = new FadeOut();
        AddChild(_fg);
    }

    public int GetBulletCount()
    {
        return _currentSpaceship.bulletCount;
    }

    public string GetTurnTimer()
    {
        return (_turnTimer).ToString();
    }

    public string GetCurrentPlayer()
    {
        if (_currentSpaceship == _spaceship1)
        {
            return "1";
        }
        else if (_currentSpaceship == _spaceship2)
        {
            return "2";
        }
        else
        {
            return "N/A";
        }
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

    private void HandleButtons()
    {
        if (Input.GetMouseButtonUp(0) && _exitButton.MouseHover() && _exitWindow.windowActive == false)
        {
            _exitWindow = new ExitWindow(_myGame);
            AddChildAt(_exitWindow, 300);
            _exitWindow.windowActive = true;
            _currentSpaceship.isActive = false;
        }

        if (_exitWindow.windowActive == false)
        {
            _currentSpaceship.isActive = true;
        }
    }

    public void HandleAttack()
    {

        if (Input.GetMouseButtonDown(0) && _bullet == null && _exitButton.MouseHover() == false)
        {
            Bullet bullet = BulletFactory.Create(_currentSpaceship.bulletType, _currentSpaceship.position.Clone(), new Vec2(Input.mouseX - _currentSpaceship.x, Input.mouseY - _currentSpaceship.y));
            _bullets.Add(bullet);
        }
    }

    private void HudOpacity()
    {
        if (_spaceship1.x < _hud.x + _hud.width && _spaceship1.x > _hud.x && _spaceship1.y < _hud.y + _hud.height)
        {
            _hud.alpha -= 0.01f;
            if (_hud.alpha <= 0.5f)
            {
                _hud.alpha = 0.5f;
            }
        }

        else if (_spaceship2.x < _hud.x + _hud.width && _spaceship2.x > _hud.x && _spaceship2.y < _hud.y + _hud.height)
        {
            _hud.alpha -= 0.01f;
            if (_hud.alpha <= 0.5f)
            {
                _hud.alpha = 0.5f;
            }
        }

        else
        {
            _hud.alpha += 0.05f;
            if (_hud.alpha >= 1)
            {
                _hud.alpha = 1;
            }
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
			if (planet.Contains(_currentSpaceship.position))
			{
				_destroyTimer--;
				_currentSpaceship.alpha = _destroyTimer / 100f;
				_currentSpaceship.Destroy();
				_myGame.SetState(MyGame.GameState.RESULT);
			}
		}

		foreach (Spaceship spaceship in _spaceships)
		{
			for (int i = _bullets.Count - 1; i >= 0; i--)
			{
				if (spaceship != _currentSpaceship)
				{
					if (_bullets[i].x >= spaceship.x - spaceship.width / 2 && _bullets[i].x <= spaceship.x + spaceship.width / 2 && _bullets[i].y <= spaceship.y + spaceship.height / 2 && _bullets[i].y >= spaceship.y - spaceship.height)
					{
						_bullets[i].Destroy();
						_bullets.RemoveAt(i);
						spaceship.Destroy();
						_myGame.SetState(MyGame.GameState.RESULT);
					}
				}
			}
		}
    }

    public void ScoreIncrease()
    {
        _currentSpaceship.score += 100;
    }

    private void TurnCheck()
    {
        if (_currentSpaceship == _spaceship2 && _currentSpaceship.bulletCount <= 0)
        {
            _timer = 0;
            _turnTimer = 30;
            _currentSpaceship.velocity.SetXY(0, 0);
            _currentSpaceship = _spaceship1;
            _spaceship2.bulletCount = 5;
            _spaceship1.isActive = true;
            _spaceship2.isActive = false;
        }

        if (_currentSpaceship == _spaceship1 && _currentSpaceship.bulletCount <= 0)
        {
            _timer = 0;
            _turnTimer = 30;
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

    private void TurnHandler()
    {
        _timer++;

        if (_timer == 60)
        {
            _turnTimer--;
            _timer = 0;
        }

        if (_turnTimer == 0)
        {
            _turnTimer = 30;

            if (_currentSpaceship == _spaceship2)
            {
                _currentSpaceship.velocity.SetXY(0, 0);
                _currentSpaceship = _spaceship1;
                _spaceship2.bulletCount = 5;
                _spaceship1.isActive = true;
                _spaceship2.isActive = false;
            }

            else if (_currentSpaceship == _spaceship1)
            {
                _currentSpaceship.velocity.SetXY(0, 0);
                _currentSpaceship = _spaceship2;
                _spaceship1.bulletCount = 5;
                _spaceship1.isActive = false;
                _spaceship2.isActive = true;
            }
        }
    }
    private void Update()
    {
        HandleButtons();

        if (_exitWindow.windowActive == false)
        {
            TurnCheck();
            TurnHandler();
            HandleAttack();
        }

        HandleGravity();
        CheckHitCollision();
        HandleBoarders();
        HudOpacity();

        if (Input.GetKeyDown(Key.R))
        {
            _myGame.SetState(MyGame.GameState.RESULT);
        }
    }
}