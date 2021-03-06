﻿using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment_2.Classes
{
	public class RicochetBullet : Bullet
	{
		public RicochetBullet(Vec2 position, Vec2 velocity) : base(
            position,
            velocity,
			3,
            4,
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
