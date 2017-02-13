using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using System.Collections.ObjectModel;

public class PlanetManager : GameObject
{
    private const int TOTAL_AMOUNT_PLANETS = 6;
    private const int SMALL_PLANET_COUNT = 3;
    private const int MEDIUM_PLANET_COUNT = 2;
    private const int LARGE_PLANET_COUNT = 1;

    private List<Planet> _listOfPlanets = new List<Planet>();
    private Planet _planet;
    private int[] _planetPosition = new int[] { 0, 0 };

    public PlanetManager()
    {
        // empty
    }

    public void createPlanets()
    {
        for (int i = 0; i < SMALL_PLANET_COUNT; i++)
        {
            _planet = new Planet(new Vec2(game.width * 0.5f, game.height / 2), 10);
            game.AddChild(_planet);
            _listOfPlanets.Add(_planet);

            _planet.SetXY(Utils.Random(_planet.width, game.width - _planet.width), Utils.Random(_planet.height, game.height));
        }
    }

    private void Update()
    {
        // empty
    }

    public Planet GetPlanet()
    {
        return _planet;
    }

    public List<Planet> GetAllPlanets()
    {
        return _listOfPlanets;
    }
}
