﻿using System;
using System.Drawing;
using GXPEngine;
using assignment_2.Classes;
using System.Collections.Generic;
using static Planet;
using static Bullet;

public class Level : GameObject
{
    private MyGame _myGame;

    private const float GRAVITATIONAL_FORCE = 25000f;
    private const float ELACITY = 0.85f;
    private const float FRICTION = 0.9995f;

    private Background _background1;
    private Sound _bgMusicSound;
    private Sound _ricochetSound;
    public SoundChannel playMusic { get; set; }

    private Spaceship _currentSpaceship;
	private Spaceship _spaceship1;
	private Spaceship _spaceship2;

    private List<Fragile> _fragiles;
    private List<Planet> _planets;
	private List<Spaceship> _spaceships;

    private Healthbar _healthbar1;
    private Healthbar _healthbar2;

    private Bullet bullet;
    private FadeOut _fg;

    private int _timer;
    private int _turnTimer = 30;

    private Button _exitButton;
    private HUD _hud;
    private ExitWindow _exitWindow;
    private bool _windowActive;

    private const float _gravForceConstant = 6.67408f * 10 - 11;

    public Level(MyGame pMyGame) : base()
    {
        _myGame = pMyGame;

		_background1 = new Background();
		AddChild(_background1);

        _fragiles = new List<Fragile>();

        _spaceships = new List<Spaceship>();
        
        _spaceship1 = new Spaceship("assets/spaceship/player_1.png", new Vec2(game.width * 0.1f, game.height * 0.4f), 0, true);
		_spaceships.Add(_spaceship1);
		AddChild(_spaceship1);
		_currentSpaceship = _spaceship1;
        
        _spaceship2 = new Spaceship("assets/spaceship/player_2.png", new Vec2(game.width * 0.9f, game.height * 0.6f), 180, false);
		_spaceships.Add(_spaceship2);
		AddChild(_spaceship2);

		_planets = new List<Planet>();

        Planet planet = PlanetFactory.Create(PlanetType.SMALL, new Vec2(game.width * 0.2f, game.height * 0.2f));
        _planets.Add(planet);
        AddChild(planet);

        planet = PlanetFactory.Create(PlanetType.SMALL, new Vec2(game.width * 0.8f, game.height * 0.8f));
        _planets.Add(planet);
        AddChild(planet);

        planet = PlanetFactory.Create(PlanetType.MEDIUM, new Vec2(game.width * 0.8f, game.height * 0.3f));
        _planets.Add(planet);
        AddChild(planet);

        planet = PlanetFactory.Create(PlanetType.BIG, new Vec2(game.width * 0.2f, game.height * 0.8f));
        _planets.Add(planet);
        AddChild(planet);

        planet = PlanetFactory.Create(PlanetType.LARGE, new Vec2(game.width * 0.5f, game.height * 0.5f));
        _planets.Add(planet);
        AddChild(planet);

        _exitButton = new Button("assets/menu/exit_button2.png");
		AddChild(_exitButton);
		_exitButton.x = game.width - (_exitButton.radius * 2);
		_exitButton.y += _exitButton.radius * 2;

        _healthbar1 = new Healthbar(_spaceship1);
        AddChild(_healthbar1);

        _healthbar2 = new Healthbar(_spaceship2);
        AddChild(_healthbar2);

        _hud = new HUD(this);
		AddChild(_hud);
		_hud.x = game.width / 2 - _hud.width / 2;

        _fg = new FadeOut();
		AddChild(_fg);

        _ricochetSound = new Sound("assets\\sfx\\placeholder_shoot2.wav");
        _bgMusicSound = new Sound("assets\\sfx\\levelmusic.mp3", true, true);
		playMusic = _bgMusicSound.Play();
	}

    public bool SetWindowActive(bool value)
    {
        _windowActive = value;
        return _windowActive;
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
        
        if (bullet != null && (bullet.x > game.width + 100 || bullet.x < -100 || bullet.y > game.height + 100 || bullet.y < -100))
        {
            bullet.Destroy();
            bullet = null;
        }

        for (int i = _fragiles.Count - 1; i >= 0; i--)
        {
            if (_fragiles[i].x > game.width + 100 || _fragiles[i].x < -100 || _fragiles[i].y > game.height + 100 || _fragiles[i].y < -100)
            {
                _fragiles[i].Destroy();
                _fragiles.RemoveAt(i);
            }
        }
    }
    
    private void HandlePlayerGlow()
    {
        if (_spaceship1.isActive == true)
        {
            _spaceship1.currentFrame = 1;
        }

        else
        {
            _spaceship1.currentFrame = 0;
        }

        if (_spaceship2.isActive == true)
        {
            _spaceship2.currentFrame = 1;
        }

        else
        {
            _spaceship2.currentFrame = 0;
        }
    }

    private void HandleButtons()
    {
        if (_windowActive == false)
        {
            _currentSpaceship.isActive = true;
        }

        if (Input.GetMouseButtonUp(0) && _exitButton.MouseHover() && _windowActive == false)
        {
            _exitWindow = new ExitWindow(_myGame, this);
            AddChildAt(_exitWindow, 300);
            _windowActive = true;
            _currentSpaceship.isActive = false;
        }
    }

    public void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0) && _exitButton.MouseHover() == false)
        {
            if (bullet == null)
            {
                if (_fragiles.Count == 0)
                {
                    bullet = BulletFactory.Create(_currentSpaceship.bulletType, _currentSpaceship.position.Clone(), new Vec2(Input.mouseX - _currentSpaceship.x, Input.mouseY - _currentSpaceship.y));
                    _currentSpaceship.bulletCount--;
                }
            }
            else
            {
                if (bullet is ClusterBullet)
                {
                    bullet.Destroy();
                    _fragiles.Add((Fragile) BulletFactory.Create(BulletType.FRAGILE, bullet.position.Clone(), new Vec2()));
                    _fragiles[0].velocity = bullet.velocity.Clone();
                    _fragiles.Add((Fragile)BulletFactory.Create(BulletType.FRAGILE, bullet.position.Clone(), new Vec2()));
                    _fragiles[1].velocity = bullet.velocity.RotateDegrees(20);
                    _fragiles.Add((Fragile)BulletFactory.Create(BulletType.FRAGILE, bullet.position.Clone(), new Vec2()));
                    _fragiles[2].velocity = bullet.velocity.RotateDegrees(-20);
                    _fragiles.Add((Fragile)BulletFactory.Create(BulletType.FRAGILE, bullet.position.Clone(), new Vec2()));
                    _fragiles[3].velocity = bullet.velocity.RotateDegrees(40);
                    _fragiles.Add((Fragile)BulletFactory.Create(BulletType.FRAGILE, bullet.position.Clone(), new Vec2()));
                    _fragiles[4].velocity = bullet.velocity.RotateDegrees(-40);
                    bullet = null;
                }
            }
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

            if (bullet != null && planet.Contains(bullet.position))
            {
                planet.health -= bullet.damage;
                //_currentSpaceship.score += 10;  
                if (bullet.health > 0)
                {
                    --bullet.health;
                    _ricochetSound.Play();
                    if (bullet is RicochetBullet)
                    {
                        Vec2 normal = bullet.position.Clone().Substract(planet.position).Normalize();
                        bullet.velocity.Substract(normal.Scale(2 * bullet.velocity.Dotproduct(normal))).Scale(ELACITY);
                    }
                }
                else
                {
                    bullet.Destroy();
                    bullet = null;
                }
            }

            for (int i = _fragiles.Count - 1; i >= 0; i--)
            {
                if (planet.Contains(_fragiles[i].position))
                {
                    _fragiles[i].Destroy();
                    planet.health -= _fragiles[i].damage;
                    _fragiles.RemoveAt(i);
                }
            }

            if (planet.Contains(_currentSpaceship.position))
            {
                Vec2 normal = _currentSpaceship.position.Clone().Substract(planet.position).Normalize();
                _currentSpaceship.velocity.Substract(normal.Scale(2 * _currentSpaceship.velocity.Dotproduct(normal))).Scale(ELACITY);
                _currentSpaceship.health -= 2;
            }
        }

		foreach (Spaceship spaceship in _spaceships)
		{
            
            if (spaceship != _currentSpaceship)
            {
                if (bullet != null && (bullet.x >= spaceship.x - spaceship.width / 2 && bullet.x <= spaceship.x + spaceship.width / 2 && bullet.y <= spaceship.y + spaceship.height / 2 && bullet.y >= spaceship.y - spaceship.height))
                {
                    
                    spaceship.health -= bullet.damage;
                    bullet.Destroy();
                    bullet = null;
                }

                for (int i = _fragiles.Count - 1; i >= 0; i--)
                {
                    if (_fragiles[i].x >= spaceship.x - spaceship.width / 2 && _fragiles[i].x <= spaceship.x + spaceship.width / 2 && _fragiles[i].y <= spaceship.y + spaceship.height / 2 && _fragiles[i].y >= spaceship.y - spaceship.height)
                    {
                        spaceship.health -= _fragiles[i].damage;
                        _fragiles[i].Destroy();
                        _fragiles.RemoveAt(i);
                    }
                }
            }
        }
    }

    private void HealthCheck()
    {
        if (_spaceship1.health <= 0)
        {
            _myGame.playerTwoWon = true;
            _myGame.SetState(MyGame.GameState.STORE);
        }

        else if (_spaceship2.health <= 0)
        {
            _myGame.playerOneWon = true;
            _myGame.SetState(MyGame.GameState.STORE);
        }
    }

    public Spaceship GetCurrentShip()
    {
        return _currentSpaceship;
    }

    private void TurnCheck()
    {

        if (_currentSpaceship == _spaceship2 && _currentSpaceship.bulletCount <= 0 && bullet == null && _fragiles.Count == 0)
        {
            _timer = 0;
            _turnTimer = 30;
            _currentSpaceship.velocity.SetXY(0, 0);
            _currentSpaceship.bulletCount = _currentSpaceship.MAX_BULLET;
            _currentSpaceship.fuel = _currentSpaceship.MAX_FUEL;
            _currentSpaceship = _spaceship1;
            _spaceship1.isActive = true;
            _spaceship2.isActive = false;
            _currentSpaceship.bulletType = BulletType.STANDARD;
            //_hud.currentBullet.currentFrame = 0;
        }

        if (_currentSpaceship == _spaceship1 && _currentSpaceship.bulletCount <= 0 && bullet == null && _fragiles.Count == 0)
        {
            _timer = 0;
            _turnTimer = 30;
            _currentSpaceship.velocity.SetXY(0, 0);
            _currentSpaceship.bulletCount = _currentSpaceship.MAX_BULLET;
            _currentSpaceship.fuel = _currentSpaceship.MAX_FUEL;
            _currentSpaceship = _spaceship2;
            _currentSpaceship.bulletType = BulletType.STANDARD;
            _spaceship1.isActive = false;
            _spaceship2.isActive = true;
        }
    }

    private void HandleGravity()
    {
        foreach(Planet planet in _planets)
        {
            if (bullet != null)
            {
                Vec2 gravityVector = new Vec2(planet.x - bullet.x, planet.y - bullet.y);
                gravityVector.Normalize();
                gravityVector.Scale(GRAVITATIONAL_FORCE * planet.mass / Mathf.Pow(bullet.DistanceTo(planet), 2));
                bullet.velocity.Add(gravityVector).Scale(FRICTION);
            }
            foreach (Fragile fragile in _fragiles)
            {
                Vec2 gravityVector = new Vec2(planet.x - fragile.x, planet.y - fragile.y);
                gravityVector.Normalize();
                gravityVector.Scale(GRAVITATIONAL_FORCE * planet.mass / Mathf.Pow(fragile.DistanceTo(planet), 2));
                fragile.velocity.Add(gravityVector).Scale(FRICTION);
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
                _spaceship2.bulletCount = _spaceship2.MAX_BULLET;
				_spaceship2.fuel = _spaceship2.MAX_FUEL;
                _spaceship1.isActive = true;
                _spaceship2.isActive = false;
            }

            else if (_currentSpaceship == _spaceship1)
            {
                _currentSpaceship.velocity.SetXY(0, 0);
                _currentSpaceship = _spaceship2;
                _spaceship1.bulletCount = _spaceship1.MAX_BULLET;
				_spaceship1.fuel = _spaceship1.MAX_FUEL;
                _spaceship1.isActive = false;
                _spaceship2.isActive = true;
            }
        }
    }

    private void HandleHealthBars()
    {
        _healthbar1.x = _spaceship1.x - _spaceship1.width / 2;
        _healthbar1.y = _spaceship1.y - 150;

        _healthbar2.x = _spaceship2.x - _spaceship1.width / 2;
        _healthbar2.y = _spaceship2.y - 150;
    }
    private void Update()
    {
        if (_windowActive == false)
        {
            HandleAttack();
            TurnCheck();
            TurnHandler();
        }

        //if (Input.GetKeyDown(Key.S))
        //{
        //    _myGame.SetState(MyGame.GameState.STORE);
        //}

        HealthCheck();
        HandlePlayerGlow();
        HandleButtons();
        HandleGravity();
        CheckHitCollision();
        HandleBoarders();
        HudOpacity();
        HandleHealthBars();
    }
}