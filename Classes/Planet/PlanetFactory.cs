﻿using GXPEngine;
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
                    return new Planet(position, 0.5f, 10, 0.25f, "assets\\planet_Tilesheets\\Planet_U_500.png");
                case PlanetType.MEDIUM:
                    return new Planet(position, 1f, 20, 0.75f, "assets\\planet_Tilesheets\\Planet_L_500.png");
                case PlanetType.BIG:
                    return new Planet(position, 2f, 40, 1f, "assets\\planet_Tilesheets\\Planet_E_500.png");
                case PlanetType.LARGE:
                    return new Planet(position, 4f, 80, 1.25f, "assets\\planet_Tilesheets\\Planet_Q_500.png");
                default:
                    return null;
            }
        }
    }
}
