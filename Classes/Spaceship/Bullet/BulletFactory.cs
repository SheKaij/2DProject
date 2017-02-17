using GXPEngine;
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
                    return new StandardBullet(position, velocity);
				case BulletType.RICOCHET:
					return new RicochetBullet(position, velocity);
                case BulletType.CONTROLLED:
                    return new ControlledBullet(position, velocity);
                case BulletType.CLUSTER:
                    return new ClusterBullet(position, velocity);
                case BulletType.FRAGILE:
                    return new Fragile(position, velocity);
                default:
                    return null;
            }
        }
    }
}
