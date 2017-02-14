using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                default:
                    return null;
            }
        }
    }
}
