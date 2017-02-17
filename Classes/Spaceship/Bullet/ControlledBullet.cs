using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment_2.Classes
{
    class ControlledBullet : Bullet
    {
        private readonly float CONTROL_STRENGTH = 10f;
        private int controlCount = 4;

        public ControlledBullet(Vec2 position, Vec2 velocity) : base(
            position,
            velocity,
			2,
            0,
            "assets\\bullet.png",
            "assets\\sfx\\placeholder_shoot2.wav",
            "assets\\sfx\\placeholder_hit2.wav",
            0.02f) { }

        protected override void Move()
        {
            rotation = velocity.GetAngleDegrees();

            if (Input.GetMouseButtonDown(0) && controlCount > 0)
            {
                velocity.Add(new Vec2(Input.mouseX - x, Input.mouseY - y).Normalize().Scale(CONTROL_STRENGTH));
                --controlCount;
            }

            position.Add(velocity);
            x = position.x;
            y = position.y;
        }
    }
}
