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
                    return new Planet(position, 0.5f, 5, 0.2f, "assets\\planet_Tilesheets\\Planet_U_500.png");
                case PlanetType.MEDIUM:
                    return new Planet(position, 1f, 10, 0.4f, "assets\\planet_Tilesheets\\Planet_U_500.png");
                case PlanetType.BIG:
                    return new Planet(position, 2f, 15, 0.6f, "assets\\planet_Tilesheets\\Planet_U_500.png");
                case PlanetType.LARGE:
                    return new Planet(position, 8f, 20, 0.8f, "assets\\planet_Tilesheets\\Planet_U_500.png");
                default:
                    return null;
            }
        }
    }
}
