using System;

namespace GXPEngine
{
	public class Vec2 
	{
		public static Vec2 zero { get { return new Vec2(0,0); }}

		public float x = 0;
		public float y = 0;

		public Vec2 (float pX = 0, float pY = 0)
		{
			x = pX;
			y = pY;
		}

        public Vec2(Vec2 vector)
        {
            x = vector.x;
            y = vector.y;
        }

        public override string ToString ()
		{
			return String.Format ("({0}, {1})", x, y);
		}

        public static float Deg2Rad(float pDegrees)
        {
            pDegrees = pDegrees * (Mathf.PI / 180);
            return pDegrees;
        }

        public static float Rad2Deg(float pRadians)
        {
            pRadians = pRadians * (180 / Mathf.PI);
            return pRadians;
        }

        public static Vec2 GetUnitVectorRadians(float pRadians)
        {
            return new Vec2(Mathf.Cos(pRadians), Mathf.Sin(pRadians));
        }

        public static Vec2 GetUnitVectorDegrees(float pDegrees)
        {
            return GetUnitVectorRadians(Deg2Rad(pDegrees));
        }

        //public static float RandomUnitVector()
        //{
        //    float randomUnitVec = 
        //    return unitDegVec;
        //}

        public Vec2 Add (Vec2 other)    // Add the vector speed to the object
        {      
			x += other.x;
			y += other.y;
            return this;
		}

        public Vec2 Substract(Vec2 other)   // Substract the vector speed to the object
        {      
            x -= other.x;
            y -= other.y;
            return this;
        }

        public float Length ()          // Calculates the diagonal speed of the object
        {
            //Console.WriteLine("The velocity BEFORE the normalization and scaling: " + _vecLength);
            return Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2));
        }

        public Vec2 Scale (float scalar)    // Scale normalized vector to desired speed, parse in the desired speed
        {
            x *= scalar;
            y *= scalar;
            return this;
        }

        public Vec2 Normalize ()    // Normalize vector to one unit/component for scaling purposes
        {
            // Readjustments were made: normalize now always returns a float value, not null anymore
            float getLength = Length(); // Store the length so that that doesn't get changed when we change the x value
            if (getLength != 0)
            {
                x = x / getLength;
                y = y / getLength;
            }
               return this;
        }

        public Vec2 Clone()         // Instantiates a new vector, which copies the properties of the parsed in other vector
        {
            Vec2 clonedVec = new Vec2(x, y);
            return clonedVec;
        }

        public void SetXY(float newX, float newY)       // Assign a new XY to the object
        {
            x = newX;
            y = newY;
        }

        public float GetAngleDegrees()
        {
            float getAngleDeg = Mathf.Atan2(y, x);
            getAngleDeg *= 180 / Mathf.PI;
            return getAngleDeg;
        }

        public float GetAngleRadians()
        {
            float getAngleRad = Mathf.Atan2(y, x);
            return getAngleRad;
        }

        public void SetAngleDegrees(float newAngleDegrees)
        {
            //
        }

        public void SetAngleRadians(float newAngleRadians)
        {
            //
        }

        public Vec2 RotateDegrees(float degrees)
        {
            return RotateRadians(Deg2Rad(degrees));
        }

        public Vec2 RotateRadians(float radiants)
        {
            return new Vec2(this.x * Mathf.Cos(radiants) - this.y * Mathf.Sin(radiants), this.x * Mathf.Sin(radiants) + this.y * Mathf.Cos(radiants));
        }

        public Vec2 RotateAroundDegrees(float px, float py, float degrees)
        {
            float pe = Mathf.Cos(degrees) * (x - px) - Mathf.Sin(degrees) * (y - py) + px;
            float pd = Mathf.Sin(degrees) * (x - px) + Mathf.Cos(degrees) * (y - py) + py;
            return new Vec2(pe, pd);
        }

        public void RotateAroundRadians()
        {

        }

        public float Dotproduct(Vec2 vector)
        {
            return this.Length() * vector.Length() * (Mathf.Cos(this.GetAngleRadians() - vector.GetAngleRadians()));
        }
    }
}