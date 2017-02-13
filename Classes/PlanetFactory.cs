using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Planet;

namespace assignment_2.Classes
{
    public static class PlanetFactory
    {
        public static Planet Create(PlanetType type, Vec2 position)
        {
            switch (type)
            {
                case PlanetType.SMALL:
                    return new Planet(position, 0.5f, 5, "assets\\prototype_planet.png");
                case PlanetType.MEDIUM:
                    return new Planet(position, 1f, 10, "assets\\prototype_planet.png");
                case PlanetType.BIG:
                    return new Planet(position, 1.5f, 15, "assets\\prototype_planet.png");
                case PlanetType.LARGE:
                    return new Planet(position, 2f, 20, "assets\\prototype_planet.png");
                default:
                    return null;
            }
        }
    }
}
