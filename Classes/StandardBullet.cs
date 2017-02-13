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
        public StandardBullet() : base ("assets\\bullet.png")
        {
            _shotSound = new Sound("assets\\sfx\\placeholder_shoot2.wav");
            _hitSound = new Sound("assets\\sfx\\placeholder_hit2.wav", false, true);
            _speed = 0.05f;
            
            _shotSound.Play();
        }

        protected override void Move()
        {
            rotation = velocity.GetAngleDegrees();

            position.Add(velocity);
            x = position.x;
            y = position.y;
        }
    }
}
