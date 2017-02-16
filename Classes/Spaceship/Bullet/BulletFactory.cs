﻿using GXPEngine;
using static Bullet;

namespace assignment_2.Classes
{
    public static class BulletFactory
    {
        public static Bullet Create(BulletType type, Vec2 position, Vec2 velocity)
        {
            switch (type)
            {
                case BulletType.STANDARD :
                    return new StandardBullet(position, velocity, 1f);
				case BulletType.RICOCHET:
					return new RicochetBullet(position, velocity, 0.5f);
                case BulletType.THIRD:
                    return new StandardBullet(position, velocity, 1f);
                case BulletType.FOURTH:
                    return new StandardBullet(position, velocity, 1f);
                default:
                    return null;
            }
        }
    }
}
