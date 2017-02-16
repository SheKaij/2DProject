using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment_2.Classes
{
    public class StandardBullet : Bullet
    {
        public StandardBullet(Vec2 position, Vec2 velocity, float damage) : base(
            position,
            velocity,
			damage,
            1,
            "assets\\bullet.png",
            "assets\\sfx\\placeholder_shoot2.wav",
            "assets\\sfx\\placeholder_hit2.wav",
            0.04f) { }

        protected override void Move()
        {
            rotation = velocity.GetAngleDegrees();

            position.Add(velocity);
            x = position.x;
            y = position.y;
        }
    }
}
